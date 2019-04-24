using UnityEditor;
using UnityEngine;
using System.IO;

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

  // constructor
  TextFileCustomEditor() : base() {
    Debug.Log("TextFileCustomEditor Construct");

    // textString = ;
    // TextAsset textFile = Resources.Load(filePath) as TextAsset;
    // Debug.Log(textFile);
  }

  // on gui
  public override void OnInspectorGUI() {
    bool enabled = GUI.enabled;
    GUI.enabled = true;
    OnGUI_Main();
    GUI.enabled = enabled;
  }

  private void OnGUI_Main() {
    if (textString.Equals(string.Empty)) textString = textAssetString;
    textString = EditorGUILayout.TextArea(textString);
  }

  private void OnGUI_Header() {

  }

  // private void 
}