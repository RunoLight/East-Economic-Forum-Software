using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tourism.Localization
{
    // [CreateAssetMenu(fileName = "LocalizationAsset", menuName = "Localization/LocalizationAsset")]
    public class LocalizationAsset : ScriptableObject
    {
        public List<LocalizationStringTerm> StringTerms;
        public List<LocalizationImageTerm> ImageTerms;
        private Dictionary<string, LocalizationString> localizedStringDictionary;
        private Dictionary<string, LocalizationSprite> localizedImageDictionary;
        [NonSerialized] public bool isInitializationStarted = false;
        [NonSerialized] public bool isInitialized = false;
        
        public async void Initialize()
        {
            if (isInitializationStarted) return;
            isInitializationStarted = true;

            localizedStringDictionary = new Dictionary<string, LocalizationString>();
            foreach (var term in StringTerms)
            {
                localizedStringDictionary.Add(term.Term, new LocalizationString(term.RuLocalize, term.EngLocalize));
            }

            localizedImageDictionary = new Dictionary<string, LocalizationSprite>();
            localizedImageDictionary.Clear();
            foreach (var term in ImageTerms)
            {
                if (string.IsNullOrEmpty(term.EngLocalizePath) || string.IsNullOrEmpty(term.RuLocalizePath))
                {
                    localizedImageDictionary.Add(term.Term, new LocalizationSprite(term.RuLocalize, term.EngLocalize));
                    Debug.Log($"added {term.Term}");
                }
                else
                {
                    Sprite ruSprite =
                        await ImageDownloadHelper.LoadSprite(
                            Application.streamingAssetsPath + "/" + term.RuLocalizePath);
                    Sprite enSprite =
                        await ImageDownloadHelper.LoadSprite(
                            Application.streamingAssetsPath + "/" + term.EngLocalizePath);
                    localizedImageDictionary.Add(term.Term, new LocalizationSprite(ruSprite, enSprite));
                    Debug.Log($"added {term.Term}");
                }
            }

            isInitialized = true;
        }

        public string LocalizeString(string term, LocalizationLanguage language)
        {
            return localizedStringDictionary.TryGetValue(term, out var localization)
                ? localization.Localize(language)
                : "<--No translation-->";
        }

        public Sprite LocalizeImage(string term, LocalizationLanguage language)
        {
            return localizedImageDictionary.TryGetValue(term, out var localization)
                ? localization.Localize(language)
                : null;
        }

        private struct LocalizationString
        {
            public string RuLocalization;
            public string EngLocalization;

            public LocalizationString(string ruLocalization, string engLocalization)
            {
                RuLocalization = ruLocalization;
                EngLocalization = engLocalization;
            }

            public string Localize(LocalizationLanguage lang)
            {
                return lang switch
                {
                    LocalizationLanguage.Russian => RuLocalization,
                    LocalizationLanguage.English => EngLocalization,
                    _ => ""
                };
            }
        }

        private struct LocalizationSprite
        {
            public Sprite RuLocalization;
            public Sprite EngLocalization;

            public LocalizationSprite(Sprite ruLocalization, Sprite engLocalization)
            {
                RuLocalization = ruLocalization;
                EngLocalization = engLocalization;
            }

            public Sprite Localize(LocalizationLanguage lang)
            {
                return lang switch
                {
                    LocalizationLanguage.Russian => RuLocalization,
                    LocalizationLanguage.English => EngLocalization,
                    _ => null
                };
            }
        }
    }
}