using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E6 RID: 230
	public class TextField : TextInputBaseField<string>
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00021982 File Offset: 0x0001FB82
		private TextField.TextInput textInput
		{
			get
			{
				return (TextField.TextInput)base.textInputBase;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00021990 File Offset: 0x0001FB90
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x000219B0 File Offset: 0x0001FBB0
		[CreateProperty]
		public bool multiline
		{
			get
			{
				return this.textInput.multiline;
			}
			set
			{
				bool previous = this.multiline;
				this.textInput.multiline = value;
				bool flag = previous != this.multiline;
				if (flag)
				{
					base.NotifyPropertyChanged(in TextField.multilineProperty);
				}
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000219EE File Offset: 0x0001FBEE
		public TextField()
			: this(null)
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000219F9 File Offset: 0x0001FBF9
		public TextField(string label)
			: this(label, -1, false, false, '*')
		{
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00021A0C File Offset: 0x0001FC0C
		public TextField(string label, int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: base(label, maxLength, maskChar, new TextField.TextInput())
		{
			base.AddToClassList(TextField.ussClassName);
			base.labelElement.AddToClassList(TextField.labelUssClassName);
			base.visualInput.AddToClassList(TextField.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.SetValueWithoutNotify("");
			this.multiline = multiline;
			base.textEdition.isPassword = isPasswordField;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00021A84 File Offset: 0x0001FC84
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00021A9C File Offset: 0x0001FC9C
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				base.textEdition.UpdateText(base.rawValue);
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00021ABC File Offset: 0x0001FCBC
		public override void SetValueWithoutNotify(string newValue)
		{
			base.SetValueWithoutNotify(newValue);
			string textValue = base.rawValue;
			bool flag = !this.multiline && base.rawValue != null;
			if (flag)
			{
				textValue = base.rawValue.Replace("\n", "");
			}
			((INotifyValueChanged<string>)this.textInput.textElement).SetValueWithoutNotify(textValue);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00021B19 File Offset: 0x0001FD19
		internal override void UpdateTextFromValue()
		{
			this.SetValueWithoutNotify(base.rawValue);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00021B2C File Offset: 0x0001FD2C
		[EventInterest(new Type[] { typeof(FocusOutEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag;
			if (base.isDelayed)
			{
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : null);
				long num2 = EventBase<FocusOutEvent>.TypeId();
				flag = (num.GetValueOrDefault() == num2) & (num != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.value = base.text;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00021B94 File Offset: 0x0001FD94
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string key = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, key);
			base.text = base.rawValue;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00021BC6 File Offset: 0x0001FDC6
		protected override string ValueToString(string value)
		{
			return value;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00021BC6 File Offset: 0x0001FDC6
		protected override string StringToValue(string str)
		{
			return str;
		}

		// Token: 0x04000458 RID: 1112
		internal static readonly BindingId multilineProperty = "multiline";

		// Token: 0x04000459 RID: 1113
		public new static readonly string ussClassName = "unity-text-field";

		// Token: 0x0400045A RID: 1114
		public new static readonly string labelUssClassName = TextField.ussClassName + "__label";

		// Token: 0x0400045B RID: 1115
		public new static readonly string inputUssClassName = TextField.ussClassName + "__input";

		// Token: 0x020000E7 RID: 231
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TextField, TextField.UxmlTraits>
		{
		}

		// Token: 0x020000E8 RID: 232
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextInputBaseField<string>.UxmlTraits
		{
			// Token: 0x06000708 RID: 1800 RVA: 0x00021C24 File Offset: 0x0001FE24
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				TextField field = (TextField)ve;
				base.Init(ve, bag, cc);
				string value = string.Empty;
				bool flag = TextField.UxmlTraits.k_Value.TryGetValueFromBag(bag, cc, ref value);
				if (flag)
				{
					field.SetValueWithoutNotify(value);
				}
				field.multiline = this.m_Multiline.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400045C RID: 1116
			private static readonly UxmlStringAttributeDescription k_Value = new UxmlStringAttributeDescription
			{
				name = "value",
				obsoleteNames = new string[] { "text" }
			};

			// Token: 0x0400045D RID: 1117
			private UxmlBoolAttributeDescription m_Multiline = new UxmlBoolAttributeDescription
			{
				name = "multiline"
			};
		}

		// Token: 0x020000E9 RID: 233
		private class TextInput : TextInputBaseField<string>.TextInputBase
		{
			// Token: 0x1700010C RID: 268
			// (get) Token: 0x0600070B RID: 1803 RVA: 0x00021CD6 File Offset: 0x0001FED6
			private TextField parentTextField
			{
				get
				{
					return (TextField)base.parent;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x0600070C RID: 1804 RVA: 0x00021CE4 File Offset: 0x0001FEE4
			// (set) Token: 0x0600070D RID: 1805 RVA: 0x00021D04 File Offset: 0x0001FF04
			public bool multiline
			{
				get
				{
					return base.textEdition.multiline;
				}
				set
				{
					bool textMatchesMultiline = value || string.IsNullOrEmpty(base.text) || !base.text.Contains("\n");
					bool flag = textMatchesMultiline && base.textEdition.multiline == value;
					if (!flag)
					{
						base.textEdition.multiline = value;
						if (value)
						{
							base.text = this.parentTextField.rawValue;
							base.SetMultiline();
						}
						else
						{
							base.text = base.text.Replace("\n", "");
							base.SetSingleLine();
						}
					}
				}
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x00021BC6 File Offset: 0x0001FDC6
			protected override string StringToValue(string str)
			{
				return str;
			}
		}
	}
}
