using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003F RID: 63
	public readonly struct DataSourceContextChanged
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00009789 File Offset: 0x00007989
		internal DataSourceContextChanged(VisualElement element, in BindingId bindingId, in DataSourceContext previousContext, in DataSourceContext newContext)
		{
			this.m_TargetElement = element;
			this.m_BindingId = bindingId;
			this.m_PreviousContext = previousContext;
			this.m_NewContext = newContext;
		}

		// Token: 0x0400013C RID: 316
		private readonly VisualElement m_TargetElement;

		// Token: 0x0400013D RID: 317
		private readonly BindingId m_BindingId;

		// Token: 0x0400013E RID: 318
		private readonly DataSourceContext m_PreviousContext;

		// Token: 0x0400013F RID: 319
		private readonly DataSourceContext m_NewContext;
	}
}
