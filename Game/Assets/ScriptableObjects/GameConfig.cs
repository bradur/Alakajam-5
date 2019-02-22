// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewGameConfig", menuName = "New GameConfig")]
public class GameConfig : ScriptableObject
{

    [Header("World")]
    [SerializeField]
    private TextAsset mapFile;

    public TextAsset MapFile { get { return mapFile; } }


    [Header("Prefabs")]
    [SerializeField]
    private CombinedMeshLayer combinedMeshLayerPrefab;

    public CombinedMeshLayer CombinedMeshLayerPrefab { get { return combinedMeshLayerPrefab; } }

    [SerializeField]
    private Transform containerPrefab;
    public Transform ContainerPrefab { get { return containerPrefab; } }

    [Header("Debugging")]

    public bool VisualDebug = false;

    public LogLevel LogLevel;

}
