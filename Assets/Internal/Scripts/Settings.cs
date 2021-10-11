using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    #region Refences
    private MainGame Manager;
    #endregion

    #region Graphics
    [Header("Graphics")]
    public bool FancyGraphics = true;
    public Image[] BlurredImages;
    public Material[] BlurredMats;
    public GameObject FancyImage;
    #endregion

    #region Audio
    [Header("Audio")]
    public Slider volumeSlider;
    public int MasterVolume;
    public AudioMixer mixer;
    #endregion

    private void Awake()
    {
        // Assign References
        Manager = GetComponent<MainGame>();

        // Assign Materials
        BlurredMats = new Material[BlurredImages.Length];
        for (int i = 0; i < BlurredImages.Length; i++)
            BlurredMats[i] = BlurredImages[i].material;
    }

    public void ChangeVolValue()
    {
        print("Changed volume value to: " + (int)volumeSlider.value);
        mixer.SetFloat("MasterVol", volumeSlider.value);
        MasterVolume = (int)volumeSlider.value;
    }

    public void LoadVolValue(int value)
    {
        print("(Loaded) Changed volume value to: " + (int)volumeSlider.value);
        volumeSlider.value = value;
        mixer.SetFloat("MasterVol", value);
        MasterVolume = (int)value;
    }

    public void GraphicsChange(bool val)
    {
        print("Changed fancy graphics value to: " + val);
        FancyGraphics = val;
        Manager.maxParticles = val ? 500 : 5000;
        Application.targetFrameRate = val ? 60 : 30;
        FancyImage.SetActive(val);

        for (int i = 0; i < BlurredImages.Length; i++)
            BlurredImages[i].material = val ? BlurredMats[i] : null;
    }
}