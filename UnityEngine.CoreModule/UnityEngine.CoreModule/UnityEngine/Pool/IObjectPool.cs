using System;

namespace UnityEngine.Pool
{
	// Token: 0x020002EB RID: 747
	public interface IObjectPool<T> where T : class
	{
		// Token: 0x060014EE RID: 5358
		void Release(T element);
	}
}
