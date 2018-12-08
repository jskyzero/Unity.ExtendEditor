using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SystemManager : MonoBehaviour {
  public GameObject pickUpHolder;
  public Text Score;
  public Text Level;

  private int pickUpNumber = 0;
  private int totalNumber = 0;
  private int levelNumber = 0;
  private const float MapSize = 4.0f;
  private const string ScoreText = "Score: ";
  private const string LevelText = "Level: ";

  private void Start() {
    InitialLevelText();
    InitialPickUps(3);
    StartAsync();
  }

  private async void StartAsync() {
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
    UpdateScoreText(pickUpNumber);
    CheckFinish();
  }

  private void InitialPickUps(int n) {
    // set total number
    pickUpNumber = 0;
    totalNumber = n;
    InitialScoreText();

    for (int i = 0; i < n; i++) {
      Vector3 randomPosition = new Vector3(
        UnityEngine.Random.Range(-MapSize, MapSize), 0.75f, 
        UnityEngine.Random.Range(-MapSize, MapSize));
      Instantiate(
        Resources.Load("Prefabs/Cube"), randomPosition, new Quaternion(),
        pickUpHolder.transform);
    }
  }

  private void CheckFinish() {
    if (pickUpNumber == totalNumber) {
      Debug.Log("Level Up");
      UpdateLevelText(levelNumber + 1);
      InitialPickUps(totalNumber + levelNumber);
    }
  }

  private void InitialScoreText() {
    UpdateScoreText(0);
  }
  
  private void UpdateScoreText(int n) {
    Score.text = ScoreText + pickUpNumber.ToString();
  }

    private void InitialLevelText() {
    UpdateLevelText(0);
  }
  
  private void UpdateLevelText(int n) {
    levelNumber = n;
    Level.text = LevelText + levelNumber.ToString();
  }
}