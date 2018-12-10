using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour {
  public GameObject pickUpHolder;
  public Text Score;
  public Text Level;
  public LevelData levelData;

  private int pickUpNumber = 0;
  private int totalNumber = 0;
  private int levelNumber = 0;
  private const float MapSize = 4.0f;
  private const string ScoreText = "Score: ";
  private const string LevelText = "Level: ";

  private void Start() {
    levelData = new LevelData();
    levelData.InitialLevels();

    InitialLevelText();
    InitialScoreText();
    InitialPickUps();

    StartAsync();
  }

  private async void StartAsync() {
    // Test Linq
    await Task.Run(() => {
      Enumerable.Range(0, 100)
        .Select(i => (new System.Random(i)).Next())
        .Where(x => x > 0 && x % 2 == 0)
        .Select(x => x % 10)
        .OrderBy(x => x)
        .ToList()
        .ForEach(new Action<int>(x => {
          //   Debug.Log(x.ToString() + " ");
        }));
    });
  }

  private void AddScore() {
    pickUpNumber += 1;
    Debug.Log(pickUpNumber);
    UpdateScoreText();
    CheckLevelFinish();
  }

  private void InitialPickUps() {
    if (levelNumber < levelData.LevelSize) {
      var eachLevel = levelData[levelNumber];
      // set total number
      totalNumber = eachLevel.Count;

      for (int i = 0; i < totalNumber; i++) {
        var eachBox = eachLevel[i];
        Vector3 position = new Vector3( eachBox.x_percent * MapSize, 
          0.75f, eachBox.z_percent * MapSize);
        Instantiate(
          Resources.Load("Prefabs/Cube"), position, new Quaternion(),
          pickUpHolder.transform);
      }
    } else {
      Debug.Log("Congratulation! you pass the game");
      Debug.Break();
    }

  }

  private void CheckLevelFinish() {
    if (pickUpNumber == totalNumber) {
      Debug.Log("Level Up");
      levelNumber += 1;
      UpdateLevelText();
      InitialScoreText();
      InitialPickUps();
    }
  }

  private void InitialScoreText() {
    pickUpNumber = 0;
    UpdateScoreText();
  }

  private void UpdateScoreText() {
    Score.text = ScoreText + pickUpNumber.ToString();
  }

  private void InitialLevelText() {
    levelNumber = 0;
    UpdateLevelText();
  }

  private void UpdateLevelText() {
    Level.text = LevelText + (levelNumber + 1).ToString();
  }
}