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
    private float defaultSize;
    public float DefaultSize { get { return defaultSize; } }


    [SerializeField]
    private LayerMask collideMask;
    public LayerMask CollideMask { get { return collideMask; } }


}
