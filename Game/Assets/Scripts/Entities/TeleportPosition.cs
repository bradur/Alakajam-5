// Date   : 23.02.2019 09:43
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

using UnityEditor;

[CustomEditor(typeof(TeleportPosition))]
public class TeleportPositionEditor : Editor
{
    public void OnSceneGUI()
    {
        TeleportPosition teleportPosition = this.target as TeleportPosition;
        Handles.color = Color.green;
        Handles.DrawWireDisc(
            teleportPosition.transform.position,
            teleportPosition.transform.up,
            0.25f
        );
    }

}

public class TeleportPosition : MonoBehaviour
{

}
