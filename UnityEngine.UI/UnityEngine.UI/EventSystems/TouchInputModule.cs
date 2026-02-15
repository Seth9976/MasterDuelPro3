using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C1 RID: 193
	[Obsolete("TouchInputModule is no longer required as Touch input is now handled in StandaloneInputModule.")]
	[AddComponentMenu("Event/Touch Input Module")]
	public class TouchInputModule : PointerInputModule
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0001BC4B File Offset: 0x00019E4B
		protected TouchInputModule()
		{
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001BC53 File Offset: 0x00019E53
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001BC5B File Offset: 0x00019E5B
		[Obsolete("allowActivationOnStandalone has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnStandalone
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001BC53 File Offset: 0x00019E53
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001BC5B File Offset: 0x00019E5B
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001BC64 File Offset: 0x00019E64
		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused)
			{
				if (this.m_InputPointerEvent != null && this.m_InputPointerEvent.pointerDrag != null && this.m_InputPointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(this.m_InputPointerEvent.pointerDrag, this.m_InputPointerEvent, ExecuteEvents.endDragHandler);
				}
				this.m_InputPointerEvent = null;
			}
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = base.input.mousePosition;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001BCE6 File Offset: 0x00019EE6
		public override bool IsModuleSupported()
		{
			return this.forceModuleActive || base.input.touchSupported;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001BD00 File Offset: 0x00019F00
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (this.m_ForceModuleActive)
			{
				return true;
			}
			if (this.UseFakeInput())
			{
				return base.input.GetMouseButtonDown(0) | ((this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f);
			}
			return base.input.touchCount > 0;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001BD65 File Offset: 0x00019F65
		private bool UseFakeInput()
		{
			return !base.input.touchSupported;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001BD75 File Offset: 0x00019F75
		public override void Process()
		{
			if (this.UseFakeInput())
			{
				this.FakeTouches();
				return;
			}
			this.ProcessTouchEvents();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001BD8C File Offset: 0x00019F8C
		private void FakeTouches()
		{
			PointerInputModule.MouseButtonEventData leftPressData = this.GetMousePointerEventData(0).GetButtonState(PointerEventData.InputButton.Left).eventData;
			if (leftPressData.PressedThisFrame())
			{
				leftPressData.buttonData.delta = Vector2.zero;
			}
			this.ProcessTouchPress(leftPressData.buttonData, leftPressData.PressedThisFrame(), leftPressData.ReleasedThisFrame());
			if (base.input.GetMouseButton(0))
			{
				this.ProcessMove(leftPressData.buttonData);
				this.ProcessDrag(leftPressData.buttonData);
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001BE04 File Offset: 0x0001A004
		private void ProcessTouchEvents()
		{
			for (int i = 0; i < base.input.touchCount; i++)
			{
				Touch touch = base.input.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool pressed;
					bool released;
					PointerEventData pointer = base.GetTouchPointerEventData(touch, out pressed, out released);
					this.ProcessTouchPress(pointer, pressed, released);
					if (!released)
					{
						this.ProcessMove(pointer);
						this.ProcessDrag(pointer);
					}
					else
					{
						base.RemovePointerData(pointer);
					}
				}
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001BE74 File Offset: 0x0001A074
		protected void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(currentOverGo, pointerEvent);
				if (pointerEvent.pointerEnter != currentOverGo)
				{
					base.HandlePointerExitAndEnter(pointerEvent, currentOverGo);
					pointerEvent.pointerEnter = currentOverGo;
				}
				GameObject newPressed = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler);
				if (newPressed == null)
				{
					newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				}
				float time = Time.unscaledTime;
				if (newPressed == pointerEvent.lastPress)
				{
					if (time - pointerEvent.clickTime < 0.3f)
					{
						int num = pointerEvent.clickCount + 1;
						pointerEvent.clickCount = num;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = time;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = newPressed;
				pointerEvent.rawPointerPress = currentOverGo;
				pointerEvent.clickTime = time;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
				this.m_InputPointerEvent = pointerEvent;
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				if (pointerEvent.pointerPress == pointerUpHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
				this.m_InputPointerEvent = pointerEvent;
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001B42E File Offset: 0x0001962E
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001C080 File Offset: 0x0001A280
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(this.UseFakeInput() ? "Input: Faked" : "Input: Touch");
			if (this.UseFakeInput())
			{
				PointerEventData pointerData = base.GetLastPointerEventData(-1);
				if (pointerData != null)
				{
					sb.AppendLine(pointerData.ToString());
				}
			}
			else
			{
				foreach (KeyValuePair<int, PointerEventData> pointerEventData in this.m_PointerData)
				{
					sb.AppendLine(pointerEventData.ToString());
				}
			}
			return sb.ToString();
		}

		// Token: 0x0400033A RID: 826
		private Vector2 m_LastMousePosition;

		// Token: 0x0400033B RID: 827
		private Vector2 m_MousePosition;

		// Token: 0x0400033C RID: 828
		private PointerEventData m_InputPointerEvent;

		// Token: 0x0400033D RID: 829
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnStandalone")]
		private bool m_ForceModuleActive;
	}
}
