using TMPro;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace Jsky.Actions
{
  [ActionCategory(ActionCategory.UnityObject)]
  [Tooltip("Set Text Mesh Pro UI Text.")]
  public class SetTextMeshProUGUIText : ComponentAction<TextMeshProUGUI>
  {
    [RequiredField] [CheckForComponent(typeof(TextMeshProUGUI))] [Tooltip("Textmesh Pro UGUI component is required.")]
    public FsmOwnerDefault gameObject;

    [UIHint(UIHint.TextArea)] [TitleAttribute("Textmesh Pro UGUI Text")] [Tooltip("The text for Textmesh Pro.")]
    public FsmString textString;

    [Tooltip("Check this box to preform this action every frame.")]
    public FsmBool everyFrame;

    public override void Reset()
    {
      gameObject = null;
      textString = null;
      everyFrame = false;
    }

    public override void OnEnter()
    {
      DoMeshChange();

      if (!everyFrame.Value)
      {
        Finish();
      }
    }

    public override void OnUpdate()
    {
      if (everyFrame.Value)
      {
        DoMeshChange();
      }
    }

    void DoMeshChange()
    {
      if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
      {
        UnityEngine.Debug.LogError("No textmesh pro component was found on " + gameObject);
        return;
      }
            
      this.cachedComponent.text = textString.Value;
    }
  }
}