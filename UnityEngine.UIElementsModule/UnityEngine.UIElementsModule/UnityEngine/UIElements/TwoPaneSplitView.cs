using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000164 RID: 356
	public class TwoPaneSplitView : VisualElement
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000339B0 File Offset: 0x00031BB0
		public VisualElement fixedPane
		{
			get
			{
				return this.m_FixedPane;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000339B8 File Offset: 0x00031BB8
		public VisualElement flexedPane
		{
			get
			{
				return this.m_FlexedPane;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000339C0 File Offset: 0x00031BC0
		internal VisualElement dragLine
		{
			get
			{
				return this.m_DragLine;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000339C8 File Offset: 0x00031BC8
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x000339D0 File Offset: 0x00031BD0
		[CreateProperty]
		public int fixedPaneIndex
		{
			get
			{
				return this.m_FixedPaneIndex;
			}
			set
			{
				bool flag = value == this.m_FixedPaneIndex;
				if (!flag)
				{
					this.Init(value, this.m_FixedPaneInitialDimension, this.m_Orientation);
					base.NotifyPropertyChanged(in TwoPaneSplitView.fixedPaneIndexProperty);
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00033A0D File Offset: 0x00031C0D
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00033A18 File Offset: 0x00031C18
		[CreateProperty]
		public float fixedPaneInitialDimension
		{
			get
			{
				return this.m_FixedPaneInitialDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneInitialDimension;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, value, this.m_Orientation);
					base.NotifyPropertyChanged(in TwoPaneSplitView.fixedPaneInitialDimensionProperty);
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00033A55 File Offset: 0x00031C55
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00033A60 File Offset: 0x00031C60
		[CreateProperty]
		public TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_Orientation;
			}
			set
			{
				bool flag = value == this.m_Orientation;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, value);
					base.NotifyPropertyChanged(in TwoPaneSplitView.orientationProperty);
				}
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00033A9D File Offset: 0x00031C9D
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00033ABC File Offset: 0x00031CBC
		internal float fixedPaneDimension
		{
			get
			{
				return string.IsNullOrEmpty(base.viewDataKey) ? this.m_FixedPaneInitialDimension : this.m_FixedPaneDimension;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set
			{
				bool flag = value == this.m_FixedPaneDimension;
				if (!flag)
				{
					this.m_FixedPaneDimension = value;
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00033AE8 File Offset: 0x00031CE8
		public TwoPaneSplitView()
		{
			this.SetupSplitView();
			this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, this.m_Orientation);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00033B3C File Offset: 0x00031D3C
		private void SetupSplitView()
		{
			base.AddToClassList(TwoPaneSplitView.s_UssClassName);
			this.m_Content = new VisualElement();
			this.m_Content.name = "unity-content-container";
			this.m_Content.AddToClassList(TwoPaneSplitView.s_ContentContainerClassName);
			base.hierarchy.Add(this.m_Content);
			this.m_DragLineAnchor = new VisualElement();
			this.m_DragLineAnchor.name = "unity-dragline-anchor";
			this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorClassName);
			base.hierarchy.Add(this.m_DragLineAnchor);
			this.m_DragLine = new VisualElement();
			this.m_DragLine.name = "unity-dragline";
			this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineClassName);
			this.m_DragLineAnchor.Add(this.m_DragLine);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00033C1C File Offset: 0x00031E1C
		public void CollapseChild(int index)
		{
			bool flag = index != 0 && index != 1;
			if (flag)
			{
				Debug.LogError("Invalid index. Must be 0 or 1.");
			}
			else
			{
				bool flag2 = this.m_LeftPane == null;
				if (flag2)
				{
					this.m_PendingCollapseToExecute = true;
					this.m_CollapsedChildIndex = index;
				}
				else
				{
					this.m_DragLine.style.display = DisplayStyle.None;
					this.m_DragLineAnchor.style.display = DisplayStyle.None;
					bool flag3 = index == 0;
					if (flag3)
					{
						this.m_RightPane.style.width = StyleKeyword.Initial;
						this.m_RightPane.style.height = StyleKeyword.Initial;
						this.m_RightPane.style.flexGrow = 1f;
						this.m_LeftPane.style.display = DisplayStyle.None;
					}
					else
					{
						this.m_LeftPane.style.width = StyleKeyword.Initial;
						this.m_LeftPane.style.height = StyleKeyword.Initial;
						this.m_LeftPane.style.flexGrow = 1f;
						this.m_RightPane.style.display = DisplayStyle.None;
					}
					this.m_CollapseMode = true;
				}
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00033D70 File Offset: 0x00031F70
		internal virtual void Init(int fixedPaneIndex, float fixedPaneInitialDimension, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_FixedPaneIndex = fixedPaneIndex;
			this.m_FixedPaneInitialDimension = fixedPaneInitialDimension;
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_HorizontalClassName);
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_VerticalClassName);
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_HorizontalClassName);
			}
			else
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_VerticalClassName);
			}
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag2)
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			}
			else
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			}
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			}
			else
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			}
			bool flag4 = this.m_Resizer != null;
			if (flag4)
			{
				this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				this.m_Resizer = null;
			}
			bool flag5 = this.m_Content.childCount != 2;
			if (flag5)
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
			else
			{
				this.PostDisplaySetup();
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00033EE8 File Offset: 0x000320E8
		private void OnPostDisplaySetup(GeometryChangedEvent evt)
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				bool postSetupWithEmptyLeftPane = this.m_LeftPane == null;
				this.PostDisplaySetup();
				bool flag2 = postSetupWithEmptyLeftPane && this.m_PendingCollapseToExecute;
				if (flag2)
				{
					this.CollapseChild(this.m_CollapsedChildIndex);
					this.m_PendingCollapseToExecute = false;
				}
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
				this.ReplacePanesBasedOnAnchor();
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00033F6C File Offset: 0x0003216C
		private void ReplacePanesBasedOnAnchor()
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_RightPane.style.left = this.m_DragLineAnchor.worldBound.width;
			}
			else
			{
				this.m_RightPane.style.top = this.m_DragLineAnchor.worldBound.height;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00033FDC File Offset: 0x000321DC
		private void IdentifyLeftAndRightPane()
		{
			this.m_LeftPane = this.m_Content[0];
			bool flag = this.m_FixedPaneIndex == 0;
			if (flag)
			{
				this.m_FixedPane = this.m_LeftPane;
			}
			else
			{
				this.m_FlexedPane = this.m_LeftPane;
			}
			this.m_RightPane = this.m_Content[1];
			bool flag2 = this.m_FixedPaneIndex == 1;
			if (flag2)
			{
				this.m_FixedPane = this.m_RightPane;
			}
			else
			{
				this.m_FlexedPane = this.m_RightPane;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003405C File Offset: 0x0003225C
		private void PostDisplaySetup()
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				bool flag2 = this.fixedPaneDimension < 0f;
				if (flag2)
				{
					this.fixedPaneDimension = this.m_FixedPaneInitialDimension;
				}
				float dimension = this.fixedPaneDimension;
				this.IdentifyLeftAndRightPane();
				this.m_FixedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FixedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FlexedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.width = StyleKeyword.Null;
				this.m_FixedPane.style.height = StyleKeyword.Null;
				this.m_FlexedPane.style.width = StyleKeyword.Null;
				this.m_FlexedPane.style.height = StyleKeyword.Null;
				bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag3)
				{
					this.m_FixedPane.style.width = dimension;
					this.m_FixedPane.style.height = StyleKeyword.Null;
				}
				else
				{
					this.m_FixedPane.style.width = StyleKeyword.Null;
					this.m_FixedPane.style.height = dimension;
				}
				this.m_FixedPane.style.flexShrink = 0f;
				this.m_FixedPane.style.flexGrow = 0f;
				this.m_FlexedPane.style.flexGrow = 1f;
				this.m_FlexedPane.style.flexShrink = 0f;
				this.m_FlexedPane.style.flexBasis = 0f;
				this.m_DragLineAnchor.style.left = 0f;
				this.m_DragLineAnchor.style.top = 0f;
				bool flag4 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag4)
				{
					float fixedPaneMargins = this.m_FixedPane.resolvedStyle.marginLeft + this.m_FixedPane.resolvedStyle.marginRight;
					bool flag5 = this.m_FixedPaneIndex == 0;
					if (flag5)
					{
						this.m_DragLineAnchor.style.left = fixedPaneMargins + this.m_FixedPaneInitialDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.left = base.resolvedStyle.width - fixedPaneMargins - this.m_FixedPaneInitialDimension - this.m_DragLineAnchor.resolvedStyle.width;
					}
				}
				else
				{
					float fixedPaneMargins2 = this.m_FixedPane.resolvedStyle.marginTop + this.m_FixedPane.resolvedStyle.marginBottom;
					bool flag6 = this.m_FixedPaneIndex == 0;
					if (flag6)
					{
						this.m_DragLineAnchor.style.top = fixedPaneMargins2 + this.m_FixedPaneInitialDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.top = base.resolvedStyle.height - fixedPaneMargins2 - this.m_FixedPaneInitialDimension - this.m_DragLineAnchor.resolvedStyle.height;
					}
				}
				bool flag7 = this.m_FixedPaneIndex == 0;
				int direction;
				if (flag7)
				{
					direction = 1;
				}
				else
				{
					direction = -1;
				}
				bool flag8 = this.m_Resizer != null;
				if (flag8)
				{
					this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				}
				this.m_Resizer = new TwoPaneSplitViewResizer(this, direction);
				this.m_DragLineAnchor.AddManipulator(this.m_Resizer);
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChange), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00034474 File Offset: 0x00032674
		private void OnSizeChange(GeometryChangedEvent evt)
		{
			this.UpdateLayout(true, true);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00034480 File Offset: 0x00032680
		private void UpdateLayout(bool updateFixedPane, bool updateDragLine)
		{
			bool collapseMode = this.m_CollapseMode;
			if (!collapseMode)
			{
				bool flag = base.resolvedStyle.display == DisplayStyle.None || base.resolvedStyle.visibility == Visibility.Hidden;
				if (!flag)
				{
					float maxLength = base.resolvedStyle.width;
					float fixedPaneLength = this.m_FixedPane.resolvedStyle.width;
					float fixedPaneMargins = this.m_FixedPane.resolvedStyle.marginLeft + this.m_FixedPane.resolvedStyle.marginRight;
					float fixedPaneMinLength = this.m_FixedPane.resolvedStyle.minWidth.value;
					float flexedPaneMargins = this.m_FlexedPane.resolvedStyle.marginLeft + this.m_FlexedPane.resolvedStyle.marginRight;
					float flexedPaneMinLength = this.m_FlexedPane.resolvedStyle.minWidth.value;
					bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
					if (flag2)
					{
						maxLength = base.resolvedStyle.height;
						fixedPaneLength = this.m_FixedPane.resolvedStyle.height;
						fixedPaneMargins = this.m_FixedPane.resolvedStyle.marginTop + this.m_FixedPane.resolvedStyle.marginBottom;
						fixedPaneMinLength = this.m_FixedPane.resolvedStyle.minHeight.value;
						flexedPaneMargins = this.m_FlexedPane.resolvedStyle.marginTop + this.m_FlexedPane.resolvedStyle.marginBottom;
						flexedPaneMinLength = this.m_FlexedPane.resolvedStyle.minHeight.value;
					}
					bool flag3 = maxLength >= fixedPaneLength + fixedPaneMargins + flexedPaneMinLength + flexedPaneMargins;
					if (flag3)
					{
						if (updateDragLine)
						{
							this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? (fixedPaneLength + fixedPaneMargins) : (maxLength - fixedPaneLength - fixedPaneMargins));
						}
					}
					else
					{
						bool flag4 = maxLength >= fixedPaneMinLength + fixedPaneMargins + flexedPaneMinLength + flexedPaneMargins;
						if (flag4)
						{
							float newDimension = maxLength - flexedPaneMinLength - flexedPaneMargins - fixedPaneMargins;
							float dimensionToAnchorOffset = ((this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? Math.Abs(this.m_DragLineAnchor.worldBound.width - (this.m_DragLine.resolvedStyle.width - Math.Abs(this.m_DragLine.resolvedStyle.left))) : Math.Abs(this.m_DragLineAnchor.worldBound.height - (this.m_DragLine.resolvedStyle.height - Math.Abs(this.m_DragLine.resolvedStyle.top))));
							newDimension -= dimensionToAnchorOffset;
							bool fixedPaneMinLengthReached = newDimension < fixedPaneMinLength;
							bool currentFixedPaneLenghtGreaterThanMin = fixedPaneLength > fixedPaneMinLength;
							bool flag5 = updateFixedPane && !fixedPaneMinLengthReached;
							if (flag5)
							{
								this.SetFixedPaneDimension(newDimension);
							}
							else
							{
								bool flag6 = updateFixedPane && fixedPaneMinLengthReached && currentFixedPaneLenghtGreaterThanMin;
								if (flag6)
								{
									this.SetFixedPaneDimension(fixedPaneMinLength);
								}
							}
							if (updateDragLine)
							{
								bool flag7 = fixedPaneMinLengthReached;
								if (flag7)
								{
									this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? fixedPaneMinLength : (maxLength - fixedPaneMinLength - fixedPaneMargins));
								}
								else
								{
									this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? (newDimension + fixedPaneMargins + dimensionToAnchorOffset) : (flexedPaneMinLength + flexedPaneMargins));
								}
							}
						}
						else
						{
							if (updateFixedPane)
							{
								this.SetFixedPaneDimension(fixedPaneMinLength);
							}
							if (updateDragLine)
							{
								this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? (fixedPaneMinLength + fixedPaneMargins) : (flexedPaneMinLength + flexedPaneMargins));
							}
						}
					}
				}
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000347C0 File Offset: 0x000329C0
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Content;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000347D8 File Offset: 0x000329D8
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string key = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, key);
			this.PostDisplaySetup();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00034804 File Offset: 0x00032A04
		private void SetDragLineOffset(float offset)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_DragLineAnchor.style.left = offset;
			}
			else
			{
				this.m_DragLineAnchor.style.top = offset;
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00034850 File Offset: 0x00032A50
		private void SetFixedPaneDimension(float dimension)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_FixedPane.style.width = dimension;
			}
			else
			{
				this.m_FixedPane.style.height = dimension;
			}
		}

		// Token: 0x040006EA RID: 1770
		internal static readonly BindingId fixedPaneIndexProperty = "fixedPaneIndex";

		// Token: 0x040006EB RID: 1771
		internal static readonly BindingId fixedPaneInitialDimensionProperty = "fixedPaneInitialDimension";

		// Token: 0x040006EC RID: 1772
		internal static readonly BindingId orientationProperty = "orientation";

		// Token: 0x040006ED RID: 1773
		private static readonly string s_UssClassName = "unity-two-pane-split-view";

		// Token: 0x040006EE RID: 1774
		private static readonly string s_ContentContainerClassName = "unity-two-pane-split-view__content-container";

		// Token: 0x040006EF RID: 1775
		private static readonly string s_HandleDragLineClassName = "unity-two-pane-split-view__dragline";

		// Token: 0x040006F0 RID: 1776
		private static readonly string s_HandleDragLineVerticalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--vertical";

		// Token: 0x040006F1 RID: 1777
		private static readonly string s_HandleDragLineHorizontalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--horizontal";

		// Token: 0x040006F2 RID: 1778
		private static readonly string s_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";

		// Token: 0x040006F3 RID: 1779
		private static readonly string s_HandleDragLineAnchorVerticalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--vertical";

		// Token: 0x040006F4 RID: 1780
		private static readonly string s_HandleDragLineAnchorHorizontalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--horizontal";

		// Token: 0x040006F5 RID: 1781
		private static readonly string s_VerticalClassName = "unity-two-pane-split-view--vertical";

		// Token: 0x040006F6 RID: 1782
		private static readonly string s_HorizontalClassName = "unity-two-pane-split-view--horizontal";

		// Token: 0x040006F7 RID: 1783
		private VisualElement m_LeftPane;

		// Token: 0x040006F8 RID: 1784
		private VisualElement m_RightPane;

		// Token: 0x040006F9 RID: 1785
		private VisualElement m_FixedPane;

		// Token: 0x040006FA RID: 1786
		private VisualElement m_FlexedPane;

		// Token: 0x040006FB RID: 1787
		[SerializeField]
		[DontCreateProperty]
		private float m_FixedPaneDimension = -1f;

		// Token: 0x040006FC RID: 1788
		private VisualElement m_DragLine;

		// Token: 0x040006FD RID: 1789
		private VisualElement m_DragLineAnchor;

		// Token: 0x040006FE RID: 1790
		private bool m_CollapseMode;

		// Token: 0x040006FF RID: 1791
		private bool m_PendingCollapseToExecute;

		// Token: 0x04000700 RID: 1792
		private int m_CollapsedChildIndex = -1;

		// Token: 0x04000701 RID: 1793
		private VisualElement m_Content;

		// Token: 0x04000702 RID: 1794
		private TwoPaneSplitViewOrientation m_Orientation;

		// Token: 0x04000703 RID: 1795
		private int m_FixedPaneIndex;

		// Token: 0x04000704 RID: 1796
		private float m_FixedPaneInitialDimension = 100f;

		// Token: 0x04000705 RID: 1797
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal TwoPaneSplitViewResizer m_Resizer;

		// Token: 0x02000165 RID: 357
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TwoPaneSplitView, TwoPaneSplitView.UxmlTraits>
		{
		}

		// Token: 0x02000166 RID: 358
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000ABA RID: 2746 RVA: 0x0003496C File Offset: 0x00032B6C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int fixedPaneIndex = this.m_FixedPaneIndex.GetValueFromBag(bag, cc);
				int fixedPaneInitialSize = this.m_FixedPaneInitialDimension.GetValueFromBag(bag, cc);
				TwoPaneSplitViewOrientation orientation = this.m_Orientation.GetValueFromBag(bag, cc);
				((TwoPaneSplitView)ve).Init(fixedPaneIndex, (float)fixedPaneInitialSize, orientation);
			}

			// Token: 0x04000706 RID: 1798
			private UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-index",
				defaultValue = 0
			};

			// Token: 0x04000707 RID: 1799
			private UxmlIntAttributeDescription m_FixedPaneInitialDimension = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-initial-dimension",
				defaultValue = 100
			};

			// Token: 0x04000708 RID: 1800
			private UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation> m_Orientation = new UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation>
			{
				name = "orientation",
				defaultValue = TwoPaneSplitViewOrientation.Horizontal
			};
		}
	}
}
