using System;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000015 RID: 21
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct NavigationEvent : IEventProperties
	{
		// Token: 0x17000022 RID: 34
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002A57 File Offset: 0x00000C57
		public DiscreteTime timestamp
		{
			[CompilerGenerated]
			set
			{
				this.<timestamp>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002A68 File Offset: 0x00000C68
		public EventSource eventSource { readonly get; set; }

		// Token: 0x17000024 RID: 36
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002A71 File Offset: 0x00000C71
		public uint playerId
		{
			[CompilerGenerated]
			set
			{
				this.<playerId>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A7A File Offset: 0x00000C7A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A82 File Offset: 0x00000C82
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002A8C File Offset: 0x00000C8C
		public override string ToString()
		{
			return string.Format("Navigation {0}", this.type) + ((this.type == NavigationEvent.Type.Move) ? string.Format(" {0}", this.direction) : "") + ((this.eventSource != EventSource.Keyboard) ? string.Format(" {0}", this.eventSource) : "");
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B04 File Offset: 0x00000D04
		internal static NavigationEvent.Direction DetermineMoveDirection(Vector2 vec, float deadZone = 0.6f)
		{
			bool flag = vec.sqrMagnitude < deadZone * deadZone;
			NavigationEvent.Direction direction;
			if (flag)
			{
				direction = NavigationEvent.Direction.None;
			}
			else
			{
				bool flag2 = Mathf.Abs(vec.x) > Mathf.Abs(vec.y);
				if (flag2)
				{
					direction = ((vec.x > 0f) ? NavigationEvent.Direction.Right : NavigationEvent.Direction.Left);
				}
				else
				{
					direction = ((vec.y > 0f) ? NavigationEvent.Direction.Up : NavigationEvent.Direction.Down);
				}
			}
			return direction;
		}

		// Token: 0x04000065 RID: 101
		public NavigationEvent.Type type;

		// Token: 0x04000066 RID: 102
		public NavigationEvent.Direction direction;

		// Token: 0x04000067 RID: 103
		public bool shouldBeUsed;

		// Token: 0x02000016 RID: 22
		public enum Type
		{
			// Token: 0x0400006D RID: 109
			Move = 1,
			// Token: 0x0400006E RID: 110
			Submit,
			// Token: 0x0400006F RID: 111
			Cancel
		}

		// Token: 0x02000017 RID: 23
		public enum Direction
		{
			// Token: 0x04000071 RID: 113
			None,
			// Token: 0x04000072 RID: 114
			Left,
			// Token: 0x04000073 RID: 115
			Up,
			// Token: 0x04000074 RID: 116
			Right,
			// Token: 0x04000075 RID: 117
			Down,
			// Token: 0x04000076 RID: 118
			Next,
			// Token: 0x04000077 RID: 119
			Previous
		}
	}
}
