using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuntimeObject", menuName = "New Runtime Object")]
public class Runtime : ScriptableObject
{
    [SerializeField]
    public float Value;
    [SerializeField]
    public bool Running;
    [SerializeField]
    public float Offset;
    [SerializeField]
    public float LastValue;
}
