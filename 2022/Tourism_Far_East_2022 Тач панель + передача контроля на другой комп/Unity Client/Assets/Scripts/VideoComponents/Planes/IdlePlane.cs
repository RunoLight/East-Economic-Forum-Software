using UnityEngine.UI;

namespace Tourism.VideoComponents.Planes
{
    public class IdlePlane : Plane
    {
        private Button exitIdleButton;

        private void Awake()
        {
            exitIdleButton = GetComponent<Button>();
            exitIdleButton.onClick.AddListener(ExitIdle);
        }
        
        private void OnEnable()
        {
            //TODO Vasya play Idle Video
        }

        private void OnDisable()
        {
            //TODO Vasya stop playing idle Video
        }

        private void OnDestroy()
        {
            exitIdleButton.onClick.RemoveListener(ExitIdle);
        }

        private void ExitIdle()
        {
            gameObject.SetActive(false);
        }
    }
}