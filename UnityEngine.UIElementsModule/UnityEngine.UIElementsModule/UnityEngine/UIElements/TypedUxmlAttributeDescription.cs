using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000481 RID: 1153
	public abstract class TypedUxmlAttributeDescription<T> : UxmlAttributeDescription
	{
		// Token: 0x060021AB RID: 8619
		public abstract T GetValueFromBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0007BC6B File Offset: 0x00079E6B
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x0007BC73 File Offset: 0x00079E73
		public T defaultValue { get; set; }
	}
}
