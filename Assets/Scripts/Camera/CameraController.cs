using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float camSpeed;
    private float lookAhead;

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + lookAhead, player.transform.position.y + 1, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * camSpeed);
    }
}
