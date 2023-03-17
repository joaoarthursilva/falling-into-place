using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    private Resolution[] _resolutions;
    private int _targetFramerate;

    private void Start()
    {
        _resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            var t = _resolutions[i];
            string option = t.width + " x " + t.height + "@" + t.refreshRate + "hz";

            options.Add(option);
            if (t.width == Screen.width && t.height == Screen.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        _targetFramerate = _resolutions[currentResIndex].refreshRate;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVSync(bool isVSync)
    {
        int vSyncVal = isVSync ? 1 : 0;
        Debug.Log(vSyncVal);
        QualitySettings.vSyncCount = vSyncVal;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        // if (Application.targetFrameRate != _targetFramerate) Application.targetFrameRate = _targetFramerate;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetRes(int resIndex)
    {
        Resolution res = _resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}