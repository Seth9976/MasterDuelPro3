using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000479 RID: 1145
	[Obsolete("UxmlAttributeOverridesFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlAttributeOverridesFactory : UxmlFactory<VisualElement, UxmlAttributeOverridesTraits>
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x0007B505 File Offset: 0x00079705
		public override string uxmlName
		{
			get
			{
				return "AttributeOverrides";
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0007B50C File Offset: 0x0007970C
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0007B524 File Offset: 0x00079724
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}
	}
}
