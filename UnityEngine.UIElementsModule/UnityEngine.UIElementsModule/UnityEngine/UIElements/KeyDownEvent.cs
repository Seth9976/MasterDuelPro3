using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E4 RID: 484
	public class KeyDownEvent : KeyboardEventBase<KeyDownEvent>
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x0003EFC3 File Offset: 0x0003D1C3
		static KeyDownEvent()
		{
			EventBase<KeyDownEvent>.SetCreateFunction(() => new KeyDownEvent());
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003EFDC File Offset: 0x0003D1DC
		internal void GetEquivalentImguiEvent(Event outImguiEvent)
		{
			bool flag = base.imguiEvent != null;
			if (flag)
			{
				outImguiEvent.CopyFrom(base.imguiEvent);
			}
			else
			{
				outImguiEvent.type = EventType.KeyDown;
				outImguiEvent.modifiers = base.modifiers;
				outImguiEvent.character = base.character;
				outImguiEvent.keyCode = base.keyCode;
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003F03C File Offset: 0x0003D23C
		protected internal override void PostDispatch(IPanel panel)
		{
			base.PostDispatch(panel);
			bool flag;
			if (panel.contextType == ContextType.Editor)
			{
				Event imguiEvent = base.imguiEvent;
				flag = imguiEvent == null || imguiEvent.type != EventType.Used;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.SendEquivalentNavigationEventIfAny(panel);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003F088 File Offset: 0x0003D288
		private void SendEquivalentNavigationEventIfAny(IPanel panel)
		{
			bool flag = base.character == '\n' || base.character == '\u0003' || base.character == '\n' || base.character == ' ';
			if (flag)
			{
				using (NavigationSubmitEvent ne = NavigationEventBase<NavigationSubmitEvent>.GetPooled(NavigationDeviceType.Keyboard, base.modifiers))
				{
					ne.elementTarget = base.elementTarget;
					panel.visualTree.SendEvent(ne);
				}
			}
			else
			{
				bool flag2 = base.keyCode == KeyCode.Escape;
				if (flag2)
				{
					using (NavigationCancelEvent ne2 = NavigationEventBase<NavigationCancelEvent>.GetPooled(NavigationDeviceType.Keyboard, base.modifiers))
					{
						ne2.elementTarget = base.elementTarget;
						panel.visualTree.SendEvent(ne2);
					}
				}
				else
				{
					bool flag3 = this.ShouldSendNavigationMoveEvent();
					if (flag3)
					{
						using (NavigationMoveEvent ne3 = NavigationMoveEvent.GetPooled(base.shiftKey ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next, NavigationDeviceType.Keyboard, base.modifiers))
						{
							ne3.elementTarget = base.elementTarget;
							panel.visualTree.SendEvent(ne3);
						}
					}
					else
					{
						bool flag4 = base.keyCode == KeyCode.RightArrow || base.keyCode == KeyCode.LeftArrow || base.keyCode == KeyCode.UpArrow || base.keyCode == KeyCode.DownArrow;
						if (flag4)
						{
							Vector2 d = ((base.keyCode == KeyCode.RightArrow) ? Vector2.right : ((base.keyCode == KeyCode.LeftArrow) ? Vector2.left : ((base.keyCode == KeyCode.UpArrow) ? Vector2.up : Vector2.down)));
							using (NavigationMoveEvent ne4 = NavigationMoveEvent.GetPooled(d, NavigationDeviceType.Keyboard, base.modifiers))
							{
								ne4.elementTarget = base.elementTarget;
								panel.visualTree.SendEvent(ne4);
							}
						}
					}
				}
			}
		}
	}
}
