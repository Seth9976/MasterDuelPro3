using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E1 RID: 225
	public abstract class BaseField<TValueType> : BindableElement, INotifyValueChanged<TValueType>, IEditableElement
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000205DC File Offset: 0x0001E7DC
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x000205F4 File Offset: 0x0001E7F4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement visualInput
		{
			get
			{
				return this.m_VisualInput;
			}
			set
			{
				bool flag = this.m_VisualInput != null;
				if (flag)
				{
					bool flag2 = this.m_VisualInput.parent == this;
					if (flag2)
					{
						this.m_VisualInput.RemoveFromHierarchy();
					}
					this.m_VisualInput = null;
				}
				bool flag3 = value != null;
				if (flag3)
				{
					this.m_VisualInput = value;
				}
				else
				{
					this.m_VisualInput = new VisualElement
					{
						pickingMode = PickingMode.Ignore
					};
				}
				this.m_VisualInput.focusable = true;
				this.m_VisualInput.AddToClassList(BaseField<TValueType>.inputUssClassName);
				base.Add(this.m_VisualInput);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0002068C File Offset: 0x0001E88C
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x000206A4 File Offset: 0x0001E8A4
		protected TValueType rawValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060006C7 RID: 1735 RVA: 0x000206B0 File Offset: 0x0001E8B0
		// (remove) Token: 0x060006C8 RID: 1736 RVA: 0x000206E8 File Offset: 0x0001E8E8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Func<TValueType, TValueType> onValidateValue;

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0002071D File Offset: 0x0001E91D
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal DispatchMode dispatchMode { get; } = DispatchMode.Default;

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00020728 File Offset: 0x0001E928
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00020740 File Offset: 0x0001E940
		[CreateProperty]
		public virtual TValueType value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !this.EqualsCurrentValue(value) || this.showMixedValue;
				if (flag)
				{
					TValueType previousValue = this.m_Value;
					this.SetValueWithoutNotify(value);
					this.showMixedValue = false;
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<TValueType> evt = ChangeEvent<TValueType>.GetPooled(previousValue, this.m_Value))
						{
							evt.elementTarget = this;
							this.SendEvent(evt, this.dispatchMode);
						}
						base.NotifyPropertyChanged(in BaseField<TValueType>.valueProperty);
					}
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000207DC File Offset: 0x0001E9DC
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x000207E4 File Offset: 0x0001E9E4
		public Label labelElement { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000207F0 File Offset: 0x0001E9F0
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00020810 File Offset: 0x0001EA10
		[CreateProperty]
		public string label
		{
			get
			{
				return this.labelElement.text;
			}
			set
			{
				bool flag = this.labelElement.text != value;
				if (flag)
				{
					this.labelElement.text = value;
					bool flag2 = string.IsNullOrEmpty(this.labelElement.text);
					if (flag2)
					{
						base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						this.labelElement.RemoveFromHierarchy();
					}
					else
					{
						bool flag3 = !base.Contains(this.labelElement);
						if (flag3)
						{
							base.hierarchy.Insert(0, this.labelElement);
							base.RemoveFromClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						}
					}
					base.NotifyPropertyChanged(in BaseField<TValueType>.labelProperty);
				}
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000208BA File Offset: 0x0001EABA
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x000208C4 File Offset: 0x0001EAC4
		[CreateProperty]
		public bool showMixedValue
		{
			get
			{
				return this.m_ShowMixedValue;
			}
			set
			{
				bool flag = value == this.m_ShowMixedValue;
				if (!flag)
				{
					this.m_ShowMixedValue = value;
					this.UpdateMixedValueContent();
					base.NotifyPropertyChanged(in BaseField<TValueType>.showMixedValueProperty);
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000208FC File Offset: 0x0001EAFC
		protected Label mixedValueLabel
		{
			get
			{
				bool flag = this.m_MixedValueLabel == null;
				if (flag)
				{
					this.m_MixedValueLabel = new Label(BaseField<TValueType>.mixedValueString)
					{
						focusable = true,
						tabIndex = -1
					};
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.labelUssClassName);
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				return this.m_MixedValueLabel;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00020965 File Offset: 0x0001EB65
		Action IEditableElement.editingStarted { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0002096D File Offset: 0x0001EB6D
		Action IEditableElement.editingEnded { get; }

		// Token: 0x060006D5 RID: 1749 RVA: 0x00020978 File Offset: 0x0001EB78
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal BaseField(string label)
		{
			base.isCompositeRoot = true;
			this.focusable = true;
			base.tabIndex = 0;
			base.excludeFromFocusRing = true;
			base.delegatesFocus = true;
			base.AddToClassList(BaseField<TValueType>.ussClassName);
			this.labelElement = new Label
			{
				focusable = true,
				tabIndex = -1
			};
			this.labelElement.AddToClassList(BaseField<TValueType>.labelUssClassName);
			bool flag = label != null;
			if (flag)
			{
				this.label = label;
			}
			else
			{
				base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
			}
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.m_VisualInput = null;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00020A46 File Offset: 0x0001EC46
		protected BaseField(string label, VisualElement visualInput)
			: this(label)
		{
			this.visualInput = visualInput;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00020A59 File Offset: 0x0001EC59
		internal virtual bool EqualsCurrentValue(TValueType value)
		{
			return EqualityComparer<TValueType>.Default.Equals(this.m_Value, value);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00020A6C File Offset: 0x0001EC6C
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			this.RegisterEditingCallbacks();
			bool flag = e.destinationPanel == null;
			if (!flag)
			{
				bool flag2 = e.destinationPanel.contextType == ContextType.Player;
				if (!flag2)
				{
					this.m_CachedInspectorElement = null;
					this.m_CachedContextWidthElement = null;
					for (VisualElement currentElement = base.parent; currentElement != null; currentElement = currentElement.parent)
					{
						bool flag3 = currentElement.ClassListContains("unity-inspector-element");
						if (flag3)
						{
							this.m_CachedInspectorElement = currentElement;
						}
						bool flag4 = currentElement.ClassListContains("unity-inspector-main-container");
						if (flag4)
						{
							this.m_CachedContextWidthElement = currentElement;
							break;
						}
					}
					bool flag5 = this.m_CachedInspectorElement == null;
					if (flag5)
					{
						base.RemoveFromClassList(BaseField<TValueType>.inspectorFieldUssClassName);
					}
					else
					{
						this.m_LabelWidthRatio = 0.45f;
						this.m_LabelExtraPadding = 37f;
						this.m_LabelBaseMinWidth = 123f;
						this.m_LabelExtraContextWidth = 1f;
						base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
						base.AddToClassList(BaseField<TValueType>.inspectorFieldUssClassName);
						base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnInspectorFieldGeometryChanged), TrickleDown.NoTrickleDown);
					}
				}
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00020B87 File Offset: 0x0001ED87
		private void OnDetachFromPanel(DetachFromPanelEvent e)
		{
			this.UnregisterEditingCallbacks();
			this.onValidateValue = null;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00020B98 File Offset: 0x0001ED98
		internal virtual void RegisterEditingCallbacks()
		{
			base.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.StartEditing), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00020BC3 File Offset: 0x0001EDC3
		internal virtual void UnregisterEditingCallbacks()
		{
			base.UnregisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.StartEditing), TrickleDown.NoTrickleDown);
			base.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00020BEE File Offset: 0x0001EDEE
		internal void StartEditing(EventBase e)
		{
			Action editingStarted = ((IEditableElement)this).editingStarted;
			if (editingStarted != null)
			{
				editingStarted();
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00020C03 File Offset: 0x0001EE03
		internal void EndEditing(EventBase e)
		{
			Action editingEnded = ((IEditableElement)this).editingEnded;
			if (editingEnded != null)
			{
				editingEnded();
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00020C18 File Offset: 0x0001EE18
		private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			float labelWidthRatio;
			bool flag = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelWidthRatioProperty, out labelWidthRatio);
			if (flag)
			{
				this.m_LabelWidthRatio = labelWidthRatio;
			}
			float labelExtraPadding;
			bool flag2 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelExtraPaddingProperty, out labelExtraPadding);
			if (flag2)
			{
				this.m_LabelExtraPadding = labelExtraPadding;
			}
			float labelBaseMinWidth;
			bool flag3 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelBaseMinWidthProperty, out labelBaseMinWidth);
			if (flag3)
			{
				this.m_LabelBaseMinWidth = labelBaseMinWidth;
			}
			float labelExtraContextWidth;
			bool flag4 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelExtraContextWidthProperty, out labelExtraContextWidth);
			if (flag4)
			{
				this.m_LabelExtraContextWidth = labelExtraContextWidth;
			}
			this.AlignLabel();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		private void OnInspectorFieldGeometryChanged(GeometryChangedEvent e)
		{
			this.AlignLabel();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00020CBC File Offset: 0x0001EEBC
		private void AlignLabel()
		{
			bool flag = !base.ClassListContains(BaseField<TValueType>.alignedFieldUssClassName) || this.m_CachedInspectorElement == null;
			if (!flag)
			{
				float totalPadding = this.m_LabelExtraPadding;
				float spacing = base.worldBound.x - this.m_CachedInspectorElement.worldBound.x - this.m_CachedInspectorElement.resolvedStyle.paddingLeft;
				totalPadding += spacing;
				totalPadding += base.resolvedStyle.paddingLeft;
				float minWidth = this.m_LabelBaseMinWidth - spacing - base.resolvedStyle.paddingLeft;
				VisualElement contextWidthElement = this.m_CachedContextWidthElement ?? this.m_CachedInspectorElement;
				this.labelElement.style.minWidth = Mathf.Max(minWidth, 0f);
				float newWidth = (contextWidthElement.resolvedStyle.width + this.m_LabelExtraContextWidth) * this.m_LabelWidthRatio - totalPadding;
				bool flag2 = Mathf.Abs(this.labelElement.resolvedStyle.width - newWidth) > 1E-30f;
				if (flag2)
				{
					this.labelElement.style.width = Mathf.Max(0f, newWidth);
				}
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00020DEC File Offset: 0x0001EFEC
		internal TValueType ValidatedValue(TValueType value)
		{
			bool flag = this.onValidateValue != null;
			TValueType tvalueType;
			if (flag)
			{
				tvalueType = this.onValidateValue(value);
			}
			else
			{
				tvalueType = value;
			}
			return tvalueType;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00020E1C File Offset: 0x0001F01C
		protected virtual void UpdateMixedValueContent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00020E24 File Offset: 0x0001F024
		public virtual void SetValueWithoutNotify(TValueType newValue)
		{
			bool skipValidation = this.m_SkipValidation;
			if (skipValidation)
			{
				this.m_Value = newValue;
			}
			else
			{
				this.m_Value = this.ValidatedValue(newValue);
			}
			bool flag = !string.IsNullOrEmpty(base.viewDataKey);
			if (flag)
			{
				base.SaveViewData();
			}
			base.MarkDirtyRepaint();
			bool showMixedValue = this.showMixedValue;
			if (showMixedValue)
			{
				this.UpdateMixedValueContent();
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00020E88 File Offset: 0x0001F088
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.m_VisualInput != null;
			if (flag)
			{
				string key = base.GetFullHierarchicalViewDataKey();
				TValueType oldValue = this.m_Value;
				base.OverwriteFromViewData(this, key);
				bool flag2 = !EqualityComparer<TValueType>.Default.Equals(oldValue, this.m_Value);
				if (flag2)
				{
					using (ChangeEvent<TValueType> evt = ChangeEvent<TValueType>.GetPooled(oldValue, this.m_Value))
					{
						evt.elementTarget = this;
						this.SetValueWithoutNotify(this.m_Value);
						this.SendEvent(evt);
					}
				}
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00020F2C File Offset: 0x0001F12C
		internal override Rect GetTooltipRect()
		{
			return (!string.IsNullOrEmpty(this.label)) ? this.labelElement.worldBound : base.worldBound;
		}

		// Token: 0x04000431 RID: 1073
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x04000432 RID: 1074
		internal static readonly BindingId labelProperty = "label";

		// Token: 0x04000433 RID: 1075
		internal static readonly BindingId showMixedValueProperty = "showMixedValue";

		// Token: 0x04000434 RID: 1076
		public static readonly string ussClassName = "unity-base-field";

		// Token: 0x04000435 RID: 1077
		public static readonly string labelUssClassName = BaseField<TValueType>.ussClassName + "__label";

		// Token: 0x04000436 RID: 1078
		public static readonly string inputUssClassName = BaseField<TValueType>.ussClassName + "__input";

		// Token: 0x04000437 RID: 1079
		public static readonly string noLabelVariantUssClassName = BaseField<TValueType>.ussClassName + "--no-label";

		// Token: 0x04000438 RID: 1080
		public static readonly string labelDraggerVariantUssClassName = BaseField<TValueType>.labelUssClassName + "--with-dragger";

		// Token: 0x04000439 RID: 1081
		public static readonly string mixedValueLabelUssClassName = BaseField<TValueType>.labelUssClassName + "--mixed-value";

		// Token: 0x0400043A RID: 1082
		public static readonly string alignedFieldUssClassName = BaseField<TValueType>.ussClassName + "__aligned";

		// Token: 0x0400043B RID: 1083
		private static readonly string inspectorFieldUssClassName = BaseField<TValueType>.ussClassName + "__inspector-field";

		// Token: 0x0400043C RID: 1084
		protected internal static readonly string mixedValueString = "—";

		// Token: 0x0400043D RID: 1085
		protected internal static readonly PropertyName serializedPropertyCopyName = "SerializedPropertyCopyName";

		// Token: 0x0400043E RID: 1086
		private static CustomStyleProperty<float> s_LabelWidthRatioProperty = new CustomStyleProperty<float>("--unity-property-field-label-width-ratio");

		// Token: 0x0400043F RID: 1087
		private static CustomStyleProperty<float> s_LabelExtraPaddingProperty = new CustomStyleProperty<float>("--unity-property-field-label-extra-padding");

		// Token: 0x04000440 RID: 1088
		private static CustomStyleProperty<float> s_LabelBaseMinWidthProperty = new CustomStyleProperty<float>("--unity-property-field-label-base-min-width");

		// Token: 0x04000441 RID: 1089
		private static CustomStyleProperty<float> s_LabelExtraContextWidthProperty = new CustomStyleProperty<float>("--unity-base-field-extra-context-width");

		// Token: 0x04000442 RID: 1090
		private float m_LabelWidthRatio;

		// Token: 0x04000443 RID: 1091
		private float m_LabelExtraPadding;

		// Token: 0x04000444 RID: 1092
		private float m_LabelBaseMinWidth;

		// Token: 0x04000445 RID: 1093
		private float m_LabelExtraContextWidth;

		// Token: 0x04000446 RID: 1094
		private VisualElement m_VisualInput;

		// Token: 0x04000447 RID: 1095
		[SerializeField]
		[DontCreateProperty]
		private TValueType m_Value;

		// Token: 0x0400044B RID: 1099
		private bool m_ShowMixedValue;

		// Token: 0x0400044C RID: 1100
		private Label m_MixedValueLabel;

		// Token: 0x0400044D RID: 1101
		private bool m_SkipValidation;

		// Token: 0x0400044E RID: 1102
		private VisualElement m_CachedContextWidthElement;

		// Token: 0x0400044F RID: 1103
		private VisualElement m_CachedInspectorElement;

		// Token: 0x020000E2 RID: 226
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x060006E7 RID: 1767 RVA: 0x00021085 File Offset: 0x0001F285
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x000210C0 File Offset: 0x0001F2C0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((BaseField<TValueType>)ve).label = this.m_Label.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000452 RID: 1106
			private UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};
		}
	}
}
