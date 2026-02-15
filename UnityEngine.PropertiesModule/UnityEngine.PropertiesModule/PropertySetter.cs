using System;

namespace Unity.Properties
{
	// Token: 0x02000011 RID: 17
	// (Invoke) Token: 0x06000026 RID: 38
	public delegate void PropertySetter<TContainer, in TValue>(ref TContainer container, TValue value);
}
