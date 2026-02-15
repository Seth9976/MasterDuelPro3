using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005F4 RID: 1524
	internal class MultiColumnHeaderColumn : VisualElement
	{
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000AB248 File Offset: 0x000A9448
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x000AB250 File Offset: 0x000A9450
		public Clickable clickable { get; private set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x000AB259 File Offset: 0x000A9459
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x000AB261 File Offset: 0x000A9461
		public ColumnMover mover { get; private set; }

		// Token: 0x17000AB9 RID: 2745
		// (set) Token: 0x0600296B RID: 10603 RVA: 0x000AB26A File Offset: 0x000A946A
		public string sortOrderLabel
		{
			set
			{
				this.m_SortIndicatorContainer.sortOrderLabel = value;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000AB279 File Offset: 0x000A9479
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x000AB281 File Offset: 0x000A9481
		public Column column { get; private set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000AB28A File Offset: 0x000A948A
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x000AB294 File Offset: 0x000A9494
		public VisualElement content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				bool flag = this.m_Content != null;
				if (flag)
				{
					bool flag2 = this.m_Content.parent == this.m_ContentContainer;
					if (flag2)
					{
						this.m_Content.RemoveFromHierarchy();
					}
					this.DestroyHeaderContent();
					this.m_Content = null;
				}
				this.m_Content = value;
				bool flag3 = this.m_Content != null;
				if (flag3)
				{
					this.m_Content.AddToClassList(MultiColumnHeaderColumn.contentUssClassName);
					this.m_ContentContainer.Add(this.m_Content);
				}
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000AB31D File Offset: 0x000A951D
		// (set) Token: 0x06002971 RID: 10609 RVA: 0x000AB344 File Offset: 0x000A9544
		private bool isContentBound
		{
			get
			{
				return this.m_Content != null && (bool)this.m_Content.GetProperty(MultiColumnHeaderColumn.s_BoundVEPropertyName);
			}
			set
			{
				VisualElement content = this.m_Content;
				if (content != null)
				{
					content.SetProperty(MultiColumnHeaderColumn.s_BoundVEPropertyName, value);
				}
			}
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000AB368 File Offset: 0x000A9568
		public MultiColumnHeaderColumn(Column column)
		{
			this.column = column;
			this.column.changed += this.OnColumnChanged;
			this.column.resized += this.OnColumnResized;
			base.AddToClassList(MultiColumnHeaderColumn.ussClassName);
			base.style.marginLeft = 0f;
			base.style.marginTop = 0f;
			base.style.marginRight = 0f;
			base.style.marginBottom = 0f;
			base.style.paddingLeft = 0f;
			base.style.paddingTop = 0f;
			base.style.paddingRight = 0f;
			base.style.paddingBottom = 0f;
			base.Add(this.m_SortIndicatorContainer = new MultiColumnHeaderColumnSortIndicator());
			this.m_ContentContainer = new VisualElement();
			this.m_ContentContainer.style.flexGrow = 1f;
			this.m_ContentContainer.style.flexShrink = 1f;
			this.m_ContentContainer.AddToClassList(MultiColumnHeaderColumn.contentContainerUssClassName);
			base.Add(this.m_ContentContainer);
			this.UpdateHeaderTemplate();
			this.UpdateGeometryFromColumn();
			this.InitManipulators();
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000AB4FC File Offset: 0x000A96FC
		private void OnColumnChanged(Column c, ColumnDataType role)
		{
			bool flag = this.column != c;
			if (!flag)
			{
				bool flag2 = role == ColumnDataType.HeaderTemplate;
				if (flag2)
				{
					IVisualElementScheduledItem scheduledHeaderTemplateUpdate = this.m_ScheduledHeaderTemplateUpdate;
					if (scheduledHeaderTemplateUpdate != null)
					{
						scheduledHeaderTemplateUpdate.Pause();
					}
					this.m_ScheduledHeaderTemplateUpdate = base.schedule.Execute(new Action(this.UpdateHeaderTemplate));
				}
				else
				{
					this.UpdateDataFromColumn();
				}
			}
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000AB561 File Offset: 0x000A9761
		private void OnColumnResized(Column c)
		{
			this.UpdateGeometryFromColumn();
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000AB56C File Offset: 0x000A976C
		private void InitManipulators()
		{
			this.AddManipulator(this.mover = new ColumnMover());
			this.mover.movingChanged += this.OnMoverChanged;
			this.AddManipulator(this.clickable = new Clickable(null));
			this.clickable.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse,
				modifiers = EventModifiers.Shift
			});
			EventModifiers multiSortingModifier = EventModifiers.Control;
			RuntimePlatform platform = Application.platform;
			bool flag = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				multiSortingModifier = EventModifiers.Command;
			}
			this.clickable.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse,
				modifiers = multiSortingModifier
			});
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000AB63C File Offset: 0x000A983C
		private void OnMoverChanged(ColumnMover mv)
		{
			bool moving = this.mover.moving;
			if (moving)
			{
				base.AddToClassList(MultiColumnHeaderColumn.movingUssClassName);
			}
			else
			{
				base.RemoveFromClassList(MultiColumnHeaderColumn.movingUssClassName);
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000AB674 File Offset: 0x000A9874
		private void UpdateDataFromColumn()
		{
			bool flag = this.column == null;
			if (!flag)
			{
				base.name = this.column.name;
				this.UnbindHeaderContent();
				this.BindHeaderContent();
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000AB6B4 File Offset: 0x000A98B4
		private void BindHeaderContent()
		{
			bool flag = !this.isContentBound;
			if (flag)
			{
				Action<VisualElement> bindCallback = this.content.GetProperty(MultiColumnHeaderColumn.s_BindingCallbackVEPropertyName) as Action<VisualElement>;
				if (bindCallback != null)
				{
					bindCallback(this.content);
				}
				this.isContentBound = true;
			}
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000AB708 File Offset: 0x000A9908
		private void UnbindHeaderContent()
		{
			bool isContentBound = this.isContentBound;
			if (isContentBound)
			{
				Action<VisualElement> unbindCallback = this.content.GetProperty(MultiColumnHeaderColumn.s_UnbindingCallbackVEPropertyName) as Action<VisualElement>;
				if (unbindCallback != null)
				{
					unbindCallback(this.content);
				}
				this.isContentBound = false;
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000AB758 File Offset: 0x000A9958
		private void DestroyHeaderContent()
		{
			this.UnbindHeaderContent();
			Action<VisualElement> destroyCallback = this.content.GetProperty(MultiColumnHeaderColumn.s_DestroyCallbackVEPropertyName) as Action<VisualElement>;
			this.content.ClearProperty(MultiColumnHeaderColumn.s_BindingCallbackVEPropertyName);
			this.content.ClearProperty(MultiColumnHeaderColumn.s_UnbindingCallbackVEPropertyName);
			this.content.ClearProperty(MultiColumnHeaderColumn.s_DestroyCallbackVEPropertyName);
			this.content.ClearProperty(MultiColumnHeaderColumn.s_BoundVEPropertyName);
			if (destroyCallback != null)
			{
				destroyCallback(this.content);
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000AB7F4 File Offset: 0x000A99F4
		private VisualElement CreateDefaultHeaderContent()
		{
			VisualElement defContent = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			defContent.AddToClassList(MultiColumnHeaderColumn.defaultContentUssClassName);
			MultiColumnHeaderColumnIcon icon = new MultiColumnHeaderColumnIcon
			{
				name = MultiColumnHeaderColumn.iconElementName,
				pickingMode = PickingMode.Ignore
			};
			Label title = new Label
			{
				name = MultiColumnHeaderColumn.titleElementName,
				pickingMode = PickingMode.Ignore
			};
			title.AddToClassList(MultiColumnHeaderColumn.titleUssClassName);
			defContent.Add(icon);
			defContent.Add(title);
			return defContent;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000AB874 File Offset: 0x000A9A74
		private void DefaultBindHeaderContent(VisualElement ve)
		{
			Label title = ve.Q(MultiColumnHeaderColumn.titleElementName, null);
			MultiColumnHeaderColumnIcon icon = ve.Q(null, null);
			ve.RemoveFromClassList(MultiColumnHeaderColumn.hasTitleUssClassName);
			bool flag = title != null;
			if (flag)
			{
				title.text = this.column.title;
			}
			bool flag2 = !string.IsNullOrEmpty(this.column.title);
			if (flag2)
			{
				ve.AddToClassList(MultiColumnHeaderColumn.hasTitleUssClassName);
			}
			bool flag3 = icon != null;
			if (flag3)
			{
				bool flag4 = this.column.icon.texture != null || this.column.icon.sprite != null || this.column.icon.vectorImage != null;
				if (flag4)
				{
					icon.isImageInline = true;
					icon.image = this.column.icon.texture;
					icon.sprite = this.column.icon.sprite;
					icon.vectorImage = this.column.icon.vectorImage;
				}
				else
				{
					bool isImageInline = icon.isImageInline;
					if (isImageInline)
					{
						icon.image = null;
						icon.sprite = null;
						icon.vectorImage = null;
					}
				}
				icon.UpdateClassList();
			}
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
		private void UpdateHeaderTemplate()
		{
			bool flag = this.column == null;
			if (!flag)
			{
				Func<VisualElement> makeContentCallback = this.column.makeHeader;
				Action<VisualElement> bindContentCallback = this.column.bindHeader;
				Action<VisualElement> unbindContentCallback = this.column.unbindHeader;
				Action<VisualElement> destroyCallback = this.column.destroyHeader;
				bool flag2 = makeContentCallback == null;
				if (flag2)
				{
					makeContentCallback = new Func<VisualElement>(this.CreateDefaultHeaderContent);
					bindContentCallback = new Action<VisualElement>(this.DefaultBindHeaderContent);
					unbindContentCallback = null;
					destroyCallback = null;
				}
				this.content = makeContentCallback();
				this.content.SetProperty(MultiColumnHeaderColumn.s_BindingCallbackVEPropertyName, bindContentCallback);
				this.content.SetProperty(MultiColumnHeaderColumn.s_UnbindingCallbackVEPropertyName, unbindContentCallback);
				this.content.SetProperty(MultiColumnHeaderColumn.s_DestroyCallbackVEPropertyName, destroyCallback);
				this.isContentBound = false;
				this.m_ScheduledHeaderTemplateUpdate = null;
				this.UpdateDataFromColumn();
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000ABABC File Offset: 0x000A9CBC
		private void UpdateGeometryFromColumn()
		{
			bool flag = float.IsNaN(this.column.desiredWidth);
			if (!flag)
			{
				base.style.width = this.column.desiredWidth;
			}
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000ABAFC File Offset: 0x000A9CFC
		public void Dispose()
		{
			this.mover.movingChanged -= this.OnMoverChanged;
			this.column.changed -= this.OnColumnChanged;
			this.column.resized -= this.OnColumnResized;
			this.RemoveManipulator(this.mover);
			this.RemoveManipulator(this.clickable);
			this.mover = null;
			this.column = null;
			this.content = null;
		}

		// Token: 0x040015DB RID: 5595
		public static readonly string ussClassName = MultiColumnCollectionHeader.ussClassName + "__column";

		// Token: 0x040015DC RID: 5596
		public static readonly string sortableUssClassName = MultiColumnHeaderColumn.ussClassName + "--sortable";

		// Token: 0x040015DD RID: 5597
		public static readonly string sortedAscendingUssClassName = MultiColumnHeaderColumn.ussClassName + "--sorted-ascending";

		// Token: 0x040015DE RID: 5598
		public static readonly string sortedDescendingUssClassName = MultiColumnHeaderColumn.ussClassName + "--sorted-descending";

		// Token: 0x040015DF RID: 5599
		public static readonly string movingUssClassName = MultiColumnHeaderColumn.ussClassName + "--moving";

		// Token: 0x040015E0 RID: 5600
		public static readonly string contentContainerUssClassName = MultiColumnHeaderColumn.ussClassName + "__content-container";

		// Token: 0x040015E1 RID: 5601
		public static readonly string contentUssClassName = MultiColumnHeaderColumn.ussClassName + "__content";

		// Token: 0x040015E2 RID: 5602
		public static readonly string defaultContentUssClassName = MultiColumnHeaderColumn.ussClassName + "__default-content";

		// Token: 0x040015E3 RID: 5603
		public static readonly string hasIconUssClassName = MultiColumnHeaderColumn.contentUssClassName + "--has-icon";

		// Token: 0x040015E4 RID: 5604
		public static readonly string hasTitleUssClassName = MultiColumnHeaderColumn.contentUssClassName + "--has-title";

		// Token: 0x040015E5 RID: 5605
		public static readonly string titleUssClassName = MultiColumnHeaderColumn.ussClassName + "__title";

		// Token: 0x040015E6 RID: 5606
		public static readonly string iconElementName = "unity-multi-column-header-column-icon";

		// Token: 0x040015E7 RID: 5607
		public static readonly string titleElementName = "unity-multi-column-header-column-title";

		// Token: 0x040015E8 RID: 5608
		private static readonly string s_BoundVEPropertyName = "__bound";

		// Token: 0x040015E9 RID: 5609
		private static readonly string s_BindingCallbackVEPropertyName = "__binding-callback";

		// Token: 0x040015EA RID: 5610
		private static readonly string s_UnbindingCallbackVEPropertyName = "__unbinding-callback";

		// Token: 0x040015EB RID: 5611
		private static readonly string s_DestroyCallbackVEPropertyName = "__destroy-callback";

		// Token: 0x040015EC RID: 5612
		private VisualElement m_ContentContainer;

		// Token: 0x040015ED RID: 5613
		private VisualElement m_Content;

		// Token: 0x040015EE RID: 5614
		private MultiColumnHeaderColumnSortIndicator m_SortIndicatorContainer;

		// Token: 0x040015EF RID: 5615
		private IVisualElementScheduledItem m_ScheduledHeaderTemplateUpdate;
	}
}
