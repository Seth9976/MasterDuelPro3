using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000DD RID: 221
	[GenerateTestsForBurstCompatibility]
	public struct StreamCompressionModel
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001DB00 File Offset: 0x0001BD00
		public unsafe static StreamCompressionModel Default
		{
			get
			{
				if (StreamCompressionModel.SharedStaticCompressionModel.Default.Data.m_Initialized == 1)
				{
					return *StreamCompressionModel.SharedStaticCompressionModel.Default.Data;
				}
				StreamCompressionModel.Initialize();
				StreamCompressionModel.SharedStaticCompressionModel.Default.Data.m_Initialized = 1;
				return *StreamCompressionModel.SharedStaticCompressionModel.Default.Data;
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001DB54 File Offset: 0x0001BD54
		private unsafe static void Initialize()
		{
			for (int i = 0; i < 16; i++)
			{
				*((ref StreamCompressionModel.SharedStaticCompressionModel.Default.Data.bucketSizes.FixedElementField) + i) = StreamCompressionModel.k_BucketSizes[i];
				*((ref StreamCompressionModel.SharedStaticCompressionModel.Default.Data.bucketOffsets.FixedElementField) + (IntPtr)i * 4) = StreamCompressionModel.k_BucketOffsets[i];
			}
			NativeArray<byte> modelData = new NativeArray<byte>(StreamCompressionModel.k_DefaultModelData.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int index = 0; index < StreamCompressionModel.k_DefaultModelData.Length; index++)
			{
				modelData[index] = StreamCompressionModel.k_DefaultModelData[index];
			}
			int numContexts = 1;
			NativeArray<byte> symbolLengths = new NativeArray<byte>(numContexts * 16, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int readOffset = 0;
			byte b = modelData[readOffset++];
			for (int j = 0; j < 16; j++)
			{
				byte length = modelData[readOffset++];
				for (int context = 0; context < numContexts; context++)
				{
					symbolLengths[numContexts * context + j] = length;
				}
			}
			int numModels = (int)modelData[readOffset] | ((int)modelData[readOffset + 1] << 8);
			readOffset += 2;
			for (int model = 0; model < numModels; model++)
			{
				int context2 = (int)modelData[readOffset] | ((int)modelData[readOffset + 1] << 8);
				readOffset += 2;
				byte b2 = modelData[readOffset++];
				for (int k = 0; k < 16; k++)
				{
					byte length2 = modelData[readOffset++];
					symbolLengths[numContexts * context2 + k] = length2;
				}
			}
			NativeArray<byte> tmpSymbolLengths = new NativeArray<byte>(16, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<ushort> tmpSymbolDecodeTable = new NativeArray<ushort>(64, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<byte> symbolCodes = new NativeArray<byte>(16, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int context3 = 0; context3 < numContexts; context3++)
			{
				for (int l = 0; l < 16; l++)
				{
					tmpSymbolLengths[l] = symbolLengths[numContexts * context3 + l];
				}
				StreamCompressionModel.GenerateHuffmanCodes(symbolCodes, 0, tmpSymbolLengths, 0, 16, 6);
				StreamCompressionModel.GenerateHuffmanDecodeTable(tmpSymbolDecodeTable, 0, tmpSymbolLengths, symbolCodes, 16, 6);
				for (int m = 0; m < 16; m++)
				{
					*((ref StreamCompressionModel.SharedStaticCompressionModel.Default.Data.encodeTable.FixedElementField) + (IntPtr)(context3 * 16 + m) * 2) = (ushort)(((int)symbolCodes[m] << 8) | (int)symbolLengths[numContexts * context3 + m]);
				}
				for (int n = 0; n < 64; n++)
				{
					*((ref StreamCompressionModel.SharedStaticCompressionModel.Default.Data.decodeTable.FixedElementField) + (IntPtr)(context3 * 64 + n) * 2) = tmpSymbolDecodeTable[n];
				}
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
		private static void GenerateHuffmanCodes(NativeArray<byte> symbolCodes, int symbolCodesOffset, NativeArray<byte> symbolLengths, int symbolLengthsOffset, int alphabetSize, int maxCodeLength)
		{
			NativeArray<byte> lengthCounts = new NativeArray<byte>(maxCodeLength + 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<byte> symbolList = new NativeArray<byte>((maxCodeLength + 1) * alphabetSize, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int symbol = 0; symbol < alphabetSize; symbol++)
			{
				int symbolLength = (int)symbolLengths[symbol + symbolLengthsOffset];
				int num = (maxCodeLength + 1) * symbolLength;
				int num2 = symbolLength;
				byte b = lengthCounts[num2];
				lengthCounts[num2] = b + 1;
				symbolList[num + (int)b] = (byte)symbol;
			}
			uint nextCodeWord = 0U;
			for (int length = 1; length <= maxCodeLength; length++)
			{
				int length_count = (int)lengthCounts[length];
				for (int i = 0; i < length_count; i++)
				{
					int symbol2 = (int)symbolList[(maxCodeLength + 1) * length + i];
					symbolCodes[symbol2 + symbolCodesOffset] = (byte)StreamCompressionModel.ReverseBits(nextCodeWord++, length);
				}
				nextCodeWord <<= 1;
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		private static uint ReverseBits(uint value, int num_bits)
		{
			value = ((value & 1431655765U) << 1) | ((value & 2863311530U) >> 1);
			value = ((value & 858993459U) << 2) | ((value & 3435973836U) >> 2);
			value = ((value & 252645135U) << 4) | ((value & 4042322160U) >> 4);
			value = ((value & 16711935U) << 8) | ((value & 4278255360U) >> 8);
			value = (value << 16) | (value >> 16);
			return value >> 32 - num_bits;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001DF18 File Offset: 0x0001C118
		private static void GenerateHuffmanDecodeTable(NativeArray<ushort> decodeTable, int decodeTableOffset, NativeArray<byte> symbolLengths, NativeArray<byte> symbolCodes, int alphabetSize, int maxCodeLength)
		{
			uint maxCode = 1U << maxCodeLength;
			for (int symbol = 0; symbol < alphabetSize; symbol++)
			{
				int length = (int)symbolLengths[symbol];
				if (length > 0)
				{
					uint code = (uint)symbolCodes[symbol];
					uint step = 1U << length;
					do
					{
						decodeTable[(int)((long)decodeTableOffset + (long)((ulong)code))] = (ushort)((symbol << 8) | length);
						code += step;
					}
					while (code < maxCode);
				}
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001DF74 File Offset: 0x0001C174
		public unsafe readonly int CalculateBucket(uint value)
		{
			int bucketIndex = StreamCompressionModel.k_FirstBucketCandidate[math.lzcnt(value)];
			if (bucketIndex + 1 < 16 && value >= *((ref this.bucketOffsets.FixedElementField) + (IntPtr)(bucketIndex + 1) * 4))
			{
				bucketIndex++;
			}
			return bucketIndex;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
		public unsafe readonly int GetCompressedSizeInBits(uint value)
		{
			int bucket = this.CalculateBucket(value);
			int bits = (int)(*((ref this.bucketSizes.FixedElementField) + bucket));
			return (int)(*((ref this.encodeTable.FixedElementField) + (IntPtr)bucket * 2) & 255) + bits;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckAlphabetSize(int alphabetSize)
		{
			if (alphabetSize != 16)
			{
				throw new InvalidOperationException("The alphabet size of compression models must be " + 16.ToString());
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001E01C File Offset: 0x0001C21C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckSymbolLength(NativeArray<byte> symbolLengths, int symbolLengthsOffset, int symbol, int length)
		{
			if ((int)symbolLengths[symbol + symbolLengthsOffset] != length)
			{
				throw new InvalidOperationException("Incorrect symbol length");
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001E036 File Offset: 0x0001C236
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckAlphabetAndMaxCodeLength(int alphabetSize, int maxCodeLength)
		{
			if (alphabetSize > 256 || maxCodeLength > 8)
			{
				throw new InvalidOperationException("Can only generate huffman codes up to alphabet size 256 and maximum code length 8");
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001E04F File Offset: 0x0001C24F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckExceedMaxCodeLength(int length, int maxCodeLength)
		{
			if (length > maxCodeLength)
			{
				throw new InvalidOperationException("Maximum code length exceeded");
			}
		}

		// Token: 0x0400042F RID: 1071
		internal static readonly byte[] k_BucketSizes = new byte[]
		{
			0, 0, 1, 2, 3, 4, 6, 8, 10, 12,
			15, 18, 21, 24, 27, 32
		};

		// Token: 0x04000430 RID: 1072
		internal static readonly uint[] k_BucketOffsets = new uint[]
		{
			0U, 1U, 2U, 4U, 8U, 16U, 32U, 96U, 352U, 1376U,
			5472U, 38240U, 300384U, 2397536U, 19174752U, 153392480U
		};

		// Token: 0x04000431 RID: 1073
		internal static readonly int[] k_FirstBucketCandidate = new int[]
		{
			15, 15, 15, 15, 14, 14, 14, 13, 13, 13,
			12, 12, 12, 11, 11, 11, 10, 10, 10, 9,
			9, 8, 8, 7, 7, 6, 5, 4, 3, 2,
			1, 1, 0
		};

		// Token: 0x04000432 RID: 1074
		internal static readonly byte[] k_DefaultModelData = new byte[]
		{
			16, 2, 3, 3, 3, 4, 4, 4, 5, 5,
			5, 6, 6, 6, 6, 6, 6, 0, 0
		};

		// Token: 0x04000433 RID: 1075
		internal const int k_AlphabetSize = 16;

		// Token: 0x04000434 RID: 1076
		internal const int k_MaxHuffmanSymbolLength = 6;

		// Token: 0x04000435 RID: 1077
		internal const int k_MaxContexts = 1;

		// Token: 0x04000436 RID: 1078
		private byte m_Initialized;

		// Token: 0x04000437 RID: 1079
		[FixedBuffer(typeof(ushort), 16)]
		internal StreamCompressionModel.<encodeTable>e__FixedBuffer encodeTable;

		// Token: 0x04000438 RID: 1080
		[FixedBuffer(typeof(ushort), 64)]
		internal StreamCompressionModel.<decodeTable>e__FixedBuffer decodeTable;

		// Token: 0x04000439 RID: 1081
		[FixedBuffer(typeof(byte), 16)]
		internal StreamCompressionModel.<bucketSizes>e__FixedBuffer bucketSizes;

		// Token: 0x0400043A RID: 1082
		[FixedBuffer(typeof(uint), 16)]
		internal StreamCompressionModel.<bucketOffsets>e__FixedBuffer bucketOffsets;

		// Token: 0x020000DE RID: 222
		private static class SharedStaticCompressionModel
		{
			// Token: 0x0400043B RID: 1083
			internal static readonly SharedStatic<StreamCompressionModel> Default = SharedStatic<StreamCompressionModel>.GetOrCreateUnsafe(0U, 6564095697914452312L, 0L);
		}

		// Token: 0x020000DF RID: 223
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <bucketOffsets>e__FixedBuffer
		{
			// Token: 0x0400043C RID: 1084
			public uint FixedElementField;
		}

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <bucketSizes>e__FixedBuffer
		{
			// Token: 0x0400043D RID: 1085
			public byte FixedElementField;
		}

		// Token: 0x020000E1 RID: 225
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <decodeTable>e__FixedBuffer
		{
			// Token: 0x0400043E RID: 1086
			public ushort FixedElementField;
		}

		// Token: 0x020000E2 RID: 226
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <encodeTable>e__FixedBuffer
		{
			// Token: 0x0400043F RID: 1087
			public ushort FixedElementField;
		}
	}
}
