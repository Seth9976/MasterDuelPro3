using System;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x020001F1 RID: 497
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public class Sampler
	{
		// Token: 0x06001392 RID: 5010 RVA: 0x000205EB File Offset: 0x0001E7EB
		internal Sampler()
		{
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00029463 File Offset: 0x00027663
		internal Sampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00029474 File Offset: 0x00027674
		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00029498 File Offset: 0x00027698
		public Recorder GetRecorder()
		{
			ProfilerRecorderHandle handle = new ProfilerRecorderHandle((ulong)this.m_Ptr.ToInt64());
			return new Recorder(handle);
		}

		// Token: 0x0400071E RID: 1822
		internal IntPtr m_Ptr;

		// Token: 0x0400071F RID: 1823
		internal static Sampler s_InvalidSampler = new Sampler();
	}
}
