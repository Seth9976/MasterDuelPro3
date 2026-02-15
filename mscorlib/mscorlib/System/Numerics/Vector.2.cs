using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200068A RID: 1674
	[Intrinsic]
	public static class Vector
	{
		// Token: 0x0600345E RID: 13406 RVA: 0x000C81EA File Offset: 0x000C63EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Equals<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Equals(left, right);
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x00033991 File Offset: 0x00031B91
		public static bool IsHardwareAccelerated
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000C81F3 File Offset: 0x000C63F3
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<ulong> AsVectorUInt64<T>(Vector<T> value) where T : struct
		{
			return (Vector<ulong>)value;
		}
	}
}
