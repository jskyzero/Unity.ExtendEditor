using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Jsky {
  [TaskDescription("Simple Rotate")]
  [TaskCategory("Jsky")]
  public class PlayerRotate : Action {
    [Tooltip("Rotate Speed")]
    public float speed;

    public override TaskStatus OnUpdate() {
      this.transform.Rotate(new Vector3(0, speed, 0));
      return TaskStatus.Success;
    }
  }
}