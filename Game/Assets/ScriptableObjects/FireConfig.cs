// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewFireConfig", menuName = "New FireConfig")]
public class FireConfig : ScriptableObject
{

    [SerializeField]
    private float maxDistance = 10f;
    public float MaxDistance { get { return maxDistance; } }

    [SerializeField]
    private float minDistance = 10f;
    public float MinDistance { get { return minDistance; } }

    [Header("Torch")]

    [SerializeField]
    private Material torchHighlightMaterial;
    public Material TorchHighlightMaterial { get { return torchHighlightMaterial; } }

    [SerializeField]
    private Material torchDeniedMaterial;
    public Material TorchDeniedMaterial { get { return torchDeniedMaterial; } }

    [SerializeField]
    private Material torchDefaultMaterial;
    public Material TorchDefaultMaterial { get { return torchDefaultMaterial; } }

    [Header("Burnable")]
    [SerializeField]
    private Material burnableHighlightMaterial;
    public Material BurnableHighlightMaterial { get { return burnableHighlightMaterial; } }

    [SerializeField]
    private Material burnableDefaultMaterial;
    public Material BurnableDefaultMaterial { get { return burnableDefaultMaterial; } }

    private Material burnableIsBurningMaterial;
    public Material BurnableIsBurningMaterial { get { return burnableIsBurningMaterial; } }

}
