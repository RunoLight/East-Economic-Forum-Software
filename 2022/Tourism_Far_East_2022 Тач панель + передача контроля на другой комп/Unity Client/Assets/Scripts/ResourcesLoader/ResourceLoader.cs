using UnityEngine;
using LocalizationAsset = Tourism.Localization.LocalizationAsset;

namespace Tourism.ResourcesLoader
{
    public static class ResourceLoader
    {
        private static LocalizationAsset cachedLocalizationAsset;
        public static LocalizationAsset LocalizationAsset 
        {
            get 
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    return AssetDatabaseUtils.GetAssetOfType<LocalizationAsset>();
                }
#endif
                if (!cachedLocalizationAsset)
                {
                    Debug.Log("LocalizationAsset will be loaded");
                    cachedLocalizationAsset = Resources.Load<LocalizationAsset>("LocalizationAsset");
                }
                return cachedLocalizationAsset;
            }
        }
    }
}