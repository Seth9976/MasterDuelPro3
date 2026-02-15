using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E4 RID: 228
	public class ColorGamutUtility
	{
		// Token: 0x06000602 RID: 1538
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ColorPrimaries GetColorPrimaries(ColorGamut gamut);

		// Token: 0x06000603 RID: 1539
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern WhitePoint GetWhitePoint(ColorGamut gamut);

		// Token: 0x06000604 RID: 1540
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern TransferFunction GetTransferFunction(ColorGamut gamut);
	}
}
