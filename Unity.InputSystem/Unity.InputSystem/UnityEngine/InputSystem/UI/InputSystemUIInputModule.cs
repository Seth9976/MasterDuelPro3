using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000118 RID: 280
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/UISupport.html#setting-up-ui-input")]
	public class InputSystemUIInputModule : BaseInputModule
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000424D0 File Offset: 0x000406D0
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x000424D8 File Offset: 0x000406D8
		public bool deselectOnBackgroundClick
		{
			get
			{
				return this.m_DeselectOnBackgroundClick;
			}
			set
			{
				this.m_DeselectOnBackgroundClick = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x000424E1 File Offset: 0x000406E1
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x000424E9 File Offset: 0x000406E9
		public UIPointerBehavior pointerBehavior
		{
			get
			{
				return this.m_PointerBehavior;
			}
			set
			{
				this.m_PointerBehavior = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000424F2 File Offset: 0x000406F2
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x000424FA File Offset: 0x000406FA
		public InputSystemUIInputModule.CursorLockBehavior cursorLockBehavior
		{
			get
			{
				return this.m_CursorLockBehavior;
			}
			set
			{
				this.m_CursorLockBehavior = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00042503 File Offset: 0x00040703
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0004250B File Offset: 0x0004070B
		internal GameObject localMultiPlayerRoot
		{
			get
			{
				return this.m_LocalMultiPlayerRoot;
			}
			set
			{
				this.m_LocalMultiPlayerRoot = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00042514 File Offset: 0x00040714
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0004251C File Offset: 0x0004071C
		public float scrollDeltaPerTick
		{
			get
			{
				return this.m_ScrollDeltaPerTick;
			}
			set
			{
				this.m_ScrollDeltaPerTick = value;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00042528 File Offset: 0x00040728
		public override void ActivateModule()
		{
			base.ActivateModule();
			GameObject toSelect = base.eventSystem.currentSelectedGameObject;
			if (toSelect == null)
			{
				toSelect = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(toSelect, this.GetBaseEventData());
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00042570 File Offset: 0x00040770
		public override bool IsPointerOverGameObject(int pointerOrTouchId)
		{
			if (InputSystem.isProcessingEvents)
			{
				Debug.LogWarning("Calling IsPointerOverGameObject() from within event processing (such as from InputAction callbacks) will not work as expected; it will query UI state from the last frame");
			}
			int stateIndex = -1;
			if (pointerOrTouchId < 0)
			{
				if (this.m_CurrentPointerId != -1)
				{
					stateIndex = this.m_CurrentPointerIndex;
				}
				else if (this.m_PointerStates.length > 0)
				{
					stateIndex = 0;
				}
			}
			else
			{
				stateIndex = this.GetPointerStateIndexFor(pointerOrTouchId);
			}
			return stateIndex != -1 && this.m_PointerStates[stateIndex].eventData.pointerEnter != null;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000425E4 File Offset: 0x000407E4
		public RaycastResult GetLastRaycastResult(int pointerOrTouchId)
		{
			int stateIndex = this.GetPointerStateIndexFor(pointerOrTouchId);
			if (stateIndex == -1)
			{
				return default(RaycastResult);
			}
			return this.m_PointerStates[stateIndex].eventData.pointerCurrentRaycast;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00042620 File Offset: 0x00040820
		private RaycastResult PerformRaycast(ExtendedPointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			if (eventData.pointerType == UIPointerType.Tracked && TrackedDeviceRaycaster.s_Instances.length > 0)
			{
				for (int i = 0; i < TrackedDeviceRaycaster.s_Instances.length; i++)
				{
					TrackedDeviceRaycaster trackedDeviceRaycaster = TrackedDeviceRaycaster.s_Instances[i];
					this.m_RaycastResultCache.Clear();
					trackedDeviceRaycaster.PerformRaycast(eventData, this.m_RaycastResultCache);
					if (this.m_RaycastResultCache.Count > 0)
					{
						RaycastResult raycastResult = this.m_RaycastResultCache[0];
						this.m_RaycastResultCache.Clear();
						return raycastResult;
					}
				}
				return default(RaycastResult);
			}
			base.eventSystem.RaycastAll(eventData, this.m_RaycastResultCache);
			RaycastResult raycastResult2 = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			this.m_RaycastResultCache.Clear();
			return raycastResult2;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000426E0 File Offset: 0x000408E0
		private void ProcessPointer(ref PointerModel state)
		{
			ExtendedPointerEventData eventData = state.eventData;
			UIPointerType pointerType = eventData.pointerType;
			if (pointerType == UIPointerType.MouseOrPen && Cursor.lockState == CursorLockMode.Locked)
			{
				eventData.position = ((this.m_CursorLockBehavior == InputSystemUIInputModule.CursorLockBehavior.OutsideScreen) ? new Vector2(-1f, -1f) : new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
				eventData.delta = default(Vector2);
			}
			else if (pointerType == UIPointerType.Tracked)
			{
				Vector3 position = state.worldPosition;
				Quaternion rotation = state.worldOrientation;
				if (this.m_XRTrackingOrigin != null)
				{
					position = this.m_XRTrackingOrigin.TransformPoint(position);
					rotation = this.m_XRTrackingOrigin.rotation * rotation;
				}
				eventData.trackedDeviceOrientation = rotation;
				eventData.trackedDevicePosition = position;
			}
			else
			{
				eventData.delta = state.screenPosition - eventData.position;
				eventData.position = state.screenPosition;
			}
			eventData.Reset();
			eventData.pointerCurrentRaycast = this.PerformRaycast(eventData);
			if (pointerType == UIPointerType.Tracked && eventData.pointerCurrentRaycast.isValid)
			{
				Vector2 screenPos = eventData.pointerCurrentRaycast.screenPosition;
				eventData.delta = screenPos - eventData.position;
				eventData.position = eventData.pointerCurrentRaycast.screenPosition;
			}
			eventData.button = PointerEventData.InputButton.Left;
			state.leftButton.CopyPressStateTo(eventData);
			this.ProcessPointerMovement(ref state, eventData);
			if (!state.changedThisFrame && (this.xrTrackingOrigin == null || state.pointerType != UIPointerType.Tracked))
			{
				return;
			}
			this.ProcessPointerButton(ref state.leftButton, eventData);
			this.ProcessPointerButtonDrag(ref state.leftButton, eventData);
			InputSystemUIInputModule.ProcessPointerScroll(ref state, eventData);
			eventData.button = PointerEventData.InputButton.Right;
			state.rightButton.CopyPressStateTo(eventData);
			this.ProcessPointerButton(ref state.rightButton, eventData);
			this.ProcessPointerButtonDrag(ref state.rightButton, eventData);
			eventData.button = PointerEventData.InputButton.Middle;
			state.middleButton.CopyPressStateTo(eventData);
			this.ProcessPointerButton(ref state.middleButton, eventData);
			this.ProcessPointerButtonDrag(ref state.middleButton, eventData);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000428DC File Offset: 0x00040ADC
		private bool PointerShouldIgnoreTransform(Transform t)
		{
			MultiplayerEventSystem multiplayerEventSystem = base.eventSystem as MultiplayerEventSystem;
			return multiplayerEventSystem != null && multiplayerEventSystem.playerRoot != null && !t.IsChildOf(multiplayerEventSystem.playerRoot.transform);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0004291C File Offset: 0x00040B1C
		private void ProcessPointerMovement(ref PointerModel pointer, ExtendedPointerEventData eventData)
		{
			GameObject currentPointerTarget = ((eventData.pointerType == UIPointerType.Touch && !pointer.leftButton.isPressed && !pointer.leftButton.wasReleasedThisFrame) ? null : eventData.pointerCurrentRaycast.gameObject);
			this.ProcessPointerMovement(eventData, currentPointerTarget);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00042968 File Offset: 0x00040B68
		private void ProcessPointerMovement(ExtendedPointerEventData eventData, GameObject currentPointerTarget)
		{
			bool wasMoved = eventData.IsPointerMoving();
			if (wasMoved)
			{
				for (int i = 0; i < eventData.hovered.Count; i++)
				{
					ExecuteEvents.Execute<IPointerMoveHandler>(eventData.hovered[i], eventData, ExecuteEvents.pointerMoveHandler);
				}
			}
			if (currentPointerTarget == null || eventData.pointerEnter == null)
			{
				for (int j = 0; j < eventData.hovered.Count; j++)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(eventData.hovered[j], eventData, ExecuteEvents.pointerExitHandler);
				}
				eventData.hovered.Clear();
				if (currentPointerTarget == null)
				{
					eventData.pointerEnter = null;
					return;
				}
			}
			if (eventData.pointerEnter == currentPointerTarget && currentPointerTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(eventData.pointerEnter, currentPointerTarget);
			Transform commonRoot = ((gameObject != null) ? gameObject.transform : null);
			Component component = (Component)currentPointerTarget.GetComponentInParent<IPointerExitHandler>();
			Transform pointerParent = ((component != null) ? component.transform : null);
			if (eventData.pointerEnter != null)
			{
				Transform current = eventData.pointerEnter.transform;
				while (current != null && (!this.sendPointerHoverToParent || !(current == commonRoot)) && (this.sendPointerHoverToParent || !(current == pointerParent)))
				{
					eventData.fullyExited = current != commonRoot && eventData.pointerEnter != currentPointerTarget;
					ExecuteEvents.Execute<IPointerExitHandler>(current.gameObject, eventData, ExecuteEvents.pointerExitHandler);
					eventData.hovered.Remove(current.gameObject);
					if (this.sendPointerHoverToParent)
					{
						current = current.parent;
					}
					if (current == commonRoot)
					{
						break;
					}
					if (!this.sendPointerHoverToParent)
					{
						current = current.parent;
					}
				}
			}
			Transform oldPointerEnter = (eventData.pointerEnter ? eventData.pointerEnter.transform : null);
			eventData.pointerEnter = currentPointerTarget;
			if (currentPointerTarget != null)
			{
				Transform current2 = currentPointerTarget.transform;
				while (current2 != null && !this.PointerShouldIgnoreTransform(current2))
				{
					eventData.reentered = current2 == commonRoot && current2 != oldPointerEnter;
					if (this.sendPointerHoverToParent && eventData.reentered)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerEnterHandler>(current2.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
					if (wasMoved)
					{
						ExecuteEvents.Execute<IPointerMoveHandler>(current2.gameObject, eventData, ExecuteEvents.pointerMoveHandler);
					}
					eventData.hovered.Add(current2.gameObject);
					if (!this.sendPointerHoverToParent && current2.GetComponent<IPointerEnterHandler>() != null)
					{
						break;
					}
					if (this.sendPointerHoverToParent)
					{
						current2 = current2.parent;
					}
					if (current2 == commonRoot)
					{
						break;
					}
					if (!this.sendPointerHoverToParent)
					{
						current2 = current2.parent;
					}
				}
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00042C24 File Offset: 0x00040E24
		private void ProcessPointerButton(ref PointerModel.ButtonState button, PointerEventData eventData)
		{
			GameObject currentOverGo = eventData.pointerCurrentRaycast.gameObject;
			if (currentOverGo != null && this.PointerShouldIgnoreTransform(currentOverGo.transform))
			{
				return;
			}
			if (button.wasPressedThisFrame)
			{
				button.pressTime = InputRuntime.s_Instance.unscaledGameTime;
				eventData.delta = Vector2.zero;
				eventData.dragging = false;
				eventData.pressPosition = eventData.position;
				eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
				eventData.eligibleForClick = true;
				eventData.useDragThreshold = true;
				GameObject selectHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
				if (selectHandler != base.eventSystem.currentSelectedGameObject && (selectHandler != null || this.m_DeselectOnBackgroundClick))
				{
					base.eventSystem.SetSelectedGameObject(null, eventData);
				}
				GameObject newPressed = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(currentOverGo, eventData, ExecuteEvents.pointerDownHandler);
				GameObject pointerClickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				if (newPressed == null)
				{
					newPressed = pointerClickHandler;
				}
				button.clickedOnSameGameObject = newPressed == eventData.lastPress && button.pressTime - eventData.clickTime <= 0.3f;
				if (eventData.clickCount > 0 && !button.clickedOnSameGameObject)
				{
					eventData.clickCount = 0;
					eventData.clickTime = 0f;
				}
				eventData.pointerPress = newPressed;
				eventData.pointerClick = pointerClickHandler;
				eventData.rawPointerPress = currentOverGo;
				eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);
				if (eventData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (button.wasReleasedThisFrame)
			{
				GameObject pointerClickHandler2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
				bool flag = eventData.pointerClick != null && eventData.pointerClick == pointerClickHandler2 && eventData.eligibleForClick;
				if (flag)
				{
					if (button.clickedOnSameGameObject)
					{
						int num = eventData.clickCount + 1;
						eventData.clickCount = num;
					}
					else
					{
						eventData.clickCount = 1;
					}
					eventData.clickTime = InputRuntime.s_Instance.unscaledGameTime;
				}
				ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
				if (flag)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(eventData.pointerClick, eventData, ExecuteEvents.pointerClickHandler);
				}
				else if (eventData.dragging && eventData.pointerDrag != null)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(currentOverGo, eventData, ExecuteEvents.dropHandler);
				}
				eventData.eligibleForClick = false;
				eventData.pointerPress = null;
				eventData.rawPointerPress = null;
				if (eventData.dragging && eventData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
				}
				eventData.dragging = false;
				eventData.pointerDrag = null;
				button.ignoreNextClick = false;
			}
			button.CopyPressStateFrom(eventData);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00042EAC File Offset: 0x000410AC
		private void ProcessPointerButtonDrag(ref PointerModel.ButtonState button, ExtendedPointerEventData eventData)
		{
			if (!eventData.IsPointerMoving() || (eventData.pointerType == UIPointerType.MouseOrPen && Cursor.lockState == CursorLockMode.Locked) || eventData.pointerDrag == null)
			{
				return;
			}
			if (!eventData.dragging && (!eventData.useDragThreshold || (double)(eventData.pressPosition - eventData.position).sqrMagnitude >= (double)base.eventSystem.pixelDragThreshold * (double)base.eventSystem.pixelDragThreshold * (double)((eventData.pointerType == UIPointerType.Tracked) ? this.m_TrackedDeviceDragThresholdMultiplier : 1f)))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
				eventData.dragging = true;
			}
			if (eventData.dragging)
			{
				if (eventData.pointerPress != eventData.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
					eventData.eligibleForClick = false;
					eventData.pointerPress = null;
					eventData.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
				button.CopyPressStateFrom(eventData);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00042FB4 File Offset: 0x000411B4
		private static void ProcessPointerScroll(ref PointerModel pointer, PointerEventData eventData)
		{
			Vector2 scrollDelta = pointer.scrollDelta;
			if (!Mathf.Approximately(scrollDelta.sqrMagnitude, 0f))
			{
				eventData.scrollDelta = scrollDelta;
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.pointerEnter), eventData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00042FFC File Offset: 0x000411FC
		internal void ProcessNavigation(ref NavigationModel navigationState)
		{
			bool usedSelectionChange = false;
			if (base.eventSystem.currentSelectedGameObject != null)
			{
				BaseEventData data = this.GetBaseEventData();
				ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
				usedSelectionChange = data.used;
			}
			if (!base.eventSystem.sendNavigationEvents)
			{
				return;
			}
			Vector2 movement = navigationState.move;
			if (!usedSelectionChange && (!Mathf.Approximately(movement.x, 0f) || !Mathf.Approximately(movement.y, 0f)))
			{
				float time = InputRuntime.s_Instance.unscaledGameTime;
				Vector2 moveVector = navigationState.move;
				MoveDirection moveDirection = MoveDirection.None;
				if (moveVector.sqrMagnitude > 0f)
				{
					if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
					{
						moveDirection = ((moveVector.x > 0f) ? MoveDirection.Right : MoveDirection.Left);
					}
					else
					{
						moveDirection = ((moveVector.y > 0f) ? MoveDirection.Up : MoveDirection.Down);
					}
				}
				if (moveDirection != this.m_NavigationState.lastMoveDirection)
				{
					this.m_NavigationState.consecutiveMoveCount = 0;
				}
				if (moveDirection != MoveDirection.None)
				{
					bool allow = true;
					if (this.m_NavigationState.consecutiveMoveCount != 0)
					{
						if (this.m_NavigationState.consecutiveMoveCount > 1)
						{
							allow = time > this.m_NavigationState.lastMoveTime + this.moveRepeatRate;
						}
						else
						{
							allow = time > this.m_NavigationState.lastMoveTime + this.moveRepeatDelay;
						}
					}
					if (allow)
					{
						AxisEventData eventData = this.m_NavigationState.eventData;
						if (eventData == null)
						{
							eventData = new ExtendedAxisEventData(base.eventSystem);
							this.m_NavigationState.eventData = eventData;
						}
						eventData.Reset();
						eventData.moveVector = moveVector;
						eventData.moveDir = moveDirection;
						if (this.IsMoveAllowed(eventData))
						{
							ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, eventData, ExecuteEvents.moveHandler);
							usedSelectionChange = eventData.used;
							this.m_NavigationState.consecutiveMoveCount = this.m_NavigationState.consecutiveMoveCount + 1;
							this.m_NavigationState.lastMoveTime = time;
							this.m_NavigationState.lastMoveDirection = moveDirection;
						}
					}
				}
				else
				{
					this.m_NavigationState.consecutiveMoveCount = 0;
				}
			}
			else
			{
				this.m_NavigationState.consecutiveMoveCount = 0;
			}
			if (!usedSelectionChange && base.eventSystem.currentSelectedGameObject != null)
			{
				InputActionReference submitAction2 = this.m_SubmitAction;
				InputAction submitAction = ((submitAction2 != null) ? submitAction2.action : null);
				InputActionReference cancelAction2 = this.m_CancelAction;
				InputAction cancelAction = ((cancelAction2 != null) ? cancelAction2.action : null);
				BaseEventData data2 = this.GetBaseEventData();
				if (cancelAction != null && cancelAction.WasPerformedThisFrame())
				{
					ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, data2, ExecuteEvents.cancelHandler);
				}
				if (!data2.used && submitAction != null && submitAction.WasPerformedThisFrame())
				{
					ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, data2, ExecuteEvents.submitHandler);
				}
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000432C0 File Offset: 0x000414C0
		private bool IsMoveAllowed(AxisEventData eventData)
		{
			if (this.m_LocalMultiPlayerRoot == null)
			{
				return true;
			}
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return true;
			}
			Selectable selectable = base.eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
			if (selectable == null)
			{
				return true;
			}
			Selectable navigationTarget = null;
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				navigationTarget = selectable.FindSelectableOnLeft();
				break;
			case MoveDirection.Up:
				navigationTarget = selectable.FindSelectableOnUp();
				break;
			case MoveDirection.Right:
				navigationTarget = selectable.FindSelectableOnRight();
				break;
			case MoveDirection.Down:
				navigationTarget = selectable.FindSelectableOnDown();
				break;
			}
			return navigationTarget == null || navigationTarget.transform.IsChildOf(this.m_LocalMultiPlayerRoot.transform);
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00043372 File Offset: 0x00041572
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0004337A File Offset: 0x0004157A
		public float moveRepeatDelay
		{
			get
			{
				return this.m_MoveRepeatDelay;
			}
			set
			{
				this.m_MoveRepeatDelay = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00043383 File Offset: 0x00041583
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0004338B File Offset: 0x0004158B
		public float moveRepeatRate
		{
			get
			{
				return this.m_MoveRepeatRate;
			}
			set
			{
				this.m_MoveRepeatRate = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00043394 File Offset: 0x00041594
		private bool explictlyIgnoreFocus
		{
			get
			{
				return InputSystem.settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000433A3 File Offset: 0x000415A3
		private bool shouldIgnoreFocus
		{
			get
			{
				return this.explictlyIgnoreFocus || InputRuntime.s_Instance.runInBackground;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x000433B9 File Offset: 0x000415B9
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x000433C1 File Offset: 0x000415C1
		[Obsolete("'repeatRate' has been obsoleted; use 'moveRepeatRate' instead. (UnityUpgradable) -> moveRepeatRate", false)]
		public float repeatRate
		{
			get
			{
				return this.moveRepeatRate;
			}
			set
			{
				this.moveRepeatRate = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000433CA File Offset: 0x000415CA
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x000433D2 File Offset: 0x000415D2
		[Obsolete("'repeatDelay' has been obsoleted; use 'moveRepeatDelay' instead. (UnityUpgradable) -> moveRepeatDelay", false)]
		public float repeatDelay
		{
			get
			{
				return this.moveRepeatDelay;
			}
			set
			{
				this.moveRepeatDelay = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x000433DB File Offset: 0x000415DB
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x000433E3 File Offset: 0x000415E3
		public Transform xrTrackingOrigin
		{
			get
			{
				return this.m_XRTrackingOrigin;
			}
			set
			{
				this.m_XRTrackingOrigin = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x000433EC File Offset: 0x000415EC
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x000433F4 File Offset: 0x000415F4
		public float trackedDeviceDragThresholdMultiplier
		{
			get
			{
				return this.m_TrackedDeviceDragThresholdMultiplier;
			}
			set
			{
				this.m_TrackedDeviceDragThresholdMultiplier = value;
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00043400 File Offset: 0x00041600
		private void SwapAction(ref InputActionReference property, InputActionReference newValue, bool actionsHooked, Action<InputAction.CallbackContext> actionCallback)
		{
			if (property == newValue || (property != null && newValue != null && property.action == newValue.action))
			{
				return;
			}
			if (property != null && actionCallback != null && actionsHooked)
			{
				property.action.performed -= actionCallback;
				property.action.canceled -= actionCallback;
			}
			InputActionReference inputActionReference = property;
			bool oldActionNull = ((inputActionReference != null) ? inputActionReference.action : null) == null;
			InputActionReference inputActionReference2 = property;
			bool oldActionEnabled = ((inputActionReference2 != null) ? inputActionReference2.action : null) != null && property.action.enabled;
			this.TryDisableInputAction(property, false);
			property = newValue;
			if (((newValue != null) ? newValue.action : null) != null && actionCallback != null && actionsHooked)
			{
				property.action.performed += actionCallback;
				property.action.canceled += actionCallback;
			}
			if (base.isActiveAndEnabled && ((newValue != null) ? newValue.action : null) != null && (oldActionEnabled || oldActionNull))
			{
				this.EnableInputAction(property);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00043503 File Offset: 0x00041703
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0004350B File Offset: 0x0004170B
		public InputActionReference point
		{
			get
			{
				return this.m_PointAction;
			}
			set
			{
				this.SwapAction(ref this.m_PointAction, value, this.m_ActionsHooked, this.m_OnPointDelegate);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00043526 File Offset: 0x00041726
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0004352E File Offset: 0x0004172E
		public InputActionReference scrollWheel
		{
			get
			{
				return this.m_ScrollWheelAction;
			}
			set
			{
				this.SwapAction(ref this.m_ScrollWheelAction, value, this.m_ActionsHooked, this.m_OnScrollWheelDelegate);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00043549 File Offset: 0x00041749
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00043551 File Offset: 0x00041751
		public InputActionReference leftClick
		{
			get
			{
				return this.m_LeftClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_LeftClickAction, value, this.m_ActionsHooked, this.m_OnLeftClickDelegate);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0004356C File Offset: 0x0004176C
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x00043574 File Offset: 0x00041774
		public InputActionReference middleClick
		{
			get
			{
				return this.m_MiddleClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_MiddleClickAction, value, this.m_ActionsHooked, this.m_OnMiddleClickDelegate);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0004358F File Offset: 0x0004178F
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x00043597 File Offset: 0x00041797
		public InputActionReference rightClick
		{
			get
			{
				return this.m_RightClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_RightClickAction, value, this.m_ActionsHooked, this.m_OnRightClickDelegate);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x000435B2 File Offset: 0x000417B2
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x000435BA File Offset: 0x000417BA
		public InputActionReference move
		{
			get
			{
				return this.m_MoveAction;
			}
			set
			{
				this.SwapAction(ref this.m_MoveAction, value, this.m_ActionsHooked, this.m_OnMoveDelegate);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000435D5 File Offset: 0x000417D5
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x000435DD File Offset: 0x000417DD
		public InputActionReference submit
		{
			get
			{
				return this.m_SubmitAction;
			}
			set
			{
				this.SwapAction(ref this.m_SubmitAction, value, this.m_ActionsHooked, null);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x000435F3 File Offset: 0x000417F3
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x000435FB File Offset: 0x000417FB
		public InputActionReference cancel
		{
			get
			{
				return this.m_CancelAction;
			}
			set
			{
				this.SwapAction(ref this.m_CancelAction, value, this.m_ActionsHooked, null);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00043611 File Offset: 0x00041811
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00043619 File Offset: 0x00041819
		public InputActionReference trackedDeviceOrientation
		{
			get
			{
				return this.m_TrackedDeviceOrientationAction;
			}
			set
			{
				this.SwapAction(ref this.m_TrackedDeviceOrientationAction, value, this.m_ActionsHooked, this.m_OnTrackedDeviceOrientationDelegate);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00043634 File Offset: 0x00041834
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0004363C File Offset: 0x0004183C
		public InputActionReference trackedDevicePosition
		{
			get
			{
				return this.m_TrackedDevicePositionAction;
			}
			set
			{
				this.SwapAction(ref this.m_TrackedDevicePositionAction, value, this.m_ActionsHooked, this.m_OnTrackedDevicePositionDelegate);
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00043658 File Offset: 0x00041858
		public void AssignDefaultActions()
		{
			if (InputSystemUIInputModule.defaultActions == null)
			{
				InputSystemUIInputModule.defaultActions = new DefaultInputActions();
			}
			this.actionsAsset = InputSystemUIInputModule.defaultActions.asset;
			this.cancel = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Cancel);
			this.submit = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Submit);
			this.move = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Navigate);
			this.leftClick = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Click);
			this.rightClick = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.RightClick);
			this.middleClick = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.MiddleClick);
			this.point = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Point);
			this.scrollWheel = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.ScrollWheel);
			this.trackedDeviceOrientation = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.TrackedDeviceOrientation);
			this.trackedDevicePosition = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.TrackedDevicePosition);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000437A8 File Offset: 0x000419A8
		public void UnassignActions()
		{
			DefaultInputActions defaultInputActions = InputSystemUIInputModule.defaultActions;
			if (defaultInputActions != null)
			{
				defaultInputActions.Dispose();
			}
			InputSystemUIInputModule.defaultActions = null;
			this.actionsAsset = null;
			this.cancel = null;
			this.submit = null;
			this.move = null;
			this.leftClick = null;
			this.rightClick = null;
			this.middleClick = null;
			this.point = null;
			this.scrollWheel = null;
			this.trackedDeviceOrientation = null;
			this.trackedDevicePosition = null;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00043818 File Offset: 0x00041A18
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x00043818 File Offset: 0x00041A18
		[Obsolete("'trackedDeviceSelect' has been obsoleted; use 'leftClick' instead.", true)]
		public InputActionReference trackedDeviceSelect
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0004381F File Offset: 0x00041A1F
		protected override void Awake()
		{
			base.Awake();
			this.m_NavigationState.Reset();
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00043832 File Offset: 0x00041A32
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.UnhookActions();
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00043840 File Offset: 0x00041A40
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_OnControlsChangedDelegate == null)
			{
				this.m_OnControlsChangedDelegate = new Action<object>(this.OnControlsChanged);
			}
			InputActionState.s_GlobalState.onActionControlsChanged.AddCallback(this.m_OnControlsChangedDelegate);
			if (this.HasNoActions())
			{
				this.AssignDefaultActions();
			}
			this.ResetPointers();
			this.HookActions();
			this.EnableAllActions();
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000438A2 File Offset: 0x00041AA2
		protected override void OnDisable()
		{
			this.ResetPointers();
			InputActionState.s_GlobalState.onActionControlsChanged.RemoveCallback(this.m_OnControlsChangedDelegate);
			this.DisableAllActions();
			this.UnhookActions();
			base.OnDisable();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000438D4 File Offset: 0x00041AD4
		private void ResetPointers()
		{
			int numPointers = this.m_PointerStates.length;
			for (int i = 0; i < numPointers; i++)
			{
				this.SendPointerExitEventsAndRemovePointer(0);
			}
			this.m_CurrentPointerId = -1;
			this.m_CurrentPointerIndex = -1;
			this.m_CurrentPointerType = UIPointerType.None;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00043918 File Offset: 0x00041B18
		private bool HasNoActions()
		{
			if (this.m_ActionsAsset != null)
			{
				return false;
			}
			InputActionReference pointAction = this.m_PointAction;
			if (((pointAction != null) ? pointAction.action : null) == null)
			{
				InputActionReference leftClickAction = this.m_LeftClickAction;
				if (((leftClickAction != null) ? leftClickAction.action : null) == null)
				{
					InputActionReference rightClickAction = this.m_RightClickAction;
					if (((rightClickAction != null) ? rightClickAction.action : null) == null)
					{
						InputActionReference middleClickAction = this.m_MiddleClickAction;
						if (((middleClickAction != null) ? middleClickAction.action : null) == null)
						{
							InputActionReference submitAction = this.m_SubmitAction;
							if (((submitAction != null) ? submitAction.action : null) == null)
							{
								InputActionReference cancelAction = this.m_CancelAction;
								if (((cancelAction != null) ? cancelAction.action : null) == null)
								{
									InputActionReference scrollWheelAction = this.m_ScrollWheelAction;
									if (((scrollWheelAction != null) ? scrollWheelAction.action : null) == null)
									{
										InputActionReference trackedDeviceOrientationAction = this.m_TrackedDeviceOrientationAction;
										if (((trackedDeviceOrientationAction != null) ? trackedDeviceOrientationAction.action : null) == null)
										{
											InputActionReference trackedDevicePositionAction = this.m_TrackedDevicePositionAction;
											return ((trackedDevicePositionAction != null) ? trackedDevicePositionAction.action : null) == null;
										}
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000439F4 File Offset: 0x00041BF4
		private void EnableAllActions()
		{
			this.EnableInputAction(this.m_PointAction);
			this.EnableInputAction(this.m_LeftClickAction);
			this.EnableInputAction(this.m_RightClickAction);
			this.EnableInputAction(this.m_MiddleClickAction);
			this.EnableInputAction(this.m_MoveAction);
			this.EnableInputAction(this.m_SubmitAction);
			this.EnableInputAction(this.m_CancelAction);
			this.EnableInputAction(this.m_ScrollWheelAction);
			this.EnableInputAction(this.m_TrackedDeviceOrientationAction);
			this.EnableInputAction(this.m_TrackedDevicePositionAction);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00043A7C File Offset: 0x00041C7C
		private void DisableAllActions()
		{
			this.TryDisableInputAction(this.m_PointAction, true);
			this.TryDisableInputAction(this.m_LeftClickAction, true);
			this.TryDisableInputAction(this.m_RightClickAction, true);
			this.TryDisableInputAction(this.m_MiddleClickAction, true);
			this.TryDisableInputAction(this.m_MoveAction, true);
			this.TryDisableInputAction(this.m_SubmitAction, true);
			this.TryDisableInputAction(this.m_CancelAction, true);
			this.TryDisableInputAction(this.m_ScrollWheelAction, true);
			this.TryDisableInputAction(this.m_TrackedDeviceOrientationAction, true);
			this.TryDisableInputAction(this.m_TrackedDevicePositionAction, true);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00043B0C File Offset: 0x00041D0C
		private void EnableInputAction(InputActionReference inputActionReference)
		{
			InputAction action = ((inputActionReference != null) ? inputActionReference.action : null);
			if (action == null)
			{
				return;
			}
			InputSystemUIInputModule.InputActionReferenceState referenceState;
			if (InputSystemUIInputModule.s_InputActionReferenceCounts.TryGetValue(action, out referenceState))
			{
				referenceState.refCount++;
				InputSystemUIInputModule.s_InputActionReferenceCounts[action] = referenceState;
			}
			else
			{
				referenceState = new InputSystemUIInputModule.InputActionReferenceState
				{
					refCount = 1,
					enabledByInputModule = !action.enabled
				};
				InputSystemUIInputModule.s_InputActionReferenceCounts.Add(action, referenceState);
			}
			action.Enable();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00043B88 File Offset: 0x00041D88
		private void TryDisableInputAction(InputActionReference inputActionReference, bool isComponentDisabling = false)
		{
			InputAction action = ((inputActionReference != null) ? inputActionReference.action : null);
			if (action == null)
			{
				return;
			}
			if (!base.isActiveAndEnabled && !isComponentDisabling)
			{
				return;
			}
			InputSystemUIInputModule.InputActionReferenceState referenceState;
			if (!InputSystemUIInputModule.s_InputActionReferenceCounts.TryGetValue(action, out referenceState))
			{
				return;
			}
			if (referenceState.refCount - 1 == 0 && referenceState.enabledByInputModule)
			{
				action.Disable();
				InputSystemUIInputModule.s_InputActionReferenceCounts.Remove(action);
				return;
			}
			referenceState.refCount--;
			InputSystemUIInputModule.s_InputActionReferenceCounts[action] = referenceState;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00043C00 File Offset: 0x00041E00
		private int GetPointerStateIndexFor(int pointerOrTouchId)
		{
			if (pointerOrTouchId == this.m_CurrentPointerId)
			{
				return this.m_CurrentPointerIndex;
			}
			for (int i = 0; i < this.m_PointerIds.length; i++)
			{
				if (this.m_PointerIds[i] == pointerOrTouchId)
				{
					return i;
				}
			}
			for (int j = 0; j < this.m_PointerStates.length; j++)
			{
				ExtendedPointerEventData eventData = this.m_PointerStates[j].eventData;
				if (eventData.touchId == pointerOrTouchId || (eventData.touchId != 0 && eventData.device.deviceId == pointerOrTouchId))
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00043C8E File Offset: 0x00041E8E
		private ref PointerModel GetPointerStateForIndex(int index)
		{
			if (index == 0)
			{
				return ref this.m_PointerStates.firstValue;
			}
			return ref this.m_PointerStates.additionalValues[index - 1];
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00043CB4 File Offset: 0x00041EB4
		private int GetDisplayIndexFor(InputControl control)
		{
			int displayIndex = 0;
			Pointer pointerCast = control.device as Pointer;
			if (pointerCast != null)
			{
				displayIndex = pointerCast.displayIndex.ReadValue();
			}
			return displayIndex;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00043CE0 File Offset: 0x00041EE0
		private int GetPointerStateIndexFor(ref InputAction.CallbackContext context)
		{
			if (this.CheckForRemovedDevice(ref context))
			{
				return -1;
			}
			InputActionPhase phase = context.phase;
			return this.GetPointerStateIndexFor(context.control, phase != InputActionPhase.Canceled);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00043D14 File Offset: 0x00041F14
		private unsafe int GetPointerStateIndexFor(InputControl control, bool createIfNotExists = true)
		{
			InputDevice device = control.device;
			InputControl controlParent = control.parent;
			int touchControlIndex = this.m_PointerTouchControls.IndexOfReference(controlParent);
			if (touchControlIndex != -1)
			{
				this.m_CurrentPointerId = this.m_PointerIds[touchControlIndex];
				this.m_CurrentPointerIndex = touchControlIndex;
				this.m_CurrentPointerType = UIPointerType.Touch;
				return touchControlIndex;
			}
			int pointerId = device.deviceId;
			int touchId = 0;
			Vector2 touchPosition = Vector2.zero;
			TouchControl touchControl = controlParent as TouchControl;
			if (touchControl != null)
			{
				touchId = *touchControl.touchId.value;
				touchPosition = *touchControl.position.value;
			}
			else
			{
				Touchscreen touchscreen = controlParent as Touchscreen;
				if (touchscreen != null)
				{
					touchId = *touchscreen.primaryTouch.touchId.value;
					touchPosition = *touchscreen.primaryTouch.position.value;
				}
			}
			int displayIndex = this.GetDisplayIndexFor(control);
			if (touchId != 0)
			{
				pointerId = ExtendedPointerEventData.MakePointerIdForTouch(pointerId, touchId);
			}
			if (this.m_CurrentPointerId == pointerId)
			{
				return this.m_CurrentPointerIndex;
			}
			if (touchId == 0)
			{
				for (int i = 0; i < this.m_PointerIds.length; i++)
				{
					if (this.m_PointerIds[i] == pointerId)
					{
						this.m_CurrentPointerId = pointerId;
						this.m_CurrentPointerIndex = i;
						this.m_CurrentPointerType = this.m_PointerStates[i].pointerType;
						return i;
					}
				}
			}
			if (!createIfNotExists)
			{
				return -1;
			}
			UIPointerType pointerType = UIPointerType.None;
			if (touchId != 0)
			{
				pointerType = UIPointerType.Touch;
			}
			else if (InputSystemUIInputModule.HaveControlForDevice(device, this.point))
			{
				pointerType = UIPointerType.MouseOrPen;
			}
			else if (InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDevicePosition))
			{
				pointerType = UIPointerType.Tracked;
			}
			if ((this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer && pointerType != UIPointerType.None) || (this.m_PointerBehavior == UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && pointerType == UIPointerType.MouseOrPen))
			{
				if (this.m_CurrentPointerIndex == -1)
				{
					this.m_CurrentPointerIndex = this.AllocatePointer(pointerId, displayIndex, touchId, pointerType, control, device, (touchId != 0) ? controlParent : null);
				}
				else
				{
					ExtendedPointerEventData eventData = this.GetPointerStateForIndex(this.m_CurrentPointerIndex).eventData;
					eventData.control = control;
					eventData.device = device;
					eventData.pointerType = pointerType;
					eventData.pointerId = pointerId;
					eventData.touchId = touchId;
					eventData.displayIndex = displayIndex;
					eventData.trackedDeviceOrientation = default(Quaternion);
					eventData.trackedDevicePosition = default(Vector3);
				}
				if (pointerType == UIPointerType.Touch)
				{
					this.GetPointerStateForIndex(this.m_CurrentPointerIndex).screenPosition = touchPosition;
				}
				this.m_CurrentPointerId = pointerId;
				this.m_CurrentPointerType = pointerType;
				return this.m_CurrentPointerIndex;
			}
			int index;
			if (pointerType != UIPointerType.None)
			{
				index = this.AllocatePointer(pointerId, displayIndex, touchId, pointerType, control, device, (touchId != 0) ? controlParent : null);
			}
			else
			{
				if (this.m_CurrentPointerId != -1)
				{
					return this.m_CurrentPointerIndex;
				}
				InputActionReference point = this.point;
				ReadOnlyArray<InputControl>? readOnlyArray;
				if (point == null)
				{
					readOnlyArray = null;
				}
				else
				{
					InputAction action = point.action;
					readOnlyArray = ((action != null) ? new ReadOnlyArray<InputControl>?(action.controls) : null);
				}
				ReadOnlyArray<InputControl>? pointControls = readOnlyArray;
				InputDevice pointerDevice = ((pointControls != null && pointControls.Value.Count > 0) ? pointControls.Value[0].device : null);
				if (pointerDevice != null && !(pointerDevice is Touchscreen))
				{
					index = this.AllocatePointer(pointerDevice.deviceId, displayIndex, 0, UIPointerType.MouseOrPen, pointControls.Value[0], pointerDevice, null);
				}
				else
				{
					InputActionReference trackedDevicePosition = this.trackedDevicePosition;
					ReadOnlyArray<InputControl>? readOnlyArray2;
					if (trackedDevicePosition == null)
					{
						readOnlyArray2 = null;
					}
					else
					{
						InputAction action2 = trackedDevicePosition.action;
						readOnlyArray2 = ((action2 != null) ? new ReadOnlyArray<InputControl>?(action2.controls) : null);
					}
					ReadOnlyArray<InputControl>? positionControls = readOnlyArray2;
					InputDevice trackedDevice = ((positionControls != null && positionControls.Value.Count > 0) ? positionControls.Value[0].device : null);
					if (trackedDevice != null)
					{
						index = this.AllocatePointer(trackedDevice.deviceId, displayIndex, 0, UIPointerType.Tracked, positionControls.Value[0], trackedDevice, null);
					}
					else
					{
						index = this.AllocatePointer(pointerId, displayIndex, 0, UIPointerType.None, control, device, null);
					}
				}
			}
			if (pointerType == UIPointerType.Touch)
			{
				this.GetPointerStateForIndex(index).screenPosition = touchPosition;
			}
			this.m_CurrentPointerId = pointerId;
			this.m_CurrentPointerIndex = index;
			this.m_CurrentPointerType = pointerType;
			return index;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0004411C File Offset: 0x0004231C
		private int AllocatePointer(int pointerId, int displayIndex, int touchId, UIPointerType pointerType, InputControl control, InputDevice device, InputControl touchControl = null)
		{
			ExtendedPointerEventData eventData = null;
			if (this.m_PointerStates.Capacity > this.m_PointerStates.length)
			{
				if (this.m_PointerStates.length == 0)
				{
					eventData = this.m_PointerStates.firstValue.eventData;
				}
				else
				{
					eventData = this.m_PointerStates.additionalValues[this.m_PointerStates.length - 1].eventData;
				}
			}
			if (eventData == null)
			{
				eventData = new ExtendedPointerEventData(base.eventSystem);
			}
			eventData.pointerId = pointerId;
			eventData.displayIndex = displayIndex;
			eventData.touchId = touchId;
			eventData.pointerType = pointerType;
			eventData.control = control;
			eventData.device = device;
			this.m_PointerIds.AppendWithCapacity(pointerId, 10);
			this.m_PointerTouchControls.AppendWithCapacity(touchControl, 10);
			return this.m_PointerStates.AppendWithCapacity(new PointerModel(eventData), 10);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000441F4 File Offset: 0x000423F4
		private void SendPointerExitEventsAndRemovePointer(int index)
		{
			ExtendedPointerEventData eventData = this.m_PointerStates[index].eventData;
			if (eventData.pointerEnter != null)
			{
				this.ProcessPointerMovement(eventData, null);
			}
			this.RemovePointerAtIndex(index);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00044230 File Offset: 0x00042430
		private void RemovePointerAtIndex(int index)
		{
			ref PointerModel state = ref this.GetPointerStateForIndex(index);
			if (state.pointerType == UIPointerType.Touch && (state.leftButton.isPressed || state.leftButton.wasReleasedThisFrame))
			{
				return;
			}
			ExtendedPointerEventData eventData = this.m_PointerStates[index].eventData;
			if (index == this.m_CurrentPointerIndex)
			{
				this.m_CurrentPointerId = -1;
				this.m_CurrentPointerIndex = -1;
				this.m_CurrentPointerType = UIPointerType.None;
			}
			else if (this.m_CurrentPointerIndex == this.m_PointerIds.length - 1)
			{
				this.m_CurrentPointerIndex = index;
			}
			this.m_PointerIds.RemoveAtByMovingTailWithCapacity(index);
			this.m_PointerTouchControls.RemoveAtByMovingTailWithCapacity(index);
			this.m_PointerStates.RemoveAtByMovingTailWithCapacity(index);
			eventData.hovered.Clear();
			eventData.device = null;
			eventData.pointerCurrentRaycast = default(RaycastResult);
			eventData.pointerPressRaycast = default(RaycastResult);
			eventData.pointerPress = null;
			eventData.pointerPress = null;
			eventData.pointerDrag = null;
			eventData.pointerEnter = null;
			eventData.rawPointerPress = null;
			if (this.m_PointerStates.length == 0)
			{
				this.m_PointerStates.firstValue.eventData = eventData;
				return;
			}
			this.m_PointerStates.additionalValues[this.m_PointerStates.length - 1].eventData = eventData;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00044370 File Offset: 0x00042570
		private void PurgeStalePointers()
		{
			for (int i = 0; i < this.m_PointerStates.length; i++)
			{
				InputDevice device = this.GetPointerStateForIndex(i).eventData.device;
				if (!device.added || (!InputSystemUIInputModule.HaveControlForDevice(device, this.point) && !InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDevicePosition) && !InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDeviceOrientation)))
				{
					this.SendPointerExitEventsAndRemovePointer(i);
					i--;
				}
			}
			this.m_NeedToPurgeStalePointers = false;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000443EC File Offset: 0x000425EC
		private static bool HaveControlForDevice(InputDevice device, InputActionReference actionReference)
		{
			InputAction action = ((actionReference != null) ? actionReference.action : null);
			if (action == null)
			{
				return false;
			}
			ReadOnlyArray<InputControl> controls = action.controls;
			for (int i = 0; i < controls.Count; i++)
			{
				if (controls[i].device == device)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00044438 File Offset: 0x00042638
		private void OnPointCallback(InputAction.CallbackContext context)
		{
			if (this.CheckForRemovedDevice(ref context) || context.canceled)
			{
				return;
			}
			int index = this.GetPointerStateIndexFor(context.control, true);
			if (index == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(index);
			pointerStateForIndex.screenPosition = context.ReadValue<Vector2>();
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00044498 File Offset: 0x00042698
		private bool IgnoreNextClick(ref InputAction.CallbackContext context, bool wasPressed)
		{
			return !this.explictlyIgnoreFocus && (context.canceled && !InputRuntime.s_Instance.isPlayerFocused && !context.control.device.canRunInBackground && wasPressed);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000444D0 File Offset: 0x000426D0
		private void OnLeftClickCallback(InputAction.CallbackContext context)
		{
			int index = this.GetPointerStateIndexFor(ref context);
			if (index == -1)
			{
				return;
			}
			ref PointerModel state = ref this.GetPointerStateForIndex(index);
			bool wasPressed = state.leftButton.isPressed;
			state.leftButton.isPressed = context.ReadValueAsButton();
			state.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, wasPressed))
			{
				state.leftButton.ignoreNextClick = true;
			}
			state.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00044548 File Offset: 0x00042748
		private void OnRightClickCallback(InputAction.CallbackContext context)
		{
			int index = this.GetPointerStateIndexFor(ref context);
			if (index == -1)
			{
				return;
			}
			ref PointerModel state = ref this.GetPointerStateForIndex(index);
			bool wasPressed = state.rightButton.isPressed;
			state.rightButton.isPressed = context.ReadValueAsButton();
			state.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, wasPressed))
			{
				state.rightButton.ignoreNextClick = true;
			}
			state.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000445C0 File Offset: 0x000427C0
		private void OnMiddleClickCallback(InputAction.CallbackContext context)
		{
			int index = this.GetPointerStateIndexFor(ref context);
			if (index == -1)
			{
				return;
			}
			ref PointerModel state = ref this.GetPointerStateForIndex(index);
			bool wasPressed = state.middleButton.isPressed;
			state.middleButton.isPressed = context.ReadValueAsButton();
			state.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, wasPressed))
			{
				state.middleButton.ignoreNextClick = true;
			}
			state.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00044637 File Offset: 0x00042837
		private bool CheckForRemovedDevice(ref InputAction.CallbackContext context)
		{
			if (context.canceled && !context.control.device.added)
			{
				this.m_NeedToPurgeStalePointers = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00044660 File Offset: 0x00042860
		private void OnScrollCallback(InputAction.CallbackContext context)
		{
			int index = this.GetPointerStateIndexFor(ref context);
			if (index == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(index);
			Vector2 scrollDelta = context.ReadValue<Vector2>();
			pointerStateForIndex.scrollDelta = scrollDelta / InputSystem.scrollWheelDeltaPerTick * this.scrollDeltaPerTick;
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000446BD File Offset: 0x000428BD
		private void OnMoveCallback(InputAction.CallbackContext context)
		{
			this.m_NavigationState.move = context.ReadValue<Vector2>();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000446D4 File Offset: 0x000428D4
		private void OnTrackedDeviceOrientationCallback(InputAction.CallbackContext context)
		{
			int index = this.GetPointerStateIndexFor(ref context);
			if (index == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(index);
			pointerStateForIndex.worldOrientation = context.ReadValue<Quaternion>();
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0004471C File Offset: 0x0004291C
		private void OnTrackedDevicePositionCallback(InputAction.CallbackContext context)
		{
			int index = this.GetPointerStateIndexFor(ref context);
			if (index == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(index);
			pointerStateForIndex.worldPosition = context.ReadValue<Vector3>();
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00044762 File Offset: 0x00042962
		private void OnControlsChanged(object obj)
		{
			this.m_NeedToPurgeStalePointers = true;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0004476C File Offset: 0x0004296C
		private void FilterPointerStatesByType()
		{
			UIPointerType pointerTypeToProcess = UIPointerType.None;
			for (int i = 0; i < this.m_PointerStates.length; i++)
			{
				ref PointerModel state = ref this.GetPointerStateForIndex(i);
				state.eventData.ReadDeviceState();
				state.CopyTouchOrPenStateFrom(state.eventData);
				if (state.changedThisFrame && pointerTypeToProcess == UIPointerType.None)
				{
					pointerTypeToProcess = state.pointerType;
				}
			}
			if (this.m_PointerBehavior == UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && pointerTypeToProcess != UIPointerType.None)
			{
				if (pointerTypeToProcess == UIPointerType.MouseOrPen)
				{
					for (int j = 0; j < this.m_PointerStates.length; j++)
					{
						ref PointerModel state2 = ref this.GetPointerStateForIndex(j);
						if (this.m_PointerStates[j].pointerType == UIPointerType.Touch)
						{
							state2.leftButton.isPressed = false;
						}
						if ((this.m_PointerStates[j].pointerType != UIPointerType.MouseOrPen && this.m_PointerStates[j].pointerType != UIPointerType.Touch) || (this.m_PointerStates[j].pointerType == UIPointerType.Touch && !state2.leftButton.isPressed && !state2.leftButton.wasReleasedThisFrame))
						{
							this.SendPointerExitEventsAndRemovePointer(j);
							j--;
						}
					}
					return;
				}
				for (int k = 0; k < this.m_PointerStates.length; k++)
				{
					if (this.m_PointerStates[k].pointerType == UIPointerType.MouseOrPen)
					{
						this.SendPointerExitEventsAndRemovePointer(k);
						k--;
					}
				}
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000448D8 File Offset: 0x00042AD8
		public override void Process()
		{
			if (this.m_NeedToPurgeStalePointers)
			{
				this.PurgeStalePointers();
			}
			if (!base.eventSystem.isFocused && !this.shouldIgnoreFocus)
			{
				for (int i = 0; i < this.m_PointerStates.length; i++)
				{
					this.m_PointerStates[i].OnFrameFinished();
				}
				return;
			}
			this.ProcessNavigation(ref this.m_NavigationState);
			this.FilterPointerStatesByType();
			for (int j = 0; j < this.m_PointerStates.length; j++)
			{
				ref PointerModel state = ref this.GetPointerStateForIndex(j);
				this.ProcessPointer(ref state);
				if (state.pointerType == UIPointerType.Touch && !state.leftButton.isPressed && !state.leftButton.wasReleasedThisFrame)
				{
					this.RemovePointerAtIndex(j);
					j--;
				}
				else
				{
					state.OnFrameFinished();
				}
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000449A0 File Offset: 0x00042BA0
		public override int ConvertUIToolkitPointerId(PointerEventData sourcePointerData)
		{
			if (this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer)
			{
				return PointerId.mousePointerId;
			}
			ExtendedPointerEventData ep = sourcePointerData as ExtendedPointerEventData;
			if (ep == null)
			{
				return base.ConvertUIToolkitPointerId(sourcePointerData);
			}
			return ep.uiToolkitPointerId;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000449D4 File Offset: 0x00042BD4
		public override Vector2 ConvertPointerEventScrollDeltaToTicks(Vector2 scrollDelta)
		{
			if (Mathf.Abs(this.scrollDeltaPerTick) < 1E-05f)
			{
				return Vector2.zero;
			}
			return scrollDelta / this.scrollDeltaPerTick;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000449FC File Offset: 0x00042BFC
		private void HookActions()
		{
			if (this.m_ActionsHooked)
			{
				return;
			}
			if (this.m_OnPointDelegate == null)
			{
				this.m_OnPointDelegate = new Action<InputAction.CallbackContext>(this.OnPointCallback);
			}
			if (this.m_OnLeftClickDelegate == null)
			{
				this.m_OnLeftClickDelegate = new Action<InputAction.CallbackContext>(this.OnLeftClickCallback);
			}
			if (this.m_OnRightClickDelegate == null)
			{
				this.m_OnRightClickDelegate = new Action<InputAction.CallbackContext>(this.OnRightClickCallback);
			}
			if (this.m_OnMiddleClickDelegate == null)
			{
				this.m_OnMiddleClickDelegate = new Action<InputAction.CallbackContext>(this.OnMiddleClickCallback);
			}
			if (this.m_OnScrollWheelDelegate == null)
			{
				this.m_OnScrollWheelDelegate = new Action<InputAction.CallbackContext>(this.OnScrollCallback);
			}
			if (this.m_OnMoveDelegate == null)
			{
				this.m_OnMoveDelegate = new Action<InputAction.CallbackContext>(this.OnMoveCallback);
			}
			if (this.m_OnTrackedDeviceOrientationDelegate == null)
			{
				this.m_OnTrackedDeviceOrientationDelegate = new Action<InputAction.CallbackContext>(this.OnTrackedDeviceOrientationCallback);
			}
			if (this.m_OnTrackedDevicePositionDelegate == null)
			{
				this.m_OnTrackedDevicePositionDelegate = new Action<InputAction.CallbackContext>(this.OnTrackedDevicePositionCallback);
			}
			this.SetActionCallbacks(true);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00044AE9 File Offset: 0x00042CE9
		private void UnhookActions()
		{
			if (!this.m_ActionsHooked)
			{
				return;
			}
			this.SetActionCallbacks(false);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00044AFC File Offset: 0x00042CFC
		private void SetActionCallbacks(bool install)
		{
			this.m_ActionsHooked = install;
			InputSystemUIInputModule.SetActionCallback(this.m_PointAction, this.m_OnPointDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_MoveAction, this.m_OnMoveDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_LeftClickAction, this.m_OnLeftClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_RightClickAction, this.m_OnRightClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_MiddleClickAction, this.m_OnMiddleClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_ScrollWheelAction, this.m_OnScrollWheelDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_TrackedDeviceOrientationAction, this.m_OnTrackedDeviceOrientationDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_TrackedDevicePositionAction, this.m_OnTrackedDevicePositionDelegate, install);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00044BA0 File Offset: 0x00042DA0
		private static void SetActionCallback(InputActionReference actionReference, Action<InputAction.CallbackContext> callback, bool install)
		{
			if (!install && callback == null)
			{
				return;
			}
			if (actionReference == null)
			{
				return;
			}
			InputAction action = actionReference.action;
			if (action == null)
			{
				return;
			}
			if (install)
			{
				action.performed += callback;
				action.canceled += callback;
				return;
			}
			action.performed -= callback;
			action.canceled -= callback;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00044BEC File Offset: 0x00042DEC
		private InputActionReference UpdateReferenceForNewAsset(InputActionReference actionReference)
		{
			InputAction oldAction = ((actionReference != null) ? actionReference.action : null);
			if (oldAction == null)
			{
				return null;
			}
			InputActionMap oldActionMap = oldAction.actionMap;
			InputActionAsset actionsAsset = this.m_ActionsAsset;
			InputActionMap newActionMap = ((actionsAsset != null) ? actionsAsset.FindActionMap(oldActionMap.name, false) : null);
			if (newActionMap == null)
			{
				return null;
			}
			InputAction newAction = newActionMap.FindAction(oldAction.name, false);
			if (newAction == null)
			{
				return null;
			}
			return InputActionReference.Create(newAction);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00044C4A File Offset: 0x00042E4A
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00044C54 File Offset: 0x00042E54
		public InputActionAsset actionsAsset
		{
			get
			{
				return this.m_ActionsAsset;
			}
			set
			{
				if (value != this.m_ActionsAsset)
				{
					this.UnhookActions();
					this.m_ActionsAsset = value;
					this.point = this.UpdateReferenceForNewAsset(this.point);
					this.move = this.UpdateReferenceForNewAsset(this.move);
					this.leftClick = this.UpdateReferenceForNewAsset(this.leftClick);
					this.rightClick = this.UpdateReferenceForNewAsset(this.rightClick);
					this.middleClick = this.UpdateReferenceForNewAsset(this.middleClick);
					this.scrollWheel = this.UpdateReferenceForNewAsset(this.scrollWheel);
					this.submit = this.UpdateReferenceForNewAsset(this.submit);
					this.cancel = this.UpdateReferenceForNewAsset(this.cancel);
					this.trackedDeviceOrientation = this.UpdateReferenceForNewAsset(this.trackedDeviceOrientation);
					this.trackedDevicePosition = this.UpdateReferenceForNewAsset(this.trackedDevicePosition);
					this.HookActions();
				}
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00044D39 File Offset: 0x00042F39
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00044D41 File Offset: 0x00042F41
		internal new bool sendPointerHoverToParent
		{
			get
			{
				return base.sendPointerHoverToParent;
			}
			set
			{
				base.sendPointerHoverToParent = value;
			}
		}

		// Token: 0x04000653 RID: 1619
		private const float kClickSpeed = 0.3f;

		// Token: 0x04000654 RID: 1620
		[FormerlySerializedAs("m_RepeatDelay")]
		[Tooltip("The Initial delay (in seconds) between an initial move action and a repeated move action.")]
		[SerializeField]
		private float m_MoveRepeatDelay = 0.5f;

		// Token: 0x04000655 RID: 1621
		[FormerlySerializedAs("m_RepeatRate")]
		[Tooltip("The speed (in seconds) that the move action repeats itself once repeating (max 1 per frame).")]
		[SerializeField]
		private float m_MoveRepeatRate = 0.1f;

		// Token: 0x04000656 RID: 1622
		[Tooltip("Scales the Eventsystem.DragThreshold, for tracked devices, to make selection easier.")]
		private float m_TrackedDeviceDragThresholdMultiplier = 2f;

		// Token: 0x04000657 RID: 1623
		[Tooltip("Transform representing the real world origin for tracking devices. When using the XR Interaction Toolkit, this should be pointing to the XR Rig's Transform.")]
		[SerializeField]
		private Transform m_XRTrackingOrigin;

		// Token: 0x04000658 RID: 1624
		private static DefaultInputActions defaultActions;

		// Token: 0x04000659 RID: 1625
		private const float kSmallestScrollDeltaPerTick = 1E-05f;

		// Token: 0x0400065A RID: 1626
		[SerializeField]
		[HideInInspector]
		private InputActionAsset m_ActionsAsset;

		// Token: 0x0400065B RID: 1627
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_PointAction;

		// Token: 0x0400065C RID: 1628
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_MoveAction;

		// Token: 0x0400065D RID: 1629
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_SubmitAction;

		// Token: 0x0400065E RID: 1630
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_CancelAction;

		// Token: 0x0400065F RID: 1631
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_LeftClickAction;

		// Token: 0x04000660 RID: 1632
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_MiddleClickAction;

		// Token: 0x04000661 RID: 1633
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_RightClickAction;

		// Token: 0x04000662 RID: 1634
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_ScrollWheelAction;

		// Token: 0x04000663 RID: 1635
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_TrackedDevicePositionAction;

		// Token: 0x04000664 RID: 1636
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_TrackedDeviceOrientationAction;

		// Token: 0x04000665 RID: 1637
		[SerializeField]
		private bool m_DeselectOnBackgroundClick = true;

		// Token: 0x04000666 RID: 1638
		[SerializeField]
		private UIPointerBehavior m_PointerBehavior;

		// Token: 0x04000667 RID: 1639
		[SerializeField]
		[HideInInspector]
		internal InputSystemUIInputModule.CursorLockBehavior m_CursorLockBehavior;

		// Token: 0x04000668 RID: 1640
		[SerializeField]
		private float m_ScrollDeltaPerTick = 6f;

		// Token: 0x04000669 RID: 1641
		private static Dictionary<InputAction, InputSystemUIInputModule.InputActionReferenceState> s_InputActionReferenceCounts = new Dictionary<InputAction, InputSystemUIInputModule.InputActionReferenceState>();

		// Token: 0x0400066A RID: 1642
		[NonSerialized]
		private bool m_ActionsHooked;

		// Token: 0x0400066B RID: 1643
		[NonSerialized]
		private bool m_NeedToPurgeStalePointers;

		// Token: 0x0400066C RID: 1644
		private Action<InputAction.CallbackContext> m_OnPointDelegate;

		// Token: 0x0400066D RID: 1645
		private Action<InputAction.CallbackContext> m_OnMoveDelegate;

		// Token: 0x0400066E RID: 1646
		private Action<InputAction.CallbackContext> m_OnLeftClickDelegate;

		// Token: 0x0400066F RID: 1647
		private Action<InputAction.CallbackContext> m_OnRightClickDelegate;

		// Token: 0x04000670 RID: 1648
		private Action<InputAction.CallbackContext> m_OnMiddleClickDelegate;

		// Token: 0x04000671 RID: 1649
		private Action<InputAction.CallbackContext> m_OnScrollWheelDelegate;

		// Token: 0x04000672 RID: 1650
		private Action<InputAction.CallbackContext> m_OnTrackedDevicePositionDelegate;

		// Token: 0x04000673 RID: 1651
		private Action<InputAction.CallbackContext> m_OnTrackedDeviceOrientationDelegate;

		// Token: 0x04000674 RID: 1652
		private Action<object> m_OnControlsChangedDelegate;

		// Token: 0x04000675 RID: 1653
		[NonSerialized]
		private int m_CurrentPointerId = -1;

		// Token: 0x04000676 RID: 1654
		[NonSerialized]
		private int m_CurrentPointerIndex = -1;

		// Token: 0x04000677 RID: 1655
		[NonSerialized]
		internal UIPointerType m_CurrentPointerType;

		// Token: 0x04000678 RID: 1656
		internal InlinedArray<int> m_PointerIds;

		// Token: 0x04000679 RID: 1657
		internal InlinedArray<InputControl> m_PointerTouchControls;

		// Token: 0x0400067A RID: 1658
		internal InlinedArray<PointerModel> m_PointerStates;

		// Token: 0x0400067B RID: 1659
		private NavigationModel m_NavigationState;

		// Token: 0x0400067C RID: 1660
		[NonSerialized]
		private GameObject m_LocalMultiPlayerRoot;

		// Token: 0x02000119 RID: 281
		private struct InputActionReferenceState
		{
			// Token: 0x0400067D RID: 1661
			public int refCount;

			// Token: 0x0400067E RID: 1662
			public bool enabledByInputModule;
		}

		// Token: 0x0200011A RID: 282
		public enum CursorLockBehavior
		{
			// Token: 0x04000680 RID: 1664
			OutsideScreen,
			// Token: 0x04000681 RID: 1665
			ScreenCenter
		}
	}
}
