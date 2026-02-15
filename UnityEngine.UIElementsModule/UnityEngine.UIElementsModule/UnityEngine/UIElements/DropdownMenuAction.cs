using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A5 RID: 421
	public class DropdownMenuAction : DropdownMenuItem
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0003B43F File Offset: 0x0003963F
		public string name { get; }

		// Token: 0x17000236 RID: 566
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0003B447 File Offset: 0x00039647
		private DropdownMenuAction.Status status
		{
			[CompilerGenerated]
			set
			{
				this.<status>k__BackingField = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0003B450 File Offset: 0x00039650
		private DropdownMenuEventInfo eventInfo
		{
			[CompilerGenerated]
			set
			{
				this.<eventInfo>k__BackingField = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0003B459 File Offset: 0x00039659
		private object userData
		{
			[CompilerGenerated]
			set
			{
				this.<userData>k__BackingField = value;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0003B464 File Offset: 0x00039664
		public static DropdownMenuAction.Status AlwaysEnabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Normal;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0003B478 File Offset: 0x00039678
		public static DropdownMenuAction.Status AlwaysDisabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0003B48B File Offset: 0x0003968B
		public DropdownMenuAction(string actionName, Action<DropdownMenuAction> actionCallback, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			this.name = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
			this.userData = userData;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003B4B3 File Offset: 0x000396B3
		public void UpdateActionStatus(DropdownMenuEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			Func<DropdownMenuAction, DropdownMenuAction.Status> func = this.actionStatusCallback;
			this.status = ((func != null) ? func(this) : DropdownMenuAction.Status.Hidden);
		}

		// Token: 0x040007C0 RID: 1984
		private readonly Action<DropdownMenuAction> actionCallback;

		// Token: 0x040007C1 RID: 1985
		private readonly Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback;

		// Token: 0x020001A6 RID: 422
		[Flags]
		public enum Status
		{
			// Token: 0x040007C3 RID: 1987
			None = 0,
			// Token: 0x040007C4 RID: 1988
			Normal = 1,
			// Token: 0x040007C5 RID: 1989
			Disabled = 2,
			// Token: 0x040007C6 RID: 1990
			Checked = 4,
			// Token: 0x040007C7 RID: 1991
			Hidden = 8
		}
	}
}
