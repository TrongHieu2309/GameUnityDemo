using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] private Transform previousLevel;
    [SerializeField] private Transform nextLevel;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                previousLevel.GetComponent<Level>().ActivateTraps(false);
                nextLevel.GetComponent<Level>().ActivateTraps(true);
            }
            else
            {
                previousLevel.GetComponent<Level>().ActivateTraps(true);
                nextLevel.GetComponent<Level>().ActivateTraps(false);
            }
        }
    }

    public void Reset()
    {
        previousLevel.GetComponent<Level>().ActivateTraps(false);
        nextLevel.GetComponent<Level>().ActivateTraps(true);
    }
}