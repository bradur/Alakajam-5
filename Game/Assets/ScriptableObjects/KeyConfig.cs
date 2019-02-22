using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName="KeyConfig", menuName="New KeyConfig")]
public class KeyConfig : ScriptableObject {

    [SerializeField]
    private string objectName = "New KeyConfig";
    public string Name { get { return objectName; } }

    public List<GameKey> GameKeys;

}

public enum PlayerAction
{
    None
}

[System.Serializable]
public class GameKey : System.Object
{
    public List<KeyCode> keyCodes;
    public PlayerAction action;
}