using Tourism.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Tourism.Localization
{
    [RequireComponent(typeof(Button))]
    public class LocalizationSwitcher : MonoBehaviour
    {
        private Button languageSwitcher;

        private void Awake()
        {
            languageSwitcher = GetComponent<Button>();
            languageSwitcher.onClick.AddListener(SwitchLanguage);
        }

        private void OnDestroy()
        {
            languageSwitcher.onClick.RemoveListener(SwitchLanguage);
        }

        private void SwitchLanguage()
        {
            LocalizationManager.SwitchLanguage();
            AppServices.I.VideoPlayer.SendStopCommand();
        }
    }
}