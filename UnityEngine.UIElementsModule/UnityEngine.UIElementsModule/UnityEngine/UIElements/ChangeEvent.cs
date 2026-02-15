using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BB RID: 443
	[EventCategory(EventCategory.ChangeValue)]
	public class ChangeEvent<T> : EventBase<ChangeEvent<T>>
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x0003BFF6 File Offset: 0x0003A1F6
		static ChangeEvent()
		{
			EventBase<ChangeEvent<T>>.SetCreateFunction(() => new ChangeEvent<T>());
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0003C00F File Offset: 0x0003A20F
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0003C017 File Offset: 0x0003A217
		public T previousValue { get; protected set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0003C020 File Offset: 0x0003A220
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0003C028 File Offset: 0x0003A228
		public T newValue { get; protected set; }

		// Token: 0x06000C83 RID: 3203 RVA: 0x0003C031 File Offset: 0x0003A231
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003C044 File Offset: 0x0003A244
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.previousValue = default(T);
			this.newValue = default(T);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0003C07C File Offset: 0x0003A27C
		public static ChangeEvent<T> GetPooled(T previousValue, T newValue)
		{
			ChangeEvent<T> e = EventBase<ChangeEvent<T>>.GetPooled();
			e.previousValue = previousValue;
			e.newValue = newValue;
			return e;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003C0A5 File Offset: 0x0003A2A5
		public ChangeEvent()
		{
			this.LocalInit();
		}
	}
}
