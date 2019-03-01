using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class BoxData {
  public float x_percent;
  public float z_percent;
}

[Serializable]
public class EachLevelData {
  public List<BoxData> eachLevelData = new List<BoxData>();
}

[Serializable]
public class LevelData {
  // total level datas
  [SerializeField]
  private List<EachLevelData> levels;
  private bool needSave;
  private string filePath = "/Config/config.json";

  // level size
  public int LevelSize { get { return levels.Count; } }
  // each level data
  public EachLevelData this [int index] {
    get {
      return levels[index];
    }
    set {
      this.needSave = true;
      levels[index] = value;
    }
  }
  // drity
  public bool NeedSave {
    get {
      return needSave;
    }
    private set {
      needSave = value;
    }
  }

  public LevelData(string path) {
    this.filePath = path + filePath;
    LoadJson();
  }

  public void AddLevel() {
    this.levels.Add(new EachLevelData());
    this.NeedSave = true;
  }

  private void InitialLevels() {
    levels = new List<EachLevelData>();
    System.Random rand = new System.Random();

    for (int i = 0; i < 3; i++) {
      EachLevelData eachLevel = new EachLevelData();
      for (int j = 0; j < 4; j++) {
        BoxData newBox = new BoxData();
        newBox.x_percent = GetRandomPercent(rand);
        newBox.z_percent = GetRandomPercent(rand);
        eachLevel.eachLevelData.Add(newBox);
      }
      levels.Add(eachLevel);
    }
  }

  private float GetRandomPercent(System.Random rand) {
    double randomDouble = rand.NextDouble();
    return (float) ((randomDouble * 2) - 1);
  }

  public void LoadJson() {
    if (File.Exists(filePath)) {
      this.levels =
        JsonUtility.FromJson<LevelData>(File.ReadAllText(filePath)).levels;
      Debug.Log(String.Format("Load config file at {0}", filePath));
    } else {
      Debug.Log(String.Format("{0}: Not found config file at {1}",
        "Warnning", filePath));
      this.InitialLevels();
      NeedSave = true;
    }
  }

  public void SaveJson() {
    string dataAsJson = JsonUtility.ToJson(this, true);
    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
    File.WriteAllText(filePath, dataAsJson);
    NeedSave = false;

    Debug.Log(String.Format("Saved config file at {0}", filePath));
  }
}