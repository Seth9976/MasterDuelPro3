using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E2 RID: 226
	[NativeHeader("Runtime/Export/Graphics/Graphics.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightProbes : Object
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		[RequiredByNativeCode]
		private static void Internal_CallLightProbesUpdatedFunction()
		{
			bool flag = LightProbes.lightProbesUpdated != null;
			if (flag)
			{
				LightProbes.lightProbesUpdated();
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000CA64 File Offset: 0x0000AC64
		[RequiredByNativeCode]
		private static void Internal_CallTetrahedralizationCompletedFunction()
		{
			bool flag = LightProbes.tetrahedralizationCompleted != null;
			if (flag)
			{
				LightProbes.tetrahedralizationCompleted();
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		[RequiredByNativeCode]
		private static void Internal_CallNeedsRetetrahedralizationFunction()
		{
			bool flag = LightProbes.needsRetetrahedralization != null;
			if (flag)
			{
				LightProbes.needsRetetrahedralization();
			}
		}

		// Token: 0x040002A4 RID: 676
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action lightProbesUpdated;

		// Token: 0x040002A5 RID: 677
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action tetrahedralizationCompleted;

		// Token: 0x040002A6 RID: 678
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action needsRetetrahedralization;
	}
}
