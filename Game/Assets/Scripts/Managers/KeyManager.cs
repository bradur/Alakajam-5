// Date   : 23.04.2017 11:09
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class KeyManager : MonoBehaviour
{

    public static KeyManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private KeyConfig keyConfig;

    public bool GetKeyDown(PlayerAction action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKeyDown(keyCode))
            {
                DebugLogger.main.LogMessage("Key {0} pressed down to perform {1}.", GetKeyString(action), action.ToString());
                return true;
            }
        }
        return false;
    }

    public bool GetKeyUp(PlayerAction action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKeyUp(keyCode))
            {
                DebugLogger.main.LogMessage("Key {0} let up to perform {1}.", GetKeyString(action), action.ToString());
                return true;
            }
        }
        return false;
    }

    public bool GetKey(PlayerAction action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKey(keyCode))
            {
                DebugLogger.main.LogMessage("Key {0} held to perform {1}.", GetKeyString(action), action.ToString());
                return true;
            }
        }
        return false;
    }

    public bool GetKeyCombo(PlayerAction actionA, PlayerAction actionB) {
        return GetKey(actionA) && GetKey(actionB);
    }

    public List<KeyCode> GetKeyCodes(PlayerAction action)
    {
        foreach (GameKey gameKey in keyConfig.GameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.keyCodes;
            }
        }
        return Enumerable.Empty<KeyCode>().ToList<KeyCode>();
    }

    public string GetKeyString(PlayerAction action)
    {
        List<string> keyStrings = new List<string>();
        foreach (GameKey gameKey in keyConfig.GameKeys)
        {
            if (gameKey.action == action)
            {
                foreach (KeyCode keyCode in gameKey.keyCodes)
                {
                    if (keyCode == KeyCode.Return)
                    {
                        keyStrings.Add("Enter");
                    }
                    else if (keyCode == KeyCode.RightControl)
                    {
                        keyStrings.Add("Right Ctrl");
                    }
                    else if (keyCode == KeyCode.Escape)
                    {
                        keyStrings.Add("Esc");
                    }
                    else
                    {
                        keyStrings.Add(keyCode.ToString());
                    }
                }
            }
        }
        return string.Join(" / ", keyStrings.ToArray());
    }
}
