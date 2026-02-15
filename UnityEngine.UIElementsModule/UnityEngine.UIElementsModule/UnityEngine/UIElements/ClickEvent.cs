using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000229 RID: 553
	public sealed class ClickEvent : PointerEventBase<ClickEvent>
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00042D86 File Offset: 0x00040F86
		static ClickEvent()
		{
			EventBase<ClickEvent>.SetCreateFunction(() => new ClickEvent());
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00042D9F File Offset: 0x00040F9F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003F9C5 File Offset: 0x0003DBC5
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00042DB0 File Offset: 0x00040FB0
		public ClickEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00042DC4 File Offset: 0x00040FC4
		internal static ClickEvent GetPooled(IPointerEvent pointerEvent, int clickCount)
		{
			ClickEvent evt = PointerEventBase<ClickEvent>.GetPooled(pointerEvent);
			evt.clickCount = clickCount;
			return evt;
		}
	}
}
