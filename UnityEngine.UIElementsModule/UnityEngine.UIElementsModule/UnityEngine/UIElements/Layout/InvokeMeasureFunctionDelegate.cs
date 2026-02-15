using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000578 RID: 1400
	// (Invoke) Token: 0x060026B7 RID: 9911
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void InvokeMeasureFunctionDelegate(ref LayoutNode node, float width, LayoutMeasureMode widthMode, float height, LayoutMeasureMode heightMode, ref IntPtr exception, out LayoutSize result);
}
