using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000237 RID: 567
	[EventCategory(EventCategory.Tooltip)]
	public class TooltipEvent : EventBase<TooltipEvent>
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x00043110 File Offset: 0x00041310
		static TooltipEvent()
		{
			EventBase<TooltipEvent>.SetCreateFunction(() => new TooltipEvent());
		}

		// Token: 0x170002D8 RID: 728
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x00043129 File Offset: 0x00041329
		public string tooltip
		{
			[CompilerGenerated]
			set
			{
				this.<tooltip>k__BackingField = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00043132 File Offset: 0x00041332
		public Rect rect
		{
			[CompilerGenerated]
			set
			{
				this.<rect>k__BackingField = value;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0004313B File Offset: 0x0004133B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0004314C File Offset: 0x0004134C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.rect = default(Rect);
			this.tooltip = string.Empty;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0004317E File Offset: 0x0004137E
		public TooltipEvent()
		{
			this.LocalInit();
		}
	}
}
