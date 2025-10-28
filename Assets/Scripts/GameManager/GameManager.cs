using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Transform respawnPoint = null;
    private List<GameObject> activatedCheckpoints = new List<GameObject>();
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        instance = this;
    }

    public bool HasRespawnPoint()
    {
        return respawnPoint != null;
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;

        foreach (var checkpoint in activatedCheckpoints)
        {
            if (checkpoint != null && checkpoint != newRespawnPoint.gameObject)
            {
                Destroy(checkpoint);
            }
        }

        activatedCheckpoints.Clear();
        activatedCheckpoints.Add(newRespawnPoint.gameObject);
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            playerController.transform.position = respawnPoint.position;
            Time.timeScale = 1f;
        }
    }
}
