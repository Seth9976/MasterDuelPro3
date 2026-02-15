using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001D RID: 29
	public readonly struct BindingActivationContext
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003374 File Offset: 0x00001574
		internal BindingActivationContext(VisualElement element, in BindingId property)
		{
			this.m_TargetElement = element;
			this.m_BindingId = property;
		}

		// Token: 0x04000039 RID: 57
		private readonly VisualElement m_TargetElement;

		// Token: 0x0400003A RID: 58
		private readonly BindingId m_BindingId;
	}
}
