using UnityEngine;

public class FruitsController : MonoBehaviour
{
    [SerializeField] private AudioClip collected;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeFruits()
    {
        anim.SetTrigger("collected");
        SoundManager.instance.PlaySound(collected);
    }

    public void DestroyFruits()
    {
        Destroy(gameObject);
    }
}
