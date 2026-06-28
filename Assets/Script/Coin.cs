using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;
    private float rotateSpeed = 180f;

    private void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
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