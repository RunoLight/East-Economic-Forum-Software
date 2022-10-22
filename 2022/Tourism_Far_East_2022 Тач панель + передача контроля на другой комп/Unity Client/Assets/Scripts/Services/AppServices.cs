using Tourism.KeyCode;
using Tourism.Localization;
using Tourism.ResourcesLoader;
using Tourism.Server;
using UnityEngine;

namespace Tourism.Services
{
    public class AppServices : MonoBehaviour
    {
        public static AppServices I { get; private set; }
        
        [SerializeField] public VideoComponents.VideoPlayer VideoPlayer;

        [SerializeField] public AudioManager.AudioManager AudioManager;
        public bool IsButtonsLocked { get; set; }

        public string serverUrl;
        public string serverPort;
        public VirtualKeyCode cancelVideoKeyCodeRu = VirtualKeyCode.VK_1;
        public VirtualKeyCode cancelVideoKeyCodeEn = VirtualKeyCode.VK_2;

        public VirtualKeyCode CancelVideoKeyCode => LocalizationManager.Language == LocalizationLanguage.English
            ? cancelVideoKeyCodeEn
            : cancelVideoKeyCodeRu;
        
        private void Awake()
        {
            I = gameObject.GetComponent<AppServices>();
            Initialization();
        }
        
        private void Initialization()
        {
            SerializationManager.LoadLocal<ServerData>(Paths.DefaultFolder + Paths.ServerDataPath, out var serverData);
            serverUrl = serverData.IP;
            serverPort = serverData.Port;
            Debug.Log($"Server configured on {serverUrl}:{serverPort}");
            LocalizationManager.Initialize();
        }
    }
}