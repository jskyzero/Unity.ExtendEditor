using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// A Text File Editor support json, xml file format.
// 
// 1. basic inspector editor
// 2. with style
// 3. compare with another

public class TextFileEditor : EditorWindow {
  string myString = "Hello World";
  bool groupEnabled;
  bool myBool = true;
  float myFloat = 1.23f;

  // Add menu item named "Text File Editor" to the Window menu
  [MenuItem("Jsky/Text File Editor")]
  public static void ShowWindow() {
    //Show existing window instance. If one doesn't exist, make one.
    EditorWindow.GetWindow(typeof(TextFileEditor));
  }

  void OnGUI() {
    GUILayout.Label("Hello", EditorStyles.boldLabel);
    myString = EditorGUILayout.TextField("Text Field", myString);

    groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
    myBool = EditorGUILayout.Toggle("Toggle", myBool);
    myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
    EditorGUILayout.EndToggleGroup();
  }
}

[CustomEditor(typeof(TextAsset), true)]
public class TextFileCustomEditor : Editor {

  private string textString = string.Empty;
  private string filePath => AssetDatabase.GetAssetPath(target);
  private string textAssetString => (target as TextAsset).text;
  private string loadString => File.ReadAllText(filePath);
  private TextAsset loadTextFile => Resources.Load(filePath) as TextAsset;

  private class UIStringDict {
    public string SaveButtonName = "保存文件";
    public string ReloadButtonName = "重新加载";
    public string NeedStyleButtonName = "格式化文档";
  }

  private static UIStringDict kUIStringDict = new UIStringDict();

  // constructor
  TextFileCustomEditor() : base() {
    // Debug.Log("TextFileCustomEditor Construct");
  }

  // on gui
  public override void OnInspectorGUI() {
    // use a action
    Action<Action> enable = (action) => {
      bool enabled = GUI.enabled;
      GUI.enabled = true; 
      action.Invoke();
      GUI.enabled = enabled;
    };
    // invoke with main gui
    enable.Invoke(OnGUI_Main);
  }

  private void OnGUI_Main() {
    if (textString.Equals(string.Empty)) textString = textAssetString;

    OnGUI_TitlePart();

    GUIStyle style = new GUIStyle();
    style.richText = true;
    textString = EditorGUILayout.TextArea(textString, style);
  }

  private void OnGUI_TitlePart() {
    GUILayout.BeginHorizontal(EditorStyles.toolbar);

    if (GUILayout.Button(kUIStringDict.SaveButtonName, EditorStyles.toolbarButton)) {
      SaveFile();
    }
    GUILayout.Space(5);
    if (GUILayout.Button(kUIStringDict.ReloadButtonName, EditorStyles.toolbarButton)) { }
    GUILayout.FlexibleSpace();
    GUILayout.Toggle(true,
      "Preview", EditorStyles.toolbarButton);

    GUILayout.EndHorizontal();
  }

  private void ReloadFile() {
    textString = loadString;
  }

  private void SaveFile() {
    File.WriteAllText(filePath, textString);
  }

  // Override this method to return false if you don't want default margins.
  public override bool UseDefaultMargins() {
    return false;
  }
}