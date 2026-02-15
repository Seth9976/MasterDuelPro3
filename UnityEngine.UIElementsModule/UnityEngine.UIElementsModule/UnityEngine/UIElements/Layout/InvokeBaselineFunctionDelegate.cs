using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000579 RID: 1401
	// (Invoke) Token: 0x060026B9 RID: 9913
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float InvokeBaselineFunctionDelegate(ref LayoutNode node, float width, float height);
}
