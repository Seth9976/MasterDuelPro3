using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015C RID: 348
	public class ToggleButtonGroup : BaseField<ToggleButtonGroupState>
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0003295E File Offset: 0x00030B5E
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00032968 File Offset: 0x00030B68
		[CreateProperty]
		public unsafe bool isMultipleSelection
		{
			get
			{
				return this.m_IsMultipleSelection;
			}
			set
			{
				bool flag = this.m_IsMultipleSelection == value;
				checked
				{
					if (!flag)
					{
						ToggleButtonGroupState toggleButtonGroupState = this.value;
						int length = toggleButtonGroupState.length;
						Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
						Span<int> selected = toggleButtonGroupState.GetActiveOptions(span);
						bool flag2 = selected.Length > 1 && this.m_Buttons.Count > 0;
						if (flag2)
						{
							toggleButtonGroupState.ResetAllOptions();
							toggleButtonGroupState[*selected[0]] = true;
							this.SetValueWithoutNotify(toggleButtonGroupState);
						}
						this.m_IsMultipleSelection = value;
						base.NotifyPropertyChanged(in ToggleButtonGroup.isMultipleSelectionProperty);
					}
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00032A04 File Offset: 0x00030C04
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00032A0C File Offset: 0x00030C0C
		[CreateProperty]
		public unsafe bool allowEmptySelection
		{
			get
			{
				return this.m_AllowEmptySelection;
			}
			set
			{
				bool flag = this.m_AllowEmptySelection == value;
				checked
				{
					if (!flag)
					{
						bool flag2 = !value;
						if (flag2)
						{
							ToggleButtonGroupState toggleButtonGroupState = this.value;
							int length = toggleButtonGroupState.length;
							Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
							bool flag3 = toggleButtonGroupState.GetActiveOptions(span).Length == 0 && this.m_Buttons.Count > 0;
							if (flag3)
							{
								toggleButtonGroupState[0] = true;
								this.SetValueWithoutNotify(toggleButtonGroupState);
							}
						}
						this.m_AllowEmptySelection = value;
						base.NotifyPropertyChanged(in ToggleButtonGroup.allowEmptySelectionProperty);
					}
				}
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00032AA4 File Offset: 0x00030CA4
		public ToggleButtonGroup()
			: this(null)
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00032AAF File Offset: 0x00030CAF
		public ToggleButtonGroup(string label)
			: this(label, new ToggleButtonGroupState(0UL, 64))
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00032AC4 File Offset: 0x00030CC4
		public ToggleButtonGroup(string label, ToggleButtonGroupState toggleButtonGroupState)
			: base(label)
		{
			base.AddToClassList(ToggleButtonGroup.ussClassName);
			base.visualInput = new VisualElement();
			this.m_ButtonGroupContainer = new VisualElement
			{
				name = ToggleButtonGroup.containerUssClassName,
				classList = { ToggleButtonGroup.buttonGroupClassName }
			};
			base.visualInput.Add(this.m_ButtonGroupContainer);
			this.m_ButtonGroupContainer.elementAdded += this.OnButtonGroupContainerElementAdded;
			this.m_ButtonGroupContainer.elementRemoved += this.OnButtonGroupContainerElementRemoved;
			this.SetValueWithoutNotify(toggleButtonGroupState);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00032B6F File Offset: 0x00030D6F
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ButtonGroupContainer ?? this;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00032B7C File Offset: 0x00030D7C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateButtonStates(this.value);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00032B94 File Offset: 0x00030D94
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				foreach (Button button in this.m_Buttons)
				{
					button.pseudoStates &= ~PseudoStates.Checked;
					button.IncrementVersion(VersionChangeType.Styles);
				}
			}
			else
			{
				this.SetValueWithoutNotify(this.value);
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00032C1C File Offset: 0x00030E1C
		public override void SetValueWithoutNotify(ToggleButtonGroupState newValue)
		{
			bool flag = newValue.length == 0;
			if (flag)
			{
				newValue = new ToggleButtonGroupState(0UL, 0);
				if (this.m_EmptyLabel == null)
				{
					this.m_EmptyLabel = new Label("Group has no buttons.")
					{
						name = ToggleButtonGroup.emptyStateLabelClassName,
						classList = { ToggleButtonGroup.emptyStateLabelClassName }
					};
				}
				base.visualInput.Insert(0, this.m_EmptyLabel);
			}
			else
			{
				VisualElement emptyLabel = this.m_EmptyLabel;
				if (emptyLabel != null)
				{
					emptyLabel.RemoveFromHierarchy();
				}
			}
			base.SetValueWithoutNotify(newValue);
			this.UpdateButtonStates(newValue);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00032CB4 File Offset: 0x00030EB4
		private void OnButtonGroupContainerElementAdded(VisualElement ve)
		{
			Button button = ve as Button;
			bool flag = button == null;
			if (flag)
			{
				bool flag2 = ve == this.m_EmptyLabel;
				if (!flag2)
				{
					base.hierarchy.Add(ve);
				}
			}
			else
			{
				bool flag3 = this.m_Buttons.Count + 1 > 64;
				if (flag3)
				{
					Debug.LogWarning(ToggleButtonGroup.k_MaxToggleButtonGroupMessage);
				}
				else
				{
					button.AddToClassList(ToggleButtonGroup.buttonClassName);
					button.clickable.clickedWithEventInfo += this.OnOptionChange;
					this.m_Buttons = this.m_ButtonGroupContainer.Query(null, null).ToList();
					this.UpdateButtonsStyling();
					bool needsSetValue = false;
					ToggleButtonGroupState toggleButtonGroupState = this.value;
					bool flag4 = this.m_Buttons.Count >= this.value.length && this.m_Buttons.Count <= 64;
					if (flag4)
					{
						toggleButtonGroupState.length = this.m_Buttons.Count;
						needsSetValue = true;
					}
					bool flag5 = this.value.data == 0UL && !this.allowEmptySelection;
					if (flag5)
					{
						toggleButtonGroupState[0] = true;
						needsSetValue = true;
					}
					bool flag6 = needsSetValue;
					if (flag6)
					{
						this.value = toggleButtonGroupState;
					}
				}
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00032E08 File Offset: 0x00031008
		private unsafe void OnButtonGroupContainerElementRemoved(VisualElement ve)
		{
			Button button = ve as Button;
			bool flag = button == null;
			checked
			{
				if (!flag)
				{
					ToggleButtonGroupState toggleButtonGroupState = this.value;
					int checkedButtonIndex = this.m_Buttons.IndexOf(button);
					int length = toggleButtonGroupState.length;
					Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
					Span<int> selected = toggleButtonGroupState.GetActiveOptions(span);
					bool isRemovedButtonChecked = selected.IndexOf(checkedButtonIndex) != -1;
					button.clickable.clickedWithEventInfo -= this.OnOptionChange;
					bool flag2 = isRemovedButtonChecked;
					if (flag2)
					{
						this.m_Buttons[checkedButtonIndex].pseudoStates &= ~PseudoStates.Checked;
					}
					this.m_Buttons.Remove(button);
					this.UpdateButtonsStyling();
					toggleButtonGroupState.length = this.m_Buttons.Count;
					bool flag3 = this.m_Buttons.Count == 0;
					if (flag3)
					{
						toggleButtonGroupState.ResetAllOptions();
						this.SetValueWithoutNotify(toggleButtonGroupState);
					}
					else
					{
						bool flag4 = isRemovedButtonChecked;
						if (flag4)
						{
							toggleButtonGroupState[checkedButtonIndex] = false;
							bool flag5 = !this.allowEmptySelection && selected.Length == 1;
							if (flag5)
							{
								toggleButtonGroupState[0] = true;
							}
							this.value = toggleButtonGroupState;
						}
					}
				}
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00032F40 File Offset: 0x00031140
		private unsafe void UpdateButtonStates(ToggleButtonGroupState options)
		{
			int length = this.value.length;
			Span<int> span;
			checked
			{
				Span<int> span2 = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
				span = options.GetActiveOptions(span2);
			}
			for (int i = 0; i < this.m_Buttons.Count; i++)
			{
				bool flag = span.IndexOf(i) == -1;
				if (flag)
				{
					this.m_Buttons[i].pseudoStates &= ~PseudoStates.Checked;
					this.m_Buttons[i].IncrementVersion(VersionChangeType.Styles);
				}
				else
				{
					this.m_Buttons[i].pseudoStates |= PseudoStates.Checked;
					this.m_Buttons[i].IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00033010 File Offset: 0x00031210
		private unsafe void OnOptionChange(EventBase evt)
		{
			Button button = evt.target as Button;
			int index = this.m_Buttons.IndexOf(button);
			ToggleButtonGroupState toggleButtonGroupState = this.value;
			int length = toggleButtonGroupState.length;
			checked
			{
				Span<int> span = new Span<int>(stackalloc byte[unchecked((UIntPtr)length) * 4], length);
				Span<int> selected = toggleButtonGroupState.GetActiveOptions(span);
				bool showMixedValue = base.showMixedValue;
				if (showMixedValue)
				{
					ToggleButtonGroupState emptiedState = this.value;
					emptiedState.ResetAllOptions();
					bool flag = this.value != emptiedState;
					if (flag)
					{
						this.SetValueWithoutNotify(emptiedState);
					}
				}
				bool isMultipleSelection = this.isMultipleSelection;
				if (isMultipleSelection)
				{
					bool flag2 = !this.allowEmptySelection && selected.Length == 1 && toggleButtonGroupState[index];
					if (flag2)
					{
						return;
					}
					bool flag3 = toggleButtonGroupState[index];
					if (flag3)
					{
						toggleButtonGroupState[index] = false;
					}
					else
					{
						toggleButtonGroupState[index] = true;
					}
				}
				else
				{
					bool flag4 = this.allowEmptySelection && selected.Length == 1 && toggleButtonGroupState[*selected[0]];
					if (flag4)
					{
						toggleButtonGroupState[*selected[0]] = false;
						bool flag5 = index != *selected[0];
						if (flag5)
						{
							toggleButtonGroupState[index] = true;
						}
					}
					else
					{
						toggleButtonGroupState.ResetAllOptions();
						toggleButtonGroupState[index] = true;
					}
				}
				this.value = toggleButtonGroupState;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00033178 File Offset: 0x00031378
		private void UpdateButtonsStyling()
		{
			int buttonCount = this.m_Buttons.Count;
			for (int i = 0; i < buttonCount; i++)
			{
				Button button = this.m_Buttons[i];
				bool isStandaloneButton = buttonCount == 1;
				bool isLeftButton = i == 0 && !isStandaloneButton;
				bool isRightButton = i == buttonCount - 1 && !isStandaloneButton;
				bool isMiddleButton = !isLeftButton && !isRightButton && !isStandaloneButton;
				button.EnableInClassList(ToggleButtonGroup.buttonStandaloneClassName, isStandaloneButton);
				button.EnableInClassList(ToggleButtonGroup.buttonLeftClassName, isLeftButton);
				button.EnableInClassList(ToggleButtonGroup.buttonRightClassName, isRightButton);
				button.EnableInClassList(ToggleButtonGroup.buttonMidClassName, isMiddleButton);
			}
		}

		// Token: 0x040006C6 RID: 1734
		private static readonly string k_MaxToggleButtonGroupMessage = string.Format("The number of buttons added to ToggleButtonGroup exceeds the maximum allowed ({0}). The newly added button will not be treated as part of this control.", 64);

		// Token: 0x040006C7 RID: 1735
		internal static readonly BindingId isMultipleSelectionProperty = "isMultipleSelection";

		// Token: 0x040006C8 RID: 1736
		internal static readonly BindingId allowEmptySelectionProperty = "allowEmptySelection";

		// Token: 0x040006C9 RID: 1737
		public new static readonly string ussClassName = "unity-toggle-button-group";

		// Token: 0x040006CA RID: 1738
		public static readonly string containerUssClassName = ToggleButtonGroup.ussClassName + "__container";

		// Token: 0x040006CB RID: 1739
		public static readonly string buttonGroupClassName = "unity-button-group";

		// Token: 0x040006CC RID: 1740
		public static readonly string buttonClassName = ToggleButtonGroup.buttonGroupClassName + "__button";

		// Token: 0x040006CD RID: 1741
		public static readonly string buttonLeftClassName = ToggleButtonGroup.buttonClassName + "--left";

		// Token: 0x040006CE RID: 1742
		public static readonly string buttonMidClassName = ToggleButtonGroup.buttonClassName + "--mid";

		// Token: 0x040006CF RID: 1743
		public static readonly string buttonRightClassName = ToggleButtonGroup.buttonClassName + "--right";

		// Token: 0x040006D0 RID: 1744
		public static readonly string buttonStandaloneClassName = ToggleButtonGroup.buttonClassName + "--standalone";

		// Token: 0x040006D1 RID: 1745
		public static readonly string emptyStateLabelClassName = ToggleButtonGroup.buttonGroupClassName + "__empty-label";

		// Token: 0x040006D2 RID: 1746
		private VisualElement m_ButtonGroupContainer;

		// Token: 0x040006D3 RID: 1747
		private List<Button> m_Buttons = new List<Button>();

		// Token: 0x040006D4 RID: 1748
		private VisualElement m_EmptyLabel;

		// Token: 0x040006D5 RID: 1749
		private bool m_IsMultipleSelection;

		// Token: 0x040006D6 RID: 1750
		private bool m_AllowEmptySelection;

		// Token: 0x0200015D RID: 349
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ToggleButtonGroup, ToggleButtonGroup.UxmlTraits>
		{
		}

		// Token: 0x0200015E RID: 350
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<ToggleButtonGroupState>.UxmlTraits
		{
			// Token: 0x06000A77 RID: 2679 RVA: 0x0003330C File Offset: 0x0003150C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ToggleButtonGroup toggleButtonGroup = (ToggleButtonGroup)ve;
				toggleButtonGroup.isMultipleSelection = this.m_IsMultipleSelection.GetValueFromBag(bag, cc);
				toggleButtonGroup.allowEmptySelection = this.m_AllowEmptySelection.GetValueFromBag(bag, cc);
			}

			// Token: 0x040006D7 RID: 1751
			private UxmlBoolAttributeDescription m_IsMultipleSelection = new UxmlBoolAttributeDescription
			{
				name = "is-multiple-selection"
			};

			// Token: 0x040006D8 RID: 1752
			private UxmlBoolAttributeDescription m_AllowEmptySelection = new UxmlBoolAttributeDescription
			{
				name = "allow-empty-selection"
			};
		}
	}
}
