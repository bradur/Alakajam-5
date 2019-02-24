using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public List<GameObject> effects;

    public ParticleSystem jumpEffect;

    private PlayerHandConfig config;

    private bool grabbed = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        config = ConfigManager.main.GetConfig("PlayerHandConfig") as PlayerHandConfig;
        animator = GetComponent<Animator>();
        config.hasFire = false;

        if (effects != null)
        {
            foreach(GameObject effect in effects)
            {
                effect.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (config.triggerGrab)
        {
            config.hasFire = true;
            config.triggerGrab = false;
            if (!grabbed)
            {
                animator.SetBool("grabbed", true);
                grabbed = true;
                foreach (GameObject effect in effects)
                {
                    effect.SetActive(true);
                }
            }
        }

        if (config.triggerThrow)
        {
            config.hasFire = false;
            config.triggerThrow = false;
            animator.SetBool("grabbed", false);
            grabbed = false;
            foreach (GameObject effect in effects)
            {
                effect.SetActive(false);
            }
        }

        if (config.triggerJump)
        {
            config.triggerJump = false;
            if (!grabbed)
            {
                animator.SetTrigger("jump");
                //jumpEffect.Stop();
                jumpEffect.Play();
            }
        }

        if (config.triggerSnap)
        {
            config.hasFire = false;
            config.triggerSnap = false;
            animator.SetTrigger("snap");
            foreach (GameObject effect in effects)
            {
                effect.SetActive(false);
            }
        }
    }
}
