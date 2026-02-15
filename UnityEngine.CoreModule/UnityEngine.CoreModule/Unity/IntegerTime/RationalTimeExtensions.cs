using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.IntegerTime
{
	// Token: 0x0200001F RID: 31
	public static class RationalTimeExtensions
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002B40 File Offset: 0x00000D40
		[FreeFunction("IntegerTime::RationalTime::ConvertRate", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime Convert(this RationalTime time, RationalTime.TicksPerSecond rate)
		{
			RationalTime rationalTime;
			RationalTimeExtensions.Convert_Injected(ref time, ref rate, out rationalTime);
			return rationalTime;
		}

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Convert_Injected([In] ref RationalTime time, [In] ref RationalTime.TicksPerSecond rate, out RationalTime ret);
	}
}
