using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public GameObject fireEffect;

    private PlayerHandConfig config;

    private bool grabbed = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        config = ConfigManager.main.GetConfig("PlayerHandConfig") as PlayerHandConfig;
        animator = GetComponent<Animator>();

        if (fireEffect != null)
        {
            fireEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (config.triggerGrab)
        {
            config.triggerGrab = false;
            if (!grabbed)
            {
                animator.SetBool("grabbed", true);
                grabbed = true;
                if (fireEffect != null)
                {
                    fireEffect.SetActive(true);
                }
            }
        }

        if (config.triggerThrow)
        {
            config.triggerThrow = false;
            animator.SetBool("grabbed", false);
            grabbed = false;
            if (fireEffect != null)
            {
                fireEffect.SetActive(false);
            }
        }

        if (config.triggerJump)
        {
            config.triggerJump = false;
            if (!grabbed)
            {
                animator.SetTrigger("jump");
            }
        }
    }
}
