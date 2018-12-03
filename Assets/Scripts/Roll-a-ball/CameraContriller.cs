using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContriller : MonoBehaviour {
  public GameObject player;
  private Vector3 delta;

  void Start() {
    delta = transform.position - player.transform.position;
  }

  void LateUpdate() {
    transform.position = player.transform.position + delta;
  }
}
