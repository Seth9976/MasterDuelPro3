using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D2 RID: 210
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Hash128Field : TextInputBaseField<Hash128>
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0001F3F7 File Offset: 0x0001D5F7
		public Hash128Field()
			: this(null, -1)
		{
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001F404 File Offset: 0x0001D604
		public Hash128Field(string label, int maxLength = -1)
			: base(label, maxLength, '\0', new Hash128Field.Hash128Input())
		{
			this.m_UpdateTextFromValue = true;
			this.SetValueWithoutNotify(default(Hash128));
			base.AddToClassList(Hash128Field.ussClassName);
			base.labelElement.AddToClassList(Hash128Field.labelUssClassName);
			base.visualInput.AddToClassList(Hash128Field.inputUssClassName);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001F468 File Offset: 0x0001D668
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001F480 File Offset: 0x0001D680
		public override Hash128 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				bool updateTextFromValue = this.m_UpdateTextFromValue;
				if (updateTextFromValue)
				{
					base.text = base.rawValue.ToString();
				}
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		internal override void UpdateValueFromText()
		{
			this.m_UpdateTextFromValue = false;
			try
			{
				this.value = this.StringToValue(base.text);
			}
			finally
			{
				this.m_UpdateTextFromValue = true;
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001F504 File Offset: 0x0001D704
		internal override void UpdateTextFromValue()
		{
			base.text = this.ValueToString(base.rawValue);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001F51C File Offset: 0x0001D71C
		public override void SetValueWithoutNotify(Hash128 newValue)
		{
			base.SetValueWithoutNotify(newValue);
			bool updateTextFromValue = this.m_UpdateTextFromValue;
			if (updateTextFromValue)
			{
				base.text = base.rawValue.ToString();
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001F55C File Offset: 0x0001D75C
		protected override string ValueToString(Hash128 value)
		{
			return value.ToString();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001F57C File Offset: 0x0001D77C
		protected override Hash128 StringToValue(string str)
		{
			return Hash128Field.Hash128Input.Parse(str);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001F594 File Offset: 0x0001D794
		[EventInterest(new Type[] { typeof(FocusOutEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool isReadOnly = base.isReadOnly;
			if (!isReadOnly)
			{
				bool flag = evt.eventTypeId == EventBase<FocusOutEvent>.TypeId();
				if (flag)
				{
					bool flag2 = string.IsNullOrEmpty(base.text);
					if (flag2)
					{
						this.value = default(Hash128);
					}
					else
					{
						base.textInputBase.UpdateValueFromText();
						base.textInputBase.UpdateTextFromValue();
					}
				}
			}
		}

		// Token: 0x04000404 RID: 1028
		internal bool m_UpdateTextFromValue;

		// Token: 0x04000405 RID: 1029
		public new static readonly string ussClassName = "unity-hash128-field";

		// Token: 0x04000406 RID: 1030
		public new static readonly string labelUssClassName = Hash128Field.ussClassName + "__label";

		// Token: 0x04000407 RID: 1031
		public new static readonly string inputUssClassName = Hash128Field.ussClassName + "__input";

		// Token: 0x020000D3 RID: 211
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Hash128Field, Hash128Field.UxmlTraits>
		{
		}

		// Token: 0x020000D4 RID: 212
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<Hash128, UxmlHash128AttributeDescription>
		{
		}

		// Token: 0x020000D5 RID: 213
		private class Hash128Input : TextInputBaseField<Hash128>.TextInputBase
		{
			// Token: 0x0600068C RID: 1676 RVA: 0x0001F64C File Offset: 0x0001D84C
			internal Hash128Input()
			{
				base.textEdition.AcceptCharacter = new Func<char, bool>(this.AcceptCharacter);
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001F66F File Offset: 0x0001D86F
			protected string allowedCharacters
			{
				get
				{
					return "0123456789abcdefABCDEF";
				}
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001F678 File Offset: 0x0001D878
			internal override bool AcceptCharacter(char c)
			{
				return base.AcceptCharacter(c) && c != '\0' && this.allowedCharacters.IndexOf(c) != -1;
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x0001F6AC File Offset: 0x0001D8AC
			protected override Hash128 StringToValue(string str)
			{
				return Hash128Field.Hash128Input.Parse(str);
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
			internal static Hash128 Parse(string str)
			{
				ulong val;
				bool flag = str.Length == 1 && ulong.TryParse(str, out val);
				Hash128 hash;
				if (flag)
				{
					hash = new Hash128(val, 0UL);
				}
				else
				{
					hash = Hash128.Parse(str);
				}
				return hash;
			}
		}
	}
}
