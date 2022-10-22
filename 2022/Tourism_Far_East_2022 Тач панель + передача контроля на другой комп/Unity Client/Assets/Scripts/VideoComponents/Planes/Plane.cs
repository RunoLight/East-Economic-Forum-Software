using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Tourism.VideoComponents.Planes
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;
        
        public async void FadeAlpha(float startValue, float endValue, float time)
        {
            var timeElapsed = 0f;
            var rawImageColor = rawImage.color;
            rawImageColor.a = startValue;
            rawImage.color = rawImageColor;
            while (timeElapsed < time)
            {
                rawImageColor = rawImage.color;
                var newColor = new Color(rawImageColor.r, rawImageColor.g, rawImageColor.b, Mathf.Lerp(rawImageColor.a, endValue, timeElapsed / time));
                rawImage.color = newColor;
                timeElapsed += Time.deltaTime;
                await Task.Delay((int)(Time.deltaTime * 1000));
            }
        }
    }
}