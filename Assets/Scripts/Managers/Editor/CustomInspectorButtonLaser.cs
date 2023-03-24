using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Managers.Editor
{
    [CustomEditor(typeof(Laser))]
    public class CustomInspectorButtonLaser : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var laser = (Laser)target;
            if (GUILayout.Button("Update Beam Params"))
            {
                laser.UpdateBeamParams();
            }
        }
    }
}