using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000113 RID: 275
	public class MultiColumnListView : BaseListView
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0002A04D File Offset: 0x0002824D
		public new MultiColumnListViewController viewController
		{
			get
			{
				return base.viewController as MultiColumnListViewController;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0002A05A File Offset: 0x0002825A
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0002A064 File Offset: 0x00028264
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
					base.NotifyPropertyChanged(in MultiColumnListView.columnsProperty);
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0002A0F4 File Offset: 0x000282F4
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0002A0FC File Offset: 0x000282FC
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
					base.NotifyPropertyChanged(in MultiColumnListView.sortColumnDescriptionsProperty);
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0002A161 File Offset: 0x00028361
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0002A16C File Offset: 0x0002836C
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
					base.NotifyPropertyChanged(in MultiColumnListView.sortingModeProperty);
				}
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002A1BD File Offset: 0x000283BD
		public MultiColumnListView()
			: this(new Columns())
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002A1CC File Offset: 0x000283CC
		public MultiColumnListView(Columns columns)
		{
			base.scrollView.viewDataKey = "unity-multi-column-scroll-view";
			this.columns = columns ?? new Columns();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002A219 File Offset: 0x00028419
		protected override CollectionViewController CreateViewController()
		{
			return new MultiColumnListViewController(this.columns, this.sortColumnDescriptions, this.m_SortedColumns);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002A234 File Offset: 0x00028434
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

		// Token: 0x060008BA RID: 2234 RVA: 0x0002A2F3 File Offset: 0x000284F3
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableMultiColumnListViewItem>();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002A2FD File Offset: 0x000284FD
		private void RaiseColumnSortingChanged()
		{
			Action action = this.columnSortingChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002A314 File Offset: 0x00028514
		private void ColumnsChanged(object sender, BindablePropertyChangedEventArgs args)
		{
			BindingId propertyName = args.propertyName;
			base.NotifyPropertyChanged(in propertyName);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002A333 File Offset: 0x00028533
		private void RaiseHeaderContextMenuPopulate(ContextualMenuPopulateEvent evt, Column column)
		{
			Action<ContextualMenuPopulateEvent, Column> action = this.headerContextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column);
			}
		}

		// Token: 0x04000573 RID: 1395
		private static readonly BindingId columnsProperty = "columns";

		// Token: 0x04000574 RID: 1396
		private static readonly BindingId sortColumnDescriptionsProperty = "sortColumnDescriptions";

		// Token: 0x04000575 RID: 1397
		private static readonly BindingId sortingModeProperty = "sortingMode";

		// Token: 0x04000576 RID: 1398
		private Columns m_Columns;

		// Token: 0x04000577 RID: 1399
		private ColumnSortingMode m_SortingMode;

		// Token: 0x04000578 RID: 1400
		private SortColumnDescriptions m_SortColumnDescriptions = new SortColumnDescriptions();

		// Token: 0x04000579 RID: 1401
		private List<SortColumnDescription> m_SortedColumns = new List<SortColumnDescription>();

		// Token: 0x0400057A RID: 1402
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action columnSortingChanged;

		// Token: 0x0400057B RID: 1403
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<ContextualMenuPopulateEvent, Column> headerContextMenuPopulateEvent;

		// Token: 0x02000114 RID: 276
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<MultiColumnListView, MultiColumnListView.UxmlTraits>
		{
		}

		// Token: 0x02000115 RID: 277
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseListView.UxmlTraits
		{
			// Token: 0x060008C0 RID: 2240 RVA: 0x0002A384 File Offset: 0x00028584
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MultiColumnListView listView = (MultiColumnListView)ve;
				string stringSortingMode;
				bool flag = this.m_SortingMode.TryGetValueFromBagAsString(bag, cc, out stringSortingMode);
				if (flag)
				{
					bool boolSortingMode;
					bool flag2 = bool.TryParse(stringSortingMode, out boolSortingMode);
					if (flag2)
					{
						listView.sortingMode = (boolSortingMode ? ColumnSortingMode.Custom : ColumnSortingMode.None);
					}
					else
					{
						listView.sortingMode = this.m_SortingMode.GetValueFromBag(bag, cc);
					}
				}
				listView.sortColumnDescriptions = this.m_SortColumnDescriptions.GetValueFromBag(bag, cc);
				listView.columns = this.m_Columns.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400057C RID: 1404
			private readonly UxmlEnumAttributeDescription<ColumnSortingMode> m_SortingMode = new UxmlEnumAttributeDescription<ColumnSortingMode>
			{
				name = "sorting-mode",
				obsoleteNames = new string[] { "sorting-enabled" }
			};

			// Token: 0x0400057D RID: 1405
			private readonly UxmlObjectAttributeDescription<Columns> m_Columns = new UxmlObjectAttributeDescription<Columns>();

			// Token: 0x0400057E RID: 1406
			private readonly UxmlObjectAttributeDescription<SortColumnDescriptions> m_SortColumnDescriptions = new UxmlObjectAttributeDescription<SortColumnDescriptions>();
		}
	}
}
