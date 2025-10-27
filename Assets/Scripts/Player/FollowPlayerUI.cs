using UnityEngine;

public class FollowPlayerUI : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, 0);

    void Awake()
    {
        gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
