using System;
using System.IO;
using UnityEngine;

namespace Tourism.VideoComponents
{
    public class LoadVideo : MonoBehaviour
    {
        public UnityEngine.Video.VideoPlayer myVideoPlayer;
        public string videoName;

        [ContextMenu("Get video player from this component")]
        private void CacheVideoPlayer()
        {
            myVideoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        }
        
        void Start()
        {
            string videoUrl = Path.Combine(Application.streamingAssetsPath, videoName);
            // string videoUrl = Application.streamingAssetsPath + "/" + videoName;
            myVideoPlayer.url = videoUrl;
        }
    }
}