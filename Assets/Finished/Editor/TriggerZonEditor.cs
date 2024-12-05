using UnityEditor;

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


        }
    }
}
