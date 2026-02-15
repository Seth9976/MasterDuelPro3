using System;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000035 RID: 53
	public interface IAllocationStrategy
	{
		// Token: 0x06000137 RID: 311
		object New(Type type, int typeHash);

		// Token: 0x06000138 RID: 312
		void Release(int typeHash, object obj);
	}
}
