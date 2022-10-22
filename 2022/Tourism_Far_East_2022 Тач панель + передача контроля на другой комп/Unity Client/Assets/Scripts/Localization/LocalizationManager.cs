using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Tourism.Localization
{
    public static class LocalizationManager
    {
        public static LocalizationLanguage Language;
        private const LocalizationLanguage DefaultLanguage = LocalizationLanguage.Russian;
        private static LocalizationAsset localizationAsset;

        // public static event Action<LocalizationLanguage> OnLanguageChanged;
        public static event Action OnLanguageChanged;

        public static void Initialize()
        {
            Language = DefaultLanguage;
            localizationAsset = ResourcesLoader.ResourceLoader.LocalizationAsset;
            localizationAsset.Initialize();
        }
        public static void SwitchLanguage()
        {
            Language = Language == LocalizationLanguage.Russian ? LocalizationLanguage.English : LocalizationLanguage.Russian;
            OnLanguageChanged?.Invoke();
        }

        public static string LocalizeString(string term)
        {
            if (localizationAsset == null)
            {
                Initialize();
            }
            return localizationAsset.LocalizeString(term, Language);
        }
        public static async Task<Sprite> LocalizeImage(string term)
        {
            if (localizationAsset == null)
            {
                Initialize();
            }

            while (localizationAsset.isInitialized == false)
            {
                await Task.Delay(10);
            }

            return localizationAsset.LocalizeImage(term, Language);
        }
    }
}