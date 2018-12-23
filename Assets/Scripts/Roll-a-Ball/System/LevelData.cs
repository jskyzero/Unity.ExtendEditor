using System;
using System.Collections.Generic;

public class BoxData {
  public float x_percent;
  public float z_percent;
}

public class LevelData {
  // total level datas
  private List<List<BoxData>> levels;

  // level size
  public int LevelSize { get { return levels.Count; } }
  // each level data
  public List<BoxData> this [int index] {
    get {
      return levels[index];
    }
  }

  public void InitialLevels() {
    levels = new List<List<BoxData>>();
    Random rand = new Random();

    for (int i = 0; i < 1; i++) {
      List<BoxData> eachLevel = new List<BoxData>();
      for (int j = 0; j < 2; j++) {
        BoxData newBox = new BoxData();
        newBox.x_percent = GetRandomPercent(rand);
        newBox.z_percent = GetRandomPercent(rand);
        eachLevel.Add(newBox);
      }
      levels.Add(eachLevel);
    }
  }

  private float GetRandomPercent(Random rand) {
      double randomDouble = rand.NextDouble();
      return (float) ((randomDouble * 2) - 1);
  }
}