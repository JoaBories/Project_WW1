using System;
using UnityEditor;
using UnityEditor.Rendering;
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
        zone.type = (ZoneTypes)EditorGUILayout.EnumPopup(zone.type);
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
                EditorGUILayout.BeginHorizontal();
                zone.toRight = EditorGUILayout.Toggle("go out to the right :", zone.toRight);
                EditorGUILayout.EndHorizontal();
                break;

            case ZoneTypes.Crate:
                EditorGUILayout.BeginHorizontal();
                zone.crateObject = (GameObject)EditorGUILayout.ObjectField("The crate object to push : ", zone.crateObject, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                zone.crateMovement = EditorGUILayout.FloatField("The movement of the crate when pushed : ", zone.crateMovement);
                EditorGUILayout.EndHorizontal();
                break;

            case ZoneTypes.Radio:
                EditorGUILayout.BeginHorizontal();
                zone.radioObject = (GameObject)EditorGUILayout.ObjectField("The radio object to break : ", zone.radioObject, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                zone.brokenRadio = (Sprite)EditorGUILayout.ObjectField("The broken radio sprite : ", zone.brokenRadio, typeof(Sprite), true);
                EditorGUILayout.EndHorizontal();
                break;

            case ZoneTypes.Shootings:
                EditorGUILayout.BeginHorizontal();
                zone.coolDown = EditorGUILayout.FloatField("Shooting cooldown", zone.coolDown);
                EditorGUILayout.EndHorizontal();
                break;
            case ZoneTypes.SceneChangeSideOfRoom:
                EditorGUILayout.BeginHorizontal();
                zone.sceneNum = EditorGUILayout.IntField("Number of the scene", zone.sceneNum);
                EditorGUILayout.EndHorizontal();
                break;
        }
    }
}
