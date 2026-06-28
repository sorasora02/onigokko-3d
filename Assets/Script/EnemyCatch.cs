using Unity.VisualScripting;
using UnityEngine;

public class EnemyCatch : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.GameOver();
        }
    }
}
