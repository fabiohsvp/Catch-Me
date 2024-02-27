using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class LocaleSelection : MonoBehaviour
{

    public TMP_Dropdown languageDropdown;

    private void Start()
    {
        
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
        languageDropdown.value = ID;
    }

    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
        languageDropdown.value = _localeID;
    }
}
