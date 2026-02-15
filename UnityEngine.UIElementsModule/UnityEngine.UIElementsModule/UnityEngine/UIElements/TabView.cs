using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014F RID: 335
	public class TabView : VisualElement
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000319AE File Offset: 0x0002FBAE
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000319B6 File Offset: 0x0002FBB6
		internal List<Tab> tabs
		{
			get
			{
				return this.m_Tabs;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000319BE File Offset: 0x0002FBBE
		internal List<VisualElement> tabHeaders
		{
			get
			{
				return this.m_TabHeaders;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000319C6 File Offset: 0x0002FBC6
		internal VisualElement header
		{
			get
			{
				return this.m_HeaderContainer;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x000319CE File Offset: 0x0002FBCE
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x000319D8 File Offset: 0x0002FBD8
		public Tab activeTab
		{
			get
			{
				return this.m_ActiveTab;
			}
			set
			{
				bool flag = value == null && this.m_Tabs.Count > 0;
				if (flag)
				{
					throw new NullReferenceException("Active tab cannot be null when there are available tabs.");
				}
				bool flag2 = this.m_Tabs.IndexOf(value) == -1;
				if (flag2)
				{
					throw new Exception("The tab to be set as active does not exist in this TabView.");
				}
				bool flag3 = value == this.m_ActiveTab;
				if (!flag3)
				{
					Tab previous = this.m_ActiveTab;
					Tab activeTab = this.m_ActiveTab;
					if (activeTab != null)
					{
						activeTab.SetInactive();
					}
					this.m_ActiveTab = value;
					Tab activeTab2 = this.m_ActiveTab;
					if (activeTab2 != null)
					{
						activeTab2.SetActive();
					}
					bool flag4 = !this.m_ApplyingViewState;
					if (flag4)
					{
						this.SaveViewState();
					}
					Action<Tab, Tab> action = this.activeTabChanged;
					if (action != null)
					{
						action(previous, value);
					}
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00031A92 File Offset: 0x0002FC92
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00031A9C File Offset: 0x0002FC9C
		[CreateProperty]
		public bool reorderable
		{
			get
			{
				return this.m_Reorderable;
			}
			set
			{
				bool flag = this.m_Reorderable == value;
				if (!flag)
				{
					this.m_Reorderable = value;
					base.EnableInClassList(TabView.reorderableUssClassName, value);
					foreach (Tab tab in this.m_Tabs)
					{
						tab.EnableTabDragHandles(value);
					}
					base.NotifyPropertyChanged(in TabView.reorderableProperty);
				}
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00031B24 File Offset: 0x0002FD24
		public TabView()
		{
			base.AddToClassList(TabView.ussClassName);
			this.m_HeaderContainer = new VisualElement
			{
				name = TabView.headerContainerClassName,
				classList = { TabView.headerContainerClassName }
			};
			base.hierarchy.Add(this.m_HeaderContainer);
			this.m_ContentContainer = new VisualElement
			{
				name = TabView.contentContainerUssClassName,
				classList = { TabView.contentContainerUssClassName }
			};
			base.hierarchy.Add(this.m_ContentContainer);
			this.m_ContentContainer.elementAdded += this.OnElementAdded;
			this.m_ContentContainer.elementRemoved += this.OnElementRemoved;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00031C08 File Offset: 0x0002FE08
		internal override void OnViewDataReady()
		{
			try
			{
				this.m_ApplyingViewState = true;
				base.OnViewDataReady();
				string key = base.GetFullHierarchicalViewDataKey();
				this.m_ViewState = base.GetOrCreateViewData<TabView.ViewState>(this.m_ViewState, key);
				this.m_ViewState.Apply(this);
			}
			finally
			{
				this.m_ApplyingViewState = false;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00031C6C File Offset: 0x0002FE6C
		private void SaveViewState()
		{
			bool applyingViewState = this.m_ApplyingViewState;
			if (!applyingViewState)
			{
				TabView.ViewState viewState = this.m_ViewState;
				if (viewState != null)
				{
					viewState.Save(this);
				}
				base.SaveViewData();
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		private void OnElementAdded(VisualElement ve)
		{
			Tab tab = ve as Tab;
			bool flag = tab == null || this.m_Reordering;
			if (!flag)
			{
				VisualElement tabHeader = tab.tabHeader;
				bool flag2 = tabHeader != null;
				if (flag2)
				{
					this.m_HeaderContainer.Add(tabHeader);
					this.m_TabHeaders.Add(tabHeader);
					this.m_Tabs.Add(tab);
					tab.EnableTabDragHandles(this.m_Reorderable);
				}
				tab.selected += this.OnTabSelected;
				bool flag3 = this.activeTab == null;
				if (flag3)
				{
					this.activeTab = tab;
				}
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00031D38 File Offset: 0x0002FF38
		private void OnElementRemoved(VisualElement ve)
		{
			Tab tab = ve as Tab;
			bool flag = tab == null || this.m_Reordering;
			if (!flag)
			{
				VisualElement tabHeaderVisualElement = tab.tabHeader;
				this.m_HeaderContainer.Remove(tabHeaderVisualElement);
				this.m_TabHeaders.Remove(tabHeaderVisualElement);
				this.m_Tabs.Remove(tab);
				tab.EnableTabDragHandles(false);
				tab.hierarchy.Insert(0, tabHeaderVisualElement);
				tab.SetInactive();
				bool flag2 = this.activeTab == tab && this.m_Tabs.Count > 0;
				if (flag2)
				{
					this.activeTab = this.m_Tabs[0];
				}
				else
				{
					bool flag3 = this.m_Tabs.Count == 0;
					if (flag3)
					{
						this.m_ActiveTab = null;
					}
				}
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00031E00 File Offset: 0x00030000
		private void OnTabSelected(Tab tab)
		{
			this.activeTab = tab;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00031E0C File Offset: 0x0003000C
		internal void ReorderTab(int from, int to)
		{
			VisualElement tabHeader = this.m_TabHeaders[from];
			Tab tab = this.m_Tabs[from];
			bool flag = !tabHeader.visible || !this.reorderable || from == to;
			if (!flag)
			{
				this.m_Reordering = true;
				this.m_TabHeaders.RemoveAt(from);
				this.m_TabHeaders.Insert(to, tabHeader);
				this.m_Tabs.RemoveAt(from);
				this.m_Tabs.Insert(to, tab);
				this.m_HeaderContainer.Insert(to, tabHeader);
				base.Insert(to, tab);
				this.m_Reordering = false;
				bool flag2 = !this.m_ApplyingViewState;
				if (flag2)
				{
					this.SaveViewState();
				}
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00031EC0 File Offset: 0x000300C0
		internal Tab FindTabByKey(string key)
		{
			return this.m_Tabs.Find((Tab tab) => tab.viewDataKey == key);
		}

		// Token: 0x0400069C RID: 1692
		internal static readonly BindingId reorderableProperty = "reorderable";

		// Token: 0x0400069D RID: 1693
		public static readonly string ussClassName = "unity-tab-view";

		// Token: 0x0400069E RID: 1694
		public static readonly string headerContainerClassName = TabView.ussClassName + "__header-container";

		// Token: 0x0400069F RID: 1695
		public static readonly string contentContainerUssClassName = TabView.ussClassName + "__content-container";

		// Token: 0x040006A0 RID: 1696
		public static readonly string reorderableUssClassName = TabView.ussClassName + "__reorderable";

		// Token: 0x040006A1 RID: 1697
		public static readonly string verticalUssClassName = TabView.ussClassName + "__vertical";

		// Token: 0x040006A2 RID: 1698
		private VisualElement m_HeaderContainer;

		// Token: 0x040006A3 RID: 1699
		private VisualElement m_ContentContainer;

		// Token: 0x040006A4 RID: 1700
		private List<Tab> m_Tabs = new List<Tab>();

		// Token: 0x040006A5 RID: 1701
		private List<VisualElement> m_TabHeaders = new List<VisualElement>();

		// Token: 0x040006A6 RID: 1702
		private Tab m_ActiveTab;

		// Token: 0x040006A7 RID: 1703
		private TabView.ViewState m_ViewState;

		// Token: 0x040006A8 RID: 1704
		private bool m_ApplyingViewState;

		// Token: 0x040006A9 RID: 1705
		private bool m_Reordering;

		// Token: 0x040006AA RID: 1706
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<Tab, Tab> activeTabChanged;

		// Token: 0x040006AB RID: 1707
		private bool m_Reorderable;

		// Token: 0x02000150 RID: 336
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TabView, TabView.UxmlTraits>
		{
		}

		// Token: 0x02000151 RID: 337
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000A2F RID: 2607 RVA: 0x00031F78 File Offset: 0x00030178
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TabView tabView = (TabView)ve;
				tabView.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
			}

			// Token: 0x040006AC RID: 1708
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};
		}

		// Token: 0x02000152 RID: 338
		[Serializable]
		private class ViewState : ISerializationCallbackReceiver
		{
			// Token: 0x06000A31 RID: 2609 RVA: 0x00031FD4 File Offset: 0x000301D4
			internal void Save(TabView tabView)
			{
				this.m_HasPersistedData = true;
				bool flag = tabView.m_ActiveTab != null;
				if (flag)
				{
					this.m_ActiveTabKey = tabView.m_ActiveTab.viewDataKey;
				}
				this.m_TabOrder.Clear();
				bool flag2 = !tabView.reorderable;
				if (!flag2)
				{
					foreach (Tab tab in tabView.tabs)
					{
						this.m_TabOrder.Add(tab.viewDataKey);
					}
				}
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x00032078 File Offset: 0x00030278
			internal void Apply(TabView tabView)
			{
				bool flag = !this.m_HasPersistedData;
				if (!flag)
				{
					int minCount = Math.Min(this.m_TabOrder.Count, tabView.tabs.Count);
					int nextValidOrderedIndex = 0;
					Tab tabToActivate = tabView.FindTabByKey(this.m_ActiveTabKey);
					bool flag2 = tabToActivate != null;
					if (flag2)
					{
						tabView.activeTab = tabToActivate;
					}
					bool flag3 = !tabView.reorderable;
					if (!flag3)
					{
						int orderedIndex = 0;
						while (orderedIndex < this.m_TabOrder.Count && nextValidOrderedIndex < minCount)
						{
							string tabKey = this.m_TabOrder[orderedIndex];
							Tab tab = tabView.FindTabByKey(tabKey);
							bool flag4 = tab != null;
							if (flag4)
							{
								int displayIndex = tabView.tabs.IndexOf(tab);
								tabView.ReorderTab(displayIndex, nextValidOrderedIndex++);
							}
							orderedIndex++;
						}
					}
				}
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x00032152 File Offset: 0x00030352
			public void OnBeforeSerialize()
			{
				this.m_HasPersistedData = true;
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x00032152 File Offset: 0x00030352
			public void OnAfterDeserialize()
			{
				this.m_HasPersistedData = true;
			}

			// Token: 0x040006AD RID: 1709
			private bool m_HasPersistedData;

			// Token: 0x040006AE RID: 1710
			[SerializeField]
			private List<string> m_TabOrder = new List<string>();

			// Token: 0x040006AF RID: 1711
			[SerializeField]
			private string m_ActiveTabKey;
		}
	}
}
