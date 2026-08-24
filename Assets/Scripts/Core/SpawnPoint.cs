using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnPointName;

    private void Start()
    {
        if (Door.NextSpawnPoint != spawnPointName)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = transform.position;
        }
    }
}