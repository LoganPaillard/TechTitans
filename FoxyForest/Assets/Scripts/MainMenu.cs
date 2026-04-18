using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle musicToggle;
    public Toggle sfxToggle;
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject creditsPanel;
    public GameObject settingsButton;
    public GameObject settingsMenu;
    public float autoCloseDelay = 15f;

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

    public void OpenCredits()
    {
        if (creditsPanel != null)
        { 
            creditsPanel.SetActive(true);
            playButton.SetActive(false);
            creditsButton.SetActive(false);
            settingsButton.SetActive(false);
            Invoke("CloseCredits", autoCloseDelay);
        }

    }

    public void CloseCredits()
    {
        CancelInvoke("CloseCredits");
        if (creditsPanel != null)  
        {
            creditsPanel.SetActive(false);
            playButton.SetActive(true);
            creditsButton.SetActive(true);
            settingsButton.SetActive(true);
        }
    }

    public void OpenSettings()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(true);
            playButton.SetActive(false);
            creditsButton.SetActive(false);
            settingsButton.SetActive(false);
            Invoke("CloseSettings", autoCloseDelay);
        }
    }

    public void CloseSettings()
    {
        CancelInvoke("CloseSettings");
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
            playButton.SetActive(true);
            creditsButton.SetActive(true);
            settingsButton.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicToggle.isOn ? 0 : -80);
        PlayerPrefs.SetInt("MusicVolume", musicToggle.isOn ? 1 : 0);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxToggle.isOn ? 0 : -80);
        PlayerPrefs.SetInt("SFXVolume", sfxToggle.isOn ? 1 : 0);
    }

    private void LoadVolume()
    {
        musicToggle.isOn = PlayerPrefs.GetInt("MusicVolume", 1) == 1;
        sfxToggle.isOn = PlayerPrefs.GetInt("SFXVolume", 1) == 1;

        SetMusicVolume();
        SetSFXVolume();
    }

    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }
}