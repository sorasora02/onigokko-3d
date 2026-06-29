using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;
    public TMP_Text scoreText;
    public Transform coinModel;

    private float rotateSpeed = 180f;

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        if (coinModel != null)
        {
            coinModel.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "+" + scoreValue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}