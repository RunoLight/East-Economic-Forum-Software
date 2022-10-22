using System;
using System.Collections;
using System.IO;
using System.Net;
using Tourism.KeyCode;
using Tourism.Localization;
using Tourism.Services;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Tourism.VideoComponents
{
    public class VideoPlayer : MonoBehaviour
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button localizationButton;
        [SerializeField] private AudioClip buttonClickAudio;
        [SerializeField] private string audioPath;
        public event Action OnAnimationsCancelRequested;
        public bool isAnyVideoPlaying { get; set; } = false;

        private void Start()
        {
            StartCoroutine(LoadClickSound());
        }

        public void SendPlayCommand(VideoTag videoTag)
        {
            SwitchButtonsVisible(true);
            SendCommandToAspClient(LocalizationManager.Language == LocalizationLanguage.Russian ? videoTag.ruCode : videoTag.enCode);
        }
        public void SendStopCommand()
        {
            CancelAnimations();
            SendCommandToAspClient(AppServices.I.CancelVideoKeyCode);
        }

        private async void SendCommandToAspClient(VirtualKeyCode keyCode)
        {
            PlayAudioOnClick();
            isAnyVideoPlaying = true;
            var videoKeyHex = ((int)keyCode).ToString("x8");
            var url = $"http://{AppServices.I.serverUrl}:{AppServices.I.serverPort}/KeyPress?keycode={videoKeyHex}";
            Debug.Log($"Send to ASP Client: {url}. Button: {keyCode.ToString()}");
            var request = (HttpWebRequest) WebRequest.Create(url);
            WebResponse response = await request.GetResponseAsync();
            Debug.Log(((HttpWebResponse)response).StatusDescription);
            response.Close();
        }

        public void CancelAnimations()
        {
            isAnyVideoPlaying = false;
            OnAnimationsCancelRequested?.Invoke();
            SwitchButtonsVisible(false);
        }

        private void SwitchButtonsVisible(bool isVideoPlaying)
        {
            homeButton.gameObject.SetActive(isVideoPlaying);
            localizationButton.gameObject.SetActive(!isVideoPlaying);
        }

        private void PlayAudioOnClick()
        {
            AppServices.I.AudioManager.PlaySound(buttonClickAudio);
        }
        
        IEnumerator LoadClickSound()
        {
            string audioUrl = Application.streamingAssetsPath+ "/" +audioPath;
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.UNKNOWN))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    buttonClickAudio = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }
    }
}