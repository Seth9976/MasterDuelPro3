using System;

namespace YgomSystem.UI
{
	// Token: 0x0200059E RID: 1438
	public interface IObjectPool<T>
	{
		// Token: 0x06002D85 RID: 11653
		void CreateReserve(int cnt);

		// Token: 0x06002D86 RID: 11654
		T Rent();

		// Token: 0x06002D87 RID: 11655
		void Return(T obj);

		// Token: 0x06002D88 RID: 11656
		void ReturnAll();
	}
}
