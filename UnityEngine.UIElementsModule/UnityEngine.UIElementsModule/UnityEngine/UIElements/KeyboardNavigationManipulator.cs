using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026D RID: 621
	public class KeyboardNavigationManipulator : Manipulator
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x000487CA File Offset: 0x000469CA
		public KeyboardNavigationManipulator(Action<KeyboardNavigationOperation, EventBase> action)
		{
			this.m_Action = action;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000487DC File Offset: 0x000469DC
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00048850 File Offset: 0x00046A50
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000488C4 File Offset: 0x00046AC4
		internal void OnKeyDown(KeyDownEvent evt)
		{
			KeyboardNavigationManipulator.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.evt = evt;
			KeyboardNavigationOperation op = KeyboardNavigationManipulator.<OnKeyDown>g__GetOperation|4_0(ref CS$<>8__locals1);
			bool flag = op > KeyboardNavigationOperation.None;
			if (flag)
			{
				this.Invoke(op, CS$<>8__locals1.evt);
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000488FB File Offset: 0x00046AFB
		private void OnNavigationCancel(NavigationCancelEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Cancel, evt);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00048907 File Offset: 0x00046B07
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Submit, evt);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00048914 File Offset: 0x00046B14
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			switch (evt.direction)
			{
			case NavigationMoveEvent.Direction.Left:
				this.Invoke(KeyboardNavigationOperation.MoveLeft, evt);
				break;
			case NavigationMoveEvent.Direction.Up:
				this.Invoke(KeyboardNavigationOperation.Previous, evt);
				break;
			case NavigationMoveEvent.Direction.Right:
				this.Invoke(KeyboardNavigationOperation.MoveRight, evt);
				break;
			case NavigationMoveEvent.Direction.Down:
				this.Invoke(KeyboardNavigationOperation.Next, evt);
				break;
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00048971 File Offset: 0x00046B71
		private void Invoke(KeyboardNavigationOperation operation, EventBase evt)
		{
			Action<KeyboardNavigationOperation, EventBase> action = this.m_Action;
			if (action != null)
			{
				action(operation, evt);
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00048988 File Offset: 0x00046B88
		[CompilerGenerated]
		internal static KeyboardNavigationOperation <OnKeyDown>g__GetOperation|4_0(ref KeyboardNavigationManipulator.<>c__DisplayClass4_0 A_0)
		{
			KeyCode keyCode = A_0.evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.A)
			{
				switch (keyCode2)
				{
				case KeyCode.UpArrow:
				case KeyCode.DownArrow:
				case KeyCode.RightArrow:
				case KeyCode.LeftArrow:
					A_0.evt.StopPropagation();
					break;
				case KeyCode.Home:
					return KeyboardNavigationOperation.Begin;
				case KeyCode.End:
					return KeyboardNavigationOperation.End;
				case KeyCode.PageUp:
					return KeyboardNavigationOperation.PageUp;
				case KeyCode.PageDown:
					return KeyboardNavigationOperation.PageDown;
				}
			}
			else if (A_0.evt.actionKey)
			{
				return KeyboardNavigationOperation.SelectAll;
			}
			return KeyboardNavigationOperation.None;
		}

		// Token: 0x04000985 RID: 2437
		private readonly Action<KeyboardNavigationOperation, EventBase> m_Action;
	}
}
