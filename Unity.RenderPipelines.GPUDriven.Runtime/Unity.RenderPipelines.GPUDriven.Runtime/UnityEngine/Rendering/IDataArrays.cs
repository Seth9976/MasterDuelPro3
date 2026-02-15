using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006D RID: 109
	internal interface IDataArrays
	{
		// Token: 0x0600021F RID: 543
		void Initialize(int initCapacity);

		// Token: 0x06000220 RID: 544
		void Dispose();

		// Token: 0x06000221 RID: 545
		void Grow(int newCapacity);

		// Token: 0x06000222 RID: 546
		void Remove(int index, int lastIndex);

		// Token: 0x06000223 RID: 547
		void SetDefault(int index);
	}
}
