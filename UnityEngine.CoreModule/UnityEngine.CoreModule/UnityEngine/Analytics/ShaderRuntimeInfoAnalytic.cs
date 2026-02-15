using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x0200031D RID: 797
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class ShaderRuntimeInfoAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600161B RID: 5659 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		private ShaderRuntimeInfoAnalytic()
			: base("shaderRuntimeInfo", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0002E79C File Offset: 0x0002C99C
		[RequiredByNativeCode]
		public static ShaderRuntimeInfoAnalytic CreateShaderRuntimeInfoAnalytic()
		{
			return new ShaderRuntimeInfoAnalytic();
		}

		// Token: 0x04000842 RID: 2114
		public long VariantsRequested = 0L;

		// Token: 0x04000843 RID: 2115
		public long VariantsRequestedMissing = 0L;

		// Token: 0x04000844 RID: 2116
		public long VariantsRequestedUnsupported = 0L;

		// Token: 0x04000845 RID: 2117
		public long VariantsRequestedCompiled = 0L;

		// Token: 0x04000846 RID: 2118
		public long VariantsRequestedViaWarmup = 0L;

		// Token: 0x04000847 RID: 2119
		public long VariantsUnused = 0L;

		// Token: 0x04000848 RID: 2120
		public int VariantsCompilationTimeTotal = 0;

		// Token: 0x04000849 RID: 2121
		public int VariantsCompilationTimeMax = 0;

		// Token: 0x0400084A RID: 2122
		public int VariantsCompilationTimeMedian = 0;

		// Token: 0x0400084B RID: 2123
		public int VariantsWarmupTimeTotal = 0;

		// Token: 0x0400084C RID: 2124
		public int VariantsWarmupTimeMax = 0;

		// Token: 0x0400084D RID: 2125
		public int VariantsWarmupTimeMedian = 0;

		// Token: 0x0400084E RID: 2126
		public bool UseProgressiveWarmup = false;

		// Token: 0x0400084F RID: 2127
		public int ShaderChunkSizeMin = 0;

		// Token: 0x04000850 RID: 2128
		public int ShaderChunkSizeMax = 0;

		// Token: 0x04000851 RID: 2129
		public int ShaderChunkSizeAvg = 0;

		// Token: 0x04000852 RID: 2130
		public int ShaderChunkCountMin = 0;

		// Token: 0x04000853 RID: 2131
		public int ShaderChunkCountMax = 0;

		// Token: 0x04000854 RID: 2132
		public int ShaderChunkCountAvg = 0;
	}
}
