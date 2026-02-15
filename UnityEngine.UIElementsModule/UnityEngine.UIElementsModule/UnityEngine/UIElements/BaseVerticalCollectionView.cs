using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000084 RID: 132
	public abstract class BaseVerticalCollectionView : BindableElement, ISerializationCallbackReceiver
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x000180EB File Offset: 0x000162EB
		internal bool HasCanStartDrag()
		{
			return this.canStartDrag != null;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000180F8 File Offset: 0x000162F8
		internal bool RaiseCanStartDrag(ReusableCollectionItem item, IEnumerable<int> ids)
		{
			Func<CanStartDragArgs, bool> func = this.canStartDrag;
			return func == null || func(new CanStartDragArgs((item != null) ? item.rootElement : null, (item != null) ? item.id : BaseTreeView.invalidId, ids));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00018140 File Offset: 0x00016340
		internal StartDragArgs RaiseSetupDragAndDrop(ReusableCollectionItem item, IEnumerable<int> ids, StartDragArgs args)
		{
			Func<SetupDragAndDropArgs, StartDragArgs> func = this.setupDragAndDrop;
			return (func != null) ? func(new SetupDragAndDropArgs((item != null) ? item.rootElement : null, ids, args)) : args;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00018178 File Offset: 0x00016378
		internal DragVisualMode RaiseHandleDragAndDrop(Vector2 pointerPosition, DragAndDropArgs dragAndDropArgs)
		{
			Func<HandleDragAndDropArgs, DragVisualMode> func = this.dragAndDropUpdate;
			return (func != null) ? func(new HandleDragAndDropArgs(pointerPosition, dragAndDropArgs)) : DragVisualMode.None;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000181A4 File Offset: 0x000163A4
		internal DragVisualMode RaiseDrop(Vector2 pointerPosition, DragAndDropArgs dragAndDropArgs)
		{
			Func<HandleDragAndDropArgs, DragVisualMode> func = this.handleDrop;
			return (func != null) ? func(new HandleDragAndDropArgs(pointerPosition, dragAndDropArgs)) : DragVisualMode.None;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x000181CF File Offset: 0x000163CF
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x000181E4 File Offset: 0x000163E4
		[CreateProperty]
		public IList itemsSource
		{
			get
			{
				CollectionViewController viewController = this.viewController;
				return (viewController != null) ? viewController.itemsSource : null;
			}
			set
			{
				IList previous = this.itemsSource;
				this.GetOrCreateViewController().itemsSource = value;
				bool flag = previous != this.itemsSource;
				if (flag)
				{
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.itemsSourceProperty);
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00018224 File Offset: 0x00016424
		public override VisualElement contentContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00018228 File Offset: 0x00016428
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00018240 File Offset: 0x00016440
		[CreateProperty]
		public SelectionType selectionType
		{
			get
			{
				return this.m_SelectionType;
			}
			set
			{
				SelectionType previous = this.m_SelectionType;
				this.m_SelectionType = value;
				bool flag = this.m_SelectionType == SelectionType.None;
				if (flag)
				{
					this.ClearSelection();
				}
				else
				{
					bool flag2 = this.m_SelectionType == SelectionType.Single;
					if (flag2)
					{
						bool flag3 = this.m_Selection.indexCount > 1;
						if (flag3)
						{
							this.SetSelection(this.m_Selection.FirstIndex());
						}
					}
				}
				bool flag4 = previous != this.m_SelectionType;
				if (flag4)
				{
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.selectionTypeProperty);
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000182C7 File Offset: 0x000164C7
		[CreateProperty(ReadOnly = true)]
		public object selectedItem
		{
			get
			{
				return this.m_Selection.FirstObject();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000182D4 File Offset: 0x000164D4
		[CreateProperty(ReadOnly = true)]
		public IEnumerable<object> selectedItems
		{
			get
			{
				foreach (int index in this.m_Selection.indices)
				{
					object item;
					bool flag = this.m_Selection.items.TryGetValue(index, out item);
					if (flag)
					{
						yield return item;
					}
					else
					{
						yield return null;
					}
					item = null;
				}
				List<int>.Enumerator enumerator = default(List<int>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000182F4 File Offset: 0x000164F4
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00018324 File Offset: 0x00016524
		[CreateProperty]
		public int selectedIndex
		{
			get
			{
				return (this.m_Selection.indexCount == 0) ? (-1) : this.m_Selection.FirstIndex();
			}
			set
			{
				int previous = this.selectedIndex;
				this.SetSelection(value);
				bool flag = previous != this.selectedIndex;
				if (flag)
				{
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.selectedIndexProperty);
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001835D File Offset: 0x0001655D
		[CreateProperty(ReadOnly = true)]
		public IEnumerable<int> selectedIndices
		{
			get
			{
				return this.m_Selection.indices;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001836A File Offset: 0x0001656A
		public IEnumerable<int> selectedIds
		{
			get
			{
				return this.m_Selection.selectedIds;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00018377 File Offset: 0x00016577
		internal IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
				return ((virtualizationController != null) ? virtualizationController.activeItems : null) ?? BaseVerticalCollectionView.k_EmptyItems;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00018394 File Offset: 0x00016594
		internal ScrollView scrollView
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_ScrollView;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001839C File Offset: 0x0001659C
		internal ListViewDragger dragger
		{
			get
			{
				return this.m_Dragger;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000183A4 File Offset: 0x000165A4
		internal CollectionVirtualizationController virtualizationController
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.GetOrCreateVirtualizationController();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000183AC File Offset: 0x000165AC
		public CollectionViewController viewController
		{
			get
			{
				return this.m_ViewController;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000183B4 File Offset: 0x000165B4
		internal float ResolveItemHeight(float height = -1f)
		{
			height = ((height < 0f) ? this.fixedItemHeight : height);
			bool flag = base.elementPanel == null;
			float num;
			if (flag)
			{
				num = height;
			}
			else
			{
				num = AlignmentUtils.RoundToPixelGrid(height, base.scaledPixelsPerPoint, 0.02f);
			}
			return num;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000183FC File Offset: 0x000165FC
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00018410 File Offset: 0x00016610
		[CreateProperty]
		public bool showBorder
		{
			get
			{
				return this.m_ScrollView.ClassListContains(BaseVerticalCollectionView.borderUssClassName);
			}
			set
			{
				bool previous = this.showBorder;
				this.m_ScrollView.EnableInClassList(BaseVerticalCollectionView.borderUssClassName, value);
				bool flag = previous != this.showBorder;
				if (flag)
				{
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.showBorderProperty);
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00018454 File Offset: 0x00016654
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001849C File Offset: 0x0001669C
		[CreateProperty]
		public bool reorderable
		{
			get
			{
				ListViewDragger dragger = this.m_Dragger;
				bool? flag;
				if (dragger == null)
				{
					flag = null;
				}
				else
				{
					ICollectionDragAndDropController dragAndDropController = dragger.dragAndDropController;
					flag = ((dragAndDropController != null) ? new bool?(dragAndDropController.enableReordering) : null);
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
			set
			{
				bool previous = this.reorderable;
				try
				{
					ICollectionDragAndDropController controller = this.m_Dragger.dragAndDropController;
					bool flag = controller != null && controller.enableReordering != value;
					if (flag)
					{
						controller.enableReordering = value;
						this.Rebuild();
					}
				}
				finally
				{
					bool flag2 = previous != this.reorderable;
					if (flag2)
					{
						base.NotifyPropertyChanged(in BaseVerticalCollectionView.reorderableProperty);
					}
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00018518 File Offset: 0x00016718
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00018520 File Offset: 0x00016720
		[CreateProperty]
		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_HorizontalScrollingEnabled;
			}
			set
			{
				bool flag = this.m_HorizontalScrollingEnabled == value;
				if (!flag)
				{
					this.m_HorizontalScrollingEnabled = value;
					this.m_ScrollView.horizontalScrollerVisibility = (value ? ScrollerVisibility.Auto : ScrollerVisibility.Hidden);
					this.m_ScrollView.mode = (value ? ScrollViewMode.VerticalAndHorizontal : ScrollViewMode.Vertical);
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.horizontalScrollingEnabledProperty);
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00018578 File Offset: 0x00016778
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00018590 File Offset: 0x00016790
		[CreateProperty]
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ShowAlternatingRowBackgrounds;
			}
			set
			{
				bool flag = this.m_ShowAlternatingRowBackgrounds == value;
				if (!flag)
				{
					this.m_ShowAlternatingRowBackgrounds = value;
					this.RefreshItems();
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.showAlternatingRowBackgroundsProperty);
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000185C7 File Offset: 0x000167C7
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000185D0 File Offset: 0x000167D0
		[CreateProperty]
		public CollectionVirtualizationMethod virtualizationMethod
		{
			get
			{
				return this.m_VirtualizationMethod;
			}
			set
			{
				bool flag = this.m_VirtualizationMethod == value;
				if (!flag)
				{
					this.m_VirtualizationMethod = value;
					this.CreateVirtualizationController();
					this.Rebuild();
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.virtualizationMethodProperty);
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001860E File Offset: 0x0001680E
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00018618 File Offset: 0x00016818
		[CreateProperty]
		public float fixedItemHeight
		{
			get
			{
				return this.m_FixedItemHeight;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("fixedItemHeight", "Value needs to be positive for virtualization.");
				}
				this.m_ItemHeightIsInline = true;
				bool flag2 = Math.Abs(this.m_FixedItemHeight - value) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = value;
					this.RefreshItems();
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.fixedItemHeightProperty);
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001867D File Offset: 0x0001687D
		internal float lastHeight
		{
			get
			{
				return this.m_LastHeight;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00018685 File Offset: 0x00016885
		private protected virtual void CreateVirtualizationController()
		{
			this.CreateVirtualizationController<ReusableCollectionItem>();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00018690 File Offset: 0x00016890
		internal CollectionVirtualizationController GetOrCreateVirtualizationController()
		{
			bool flag = this.m_VirtualizationController == null;
			if (flag)
			{
				this.CreateVirtualizationController();
			}
			return this.m_VirtualizationController;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000186BC File Offset: 0x000168BC
		internal void CreateVirtualizationController<T>() where T : ReusableCollectionItem, new()
		{
			CollectionVirtualizationMethod virtualizationMethod = this.virtualizationMethod;
			CollectionVirtualizationMethod collectionVirtualizationMethod = virtualizationMethod;
			if (collectionVirtualizationMethod != CollectionVirtualizationMethod.FixedHeight)
			{
				if (collectionVirtualizationMethod != CollectionVirtualizationMethod.DynamicHeight)
				{
					throw new ArgumentOutOfRangeException("virtualizationMethod", this.virtualizationMethod, "Unsupported virtualizationMethod virtualization");
				}
				this.m_VirtualizationController = new DynamicHeightVirtualizationController<T>(this);
			}
			else
			{
				this.m_VirtualizationController = new FixedHeightVirtualizationController<T>(this);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00018718 File Offset: 0x00016918
		internal CollectionViewController GetOrCreateViewController()
		{
			bool flag = this.m_ViewController == null;
			if (flag)
			{
				this.SetViewController(this.CreateViewController());
			}
			return this.m_ViewController;
		}

		// Token: 0x060004FE RID: 1278
		protected abstract CollectionViewController CreateViewController();

		// Token: 0x060004FF RID: 1279 RVA: 0x0001874C File Offset: 0x0001694C
		public virtual void SetViewController(CollectionViewController controller)
		{
			bool flag = this.m_ViewController != null;
			if (flag)
			{
				this.m_ViewController.itemIndexChanged -= this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged -= this.m_ItemsSourceChangedCallback;
				this.m_ViewController.Dispose();
				this.m_ViewController = null;
			}
			this.m_ViewController = controller;
			bool flag2 = this.m_ViewController != null;
			if (flag2)
			{
				this.m_ViewController.SetView(this);
				this.m_ViewController.itemIndexChanged += this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged += this.m_ItemsSourceChangedCallback;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000187E8 File Offset: 0x000169E8
		internal virtual ListViewDragger CreateDragger()
		{
			return new ListViewDragger(this);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00018800 File Offset: 0x00016A00
		internal void InitializeDragAndDropController(bool enableReordering)
		{
			bool flag = this.m_Dragger != null;
			if (flag)
			{
				this.m_Dragger.UnregisterCallbacksFromTarget(true);
				this.m_Dragger.dragAndDropController = null;
				this.m_Dragger = null;
			}
			this.m_Dragger = this.CreateDragger();
			this.m_Dragger.dragAndDropController = this.CreateDragAndDropController();
			bool flag2 = this.m_Dragger.dragAndDropController == null;
			if (!flag2)
			{
				this.m_Dragger.dragAndDropController.enableReordering = enableReordering;
			}
		}

		// Token: 0x06000502 RID: 1282
		internal abstract ICollectionDragAndDropController CreateDragAndDropController();

		// Token: 0x06000503 RID: 1283 RVA: 0x00018884 File Offset: 0x00016A84
		public BaseVerticalCollectionView()
		{
			base.AddToClassList(BaseVerticalCollectionView.ussClassName);
			this.m_Selection = new BaseVerticalCollectionView.Selection
			{
				selectedIds = this.m_SelectedIds
			};
			this.selectionType = SelectionType.Single;
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(BaseVerticalCollectionView.listScrollViewUssClassName);
			this.m_ScrollView.verticalScroller.valueChanged += delegate(float v)
			{
				this.OnScroll(new Vector2(0f, v));
			};
			this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChanged), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_ScrollView);
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.contentContainer.usageHints &= ~UsageHints.GroupTransform;
			this.m_ScrollView.viewDataKey = "unity-vertical-collection-scroll-view";
			this.m_ScrollView.verticalScroller.viewDataKey = null;
			this.m_ScrollView.horizontalScroller.viewDataKey = null;
			this.focusable = true;
			base.isCompositeRoot = true;
			base.delegatesFocus = true;
			this.m_ItemIndexChangedCallback = new Action<int, int>(this.OnItemIndexChanged);
			this.m_ItemsSourceChangedCallback = new Action(this.OnItemsSourceChanged);
			this.InitializeDragAndDropController(false);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00018A78 File Offset: 0x00016C78
		public BaseVerticalCollectionView(IList itemsSource, float itemHeight = -1f)
			: this()
		{
			bool flag = Math.Abs(itemHeight - -1f) > float.Epsilon;
			if (flag)
			{
				this.m_FixedItemHeight = itemHeight;
				this.m_ItemHeightIsInline = true;
			}
			bool flag2 = itemsSource != null;
			if (flag2)
			{
				this.itemsSource = itemsSource;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public VisualElement GetRootElementForId(int id)
		{
			ReusableCollectionItem reusableCollectionItem = this.activeItems.FirstOrDefault((ReusableCollectionItem t) => t.id == id);
			return (reusableCollectionItem != null) ? reusableCollectionItem.rootElement : null;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00018B0C File Offset: 0x00016D0C
		internal virtual bool HasValidDataAndBindings()
		{
			return this.m_ViewController != null && this.itemsSource != null;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00018B32 File Offset: 0x00016D32
		private void OnItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action(srcIndex, dstIndex);
			}
			this.RefreshItems();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00018B50 File Offset: 0x00016D50
		private void OnItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action();
			}
			BindingId bindingId = "itemsSource";
			base.NotifyPropertyChanged(in bindingId);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00018B84 File Offset: 0x00016D84
		public void RefreshItems()
		{
			using (BaseVerticalCollectionView.k_RefreshMarker.Auto())
			{
				bool flag = this.m_ViewController == null;
				if (!flag)
				{
					IVisualElementScheduledItem rebuildScheduled = this.m_RebuildScheduled;
					bool flag2 = rebuildScheduled != null && rebuildScheduled.isActive;
					if (flag2)
					{
						this.Rebuild();
					}
					else
					{
						this.m_ViewController.PreRefresh();
						this.RefreshSelection();
						this.virtualizationController.Refresh(false);
						this.PostRefresh();
					}
				}
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00018C1C File Offset: 0x00016E1C
		public void Rebuild()
		{
			using (BaseVerticalCollectionView.k_RebuildMarker.Auto())
			{
				bool flag = this.m_ViewController == null;
				if (!flag)
				{
					this.m_ViewController.PreRefresh();
					this.RefreshSelection();
					this.virtualizationController.Refresh(true);
					this.PostRefresh();
					IVisualElementScheduledItem rebuildScheduled = this.m_RebuildScheduled;
					if (rebuildScheduled != null)
					{
						rebuildScheduled.Pause();
					}
				}
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00018CA4 File Offset: 0x00016EA4
		internal void ScheduleRebuild()
		{
			bool flag = this.m_RebuildScheduled == null;
			if (flag)
			{
				this.m_RebuildScheduled = base.schedule.Execute(new Action(this.Rebuild));
			}
			else
			{
				bool flag2 = !this.m_RebuildScheduled.isActive;
				if (flag2)
				{
					this.m_RebuildScheduled.Resume();
				}
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00018CFC File Offset: 0x00016EFC
		private void RefreshSelection()
		{
			BaseVerticalCollectionView.<>c__DisplayClass191_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedIndicesChanged = false;
			CS$<>8__locals1.previousSelectionCount = this.m_Selection.indexCount;
			this.m_Selection.items.Clear();
			CollectionViewController viewController = this.viewController;
			bool flag = ((viewController != null) ? viewController.itemsSource : null) == null;
			if (flag)
			{
				this.m_Selection.ClearIndices();
				this.<RefreshSelection>g__NotifyIfChanged|191_0(ref CS$<>8__locals1);
			}
			else
			{
				bool flag2 = this.m_Selection.idCount > 0;
				if (flag2)
				{
					List<int> list;
					using (CollectionPool<List<int>, int>.Get(out list))
					{
						foreach (int id in this.m_Selection.selectedIds)
						{
							int index = this.viewController.GetIndexForId(id);
							bool flag3 = index < 0;
							if (flag3)
							{
								CS$<>8__locals1.selectedIndicesChanged = true;
							}
							else
							{
								bool flag4 = !this.m_Selection.ContainsIndex(index);
								if (flag4)
								{
									CS$<>8__locals1.selectedIndicesChanged = true;
								}
								list.Add(index);
							}
						}
						this.m_Selection.ClearIndices();
						foreach (int index2 in list)
						{
							this.m_Selection.AddIndex(index2, this.viewController.GetItemForIndex(index2));
						}
					}
				}
				this.<RefreshSelection>g__NotifyIfChanged|191_0(ref CS$<>8__locals1);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00018EB8 File Offset: 0x000170B8
		private protected virtual void PostRefresh()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_LastHeight = this.m_ScrollView.layout.height;
				bool flag2 = base.panel == null || float.IsNaN(this.m_ScrollView.layout.height);
				if (!flag2)
				{
					this.Resize(this.m_ScrollView.layout.size);
				}
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00018F34 File Offset: 0x00017134
		public void ScrollToItem(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.ScrollToItem(index);
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00018F60 File Offset: 0x00017160
		public void ScrollToItemById(int id)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				int index = this.viewController.GetIndexForId(id);
				this.virtualizationController.ScrollToItem(index);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00018F98 File Offset: 0x00017198
		private void OnScroll(Vector2 offset)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.OnScroll(offset);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00018FC2 File Offset: 0x000171C2
		private void Resize(Vector2 size)
		{
			this.virtualizationController.Resize(size);
			this.m_LastHeight = size.y;
			this.virtualizationController.UpdateBackground();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00018FEC File Offset: 0x000171EC
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_ScrollView.contentContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000190B0 File Offset: 0x000172B0
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00019160 File Offset: 0x00017360
		private bool Apply(KeyboardNavigationOperation op, bool shiftKey, bool altKey)
		{
			BaseVerticalCollectionView.<>c__DisplayClass202_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.shiftKey = shiftKey;
			bool flag = this.selectionType == SelectionType.None || !this.HasValidDataAndBindings();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				switch (op)
				{
				case KeyboardNavigationOperation.SelectAll:
					this.SelectAll();
					return true;
				case KeyboardNavigationOperation.Cancel:
					this.ClearSelection();
					return true;
				case KeyboardNavigationOperation.Submit:
				{
					Action<IEnumerable<object>> action = this.itemsChosen;
					if (action != null)
					{
						action(this.selectedItems);
					}
					this.ScrollToItem(this.selectedIndex);
					return true;
				}
				case KeyboardNavigationOperation.Previous:
				{
					bool flag3 = this.selectedIndex > 0;
					if (flag3)
					{
						this.<Apply>g__HandleSelectionAndScroll|202_0(this.selectedIndex - 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.Next:
				{
					bool flag4 = this.selectedIndex + 1 < this.m_ViewController.itemsSource.Count;
					if (flag4)
					{
						this.<Apply>g__HandleSelectionAndScroll|202_0(this.selectedIndex + 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.MoveRight:
				{
					bool flag5 = this.m_Selection.indexCount > 0;
					if (flag5)
					{
						return this.HandleItemNavigation(true, altKey);
					}
					break;
				}
				case KeyboardNavigationOperation.MoveLeft:
				{
					bool flag6 = this.m_Selection.indexCount > 0;
					if (flag6)
					{
						return this.HandleItemNavigation(false, altKey);
					}
					break;
				}
				case KeyboardNavigationOperation.PageUp:
				{
					bool flag7 = this.m_Selection.indexCount > 0;
					if (flag7)
					{
						int selectionUp = (this.m_IsRangeSelectionDirectionUp ? this.m_Selection.minIndex : this.m_Selection.maxIndex);
						this.<Apply>g__HandleSelectionAndScroll|202_0(Mathf.Max(0, selectionUp - (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.PageDown:
				{
					bool flag8 = this.m_Selection.indexCount > 0;
					if (flag8)
					{
						int selectionDown = (this.m_IsRangeSelectionDirectionUp ? this.m_Selection.minIndex : this.m_Selection.maxIndex);
						this.<Apply>g__HandleSelectionAndScroll|202_0(Mathf.Min(this.viewController.itemsSource.Count - 1, selectionDown + (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.Begin:
					this.<Apply>g__HandleSelectionAndScroll|202_0(0, ref CS$<>8__locals1);
					return true;
				case KeyboardNavigationOperation.End:
					this.<Apply>g__HandleSelectionAndScroll|202_0(this.m_ViewController.itemsSource.Count - 1, ref CS$<>8__locals1);
					return true;
				default:
					throw new ArgumentOutOfRangeException("op", op, null);
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000193EC File Offset: 0x000175EC
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			KeyDownEvent keyDownEvent = sourceEvent as KeyDownEvent;
			bool flag;
			if (keyDownEvent == null || !keyDownEvent.shiftKey)
			{
				INavigationEvent navigationEvent = sourceEvent as INavigationEvent;
				if (navigationEvent != null)
				{
					if (navigationEvent.shiftKey)
					{
						goto IL_0030;
					}
				}
				flag = false;
				goto IL_0038;
			}
			IL_0030:
			flag = true;
			IL_0038:
			bool shiftKey = flag;
			keyDownEvent = sourceEvent as KeyDownEvent;
			bool flag2;
			if (keyDownEvent == null || !keyDownEvent.altKey)
			{
				INavigationEvent navigationEvent = sourceEvent as INavigationEvent;
				if (navigationEvent != null)
				{
					if (navigationEvent.altKey)
					{
						goto IL_006C;
					}
				}
				flag2 = false;
				goto IL_0072;
			}
			IL_006C:
			flag2 = true;
			IL_0072:
			bool altKey = flag2;
			bool flag3 = this.Apply(op, shiftKey, altKey);
			if (flag3)
			{
				sourceEvent.StopPropagation();
			}
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.IgnoreEvent(sourceEvent);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00019498 File Offset: 0x00017698
		private protected virtual bool HandleItemNavigation(bool moveIn, bool altKey)
		{
			return false;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000194AC File Offset: 0x000176AC
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) == 0;
				if (flag2)
				{
					this.ProcessPointerUp(evt);
				}
				else
				{
					this.ProcessPointerDown(evt);
				}
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000194EE File Offset: 0x000176EE
		private void OnPointerDown(PointerDownEvent evt)
		{
			this.ProcessPointerDown(evt);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000194FC File Offset: 0x000176FC
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					this.ClearSelection();
				}
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001952F File Offset: 0x0001772F
		private void OnPointerUp(PointerUpEvent evt)
		{
			this.ProcessPointerUp(evt);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001953C File Offset: 0x0001773C
		private void ProcessPointerDown(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					int button = evt.button;
					bool flag3 = button != 0 && button != 1;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							this.m_TouchDownPosition = evt.position;
						}
						else
						{
							this.DoSelect(evt.localPosition, evt.button, evt.clickCount, evt.actionKey, evt.shiftKey);
						}
					}
				}
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000195D4 File Offset: 0x000177D4
		private void ProcessPointerUp(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					int button = evt.button;
					bool flag3 = button != 0 && button != 1;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							bool flag5 = (evt.position - this.m_TouchDownPosition).sqrMagnitude <= 100f;
							if (flag5)
							{
								this.DoSelect(evt.localPosition, evt.button, evt.clickCount, evt.actionKey, evt.shiftKey);
							}
						}
						else
						{
							int clickedIndex = this.virtualizationController.GetIndexFromPosition(evt.localPosition);
							bool flag6 = this.selectionType == SelectionType.Multiple && evt.button == 0 && !evt.shiftKey && !evt.actionKey && this.m_Selection.indexCount > 1 && this.m_Selection.ContainsIndex(clickedIndex);
							if (flag6)
							{
								this.ProcessSingleClick(clickedIndex);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00019700 File Offset: 0x00017900
		private void DoSelect(Vector2 localPosition, int mouseButton, int clickCount, bool actionKey, bool shiftKey)
		{
			int clickedIndex = this.virtualizationController.GetIndexFromPosition(localPosition);
			int effectiveClickCount = ((this.m_Selection.indexCount > 0 && this.m_Selection.FirstIndex() != clickedIndex) ? 1 : ((clickCount > 2) ? 2 : clickCount));
			bool flag = clickedIndex > this.viewController.itemsSource.Count - 1;
			if (!flag)
			{
				bool flag2 = this.selectionType == SelectionType.None;
				if (!flag2)
				{
					int clickedItemId = this.viewController.GetIdForIndex(clickedIndex);
					int num = effectiveClickCount;
					int num2 = num;
					if (num2 != 1)
					{
						if (num2 == 2)
						{
							bool flag3 = this.itemsChosen == null;
							if (!flag3)
							{
								bool wasClickedIndexInSelection = false;
								foreach (int index in this.selectedIndices)
								{
									bool flag4 = clickedIndex == index;
									if (flag4)
									{
										wasClickedIndexInSelection = true;
										break;
									}
								}
								this.ProcessSingleClick(clickedIndex);
								bool flag5 = !wasClickedIndexInSelection;
								if (!flag5)
								{
									bool flag6 = !this.allowSingleClickChoice && mouseButton == 0;
									if (flag6)
									{
										Action<IEnumerable<object>> action = this.itemsChosen;
										if (action != null)
										{
											action(this.selectedItems);
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag7 = this.selectionType == SelectionType.Multiple && actionKey;
						if (flag7)
						{
							bool flag8 = this.m_Selection.ContainsId(clickedItemId);
							if (flag8)
							{
								this.RemoveFromSelection(clickedIndex);
							}
							else
							{
								this.AddToSelection(clickedIndex);
							}
						}
						else
						{
							bool flag9 = this.selectionType == SelectionType.Multiple && shiftKey;
							if (flag9)
							{
								bool flag10 = this.m_Selection.indexCount == 0;
								if (flag10)
								{
									this.SetSelection(clickedIndex);
								}
								else
								{
									this.DoRangeSelection(clickedIndex);
								}
							}
							else
							{
								bool flag11 = this.selectionType == SelectionType.Multiple && this.m_Selection.ContainsIndex(clickedIndex);
								if (flag11)
								{
									Action selectionNotChanged = this.m_SelectionNotChanged;
									if (selectionNotChanged != null)
									{
										selectionNotChanged();
									}
								}
								else
								{
									bool flag12 = this.selectionType == SelectionType.Single && this.m_Selection.ContainsIndex(clickedIndex);
									if (flag12)
									{
										Action selectionNotChanged2 = this.m_SelectionNotChanged;
										if (selectionNotChanged2 != null)
										{
											selectionNotChanged2();
										}
									}
									else
									{
										this.SetSelection(clickedIndex);
									}
									bool flag13 = this.allowSingleClickChoice && mouseButton == 0;
									if (flag13)
									{
										Action<IEnumerable<object>> action2 = this.itemsChosen;
										if (action2 != null)
										{
											action2(this.selectedItems);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00019974 File Offset: 0x00017B74
		internal void DoRangeSelection(int rangeSelectionFinalIndex)
		{
			int selectionOrigin = (this.m_IsRangeSelectionDirectionUp ? this.m_Selection.maxIndex : this.m_Selection.minIndex);
			this.ClearSelectionWithoutValidation();
			List<int> range = new List<int>();
			this.m_IsRangeSelectionDirectionUp = rangeSelectionFinalIndex < selectionOrigin;
			bool isRangeSelectionDirectionUp = this.m_IsRangeSelectionDirectionUp;
			if (isRangeSelectionDirectionUp)
			{
				for (int i = rangeSelectionFinalIndex; i <= selectionOrigin; i++)
				{
					range.Add(i);
				}
			}
			else
			{
				for (int j = rangeSelectionFinalIndex; j >= selectionOrigin; j--)
				{
					range.Add(j);
				}
			}
			this.AddToSelection(range);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00019A11 File Offset: 0x00017C11
		private void ProcessSingleClick(int clickedIndex)
		{
			this.SetSelection(clickedIndex);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00019A1C File Offset: 0x00017C1C
		internal void SelectAll()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = this.selectionType != SelectionType.Multiple;
				if (!flag2)
				{
					for (int index = 0; index < this.m_ViewController.itemsSource.Count; index++)
					{
						int id = this.viewController.GetIdForIndex(index);
						object item = this.viewController.GetItemForIndex(index);
						foreach (ReusableCollectionItem recycledItem in this.activeItems)
						{
							bool flag3 = recycledItem.id == id;
							if (flag3)
							{
								recycledItem.SetSelected(true);
							}
						}
						bool flag4 = !this.m_Selection.ContainsId(id);
						if (flag4)
						{
							this.m_Selection.AddId(id);
							this.m_Selection.AddIndex(index, item);
						}
					}
					this.NotifyOfSelectionChange();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00019B34 File Offset: 0x00017D34
		public void AddToSelection(int index)
		{
			this.AddToSelection(new int[] { index });
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00019B48 File Offset: 0x00017D48
		internal void AddToSelection(IList<int> indexes)
		{
			bool flag = !this.HasValidDataAndBindings() || indexes == null || indexes.Count == 0;
			if (!flag)
			{
				foreach (int index in indexes)
				{
					this.AddToSelectionWithoutValidation(index);
				}
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00019BC4 File Offset: 0x00017DC4
		private void AddToSelectionWithoutValidation(int index)
		{
			bool flag = this.m_Selection.ContainsIndex(index);
			if (!flag)
			{
				int id = this.viewController.GetIdForIndex(index);
				object item = this.viewController.GetItemForIndex(index);
				foreach (ReusableCollectionItem recycledItem in this.activeItems)
				{
					bool flag2 = recycledItem.id == id;
					if (flag2)
					{
						recycledItem.SetSelected(true);
					}
				}
				this.m_Selection.AddId(id);
				this.m_Selection.AddIndex(index, item);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00019C70 File Offset: 0x00017E70
		public void RemoveFromSelection(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.RemoveFromSelectionWithoutValidation(index);
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00019CA4 File Offset: 0x00017EA4
		private void RemoveFromSelectionWithoutValidation(int index)
		{
			bool flag = !this.m_Selection.TryRemove(index);
			if (!flag)
			{
				int id = this.viewController.GetIdForIndex(index);
				foreach (ReusableCollectionItem recycledItem in this.activeItems)
				{
					bool flag2 = recycledItem.id == id;
					if (flag2)
					{
						recycledItem.SetSelected(false);
					}
				}
				this.m_Selection.RemoveId(id);
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00019D34 File Offset: 0x00017F34
		public void SetSelection(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				this.ClearSelection();
			}
			else
			{
				this.SetSelection(new int[] { index });
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00019D65 File Offset: 0x00017F65
		public void SetSelection(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, true);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00019D71 File Offset: 0x00017F71
		public void SetSelectionWithoutNotify(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, false);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00019D80 File Offset: 0x00017F80
		internal void SetSelectionInternal(IEnumerable<int> indices, bool sendNotification)
		{
			bool flag = !this.HasValidDataAndBindings() || indices == null;
			if (!flag)
			{
				bool flag2 = this.MatchesExistingSelection(indices);
				if (!flag2)
				{
					this.ClearSelectionWithoutValidation();
					ICollection collection = indices as ICollection;
					bool flag3 = collection != null && this.m_Selection.capacity < collection.Count;
					if (flag3)
					{
						this.m_Selection.capacity = collection.Count;
					}
					foreach (int index in indices)
					{
						this.AddToSelectionWithoutValidation(index);
					}
					if (sendNotification)
					{
						this.NotifyOfSelectionChange();
					}
					base.SaveViewData();
				}
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00019E50 File Offset: 0x00018050
		private bool MatchesExistingSelection(IEnumerable<int> indices)
		{
			IList<int> indicesCollection = indices as IList<int>;
			List<int> pooled = null;
			bool flag3;
			try
			{
				bool flag = indicesCollection == null;
				if (flag)
				{
					pooled = CollectionPool<List<int>, int>.Get();
					pooled.AddRange(indices);
					indicesCollection = pooled;
				}
				bool flag2 = indicesCollection.Count != this.m_Selection.indexCount;
				if (flag2)
				{
					flag3 = false;
				}
				else
				{
					for (int i = 0; i < indicesCollection.Count; i++)
					{
						bool flag4 = indicesCollection[i] != this.m_Selection.indices[i];
						if (flag4)
						{
							return false;
						}
					}
					flag3 = true;
				}
			}
			finally
			{
				bool flag5 = pooled != null;
				if (flag5)
				{
					CollectionPool<List<int>, int>.Release(pooled);
				}
			}
			return flag3;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00019F14 File Offset: 0x00018114
		private void NotifyOfSelectionChange()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				Action<IEnumerable<object>> action = this.selectionChanged;
				if (action != null)
				{
					action(this.selectedItems);
				}
				Action<IEnumerable<int>> action2 = this.selectedIndicesChanged;
				if (action2 != null)
				{
					action2(this.m_Selection.indices);
				}
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00019F68 File Offset: 0x00018168
		public void ClearSelection()
		{
			bool flag = !this.HasValidDataAndBindings() || this.m_Selection.idCount == 0;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00019FA4 File Offset: 0x000181A4
		private void ClearSelectionWithoutValidation()
		{
			foreach (ReusableCollectionItem recycledItem in this.activeItems)
			{
				recycledItem.SetSelected(false);
			}
			this.m_Selection.Clear();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001A004 File Offset: 0x00018204
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string key = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, key);
			this.m_ScrollView.UpdateContentViewTransform();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001A038 File Offset: 0x00018238
		[EventInterest(new Type[]
		{
			typeof(PointerUpEvent),
			typeof(FocusInEvent),
			typeof(FocusOutEvent),
			typeof(NavigationSubmitEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
			if (flag)
			{
				ListViewDragger dragger = this.m_Dragger;
				if (dragger != null)
				{
					dragger.OnPointerUpEvent((PointerUpEvent)evt);
				}
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
				if (flag2)
				{
					CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
					if (virtualizationController != null)
					{
						virtualizationController.OnFocusIn(evt.elementTarget);
					}
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusOutEvent>.TypeId();
					if (flag3)
					{
						CollectionVirtualizationController virtualizationController2 = this.m_VirtualizationController;
						if (virtualizationController2 != null)
						{
							virtualizationController2.OnFocusOut(((FocusOutEvent)evt).relatedTarget as VisualElement);
						}
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = evt.target == this;
							if (flag5)
							{
								this.m_ScrollView.contentContainer.Focus();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000020EA File Offset: 0x000002EA
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("ExecuteDefaultAction override has been removed because default event handling was migrated to HandleEventBubbleUp. Please use HandleEventBubbleUp.", false)]
		protected override void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001A118 File Offset: 0x00018318
		private void OnSizeChanged(GeometryChangedEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = Mathf.Approximately(evt.newRect.width, evt.oldRect.width) && Mathf.Approximately(evt.newRect.height, evt.oldRect.height);
				if (!flag2)
				{
					this.Resize(evt.newRect.size);
				}
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001A198 File Offset: 0x00018398
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int height;
			bool flag = !this.m_ItemHeightIsInline && e.customStyle.TryGetValue(BaseVerticalCollectionView.s_ItemHeightProperty, out height);
			if (flag)
			{
				bool flag2 = Math.Abs(this.m_FixedItemHeight - (float)height) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = (float)height;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000020EA File Offset: 0x000002EA
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001A1F3 File Offset: 0x000183F3
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_Selection.selectedIds = this.m_SelectedIds;
			this.RefreshItems();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001A3E4 File Offset: 0x000185E4
		[CompilerGenerated]
		private void <RefreshSelection>g__NotifyIfChanged|191_0(ref BaseVerticalCollectionView.<>c__DisplayClass191_0 A_1)
		{
			bool flag = A_1.selectedIndicesChanged || this.m_Selection.indexCount != A_1.previousSelectionCount;
			if (flag)
			{
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001A420 File Offset: 0x00018620
		[CompilerGenerated]
		private void <Apply>g__HandleSelectionAndScroll|202_0(int index, ref BaseVerticalCollectionView.<>c__DisplayClass202_0 A_2)
		{
			bool flag = index < 0 || index >= this.m_ViewController.itemsSource.Count;
			if (!flag)
			{
				bool flag2 = ((this.selectionType == SelectionType.Multiple) & A_2.shiftKey) && this.m_Selection.indexCount != 0;
				if (flag2)
				{
					this.DoRangeSelection(index);
				}
				else
				{
					this.selectedIndex = index;
				}
				this.ScrollToItem(index);
			}
		}

		// Token: 0x040002D4 RID: 724
		internal static readonly BindingId itemsSourceProperty = "itemsSource";

		// Token: 0x040002D5 RID: 725
		internal static readonly BindingId selectionTypeProperty = "selectionType";

		// Token: 0x040002D6 RID: 726
		internal static readonly BindingId selectedItemProperty = "selectedItem";

		// Token: 0x040002D7 RID: 727
		internal static readonly BindingId selectedItemsProperty = "selectedItems";

		// Token: 0x040002D8 RID: 728
		internal static readonly BindingId selectedIndexProperty = "selectedIndex";

		// Token: 0x040002D9 RID: 729
		internal static readonly BindingId selectedIndicesProperty = "selectedIndices";

		// Token: 0x040002DA RID: 730
		internal static readonly BindingId showBorderProperty = "showBorder";

		// Token: 0x040002DB RID: 731
		internal static readonly BindingId reorderableProperty = "reorderable";

		// Token: 0x040002DC RID: 732
		internal static readonly BindingId horizontalScrollingEnabledProperty = "horizontalScrollingEnabled";

		// Token: 0x040002DD RID: 733
		internal static readonly BindingId showAlternatingRowBackgroundsProperty = "showAlternatingRowBackgrounds";

		// Token: 0x040002DE RID: 734
		internal static readonly BindingId virtualizationMethodProperty = "virtualizationMethod";

		// Token: 0x040002DF RID: 735
		internal static readonly BindingId fixedItemHeightProperty = "fixedItemHeight";

		// Token: 0x040002E0 RID: 736
		private static readonly ProfilerMarker k_RefreshMarker = new ProfilerMarker("BaseVerticalCollectionView.RefreshItems");

		// Token: 0x040002E1 RID: 737
		private static readonly ProfilerMarker k_RebuildMarker = new ProfilerMarker("BaseVerticalCollectionView.Rebuild");

		// Token: 0x040002E2 RID: 738
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IEnumerable<object>> itemsChosen;

		// Token: 0x040002E3 RID: 739
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IEnumerable<object>> selectionChanged;

		// Token: 0x040002E4 RID: 740
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<int>> selectedIndicesChanged;

		// Token: 0x040002E5 RID: 741
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<int, int> itemIndexChanged;

		// Token: 0x040002E6 RID: 742
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action itemsSourceChanged;

		// Token: 0x040002E7 RID: 743
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action m_SelectionNotChanged = delegate
		{
		};

		// Token: 0x040002E8 RID: 744
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<CanStartDragArgs, bool> canStartDrag;

		// Token: 0x040002E9 RID: 745
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<SetupDragAndDropArgs, StartDragArgs> setupDragAndDrop;

		// Token: 0x040002EA RID: 746
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<HandleDragAndDropArgs, DragVisualMode> dragAndDropUpdate;

		// Token: 0x040002EB RID: 747
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<HandleDragAndDropArgs, DragVisualMode> handleDrop;

		// Token: 0x040002EC RID: 748
		private SelectionType m_SelectionType;

		// Token: 0x040002ED RID: 749
		private static readonly List<ReusableCollectionItem> k_EmptyItems = new List<ReusableCollectionItem>();

		// Token: 0x040002EE RID: 750
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool allowSingleClickChoice = false;

		// Token: 0x040002EF RID: 751
		private bool m_HorizontalScrollingEnabled;

		// Token: 0x040002F0 RID: 752
		[DontCreateProperty]
		[SerializeField]
		private AlternatingRowBackground m_ShowAlternatingRowBackgrounds = AlternatingRowBackground.None;

		// Token: 0x040002F1 RID: 753
		internal static readonly string k_InvalidTemplateError = "Template Not Found";

		// Token: 0x040002F2 RID: 754
		internal float m_FixedItemHeight = 22f;

		// Token: 0x040002F3 RID: 755
		internal bool m_ItemHeightIsInline;

		// Token: 0x040002F4 RID: 756
		private CollectionVirtualizationMethod m_VirtualizationMethod;

		// Token: 0x040002F5 RID: 757
		private readonly ScrollView m_ScrollView;

		// Token: 0x040002F6 RID: 758
		private CollectionViewController m_ViewController;

		// Token: 0x040002F7 RID: 759
		private CollectionVirtualizationController m_VirtualizationController;

		// Token: 0x040002F8 RID: 760
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x040002F9 RID: 761
		[SerializeField]
		[DontCreateProperty]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal SerializedVirtualizationData serializedVirtualizationData = new SerializedVirtualizationData();

		// Token: 0x040002FA RID: 762
		[DontCreateProperty]
		[SerializeField]
		private List<int> m_SelectedIds = new List<int>();

		// Token: 0x040002FB RID: 763
		private readonly BaseVerticalCollectionView.Selection m_Selection;

		// Token: 0x040002FC RID: 764
		private float m_LastHeight;

		// Token: 0x040002FD RID: 765
		private bool m_IsRangeSelectionDirectionUp;

		// Token: 0x040002FE RID: 766
		private ListViewDragger m_Dragger;

		// Token: 0x040002FF RID: 767
		internal static CustomStyleProperty<int> s_ItemHeightProperty = new CustomStyleProperty<int>("--unity-item-height");

		// Token: 0x04000300 RID: 768
		private Action<int, int> m_ItemIndexChangedCallback;

		// Token: 0x04000301 RID: 769
		private Action m_ItemsSourceChangedCallback;

		// Token: 0x04000302 RID: 770
		internal IVisualElementScheduledItem m_RebuildScheduled;

		// Token: 0x04000303 RID: 771
		public static readonly string ussClassName = "unity-collection-view";

		// Token: 0x04000304 RID: 772
		public static readonly string borderUssClassName = BaseVerticalCollectionView.ussClassName + "--with-border";

		// Token: 0x04000305 RID: 773
		public static readonly string itemUssClassName = BaseVerticalCollectionView.ussClassName + "__item";

		// Token: 0x04000306 RID: 774
		public static readonly string dragHoverBarUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-bar";

		// Token: 0x04000307 RID: 775
		public static readonly string dragHoverMarkerUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-marker";

		// Token: 0x04000308 RID: 776
		public static readonly string itemDragHoverUssClassName = BaseVerticalCollectionView.itemUssClassName + "--drag-hover";

		// Token: 0x04000309 RID: 777
		public static readonly string itemSelectedVariantUssClassName = BaseVerticalCollectionView.itemUssClassName + "--selected";

		// Token: 0x0400030A RID: 778
		public static readonly string itemAlternativeBackgroundUssClassName = BaseVerticalCollectionView.itemUssClassName + "--alternative-background";

		// Token: 0x0400030B RID: 779
		public static readonly string listScrollViewUssClassName = BaseVerticalCollectionView.ussClassName + "__scroll-view";

		// Token: 0x0400030C RID: 780
		internal static readonly string backgroundFillUssClassName = BaseVerticalCollectionView.ussClassName + "__background-fill";

		// Token: 0x0400030D RID: 781
		internal int m_PreviousRefreshedCount;

		// Token: 0x0400030E RID: 782
		private Vector3 m_TouchDownPosition;

		// Token: 0x02000085 RID: 133
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000539 RID: 1337 RVA: 0x0001A498 File Offset: 0x00018698
			public UxmlTraits()
			{
				base.focusable.defaultValue = true;
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x0001A5AC File Offset: 0x000187AC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int itemHeight = 0;
				BaseVerticalCollectionView view = (BaseVerticalCollectionView)ve;
				view.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				bool flag = this.m_FixedItemHeight.TryGetValueFromBag(bag, cc, ref itemHeight);
				if (flag)
				{
					view.fixedItemHeight = (float)itemHeight;
				}
				view.virtualizationMethod = this.m_VirtualizationMethod.GetValueFromBag(bag, cc);
				view.showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				view.selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				view.showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
				view.horizontalScrollingEnabled = this.m_HorizontalScrollingEnabled.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400030F RID: 783
			private readonly UxmlEnumAttributeDescription<CollectionVirtualizationMethod> m_VirtualizationMethod = new UxmlEnumAttributeDescription<CollectionVirtualizationMethod>
			{
				name = "virtualization-method",
				defaultValue = CollectionVirtualizationMethod.FixedHeight
			};

			// Token: 0x04000310 RID: 784
			private readonly UxmlIntAttributeDescription m_FixedItemHeight = new UxmlIntAttributeDescription
			{
				name = "fixed-item-height",
				obsoleteNames = new string[] { "itemHeight, item-height" },
				defaultValue = 22
			};

			// Token: 0x04000311 RID: 785
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x04000312 RID: 786
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x04000313 RID: 787
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			// Token: 0x04000314 RID: 788
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};

			// Token: 0x04000315 RID: 789
			private readonly UxmlBoolAttributeDescription m_HorizontalScrollingEnabled = new UxmlBoolAttributeDescription
			{
				name = "horizontal-scrolling",
				defaultValue = false
			};
		}

		// Token: 0x02000086 RID: 134
		private class Selection
		{
			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001A663 File Offset: 0x00018863
			// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001A66B File Offset: 0x0001886B
			public List<int> selectedIds { get; set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001A674 File Offset: 0x00018874
			public int indexCount
			{
				get
				{
					return this.indices.Count;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001A681 File Offset: 0x00018881
			public int idCount
			{
				get
				{
					return this.selectedIds.Count;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001A690 File Offset: 0x00018890
			public int minIndex
			{
				get
				{
					bool flag = this.m_MinIndex == -1;
					if (flag)
					{
						this.m_MinIndex = this.indices.Min();
					}
					return this.m_MinIndex;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001A6C8 File Offset: 0x000188C8
			public int maxIndex
			{
				get
				{
					bool flag = this.m_MaxIndex == -1;
					if (flag)
					{
						this.m_MaxIndex = this.indices.Max();
					}
					return this.m_MaxIndex;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001A6FE File Offset: 0x000188FE
			// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001A70C File Offset: 0x0001890C
			public int capacity
			{
				get
				{
					return this.indices.Capacity;
				}
				set
				{
					this.indices.Capacity = value;
					bool flag = this.selectedIds.Capacity < value;
					if (flag)
					{
						this.selectedIds.Capacity = value;
					}
				}
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x0001A746 File Offset: 0x00018946
			public int FirstIndex()
			{
				return (this.indices.Count > 0) ? this.indices[0] : (-1);
			}

			// Token: 0x06000544 RID: 1348 RVA: 0x0001A768 File Offset: 0x00018968
			public object FirstObject()
			{
				object obj;
				return this.items.TryGetValue(this.FirstIndex(), out obj) ? obj : null;
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x0001A78E File Offset: 0x0001898E
			public bool ContainsIndex(int index)
			{
				return this.m_IndexLookup.Contains(index);
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x0001A79C File Offset: 0x0001899C
			public bool ContainsId(int id)
			{
				return this.m_IdLookup.Contains(id);
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x0001A7AA File Offset: 0x000189AA
			public void AddId(int id)
			{
				this.selectedIds.Add(id);
				this.m_IdLookup.Add(id);
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x0001A7C8 File Offset: 0x000189C8
			public void AddIndex(int index, object obj)
			{
				this.m_IndexLookup.Add(index);
				this.indices.Add(index);
				this.items[index] = obj;
				bool flag = index < this.m_MinIndex;
				if (flag)
				{
					this.m_MinIndex = index;
				}
				bool flag2 = index > this.m_MaxIndex;
				if (flag2)
				{
					this.m_MaxIndex = index;
				}
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x0001A828 File Offset: 0x00018A28
			public bool TryRemove(int index)
			{
				bool flag = !this.m_IndexLookup.Remove(index);
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int i = this.indices.IndexOf(index);
					bool flag3 = i >= 0;
					if (flag3)
					{
						this.indices.RemoveAt(i);
						this.items.Remove(index);
						bool flag4 = index == this.m_MinIndex;
						if (flag4)
						{
							this.m_MinIndex = -1;
						}
						bool flag5 = index == this.m_MaxIndex;
						if (flag5)
						{
							this.m_MaxIndex = -1;
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x0001A8B2 File Offset: 0x00018AB2
			public void RemoveId(int id)
			{
				this.selectedIds.Remove(id);
				this.m_IdLookup.Remove(id);
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x0001A8CF File Offset: 0x00018ACF
			public void ClearItems()
			{
				this.items.Clear();
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x0001A8DE File Offset: 0x00018ADE
			public void ClearIds()
			{
				this.m_IdLookup.Clear();
				this.selectedIds.Clear();
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x0001A8F9 File Offset: 0x00018AF9
			public void ClearIndices()
			{
				this.m_IndexLookup.Clear();
				this.indices.Clear();
				this.m_MinIndex = -1;
				this.m_MaxIndex = -1;
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x0001A922 File Offset: 0x00018B22
			public void Clear()
			{
				this.ClearItems();
				this.ClearIds();
				this.ClearIndices();
			}

			// Token: 0x04000316 RID: 790
			private readonly HashSet<int> m_IndexLookup = new HashSet<int>();

			// Token: 0x04000317 RID: 791
			private readonly HashSet<int> m_IdLookup = new HashSet<int>();

			// Token: 0x04000318 RID: 792
			private int m_MinIndex = -1;

			// Token: 0x04000319 RID: 793
			private int m_MaxIndex = -1;

			// Token: 0x0400031B RID: 795
			public readonly List<int> indices = new List<int>();

			// Token: 0x0400031C RID: 796
			public readonly Dictionary<int, object> items = new Dictionary<int, object>();
		}
	}
}
