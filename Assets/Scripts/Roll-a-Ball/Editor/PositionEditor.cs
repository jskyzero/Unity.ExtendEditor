using System;
using UnityEditor;
using UnityEngine;

public class PositionEditor : EditorWindow {

  [MenuItem("Jsky/Position Editor")]
  private static void ShowWindow() {
    EditorWindow.GetWindow(typeof(PositionEditor));
  }

  // GUI variables
  private int levelIndex = 0;
  private string[] levelTitle = { "level 1", "level 2", "level 3" };
  private int itemIndex = 0;
  private string[] itemTitle = { "item 1", "item 2", "item 3", "item 3" };

  private Vector2 levelViewVector = Vector2.zero;
  private Vector2 itemViewVector = Vector2.zero;

  // root gui construct
  private void OnGUI() {
    var width = Screen.width;
    var height = Screen.height;

    GUI.Label(new Rect(5, 0, width, 20), "roll-a-ball position editor");
    OnGUI_LevelPart(width, height);
    OnGUI_ItemPart(width, height);
  }

  // level part gui
  private void OnGUI_LevelPart(int width, int height) {
    GUI.BeginGroup(new Rect(0, 20, width / 3, height));
    GUI.Box(new Rect(0, 0, width / 3, height), "");

    var levelHeight = levelTitle.Length * 40;
    levelViewVector = GUI.BeginScrollView(new Rect(0, 0, width / 3, height),
      levelViewVector, new Rect(0, 0, width / 3, Math.Max(levelHeight, height)));
    levelIndex = GUI.SelectionGrid(new Rect(0, 0, width / 3, levelHeight), levelIndex,
      levelTitle, 1);

    GUI.EndScrollView();
    GUI.EndGroup();
  }

  private void OnGUI_ItemPart(int width, int height) {
    // var width = Screen.width;
    // var height = Screen.height;
    GUI.BeginGroup(new Rect(width / 3, 20, width / 3 * 2, height));
    var itemHeignt = itemTitle.Length * 25;
    itemViewVector = GUI.BeginScrollView(new Rect(0, 0, width / 3 * 2, height),
      itemViewVector, new Rect(0, 0, width / 3 * 2, Math.Min(itemHeignt, height)));
    itemIndex = GUI.SelectionGrid(new Rect(0, 0, width / 3 * 2, itemHeignt), itemIndex,
      itemTitle, 1);
    GUI.EndScrollView();
    GUI.EndGroup();
  }
}