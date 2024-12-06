using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerZone))]
public class TriggerZonEditor : Editor
{
    private TriggerZone zone;

    public void OnEnable()
    {
        zone = (TriggerZone)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        zone.type = (ZoneTypes)EditorGUILayout.EnumPopup("Trigger type ",zone.type);
        EditorGUILayout.EndHorizontal();

        switch(zone.type)
        {
            case ZoneTypes.Climb:

                EditorGUILayout.BeginHorizontal();
                zone.climb_right = EditorGUILayout.Toggle("Climb to the right :", zone.climb_right);
                EditorGUILayout.EndHorizontal();
                break;

            case ZoneTypes.BarbedWire:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("The player will die");
                EditorGUILayout.EndHorizontal();
                break;

            case ZoneTypes.Door:
                EditorGUILayout.BeginHorizontal();
                zone.nextDoor = (GameObject)EditorGUILayout.ObjectField("The next door to go :", zone.nextDoor, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                break;

            case ZoneTypes.SideOfRoom:

                EditorGUILayout.BeginHorizontal();
                zone.nextDoor = (GameObject)EditorGUILayout.ObjectField("The next door to go :", zone.nextDoor, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                break;
        }
    }
}
