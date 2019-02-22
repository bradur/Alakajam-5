/*
 from https://forum.unity.com/threads/drawing-a-sprite-in-editor-window.419199/
 */
using UnityEngine;
using System.Collections.Generic;

namespace UnityEditor
{

    [CustomPropertyDrawer(typeof(PreviewTexture2DAttribute))]
    public class PreviewTexture2DDrawer : PropertyDrawer
    {

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.PropertyField(property, true);

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue != null)
                {
                    Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
                    if (previewTexture != null)
                    {
                        GUILayout.Label(
                            previewTexture,
                            GUILayout.Width(previewTexture.width),
                            GUILayout.Height(previewTexture.height)
                        );
                    }
                }
            }
        }

    }
}