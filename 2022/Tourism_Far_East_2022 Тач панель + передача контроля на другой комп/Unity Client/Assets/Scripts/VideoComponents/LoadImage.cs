using UnityEngine;
using Tourism.Localization;
using UnityEngine.UI;


namespace Tourism.VideoComponents
{
    public class LoadImage : MonoBehaviour
    {
        public Image image;
        public string path;
        public string regionName;
        public bool activeImageVariant;

        [ContextMenu("Get video player from this component")]
        private void CacheVideoPlayer()
        {
            image = GetComponent<Image>();
        }

        async void Start()
        {
            string url = path + regionName + (activeImageVariant ? "_active" : "") + ".png";
            var sprite = await ImageDownloadHelper.LoadSprite(url);
            image.sprite = sprite;
        }
    }
}