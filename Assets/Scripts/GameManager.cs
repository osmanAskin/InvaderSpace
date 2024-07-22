using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    public GameObject gameOverObject;
    public GameObject tapToMoveObject;

    private int score;
    private int hiscore;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("hiscore")) 
        {
            hiscore = PlayerPrefs.GetInt("hiscore");
            hiScoreText.text = hiscore.ToString().PadLeft(4, '0');
        }
        
    }

    public void SetScore() 
    {
        score += 50;
        scoreText.text = score.ToString().PadLeft(4, '0');
        SetHiScore();
    }

    public void SetHiScore() 
    {
        if (score >= hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetInt("hiscore", hiscore);
            hiScoreText.text = hiscore.ToString().PadLeft(4, '0');

        }
    }

    public void LevelLoaded() 
    {
        gameObject.SetActive(true);
        tapToMoveObject.SetActive(false);
        gameOverObject.SetActive(true);
        Invoke(nameof(GetGameOverScreen), 3f);
    }

    public void GetGameOverScreen() 
    {
        SceneManager.LoadScene(sceneName: SceneManager.GetActiveScene().name);
    }

    public void TapToMove() 
    {
        Time.timeScale = 1f;
    }
}
