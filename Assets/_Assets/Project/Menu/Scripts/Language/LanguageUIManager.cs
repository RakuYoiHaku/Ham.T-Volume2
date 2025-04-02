using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Language
{
    public class LanguageUIManager : MonoBehaviour
    {
        private TMP_Dropdown languageDropdown;

        private void OnDestroy()
        {
            LanguageManager.instance.OnLanguageChanged -= UpdateDropdown;
        }

        private void Start()
        {
            LanguageManager.instance.OnLanguageChanged += UpdateDropdown;
            SetupDropdown();
        }

        private void SetupDropdown()
        {
            languageDropdown = FindObjectOfType<TMP_Dropdown>(true);
            if (languageDropdown != null)
            {
                List<string> options = new List<string>();

                // 드롭다운에 추가할 언어 옵션들을 번역된 텍스트로 준비
                foreach (var opt in LanguageManager.instance.languageOptions)
                {
                    string translatedOption = LanguageManager.instance.CurrentLanguageAsset.GetTranslation(opt.languageName);
                    options.Add(translatedOption);
                }

                languageDropdown.ClearOptions();
                languageDropdown.AddOptions(options);

                int currentIndex = LanguageManager.instance.languageOptions.FindIndex(x => x.languageName == PlayerPrefs.GetString("SelectedLanguage"));
                if (currentIndex == -1)
                {
                    Debug.LogError("언어 'selectedLanguage'를 찾을 수 없음! 기본값(0)으로 설정");
                    currentIndex = 0;  // 기본값으로 설정
                }
                languageDropdown.onValueChanged.RemoveAllListeners();
                languageDropdown.onValueChanged.AddListener(LanguageManager.instance.ChangeLanguage);
                languageDropdown.value = currentIndex >= 0 ? currentIndex : 0;
                languageDropdown.RefreshShownValue();
            }
        }

        private void UpdateDropdown()
        {
            if (languageDropdown == null) return;

            languageDropdown.ClearOptions();
            foreach (var dropdown in LanguageManager.instance.CurrentLanguageAsset.dropdownTranslations)
            {
                if (dropdown.dropdownKey == "Language")
                {
                    int selectedindex = LanguageManager.instance.languageOptions.FindIndex(x => x.languageName == PlayerPrefs.GetString("SelectedLanguage"));
                    if (selectedindex > 0)
                    {
                        //선택된 언어를 리스트 맨 앞으로 이동
                        LanguageOption selecteOption = LanguageManager.instance.languageOptions[selectedindex];
                        LanguageManager.instance.languageOptions.RemoveAt(selectedindex);
                        LanguageManager.instance.languageOptions.Insert(0, selecteOption);
                    }
                    // 드롭다운 옵션을 그대로 추가 (예: 영어 상태면 ["English", "Korean"], 한국어 상태면 ["한국어", "영어"])
                    languageDropdown.AddOptions(dropdown.options);
                    break;
                }
            }
            languageDropdown.RefreshShownValue();

            // 폰트 설정 (현재 사용 중인 폰트로 설정)
            languageDropdown.captionText.font = LanguageManager.instance.CurrentFontSecondary;
            languageDropdown.itemText.font = LanguageManager.instance.CurrentFontSecondary;

            // 폰트 크기 설정
            float? labelFontSize = LanguageManager.instance.CurrentLanguageAsset.GetDropdownLabelFontSize();
            float? itemFontSize = LanguageManager.instance.CurrentLanguageAsset.GetDropdownItemFontSize();

            if (languageDropdown.captionText != null && labelFontSize.HasValue)
                languageDropdown.captionText.fontSize = labelFontSize.Value;

            if (languageDropdown.itemText != null && itemFontSize.HasValue)
                languageDropdown.itemText.fontSize = itemFontSize.Value;
        }
    }
}
