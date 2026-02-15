using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000227 RID: 551
	public sealed class PointerCancelEvent : PointerEventBase<PointerCancelEvent>
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00042CAA File Offset: 0x00040EAA
		static PointerCancelEvent()
		{
			EventBase<PointerCancelEvent>.SetCreateFunction(() => new PointerCancelEvent());
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00042CC3 File Offset: 0x00040EC3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000429F6 File Offset: 0x00040BF6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00042CD4 File Offset: 0x00040ED4
		public PointerCancelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00042CE8 File Offset: 0x00040EE8
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				((IPointerEventInternal)this).compatibilityMouseEvent = MouseUpEvent.GetPooled(this);
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00042D18 File Offset: 0x00040F18
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
