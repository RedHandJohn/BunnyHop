using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BunnyHop.CameraScaling;

namespace BunnyHop.EditorScripts
{
    [CustomEditor(typeof(CameraAnchor))]
    public class CameraAnchorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            CameraAnchor cameraAnchor = (CameraAnchor)target;

            cameraAnchor.AnchorType = (CameraAnchorType)EditorGUILayout.EnumPopup("Anchor Type", cameraAnchor.AnchorType, options: null);

            if (GUILayout.Button("Set Initial Position"))
            {
                cameraAnchor.SetInitialPosition();
            }

            EditorGUILayout.LabelField("Offset: " + cameraAnchor.Offset.ToString("F8"));
        }
    }
}
