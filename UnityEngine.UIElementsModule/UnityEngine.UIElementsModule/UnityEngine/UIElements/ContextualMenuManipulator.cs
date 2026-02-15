using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006D RID: 109
	public class ContextualMenuManipulator : PointerManipulator
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x00013478 File Offset: 0x00011678
		public ContextualMenuManipulator(Action<ContextualMenuPopulateEvent> menuBuilder)
		{
			this.m_MenuBuilder = menuBuilder;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.RightMouse
			});
			bool isOSXContextualMenuPlatform = UIElementsUtility.isOSXContextualMenuPlatform;
			if (isOSXContextualMenuPlatform)
			{
				base.activators.Add(new ManipulatorActivationFilter
				{
					button = MouseButton.LeftMouse,
					modifiers = EventModifiers.Control
				});
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000134E4 File Offset: 0x000116E4
		protected override void RegisterCallbacksOnTarget()
		{
			bool isOSXContextualMenuPlatform = UIElementsUtility.isOSXContextualMenuPlatform;
			if (isOSXContextualMenuPlatform)
			{
				base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEventOSX), TrickleDown.NoTrickleDown);
				base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEventOSX), TrickleDown.NoTrickleDown);
				base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEventOSX), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
				base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			}
			base.target.RegisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000135B0 File Offset: 0x000117B0
		protected override void UnregisterCallbacksFromTarget()
		{
			bool isOSXContextualMenuPlatform = UIElementsUtility.isOSXContextualMenuPlatform;
			if (isOSXContextualMenuPlatform)
			{
				base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEventOSX), TrickleDown.NoTrickleDown);
				base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEventOSX), TrickleDown.NoTrickleDown);
				base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEventOSX), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
				base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			}
			base.target.UnregisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001367C File Offset: 0x0001187C
		private void OnPointerUpEvent(IPointerEvent evt)
		{
			this.ProcessPointerEvent(evt);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013688 File Offset: 0x00011888
		private void OnPointerDownEventOSX(IPointerEvent evt)
		{
			BaseVisualElementPanel elementPanel = base.target.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.contextualMenuManager : null) != null;
			if (flag)
			{
				base.target.elementPanel.contextualMenuManager.displayMenuHandledOSX = false;
			}
			this.ProcessPointerEvent(evt);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000136D4 File Offset: 0x000118D4
		private void OnPointerUpEventOSX(IPointerEvent evt)
		{
			BaseVisualElementPanel elementPanel = base.target.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.contextualMenuManager : null) != null && base.target.elementPanel.contextualMenuManager.displayMenuHandledOSX;
			if (!flag)
			{
				this.ProcessPointerEvent(evt);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013724 File Offset: 0x00011924
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool isPointerUp = evt.isPointerUp;
			if (isPointerUp)
			{
				this.OnPointerUpEvent(evt);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013744 File Offset: 0x00011944
		private void OnPointerMoveEventOSX(PointerMoveEvent evt)
		{
			bool isPointerUp = evt.isPointerUp;
			if (isPointerUp)
			{
				this.OnPointerUpEventOSX(evt);
			}
			else
			{
				bool isPointerDown = evt.isPointerDown;
				if (isPointerDown)
				{
					this.OnPointerDownEventOSX(evt);
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013778 File Offset: 0x00011978
		private void ProcessPointerEvent(IPointerEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.DoDisplayMenu(evt as EventBase);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000137A0 File Offset: 0x000119A0
		private void OnKeyUpEvent(KeyUpEvent evt)
		{
			bool flag = evt.keyCode == KeyCode.Menu;
			if (flag)
			{
				this.DoDisplayMenu(evt);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000137CC File Offset: 0x000119CC
		private void DoDisplayMenu(EventBase evt)
		{
			BaseVisualElementPanel elementPanel = base.target.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.contextualMenuManager : null) != null;
			if (flag)
			{
				base.target.elementPanel.contextualMenuManager.DisplayMenu(evt, base.target);
				evt.StopPropagation();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001381E File Offset: 0x00011A1E
		private void OnContextualMenuEvent(ContextualMenuPopulateEvent evt)
		{
			Action<ContextualMenuPopulateEvent> menuBuilder = this.m_MenuBuilder;
			if (menuBuilder != null)
			{
				menuBuilder(evt);
			}
		}

		// Token: 0x04000208 RID: 520
		private Action<ContextualMenuPopulateEvent> m_MenuBuilder;
	}
}
