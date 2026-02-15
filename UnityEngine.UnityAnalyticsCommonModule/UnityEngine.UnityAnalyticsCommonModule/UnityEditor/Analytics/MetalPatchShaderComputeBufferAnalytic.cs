using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000010 RID: 16
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class MetalPatchShaderComputeBufferAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000224B File Offset: 0x0000044B
		public MetalPatchShaderComputeBufferAnalytic()
			: base("MetalPatchShaderComputeBuffersUsage", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002264 File Offset: 0x00000464
		[RequiredByNativeCode]
		internal static MetalPatchShaderComputeBufferAnalytic CreateMetalPatchShaderComputeBufferAnalytic()
		{
			return new MetalPatchShaderComputeBufferAnalytic();
		}
	}
}
