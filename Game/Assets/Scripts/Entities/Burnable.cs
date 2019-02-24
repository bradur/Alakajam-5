// Date   : 23.02.2019 15:38
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

public class Burnable : MonoBehaviour
{

    private bool isLit = false;
    public bool IsLit { get { return isLit; } }

    [SerializeField]
    private MeshRenderer meshRenderer;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void Burn() {
        isLit = true;
        animator.SetTrigger("Burn");
        AudioManager.main.PlaySound(SoundType.LightFire);
        AudioManager.main.PlaySound(SoundType.Burn);
    }

    public void BurnFinished() {
        AudioManager.main.StopSound(SoundType.Burn);
        Destroy(gameObject);
    }

}
