using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200059C RID: 1436
	public interface IGenericScrollViewSupport
	{
		// Token: 0x06002D7D RID: 11645
		void OnItemSetData(GameObject gob, int dataindex);

		// Token: 0x06002D7E RID: 11646
		void OnItemExit(GameObject gob, int dataindex);

		// Token: 0x06002D7F RID: 11647
		void OnItemInitialize(GameObject gob);

		// Token: 0x06002D80 RID: 11648
		void OnGsvStanby();
	}
}
