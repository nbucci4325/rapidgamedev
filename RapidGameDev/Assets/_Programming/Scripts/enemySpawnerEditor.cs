using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(enemySpawner))]
public class enemySpawnerEditor : Editor
{
    private void OnSceneGUI()
    {
        enemySpawner script = (enemySpawner) target;
        Handles.color = script.color;
        Handles.DrawWireDisc(script.transform.position, Vector3.up, script.spawnerSize);
        EditorGUI.BeginChangeCheck();
        float newRadius = Handles.RadiusHandle(Quaternion.identity, script.transform.position, script.spawnerSize);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(script, "Change Radius");
            script.spawnerSize = newRadius;
        }
    }
}
