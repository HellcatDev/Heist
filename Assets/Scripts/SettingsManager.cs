using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    // Script will be completed next semester.

    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown antiAliasingDropdown;
    public TMP_Dropdown vSyncDropdown;
    public TMP_Dropdown overallQuality;
    public TMP_Dropdown textureResolutionDropdown;
    public Toggle bloom;
    public Toggle vignette;
    public Toggle motionBlur;
    public TMP_Dropdown shadowQualityDropdown;
    public Slider masterVolumeSlider;
    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider dialogVolumeSlider;
    public Toggle discordRichPresence;

    public Resolution[] resolutions;
    public GameSettings gameSettings;
    public DiscordController discordController;

    /// <summary>
    /// Adds listeners to buttons which listens to the value changing of drop downs, toggles and buttons.
    /// </summary>
    private void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        antiAliasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        overallQuality.onValueChanged.AddListener(delegate { OnOverallQualityChange(); });
        textureResolutionDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        bloom.onValueChanged.AddListener(delegate { OnBloomChange(); });
        vignette.onValueChanged.AddListener(delegate { OnVignetteChange(); });
        motionBlur.onValueChanged.AddListener(delegate { OnMotionBlurChange(); });
        shadowQualityDropdown.onValueChanged.AddListener(delegate { OnShadowQualityChange(); });
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolumeChange(); });
        soundVolumeSlider.onValueChanged.AddListener(delegate { OnSoundVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        dialogVolumeSlider.onValueChanged.AddListener(delegate { OnDialogVolumeChange(); });

        resolutions = Screen.resolutions;

        
    }

    private void Start()
    {
        if (discordController.discordPresent == false)
        {
            discordRichPresence.isOn = false;
            discordRichPresence.interactable = false;
        }
    }

    public void OnFullscreenToggle()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {

    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = textureResolutionDropdown.value;
        
    }

    public void OnAntiAliasingChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, antiAliasingDropdown.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = vSyncDropdown.value;
    }

    public void OnOverallQualityChange()
    {

    }

    public void OnBloomChange()
    {
        
    }

    public void OnVignetteChange()
    {

    }

    public void OnMotionBlurChange()
    {

    }

    public void OnShadowQualityChange()
    {
        if (shadowQualityDropdown.value == 0) // Very high settings
        {
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
        }
        else if (shadowQualityDropdown.value == 1) // High settings
        {
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = ShadowResolution.High;
        }
        else if (shadowQualityDropdown.value == 2) // Medium settings
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            QualitySettings.shadowResolution = ShadowResolution.Medium;
        }
        else if (shadowQualityDropdown.value == 3) // Low settings
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            QualitySettings.shadowResolution = ShadowResolution.Low;
        }
        else if (shadowQualityDropdown.value == 4) // Off settings
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            QualitySettings.shadowResolution = ShadowResolution.Low;
        }
    }

    public void OnMasterVolumeChange()
    {

    }

    public void OnSoundVolumeChange()
    {

    }

    public void OnMusicVolumeChange()
    {

    }

    public void OnDialogVolumeChange()
    {

    }

    public void OnDiscordRichPresence()
    {
        if (discordRichPresence.isOn == true)
        {
            discordController.discordRichPresence = true;
            discordController.StartDRP();
        }
        else
        {
            discordController.discordRichPresence = false;
            discordController.DRPShutdown();
        }
    }

    public void SaveSettings()
    {

    }

    public void LoadSettings()
    {

    }
}
