using UnityEngine;
using UnityEditor;


public class PositionEditor : EditorWindow {

  [MenuItem("Jsky/Position Editor")]
  private static void ShowWindow() {
    EditorWindow.GetWindow(typeof(PositionEditor));
  }


  private void OnGUI() {
    GUILayout.Label("Position Editor");
  }
}