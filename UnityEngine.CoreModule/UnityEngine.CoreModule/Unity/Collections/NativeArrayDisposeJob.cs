using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x0200005A RID: 90
	[NativeClass(null)]
	internal struct NativeArrayDisposeJob : IJob
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00003FBD File Offset: 0x000021BD
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003FCC File Offset: 0x000021CC
		[RequiredByNativeCode]
		internal static void RegisterNativeArrayDisposeJobReflectionData()
		{
			IJobExtensions.EarlyJobInit<NativeArrayDisposeJob>();
		}

		// Token: 0x04000109 RID: 265
		internal NativeArrayDispose Data;
	}
}
