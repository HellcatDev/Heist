using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class GameSettings
{
    public bool fullscreen;
    public int antiAliasing;
    public int vSync;
    public int resolutionIndex;
    public string overallQuality;
    public int textureResolution;
    public bool bloom;
    public bool motionBlur;
    public bool vignette;
    public int shadowQuality;
    public float masterVolume;
    public float soundVolume;
    public float musicVolume;
    public float dialogVolume;


    public AudioMixer audioMixer;


    // Graphical Settings

    public void OverallQuality(int qualityIndex)
    {
        if (qualityIndex == 0) //Low
        {

        }
        else if (qualityIndex == 1) //Medium
        {

        }
        else if (qualityIndex == 2) //High
        {

        }
        else if (qualityIndex == 3) //Very High
        {

        }
        else if (qualityIndex == 4) //Ultra
        {

        }
        else if (qualityIndex == 5) //Custom
        {
            
        }
    }

    public void TextureQuality(string Quality)
    {

    }

    public void Bloom(bool Identifier)
    {

    }

    public void Vignette(bool Identifier)
    {

    }

    public void MotionBlur(bool Identifier)
    {

    }

    public void ShadowQuality(string Quality)
    {

    }

    // Audio Settings

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
