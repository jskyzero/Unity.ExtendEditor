using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class CustomEditors : MonoBehaviour {
    public Vector3 lookAtPoint = Vector3.zero;

    public void Update() {
        transform.LookAt(lookAtPoint);
    }
}

[CustomEditor(typeof(CustomEditors))]
[CanEditMultipleObjects]
public class CustomEditorsEditor : Editor {
    SerializedProperty lookAtPoint;

    void OnEnable() {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(lookAtPoint);
        serializedObject.ApplyModifiedProperties();

        if (lookAtPoint.vector3Value.y < (target as CustomEditors).transform.position.y) 
            EditorGUILayout.LabelField("Above this object");
        else if (lookAtPoint.vector3Value.y > (target as CustomEditors).transform.position.y)
            EditorGUILayout.LabelField("Below this object");
    }

    public void OnSceneGUI() {
        var t = (target as CustomEditors);

        EditorGUI.BeginChangeCheck();
        Vector3 pos = Handles.PositionHandle(t.lookAtPoint, Quaternion.identity);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(target, "Move Point");
            t.lookAtPoint = pos;
            t.Update();
        }
    }
}