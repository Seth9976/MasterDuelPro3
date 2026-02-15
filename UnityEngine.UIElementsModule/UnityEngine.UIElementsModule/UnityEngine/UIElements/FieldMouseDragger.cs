using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000247 RID: 583
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class FieldMouseDragger<T> : BaseFieldMouseDragger
	{
		// Token: 0x06000F9E RID: 3998 RVA: 0x0004346D File Offset: 0x0004166D
		public FieldMouseDragger(IValueField<T> drivenField)
		{
			this.m_DrivenField = drivenField;
			this.m_DragElement = null;
			this.m_DragHotZone = new Rect(0f, 0f, -1f, -1f);
			this.dragging = false;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000434AC File Offset: 0x000416AC
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x000434B4 File Offset: 0x000416B4
		public bool dragging { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x000434BD File Offset: 0x000416BD
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x000434C5 File Offset: 0x000416C5
		public T startValue { get; set; }

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000434D0 File Offset: 0x000416D0
		public sealed override void SetDragZone(VisualElement dragElement, Rect hotZone)
		{
			bool flag = this.m_DragElement != null;
			if (flag)
			{
				this.m_DragElement.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.UpdateValueOnPointerDown), TrickleDown.TrickleDown);
				this.m_DragElement.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.UpdateValueOnPointerUp), TrickleDown.NoTrickleDown);
				this.m_DragElement.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.UpdateValueOnKeyDown), TrickleDown.NoTrickleDown);
			}
			this.m_DragElement = dragElement;
			this.m_DragHotZone = hotZone;
			bool flag2 = this.m_DragElement != null;
			if (flag2)
			{
				this.dragging = false;
				this.m_DragElement.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.UpdateValueOnPointerDown), TrickleDown.TrickleDown);
				this.m_DragElement.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.UpdateValueOnPointerUp), TrickleDown.NoTrickleDown);
				this.m_DragElement.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.UpdateValueOnKeyDown), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000435A8 File Offset: 0x000417A8
		private bool CanStartDrag(int button, Vector2 localPosition)
		{
			return button == 0 && (this.m_DragHotZone.width < 0f || this.m_DragHotZone.height < 0f || this.m_DragHotZone.Contains(this.m_DragElement.WorldToLocal(localPosition)));
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00043600 File Offset: 0x00041800
		private void UpdateValueOnPointerDown(PointerDownEvent evt)
		{
			bool flag = this.CanStartDrag(evt.button, evt.localPosition);
			if (flag)
			{
				bool flag2 = evt.pointerType == PointerType.mouse;
				if (flag2)
				{
					this.m_DragElement.CaptureMouse();
					this.ProcessDownEvent(evt);
				}
				else
				{
					bool flag3 = this.m_DragElement.panel.contextType == ContextType.Editor;
					if (flag3)
					{
						this.m_DragElement.CapturePointer(evt.pointerId);
						this.ProcessDownEvent(evt);
					}
				}
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004368C File Offset: 0x0004188C
		private void ProcessDownEvent(EventBase evt)
		{
			evt.StopPropagation();
			this.dragging = true;
			this.m_DragElement.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.UpdateValueOnPointerMove), TrickleDown.NoTrickleDown);
			this.startValue = this.m_DrivenField.value;
			this.m_DrivenField.StartDragging();
			BaseVisualElementPanel baseVisualElementPanel = this.m_DragElement.panel as BaseVisualElementPanel;
			if (baseVisualElementPanel != null)
			{
				UIElementsBridge uiElementsBridge = baseVisualElementPanel.uiElementsBridge;
				if (uiElementsBridge != null)
				{
					uiElementsBridge.SetWantsMouseJumping(1);
				}
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00043708 File Offset: 0x00041908
		private void UpdateValueOnPointerMove(PointerMoveEvent evt)
		{
			this.ProcessMoveEvent(evt.shiftKey, evt.altKey, evt.deltaPosition);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004372C File Offset: 0x0004192C
		private void ProcessMoveEvent(bool shiftKey, bool altKey, Vector2 deltaPosition)
		{
			bool dragging = this.dragging;
			if (dragging)
			{
				DeltaSpeed s = (shiftKey ? DeltaSpeed.Fast : (altKey ? DeltaSpeed.Slow : DeltaSpeed.Normal));
				this.m_DrivenField.ApplyInputDeviceDelta(deltaPosition, s, this.startValue);
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004376D File Offset: 0x0004196D
		private void UpdateValueOnPointerUp(PointerUpEvent evt)
		{
			this.ProcessUpEvent(evt, evt.pointerId);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00043780 File Offset: 0x00041980
		private void ProcessUpEvent(EventBase evt, int pointerId)
		{
			bool dragging = this.dragging;
			if (dragging)
			{
				this.dragging = false;
				this.m_DragElement.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.UpdateValueOnPointerMove), TrickleDown.NoTrickleDown);
				this.m_DragElement.ReleasePointer(pointerId);
				bool flag = evt is IMouseEvent;
				if (flag)
				{
					this.m_DragElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
				}
				BaseVisualElementPanel baseVisualElementPanel = this.m_DragElement.panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					UIElementsBridge uiElementsBridge = baseVisualElementPanel.uiElementsBridge;
					if (uiElementsBridge != null)
					{
						uiElementsBridge.SetWantsMouseJumping(0);
					}
				}
				this.m_DrivenField.StopDragging();
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00043824 File Offset: 0x00041A24
		private void UpdateValueOnKeyDown(KeyDownEvent evt)
		{
			bool flag = this.dragging && evt.keyCode == KeyCode.Escape;
			if (flag)
			{
				this.dragging = false;
				this.m_DrivenField.value = this.startValue;
				this.m_DrivenField.StopDragging();
				VisualElement elementTarget = evt.elementTarget;
				IPanel panel = ((elementTarget != null) ? elementTarget.panel : null);
				panel.ReleasePointer(PointerId.mousePointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					UIElementsBridge uiElementsBridge = baseVisualElementPanel.uiElementsBridge;
					if (uiElementsBridge != null)
					{
						uiElementsBridge.SetWantsMouseJumping(0);
					}
				}
			}
		}

		// Token: 0x040008CE RID: 2254
		private readonly IValueField<T> m_DrivenField;

		// Token: 0x040008CF RID: 2255
		private VisualElement m_DragElement;

		// Token: 0x040008D0 RID: 2256
		private Rect m_DragHotZone;
	}
}
