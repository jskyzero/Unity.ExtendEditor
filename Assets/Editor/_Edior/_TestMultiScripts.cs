// reference: https://answers.unity.com/questions/538037/multi-similar-script-components-same-game-object.html

using UnityEngine;
using System;

public class _TestMultiScripts : MonoBehaviour {
  private void Start() {
    float floatValue = this.GetComponents<PropertyDrawers>()[0].myFloat;
    Debug.Log(floatValue);
    Vector3 Vec3Value =  this.GetComponent<CustomEditors>().lookAtPoint;
    Debug.Log(Vec3Value);
  }
}