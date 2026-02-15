using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020B RID: 523
	public class NavigationMoveEvent : NavigationEventBase<NavigationMoveEvent>
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x000408CB File Offset: 0x0003EACB
		static NavigationMoveEvent()
		{
			EventBase<NavigationMoveEvent>.SetCreateFunction(() => new NavigationMoveEvent());
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000408E4 File Offset: 0x0003EAE4
		internal static NavigationMoveEvent.Direction DetermineMoveDirection(float x, float y, float deadZone = 0.6f)
		{
			bool flag = new Vector2(x, y).sqrMagnitude < deadZone * deadZone;
			NavigationMoveEvent.Direction direction;
			if (flag)
			{
				direction = NavigationMoveEvent.Direction.None;
			}
			else
			{
				bool flag2 = Mathf.Abs(x) > Mathf.Abs(y);
				if (flag2)
				{
					bool flag3 = x > 0f;
					if (flag3)
					{
						direction = NavigationMoveEvent.Direction.Right;
					}
					else
					{
						direction = NavigationMoveEvent.Direction.Left;
					}
				}
				else
				{
					bool flag4 = y > 0f;
					if (flag4)
					{
						direction = NavigationMoveEvent.Direction.Up;
					}
					else
					{
						direction = NavigationMoveEvent.Direction.Down;
					}
				}
			}
			return direction;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0004094F File Offset: 0x0003EB4F
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x00040957 File Offset: 0x0003EB57
		public NavigationMoveEvent.Direction direction { get; private set; }

		// Token: 0x17000293 RID: 659
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00040960 File Offset: 0x0003EB60
		private Vector2 move
		{
			[CompilerGenerated]
			set
			{
				this.<move>k__BackingField = value;
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0004096C File Offset: 0x0003EB6C
		public static NavigationMoveEvent GetPooled(Vector2 moveVector, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent e = NavigationEventBase<NavigationMoveEvent>.GetPooled(NavigationDeviceType.Unknown, modifiers);
			e.direction = NavigationMoveEvent.DetermineMoveDirection(moveVector.x, moveVector.y, 0.6f);
			e.move = moveVector;
			return e;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000409AC File Offset: 0x0003EBAC
		internal static NavigationMoveEvent GetPooled(Vector2 moveVector, NavigationDeviceType deviceType, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent e = NavigationEventBase<NavigationMoveEvent>.GetPooled(deviceType, modifiers);
			e.direction = NavigationMoveEvent.DetermineMoveDirection(moveVector.x, moveVector.y, 0.6f);
			e.move = moveVector;
			return e;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000409EC File Offset: 0x0003EBEC
		public static NavigationMoveEvent GetPooled(NavigationMoveEvent.Direction direction, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent e = NavigationEventBase<NavigationMoveEvent>.GetPooled(NavigationDeviceType.Unknown, modifiers);
			e.direction = direction;
			e.move = Vector2.zero;
			return e;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00040A1C File Offset: 0x0003EC1C
		internal static NavigationMoveEvent GetPooled(NavigationMoveEvent.Direction direction, NavigationDeviceType deviceType, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent e = NavigationEventBase<NavigationMoveEvent>.GetPooled(deviceType, modifiers);
			e.direction = direction;
			e.move = Vector2.zero;
			return e;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00040A4B File Offset: 0x0003EC4B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00040A5C File Offset: 0x0003EC5C
		public NavigationMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00040A6D File Offset: 0x0003EC6D
		private void LocalInit()
		{
			this.direction = NavigationMoveEvent.Direction.None;
			this.move = Vector2.zero;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00040A84 File Offset: 0x0003EC84
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.focusController.SwitchFocusOnEvent(panel.focusController.GetLeafFocusedElement(), this);
			base.PostDispatch(panel);
		}

		// Token: 0x0200020C RID: 524
		public enum Direction
		{
			// Token: 0x04000875 RID: 2165
			None,
			// Token: 0x04000876 RID: 2166
			Left,
			// Token: 0x04000877 RID: 2167
			Up,
			// Token: 0x04000878 RID: 2168
			Right,
			// Token: 0x04000879 RID: 2169
			Down,
			// Token: 0x0400087A RID: 2170
			Next,
			// Token: 0x0400087B RID: 2171
			Previous
		}
	}
}
