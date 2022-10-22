using UnityEngine;
using UnityEngine.UI;

namespace Tourism.Localization
{
    [RequireComponent(typeof(Image))]
    public class LocalizedImage : MonoBehaviour, ILocalized
    {
        [SerializeField] private string term;
        [SerializeField] private bool updateSize = false;
        private Image image;
        public string Term => term;

        private void Start()
        {
            image = GetComponent<Image>();
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
        public async void Localize()
        {
            
            image.sprite = await LocalizationManager.LocalizeImage(term);
            if (updateSize)
            {
                transform.GetComponent<RectTransform>().sizeDelta = new Vector2(image.sprite.rect.width, image.sprite.rect.height);
            }
        }

        private async void OnValidate()
        {
            // var image = GetComponent<Image>();
                // image.sprite = await LocalizationManager.LocalizeImage(term);
        }
    }
}