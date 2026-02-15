using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E0 RID: 480
	public class InputEvent : EventBase<InputEvent>
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x0003ED22 File Offset: 0x0003CF22
		static InputEvent()
		{
			EventBase<InputEvent>.SetCreateFunction(() => new InputEvent());
		}

		// Token: 0x17000263 RID: 611
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0003ED3B File Offset: 0x0003CF3B
		protected string previousData
		{
			[CompilerGenerated]
			set
			{
				this.<previousData>k__BackingField = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0003ED44 File Offset: 0x0003CF44
		protected string newData
		{
			[CompilerGenerated]
			set
			{
				this.<newData>k__BackingField = value;
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003ED4D File Offset: 0x0003CF4D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003ED5E File Offset: 0x0003CF5E
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.previousData = null;
			this.newData = null;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		public static InputEvent GetPooled(string previousData, string newData)
		{
			InputEvent e = EventBase<InputEvent>.GetPooled();
			e.previousData = previousData;
			e.newData = newData;
			return e;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003EDA5 File Offset: 0x0003CFA5
		public InputEvent()
		{
			this.LocalInit();
		}
	}
}
