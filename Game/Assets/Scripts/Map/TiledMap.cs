using UnityEngine;
using TiledSharp;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

public class TiledMap : MonoBehaviour
{

    private List<Transform> containers = new List<Transform>();

    private Transform containerPrefab;

    private MeshTileMap meshTileMapPrefab;

    private CombinedMeshLayer combinedMeshLayerPrefab;

    private GameConfig gameConfig;

    private GameObject wallPrefab;

    private void Start()
    {
        XDocument mapX = XDocument.Parse(gameConfig.MapFile.text);
        TmxMap map = new TmxMap(mapX);
        combinedMeshLayerPrefab = gameConfig.CombinedMeshLayerPrefab;
        Initialize(map);
    }

    private void Initialize(TmxMap map)
    {
        meshTileMapPrefab = Resources.Load<MeshTileMap>("MeshTileMap");
        DrawLayers(map);
    }

    private void DrawLayers(TmxMap map)
    {
        int mapHeight = map.Height;
        
        foreach (TmxLayer layer in map.Layers)
        {
            string layerType = Tools.GetProperty(layer.Properties, "Type");
            if (layerType == "Ground") {
                MeshTileMap meshTileMap = Instantiate(meshTileMapPrefab);
                meshTileMap.Init(layer, map);
            } else if (layerType == "Wall") {
                CombinedMeshLayer combinedMeshLayer = SpawnWallLayer(layer);
            }
        }
    }

    private CombinedMeshLayer SpawnWallLayer(TmxLayer layer) {
        CombinedMeshLayer meshLayer = Instantiate(combinedMeshLayerPrefab);
        meshLayer.Initialize(layer.Name);

        Transform wallContainer = GetContainer("Walls");
        meshLayer.transform.SetParent(wallContainer);
        foreach (TmxLayerTile tile in layer.Tiles) {
            if (tile.Gid != 0) {
                GameObject wall = Instantiate(wallPrefab);
                if (wallPrefab != null) {
                    MeshFilter meshFilter = wall.GetComponent<MeshFilter>();
                    if (meshFilter != null) {
                        meshLayer.Add(meshFilter);
                    }
                } else {
                    DebugLogger.main.LogWarning("Could not find wall for gid {0}", tile.Gid);
                }
            }
        }
        meshLayer.Build();
        return meshLayer;
    }

    private void SpawnObjects(TmxMap map)
    {
        int mapHeight = map.Height;
        foreach (TmxObjectGroup objectGroup in map.ObjectGroups)
        {
            foreach (TmxObject tmxObject in objectGroup.Objects)
            {
                int xPos = (int)(tmxObject.X);
                int yPos = (int)(tmxObject.Y);
                SpawnObject(
                    xPos,
                    yPos,
                    tmxObject
                );
            }
        }
    }


    private void SpawnObject(int x, int y, TmxObject tmxObject)
    {
        if (tmxObject.Tile != null)
        {

        }
    }

    private Transform GetContainer(string containerName)
    {
        Transform newContainer = null;
        foreach (Transform container in containers)
        {
            if (container.name == containerName)
            {
                newContainer = container;
                break;
            }
        }
        if (newContainer == null)
        {
            newContainer = Instantiate(containerPrefab);
            newContainer.name = containerName;
            containers.Add(newContainer);
        }
        return newContainer;
    }


}
