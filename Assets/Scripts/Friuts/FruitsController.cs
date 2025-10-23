using UnityEngine;

public class FruitsController : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeFruits()
    {
        anim.SetTrigger("collected");
    }

    public void DestroyFruits()
    {
        Destroy(gameObject);
    }
}
