using Tourism.KeyCode;
using UnityEngine;

namespace Tourism.VideoComponents
{
    [CreateAssetMenu(fileName = "VideoTag", menuName = "VideoTag")]
    public class VideoTag : ScriptableObject
    {
        public VirtualKeyCode enCode;
        public VirtualKeyCode ruCode;
        public float videoTime = 0f;
    }
}