using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIScripts.Utils.ColorPicker
{
    public class ColorPickerOwn : MonoBehaviour
    {
        [SerializeField] private Image colors;
        [SerializeField] private Transform selectCircle;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Image currentColor;

        [SerializeField] private StyledButton applyButton;
        [SerializeField] private StyledButton cancelButton;
        
        private RectTransform _colorsRect;
        private Texture2D _colorTexture;
        private Image _selectCircleImage;
        private Color _selectedColor;

        private RectTransform _currentColorRect;

        private Color32[] _pixels;
        private Dictionary<Color32, Vector2Int> _colorCoord = new Dictionary<Color32, Vector2Int>();

        public UnityEvent<Color> onHover;
        public UnityEvent<Color> onSelect;
        public UnityEvent<Color> onCurrentClick;
        public UnityEvent<Color> onApplyColor;

        private void Awake()
        {
            if (!_colorsRect) _colorsRect = colors.GetComponent<RectTransform>();
            if (!_colorTexture) _colorTexture = colors.mainTexture as Texture2D;

            if (_colorTexture != null)
            {
                _pixels = _colorTexture.GetPixels32();
                InitColorDict();
            }

            inputField.onValueChanged.AddListener(InputFieldChangeListener);
            selectCircle.gameObject.SetActive(false);

            onSelect.AddListener(ColorSelectListener);
            
            applyButton.onClick.AddListener(() =>
            {
                onApplyColor.Invoke(_selectedColor);
                gameObject.SetActive(false);
            });
            
            cancelButton.onClick.AddListener(() => gameObject.SetActive(false));

            if (!_currentColorRect) _currentColorRect = currentColor.GetComponent<RectTransform>();
        }

        private void Init()
        {
            
        }

        private void MoveCircle(Vector2Int pos)
        {
            var x = _colorsRect.rect.width / _colorTexture.width * pos.x;
            var y = _colorsRect.rect.height / _colorTexture.height * pos.y;
            selectCircle.localPosition = new Vector3(x, y, 0);
        }

        private Vector2Int FindPixelByColor(Color color)
        {
            _colorCoord.TryGetValue(color, out var pos);
            return pos;
        }

        private void InitColorDict()
        {
            for (int i = 0; i < _colorTexture.width * _colorTexture.height; i++)
            {
                var y = i / _colorTexture.height;
                var x = i % _colorTexture.width;
                if (!_colorCoord.ContainsKey(_pixels[i]))
                    _colorCoord.Add(_pixels[i], new Vector2Int(x, y));
            }
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(_colorsRect, mousePosition))
            {
                Vector2 delta;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_colorsRect, mousePosition, null,
                    out delta);

                var width = _colorsRect.rect.width;
                var height = _colorsRect.rect.height;

                var x = Mathf.Clamp(delta.x / width, 0f, 1f);
                var y = Mathf.Clamp(delta.y / height, 0f, 1f);

                var textureX = Mathf.RoundToInt(x * _colorTexture.width);
                var textureY = Mathf.RoundToInt(y * _colorTexture.height);

                Color color = _colorTexture.GetPixel(textureX, textureY);
                if (_selectedColor == color) return;
                if (color.a == 0) return;
                onHover?.Invoke(color);

                if (Input.GetMouseButton(0))
                {
                    if (!selectCircle.gameObject.activeSelf)
                        selectCircle.gameObject.SetActive(true);
                    selectCircle.localPosition = delta;
                    onSelect.Invoke(color);
                    inputField.SetTextWithoutNotify("#" + ColorUtility.ToHtmlStringRGB(color));
                }
            }

            if (RectTransformUtility.RectangleContainsScreenPoint(_currentColorRect, mousePosition))
            {
                if (Input.GetMouseButton(0))
                {
                    if (currentColor.color == _selectedColor) return;
                    inputField.Select();
                    inputField.text = "#" + ColorUtility.ToHtmlStringRGB(currentColor.color);
                    // inputField.SetTextWithoutNotify("#" + ColorUtility.ToHtmlStringRGB(currentColor.color));
                }
            }
        }

        private void ColorSelectListener(Color color)
        {
            _selectedColor = color;
        }

        private void InputFieldChangeListener(string text)
        {
            if (ColorUtility.TryParseHtmlString(text, out var color))
            {
                if (_selectedColor == color) return;
                onSelect?.Invoke(color);
                MoveCircle(FindPixelByColor(color));
            }
        }
    }
}