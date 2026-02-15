using System;
using UnityEngine.Profiling;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x0200006C RID: 108
	public class EngineEmitter : IProfilerEmitter
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009F35 File Offset: 0x00008135
		public bool IsEnabled
		{
			get
			{
				return Profiler.enabled;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006444 File Offset: 0x00004644
		public void EmitFrameMetaData(Guid id, int tag, Array data)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009F3C File Offset: 0x0000813C
		public void InitialiseCallbacks(Action<float> d)
		{
			ComponentSingleton<MonoBehaviourCallbackHooks>.Instance.OnLateUpdateDelegate += d;
		}
	}
}
