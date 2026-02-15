using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	public interface IArrayPool<[Nullable(2)] T>
	{
		// Token: 0x06000015 RID: 21
		T[] Rent(int minimumLength);

		// Token: 0x06000016 RID: 22
		void Return([Nullable(new byte[] { 2, 1 })] T[] array);
	}
}
