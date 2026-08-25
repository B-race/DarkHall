using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnPoint;

    public static string NextSpawnPoint { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NextSpawnPoint = targetSpawnPoint;
            SceneManager.LoadScene(targetScene);
        }
    }
}