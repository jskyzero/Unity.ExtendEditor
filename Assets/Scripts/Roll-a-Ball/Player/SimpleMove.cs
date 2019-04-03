using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
  public float speed;
  public GameObject system;
  [SerializeField]
  private Rigidbody rb;

  void Start() {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    rb.AddForce(movement * speed);
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("PickUp")) {
      other.gameObject.SetActive(false);
    }

    system.SendMessage("AddScore", null);
  }
}