using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Tourism.Localization
{
    public class ImageDownloadHelper : MonoBehaviour
    {
        public static async Task<Sprite> LoadSprite(string path)
        {
            return await LoadSpriteInternal(path);
        }

        private static async Task<Sprite> LoadSpriteInternal(string path)
        {
            // string audioUrl = Application.streamingAssetsPath + "/" + path;
            // string spriteUrl = "file://" + Application.streamingAssetsPath + "/" + path;
            string spriteUrl = "file://" + Path.Combine(Application.streamingAssetsPath, path);

            UnityWebRequest www = UnityWebRequestTexture.GetTexture(spriteUrl);
            var a = www.SendWebRequest();
            while (a.isDone == false)
            {
                await Task.Delay(10);
            }
            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"error on path \"{spriteUrl}\"");
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            return sprite;
        }
    }
}