using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000575 RID: 1397
	[DefaultMember("Item")]
	internal struct LayoutNode : IEquatable<LayoutNode>
	{
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x00099119 File Offset: 0x00097319
		public float LayoutX
		{
			get
			{
				return this.Layout.Position.FixedElementField;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x0009912C File Offset: 0x0009732C
		public unsafe float LayoutY
		{
			get
			{
				return *((ref this.Layout.Position.FixedElementField) + 4);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00099141 File Offset: 0x00097341
		public unsafe float LayoutRight
		{
			get
			{
				return *((ref this.Layout.Position.FixedElementField) + (IntPtr)2 * 4);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00099159 File Offset: 0x00097359
		public unsafe float LayoutBottom
		{
			get
			{
				return *((ref this.Layout.Position.FixedElementField) + (IntPtr)3 * 4);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00099171 File Offset: 0x00097371
		public float LayoutWidth
		{
			get
			{
				return this.Layout.Dimensions.FixedElementField;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00099184 File Offset: 0x00097384
		public unsafe float LayoutHeight
		{
			get
			{
				return *((ref this.Layout.Dimensions.FixedElementField) + 4);
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x00099199 File Offset: 0x00097399
		public float LayoutMarginLeft
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Left);
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x000991AD File Offset: 0x000973AD
		public float LayoutMarginTop
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Top);
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000991C1 File Offset: 0x000973C1
		public float LayoutMarginRight
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Right);
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000991D5 File Offset: 0x000973D5
		public float LayoutMarginBottom
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Bottom);
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x000991E9 File Offset: 0x000973E9
		public float LayoutPaddingLeft
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Left);
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000991FD File Offset: 0x000973FD
		public float LayoutPaddingTop
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Top);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x00099211 File Offset: 0x00097411
		public float LayoutPaddingRight
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Right);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x00099225 File Offset: 0x00097425
		public float LayoutPaddingBottom
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Bottom);
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x00099239 File Offset: 0x00097439
		public float LayoutBorderLeft
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Left);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x0009924D File Offset: 0x0009744D
		public float LayoutBorderTop
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Top);
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x00099261 File Offset: 0x00097461
		public float LayoutBorderRight
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Right);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00099275 File Offset: 0x00097475
		public float LayoutBorderBottom
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Bottom);
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x00099289 File Offset: 0x00097489
		public float ComputedFlexBasis
		{
			get
			{
				return this.Layout.ComputedFlexBasis;
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00099298 File Offset: 0x00097498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe float GetLayoutValue(float* buffer, LayoutEdge edge)
		{
			if (!true)
			{
			}
			float num;
			if (edge != LayoutEdge.Left)
			{
				if (edge != LayoutEdge.Right)
				{
					num = buffer[(IntPtr)edge];
				}
				else
				{
					num = ((this.Layout.Direction == LayoutDirection.RTL) ? buffer[4] : buffer[5]);
				}
			}
			else
			{
				num = ((this.Layout.Direction == LayoutDirection.RTL) ? buffer[5] : buffer[4]);
			}
			if (!true)
			{
			}
			return num;
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x0009930A File Offset: 0x0009750A
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x0009932D File Offset: 0x0009752D
		public LayoutNode Parent
		{
			get
			{
				return new LayoutNode(this.m_Access, this.m_Access.GetNodeData(this.m_Handle).Parent);
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).Parent = value.m_Handle;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x0009934B File Offset: 0x0009754B
		private LayoutList<LayoutHandle> Children
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).Children;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x00099364 File Offset: 0x00097564
		public int Count
		{
			get
			{
				return this.Children.IsCreated ? this.Children.Count : 0;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00099394 File Offset: 0x00097594
		public void Insert(int index, LayoutNode child)
		{
			ref LayoutNodeData data = ref this.m_Access.GetNodeData(this.m_Handle);
			bool flag = !data.Children.IsCreated;
			if (flag)
			{
				data.Children = new LayoutList<LayoutHandle>(4, Allocator.Persistent);
			}
			data.Children.Insert(index, child.Handle);
			child.Parent = this;
			this.MarkDirty();
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000993FC File Offset: 0x000975FC
		public unsafe void RemoveAt(int index)
		{
			ref LayoutNodeData data = ref this.m_Access.GetNodeData(this.m_Handle);
			Assert.IsTrue(data.Children.IsCreated);
			bool flag = (ulong)index >= (ulong)((long)data.Children.Count);
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			LayoutHandle childHandle = *data.Children[index];
			ref LayoutNodeData childData = ref this.m_Access.GetNodeData(childHandle);
			bool isOwned = childData.Parent.Equals(this.m_Handle);
			childData.Parent = LayoutHandle.Undefined;
			data.Children.RemoveAt(index);
			bool flag2 = isOwned;
			if (flag2)
			{
				this.MarkDirty();
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000994A4 File Offset: 0x000976A4
		public void Clear()
		{
			ref LayoutNodeData data = ref this.m_Access.GetNodeData(this.m_Handle);
			bool flag = !data.Children.IsCreated;
			if (!flag)
			{
				while (data.Children.Count > 0)
				{
					this.RemoveAt(data.Children.Count - 1);
				}
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x00099500 File Offset: 0x00097700
		public LayoutFlexDirection FlexDirection
		{
			set
			{
				bool flag = this.Style.FlexDirection == value;
				if (!flag)
				{
					this.Style.FlexDirection = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009CA RID: 2506
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x00099538 File Offset: 0x00097738
		public LayoutJustify JustifyContent
		{
			set
			{
				bool flag = this.Style.JustifyContent == value;
				if (!flag)
				{
					this.Style.JustifyContent = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009CB RID: 2507
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x00099570 File Offset: 0x00097770
		public LayoutDisplay Display
		{
			set
			{
				bool flag = this.Style.Display == value;
				if (!flag)
				{
					this.Style.Display = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009CC RID: 2508
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x000995A8 File Offset: 0x000977A8
		public LayoutAlign AlignItems
		{
			set
			{
				bool flag = this.Style.AlignItems == value;
				if (!flag)
				{
					this.Style.AlignItems = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009CD RID: 2509
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x000995E0 File Offset: 0x000977E0
		public LayoutAlign AlignSelf
		{
			set
			{
				bool flag = this.Style.AlignSelf == value;
				if (!flag)
				{
					this.Style.AlignSelf = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009CE RID: 2510
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x00099618 File Offset: 0x00097818
		public LayoutAlign AlignContent
		{
			set
			{
				bool flag = this.Style.AlignContent == value;
				if (!flag)
				{
					this.Style.AlignContent = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009CF RID: 2511
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x00099650 File Offset: 0x00097850
		public LayoutPositionType PositionType
		{
			set
			{
				bool flag = this.Style.PositionType == value;
				if (!flag)
				{
					this.Style.PositionType = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x00099688 File Offset: 0x00097888
		public LayoutWrap Wrap
		{
			set
			{
				bool flag = this.Style.FlexWrap == value;
				if (!flag)
				{
					this.Style.FlexWrap = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x000996BD File Offset: 0x000978BD
		public float FlexGrow
		{
			set
			{
				this.SetValue(ref this.Style.FlexGrow, value);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x000996D2 File Offset: 0x000978D2
		public float FlexShrink
		{
			set
			{
				this.SetValue(ref this.Style.FlexShrink, value);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000996E7 File Offset: 0x000978E7
		public LayoutValue FlexBasis
		{
			set
			{
				this.SetStyleValueUnit(ref this.Style.FlexBasis, value);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x000996FC File Offset: 0x000978FC
		public LayoutValue Width
		{
			set
			{
				this.SetStyleValueUnit(this.Style.dimensions[0], value);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x00099717 File Offset: 0x00097917
		public LayoutValue Height
		{
			set
			{
				this.SetStyleValueUnit(this.Style.dimensions[1], value);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (set) Token: 0x06002679 RID: 9849 RVA: 0x00099732 File Offset: 0x00097932
		public LayoutValue MaxWidth
		{
			set
			{
				this.SetStyleValue(this.Style.maxDimensions[0], value);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x0009974D File Offset: 0x0009794D
		public LayoutValue MaxHeight
		{
			set
			{
				this.SetStyleValue(this.Style.maxDimensions[1], value);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x00099768 File Offset: 0x00097968
		public LayoutValue MinWidth
		{
			set
			{
				this.SetStyleValue(this.Style.minDimensions[0], value);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x00099783 File Offset: 0x00097983
		public LayoutValue MinHeight
		{
			set
			{
				this.SetStyleValue(this.Style.minDimensions[1], value);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x000997A0 File Offset: 0x000979A0
		public LayoutOverflow Overflow
		{
			set
			{
				bool flag = this.Style.Overflow == value;
				if (!flag)
				{
					this.Style.Overflow = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009DB RID: 2523
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000997D5 File Offset: 0x000979D5
		public LayoutValue Left
		{
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Left, value);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x000997E0 File Offset: 0x000979E0
		public LayoutValue Top
		{
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Top, value);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000997EB File Offset: 0x000979EB
		public LayoutValue Right
		{
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Right, value);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x000997F6 File Offset: 0x000979F6
		public LayoutValue Bottom
		{
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Bottom, value);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x00099801 File Offset: 0x00097A01
		public LayoutValue MarginLeft
		{
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Left, value);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x0009980C File Offset: 0x00097A0C
		public LayoutValue MarginTop
		{
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Top, value);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (set) Token: 0x06002684 RID: 9860 RVA: 0x00099817 File Offset: 0x00097A17
		public LayoutValue MarginRight
		{
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Right, value);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x00099822 File Offset: 0x00097A22
		public LayoutValue MarginBottom
		{
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Bottom, value);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x0009982D File Offset: 0x00097A2D
		public LayoutValue PaddingLeft
		{
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Left, value);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x00099838 File Offset: 0x00097A38
		public LayoutValue PaddingTop
		{
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Top, value);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x00099843 File Offset: 0x00097A43
		public LayoutValue PaddingRight
		{
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Right, value);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x0009984E File Offset: 0x00097A4E
		public LayoutValue PaddingBottom
		{
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Bottom, value);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (set) Token: 0x0600268A RID: 9866 RVA: 0x00099859 File Offset: 0x00097A59
		public float BorderLeftWidth
		{
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[0], value);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x00099874 File Offset: 0x00097A74
		public float BorderTopWidth
		{
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[1], value);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (set) Token: 0x0600268C RID: 9868 RVA: 0x0009988F File Offset: 0x00097A8F
		public float BorderRightWidth
		{
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[2], value);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x000998AA File Offset: 0x00097AAA
		public float BorderBottomWidth
		{
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[3], value);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000998C8 File Offset: 0x00097AC8
		private void SetValue(ref float currentValue, float newValue)
		{
			bool flag = currentValue.Equals(newValue);
			if (!flag)
			{
				currentValue = newValue;
				this.MarkDirty();
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000998F0 File Offset: 0x00097AF0
		private void SetStyleValue(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = newValue.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.SetStyleValuePercent(ref currentValue, newValue);
			}
			else
			{
				this.SetStyleValuePoint(ref currentValue, newValue);
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00099924 File Offset: 0x00097B24
		private void SetStyleValueUnit(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = newValue.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.SetStyleValuePercent(ref currentValue, newValue);
			}
			else
			{
				bool flag2 = newValue.Unit == LayoutUnit.Auto;
				if (flag2)
				{
					this.SetStyleValueAuto(ref currentValue);
				}
				else
				{
					this.SetStyleValuePoint(ref currentValue, newValue);
				}
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00099974 File Offset: 0x00097B74
		private void SetStyleValuePoint(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = float.IsNaN(currentValue.Value) && float.IsNaN(newValue.Value) && newValue.Unit == currentValue.Unit;
			if (!flag)
			{
				bool flag2 = currentValue.Value != newValue.Value || currentValue.Unit != LayoutUnit.Point;
				if (flag2)
				{
					bool flag3 = float.IsNaN(newValue.Value);
					if (flag3)
					{
						currentValue = LayoutValue.Auto();
					}
					else
					{
						currentValue = LayoutValue.Point(newValue.Value);
					}
					this.MarkDirty();
				}
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00099A10 File Offset: 0x00097C10
		private void SetStyleValuePercent(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = currentValue.Value != newValue.Value || currentValue.Unit != LayoutUnit.Percent;
			if (flag)
			{
				bool flag2 = float.IsNaN(newValue.Value);
				if (flag2)
				{
					currentValue = LayoutValue.Auto();
				}
				else
				{
					currentValue = newValue;
				}
				this.MarkDirty();
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00099A70 File Offset: 0x00097C70
		private void SetStyleValueAuto(ref LayoutValue currentValue)
		{
			bool flag = currentValue.Unit != LayoutUnit.Auto;
			if (flag)
			{
				currentValue = LayoutValue.Auto();
				this.MarkDirty();
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00099AA4 File Offset: 0x00097CA4
		private void SetStyleEdgePosition(LayoutEdge edge, LayoutValue value)
		{
			bool flag = value.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.StyleEdgeSetPercent(this.Style.position[(int)edge], value.Value);
			}
			else
			{
				this.StyleEdgeSetPoint(this.Style.position[(int)edge], value.Value);
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x00099B04 File Offset: 0x00097D04
		private void SetStyleEdgeMargin(LayoutEdge edge, LayoutValue value)
		{
			bool flag = value.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.StyleEdgeSetPercent(this.Style.margin[(int)edge], value.Value);
			}
			else
			{
				bool flag2 = value.Unit == LayoutUnit.Auto;
				if (flag2)
				{
					this.StyleEdgeSetAuto(this.Style.margin[(int)edge]);
				}
				else
				{
					this.StyleEdgeSetPoint(this.Style.margin[(int)edge], value.Value);
				}
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00099B90 File Offset: 0x00097D90
		private void SetStyleEdgePadding(LayoutEdge edge, LayoutValue value)
		{
			bool flag = value.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.StyleEdgeSetPercent(this.Style.padding[(int)edge], value.Value);
			}
			else
			{
				this.StyleEdgeSetPoint(this.Style.padding[(int)edge], value.Value);
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x00099BF0 File Offset: 0x00097DF0
		private void StyleEdgeSetPercent(ref LayoutValue value, float newValue)
		{
			bool flag = value.Value != newValue || value.Unit != LayoutUnit.Percent;
			if (flag)
			{
				value = (float.IsNaN(newValue) ? LayoutValue.Undefined() : LayoutValue.Percent(newValue));
				this.MarkDirty();
			}
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00099C40 File Offset: 0x00097E40
		private void StyleEdgeSetAuto(ref LayoutValue value)
		{
			bool flag = value.Unit != LayoutUnit.Auto;
			if (flag)
			{
				value = LayoutValue.Auto();
				this.MarkDirty();
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00099C74 File Offset: 0x00097E74
		private void StyleEdgeSetPoint(ref LayoutValue value, float newValue)
		{
			bool flag = float.IsNaN(value.Value) && float.IsNaN(newValue);
			if (!flag)
			{
				bool flag2 = value.Value != newValue || value.Unit != LayoutUnit.Point;
				if (flag2)
				{
					value = (float.IsNaN(newValue) ? LayoutValue.Undefined() : LayoutValue.Point(newValue));
					this.MarkDirty();
				}
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x00099CE0 File Offset: 0x00097EE0
		public static LayoutNode Undefined
		{
			get
			{
				return new LayoutNode(default(LayoutDataAccess), LayoutHandle.Undefined);
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x00099D00 File Offset: 0x00097F00
		internal LayoutNode(LayoutDataAccess access, LayoutHandle handle)
		{
			this.m_Access = access;
			this.m_Handle = handle;
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x00099D11 File Offset: 0x00097F11
		public bool IsUndefined
		{
			get
			{
				return this.m_Handle.Equals(LayoutHandle.Undefined);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x00099D23 File Offset: 0x00097F23
		public LayoutHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00099D2B File Offset: 0x00097F2B
		public ref LayoutComputedData Layout
		{
			get
			{
				return this.m_Access.GetComputedData(this.m_Handle);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x00099D3E File Offset: 0x00097F3E
		public ref LayoutStyleData Style
		{
			get
			{
				return this.m_Access.GetStyleData(this.m_Handle);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x00099D51 File Offset: 0x00097F51
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x00099D69 File Offset: 0x00097F69
		public bool IsDirty
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).IsDirty;
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).IsDirty = value;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x00099D83 File Offset: 0x00097F83
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x00099D9B File Offset: 0x00097F9B
		public bool HasNewLayout
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).HasNewLayout;
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).HasNewLayout = value;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x00099DB5 File Offset: 0x00097FB5
		public bool IsMeasureDefined
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).ManagedMeasureFunctionIndex != 0;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x00099DD0 File Offset: 0x00097FD0
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x00099DE3 File Offset: 0x00097FE3
		public LayoutMeasureFunction Measure
		{
			get
			{
				return this.m_Access.GetMeasureFunction(this.m_Handle);
			}
			set
			{
				this.m_Access.SetMeasureFunction(this.m_Handle, value);
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x00099DF8 File Offset: 0x00097FF8
		public void SetOwner(VisualElement func)
		{
			this.m_Access.SetOwner(this.m_Handle, func);
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00099E10 File Offset: 0x00098010
		public VisualElement GetOwner()
		{
			return this.m_Access.GetOwner(this.m_Handle);
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x00099E33 File Offset: 0x00098033
		public LayoutBaselineFunction Baseline
		{
			get
			{
				return this.m_Access.GetBaselineFunction(this.m_Handle);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (set) Token: 0x060026AA RID: 9898 RVA: 0x00099E46 File Offset: 0x00098046
		public LayoutConfig Config
		{
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).Config = value.Handle;
			}
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00099E68 File Offset: 0x00098068
		public void MarkDirty()
		{
			bool isDirty = this.IsDirty;
			if (!isDirty)
			{
				this.IsDirty = true;
				this.Layout.ComputedFlexBasis = float.NaN;
				bool flag = !this.Parent.IsUndefined;
				if (flag)
				{
					this.Parent.MarkDirty();
				}
			}
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00099EBE File Offset: 0x000980BE
		public void MarkLayoutSeen()
		{
			this.HasNewLayout = false;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x00099ECC File Offset: 0x000980CC
		public void CopyFromComputedStyle(ComputedStyle style)
		{
			this.FlexGrow = style.flexGrow;
			this.FlexShrink = style.flexShrink;
			this.FlexBasis = style.flexBasis.ToLayoutValue();
			this.Left = style.left.ToLayoutValue();
			this.Top = style.top.ToLayoutValue();
			this.Right = style.right.ToLayoutValue();
			this.Bottom = style.bottom.ToLayoutValue();
			this.MarginLeft = style.marginLeft.ToLayoutValue();
			this.MarginTop = style.marginTop.ToLayoutValue();
			this.MarginRight = style.marginRight.ToLayoutValue();
			this.MarginBottom = style.marginBottom.ToLayoutValue();
			this.PaddingLeft = style.paddingLeft.ToLayoutValue();
			this.PaddingTop = style.paddingTop.ToLayoutValue();
			this.PaddingRight = style.paddingRight.ToLayoutValue();
			this.PaddingBottom = style.paddingBottom.ToLayoutValue();
			this.BorderLeftWidth = style.borderLeftWidth;
			this.BorderTopWidth = style.borderTopWidth;
			this.BorderRightWidth = style.borderRightWidth;
			this.BorderBottomWidth = style.borderBottomWidth;
			this.Width = style.width.ToLayoutValue();
			this.Height = style.height.ToLayoutValue();
			this.PositionType = (LayoutPositionType)style.position;
			this.Overflow = (LayoutOverflow)style.overflow;
			this.AlignSelf = (LayoutAlign)style.alignSelf;
			this.MaxWidth = style.maxWidth.ToLayoutValue();
			this.MaxHeight = style.maxHeight.ToLayoutValue();
			this.MinWidth = style.minWidth.ToLayoutValue();
			this.MinHeight = style.minHeight.ToLayoutValue();
			this.FlexDirection = (LayoutFlexDirection)style.flexDirection;
			this.AlignContent = (LayoutAlign)style.alignContent;
			this.AlignItems = (LayoutAlign)style.alignItems;
			this.JustifyContent = (LayoutJustify)style.justifyContent;
			this.Wrap = (LayoutWrap)style.flexWrap;
			this.Display = (LayoutDisplay)style.display;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0009A118 File Offset: 0x00098318
		public void SoftReset()
		{
			ref LayoutNodeData data = ref this.m_Access.GetNodeData(this.m_Handle);
			data.HasNewLayout = true;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0009A140 File Offset: 0x00098340
		public bool Equals(LayoutNode other)
		{
			return this.m_Handle.Equals(other.m_Handle);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0009A164 File Offset: 0x00098364
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is LayoutNode)
			{
				LayoutNode other = (LayoutNode)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0009A190 File Offset: 0x00098390
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode();
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0009A1B3 File Offset: 0x000983B3
		public void CalculateLayout(float width = float.NaN, float height = float.NaN)
		{
			LayoutProcessor.CalculateLayout(this, width, height, this.Style.Direction);
		}

		// Token: 0x04001378 RID: 4984
		private const int k_DefaultChildCapacity = 4;

		// Token: 0x04001379 RID: 4985
		private readonly LayoutDataAccess m_Access;

		// Token: 0x0400137A RID: 4986
		private readonly LayoutHandle m_Handle;
	}
}
