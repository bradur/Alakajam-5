using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName="KeyConfig", menuName="New KeyConfig")]
public class KeyConfig : ScriptableObject {

    public List<GameKey> GameKeys;

}

public enum PlayerAction
{
    None,
    Teleport,
    ExtinguishOrLightFire,
    Restart,
    Quit
}

[System.Serializable]
public class GameKey : System.Object
{
    public List<KeyCode> keyCodes;
    public PlayerAction action;
}