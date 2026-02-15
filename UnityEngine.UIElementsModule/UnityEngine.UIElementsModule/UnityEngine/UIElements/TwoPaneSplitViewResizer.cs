using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000168 RID: 360
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class TwoPaneSplitViewResizer : PointerManipulator
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00034A32 File Offset: 0x00032C32
		private TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_SplitView.orientation;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00034A3F File Offset: 0x00032C3F
		private VisualElement fixedPane
		{
			get
			{
				return this.m_SplitView.fixedPane;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00034A4C File Offset: 0x00032C4C
		private VisualElement flexedPane
		{
			get
			{
				return this.m_SplitView.flexedPane;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00034A5C File Offset: 0x00032C5C
		private float fixedPaneMinDimension
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.fixedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					num = this.fixedPane.resolvedStyle.minHeight.value;
				}
				return num;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00034AB0 File Offset: 0x00032CB0
		private float fixedPaneMargins
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.fixedPane.resolvedStyle.marginLeft + this.fixedPane.resolvedStyle.marginRight;
				}
				else
				{
					num = this.fixedPane.resolvedStyle.marginTop + this.fixedPane.resolvedStyle.marginBottom;
				}
				return num;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00034B14 File Offset: 0x00032D14
		private float flexedPaneMinDimension
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.flexedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					num = this.flexedPane.resolvedStyle.minHeight.value;
				}
				return num;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00034B68 File Offset: 0x00032D68
		private float flexedPaneMargin
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.flexedPane.resolvedStyle.marginLeft + this.flexedPane.resolvedStyle.marginRight;
				}
				else
				{
					num = this.flexedPane.resolvedStyle.marginTop + this.flexedPane.resolvedStyle.marginBottom;
				}
				return num;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00034BCC File Offset: 0x00032DCC
		public TwoPaneSplitViewResizer(TwoPaneSplitView splitView, int dir)
		{
			this.m_SplitView = splitView;
			this.m_Direction = dir;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00034C14 File Offset: 0x00032E14
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00034C70 File Offset: 0x00032E70
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00034CCC File Offset: 0x00032ECC
		public void ApplyDelta(float delta)
		{
			float oldDimension = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.fixedPane.resolvedStyle.width : this.fixedPane.resolvedStyle.height);
			float newDimension = oldDimension + delta;
			float adjustedfixedPaneMinDimension = this.fixedPaneMinDimension;
			bool flag = this.m_SplitView.fixedPaneIndex == 1;
			if (flag)
			{
				adjustedfixedPaneMinDimension += ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? (base.target.worldBound.width + Math.Abs(this.m_SplitView.dragLine.resolvedStyle.left)) : (base.target.worldBound.height + Math.Abs(this.m_SplitView.dragLine.resolvedStyle.top)));
			}
			bool flag2 = newDimension < oldDimension && newDimension < adjustedfixedPaneMinDimension;
			if (flag2)
			{
				newDimension = adjustedfixedPaneMinDimension;
			}
			float maxDimension = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.resolvedStyle.width : this.m_SplitView.resolvedStyle.height);
			maxDimension -= this.flexedPaneMinDimension + this.flexedPaneMargin + this.fixedPaneMargins;
			bool flag3 = this.m_SplitView.fixedPaneIndex == 0;
			if (flag3)
			{
				maxDimension -= ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? Math.Abs(base.target.worldBound.width - (this.m_SplitView.dragLine.resolvedStyle.width - Math.Abs(this.m_SplitView.dragLine.resolvedStyle.left))) : Math.Abs(base.target.worldBound.height - (this.m_SplitView.dragLine.resolvedStyle.height - Math.Abs(this.m_SplitView.dragLine.resolvedStyle.top))));
			}
			bool flag4 = newDimension > oldDimension && newDimension > maxDimension;
			if (flag4)
			{
				newDimension = maxDimension;
			}
			bool flag5 = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag5)
			{
				this.fixedPane.style.width = newDimension;
				bool flag6 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag6)
				{
					float newLeftValue = newDimension + this.fixedPaneMargins;
					bool flag7 = newLeftValue >= this.fixedPaneMinDimension;
					if (flag7)
					{
						base.target.style.left = newLeftValue;
					}
				}
				else
				{
					float newLeftValue2 = this.m_SplitView.resolvedStyle.width - newDimension - this.fixedPaneMargins;
					bool flag8 = newLeftValue2 >= this.flexedPaneMinDimension + this.flexedPaneMargin;
					if (flag8)
					{
						base.target.style.left = newLeftValue2;
					}
				}
			}
			else
			{
				this.fixedPane.style.height = newDimension;
				bool flag9 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag9)
				{
					float newTopValue = newDimension + this.fixedPaneMargins;
					bool flag10 = newTopValue >= this.fixedPaneMinDimension;
					if (flag10)
					{
						base.target.style.top = newTopValue;
					}
				}
				else
				{
					float newTopValue2 = this.m_SplitView.resolvedStyle.height - newDimension - this.fixedPaneMargins;
					bool flag11 = newTopValue2 >= this.flexedPaneMinDimension + this.flexedPaneMargin;
					if (flag11)
					{
						base.target.style.top = newTopValue2;
					}
				}
			}
			this.m_SplitView.fixedPaneDimension = newDimension;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00035040 File Offset: 0x00033240
		protected void OnPointerDown(PointerDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					this.m_Start = e.localPosition;
					this.m_Active = true;
					base.target.CapturePointer(e.pointerId);
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0003509C File Offset: 0x0003329C
		protected void OnPointerMove(PointerMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId);
			if (!flag)
			{
				bool dragLineIsBeforeAnchor = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? (this.m_SplitView.dragLine.worldBound.x < base.target.worldBound.x) : (this.m_SplitView.dragLine.worldBound.y < base.target.worldBound.y));
				float distanceBetweenDragLineAndAnchor = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? Math.Abs(base.target.worldBound.x - this.m_SplitView.dragLine.worldBound.x) : Math.Abs(base.target.worldBound.y - this.m_SplitView.dragLine.worldBound.y));
				float dragLineOffset = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.dragLine.resolvedStyle.left : this.m_SplitView.dragLine.resolvedStyle.top);
				bool flag2 = dragLineIsBeforeAnchor && Math.Abs(dragLineOffset) + 1f <= distanceBetweenDragLineAndAnchor;
				if (flag2)
				{
					this.InterruptPointerMove(e);
				}
				else
				{
					Vector2 diff = e.localPosition - this.m_Start;
					float mouseDiff = diff.x;
					bool flag3 = this.orientation == TwoPaneSplitViewOrientation.Vertical;
					if (flag3)
					{
						mouseDiff = diff.y;
					}
					this.m_Delta = (float)this.m_Direction * mouseDiff;
					this.ApplyDelta(this.m_Delta);
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00035268 File Offset: 0x00033468
		protected void OnPointerUp(PointerUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId) || !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000352C4 File Offset: 0x000334C4
		protected void InterruptPointerMove(PointerMoveEvent e)
		{
			bool flag = !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x0400070C RID: 1804
		private Vector3 m_Start;

		// Token: 0x0400070D RID: 1805
		protected bool m_Active;

		// Token: 0x0400070E RID: 1806
		private TwoPaneSplitView m_SplitView;

		// Token: 0x0400070F RID: 1807
		private int m_Direction;

		// Token: 0x04000710 RID: 1808
		private float m_Delta;
	}
}
