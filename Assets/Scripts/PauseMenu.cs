using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ua;
    [SerializeField] private Button en;
    [SerializeField] private GameObject unmute;
    [SerializeField] private GameObject mute;
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject pause;
    [SerializeField] private LocalizationMainScene localizationMainScene;

    public AudioMixer audioMixer;
    private string localizationType;
    private float volume;
    private int qualityIndex;
    private bool soundPause;

    void Start()
    {
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>("players");

        volume = loadedData.volumeSave;
        qualityIndex = loadedData.qualityIndexSave;
        soundPause = loadedData.soundPauseSave;
        localizationType = loadedData.localizationTypeSave;

        sliderVolume.value = volume;
        dropdown.value = qualityIndex;

        SetVolume(volume);
        SetQuality(qualityIndex);
        Sound(soundPause);

        pause.SetActive(false);

        if (soundPause)
        {
            mute.SetActive(true);
            unmute.SetActive(false);
        }
        else
        {
            mute.SetActive(false);
            unmute.SetActive(true);
        }

        if (localizationType == "ua")
        {
            ua.interactable = false;
            en.interactable = true;
        }
        else
        {
            en.interactable = false;
            ua.interactable = true;
        }

    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        this.qualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void Sound(bool soundPause)
    {
        if (soundPause == true)
        {
            AudioListener.pause = true;
            this.soundPause = true;

            foreach (Sound s in AudioManager.instance.sounds)
            {
                if (s.source != null)
                s.source.Stop();
            }
        }
        else
        {
            AudioListener.pause = false;
            this.soundPause = false;
        }
    }

    public void PauseOff()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        AudioManager.instance.Stop("GameMusic");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene("EllissiumScene");
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnDestroy()
    {
        PlayerInfo saveData = new PlayerInfo
        {
            volumeSave = volume,
            qualityIndexSave = qualityIndex,
            localizationTypeSave = localizationMainScene.Localizationtype,
            indexSave = localizationMainScene.ItemIndex,
            soundPauseSave = soundPause
        };

        DataSaver.saveData(saveData, "players");
    }
}