using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020005A4 RID: 1444
	internal struct LayoutState
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0009B77C File Offset: 0x0009997C
		public static LayoutState Default
		{
			get
			{
				return new LayoutState
				{
					measureFunctionCallback = LayoutDelegates.s_InvokeMeasureFunction,
					baselineFunctionCallback = LayoutDelegates.s_InvokeBaselineFunction
				};
			}
		}

		// Token: 0x0400144D RID: 5197
		public IntPtr measureFunctionCallback;

		// Token: 0x0400144E RID: 5198
		public IntPtr baselineFunctionCallback;

		// Token: 0x0400144F RID: 5199
		public IntPtr unusedExceptionPointer;

		// Token: 0x04001450 RID: 5200
		public uint depth;

		// Token: 0x04001451 RID: 5201
		public uint currentGenerationCount;

		// Token: 0x04001452 RID: 5202
		[MarshalAs(UnmanagedType.U1)]
		public bool error;
	}
}
