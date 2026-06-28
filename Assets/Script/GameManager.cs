using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text resultText;
    public TMP_Text ScoreText;
    public float timeLimit = 30f;

    private bool isGameEnd = false;
    private int score;
    private bool quitGame = false;

    void Start()
    {
        Time.timeScale = 1f;
        resultText.text = "";
        UpdateTimerText();
        score = 0;
        UpdateScoreText();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isGameEnd)
        {
            if(Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0f)
        {
            timeLimit = 0f;
            Clear();
        }

        UpdateTimerText();

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (quitGame)
        {
            QuitGame();
        }
            else
        {
        quitGame = true;
        resultText.text = "Press ESC again to Quit";
        }
        return;
}
}

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeLimit);
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        ScoreText.text = "Score:" + score;
    }

    public void GameOver()
    {
        if (isGameEnd) return;

        isGameEnd = true;
        resultText.text = "GAME OVER";
        resultText.text = "YourScore " + score;
        Time.timeScale = 0f;
    }

    public void Clear()
    {
        if (isGameEnd) return;

        isGameEnd = true;
        resultText.text = "CLEAR!";
        resultText.text = "YourScore " + score;
        Time.timeScale = 0f;
    }
    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}