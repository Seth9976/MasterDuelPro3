using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000116 RID: 278
	public class MultiColumnTreeView : BaseTreeView
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002A470 File Offset: 0x00028670
		public new MultiColumnTreeViewController viewController
		{
			get
			{
				return base.viewController as MultiColumnTreeViewController;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0002A47D File Offset: 0x0002867D
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0002A488 File Offset: 0x00028688
		[CreateProperty]
		public Columns columns
		{
			get
			{
				return this.m_Columns;
			}
			private set
			{
				bool flag = this.m_Columns != null;
				if (flag)
				{
					this.m_Columns.propertyChanged -= this.ColumnsChanged;
				}
				bool flag2 = value == null;
				if (flag2)
				{
					this.m_Columns.Clear();
				}
				else
				{
					this.m_Columns = value;
					this.m_Columns.propertyChanged += this.ColumnsChanged;
					bool flag3 = this.m_Columns.Count > 0;
					if (flag3)
					{
						base.GetOrCreateViewController();
					}
					base.NotifyPropertyChanged(in MultiColumnTreeView.columnsProperty);
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002A518 File Offset: 0x00028718
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0002A520 File Offset: 0x00028720
		[CreateProperty]
		public SortColumnDescriptions sortColumnDescriptions
		{
			get
			{
				return this.m_SortColumnDescriptions;
			}
			private set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_SortColumnDescriptions.Clear();
				}
				else
				{
					this.m_SortColumnDescriptions = value;
					bool flag2 = this.viewController != null;
					if (flag2)
					{
						this.viewController.columnController.header.sortDescriptions = value;
						this.RaiseColumnSortingChanged();
					}
					base.NotifyPropertyChanged(in MultiColumnTreeView.sortColumnDescriptionsProperty);
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0002A585 File Offset: 0x00028785
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0002A590 File Offset: 0x00028790
		[CreateProperty]
		public ColumnSortingMode sortingMode
		{
			get
			{
				return this.m_SortingMode;
			}
			set
			{
				bool flag = this.sortingMode == value;
				if (!flag)
				{
					this.m_SortingMode = value;
					bool flag2 = this.viewController != null;
					if (flag2)
					{
						this.viewController.columnController.sortingMode = value;
					}
					base.NotifyPropertyChanged(in MultiColumnTreeView.sortingModeProperty);
				}
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002A5E1 File Offset: 0x000287E1
		public MultiColumnTreeView()
			: this(new Columns())
		{
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002A5F0 File Offset: 0x000287F0
		public MultiColumnTreeView(Columns columns)
		{
			base.scrollView.viewDataKey = "unity-multi-column-scroll-view";
			this.columns = columns ?? new Columns();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002A63D File Offset: 0x0002883D
		protected override CollectionViewController CreateViewController()
		{
			return new DefaultMultiColumnTreeViewController<object>(this.columns, this.sortColumnDescriptions, this.m_SortedColumns);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002A658 File Offset: 0x00028858
		public override void SetViewController(CollectionViewController controller)
		{
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.columnController.columnSortingChanged -= this.RaiseColumnSortingChanged;
				this.viewController.columnController.headerContextMenuPopulateEvent -= this.RaiseHeaderContextMenuPopulate;
			}
			base.SetViewController(controller);
			bool flag2 = this.viewController != null;
			if (flag2)
			{
				this.viewController.columnController.sortingMode = this.m_SortingMode;
				this.viewController.columnController.columnSortingChanged += this.RaiseColumnSortingChanged;
				this.viewController.columnController.headerContextMenuPopulateEvent += this.RaiseHeaderContextMenuPopulate;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002A717 File Offset: 0x00028917
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableMultiColumnTreeViewItem>();
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002A721 File Offset: 0x00028921
		private void RaiseColumnSortingChanged()
		{
			Action action = this.columnSortingChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002A738 File Offset: 0x00028938
		private void ColumnsChanged(object sender, BindablePropertyChangedEventArgs args)
		{
			BindingId propertyName = args.propertyName;
			base.NotifyPropertyChanged(in propertyName);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002A757 File Offset: 0x00028957
		private void RaiseHeaderContextMenuPopulate(ContextualMenuPopulateEvent evt, Column column)
		{
			Action<ContextualMenuPopulateEvent, Column> action = this.headerContextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column);
			}
		}

		// Token: 0x0400057F RID: 1407
		private static readonly BindingId columnsProperty = "columns";

		// Token: 0x04000580 RID: 1408
		private static readonly BindingId sortColumnDescriptionsProperty = "sortColumnDescriptions";

		// Token: 0x04000581 RID: 1409
		private static readonly BindingId sortingModeProperty = "sortingMode";

		// Token: 0x04000582 RID: 1410
		private Columns m_Columns;

		// Token: 0x04000583 RID: 1411
		private ColumnSortingMode m_SortingMode;

		// Token: 0x04000584 RID: 1412
		private SortColumnDescriptions m_SortColumnDescriptions = new SortColumnDescriptions();

		// Token: 0x04000585 RID: 1413
		private List<SortColumnDescription> m_SortedColumns = new List<SortColumnDescription>();

		// Token: 0x04000586 RID: 1414
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action columnSortingChanged;

		// Token: 0x04000587 RID: 1415
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<ContextualMenuPopulateEvent, Column> headerContextMenuPopulateEvent;

		// Token: 0x02000117 RID: 279
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<MultiColumnTreeView, MultiColumnTreeView.UxmlTraits>
		{
		}

		// Token: 0x02000118 RID: 280
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseTreeView.UxmlTraits
		{
			// Token: 0x060008D3 RID: 2259 RVA: 0x0002A7A8 File Offset: 0x000289A8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MultiColumnTreeView treeView = (MultiColumnTreeView)ve;
				string stringSortingMode;
				bool flag = this.m_SortingMode.TryGetValueFromBagAsString(bag, cc, out stringSortingMode);
				if (flag)
				{
					bool boolSortingMode;
					bool flag2 = bool.TryParse(stringSortingMode, out boolSortingMode);
					if (flag2)
					{
						treeView.sortingMode = (boolSortingMode ? ColumnSortingMode.Custom : ColumnSortingMode.None);
					}
					else
					{
						treeView.sortingMode = this.m_SortingMode.GetValueFromBag(bag, cc);
					}
				}
				treeView.sortColumnDescriptions = this.m_SortColumnDescriptions.GetValueFromBag(bag, cc);
				treeView.columns = this.m_Columns.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000588 RID: 1416
			private readonly UxmlEnumAttributeDescription<ColumnSortingMode> m_SortingMode = new UxmlEnumAttributeDescription<ColumnSortingMode>
			{
				name = "sorting-mode",
				obsoleteNames = new string[] { "sorting-enabled" }
			};

			// Token: 0x04000589 RID: 1417
			private readonly UxmlObjectAttributeDescription<Columns> m_Columns = new UxmlObjectAttributeDescription<Columns>();

			// Token: 0x0400058A RID: 1418
			private readonly UxmlObjectAttributeDescription<SortColumnDescriptions> m_SortColumnDescriptions = new UxmlObjectAttributeDescription<SortColumnDescriptions>();
		}
	}
}
