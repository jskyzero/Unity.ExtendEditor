#if UNITY_EDITOR

namespace JSKY.Editor {

  using System.Linq;
  using System;
  using Sirenix.OdinInspector.Editor;
  using Sirenix.OdinInspector;
  using Sirenix.Utilities.Editor;
  using Sirenix.Utilities;
  using UnityEditor;
  using UnityEngine.SceneManagement;
  using UnityEngine;

  public class PostionEditor2 : OdinEditorWindow {
    [MenuItem("Tools/Jsky/Position Editor2")]
    private static void OpenWindow() {
      var window = GetWindow<PostionEditor2>();
      window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
      window.init();
    }

    private void init() {
      // find system
      try {
        var scene = SceneManager.GetActiveScene();
        systemObj = scene.GetRootGameObjects().First(
          (obj) => obj.name == "System.Logic");
      } catch (InvalidOperationException) {
        // log and return
        Debug.LogError("Can't Find System GameObeject");
        return;
      }
      // find or add holder
      previewHolderTransform = systemObj.transform.Find("PreviewHolder");
      if (previewHolderTransform == null) {
        previewHolder = new GameObject() { name = "PreviewHolder" };
        previewHolderTransform = previewHolder.transform;
        previewHolder.transform.parent = systemObj.transform;
      } else {
        previewHolder = previewHolderTransform.gameObject;
      }

      enablePreview = previewHolder.activeInHierarchy;
      Reload();
      Preview();
    }

    [HorizontalGroup(Width = 0.25f)]
    [Button("Save")]
    private void Save() {
      levelData.SaveJson();
    }

    [HorizontalGroup(Width = 0.25f)]
    [Button("Reload")]
    private void Reload() {
      levelData = new LevelData(Application.dataPath);
    }

    [HorizontalGroup(Width = 0.25f)]
    [LabelText("Preview"), LabelWidth(100), OnValueChanged("ChangePreview")]
    public bool enablePreview = false;

    [HorizontalGroup(Width = 0.25f)]
    [LabelText("Preview"), LabelWidth(100), ShowIf("enablePreview")]
    public int index = 0;

    [ReadOnly]
    public GameObject systemObj = null;
    [ReadOnly]
    public Transform previewHolderTransform = null;
    [ReadOnly]
    public GameObject previewHolder = null;
    [InfoBox("Click to Change Data")]
    [OnValueChanged("Preview", true)]
    public LevelData levelData;

    private void ChangePreview() {
      previewHolder.SetActive(enablePreview);
    }

    private void Preview() {
      if (!enablePreview) return;

      if (levelData.LevelCount == 0) return;
      var eachLevelData = levelData[index];

      var mapSize = systemObj.GetComponent<SystemManager>().MapSize;
      // travel and add all
      for (int i = 0; i < previewHolderTransform.childCount; i++) {
        if (i < eachLevelData.Count) {
          var name = "Index_" + i.ToString();
          previewHolderTransform.GetChild(i).gameObject.name = name;
        } else {
          DestroyImmediate(previewHolderTransform.GetChild(i).gameObject);
        }
      }

      for (int i = 0; i < eachLevelData.Count; i++) {
        var name = "Index_" + i.ToString();
        Vector3 position = new Vector3(
          eachLevelData[i].x_percent * mapSize, 0.75f,
          eachLevelData[i].z_percent * mapSize);
        // try find
        var eachTransform = previewHolderTransform.Find(name);
        if (eachTransform == null) {
          var eachObj = Instantiate(
            Resources.Load("Prefabs/Cube"), position, new Quaternion(),
            previewHolderTransform);
          eachObj.name = name;
        } else {
          eachTransform.position = position;
        }
      }
    }
  }
}

#endif