using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text HighScoretext;
    public Text Scoretext;

    void Start()
    {
        int HighScoreRecord = PlayerPrefs.GetInt("HighScoreRecord");
        int ScoreRecord = PlayerPrefs.GetInt("ScoreRecord");
        HighScoretext.text = "High Score : " + HighScoreRecord;
        Scoretext.text = "Score : " + ScoreRecord;
    }

    
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("level1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
