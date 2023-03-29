using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _effectSlider;
    [SerializeField] private Slider _musicSlider;

    [Header("Mixers")]
    [SerializeField] private AudioMixer _audioMixer;

    void Start()
    {
        float master = PlayerPrefs.GetFloat("master");
        float effects = PlayerPrefs.GetFloat("effects");
        float music = PlayerPrefs.GetFloat("music");

        _masterSlider.value = master;
        _effectSlider.value = effects;
        _musicSlider.value = music;

        SetMasterLevel(master);
        SetEffectLevel(effects);
        SetMusicLevel(music);
    }

    public void SetMasterLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("master", sliderValue);
        PlayerPrefs.Save();
        _audioMixer.SetFloat("master", Mathf.Log10(sliderValue) * 20);
    }

    public void SetEffectLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("effects", sliderValue);
        PlayerPrefs.Save();
        _audioMixer.SetFloat("effects", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("music", sliderValue);
        PlayerPrefs.Save();
        _audioMixer.SetFloat("music", Mathf.Log10(sliderValue) * 20);
    }
}
