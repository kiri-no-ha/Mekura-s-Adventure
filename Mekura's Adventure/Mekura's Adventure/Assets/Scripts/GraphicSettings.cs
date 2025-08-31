using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GraphicSettings : MonoBehaviour
{
    public Toggle Toggle;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown QualityDropdown;
    public Slider slidersoundvolume;
    public Slider sliderdiologvolume;
    public Slider slidermusicvolume;

    Resolution[] resolutions;
    
    void Start()
    {
        Toggle.isOn = true;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {
            Debug.Log(resolution.width);
        }
        int currentresolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height + " HZ " + resolutions[i].refreshRateRatio;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentresolution = i;
                Debug.Log($"{Screen.currentResolution.width}=={resolutions[i].width} =>{Screen.currentResolution.width== resolutions[i].width}");
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentresolution);
        
    }
    public void SetResolutions(int currentResolution)
    {
        Resolution res = resolutions[currentResolution];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetQuality(int currentQuality)
    {
        QualitySettings.SetQualityLevel(currentQuality);
    }
    public void setFullScreen(bool isFullcreen)
    {
        Screen.fullScreen = isFullcreen;
    }
    // ѕока что PlayerPrefs, потом что то другое придумаем
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualityPreference", QualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetInt("MusicVolumePreference", (int)slidermusicvolume.value);
        PlayerPrefs.SetInt("DiologVolumePreference", (int)sliderdiologvolume.value);
        PlayerPrefs.SetInt("SoundVolumePreference", (int)slidersoundvolume.value);
    }
        
    public void LoadSettings(int currentres)
    {
        if (PlayerPrefs.HasKey("QualityPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("QualityPreference");
        }
        else
        {
            QualityDropdown.value = 1;
        }
        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else
        {
            resolutionDropdown.value = currentres;
        }
        if (PlayerPrefs.HasKey("FullScreenPreference"))
        {
            Toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference"));
        }
        else
        {
            Toggle.isOn = true;
        }
        //SetResolutions(currentres);
        //SetQuality();
    }
}
