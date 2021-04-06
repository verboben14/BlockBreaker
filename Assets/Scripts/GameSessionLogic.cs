using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSessionLogic : MonoBehaviour
{
    // configuration parameters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 14;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled = false;

    // state variables
    [SerializeField] int currentScore = 0;
    int currentLevel = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSessionLogic>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetScoreTextToCurrentScore();
        SetCurrentLevel(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void SetCurrentLevel(int levelIndex)
    {
        currentLevel = levelIndex;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void AddPointsPerBlockDestroyedToPoint()
    {
        currentScore += pointsPerBlockDestroyed;
        SetScoreTextToCurrentScore();
    }

    private void SetScoreTextToCurrentScore()
    {
        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
