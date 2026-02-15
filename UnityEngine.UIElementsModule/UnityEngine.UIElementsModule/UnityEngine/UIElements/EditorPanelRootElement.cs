using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000292 RID: 658
	internal class EditorPanelRootElement : PanelRootElement
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x0004A73C File Offset: 0x0004893C
		public EditorPanelRootElement()
		{
			base.pickingMode = PickingMode.Position;
			base.RegisterCallback<ExecuteCommandEvent>(new EventCallback<ExecuteCommandEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<ValidateCommandEvent>(new EventCallback<ValidateCommandEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseEnterWindowEvent>(new EventCallback<MouseEnterWindowEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseLeaveWindowEvent>(new EventCallback<MouseLeaveWindowEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<IMGUIEvent>(new EventCallback<IMGUIEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004A7C0 File Offset: 0x000489C0
		private void OnEventCompletedAtAnyTarget(EventBase evt)
		{
			bool propagateToIMGUI = evt.propagateToIMGUI;
			if (propagateToIMGUI)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, this);
				evt.propagateToIMGUI = false;
			}
		}
	}
}
