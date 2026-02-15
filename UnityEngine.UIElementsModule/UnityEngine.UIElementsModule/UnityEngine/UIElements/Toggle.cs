using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000159 RID: 345
	public class Toggle : BaseBoolField
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x00032744 File Offset: 0x00030944
		public Toggle()
			: this(null)
		{
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00032750 File Offset: 0x00030950
		public Toggle(string label)
			: base(label)
		{
			base.AddToClassList(Toggle.ussClassName);
			base.visualInput.AddToClassList(Toggle.inputUssClassName);
			base.labelElement.AddToClassList(Toggle.labelUssClassName);
			this.m_CheckMark.AddToClassList(Toggle.checkmarkUssClassName);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000327A5 File Offset: 0x000309A5
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(Toggle.textUssClassName);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000327C0 File Offset: 0x000309C0
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
				this.m_CheckMark.AddToClassList(Toggle.mixedValuesUssClassName);
			}
			else
			{
				this.m_CheckMark.RemoveFromClassList(Toggle.mixedValuesUssClassName);
				bool value = this.value;
				if (value)
				{
					base.visualInput.pseudoStates |= PseudoStates.Checked;
					base.pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					base.visualInput.pseudoStates &= ~PseudoStates.Checked;
					base.pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x040006BE RID: 1726
		public new static readonly string ussClassName = "unity-toggle";

		// Token: 0x040006BF RID: 1727
		public new static readonly string labelUssClassName = Toggle.ussClassName + "__label";

		// Token: 0x040006C0 RID: 1728
		public new static readonly string inputUssClassName = Toggle.ussClassName + "__input";

		// Token: 0x040006C1 RID: 1729
		[Obsolete]
		public static readonly string noTextVariantUssClassName = Toggle.ussClassName + "--no-text";

		// Token: 0x040006C2 RID: 1730
		public static readonly string checkmarkUssClassName = Toggle.ussClassName + "__checkmark";

		// Token: 0x040006C3 RID: 1731
		public static readonly string textUssClassName = Toggle.ussClassName + "__text";

		// Token: 0x040006C4 RID: 1732
		public static readonly string mixedValuesUssClassName = Toggle.ussClassName + "__mixed-values";

		// Token: 0x0200015A RID: 346
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Toggle, Toggle.UxmlTraits>
		{
		}

		// Token: 0x0200015B RID: 347
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x06000A63 RID: 2659 RVA: 0x00032918 File Offset: 0x00030B18
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((Toggle)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x040006C5 RID: 1733
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
