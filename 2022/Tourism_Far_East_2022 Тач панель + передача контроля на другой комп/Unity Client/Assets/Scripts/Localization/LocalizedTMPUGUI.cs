using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace Tourism.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTMPUGUI : MonoBehaviour, ILocalized
    {
        [SerializeField] private string term;
        [SerializeField] public string localization;
        private TextMeshProUGUI textMeshProUGUI;
        public string Term => term;
        private void Start()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            LocalizationManager.OnLanguageChanged += Localize;
            Localize();
        }

        private void OnDestroy()
        {
            LocalizationManager.OnLanguageChanged -= Localize;
        }
        public void SetTerm(string newTerm)
        {
            term = newTerm;
            Localize();
        }
        private void Localize()
        {
            textMeshProUGUI.text = LocalizationManager.LocalizeString(term);
        }

        private void OnValidate()
        {
            localization = LocalizationManager.LocalizeString(term);
            GetComponent<TextMeshProUGUI>().text = localization;
        }
    }
}