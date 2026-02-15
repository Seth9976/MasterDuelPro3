using System;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x02000072 RID: 114
	internal interface IProfilerEmitter
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600027B RID: 635
		bool IsEnabled { get; }

		// Token: 0x0600027C RID: 636
		void EmitFrameMetaData(Guid id, int tag, Array data);

		// Token: 0x0600027D RID: 637
		void InitialiseCallbacks(Action<float> onLateUpdateDelegate);
	}
}
