using System;

namespace Unity.Properties
{
	// Token: 0x02000064 RID: 100
	internal interface IConstructor<out T> : IConstructor
	{
		// Token: 0x06000237 RID: 567
		T Instantiate();
	}
}
