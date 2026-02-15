using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000135 RID: 309
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Hashing/Hash128.bindings.h")]
	[NativeHeader("Runtime/Utilities/Hash128.h")]
	[Serializable]
	public struct Hash128 : IComparable, IComparable<Hash128>, IEquatable<Hash128>
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00018792 File Offset: 0x00016992
		public Hash128(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
		{
			this.u64_0 = ((ulong)u32_1 << 32) | (ulong)u32_0;
			this.u64_1 = ((ulong)u32_3 << 32) | (ulong)u32_2;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000187B2 File Offset: 0x000169B2
		public Hash128(ulong u64_0, ulong u64_1)
		{
			this.u64_0 = u64_0;
			this.u64_1 = u64_1;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000187C3 File Offset: 0x000169C3
		public bool isValid
		{
			get
			{
				return this.u64_0 != 0UL || this.u64_1 > 0UL;
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000187DC File Offset: 0x000169DC
		public int CompareTo(Hash128 rhs)
		{
			bool flag = this < rhs;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				bool flag2 = this > rhs;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00018818 File Offset: 0x00016A18
		public override string ToString()
		{
			return Hash128.Hash128ToStringImpl(this);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00018838 File Offset: 0x00016A38
		[FreeFunction("StringToHash128", IsThreadSafe = true)]
		public unsafe static Hash128 Parse(string hashString)
		{
			Hash128 hash2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(hashString, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = hashString.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Hash128 hash;
				Hash128.Parse_Injected(ref managedSpanWrapper, out hash);
			}
			finally
			{
				char* ptr = null;
				Hash128 hash;
				hash2 = hash;
			}
			return hash2;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00018894 File Offset: 0x00016A94
		[FreeFunction("Hash128ToString", IsThreadSafe = true)]
		private static string Hash128ToStringImpl(Hash128 hash)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Hash128.Hash128ToStringImpl_Injected(ref hash, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000D13 RID: 3347
		[FreeFunction("ComputeHash128FromScriptPointer", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromPtr(IntPtr data, int start, int count, int elemSize, ref Hash128 hash);

		// Token: 0x06000D14 RID: 3348 RVA: 0x000188C8 File Offset: 0x00016AC8
		public unsafe static Hash128 Compute<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr2 = &val)
			{
				void* ptr = (void*)ptr2;
				Hash128 h = default(Hash128);
				Hash128.ComputeFromPtr((IntPtr)ptr, 0, 1, UnsafeUtility.SizeOf<T>(), ref h);
				return h;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00018900 File Offset: 0x00016B00
		public static Hash128 Compute(int val)
		{
			Hash128 h = default(Hash128);
			h.Append(val);
			return h;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00018924 File Offset: 0x00016B24
		public unsafe void Append<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr2 = &val)
			{
				void* ptr = (void*)ptr2;
				Hash128.ComputeFromPtr((IntPtr)ptr, 0, 1, UnsafeUtility.SizeOf<T>(), ref this);
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00018950 File Offset: 0x00016B50
		public void Append(int val)
		{
			this.ShortHash4((uint)val);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0001895C File Offset: 0x00016B5C
		public override bool Equals(object obj)
		{
			return obj is Hash128 && this == (Hash128)obj;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0001898C File Offset: 0x00016B8C
		public bool Equals(Hash128 obj)
		{
			return this == obj;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000189AC File Offset: 0x00016BAC
		public override int GetHashCode()
		{
			return this.u64_0.GetHashCode() ^ this.u64_1.GetHashCode();
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000189D8 File Offset: 0x00016BD8
		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is Hash128);
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				Hash128 rhs = (Hash128)obj;
				num = this.CompareTo(rhs);
			}
			return num;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00018A14 File Offset: 0x00016C14
		public static bool operator ==(Hash128 hash1, Hash128 hash2)
		{
			return hash1.u64_0 == hash2.u64_0 && hash1.u64_1 == hash2.u64_1;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00018A48 File Offset: 0x00016C48
		public static bool operator !=(Hash128 hash1, Hash128 hash2)
		{
			return !(hash1 == hash2);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00018A64 File Offset: 0x00016C64
		public static bool operator <(Hash128 x, Hash128 y)
		{
			bool flag = x.u64_0 != y.u64_0;
			bool flag2;
			if (flag)
			{
				flag2 = x.u64_0 < y.u64_0;
			}
			else
			{
				flag2 = x.u64_1 < y.u64_1;
			}
			return flag2;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00018AAC File Offset: 0x00016CAC
		public static bool operator >(Hash128 x, Hash128 y)
		{
			bool flag = x < y;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = x == y;
				flag2 = !flag3;
			}
			return flag2;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00018AE0 File Offset: 0x00016CE0
		private void ShortHash4(uint data)
		{
			ulong a = this.u64_0;
			ulong b = this.u64_1;
			ulong c = 16045690984833335023UL;
			ulong d = 16045690984833335023UL;
			d += 288230376151711744UL;
			c += (ulong)data;
			Hash128.ShortEnd(ref a, ref b, ref c, ref d);
			this.u64_0 = a;
			this.u64_1 = b;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00018B40 File Offset: 0x00016D40
		private static void ShortEnd(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			h3 ^= h2;
			Hash128.Rot64(ref h2, 15);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 52);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 26);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 51);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 28);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 9);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 47);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 54);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 32);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 25);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 63);
			h1 += h0;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00018C4B File Offset: 0x00016E4B
		private static void Rot64(ref ulong x, int k)
		{
			x = (x << k) | (x >> 64 - k);
		}

		// Token: 0x06000D23 RID: 3363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Parse_Injected(ref ManagedSpanWrapper hashString, out Hash128 ret);

		// Token: 0x06000D24 RID: 3364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Hash128ToStringImpl_Injected([In] ref Hash128 hash, out ManagedSpanWrapper ret);

		// Token: 0x04000410 RID: 1040
		internal ulong u64_0;

		// Token: 0x04000411 RID: 1041
		internal ulong u64_1;

		// Token: 0x04000412 RID: 1042
		private const ulong kConst = 16045690984833335023UL;
	}
}
