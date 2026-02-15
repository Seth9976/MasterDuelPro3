using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BC RID: 188
	public class DropdownField : PopupField<string>
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001CD96 File Offset: 0x0001AF96
		public DropdownField()
			: this(null)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001CDA1 File Offset: 0x0001AFA1
		public DropdownField(string label)
			: base(label)
		{
		}

		// Token: 0x020000BD RID: 189
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<DropdownField, DropdownField.UxmlTraits>
		{
		}

		// Token: 0x020000BE RID: 190
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<string>.UxmlTraits
		{
			// Token: 0x0600060E RID: 1550 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				DropdownField f = (DropdownField)ve;
				List<string> choices = UxmlUtility.ParseStringListAttribute(this.m_Choices.GetValueFromBag(bag, cc));
				bool flag = choices != null;
				if (flag)
				{
					f.choices = choices;
				}
				f.index = this.m_Index.GetValueFromBag(bag, cc);
			}

			// Token: 0x040003B5 RID: 949
			private UxmlIntAttributeDescription m_Index = new UxmlIntAttributeDescription
			{
				name = "index"
			};

			// Token: 0x040003B6 RID: 950
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}
	}
}
