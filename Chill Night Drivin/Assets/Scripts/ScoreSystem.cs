using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    public const string HighScoreKey = "HighScore";

    private float score;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime*scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
    private void OnDestroy()
    {
        int currentHighScore=  PlayerPrefs.GetInt("HighScore", 0);
        if(score>currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", Mathf.FloorToInt(score));
        }
    }
}
