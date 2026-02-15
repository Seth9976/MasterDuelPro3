using System;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000BF RID: 191
	[AddComponentMenu("Event/Standalone Input Module")]
	public class StandaloneInputModule : PointerInputModule
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001B0A0 File Offset: 0x000192A0
		protected StandaloneInputModule()
		{
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x000093DE File Offset: 0x000075DE
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public StandaloneInputModule.InputMode inputMode
		{
			get
			{
				return StandaloneInputModule.InputMode.Mouse;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001B0F5 File Offset: 0x000192F5
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001B0FD File Offset: 0x000192FD
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnMobileDevice
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

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001B0F5 File Offset: 0x000192F5
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001B0FD File Offset: 0x000192FD
		[Obsolete("forceModuleActive has been deprecated. There is no need to force the module awake as StandaloneInputModule works for all platforms")]
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

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001B106 File Offset: 0x00019306
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001B10E File Offset: 0x0001930E
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001B117 File Offset: 0x00019317
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001B11F File Offset: 0x0001931F
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001B128 File Offset: 0x00019328
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001B130 File Offset: 0x00019330
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				this.m_HorizontalAxis = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001B139 File Offset: 0x00019339
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001B141 File Offset: 0x00019341
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				this.m_VerticalAxis = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001B14A File Offset: 0x0001934A
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001B152 File Offset: 0x00019352
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				this.m_SubmitButton = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001B15B File Offset: 0x0001935B
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001B163 File Offset: 0x00019363
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				this.m_CancelButton = value;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000D133 File Offset: 0x0000B333
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return true;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001B16C File Offset: 0x0001936C
		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				if (this.m_InputPointerEvent != null && this.m_InputPointerEvent.pointerDrag != null && this.m_InputPointerEvent.dragging)
				{
					this.ReleaseMouse(this.m_InputPointerEvent, this.m_InputPointerEvent.pointerCurrentRaycast.gameObject);
				}
				this.m_InputPointerEvent = null;
				return;
			}
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = base.input.mousePosition;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001B1FC File Offset: 0x000193FC
		private void ReleaseMouse(PointerEventData pointerEvent, GameObject currentOverGo)
		{
			ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
			GameObject pointerClickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
			if (pointerEvent.pointerClick == pointerClickHandler && pointerEvent.eligibleForClick)
			{
				ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerClick, pointerEvent, ExecuteEvents.pointerClickHandler);
			}
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.ExecuteHierarchy<IDropHandler>(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
			}
			pointerEvent.eligibleForClick = false;
			pointerEvent.pointerPress = null;
			pointerEvent.rawPointerPress = null;
			pointerEvent.pointerClick = null;
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
			}
			pointerEvent.dragging = false;
			pointerEvent.pointerDrag = null;
			if (currentOverGo != pointerEvent.pointerEnter)
			{
				base.HandlePointerExitAndEnter(pointerEvent, null);
				base.HandlePointerExitAndEnter(pointerEvent, currentOverGo);
			}
			this.m_InputPointerEvent = pointerEvent;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001B2E4 File Offset: 0x000194E4
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			bool shouldActivate = this.m_ForceModuleActive;
			shouldActivate |= base.input.GetButtonDown(this.m_SubmitButton);
			shouldActivate |= base.input.GetButtonDown(this.m_CancelButton);
			shouldActivate |= !Mathf.Approximately(base.input.GetAxisRaw(this.m_HorizontalAxis), 0f);
			shouldActivate |= !Mathf.Approximately(base.input.GetAxisRaw(this.m_VerticalAxis), 0f);
			shouldActivate |= (this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f;
			shouldActivate |= base.input.GetMouseButtonDown(0);
			if (base.input.touchCount > 0)
			{
				shouldActivate = true;
			}
			return shouldActivate;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001B3B0 File Offset: 0x000195B0
		public override void ActivateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			base.ActivateModule();
			this.m_MousePosition = base.input.mousePosition;
			this.m_LastMousePosition = base.input.mousePosition;
			GameObject toSelect = base.eventSystem.currentSelectedGameObject;
			if (toSelect == null)
			{
				toSelect = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(toSelect, this.GetBaseEventData());
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001B42E File Offset: 0x0001962E
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001B43C File Offset: 0x0001963C
		public override void Process()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			bool usedEvent = this.SendUpdateEventToSelectedObject();
			if (!this.ProcessTouchEvents() && base.input.mousePresent)
			{
				this.ProcessMouseEvent();
			}
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!usedEvent)
				{
					usedEvent |= this.SendMoveEventToSelectedObject();
				}
				if (!usedEvent)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001B4A4 File Offset: 0x000196A4
		private bool ProcessTouchEvents()
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
			return base.input.touchCount > 0;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001B520 File Offset: 0x00019720
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
				if (Time.unscaledTime - pointerEvent.clickTime >= 0.3f)
				{
					pointerEvent.clickCount = 0;
				}
				GameObject newPressed = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler);
				GameObject newClick = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				if (newPressed == null)
				{
					newPressed = newClick;
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
				pointerEvent.pointerClick = newClick;
				pointerEvent.clickTime = time;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject pointerClickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				if (pointerEvent.pointerClick == pointerClickHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerClick, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				pointerEvent.pointerClick = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
			this.m_InputPointerEvent = pointerEvent;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001B750 File Offset: 0x00019950
		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData data = this.GetBaseEventData();
			if (base.input.GetButtonDown(this.m_SubmitButton))
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, data, ExecuteEvents.submitHandler);
			}
			if (base.input.GetButtonDown(this.m_CancelButton))
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, data, ExecuteEvents.cancelHandler);
			}
			return data.used;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001B7D4 File Offset: 0x000199D4
		private Vector2 GetRawMoveVector()
		{
			Vector2 move = Vector2.zero;
			move.x = base.input.GetAxisRaw(this.m_HorizontalAxis);
			move.y = base.input.GetAxisRaw(this.m_VerticalAxis);
			if (base.input.GetButtonDown(this.m_HorizontalAxis))
			{
				if (move.x < 0f)
				{
					move.x = -1f;
				}
				if (move.x > 0f)
				{
					move.x = 1f;
				}
			}
			if (base.input.GetButtonDown(this.m_VerticalAxis))
			{
				if (move.y < 0f)
				{
					move.y = -1f;
				}
				if (move.y > 0f)
				{
					move.y = 1f;
				}
			}
			return move;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		protected bool SendMoveEventToSelectedObject()
		{
			float time = Time.unscaledTime;
			Vector2 movement = this.GetRawMoveVector();
			if (Mathf.Approximately(movement.x, 0f) && Mathf.Approximately(movement.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool similarDir = Vector2.Dot(movement, this.m_LastMoveVector) > 0f;
			if (similarDir && this.m_ConsecutiveMoveCount == 1)
			{
				if (time <= this.m_PrevActionTime + this.m_RepeatDelay)
				{
					return false;
				}
			}
			else if (time <= this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(movement.x, movement.y, 0.6f);
			if (axisEventData.moveDir != MoveDirection.None)
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				if (!similarDir)
				{
					this.m_ConsecutiveMoveCount = 0;
				}
				this.m_ConsecutiveMoveCount++;
				this.m_PrevActionTime = time;
				this.m_LastMoveVector = movement;
			}
			else
			{
				this.m_ConsecutiveMoveCount = 0;
			}
			return axisEventData.used;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001B9A2 File Offset: 0x00019BA2
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000093DE File Offset: 0x000075DE
		[Obsolete("This method is no longer checked, overriding it with return true does nothing!")]
		protected virtual bool ForceAutoSelect()
		{
			return false;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001B9AC File Offset: 0x00019BAC
		protected void ProcessMouseEvent(int id)
		{
			PointerInputModule.MouseState mouseData = this.GetMousePointerEventData(id);
			PointerInputModule.MouseButtonEventData leftButtonData = mouseData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			this.m_CurrentFocusedGameObject = leftButtonData.buttonData.pointerCurrentRaycast.gameObject;
			this.ProcessMousePress(leftButtonData);
			this.ProcessMove(leftButtonData.buttonData);
			this.ProcessDrag(leftButtonData.buttonData);
			this.ProcessMousePress(mouseData.GetButtonState(PointerEventData.InputButton.Right).eventData);
			this.ProcessDrag(mouseData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
			this.ProcessMousePress(mouseData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
			this.ProcessDrag(mouseData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!Mathf.Approximately(leftButtonData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(leftButtonData.buttonData.pointerCurrentRaycast.gameObject), leftButtonData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001BAA0 File Offset: 0x00019CA0
		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData data = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
			return data.used;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		protected void ProcessMousePress(PointerInputModule.MouseButtonEventData data)
		{
			PointerEventData pointerEvent = data.buttonData;
			GameObject currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(currentOverGo, pointerEvent);
				if (Time.unscaledTime - pointerEvent.clickTime >= 0.3f)
				{
					pointerEvent.clickCount = 0;
				}
				GameObject newPressed = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler);
				GameObject newClick = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				if (newPressed == null)
				{
					newPressed = newClick;
				}
				float time = Time.unscaledTime;
				if (newPressed == pointerEvent.lastPress)
				{
					if (time - pointerEvent.clickTime < 0.3f)
					{
						PointerEventData pointerEventData = pointerEvent;
						int num = pointerEventData.clickCount + 1;
						pointerEventData.clickCount = num;
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
				pointerEvent.pointerClick = newClick;
				pointerEvent.clickTime = time;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
				this.m_InputPointerEvent = pointerEvent;
			}
			if (data.ReleasedThisFrame())
			{
				this.ReleaseMouse(pointerEvent, currentOverGo);
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001BC43 File Offset: 0x00019E43
		protected GameObject GetCurrentFocusedGameObject()
		{
			return this.m_CurrentFocusedGameObject;
		}

		// Token: 0x04000328 RID: 808
		private float m_PrevActionTime;

		// Token: 0x04000329 RID: 809
		private Vector2 m_LastMoveVector;

		// Token: 0x0400032A RID: 810
		private int m_ConsecutiveMoveCount;

		// Token: 0x0400032B RID: 811
		private Vector2 m_LastMousePosition;

		// Token: 0x0400032C RID: 812
		private Vector2 m_MousePosition;

		// Token: 0x0400032D RID: 813
		private GameObject m_CurrentFocusedGameObject;

		// Token: 0x0400032E RID: 814
		private PointerEventData m_InputPointerEvent;

		// Token: 0x0400032F RID: 815
		private const float doubleClickTime = 0.3f;

		// Token: 0x04000330 RID: 816
		[SerializeField]
		private string m_HorizontalAxis = "Horizontal";

		// Token: 0x04000331 RID: 817
		[SerializeField]
		private string m_VerticalAxis = "Vertical";

		// Token: 0x04000332 RID: 818
		[SerializeField]
		private string m_SubmitButton = "Submit";

		// Token: 0x04000333 RID: 819
		[SerializeField]
		private string m_CancelButton = "Cancel";

		// Token: 0x04000334 RID: 820
		[SerializeField]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x04000335 RID: 821
		[SerializeField]
		private float m_RepeatDelay = 0.5f;

		// Token: 0x04000336 RID: 822
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		[HideInInspector]
		private bool m_ForceModuleActive;

		// Token: 0x020000C0 RID: 192
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public enum InputMode
		{
			// Token: 0x04000338 RID: 824
			Mouse,
			// Token: 0x04000339 RID: 825
			Buttons
		}
	}
}
