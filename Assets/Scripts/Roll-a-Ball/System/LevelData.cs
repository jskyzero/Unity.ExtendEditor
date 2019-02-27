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
  private string filePath = "/Config/config.json";

  // level size
  public int LevelSize { get { return levels.Count; } }
  // each level data
  public EachLevelData this [int index] {
    get {
      return levels[index];
    }
  }

  public LevelData(string path) {
    this.filePath = path + filePath;
    LoadJson();
  }

  private void InitialLevels() {
    levels = new List<EachLevelData>();
    System.Random rand = new System.Random();

    for (int i = 0; i < 1; i++) {
      EachLevelData eachLevel = new EachLevelData();
      for (int j = 0; j < 2; j++) {
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
    } else {
      Debug.Log(String.Format("{0}: Not found config file at {1}",  
        "Warnning", filePath));
      this.InitialLevels();
    }
  }

  public void SaveJson() {
    string dataAsJson = JsonUtility.ToJson(this);
    Debug.Log(dataAsJson);
    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
    File.WriteAllText(filePath, dataAsJson);
  }
}