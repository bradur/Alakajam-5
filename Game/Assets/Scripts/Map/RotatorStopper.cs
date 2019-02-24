using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorStopper : MonoBehaviour
{
    [SerializeField]
    Rotator rotator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if(other.tag == "MainCamera")
        {
            Debug.Log("Stopper called");
            rotator.Stop();
        }
    }
}
