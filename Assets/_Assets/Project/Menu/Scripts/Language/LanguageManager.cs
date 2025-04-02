using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Language
{
    [System.Serializable]
    public class LanguageOption
    {
        public string languageName;         // 예: "English", "Korean"
        public LanguageAsset languageAsset;
        public TMP_FontAsset primaryFont;
        public TMP_FontAsset secondaryFont;
    }
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager instance;

        public List<LanguageOption> languageOptions;
        public LanguageAsset CurrentLanguageAsset;
        public TMP_FontAsset CurrentFontPrimary;
        public TMP_FontAsset CurrentFontSecondary;
        private string selectedLanguage;

        public delegate void LanguageChanged();
        public event LanguageChanged OnLanguageChanged; // 언어 변경 이벤트

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);

                if (!PlayerPrefs.HasKey("HasLaunchedBefore"))
                {
                    PlayerPrefs.SetInt("HasLaunchedBefore", 1);
                    PlayerPrefs.DeleteKey("SelectedLanguage"); // 첫 실행 때만 초기화
                    PlayerPrefs.Save();
                }

                LoadLanguageSetting();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadLanguageSetting()
        {
            selectedLanguage = PlayerPrefs.GetString("SelectedLanguage", languageOptions[0].languageName);
            ApplySavedLanguage();
        }

        private void ApplySavedLanguage()
        {
            LanguageOption option = languageOptions.Find(x => x.languageName == selectedLanguage);
            if (option != null)
            {
                SetLanguage(option.languageAsset, option.primaryFont, option.secondaryFont);
            }
            else
            {
                SetLanguage(languageOptions[0].languageAsset, languageOptions[0].primaryFont, languageOptions[0].secondaryFont);
            }
        }

        private void SetLanguage(LanguageAsset asset, TMP_FontAsset primaryFont, TMP_FontAsset secondaryFont)
        {
            CurrentLanguageAsset = asset;
            CurrentFontPrimary = primaryFont;
            CurrentFontSecondary = secondaryFont;
            OnLanguageChanged?.Invoke(); // 모든 UI가 언어 변경을 감지하도록 이벤트 호출
        }

        public void ChangeLanguage(int index)
        {
            if (index < 0 || index >= languageOptions.Count) return;

            LanguageOption option = languageOptions[index];
            selectedLanguage = option.languageName;
            PlayerPrefs.SetString("SelectedLanguage", selectedLanguage);
            PlayerPrefs.Save();
            SetLanguage(option.languageAsset, option.primaryFont, option.secondaryFont);
        }
    }
}
