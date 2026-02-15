using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003A RID: 58
	public readonly struct BindingInfo
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009588 File Offset: 0x00007788
		public Binding binding { get; }

		// Token: 0x060001E8 RID: 488 RVA: 0x00009590 File Offset: 0x00007790
		private BindingInfo(VisualElement targetElement, in BindingId bindingId, Binding binding)
		{
			this.<targetElement>k__BackingField = targetElement;
			this.<bindingId>k__BackingField = bindingId;
			this.binding = binding;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000095B0 File Offset: 0x000077B0
		internal static BindingInfo FromRequest(VisualElement target, in PropertyPath targetPath, Binding binding)
		{
			BindingId bindingId = in targetPath;
			return new BindingInfo(target, in bindingId, binding);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000095D4 File Offset: 0x000077D4
		internal static BindingInfo FromBindingData(in DataBindingManager.BindingData bindingData)
		{
			return new BindingInfo(bindingData.target.element, in bindingData.target.bindingId, bindingData.binding);
		}
	}
}
