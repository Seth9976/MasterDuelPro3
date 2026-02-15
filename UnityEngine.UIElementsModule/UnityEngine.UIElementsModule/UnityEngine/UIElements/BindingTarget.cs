using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000021 RID: 33
	internal readonly struct BindingTarget
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000033F5 File Offset: 0x000015F5
		public BindingTarget(VisualElement element, in BindingId bindingId)
		{
			this.element = element;
			this.bindingId = bindingId;
		}

		// Token: 0x04000045 RID: 69
		public readonly VisualElement element;

		// Token: 0x04000046 RID: 70
		public readonly BindingId bindingId;
	}
}
