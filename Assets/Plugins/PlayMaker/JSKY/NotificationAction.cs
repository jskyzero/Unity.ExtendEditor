using TMPro;
using Michsky.UI.ModernUIPack;
using HutongGames.PlayMaker;

namespace Jsky.Actions {
  [ActionCategory(ActionCategory.UnityObject)]
  [Tooltip("UI库UI提示逻辑")]
  public class NotificationAction : FsmStateAction {
    [RequiredField]
    [Tooltip("提示UI组件")]
    public NotificationManager myNotification;
    [Tooltip("提示 标题")]
    public string title;
    [Tooltip("提示 内容")]
    public string content;

    public override void Reset()
    {
      myNotification = null;
      title = null;
      content = null;
    }

    public override void OnEnter()
    {
      this.OnUpdate();
      // if (!everyFrame)
      // {
        Finish();
      // }
    }

    public override void OnUpdate() {
      notification(title, content);
    }

    private void notification(string title, string content) {
      var newNotification = UnityEngine.Object.Instantiate(myNotification);
      newNotification.transform.position = myNotification.transform.position;
      newNotification.transform.SetParent(myNotification.transform.parent);
      // newNotification.transform.parent = myNotification.transform.parent;
      newNotification.title = title;
      newNotification.description = content;
      newNotification.useStacking = true;
      newNotification.UpdateUI(); // Update UI
      newNotification.OpenNotification(); // Open notification
    }
  }
}