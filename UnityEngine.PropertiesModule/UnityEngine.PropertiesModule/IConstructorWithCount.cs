using System;

namespace Unity.Properties
{
	// Token: 0x02000065 RID: 101
	internal interface IConstructorWithCount<out T> : IConstructor
	{
		// Token: 0x06000238 RID: 568
		T InstantiateWithCount(int count);
	}
}
