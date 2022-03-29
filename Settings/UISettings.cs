using System;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UIScripts.Utils;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UIScripts
{
    [RequireComponent(typeof(GameCursor))]
    public class UISettings: MonoBehaviour
    {
        private const string RESOURCES_PATH = "Assets/Resources/UI/";
        private const string COLORS_PATH = "Settings/ui-colors.xml";
        private const string SETTINGS_PATH = "Settings/";
        private const string DEFAULT_COLORS_PATH = "UI/default-colors";
        
        private const string FONTS_PATH = "Fonts & Materials/";
        private const string TYRES_PATH = "UI/Images/Icons/Tyres/";
    
        //Fonts
        private const string REGULAR_FONT_PATH = FONTS_PATH + "Regular";
        private const string BOLD_FONT_PATH = FONTS_PATH + "Bold";

        //Tyres
        private const string SOFT = TYRES_PATH + "soft";
        private const string WET = TYRES_PATH + "wet";
        private const string HARD = TYRES_PATH + "hard";
        private const string INTERMEDIATE = TYRES_PATH + "inter";
        private const string MEDIUM = TYRES_PATH + "medium";
    
        private static UISettings _instance;

        public static UISettings Instance
        {
            get
            {
                var isFound = _instance == null ? "empty" : _instance.name;

                if (_instance != null) return _instance;
                _instance = FindObjectOfType<UISettings>();
                
                if (!_instance)
                {
                    var obj = new GameObject("UISettings");
                    _instance = obj.AddComponent<UISettings>();
                }
                
                _instance.LoadSettings();
                if (!Application.isEditor)
                    DontDestroyOnLoad(_instance);
                return _instance;
            }
        }
    
        public ColorSettings colors;
        public FontSettings fonts;
        public TyresSettings tyres;
        public BonusSprites sprites;
        private static bool _isLoaded;

        private GameCursor _gameCursor = null;

        public GameCursor GameCursor
        {
            get
            {
                // print($"Try get GameCursor, current instance is: {_instance}");
                // print($"Try get GameCursor, current _gameCursor is: {_gameCursor}");
                if (_gameCursor == null)
                    _gameCursor = _instance.GetComponent<GameCursor>();

                if (_gameCursor == null)
                    _gameCursor = _instance.gameObject.AddComponent<GameCursor>();
                
                return _gameCursor;
            }
        }

        public void LoadSettings()
        {
            try
            {
                colors = LoadColors();
            }
            catch (Exception e)
            {
                MakeColorsFile();
                colors = LoadColors();
            }

            if (fonts.bold == null && fonts.regular == null)
            {
                StartCoroutine(LoadFontsRoutine());
            }
            tyres = LoadTyres();
            GameCursor.Initialize();
        }

        private IEnumerator LoadFontsRoutine()
        {
#if UNITY_EDITOR
            while (EditorApplication.isUpdating)
            {
                yield return null;
            }
#endif
            
            yield return fonts = LoadFonts();
        }


        public static ColorSettings LoadColors() => GetColorsFromHex(Load<HexColors>(COLORS_PATH));

        private static void MakeColorsFile()
        {
            if (!Directory.Exists(SETTINGS_PATH))
                Directory.CreateDirectory(SETTINGS_PATH);
            
            var json = Resources.Load<TextAsset>(DEFAULT_COLORS_PATH);
            var defaultColors = JsonUtility.FromJson<HexColors>(json.ToString());
            var uiColors = GetColorsFromHex(defaultColors);
            SaveColors(uiColors);
        }
        
        public static void SaveColors(ColorSettings colorSettings) => 
            XMLHelper.Serialize(ToHexColors(colorSettings), COLORS_PATH);

        public static void SaveColors() => SaveColors(_instance.colors);

        public static void UpdateUIColors(GameObject gameObject)
        {
            gameObject.GetComponentsInChildren<StyledImage>()
                .ToList()
                .ForEach(image => image.UpdateUI());
            
            gameObject.GetComponentsInChildren<StyledText>()
                .ToList()
                .ForEach(text => text.UpdateUI());
            
            gameObject.GetComponentsInChildren<SelectionItem>()
                .ToList()
                .ForEach(selectionItem => selectionItem.Initialize());
            
            gameObject.GetComponentsInChildren<StyledButton>()
                .ToList()
                .ForEach(styledButton => styledButton.UpdateUI());
            
            gameObject.GetComponentsInChildren<StyledStringDropdown>()
                .ToList()
                .ForEach(styledDropdown => styledDropdown.UpdateUI());
        }

        private static FontSettings LoadFonts()
        {
            return new FontSettings
            {
                regular = LoadFont(REGULAR_FONT_PATH),
                bold = LoadFont(BOLD_FONT_PATH)
            };
        }

        private static TyresSettings LoadTyres()
        {
            return new TyresSettings
            {
                hard = LoadSprite(HARD),
                wet = LoadSprite(WET),
                soft = LoadSprite(SOFT),
                intermediate = LoadSprite(INTERMEDIATE),
                medium = LoadSprite(MEDIUM)
            };
        }
        
        
        private static TMP_FontAsset LoadFont(string path) => UnityEngine.Resources.Load<TMP_FontAsset>(path);
        private static Sprite LoadSprite(string path) => UnityEngine.Resources.Load<Sprite>(path);
        
        private static T Load<T>(string path) => XMLHelper.Deserialize<T>(path);
        private static ColorSettings GetColorsFromHex(HexColors hexColors)
        {
            return new ColorSettings
            {
                active = hexColors.active.RGBA(),
                activeHover = hexColors.activeHover.RGBA(),
                background1 = hexColors.background1.RGBA(),
                background2 = hexColors.background2.RGBA(),
                mainBackground = hexColors.mainBackground.RGBA(),
                selection = hexColors.selection.RGBA(),
                text = hexColors.text.RGBA(),
                textBestParameter = hexColors.textBestParameter.RGBA(),
                textGreen = hexColors.textGreen.RGBA(),
                textRed = hexColors.textRed.RGBA(),
                textYellow = hexColors.textYellow.RGBA(),
                title = hexColors.title.RGBA(),
                progressGreen = hexColors.progressGreen.RGBA(),
                building = hexColors.building.RGBA(),
                bestSector = hexColors.bestSector.RGBA(), 
                fasterSector = hexColors.fasterSector.RGBA(),
                slowerSector = hexColors.slowerSector.RGBA(),
                skillBarStart = hexColors.skillBarStart.RGBA(),
                skillBarMiddle = hexColors.skillBarMiddle.RGBA(),
                skillBarEnd = hexColors.skillBarEnd.RGBA(),
                playerTableColor = hexColors.playerTableColor.RGBA(),
                contractWorkColor = hexColors.contractWorkColor.RGBA(),
                contractClientColor = hexColors.contractClientColor.RGBA()
            };
        }
        private static HexColors ToHexColors(ColorSettings colorSettings)
        {
            return new HexColors()
            {
                active = colorSettings.active.ToHexColor(),
                activeHover = colorSettings.activeHover.ToHexColor(),
                background1 = colorSettings.background1.ToHexColor(),
                background2 = colorSettings.background2.ToHexColor(),
                mainBackground = colorSettings.mainBackground.ToHexColor(),
                selection = colorSettings.selection.ToHexColor(),
                text = colorSettings.text.ToHexColor(),
                textBestParameter = colorSettings.textBestParameter.ToHexColor(),
                textGreen = colorSettings.textGreen.ToHexColor(),
                textRed = colorSettings.textRed.ToHexColor(),
                textYellow = colorSettings.textYellow.ToHexColor(),
                title = colorSettings.title.ToHexColor(),
                progressGreen = colorSettings.progressGreen.ToHexColor(),
                building = colorSettings.building.ToHexColor(),
                bestSector = colorSettings.bestSector.ToHexColor(),
                fasterSector = colorSettings.fasterSector.ToHexColor(),
                slowerSector = colorSettings.slowerSector.ToHexColor(),
                skillBarStart = colorSettings.skillBarStart.ToHexColor(),
                skillBarMiddle = colorSettings.skillBarMiddle.ToHexColor(),
                skillBarEnd = colorSettings.skillBarEnd.ToHexColor(),
                playerTableColor = colorSettings.playerTableColor.ToHexColor(),
                contractWorkColor = colorSettings.contractWorkColor.ToHexColor(),
                contractClientColor = colorSettings.contractClientColor.ToHexColor()
            };
        }
    }
}