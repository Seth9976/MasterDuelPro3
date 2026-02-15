using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000055 RID: 85
	public interface ITimeControl
	{
		// Token: 0x060002DB RID: 731
		void SetTime(double time);

		// Token: 0x060002DC RID: 732
		void OnControlTimeStart();

		// Token: 0x060002DD RID: 733
		void OnControlTimeStop();
	}
}
