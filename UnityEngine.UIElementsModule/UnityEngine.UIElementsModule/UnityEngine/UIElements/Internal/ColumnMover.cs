using System;
using System.Diagnostics;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005E8 RID: 1512
	internal class ColumnMover : PointerManipulator
	{
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000A87FD File Offset: 0x000A69FD
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x000A8805 File Offset: 0x000A6A05
		public ColumnLayout columnLayout { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000A880E File Offset: 0x000A6A0E
		// (set) Token: 0x060028F5 RID: 10485 RVA: 0x000A8818 File Offset: 0x000A6A18
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				bool flag = this.m_Active == value;
				if (!flag)
				{
					this.m_Active = value;
					Action<ColumnMover> action = this.activeChanged;
					if (action != null)
					{
						action(this);
					}
				}
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000A884F File Offset: 0x000A6A4F
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x000A8858 File Offset: 0x000A6A58
		public bool moving
		{
			get
			{
				return this.m_Moving;
			}
			set
			{
				bool flag = this.m_Moving == value;
				if (!flag)
				{
					this.m_Moving = value;
					Action<ColumnMover> action = this.movingChanged;
					if (action != null)
					{
						action(this);
					}
				}
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060028F8 RID: 10488 RVA: 0x000A8890 File Offset: 0x000A6A90
		// (remove) Token: 0x060028F9 RID: 10489 RVA: 0x000A88C8 File Offset: 0x000A6AC8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ColumnMover> activeChanged;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060028FA RID: 10490 RVA: 0x000A8900 File Offset: 0x000A6B00
		// (remove) Token: 0x060028FB RID: 10491 RVA: 0x000A8938 File Offset: 0x000A6B38
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ColumnMover> movingChanged;

		// Token: 0x060028FC RID: 10492 RVA: 0x000A8970 File Offset: 0x000A6B70
		public ColumnMover()
		{
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000A89A4 File Offset: 0x000A6BA4
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000A8A48 File Offset: 0x000A6C48
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000A8AEC File Offset: 0x000A6CEC
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000A8B24 File Offset: 0x000A6D24
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000A8B54 File Offset: 0x000A6D54
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000A8B98 File Offset: 0x000A6D98
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000A8BD0 File Offset: 0x000A6DD0
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000A8BFC File Offset: 0x000A6DFC
		protected void ProcessCancelEvent(EventBase evt, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			bool moving = this.moving;
			if (moving)
			{
				this.EndDragMove(true);
			}
			evt.StopPropagation();
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000A8C5C File Offset: 0x000A6E5C
		private void OnKeyDown(KeyDownEvent e)
		{
			bool flag = e.keyCode == KeyCode.Escape && this.moving;
			if (flag)
			{
				this.EndDragMove(true);
			}
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000A8C8C File Offset: 0x000A6E8C
		private void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			bool active = this.active;
			if (active)
			{
				evt.StopImmediatePropagation();
			}
			else
			{
				base.target.CapturePointer(pointerId);
				bool flag = !(evt is IPointerEvent);
				if (flag)
				{
					base.target.panel.ProcessPointerCapture(pointerId);
				}
				VisualElement ve = evt.currentTarget as VisualElement;
				MultiColumnCollectionHeader header = ve.GetFirstAncestorOfType<MultiColumnCollectionHeader>();
				bool flag2 = !header.columns.reorderable;
				if (!flag2)
				{
					this.m_Header = header;
					Vector2 pos = ve.ChangeCoordinatesTo(this.m_Header, localPosition);
					this.columnLayout = this.m_Header.columnLayout;
					this.m_Cancelled = false;
					this.m_StartPos = pos.x;
					this.active = true;
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000A8D54 File Offset: 0x000A6F54
		private void ProcessMoveEvent(EventBase e, Vector2 localPosition)
		{
			bool cancelled = this.m_Cancelled;
			if (!cancelled)
			{
				VisualElement ve = e.currentTarget as VisualElement;
				Vector2 pos = ve.ChangeCoordinatesTo(this.m_Header, localPosition);
				bool flag = !this.moving && Mathf.Abs(this.m_StartPos - pos.x) > 5f;
				if (flag)
				{
					this.BeginDragMove(this.m_StartPos);
				}
				bool moving = this.moving;
				if (moving)
				{
					this.DragMove(pos.x);
				}
				e.StopPropagation();
			}
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000A8DE4 File Offset: 0x000A6FE4
		private void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			bool shouldStopPropagationImmediately = this.moving || this.m_Cancelled;
			this.EndDragMove(false);
			bool flag2 = shouldStopPropagationImmediately;
			if (flag2)
			{
				evt.StopImmediatePropagation();
			}
			else
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000A8E58 File Offset: 0x000A7058
		private void BeginDragMove(float pos)
		{
			float right = 0f;
			Columns columns = this.columnLayout.columns;
			foreach (Column column in columns.visibleList)
			{
				right += this.columnLayout.GetDesiredWidth(column);
				bool flag = this.m_ColumnToMove == null;
				if (flag)
				{
					bool flag2 = right > pos;
					if (flag2)
					{
						this.m_ColumnToMove = column;
					}
				}
			}
			this.moving = true;
			this.m_LastPos = pos;
			this.m_PreviewElement = new MultiColumnHeaderColumnMovePreview();
			this.m_LocationPreviewElement = new MultiColumnHeaderColumnMoveLocationPreview();
			this.m_Header.hierarchy.Add(this.m_PreviewElement);
			ScrollView firstAncestorOfType = this.m_Header.GetFirstAncestorOfType<ScrollView>();
			VisualElement locationPreviewParent = ((firstAncestorOfType != null) ? firstAncestorOfType.parent : null) ?? this.m_Header;
			locationPreviewParent.hierarchy.Add(this.m_LocationPreviewElement);
			this.m_ColumnToMovePos = this.columnLayout.GetDesiredPosition(this.m_ColumnToMove);
			this.m_ColumnToMoveWidth = this.columnLayout.GetDesiredWidth(this.m_ColumnToMove);
			this.UpdateMoveLocation();
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000A8F98 File Offset: 0x000A7198
		internal void DragMove(float pos)
		{
			this.m_LastPos = pos;
			this.UpdateMoveLocation();
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000A8FAC File Offset: 0x000A71AC
		private void UpdatePreviewPosition()
		{
			this.m_PreviewElement.style.left = this.m_ColumnToMovePos + this.m_LastPos - this.m_StartPos;
			this.m_PreviewElement.style.width = this.m_ColumnToMoveWidth;
			bool flag = this.m_DestinationColumn != null;
			if (flag)
			{
				this.m_LocationPreviewElement.style.left = this.columnLayout.GetDesiredPosition(this.m_DestinationColumn) + ((!this.m_MoveBeforeDestination) ? this.columnLayout.GetDesiredWidth(this.m_DestinationColumn) : 0f);
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000A9058 File Offset: 0x000A7258
		private void UpdateMoveLocation()
		{
			float right = 0f;
			this.m_DestinationColumn = null;
			this.m_MoveBeforeDestination = false;
			foreach (Column column in this.columnLayout.columns.visibleList)
			{
				this.m_DestinationColumn = column;
				float w = this.columnLayout.GetDesiredWidth(this.m_DestinationColumn);
				float centerPos = right + w / 2f;
				right += w;
				bool flag = right > this.m_LastPos;
				if (flag)
				{
					this.m_MoveBeforeDestination = this.m_LastPos < centerPos;
					break;
				}
			}
			this.UpdatePreviewPosition();
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000A9114 File Offset: 0x000A7314
		private void EndDragMove(bool cancelled)
		{
			bool flag = !this.moving || this.m_Cancelled;
			if (!flag)
			{
				this.m_Cancelled = cancelled;
				bool flag2 = !cancelled;
				if (flag2)
				{
					int destIndex = this.m_DestinationColumn.displayIndex;
					bool flag3 = !this.m_MoveBeforeDestination;
					if (flag3)
					{
						destIndex++;
					}
					bool flag4 = this.m_ColumnToMove.displayIndex < destIndex;
					if (flag4)
					{
						destIndex--;
					}
					bool flag5 = this.m_ColumnToMove.displayIndex != destIndex;
					if (flag5)
					{
						this.columnLayout.columns.ReorderDisplay(this.m_ColumnToMove.displayIndex, destIndex);
					}
				}
				VisualElement previewElement = this.m_PreviewElement;
				if (previewElement != null)
				{
					previewElement.RemoveFromHierarchy();
				}
				this.m_PreviewElement = null;
				MultiColumnHeaderColumnMoveLocationPreview locationPreviewElement = this.m_LocationPreviewElement;
				if (locationPreviewElement != null)
				{
					locationPreviewElement.RemoveFromHierarchy();
				}
				this.m_LocationPreviewElement = null;
				this.m_ColumnToMove = null;
				this.moving = false;
			}
		}

		// Token: 0x04001597 RID: 5527
		private float m_StartPos;

		// Token: 0x04001598 RID: 5528
		private float m_LastPos;

		// Token: 0x04001599 RID: 5529
		private bool m_Active;

		// Token: 0x0400159A RID: 5530
		private bool m_Moving;

		// Token: 0x0400159B RID: 5531
		private bool m_Cancelled;

		// Token: 0x0400159C RID: 5532
		private MultiColumnCollectionHeader m_Header;

		// Token: 0x0400159D RID: 5533
		private VisualElement m_PreviewElement;

		// Token: 0x0400159E RID: 5534
		private MultiColumnHeaderColumnMoveLocationPreview m_LocationPreviewElement;

		// Token: 0x0400159F RID: 5535
		private Column m_ColumnToMove;

		// Token: 0x040015A0 RID: 5536
		private float m_ColumnToMovePos;

		// Token: 0x040015A1 RID: 5537
		private float m_ColumnToMoveWidth;

		// Token: 0x040015A2 RID: 5538
		private Column m_DestinationColumn;

		// Token: 0x040015A3 RID: 5539
		private bool m_MoveBeforeDestination;
	}
}
