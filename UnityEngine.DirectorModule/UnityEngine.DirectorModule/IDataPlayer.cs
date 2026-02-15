using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000003 RID: 3
	internal interface IDataPlayer
	{
		// Token: 0x06000004 RID: 4
		void Bind(DataPlayableOutput output);

		// Token: 0x06000005 RID: 5
		void Release(DataPlayableOutput output);
	}
}
