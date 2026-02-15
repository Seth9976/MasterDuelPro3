using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001AD RID: 429
	public static class BitArrayUtilities
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x0002C31E File Offset: 0x0002A51E
		public static bool Get8(uint index, byte data)
		{
			return ((int)data & (1 << (int)index)) != 0;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002C31E File Offset: 0x0002A51E
		public static bool Get16(uint index, ushort data)
		{
			return ((int)data & (1 << (int)index)) != 0;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002C31E File Offset: 0x0002A51E
		public static bool Get32(uint index, uint data)
		{
			return (data & (1U << (int)index)) > 0U;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002C32B File Offset: 0x0002A52B
		public static bool Get64(uint index, ulong data)
		{
			return (data & (1UL << (int)index)) > 0UL;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002C33A File Offset: 0x0002A53A
		public static bool Get128(uint index, ulong data1, ulong data2)
		{
			if (index >= 64U)
			{
				return (data2 & (1UL << (int)(index - 64U))) > 0UL;
			}
			return (data1 & (1UL << (int)index)) > 0UL;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002C360 File Offset: 0x0002A560
		public static bool Get256(uint index, ulong data1, ulong data2, ulong data3, ulong data4)
		{
			if (index >= 128U)
			{
				if (index >= 192U)
				{
					return (data4 & (1UL << (int)(index - 192U))) > 0UL;
				}
				return (data3 & (1UL << (int)(index - 128U))) > 0UL;
			}
			else
			{
				if (index >= 64U)
				{
					return (data2 & (1UL << (int)(index - 64U))) > 0UL;
				}
				return (data1 & (1UL << (int)index)) > 0UL;
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002C3C9 File Offset: 0x0002A5C9
		public static void Set8(uint index, ref byte data, bool value)
		{
			data = (byte)(value ? ((int)data | (1 << (int)index)) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002C3E6 File Offset: 0x0002A5E6
		public static void Set16(uint index, ref ushort data, bool value)
		{
			data = (ushort)(value ? ((int)data | (1 << (int)index)) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002C403 File Offset: 0x0002A603
		public static void Set32(uint index, ref uint data, bool value)
		{
			data = (value ? (data | (1U << (int)index)) : (data & ~(1U << (int)index)));
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002C41F File Offset: 0x0002A61F
		public static void Set64(uint index, ref ulong data, bool value)
		{
			data = (value ? (data | (1UL << (int)index)) : (data & ~(1UL << (int)index)));
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002C440 File Offset: 0x0002A640
		public static void Set128(uint index, ref ulong data1, ref ulong data2, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | (1UL << (int)index)) : (data1 & ~(1UL << (int)index)));
				return;
			}
			data2 = (value ? (data2 | (1UL << (int)(index - 64U))) : (data2 & ~(1UL << (int)(index - 64U))));
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002C494 File Offset: 0x0002A694
		public static void Set256(uint index, ref ulong data1, ref ulong data2, ref ulong data3, ref ulong data4, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | (1UL << (int)index)) : (data1 & ~(1UL << (int)index)));
				return;
			}
			if (index < 128U)
			{
				data2 = (value ? (data2 | (1UL << (int)(index - 64U))) : (data2 & ~(1UL << (int)(index - 64U))));
				return;
			}
			if (index < 192U)
			{
				data3 = (value ? (data3 | (1UL << (int)(index - 64U))) : (data3 & ~(1UL << (int)(index - 128U))));
				return;
			}
			data4 = (value ? (data4 | (1UL << (int)(index - 64U))) : (data4 & ~(1UL << (int)(index - 192U))));
		}
	}
}
