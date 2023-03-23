using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Managers.Editor
{
    [CustomEditor(typeof(ParameterManager))]
    public class CustomInspectorButton : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var parameterManager = (ParameterManager) target;
            if (GUILayout.Button("Apply Scale"))
            {
                parameterManager.ChangeScale();
            }
        }
    }
}