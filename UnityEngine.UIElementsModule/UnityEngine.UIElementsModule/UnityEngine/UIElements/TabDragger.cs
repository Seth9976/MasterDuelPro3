using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014E RID: 334
	internal class TabDragger : PointerManipulator
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00030F2A File Offset: 0x0002F12A
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00030F32 File Offset: 0x0002F132
		private TabLayout tabLayout { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00030F3B File Offset: 0x0002F13B
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00030F43 File Offset: 0x0002F143
		internal bool active { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00030F4C File Offset: 0x0002F14C
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00030F54 File Offset: 0x0002F154
		internal bool isVertical { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00030F5D File Offset: 0x0002F15D
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00030F68 File Offset: 0x0002F168
		internal bool moving
		{
			get
			{
				return this.m_Moving;
			}
			private set
			{
				bool flag = this.m_Moving == value;
				if (!flag)
				{
					this.m_Moving = value;
					this.m_TabToMove.EnableInClassList(Tab.draggingUssClassName, this.moving);
				}
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00030FA4 File Offset: 0x0002F1A4
		public TabDragger()
		{
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00030FE4 File Offset: 0x0002F1E4
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00031088 File Offset: 0x0002F288
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0003112C File Offset: 0x0002F32C
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				bool active = this.active;
				if (active)
				{
					evt.StopImmediatePropagation();
				}
				else
				{
					this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
				}
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00031178 File Offset: 0x0002F378
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000311A8 File Offset: 0x0002F3A8
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000311EC File Offset: 0x0002F3EC
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00031224 File Offset: 0x0002F424
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00031250 File Offset: 0x0002F450
		private void ProcessCancelEvent(EventBase evt, int pointerId)
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
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000312A8 File Offset: 0x0002F4A8
		private void OnKeyDown(KeyDownEvent e)
		{
			bool flag = e.keyCode == KeyCode.Escape && this.moving;
			if (flag)
			{
				this.active = false;
				bool flag2 = this.m_DraggingPointerId != PointerId.invalidPointerId;
				if (flag2)
				{
					base.target.ReleasePointer(this.m_DraggingPointerId);
				}
				this.EndDragMove(true);
				e.StopPropagation();
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00031310 File Offset: 0x0002F510
		private void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			VisualElement ve = evt.currentTarget as VisualElement;
			TabView tabView = ((ve != null) ? ve.GetFirstAncestorOfType<TabView>() : null);
			bool flag = tabView == null || !tabView.reorderable;
			if (!flag)
			{
				base.target.CapturePointer(pointerId);
				this.m_DraggingPointerId = pointerId;
				bool flag2 = !(evt is IPointerEvent);
				if (flag2)
				{
					base.target.panel.ProcessPointerCapture(pointerId);
				}
				this.m_TabView = tabView;
				this.m_Header = tabView.header;
				this.isVertical = this.m_Header.resolvedStyle.flexDirection == FlexDirection.Column;
				this.tabLayout = new TabLayout(this.m_TabView, this.isVertical);
				Vector2 pos = ve.ChangeCoordinatesTo(this.m_Header, localPosition);
				this.m_Cancelled = false;
				this.m_StartPos = (this.isVertical ? pos.y : pos.x);
				this.active = true;
				evt.StopPropagation();
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0003140C File Offset: 0x0002F60C
		private void ProcessMoveEvent(EventBase e, Vector2 localPosition)
		{
			bool cancelled = this.m_Cancelled;
			if (!cancelled)
			{
				VisualElement ve = e.currentTarget as VisualElement;
				Vector2 pos = ve.ChangeCoordinatesTo(this.m_Header, localPosition);
				float currentPos = (this.isVertical ? pos.y : pos.x);
				bool flag = !this.moving && Mathf.Abs(this.m_StartPos - currentPos) > 5f;
				if (flag)
				{
					this.BeginDragMove(this.m_StartPos);
				}
				bool moving = this.moving;
				if (moving)
				{
					this.DragMove(currentPos);
				}
				e.StopPropagation();
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000314AC File Offset: 0x0002F6AC
		private void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			this.EndDragMove(false);
			evt.StopPropagation();
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00031500 File Offset: 0x0002F700
		private void BeginDragMove(float pos)
		{
			float destination = 0f;
			List<VisualElement> tabs = this.m_TabView.tabHeaders;
			this.m_TabToMove = this.m_TabView.tabHeaders[0];
			foreach (VisualElement tab in tabs)
			{
				destination += (this.isVertical ? TabLayout.GetHeight(tab) : TabLayout.GetWidth(tab));
				bool flag = destination > pos;
				if (flag)
				{
					this.m_TabToMove = tab;
					break;
				}
			}
			this.moving = true;
			this.m_LastPos = pos;
			this.m_PreviewElement = new TabDragPreview();
			this.m_LocationPreviewElement = new TabDragLocationPreview
			{
				classList = { this.isVertical ? TabDragLocationPreview.verticalUssClassName : TabDragLocationPreview.horizontalUssClassName }
			};
			this.m_Header.hierarchy.Add(this.m_PreviewElement);
			this.m_Header.Add(this.m_LocationPreviewElement);
			int index = this.m_TabView.tabHeaders.IndexOf(this.m_TabToMove);
			Tab activatedTab = this.m_TabView.tabs[index];
			this.m_TabView.activeTab = activatedTab;
			this.m_TabToMovePos = this.tabLayout.GetTabOffset(this.m_TabToMove);
			this.UpdateMoveLocation();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00031670 File Offset: 0x0002F870
		private void DragMove(float pos)
		{
			this.m_LastPos = pos;
			this.UpdateMoveLocation();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00031684 File Offset: 0x0002F884
		private void UpdatePreviewPosition()
		{
			float pos = this.m_TabToMovePos + this.m_LastPos - this.m_StartPos;
			float tabToMoveWidth = TabLayout.GetWidth(this.m_TabToMove);
			float destinationPos = this.tabLayout.GetTabOffset(this.m_DestinationTab);
			float size = (this.isVertical ? TabLayout.GetHeight(this.m_DestinationTab) : TabLayout.GetWidth(this.m_DestinationTab));
			float offset = ((!this.m_MoveBeforeDestination) ? size : 0f);
			bool isVertical = this.isVertical;
			if (isVertical)
			{
				this.m_PreviewElement.style.top = pos;
				this.m_PreviewElement.style.height = TabLayout.GetHeight(this.m_TabToMove);
				this.m_PreviewElement.style.width = tabToMoveWidth;
				bool flag = this.m_DestinationTab != null;
				if (flag)
				{
					this.m_LocationPreviewElement.preview.style.width = tabToMoveWidth;
					this.m_LocationPreviewElement.style.top = destinationPos + offset;
				}
			}
			else
			{
				this.m_PreviewElement.style.left = pos;
				this.m_PreviewElement.style.width = tabToMoveWidth;
				bool flag2 = this.m_DestinationTab != null;
				if (flag2)
				{
					this.m_LocationPreviewElement.style.left = destinationPos + offset;
				}
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000317FC File Offset: 0x0002F9FC
		private void UpdateMoveLocation()
		{
			float destination = 0f;
			this.m_DestinationTab = null;
			this.m_MoveBeforeDestination = false;
			foreach (VisualElement tab in this.m_TabView.tabHeaders)
			{
				this.m_DestinationTab = tab;
				float size = (this.isVertical ? TabLayout.GetHeight(this.m_DestinationTab) : TabLayout.GetWidth(this.m_DestinationTab));
				float centerPos = destination + size / 2f;
				destination += size;
				bool flag = destination > this.m_LastPos;
				if (flag)
				{
					this.m_MoveBeforeDestination = this.m_LastPos < centerPos;
					break;
				}
			}
			this.UpdatePreviewPosition();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000318C8 File Offset: 0x0002FAC8
		private void EndDragMove(bool cancelled)
		{
			bool flag = !this.moving || this.m_Cancelled;
			if (!flag)
			{
				this.m_Cancelled = cancelled;
				bool flag2 = !cancelled;
				if (flag2)
				{
					int startIndex = this.m_TabView.tabHeaders.IndexOf(this.m_TabToMove);
					int destIndex = this.m_TabView.tabHeaders.IndexOf(this.m_DestinationTab);
					bool flag3 = !this.m_MoveBeforeDestination;
					if (flag3)
					{
						destIndex++;
					}
					bool flag4 = startIndex < destIndex;
					if (flag4)
					{
						destIndex--;
					}
					bool flag5 = startIndex != destIndex;
					if (flag5)
					{
						this.tabLayout.ReorderDisplay(startIndex, destIndex);
					}
				}
				VisualElement previewElement = this.m_PreviewElement;
				if (previewElement != null)
				{
					previewElement.RemoveFromHierarchy();
				}
				this.m_PreviewElement = null;
				TabDragLocationPreview locationPreviewElement = this.m_LocationPreviewElement;
				if (locationPreviewElement != null)
				{
					locationPreviewElement.RemoveFromHierarchy();
				}
				this.m_LocationPreviewElement = null;
				this.moving = false;
				this.m_TabToMove = null;
			}
		}

		// Token: 0x0400068C RID: 1676
		private float m_StartPos;

		// Token: 0x0400068D RID: 1677
		private float m_LastPos;

		// Token: 0x0400068E RID: 1678
		private bool m_Moving;

		// Token: 0x0400068F RID: 1679
		private bool m_Cancelled;

		// Token: 0x04000690 RID: 1680
		private VisualElement m_Header;

		// Token: 0x04000691 RID: 1681
		private TabView m_TabView;

		// Token: 0x04000692 RID: 1682
		private VisualElement m_PreviewElement;

		// Token: 0x04000693 RID: 1683
		private TabDragLocationPreview m_LocationPreviewElement;

		// Token: 0x04000694 RID: 1684
		private VisualElement m_TabToMove;

		// Token: 0x04000695 RID: 1685
		private float m_TabToMovePos;

		// Token: 0x04000696 RID: 1686
		private VisualElement m_DestinationTab;

		// Token: 0x04000697 RID: 1687
		private bool m_MoveBeforeDestination;

		// Token: 0x04000698 RID: 1688
		private int m_DraggingPointerId = PointerId.invalidPointerId;
	}
}
