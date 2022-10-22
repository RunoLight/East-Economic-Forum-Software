using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Tourism.Localization;
using Tourism.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Tourism.VideoComponents
{
    [RequireComponent(typeof(Button))]
    public class VideoButtonIdentifier : MonoBehaviour
    {
        [SerializeField] private VideoTag videoTag;
        private Button button;
        private UnityEngine.Video.VideoPlayer pulseVideo;
        private CanvasGroup pulseCanvasGroup;
        private CanvasGroup parentCanvasGroup;
        private CanvasGroup inactiveCanvasGroup;
        private CanvasGroup activeCanvasGroup;
        private TextMeshProUGUI buttonText;
        private bool stopAnimation;
        private bool isCurrentButtonActive = false;

        private void Awake()
        {
            button = GetComponent<Button>();
            pulseVideo = GetComponentInChildren<UnityEngine.Video.VideoPlayer>();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            pulseCanvasGroup = pulseVideo.GetComponent<CanvasGroup>();
            parentCanvasGroup = GetComponentInParent<CanvasGroup>();
            inactiveCanvasGroup = transform.GetChild(1).GetComponent<CanvasGroup>();
            activeCanvasGroup = transform.GetChild(2).GetComponent<CanvasGroup>();
            button.onClick.AddListener(PlayVideoWithTag);
        }

        private void Start()
        {
            AppServices.I.VideoPlayer.OnAnimationsCancelRequested += StopAnimation;
        }

        private void StopAnimation()
        {
            stopAnimation = true;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(PlayVideoWithTag);
            AppServices.I.VideoPlayer.OnAnimationsCancelRequested -= StopAnimation;
        }

        private async void PlayVideoWithTag()
        {
            if (isCurrentButtonActive)
            {
                return;
            }
            isCurrentButtonActive = true;
            AppServices.I.VideoPlayer.CancelAnimations();
            AppServices.I.VideoPlayer.SendPlayCommand(videoTag);
            AppServices.I.IsButtonsLocked = true;
            parentCanvasGroup.interactable = false;
            AlphaFade(inactiveCanvasGroup, 1f, 0f, 0.5f);
            AlphaFade(activeCanvasGroup, 0f, 1f, 0.5f);
            FontSizeFade(buttonText, 20, 24, 0.4f);
            activeCanvasGroup.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 0.5f);
            await Task.Delay(500);
            parentCanvasGroup.interactable = true;
            AppServices.I.IsButtonsLocked = false;
            stopAnimation = false;
            PlayPulseAnimation();
        }

        private async void PlayPulseAnimation()
        {
            var evaluatedTime = 0f;
            AlphaFade(pulseCanvasGroup, 0f,1f, 0.5f);
            pulseVideo.Play();
            while (evaluatedTime < videoTag.videoTime && !stopAnimation)
            {
                var startTime = Time.time;
                await Task.Yield();
                evaluatedTime += Time.time - startTime;
            }

            if (evaluatedTime > videoTag.videoTime)
            {
                AppServices.I.VideoPlayer.isAnyVideoPlaying = false;
            }
            if (!AppServices.I.VideoPlayer.isAnyVideoPlaying)
            {
                AppServices.I.VideoPlayer.CancelAnimations();
                Debug.LogWarning("Sending to home video");
                AppServices.I.VideoPlayer.SendStopCommand();
            }

            stopAnimation = false;
            isCurrentButtonActive = false;
            activeCanvasGroup.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            AlphaFade(pulseCanvasGroup, 1f,0f, 0.5f);
            AlphaFade(inactiveCanvasGroup, 0f, 1f, 0.5f);
            AlphaFade(activeCanvasGroup, 1f, 0f, 0.5f);
            FontSizeFade(buttonText, 24, 20, 0.4f);
            pulseVideo.Stop();
        }

        private async void AlphaFade(CanvasGroup canvas, float startValue, float endValue, float duration)
        {
            var evaluatedTime = 0f;
            while (evaluatedTime < duration)
            {
                var startTime = Time.time;
                canvas.alpha = Mathf.Lerp(startValue, endValue, evaluatedTime / duration);
                await Task.Yield();
                evaluatedTime += Time.time - startTime;
            }
        }
        private async void FontSizeFade(TMP_Text tmrpo, float startValue, float endValue, float duration)
        {
            var evaluatedTime = 0f;
            while (evaluatedTime < duration)
            {
                var startTime = Time.time;
                tmrpo.fontSize = Mathf.Lerp(startValue, endValue, evaluatedTime / duration);
                await Task.Yield();
                evaluatedTime += Time.time - startTime;
            }
        }
    }
}