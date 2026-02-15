using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000BB RID: 187
	public abstract class PointerInputModule : BaseInputModule
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0001A88D File Offset: 0x00018A8D
		protected bool GetPointerData(int id, out PointerEventData data, bool create)
		{
			if (!this.m_PointerData.TryGetValue(id, out data) && create)
			{
				data = new PointerEventData(base.eventSystem)
				{
					pointerId = id
				};
				this.m_PointerData.Add(id, data);
				return true;
			}
			return false;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		protected void RemovePointerData(PointerEventData data)
		{
			this.m_PointerData.Remove(data.pointerId);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001A8DC File Offset: 0x00018ADC
		protected PointerEventData GetTouchPointerEventData(Touch input, out bool pressed, out bool released)
		{
			PointerEventData pointerData;
			bool created = this.GetPointerData(input.fingerId, out pointerData, true);
			pointerData.Reset();
			pressed = created || input.phase == TouchPhase.Began;
			released = input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended;
			if (created)
			{
				pointerData.position = input.position;
			}
			if (pressed)
			{
				pointerData.delta = Vector2.zero;
			}
			else
			{
				pointerData.delta = input.position - pointerData.position;
			}
			pointerData.position = input.position;
			pointerData.button = PointerEventData.InputButton.Left;
			if (input.phase == TouchPhase.Canceled)
			{
				pointerData.pointerCurrentRaycast = default(RaycastResult);
			}
			else
			{
				base.eventSystem.RaycastAll(pointerData, this.m_RaycastResultCache);
				RaycastResult raycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
				pointerData.pointerCurrentRaycast = raycast;
				this.m_RaycastResultCache.Clear();
			}
			pointerData.pressure = input.pressure;
			pointerData.altitudeAngle = input.altitudeAngle;
			pointerData.azimuthAngle = input.azimuthAngle;
			pointerData.radius = Vector2.one * input.radius;
			pointerData.radiusVariance = Vector2.one * input.radiusVariance;
			return pointerData;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001AA1C File Offset: 0x00018C1C
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
			to.pressure = from.pressure;
			to.tangentialPressure = from.tangentialPressure;
			to.altitudeAngle = from.altitudeAngle;
			to.azimuthAngle = from.azimuthAngle;
			to.twist = from.twist;
			to.radius = from.radius;
			to.radiusVariance = from.radiusVariance;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001AABC File Offset: 0x00018CBC
		protected PointerEventData.FramePressState StateForMouseButton(int buttonId)
		{
			bool pressed = base.input.GetMouseButtonDown(buttonId);
			bool released = base.input.GetMouseButtonUp(buttonId);
			if (pressed && released)
			{
				return PointerEventData.FramePressState.PressedAndReleased;
			}
			if (pressed)
			{
				return PointerEventData.FramePressState.Pressed;
			}
			if (released)
			{
				return PointerEventData.FramePressState.Released;
			}
			return PointerEventData.FramePressState.NotChanged;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001AAF5 File Offset: 0x00018CF5
		protected virtual PointerInputModule.MouseState GetMousePointerEventData()
		{
			return this.GetMousePointerEventData(0);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001AB00 File Offset: 0x00018D00
		protected virtual PointerInputModule.MouseState GetMousePointerEventData(int id)
		{
			PointerEventData leftData;
			bool pointerData = this.GetPointerData(-1, out leftData, true);
			leftData.Reset();
			if (pointerData)
			{
				leftData.position = base.input.mousePosition;
			}
			Vector2 pos = base.input.mousePosition;
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				leftData.position = new Vector2(-1f, -1f);
				leftData.delta = Vector2.zero;
			}
			else
			{
				leftData.delta = pos - leftData.position;
				leftData.position = pos;
			}
			leftData.scrollDelta = base.input.mouseScrollDelta;
			leftData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(leftData, this.m_RaycastResultCache);
			RaycastResult raycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			leftData.pointerCurrentRaycast = raycast;
			this.m_RaycastResultCache.Clear();
			PointerEventData rightData;
			this.GetPointerData(-2, out rightData, true);
			rightData.Reset();
			this.CopyFromTo(leftData, rightData);
			rightData.button = PointerEventData.InputButton.Right;
			PointerEventData middleData;
			this.GetPointerData(-3, out middleData, true);
			middleData.Reset();
			this.CopyFromTo(leftData, middleData);
			middleData.button = PointerEventData.InputButton.Middle;
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, this.StateForMouseButton(0), leftData);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, this.StateForMouseButton(1), rightData);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, this.StateForMouseButton(2), middleData);
			return this.m_MouseState;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001AC50 File Offset: 0x00018E50
		protected PointerEventData GetLastPointerEventData(int id)
		{
			PointerEventData data;
			this.GetPointerData(id, out data, false);
			return data;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001AC6C File Offset: 0x00018E6C
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001AC98 File Offset: 0x00018E98
		protected virtual void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject targetGO = ((Cursor.lockState == CursorLockMode.Locked) ? null : pointerEvent.pointerCurrentRaycast.gameObject);
			base.HandlePointerExitAndEnter(pointerEvent, targetGO);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		protected virtual void ProcessDrag(PointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || Cursor.lockState == CursorLockMode.Locked || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (!pointerEvent.dragging && PointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001AD90 File Offset: 0x00018F90
		public override bool IsPointerOverGameObject(int pointerId)
		{
			PointerEventData lastPointer = this.GetLastPointerEventData(pointerId);
			return lastPointer != null && lastPointer.pointerEnter != null;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (PointerEventData pointer in this.m_PointerData.Values)
			{
				base.HandlePointerExitAndEnter(pointer, null);
			}
			this.m_PointerData.Clear();
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001AE30 File Offset: 0x00019030
		public override string ToString()
		{
			string text = "<b>Pointer Input Module of type: </b>";
			Type type = base.GetType();
			StringBuilder sb = new StringBuilder(text + ((type != null) ? type.ToString() : null));
			sb.AppendLine();
			foreach (KeyValuePair<int, PointerEventData> pointer in this.m_PointerData)
			{
				if (pointer.Value != null)
				{
					sb.AppendLine("<B>Pointer:</b> " + pointer.Key.ToString());
					sb.AppendLine(pointer.Value.ToString());
				}
			}
			return sb.ToString();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001AEE8 File Offset: 0x000190E8
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			if (ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo) != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x0400031D RID: 797
		public const int kMouseLeftId = -1;

		// Token: 0x0400031E RID: 798
		public const int kMouseRightId = -2;

		// Token: 0x0400031F RID: 799
		public const int kMouseMiddleId = -3;

		// Token: 0x04000320 RID: 800
		public const int kFakeTouchesId = -4;

		// Token: 0x04000321 RID: 801
		protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData>();

		// Token: 0x04000322 RID: 802
		private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();

		// Token: 0x020000BC RID: 188
		protected class ButtonState
		{
			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001AF2D File Offset: 0x0001912D
			// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001AF35 File Offset: 0x00019135
			public PointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001AF3E File Offset: 0x0001913E
			// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001AF46 File Offset: 0x00019146
			public PointerEventData.InputButton button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x04000323 RID: 803
			private PointerEventData.InputButton m_Button;

			// Token: 0x04000324 RID: 804
			private PointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x020000BD RID: 189
		protected class MouseState
		{
			// Token: 0x06000702 RID: 1794 RVA: 0x0001AF50 File Offset: 0x00019150
			public bool AnyPressesThisFrame()
			{
				int trackedButtonsCount = this.m_TrackedButtons.Count;
				for (int i = 0; i < trackedButtonsCount; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x0001AF90 File Offset: 0x00019190
			public bool AnyReleasesThisFrame()
			{
				int trackedButtonsCount = this.m_TrackedButtons.Count;
				for (int i = 0; i < trackedButtonsCount; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x0001AFD0 File Offset: 0x000191D0
			public PointerInputModule.ButtonState GetButtonState(PointerEventData.InputButton button)
			{
				PointerInputModule.ButtonState tracked = null;
				int trackedButtonsCount = this.m_TrackedButtons.Count;
				for (int i = 0; i < trackedButtonsCount; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						tracked = this.m_TrackedButtons[i];
						break;
					}
				}
				if (tracked == null)
				{
					tracked = new PointerInputModule.ButtonState
					{
						button = button,
						eventData = new PointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(tracked);
				}
				return tracked;
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x0001B042 File Offset: 0x00019242
			public void SetButtonState(PointerEventData.InputButton button, PointerEventData.FramePressState stateForMouseButton, PointerEventData data)
			{
				PointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x04000325 RID: 805
			private List<PointerInputModule.ButtonState> m_TrackedButtons = new List<PointerInputModule.ButtonState>();
		}

		// Token: 0x020000BE RID: 190
		public class MouseButtonEventData
		{
			// Token: 0x06000707 RID: 1799 RVA: 0x0001B075 File Offset: 0x00019275
			public bool PressedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Pressed || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x0001B08A File Offset: 0x0001928A
			public bool ReleasedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Released || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x04000326 RID: 806
			public PointerEventData.FramePressState buttonState;

			// Token: 0x04000327 RID: 807
			public PointerEventData buttonData;
		}
	}
}
