using System;
using System.Runtime.CompilerServices;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Engine
{
	// Token: 0x0200000B RID: 11
	internal class LZ4_64 : LZ4_xx
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002797 File Offset: 0x00000997
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected unsafe static ulong LZ4_read_ARCH(void* p)
		{
			return (ulong)(*(long*)p);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000295C File Offset: 0x00000B5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static uint LZ4_NbCommonBytes(ulong val)
		{
			checked
			{
				return LZ4_64.DeBruijnBytePos[(int)((IntPtr)(unchecked((val & -val) * 151050438428048703UL) >> 58))];
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002976 File Offset: 0x00000B76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static LZ4_xx.tableType_t LZ4_tableType(int inputSize)
		{
			if (inputSize >= 65547)
			{
				return LZ4_xx.tableType_t.byU32;
			}
			return LZ4_xx.tableType_t.byU16;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002984 File Offset: 0x00000B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected unsafe static uint LZ4_count(byte* pIn, byte* pMatch, byte* pInLimit)
		{
			byte* ptr = pIn;
			if (pIn < pInLimit - 7)
			{
				ulong num = LZ4_64.LZ4_read_ARCH((void*)pMatch) ^ LZ4_64.LZ4_read_ARCH((void*)pIn);
				if (num != 0UL)
				{
					return LZ4_64.LZ4_NbCommonBytes(num);
				}
				pIn += 8;
				pMatch += 8;
			}
			while (pIn < pInLimit - 7)
			{
				ulong num2 = LZ4_64.LZ4_read_ARCH((void*)pMatch) ^ LZ4_64.LZ4_read_ARCH((void*)pIn);
				if (num2 != 0UL)
				{
					return (uint)((long)(pIn + LZ4_64.LZ4_NbCommonBytes(num2) - ptr));
				}
				pIn += 8;
				pMatch += 8;
			}
			if (pIn < pInLimit - 3 && Mem.Peek32((void*)pMatch) == Mem.Peek32((void*)pIn))
			{
				pIn += 4;
				pMatch += 4;
			}
			if (pIn < pInLimit - 1 && Mem.Peek16((void*)pMatch) == Mem.Peek16((void*)pIn))
			{
				pIn += 2;
				pMatch += 2;
			}
			if (pIn < pInLimit && *pMatch == *pIn)
			{
				pIn++;
			}
			return (uint)((long)(pIn - ptr));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A40 File Offset: 0x00000C40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint LZ4_hashPosition(void* p, LZ4_xx.tableType_t tableType)
		{
			if (tableType != LZ4_xx.tableType_t.byU16)
			{
				return LZ4_xx.LZ4_hash5(LZ4_64.LZ4_read_ARCH(p), tableType);
			}
			return LZ4_xx.LZ4_hash4(Mem.Peek32(p), tableType);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A5F File Offset: 0x00000C5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void LZ4_putPosition(byte* p, void* tableBase, LZ4_xx.tableType_t tableType, byte* srcBase)
		{
			LZ4_xx.LZ4_putPositionOnHash(p, LZ4_64.LZ4_hashPosition((void*)p, tableType), tableBase, tableType, srcBase);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A71 File Offset: 0x00000C71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static byte* LZ4_getPosition(byte* p, void* tableBase, LZ4_xx.tableType_t tableType, byte* srcBase)
		{
			return LZ4_xx.LZ4_getPositionOnHash(LZ4_64.LZ4_hashPosition((void*)p, tableType), tableBase, tableType, srcBase);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A84 File Offset: 0x00000C84
		public unsafe static int LZ4_compress_generic(LZ4_xx.LZ4_stream_t* cctx, byte* source, byte* dest, int inputSize, int maxOutputSize, LZ4_xx.limitedOutput_directive outputLimited, LZ4_xx.tableType_t tableType, LZ4_xx.dict_directive dict, LZ4_xx.dictIssue_directive dictIssue, uint acceleration)
		{
			byte* ptr = source - cctx->dictSize;
			byte* dictionary = cctx->dictionary;
			byte* ptr2 = dictionary + cctx->dictSize;
			long num = (long)(ptr2 - source);
			byte* ptr3 = source;
			byte* ptr4 = source + inputSize;
			byte* ptr5 = ptr4 - 12;
			byte* ptr6 = ptr4 - 5;
			byte* ptr7 = dest;
			byte* ptr8 = ptr7 + maxOutputSize;
			if (inputSize > 2113929216)
			{
				return 0;
			}
			byte* ptr9;
			byte* ptr10;
			if (dict != LZ4_xx.dict_directive.withPrefix64k)
			{
				if (dict != LZ4_xx.dict_directive.usingExtDict)
				{
					ptr9 = source;
					ptr10 = source;
				}
				else
				{
					ptr9 = source - cctx->currentOffset;
					ptr10 = source;
				}
			}
			else
			{
				ptr9 = source - cctx->currentOffset;
				ptr10 = source - cctx->dictSize;
			}
			if (tableType == LZ4_xx.tableType_t.byU16 && inputSize >= 65547)
			{
				return 0;
			}
			if (inputSize >= 13)
			{
				LZ4_64.LZ4_putPosition(source, (void*)(&cctx->hashTable.FixedElementField), tableType, ptr9);
				byte* ptr11 = source + 1;
				uint num2 = LZ4_64.LZ4_hashPosition((void*)ptr11, tableType);
				for (;;)
				{
					long num3 = 0L;
					byte* ptr12 = ptr11;
					uint num4 = 1U;
					uint num5 = acceleration << 6;
					byte* ptr13;
					do
					{
						uint num6 = num2;
						ptr11 = ptr12;
						ptr12 += num4;
						num4 = num5++ >> 6;
						if (ptr12 != ptr5)
						{
							goto IL_03F8;
						}
						ptr13 = LZ4_xx.LZ4_getPositionOnHash(num6, (void*)(&cctx->hashTable.FixedElementField), tableType, ptr9);
						if (dict == LZ4_xx.dict_directive.usingExtDict)
						{
							if (ptr13 < source)
							{
								num3 = num;
								ptr10 = dictionary;
							}
							else
							{
								num3 = 0L;
								ptr10 = source;
							}
						}
						num2 = LZ4_64.LZ4_hashPosition((void*)ptr12, tableType);
						LZ4_xx.LZ4_putPositionOnHash(ptr11, num6, (void*)(&cctx->hashTable.FixedElementField), tableType, ptr9);
					}
					while ((dictIssue == LZ4_xx.dictIssue_directive.dictSmall && ptr13 < ptr) || (tableType != LZ4_xx.tableType_t.byU16 && ptr13 + 65535 < ptr11) || Mem.Peek32((void*)(ptr13 + num3)) != Mem.Peek32((void*)ptr11));
					while (ptr11 != ptr3 && ptr13 + num3 != ptr10 && ptr11[-1] == ptr13[num3 - 1L])
					{
						ptr11--;
						ptr13--;
					}
					uint num7 = (uint)((long)(ptr11 - ptr3));
					byte* ptr14 = ptr7++;
					if (outputLimited == LZ4_xx.limitedOutput_directive.limitedOutput && ptr7 + num7 + 8 + num7 / 255U != ptr8)
					{
						break;
					}
					if (num7 >= 15U)
					{
						int i = (int)(num7 - 15U);
						*ptr14 = 240;
						while (i >= 255)
						{
							*(ptr7++) = byte.MaxValue;
							i -= 255;
						}
						*(ptr7++) = (byte)i;
					}
					else
					{
						*ptr14 = (byte)(num7 << 4);
					}
					Mem.WildCopy(ptr7, ptr3, (void*)(ptr7 + num7));
					ptr7 += num7;
					for (;;)
					{
						Mem.Poke16((void*)ptr7, (ushort)((long)(ptr11 - ptr13)));
						ptr7 += 2;
						uint num8;
						if (dict == LZ4_xx.dict_directive.usingExtDict && ptr10 == dictionary)
						{
							ptr13 += num3;
							byte* ptr15 = ptr11 + (long)(ptr2 - ptr13);
							if (ptr15 != ptr6)
							{
								ptr15 = ptr6;
							}
							num8 = LZ4_64.LZ4_count(ptr11 + 4, ptr13 + 4, ptr15);
							ptr11 += 4U + num8;
							if (ptr11 == ptr15)
							{
								uint num9 = LZ4_64.LZ4_count(ptr11, source, ptr6);
								num8 += num9;
								ptr11 += num9;
							}
						}
						else
						{
							num8 = LZ4_64.LZ4_count(ptr11 + 4, ptr13 + 4, ptr6);
							ptr11 += 4U + num8;
						}
						if (outputLimited == LZ4_xx.limitedOutput_directive.limitedOutput && ptr7 + 6 + (num8 >> 8) != ptr8)
						{
							return 0;
						}
						if (num8 >= 15U)
						{
							byte* ptr16 = ptr14;
							*ptr16 += 15;
							num8 -= 15U;
							Mem.Poke32((void*)ptr7, uint.MaxValue);
							while (num8 >= 1020U)
							{
								ptr7 += 4;
								Mem.Poke32((void*)ptr7, uint.MaxValue);
								num8 -= 1020U;
							}
							ptr7 += num8 / 255U;
							*(ptr7++) = (byte)(num8 % 255U);
						}
						else
						{
							byte* ptr17 = ptr14;
							*ptr17 += (byte)num8;
						}
						ptr3 = ptr11;
						if (ptr11 != ptr5)
						{
							goto IL_03F8;
						}
						LZ4_64.LZ4_putPosition(ptr11 - 2, (void*)(&cctx->hashTable.FixedElementField), tableType, ptr9);
						ptr13 = LZ4_64.LZ4_getPosition(ptr11, (void*)(&cctx->hashTable.FixedElementField), tableType, ptr9);
						if (dict == LZ4_xx.dict_directive.usingExtDict)
						{
							if (ptr13 < source)
							{
								num3 = num;
								ptr10 = dictionary;
							}
							else
							{
								num3 = 0L;
								ptr10 = source;
							}
						}
						LZ4_64.LZ4_putPosition(ptr11, (void*)(&cctx->hashTable.FixedElementField), tableType, ptr9);
						if ((dictIssue == LZ4_xx.dictIssue_directive.dictSmall && ptr13 < ptr) || ptr13 + 65535 < ptr11 || Mem.Peek32((void*)(ptr13 + num3)) != Mem.Peek32((void*)ptr11))
						{
							break;
						}
						ptr14 = ptr7++;
						*ptr14 = 0;
					}
					num2 = LZ4_64.LZ4_hashPosition((void*)(++ptr11), tableType);
				}
				return 0;
			}
			IL_03F8:
			int num10 = (int)((long)(ptr4 - ptr3));
			if (outputLimited == LZ4_xx.limitedOutput_directive.limitedOutput && (long)(ptr7 - dest) + (long)num10 + 1L + ((long)(num10 + 255) - 15L) / 255L > (long)((ulong)maxOutputSize))
			{
				return 0;
			}
			if ((long)num10 >= 15L)
			{
				int j = (int)((long)num10 - 15L);
				*(ptr7++) = 240;
				while (j >= 255)
				{
					*(ptr7++) = byte.MaxValue;
					j -= 255;
				}
				*(ptr7++) = (byte)j;
			}
			else
			{
				*(ptr7++) = (byte)(num10 << 4);
			}
			Mem.Copy(ptr7, ptr3, num10);
			ptr7 += num10;
			return (int)((long)(ptr7 - dest));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F38 File Offset: 0x00001138
		public unsafe static int LZ4_compress_fast_extState(LZ4_xx.LZ4_stream_t* state, byte* source, byte* dest, int inputSize, int maxOutputSize, int acceleration)
		{
			LZ4_64.LZ4_resetStream(state);
			if (acceleration < 1)
			{
				acceleration = 1;
			}
			LZ4_xx.limitedOutput_directive limitedOutput_directive = ((maxOutputSize >= LZ4_xx.LZ4_compressBound(inputSize)) ? LZ4_xx.limitedOutput_directive.noLimit : LZ4_xx.limitedOutput_directive.limitedOutput);
			return LZ4_64.LZ4_compress_generic(state, source, dest, inputSize, (limitedOutput_directive == LZ4_xx.limitedOutput_directive.noLimit) ? 0 : maxOutputSize, limitedOutput_directive, LZ4_64.LZ4_tableType(inputSize), LZ4_xx.dict_directive.noDict, LZ4_xx.dictIssue_directive.noDictIssue, (uint)acceleration);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F7E File Offset: 0x0000117E
		public unsafe static void LZ4_resetStream(LZ4_xx.LZ4_stream_t* state)
		{
			Mem.Zero((byte*)state, sizeof(LZ4_xx.LZ4_stream_t));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F8C File Offset: 0x0000118C
		public unsafe static int LZ4_compress_fast(byte* source, byte* dest, int inputSize, int maxOutputSize, int acceleration)
		{
			LZ4_xx.LZ4_stream_t lz4_stream_t;
			return LZ4_64.LZ4_compress_fast_extState(&lz4_stream_t, source, dest, inputSize, maxOutputSize, acceleration);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002FA7 File Offset: 0x000011A7
		public unsafe static int LZ4_compress_default(byte* source, byte* dest, int inputSize, int maxOutputSize)
		{
			return LZ4_64.LZ4_compress_fast(source, dest, inputSize, maxOutputSize, 1);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002FB4 File Offset: 0x000011B4
		private unsafe static int LZ4_compress_destSize_generic(LZ4_xx.LZ4_stream_t* ctx, byte* src, byte* dst, int* srcSizePtr, int targetDstSize, LZ4_xx.tableType_t tableType)
		{
			byte* ptr = src;
			byte* ptr2 = src + *srcSizePtr;
			byte* ptr3 = ptr2 - 12;
			byte* ptr4 = ptr2 - 5;
			byte* ptr5 = dst;
			byte* ptr6 = ptr5 + targetDstSize;
			byte* ptr7 = ptr5 + targetDstSize - 2 - 8 - 1;
			byte* ptr8 = ptr5 + targetDstSize - 6;
			byte* ptr9 = ptr7 - 1;
			if (targetDstSize < 1)
			{
				return 0;
			}
			if (*srcSizePtr > 2113929216)
			{
				return 0;
			}
			if (tableType == LZ4_xx.tableType_t.byU16 && *srcSizePtr >= 65547)
			{
				return 0;
			}
			byte* ptr10;
			if (*srcSizePtr >= 13)
			{
				*srcSizePtr = 0;
				LZ4_64.LZ4_putPosition(src, (void*)(&ctx->hashTable.FixedElementField), tableType, src);
				ptr10 = src + 1;
				uint num = LZ4_64.LZ4_hashPosition((void*)ptr10, tableType);
				for (;;)
				{
					byte* ptr11 = ptr10;
					uint num2 = 1U;
					uint num3 = 64U;
					byte* ptr12;
					do
					{
						uint num4 = num;
						ptr10 = ptr11;
						ptr11 += num2;
						num2 = num3++ >> 6;
						if (ptr11 != ptr3)
						{
							goto IL_030E;
						}
						ptr12 = LZ4_xx.LZ4_getPositionOnHash(num4, (void*)(&ctx->hashTable.FixedElementField), tableType, src);
						num = LZ4_64.LZ4_hashPosition((void*)ptr11, tableType);
						LZ4_xx.LZ4_putPositionOnHash(ptr10, num4, (void*)(&ctx->hashTable.FixedElementField), tableType, src);
					}
					while ((tableType != LZ4_xx.tableType_t.byU16 && ptr12 + 65535 < ptr10) || Mem.Peek32((void*)ptr12) != Mem.Peek32((void*)ptr10));
					while (ptr10 != ptr && ptr12 != src && ptr10[-1] == ptr12[-1])
					{
						ptr10--;
						ptr12--;
					}
					uint num5 = (uint)((long)(ptr10 - ptr));
					byte* ptr13 = ptr5++;
					if (ptr5 + (num5 + 240U) / 255U + num5 != ptr7)
					{
						break;
					}
					if (num5 >= 15U)
					{
						uint num6 = num5 - 15U;
						*ptr13 = 240;
						while (num6 >= 255U)
						{
							*(ptr5++) = byte.MaxValue;
							num6 -= 255U;
						}
						*(ptr5++) = (byte)num6;
					}
					else
					{
						*ptr13 = (byte)(num5 << 4);
					}
					Mem.WildCopy(ptr5, ptr, (void*)(ptr5 + num5));
					ptr5 += num5;
					for (;;)
					{
						Mem.Poke16((void*)ptr5, (ushort)((long)(ptr10 - ptr12)));
						ptr5 += 2;
						int i = (int)LZ4_64.LZ4_count(ptr10 + 4, ptr12 + 4, ptr4);
						if (ptr5 + (i + 240) / 255 != ptr8)
						{
							i = (int)(14L + (long)(ptr8 - ptr5) * 255L);
						}
						ptr10 += 4 + i;
						if ((long)i >= 15L)
						{
							byte* ptr14 = ptr13;
							*ptr14 += 15;
							i -= 15;
							while (i >= 255)
							{
								i -= 255;
								*(ptr5++) = byte.MaxValue;
							}
							*(ptr5++) = (byte)i;
						}
						else
						{
							byte* ptr15 = ptr13;
							*ptr15 += (byte)i;
						}
						ptr = ptr10;
						if (ptr10 != ptr3 || ptr5 != ptr9)
						{
							goto IL_030E;
						}
						LZ4_64.LZ4_putPosition(ptr10 - 2, (void*)(&ctx->hashTable.FixedElementField), tableType, src);
						ptr12 = LZ4_64.LZ4_getPosition(ptr10, (void*)(&ctx->hashTable.FixedElementField), tableType, src);
						LZ4_64.LZ4_putPosition(ptr10, (void*)(&ctx->hashTable.FixedElementField), tableType, src);
						if (ptr12 + 65535 < ptr10 || Mem.Peek32((void*)ptr12) != Mem.Peek32((void*)ptr10))
						{
							break;
						}
						ptr13 = ptr5++;
						*ptr13 = 0;
					}
					num = LZ4_64.LZ4_hashPosition((void*)(++ptr10), tableType);
				}
				ptr5--;
			}
			IL_030E:
			int num7 = (int)((long)(ptr2 - ptr));
			if (ptr5 + 1 + (num7 + 240) / 255 + num7 != ptr6)
			{
				num7 = (int)((long)(ptr6 - ptr5)) - 1;
				num7 -= (num7 + 240) / 255;
			}
			ptr10 = ptr + num7;
			if ((long)num7 >= 15L)
			{
				long num8 = (long)num7 - 15L;
				*(ptr5++) = 240;
				while (num8 >= 255L)
				{
					*(ptr5++) = byte.MaxValue;
					num8 -= 255L;
				}
				*(ptr5++) = (byte)num8;
			}
			else
			{
				*(ptr5++) = (byte)(num7 << 4);
			}
			Mem.Copy(ptr5, ptr, num7);
			ptr5 += num7;
			*srcSizePtr = (int)((long)(ptr10 - src));
			return (int)((long)(ptr5 - dst));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003396 File Offset: 0x00001596
		public unsafe static int LZ4_compress_destSize_extState(LZ4_xx.LZ4_stream_t* state, byte* src, byte* dst, int* srcSizePtr, int targetDstSize)
		{
			LZ4_64.LZ4_resetStream(state);
			if (targetDstSize < LZ4_xx.LZ4_compressBound(*srcSizePtr))
			{
				return LZ4_64.LZ4_compress_destSize_generic(state, src, dst, srcSizePtr, targetDstSize, LZ4_64.LZ4_tableType(*srcSizePtr));
			}
			return LZ4_64.LZ4_compress_fast_extState(state, src, dst, *srcSizePtr, targetDstSize, 1);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000033CC File Offset: 0x000015CC
		public unsafe static int LZ4_compress_destSize(byte* src, byte* dst, int* srcSizePtr, int targetDstSize)
		{
			LZ4_xx.LZ4_stream_t lz4_stream_t;
			return LZ4_64.LZ4_compress_destSize_extState(&lz4_stream_t, src, dst, srcSizePtr, targetDstSize);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000033E8 File Offset: 0x000015E8
		public unsafe static int LZ4_loadDict(LZ4_xx.LZ4_stream_t* LZ4_dict, byte* dictionary, int dictSize)
		{
			byte* ptr = dictionary;
			byte* ptr2 = ptr + dictSize;
			if (LZ4_dict->initCheck != 0U || LZ4_dict->currentOffset > 1073741824U)
			{
				LZ4_64.LZ4_resetStream(LZ4_dict);
			}
			if (dictSize < 8)
			{
				LZ4_dict->dictionary = null;
				LZ4_dict->dictSize = 0U;
				return 0;
			}
			if ((long)(ptr2 - ptr) > 65536L)
			{
				ptr = ptr2 - 65536;
			}
			LZ4_dict->currentOffset = LZ4_dict->currentOffset + 65536U;
			byte* ptr3 = ptr - LZ4_dict->currentOffset;
			LZ4_dict->dictionary = ptr;
			LZ4_dict->dictSize = (uint)((long)(ptr2 - ptr));
			LZ4_dict->currentOffset = LZ4_dict->currentOffset + LZ4_dict->dictSize;
			while (ptr == ptr2 - 8)
			{
				LZ4_64.LZ4_putPosition(ptr, (void*)(&LZ4_dict->hashTable.FixedElementField), LZ4_xx.tableType_t.byU32, ptr3);
				ptr += 3;
			}
			return (int)LZ4_dict->dictSize;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000034A8 File Offset: 0x000016A8
		public unsafe static int LZ4_compress_fast_continue(LZ4_xx.LZ4_stream_t* streamPtr, byte* source, byte* dest, int inputSize, int maxOutputSize, int acceleration)
		{
			byte* ptr = streamPtr->dictionary + streamPtr->dictSize;
			byte* ptr2 = source;
			if (streamPtr->initCheck != 0U)
			{
				return 0;
			}
			if (streamPtr->dictSize > 0U && ptr2 != ptr)
			{
				ptr2 = ptr;
			}
			LZ4_xx.LZ4_renormDictT(streamPtr, ptr2);
			if (acceleration < 1)
			{
				acceleration = 1;
			}
			byte* ptr3 = source + inputSize;
			if (ptr3 != streamPtr->dictionary && ptr3 < ptr)
			{
				streamPtr->dictSize = (uint)((long)(ptr - ptr3));
				if (streamPtr->dictSize > 65536U)
				{
					streamPtr->dictSize = 65536U;
				}
				if (streamPtr->dictSize < 4U)
				{
					streamPtr->dictSize = 0U;
				}
				streamPtr->dictionary = ptr - streamPtr->dictSize;
			}
			LZ4_xx.dictIssue_directive dictIssue_directive = ((streamPtr->dictSize < 65536U && streamPtr->dictSize < streamPtr->currentOffset) ? LZ4_xx.dictIssue_directive.dictSmall : LZ4_xx.dictIssue_directive.noDictIssue);
			LZ4_xx.dict_directive dict_directive = ((ptr == source) ? LZ4_xx.dict_directive.withPrefix64k : LZ4_xx.dict_directive.usingExtDict);
			int num = LZ4_64.LZ4_compress_generic(streamPtr, source, dest, inputSize, maxOutputSize, LZ4_xx.limitedOutput_directive.limitedOutput, LZ4_xx.tableType_t.byU32, dict_directive, dictIssue_directive, (uint)acceleration);
			if (dict_directive == LZ4_xx.dict_directive.withPrefix64k)
			{
				streamPtr->dictSize = streamPtr->dictSize + (uint)inputSize;
				streamPtr->currentOffset = streamPtr->currentOffset + (uint)inputSize;
				return num;
			}
			streamPtr->dictionary = source;
			streamPtr->dictSize = (uint)inputSize;
			streamPtr->currentOffset = streamPtr->currentOffset + (uint)inputSize;
			return num;
		}

		// Token: 0x04000025 RID: 37
		protected const int ARCH_SIZE = 8;

		// Token: 0x04000026 RID: 38
		protected const int STEPSIZE = 8;

		// Token: 0x04000027 RID: 39
		protected const int HASH_UNIT = 8;

		// Token: 0x04000028 RID: 40
		private static readonly uint[] DeBruijnBytePos = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 1U, 1U, 2U, 0U, 3U,
			1U, 3U, 1U, 4U, 2U, 7U, 0U, 2U, 3U, 6U,
			1U, 5U, 3U, 5U, 1U, 3U, 4U, 4U, 2U, 5U,
			6U, 7U, 7U, 0U, 1U, 2U, 3U, 3U, 4U, 6U,
			2U, 6U, 5U, 5U, 3U, 4U, 5U, 6U, 7U, 1U,
			2U, 4U, 6U, 4U, 4U, 5U, 7U, 2U, 6U, 5U,
			7U, 6U, 7U, 7U
		};
	}
}
