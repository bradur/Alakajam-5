using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFinalTime : MonoBehaviour
{
    [SerializeField]
    private Runtime runtime;
    [SerializeField]
    private GameObject finalRuntimeUIElement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        finalRuntimeUIElement.SetActive(!runtime.Running);
    }
}
