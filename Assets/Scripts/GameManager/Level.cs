using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject[] traps;

    private Vector3[] initialPositions;

    void Awake()
    {
        // Save initial positions for all traps
        initialPositions = new Vector3[traps.Length];

        //// FIRST WAY
        // for (int i = 0; i < traps.Length; i++)
        // {
        //     if (traps[i] != null)
        //     {
        //         initialPositions[i] = traps[i].transform.position;
        //     }
        // }

        //// SECOND WAY
        foreach (var trap in traps)
        {
            initialPositions[System.Array.IndexOf(traps, trap)] = trap.transform.position;
        }
    }

    public void ActivateTraps(bool _static)
    {
        //// FIRST WAY
        // for (int i = 0; i < traps.Length; i++)
        // {
        //     if (traps[i] != null)
        //     {
        //         traps[i].transform.position = initialPositions[i];
        //         Debug.Log($"Reset trap {i} to {initialPositions[i]}");
        //     }
        // }

        //// SECOND WAY
        foreach (var trap in traps)
        {
            if (trap != null)
            {
                int index = System.Array.IndexOf(traps, trap);
                if (trap.CompareTag("SpikedBall"))
                {
                    Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        trap.GetComponent<SpikedBall>().ResetToInitial();
                        trap.transform.position = initialPositions[index];
                    }
                }
                else
                {
                    trap.transform.position = initialPositions[index];
                }
            }
        }
    }
}