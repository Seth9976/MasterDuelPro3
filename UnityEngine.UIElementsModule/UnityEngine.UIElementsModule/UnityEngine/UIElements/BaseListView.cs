using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000074 RID: 116
	public abstract class BaseListView : BaseVerticalCollectionView
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00014358 File Offset: 0x00012558
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00014360 File Offset: 0x00012560
		[CreateProperty]
		public bool showBoundCollectionSize
		{
			get
			{
				return this.m_ShowBoundCollectionSize;
			}
			set
			{
				bool flag = this.m_ShowBoundCollectionSize == value;
				if (!flag)
				{
					this.m_ShowBoundCollectionSize = value;
					this.SetupArraySizeField();
					base.NotifyPropertyChanged(in BaseListView.showBoundCollectionSizeProperty);
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00014397 File Offset: 0x00012597
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000143A0 File Offset: 0x000125A0
		[CreateProperty]
		public bool showFoldoutHeader
		{
			get
			{
				return this.m_ShowFoldoutHeader;
			}
			set
			{
				bool previous = this.m_ShowFoldoutHeader;
				this.m_ShowFoldoutHeader = value;
				try
				{
					bool flag = this.makeHeader != null;
					if (!flag)
					{
						base.EnableInClassList(BaseListView.listViewWithHeaderUssClassName, value);
						bool showFoldoutHeader = this.m_ShowFoldoutHeader;
						if (showFoldoutHeader)
						{
							this.AddFoldout();
						}
						else
						{
							bool flag2 = this.m_Foldout != null;
							if (flag2)
							{
								VisualElement visualElement = this.drawnFooter;
								if (visualElement != null)
								{
									visualElement.RemoveFromHierarchy();
								}
								this.RemoveFoldout();
							}
						}
						this.SetupArraySizeField();
						this.UpdateListViewLabel();
						bool flag3 = this.makeFooter == null;
						if (flag3)
						{
							bool showAddRemoveFooter = this.showAddRemoveFooter;
							if (showAddRemoveFooter)
							{
								this.EnableFooter(true);
							}
						}
						else
						{
							bool showFoldoutHeader2 = this.m_ShowFoldoutHeader;
							if (showFoldoutHeader2)
							{
								VisualElement visualElement2 = this.drawnFooter;
								if (visualElement2 != null)
								{
									visualElement2.RemoveFromHierarchy();
								}
								Foldout foldout = this.m_Foldout;
								if (foldout != null)
								{
									foldout.contentContainer.Add(this.drawnFooter);
								}
							}
							else
							{
								base.hierarchy.Add(this.drawnFooter);
								base.hierarchy.BringToFront(this.drawnFooter);
							}
						}
					}
				}
				finally
				{
					bool flag4 = previous != this.m_ShowFoldoutHeader;
					if (flag4)
					{
						base.NotifyPropertyChanged(in BaseListView.showFoldoutHeaderProperty);
					}
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014500 File Offset: 0x00012700
		private void AddFoldout()
		{
			bool flag = this.m_Foldout != null;
			if (!flag)
			{
				this.m_Foldout = new Foldout
				{
					name = BaseListView.foldoutHeaderUssClassName,
					text = this.m_HeaderTitle
				};
				this.m_Foldout.toggle.tabIndex = 10;
				this.m_Foldout.toggle.acceptClicksIfDisabled = true;
				this.m_Foldout.AddToClassList(BaseListView.foldoutHeaderUssClassName);
				base.hierarchy.Add(this.m_Foldout);
				this.m_Foldout.Add(base.scrollView);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000145A4 File Offset: 0x000127A4
		private void RemoveFoldout()
		{
			Foldout foldout = this.m_Foldout;
			if (foldout != null)
			{
				foldout.RemoveFromHierarchy();
			}
			this.m_Foldout = null;
			base.hierarchy.Add(base.scrollView);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000145E0 File Offset: 0x000127E0
		internal void SetupArraySizeField()
		{
			bool flag = !this.showBoundCollectionSize || (!this.showFoldoutHeader && base.GetProperty("__unity-collection-view-internal-binding") == null) || this.drawnHeader != null;
			if (flag)
			{
				TextField arraySizeField = this.m_ArraySizeField;
				if (arraySizeField != null)
				{
					arraySizeField.RemoveFromHierarchy();
				}
			}
			else
			{
				bool flag2 = this.m_ArraySizeField == null;
				if (flag2)
				{
					this.m_ArraySizeField = new TextField
					{
						name = BaseListView.arraySizeFieldUssClassName,
						tabIndex = 20
					};
					this.m_ArraySizeField.AddToClassList(BaseListView.arraySizeFieldUssClassName);
					this.m_ArraySizeField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnArraySizeFieldChanged));
					this.m_ArraySizeField.isDelayed = true;
					this.m_ArraySizeField.focusable = true;
				}
				this.m_ArraySizeField.EnableInClassList(BaseListView.arraySizeFieldWithFooterUssClassName, this.showAddRemoveFooter);
				this.m_ArraySizeField.EnableInClassList(BaseListView.arraySizeFieldWithHeaderUssClassName, this.showFoldoutHeader);
				bool showFoldoutHeader = this.showFoldoutHeader;
				if (showFoldoutHeader)
				{
					this.m_ArraySizeField.label = string.Empty;
					base.hierarchy.Add(this.m_ArraySizeField);
				}
				else
				{
					this.m_ArraySizeField.label = BaseListView.k_SizeFieldLabel;
					base.hierarchy.Insert(0, this.m_ArraySizeField);
				}
				this.UpdateArraySizeField();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001473C File Offset: 0x0001293C
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00014744 File Offset: 0x00012944
		[CreateProperty]
		public string headerTitle
		{
			get
			{
				return this.m_HeaderTitle;
			}
			set
			{
				string previous = this.m_HeaderTitle;
				this.m_HeaderTitle = value;
				bool flag = this.m_Foldout != null;
				if (flag)
				{
					this.m_Foldout.text = this.m_HeaderTitle;
				}
				bool flag2 = string.CompareOrdinal(previous, this.m_HeaderTitle) != 0;
				if (flag2)
				{
					base.NotifyPropertyChanged(in BaseListView.headerTitleProperty);
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001479E File Offset: 0x0001299E
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x000147A8 File Offset: 0x000129A8
		[CreateProperty]
		public Func<VisualElement> makeHeader
		{
			get
			{
				return this.m_MakeHeader;
			}
			set
			{
				bool flag = value == this.m_MakeHeader;
				if (!flag)
				{
					this.RemoveFoldout();
					this.m_MakeHeader = value;
					bool flag2 = this.m_MakeHeader != null;
					if (flag2)
					{
						this.SetupArraySizeField();
						this.drawnHeader = this.m_MakeHeader();
						this.drawnHeader.tabIndex = 1;
						base.hierarchy.Add(this.drawnHeader);
						base.hierarchy.SendToBack(this.drawnHeader);
					}
					else
					{
						VisualElement visualElement = this.drawnHeader;
						if (visualElement != null)
						{
							visualElement.RemoveFromHierarchy();
						}
						this.drawnHeader = null;
						bool showFoldoutHeader = this.showFoldoutHeader;
						if (showFoldoutHeader)
						{
							this.AddFoldout();
							this.SetupArraySizeField();
							this.UpdateListViewLabel();
						}
					}
					bool flag3 = this.drawnFooter != null;
					if (flag3)
					{
						bool flag4 = this.m_Foldout != null;
						if (flag4)
						{
							this.drawnFooter.RemoveFromHierarchy();
							this.m_Foldout.contentContainer.hierarchy.Add(this.drawnFooter);
						}
						else
						{
							base.hierarchy.Add(this.drawnFooter);
							VisualElement visualElement2 = this.drawnFooter;
							if (visualElement2 != null)
							{
								visualElement2.BringToFront();
							}
						}
					}
					else
					{
						this.EnableFooter(this.showAddRemoveFooter);
					}
					base.NotifyPropertyChanged(in BaseListView.makeHeaderProperty);
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001490A File Offset: 0x00012B0A
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00014914 File Offset: 0x00012B14
		[CreateProperty]
		public Func<VisualElement> makeFooter
		{
			get
			{
				return this.m_MakeFooter;
			}
			set
			{
				bool flag = value == this.m_MakeFooter;
				if (!flag)
				{
					this.m_MakeFooter = value;
					bool flag2 = this.m_MakeFooter != null;
					if (flag2)
					{
						VisualElement footer = this.m_Footer;
						if (footer != null)
						{
							footer.RemoveFromHierarchy();
						}
						this.m_Footer = null;
						this.drawnFooter = this.m_MakeFooter();
						bool flag3 = this.m_Foldout != null;
						if (flag3)
						{
							this.m_Foldout.contentContainer.Add(this.drawnFooter);
						}
						else
						{
							base.hierarchy.Add(this.drawnFooter);
							base.hierarchy.BringToFront(this.drawnFooter);
						}
						base.EnableInClassList(BaseListView.listViewWithFooterUssClassName, true);
						base.scrollView.EnableInClassList(BaseListView.scrollViewWithFooterUssClassName, true);
					}
					else
					{
						VisualElement visualElement = this.drawnFooter;
						if (visualElement != null)
						{
							visualElement.RemoveFromHierarchy();
						}
						this.drawnFooter = null;
						this.EnableFooter(this.m_ShowAddRemoveFooter);
					}
					base.NotifyPropertyChanged(in BaseListView.makeFooterProperty);
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00014A23 File Offset: 0x00012C23
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00014A30 File Offset: 0x00012C30
		[CreateProperty]
		public bool showAddRemoveFooter
		{
			get
			{
				return this.m_Footer != null;
			}
			set
			{
				bool previous = this.showAddRemoveFooter;
				this.m_ShowAddRemoveFooter = value;
				bool flag = this.makeFooter == null;
				if (flag)
				{
					this.EnableFooter(value);
				}
				bool flag2 = value && this.m_ArraySizeField != null;
				if (flag2)
				{
					this.m_ArraySizeField.AddToClassList(BaseListView.arraySizeFieldWithFooterUssClassName);
				}
				bool flag3 = previous != this.showFoldoutHeader;
				if (flag3)
				{
					base.NotifyPropertyChanged(in BaseListView.showAddRemoveFooterProperty);
				}
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00014AA4 File Offset: 0x00012CA4
		private void EnableFooter(bool enabled)
		{
			base.EnableInClassList(BaseListView.listViewWithFooterUssClassName, enabled);
			base.scrollView.EnableInClassList(BaseListView.scrollViewWithFooterUssClassName, enabled);
			if (enabled)
			{
				bool flag = this.m_Footer == null;
				if (flag)
				{
					this.m_Footer = new VisualElement
					{
						name = BaseListView.footerUssClassName
					};
					this.m_Footer.AddToClassList(BaseListView.footerUssClassName);
					this.m_AddButton = new Button(new Action(this.OnAddClicked))
					{
						name = BaseListView.footerAddButtonName,
						text = "+"
					};
					this.m_AddButton.SetEnabled(this.allowAdd);
					this.m_Footer.Add(this.m_AddButton);
					this.m_RemoveButton = new Button(new Action(this.OnRemoveClicked))
					{
						name = BaseListView.footerRemoveButtonName,
						text = "-"
					};
					this.m_RemoveButton.SetEnabled(this.allowRemove);
					this.m_Footer.Add(this.m_RemoveButton);
				}
				bool flag2 = this.m_Foldout != null;
				if (flag2)
				{
					this.m_Foldout.contentContainer.Add(this.m_Footer);
				}
				else
				{
					base.hierarchy.Add(this.m_Footer);
				}
			}
			else
			{
				Button removeButton = this.m_RemoveButton;
				if (removeButton != null)
				{
					removeButton.RemoveFromHierarchy();
				}
				Button addButton = this.m_AddButton;
				if (addButton != null)
				{
					addButton.RemoveFromHierarchy();
				}
				VisualElement footer = this.m_Footer;
				if (footer != null)
				{
					footer.RemoveFromHierarchy();
				}
				this.m_RemoveButton = null;
				this.m_AddButton = null;
				this.m_Footer = null;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00014C48 File Offset: 0x00012E48
		private IVisualElementScheduledItem trackItemCount
		{
			get
			{
				bool flag = this.m_TrackedItem != null;
				IVisualElementScheduledItem visualElementScheduledItem;
				if (flag)
				{
					visualElementScheduledItem = this.m_TrackedItem;
				}
				else
				{
					this.m_TrackedItem = base.schedule.Execute(this.trackCount).Until(this.untilManualBindingSourceSelectionMode);
					visualElementScheduledItem = this.m_TrackedItem;
				}
				return visualElementScheduledItem;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00014C98 File Offset: 0x00012E98
		private Action trackCount
		{
			get
			{
				Action action;
				if ((action = this.m_TrackCount) == null)
				{
					action = (this.m_TrackCount = delegate
					{
						IList itemsSource = base.itemsSource;
						int? num = ((itemsSource != null) ? new int?(itemsSource.Count) : null);
						int previousRefreshedCount = this.m_PreviousRefreshedCount;
						bool flag = !((num.GetValueOrDefault() == previousRefreshedCount) & (num != null));
						if (flag)
						{
							base.RefreshItems();
						}
					});
				}
				return action;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00014CC4 File Offset: 0x00012EC4
		private Func<bool> untilManualBindingSourceSelectionMode
		{
			get
			{
				Func<bool> func;
				if ((func = this.m_WhileAutoAssign) == null)
				{
					func = (this.m_WhileAutoAssign = () => !this.autoAssignSource);
				}
				return func;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014CF0 File Offset: 0x00012EF0
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00014CF8 File Offset: 0x00012EF8
		[CreateProperty]
		public BindingSourceSelectionMode bindingSourceSelectionMode
		{
			get
			{
				return this.m_BindingSourceSelectionMode;
			}
			set
			{
				bool flag = this.m_BindingSourceSelectionMode == value;
				if (!flag)
				{
					this.m_BindingSourceSelectionMode = value;
					base.Rebuild();
					base.NotifyPropertyChanged(in BaseListView.bindingSourceSelectionModeProperty);
					bool autoAssignSource = this.autoAssignSource;
					if (autoAssignSource)
					{
						this.trackItemCount.Resume();
					}
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014D45 File Offset: 0x00012F45
		internal bool autoAssignSource
		{
			get
			{
				return this.bindingSourceSelectionMode == BindingSourceSelectionMode.AutoAssign;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00014D50 File Offset: 0x00012F50
		private void AddItems(int itemCount)
		{
			this.viewController.AddItems(itemCount);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014D60 File Offset: 0x00012F60
		private void OnArraySizeFieldChanged(ChangeEvent<string> evt)
		{
			bool flag = this.m_ArraySizeField.showMixedValue && BaseField<string>.mixedValueString == evt.newValue;
			if (!flag)
			{
				int value;
				bool flag2 = !int.TryParse(evt.newValue, out value) || value < 0;
				if (flag2)
				{
					this.m_ArraySizeField.SetValueWithoutNotify(evt.previousValue);
				}
				else
				{
					int count = this.viewController.GetItemsCount();
					bool flag3 = count == 0 && value == this.viewController.GetItemsMinCount();
					if (!flag3)
					{
						bool flag4 = value > count;
						if (flag4)
						{
							this.viewController.AddItems(value - count);
						}
						else
						{
							bool flag5 = value < count;
							if (flag5)
							{
								this.viewController.RemoveItems(count - value);
							}
							else
							{
								bool flag6 = value == 0;
								if (flag6)
								{
									this.viewController.ClearItems();
									this.m_IsOverMultiEditLimit = false;
								}
							}
						}
						this.UpdateListViewLabel();
					}
				}
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00014E54 File Offset: 0x00013054
		internal void UpdateArraySizeField()
		{
			bool flag = !this.HasValidDataAndBindings() || this.m_ArraySizeField == null;
			if (!flag)
			{
				bool flag2 = !this.m_ArraySizeField.showMixedValue;
				if (flag2)
				{
					this.m_ArraySizeField.SetValueWithoutNotify(this.viewController.GetItemsMinCount().ToString());
				}
				VisualElement footer = this.footer;
				if (footer != null)
				{
					footer.SetEnabled(!this.m_IsOverMultiEditLimit);
				}
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00014EC8 File Offset: 0x000130C8
		internal void UpdateListViewLabel()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool noItemsCount = base.itemsSource.Count == 0;
				bool isOverMultiEditLimit = this.m_IsOverMultiEditLimit;
				if (isOverMultiEditLimit)
				{
					if (this.m_ListViewLabel == null)
					{
						this.m_ListViewLabel = new Label();
					}
					this.m_ListViewLabel.text = this.m_MaxMultiEditStr;
					base.scrollView.contentViewport.Add(this.m_ListViewLabel);
				}
				else
				{
					bool flag2 = noItemsCount;
					if (flag2)
					{
						bool flag3 = this.m_MakeNoneElement != null;
						if (flag3)
						{
							if (this.m_NoneElement == null)
							{
								this.m_NoneElement = this.m_MakeNoneElement();
							}
							base.scrollView.contentViewport.Add(this.m_NoneElement);
							Label listViewLabel = this.m_ListViewLabel;
							if (listViewLabel != null)
							{
								listViewLabel.RemoveFromHierarchy();
							}
							this.m_ListViewLabel = null;
						}
						else
						{
							if (this.m_ListViewLabel == null)
							{
								this.m_ListViewLabel = new Label();
							}
							this.m_ListViewLabel.text = BaseListView.k_EmptyListStr;
							base.scrollView.contentViewport.Add(this.m_ListViewLabel);
							VisualElement noneElement = this.m_NoneElement;
							if (noneElement != null)
							{
								noneElement.RemoveFromHierarchy();
							}
							this.m_NoneElement = null;
						}
					}
					else
					{
						VisualElement noneElement2 = this.m_NoneElement;
						if (noneElement2 != null)
						{
							noneElement2.RemoveFromHierarchy();
						}
						this.m_NoneElement = null;
						Label listViewLabel2 = this.m_ListViewLabel;
						if (listViewLabel2 != null)
						{
							listViewLabel2.RemoveFromHierarchy();
						}
						this.m_ListViewLabel = null;
					}
				}
				Label listViewLabel3 = this.m_ListViewLabel;
				if (listViewLabel3 != null)
				{
					listViewLabel3.EnableInClassList(BaseListView.emptyLabelUssClassName, noItemsCount);
				}
				Label listViewLabel4 = this.m_ListViewLabel;
				if (listViewLabel4 != null)
				{
					listViewLabel4.EnableInClassList(BaseListView.overMaxMultiEditLimitClassName, this.m_IsOverMultiEditLimit);
				}
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001506C File Offset: 0x0001326C
		private void OnAddClicked()
		{
			IList itemsSource = base.itemsSource;
			int itemsCountPreCallback = ((itemsSource != null) ? itemsSource.Count : 0);
			bool flag = this.overridingAddButtonBehavior != null;
			if (flag)
			{
				this.overridingAddButtonBehavior(this, this.m_AddButton);
			}
			else
			{
				bool flag2 = this.onAdd != null;
				if (flag2)
				{
					this.onAdd(this);
				}
				else
				{
					this.AddItems(1);
				}
			}
			bool flag3 = base.itemsSource != null && itemsCountPreCallback != base.itemsSource.Count;
			if (flag3)
			{
				this.OnItemsSourceSizeChanged();
				Action fnSetSelection = delegate
				{
					base.SetSelection(base.itemsSource.Count - 1);
					base.ScrollToItem(-1);
				};
				bool flag4 = base.GetProperty("__unity-collection-view-internal-binding") == null;
				if (flag4)
				{
					fnSetSelection();
				}
				else
				{
					base.schedule.Execute(fnSetSelection).ExecuteLater(100L);
				}
			}
			bool flag5 = this.HasValidDataAndBindings() && this.m_ArraySizeField != null;
			if (flag5)
			{
				this.m_ArraySizeField.showMixedValue = false;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00015174 File Offset: 0x00013374
		private void OnRemoveClicked()
		{
			bool flag = this.onRemove != null;
			if (flag)
			{
				this.onRemove(this);
			}
			else
			{
				bool flag2 = base.selectedIndices.Any<int>();
				if (flag2)
				{
					this.viewController.RemoveItems(base.selectedIndices.ToList<int>());
					base.ClearSelection();
				}
				else
				{
					bool flag3 = base.itemsSource.Count > 0;
					if (flag3)
					{
						int index = base.itemsSource.Count - 1;
						this.viewController.RemoveItem(index);
					}
				}
			}
			bool flag4 = this.HasValidDataAndBindings() && this.m_ArraySizeField != null;
			if (flag4)
			{
				this.m_ArraySizeField.showMixedValue = false;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00015227 File Offset: 0x00013427
		internal VisualElement footer
		{
			get
			{
				return this.m_Footer;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001522F File Offset: 0x0001342F
		public new BaseListViewController viewController
		{
			get
			{
				return base.viewController as BaseListViewController;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001523C File Offset: 0x0001343C
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableListViewItem>();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00015248 File Offset: 0x00013448
		public override void SetViewController(CollectionViewController controller)
		{
			if (this.m_ItemAddedCallback == null)
			{
				this.m_ItemAddedCallback = new Action<IEnumerable<int>>(this.OnItemAdded);
			}
			if (this.m_ItemRemovedCallback == null)
			{
				this.m_ItemRemovedCallback = new Action<IEnumerable<int>>(this.OnItemsRemoved);
			}
			if (this.m_ItemsSourceSizeChangedCallback == null)
			{
				this.m_ItemsSourceSizeChangedCallback = new Action(this.OnItemsSourceSizeChanged);
			}
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.itemsAdded -= this.m_ItemAddedCallback;
				this.viewController.itemsRemoved -= this.m_ItemRemovedCallback;
				this.viewController.itemsSourceSizeChanged -= this.m_ItemsSourceSizeChangedCallback;
			}
			base.SetViewController(controller);
			bool flag2 = this.viewController != null;
			if (flag2)
			{
				this.viewController.itemsAdded += this.m_ItemAddedCallback;
				this.viewController.itemsRemoved += this.m_ItemRemovedCallback;
				this.viewController.itemsSourceSizeChanged += this.m_ItemsSourceSizeChangedCallback;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00015336 File Offset: 0x00013536
		private void OnItemAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001534C File Offset: 0x0001354C
		private void OnItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00015364 File Offset: 0x00013564
		private void OnItemsSourceSizeChanged()
		{
			bool flag = base.GetProperty("__unity-collection-view-internal-binding") == null;
			if (flag)
			{
				base.RefreshItems();
			}
			Action action = this.itemsSourceSizeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000435 RID: 1077 RVA: 0x000153A4 File Offset: 0x000135A4
		// (remove) Token: 0x06000436 RID: 1078 RVA: 0x000153DC File Offset: 0x000135DC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action reorderModeChanged;

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015411 File Offset: 0x00013611
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0001541C File Offset: 0x0001361C
		[CreateProperty]
		public ListViewReorderMode reorderMode
		{
			get
			{
				return this.m_ReorderMode;
			}
			set
			{
				bool flag = value == this.m_ReorderMode;
				if (!flag)
				{
					this.m_ReorderMode = value;
					base.InitializeDragAndDropController(base.reorderable);
					Action action = this.reorderModeChanged;
					if (action != null)
					{
						action();
					}
					base.Rebuild();
					base.NotifyPropertyChanged(in BaseListView.reorderModeProperty);
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00015472 File Offset: 0x00013672
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0001547C File Offset: 0x0001367C
		[CreateProperty]
		public Func<VisualElement> makeNoneElement
		{
			get
			{
				return this.m_MakeNoneElement;
			}
			set
			{
				bool flag = value == this.m_MakeNoneElement;
				if (!flag)
				{
					this.m_MakeNoneElement = value;
					VisualElement noneElement = this.m_NoneElement;
					if (noneElement != null)
					{
						noneElement.RemoveFromHierarchy();
					}
					this.m_NoneElement = null;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.makeNoneElementProperty);
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000154CF File Offset: 0x000136CF
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x000154D8 File Offset: 0x000136D8
		[CreateProperty]
		public bool allowAdd
		{
			get
			{
				return this.m_AllowAdd;
			}
			set
			{
				bool flag = value == this.m_AllowAdd;
				if (!flag)
				{
					this.m_AllowAdd = value;
					Button addButton = this.m_AddButton;
					if (addButton != null)
					{
						addButton.SetEnabled(this.m_AllowAdd);
					}
					base.NotifyPropertyChanged(in BaseListView.allowAddProperty);
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00015520 File Offset: 0x00013720
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00015528 File Offset: 0x00013728
		[CreateProperty]
		public Action<BaseListView, Button> overridingAddButtonBehavior
		{
			get
			{
				return this.m_OverridingAddButtonBehavior;
			}
			set
			{
				bool flag = value == this.m_OverridingAddButtonBehavior;
				if (!flag)
				{
					this.m_OverridingAddButtonBehavior = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.overridingAddButtonBehaviorProperty);
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00015562 File Offset: 0x00013762
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0001556C File Offset: 0x0001376C
		[CreateProperty]
		public Action<BaseListView> onAdd
		{
			get
			{
				return this.m_OnAdd;
			}
			set
			{
				bool flag = value == this.m_OnAdd;
				if (!flag)
				{
					this.m_OnAdd = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.onAddProperty);
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000155A6 File Offset: 0x000137A6
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x000155B0 File Offset: 0x000137B0
		[CreateProperty]
		public bool allowRemove
		{
			get
			{
				return this.m_AllowRemove;
			}
			set
			{
				bool flag = value == this.m_AllowRemove;
				if (!flag)
				{
					this.m_AllowRemove = value;
					Button removeButton = this.m_RemoveButton;
					if (removeButton != null)
					{
						removeButton.SetEnabled(this.allowRemove);
					}
					base.NotifyPropertyChanged(in BaseListView.allowRemoveProperty);
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000155F8 File Offset: 0x000137F8
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00015600 File Offset: 0x00013800
		[CreateProperty]
		public Action<BaseListView> onRemove
		{
			get
			{
				return this.m_OnRemove;
			}
			set
			{
				bool flag = value == this.m_OnRemove;
				if (!flag)
				{
					this.m_OnRemove = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseListView.onRemoveProperty);
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001563C File Offset: 0x0001383C
		internal override ListViewDragger CreateDragger()
		{
			bool flag = this.m_ReorderMode == ListViewReorderMode.Simple;
			ListViewDragger listViewDragger;
			if (flag)
			{
				listViewDragger = new ListViewDragger(this);
			}
			else
			{
				listViewDragger = new ListViewDraggerAnimated(this);
			}
			return listViewDragger;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001566A File Offset: 0x0001386A
		internal override ICollectionDragAndDropController CreateDragAndDropController()
		{
			return new ListViewReorderableDragAndDropController(this);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015672 File Offset: 0x00013872
		public BaseListView()
			: this(null, -1f)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00015684 File Offset: 0x00013884
		public BaseListView(IList itemsSource, float itemHeight = -1f)
			: base(itemsSource, itemHeight)
		{
			base.AddToClassList(BaseListView.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			this.allowAdd = true;
			this.allowRemove = true;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000156DB File Offset: 0x000138DB
		private protected override void PostRefresh()
		{
			this.UpdateArraySizeField();
			this.UpdateListViewLabel();
			base.PostRefresh();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000156F4 File Offset: 0x000138F4
		private protected override bool HandleItemNavigation(bool moveIn, bool altPressed)
		{
			bool hasChanges = false;
			foreach (int index in base.selectedIndices)
			{
				foreach (ReusableCollectionItem reusableCollectionItem in base.activeItems)
				{
					bool flag = reusableCollectionItem.index == index && base.GetProperty("__unity-collection-view-internal-binding") != null;
					if (flag)
					{
						Foldout foldout = reusableCollectionItem.bindableElement.Q(null, null);
						bool flag2 = foldout != null;
						if (flag2)
						{
							foldout.value = moveIn;
							hasChanges = true;
						}
					}
				}
			}
			return hasChanges;
		}

		// Token: 0x04000227 RID: 551
		private static readonly string k_SizeFieldLabel = "Size";

		// Token: 0x04000228 RID: 552
		internal static readonly BindingId showBoundCollectionSizeProperty = "showBoundCollectionSize";

		// Token: 0x04000229 RID: 553
		internal static readonly BindingId showFoldoutHeaderProperty = "showFoldoutHeader";

		// Token: 0x0400022A RID: 554
		internal static readonly BindingId headerTitleProperty = "headerTitle";

		// Token: 0x0400022B RID: 555
		internal static readonly BindingId makeHeaderProperty = "makeHeader";

		// Token: 0x0400022C RID: 556
		internal static readonly BindingId makeFooterProperty = "makeFooter";

		// Token: 0x0400022D RID: 557
		internal static readonly BindingId showAddRemoveFooterProperty = "showAddRemoveFooter";

		// Token: 0x0400022E RID: 558
		internal static readonly BindingId bindingSourceSelectionModeProperty = "bindingSourceSelectionMode";

		// Token: 0x0400022F RID: 559
		internal static readonly BindingId reorderModeProperty = "reorderMode";

		// Token: 0x04000230 RID: 560
		internal static readonly BindingId makeNoneElementProperty = "makeNoneElement";

		// Token: 0x04000231 RID: 561
		internal static readonly BindingId allowAddProperty = "allowAdd";

		// Token: 0x04000232 RID: 562
		internal static readonly BindingId overridingAddButtonBehaviorProperty = "overridingAddButtonBehavior";

		// Token: 0x04000233 RID: 563
		internal static readonly BindingId onAddProperty = "onAdd";

		// Token: 0x04000234 RID: 564
		internal static readonly BindingId allowRemoveProperty = "allowRemove";

		// Token: 0x04000235 RID: 565
		internal static readonly BindingId onRemoveProperty = "onRemove";

		// Token: 0x04000236 RID: 566
		private bool m_ShowBoundCollectionSize = true;

		// Token: 0x04000237 RID: 567
		private bool m_ShowFoldoutHeader;

		// Token: 0x04000238 RID: 568
		private string m_HeaderTitle;

		// Token: 0x04000239 RID: 569
		private VisualElement drawnHeader;

		// Token: 0x0400023A RID: 570
		private Func<VisualElement> m_MakeHeader;

		// Token: 0x0400023B RID: 571
		private VisualElement drawnFooter;

		// Token: 0x0400023C RID: 572
		private Func<VisualElement> m_MakeFooter;

		// Token: 0x0400023D RID: 573
		private bool m_ShowAddRemoveFooter;

		// Token: 0x0400023E RID: 574
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<int>> itemsAdded;

		// Token: 0x0400023F RID: 575
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<int>> itemsRemoved;

		// Token: 0x04000240 RID: 576
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action itemsSourceSizeChanged;

		// Token: 0x04000241 RID: 577
		private IVisualElementScheduledItem m_TrackedItem;

		// Token: 0x04000242 RID: 578
		private Action m_TrackCount;

		// Token: 0x04000243 RID: 579
		private Func<bool> m_WhileAutoAssign;

		// Token: 0x04000244 RID: 580
		private BindingSourceSelectionMode m_BindingSourceSelectionMode = BindingSourceSelectionMode.Manual;

		// Token: 0x04000245 RID: 581
		private Label m_ListViewLabel;

		// Token: 0x04000246 RID: 582
		private Foldout m_Foldout;

		// Token: 0x04000247 RID: 583
		private TextField m_ArraySizeField;

		// Token: 0x04000248 RID: 584
		private bool m_IsOverMultiEditLimit;

		// Token: 0x04000249 RID: 585
		private VisualElement m_Footer;

		// Token: 0x0400024A RID: 586
		private Button m_AddButton;

		// Token: 0x0400024B RID: 587
		private Button m_RemoveButton;

		// Token: 0x0400024C RID: 588
		private Action<IEnumerable<int>> m_ItemAddedCallback;

		// Token: 0x0400024D RID: 589
		private Action<IEnumerable<int>> m_ItemRemovedCallback;

		// Token: 0x0400024E RID: 590
		private Action m_ItemsSourceSizeChangedCallback;

		// Token: 0x0400024F RID: 591
		private ListViewReorderMode m_ReorderMode;

		// Token: 0x04000251 RID: 593
		private VisualElement m_NoneElement;

		// Token: 0x04000252 RID: 594
		private Func<VisualElement> m_MakeNoneElement;

		// Token: 0x04000253 RID: 595
		private bool m_AllowAdd = true;

		// Token: 0x04000254 RID: 596
		private Action<BaseListView, Button> m_OverridingAddButtonBehavior;

		// Token: 0x04000255 RID: 597
		private Action<BaseListView> m_OnAdd;

		// Token: 0x04000256 RID: 598
		private bool m_AllowRemove = true;

		// Token: 0x04000257 RID: 599
		private Action<BaseListView> m_OnRemove;

		// Token: 0x04000258 RID: 600
		public new static readonly string ussClassName = "unity-list-view";

		// Token: 0x04000259 RID: 601
		public new static readonly string itemUssClassName = BaseListView.ussClassName + "__item";

		// Token: 0x0400025A RID: 602
		public static readonly string emptyLabelUssClassName = BaseListView.ussClassName + "__empty-label";

		// Token: 0x0400025B RID: 603
		public static readonly string overMaxMultiEditLimitClassName = BaseListView.ussClassName + "__over-max-multi-edit-limit-label";

		// Token: 0x0400025C RID: 604
		public static readonly string reorderableUssClassName = BaseListView.ussClassName + "__reorderable";

		// Token: 0x0400025D RID: 605
		public static readonly string reorderableItemUssClassName = BaseListView.reorderableUssClassName + "-item";

		// Token: 0x0400025E RID: 606
		public static readonly string reorderableItemContainerUssClassName = BaseListView.reorderableItemUssClassName + "__container";

		// Token: 0x0400025F RID: 607
		public static readonly string reorderableItemHandleUssClassName = BaseListView.reorderableUssClassName + "-handle";

		// Token: 0x04000260 RID: 608
		public static readonly string reorderableItemHandleBarUssClassName = BaseListView.reorderableItemHandleUssClassName + "-bar";

		// Token: 0x04000261 RID: 609
		public static readonly string footerUssClassName = BaseListView.ussClassName + "__footer";

		// Token: 0x04000262 RID: 610
		public static readonly string foldoutHeaderUssClassName = BaseListView.ussClassName + "__foldout-header";

		// Token: 0x04000263 RID: 611
		public static readonly string arraySizeFieldUssClassName = BaseListView.ussClassName + "__size-field";

		// Token: 0x04000264 RID: 612
		public static readonly string arraySizeFieldWithHeaderUssClassName = BaseListView.arraySizeFieldUssClassName + "--with-header";

		// Token: 0x04000265 RID: 613
		public static readonly string arraySizeFieldWithFooterUssClassName = BaseListView.arraySizeFieldUssClassName + "--with-footer";

		// Token: 0x04000266 RID: 614
		public static readonly string listViewWithHeaderUssClassName = BaseListView.ussClassName + "--with-header";

		// Token: 0x04000267 RID: 615
		public static readonly string listViewWithFooterUssClassName = BaseListView.ussClassName + "--with-footer";

		// Token: 0x04000268 RID: 616
		public static readonly string scrollViewWithFooterUssClassName = BaseListView.ussClassName + "__scroll-view--with-footer";

		// Token: 0x04000269 RID: 617
		public static readonly string footerAddButtonName = BaseListView.ussClassName + "__add-button";

		// Token: 0x0400026A RID: 618
		public static readonly string footerRemoveButtonName = BaseListView.ussClassName + "__remove-button";

		// Token: 0x0400026B RID: 619
		private string m_MaxMultiEditStr;

		// Token: 0x0400026C RID: 620
		private static readonly string k_EmptyListStr = "List is empty";

		// Token: 0x02000075 RID: 117
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseVerticalCollectionView.UxmlTraits
		{
			// Token: 0x0600044F RID: 1103 RVA: 0x00015AC0 File Offset: 0x00013CC0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BaseListView view = (BaseListView)ve;
				view.reorderMode = this.m_ReorderMode.GetValueFromBag(bag, cc);
				view.showFoldoutHeader = this.m_ShowFoldoutHeader.GetValueFromBag(bag, cc);
				view.headerTitle = this.m_HeaderTitle.GetValueFromBag(bag, cc);
				view.showAddRemoveFooter = this.m_ShowAddRemoveFooter.GetValueFromBag(bag, cc);
				view.allowAdd = this.m_AllowAdd.GetValueFromBag(bag, cc);
				view.allowRemove = this.m_AllowRemove.GetValueFromBag(bag, cc);
				view.showBoundCollectionSize = this.m_ShowBoundCollectionSize.GetValueFromBag(bag, cc);
				view.bindingSourceSelectionMode = this.m_BindingSourceSelectionMode.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00015B80 File Offset: 0x00013D80
			protected UxmlTraits()
			{
				this.m_PickingMode.defaultValue = PickingMode.Ignore;
			}

			// Token: 0x0400026D RID: 621
			private readonly UxmlBoolAttributeDescription m_ShowFoldoutHeader = new UxmlBoolAttributeDescription
			{
				name = "show-foldout-header",
				defaultValue = false
			};

			// Token: 0x0400026E RID: 622
			private readonly UxmlStringAttributeDescription m_HeaderTitle = new UxmlStringAttributeDescription
			{
				name = "header-title",
				defaultValue = string.Empty
			};

			// Token: 0x0400026F RID: 623
			private readonly UxmlBoolAttributeDescription m_ShowAddRemoveFooter = new UxmlBoolAttributeDescription
			{
				name = "show-add-remove-footer",
				defaultValue = false
			};

			// Token: 0x04000270 RID: 624
			private readonly UxmlBoolAttributeDescription m_AllowAdd = new UxmlBoolAttributeDescription
			{
				name = "allow-add",
				defaultValue = true
			};

			// Token: 0x04000271 RID: 625
			private readonly UxmlBoolAttributeDescription m_AllowRemove = new UxmlBoolAttributeDescription
			{
				name = "allow-remove",
				defaultValue = true
			};

			// Token: 0x04000272 RID: 626
			private readonly UxmlEnumAttributeDescription<ListViewReorderMode> m_ReorderMode = new UxmlEnumAttributeDescription<ListViewReorderMode>
			{
				name = "reorder-mode",
				defaultValue = ListViewReorderMode.Simple
			};

			// Token: 0x04000273 RID: 627
			private readonly UxmlBoolAttributeDescription m_ShowBoundCollectionSize = new UxmlBoolAttributeDescription
			{
				name = "show-bound-collection-size",
				defaultValue = true
			};

			// Token: 0x04000274 RID: 628
			private readonly UxmlEnumAttributeDescription<BindingSourceSelectionMode> m_BindingSourceSelectionMode = new UxmlEnumAttributeDescription<BindingSourceSelectionMode>
			{
				name = "binding-source-selection-mode",
				defaultValue = BindingSourceSelectionMode.Manual
			};
		}
	}
}
