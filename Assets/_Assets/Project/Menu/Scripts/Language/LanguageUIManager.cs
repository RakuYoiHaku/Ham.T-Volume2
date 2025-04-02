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

                // ��Ӵٿ �߰��� ��� �ɼǵ��� ������ �ؽ�Ʈ�� �غ�
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
                    Debug.LogError("��� 'selectedLanguage'�� ã�� �� ����! �⺻��(0)���� ����");
                    currentIndex = 0;  // �⺻������ ����
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
                        //���õ� �� ����Ʈ �� ������ �̵�
                        LanguageOption selecteOption = LanguageManager.instance.languageOptions[selectedindex];
                        LanguageManager.instance.languageOptions.RemoveAt(selectedindex);
                        LanguageManager.instance.languageOptions.Insert(0, selecteOption);
                    }
                    // ��Ӵٿ� �ɼ��� �״�� �߰� (��: ���� ���¸� ["English", "Korean"], �ѱ��� ���¸� ["�ѱ���", "����"])
                    languageDropdown.AddOptions(dropdown.options);
                    break;
                }
            }
            languageDropdown.RefreshShownValue();

            // ��Ʈ ���� (���� ��� ���� ��Ʈ�� ����)
            languageDropdown.captionText.font = LanguageManager.instance.CurrentFontSecondary;
            languageDropdown.itemText.font = LanguageManager.instance.CurrentFontSecondary;

            // ��Ʈ ũ�� ����
            float? labelFontSize = LanguageManager.instance.CurrentLanguageAsset.GetDropdownLabelFontSize();
            float? itemFontSize = LanguageManager.instance.CurrentLanguageAsset.GetDropdownItemFontSize();

            if (languageDropdown.captionText != null && labelFontSize.HasValue)
                languageDropdown.captionText.fontSize = labelFontSize.Value;

            if (languageDropdown.itemText != null && itemFontSize.HasValue)
                languageDropdown.itemText.fontSize = itemFontSize.Value;
        }
    }
}
