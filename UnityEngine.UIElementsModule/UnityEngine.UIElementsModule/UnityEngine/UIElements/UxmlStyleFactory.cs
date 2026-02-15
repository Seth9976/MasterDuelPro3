using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000475 RID: 1141
	[Obsolete("UxmlStyleFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlStyleFactory : UxmlFactory<VisualElement, UxmlStyleTraits>
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0007B3D3 File Offset: 0x000795D3
		public override string uxmlName
		{
			get
			{
				return "Style";
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007B3DA File Offset: 0x000795DA
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0007B3F4 File Offset: 0x000795F4
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}
	}
}
