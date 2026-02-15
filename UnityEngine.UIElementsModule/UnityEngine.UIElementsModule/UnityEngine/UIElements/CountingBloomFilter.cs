using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000173 RID: 371
	internal struct CountingBloomFilter
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x000358A8 File Offset: 0x00033AA8
		private unsafe void AdjustSlot(uint index, bool increment)
		{
			if (increment)
			{
				bool flag = *((ref this.m_Counters.FixedElementField) + (UIntPtr)index) != byte.MaxValue;
				if (flag)
				{
					ref byte ptr = (ref this.m_Counters.FixedElementField) + (UIntPtr)index;
					ptr += 1;
				}
			}
			else
			{
				bool flag2 = *((ref this.m_Counters.FixedElementField) + (UIntPtr)index) > 0;
				if (flag2)
				{
					ref byte ptr2 = (ref this.m_Counters.FixedElementField) + (UIntPtr)index;
					ptr2 -= 1;
				}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003591C File Offset: 0x00033B1C
		private uint Hash1(uint hash)
		{
			return hash & 16383U;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00035938 File Offset: 0x00033B38
		private uint Hash2(uint hash)
		{
			return (hash >> 14) & 16383U;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00035954 File Offset: 0x00033B54
		private unsafe bool IsSlotEmpty(uint index)
		{
			return *((ref this.m_Counters.FixedElementField) + (UIntPtr)index) == 0;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00035978 File Offset: 0x00033B78
		public void InsertHash(uint hash)
		{
			this.AdjustSlot(this.Hash1(hash), true);
			this.AdjustSlot(this.Hash2(hash), true);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00035999 File Offset: 0x00033B99
		public void RemoveHash(uint hash)
		{
			this.AdjustSlot(this.Hash1(hash), false);
			this.AdjustSlot(this.Hash2(hash), false);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000359BC File Offset: 0x00033BBC
		public bool ContainsHash(uint hash)
		{
			return !this.IsSlotEmpty(this.Hash1(hash)) && !this.IsSlotEmpty(this.Hash2(hash));
		}

		// Token: 0x0400071B RID: 1819
		[FixedBuffer(typeof(byte), 16384)]
		private CountingBloomFilter.<m_Counters>e__FixedBuffer m_Counters;

		// Token: 0x02000174 RID: 372
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 16384)]
		public struct <m_Counters>e__FixedBuffer
		{
			// Token: 0x0400071C RID: 1820
			public byte FixedElementField;
		}
	}
}
