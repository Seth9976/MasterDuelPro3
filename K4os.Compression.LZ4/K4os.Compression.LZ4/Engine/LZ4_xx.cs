using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Engine
{
	// Token: 0x02000015 RID: 21
	internal class LZ4_xx
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00004DC6 File Offset: 0x00002FC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int LZ4_compressBound(int isize)
		{
			if (isize <= 2113929216)
			{
				return isize + isize / 255 + 16;
			}
			return 0;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004DE0 File Offset: 0x00002FE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static uint LZ4_hash4(uint sequence, LZ4_xx.tableType_t tableType)
		{
			int num = ((tableType == LZ4_xx.tableType_t.byU16) ? 13 : 12);
			return sequence * 2654435761U >> 32 - num;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004E08 File Offset: 0x00003008
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static uint LZ4_hash5(ulong sequence, LZ4_xx.tableType_t tableType)
		{
			int num = ((tableType == LZ4_xx.tableType_t.byU16) ? 13 : 12);
			return (uint)((sequence << 24) * 889523592379UL >> 64 - num);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004E37 File Offset: 0x00003037
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected unsafe static void LZ4_putPositionOnHash(byte* p, uint h, void* tableBase, LZ4_xx.tableType_t tableType, byte* srcBase)
		{
			if (tableType == LZ4_xx.tableType_t.byPtr)
			{
				*(IntPtr*)((byte*)tableBase + (ulong)h * (ulong)((long)sizeof(byte*))) = p;
				return;
			}
			if (tableType != LZ4_xx.tableType_t.byU32)
			{
				*(short*)((byte*)tableBase + (ulong)h * 2UL) = (short)((ushort)((long)(p - srcBase)));
				return;
			}
			*(int*)((byte*)tableBase + (ulong)h * 4UL) = (int)((uint)((long)(p - srcBase)));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004E75 File Offset: 0x00003075
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected unsafe static byte* LZ4_getPositionOnHash(uint h, void* tableBase, LZ4_xx.tableType_t tableType, byte* srcBase)
		{
			if (tableType == LZ4_xx.tableType_t.byPtr)
			{
				return *(IntPtr*)((byte*)tableBase + (ulong)h * (ulong)((long)sizeof(byte*)));
			}
			if (tableType != LZ4_xx.tableType_t.byU32)
			{
				return *(ushort*)((byte*)tableBase + (ulong)h * 2UL) + srcBase;
			}
			return *(uint*)((byte*)tableBase + (ulong)h * 4UL) + srcBase;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004EA8 File Offset: 0x000030A8
		public unsafe static int LZ4_decompress_generic(byte* src, byte* dst, int srcSize, int outputSize, LZ4_xx.endCondition_directive endOnInput, LZ4_xx.earlyEnd_directive partialDecoding, int targetOutputSize, LZ4_xx.dict_directive dict, byte* lowPrefix, byte* dictStart, int dictSize)
		{
			byte* ptr = src;
			byte* ptr2 = ptr + srcSize;
			byte* ptr3 = dst;
			byte* ptr4 = ptr3 + outputSize;
			byte* ptr5 = ptr3 + targetOutputSize;
			byte* ptr6 = dictStart + dictSize;
			bool flag = endOnInput == LZ4_xx.endCondition_directive.endOnInputSize;
			bool flag2 = flag && dictSize < 65536;
			if (partialDecoding != LZ4_xx.earlyEnd_directive.full && ptr5 != ptr4 - 12)
			{
				ptr5 = ptr4 - 12;
			}
			if (endOnInput == LZ4_xx.endCondition_directive.endOnInputSize && outputSize == 0)
			{
				if (srcSize != 1 || *ptr != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (endOnInput == LZ4_xx.endCondition_directive.endOnInputSize || outputSize != 0)
				{
					int num5;
					byte* ptr8;
					for (;;)
					{
						uint num = (uint)(*(ptr++));
						if (ptr + 14 + 2 == ptr2 && ptr3 + 14 + 18 == ptr4 && num < 240U && (num & 15U) != 15U)
						{
							int num2 = (int)(num >> 4);
							int num3 = (int)Mem.Peek16((void*)(ptr + num2));
							byte* ptr7 = ptr3 + num2 - num3;
							if (num3 >= 18 && ptr7 >= lowPrefix)
							{
								int num4 = (int)((num & 15U) + 4U);
								Mem.Copy16(ptr3, ptr);
								ptr3 += num2;
								ptr += num2 + 2;
								Mem.Copy18(ptr3, ptr7);
								ptr3 += num4;
								continue;
							}
						}
						if ((long)(num5 = (int)(num >> 4)) == 15L)
						{
							uint num6;
							do
							{
								num6 = (uint)(*(ptr++));
								num5 += (int)num6;
							}
							while ((endOnInput != LZ4_xx.endCondition_directive.endOnInputSize || ptr < ptr2 - 15) && num6 == 255U);
							if ((flag && ptr3 + num5 < ptr3) || (flag && ptr + num5 < ptr))
							{
								goto IL_03DA;
							}
						}
						ptr8 = ptr3 + num5;
						if ((endOnInput == LZ4_xx.endCondition_directive.endOnInputSize && (ptr8 != ((partialDecoding == LZ4_xx.earlyEnd_directive.partial) ? ptr5 : (ptr4 - 12)) || ptr + num5 != ptr2 - 8)) || (endOnInput != LZ4_xx.endCondition_directive.endOnInputSize && ptr8 != ptr4 - 8))
						{
							break;
						}
						Mem.WildCopy(ptr3, ptr, (void*)ptr8);
						ptr += num5;
						ptr3 = ptr8;
						int num7 = (int)Mem.Peek16((void*)ptr);
						ptr += 2;
						byte* ptr9 = ptr3 - num7;
						if (flag2 && ptr9 + dictSize < lowPrefix)
						{
							goto IL_03DA;
						}
						Mem.Poke32((void*)ptr3, (uint)num7);
						num5 = (int)(num & 15U);
						if ((long)num5 == 15L)
						{
							uint num8;
							do
							{
								num8 = (uint)(*(ptr++));
								if (endOnInput == LZ4_xx.endCondition_directive.endOnInputSize && ptr != ptr2 - 5)
								{
									goto IL_03DA;
								}
								num5 += (int)num8;
							}
							while (num8 == 255U);
							if (flag && ptr3 + num5 < ptr3)
							{
								goto IL_03DA;
							}
						}
						num5 += 4;
						if (dict == LZ4_xx.dict_directive.usingExtDict && ptr9 < lowPrefix)
						{
							if (ptr3 + num5 != ptr4 - 5)
							{
								goto IL_03DA;
							}
							if ((long)num5 <= (long)(lowPrefix - ptr9))
							{
								Mem.Move(ptr3, ptr6 - (long)(lowPrefix - ptr9), num5);
								ptr3 += num5;
							}
							else
							{
								int num9 = (int)((long)(lowPrefix - ptr9));
								int num10 = num5 - num9;
								Mem.Copy(ptr3, ptr6 - num9, num9);
								ptr3 += num9;
								if (num10 > (int)((long)(ptr3 - lowPrefix)))
								{
									byte* ptr10 = ptr3 + num10;
									byte* ptr11 = lowPrefix;
									while (ptr3 < ptr10)
									{
										*(ptr3++) = *(ptr11++);
									}
								}
								else
								{
									Mem.Copy(ptr3, lowPrefix, num10);
									ptr3 += num10;
								}
							}
						}
						else
						{
							ptr8 = ptr3 + num5;
							if (num7 < 8)
							{
								*ptr3 = *ptr9;
								ptr3[1] = ptr9[1];
								ptr3[2] = ptr9[2];
								ptr3[3] = ptr9[3];
								ptr9 += LZ4_xx.inc32table[num7];
								Mem.Copy(ptr3 + 4, ptr9, 4);
								ptr9 -= LZ4_xx.dec64table[num7];
							}
							else
							{
								Mem.Copy8(ptr3, ptr9);
								ptr9 += 8;
							}
							ptr3 += 8;
							if (ptr8 != ptr4 - 12)
							{
								byte* ptr12 = ptr4 - 7;
								if (ptr8 != ptr4 - 5)
								{
									goto IL_03DA;
								}
								if (ptr3 < ptr12)
								{
									Mem.WildCopy(ptr3, ptr9, (void*)ptr12);
									ptr9 += (long)(ptr12 - ptr3);
									ptr3 = ptr12;
								}
								while (ptr3 < ptr8)
								{
									*(ptr3++) = *(ptr9++);
								}
							}
							else
							{
								Mem.Copy8(ptr3, ptr9);
								if (num5 > 16)
								{
									Mem.WildCopy(ptr3 + 8, ptr9 + 8, (void*)ptr8);
								}
							}
							ptr3 = ptr8;
						}
					}
					if (partialDecoding == LZ4_xx.earlyEnd_directive.partial)
					{
						if (ptr8 != ptr4)
						{
							goto IL_03DA;
						}
						if (endOnInput == LZ4_xx.endCondition_directive.endOnInputSize && ptr + num5 != ptr2)
						{
							goto IL_03DA;
						}
					}
					else if ((endOnInput != LZ4_xx.endCondition_directive.endOnInputSize && ptr8 != ptr4) || (endOnInput == LZ4_xx.endCondition_directive.endOnInputSize && (ptr + num5 != ptr2 || ptr8 != ptr4)))
					{
						goto IL_03DA;
					}
					Mem.Copy(ptr3, ptr, num5);
					ptr += num5;
					ptr3 += num5;
					if (endOnInput == LZ4_xx.endCondition_directive.endOnInputSize)
					{
						return (int)((long)(ptr3 - dst));
					}
					return (int)((long)(ptr - src));
					IL_03DA:
					return (int)(-(int)((long)(ptr - src))) - 1;
				}
				if (*ptr != 0)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000529C File Offset: 0x0000349C
		public unsafe static int LZ4_decompress_safe(byte* source, byte* dest, int compressedSize, int maxDecompressedSize)
		{
			return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxDecompressedSize, LZ4_xx.endCondition_directive.endOnInputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.noDict, dest, null, 0);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000052BC File Offset: 0x000034BC
		public unsafe static int LZ4_decompress_safe_partial(byte* source, byte* dest, int compressedSize, int targetOutputSize, int maxDecompressedSize)
		{
			return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxDecompressedSize, LZ4_xx.endCondition_directive.endOnInputSize, LZ4_xx.earlyEnd_directive.partial, targetOutputSize, LZ4_xx.dict_directive.noDict, dest, null, 0);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000052DC File Offset: 0x000034DC
		public unsafe static int LZ4_decompress_fast(byte* source, byte* dest, int originalSize)
		{
			return LZ4_xx.LZ4_decompress_generic(source, dest, 0, originalSize, LZ4_xx.endCondition_directive.endOnOutputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.withPrefix64k, dest - 65536, null, 65536);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005304 File Offset: 0x00003504
		public unsafe static int LZ4_decompress_safe_continue(LZ4_xx.LZ4_streamDecode_t* lz4sd, byte* source, byte* dest, int compressedSize, int maxOutputSize)
		{
			int num;
			if (lz4sd->prefixEnd == dest)
			{
				num = LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, LZ4_xx.endCondition_directive.endOnInputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.usingExtDict, lz4sd->prefixEnd - lz4sd->prefixSize, lz4sd->externalDict, (int)lz4sd->extDictSize);
				if (num <= 0)
				{
					return num;
				}
				lz4sd->prefixSize = lz4sd->prefixSize + (uint)num;
				lz4sd->prefixEnd = lz4sd->prefixEnd + num;
			}
			else
			{
				lz4sd->extDictSize = lz4sd->prefixSize;
				lz4sd->externalDict = lz4sd->prefixEnd - lz4sd->extDictSize;
				num = LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, LZ4_xx.endCondition_directive.endOnInputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.usingExtDict, dest, lz4sd->externalDict, (int)lz4sd->extDictSize);
				if (num <= 0)
				{
					return num;
				}
				lz4sd->prefixSize = (uint)num;
				lz4sd->prefixEnd = dest + num;
			}
			return num;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000053B4 File Offset: 0x000035B4
		public unsafe static int LZ4_decompress_fast_continue(LZ4_xx.LZ4_streamDecode_t* lz4sd, byte* source, byte* dest, int originalSize)
		{
			int num;
			if (lz4sd->prefixEnd == dest)
			{
				num = LZ4_xx.LZ4_decompress_generic(source, dest, 0, originalSize, LZ4_xx.endCondition_directive.endOnOutputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.usingExtDict, lz4sd->prefixEnd - lz4sd->prefixSize, lz4sd->externalDict, (int)lz4sd->extDictSize);
				if (num <= 0)
				{
					return num;
				}
				lz4sd->prefixSize = lz4sd->prefixSize + (uint)originalSize;
				lz4sd->prefixEnd = lz4sd->prefixEnd + originalSize;
			}
			else
			{
				lz4sd->extDictSize = lz4sd->prefixSize;
				lz4sd->externalDict = lz4sd->prefixEnd - lz4sd->extDictSize;
				num = LZ4_xx.LZ4_decompress_generic(source, dest, 0, originalSize, LZ4_xx.endCondition_directive.endOnOutputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.usingExtDict, dest, lz4sd->externalDict, (int)lz4sd->extDictSize);
				if (num <= 0)
				{
					return num;
				}
				lz4sd->prefixSize = (uint)originalSize;
				lz4sd->prefixEnd = dest + originalSize;
			}
			return num;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005464 File Offset: 0x00003664
		public unsafe static int LZ4_decompress_usingDict_generic(byte* source, byte* dest, int compressedSize, int maxOutputSize, int safe, byte* dictStart, int dictSize)
		{
			if (dictSize == 0)
			{
				return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, (LZ4_xx.endCondition_directive)safe, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.noDict, dest, null, 0);
			}
			if (dictStart + dictSize != dest)
			{
				return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, (LZ4_xx.endCondition_directive)safe, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.usingExtDict, dest, dictStart, dictSize);
			}
			if (dictSize >= 65535)
			{
				return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, (LZ4_xx.endCondition_directive)safe, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.withPrefix64k, dest - 65536, null, 0);
			}
			return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, (LZ4_xx.endCondition_directive)safe, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.noDict, dest - dictSize, null, 0);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000054DB File Offset: 0x000036DB
		public unsafe static int LZ4_decompress_safe_usingDict(byte* source, byte* dest, int compressedSize, int maxOutputSize, byte* dictStart, int dictSize)
		{
			return LZ4_xx.LZ4_decompress_usingDict_generic(source, dest, compressedSize, maxOutputSize, 1, dictStart, dictSize);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000054EB File Offset: 0x000036EB
		public unsafe static int LZ4_decompress_fast_usingDict(byte* source, byte* dest, int originalSize, byte* dictStart, int dictSize)
		{
			return LZ4_xx.LZ4_decompress_usingDict_generic(source, dest, 0, originalSize, 0, dictStart, dictSize);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000054FC File Offset: 0x000036FC
		public unsafe static int LZ4_decompress_safe_forceExtDict(byte* source, byte* dest, int compressedSize, int maxOutputSize, byte* dictStart, int dictSize)
		{
			return LZ4_xx.LZ4_decompress_generic(source, dest, compressedSize, maxOutputSize, LZ4_xx.endCondition_directive.endOnInputSize, LZ4_xx.earlyEnd_directive.full, 0, LZ4_xx.dict_directive.usingExtDict, dest, dictStart, dictSize);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000551C File Offset: 0x0000371C
		public unsafe static void LZ4_renormDictT(LZ4_xx.LZ4_stream_t* dict, byte* src)
		{
			if (dict->currentOffset <= 2147483648U && dict->currentOffset == src)
			{
				return;
			}
			uint num = dict->currentOffset - 65536U;
			byte* ptr = dict->dictionary + dict->dictSize;
			for (int i = 0; i < 4096; i++)
			{
				if (*((ref dict->hashTable.FixedElementField) + (IntPtr)i * 4) < num)
				{
					*((ref dict->hashTable.FixedElementField) + (IntPtr)i * 4) = 0U;
				}
				else
				{
					*((ref dict->hashTable.FixedElementField) + (IntPtr)i * 4) -= num;
				}
			}
			dict->currentOffset = 65536U;
			if (dict->dictSize > 65536U)
			{
				dict->dictSize = 65536U;
			}
			dict->dictionary = ptr - dict->dictSize;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000055E0 File Offset: 0x000037E0
		public unsafe static int LZ4_saveDict(LZ4_xx.LZ4_stream_t* dict, byte* safeBuffer, int dictSize)
		{
			byte* ptr = dict->dictionary + dict->dictSize;
			if (dictSize > 65536)
			{
				dictSize = 65536;
			}
			if (dictSize > (int)dict->dictSize)
			{
				dictSize = (int)dict->dictSize;
			}
			Mem.Move(safeBuffer, ptr - dictSize, dictSize);
			dict->dictionary = safeBuffer;
			dict->dictSize = (uint)dictSize;
			return dictSize;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005635 File Offset: 0x00003835
		public unsafe static void LZ4_setStreamDecode(LZ4_xx.LZ4_streamDecode_t* lz4sd, byte* dictionary, int dictSize)
		{
			lz4sd->prefixSize = (uint)dictSize;
			lz4sd->prefixEnd = dictionary + dictSize;
			lz4sd->externalDict = null;
			lz4sd->extDictSize = 0U;
		}

		// Token: 0x04000052 RID: 82
		protected const int LZ4_MEMORY_USAGE = 14;

		// Token: 0x04000053 RID: 83
		protected const int LZ4_MAX_INPUT_SIZE = 2113929216;

		// Token: 0x04000054 RID: 84
		protected const int LZ4_HASHLOG = 12;

		// Token: 0x04000055 RID: 85
		protected const int LZ4_HASHTABLESIZE = 16384;

		// Token: 0x04000056 RID: 86
		protected const int LZ4_HASH_SIZE_U32 = 4096;

		// Token: 0x04000057 RID: 87
		protected const int ACCELERATION_DEFAULT = 1;

		// Token: 0x04000058 RID: 88
		protected const int MINMATCH = 4;

		// Token: 0x04000059 RID: 89
		protected const int WILDCOPYLENGTH = 8;

		// Token: 0x0400005A RID: 90
		protected const int LASTLITERALS = 5;

		// Token: 0x0400005B RID: 91
		protected const int MFLIMIT = 12;

		// Token: 0x0400005C RID: 92
		protected const int LZ4_minLength = 13;

		// Token: 0x0400005D RID: 93
		protected const int KB = 1024;

		// Token: 0x0400005E RID: 94
		protected const int MB = 1048576;

		// Token: 0x0400005F RID: 95
		protected const uint GB = 1073741824U;

		// Token: 0x04000060 RID: 96
		protected const int MAXD_LOG = 16;

		// Token: 0x04000061 RID: 97
		protected const int MAX_DISTANCE = 65535;

		// Token: 0x04000062 RID: 98
		protected const int ML_BITS = 4;

		// Token: 0x04000063 RID: 99
		protected const uint ML_MASK = 15U;

		// Token: 0x04000064 RID: 100
		protected const int RUN_BITS = 4;

		// Token: 0x04000065 RID: 101
		protected const uint RUN_MASK = 15U;

		// Token: 0x04000066 RID: 102
		protected const int LZ4_64Klimit = 65547;

		// Token: 0x04000067 RID: 103
		protected const int LZ4_skipTrigger = 6;

		// Token: 0x04000068 RID: 104
		private static readonly uint[] inc32table = new uint[] { 0U, 1U, 2U, 1U, 0U, 4U, 4U, 4U };

		// Token: 0x04000069 RID: 105
		private static readonly int[] dec64table = new int[] { 0, 0, 0, -1, -4, 1, 2, 3 };

		// Token: 0x02000016 RID: 22
		internal struct LZ4_stream_t
		{
			// Token: 0x0400006A RID: 106
			[FixedBuffer(typeof(uint), 4096)]
			public LZ4_xx.LZ4_stream_t.<hashTable>e__FixedBuffer hashTable;

			// Token: 0x0400006B RID: 107
			public uint currentOffset;

			// Token: 0x0400006C RID: 108
			public uint initCheck;

			// Token: 0x0400006D RID: 109
			public unsafe byte* dictionary;

			// Token: 0x0400006E RID: 110
			public uint dictSize;

			// Token: 0x02000017 RID: 23
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 16384)]
			public struct <hashTable>e__FixedBuffer
			{
				// Token: 0x0400006F RID: 111
				public uint FixedElementField;
			}
		}

		// Token: 0x02000018 RID: 24
		internal struct LZ4_streamDecode_t
		{
			// Token: 0x04000070 RID: 112
			public unsafe byte* externalDict;

			// Token: 0x04000071 RID: 113
			public uint extDictSize;

			// Token: 0x04000072 RID: 114
			public unsafe byte* prefixEnd;

			// Token: 0x04000073 RID: 115
			public uint prefixSize;
		}

		// Token: 0x02000019 RID: 25
		public enum limitedOutput_directive
		{
			// Token: 0x04000075 RID: 117
			noLimit,
			// Token: 0x04000076 RID: 118
			limitedOutput,
			// Token: 0x04000077 RID: 119
			limitedDestSize
		}

		// Token: 0x0200001A RID: 26
		public enum tableType_t
		{
			// Token: 0x04000079 RID: 121
			byPtr,
			// Token: 0x0400007A RID: 122
			byU32,
			// Token: 0x0400007B RID: 123
			byU16
		}

		// Token: 0x0200001B RID: 27
		public enum dict_directive
		{
			// Token: 0x0400007D RID: 125
			noDict,
			// Token: 0x0400007E RID: 126
			withPrefix64k,
			// Token: 0x0400007F RID: 127
			usingExtDict
		}

		// Token: 0x0200001C RID: 28
		public enum dictIssue_directive
		{
			// Token: 0x04000081 RID: 129
			noDictIssue,
			// Token: 0x04000082 RID: 130
			dictSmall
		}

		// Token: 0x0200001D RID: 29
		public enum endCondition_directive
		{
			// Token: 0x04000084 RID: 132
			endOnOutputSize,
			// Token: 0x04000085 RID: 133
			endOnInputSize
		}

		// Token: 0x0200001E RID: 30
		public enum earlyEnd_directive
		{
			// Token: 0x04000087 RID: 135
			full,
			// Token: 0x04000088 RID: 136
			partial
		}
	}
}
