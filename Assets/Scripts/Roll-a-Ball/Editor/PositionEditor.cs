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
  private int itemIndex = 0;

  private Vector2 levelViewVector = Vector2.zero;
  private Vector2 itemViewVector = Vector2.zero;

  private PositionEditor() { }

  // root gui construct
  private void OnGUI() {
    if (levelData == null) levelData = new LevelData(Application.dataPath);

    var width = Screen.width / (int) EditorGUIUtility.pixelsPerPoint;
    var height = Screen.height / (int) EditorGUIUtility.pixelsPerPoint;

    GUILayout.Label("roll-a-ball position editor");

    GUILayout.BeginHorizontal();
    this.OnGUI_LevelPart();
    this.OnGUI_ItemPart();
    GUILayout.EndHorizontal();

    if (levelData.NeedSave) SaveEditorConfig();
  }

  // level part gui
  private void OnGUI_LevelPart() {
    var levelTitle = Enumerable
      .Range(0, levelData.LevelCount)
      .Select(x => x.ToString()).ToArray();

    levelViewVector = GUILayout.BeginScrollView(
      levelViewVector, false, false,
      GUILayout.MinWidth(300), GUILayout.MaxWidth(400));
    levelIndex = GUILayout.SelectionGrid(
      levelIndex, levelTitle, 1, GUILayout.Height(50 * levelTitle.Length));
    GUILayout.FlexibleSpace();
    if (GUILayout.Button("Add", GUILayout.Height(50))) {
      levelData.AddLevel();
    }
    GUILayout.EndScrollView();
  }

  private void OnGUI_ItemPart() {
    var eachLevelData = levelData[levelIndex];
    itemViewVector = GUILayout.BeginScrollView(itemViewVector);


    GUILayout.EndScrollView();
  }

  private void SaveEditorConfig() {
    levelData.SaveJson();
  }
}