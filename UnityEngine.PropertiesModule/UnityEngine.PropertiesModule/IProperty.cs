using System;

namespace Unity.Properties
{
	// Token: 0x02000016 RID: 22
	public interface IProperty
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47
		string Name { get; }

		// Token: 0x06000030 RID: 48
		Type DeclaredValueType();
	}
}
