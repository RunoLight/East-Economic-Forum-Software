using Tourism.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Tourism.VideoComponents
{
    [RequireComponent(typeof(Button))]
    public class HomeButtonIdentifier : MonoBehaviour
    {
        private Button homeButton;

        private void Awake()
        {
            homeButton = GetComponent<Button>();
            homeButton.onClick.AddListener(OpenIdleVideo);
        }

        private void OnDestroy()
        {
            homeButton.onClick.RemoveListener(OpenIdleVideo);
        }

        private void OpenIdleVideo()
        {
            if (AppServices.I.IsButtonsLocked) return;
            
            AppServices.I.VideoPlayer.CancelAnimations();
            AppServices.I.VideoPlayer.SendStopCommand();
        }
    }
}