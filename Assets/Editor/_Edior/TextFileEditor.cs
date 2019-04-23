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
  private string filePath => AssetDatabase.GetAssetPath(target);
  private TextAsset textFile => Resources.Load(filePath) as TextAsset;
  private string textString;

  // constructor
  TextFileCustomEditor() : base() {
    Debug.Log("TextFileCustomEditor Construct");

    // textString = File.ReadAllText(filePath);
    // Debug.Log(filePath);
    // TextAsset textFile = Resources.Load(filePath) as TextAsset;
    // Debug.Log(textFile);
  }

  // on gui
  public override void OnInspectorGUI() {


    // if (!useDefaultGUI) {
    bool enabled = GUI.enabled;
    GUI.enabled = true;
    showTextFileGUI();
    GUI.enabled = enabled;
    // } else {
    //   useDefaultGUI = EditorGUILayout.Toggle(useDefaultGUI);
    //   // origin base GUI version
    //   // base.OnInspectorGUI();
    // }
  }

  private void showTextFileGUI() {
    textString = EditorGUILayout.TextArea((target as TextAsset).text);
  }
}