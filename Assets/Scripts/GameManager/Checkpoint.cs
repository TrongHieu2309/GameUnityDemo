using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] private Transform currentLevel;

    void Update()
    {
        if (GameManager.instance.isRespawn == true)
        {
            currentLevel.GetComponent<Level>().ActivateTraps(false);
        }
    }
}