using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName= "PlayerHandConfig", menuName="New PlayerHandConfig")]
public class PlayerHandConfig : ScriptableObject {

    public bool triggerGrab;
    public bool triggerThrow;
    public bool triggerJump;
    public bool triggerSnap;

    public bool hasFire = false;

}