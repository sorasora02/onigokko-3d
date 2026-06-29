using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text resultText;
    public TMP_Text ScoreText;
    public float timeLimit = 30f;
    private bool quitGame = false;
    private bool resultQuitCancel = false;
    private bool diffcultyQuitCancel = false;

    [Header("Difficulty")]
    public GameObject difficultyPanel;
    public NavMeshAgent enemyAgent;

    public float easyEnemySpeed = 2.5f;
    public int easyClearScore = 3;

    public float normalEnemySpeed = 3.5f;
    public int normalClearScore = 5;

    public float hardEnemySpeed = 5f;
    public int hardClearScore = 8;

    private bool isGameEnd = false;
    private int score;
    private int clearScore;
    private bool isGameStarted = false;

    void Start()
    {
        score = 0;
        isGameEnd = false;

        resultText.text = "";
        UpdateTimerText();
        UpdateScoreText();

        DifficultyMenu();
    }

    void Update()
    {
        if (quitGame)
        {
        if (Keyboard.current != null && Keyboard.current.yKey.wasPressedThisFrame)
            {
            QuitGame();
            return;
        }

        if (Keyboard.current != null && Keyboard.current.nKey.wasPressedThisFrame)
        {
            CancelQuit();
            return;
        }

        return;
    }
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OpenQuitConfirm();
            return;
        }

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (!isGameStarted)
        {
            return;
        }

        if (isGameEnd)
        {
            return;
        }

        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0f)
        {
            timeLimit = 0f;

            if (score >= clearScore)
            {
                Clear();
            }
            else
            {
                GameOver();
            }
        }

        UpdateTimerText();
    }
    public void DifficultyMenu()
    {
        isGameStarted = false;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (difficultyPanel != null)
        {
            difficultyPanel.SetActive(true);
        }

        if (resultText != null)
        {
            resultText.text = "";
        }
    }

    public void SelectEasy()
    {
        StartGame(easyEnemySpeed, easyClearScore);
    }

    public void SelectNormal()
    {
        StartGame(normalEnemySpeed, normalClearScore);
    }

    public void SelectHard()
    {
        StartGame(hardEnemySpeed, hardClearScore);
    }

    private void StartGame(float enemySpeed, int requiredScore)
    {
        clearScore = requiredScore;
        isGameStarted = true;
        isGameEnd = false;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (difficultyPanel != null)
        {
            difficultyPanel.SetActive(false);
        }

        if (enemyAgent != null)
        {
            enemyAgent.speed = enemySpeed;
        }

        resultText.text = "";
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeLimit);
    }

    public void AddScore(int amount)
    {
        if (!isGameStarted || isGameEnd)
        {
            return;
        }

        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score + " / " + clearScore;
    }

    public void GameOver()
    {
        if (isGameEnd) return;

        isGameEnd = true;
        resultText.text = "GAME OVER\nYour Score: " + score;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Clear()
    {
        if (isGameEnd) return;

        isGameEnd = true;
        resultText.text = "CLEAR!\nYour Score: " + score;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    private void OpenQuitConfirm()
    {
        quitGame = true;

        resultQuitCancel = isGameStarted && !isGameEnd;
        diffcultyQuitCancel = !isGameStarted && !isGameEnd;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (difficultyPanel != null)
        {
            difficultyPanel.SetActive(false);
        }

        resultText.text = "Quit Game?\nY: Yes   N: No";
    }

    private void CancelQuit()
    {
        quitGame = false;

        resultText.text = "";

        if (diffcultyQuitCancel && difficultyPanel != null)
        {
            difficultyPanel.SetActive(true);
        }

        if (resultQuitCancel)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}