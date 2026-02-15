using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x020004FB RID: 1275
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRendererJobProcessor.h")]
	internal static class JobProcessor
	{
		// Token: 0x060023AB RID: 9131 RVA: 0x00082D8C File Offset: 0x00080F8C
		internal static JobHandle ScheduleNudgeJobs(IntPtr buffer, int jobCount)
		{
			JobHandle jobHandle;
			JobProcessor.ScheduleNudgeJobs_Injected(buffer, jobCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00082DA4 File Offset: 0x00080FA4
		internal static JobHandle ScheduleConvertMeshJobs(IntPtr buffer, int jobCount)
		{
			JobHandle jobHandle;
			JobProcessor.ScheduleConvertMeshJobs_Injected(buffer, jobCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x00082DBC File Offset: 0x00080FBC
		internal static JobHandle ScheduleCopyMeshJobs(IntPtr buffer, int jobCount)
		{
			JobHandle jobHandle;
			JobProcessor.ScheduleCopyMeshJobs_Injected(buffer, jobCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060023AE RID: 9134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleNudgeJobs_Injected(IntPtr buffer, int jobCount, out JobHandle ret);

		// Token: 0x060023AF RID: 9135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleConvertMeshJobs_Injected(IntPtr buffer, int jobCount, out JobHandle ret);

		// Token: 0x060023B0 RID: 9136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleCopyMeshJobs_Injected(IntPtr buffer, int jobCount, out JobHandle ret);
	}
}
