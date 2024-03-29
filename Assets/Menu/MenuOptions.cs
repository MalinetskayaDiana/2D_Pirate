using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuOptions : MonoBehaviour
{
    public bool menuOptions;
    public GameObject options;
    public AudioMixer audioMixer;
    public AudioSource buttonSetSound;
    public AudioSource buttonSound;
    public AudioSource buttonVolumeSound;
    public AudioSource buttonTirListSound;

    public void Resume()
    {
        buttonSetSound.Play();
        options.SetActive(false);
        Time.timeScale = 1f;
        menuOptions = false;
    }

    public void Settings()
    {
        buttonSound.Play();
        options.SetActive(true);
        Time.timeScale = 0f;
        menuOptions = true;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void Sound()
    {
        AudioListener.pause = !AudioListener.pause;
        buttonVolumeSound.Play();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        buttonTirListSound.Play();
    }
}
