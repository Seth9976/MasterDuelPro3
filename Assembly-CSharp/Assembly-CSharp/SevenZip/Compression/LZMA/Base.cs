using System;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x02000193 RID: 403
	internal abstract class Base
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001B023 File Offset: 0x00019223
		public static uint GetLenToPosState(uint len)
		{
			len -= 2U;
			if (len < 4U)
			{
				return len;
			}
			return 3U;
		}

		// Token: 0x04000A51 RID: 2641
		public const uint kNumRepDistances = 4U;

		// Token: 0x04000A52 RID: 2642
		public const uint kNumStates = 12U;

		// Token: 0x04000A53 RID: 2643
		public const int kNumPosSlotBits = 6;

		// Token: 0x04000A54 RID: 2644
		public const int kDicLogSizeMin = 0;

		// Token: 0x04000A55 RID: 2645
		public const int kNumLenToPosStatesBits = 2;

		// Token: 0x04000A56 RID: 2646
		public const uint kNumLenToPosStates = 4U;

		// Token: 0x04000A57 RID: 2647
		public const uint kMatchMinLen = 2U;

		// Token: 0x04000A58 RID: 2648
		public const int kNumAlignBits = 4;

		// Token: 0x04000A59 RID: 2649
		public const uint kAlignTableSize = 16U;

		// Token: 0x04000A5A RID: 2650
		public const uint kAlignMask = 15U;

		// Token: 0x04000A5B RID: 2651
		public const uint kStartPosModelIndex = 4U;

		// Token: 0x04000A5C RID: 2652
		public const uint kEndPosModelIndex = 14U;

		// Token: 0x04000A5D RID: 2653
		public const uint kNumPosModels = 10U;

		// Token: 0x04000A5E RID: 2654
		public const uint kNumFullDistances = 128U;

		// Token: 0x04000A5F RID: 2655
		public const uint kNumLitPosStatesBitsEncodingMax = 4U;

		// Token: 0x04000A60 RID: 2656
		public const uint kNumLitContextBitsMax = 8U;

		// Token: 0x04000A61 RID: 2657
		public const int kNumPosStatesBitsMax = 4;

		// Token: 0x04000A62 RID: 2658
		public const uint kNumPosStatesMax = 16U;

		// Token: 0x04000A63 RID: 2659
		public const int kNumPosStatesBitsEncodingMax = 4;

		// Token: 0x04000A64 RID: 2660
		public const uint kNumPosStatesEncodingMax = 16U;

		// Token: 0x04000A65 RID: 2661
		public const int kNumLowLenBits = 3;

		// Token: 0x04000A66 RID: 2662
		public const int kNumMidLenBits = 3;

		// Token: 0x04000A67 RID: 2663
		public const int kNumHighLenBits = 8;

		// Token: 0x04000A68 RID: 2664
		public const uint kNumLowLenSymbols = 8U;

		// Token: 0x04000A69 RID: 2665
		public const uint kNumMidLenSymbols = 8U;

		// Token: 0x04000A6A RID: 2666
		public const uint kNumLenSymbols = 272U;

		// Token: 0x04000A6B RID: 2667
		public const uint kMatchMaxLen = 273U;

		// Token: 0x02000194 RID: 404
		public struct State
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x0001B031 File Offset: 0x00019231
			public void Init()
			{
				this.Index = 0U;
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0001B03A File Offset: 0x0001923A
			public void UpdateChar()
			{
				if (this.Index < 4U)
				{
					this.Index = 0U;
					return;
				}
				if (this.Index < 10U)
				{
					this.Index -= 3U;
					return;
				}
				this.Index -= 6U;
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0001B074 File Offset: 0x00019274
			public void UpdateMatch()
			{
				this.Index = ((this.Index < 7U) ? 7U : 10U);
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0001B08A File Offset: 0x0001928A
			public void UpdateRep()
			{
				this.Index = ((this.Index < 7U) ? 8U : 11U);
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0001B0A0 File Offset: 0x000192A0
			public void UpdateShortRep()
			{
				this.Index = ((this.Index < 7U) ? 9U : 11U);
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0001B0B7 File Offset: 0x000192B7
			public bool IsCharState()
			{
				return this.Index < 7U;
			}

			// Token: 0x04000A6C RID: 2668
			public uint Index;
		}
	}
}
