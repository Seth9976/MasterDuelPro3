using System;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000036 RID: 54
	public class DefaultAllocationStrategy : IAllocationStrategy
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000643C File Offset: 0x0000463C
		public object New(Type type, int typeHash)
		{
			return Activator.CreateInstance(type);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006444 File Offset: 0x00004644
		public void Release(int typeHash, object obj)
		{
		}
	}
}
