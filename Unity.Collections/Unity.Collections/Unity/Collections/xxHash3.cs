using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000F6 RID: 246
	[GenerateTestsForBurstCompatibility]
	[GenerateTestsForBurstCompatibility]
	[BurstCompile]
	[GenerateTestsForBurstCompatibility]
	public static class xxHash3
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		internal unsafe static void Avx2HashLongInternalLoop(ulong* acc, byte* input, byte* dest, long length, byte* secret, int isHash64)
		{
			if (X86.Avx2.IsAvx2Supported)
			{
				long nb_blocks = (length - 1L) / 1024L;
				int i = 0;
				while ((long)i < nb_blocks)
				{
					xxHash3.Avx2Accumulate(acc, input + i * 1024, (dest == null) ? null : (dest + i * 1024), secret, 16L, isHash64);
					xxHash3.Avx2ScrambleAcc(acc, secret + 192 - 64);
					i++;
				}
				long nbStripes = (length - 1L - 1024L * nb_blocks) / 64L;
				xxHash3.Avx2Accumulate(acc, input + nb_blocks * 1024L, (dest == null) ? null : (dest + nb_blocks * 1024L), secret, nbStripes, isHash64);
				byte* p = input + length - 64;
				xxHash3.Avx2Accumulate512(acc, p, null, secret + 192 - 64 - 7);
				if (dest != null)
				{
					long remaining = length % 64L;
					if (remaining != 0L)
					{
						UnsafeUtility.MemCpy((void*)(dest + length - remaining), (void*)(input + length - remaining), remaining);
					}
				}
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001F98C File Offset: 0x0001DB8C
		internal unsafe static void Avx2ScrambleAcc(ulong* acc, byte* secret)
		{
			if (X86.Avx2.IsAvx2Supported)
			{
				v256 prime32 = X86.Avx.mm256_set1_epi32(-1640531535);
				v256 v = *(v256*)acc;
				v256 shifted = X86.Avx2.mm256_srli_epi64(v, 47);
				v256 v2 = X86.Avx2.mm256_xor_si256(v, shifted);
				v256 key_vec = X86.Avx.mm256_loadu_si256((void*)secret);
				v256 v3 = X86.Avx2.mm256_xor_si256(v2, key_vec);
				v256 data_key_hi = X86.Avx2.mm256_shuffle_epi32(v3, X86.Sse.SHUFFLE(0, 3, 0, 1));
				v256 prod_lo = X86.Avx2.mm256_mul_epu32(v3, prime32);
				v256 prod_hi = X86.Avx2.mm256_mul_epu32(data_key_hi, prime32);
				*(v256*)acc = X86.Avx2.mm256_add_epi64(prod_lo, X86.Avx2.mm256_slli_epi64(prod_hi, 32));
				v256 v4 = *(v256*)(acc + sizeof(v256) / 8);
				shifted = X86.Avx2.mm256_srli_epi64(v4, 47);
				v256 v5 = X86.Avx2.mm256_xor_si256(v4, shifted);
				key_vec = X86.Avx.mm256_loadu_si256((void*)(secret + sizeof(v256)));
				v256 v6 = X86.Avx2.mm256_xor_si256(v5, key_vec);
				data_key_hi = X86.Avx2.mm256_shuffle_epi32(v6, X86.Sse.SHUFFLE(0, 3, 0, 1));
				prod_lo = X86.Avx2.mm256_mul_epu32(v6, prime32);
				prod_hi = X86.Avx2.mm256_mul_epu32(data_key_hi, prime32);
				*(v256*)(acc + sizeof(v256) / 8) = X86.Avx2.mm256_add_epi64(prod_lo, X86.Avx2.mm256_slli_epi64(prod_hi, 32));
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001FA84 File Offset: 0x0001DC84
		internal unsafe static void Avx2Accumulate(ulong* acc, byte* input, byte* dest, byte* secret, long nbStripes, int isHash64)
		{
			if (X86.Avx2.IsAvx2Supported)
			{
				int i = 0;
				while ((long)i < nbStripes)
				{
					byte* xInput = input + i * 64;
					xxHash3.Avx2Accumulate512(acc, xInput, (dest == null) ? null : (dest + i * 64), secret + i * 8);
					i++;
				}
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		internal unsafe static void Avx2Accumulate512(ulong* acc, byte* input, byte* dest, byte* secret)
		{
			if (X86.Avx2.IsAvx2Supported)
			{
				v256 data_vec = X86.Avx.mm256_loadu_si256((void*)input);
				v256 key_vec = X86.Avx.mm256_loadu_si256((void*)secret);
				v256 v = X86.Avx2.mm256_xor_si256(data_vec, key_vec);
				if (dest != null)
				{
					X86.Avx.mm256_storeu_si256((void*)dest, data_vec);
				}
				v256 data_key_lo = X86.Avx2.mm256_shuffle_epi32(v, X86.Sse.SHUFFLE(0, 3, 0, 1));
				v256 product = X86.Avx2.mm256_mul_epu32(v, data_key_lo);
				v256 data_swap = X86.Avx2.mm256_shuffle_epi32(data_vec, X86.Sse.SHUFFLE(1, 0, 3, 2));
				v256 sum = X86.Avx2.mm256_add_epi64(*(v256*)acc, data_swap);
				*(v256*)acc = X86.Avx2.mm256_add_epi64(product, sum);
				data_vec = X86.Avx.mm256_loadu_si256((void*)(input + sizeof(v256)));
				key_vec = X86.Avx.mm256_loadu_si256((void*)(secret + sizeof(v256)));
				v256 v2 = X86.Avx2.mm256_xor_si256(data_vec, key_vec);
				if (dest != null)
				{
					X86.Avx.mm256_storeu_si256((void*)(dest + 32), data_vec);
				}
				data_key_lo = X86.Avx2.mm256_shuffle_epi32(v2, X86.Sse.SHUFFLE(0, 3, 0, 1));
				product = X86.Avx2.mm256_mul_epu32(v2, data_key_lo);
				data_swap = X86.Avx2.mm256_shuffle_epi32(data_vec, X86.Sse.SHUFFLE(1, 0, 3, 2));
				sum = X86.Avx2.mm256_add_epi64(*(v256*)(acc + sizeof(v256) / 8), data_swap);
				*(v256*)(acc + sizeof(v256) / 8) = X86.Avx2.mm256_add_epi64(product, sum);
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001FBDC File Offset: 0x0001DDDC
		public unsafe static uint2 Hash64(void* input, long length)
		{
			byte[] kSecret;
			void* secret;
			if ((kSecret = xxHashDefaultKey.kSecret) == null || kSecret.Length == 0)
			{
				secret = null;
			}
			else
			{
				secret = (void*)(&kSecret[0]);
			}
			return xxHash3.ToUint2(xxHash3.Hash64Internal((byte*)input, null, length, (byte*)secret, 0UL));
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001FC16 File Offset: 0x0001DE16
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static uint2 Hash64<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in T input) where T : struct, ValueType
		{
			return xxHash3.Hash64(UnsafeUtilityExtensions.AddressOf<T>(in input), (long)UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		public unsafe static uint2 Hash64(void* input, long length, ulong seed)
		{
			byte[] kSecret;
			byte* secret;
			if ((kSecret = xxHashDefaultKey.kSecret) == null || kSecret.Length == 0)
			{
				secret = null;
			}
			else
			{
				secret = &kSecret[0];
			}
			return xxHash3.ToUint2(xxHash3.Hash64Internal((byte*)input, null, length, secret, seed));
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001FC68 File Offset: 0x0001DE68
		public unsafe static uint4 Hash128(void* input, long length)
		{
			byte[] kSecret;
			void* secret;
			if ((kSecret = xxHashDefaultKey.kSecret) == null || kSecret.Length == 0)
			{
				secret = null;
			}
			else
			{
				secret = (void*)(&kSecret[0]);
			}
			uint4 result;
			xxHash3.Hash128Internal((byte*)input, null, length, (byte*)secret, 0UL, out result);
			return result;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static uint4 Hash128<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in T input) where T : struct, ValueType
		{
			return xxHash3.Hash128(UnsafeUtilityExtensions.AddressOf<T>(in input), (long)UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public unsafe static uint4 Hash128(void* input, void* destination, long length)
		{
			byte[] kSecret;
			byte* secret;
			if ((kSecret = xxHashDefaultKey.kSecret) == null || kSecret.Length == 0)
			{
				secret = null;
			}
			else
			{
				secret = &kSecret[0];
			}
			uint4 result;
			xxHash3.Hash128Internal((byte*)input, (byte*)destination, length, secret, 0UL, out result);
			return result;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001FCEC File Offset: 0x0001DEEC
		public unsafe static uint4 Hash128(void* input, long length, ulong seed)
		{
			byte[] kSecret;
			byte* secret;
			if ((kSecret = xxHashDefaultKey.kSecret) == null || kSecret.Length == 0)
			{
				secret = null;
			}
			else
			{
				secret = &kSecret[0];
			}
			uint4 result;
			xxHash3.Hash128Internal((byte*)input, null, length, secret, seed, out result);
			return result;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001FD24 File Offset: 0x0001DF24
		public unsafe static uint4 Hash128(void* input, void* destination, long length, ulong seed)
		{
			byte[] kSecret;
			byte* secret;
			if ((kSecret = xxHashDefaultKey.kSecret) == null || kSecret.Length == 0)
			{
				secret = null;
			}
			else
			{
				secret = &kSecret[0];
			}
			uint4 result;
			xxHash3.Hash128Internal((byte*)input, (byte*)destination, length, secret, seed, out result);
			return result;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		internal unsafe static ulong Hash64Internal(byte* input, byte* dest, long length, byte* secret, ulong seed)
		{
			if (length < 16L)
			{
				return xxHash3.Hash64Len0To16(input, length, secret, seed);
			}
			if (length < 128L)
			{
				return xxHash3.Hash64Len17To128(input, length, secret, seed);
			}
			if (length < 240L)
			{
				return xxHash3.Hash64Len129To240(input, length, secret, seed);
			}
			if (seed != 0UL)
			{
				byte* newSecret = (byte*)Memory.Unmanaged.Allocate(192L, 64, Allocator.Temp);
				xxHash3.EncodeSecretKey(newSecret, secret, seed);
				ulong num = xxHash3.Hash64Long(input, dest, length, newSecret);
				Memory.Unmanaged.Free<byte>(newSecret, Allocator.Temp);
				return num;
			}
			return xxHash3.Hash64Long(input, dest, length, secret);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
		internal unsafe static void Hash128Internal(byte* input, byte* dest, long length, byte* secret, ulong seed, out uint4 result)
		{
			if (dest != null && length < 240L)
			{
				UnsafeUtility.MemCpy((void*)dest, (void*)input, length);
			}
			if (length < 16L)
			{
				xxHash3.Hash128Len0To16(input, length, secret, seed, out result);
				return;
			}
			if (length < 128L)
			{
				xxHash3.Hash128Len17To128(input, length, secret, seed, out result);
				return;
			}
			if (length < 240L)
			{
				xxHash3.Hash128Len129To240(input, length, secret, seed, out result);
				return;
			}
			if (seed != 0UL)
			{
				byte* newSecret = (stackalloc byte[(UIntPtr)223] + 31L) & -32L;
				xxHash3.EncodeSecretKey(newSecret, secret, seed);
				xxHash3.Hash128Long(input, dest, length, newSecret, out result);
				return;
			}
			xxHash3.Hash128Long(input, dest, length, secret, out result);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001FE80 File Offset: 0x0001E080
		private unsafe static ulong Hash64Len1To3(byte* input, long len, byte* secret, ulong seed)
		{
			ulong num = (ulong)(*input);
			byte c2 = input[len >> 1];
			byte c3 = input[len - 1L];
			ulong num2 = (num << 16) | (ulong)((ulong)c2 << 24) | (ulong)c3 | (ulong)((ulong)((uint)len) << 8);
			ulong bitflip = (ulong)(xxHash3.Read32LE((void*)secret) ^ xxHash3.Read32LE((void*)(secret + 4))) + seed;
			return xxHash3.AvalancheH64(num2 ^ bitflip);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001FECC File Offset: 0x0001E0CC
		private unsafe static ulong Hash64Len4To8(byte* input, long length, byte* secret, ulong seed)
		{
			seed ^= (ulong)xxHash3.Swap32((uint)seed) << 32;
			uint input2 = xxHash3.Read32LE((void*)input);
			ulong num = (ulong)xxHash3.Read32LE((void*)(input + length - 4));
			ulong bitflip = (xxHash3.Read64LE((void*)(secret + 8)) ^ xxHash3.Read64LE((void*)(secret + 16))) - seed;
			return xxHash3.rrmxmx((num + ((ulong)input2 << 32)) ^ bitflip, (ulong)length);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001FF20 File Offset: 0x0001E120
		private unsafe static ulong Hash64Len9To16(byte* input, long length, byte* secret, ulong seed)
		{
			ulong bitflip = (xxHash3.Read64LE((void*)(secret + 24)) ^ xxHash3.Read64LE((void*)(secret + 32))) + seed;
			ulong bitflip2 = (xxHash3.Read64LE((void*)(secret + 40)) ^ xxHash3.Read64LE((void*)(secret + 48))) - seed;
			ulong input_lo = xxHash3.Read64LE((void*)input) ^ bitflip;
			ulong input_hi = xxHash3.Read64LE((void*)(input + length - 8)) ^ bitflip2;
			return xxHash3.Avalanche((ulong)(length + (long)xxHash3.Swap64(input_lo) + (long)input_hi + (long)xxHash3.Mul128Fold64(input_lo, input_hi)));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001FF88 File Offset: 0x0001E188
		private unsafe static ulong Hash64Len0To16(byte* input, long length, byte* secret, ulong seed)
		{
			if (length > 8L)
			{
				return xxHash3.Hash64Len9To16(input, length, secret, seed);
			}
			if (length >= 4L)
			{
				return xxHash3.Hash64Len4To8(input, length, secret, seed);
			}
			if (length > 0L)
			{
				return xxHash3.Hash64Len1To3(input, length, secret, seed);
			}
			return xxHash3.AvalancheH64(seed ^ (xxHash3.Read64LE((void*)(secret + 56)) ^ xxHash3.Read64LE((void*)(secret + 64))));
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001FFDC File Offset: 0x0001E1DC
		private unsafe static ulong Hash64Len17To128(byte* input, long length, byte* secret, ulong seed)
		{
			ulong acc = (ulong)(length * -7046029288634856825L);
			if (length > 32L)
			{
				if (length > 64L)
				{
					if (length > 96L)
					{
						acc += xxHash3.Mix16(input + 48, secret + 96, seed);
						acc += xxHash3.Mix16(input + length - 64, secret + 112, seed);
					}
					acc += xxHash3.Mix16(input + 32, secret + 64, seed);
					acc += xxHash3.Mix16(input + length - 48, secret + 80, seed);
				}
				acc += xxHash3.Mix16(input + 16, secret + 32, seed);
				acc += xxHash3.Mix16(input + length - 32, secret + 48, seed);
			}
			acc += xxHash3.Mix16(input, secret, seed);
			acc += xxHash3.Mix16(input + length - 16, secret + 16, seed);
			return xxHash3.Avalanche(acc);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002009C File Offset: 0x0001E29C
		private unsafe static ulong Hash64Len129To240(byte* input, long length, byte* secret, ulong seed)
		{
			ulong acc = (ulong)(length * -7046029288634856825L);
			int nbRounds = (int)length / 16;
			for (int i = 0; i < 8; i++)
			{
				acc += xxHash3.Mix16(input + 16 * i, secret + 16 * i, seed);
			}
			acc = xxHash3.Avalanche(acc);
			for (int j = 8; j < nbRounds; j++)
			{
				acc += xxHash3.Mix16(input + 16 * j, secret + 16 * (j - 8) + 3, seed);
			}
			acc += xxHash3.Mix16(input + length - 16, secret + 136 - 17, seed);
			return xxHash3.Avalanche(acc);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00020128 File Offset: 0x0001E328
		[BurstCompile]
		[MonoPInvokeCallback(typeof(xxHash3.Hash64Long_00000A73$PostfixBurstDelegate))]
		private unsafe static ulong Hash64Long(byte* input, byte* dest, long length, byte* secret)
		{
			return xxHash3.Hash64Long_00000A73$BurstDirectCall.Invoke(input, dest, length, secret);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00020134 File Offset: 0x0001E334
		private unsafe static void Hash128Len1To3(byte* input, long length, byte* secret, ulong seed, out uint4 result)
		{
			int num = (int)(*input);
			byte c2 = input[length >> 1];
			byte c3 = input[length - 1L];
			int num2 = (num << 16) + ((int)c2 << 24) + (int)c3 + (int)((int)((uint)length) << 8);
			uint combinedh = xxHash3.RotL32(xxHash3.Swap32((uint)num2), 13);
			ulong bitflipl = (ulong)(xxHash3.Read32LE((void*)secret) ^ xxHash3.Read32LE((void*)(secret + 4))) + seed;
			ulong bitfliph = (ulong)(xxHash3.Read32LE((void*)(secret + 8)) ^ xxHash3.Read32LE((void*)(secret + 12))) - seed;
			ulong keyed_lo = (ulong)num2 ^ bitflipl;
			ulong keyed_hi = (ulong)combinedh ^ bitfliph;
			result = xxHash3.ToUint4(xxHash3.AvalancheH64(keyed_lo), xxHash3.AvalancheH64(keyed_hi));
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000201C4 File Offset: 0x0001E3C4
		private unsafe static void Hash128Len4To8(byte* input, long len, byte* secret, ulong seed, out uint4 result)
		{
			seed ^= (ulong)xxHash3.Swap32((uint)seed) << 32;
			ulong num = (ulong)xxHash3.Read32LE((void*)input);
			uint input_hi = xxHash3.Read32LE((void*)(input + len - 4));
			ulong num2 = num + ((ulong)input_hi << 32);
			ulong bitflip = (xxHash3.Read64LE((void*)(secret + 16)) ^ xxHash3.Read64LE((void*)(secret + 24))) + seed;
			ulong high;
			ulong low = Common.umul128(num2 ^ bitflip, (ulong)(-7046029288634856825L + (len << 2)), out high);
			high += low << 1;
			low ^= high >> 3;
			low = xxHash3.XorShift64(low, 35);
			low *= 11507291218515648293UL;
			low = xxHash3.XorShift64(low, 28);
			high = xxHash3.Avalanche(high);
			result = xxHash3.ToUint4(low, high);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00020268 File Offset: 0x0001E468
		private unsafe static void Hash128Len9To16(byte* input, long len, byte* secret, ulong seed, out uint4 result)
		{
			ulong bitflipl = (xxHash3.Read64LE((void*)(secret + 32)) ^ xxHash3.Read64LE((void*)(secret + 40))) - seed;
			ulong bitfliph = (xxHash3.Read64LE((void*)(secret + 48)) ^ xxHash3.Read64LE((void*)(secret + 56))) + seed;
			ulong num = xxHash3.Read64LE((void*)input);
			ulong input_hi = xxHash3.Read64LE((void*)(input + len - 8));
			ulong high;
			ulong num2 = Common.umul128(num ^ input_hi ^ bitflipl, 11400714785074694791UL, out high) + (ulong)((ulong)(len - 1L) << 54);
			input_hi ^= bitfliph;
			high += input_hi + xxHash3.Mul32To64((uint)input_hi, 2246822518U);
			ulong hhigh;
			ulong hlow = Common.umul128(num2 ^ xxHash3.Swap64(high), 14029467366897019727UL, out hhigh);
			hhigh += high * 14029467366897019727UL;
			result = xxHash3.ToUint4(xxHash3.Avalanche(hlow), xxHash3.Avalanche(hhigh));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00020328 File Offset: 0x0001E528
		private unsafe static void Hash128Len0To16(byte* input, long length, byte* secret, ulong seed, out uint4 result)
		{
			if (length > 8L)
			{
				xxHash3.Hash128Len9To16(input, length, secret, seed, out result);
				return;
			}
			if (length >= 4L)
			{
				xxHash3.Hash128Len4To8(input, length, secret, seed, out result);
				return;
			}
			if (length > 0L)
			{
				xxHash3.Hash128Len1To3(input, length, secret, seed, out result);
				return;
			}
			ulong bitflipl = xxHash3.Read64LE((void*)(secret + 64)) ^ xxHash3.Read64LE((void*)(secret + 72));
			ulong bitfliph = xxHash3.Read64LE((void*)(secret + 80)) ^ xxHash3.Read64LE((void*)(secret + 88));
			ulong low = xxHash3.AvalancheH64(seed ^ bitflipl);
			ulong hi = xxHash3.AvalancheH64(seed ^ bitfliph);
			result = xxHash3.ToUint4(low, hi);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000203B0 File Offset: 0x0001E5B0
		private unsafe static void Hash128Len17To128(byte* input, long length, byte* secret, ulong seed, out uint4 result)
		{
			xxHash3.ulong2 acc = new xxHash3.ulong2((ulong)(length * -7046029288634856825L), 0UL);
			if (length > 32L)
			{
				if (length > 64L)
				{
					if (length > 96L)
					{
						acc = xxHash3.Mix32(acc, input + 48, input + length - 64, secret + 96, seed);
					}
					acc = xxHash3.Mix32(acc, input + 32, input + length - 48, secret + 64, seed);
				}
				acc = xxHash3.Mix32(acc, input + 16, input + length - 32, secret + 32, seed);
			}
			acc = xxHash3.Mix32(acc, input, input + length - 16, secret, seed);
			ulong low64 = acc.x + acc.y;
			ulong high64 = acc.x * 11400714785074694791UL + acc.y * 9650029242287828579UL + (ulong)((length - (long)seed) * -4417276706812531889L);
			result = xxHash3.ToUint4(xxHash3.Avalanche(low64), 0UL - xxHash3.Avalanche(high64));
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00020494 File Offset: 0x0001E694
		private unsafe static void Hash128Len129To240(byte* input, long length, byte* secret, ulong seed, out uint4 result)
		{
			xxHash3.ulong2 acc = new xxHash3.ulong2((ulong)(length * -7046029288634856825L), 0UL);
			long nbRounds = length / 32L;
			int i;
			for (i = 0; i < 4; i++)
			{
				acc = xxHash3.Mix32(acc, input + 32 * i, input + 32 * i + 16, secret + 32 * i, seed);
			}
			acc.x = xxHash3.Avalanche(acc.x);
			acc.y = xxHash3.Avalanche(acc.y);
			i = 4;
			while ((long)i < nbRounds)
			{
				acc = xxHash3.Mix32(acc, input + 32 * i, input + 32 * i + 16, secret + 3 + 32 * (i - 4), seed);
				i++;
			}
			acc = xxHash3.Mix32(acc, input + length - 16, input + length - 32, secret + 136 - 17 - 16, 0UL - seed);
			ulong low64 = acc.x + acc.y;
			ulong high64 = acc.x * 11400714785074694791UL + acc.y * 9650029242287828579UL + (ulong)((length - (long)seed) * -4417276706812531889L);
			result = xxHash3.ToUint4(xxHash3.Avalanche(low64), 0UL - xxHash3.Avalanche(high64));
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000205B7 File Offset: 0x0001E7B7
		[BurstCompile]
		[MonoPInvokeCallback(typeof(xxHash3.Hash128Long_00000A7A$PostfixBurstDelegate))]
		private unsafe static void Hash128Long(byte* input, byte* dest, long length, byte* secret, out uint4 result)
		{
			xxHash3.Hash128Long_00000A7A$BurstDirectCall.Invoke(input, dest, length, secret, out result);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000205C4 File Offset: 0x0001E7C4
		internal static uint2 ToUint2(ulong u)
		{
			return new uint2((uint)(u & (ulong)(-1)), (uint)(u >> 32));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000205D5 File Offset: 0x0001E7D5
		internal static uint4 ToUint4(ulong ul0, ulong ul1)
		{
			return new uint4((uint)(ul0 & (ulong)(-1)), (uint)(ul0 >> 32), (uint)(ul1 & (ulong)(-1)), (uint)(ul1 >> 32));
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000205F0 File Offset: 0x0001E7F0
		internal unsafe static void EncodeSecretKey(byte* dst, byte* secret, ulong seed)
		{
			int seedInitCount = 12;
			for (int i = 0; i < seedInitCount; i++)
			{
				xxHash3.Write64LE((void*)(dst + 16 * i), xxHash3.Read64LE((void*)(secret + 16 * i)) + seed);
				xxHash3.Write64LE((void*)(dst + 16 * i + 8), xxHash3.Read64LE((void*)(secret + 16 * i + 8)) - seed);
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00020640 File Offset: 0x0001E840
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static ulong Read64LE(void* addr)
		{
			return (ulong)(*(long*)addr);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00020644 File Offset: 0x0001E844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint Read32LE(void* addr)
		{
			return *(uint*)addr;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00020648 File Offset: 0x0001E848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void Write64LE(void* addr, ulong value)
		{
			*(long*)addr = (long)value;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002064D File Offset: 0x0001E84D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void Read32LE(void* addr, uint value)
		{
			*(int*)addr = (int)value;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00020652 File Offset: 0x0001E852
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Mul32To64(uint x, uint y)
		{
			return (ulong)x * (ulong)y;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002065C File Offset: 0x0001E85C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Swap64(ulong x)
		{
			return ((x << 56) & 18374686479671623680UL) | ((x << 40) & 71776119061217280UL) | ((x << 24) & 280375465082880UL) | ((x << 8) & 1095216660480UL) | ((x >> 8) & (ulong)(-16777216)) | ((x >> 24) & 16711680UL) | ((x >> 40) & 65280UL) | ((x >> 56) & 255UL);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000206D2 File Offset: 0x0001E8D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Swap32(uint x)
		{
			return ((x << 24) & 4278190080U) | ((x << 8) & 16711680U) | ((x >> 8) & 65280U) | ((x >> 24) & 255U);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000206FD File Offset: 0x0001E8FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint RotL32(uint x, int r)
		{
			return (x << r) | (x >> 32 - r);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002070F File Offset: 0x0001E90F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong RotL64(ulong x, int r)
		{
			return (x << r) | (x >> 64 - r);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00020721 File Offset: 0x0001E921
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong XorShift64(ulong v64, int shift)
		{
			return v64 ^ (v64 >> shift);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002072C File Offset: 0x0001E92C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Mul128Fold64(ulong lhs, ulong rhs)
		{
			ulong hi;
			return Common.umul128(lhs, rhs, out hi) ^ hi;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00020744 File Offset: 0x0001E944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static ulong Mix16(byte* input, byte* secret, ulong seed)
		{
			ulong num = xxHash3.Read64LE((void*)input);
			ulong input_hi = xxHash3.Read64LE((void*)(input + 8));
			return xxHash3.Mul128Fold64(num ^ (xxHash3.Read64LE((void*)secret) + seed), input_hi ^ (xxHash3.Read64LE((void*)(secret + 8)) - seed));
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002077C File Offset: 0x0001E97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static xxHash3.ulong2 Mix32(xxHash3.ulong2 acc, byte* input_1, byte* input_2, byte* secret, ulong seed)
		{
			ulong num = (acc.x + xxHash3.Mix16(input_1, secret, seed)) ^ (xxHash3.Read64LE((void*)input_2) + xxHash3.Read64LE((void*)(input_2 + 8)));
			ulong l = acc.y + xxHash3.Mix16(input_2, secret + 16, seed);
			l ^= xxHash3.Read64LE((void*)input_1) + xxHash3.Read64LE((void*)(input_1 + 8));
			return new xxHash3.ulong2(num, l);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000207D5 File Offset: 0x0001E9D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Avalanche(ulong h64)
		{
			h64 = xxHash3.XorShift64(h64, 37);
			h64 *= 1609587791953885689UL;
			h64 = xxHash3.XorShift64(h64, 32);
			return h64;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000207F9 File Offset: 0x0001E9F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong AvalancheH64(ulong h64)
		{
			h64 ^= h64 >> 33;
			h64 *= 14029467366897019727UL;
			h64 ^= h64 >> 29;
			h64 *= 1609587929392839161UL;
			h64 ^= h64 >> 32;
			return h64;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00020830 File Offset: 0x0001EA30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong rrmxmx(ulong h64, ulong length)
		{
			h64 ^= xxHash3.RotL64(h64, 49) ^ xxHash3.RotL64(h64, 24);
			h64 *= 11507291218515648293UL;
			h64 ^= (h64 >> 35) + length;
			h64 *= 11507291218515648293UL;
			return xxHash3.XorShift64(h64, 28);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002087E File Offset: 0x0001EA7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static ulong Mix2Acc(ulong acc0, ulong acc1, byte* secret)
		{
			return xxHash3.Mul128Fold64(acc0 ^ xxHash3.Read64LE((void*)secret), acc1 ^ xxHash3.Read64LE((void*)(secret + 8)));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00020898 File Offset: 0x0001EA98
		internal unsafe static ulong MergeAcc(ulong* acc, byte* secret, ulong start)
		{
			return xxHash3.Avalanche(start + xxHash3.Mix2Acc(*acc, acc[1], secret) + xxHash3.Mix2Acc(acc[2], acc[3], secret + 16) + xxHash3.Mix2Acc(acc[4], acc[5], secret + 32) + xxHash3.Mix2Acc(acc[6], acc[7], secret + 48));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00020900 File Offset: 0x0001EB00
		private unsafe static void DefaultHashLongInternalLoop(ulong* acc, byte* input, byte* dest, long length, byte* secret, int isHash64)
		{
			long nb_blocks = (length - 1L) / 1024L;
			int i = 0;
			while ((long)i < nb_blocks)
			{
				xxHash3.DefaultAccumulate(acc, input + i * 1024, (dest == null) ? null : (dest + i * 1024), secret, 16L, isHash64);
				xxHash3.DefaultScrambleAcc(acc, secret + 192 - 64);
				i++;
			}
			long nbStripes = (length - 1L - 1024L * nb_blocks) / 64L;
			xxHash3.DefaultAccumulate(acc, input + nb_blocks * 1024L, (dest == null) ? null : (dest + nb_blocks * 1024L), secret, nbStripes, isHash64);
			byte* p = input + length - 64;
			xxHash3.DefaultAccumulate512(acc, p, null, secret + 192 - 64 - 7, isHash64);
			if (dest != null)
			{
				long remaining = length % 64L;
				if (remaining != 0L)
				{
					UnsafeUtility.MemCpy((void*)(dest + length - remaining), (void*)(input + length - remaining), remaining);
				}
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000209E4 File Offset: 0x0001EBE4
		internal unsafe static void DefaultAccumulate(ulong* acc, byte* input, byte* dest, byte* secret, long nbStripes, int isHash64)
		{
			int i = 0;
			while ((long)i < nbStripes)
			{
				xxHash3.DefaultAccumulate512(acc, input + i * 64, (dest == null) ? null : (dest + i * 64), secret + i * 8, isHash64);
				i++;
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00020A24 File Offset: 0x0001EC24
		internal unsafe static void DefaultAccumulate512(ulong* acc, byte* input, byte* dest, byte* secret, int isHash64)
		{
			int count = 8;
			for (int i = 0; i < count; i++)
			{
				ulong data_val = xxHash3.Read64LE((void*)(input + 8 * i));
				ulong data_key = data_val ^ xxHash3.Read64LE((void*)(secret + i * 8));
				if (dest != null)
				{
					xxHash3.Write64LE((void*)(dest + 8 * i), data_val);
				}
				acc[i ^ 1] += data_val;
				acc[i] += xxHash3.Mul32To64((uint)(data_key & (ulong)(-1)), (uint)(data_key >> 32));
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00020A90 File Offset: 0x0001EC90
		internal unsafe static void DefaultScrambleAcc(ulong* acc, byte* secret)
		{
			for (int i = 0; i < 8; i++)
			{
				ulong key64 = xxHash3.Read64LE((void*)(secret + 8 * i));
				ulong acc2 = acc[i];
				acc2 = xxHash3.XorShift64(acc2, 47);
				acc2 ^= key64;
				acc2 *= (ulong)(-1640531535);
				acc[i] = acc2;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00020ADC File Offset: 0x0001ECDC
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong Hash64Long$BurstManaged(byte* input, byte* dest, long length, byte* secret)
		{
			ulong* acc = (stackalloc ulong[(UIntPtr)95] + 31L / 8L) & -32L;
			*acc = (ulong)(-1028477379);
			acc[1] = 11400714785074694791UL;
			acc[2] = 14029467366897019727UL;
			acc[3] = 1609587929392839161UL;
			acc[4] = 9650029242287828579UL;
			acc[5] = (ulong)(-2048144777);
			acc[6] = 2870177450012600261UL;
			acc[7] = (ulong)(-1640531535);
			if (X86.Avx2.IsAvx2Supported)
			{
				xxHash3.Avx2HashLongInternalLoop(acc, input, dest, length, secret, 1);
			}
			else
			{
				xxHash3.DefaultHashLongInternalLoop(acc, input, dest, length, secret, 1);
			}
			return xxHash3.MergeAcc(acc, secret + 11, (ulong)(length * -7046029288634856825L));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00020B9C File Offset: 0x0001ED9C
		[BurstCompile]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Hash128Long$BurstManaged(byte* input, byte* dest, long length, byte* secret, out uint4 result)
		{
			ulong* acc = (stackalloc ulong[(UIntPtr)95] + 31L / 8L) & -32L;
			*acc = (ulong)(-1028477379);
			acc[1] = 11400714785074694791UL;
			acc[2] = 14029467366897019727UL;
			acc[3] = 1609587929392839161UL;
			acc[4] = 9650029242287828579UL;
			acc[5] = (ulong)(-2048144777);
			acc[6] = 2870177450012600261UL;
			acc[7] = (ulong)(-1640531535);
			if (X86.Avx2.IsAvx2Supported)
			{
				xxHash3.Avx2HashLongInternalLoop(acc, input, dest, length, secret, 0);
			}
			else
			{
				xxHash3.DefaultHashLongInternalLoop(acc, input, dest, length, secret, 0);
			}
			ulong low64 = xxHash3.MergeAcc(acc, secret + 11, (ulong)(length * -7046029288634856825L));
			ulong high64 = xxHash3.MergeAcc(acc, secret + 192 - 64 - 11, (ulong)(~(ulong)(length * -4417276706812531889L)));
			result = xxHash3.ToUint4(low64, high64);
		}

		// Token: 0x04000475 RID: 1141
		private const int STRIPE_LEN = 64;

		// Token: 0x04000476 RID: 1142
		private const int ACC_NB = 8;

		// Token: 0x04000477 RID: 1143
		private const int SECRET_CONSUME_RATE = 8;

		// Token: 0x04000478 RID: 1144
		private const int SECRET_KEY_SIZE = 192;

		// Token: 0x04000479 RID: 1145
		private const int SECRET_KEY_MIN_SIZE = 136;

		// Token: 0x0400047A RID: 1146
		private const int SECRET_LASTACC_START = 7;

		// Token: 0x0400047B RID: 1147
		private const int NB_ROUNDS = 16;

		// Token: 0x0400047C RID: 1148
		private const int BLOCK_LEN = 1024;

		// Token: 0x0400047D RID: 1149
		private const uint PRIME32_1 = 2654435761U;

		// Token: 0x0400047E RID: 1150
		private const uint PRIME32_2 = 2246822519U;

		// Token: 0x0400047F RID: 1151
		private const uint PRIME32_3 = 3266489917U;

		// Token: 0x04000480 RID: 1152
		private const uint PRIME32_5 = 374761393U;

		// Token: 0x04000481 RID: 1153
		private const ulong PRIME64_1 = 11400714785074694791UL;

		// Token: 0x04000482 RID: 1154
		private const ulong PRIME64_2 = 14029467366897019727UL;

		// Token: 0x04000483 RID: 1155
		private const ulong PRIME64_3 = 1609587929392839161UL;

		// Token: 0x04000484 RID: 1156
		private const ulong PRIME64_4 = 9650029242287828579UL;

		// Token: 0x04000485 RID: 1157
		private const ulong PRIME64_5 = 2870177450012600261UL;

		// Token: 0x04000486 RID: 1158
		private const int MIDSIZE_MAX = 240;

		// Token: 0x04000487 RID: 1159
		private const int MIDSIZE_STARTOFFSET = 3;

		// Token: 0x04000488 RID: 1160
		private const int MIDSIZE_LASTOFFSET = 17;

		// Token: 0x04000489 RID: 1161
		private const int SECRET_MERGEACCS_START = 11;

		// Token: 0x020000F7 RID: 247
		[GenerateTestsForBurstCompatibility]
		public struct StreamingState
		{
			// Token: 0x06000AB6 RID: 2742 RVA: 0x00020C8B File Offset: 0x0001EE8B
			public StreamingState(bool isHash64, ulong seed = 0UL)
			{
				this.State = default(xxHash3.StreamingState.StreamingStateData);
				this.Reset(isHash64, seed);
			}

			// Token: 0x06000AB7 RID: 2743 RVA: 0x00020CA4 File Offset: 0x0001EEA4
			public unsafe void Reset(bool isHash64, ulong seed = 0UL)
			{
				int size = UnsafeUtility.SizeOf<xxHash3.StreamingState.StreamingStateData>();
				UnsafeUtility.MemClear(UnsafeUtility.AddressOf<xxHash3.StreamingState.StreamingStateData>(ref this.State), (long)size);
				this.State.IsHash64 = (isHash64 ? 1 : 0);
				ulong* acc = this.Acc;
				*acc = (ulong)(-1028477379);
				acc[1] = 11400714785074694791UL;
				acc[2] = 14029467366897019727UL;
				acc[3] = 1609587929392839161UL;
				acc[4] = 9650029242287828579UL;
				acc[5] = (ulong)(-2048144777);
				acc[6] = 2870177450012600261UL;
				acc[7] = (ulong)(-1640531535);
				this.State.Seed = seed;
				byte[] array;
				byte* secret;
				if ((array = xxHashDefaultKey.kSecret) == null || array.Length == 0)
				{
					secret = null;
				}
				else
				{
					secret = &array[0];
				}
				if (seed != 0UL)
				{
					xxHash3.EncodeSecretKey(this.SecretKey, secret, seed);
				}
				else
				{
					UnsafeUtility.MemCpy((void*)this.SecretKey, (void*)secret, 192L);
				}
				array = null;
			}

			// Token: 0x06000AB8 RID: 2744 RVA: 0x00020DA0 File Offset: 0x0001EFA0
			public unsafe void Update(void* input, int length)
			{
				byte* bInput = (byte*)input;
				byte* bEnd = bInput + length;
				int isHash64 = this.State.IsHash64;
				byte* secret = this.SecretKey;
				this.State.TotalLength = this.State.TotalLength + (long)length;
				if (this.State.BufferedSize + length <= xxHash3.StreamingState.INTERNAL_BUFFER_SIZE)
				{
					UnsafeUtility.MemCpy((void*)(this.Buffer + this.State.BufferedSize), (void*)bInput, (long)length);
					this.State.BufferedSize = this.State.BufferedSize + length;
					return;
				}
				if (this.State.BufferedSize != 0)
				{
					int loadSize = xxHash3.StreamingState.INTERNAL_BUFFER_SIZE - this.State.BufferedSize;
					UnsafeUtility.MemCpy((void*)(this.Buffer + this.State.BufferedSize), (void*)bInput, (long)loadSize);
					bInput += loadSize;
					this.ConsumeStripes(this.Acc, ref this.State.NbStripesSoFar, this.Buffer, (long)xxHash3.StreamingState.INTERNAL_BUFFER_STRIPES, secret, isHash64);
					this.State.BufferedSize = 0;
				}
				if (bInput + xxHash3.StreamingState.INTERNAL_BUFFER_SIZE < bEnd)
				{
					byte* limit = bEnd - xxHash3.StreamingState.INTERNAL_BUFFER_SIZE;
					do
					{
						this.ConsumeStripes(this.Acc, ref this.State.NbStripesSoFar, bInput, (long)xxHash3.StreamingState.INTERNAL_BUFFER_STRIPES, secret, isHash64);
						bInput += xxHash3.StreamingState.INTERNAL_BUFFER_SIZE;
					}
					while (bInput < limit);
					UnsafeUtility.MemCpy((void*)(this.Buffer + xxHash3.StreamingState.INTERNAL_BUFFER_SIZE - 64), (void*)(bInput - 64), 64L);
				}
				if (bInput < bEnd)
				{
					long newBufferedSize = (long)(bEnd - bInput);
					UnsafeUtility.MemCpy((void*)this.Buffer, (void*)bInput, newBufferedSize);
					this.State.BufferedSize = (int)newBufferedSize;
				}
			}

			// Token: 0x06000AB9 RID: 2745 RVA: 0x00020F0A File Offset: 0x0001F10A
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public void Update<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in T input) where T : struct, ValueType
			{
				this.Update(UnsafeUtilityExtensions.AddressOf<T>(in input), UnsafeUtility.SizeOf<T>());
			}

			// Token: 0x06000ABA RID: 2746 RVA: 0x00020F20 File Offset: 0x0001F120
			public unsafe uint4 DigestHash128()
			{
				byte* secret = this.SecretKey;
				uint4 hash;
				if (this.State.TotalLength > 240L)
				{
					ulong* acc = stackalloc ulong[(UIntPtr)64];
					this.DigestLong(acc, secret, 0);
					ulong num = xxHash3.MergeAcc(acc, secret + 11, (ulong)(this.State.TotalLength * -7046029288634856825L));
					ulong high64 = xxHash3.MergeAcc(acc, secret + xxHash3.StreamingState.SECRET_LIMIT - 11, (ulong)(~(ulong)(this.State.TotalLength * -4417276706812531889L)));
					hash = xxHash3.ToUint4(num, high64);
				}
				else
				{
					hash = xxHash3.Hash128((void*)this.Buffer, this.State.TotalLength, this.State.Seed);
				}
				this.Reset(this.State.IsHash64 == 1, this.State.Seed);
				return hash;
			}

			// Token: 0x06000ABB RID: 2747 RVA: 0x00020FE8 File Offset: 0x0001F1E8
			public unsafe uint2 DigestHash64()
			{
				byte* secret = this.SecretKey;
				uint2 hash;
				if (this.State.TotalLength > 240L)
				{
					ulong* acc = stackalloc ulong[(UIntPtr)64];
					this.DigestLong(acc, secret, 1);
					hash = xxHash3.ToUint2(xxHash3.MergeAcc(acc, secret + 11, (ulong)(this.State.TotalLength * -7046029288634856825L)));
				}
				else
				{
					hash = xxHash3.Hash64((void*)this.Buffer, this.State.TotalLength, this.State.Seed);
				}
				this.Reset(this.State.IsHash64 == 1, this.State.Seed);
				return hash;
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00021087 File Offset: 0x0001F287
			private unsafe ulong* Acc
			{
				[DebuggerStepThrough]
				get
				{
					return (ulong*)UnsafeUtility.AddressOf<ulong>(ref this.State.Acc);
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00021099 File Offset: 0x0001F299
			private unsafe byte* Buffer
			{
				[DebuggerStepThrough]
				get
				{
					return (byte*)UnsafeUtility.AddressOf<byte>(ref this.State.Buffer);
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000ABE RID: 2750 RVA: 0x000210AB File Offset: 0x0001F2AB
			private unsafe byte* SecretKey
			{
				[DebuggerStepThrough]
				get
				{
					return (byte*)UnsafeUtility.AddressOf<byte>(ref this.State.SecretKey);
				}
			}

			// Token: 0x06000ABF RID: 2751 RVA: 0x000210C0 File Offset: 0x0001F2C0
			private unsafe void DigestLong(ulong* acc, byte* secret, int isHash64)
			{
				UnsafeUtility.MemCpy((void*)acc, (void*)this.Acc, 64L);
				if (this.State.BufferedSize >= 64)
				{
					int totalNbStripes = (this.State.BufferedSize - 1) / 64;
					this.ConsumeStripes(acc, ref this.State.NbStripesSoFar, this.Buffer, (long)totalNbStripes, secret, isHash64);
					if (X86.Avx2.IsAvx2Supported)
					{
						xxHash3.Avx2Accumulate512(acc, this.Buffer + this.State.BufferedSize - 64, null, secret + xxHash3.StreamingState.SECRET_LIMIT - 7);
						return;
					}
					xxHash3.DefaultAccumulate512(acc, this.Buffer + this.State.BufferedSize - 64, null, secret + xxHash3.StreamingState.SECRET_LIMIT - 7, isHash64);
					return;
				}
				else
				{
					byte* lastStripe = stackalloc byte[(UIntPtr)64];
					int catchupSize = 64 - this.State.BufferedSize;
					UnsafeUtility.MemCpy((void*)lastStripe, (void*)(this.Buffer + xxHash3.StreamingState.INTERNAL_BUFFER_SIZE - catchupSize), (long)catchupSize);
					UnsafeUtility.MemCpy((void*)(lastStripe + catchupSize), (void*)this.Buffer, (long)this.State.BufferedSize);
					if (X86.Avx2.IsAvx2Supported)
					{
						xxHash3.Avx2Accumulate512(acc, lastStripe, null, secret + xxHash3.StreamingState.SECRET_LIMIT - 7);
						return;
					}
					xxHash3.DefaultAccumulate512(acc, lastStripe, null, secret + xxHash3.StreamingState.SECRET_LIMIT - 7, isHash64);
					return;
				}
			}

			// Token: 0x06000AC0 RID: 2752 RVA: 0x000211E4 File Offset: 0x0001F3E4
			private unsafe void ConsumeStripes(ulong* acc, ref int nbStripesSoFar, byte* input, long totalStripes, byte* secret, int isHash64)
			{
				if ((long)(xxHash3.StreamingState.NB_STRIPES_PER_BLOCK - nbStripesSoFar) <= totalStripes)
				{
					int nbStripes = xxHash3.StreamingState.NB_STRIPES_PER_BLOCK - nbStripesSoFar;
					if (X86.Avx2.IsAvx2Supported)
					{
						xxHash3.Avx2Accumulate(acc, input, null, secret + nbStripesSoFar * 8, (long)nbStripes, isHash64);
						xxHash3.Avx2ScrambleAcc(acc, secret + xxHash3.StreamingState.SECRET_LIMIT);
						xxHash3.Avx2Accumulate(acc, input + nbStripes * 64, null, secret, totalStripes - (long)nbStripes, isHash64);
					}
					else
					{
						xxHash3.DefaultAccumulate(acc, input, null, secret + nbStripesSoFar * 8, (long)nbStripes, isHash64);
						xxHash3.DefaultScrambleAcc(acc, secret + xxHash3.StreamingState.SECRET_LIMIT);
						xxHash3.DefaultAccumulate(acc, input + nbStripes * 64, null, secret, totalStripes - (long)nbStripes, isHash64);
					}
					nbStripesSoFar = (int)totalStripes - nbStripes;
					return;
				}
				if (X86.Avx2.IsAvx2Supported)
				{
					xxHash3.Avx2Accumulate(acc, input, null, secret + nbStripesSoFar * 8, totalStripes, isHash64);
				}
				else
				{
					xxHash3.DefaultAccumulate(acc, input, null, secret + nbStripesSoFar * 8, totalStripes, isHash64);
				}
				nbStripesSoFar += (int)totalStripes;
			}

			// Token: 0x06000AC1 RID: 2753 RVA: 0x000212C8 File Offset: 0x0001F4C8
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckKeySize(int isHash64)
			{
				if (this.State.IsHash64 != isHash64)
				{
					string s = ((this.State.IsHash64 != 0) ? "64" : "128");
					throw new InvalidOperationException("The streaming state was create for " + s + " bits hash key, the calling method doesn't support this key size, please use the appropriate API");
				}
			}

			// Token: 0x0400048A RID: 1162
			private static readonly int SECRET_LIMIT = 128;

			// Token: 0x0400048B RID: 1163
			private static readonly int NB_STRIPES_PER_BLOCK = xxHash3.StreamingState.SECRET_LIMIT / 8;

			// Token: 0x0400048C RID: 1164
			private static readonly int INTERNAL_BUFFER_SIZE = 256;

			// Token: 0x0400048D RID: 1165
			private static readonly int INTERNAL_BUFFER_STRIPES = xxHash3.StreamingState.INTERNAL_BUFFER_SIZE / 64;

			// Token: 0x0400048E RID: 1166
			private xxHash3.StreamingState.StreamingStateData State;

			// Token: 0x020000F8 RID: 248
			[StructLayout(LayoutKind.Explicit)]
			private struct StreamingStateData
			{
				// Token: 0x0400048F RID: 1167
				[FieldOffset(0)]
				public ulong Acc;

				// Token: 0x04000490 RID: 1168
				[FieldOffset(64)]
				public byte Buffer;

				// Token: 0x04000491 RID: 1169
				[FieldOffset(320)]
				public int IsHash64;

				// Token: 0x04000492 RID: 1170
				[FieldOffset(324)]
				public int BufferedSize;

				// Token: 0x04000493 RID: 1171
				[FieldOffset(328)]
				public int NbStripesSoFar;

				// Token: 0x04000494 RID: 1172
				[FieldOffset(336)]
				public long TotalLength;

				// Token: 0x04000495 RID: 1173
				[FieldOffset(344)]
				public ulong Seed;

				// Token: 0x04000496 RID: 1174
				[FieldOffset(352)]
				public byte SecretKey;

				// Token: 0x04000497 RID: 1175
				[FieldOffset(540)]
				public byte _PadEnd;
			}
		}

		// Token: 0x020000F9 RID: 249
		private struct ulong2
		{
			// Token: 0x06000AC3 RID: 2755 RVA: 0x00021342 File Offset: 0x0001F542
			public ulong2(ulong x, ulong y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04000498 RID: 1176
			public ulong x;

			// Token: 0x04000499 RID: 1177
			public ulong y;
		}

		// Token: 0x020000FA RID: 250
		// (Invoke) Token: 0x06000AC5 RID: 2757
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate ulong Hash64Long_00000A73$PostfixBurstDelegate(byte* input, byte* dest, long length, byte* secret);

		// Token: 0x020000FB RID: 251
		internal static class Hash64Long_00000A73$BurstDirectCall
		{
			// Token: 0x06000AC8 RID: 2760 RVA: 0x00021354 File Offset: 0x0001F554
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (xxHash3.Hash64Long_00000A73$BurstDirectCall.Pointer == 0)
				{
					xxHash3.Hash64Long_00000A73$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<xxHash3.Hash64Long_00000A73$PostfixBurstDelegate>(new xxHash3.Hash64Long_00000A73$PostfixBurstDelegate(xxHash3.Hash64Long)).Value;
				}
				A_0 = xxHash3.Hash64Long_00000A73$BurstDirectCall.Pointer;
			}

			// Token: 0x06000AC9 RID: 2761 RVA: 0x00021394 File Offset: 0x0001F594
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				xxHash3.Hash64Long_00000A73$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000ACA RID: 2762 RVA: 0x000213AC File Offset: 0x0001F5AC
			public unsafe static ulong Invoke(byte* input, byte* dest, long length, byte* secret)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = xxHash3.Hash64Long_00000A73$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.UInt64(System.Byte*,System.Byte*,System.Int64,System.Byte*), input, dest, length, secret, functionPointer);
					}
				}
				return xxHash3.Hash64Long$BurstManaged(input, dest, length, secret);
			}

			// Token: 0x0400049A RID: 1178
			private static IntPtr Pointer;
		}

		// Token: 0x020000FC RID: 252
		// (Invoke) Token: 0x06000ACC RID: 2764
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void Hash128Long_00000A7A$PostfixBurstDelegate(byte* input, byte* dest, long length, byte* secret, out uint4 result);

		// Token: 0x020000FD RID: 253
		internal static class Hash128Long_00000A7A$BurstDirectCall
		{
			// Token: 0x06000ACF RID: 2767 RVA: 0x000213E4 File Offset: 0x0001F5E4
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (xxHash3.Hash128Long_00000A7A$BurstDirectCall.Pointer == 0)
				{
					xxHash3.Hash128Long_00000A7A$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<xxHash3.Hash128Long_00000A7A$PostfixBurstDelegate>(new xxHash3.Hash128Long_00000A7A$PostfixBurstDelegate(xxHash3.Hash128Long)).Value;
				}
				A_0 = xxHash3.Hash128Long_00000A7A$BurstDirectCall.Pointer;
			}

			// Token: 0x06000AD0 RID: 2768 RVA: 0x00021424 File Offset: 0x0001F624
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				xxHash3.Hash128Long_00000A7A$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000AD1 RID: 2769 RVA: 0x0002143C File Offset: 0x0001F63C
			public unsafe static void Invoke(byte* input, byte* dest, long length, byte* secret, out uint4 result)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = xxHash3.Hash128Long_00000A7A$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(System.Byte*,System.Byte*,System.Int64,System.Byte*,Unity.Mathematics.uint4&), input, dest, length, secret, ref result, functionPointer);
						return;
					}
				}
				xxHash3.Hash128Long$BurstManaged(input, dest, length, secret, out result);
			}

			// Token: 0x0400049B RID: 1179
			private static IntPtr Pointer;
		}
	}
}
