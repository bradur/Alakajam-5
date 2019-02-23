// Date   : 23.02.2019 14:01
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

public class BurnableCollider : MonoBehaviour
{
    [SerializeField]
    private Burnable burnable;
    public Burnable Burnable { get { return burnable; } }

    private MeshRenderer meshRenderer;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetMaterial(Material material) {
        meshRenderer.material = material;
    }
}
