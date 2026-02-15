using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000158 RID: 344
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	[Obsolete("TextValueFieldTraits<TValueType, TValueUxmlAttributeType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class TextValueFieldTraits<TValueType, TValueUxmlAttributeType> : BaseFieldTraits<TValueType, TValueUxmlAttributeType> where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00032650 File Offset: 0x00030850
		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			TextInputBaseField<TValueType> field = (TextInputBaseField<TValueType>)ve;
			bool flag = field != null;
			if (flag)
			{
				field.textEdition.placeholder = this.m_PlaceholderText.GetValueFromBag(bag, cc);
				field.textEdition.hidePlaceholderOnFocus = this.m_HidePlaceholderOnFocus.GetValueFromBag(bag, cc);
				field.isReadOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				field.isDelayed = this.m_IsDelayed.GetValueFromBag(bag, cc);
			}
		}

		// Token: 0x040006BA RID: 1722
		private UxmlStringAttributeDescription m_PlaceholderText = new UxmlStringAttributeDescription
		{
			name = "placeholder-text"
		};

		// Token: 0x040006BB RID: 1723
		private UxmlBoolAttributeDescription m_HidePlaceholderOnFocus = new UxmlBoolAttributeDescription
		{
			name = "hide-placeholder-on-focus"
		};

		// Token: 0x040006BC RID: 1724
		private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
		{
			name = "readonly"
		};

		// Token: 0x040006BD RID: 1725
		private UxmlBoolAttributeDescription m_IsDelayed = new UxmlBoolAttributeDescription
		{
			name = "is-delayed"
		};
	}
}
