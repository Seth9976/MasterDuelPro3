using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200049F RID: 1183
	[Obsolete("IUxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public interface IBaseUxmlFactory
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002203 RID: 8707
		string uxmlQualifiedName { get; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002204 RID: 8708
		Type uxmlType { get; }

		// Token: 0x06002205 RID: 8709
		bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc);
	}
}
