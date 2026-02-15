using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x0200001B RID: 27
	internal static class BurstString
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003160 File Offset: 0x00001360
		private static uint LogBase2(uint val)
		{
			uint temp = val >> 24;
			if (temp != 0U)
			{
				return (uint)(24 + BurstString.logTable[(int)temp]);
			}
			temp = val >> 16;
			if (temp != 0U)
			{
				return (uint)(16 + BurstString.logTable[(int)temp]);
			}
			temp = val >> 8;
			if (temp != 0U)
			{
				return (uint)(8 + BurstString.logTable[(int)temp]);
			}
			return (uint)BurstString.logTable[(int)val];
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031AC File Offset: 0x000013AC
		private unsafe static int BigInt_Compare(in BurstString.tBigInt lhs, in BurstString.tBigInt rhs)
		{
			int lengthDiff = lhs.m_length - rhs.m_length;
			if (lengthDiff != 0)
			{
				return lengthDiff;
			}
			int i = lhs.m_length - 1;
			while (i >= 0)
			{
				if (*((ref lhs.m_blocks.FixedElementField) + (IntPtr)i * 4) != *((ref rhs.m_blocks.FixedElementField) + (IntPtr)i * 4))
				{
					if (*((ref lhs.m_blocks.FixedElementField) + (IntPtr)i * 4) > *((ref rhs.m_blocks.FixedElementField) + (IntPtr)i * 4))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i--;
				}
			}
			return 0;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000322C File Offset: 0x0000142C
		private static void BigInt_Add(out BurstString.tBigInt pResult, in BurstString.tBigInt lhs, in BurstString.tBigInt rhs)
		{
			if (lhs.m_length < rhs.m_length)
			{
				BurstString.BigInt_Add_internal(out pResult, in rhs, in lhs);
				return;
			}
			BurstString.BigInt_Add_internal(out pResult, in lhs, in rhs);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003250 File Offset: 0x00001450
		private unsafe static void BigInt_Add_internal(out BurstString.tBigInt pResult, in BurstString.tBigInt pLarge, in BurstString.tBigInt pSmall)
		{
			int largeLen = pLarge.m_length;
			int smallLen = pSmall.m_length;
			pResult.m_length = largeLen;
			ulong carry = 0UL;
			fixed (uint* ptr = &pLarge.m_blocks.FixedElementField)
			{
				uint* pLargeCur = ptr;
				fixed (uint* ptr2 = &pSmall.m_blocks.FixedElementField)
				{
					uint* pSmallCur = ptr2;
					fixed (uint* ptr3 = &pResult.m_blocks.FixedElementField)
					{
						uint* ptr4 = ptr3;
						uint* pLargeCur2 = pLargeCur;
						uint* pSmallCur2 = pSmallCur;
						uint* pResultCur = ptr4;
						uint* pLargeEnd = pLargeCur2 + largeLen;
						uint* pSmallEnd = pSmallCur2 + smallLen;
						while (pSmallCur2 != pSmallEnd)
						{
							ulong sum = carry + (ulong)(*pLargeCur2) + (ulong)(*pSmallCur2);
							carry = sum >> 32;
							*pResultCur = (uint)(sum & (ulong)(-1));
							pLargeCur2++;
							pSmallCur2++;
							pResultCur++;
						}
						while (pLargeCur2 != pLargeEnd)
						{
							ulong sum2 = carry + (ulong)(*pLargeCur2);
							carry = sum2 >> 32;
							*pResultCur = (uint)(sum2 & (ulong)(-1));
							pLargeCur2++;
							pResultCur++;
						}
						if (carry != 0UL)
						{
							*pResultCur = 1U;
							pResult.m_length = largeLen + 1;
						}
						else
						{
							pResult.m_length = largeLen;
						}
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003349 File Offset: 0x00001549
		private static void BigInt_Multiply(out BurstString.tBigInt pResult, in BurstString.tBigInt lhs, in BurstString.tBigInt rhs)
		{
			if (lhs.m_length < rhs.m_length)
			{
				BurstString.BigInt_Multiply_internal(out pResult, in rhs, in lhs);
				return;
			}
			BurstString.BigInt_Multiply_internal(out pResult, in lhs, in rhs);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000336C File Offset: 0x0000156C
		private unsafe static void BigInt_Multiply_internal(out BurstString.tBigInt pResult, in BurstString.tBigInt pLarge, in BurstString.tBigInt pSmall)
		{
			int maxResultLen = pLarge.m_length + pSmall.m_length;
			for (int i = 0; i < maxResultLen; i++)
			{
				*((ref pResult.m_blocks.FixedElementField) + (IntPtr)i * 4) = 0U;
			}
			fixed (uint* ptr = &pLarge.m_blocks.FixedElementField)
			{
				uint* pLargeBeg = ptr;
				uint* pLargeEnd = pLargeBeg + pLarge.m_length;
				fixed (uint* ptr2 = &pResult.m_blocks.FixedElementField)
				{
					uint* ptr3 = ptr2;
					fixed (uint* ptr4 = &pSmall.m_blocks.FixedElementField)
					{
						uint* pSmallCur = ptr4;
						uint* pSmallEnd = pSmallCur + pSmall.m_length;
						uint* pResultStart = ptr3;
						while (pSmallCur != pSmallEnd)
						{
							uint multiplier = *pSmallCur;
							if (multiplier != 0U)
							{
								uint* pLargeCur = pLargeBeg;
								uint* pResultCur = pResultStart;
								ulong carry = 0UL;
								do
								{
									ulong product = (ulong)(*pResultCur) + (ulong)(*pLargeCur) * (ulong)multiplier + carry;
									carry = product >> 32;
									*pResultCur = (uint)(product & (ulong)(-1));
									pLargeCur++;
									pResultCur++;
								}
								while (pLargeCur != pLargeEnd);
								*pResultCur = (uint)(carry & (ulong)(-1));
							}
							pSmallCur++;
							pResultStart++;
						}
						if (maxResultLen > 0 && *((ref pResult.m_blocks.FixedElementField) + (IntPtr)(maxResultLen - 1) * 4) == 0U)
						{
							pResult.m_length = maxResultLen - 1;
						}
						else
						{
							pResult.m_length = maxResultLen;
						}
					}
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003490 File Offset: 0x00001690
		private unsafe static void BigInt_Multiply(out BurstString.tBigInt pResult, in BurstString.tBigInt lhs, uint rhs)
		{
			uint carry = 0U;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* pResultCur = ptr;
				fixed (uint* ptr2 = &lhs.m_blocks.FixedElementField)
				{
					uint* ptr3 = ptr2;
					uint* pResultCur2 = pResultCur;
					uint* pLhsCur = ptr3;
					uint* pLhsEnd = pLhsCur + lhs.m_length;
					while (pLhsCur != pLhsEnd)
					{
						ulong product = (ulong)(*pLhsCur) * (ulong)rhs + (ulong)carry;
						*pResultCur2 = (uint)(product & (ulong)(-1));
						carry = (uint)(product >> 32);
						pLhsCur++;
						pResultCur2++;
					}
					if (carry != 0U)
					{
						*pResultCur2 = carry;
						pResult.m_length = lhs.m_length + 1;
					}
					else
					{
						pResult.m_length = lhs.m_length;
					}
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003528 File Offset: 0x00001728
		private unsafe static void BigInt_Multiply2(out BurstString.tBigInt pResult, in BurstString.tBigInt input)
		{
			uint carry = 0U;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* pResultCur = ptr;
				fixed (uint* ptr2 = &input.m_blocks.FixedElementField)
				{
					uint* ptr3 = ptr2;
					uint* pResultCur2 = pResultCur;
					uint* pLhsCur = ptr3;
					uint* pLhsEnd = pLhsCur + input.m_length;
					while (pLhsCur != pLhsEnd)
					{
						uint cur = *pLhsCur;
						*pResultCur2 = (cur << 1) | carry;
						carry = cur >> 31;
						pLhsCur++;
						pResultCur2++;
					}
					if (carry != 0U)
					{
						*pResultCur2 = carry;
						pResult.m_length = input.m_length + 1;
					}
					else
					{
						pResult.m_length = input.m_length;
					}
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000035B8 File Offset: 0x000017B8
		private unsafe static void BigInt_Multiply2(ref BurstString.tBigInt pResult)
		{
			uint carry = 0U;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* pCur = ptr;
				uint* pEnd = pCur + pResult.m_length;
				while (pCur != pEnd)
				{
					uint cur = *pCur;
					*pCur = (cur << 1) | carry;
					carry = cur >> 31;
					pCur++;
				}
				if (carry != 0U)
				{
					*pCur = carry;
					pResult.m_length++;
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003614 File Offset: 0x00001814
		private unsafe static void BigInt_Multiply10(ref BurstString.tBigInt pResult)
		{
			ulong carry = 0UL;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* pCur = ptr;
				uint* pEnd = pCur + pResult.m_length;
				while (pCur != pEnd)
				{
					ulong product = (ulong)(*pCur) * 10UL + carry;
					*pCur = (uint)(product & (ulong)(-1));
					carry = product >> 32;
					pCur++;
				}
				if (carry != 0UL)
				{
					*pCur = (uint)carry;
					pResult.m_length++;
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003678 File Offset: 0x00001878
		private unsafe static BurstString.tBigInt g_PowerOf10_Big(int i)
		{
			BurstString.tBigInt result;
			if (i == 0)
			{
				result.m_length = 1;
				result.m_blocks.FixedElementField = 100000000U;
			}
			else if (i == 1)
			{
				result.m_length = 2;
				result.m_blocks.FixedElementField = 1874919424U;
				*((ref result.m_blocks.FixedElementField) + 4) = 2328306U;
			}
			else if (i == 2)
			{
				result.m_length = 4;
				result.m_blocks.FixedElementField = 0U;
				*((ref result.m_blocks.FixedElementField) + 4) = 2242703233U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)2 * 4) = 762134875U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)3 * 4) = 1262U;
			}
			else if (i == 3)
			{
				result.m_length = 7;
				result.m_blocks.FixedElementField = 0U;
				*((ref result.m_blocks.FixedElementField) + 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)2 * 4) = 3211403009U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)3 * 4) = 1849224548U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)4 * 4) = 3668416493U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)5 * 4) = 3913284084U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)6 * 4) = 1593091U;
			}
			else if (i == 4)
			{
				result.m_length = 14;
				result.m_blocks.FixedElementField = 0U;
				*((ref result.m_blocks.FixedElementField) + 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)2 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)3 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)4 * 4) = 781532673U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)5 * 4) = 64985353U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)6 * 4) = 253049085U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)7 * 4) = 594863151U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)8 * 4) = 3553621484U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)9 * 4) = 3288652808U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)10 * 4) = 3167596762U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)11 * 4) = 2788392729U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)12 * 4) = 3911132675U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)13 * 4) = 590U;
			}
			else
			{
				result.m_length = 27;
				result.m_blocks.FixedElementField = 0U;
				*((ref result.m_blocks.FixedElementField) + 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)2 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)3 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)4 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)5 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)6 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)7 * 4) = 0U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)8 * 4) = 2553183233U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)9 * 4) = 3201533787U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)10 * 4) = 3638140786U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)11 * 4) = 303378311U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)12 * 4) = 1809731782U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)13 * 4) = 3477761648U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)14 * 4) = 3583367183U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)15 * 4) = 649228654U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)16 * 4) = 2915460784U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)17 * 4) = 487929380U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)18 * 4) = 1011012442U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)19 * 4) = 1677677582U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)20 * 4) = 3428152256U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)21 * 4) = 1710878487U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)22 * 4) = 1438394610U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)23 * 4) = 2161952759U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)24 * 4) = 4100910556U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)25 * 4) = 1608314830U;
				*((ref result.m_blocks.FixedElementField) + (IntPtr)26 * 4) = 349175U;
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B8C File Offset: 0x00001D8C
		private static void BigInt_Pow10(out BurstString.tBigInt pResult, uint exponent)
		{
			BurstString.tBigInt temp = default(BurstString.tBigInt);
			BurstString.tBigInt temp2 = default(BurstString.tBigInt);
			ref BurstString.tBigInt pCurTemp = ref temp;
			ref BurstString.tBigInt pNextTemp = ref temp2;
			uint smallExponent = exponent & 7U;
			pCurTemp.SetU32(BurstString.g_PowerOf10_U32[(int)smallExponent]);
			exponent >>= 3;
			int tableIdx = 0;
			while (exponent != 0U)
			{
				if ((exponent & 1U) != 0U)
				{
					ref BurstString.tBigInt ptr = ref pNextTemp;
					ref BurstString.tBigInt ptr2 = ref pCurTemp;
					BurstString.tBigInt tBigInt = BurstString.g_PowerOf10_Big(tableIdx);
					BurstString.BigInt_Multiply(out ptr, in ptr2, in tBigInt);
					ref BurstString.tBigInt pSwap = ref pCurTemp;
					pCurTemp = pNextTemp;
					pNextTemp = pSwap;
				}
				tableIdx++;
				exponent >>= 1;
			}
			pResult = pCurTemp;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003C1C File Offset: 0x00001E1C
		private static void BigInt_MultiplyPow10(out BurstString.tBigInt pResult, in BurstString.tBigInt input, uint exponent)
		{
			BurstString.tBigInt temp = default(BurstString.tBigInt);
			BurstString.tBigInt temp2 = default(BurstString.tBigInt);
			ref BurstString.tBigInt pCurTemp = ref temp;
			ref BurstString.tBigInt pNextTemp = ref temp2;
			uint smallExponent = exponent & 7U;
			if (smallExponent != 0U)
			{
				BurstString.BigInt_Multiply(out pCurTemp, in input, BurstString.g_PowerOf10_U32[(int)smallExponent]);
			}
			else
			{
				pCurTemp = input;
			}
			exponent >>= 3;
			int tableIdx = 0;
			while (exponent != 0U)
			{
				if ((exponent & 1U) != 0U)
				{
					ref BurstString.tBigInt ptr = ref pNextTemp;
					ref BurstString.tBigInt ptr2 = ref pCurTemp;
					BurstString.tBigInt tBigInt = BurstString.g_PowerOf10_Big(tableIdx);
					BurstString.BigInt_Multiply(out ptr, in ptr2, in tBigInt);
					ref BurstString.tBigInt pSwap = ref pCurTemp;
					pCurTemp = pNextTemp;
					pNextTemp = pSwap;
				}
				tableIdx++;
				exponent >>= 1;
			}
			pResult = pCurTemp;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003CBC File Offset: 0x00001EBC
		private unsafe static void BigInt_Pow2(out BurstString.tBigInt pResult, uint exponent)
		{
			int blockIdx = (int)(exponent / 32U);
			uint i = 0U;
			while ((ulong)i <= (ulong)((long)blockIdx))
			{
				*((ref pResult.m_blocks.FixedElementField) + (IntPtr)((ulong)i * 4UL)) = 0U;
				i += 1U;
			}
			pResult.m_length = blockIdx + 1;
			int bitIdx = (int)(exponent % 32U);
			*((ref pResult.m_blocks.FixedElementField) + (IntPtr)blockIdx * 4) |= 1U << bitIdx;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D18 File Offset: 0x00001F18
		private unsafe static uint BigInt_DivideWithRemainder_MaxQuotient9(ref BurstString.tBigInt pDividend, in BurstString.tBigInt divisor)
		{
			int length = divisor.m_length;
			if (pDividend.m_length < divisor.m_length)
			{
				return 0U;
			}
			fixed (uint* ptr = &divisor.m_blocks.FixedElementField)
			{
				uint* pDivisorCur = ptr;
				fixed (uint* ptr2 = &pDividend.m_blocks.FixedElementField)
				{
					uint* pDividendCur = ptr2;
					uint* pDivisorCur2 = pDivisorCur;
					uint* pDividendCur2 = pDividendCur;
					uint* pFinalDivisorBlock = pDivisorCur2 + length - 1;
					uint quotient = *(pDividendCur2 + length - 1) / (*pFinalDivisorBlock + 1U);
					if (quotient != 0U)
					{
						ulong borrow = 0UL;
						ulong carry = 0UL;
						do
						{
							ulong product = (ulong)(*pDivisorCur2) * (ulong)quotient + carry;
							carry = product >> 32;
							ulong difference = (ulong)(*pDividendCur2) - (product & (ulong)(-1)) - borrow;
							borrow = (difference >> 32) & 1UL;
							*pDividendCur2 = (uint)(difference & (ulong)(-1));
							pDivisorCur2++;
							pDividendCur2++;
						}
						while (pDivisorCur2 == pFinalDivisorBlock);
						while (length > 0 && *((ref pDividend.m_blocks.FixedElementField) + (IntPtr)(length - 1) * 4) == 0U)
						{
							length--;
						}
						pDividend.m_length = length;
					}
					if (BurstString.BigInt_Compare(in pDividend, in divisor) >= 0)
					{
						quotient += 1U;
						pDivisorCur2 = pDivisorCur;
						pDividendCur2 = pDividendCur;
						ulong borrow2 = 0UL;
						do
						{
							ulong difference2 = (ulong)(*pDividendCur2) - (ulong)(*pDivisorCur2) - borrow2;
							borrow2 = (difference2 >> 32) & 1UL;
							*pDividendCur2 = (uint)(difference2 & (ulong)(-1));
							pDivisorCur2++;
							pDividendCur2++;
						}
						while (pDivisorCur2 == pFinalDivisorBlock);
						while (length > 0 && *((ref pDividend.m_blocks.FixedElementField) + (IntPtr)(length - 1) * 4) == 0U)
						{
							length--;
						}
						pDividend.m_length = length;
					}
					return quotient;
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003E70 File Offset: 0x00002070
		private unsafe static void BigInt_ShiftLeft(ref BurstString.tBigInt pResult, uint shift)
		{
			int shiftBlocks = (int)(shift / 32U);
			int shiftBits = (int)(shift % 32U);
			int inLength = pResult.m_length;
			if (shiftBits == 0)
			{
				fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
				{
					uint* pInBlocks = ptr;
					uint* pInCur = pInBlocks + inLength - 1;
					uint* pOutCur = pInCur + shiftBlocks;
					while (pInCur >= pInBlocks)
					{
						*pOutCur = *pInCur;
						pInCur--;
						pOutCur--;
					}
				}
				uint i = 0U;
				while ((ulong)i < (ulong)((long)shiftBlocks))
				{
					*((ref pResult.m_blocks.FixedElementField) + (IntPtr)((ulong)i * 4UL)) = 0U;
					i += 1U;
				}
				pResult.m_length += shiftBlocks;
				return;
			}
			int inBlockIdx = inLength - 1;
			int outBlockIdx = inLength + shiftBlocks;
			pResult.m_length = outBlockIdx + 1;
			int lowBitsShift = 32 - shiftBits;
			uint highBits = 0U;
			uint block = *((ref pResult.m_blocks.FixedElementField) + (IntPtr)inBlockIdx * 4);
			uint lowBits = block >> lowBitsShift;
			while (inBlockIdx > 0)
			{
				*((ref pResult.m_blocks.FixedElementField) + (IntPtr)outBlockIdx * 4) = highBits | lowBits;
				highBits = block << shiftBits;
				inBlockIdx--;
				outBlockIdx--;
				block = *((ref pResult.m_blocks.FixedElementField) + (IntPtr)inBlockIdx * 4);
				lowBits = block >> lowBitsShift;
			}
			*((ref pResult.m_blocks.FixedElementField) + (IntPtr)outBlockIdx * 4) = highBits | lowBits;
			*((ref pResult.m_blocks.FixedElementField) + (IntPtr)(outBlockIdx - 1) * 4) = block << shiftBits;
			uint j = 0U;
			while ((ulong)j < (ulong)((long)shiftBlocks))
			{
				*((ref pResult.m_blocks.FixedElementField) + (IntPtr)((ulong)j * 4UL)) = 0U;
				j += 1U;
			}
			if (*((ref pResult.m_blocks.FixedElementField) + (IntPtr)(pResult.m_length - 1) * 4) == 0U)
			{
				pResult.m_length--;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004010 File Offset: 0x00002210
		private unsafe static uint Dragon4(ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, BurstString.CutoffMode cutoffMode, uint cutoffNumber, byte* pOutBuffer, uint bufferSize, out int pOutExponent)
		{
			byte* pCurDigit = pOutBuffer;
			if (mantissa == 0UL)
			{
				*pCurDigit = 48;
				pOutExponent = 0;
				return 1U;
			}
			BurstString.tBigInt scale = default(BurstString.tBigInt);
			BurstString.tBigInt scaledValue = default(BurstString.tBigInt);
			BurstString.tBigInt scaledMarginLow = default(BurstString.tBigInt);
			BurstString.tBigInt optionalMarginHigh = default(BurstString.tBigInt);
			BurstString.tBigInt* pScaledMarginHigh;
			if (hasUnequalMargins)
			{
				if (exponent > 0)
				{
					scaledValue.SetU64(4UL * mantissa);
					BurstString.BigInt_ShiftLeft(ref scaledValue, (uint)exponent);
					scale.SetU32(4U);
					BurstString.BigInt_Pow2(out scaledMarginLow, (uint)exponent);
					BurstString.BigInt_Pow2(out optionalMarginHigh, (uint)(exponent + 1));
				}
				else
				{
					scaledValue.SetU64(4UL * mantissa);
					BurstString.BigInt_Pow2(out scale, (uint)(-exponent + 2));
					scaledMarginLow.SetU32(1U);
					optionalMarginHigh.SetU32(2U);
				}
				pScaledMarginHigh = &optionalMarginHigh;
			}
			else
			{
				if (exponent > 0)
				{
					scaledValue.SetU64(2UL * mantissa);
					BurstString.BigInt_ShiftLeft(ref scaledValue, (uint)exponent);
					scale.SetU32(2U);
					BurstString.BigInt_Pow2(out scaledMarginLow, (uint)exponent);
				}
				else
				{
					scaledValue.SetU64(2UL * mantissa);
					BurstString.BigInt_Pow2(out scale, (uint)(-exponent + 1));
					scaledMarginLow.SetU32(1U);
				}
				pScaledMarginHigh = &scaledMarginLow;
			}
			int digitExponent = (int)Math.Ceiling((double)(mantissaHighBitIdx + (uint)exponent) * 0.3010299956639812 - 0.69);
			if (cutoffMode == BurstString.CutoffMode.FractionLength && digitExponent <= (int)(-(int)cutoffNumber))
			{
				digitExponent = (int)(-cutoffNumber + 1U);
			}
			if (digitExponent > 0)
			{
				BurstString.tBigInt temp;
				BurstString.BigInt_MultiplyPow10(out temp, in scale, (uint)digitExponent);
				scale = temp;
			}
			else if (digitExponent < 0)
			{
				BurstString.tBigInt pow10;
				BurstString.BigInt_Pow10(out pow10, (uint)(-(uint)digitExponent));
				BurstString.tBigInt temp2;
				BurstString.BigInt_Multiply(out temp2, in scaledValue, in pow10);
				scaledValue = temp2;
				BurstString.BigInt_Multiply(out temp2, in scaledMarginLow, in pow10);
				scaledMarginLow = temp2;
				if (pScaledMarginHigh != &scaledMarginLow)
				{
					BurstString.BigInt_Multiply2(out *pScaledMarginHigh, in scaledMarginLow);
				}
			}
			if (BurstString.BigInt_Compare(in scaledValue, in scale) >= 0)
			{
				digitExponent++;
			}
			else
			{
				BurstString.BigInt_Multiply10(ref scaledValue);
				BurstString.BigInt_Multiply10(ref scaledMarginLow);
				if (pScaledMarginHigh != &scaledMarginLow)
				{
					BurstString.BigInt_Multiply2(out *pScaledMarginHigh, in scaledMarginLow);
				}
			}
			int cutoffExponent = digitExponent - (int)bufferSize;
			switch (cutoffMode)
			{
			case BurstString.CutoffMode.TotalLength:
			{
				int desiredCutoffExponent = digitExponent - (int)cutoffNumber;
				if (desiredCutoffExponent > cutoffExponent)
				{
					cutoffExponent = desiredCutoffExponent;
				}
				break;
			}
			case BurstString.CutoffMode.FractionLength:
			{
				int desiredCutoffExponent2 = (int)(-(int)cutoffNumber);
				if (desiredCutoffExponent2 > cutoffExponent)
				{
					cutoffExponent = desiredCutoffExponent2;
				}
				break;
			}
			}
			pOutExponent = digitExponent - 1;
			uint hiBlock = scale.GetBlock(scale.GetLength() - 1);
			if (hiBlock < 8U || hiBlock > 429496729U)
			{
				uint hiBlockLog2 = BurstString.LogBase2(hiBlock);
				uint shift = (59U - hiBlockLog2) % 32U;
				BurstString.BigInt_ShiftLeft(ref scale, shift);
				BurstString.BigInt_ShiftLeft(ref scaledValue, shift);
				BurstString.BigInt_ShiftLeft(ref scaledMarginLow, shift);
				if (pScaledMarginHigh != &scaledMarginLow)
				{
					BurstString.BigInt_Multiply2(out *pScaledMarginHigh, in scaledMarginLow);
				}
			}
			uint outputDigit;
			bool low;
			bool high;
			if (cutoffMode == BurstString.CutoffMode.Unique)
			{
				for (;;)
				{
					digitExponent--;
					outputDigit = BurstString.BigInt_DivideWithRemainder_MaxQuotient9(ref scaledValue, in scale);
					BurstString.tBigInt scaledValueHigh;
					BurstString.BigInt_Add(out scaledValueHigh, in scaledValue, in *pScaledMarginHigh);
					low = BurstString.BigInt_Compare(in scaledValue, in scaledMarginLow) < 0;
					high = BurstString.BigInt_Compare(in scaledValueHigh, in scale) > 0;
					if ((low || high) | (digitExponent == cutoffExponent))
					{
						break;
					}
					*pCurDigit = (byte)(48U + outputDigit);
					pCurDigit++;
					BurstString.BigInt_Multiply10(ref scaledValue);
					BurstString.BigInt_Multiply10(ref scaledMarginLow);
					if (pScaledMarginHigh != &scaledMarginLow)
					{
						BurstString.BigInt_Multiply2(out *pScaledMarginHigh, in scaledMarginLow);
					}
				}
			}
			else
			{
				low = false;
				high = false;
				for (;;)
				{
					digitExponent--;
					outputDigit = BurstString.BigInt_DivideWithRemainder_MaxQuotient9(ref scaledValue, in scale);
					if (scaledValue.IsZero() | (digitExponent == cutoffExponent))
					{
						break;
					}
					*pCurDigit = (byte)(48U + outputDigit);
					pCurDigit++;
					BurstString.BigInt_Multiply10(ref scaledValue);
				}
			}
			bool roundDown = low;
			if (low == high)
			{
				BurstString.BigInt_Multiply2(ref scaledValue);
				int num = BurstString.BigInt_Compare(in scaledValue, in scale);
				roundDown = num < 0;
				if (num == 0)
				{
					roundDown = (outputDigit & 1U) == 0U;
				}
			}
			if (roundDown)
			{
				*pCurDigit = (byte)(48U + outputDigit);
				pCurDigit++;
			}
			else if (outputDigit == 9U)
			{
				while (pCurDigit != pOutBuffer)
				{
					pCurDigit--;
					if (*pCurDigit != 57)
					{
						byte* ptr = pCurDigit;
						*ptr += 1;
						pCurDigit++;
						goto IL_0368;
					}
				}
				*pCurDigit = 49;
				pCurDigit++;
				pOutExponent++;
			}
			else
			{
				*pCurDigit = (byte)(48U + outputDigit + 1U);
				pCurDigit++;
			}
			IL_0368:
			return (uint)((long)(pCurDigit - pOutBuffer));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004390 File Offset: 0x00002590
		private unsafe static int FormatPositional(byte* pOutBuffer, uint bufferSize, ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, int precision)
		{
			uint maxPrintLen = bufferSize - 1U;
			int printExponent;
			uint numPrintDigits;
			if (precision < 0)
			{
				numPrintDigits = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.Unique, 0U, pOutBuffer, maxPrintLen, out printExponent);
			}
			else
			{
				numPrintDigits = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.FractionLength, (uint)precision, pOutBuffer, maxPrintLen, out printExponent);
			}
			uint numFractionDigits = 0U;
			if (printExponent >= 0)
			{
				uint numWholeDigits = (uint)(printExponent + 1);
				if (numPrintDigits < numWholeDigits)
				{
					if (numWholeDigits > maxPrintLen)
					{
						numWholeDigits = maxPrintLen;
					}
					while (numPrintDigits < numWholeDigits)
					{
						pOutBuffer[numPrintDigits] = 48;
						numPrintDigits += 1U;
					}
				}
				else if (numPrintDigits > numWholeDigits)
				{
					numFractionDigits = numPrintDigits - numWholeDigits;
					uint maxFractionDigits = maxPrintLen - numWholeDigits - 1U;
					if (numFractionDigits > maxFractionDigits)
					{
						numFractionDigits = maxFractionDigits;
					}
					Unsafe.CopyBlock((void*)(pOutBuffer + numWholeDigits + 1), (void*)(pOutBuffer + numWholeDigits), numFractionDigits);
					pOutBuffer[numWholeDigits] = 46;
					numPrintDigits = numWholeDigits + 1U + numFractionDigits;
				}
			}
			else
			{
				if (maxPrintLen > 2U)
				{
					uint numFractionZeros = (uint)(-printExponent - 1);
					uint maxFractionZeros = maxPrintLen - 2U;
					if (numFractionZeros > maxFractionZeros)
					{
						numFractionZeros = maxFractionZeros;
					}
					uint digitsStartIdx = 2U + numFractionZeros;
					numFractionDigits = numPrintDigits;
					uint maxFractionDigits2 = maxPrintLen - digitsStartIdx;
					if (numFractionDigits > maxFractionDigits2)
					{
						numFractionDigits = maxFractionDigits2;
					}
					Unsafe.CopyBlock((void*)(pOutBuffer + digitsStartIdx), (void*)pOutBuffer, numFractionDigits);
					for (uint i = 2U; i < digitsStartIdx; i += 1U)
					{
						pOutBuffer[i] = 48;
					}
					numFractionDigits += numFractionZeros;
					numPrintDigits = numFractionDigits;
				}
				if (maxPrintLen > 1U)
				{
					pOutBuffer[1] = 46;
					numPrintDigits += 1U;
				}
				if (maxPrintLen > 0U)
				{
					*pOutBuffer = 48;
					numPrintDigits += 1U;
				}
			}
			if (precision > (int)numFractionDigits && numPrintDigits < maxPrintLen)
			{
				if (numFractionDigits == 0U)
				{
					pOutBuffer[numPrintDigits++] = 46;
				}
				uint totalDigits = (uint)((ulong)numPrintDigits + (ulong)((long)(precision - (int)numFractionDigits)));
				if (totalDigits > maxPrintLen)
				{
					totalDigits = maxPrintLen;
				}
				while (numPrintDigits < totalDigits)
				{
					pOutBuffer[numPrintDigits] = 48;
					numPrintDigits += 1U;
				}
			}
			return (int)numPrintDigits;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000044F0 File Offset: 0x000026F0
		private unsafe static int FormatScientific(byte* pOutBuffer, uint bufferSize, ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, int precision)
		{
			int printExponent;
			uint numPrintDigits;
			if (precision < 0)
			{
				numPrintDigits = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.Unique, 0U, pOutBuffer, bufferSize, out printExponent);
			}
			else
			{
				numPrintDigits = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.TotalLength, (uint)(precision + 1), pOutBuffer, bufferSize, out printExponent);
			}
			byte* pCurOut = pOutBuffer;
			if (bufferSize > 1U)
			{
				pCurOut++;
				bufferSize -= 1U;
			}
			uint numFractionDigits = numPrintDigits - 1U;
			if (numFractionDigits > 0U && bufferSize > 1U)
			{
				uint maxFractionDigits = bufferSize - 2U;
				if (numFractionDigits > maxFractionDigits)
				{
					numFractionDigits = maxFractionDigits;
				}
				Unsafe.CopyBlock((void*)(pCurOut + 1), (void*)pCurOut, numFractionDigits);
				*pCurOut = 46;
				pCurOut += 1U + numFractionDigits;
				bufferSize -= 1U + numFractionDigits;
			}
			if (precision > (int)numFractionDigits && bufferSize > 1U)
			{
				if (numFractionDigits == 0U)
				{
					*pCurOut = 46;
					pCurOut++;
					bufferSize -= 1U;
				}
				uint numZeros = (uint)((long)precision - (long)((ulong)numFractionDigits));
				if (numZeros > bufferSize - 1U)
				{
					numZeros = bufferSize - 1U;
				}
				byte* pEnd = pCurOut + numZeros;
				while (pCurOut < pEnd)
				{
					*pCurOut = 48;
					pCurOut++;
				}
			}
			if (bufferSize > 1U)
			{
				byte* exponentBuffer = stackalloc byte[(UIntPtr)5];
				*exponentBuffer = 101;
				if (printExponent >= 0)
				{
					exponentBuffer[1] = 43;
				}
				else
				{
					exponentBuffer[1] = 45;
					printExponent = -printExponent;
				}
				uint hundredsPlace = (uint)(printExponent / 100);
				uint tensPlace = (uint)(((long)printExponent - (long)((ulong)(hundredsPlace * 100U))) / 10L);
				uint onesPlace = (uint)((long)printExponent - (long)((ulong)(hundredsPlace * 100U)) - (long)((ulong)(tensPlace * 10U)));
				exponentBuffer[2] = (byte)(48U + hundredsPlace);
				exponentBuffer[3] = (byte)(48U + tensPlace);
				exponentBuffer[4] = (byte)(48U + onesPlace);
				uint maxExponentSize = bufferSize - 1U;
				uint exponentSize = ((5U < maxExponentSize) ? 5U : maxExponentSize);
				Unsafe.CopyBlock((void*)pCurOut, (void*)exponentBuffer, exponentSize);
				pCurOut += exponentSize;
				bufferSize -= exponentSize;
			}
			return (int)((long)(pCurOut - pOutBuffer));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004654 File Offset: 0x00002854
		private unsafe static void FormatInfinityNaN(byte* dest, ref int destIndex, int destLength, ulong mantissa, bool isNegative, BurstString.FormatOptions formatOptions)
		{
			int length = ((mantissa == 0UL) ? (8 + (isNegative ? 1 : 0)) : 3);
			int align = (int)formatOptions.AlignAndSize;
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, align, length))
			{
				return;
			}
			if (mantissa == 0UL)
			{
				if (isNegative)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = 45;
				}
				for (int i = 0; i < 8; i++)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = BurstString.InfinityString[i];
				}
			}
			else
			{
				for (int j = 0; j < 3; j++)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = BurstString.NanString[j];
				}
			}
			BurstString.AlignRight(dest, ref destIndex, destLength, align, length);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004700 File Offset: 0x00002900
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertFloatToString(byte* dest, ref int destIndex, int destLength, float value, BurstString.FormatOptions formatOptions)
		{
			BurstString.tFloatUnion32 floatUnion = default(BurstString.tFloatUnion32);
			floatUnion.m_floatingPoint = value;
			uint floatExponent = floatUnion.GetExponent();
			uint floatMantissa = floatUnion.GetMantissa();
			if (floatExponent == 255U)
			{
				BurstString.FormatInfinityNaN(dest, ref destIndex, destLength, (ulong)floatMantissa, floatUnion.IsNegative(), formatOptions);
				return;
			}
			uint mantissa;
			int exponent;
			uint mantissaHighBitIdx;
			bool hasUnequalMargins;
			if (floatExponent != 0U)
			{
				mantissa = (uint)(8388608UL | (ulong)floatMantissa);
				exponent = (int)(floatExponent - 127U - 23U);
				mantissaHighBitIdx = 23U;
				hasUnequalMargins = floatExponent != 1U && floatMantissa == 0U;
			}
			else
			{
				mantissa = floatMantissa;
				exponent = -149;
				mantissaHighBitIdx = BurstString.LogBase2(mantissa);
				hasUnequalMargins = false;
			}
			int precision = ((formatOptions.Specifier == 0) ? (-1) : ((int)formatOptions.Specifier));
			int bufferSize = Math.Max(10, precision + 1);
			byte* pOutBuffer = stackalloc byte[(UIntPtr)bufferSize];
			if (precision < 0)
			{
				precision = 7;
			}
			int printExponent;
			uint numPrintDigits = BurstString.Dragon4((ulong)mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.TotalLength, (uint)precision, pOutBuffer, (uint)(bufferSize - 1), out printExponent);
			pOutBuffer[numPrintDigits] = 0;
			bool isNegative = floatUnion.IsNegative();
			if (floatUnion.m_integer == 2147483648U)
			{
				isNegative = false;
			}
			BurstString.NumberBuffer number = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Float, pOutBuffer, (int)numPrintDigits, printExponent + 1, isNegative);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref number, precision, formatOptions);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004814 File Offset: 0x00002A14
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertDoubleToString(byte* dest, ref int destIndex, int destLength, double value, BurstString.FormatOptions formatOptions)
		{
			BurstString.tFloatUnion64 floatUnion = default(BurstString.tFloatUnion64);
			floatUnion.m_floatingPoint = value;
			uint floatExponent = floatUnion.GetExponent();
			ulong floatMantissa = floatUnion.GetMantissa();
			if (floatExponent == 2047U)
			{
				BurstString.FormatInfinityNaN(dest, ref destIndex, destLength, floatMantissa, floatUnion.IsNegative(), formatOptions);
				return;
			}
			ulong mantissa;
			int exponent;
			uint mantissaHighBitIdx;
			bool hasUnequalMargins;
			if (floatExponent != 0U)
			{
				mantissa = 4503599627370496UL | floatMantissa;
				exponent = (int)(floatExponent - 1023U - 52U);
				mantissaHighBitIdx = 52U;
				hasUnequalMargins = floatExponent != 1U && floatMantissa == 0UL;
			}
			else
			{
				mantissa = floatMantissa;
				exponent = -1074;
				mantissaHighBitIdx = BurstString.LogBase2((uint)mantissa);
				hasUnequalMargins = false;
			}
			int precision = ((formatOptions.Specifier == 0) ? (-1) : ((int)formatOptions.Specifier));
			int bufferSize = Math.Max(18, precision + 1);
			byte* pOutBuffer = stackalloc byte[(UIntPtr)bufferSize];
			if (precision < 0)
			{
				precision = 15;
			}
			int printExponent;
			uint numPrintDigits = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.TotalLength, (uint)precision, pOutBuffer, (uint)(bufferSize - 1), out printExponent);
			pOutBuffer[numPrintDigits] = 0;
			bool isNegative = floatUnion.IsNegative();
			if (floatUnion.m_integer == 9223372036854775808UL)
			{
				isNegative = false;
			}
			BurstString.NumberBuffer number = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Float, pOutBuffer, (int)numPrintDigits, printExponent + 1, isNegative);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref number, precision, formatOptions);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004930 File Offset: 0x00002B30
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyFixedString(byte* dest, int destLength, byte* src, int srcLength)
		{
			int finalLength = ((srcLength > destLength) ? destLength : srcLength);
			*(short*)(dest - 2) = (short)((ushort)finalLength);
			dest[finalLength] = 0;
			UnsafeUtility.MemCpy((void*)dest, (void*)src, (long)finalLength);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000495C File Offset: 0x00002B5C
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, byte* src, int srcLength, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, srcLength))
			{
				return;
			}
			int maxToCopy = destLength - destIndex;
			int toCopyLength = ((srcLength > maxToCopy) ? maxToCopy : srcLength);
			if (toCopyLength > 0)
			{
				UnsafeUtility.MemCpy((void*)(dest + destIndex), (void*)src, (long)toCopyLength);
				destIndex += toCopyLength;
				BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, srcLength);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049BC File Offset: 0x00002BBC
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, float value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertFloatToString(dest, ref destIndex, destLength, value, options);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000049DC File Offset: 0x00002BDC
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, double value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertDoubleToString(dest, ref destIndex, destLength, value, options);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000049FC File Offset: 0x00002BFC
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, bool value, int formatOptionsRaw)
		{
			int length = (value ? 4 : 5);
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, length))
			{
				return;
			}
			if (value)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 84;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 114;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 117;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 101;
			}
			else
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 70;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 97;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 108;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 115;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 101;
			}
			BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, length);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004AF8 File Offset: 0x00002CF8
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, char value, int formatOptionsRaw)
		{
			int length = ((value <= '\u007f') ? 1 : ((value <= '߿') ? 2 : 3));
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, 1))
			{
				return;
			}
			if (length == 1)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = (byte)value;
			}
			else if (length == 2)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = (byte)((value >> 6) | 'À');
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = (byte)((value & '?') | '\u0080');
			}
			else if (length == 3)
			{
				if (value >= '\ud800' && value <= '\udfff')
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = 239;
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = 191;
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = 189;
				}
				else
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = (byte)((value >> 12) | 'à');
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = (byte)(((value >> 6) & '?') | '\u0080');
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = (byte)((value & '?') | '\u0080');
				}
			}
			BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, 1);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004C63 File Offset: 0x00002E63
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, byte value, int formatOptionsRaw)
		{
			BurstString.Format(dest, ref destIndex, destLength, (ulong)value, formatOptionsRaw);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004C63 File Offset: 0x00002E63
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, ushort value, int formatOptionsRaw)
		{
			BurstString.Format(dest, ref destIndex, destLength, (ulong)value, formatOptionsRaw);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004C74 File Offset: 0x00002E74
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, uint value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)value, options);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004C98 File Offset: 0x00002E98
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, ulong value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, value, options);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004CB8 File Offset: 0x00002EB8
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, sbyte value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (options.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)((byte)value), options);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)value, options);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004CF0 File Offset: 0x00002EF0
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, short value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (options.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)((ushort)value), options);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)value, options);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004D28 File Offset: 0x00002F28
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, int value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (options.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)value, options);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)value, options);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004D60 File Offset: 0x00002F60
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, long value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (options.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)value, options);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, value, options);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004D94 File Offset: 0x00002F94
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertUnsignedIntegerToString(byte* dest, ref int destIndex, int destLength, ulong value, BurstString.FormatOptions options)
		{
			uint basis = (uint)options.GetBase();
			if (basis < 2U || basis > 36U)
			{
				return;
			}
			int length = 0;
			ulong tmp = value;
			do
			{
				tmp /= (ulong)basis;
				length++;
			}
			while (tmp != 0UL);
			int tmpIndex = length - 1;
			byte* tmpBuffer = stackalloc byte[(UIntPtr)(length + 1)];
			tmp = value;
			do
			{
				tmpBuffer[tmpIndex--] = BurstString.ValueToIntegerChar((int)(tmp % (ulong)basis), options.Uppercase);
				tmp /= (ulong)basis;
			}
			while (tmp != 0UL);
			tmpBuffer[length] = 0;
			BurstString.NumberBuffer numberBuffer = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Integer, tmpBuffer, length, length, false);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref numberBuffer, (int)options.Specifier, options);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004E1C File Offset: 0x0000301C
		private static int GetLengthIntegerToString(long value, int basis, int zeroPadding)
		{
			int length = 0;
			long tmp = value;
			do
			{
				tmp /= (long)basis;
				length++;
			}
			while (tmp != 0L);
			if (length < zeroPadding)
			{
				length = zeroPadding;
			}
			if (value < 0L)
			{
				length++;
			}
			return length;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004E4C File Offset: 0x0000304C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertIntegerToString(byte* dest, ref int destIndex, int destLength, long value, BurstString.FormatOptions options)
		{
			int basis = options.GetBase();
			if (basis < 2 || basis > 36)
			{
				return;
			}
			int length = 0;
			long tmp = value;
			do
			{
				tmp /= (long)basis;
				length++;
			}
			while (tmp != 0L);
			byte* tmpBuffer = stackalloc byte[(UIntPtr)(length + 1)];
			tmp = value;
			int tmpIndex = length - 1;
			do
			{
				tmpBuffer[tmpIndex--] = BurstString.ValueToIntegerChar((int)(tmp % (long)basis), options.Uppercase);
				tmp /= (long)basis;
			}
			while (tmp != 0L);
			tmpBuffer[length] = 0;
			BurstString.NumberBuffer numberBuffer = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Integer, tmpBuffer, length, length, value < 0L);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref numberBuffer, (int)options.Specifier, options);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004ED4 File Offset: 0x000030D4
		private unsafe static void FormatNumber(byte* dest, ref int destIndex, int destLength, ref BurstString.NumberBuffer number, int nMaxDigits, BurstString.FormatOptions options)
		{
			bool isCorrectlyRounded = number.Kind == BurstString.NumberBufferKind.Float;
			if (number.Kind == BurstString.NumberBufferKind.Integer && options.Kind == BurstString.NumberFormatKind.General && options.Specifier == 0)
			{
				options.Kind = BurstString.NumberFormatKind.Decimal;
			}
			BurstString.NumberFormatKind kind = options.Kind;
			if (kind != BurstString.NumberFormatKind.General && kind - BurstString.NumberFormatKind.Decimal <= 2)
			{
				int length = number.DigitsCount;
				int zeroPadding = (int)options.Specifier;
				int actualZeroPadding = 0;
				if (length < zeroPadding)
				{
					actualZeroPadding = zeroPadding - length;
					length = zeroPadding;
				}
				bool outputPositiveSign = options.Kind == BurstString.NumberFormatKind.DecimalForceSigned;
				length += ((number.IsNegative || outputPositiveSign) ? 1 : 0);
				if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, length))
				{
					return;
				}
				BurstString.FormatDecimalOrHexadecimal(dest, ref destIndex, destLength, ref number, actualZeroPadding, outputPositiveSign);
				BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, length);
				return;
			}
			else
			{
				if (nMaxDigits < 1)
				{
					nMaxDigits = number.DigitsCount;
				}
				BurstString.RoundNumber(ref number, nMaxDigits, isCorrectlyRounded);
				int length = BurstString.GetLengthForFormatGeneral(ref number, nMaxDigits);
				if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, length))
				{
					return;
				}
				BurstString.FormatGeneral(dest, ref destIndex, destLength, ref number, nMaxDigits, options.Uppercase ? 69 : 101);
				BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, length);
				return;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004FE8 File Offset: 0x000031E8
		private unsafe static void FormatDecimalOrHexadecimal(byte* dest, ref int destIndex, int destLength, ref BurstString.NumberBuffer number, int zeroPadding, bool outputPositiveSign)
		{
			if (number.IsNegative)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 45;
			}
			else if (outputPositiveSign)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 43;
			}
			for (int i = 0; i < zeroPadding; i++)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 48;
			}
			int digitCount = number.DigitsCount;
			byte* digits = number.GetDigitsPointer();
			for (int j = 0; j < digitCount; j++)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = digits[j];
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005081 File Offset: 0x00003281
		private static byte ValueToIntegerChar(int value, bool uppercase)
		{
			value = ((value < 0) ? (-value) : value);
			if (value <= 9)
			{
				return (byte)(48 + value);
			}
			if (value < 36)
			{
				return (byte)((uppercase ? 65 : 97) + (value - 10));
			}
			return 63;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000050B0 File Offset: 0x000032B0
		private static void OptsSplit(string fullFormat, out string padding, out string format)
		{
			string[] split = fullFormat.Split(BurstString.SplitByColon, StringSplitOptions.RemoveEmptyEntries);
			format = split[0];
			padding = null;
			if (split.Length == 2)
			{
				padding = format;
				format = split[1];
				return;
			}
			if (split.Length != 1)
			{
				throw new ArgumentException(string.Format("Format `{0}` not supported. Invalid number {1} of :. Expecting no more than one.", format, split.Length));
			}
			if (format[0] == ',')
			{
				padding = format;
				format = null;
				return;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005118 File Offset: 0x00003318
		public static BurstString.FormatOptions ParseFormatToFormatOptions(string fullFormat)
		{
			if (string.IsNullOrWhiteSpace(fullFormat))
			{
				return default(BurstString.FormatOptions);
			}
			string padding;
			string format;
			BurstString.OptsSplit(fullFormat, out padding, out format);
			format = ((format != null) ? format.Trim() : null);
			padding = ((padding != null) ? padding.Trim() : null);
			int alignAndSize = 0;
			BurstString.NumberFormatKind formatKind = BurstString.NumberFormatKind.General;
			bool lowercase = false;
			int specifier = 0;
			if (!string.IsNullOrEmpty(format))
			{
				char c = format[0];
				if (c <= 'X')
				{
					if (c == 'D')
					{
						formatKind = BurstString.NumberFormatKind.Decimal;
						goto IL_00BA;
					}
					if (c == 'G')
					{
						formatKind = BurstString.NumberFormatKind.General;
						goto IL_00BA;
					}
					if (c == 'X')
					{
						formatKind = BurstString.NumberFormatKind.Hexadecimal;
						goto IL_00BA;
					}
				}
				else
				{
					if (c == 'd')
					{
						formatKind = BurstString.NumberFormatKind.Decimal;
						lowercase = true;
						goto IL_00BA;
					}
					if (c == 'g')
					{
						formatKind = BurstString.NumberFormatKind.General;
						lowercase = true;
						goto IL_00BA;
					}
					if (c == 'x')
					{
						formatKind = BurstString.NumberFormatKind.Hexadecimal;
						lowercase = true;
						goto IL_00BA;
					}
				}
				throw new ArgumentException("Format `" + format + "` not supported. Only G, g, D, d, X, x are supported.");
				IL_00BA:
				if (format.Length > 1)
				{
					string specifierString = format.Substring(1);
					uint unsignedSpecifier;
					if (!uint.TryParse(specifierString, out unsignedSpecifier))
					{
						throw new ArgumentException(string.Concat(new string[] { "Expecting an unsigned integer for specifier `", format, "` instead of ", specifierString, "." }));
					}
					specifier = (int)unsignedSpecifier;
				}
			}
			if (!string.IsNullOrEmpty(padding))
			{
				if (padding[0] != ',')
				{
					throw new ArgumentException("Invalid padding `" + padding + "`, expecting to start with a leading `,` comma.");
				}
				string numberStr = padding.Substring(1);
				if (!int.TryParse(numberStr, out alignAndSize))
				{
					throw new ArgumentException("Expecting an integer for align/size padding `" + numberStr + "`.");
				}
			}
			return new BurstString.FormatOptions(formatKind, (sbyte)alignAndSize, (byte)specifier, lowercase);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005293 File Offset: 0x00003493
		private unsafe static bool AlignRight(byte* dest, ref int destIndex, int destLength, int align, int length)
		{
			if (align < 0)
			{
				align = -align;
				return BurstString.AlignLeft(dest, ref destIndex, destLength, align, length);
			}
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000052AC File Offset: 0x000034AC
		private unsafe static bool AlignLeft(byte* dest, ref int destIndex, int destLength, int align, int length)
		{
			if (align > 0)
			{
				while (length < align)
				{
					if (destIndex >= destLength)
					{
						return true;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = 32;
					length++;
				}
			}
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000052E0 File Offset: 0x000034E0
		private unsafe static int GetLengthForFormatGeneral(ref BurstString.NumberBuffer number, int nMaxDigits)
		{
			int length = 0;
			int digPos = number.Scale;
			bool scientific = false;
			if (digPos > nMaxDigits || digPos < -3)
			{
				digPos = 1;
				scientific = true;
			}
			byte* dig = number.GetDigitsPointer();
			if (number.IsNegative)
			{
				length++;
			}
			if (digPos > 0)
			{
				do
				{
					if (*dig != 0)
					{
						dig++;
					}
					length++;
				}
				while (--digPos > 0);
			}
			else
			{
				length++;
			}
			if (*dig != 0 || digPos < 0)
			{
				length++;
				while (digPos < 0)
				{
					length++;
					digPos++;
				}
				while (*dig != 0)
				{
					length++;
					dig++;
				}
			}
			if (scientific)
			{
				length++;
				int exponent = number.Scale - 1;
				if (exponent >= 0)
				{
					length++;
				}
				length += BurstString.GetLengthIntegerToString((long)exponent, 10, 2);
			}
			return length;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005388 File Offset: 0x00003588
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void FormatGeneral(byte* dest, ref int destIndex, int destLength, ref BurstString.NumberBuffer number, int nMaxDigits, byte expChar)
		{
			int digPos = number.Scale;
			bool scientific = false;
			if (digPos > nMaxDigits || digPos < -3)
			{
				digPos = 1;
				scientific = true;
			}
			byte* dig = number.GetDigitsPointer();
			int num;
			if (number.IsNegative)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 45;
			}
			if (digPos > 0)
			{
				while (destIndex < destLength)
				{
					num = destIndex;
					destIndex = num + 1;
					dest[num] = ((*dig != 0) ? (*(dig++)) : 48);
					if (--digPos <= 0)
					{
						goto IL_007C;
					}
				}
				return;
			}
			if (destIndex >= destLength)
			{
				return;
			}
			num = destIndex;
			destIndex = num + 1;
			dest[num] = 48;
			IL_007C:
			if (*dig != 0 || digPos < 0)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 46;
				while (digPos < 0)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = 48;
					digPos++;
				}
				while (*dig != 0)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = *(dig++);
				}
			}
			if (scientific)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = expChar;
				int exponent = number.Scale - 1;
				BurstString.FormatOptions exponentFormatOptions = new BurstString.FormatOptions(BurstString.NumberFormatKind.DecimalForceSigned, 0, 2, false);
				BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)exponent, exponentFormatOptions);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000054A4 File Offset: 0x000036A4
		private unsafe static void RoundNumber(ref BurstString.NumberBuffer number, int pos, bool isCorrectlyRounded)
		{
			byte* dig = number.GetDigitsPointer();
			int i = 0;
			while (i < pos && dig[i] != 0)
			{
				i++;
			}
			if (i == pos && BurstString.ShouldRoundUp(dig, i, isCorrectlyRounded))
			{
				while (i > 0 && dig[i - 1] == 57)
				{
					i--;
				}
				if (i > 0)
				{
					byte* ptr = dig + (i - 1);
					*ptr += 1;
				}
				else
				{
					number.Scale++;
					*dig = 49;
					i = 1;
				}
			}
			else
			{
				while (i > 0 && dig[i - 1] == 48)
				{
					i--;
				}
			}
			if (i == 0)
			{
				number.Scale = 0;
			}
			dig[i] = 0;
			number.DigitsCount = i;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005538 File Offset: 0x00003738
		private unsafe static bool ShouldRoundUp(byte* dig, int i, bool isCorrectlyRounded)
		{
			byte digit = dig[i];
			return digit != 0 && !isCorrectlyRounded && digit >= 53;
		}

		// Token: 0x040000C8 RID: 200
		private static readonly byte[] logTable = new byte[]
		{
			0, 0, 1, 1, 2, 2, 2, 2, 3, 3,
			3, 3, 3, 3, 3, 3, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7
		};

		// Token: 0x040000C9 RID: 201
		private static readonly uint[] g_PowerOf10_U32 = new uint[] { 1U, 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U };

		// Token: 0x040000CA RID: 202
		private static readonly byte[] InfinityString = new byte[] { 73, 110, 102, 105, 110, 105, 116, 121 };

		// Token: 0x040000CB RID: 203
		private static readonly byte[] NanString = new byte[] { 78, 97, 78 };

		// Token: 0x040000CC RID: 204
		private const int SinglePrecision = 9;

		// Token: 0x040000CD RID: 205
		private const int DoublePrecision = 17;

		// Token: 0x040000CE RID: 206
		internal const int SingleNumberBufferLength = 10;

		// Token: 0x040000CF RID: 207
		internal const int DoubleNumberBufferLength = 18;

		// Token: 0x040000D0 RID: 208
		private const int SinglePrecisionCustomFormat = 7;

		// Token: 0x040000D1 RID: 209
		private const int DoublePrecisionCustomFormat = 15;

		// Token: 0x040000D2 RID: 210
		private static readonly char[] SplitByColon = new char[] { ':' };

		// Token: 0x0200001C RID: 28
		public struct tBigInt
		{
			// Token: 0x060000BD RID: 189 RVA: 0x000055D5 File Offset: 0x000037D5
			public int GetLength()
			{
				return this.m_length;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x000055DD File Offset: 0x000037DD
			public unsafe uint GetBlock(int idx)
			{
				return *((ref this.m_blocks.FixedElementField) + (IntPtr)idx * 4);
			}

			// Token: 0x060000BF RID: 191 RVA: 0x000055F0 File Offset: 0x000037F0
			public void SetZero()
			{
				this.m_length = 0;
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x000055F9 File Offset: 0x000037F9
			public bool IsZero()
			{
				return this.m_length == 0;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00005604 File Offset: 0x00003804
			public unsafe void SetU64(ulong val)
			{
				if (val > (ulong)(-1))
				{
					this.m_blocks.FixedElementField = (uint)(val & (ulong)(-1));
					*((ref this.m_blocks.FixedElementField) + 4) = (uint)((val >> 32) & (ulong)(-1));
					this.m_length = 2;
					return;
				}
				if (val != 0UL)
				{
					this.m_blocks.FixedElementField = (uint)(val & (ulong)(-1));
					this.m_length = 1;
					return;
				}
				this.m_length = 0;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00005668 File Offset: 0x00003868
			public void SetU32(uint val)
			{
				if (val != 0U)
				{
					this.m_blocks.FixedElementField = val;
					this.m_length = ((val != 0U) ? 1 : 0);
					return;
				}
				this.m_length = 0;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x0000568F File Offset: 0x0000388F
			public uint GetU32()
			{
				if (this.m_length != 0)
				{
					return this.m_blocks.FixedElementField;
				}
				return 0U;
			}

			// Token: 0x040000D3 RID: 211
			private const int c_BigInt_MaxBlocks = 35;

			// Token: 0x040000D4 RID: 212
			public int m_length;

			// Token: 0x040000D5 RID: 213
			[FixedBuffer(typeof(uint), 35)]
			public BurstString.tBigInt.<m_blocks>e__FixedBuffer m_blocks;

			// Token: 0x0200001D RID: 29
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 140)]
			public struct <m_blocks>e__FixedBuffer
			{
				// Token: 0x040000D6 RID: 214
				public uint FixedElementField;
			}
		}

		// Token: 0x0200001E RID: 30
		public enum CutoffMode
		{
			// Token: 0x040000D8 RID: 216
			Unique,
			// Token: 0x040000D9 RID: 217
			TotalLength,
			// Token: 0x040000DA RID: 218
			FractionLength
		}

		// Token: 0x0200001F RID: 31
		public enum PrintFloatFormat
		{
			// Token: 0x040000DC RID: 220
			Positional,
			// Token: 0x040000DD RID: 221
			Scientific
		}

		// Token: 0x02000020 RID: 32
		[StructLayout(LayoutKind.Explicit)]
		public struct tFloatUnion32
		{
			// Token: 0x060000C4 RID: 196 RVA: 0x000056A7 File Offset: 0x000038A7
			public bool IsNegative()
			{
				return this.m_integer >> 31 > 0U;
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x000056B5 File Offset: 0x000038B5
			public uint GetExponent()
			{
				return (this.m_integer >> 23) & 255U;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x000056C6 File Offset: 0x000038C6
			public uint GetMantissa()
			{
				return this.m_integer & 8388607U;
			}

			// Token: 0x040000DE RID: 222
			[FieldOffset(0)]
			public float m_floatingPoint;

			// Token: 0x040000DF RID: 223
			[FieldOffset(0)]
			public uint m_integer;
		}

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Explicit)]
		public struct tFloatUnion64
		{
			// Token: 0x060000C7 RID: 199 RVA: 0x000056D4 File Offset: 0x000038D4
			public bool IsNegative()
			{
				return this.m_integer >> 63 > 0UL;
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x000056E3 File Offset: 0x000038E3
			public uint GetExponent()
			{
				return (uint)((this.m_integer >> 52) & 2047UL);
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x000056F6 File Offset: 0x000038F6
			public ulong GetMantissa()
			{
				return this.m_integer & 4503599627370495UL;
			}

			// Token: 0x040000E0 RID: 224
			[FieldOffset(0)]
			public double m_floatingPoint;

			// Token: 0x040000E1 RID: 225
			[FieldOffset(0)]
			public ulong m_integer;
		}

		// Token: 0x02000022 RID: 34
		internal class PreserveAttribute : Attribute
		{
		}

		// Token: 0x02000023 RID: 35
		private enum NumberBufferKind
		{
			// Token: 0x040000E3 RID: 227
			Integer,
			// Token: 0x040000E4 RID: 228
			Float
		}

		// Token: 0x02000024 RID: 36
		private struct NumberBuffer
		{
			// Token: 0x060000CB RID: 203 RVA: 0x00005708 File Offset: 0x00003908
			public unsafe NumberBuffer(BurstString.NumberBufferKind kind, byte* buffer, int digitsCount, int scale, bool isNegative)
			{
				this.Kind = kind;
				this._buffer = buffer;
				this.DigitsCount = digitsCount;
				this.Scale = scale;
				this.IsNegative = isNegative;
			}

			// Token: 0x060000CC RID: 204 RVA: 0x0000572F File Offset: 0x0000392F
			public unsafe byte* GetDigitsPointer()
			{
				return this._buffer;
			}

			// Token: 0x040000E5 RID: 229
			private unsafe readonly byte* _buffer;

			// Token: 0x040000E6 RID: 230
			public BurstString.NumberBufferKind Kind;

			// Token: 0x040000E7 RID: 231
			public int DigitsCount;

			// Token: 0x040000E8 RID: 232
			public int Scale;

			// Token: 0x040000E9 RID: 233
			public readonly bool IsNegative;
		}

		// Token: 0x02000025 RID: 37
		public enum NumberFormatKind : byte
		{
			// Token: 0x040000EB RID: 235
			General,
			// Token: 0x040000EC RID: 236
			Decimal,
			// Token: 0x040000ED RID: 237
			DecimalForceSigned,
			// Token: 0x040000EE RID: 238
			Hexadecimal
		}

		// Token: 0x02000026 RID: 38
		public struct FormatOptions
		{
			// Token: 0x060000CD RID: 205 RVA: 0x00005737 File Offset: 0x00003937
			public FormatOptions(BurstString.NumberFormatKind kind, sbyte alignAndSize, byte specifier, bool lowercase)
			{
				this = default(BurstString.FormatOptions);
				this.Kind = kind;
				this.AlignAndSize = alignAndSize;
				this.Specifier = specifier;
				this.Lowercase = lowercase;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000CE RID: 206 RVA: 0x0000575D File Offset: 0x0000395D
			public bool Uppercase
			{
				get
				{
					return !this.Lowercase;
				}
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00005768 File Offset: 0x00003968
			public unsafe int EncodeToRaw()
			{
				BurstString.FormatOptions value = this;
				return *(int*)(&value);
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00005780 File Offset: 0x00003980
			public int GetBase()
			{
				if (this.Kind == BurstString.NumberFormatKind.Hexadecimal)
				{
					return 16;
				}
				return 10;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00005790 File Offset: 0x00003990
			public override string ToString()
			{
				return string.Format("{0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}", new object[] { "Kind", this.Kind, "AlignAndSize", this.AlignAndSize, "Specifier", this.Specifier, "Uppercase", this.Uppercase });
			}

			// Token: 0x040000EF RID: 239
			public BurstString.NumberFormatKind Kind;

			// Token: 0x040000F0 RID: 240
			public sbyte AlignAndSize;

			// Token: 0x040000F1 RID: 241
			public byte Specifier;

			// Token: 0x040000F2 RID: 242
			public bool Lowercase;
		}
	}
}
