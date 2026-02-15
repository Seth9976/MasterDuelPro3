using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020000CC RID: 204
	public class GenericDropdownMenu : IGenericMenu
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001DE50 File Offset: 0x0001C050
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001DE58 File Offset: 0x0001C058
		internal bool isSingleSelectionDropdown { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001DE61 File Offset: 0x0001C061
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001DE69 File Offset: 0x0001C069
		internal bool closeOnParentResize { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001DE72 File Offset: 0x0001C072
		public VisualElement contentContainer
		{
			get
			{
				return this.m_ScrollView.contentContainer;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001DE80 File Offset: 0x0001C080
		public GenericDropdownMenu()
		{
			this.m_MenuContainer = new VisualElement();
			this.m_MenuContainer.AddToClassList(GenericDropdownMenu.ussClassName);
			this.m_OuterContainer = new VisualElement();
			this.m_OuterContainer.AddToClassList(GenericDropdownMenu.containerOuterUssClassName);
			this.m_MenuContainer.Add(this.m_OuterContainer);
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(GenericDropdownMenu.containerInnerUssClassName);
			this.m_ScrollView.pickingMode = PickingMode.Position;
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			this.m_ScrollView.mode = ScrollViewMode.VerticalAndHorizontal;
			this.m_OuterContainer.hierarchy.Add(this.m_ScrollView);
			this.m_MenuContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_MenuContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.isSingleSelectionDropdown = true;
			this.closeOnParentResize = true;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001DF9C File Offset: 0x0001C19C
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_MenuContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.destinationPanel.visualTree.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnInitialDisplay), InvokePolicy.Once, TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001E098 File Offset: 0x0001C298
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_MenuContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.originPanel.visualTree.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001E168 File Offset: 0x0001C368
		private void Hide(bool giveFocusBack = false)
		{
			this.m_MenuContainer.RemoveFromHierarchy();
			bool flag = this.m_TargetElement != null;
			if (flag)
			{
				this.m_TargetElement.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_TargetElement.pseudoStates ^= PseudoStates.Active;
				bool flag2 = giveFocusBack && this.m_TargetElement.canGrabFocus;
				if (flag2)
				{
					this.m_TargetElement.Focus();
				}
			}
			this.m_TargetElement = null;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			bool flag = this.Apply(op);
			if (flag)
			{
				sourceEvent.StopPropagation();
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001E20C File Offset: 0x0001C40C
		private bool Apply(KeyboardNavigationOperation op)
		{
			GenericDropdownMenu.<>c__DisplayClass48_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedIndex = this.GetSelectedIndex();
			switch (op)
			{
			case KeyboardNavigationOperation.Cancel:
				this.Hide(true);
				return true;
			case KeyboardNavigationOperation.Submit:
			{
				GenericDropdownMenu.MenuItem item = ((CS$<>8__locals1.selectedIndex != -1) ? this.m_Items[CS$<>8__locals1.selectedIndex] : null);
				bool flag = CS$<>8__locals1.selectedIndex >= 0 && item.element.enabledSelf;
				if (flag)
				{
					Action action = item.action;
					if (action != null)
					{
						action();
					}
					Action<object> actionUserData = item.actionUserData;
					if (actionUserData != null)
					{
						actionUserData(item.element.userData);
					}
				}
				this.Hide(true);
				return true;
			}
			case KeyboardNavigationOperation.Previous:
				this.<Apply>g__UpdateSelectionUp|48_1((CS$<>8__locals1.selectedIndex < 0) ? (this.m_Items.Count - 1) : (CS$<>8__locals1.selectedIndex - 1), ref CS$<>8__locals1);
				return true;
			case KeyboardNavigationOperation.Next:
				this.<Apply>g__UpdateSelectionDown|48_0(CS$<>8__locals1.selectedIndex + 1, ref CS$<>8__locals1);
				return true;
			case KeyboardNavigationOperation.PageUp:
			case KeyboardNavigationOperation.Begin:
				this.<Apply>g__UpdateSelectionDown|48_0(0, ref CS$<>8__locals1);
				return true;
			case KeyboardNavigationOperation.PageDown:
			case KeyboardNavigationOperation.End:
				this.<Apply>g__UpdateSelectionUp|48_1(this.m_Items.Count - 1, ref CS$<>8__locals1);
				return true;
			}
			return false;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001E368 File Offset: 0x0001C568
		private void OnPointerDown(PointerDownEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.elementTarget);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		private void OnPointerMove(PointerMoveEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.elementTarget);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001E440 File Offset: 0x0001C640
		private void OnPointerUp(PointerUpEvent evt)
		{
			int selectedIndex = this.GetSelectedIndex();
			bool flag = selectedIndex != -1;
			if (flag)
			{
				GenericDropdownMenu.MenuItem item = this.m_Items[selectedIndex];
				Action action = item.action;
				if (action != null)
				{
					action();
				}
				Action<object> actionUserData = item.actionUserData;
				if (actionUserData != null)
				{
					actionUserData(item.element.userData);
				}
				bool isSingleSelectionDropdown = this.isSingleSelectionDropdown;
				if (isSingleSelectionDropdown)
				{
					this.Hide(true);
				}
			}
			bool flag2 = evt.pointerId != PointerId.mousePointerId;
			if (flag2)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		private void OnFocusOut(FocusOutEvent evt)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				this.Hide(false);
			}
			else
			{
				this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001E540 File Offset: 0x0001C740
		private void OnParentResized(GeometryChangedEvent evt)
		{
			bool closeOnParentResize = this.closeOnParentResize;
			if (closeOnParentResize)
			{
				this.Hide(true);
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001E564 File Offset: 0x0001C764
		private void UpdateSelection(VisualElement target)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				int selectedIndex = this.GetSelectedIndex();
				bool flag2 = selectedIndex >= 0;
				if (flag2)
				{
					this.m_Items[selectedIndex].element.pseudoStates &= ~PseudoStates.Hover;
				}
			}
			else
			{
				bool flag3 = target == null;
				if (!flag3)
				{
					bool flag4 = (target.pseudoStates & PseudoStates.Hover) != PseudoStates.Hover;
					if (flag4)
					{
						int selectedIndex2 = this.GetSelectedIndex();
						bool flag5 = selectedIndex2 >= 0;
						if (flag5)
						{
							this.m_Items[selectedIndex2].element.pseudoStates &= ~PseudoStates.Hover;
						}
						target.pseudoStates |= PseudoStates.Hover;
					}
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001E628 File Offset: 0x0001C828
		private void ChangeSelectedIndex(int newIndex, int previousIndex)
		{
			bool flag = previousIndex >= 0 && previousIndex < this.m_Items.Count;
			if (flag)
			{
				this.m_Items[previousIndex].element.pseudoStates &= ~PseudoStates.Hover;
			}
			bool flag2 = newIndex >= 0 && newIndex < this.m_Items.Count;
			if (flag2)
			{
				this.m_Items[newIndex].element.pseudoStates |= PseudoStates.Hover;
				this.m_ScrollView.ScrollTo(this.m_Items[newIndex].element);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		private int GetSelectedIndex()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				bool flag = (this.m_Items[i].element.pseudoStates & PseudoStates.Hover) == PseudoStates.Hover;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001E71C File Offset: 0x0001C91C
		public void AddItem(string itemName, bool isChecked, Action action)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, null);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.action = action;
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001E748 File Offset: 0x0001C948
		public void AddItem(string itemName, bool isChecked, Action<object> action, object data)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, data);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.actionUserData = action;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001E774 File Offset: 0x0001C974
		public void AddSeparator(string path)
		{
			VisualElement separator = new VisualElement();
			separator.AddToClassList(GenericDropdownMenu.separatorUssClassName);
			separator.pickingMode = PickingMode.Ignore;
			this.m_ScrollView.Add(separator);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		private GenericDropdownMenu.MenuItem AddItem(string itemName, bool isChecked, bool isEnabled, object data = null)
		{
			bool flag = string.IsNullOrEmpty(itemName) || itemName.EndsWith("/");
			GenericDropdownMenu.MenuItem menuItem2;
			if (flag)
			{
				this.AddSeparator(itemName);
				menuItem2 = null;
			}
			else
			{
				for (int i = 0; i < this.m_Items.Count; i++)
				{
					bool flag2 = itemName == this.m_Items[i].name;
					if (flag2)
					{
						return null;
					}
				}
				VisualElement rowElement = new VisualElement();
				rowElement.AddToClassList(GenericDropdownMenu.itemUssClassName);
				rowElement.SetEnabled(isEnabled);
				rowElement.userData = data;
				VisualElement itemContent = new VisualElement();
				itemContent.AddToClassList(GenericDropdownMenu.itemContentUssClassName);
				VisualElement checkElement = new VisualElement();
				checkElement.AddToClassList(GenericDropdownMenu.checkmarkUssClassName);
				checkElement.pickingMode = PickingMode.Ignore;
				itemContent.Add(checkElement);
				if (isChecked)
				{
					rowElement.pseudoStates |= PseudoStates.Checked;
				}
				Label label = new Label(itemName);
				label.AddToClassList(GenericDropdownMenu.labelUssClassName);
				label.pickingMode = PickingMode.Ignore;
				itemContent.Add(label);
				rowElement.Add(itemContent);
				this.m_ScrollView.Add(rowElement);
				GenericDropdownMenu.MenuItem menuItem = new GenericDropdownMenu.MenuItem
				{
					name = itemName,
					element = rowElement
				};
				this.m_Items.Add(menuItem);
				menuItem2 = menuItem;
			}
			return menuItem2;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001E900 File Offset: 0x0001CB00
		public void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false)
		{
			bool flag = targetElement == null;
			if (flag)
			{
				Debug.LogError("VisualElement Generic Menu needs a target to find a root to attach to.");
			}
			else
			{
				this.m_TargetElement = targetElement;
				this.m_TargetElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_PanelRootVisualContainer = this.m_TargetElement.GetRootVisualContainer();
				bool flag2 = this.m_PanelRootVisualContainer == null;
				if (flag2)
				{
					Debug.LogError("Could not find rootVisualContainer...");
				}
				else
				{
					this.m_PanelRootVisualContainer.Add(this.m_MenuContainer);
					this.m_MenuContainer.style.left = this.m_PanelRootVisualContainer.layout.x;
					this.m_MenuContainer.style.top = this.m_PanelRootVisualContainer.layout.y;
					this.m_MenuContainer.style.width = this.m_PanelRootVisualContainer.layout.width;
					this.m_MenuContainer.style.height = this.m_PanelRootVisualContainer.layout.height;
					this.m_MenuContainer.style.fontSize = this.m_TargetElement.computedStyle.fontSize;
					this.m_MenuContainer.style.unityFont = this.m_TargetElement.computedStyle.unityFont;
					this.m_MenuContainer.style.unityFontDefinition = this.m_TargetElement.computedStyle.unityFontDefinition;
					Rect local = this.m_PanelRootVisualContainer.WorldToLocal(position);
					this.m_PositionTop = local.y + position.height - this.m_PanelRootVisualContainer.layout.y;
					this.m_PositionLeft = local.x - this.m_PanelRootVisualContainer.layout.x;
					this.m_OuterContainer.style.left = this.m_PositionLeft;
					this.m_OuterContainer.style.top = this.m_PositionTop;
					this.m_OuterContainer.style.maxHeight = Length.None();
					this.m_OuterContainer.style.maxWidth = Length.None();
					this.m_DesiredRect = (anchored ? position : Rect.zero);
					this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
					this.m_ShownAboveTarget = false;
					this.EnsureVisibilityInParent();
					bool flag3 = targetElement != null;
					if (flag3)
					{
						targetElement.pseudoStates |= PseudoStates.Active;
					}
				}
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001EBBE File Offset: 0x0001CDBE
		private void OnTargetElementDetachFromPanel(DetachFromPanelEvent evt)
		{
			this.Hide(false);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001EBC9 File Offset: 0x0001CDC9
		private void OnContainerGeometryChanged(GeometryChangedEvent evt)
		{
			this.EnsureVisibilityInParent();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001EBD3 File Offset: 0x0001CDD3
		private void OnInitialDisplay(GeometryChangedEvent evt)
		{
			this.m_ContentWidth = this.GetLargestItemWidth() + 20f;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001EBE8 File Offset: 0x0001CDE8
		private void EnsureVisibilityInParent()
		{
			bool flag = this.m_PanelRootVisualContainer != null && !float.IsNaN(this.m_OuterContainer.layout.width) && !float.IsNaN(this.m_OuterContainer.layout.height);
			if (flag)
			{
				bool flag2 = this.m_DesiredRect == Rect.zero;
				if (flag2)
				{
					float posX = Math.Max(0f, Mathf.Min(this.m_PositionLeft, this.m_PanelRootVisualContainer.layout.width - this.m_OuterContainer.layout.width));
					float posY = Mathf.Min(this.m_PositionTop, Mathf.Max(0f, this.m_PanelRootVisualContainer.layout.height - this.m_OuterContainer.layout.height));
					this.m_OuterContainer.style.left = posX;
					this.m_OuterContainer.style.top = posY;
				}
				else
				{
					float dropdownWidth = this.m_ContentWidth;
					bool isVerticalScrollDisplayed = this.m_ScrollView.isVerticalScrollDisplayed;
					if (isVerticalScrollDisplayed)
					{
						dropdownWidth += Mathf.Ceil(this.m_ScrollView.verticalScroller.computedStyle.width.value);
					}
					dropdownWidth = (this.m_FitContentWidth ? dropdownWidth : this.m_DesiredRect.width);
					this.m_OuterContainer.style.width = dropdownWidth;
					float spaceToTheRight = this.m_PanelRootVisualContainer.layout.width - this.m_PositionLeft;
					bool flag3 = spaceToTheRight <= dropdownWidth;
					if (flag3)
					{
						this.m_PositionLeft -= dropdownWidth - spaceToTheRight + 2f;
					}
					this.m_PositionLeft = Math.Max(this.m_PositionLeft, 0f);
					bool flag4 = this.m_PositionLeft == 0f;
					if (flag4)
					{
						this.m_OuterContainer.style.maxWidth = Math.Min(this.m_PanelRootVisualContainer.layout.width, dropdownWidth);
					}
					this.m_OuterContainer.style.left = this.m_PositionLeft;
				}
				Rect targetElement = this.m_MenuContainer.WorldToLocal(this.m_TargetElement.worldBound);
				float itemHeight = this.m_Items[0].element.layout.height + 20f;
				float dropdownHeight = this.m_OuterContainer.layout.height;
				float targetElementTop = targetElement.y;
				float actualTop = this.m_OuterContainer.worldBound.y;
				float spaceBelow = (this.m_ShownAboveTarget ? (targetElementTop - actualTop) : (this.m_PanelRootVisualContainer.worldBound.height - actualTop));
				float spaceAbove = (this.m_ShownAboveTarget ? (this.m_PanelRootVisualContainer.worldBound.height - actualTop) : targetElementTop);
				bool adjustTop = spaceBelow < dropdownHeight;
				bool flag5 = adjustTop && spaceAbove > spaceBelow;
				if (flag5)
				{
					this.m_PositionTop = targetElementTop - dropdownHeight;
					this.m_PositionTop = Math.Max(this.m_PositionTop, 0f);
					this.m_OuterContainer.style.maxHeight = ((this.m_PositionTop == 0f) ? Math.Max(targetElementTop, itemHeight) : Length.None());
					this.m_OuterContainer.style.top = this.m_PositionTop;
					this.m_ShownAboveTarget = true;
				}
				else
				{
					bool flag6 = adjustTop;
					if (flag6)
					{
						bool flag7 = spaceBelow < itemHeight;
						if (flag7)
						{
							this.m_OuterContainer.style.maxHeight = itemHeight;
							this.m_PositionTop = this.m_PanelRootVisualContainer.worldBound.height - itemHeight;
						}
						else
						{
							this.m_OuterContainer.style.maxHeight = spaceBelow;
						}
						this.m_OuterContainer.style.top = this.m_PositionTop;
					}
				}
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001F018 File Offset: 0x0001D218
		private float GetLargestItemWidth()
		{
			float largestWidth = 0f;
			bool flag = this.m_Items.Count == 0 && this.m_ScrollView.contentContainer.childCount > 0;
			if (flag)
			{
				List<GenericDropdownMenu.MenuItem> menuItems = CollectionPool<List<GenericDropdownMenu.MenuItem>, GenericDropdownMenu.MenuItem>.Get();
				foreach (VisualElement element in this.m_ScrollView.contentContainer.Children())
				{
					menuItems.Add(new GenericDropdownMenu.MenuItem
					{
						element = element
					});
				}
				this.m_Items.AddRange(menuItems);
				CollectionPool<List<GenericDropdownMenu.MenuItem>, GenericDropdownMenu.MenuItem>.Release(menuItems);
			}
			foreach (GenericDropdownMenu.MenuItem item in this.m_Items)
			{
				largestWidth = Math.Max(largestWidth, item.element.layout.width);
			}
			return largestWidth;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001F1E8 File Offset: 0x0001D3E8
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionDown|48_0(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass48_0 A_2)
		{
			while (newIndex < this.m_Items.Count)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex++;
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001F23C File Offset: 0x0001D43C
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionUp|48_1(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass48_0 A_2)
		{
			while (newIndex >= 0)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex--;
			}
		}

		// Token: 0x040003E0 RID: 992
		public static readonly string ussClassName = "unity-base-dropdown";

		// Token: 0x040003E1 RID: 993
		public static readonly string itemUssClassName = GenericDropdownMenu.ussClassName + "__item";

		// Token: 0x040003E2 RID: 994
		public static readonly string itemContentUssClassName = GenericDropdownMenu.ussClassName + "__item-content";

		// Token: 0x040003E3 RID: 995
		public static readonly string labelUssClassName = GenericDropdownMenu.ussClassName + "__label";

		// Token: 0x040003E4 RID: 996
		public static readonly string containerInnerUssClassName = GenericDropdownMenu.ussClassName + "__container-inner";

		// Token: 0x040003E5 RID: 997
		public static readonly string containerOuterUssClassName = GenericDropdownMenu.ussClassName + "__container-outer";

		// Token: 0x040003E6 RID: 998
		public static readonly string checkmarkUssClassName = GenericDropdownMenu.ussClassName + "__checkmark";

		// Token: 0x040003E7 RID: 999
		public static readonly string separatorUssClassName = GenericDropdownMenu.ussClassName + "__separator";

		// Token: 0x040003E8 RID: 1000
		public static readonly string contentWidthUssClassName = GenericDropdownMenu.ussClassName + "--content-width-menu";

		// Token: 0x040003E9 RID: 1001
		private List<GenericDropdownMenu.MenuItem> m_Items = new List<GenericDropdownMenu.MenuItem>();

		// Token: 0x040003EA RID: 1002
		private VisualElement m_MenuContainer;

		// Token: 0x040003EB RID: 1003
		private VisualElement m_OuterContainer;

		// Token: 0x040003EC RID: 1004
		private ScrollView m_ScrollView;

		// Token: 0x040003ED RID: 1005
		private VisualElement m_PanelRootVisualContainer;

		// Token: 0x040003EE RID: 1006
		private VisualElement m_TargetElement;

		// Token: 0x040003EF RID: 1007
		private Rect m_DesiredRect;

		// Token: 0x040003F0 RID: 1008
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x040003F1 RID: 1009
		private float m_PositionTop;

		// Token: 0x040003F2 RID: 1010
		private float m_PositionLeft;

		// Token: 0x040003F3 RID: 1011
		private float m_ContentWidth;

		// Token: 0x040003F4 RID: 1012
		private bool m_FitContentWidth;

		// Token: 0x040003F5 RID: 1013
		private bool m_ShownAboveTarget;

		// Token: 0x040003F8 RID: 1016
		private Vector2 m_MousePosition;

		// Token: 0x020000CD RID: 205
		internal class MenuItem
		{
			// Token: 0x040003F9 RID: 1017
			public string name;

			// Token: 0x040003FA RID: 1018
			public VisualElement element;

			// Token: 0x040003FB RID: 1019
			public Action action;

			// Token: 0x040003FC RID: 1020
			public Action<object> actionUserData;
		}
	}
}
