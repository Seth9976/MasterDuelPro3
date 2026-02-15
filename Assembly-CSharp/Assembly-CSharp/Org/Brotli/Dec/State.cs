using System;
using System.IO;

namespace Org.Brotli.Dec
{
	// Token: 0x02000083 RID: 131
	internal sealed class State
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00009370 File Offset: 0x00007570
		private static int DecodeWindowBits(BitReader br)
		{
			if (BitReader.ReadBits(br, 1) == 0)
			{
				return 16;
			}
			int i = BitReader.ReadBits(br, 3);
			if (i != 0)
			{
				return 17 + i;
			}
			i = BitReader.ReadBits(br, 3);
			if (i != 0)
			{
				return 8 + i;
			}
			return 17;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000093AC File Offset: 0x000075AC
		internal static void SetInput(State state, Stream input)
		{
			if (state.runningState != 0)
			{
				throw new InvalidOperationException("State MUST be uninitialized");
			}
			BitReader.Init(state.br, input);
			int windowBits = State.DecodeWindowBits(state.br);
			if (windowBits == 9)
			{
				throw new BrotliRuntimeException("Invalid 'windowBits' code");
			}
			state.maxRingBufferSize = 1 << windowBits;
			state.maxBackwardDistance = state.maxRingBufferSize - 16;
			state.runningState = 1;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009416 File Offset: 0x00007616
		internal static void Close(State state)
		{
			if (state.runningState == 0)
			{
				throw new InvalidOperationException("State MUST be initialized");
			}
			if (state.runningState == 11)
			{
				return;
			}
			state.runningState = 11;
			BitReader.Close(state.br);
		}

		// Token: 0x04000306 RID: 774
		internal int runningState;

		// Token: 0x04000307 RID: 775
		internal int nextRunningState;

		// Token: 0x04000308 RID: 776
		internal readonly BitReader br = new BitReader();

		// Token: 0x04000309 RID: 777
		internal byte[] ringBuffer;

		// Token: 0x0400030A RID: 778
		internal readonly int[] blockTypeTrees = new int[3240];

		// Token: 0x0400030B RID: 779
		internal readonly int[] blockLenTrees = new int[3240];

		// Token: 0x0400030C RID: 780
		internal int metaBlockLength;

		// Token: 0x0400030D RID: 781
		internal bool inputEnd;

		// Token: 0x0400030E RID: 782
		internal bool isUncompressed;

		// Token: 0x0400030F RID: 783
		internal bool isMetadata;

		// Token: 0x04000310 RID: 784
		internal readonly HuffmanTreeGroup hGroup0 = new HuffmanTreeGroup();

		// Token: 0x04000311 RID: 785
		internal readonly HuffmanTreeGroup hGroup1 = new HuffmanTreeGroup();

		// Token: 0x04000312 RID: 786
		internal readonly HuffmanTreeGroup hGroup2 = new HuffmanTreeGroup();

		// Token: 0x04000313 RID: 787
		internal readonly int[] blockLength = new int[3];

		// Token: 0x04000314 RID: 788
		internal readonly int[] numBlockTypes = new int[3];

		// Token: 0x04000315 RID: 789
		internal readonly int[] blockTypeRb = new int[6];

		// Token: 0x04000316 RID: 790
		internal readonly int[] distRb = new int[] { 16, 15, 11, 4 };

		// Token: 0x04000317 RID: 791
		internal int pos;

		// Token: 0x04000318 RID: 792
		internal int maxDistance;

		// Token: 0x04000319 RID: 793
		internal int distRbIdx;

		// Token: 0x0400031A RID: 794
		internal bool trivialLiteralContext;

		// Token: 0x0400031B RID: 795
		internal int literalTreeIndex;

		// Token: 0x0400031C RID: 796
		internal int literalTree;

		// Token: 0x0400031D RID: 797
		internal int j;

		// Token: 0x0400031E RID: 798
		internal int insertLength;

		// Token: 0x0400031F RID: 799
		internal byte[] contextModes;

		// Token: 0x04000320 RID: 800
		internal byte[] contextMap;

		// Token: 0x04000321 RID: 801
		internal int contextMapSlice;

		// Token: 0x04000322 RID: 802
		internal int distContextMapSlice;

		// Token: 0x04000323 RID: 803
		internal int contextLookupOffset1;

		// Token: 0x04000324 RID: 804
		internal int contextLookupOffset2;

		// Token: 0x04000325 RID: 805
		internal int treeCommandOffset;

		// Token: 0x04000326 RID: 806
		internal int distanceCode;

		// Token: 0x04000327 RID: 807
		internal byte[] distContextMap;

		// Token: 0x04000328 RID: 808
		internal int numDirectDistanceCodes;

		// Token: 0x04000329 RID: 809
		internal int distancePostfixMask;

		// Token: 0x0400032A RID: 810
		internal int distancePostfixBits;

		// Token: 0x0400032B RID: 811
		internal int distance;

		// Token: 0x0400032C RID: 812
		internal int copyLength;

		// Token: 0x0400032D RID: 813
		internal int copyDst;

		// Token: 0x0400032E RID: 814
		internal int maxBackwardDistance;

		// Token: 0x0400032F RID: 815
		internal int maxRingBufferSize;

		// Token: 0x04000330 RID: 816
		internal int ringBufferSize;

		// Token: 0x04000331 RID: 817
		internal long expectedTotalSize;

		// Token: 0x04000332 RID: 818
		internal byte[] customDictionary = new byte[0];

		// Token: 0x04000333 RID: 819
		internal int bytesToIgnore;

		// Token: 0x04000334 RID: 820
		internal int outputOffset;

		// Token: 0x04000335 RID: 821
		internal int outputLength;

		// Token: 0x04000336 RID: 822
		internal int outputUsed;

		// Token: 0x04000337 RID: 823
		internal int bytesWritten;

		// Token: 0x04000338 RID: 824
		internal int bytesToWrite;

		// Token: 0x04000339 RID: 825
		internal byte[] output;
	}
}
