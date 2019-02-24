using UnityEngine;
using System.Collections;

using UnityEditor;

[CustomEditor(typeof(FireSource))]
public class FireSourceEditor : Editor
{
    private FireSource fireSource;

    private SpriteRenderer debugRenderer;

    public void OnSceneGUI()
    {
        fireSource = this.target as FireSource;
        debugRenderer = fireSource.GetComponentInChildren<SpriteRenderer>();
        Vector3 scale = new Vector3(fireSource.Range * 2, fireSource.Range * 2, 1f);
        Vector3 pos = debugRenderer.transform.position;
        pos.y = 0.1f;
        debugRenderer.transform.position = pos;
        debugRenderer.transform.localScale = scale;
    }

}