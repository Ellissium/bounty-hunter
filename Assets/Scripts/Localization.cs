using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Localization : MonoBehaviour
{
    public PlayerInfo loadedData;
    [SerializeField] private Text muteSoundText;
    [SerializeField] private Text soundVolumeText;
    [SerializeField] private Text graphicsQualityText;
    [SerializeField] private Text languageText;
    [SerializeField] private Text generalText;
    [SerializeField] private Text controlText;
    [SerializeField] private Text playText;
    [SerializeField] private Text settingsText;
    [SerializeField] private Text aboutUsText;
    [SerializeField] private Text exitText;
    [SerializeField] private Text itemText;
    [SerializeField] private Text labelText;
    [SerializeField] private Text aboutUsMenuText;
    [SerializeField] private Text walkText;
    [SerializeField] private Text shootText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Text interactionText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text lastTimeText;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject aboutUsMenu;
    [SerializeField] private GameObject dropdawn;
    [SerializeField] private GameObject template;

    private GameObject[] buttonsText;
    private GameObject[] defaultsText;
    private string localizationType;
    private int itemIndex;
    private float bestTime;
    private long lasttime;


    public string Localizationtype { get { return localizationType; } set { localizationType = value; } }

    public int ItemIndex { get { return itemIndex; } set { itemIndex = value; } }

    private void Start()
    {
        buttonsText = GameObject.FindGameObjectsWithTag("ButtonText");
        defaultsText = GameObject.FindGameObjectsWithTag("DefaultText");

        settingsMenu.SetActive(false);
        template.SetActive(false);
        aboutUsMenu.SetActive(false);

        loadedData = DataSaver.loadData<PlayerInfo>("players");
        if (loadedData == null)
        {
            loadedData = new PlayerInfo();
            DataSaver.saveData(loadedData, "players");
        }
        itemIndex = loadedData.indexSave;
        localizationType = loadedData.localizationTypeSave;
        /*lasttime = Mathf.Min(loadedData.recordsSave);*/
        Debug.Log(localizationType);
        SetLocalization();
        SetIndex(itemIndex);
    }

    public void SetIndex(int index) 
    {
        itemIndex = index;
    }

    public void SetLocalizationType(string localization)
    {
        localizationType = localization;
    }

    public void SetLocalization()
    {
        if (localizationType == "ua")
        {
            foreach (GameObject buttonText in buttonsText)
            {
                buttonText.GetComponent<Text>().fontSize = 11;
                buttonText.GetComponent<Text>().fontStyle = FontStyle.Bold;
                Debug.Log(buttonText.GetComponent<Text>().fontSize);
            }
            
            foreach (GameObject defaultText in defaultsText)
            {
                defaultText.GetComponent<Text>().fontSize = 20;
                defaultText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            }

            dropdawn.GetComponent<Dropdown>().options[0].text = "Дуже Низька";
            dropdawn.GetComponent<Dropdown>().options[1].text = "Низька";
            dropdawn.GetComponent<Dropdown>().options[2].text = "Середня";
            dropdawn.GetComponent<Dropdown>().options[3].text = "Висока";
            dropdawn.GetComponent<Dropdown>().options[4].text = "Дуже Висока";
            dropdawn.GetComponent<Dropdown>().options[5].text = "Ультра";
            itemText.fontSize = 18;
            itemText.fontStyle = FontStyle.Bold;

            labelText.text = dropdawn.GetComponent<Dropdown>().options[itemIndex].text;
            labelText.fontSize = 18;
            labelText.fontStyle = FontStyle.Bold;

            muteSoundText.text = "Вмк/Вимк Звук";
            soundVolumeText.text = "Гучність Звуку";
            graphicsQualityText.text = "Якість Графіки";
            languageText.text = "Мова";
            generalText.text = "Загальні";
            controlText.text = "Керування";
            playText.text = "Грати";
            settingsText.text = "Налаштування";
            aboutUsText.text = "Про нас";
            exitText.text = "Вихід";
            aboutUsMenuText.text = "Ця гра була створена як навчальний курсовий проєкт командою з двох друзів — Єлисея Черкова та Миколи Ткаченка. Цей продукт є повністю некомерційним і розроблений виключно для академічних та наукових цілей.";
            walkText.text = "Ходити";
            shootText.text = "Стріляти";
            pauseText.text = "Пауза";
            interactionText.text = "Взаємодія";
            bestTimeText.text = "Найкращий Час:";
            lastTimeText.text = "Останній Час:";
            DrawTextInfo();
        }
        else if (localizationType == "en")
        {
            foreach (GameObject buttonText in buttonsText)
            {
                buttonText.GetComponent<Text>().fontSize = 11;
                buttonText.GetComponent<Text>().fontStyle = FontStyle.Normal;
            }

            foreach (GameObject defaultText in defaultsText)
            {
                defaultText.GetComponent<Text>().fontSize = 20;
                defaultText.GetComponent<Text>().fontStyle = FontStyle.Normal;
            }
       
            dropdawn.GetComponent<Dropdown>().options[0].text = "Very Low";
            dropdawn.GetComponent<Dropdown>().options[1].text = "Low";
            dropdawn.GetComponent<Dropdown>().options[2].text = "Medium";
            dropdawn.GetComponent<Dropdown>().options[3].text = "High";
            dropdawn.GetComponent<Dropdown>().options[4].text = "Very High";
            dropdawn.GetComponent<Dropdown>().options[5].text = "Ultra";
            itemText.fontSize = 18;
            itemText.fontStyle = FontStyle.Normal;

            labelText.text = dropdawn.GetComponent<Dropdown>().options[itemIndex].text;
            labelText.fontSize = 18;
            labelText.fontStyle = FontStyle.Normal;

            muteSoundText.text = "Mute/Unmute Sound";
            soundVolumeText.text = "Sound Volume";
            graphicsQualityText.text = "Graphics Quality";
            languageText.text = "Language";
            generalText.text = "General";
            controlText.text = "Control";
            playText.text = "Play";
            settingsText.text = "Settings";
            aboutUsText.text = "About Us";
            exitText.text = "Exit";
            aboutUsMenuText.text = "This game was created as an educational course project by a team of two friends, Yelysei Cherkov and Mykola Tkachenko. This product is entirely non-commercial and was developed for academic and scientific purposes only.";
            walkText.text = "Walk";
            shootText.text = "Shoot";
            pauseText.text = "Pause";
            interactionText.text = "Interaction";
            bestTimeText.text = "Best Time:";
            lastTimeText.text = "Last Time:";
            DrawTextInfo();
        }
    }
    private void DrawTextInfo()
    {
        if (loadedData.bestRecordSave == 0)
        {
            bestTimeText.text += " -";
        } 
        else
        {
            TimeSpan t = TimeSpan.FromSeconds(loadedData.bestRecordSave);
            bestTimeText.text += " " + t.ToString(@"mm\:ss");
        }
        if (loadedData.lastRecordSave == 0)
        {
            lastTimeText.text += " -";
        }
        else
        {
            TimeSpan t = TimeSpan.FromSeconds(loadedData.lastRecordSave);
            lastTimeText.text += " " + t.ToString(@"mm\:ss");
        }
    }
}
