using System;
using UnityEngine;

namespace Tourism.Localization
{
    [Serializable]
    public struct LocalizationImageTerm
    {
        public string Term;
        public Sprite EngLocalize;
        public string EngLocalizePath;
        public Sprite RuLocalize;
        public string RuLocalizePath;
    }
}