using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x02000686 RID: 1670
	internal class ConstantHelper
	{
		// Token: 0x0600343F RID: 13375 RVA: 0x000C4538 File Offset: 0x000C2738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte b = 0;
			*(&b) = byte.MaxValue;
			return b;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000C4554 File Offset: 0x000C2754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte b = 0;
			*(&b) = -1;
			return b;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000C456C File Offset: 0x000C276C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort num = 0;
			*(&num) = ushort.MaxValue;
			return num;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000C4588 File Offset: 0x000C2788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000C45A0 File Offset: 0x000C27A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint num = 0U;
			*(&num) = uint.MaxValue;
			return num;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000C45B8 File Offset: 0x000C27B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000C45D0 File Offset: 0x000C27D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong num = 0UL;
			*(&num) = ulong.MaxValue;
			return num;
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000C45E8 File Offset: 0x000C27E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long num = 0L;
			*(&num) = -1L;
			return num;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000C4600 File Offset: 0x000C2800
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float num = 0f;
			*(int*)(&num) = -1;
			return num;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000C461C File Offset: 0x000C281C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double num = 0.0;
			*(long*)(&num) = -1L;
			return num;
		}
	}
}
