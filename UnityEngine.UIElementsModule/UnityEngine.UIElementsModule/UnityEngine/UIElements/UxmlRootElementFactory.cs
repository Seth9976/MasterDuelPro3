using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000473 RID: 1139
	[Obsolete("UxmlRootElementFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlRootElementFactory : UxmlFactory<VisualElement, UxmlRootElementTraits>
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007B361 File Offset: 0x00079561
		public override string uxmlName
		{
			get
			{
				return "UXML";
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0007B368 File Offset: 0x00079568
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0007B380 File Offset: 0x00079580
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}
	}
}
