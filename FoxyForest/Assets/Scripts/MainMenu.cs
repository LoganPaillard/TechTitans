using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle masterToggle;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private bool isPlaying = false;

    private void Start()
    {
        LoadVolume();
    }

    public void Play()
    {
        if (isPlaying) return;
        isPlaying = true;
        LevelManager.Instance.LoadScene(SceneID.Spring, TransitionID.SeasonWipe);
    }

    public void Menu() {
        isPlaying = false;
        LevelManager.Instance.LoadScene(SceneID.MainMenu, TransitionID.CrossFade);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void setMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", masterToggle.isOn ? 0 : -80);
        PlayerPrefs.SetInt("MasterVolume", masterToggle.isOn ? 1 : 0);
    }

    public void setMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicToggle.isOn ? 0 : -80);
        PlayerPrefs.SetInt("MusicVolume", musicToggle.isOn ? 1 : 0);
    }

    public void setSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxToggle.isOn ? 0 : -80);
        PlayerPrefs.SetInt("SFXVolume", sfxToggle.isOn ? 1 : 0);
    }

    private void LoadVolume()
    {
        masterToggle.isOn = PlayerPrefs.GetInt("MasterVolume", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("MusicVolume", 1) == 1;
        sfxToggle.isOn = PlayerPrefs.GetInt("SFXVolume", 1) == 1;

        setMasterVolume();
        setMusicVolume();
        setSFXVolume();
    }

    public void togglePause()
    {
        GameManager.Instance.togglePause();
    }

    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }
}