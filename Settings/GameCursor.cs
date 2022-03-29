using UnityEngine;

namespace UIScripts
{
    public class GameCursor : MonoBehaviour
    {
        private const string FolderPath = "UI/Images/Cursor/";
        private const string CommonPath = FolderPath + "Common";
        private const string LinkPath = FolderPath + "Link";
        private const string WaitingPath = FolderPath + "Waiting";
        private const string InfoPath = FolderPath + "Info";
        private const string MousePath = FolderPath + "Mouse";
        
        [SerializeField] private Texture2D common;
        [SerializeField] private Texture2D link;
        [SerializeField] private Texture2D info;
        [SerializeField] private Texture2D mouse;
        [SerializeField] private Texture2D[] waitingTextures = new Texture2D[4];
        [SerializeField] private Vector2 commonHotspot = new Vector2(2, 0);
        [SerializeField] private Vector2 linkHotspot = new Vector2(4, 0);
        [SerializeField] private Vector2 infoHotspot = new Vector2(4, 0);
        [SerializeField] private Vector2 mouseHotspot = new Vector2(0, 0);
        [SerializeField] private Vector2 waitingHotspot = new Vector2(0, 0);
        
        [SerializeField] private float frameRate = 0.1f;

        private bool _isWaiting;
        private int _currentFrame;
        private float _frameTimer;

        public bool IsWaiting
        {
            set
            {
                _isWaiting = value;
                SetCursorType(value ? CursorType.Waiting : CursorType.Common);
            }
        }

        public void Initialize()
        {
            common = UnityEngine.Resources.Load<Texture2D>(CommonPath);
            link = UnityEngine.Resources.Load<Texture2D>(LinkPath);
            info = UnityEngine.Resources.Load<Texture2D>(InfoPath);
            mouse = UnityEngine.Resources.Load<Texture2D>(MousePath);
            for (int i = 0; i < 4; i++)
            {
                waitingTextures[i] = UnityEngine.Resources.Load<Texture2D>($"{WaitingPath}-{i}");
            }
            
            SetCursorType(CursorType.Common);
        }

        public void SetCursorType(CursorType type)
        {
            if (_isWaiting)
                return;
            
            var texture = GetTextureByCursorType(type);
            var hotspot = GetHotspotByCursorType(type);
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);

            if (type == CursorType.Waiting)
            {
                _isWaiting = true;
            }
        }

        private Texture2D GetTextureByCursorType(CursorType type)
        {
            switch (type)
            {
                case CursorType.Common : return common;
                case CursorType.Link : return link;
                case CursorType.Info : return info;
                case CursorType.Waiting : return waitingTextures[0];
                case CursorType.Mouse : return mouse;
                default : return common;
            }
        }

        private Vector2 GetHotspotByCursorType(CursorType type)
        {
            switch (type)
            {
                case CursorType.Common : return commonHotspot;
                case CursorType.Link : return linkHotspot;
                case CursorType.Info : return infoHotspot;
                case CursorType.Waiting : return waitingHotspot;
                case CursorType.Mouse : return mouseHotspot;
                default : return Vector2.zero;
            }
        }

        private void Update()
        {
            if (_isWaiting)
            {
                _frameTimer -= Time.deltaTime;
                if (_frameTimer <= 0)
                {
                    _frameTimer += frameRate;
                    _currentFrame = (_currentFrame + 1) % waitingTextures.Length;
                    Cursor.SetCursor(waitingTextures[_currentFrame], commonHotspot, CursorMode.Auto);   
                }
            }
        }
    }
}