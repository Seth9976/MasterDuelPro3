using System;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EA RID: 234
	public abstract class TextInputBaseField<TValueType> : BaseField<TValueType>, IDelayedField
	{
		// Token: 0x1700010E RID: 270
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00021DB0 File Offset: 0x0001FFB0
		internal bool password
		{
			set
			{
				this.textEdition.isPassword = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00021DBF File Offset: 0x0001FFBF
		internal bool readOnly
		{
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00021DC9 File Offset: 0x0001FFC9
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00021DD8 File Offset: 0x0001FFD8
		[CreateProperty]
		internal string placeholderText
		{
			get
			{
				return this.textEdition.placeholder;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set
			{
				bool flag = this.textEdition.placeholder == value;
				if (!flag)
				{
					this.textEdition.placeholder = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.placeholderTextProperty);
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00021E16 File Offset: 0x00020016
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00021E24 File Offset: 0x00020024
		[CreateProperty]
		internal bool hidePlaceholderOnFocus
		{
			get
			{
				return this.textEdition.hidePlaceholderOnFocus;
			}
			set
			{
				bool flag = this.textEdition.hidePlaceholderOnFocus == value;
				if (!flag)
				{
					this.textEdition.hidePlaceholderOnFocus = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.hidePlaceholderOnFocusProperty);
				}
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00021E60 File Offset: 0x00020060
		protected TextInputBaseField(string label, int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: base(label, textInputBase)
		{
			base.tabIndex = 0;
			base.delegatesFocus = true;
			base.labelElement.tabIndex = -1;
			base.AddToClassList(TextInputBaseField<TValueType>.ussClassName);
			base.labelElement.AddToClassList(TextInputBaseField<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
			this.m_TextInputBase = textInputBase;
			this.m_TextInputBase.textEdition.maxLength = maxLength;
			this.m_TextInputBase.textEdition.maskChar = maskChar;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnFieldCustomStyleResolved), TrickleDown.NoTrickleDown);
			TextElement textElement = textInputBase.textElement;
			textElement.OnPlaceholderChanged = (Action)Delegate.Combine(textElement.OnPlaceholderChanged, new Action(this.OnPlaceholderChanged));
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00021F3D File Offset: 0x0002013D
		protected internal TextInputBaseField<TValueType>.TextInputBase textInputBase
		{
			get
			{
				return this.m_TextInputBase;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00021F45 File Offset: 0x00020145
		[CreateProperty(ReadOnly = true)]
		public ITextSelection textSelection
		{
			get
			{
				return this.m_TextInputBase.textElement.selection;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00021F57 File Offset: 0x00020157
		[CreateProperty(ReadOnly = true)]
		public ITextEdition textEdition
		{
			get
			{
				return this.m_TextInputBase.textElement.edition;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00021F69 File Offset: 0x00020169
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00021F7B File Offset: 0x0002017B
		protected Action<bool> onIsReadOnlyChanged
		{
			get
			{
				return this.m_TextInputBase.textElement.onIsReadOnlyChanged;
			}
			set
			{
				this.m_TextInputBase.textElement.onIsReadOnlyChanged = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00021F8E File Offset: 0x0002018E
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00021F9C File Offset: 0x0002019C
		[CreateProperty]
		public bool isReadOnly
		{
			get
			{
				return this.textEdition.isReadOnly;
			}
			set
			{
				bool flag = this.textEdition.isReadOnly == value;
				if (!flag)
				{
					this.textEdition.isReadOnly = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.isReadOnlyProperty);
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00021FD7 File Offset: 0x000201D7
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x00021FE4 File Offset: 0x000201E4
		[CreateProperty]
		public bool isPasswordField
		{
			get
			{
				return this.textEdition.isPassword;
			}
			set
			{
				bool flag = this.textEdition.isPassword == value;
				if (!flag)
				{
					this.textEdition.isPassword = value;
					this.m_TextInputBase.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.isPasswordFieldProperty);
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00022030 File Offset: 0x00020230
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00022040 File Offset: 0x00020240
		[CreateProperty]
		public bool autoCorrection
		{
			get
			{
				return this.textEdition.autoCorrection;
			}
			set
			{
				bool flag = this.textEdition.autoCorrection == value;
				if (!flag)
				{
					this.textEdition.autoCorrection = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.autoCorrectionProperty);
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0002207B File Offset: 0x0002027B
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00022088 File Offset: 0x00020288
		[CreateProperty]
		public bool hideMobileInput
		{
			get
			{
				return this.textEdition.hideMobileInput;
			}
			set
			{
				bool flag = this.textEdition.hideMobileInput == value;
				if (!flag)
				{
					this.textEdition.hideMobileInput = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.hideMobileInputProperty);
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x000220C3 File Offset: 0x000202C3
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x000220D0 File Offset: 0x000202D0
		[CreateProperty]
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.textEdition.keyboardType;
			}
			set
			{
				bool flag = this.textEdition.keyboardType == value;
				if (!flag)
				{
					this.textEdition.keyboardType = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.keyboardTypeProperty);
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0002210B File Offset: 0x0002030B
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00022118 File Offset: 0x00020318
		[CreateProperty]
		public int maxLength
		{
			get
			{
				return this.textEdition.maxLength;
			}
			set
			{
				bool flag = this.textEdition.maxLength == value;
				if (!flag)
				{
					this.textEdition.maxLength = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.maxLengthProperty);
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00022153 File Offset: 0x00020353
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00022160 File Offset: 0x00020360
		[CreateProperty]
		public bool isDelayed
		{
			get
			{
				return this.textEdition.isDelayed;
			}
			set
			{
				bool flag = this.textEdition.isDelayed == value;
				if (!flag)
				{
					this.textEdition.isDelayed = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.isDelayedProperty);
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0002219B File Offset: 0x0002039B
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x000221A8 File Offset: 0x000203A8
		[CreateProperty]
		public char maskChar
		{
			get
			{
				return this.textEdition.maskChar;
			}
			set
			{
				bool flag = this.textEdition.maskChar == value;
				if (!flag)
				{
					this.textEdition.maskChar = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.maskCharProperty);
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000221E3 File Offset: 0x000203E3
		[CreateProperty(ReadOnly = true)]
		public Color selectionColor
		{
			get
			{
				return this.textSelection.selectionColor;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000221F0 File Offset: 0x000203F0
		[CreateProperty(ReadOnly = true)]
		public Color cursorColor
		{
			get
			{
				return this.textSelection.cursorColor;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x000221FD File Offset: 0x000203FD
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0002220C File Offset: 0x0002040C
		[CreateProperty]
		public int cursorIndex
		{
			get
			{
				return this.textSelection.cursorIndex;
			}
			set
			{
				bool flag = this.textSelection.cursorIndex == value;
				if (!flag)
				{
					this.textSelection.cursorIndex = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.cursorIndexProperty);
				}
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00022247 File Offset: 0x00020447
		[CreateProperty(ReadOnly = true)]
		public Vector2 cursorPosition
		{
			get
			{
				return this.textSelection.cursorPosition;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00022254 File Offset: 0x00020454
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00022264 File Offset: 0x00020464
		[CreateProperty]
		public int selectIndex
		{
			get
			{
				return this.textSelection.selectIndex;
			}
			set
			{
				bool flag = this.textSelection.selectIndex == value;
				if (!flag)
				{
					this.textSelection.selectIndex = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.selectIndexProperty);
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0002229F File Offset: 0x0002049F
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x000222AC File Offset: 0x000204AC
		[CreateProperty]
		public bool selectAllOnFocus
		{
			get
			{
				return this.textSelection.selectAllOnFocus;
			}
			set
			{
				bool flag = this.textSelection.selectAllOnFocus == value;
				if (!flag)
				{
					this.textSelection.selectAllOnFocus = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.selectAllOnFocusProperty);
				}
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000222E7 File Offset: 0x000204E7
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x000222F4 File Offset: 0x000204F4
		[CreateProperty]
		public bool selectAllOnMouseUp
		{
			get
			{
				return this.textSelection.selectAllOnMouseUp;
			}
			set
			{
				bool flag = this.textSelection.selectAllOnMouseUp == value;
				if (!flag)
				{
					this.textSelection.selectAllOnMouseUp = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.selectAllOnMouseUpProperty);
				}
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0002232F File Offset: 0x0002052F
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0002233C File Offset: 0x0002053C
		[CreateProperty]
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.textSelection.doubleClickSelectsWord;
			}
			set
			{
				bool flag = this.textSelection.doubleClickSelectsWord == value;
				if (!flag)
				{
					this.textSelection.doubleClickSelectsWord = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.doubleClickSelectsWordProperty);
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00022377 File Offset: 0x00020577
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00022384 File Offset: 0x00020584
		[CreateProperty]
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.textSelection.tripleClickSelectsLine;
			}
			set
			{
				bool flag = this.textSelection.tripleClickSelectsLine == value;
				if (!flag)
				{
					this.textSelection.tripleClickSelectsLine = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.tripleClickSelectsLineProperty);
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000223BF File Offset: 0x000205BF
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x000223CC File Offset: 0x000205CC
		public string text
		{
			get
			{
				return this.m_TextInputBase.text;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			protected internal set
			{
				this.m_TextInputBase.text = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000223DB File Offset: 0x000205DB
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x000223F0 File Offset: 0x000205F0
		[CreateProperty]
		public bool emojiFallbackSupport
		{
			get
			{
				return this.m_TextInputBase.textElement.emojiFallbackSupport;
			}
			set
			{
				bool flag = this.m_TextInputBase.textElement.emojiFallbackSupport == value;
				if (!flag)
				{
					base.labelElement.emojiFallbackSupport = value;
					this.m_TextInputBase.textElement.emojiFallbackSupport = value;
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.emojiFallbackSupportProperty);
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00022442 File Offset: 0x00020642
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00022450 File Offset: 0x00020650
		[CreateProperty]
		public ScrollerVisibility verticalScrollerVisibility
		{
			get
			{
				return this.textInputBase.verticalScrollerVisibility;
			}
			set
			{
				bool flag = this.textInputBase.verticalScrollerVisibility == value;
				if (!flag)
				{
					this.textInputBase.SetVerticalScrollerVisibility(value);
					base.NotifyPropertyChanged(in TextInputBaseField<TValueType>.verticalScrollerVisibilityProperty);
				}
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002248C File Offset: 0x0002068C
		[EventInterest(new Type[]
		{
			typeof(NavigationSubmitEvent),
			typeof(FocusInEvent),
			typeof(FocusEvent),
			typeof(FocusOutEvent),
			typeof(BlurEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool isReadOnly = this.textEdition.isReadOnly;
			if (!isReadOnly)
			{
				bool flag = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId() && evt.target != this.textInputBase.textElement;
				if (flag)
				{
					this.textInputBase.textElement.Focus();
				}
				else
				{
					bool flag2 = evt.eventTypeId == EventBase<NavigationMoveEvent>.TypeId() && evt.target != this.textInputBase.textElement;
					if (flag2)
					{
						this.focusController.SwitchFocusOnEvent(this.textInputBase.textElement, evt);
					}
					else
					{
						bool flag3 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
						if (flag3)
						{
							bool showMixedValue = base.showMixedValue;
							if (showMixedValue)
							{
								((INotifyValueChanged<string>)this.textInputBase.textElement).SetValueWithoutNotify(null);
							}
						}
						else
						{
							bool flag4 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
							if (flag4)
							{
								this.UpdatePlaceholderClassList(null);
							}
							else
							{
								bool flag5 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
								if (flag5)
								{
									bool showMixedValue2 = base.showMixedValue;
									if (showMixedValue2)
									{
										this.UpdateMixedValueContent();
									}
									this.UpdatePlaceholderClassList(null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000742 RID: 1858
		protected abstract string ValueToString(TValueType value);

		// Token: 0x06000743 RID: 1859
		protected abstract TValueType StringToValue(string str);

		// Token: 0x06000744 RID: 1860 RVA: 0x000225C4 File Offset: 0x000207C4
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				((INotifyValueChanged<string>)this.textInputBase.textElement).SetValueWithoutNotify(BaseField<TValueType>.mixedValueString);
				base.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				VisualElement visualInput = base.visualInput;
				if (visualInput != null)
				{
					visualInput.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
			}
			else
			{
				this.UpdateTextFromValue();
				VisualElement visualInput2 = base.visualInput;
				if (visualInput2 != null)
				{
					visualInput2.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				base.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00022648 File Offset: 0x00020848
		internal void OnPlaceholderChanged()
		{
			bool flag = !string.IsNullOrEmpty(this.textEdition.placeholder);
			if (flag)
			{
				base.RegisterCallback<ChangeEvent<TValueType>>(new EventCallback<ChangeEvent<TValueType>>(this.UpdatePlaceholderClassList), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.UnregisterCallback<ChangeEvent<TValueType>>(new EventCallback<ChangeEvent<TValueType>>(this.UpdatePlaceholderClassList), TrickleDown.NoTrickleDown);
			}
			this.UpdatePlaceholderClassList(null);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000226A0 File Offset: 0x000208A0
		internal void UpdatePlaceholderClassList(ChangeEvent<TValueType> evt = null)
		{
			bool showPlaceholderText = this.textInputBase.textElement.showPlaceholderText;
			if (showPlaceholderText)
			{
				base.visualInput.AddToClassList(TextInputBaseField<TValueType>.placeholderUssClassName);
			}
			else
			{
				base.visualInput.RemoveFromClassList(TextInputBaseField<TValueType>.placeholderUssClassName);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000226E6 File Offset: 0x000208E6
		internal virtual void UpdateValueFromText()
		{
			this.value = this.StringToValue(this.text);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000020EA File Offset: 0x000002EA
		internal virtual void UpdateTextFromValue()
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000226FC File Offset: 0x000208FC
		private void OnFieldCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this.m_TextInputBase.OnInputCustomStyleResolved(e);
		}

		// Token: 0x0400045E RID: 1118
		internal static readonly BindingId autoCorrectionProperty = "autoCorrection";

		// Token: 0x0400045F RID: 1119
		internal static readonly BindingId hideMobileInputProperty = "hideMobileInput";

		// Token: 0x04000460 RID: 1120
		internal static readonly BindingId hidePlaceholderOnFocusProperty = "hidePlaceholderOnFocus";

		// Token: 0x04000461 RID: 1121
		internal static readonly BindingId keyboardTypeProperty = "keyboardType";

		// Token: 0x04000462 RID: 1122
		internal static readonly BindingId isReadOnlyProperty = "isReadOnly";

		// Token: 0x04000463 RID: 1123
		internal static readonly BindingId isPasswordFieldProperty = "isPasswordField";

		// Token: 0x04000464 RID: 1124
		internal static readonly BindingId textSelectionProperty = "textSelection";

		// Token: 0x04000465 RID: 1125
		internal static readonly BindingId textEditionProperty = "textEdition";

		// Token: 0x04000466 RID: 1126
		internal static readonly BindingId placeholderTextProperty = "placeholderText";

		// Token: 0x04000467 RID: 1127
		internal static readonly BindingId selectionColorProperty = "selectionColor";

		// Token: 0x04000468 RID: 1128
		internal static readonly BindingId cursorColorProperty = "cursorColor";

		// Token: 0x04000469 RID: 1129
		internal static readonly BindingId cursorIndexProperty = "cursorIndex";

		// Token: 0x0400046A RID: 1130
		internal static readonly BindingId cursorPositionProperty = "cursorPosition";

		// Token: 0x0400046B RID: 1131
		internal static readonly BindingId selectIndexProperty = "selectIndex";

		// Token: 0x0400046C RID: 1132
		internal static readonly BindingId selectAllOnFocusProperty = "selectAllOnFocus";

		// Token: 0x0400046D RID: 1133
		internal static readonly BindingId selectAllOnMouseUpProperty = "selectAllOnMouseUp";

		// Token: 0x0400046E RID: 1134
		internal static readonly BindingId maxLengthProperty = "maxLength";

		// Token: 0x0400046F RID: 1135
		internal static readonly BindingId doubleClickSelectsWordProperty = "doubleClickSelectsWord";

		// Token: 0x04000470 RID: 1136
		internal static readonly BindingId tripleClickSelectsLineProperty = "tripleClickSelectsLine";

		// Token: 0x04000471 RID: 1137
		internal static readonly BindingId emojiFallbackSupportProperty = "emojiFallbackSupport";

		// Token: 0x04000472 RID: 1138
		internal static readonly BindingId isDelayedProperty = "isDelayed";

		// Token: 0x04000473 RID: 1139
		internal static readonly BindingId maskCharProperty = "maskChar";

		// Token: 0x04000474 RID: 1140
		internal static readonly BindingId verticalScrollerVisibilityProperty = "verticalScrollerVisibility";

		// Token: 0x04000475 RID: 1141
		private static CustomStyleProperty<Color> s_SelectionColorProperty = new CustomStyleProperty<Color>("--unity-selection-color");

		// Token: 0x04000476 RID: 1142
		private static CustomStyleProperty<Color> s_CursorColorProperty = new CustomStyleProperty<Color>("--unity-cursor-color");

		// Token: 0x04000477 RID: 1143
		public new static readonly string ussClassName = "unity-base-text-field";

		// Token: 0x04000478 RID: 1144
		public new static readonly string labelUssClassName = TextInputBaseField<TValueType>.ussClassName + "__label";

		// Token: 0x04000479 RID: 1145
		public new static readonly string inputUssClassName = TextInputBaseField<TValueType>.ussClassName + "__input";

		// Token: 0x0400047A RID: 1146
		internal static readonly string multilineContainerClassName = TextInputBaseField<TValueType>.ussClassName + "__multiline-container";

		// Token: 0x0400047B RID: 1147
		public static readonly string singleLineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--single-line";

		// Token: 0x0400047C RID: 1148
		public static readonly string multilineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--multiline";

		// Token: 0x0400047D RID: 1149
		public static readonly string placeholderUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--placeholder";

		// Token: 0x0400047E RID: 1150
		internal static readonly string multilineInputWithScrollViewUssClassName = TextInputBaseField<TValueType>.multilineInputUssClassName + "--scroll-view";

		// Token: 0x0400047F RID: 1151
		public static readonly string textInputUssName = "unity-text-input";

		// Token: 0x04000480 RID: 1152
		private TextInputBaseField<TValueType>.TextInputBase m_TextInputBase;

		// Token: 0x020000EB RID: 235
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<string, UxmlStringAttributeDescription>
		{
			// Token: 0x0600074B RID: 1867 RVA: 0x00022930 File Offset: 0x00020B30
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextInputBaseField<TValueType> field = (TextInputBaseField<TValueType>)ve;
				field.maxLength = this.m_MaxLength.GetValueFromBag(bag, cc);
				field.password = this.m_Password.GetValueFromBag(bag, cc);
				field.readOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				field.isDelayed = this.m_IsDelayed.GetValueFromBag(bag, cc);
				field.textSelection.selectAllOnFocus = this.m_SelectAllOnFocus.GetValueFromBag(bag, cc);
				field.textSelection.selectAllOnMouseUp = this.m_SelectAllOnMouseUp.GetValueFromBag(bag, cc);
				field.doubleClickSelectsWord = this.m_SelectWordByDoubleClick.GetValueFromBag(bag, cc);
				field.tripleClickSelectsLine = this.m_SelectLineByTripleClick.GetValueFromBag(bag, cc);
				field.emojiFallbackSupport = this.m_EmojiFallbackSupport.GetValueFromBag(bag, cc);
				ScrollerVisibility verticalScrollerVisibility = ScrollerVisibility.Hidden;
				this.m_VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref verticalScrollerVisibility);
				field.verticalScrollerVisibility = verticalScrollerVisibility;
				field.hideMobileInput = this.m_HideMobileInput.GetValueFromBag(bag, cc);
				field.keyboardType = this.m_KeyboardType.GetValueFromBag(bag, cc);
				field.autoCorrection = this.m_AutoCorrection.GetValueFromBag(bag, cc);
				string maskCharacter = this.m_MaskCharacter.GetValueFromBag(bag, cc);
				field.maskChar = (string.IsNullOrEmpty(maskCharacter) ? '*' : maskCharacter[0]);
				field.placeholderText = this.m_PlaceholderText.GetValueFromBag(bag, cc);
				field.hidePlaceholderOnFocus = this.m_HidePlaceholderOnFocus.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000481 RID: 1153
			private UxmlIntAttributeDescription m_MaxLength = new UxmlIntAttributeDescription
			{
				name = "max-length",
				obsoleteNames = new string[] { "maxLength" },
				defaultValue = -1
			};

			// Token: 0x04000482 RID: 1154
			private UxmlBoolAttributeDescription m_Password = new UxmlBoolAttributeDescription
			{
				name = "password"
			};

			// Token: 0x04000483 RID: 1155
			private UxmlStringAttributeDescription m_MaskCharacter = new UxmlStringAttributeDescription
			{
				name = "mask-character",
				obsoleteNames = new string[] { "maskCharacter" },
				defaultValue = '*'.ToString()
			};

			// Token: 0x04000484 RID: 1156
			private UxmlStringAttributeDescription m_PlaceholderText = new UxmlStringAttributeDescription
			{
				name = "placeholder-text"
			};

			// Token: 0x04000485 RID: 1157
			private UxmlBoolAttributeDescription m_HidePlaceholderOnFocus = new UxmlBoolAttributeDescription
			{
				name = "hide-placeholder-on-focus"
			};

			// Token: 0x04000486 RID: 1158
			private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
			{
				name = "readonly"
			};

			// Token: 0x04000487 RID: 1159
			private UxmlBoolAttributeDescription m_IsDelayed = new UxmlBoolAttributeDescription
			{
				name = "is-delayed"
			};

			// Token: 0x04000488 RID: 1160
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "vertical-scroller-visibility",
				defaultValue = ScrollerVisibility.Hidden
			};

			// Token: 0x04000489 RID: 1161
			private UxmlBoolAttributeDescription m_SelectAllOnMouseUp = new UxmlBoolAttributeDescription
			{
				name = "select-all-on-mouse-up",
				defaultValue = true
			};

			// Token: 0x0400048A RID: 1162
			private UxmlBoolAttributeDescription m_SelectAllOnFocus = new UxmlBoolAttributeDescription
			{
				name = "select-all-on-focus",
				defaultValue = true
			};

			// Token: 0x0400048B RID: 1163
			private UxmlBoolAttributeDescription m_SelectWordByDoubleClick = new UxmlBoolAttributeDescription
			{
				name = "select-word-by-double-click",
				defaultValue = true
			};

			// Token: 0x0400048C RID: 1164
			private UxmlBoolAttributeDescription m_SelectLineByTripleClick = new UxmlBoolAttributeDescription
			{
				name = "select-line-by-triple-click",
				defaultValue = true
			};

			// Token: 0x0400048D RID: 1165
			private UxmlBoolAttributeDescription m_EmojiFallbackSupport = new UxmlBoolAttributeDescription
			{
				name = "emoji-fallback-support",
				defaultValue = true
			};

			// Token: 0x0400048E RID: 1166
			private UxmlBoolAttributeDescription m_HideMobileInput = new UxmlBoolAttributeDescription
			{
				name = "hide-mobile-input"
			};

			// Token: 0x0400048F RID: 1167
			private UxmlEnumAttributeDescription<TouchScreenKeyboardType> m_KeyboardType = new UxmlEnumAttributeDescription<TouchScreenKeyboardType>
			{
				name = "keyboard-type"
			};

			// Token: 0x04000490 RID: 1168
			private UxmlBoolAttributeDescription m_AutoCorrection = new UxmlBoolAttributeDescription
			{
				name = "auto-correction"
			};
		}

		// Token: 0x020000EC RID: 236
		protected internal abstract class TextInputBase : VisualElement
		{
			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x00022CAF File Offset: 0x00020EAF
			// (set) Token: 0x0600074E RID: 1870 RVA: 0x00022CB7 File Offset: 0x00020EB7
			internal TextElement textElement { get; private set; }

			// Token: 0x0600074F RID: 1871 RVA: 0x00022CC0 File Offset: 0x00020EC0
			internal TextInputBase()
			{
				base.delegatesFocus = true;
				this.textElement = new TextElement();
				this.textElement.selection.isSelectable = true;
				this.textEdition.isReadOnly = false;
				this.textSelection.isSelectable = true;
				this.textSelection.selectAllOnFocus = true;
				this.textSelection.selectAllOnMouseUp = true;
				this.textElement.enableRichText = false;
				this.textElement.tabIndex = 0;
				ITextEdition textEdition = this.textEdition;
				textEdition.AcceptCharacter = (Func<char, bool>)Delegate.Combine(textEdition.AcceptCharacter, new Func<char, bool>(this.AcceptCharacter));
				ITextEdition textEdition2 = this.textEdition;
				textEdition2.UpdateScrollOffset = (Action<bool>)Delegate.Combine(textEdition2.UpdateScrollOffset, new Action<bool>(this.UpdateScrollOffset));
				ITextEdition textEdition3 = this.textEdition;
				textEdition3.UpdateValueFromText = (Action)Delegate.Combine(textEdition3.UpdateValueFromText, new Action(this.UpdateValueFromText));
				ITextEdition textEdition4 = this.textEdition;
				textEdition4.UpdateTextFromValue = (Action)Delegate.Combine(textEdition4.UpdateTextFromValue, new Action(this.UpdateTextFromValue));
				ITextEdition textEdition5 = this.textEdition;
				textEdition5.MoveFocusToCompositeRoot = (Action)Delegate.Combine(textEdition5.MoveFocusToCompositeRoot, new Action(this.MoveFocusToCompositeRoot));
				this.textEdition.GetDefaultValueType = new Func<string>(this.GetDefaultValueType);
				base.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
				base.name = TextInputBaseField<string>.textInputUssName;
				this.SetSingleLine();
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnInputCustomStyleResolved), TrickleDown.NoTrickleDown);
				base.tabIndex = -1;
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00022E82 File Offset: 0x00021082
			public ITextSelection textSelection
			{
				get
				{
					return this.textElement.selection;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x00022E8F File Offset: 0x0002108F
			public ITextEdition textEdition
			{
				get
				{
					return this.textElement.edition;
				}
			}

			// Token: 0x1700012D RID: 301
			// (set) Token: 0x06000752 RID: 1874 RVA: 0x00022E9C File Offset: 0x0002109C
			internal bool isDragging
			{
				[CompilerGenerated]
				set
				{
					this.<isDragging>k__BackingField = value;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000753 RID: 1875 RVA: 0x00022EA5 File Offset: 0x000210A5
			// (set) Token: 0x06000754 RID: 1876 RVA: 0x00022EB4 File Offset: 0x000210B4
			public string text
			{
				get
				{
					return this.textElement.text;
				}
				set
				{
					bool flag = this.textElement.text == value;
					if (!flag)
					{
						this.textElement.text = value;
					}
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000755 RID: 1877 RVA: 0x00022EE6 File Offset: 0x000210E6
			internal string originalText
			{
				get
				{
					return this.textElement.originalText;
				}
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x00022EF3 File Offset: 0x000210F3
			protected virtual TValueType StringToValue(string str)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x00022EFC File Offset: 0x000210FC
			internal void UpdateValueFromText()
			{
				TextInputBaseField<TValueType> parentTextField = (TextInputBaseField<TValueType>)base.parent;
				parentTextField.UpdateValueFromText();
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x00022F20 File Offset: 0x00021120
			internal void UpdateTextFromValue()
			{
				TextInputBaseField<TValueType> parentTextField = (TextInputBaseField<TValueType>)base.parent;
				parentTextField.UpdateTextFromValue();
			}

			// Token: 0x06000759 RID: 1881 RVA: 0x00022F44 File Offset: 0x00021144
			internal void MoveFocusToCompositeRoot()
			{
				TextInputBaseField<TValueType> parentTextField = (TextInputBaseField<TValueType>)base.parent;
				this.focusController.SwitchFocus(parentTextField, false, DispatchMode.Default);
				this.textEdition.keyboardType = TouchScreenKeyboardType.Default;
				this.textEdition.autoCorrection = false;
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x00022F87 File Offset: 0x00021187
			private void MakeSureScrollViewDoesNotLeakEvents(ChangeEvent<float> evt)
			{
				evt.StopPropagation();
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x00022F94 File Offset: 0x00021194
			internal void SetSingleLine()
			{
				base.hierarchy.Clear();
				this.RemoveMultilineComponents();
				base.Add(this.textElement);
				base.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
				this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementUssClassName);
				this.textElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
				bool flag = this.scrollOffset != Vector2.zero;
				if (flag)
				{
					this.scrollOffset.y = 0f;
					this.UpdateScrollOffset(false);
				}
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x0002302C File Offset: 0x0002122C
			internal void SetMultiline()
			{
				bool flag = !this.textEdition.multiline;
				if (!flag)
				{
					this.RemoveSingleLineComponents();
					this.RemoveMultilineComponents();
					bool flag2 = this.verticalScrollerVisibility != ScrollerVisibility.Hidden && this.scrollView == null;
					if (flag2)
					{
						this.scrollView = new ScrollView();
						this.scrollView.Add(this.textElement);
						base.Add(this.scrollView);
						this.SetScrollViewMode();
						this.scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
						this.scrollView.verticalScrollerVisibility = this.verticalScrollerVisibility;
						this.scrollView.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerScrollviewUssClassName);
						this.scrollView.contentViewport.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerViewportUssClassName);
						this.scrollView.contentContainer.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerContentContainerUssClassName);
						this.scrollView.contentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.ScrollViewOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
						this.scrollView.verticalScroller.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
						this.scrollView.verticalScroller.slider.focusable = false;
						this.scrollView.horizontalScroller.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
						this.scrollView.horizontalScroller.slider.focusable = false;
						base.AddToClassList(TextInputBaseField<TValueType>.multilineInputWithScrollViewUssClassName);
						this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementWithScrollViewUssClassName);
					}
					else
					{
						bool flag3 = this.multilineContainer == null;
						if (flag3)
						{
							this.textElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
							this.multilineContainer = new VisualElement
							{
								classList = { TextInputBaseField<TValueType>.multilineContainerClassName }
							};
							this.multilineContainer.Add(this.textElement);
							base.Add(this.multilineContainer);
							this.SetMultilineContainerStyle();
							base.AddToClassList(TextInputBaseField<TValueType>.multilineInputUssClassName);
							this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementUssClassName);
						}
					}
				}
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x00023244 File Offset: 0x00021444
			private void ScrollViewOnGeometryChangedEvent(GeometryChangedEvent e)
			{
				bool flag = e.oldRect.size == e.newRect.size;
				if (!flag)
				{
					this.UpdateScrollOffset(false);
				}
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x00023284 File Offset: 0x00021484
			private void TextElementOnGeometryChangedEvent(GeometryChangedEvent e)
			{
				bool flag = e.oldRect.size == e.newRect.size;
				if (!flag)
				{
					bool widthChanged = Math.Abs(e.oldRect.size.x - e.newRect.size.x) > 1E-30f;
					this.UpdateScrollOffset(false, widthChanged);
				}
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x000232F8 File Offset: 0x000214F8
			internal void OnInputCustomStyleResolved(CustomStyleResolvedEvent e)
			{
				ICustomStyle customStyle = e.customStyle;
				Color selectionValue;
				bool flag = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_SelectionColorProperty, out selectionValue);
				if (flag)
				{
					this.textSelection.selectionColor = selectionValue;
				}
				Color cursorValue;
				bool flag2 = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_CursorColorProperty, out cursorValue);
				if (flag2)
				{
					this.textSelection.cursorColor = cursorValue;
				}
				this.SetScrollViewMode();
				this.SetMultilineContainerStyle();
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x0002335C File Offset: 0x0002155C
			private string GetDefaultValueType()
			{
				TValueType tvalueType = default(TValueType);
				string text;
				if (tvalueType != null)
				{
					tvalueType = default(TValueType);
					text = tvalueType.ToString();
				}
				else
				{
					text = "";
				}
				return text;
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x0002339C File Offset: 0x0002159C
			internal virtual bool AcceptCharacter(char c)
			{
				return !this.textEdition.isReadOnly && base.enabledInHierarchy;
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x000233C4 File Offset: 0x000215C4
			internal void UpdateScrollOffset(bool isBackspace = false)
			{
				this.UpdateScrollOffset(isBackspace, false);
			}

			// Token: 0x06000763 RID: 1891 RVA: 0x000233D0 File Offset: 0x000215D0
			internal void UpdateScrollOffset(bool isBackspace, bool widthChanged)
			{
				ITextSelection selection = this.textSelection;
				bool flag = selection.cursorIndex < 0 || (selection.cursorIndex <= 0 && selection.selectIndex <= 0 && this.scrollOffset == Vector2.zero);
				if (!flag)
				{
					bool flag2 = this.scrollView != null;
					if (flag2)
					{
						this.scrollOffset = this.GetScrollOffset(this.scrollView.scrollOffset.x, this.scrollView.scrollOffset.y, this.scrollView.contentViewport.layout.width, isBackspace, widthChanged);
						this.scrollView.scrollOffset = this.scrollOffset;
						this.m_ScrollViewWasClamped = this.scrollOffset.x > this.scrollView.scrollOffset.x || this.scrollOffset.y > this.scrollView.scrollOffset.y;
					}
					else
					{
						Vector3 t = this.textElement.transform.position;
						this.scrollOffset = this.GetScrollOffset(this.scrollOffset.x, this.scrollOffset.y, base.contentRect.width, isBackspace, widthChanged);
						t.y = -Mathf.Min(this.scrollOffset.y, Math.Abs(this.textElement.contentRect.height - base.contentRect.height));
						t.x = -this.scrollOffset.x;
						bool flag3 = !t.Equals(this.textElement.transform.position);
						if (flag3)
						{
							this.textElement.transform.position = t;
						}
					}
				}
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x0002359C File Offset: 0x0002179C
			private Vector2 GetScrollOffset(float xOffset, float yOffset, float contentViewportWidth, bool isBackspace, bool widthChanged)
			{
				Vector2 cursorPos = this.textSelection.cursorPosition;
				float cursorWidth = this.textSelection.cursorWidth;
				float newXOffset = xOffset;
				float newYOffset = yOffset;
				bool flag = Math.Abs(this.lastCursorPos.x - cursorPos.x) > 0.05f || this.m_ScrollViewWasClamped || widthChanged;
				if (flag)
				{
					bool flag2 = cursorPos.x > xOffset + contentViewportWidth - cursorWidth || (xOffset > 0f && widthChanged);
					if (flag2)
					{
						float roundedValue = Mathf.Ceil(cursorPos.x + cursorWidth - contentViewportWidth);
						newXOffset = Mathf.Max(roundedValue, 0f);
					}
					else
					{
						bool flag3 = cursorPos.x < xOffset + 5f;
						if (flag3)
						{
							newXOffset = Mathf.Max(cursorPos.x - 5f, 0f);
						}
					}
				}
				bool flag4 = this.textEdition.multiline && (Math.Abs(this.lastCursorPos.y - cursorPos.y) > 0.05f || this.m_ScrollViewWasClamped);
				if (flag4)
				{
					bool flag5 = cursorPos.y > base.contentRect.height + yOffset;
					if (flag5)
					{
						newYOffset = cursorPos.y - base.contentRect.height;
					}
					else
					{
						bool flag6 = cursorPos.y < this.textSelection.lineHeightAtCursorPosition + yOffset + 0.05f;
						if (flag6)
						{
							newYOffset = cursorPos.y - this.textSelection.lineHeightAtCursorPosition;
						}
					}
				}
				this.lastCursorPos = cursorPos;
				bool flag7 = Math.Abs(xOffset - newXOffset) > 0.05f || Math.Abs(yOffset - newYOffset) > 0.05f;
				Vector2 vector;
				if (flag7)
				{
					vector = new Vector2(newXOffset, newYOffset);
				}
				else
				{
					vector = ((this.scrollView != null) ? this.scrollView.scrollOffset : this.scrollOffset);
				}
				return vector;
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x00023770 File Offset: 0x00021970
			internal void SetScrollViewMode()
			{
				bool flag = this.scrollView == null;
				if (!flag)
				{
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalHorizontalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.horizontalVariantInnerTextElementUssClassName);
					bool flag2 = this.textEdition.multiline && (base.computedStyle.whiteSpace == WhiteSpace.Normal || base.computedStyle.whiteSpace == WhiteSpace.PreWrap);
					if (flag2)
					{
						this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.verticalVariantInnerTextElementUssClassName);
						this.scrollView.mode = ScrollViewMode.Vertical;
					}
					else
					{
						bool multiline = this.textEdition.multiline;
						if (multiline)
						{
							this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.verticalHorizontalVariantInnerTextElementUssClassName);
							this.scrollView.mode = ScrollViewMode.VerticalAndHorizontal;
						}
						else
						{
							this.textElement.AddToClassList(TextInputBaseField<TValueType>.TextInputBase.horizontalVariantInnerTextElementUssClassName);
							this.scrollView.mode = ScrollViewMode.Horizontal;
						}
					}
				}
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x00023868 File Offset: 0x00021A68
			private void SetMultilineContainerStyle()
			{
				bool flag = this.multilineContainer != null;
				if (flag)
				{
					bool flag2 = base.computedStyle.whiteSpace == WhiteSpace.Normal || base.computedStyle.whiteSpace == WhiteSpace.PreWrap;
					if (flag2)
					{
						base.style.overflow = Overflow.Hidden;
					}
					else
					{
						base.style.overflow = (Overflow)2;
					}
				}
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x000238D0 File Offset: 0x00021AD0
			private void RemoveSingleLineComponents()
			{
				base.RemoveFromClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
				this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementUssClassName);
				this.textElement.RemoveFromHierarchy();
				this.textElement.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x00023920 File Offset: 0x00021B20
			private void RemoveMultilineComponents()
			{
				bool flag = this.scrollView != null;
				if (flag)
				{
					this.scrollView.RemoveFromHierarchy();
					this.scrollView.contentContainer.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.ScrollViewOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
					this.scrollView.verticalScroller.slider.UnregisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
					this.scrollView.horizontalScroller.slider.UnregisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.MakeSureScrollViewDoesNotLeakEvents));
					this.scrollView = null;
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.verticalHorizontalVariantInnerTextElementUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.horizontalVariantInnerTextElementUssClassName);
					base.RemoveFromClassList(TextInputBaseField<TValueType>.multilineInputWithScrollViewUssClassName);
					this.textElement.RemoveFromClassList(TextInputBaseField<TValueType>.TextInputBase.innerTextElementWithScrollViewUssClassName);
				}
				bool flag2 = this.multilineContainer != null;
				if (flag2)
				{
					this.textElement.transform.position = Vector3.zero;
					this.multilineContainer.RemoveFromHierarchy();
					this.textElement.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.TextElementOnGeometryChangedEvent), TrickleDown.NoTrickleDown);
					this.multilineContainer = null;
					base.RemoveFromClassList(TextInputBaseField<TValueType>.multilineInputUssClassName);
				}
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x00023A64 File Offset: 0x00021C64
			internal bool SetVerticalScrollerVisibility(ScrollerVisibility sv)
			{
				bool multiline = this.textEdition.multiline;
				bool flag2;
				if (multiline)
				{
					this.verticalScrollerVisibility = sv;
					bool flag = this.scrollView == null;
					if (flag)
					{
						this.SetMultiline();
					}
					else
					{
						this.scrollView.verticalScrollerVisibility = this.verticalScrollerVisibility;
					}
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x04000492 RID: 1170
			internal ScrollView scrollView;

			// Token: 0x04000493 RID: 1171
			internal VisualElement multilineContainer;

			// Token: 0x04000494 RID: 1172
			public static readonly string innerComponentsModifierName = "--inner-input-field-component";

			// Token: 0x04000495 RID: 1173
			public static readonly string innerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x04000496 RID: 1174
			internal static readonly string innerTextElementWithScrollViewUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--scroll-view";

			// Token: 0x04000497 RID: 1175
			public static readonly string horizontalVariantInnerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--horizontal";

			// Token: 0x04000498 RID: 1176
			public static readonly string verticalVariantInnerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--vertical";

			// Token: 0x04000499 RID: 1177
			public static readonly string verticalHorizontalVariantInnerTextElementUssClassName = TextElement.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName + "--vertical-horizontal";

			// Token: 0x0400049A RID: 1178
			public static readonly string innerScrollviewUssClassName = ScrollView.ussClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x0400049B RID: 1179
			public static readonly string innerViewportUssClassName = ScrollView.viewportUssClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x0400049C RID: 1180
			public static readonly string innerContentContainerUssClassName = ScrollView.contentUssClassName + TextInputBaseField<TValueType>.TextInputBase.innerComponentsModifierName;

			// Token: 0x0400049E RID: 1182
			internal Vector2 scrollOffset = Vector2.zero;

			// Token: 0x0400049F RID: 1183
			private bool m_ScrollViewWasClamped;

			// Token: 0x040004A0 RID: 1184
			private Vector2 lastCursorPos = Vector2.zero;

			// Token: 0x040004A1 RID: 1185
			internal ScrollerVisibility verticalScrollerVisibility = ScrollerVisibility.Hidden;
		}
	}
}
