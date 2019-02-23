// Date   : 23.02.2019 14:01
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

public class TorchCollider : MonoBehaviour
{
    [SerializeField]
    private FireSource fireSource;
    public FireSource FireSource { get { return fireSource; } }

    private MeshRenderer meshRenderer;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetMaterial(Material material) {
        meshRenderer.material = material;
    }
}
