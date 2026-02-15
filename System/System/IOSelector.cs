using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200010B RID: 267
	internal static class IOSelector
	{
		// Token: 0x06000550 RID: 1360
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Add(IntPtr handle, IOSelectorJob job);
	}
}
