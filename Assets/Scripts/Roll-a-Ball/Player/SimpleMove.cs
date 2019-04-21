using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
  public float speed = 2.0f;
  // public float jumpForce = 200.0f;
  public GameObject system;
  [SerializeField]
  private Rigidbody rb;

  private void Start() {
    rb = GetComponent<Rigidbody>();
  }

  private void FixedUpdate() {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    Move(movement);

    // if (Input.GetKeyDown(KeyCode.Space)) {
    //   Jump();
    // }
  }

  // private void Jump() {
  //   Vector3 up = transform.TransformDirection(Vector3.up);
  //   rb.AddForce(up * jumpForce);
  // }

  private void Move(Vector3 movement) {
    rb.AddForce(movement * speed);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("PickUp")) {
      other.gameObject.SetActive(false);
    }

    system.SendMessage("AddScore", null);
  }
}