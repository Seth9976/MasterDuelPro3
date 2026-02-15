using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E3 RID: 227
	[Obsolete("BaseFieldTraits<TValueType, TValueUxmlAttributeType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class BaseFieldTraits<TValueType, TValueUxmlAttributeType> : BaseField<TValueType>.UxmlTraits where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x000210E6 File Offset: 0x0001F2E6
		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			((INotifyValueChanged<TValueType>)ve).SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00021111 File Offset: 0x0001F311
		public BaseFieldTraits()
		{
			TValueUxmlAttributeType tvalueUxmlAttributeType = new TValueUxmlAttributeType();
			tvalueUxmlAttributeType.name = "value";
			this.m_Value = tvalueUxmlAttributeType;
			base..ctor();
		}

		// Token: 0x04000453 RID: 1107
		private TValueUxmlAttributeType m_Value;
	}
}
