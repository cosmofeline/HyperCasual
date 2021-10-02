using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int maxEnergy;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    private int energy;
    [SerializeField] private TMP_Text highScoreText;
    private void Start() {
        OnApplicationFocus(true);
       
    }
    private void OnApplicationFocus(bool hasFocus)
    
    {
        if(!hasFocus){return;}
        CancelInvoke();
        int highScore=PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if(energy==0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if (energyReadyString == string.Empty) { return; }

            System.DateTime energyReady=System.DateTime.Parse(energyReadyString);
            if(System.DateTime.Now>energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                  playButton.interactable=false;
                Invoke("EnergyRecharged",(energyReady-DateTime.Now).Seconds);
            }
        }
        energyText.text = $"x{energy}";
       
    }
    private void EnergyRecharged()
    {
        playButton.interactable=true;
        
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
                  energyText.text = $"x{energy}";
    }
        public void Play()
    {
        if (energy < 1) { return; }
        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);

        if (energy == 0) {  
            System.DateTime energyReady=System.DateTime.Now.AddMinutes(energyRechargeDuration);
        PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
        #if UNITY_ANDROID
        androidNotificationHandler.ScheduleNotification(energyReady);
        #endif
        }
      

        SceneManager.LoadScene(1);
    }
}
