using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PositionEditor : EditorWindow {

  [MenuItem("Jsky/Position Editor")]
  private static void ShowWindow() {
    EditorWindow.GetWindow(typeof(PositionEditor));
  }

  private LevelData levelData;

  // GUI variables
  private int levelIndex = 0;
  private bool needLogs = true;

  private Vector2 levelViewVector = Vector2.zero;
  private Vector2 itemViewVector = Vector2.zero;

  // private PositionEditor() { Debug.Log("Construct"); }
  // ~PositionEditor() { Debug.Log("Release"); }

  private void OnDestroy() {
    levelData = null;
    Debug.Log("Position Editor Destroyed");
  }

  // root gui construct
  private void OnGUI() {
    // load data
    if (levelData == null) LoadEditorConfig();
    // gui variable
    // var width = Screen.width / (int) EditorGUIUtility.pixelsPerPoint;
    // var height = Screen.height / (int) EditorGUIUtility.pixelsPerPoint;

    OnGUI_TitlePart();
    GUILayout.BeginHorizontal();
    this.OnGUI_LevelPart();
    this.OnGUI_ItemPart();
    GUILayout.EndHorizontal();
  }

  private void OnGUI_TitlePart() {
    GUILayout.BeginHorizontal(EditorStyles.toolbar);

    if (GUILayout.Button("Save Config", EditorStyles.toolbarButton)) {
         SaveEditorConfig();
        //  EditorGUIUtility.ExitGUI();
    }
    GUILayout.Space(5);

    needLogs = GUILayout.Toggle(needLogs, 
      "Show Logs(useless now)", EditorStyles.toolbarButton);

    GUILayout.FlexibleSpace();

    if (GUILayout.Button("Reload Config", EditorStyles.toolbarButton)) {
      LoadEditorConfig();
    }
    GUILayout.EndHorizontal();
  }

  // level part gui
  private void OnGUI_LevelPart() {
    var levelTitle = Enumerable.Range(0, levelData.LevelCount)
      .Select(x => x.ToString()).ToArray();

    levelViewVector = GUILayout.BeginScrollView(
      levelViewVector, false, false,
      GUILayout.MinWidth(300 / (int) EditorGUIUtility.pixelsPerPoint), 
      GUILayout.MaxWidth(400 / (int) EditorGUIUtility.pixelsPerPoint));

    levelIndex = GUILayout.SelectionGrid(
      levelIndex, levelTitle, 1);
    GUILayout.FlexibleSpace();

    if (GUILayout.Button("Add")) {
      levelData.AddLevel();
    }

    if (GUILayout.Button("Delete")) {
      if (levelData.LevelCount == 0) return;
      levelData.DeleteLevel(levelIndex);
      levelIndex = levelIndex == 0 ? 0 : levelIndex - 1;
    }

    GUILayout.EndScrollView();
  }

  private void OnGUI_ItemPart() {
    if (levelData.LevelCount == 0) return;
    var eachLevelData = levelData[levelIndex];
    itemViewVector = GUILayout.BeginScrollView(itemViewVector);
    if (GUILayout.Button("Add")) {
      eachLevelData.Add(new BoxData());
    }

    for (int i = 0; i < eachLevelData.Count; i++) {
      GUILayout.Space(5);
      GUILayout.Label("index: " + i.ToString());
      eachLevelData[i].x_percent = 
        EditorGUILayout.FloatField("x", eachLevelData[i].x_percent);
      eachLevelData[i].z_percent = 
        EditorGUILayout.FloatField("z", eachLevelData[i].z_percent);
      if (GUILayout.Button("Delete")) {
        eachLevelData.RemoveAt(i);
      }
    }

    GUILayout.EndScrollView();
  }

  private void LoadEditorConfig() {
    levelData = new LevelData(Application.dataPath);
  }

  private void SaveEditorConfig() {
    levelData.SaveJson();
  }
}