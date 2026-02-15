using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Engine
{
	// Token: 0x0200000C RID: 12
	internal class LZ4_64_HC : LZ4_64
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000035D6 File Offset: 0x000017D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static ushort DELTANEXTU16(ushort* table, ushort pos)
		{
			return table[pos];
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000035DF File Offset: 0x000017DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void DELTANEXTU16(ushort* table, ushort pos, ushort value)
		{
			table[pos] = value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000035E9 File Offset: 0x000017E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint LZ4HC_hashPtr(void* ptr)
		{
			return Mem.Peek32(ptr) * 2654435761U >> 17;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000035FC File Offset: 0x000017FC
		public unsafe static void LZ4HC_init(LZ4_64_HC.LZ4HC_CCtx_t* hc4, byte* start)
		{
			Mem.Zero((byte*)(&hc4->hashTable.FixedElementField), 131072);
			Mem.Fill((byte*)(&hc4->chainTable.FixedElementField), byte.MaxValue, 131072);
			hc4->nextToUpdate = 65536U;
			hc4->basep = start - 65536;
			hc4->end = start;
			hc4->dictBase = start - 65536;
			hc4->dictLimit = 65536U;
			hc4->lowLimit = 65536U;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000367C File Offset: 0x0000187C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void LZ4HC_insert(LZ4_64_HC.LZ4HC_CCtx_t* hc4, byte* ip)
		{
			ushort* ptr = &hc4->chainTable.FixedElementField;
			uint* ptr2 = &hc4->hashTable.FixedElementField;
			byte* basep = hc4->basep;
			uint num = (uint)((long)(ip - basep));
			for (uint num2 = hc4->nextToUpdate; num2 < num; num2 += 1U)
			{
				uint num3 = LZ4_64_HC.LZ4HC_hashPtr((void*)(basep + num2));
				uint num4 = num2 - ptr2[(ulong)num3 * 4UL / 4UL];
				if (num4 > 65535U)
				{
					num4 = 65535U;
				}
				LZ4_64_HC.DELTANEXTU16(ptr, (ushort)num2, (ushort)num4);
				ptr2[(ulong)num3 * 4UL / 4UL] = num2;
			}
			hc4->nextToUpdate = num;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003714 File Offset: 0x00001914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int LZ4HC_countBack(byte* ip, byte* match, byte* iMin, byte* mMin)
		{
			int num = 0;
			while (ip + num != iMin && match + num != mMin && ip[num - 1] == match[num - 1])
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003744 File Offset: 0x00001944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint LZ4HC_countPattern(byte* ip, byte* iEnd, uint pattern32)
		{
			byte* ptr = ip;
			ulong num = (ulong)pattern32 | ((ulong)pattern32 << 32);
			while (ip < iEnd - 7)
			{
				ulong num2 = LZ4_64.LZ4_read_ARCH((void*)ip) ^ num;
				if (num2 != 0UL)
				{
					ip += LZ4_64.LZ4_NbCommonBytes(num2);
					return (uint)((long)(ip - ptr));
				}
				ip += 8;
			}
			ulong num3 = num;
			while (ip < iEnd && *ip == (byte)num3)
			{
				ip++;
				num3 >>= 8;
			}
			return (uint)((long)(ip - ptr));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000037A8 File Offset: 0x000019A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint LZ4HC_reverseCountPattern(byte* ip, byte* iLow, uint pattern)
		{
			byte* ptr = ip;
			while (ip >= iLow + 4 && Mem.Peek32((void*)(ip - 4)) == pattern)
			{
				ip -= 4;
			}
			byte* ptr2 = (byte*)(&pattern) + 3;
			while (ip != iLow && ip[-1] == *ptr2)
			{
				ip--;
				ptr2--;
			}
			return (uint)((long)(ptr - ip));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037F4 File Offset: 0x000019F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int LZ4HC_InsertAndGetWiderMatch(LZ4_64_HC.LZ4HC_CCtx_t* hc4, byte* ip, byte* iLowLimit, byte* iHighLimit, int longest, byte** matchpos, byte** startpos, int maxNbAttempts, int patternAnalysis)
		{
			ushort* ptr = &hc4->chainTable.FixedElementField;
			IntPtr intPtr = &hc4->hashTable.FixedElementField;
			byte* basep = hc4->basep;
			uint dictLimit = hc4->dictLimit;
			byte* ptr2 = basep + dictLimit;
			uint num = ((hc4->lowLimit + 65536U > (uint)((long)(ip - basep))) ? hc4->lowLimit : ((uint)((long)(ip - basep)) - 65535U));
			byte* dictBase = hc4->dictBase;
			int num2 = (int)((long)(ip - iLowLimit));
			int num3 = maxNbAttempts;
			uint num4 = Mem.Peek32((void*)ip);
			LZ4_64_HC.repeat_state_e repeat_state_e = LZ4_64_HC.repeat_state_e.rep_untested;
			int num5 = 0;
			LZ4_64_HC.LZ4HC_insert(hc4, ip);
			uint num6 = *(intPtr + (IntPtr)((ulong)LZ4_64_HC.LZ4HC_hashPtr((void*)ip) * 4UL));
			while (num6 >= num && num3 != 0)
			{
				num3--;
				if (num6 >= dictLimit)
				{
					byte* ptr3 = basep + num6;
					if (iLowLimit[longest] == (ptr3 - num2)[longest] && Mem.Peek32((void*)ptr3) == num4)
					{
						int num7 = (int)(4U + LZ4_64.LZ4_count(ip + 4, ptr3 + 4, iHighLimit));
						int num8 = 0;
						while (ip + num8 != iLowLimit && ptr3 + num8 != ptr2 && ip[num8 - 1] == ptr3[num8 - 1])
						{
							num8--;
						}
						num7 -= num8;
						if (num7 > longest)
						{
							longest = num7;
							*(IntPtr*)matchpos = ptr3 + num8;
							*(IntPtr*)startpos = ip + num8;
						}
					}
				}
				else
				{
					byte* ptr4 = dictBase + num6;
					if (Mem.Peek32((void*)ptr4) == num4)
					{
						int num9 = 0;
						byte* ptr5 = ip + (dictLimit - num6);
						if (ptr5 != iHighLimit)
						{
							ptr5 = iHighLimit;
						}
						int num10 = (int)(4U + LZ4_64.LZ4_count(ip + 4, ptr4 + 4, ptr5));
						if (ip + num10 == ptr5 && ptr5 < iHighLimit)
						{
							num10 += (int)LZ4_64.LZ4_count(ip + num10, basep + dictLimit, iHighLimit);
						}
						while (ip + num9 != iLowLimit && (ulong)num6 + (ulong)((long)num9) > (ulong)num && ip[num9 - 1] == ptr4[num9 - 1])
						{
							num9--;
						}
						num10 -= num9;
						if (num10 > longest)
						{
							longest = num10;
							*(IntPtr*)matchpos = basep + num6 + num9;
							*(IntPtr*)startpos = ip + num9;
						}
					}
				}
				ushort num11 = LZ4_64_HC.DELTANEXTU16(ptr, (ushort)num6);
				num6 -= (uint)num11;
				if (patternAnalysis != 0 && num11 == 1)
				{
					if (repeat_state_e == LZ4_64_HC.repeat_state_e.rep_untested)
					{
						if (((num4 & 65535U) == num4 >> 16) & ((num4 & 255U) == num4 >> 24))
						{
							repeat_state_e = LZ4_64_HC.repeat_state_e.rep_confirmed;
							num5 = (int)(LZ4_64_HC.LZ4HC_countPattern(ip + 4, iHighLimit, num4) + 4U);
						}
						else
						{
							repeat_state_e = LZ4_64_HC.repeat_state_e.rep_not;
						}
					}
					if (repeat_state_e == LZ4_64_HC.repeat_state_e.rep_confirmed && num6 >= dictLimit)
					{
						byte* ptr6 = basep + num6;
						if (Mem.Peek32((void*)ptr6) == num4)
						{
							int num12 = (int)(LZ4_64_HC.LZ4HC_countPattern(ptr6 + 4, iHighLimit, num4) + 4U);
							byte* ptr7 = ((ptr2 + 65535 >= ip) ? ptr2 : (ip - 65535));
							int num13 = (int)LZ4_64_HC.LZ4HC_reverseCountPattern(ptr6, ptr7, num4);
							if (num13 + num12 >= num5 && num12 <= num5)
							{
								num6 += (uint)(num12 - num5);
							}
							else
							{
								num6 -= (uint)num13;
							}
						}
					}
				}
			}
			return longest;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003ABC File Offset: 0x00001CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int LZ4HC_InsertAndFindBestMatch(LZ4_64_HC.LZ4HC_CCtx_t* hc4, byte* ip, byte* iLimit, byte** matchpos, int maxNbAttempts, int patternAnalysis)
		{
			byte* ptr = ip;
			return LZ4_64_HC.LZ4HC_InsertAndGetWiderMatch(hc4, ip, ip, iLimit, 3, matchpos, &ptr, maxNbAttempts, patternAnalysis);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003AE0 File Offset: 0x00001CE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int LZ4HC_encodeSequence(byte** ip, byte** op, byte** anchor, int matchLength, byte* match, LZ4_xx.limitedOutput_directive limit, byte* oend)
		{
			byte* ptr = *(IntPtr*)op;
			*(IntPtr*)op = ptr + 1;
			byte* ptr2 = ptr;
			ulong num = (ulong)((long)((*(IntPtr*)ip - *(IntPtr*)anchor) / 1));
			if (limit != LZ4_xx.limitedOutput_directive.noLimit && *(IntPtr*)op + (num >> 8) + num + 8UL != oend)
			{
				return 1;
			}
			if (num >= 15UL)
			{
				ulong num2 = num - 15UL;
				*ptr2 = 240;
				while (num2 >= 255UL)
				{
					ptr = *(IntPtr*)op;
					*(IntPtr*)op = ptr + 1;
					*ptr = byte.MaxValue;
					num2 -= 255UL;
				}
				ptr = *(IntPtr*)op;
				*(IntPtr*)op = ptr + 1;
				*ptr = (byte)num2;
			}
			else
			{
				*ptr2 = (byte)(num << 4);
			}
			Mem.WildCopy(*(IntPtr*)op, *(IntPtr*)anchor, (void*)(*(IntPtr*)op + (byte*)((UIntPtr)num)));
			*(IntPtr*)op = *(IntPtr*)op + (IntPtr)((UIntPtr)num);
			Mem.Poke16(*(IntPtr*)op, (*(IntPtr*)ip - match) / 1);
			*(IntPtr*)op = *(IntPtr*)op + 2;
			num = (ulong)((long)(matchLength - 4));
			if (limit != LZ4_xx.limitedOutput_directive.noLimit && *(IntPtr*)op + (num >> 8) + 6 != oend)
			{
				return 1;
			}
			if (num >= 15UL)
			{
				byte* ptr3 = ptr2;
				*ptr3 += 15;
				for (num -= 15UL; num >= 510UL; num -= 510UL)
				{
					ptr = *(IntPtr*)op;
					*(IntPtr*)op = ptr + 1;
					*ptr = byte.MaxValue;
					ptr = *(IntPtr*)op;
					*(IntPtr*)op = ptr + 1;
					*ptr = byte.MaxValue;
				}
				if (num >= 255UL)
				{
					num -= 255UL;
					ptr = *(IntPtr*)op;
					*(IntPtr*)op = ptr + 1;
					*ptr = byte.MaxValue;
				}
				ptr = *(IntPtr*)op;
				*(IntPtr*)op = ptr + 1;
				*ptr = (byte)num;
			}
			else
			{
				byte* ptr4 = ptr2;
				*ptr4 += (byte)num;
			}
			*(IntPtr*)ip = *(IntPtr*)ip + (IntPtr)matchLength;
			*(IntPtr*)anchor = *(IntPtr*)ip;
			return 0;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C2B File Offset: 0x00001E2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LZ4HC_literalsPrice(int litlen)
		{
			if (litlen < 15)
			{
				return litlen;
			}
			return litlen + (int)(1L + ((long)litlen - 15L) / 255L);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003C48 File Offset: 0x00001E48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LZ4HC_sequencePrice(int litlen, int mlen)
		{
			int num = 3 + LZ4_64_HC.LZ4HC_literalsPrice(litlen);
			if (mlen < 19)
			{
				return num;
			}
			return num + (int)(1L + ((long)mlen - 19L) / 255L);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003C78 File Offset: 0x00001E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static LZ4_64_HC.LZ4HC_match_t LZ4HC_FindLongerMatch(LZ4_64_HC.LZ4HC_CCtx_t* ctx, byte* ip, byte* iHighLimit, int minLen, int nbSearches)
		{
			LZ4_64_HC.LZ4HC_match_t lz4HC_match_t;
			Mem.Zero((byte*)(&lz4HC_match_t), sizeof(LZ4_64_HC.LZ4HC_match_t));
			byte* ptr = null;
			int num = LZ4_64_HC.LZ4HC_InsertAndGetWiderMatch(ctx, ip, ip, iHighLimit, minLen, &ptr, &ip, nbSearches, 1);
			if (num <= minLen)
			{
				return lz4HC_match_t;
			}
			lz4HC_match_t.len = num;
			lz4HC_match_t.off = (int)((long)(ip - ptr));
			return lz4HC_match_t;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003CC8 File Offset: 0x00001EC8
		private unsafe static int LZ4HC_compress_optimal(LZ4_64_HC.LZ4HC_CCtx_t* ctx, byte* source, byte* dst, int* srcSizePtr, int dstCapacity, int nbSearches, ulong sufficient_len, LZ4_xx.limitedOutput_directive limit, int fullUpdate)
		{
			LZ4_64_HC.LZ4HC_optimal_t* ptr;
			byte* ptr2;
			byte* ptr3;
			checked
			{
				ptr = stackalloc LZ4_64_HC.LZ4HC_optimal_t[unchecked((UIntPtr)4099) * (UIntPtr)sizeof(LZ4_64_HC.LZ4HC_optimal_t)];
				ptr2 = source;
				ptr3 = ptr2;
			}
			byte* ptr4 = ptr2 + *srcSizePtr;
			byte* ptr5 = ptr4 - 12;
			byte* ptr6 = ptr4 - 5;
			byte* ptr7 = dst;
			byte* ptr8 = ptr7 + dstCapacity;
			*srcSizePtr = 0;
			if (limit == LZ4_xx.limitedOutput_directive.limitedDestSize)
			{
				ptr8 -= 5;
			}
			if (sufficient_len >= 4096UL)
			{
				sufficient_len = 4095UL;
			}
			IL_0683:
			while (ptr2 < ptr5)
			{
				int num = (int)((long)(ptr2 - ptr3));
				LZ4_64_HC.LZ4HC_match_t lz4HC_match_t = LZ4_64_HC.LZ4HC_FindLongerMatch(ctx, ptr2, ptr6, 3, nbSearches);
				if (lz4HC_match_t.len == 0)
				{
					ptr2++;
				}
				else
				{
					byte* ptr10;
					if ((long)lz4HC_match_t.len <= (long)sufficient_len)
					{
						for (int i = 0; i < 4; i++)
						{
							int num2 = LZ4_64_HC.LZ4HC_literalsPrice(num + i);
							ptr[i].mlen = 1;
							ptr[i].off = 0;
							ptr[i].litlen = num + i;
							ptr[i].price = num2;
						}
						int j = 4;
						int len = lz4HC_match_t.len;
						int off = lz4HC_match_t.off;
						while (j <= len)
						{
							int num3 = LZ4_64_HC.LZ4HC_sequencePrice(num, j);
							ptr[j].mlen = j;
							ptr[j].off = off;
							ptr[j].litlen = num;
							ptr[j].price = num3;
							j++;
						}
						int num4 = lz4HC_match_t.len;
						for (int k = 1; k <= 3; k++)
						{
							ptr[num4 + k].mlen = 1;
							ptr[num4 + k].off = 0;
							ptr[num4 + k].litlen = k;
							ptr[num4 + k].price = ptr[num4].price + LZ4_64_HC.LZ4HC_literalsPrice(k);
						}
						int l = 1;
						int num5;
						int num6;
						while (l < num4)
						{
							byte* ptr9 = ptr2 + l;
							if (ptr9 >= ptr5)
							{
								break;
							}
							if (fullUpdate != 0)
							{
								if (ptr[l + 1].price > ptr[l].price || ptr[l + 4].price >= ptr[l].price + 3)
								{
									goto IL_02C3;
								}
							}
							else if (ptr[l + 1].price > ptr[l].price)
							{
								goto IL_02C3;
							}
							IL_0572:
							l++;
							continue;
							IL_02C3:
							LZ4_64_HC.LZ4HC_match_t lz4HC_match_t2 = LZ4_64_HC.LZ4HC_FindLongerMatch(ctx, ptr9, ptr6, (fullUpdate != 0) ? 3 : (num4 - l), nbSearches);
							if (lz4HC_match_t2.len == 0)
							{
								goto IL_0572;
							}
							if ((long)lz4HC_match_t2.len > (long)sufficient_len || lz4HC_match_t2.len + l >= 4096)
							{
								num5 = lz4HC_match_t2.len;
								num6 = lz4HC_match_t2.off;
								num4 = l + 1;
								IL_05AE:
								int num7 = l;
								int num8 = num5;
								int num9 = num6;
								for (;;)
								{
									int mlen = ptr[num7].mlen;
									int off2 = ptr[num7].off;
									ptr[num7].mlen = num8;
									ptr[num7].off = num9;
									num8 = mlen;
									num9 = off2;
									if (mlen > num7)
									{
										break;
									}
									num7 -= mlen;
								}
								int m = 0;
								while (m < num4)
								{
									int mlen2 = ptr[m].mlen;
									int off3 = ptr[m].off;
									if (mlen2 == 1)
									{
										ptr2++;
										m++;
									}
									else
									{
										m += mlen2;
										ptr10 = ptr7;
										if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr2, &ptr7, &ptr3, mlen2, ptr2 - off3, limit, ptr8) != 0)
										{
											goto IL_0782;
										}
									}
								}
								goto IL_0683;
							}
							int litlen = ptr[l].litlen;
							for (int n = 1; n < 4; n++)
							{
								int num10 = ptr[l].price - LZ4_64_HC.LZ4HC_literalsPrice(litlen) + LZ4_64_HC.LZ4HC_literalsPrice(litlen + n);
								int num11 = l + n;
								if (num10 < ptr[num11].price)
								{
									ptr[num11].mlen = 1;
									ptr[num11].off = 0;
									ptr[num11].litlen = litlen + n;
									ptr[num11].price = num10;
								}
							}
							int len2 = lz4HC_match_t2.len;
							for (int num12 = 4; num12 <= len2; num12++)
							{
								int num13 = l + num12;
								int off4 = lz4HC_match_t2.off;
								int num14;
								int num15;
								if (ptr[l].mlen == 1)
								{
									num14 = ptr[l].litlen;
									num15 = ((l > num14) ? ptr[l - num14].price : 0) + LZ4_64_HC.LZ4HC_sequencePrice(num14, num12);
								}
								else
								{
									num14 = 0;
									num15 = ptr[l].price + LZ4_64_HC.LZ4HC_sequencePrice(0, num12);
								}
								if (num13 > num4 + 3 || num15 <= ptr[num13].price)
								{
									if (num12 == len2 && num4 < num13)
									{
										num4 = num13;
									}
									ptr[num13].mlen = num12;
									ptr[num13].off = off4;
									ptr[num13].litlen = num14;
									ptr[num13].price = num15;
								}
							}
							for (int num16 = 1; num16 <= 3; num16++)
							{
								ptr[num4 + num16].mlen = 1;
								ptr[num4 + num16].off = 0;
								ptr[num4 + num16].litlen = num16;
								ptr[num4 + num16].price = ptr[num4].price + LZ4_64_HC.LZ4HC_literalsPrice(num16);
							}
							goto IL_0572;
						}
						num5 = ptr[num4].mlen;
						num6 = ptr[num4].off;
						l = num4 - num5;
						goto IL_05AE;
					}
					int len3 = lz4HC_match_t.len;
					byte* ptr11 = ptr2 - lz4HC_match_t.off;
					ptr10 = ptr7;
					if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr2, &ptr7, &ptr3, len3, ptr11, limit, ptr8) == 0)
					{
						continue;
					}
					IL_0782:
					if (limit != LZ4_xx.limitedOutput_directive.limitedDestSize)
					{
						return 0;
					}
					ptr7 = ptr10;
					break;
				}
			}
			ulong num17 = (ulong)((long)(ptr4 - ptr3));
			ulong num18 = (num17 + 255UL - 15UL) / 255UL;
			ulong num19 = 1UL + num18 + num17;
			if (limit == LZ4_xx.limitedOutput_directive.limitedDestSize)
			{
				ptr8 += 5;
			}
			if (limit != LZ4_xx.limitedOutput_directive.noLimit && ptr7 + num19 != ptr8)
			{
				if (limit == LZ4_xx.limitedOutput_directive.limitedOutput)
				{
					return 0;
				}
				num17 = (ulong)((long)(ptr8 - ptr7) - 1L);
				num18 = (num17 + 255UL - 15UL) / 255UL;
				num17 -= num18;
			}
			ptr2 = ptr3 + num17;
			if (num17 >= 15UL)
			{
				ulong num20 = num17 - 15UL;
				*(ptr7++) = 240;
				while (num20 >= 255UL)
				{
					*(ptr7++) = byte.MaxValue;
					num20 -= 255UL;
				}
				*(ptr7++) = (byte)num20;
			}
			else
			{
				*(ptr7++) = (byte)(num17 << 4);
			}
			Mem.Copy(ptr7, ptr3, (int)num17);
			ptr7 += num17;
			*srcSizePtr = (int)((long)(ptr2 - source));
			return (int)((long)(ptr7 - dst));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004468 File Offset: 0x00002668
		private unsafe static int LZ4HC_compress_hashChain(LZ4_64_HC.LZ4HC_CCtx_t* ctx, byte* source, byte* dest, int* srcSizePtr, int maxOutputSize, uint maxNbAttempts, LZ4_xx.limitedOutput_directive limit)
		{
			int num = *srcSizePtr;
			int num2 = ((maxNbAttempts > 64U) ? 1 : 0);
			byte* ptr = source;
			byte* ptr2 = ptr;
			byte* ptr3 = ptr + num;
			byte* ptr4 = ptr3 - 12;
			byte* ptr5 = ptr3 - 5;
			byte* ptr6 = dest;
			byte* ptr7 = ptr6 + maxOutputSize;
			byte* ptr8 = null;
			byte* ptr9 = null;
			byte* ptr10 = null;
			byte* ptr11 = null;
			byte* ptr12 = null;
			*srcSizePtr = 0;
			if (limit == LZ4_xx.limitedOutput_directive.limitedDestSize)
			{
				ptr7 -= 5;
			}
			if (num >= 13)
			{
				while (ptr < ptr4)
				{
					int num3 = LZ4_64_HC.LZ4HC_InsertAndFindBestMatch(ctx, ptr, ptr5, &ptr8, (int)maxNbAttempts, num2);
					if (num3 < 4)
					{
						ptr++;
					}
					else
					{
						byte* ptr13 = ptr;
						byte* ptr14 = ptr8;
						int num4 = num3;
						byte* ptr15;
						for (;;)
						{
							int num5;
							if (ptr + num3 < ptr4)
							{
								num5 = LZ4_64_HC.LZ4HC_InsertAndGetWiderMatch(ctx, ptr + num3 - 2, ptr, ptr5, num3, &ptr10, &ptr9, (int)maxNbAttempts, num2);
							}
							else
							{
								num5 = num3;
							}
							if (num5 == num3)
							{
								break;
							}
							if (ptr13 < ptr && ptr9 < ptr + num4)
							{
								ptr = ptr13;
								ptr8 = ptr14;
								num3 = num4;
							}
							if ((long)(ptr9 - ptr) < 3L)
							{
								num3 = num5;
								ptr = ptr9;
								ptr8 = ptr10;
							}
							else
							{
								int num8;
								for (;;)
								{
									if ((long)(ptr9 - ptr) < 18L)
									{
										int num6 = num3;
										if (num6 > 18)
										{
											num6 = 18;
										}
										if (ptr + num6 != ptr9 + num5 - 4)
										{
											num6 = (int)((long)(ptr9 - ptr)) + num5 - 4;
										}
										int num7 = num6 - (int)((long)(ptr9 - ptr));
										if (num7 > 0)
										{
											ptr9 += num7;
											ptr10 += num7;
											num5 -= num7;
										}
									}
									if (ptr9 + num5 < ptr4)
									{
										num8 = LZ4_64_HC.LZ4HC_InsertAndGetWiderMatch(ctx, ptr9 + num5 - 3, ptr9, ptr5, num5, &ptr12, &ptr11, (int)maxNbAttempts, num2);
									}
									else
									{
										num8 = num5;
									}
									if (num8 == num5)
									{
										goto Block_16;
									}
									if (ptr11 < ptr + num3 + 3)
									{
										if (ptr11 >= ptr + num3)
										{
											break;
										}
										ptr9 = ptr11;
										ptr10 = ptr12;
										num5 = num8;
									}
									else
									{
										if (ptr9 < ptr + num3)
										{
											if ((long)(ptr9 - ptr) < 15L)
											{
												if (num3 > 18)
												{
													num3 = 18;
												}
												if (ptr + num3 != ptr9 + num5 - 4)
												{
													num3 = (int)((long)(ptr9 - ptr)) + num5 - 4;
												}
												int num9 = num3 - (int)((long)(ptr9 - ptr));
												if (num9 > 0)
												{
													ptr9 += num9;
													ptr10 += num9;
													num5 -= num9;
												}
											}
											else
											{
												num3 = (int)((long)(ptr9 - ptr));
											}
										}
										ptr15 = ptr6;
										if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr, &ptr6, &ptr2, num3, ptr8, limit, ptr7) != 0)
										{
											goto IL_043A;
										}
										ptr = ptr9;
										ptr8 = ptr10;
										num3 = num5;
										ptr9 = ptr11;
										ptr10 = ptr12;
										num5 = num8;
									}
								}
								if (ptr9 < ptr + num3)
								{
									int num10 = (int)((long)(ptr + num3 - ptr9));
									ptr9 += num10;
									ptr10 += num10;
									num5 -= num10;
									if (num5 < 4)
									{
										ptr9 = ptr11;
										ptr10 = ptr12;
										num5 = num8;
									}
								}
								ptr15 = ptr6;
								if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr, &ptr6, &ptr2, num3, ptr8, limit, ptr7) != 0)
								{
									goto IL_043A;
								}
								ptr = ptr11;
								ptr8 = ptr12;
								num3 = num8;
								ptr13 = ptr9;
								ptr14 = ptr10;
								num4 = num5;
							}
						}
						ptr15 = ptr6;
						if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr, &ptr6, &ptr2, num3, ptr8, limit, ptr7) != 0)
						{
							goto IL_043A;
						}
						continue;
						Block_16:
						if (ptr9 < ptr + num3)
						{
							num3 = (int)((long)(ptr9 - ptr));
						}
						ptr15 = ptr6;
						if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr, &ptr6, &ptr2, num3, ptr8, limit, ptr7) == 0)
						{
							ptr = ptr9;
							ptr15 = ptr6;
							int num5;
							if (LZ4_64_HC.LZ4HC_encodeSequence(&ptr, &ptr6, &ptr2, num5, ptr10, limit, ptr7) == 0)
							{
								continue;
							}
						}
						IL_043A:
						if (limit != LZ4_xx.limitedOutput_directive.limitedDestSize)
						{
							return 0;
						}
						ptr6 = ptr15;
						break;
					}
				}
			}
			ulong num11 = (ulong)((long)(ptr3 - ptr2));
			ulong num12 = (num11 + 255UL - 15UL) / 255UL;
			ulong num13 = 1UL + num12 + num11;
			if (limit == LZ4_xx.limitedOutput_directive.limitedDestSize)
			{
				ptr7 += 5;
			}
			if (limit != LZ4_xx.limitedOutput_directive.noLimit && ptr6 + num13 != ptr7)
			{
				if (limit == LZ4_xx.limitedOutput_directive.limitedOutput)
				{
					return 0;
				}
				num11 = (ulong)((long)(ptr7 - ptr6) - 1L);
				num12 = (num11 + 255UL - 15UL) / 255UL;
				num11 -= num12;
			}
			ptr = ptr2 + num11;
			if (num11 >= 15UL)
			{
				ulong num14 = num11 - 15UL;
				*(ptr6++) = 240;
				while (num14 >= 255UL)
				{
					*(ptr6++) = byte.MaxValue;
					num14 -= 255UL;
				}
				*(ptr6++) = (byte)num14;
			}
			else
			{
				*(ptr6++) = (byte)(num11 << 4);
			}
			Mem.Copy(ptr6, ptr2, (int)num11);
			ptr6 += num11;
			*srcSizePtr = (int)((long)(ptr - source));
			return (int)((long)(ptr6 - dest));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000048C0 File Offset: 0x00002AC0
		private unsafe static int LZ4HC_compress_generic(LZ4_64_HC.LZ4HC_CCtx_t* ctx, byte* src, byte* dst, int* srcSizePtr, int dstCapacity, int cLevel, LZ4_xx.limitedOutput_directive limit)
		{
			if (limit == LZ4_xx.limitedOutput_directive.limitedDestSize && dstCapacity < 1)
			{
				return 0;
			}
			if (*srcSizePtr > 2113929216)
			{
				return 0;
			}
			ctx->end = ctx->end + *srcSizePtr;
			if (cLevel < 1)
			{
				cLevel = 9;
			}
			cLevel = Math.Min(12, cLevel);
			LZ4_64_HC.cParams_t cParams_t = LZ4_64_HC.clTable[cLevel];
			if (cParams_t.strat == LZ4_64_HC.lz4hc_strat_e.lz4hc)
			{
				return LZ4_64_HC.LZ4HC_compress_hashChain(ctx, src, dst, srcSizePtr, dstCapacity, cParams_t.nbSearches, limit);
			}
			return LZ4_64_HC.LZ4HC_compress_optimal(ctx, src, dst, srcSizePtr, dstCapacity, (int)cParams_t.nbSearches, (ulong)cParams_t.targetLength, limit, (cLevel == 12) ? 1 : 0);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004951 File Offset: 0x00002B51
		private unsafe static int LZ4_compress_HC_extStateHC(LZ4_64_HC.LZ4HC_CCtx_t* ctx, byte* src, byte* dst, int srcSize, int dstCapacity, int compressionLevel)
		{
			if ((ctx & (sizeof(void*) - 1)) != null)
			{
				return 0;
			}
			LZ4_64_HC.LZ4HC_init(ctx, src);
			return LZ4_64_HC.LZ4HC_compress_generic(ctx, src, dst, &srcSize, dstCapacity, compressionLevel, (dstCapacity < LZ4_xx.LZ4_compressBound(srcSize)) ? LZ4_xx.limitedOutput_directive.limitedOutput : LZ4_xx.limitedOutput_directive.noLimit);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004987 File Offset: 0x00002B87
		private unsafe static LZ4_64_HC.LZ4HC_CCtx_t* AllocCtx()
		{
			return (LZ4_64_HC.LZ4HC_CCtx_t*)Mem.Alloc(sizeof(LZ4_64_HC.LZ4HC_CCtx_t));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004994 File Offset: 0x00002B94
		private unsafe static void FreeCtx(LZ4_64_HC.LZ4HC_CCtx_t* context)
		{
			Mem.Free((void*)context);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000499C File Offset: 0x00002B9C
		internal unsafe static int LZ4_compress_HC(byte* src, byte* dst, int srcSize, int dstCapacity, int compressionLevel)
		{
			LZ4_64_HC.LZ4HC_CCtx_t* ptr = LZ4_64_HC.AllocCtx();
			int num;
			try
			{
				num = LZ4_64_HC.LZ4_compress_HC_extStateHC(ptr, src, dst, srcSize, dstCapacity, compressionLevel);
			}
			finally
			{
				LZ4_64_HC.FreeCtx(ptr);
			}
			return num;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000049D8 File Offset: 0x00002BD8
		private unsafe static int LZ4_compress_HC_destSize(LZ4_64_HC.LZ4HC_CCtx_t* ctx, byte* source, byte* dest, int* sourceSizePtr, int targetDestSize, int cLevel)
		{
			LZ4_64_HC.LZ4HC_init(ctx, source);
			return LZ4_64_HC.LZ4HC_compress_generic(ctx, source, dest, sourceSizePtr, targetDestSize, cLevel, LZ4_xx.limitedOutput_directive.limitedDestSize);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000049EF File Offset: 0x00002BEF
		public unsafe static void LZ4_resetStreamHC(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, int compressionLevel)
		{
			ctxPtr->basep = null;
			LZ4_64_HC.LZ4_setCompressionLevel(ctxPtr, compressionLevel);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004A00 File Offset: 0x00002C00
		public unsafe static void LZ4_setCompressionLevel(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, int compressionLevel)
		{
			if (compressionLevel < 1)
			{
				compressionLevel = 1;
			}
			if (compressionLevel > 12)
			{
				compressionLevel = 12;
			}
			ctxPtr->compressionLevel = compressionLevel;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004A19 File Offset: 0x00002C19
		private unsafe static int LZ4_loadDictHC(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, byte* dictionary, int dictSize)
		{
			if (dictSize > 65536)
			{
				dictionary += dictSize - 65536;
				dictSize = 65536;
			}
			LZ4_64_HC.LZ4HC_init(ctxPtr, dictionary);
			ctxPtr->end = dictionary + dictSize;
			if (dictSize >= 4)
			{
				LZ4_64_HC.LZ4HC_insert(ctxPtr, ctxPtr->end - 3);
			}
			return dictSize;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004A58 File Offset: 0x00002C58
		private unsafe static void LZ4HC_setExternalDict(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, byte* newBlock)
		{
			if (ctxPtr->end >= ctxPtr->basep + 4)
			{
				LZ4_64_HC.LZ4HC_insert(ctxPtr, ctxPtr->end - 3);
			}
			ctxPtr->lowLimit = ctxPtr->dictLimit;
			ctxPtr->dictLimit = (uint)((long)(ctxPtr->end - ctxPtr->basep));
			ctxPtr->dictBase = ctxPtr->basep;
			ctxPtr->basep = newBlock - ctxPtr->dictLimit;
			ctxPtr->end = newBlock;
			ctxPtr->nextToUpdate = ctxPtr->dictLimit;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004AD4 File Offset: 0x00002CD4
		private unsafe static int LZ4_compressHC_continue_generic(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, byte* src, byte* dst, int* srcSizePtr, int dstCapacity, LZ4_xx.limitedOutput_directive limit)
		{
			if (ctxPtr->basep == null)
			{
				LZ4_64_HC.LZ4HC_init(ctxPtr, src);
			}
			if ((long)(ctxPtr->end - ctxPtr->basep) > (long)((ulong)(-2147483648)))
			{
				ulong num = (ulong)((long)(ctxPtr->end - ctxPtr->basep) - (long)((ulong)ctxPtr->dictLimit));
				if (num > 65536UL)
				{
					num = 65536UL;
				}
				LZ4_64_HC.LZ4_loadDictHC(ctxPtr, ctxPtr->end - num, (int)num);
			}
			if (src != ctxPtr->end)
			{
				LZ4_64_HC.LZ4HC_setExternalDict(ctxPtr, src);
			}
			byte* ptr = src + *srcSizePtr;
			byte* ptr2 = ctxPtr->dictBase + ctxPtr->lowLimit;
			byte* ptr3 = ctxPtr->dictBase + ctxPtr->dictLimit;
			if (ptr != ptr2 && src < ptr3)
			{
				if (ptr != ptr3)
				{
					ptr = ptr3;
				}
				ctxPtr->lowLimit = (uint)((long)(ptr - ctxPtr->dictBase));
				if (ctxPtr->dictLimit - ctxPtr->lowLimit < 4U)
				{
					ctxPtr->lowLimit = ctxPtr->dictLimit;
				}
			}
			return LZ4_64_HC.LZ4HC_compress_generic(ctxPtr, src, dst, srcSizePtr, dstCapacity, ctxPtr->compressionLevel, limit);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public unsafe static int LZ4_compress_HC_continue(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, byte* src, byte* dst, int srcSize, int dstCapacity)
		{
			return LZ4_64_HC.LZ4_compressHC_continue_generic(ctxPtr, src, dst, &srcSize, dstCapacity, (dstCapacity < LZ4_xx.LZ4_compressBound(srcSize)) ? LZ4_xx.limitedOutput_directive.limitedOutput : LZ4_xx.limitedOutput_directive.noLimit);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004BE4 File Offset: 0x00002DE4
		private unsafe static int LZ4_compress_HC_continue_destSize(LZ4_64_HC.LZ4HC_CCtx_t* ctxPtr, byte* src, byte* dst, int* srcSizePtr, int targetDestSize)
		{
			return LZ4_64_HC.LZ4_compressHC_continue_generic(ctxPtr, src, dst, srcSizePtr, targetDestSize, LZ4_xx.limitedOutput_directive.limitedDestSize);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public unsafe static int LZ4_saveDictHC(LZ4_64_HC.LZ4HC_CCtx_t* LZ4_streamHCPtr, byte* safeBuffer, int dictSize)
		{
			int num = (int)((long)(LZ4_streamHCPtr->end - (LZ4_streamHCPtr->basep + LZ4_streamHCPtr->dictLimit)));
			if (dictSize > 65536)
			{
				dictSize = 65536;
			}
			if (dictSize < 4)
			{
				dictSize = 0;
			}
			if (dictSize > num)
			{
				dictSize = num;
			}
			Mem.Move(safeBuffer, LZ4_streamHCPtr->end - dictSize, dictSize);
			uint num2 = (uint)((long)(LZ4_streamHCPtr->end - LZ4_streamHCPtr->basep));
			LZ4_streamHCPtr->end = safeBuffer + dictSize;
			LZ4_streamHCPtr->basep = LZ4_streamHCPtr->end - num2;
			LZ4_streamHCPtr->dictLimit = num2 - (uint)dictSize;
			LZ4_streamHCPtr->lowLimit = num2 - (uint)dictSize;
			if (LZ4_streamHCPtr->nextToUpdate < LZ4_streamHCPtr->dictLimit)
			{
				LZ4_streamHCPtr->nextToUpdate = LZ4_streamHCPtr->dictLimit;
			}
			return dictSize;
		}

		// Token: 0x04000029 RID: 41
		private const int LZ4HC_CLEVEL_MIN = 3;

		// Token: 0x0400002A RID: 42
		private const int LZ4HC_CLEVEL_DEFAULT = 9;

		// Token: 0x0400002B RID: 43
		private const int LZ4HC_CLEVEL_OPT_MIN = 10;

		// Token: 0x0400002C RID: 44
		private const int LZ4HC_CLEVEL_MAX = 12;

		// Token: 0x0400002D RID: 45
		private const int LZ4HC_DICTIONARY_LOGSIZE = 16;

		// Token: 0x0400002E RID: 46
		private const int LZ4HC_MAXD = 65536;

		// Token: 0x0400002F RID: 47
		private const int LZ4HC_MAXD_MASK = 65535;

		// Token: 0x04000030 RID: 48
		private const int LZ4HC_HASH_LOG = 15;

		// Token: 0x04000031 RID: 49
		private const int LZ4HC_HASHTABLESIZE = 32768;

		// Token: 0x04000032 RID: 50
		private const int LZ4HC_HASH_MASK = 32767;

		// Token: 0x04000033 RID: 51
		private const int OPTIMAL_ML = 18;

		// Token: 0x04000034 RID: 52
		private const int LZ4_OPT_NUM = 4096;

		// Token: 0x04000035 RID: 53
		private static LZ4_64_HC.cParams_t[] clTable = new LZ4_64_HC.cParams_t[]
		{
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 2U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 2U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 2U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 4U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 8U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 16U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 32U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 64U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 128U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4hc, 256U, 16U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4opt, 96U, 64U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4opt, 512U, 128U),
			new LZ4_64_HC.cParams_t(LZ4_64_HC.lz4hc_strat_e.lz4opt, 8192U, 4096U)
		};

		// Token: 0x0200000D RID: 13
		public struct LZ4HC_CCtx_t
		{
			// Token: 0x04000036 RID: 54
			[FixedBuffer(typeof(uint), 32768)]
			public LZ4_64_HC.LZ4HC_CCtx_t.<hashTable>e__FixedBuffer hashTable;

			// Token: 0x04000037 RID: 55
			[FixedBuffer(typeof(ushort), 65536)]
			public LZ4_64_HC.LZ4HC_CCtx_t.<chainTable>e__FixedBuffer chainTable;

			// Token: 0x04000038 RID: 56
			public unsafe byte* end;

			// Token: 0x04000039 RID: 57
			public unsafe byte* basep;

			// Token: 0x0400003A RID: 58
			public unsafe byte* dictBase;

			// Token: 0x0400003B RID: 59
			public unsafe byte* inputBuffer;

			// Token: 0x0400003C RID: 60
			public uint dictLimit;

			// Token: 0x0400003D RID: 61
			public uint lowLimit;

			// Token: 0x0400003E RID: 62
			public uint nextToUpdate;

			// Token: 0x0400003F RID: 63
			public int compressionLevel;

			// Token: 0x0200000E RID: 14
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 131072)]
			public struct <hashTable>e__FixedBuffer
			{
				// Token: 0x04000040 RID: 64
				public uint FixedElementField;
			}

			// Token: 0x0200000F RID: 15
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 131072)]
			public struct <chainTable>e__FixedBuffer
			{
				// Token: 0x04000041 RID: 65
				public ushort FixedElementField;
			}
		}

		// Token: 0x02000010 RID: 16
		private enum repeat_state_e
		{
			// Token: 0x04000043 RID: 67
			rep_untested,
			// Token: 0x04000044 RID: 68
			rep_not,
			// Token: 0x04000045 RID: 69
			rep_confirmed
		}

		// Token: 0x02000011 RID: 17
		private struct LZ4HC_optimal_t
		{
			// Token: 0x04000046 RID: 70
			public int price;

			// Token: 0x04000047 RID: 71
			public int off;

			// Token: 0x04000048 RID: 72
			public int mlen;

			// Token: 0x04000049 RID: 73
			public int litlen;
		}

		// Token: 0x02000012 RID: 18
		private struct LZ4HC_match_t
		{
			// Token: 0x0400004A RID: 74
			public int off;

			// Token: 0x0400004B RID: 75
			public int len;
		}

		// Token: 0x02000013 RID: 19
		private enum lz4hc_strat_e
		{
			// Token: 0x0400004D RID: 77
			lz4hc,
			// Token: 0x0400004E RID: 78
			lz4opt
		}

		// Token: 0x02000014 RID: 20
		private struct cParams_t
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00004DAF File Offset: 0x00002FAF
			public cParams_t(LZ4_64_HC.lz4hc_strat_e strat, uint nbSearches, uint targetLength)
			{
				this.strat = strat;
				this.nbSearches = nbSearches;
				this.targetLength = targetLength;
			}

			// Token: 0x0400004F RID: 79
			public readonly LZ4_64_HC.lz4hc_strat_e strat;

			// Token: 0x04000050 RID: 80
			public readonly uint nbSearches;

			// Token: 0x04000051 RID: 81
			public readonly uint targetLength;
		}
	}
}
