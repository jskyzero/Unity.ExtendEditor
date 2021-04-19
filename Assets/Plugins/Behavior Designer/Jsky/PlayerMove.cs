using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Jsky {

  [TaskDescription("Simple Move")]
  [TaskCategory("Jsky")]
  public class PlayerMove : Action {
    [Tooltip("target")]
    public SharedGameObject target;
    [Tooltip("speed")]
    public float speed;
    [Tooltip("break space")]
    public float space = 0.05f;

    public override TaskStatus OnUpdate() {
      Vector3 ZeroY = new Vector3(1, 0, 1);
      Vector3 distence = Vector3.Scale(transform.position, ZeroY) -
        Vector3.Scale(target.Value.transform.position, ZeroY);
      if (Vector3.SqrMagnitude(distence) < space) {
        return TaskStatus.Success;
      }
      var movePostion = Vector3.MoveTowards(transform.position, target.Value.transform.position, speed * Time.deltaTime);
      transform.position = new Vector3(movePostion.x, transform.position.y, movePostion.z);
      return TaskStatus.Running;
    }
  }
}