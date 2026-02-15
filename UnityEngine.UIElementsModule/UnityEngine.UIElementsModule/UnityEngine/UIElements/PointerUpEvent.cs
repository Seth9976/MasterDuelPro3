using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000225 RID: 549
	public sealed class PointerUpEvent : PointerEventBase<PointerUpEvent>
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x00042BD1 File Offset: 0x00040DD1
		static PointerUpEvent()
		{
			EventBase<PointerUpEvent>.SetCreateFunction(() => new PointerUpEvent());
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00042BEA File Offset: 0x00040DEA
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000429F6 File Offset: 0x00040BF6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00042BFB File Offset: 0x00040DFB
		public PointerUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00042C0C File Offset: 0x00040E0C
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				((IPointerEventInternal)this).compatibilityMouseEvent = MouseUpEvent.GetPooled(this);
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00042C3C File Offset: 0x00040E3C
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = PointerType.IsDirectManipulationDevice(base.pointerType);
			if (flag)
			{
				panel.ReleasePointer(base.pointerId);
				BaseVisualElementPanel basePanel = panel as BaseVisualElementPanel;
				if (basePanel != null)
				{
					basePanel.ClearCachedElementUnderPointer(base.pointerId, this);
				}
			}
			base.PostDispatch(panel);
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
		}
	}
}
