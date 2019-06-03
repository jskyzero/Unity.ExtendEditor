using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

// A Text File Editor support json, xml file format.
// 
// 1. basic inspector editor
// 2. with style
// 3. compare with another

// // seems no need to create a individual windows
// public class TextFileEditor : EditorWindow {
//   string myString = "Hello World";
//   bool groupEnabled;
//   bool myBool = true;
//   float myFloat = 1.23f;

//   // Add menu item named "Text File Editor" to the Window menu
//   [MenuItem("Jsky/Text File Editor")]
//   public static void ShowWindow() {
//     //Show existing window instance. If one doesn't exist, make one.
//     EditorWindow.GetWindow(typeof(TextFileEditor));
//   }

//   void OnGUI() {
//     GUILayout.Label("Hello", EditorStyles.boldLabel);
//     myString = EditorGUILayout.TextField("Text Field", myString);

//     groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
//     myBool = EditorGUILayout.Toggle("Toggle", myBool);
//     myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
//     EditorGUILayout.EndToggleGroup();
//   }
// }

[CustomEditor(typeof(TextAsset), true)]
public class TextFileCustomEditor : Editor {

  private string textString = string.Empty;
  private string filePath => AssetDatabase.GetAssetPath(target);
  private string textAssetString => (target as TextAsset).text;
  private string loadString => File.ReadAllText(filePath);
  private TextAsset loadTextFile => Resources.Load(filePath) as TextAsset;

  private bool needStyle = false;
  private bool saveWithoutStyle = true;

  private class UIStringDict {
    public string SaveButtonName = "保存文件";
    public string ReloadButtonName = "重新加载";
    // public string NeedStyleButtonName = "格式化文档";
    // public string SaveWithoutStyle = "无格式保存";
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
    if (textString.Equals(string.Empty))
      textString = textAssetString;

    OnGUI_TitlePart();
    OnGUI_MainPart();
  }

  private void OnGUI_TitlePart() {
    GUILayout.BeginHorizontal(EditorStyles.toolbar);

    if (GUILayout.Button(kUIStringDict.SaveButtonName,
        EditorStyles.toolbarButton)) {
      SaveFile();
    }
    GUILayout.Space(5);
    if (GUILayout.Button(kUIStringDict.ReloadButtonName,
        EditorStyles.toolbarButton)) {
      ReloadFile();
    }
    // GUILayout.Space(5);
    // if (GUILayout.Button("TEST Format",
    //     EditorStyles.toolbarButton)) {
    //   FormatString("");
    // }
    GUILayout.FlexibleSpace();
    // needStyle = GUILayout.Toggle(needStyle,
    //   kUIStringDict.NeedStyleButtonName, EditorStyles.toolbarButton);
    // if (needStyle) {
    //   GUILayout.Space(5);
    //   saveWithoutStyle = GUILayout.Toggle(saveWithoutStyle,
    //     kUIStringDict.SaveWithoutStyle, EditorStyles.toolbarButton);
    // }

    GUILayout.EndHorizontal();
  }

  private void OnGUI_MainPart() {
    // GUIStyle style = new GUIStyle();
    // style.richText = true;
    // textString = EditorGUILayout.TextArea(textString, style);
    textString = EditorGUILayout.TextArea(textString);
  }

  private void ReloadFile() {
    textString = loadString;
  }

  private void SaveFile() {
    File.WriteAllText(filePath, textString);
  }

  private static string FormatString(string originString) {
    originString = "";
    string finalString = "";

    Func<string, string> formatJsonString = json => {
      dynamic parsedJson = JsonConvert.DeserializeObject(json);
      return JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
    };

    Func<string, string> formatXmlString = xml => {
      var stringBuilder = new StringBuilder();
      var element = XElement.Parse(xml);
      var settings = new XmlWriterSettings() {
        OmitXmlDeclaration = true,
        Indent = true,
        NewLineOnAttributes = true,
      };
      using(var xmlWriter = XmlWriter.Create(stringBuilder, settings)) {
        element.Save(xmlWriter);
      }
      return stringBuilder.ToString();
    };

    Func<string, string> formatOriginString = str => {
      return str;
    };

    return finalString;
  }

  // Override this method to return false if you don't want default margins.
  public override bool UseDefaultMargins() {
    return false;
  }
}