using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000147 RID: 327
	public class Tab : VisualElement
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060009DA RID: 2522 RVA: 0x0003035C File Offset: 0x0002E55C
		// (remove) Token: 0x060009DB RID: 2523 RVA: 0x00030394 File Offset: 0x0002E594
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Tab> selected;

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000303C9 File Offset: 0x0002E5C9
		public VisualElement tabHeader
		{
			get
			{
				return this.m_TabHeader;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x000303D1 File Offset: 0x0002E5D1
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x000303DC File Offset: 0x0002E5DC
		[CreateProperty]
		public string label
		{
			get
			{
				return this.m_Label;
			}
			set
			{
				bool flag = string.CompareOrdinal(value, this.m_Label) == 0;
				if (!flag)
				{
					this.m_TabHeaderLabel.text = value;
					this.m_TabHeaderLabel.EnableInClassList(Tab.tabHeaderEmptyLabeUssClassName, string.IsNullOrEmpty(value));
					this.m_TabHeaderImage.EnableInClassList(Tab.tabHeaderStandaloneImageUssClassName, string.IsNullOrEmpty(value));
					this.m_Label = value;
					base.NotifyPropertyChanged(in Tab.labelProperty);
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0003044D File Offset: 0x0002E64D
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00030458 File Offset: 0x0002E658
		[CreateProperty]
		public Background iconImage
		{
			get
			{
				return this.m_IconImage;
			}
			set
			{
				bool flag = value == this.m_IconImage;
				if (!flag)
				{
					bool flag2 = value.IsEmpty();
					if (flag2)
					{
						this.m_TabHeaderImage.image = null;
						this.m_TabHeaderImage.sprite = null;
						this.m_TabHeaderImage.vectorImage = null;
						this.m_TabHeaderImage.AddToClassList(Tab.tabHeaderEmptyImageUssClassName);
						this.m_TabHeaderImage.RemoveFromClassList(Tab.tabHeaderStandaloneImageUssClassName);
						this.m_IconImage = value;
						base.NotifyPropertyChanged(in Tab.iconImageProperty);
					}
					else
					{
						bool flag3 = value.texture;
						if (flag3)
						{
							this.m_TabHeaderImage.image = value.texture;
						}
						else
						{
							bool flag4 = value.sprite;
							if (flag4)
							{
								this.m_TabHeaderImage.sprite = value.sprite;
							}
							else
							{
								bool flag5 = value.renderTexture;
								if (flag5)
								{
									this.m_TabHeaderImage.image = value.renderTexture;
								}
								else
								{
									this.m_TabHeaderImage.vectorImage = value.vectorImage;
								}
							}
						}
						this.m_TabHeaderImage.RemoveFromClassList(Tab.tabHeaderEmptyImageUssClassName);
						this.m_TabHeaderImage.EnableInClassList(Tab.tabHeaderStandaloneImageUssClassName, string.IsNullOrEmpty(this.m_Label));
						this.m_IconImage = value;
						base.NotifyPropertyChanged(in Tab.iconImageProperty);
					}
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x000305AC File Offset: 0x0002E7AC
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x000305B4 File Offset: 0x0002E7B4
		[CreateProperty]
		public bool closeable
		{
			get
			{
				return this.m_Closeable;
			}
			set
			{
				bool flag = this.m_Closeable == value;
				if (!flag)
				{
					this.m_Closeable = value;
					this.m_TabHeader.EnableInClassList(Tab.closeableUssClassName, value);
					this.EnableTabCloseButton(value);
					base.NotifyPropertyChanged(in Tab.closeableProperty);
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000305FE File Offset: 0x0002E7FE
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00030606 File Offset: 0x0002E806
		public Tab()
			: this(null, null)
		{
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00030618 File Offset: 0x0002E818
		public Tab(string label, Background iconImage)
		{
			base.AddToClassList(Tab.ussClassName);
			this.m_TabHeader = new VisualElement
			{
				classList = { Tab.tabHeaderUssClassName },
				name = Tab.tabHeaderUssClassName
			};
			this.m_DragHandle = new VisualElement
			{
				name = Tab.reorderableItemHandleUssClassName,
				classList = { Tab.reorderableItemHandleUssClassName }
			};
			this.m_DragHandle.AddToClassList(Tab.reorderableItemHandleUssClassName);
			this.m_DragHandle.Add(new VisualElement
			{
				name = Tab.reorderableItemHandleBarUssClassName,
				classList = 
				{
					Tab.reorderableItemHandleBarUssClassName,
					Tab.reorderableItemHandleBarUssClassName + "--left"
				}
			});
			this.m_DragHandle.Add(new VisualElement
			{
				name = Tab.reorderableItemHandleBarUssClassName,
				classList = { Tab.reorderableItemHandleBarUssClassName }
			});
			this.m_TabHeaderImage = new Image
			{
				name = Tab.tabHeaderImageUssClassName,
				classList = 
				{
					Tab.tabHeaderImageUssClassName,
					Tab.tabHeaderEmptyImageUssClassName
				}
			};
			this.m_TabHeader.Add(this.m_TabHeaderImage);
			this.m_TabHeaderLabel = new Label
			{
				name = Tab.tabHeaderLabelUssClassName,
				classList = { Tab.tabHeaderLabelUssClassName }
			};
			this.m_TabHeader.Add(this.m_TabHeaderLabel);
			this.m_TabHeader.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnTabClicked), TrickleDown.NoTrickleDown);
			this.m_TabHeader.Add(new VisualElement
			{
				name = Tab.tabHeaderUnderlineUssClassName,
				classList = { Tab.tabHeaderUnderlineUssClassName }
			});
			this.m_CloseButton = new VisualElement
			{
				name = Tab.closeButtonUssClassName,
				classList = { Tab.closeButtonUssClassName }
			};
			this.m_CloseButton.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnCloseButtonClicked), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_TabHeader);
			this.m_ContentContainer = new VisualElement
			{
				name = Tab.contentUssClassName,
				classList = { Tab.contentUssClassName },
				userData = this.m_TabHeader
			};
			base.hierarchy.Add(this.m_ContentContainer);
			this.label = label;
			this.iconImage = iconImage;
			this.m_DragHandle.AddManipulator(this.<dragger>k__BackingField = new TabDragger());
			this.m_TabHeader.RegisterCallback<TooltipEvent>(new EventCallback<TooltipEvent>(this.UpdateTooltip), TrickleDown.NoTrickleDown);
			base.RegisterCallback<TooltipEvent>(delegate(TooltipEvent evt)
			{
				evt.StopImmediatePropagation();
			}, TrickleDown.NoTrickleDown);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000308E8 File Offset: 0x0002EAE8
		private void UpdateTooltip(TooltipEvent evt)
		{
			VisualElement element = evt.currentTarget as VisualElement;
			bool flag = element != null && !string.IsNullOrEmpty(base.tooltip);
			if (flag)
			{
				evt.rect = element.GetTooltipRect();
				evt.tooltip = base.tooltip;
				evt.StopImmediatePropagation();
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0003093D File Offset: 0x0002EB3D
		private void AddDragHandles()
		{
			this.m_TabHeader.Insert(0, this.m_DragHandle);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00030954 File Offset: 0x0002EB54
		private void RemoveDragHandles()
		{
			bool flag = this.m_TabHeader.Contains(this.m_DragHandle);
			if (flag)
			{
				this.m_TabHeader.Remove(this.m_DragHandle);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0003098C File Offset: 0x0002EB8C
		internal void EnableTabDragHandles(bool enable)
		{
			if (enable)
			{
				this.AddDragHandles();
			}
			else
			{
				this.RemoveDragHandles();
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000309AF File Offset: 0x0002EBAF
		private void AddCloseButton()
		{
			this.m_TabHeader.Add(this.m_CloseButton);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000309C4 File Offset: 0x0002EBC4
		private void RemoveCloseButton()
		{
			bool flag = this.m_TabHeader.Contains(this.m_CloseButton);
			if (flag)
			{
				this.m_TabHeader.Remove(this.m_CloseButton);
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000309FC File Offset: 0x0002EBFC
		internal void EnableTabCloseButton(bool enable)
		{
			if (enable)
			{
				this.AddCloseButton();
			}
			else
			{
				this.RemoveCloseButton();
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00030A1F File Offset: 0x0002EC1F
		internal void SetActive()
		{
			this.m_TabHeader.pseudoStates |= PseudoStates.Checked;
			base.pseudoStates |= PseudoStates.Checked;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00030A45 File Offset: 0x0002EC45
		internal void SetInactive()
		{
			this.m_TabHeader.pseudoStates &= ~PseudoStates.Checked;
			base.pseudoStates &= ~PseudoStates.Checked;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00030A6D File Offset: 0x0002EC6D
		private void OnTabClicked(PointerDownEvent _)
		{
			Action<Tab> action = this.selected;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00030A84 File Offset: 0x0002EC84
		private void OnCloseButtonClicked(PointerDownEvent evt)
		{
			Func<bool> func = this.closing;
			bool canClose = func == null || func();
			bool flag = canClose;
			if (flag)
			{
				base.RemoveFromHierarchy();
				Action<Tab> action = this.closed;
				if (action != null)
				{
					action(this);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x0400065F RID: 1631
		internal static readonly BindingId labelProperty = "label";

		// Token: 0x04000660 RID: 1632
		internal static readonly BindingId iconImageProperty = "iconImage";

		// Token: 0x04000661 RID: 1633
		internal static readonly BindingId closeableProperty = "closeable";

		// Token: 0x04000662 RID: 1634
		public static readonly string ussClassName = "unity-tab";

		// Token: 0x04000663 RID: 1635
		public static readonly string tabHeaderUssClassName = Tab.ussClassName + "__header";

		// Token: 0x04000664 RID: 1636
		public static readonly string tabHeaderImageUssClassName = Tab.tabHeaderUssClassName + "-image";

		// Token: 0x04000665 RID: 1637
		public static readonly string tabHeaderEmptyImageUssClassName = Tab.tabHeaderImageUssClassName + "--empty";

		// Token: 0x04000666 RID: 1638
		public static readonly string tabHeaderStandaloneImageUssClassName = Tab.tabHeaderImageUssClassName + "--standalone";

		// Token: 0x04000667 RID: 1639
		public static readonly string tabHeaderLabelUssClassName = Tab.tabHeaderUssClassName + "-label";

		// Token: 0x04000668 RID: 1640
		public static readonly string tabHeaderEmptyLabeUssClassName = Tab.tabHeaderLabelUssClassName + "--empty";

		// Token: 0x04000669 RID: 1641
		public static readonly string tabHeaderUnderlineUssClassName = Tab.tabHeaderUssClassName + "-underline";

		// Token: 0x0400066A RID: 1642
		public static readonly string contentUssClassName = Tab.ussClassName + "__content-container";

		// Token: 0x0400066B RID: 1643
		public static readonly string draggingUssClassName = Tab.ussClassName + "--dragging";

		// Token: 0x0400066C RID: 1644
		public static readonly string reorderableUssClassName = Tab.ussClassName + "__reorderable";

		// Token: 0x0400066D RID: 1645
		public static readonly string reorderableItemHandleUssClassName = Tab.reorderableUssClassName + "-handle";

		// Token: 0x0400066E RID: 1646
		public static readonly string reorderableItemHandleBarUssClassName = Tab.reorderableItemHandleUssClassName + "-bar";

		// Token: 0x0400066F RID: 1647
		public static readonly string closeableUssClassName = Tab.tabHeaderUssClassName + "__closeable";

		// Token: 0x04000670 RID: 1648
		public static readonly string closeButtonUssClassName = Tab.ussClassName + "__close-button";

		// Token: 0x04000672 RID: 1650
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<bool> closing;

		// Token: 0x04000673 RID: 1651
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<Tab> closed;

		// Token: 0x04000674 RID: 1652
		private string m_Label;

		// Token: 0x04000675 RID: 1653
		private Background m_IconImage;

		// Token: 0x04000676 RID: 1654
		private bool m_Closeable;

		// Token: 0x04000677 RID: 1655
		private VisualElement m_ContentContainer;

		// Token: 0x04000678 RID: 1656
		private VisualElement m_DragHandle;

		// Token: 0x04000679 RID: 1657
		private VisualElement m_CloseButton;

		// Token: 0x0400067A RID: 1658
		private VisualElement m_TabHeader;

		// Token: 0x0400067B RID: 1659
		private Image m_TabHeaderImage;

		// Token: 0x0400067C RID: 1660
		private Label m_TabHeaderLabel;

		// Token: 0x02000148 RID: 328
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Tab, Tab.UxmlTraits>
		{
		}

		// Token: 0x02000149 RID: 329
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x060009F3 RID: 2547 RVA: 0x00030C38 File Offset: 0x0002EE38
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Tab tab = (Tab)ve;
				tab.label = this.m_Label.GetValueFromBag(bag, cc);
				tab.iconImage = this.m_IconImage.GetValueFromBag(bag, cc);
				tab.closeable = this.m_Closeable.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400067E RID: 1662
			private readonly UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};

			// Token: 0x0400067F RID: 1663
			private readonly UxmlImageAttributeDescription m_IconImage = new UxmlImageAttributeDescription
			{
				name = "icon-image"
			};

			// Token: 0x04000680 RID: 1664
			private readonly UxmlBoolAttributeDescription m_Closeable = new UxmlBoolAttributeDescription
			{
				name = "closeable",
				defaultValue = false
			};
		}
	}
}
