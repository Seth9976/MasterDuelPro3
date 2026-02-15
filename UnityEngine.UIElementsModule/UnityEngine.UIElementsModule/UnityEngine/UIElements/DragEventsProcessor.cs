using System;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018C RID: 396
	internal abstract class DragEventsProcessor
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0000C45B File Offset: 0x0000A65B
		protected virtual bool supportsDragEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00038540 File Offset: 0x00036740
		private bool useDragEvents
		{
			get
			{
				return this.isEditorContext && this.supportsDragEvents;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00038553 File Offset: 0x00036753
		protected IDragAndDrop dragAndDrop
		{
			get
			{
				return DragAndDropUtility.GetDragAndDrop(this.m_Target.panel);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00038568 File Offset: 0x00036768
		internal virtual bool isEditorContext
		{
			get
			{
				Assert.IsNotNull<VisualElement>(this.m_Target);
				Assert.IsNotNull<VisualElement>(this.m_Target.parent);
				return this.m_Target.panel.contextType == ContextType.Editor;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000385AC File Offset: 0x000367AC
		internal DragEventsProcessor(VisualElement target)
		{
			this.m_Target = target;
			this.m_Target.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.RegisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			this.RegisterCallbacksFromTarget();
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00038601 File Offset: 0x00036801
		private void RegisterCallbacksFromTarget(AttachToPanelEvent evt)
		{
			this.RegisterCallbacksFromTarget();
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0003860C File Offset: 0x0003680C
		private void RegisterCallbacksFromTarget()
		{
			bool isRegistered = this.m_IsRegistered;
			if (!isRegistered)
			{
				this.m_IsRegistered = true;
				this.m_Target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
				this.m_Target.RegisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancelEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCapturedOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000386C6 File Offset: 0x000368C6
		private void UnregisterCallbacksFromTarget(DetachFromPanelEvent evt)
		{
			this.UnregisterCallbacksFromTarget(false);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000386D4 File Offset: 0x000368D4
		internal void UnregisterCallbacksFromTarget(bool unregisterPanelEvents = false)
		{
			this.m_IsRegistered = false;
			this.m_Target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
			this.m_Target.UnregisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancelEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCapturedOut), TrickleDown.NoTrickleDown);
			if (unregisterPanelEvents)
			{
				this.m_Target.UnregisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.RegisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
				this.m_Target.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000BBD RID: 3005
		protected abstract bool CanStartDrag(Vector3 pointerPosition);

		// Token: 0x06000BBE RID: 3006
		protected internal abstract StartDragArgs StartDrag(Vector3 pointerPosition);

		// Token: 0x06000BBF RID: 3007
		protected internal abstract void UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000BC0 RID: 3008
		protected internal abstract void OnDrop(Vector3 pointerPosition);

		// Token: 0x06000BC1 RID: 3009
		protected abstract void ClearDragAndDropUI(bool dragCancelled);

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000387B8 File Offset: 0x000369B8
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			bool flag = evt.button != 0;
			if (flag)
			{
				this.m_DragState = DragEventsProcessor.DragState.None;
			}
			else
			{
				bool flag2 = this.CanStartDrag(evt.position);
				if (flag2)
				{
					this.m_DragState = DragEventsProcessor.DragState.CanStartDrag;
					this.m_Start = evt.position;
				}
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00038804 File Offset: 0x00036A04
		internal void OnPointerUpEvent(PointerUpEvent evt)
		{
			bool flag = !this.useDragEvents && this.m_DragState == DragEventsProcessor.DragState.Dragging;
			if (flag)
			{
				DragEventsProcessor target = this.GetDropTarget(evt.position) ?? this;
				target.UpdateDrag(evt.position);
				target.OnDrop(evt.position);
				target.ClearDragAndDropUI(false);
				evt.StopPropagation();
			}
			this.m_Target.ReleasePointer(evt.pointerId);
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000388A1 File Offset: 0x00036AA1
		private void OnPointerLeaveEvent(PointerLeaveEvent evt)
		{
			this.ClearDragAndDropUI(false);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000388AC File Offset: 0x00036AAC
		private void OnPointerCancelEvent(PointerCancelEvent evt)
		{
			bool flag = !this.useDragEvents;
			if (flag)
			{
				this.ClearDragAndDropUI(true);
			}
			this.m_Target.ReleasePointer(evt.pointerId);
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00038904 File Offset: 0x00036B04
		private void OnPointerCapturedOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.useDragEvents;
			if (flag)
			{
				this.ClearDragAndDropUI(true);
			}
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003894C File Offset: 0x00036B4C
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool isHandledByDraggable = evt.isHandledByDraggable;
			if (!isHandledByDraggable)
			{
				bool flag = !this.useDragEvents && this.m_DragState == DragEventsProcessor.DragState.Dragging;
				if (flag)
				{
					DragEventsProcessor target = this.GetDropTarget(evt.position) ?? this;
					target.UpdateDrag(evt.position);
				}
				else
				{
					bool flag2 = this.m_DragState != DragEventsProcessor.DragState.CanStartDrag;
					if (!flag2)
					{
						bool flag3 = (this.m_Start - evt.position).sqrMagnitude >= 100f;
						if (flag3)
						{
							StartDragArgs startDragArgs = this.StartDrag(this.m_Start);
							bool flag4 = startDragArgs.visualMode == DragVisualMode.Rejected;
							if (flag4)
							{
								this.m_DragState = DragEventsProcessor.DragState.None;
							}
							else
							{
								bool flag5 = !this.useDragEvents;
								if (flag5)
								{
									bool supportsDragEvents = this.supportsDragEvents;
									if (supportsDragEvents)
									{
										this.dragAndDrop.StartDrag(startDragArgs, evt.position);
									}
								}
								else
								{
									bool flag6 = Event.current != null && Event.current.type != EventType.MouseDown && Event.current.type != EventType.MouseDrag;
									if (flag6)
									{
										return;
									}
									this.dragAndDrop.StartDrag(startDragArgs, evt.position);
								}
								this.m_DragState = DragEventsProcessor.DragState.Dragging;
								this.m_Target.CapturePointer(evt.pointerId);
								evt.isHandledByDraggable = true;
								evt.StopPropagation();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00038AC0 File Offset: 0x00036CC0
		private DragEventsProcessor GetDropTarget(Vector2 position)
		{
			DragEventsProcessor target = null;
			bool flag = this.m_Target.worldBound.Contains(position);
			if (flag)
			{
				target = this;
			}
			else
			{
				bool supportsDragEvents = this.supportsDragEvents;
				if (supportsDragEvents)
				{
					VisualElement leafTarget = this.m_Target.elementPanel.Pick(position);
					BaseVerticalCollectionView targetView = ((leafTarget != null) ? leafTarget.GetFirstOfType<BaseVerticalCollectionView>() : null);
					target = ((targetView != null) ? targetView.dragger : null);
				}
			}
			return target;
		}

		// Token: 0x04000772 RID: 1906
		private bool m_IsRegistered;

		// Token: 0x04000773 RID: 1907
		private DragEventsProcessor.DragState m_DragState;

		// Token: 0x04000774 RID: 1908
		private Vector3 m_Start;

		// Token: 0x04000775 RID: 1909
		protected readonly VisualElement m_Target;

		// Token: 0x0200018D RID: 397
		internal enum DragState
		{
			// Token: 0x04000777 RID: 1911
			None,
			// Token: 0x04000778 RID: 1912
			CanStartDrag,
			// Token: 0x04000779 RID: 1913
			Dragging
		}
	}
}
