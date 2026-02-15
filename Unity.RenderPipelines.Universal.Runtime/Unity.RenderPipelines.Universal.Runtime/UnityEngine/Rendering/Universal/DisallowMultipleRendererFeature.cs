using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200016A RID: 362
	[AttributeUsage(AttributeTargets.Class)]
	public class DisallowMultipleRendererFeature : Attribute
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00025A49 File Offset: 0x00023C49
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00025A40 File Offset: 0x00023C40
		public string customTitle { get; private set; }

		// Token: 0x060007DD RID: 2013 RVA: 0x00025A51 File Offset: 0x00023C51
		public DisallowMultipleRendererFeature(string customTitle = null)
		{
			this.customTitle = customTitle;
		}
	}
}
