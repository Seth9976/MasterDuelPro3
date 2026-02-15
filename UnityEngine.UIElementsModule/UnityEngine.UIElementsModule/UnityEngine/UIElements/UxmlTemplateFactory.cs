using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000477 RID: 1143
	[Obsolete("UxmlTemplateFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlTemplateFactory : UxmlFactory<VisualElement, UxmlTemplateTraits>
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0007B469 File Offset: 0x00079669
		public override string uxmlName
		{
			get
			{
				return "Template";
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0007B470 File Offset: 0x00079670
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0007B488 File Offset: 0x00079688
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}
	}
}
