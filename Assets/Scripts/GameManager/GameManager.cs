using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioClip checkPointSound;
    [SerializeField] private AudioClip respawnSound;
    
    public static GameManager instance { get; private set; }
    private Transform respawnPoint = null;
    private List<GameObject> activatedCheckpoints = new List<GameObject>();
    private PlayerController playerController;
    private float score = 0;
    public bool isRespawn;

    void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        instance = this;
    }

    void Start()
    {
        UpdateScore();
    }

    public bool HasRespawnPoint()
    {
        return respawnPoint != null;
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        if (!activatedCheckpoints.Contains(newRespawnPoint.gameObject))
        {
            SoundManager.instance.PlaySound(checkPointSound);
            activatedCheckpoints.Add(newRespawnPoint.gameObject);
        }

        respawnPoint = newRespawnPoint;

        foreach (var checkpoint in activatedCheckpoints)
        {
            if (checkpoint != null && checkpoint != newRespawnPoint.gameObject)
            {
                Destroy(checkpoint);
            }
        }
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            isRespawn = true;
            Health.instance.isDead = false;
            SoundManager.instance.PlaySound(respawnSound);
            SpikedBall spikedBall = FindAnyObjectByType<SpikedBall>();
            if (spikedBall != null)
            {
                spikedBall.ResetToInitial();
            }

            Door door = FindAnyObjectByType<Door>();
            if (door != null)
            {
                door.Reset();
            }
            playerController.transform.position = respawnPoint.position;
            Time.timeScale = 1f;
        }
    }

    public void AddScore(float points)
    {
        score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
