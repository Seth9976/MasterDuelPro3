using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020001D8 RID: 472
	internal ref struct HashFNV1A32
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x000315B8 File Offset: 0x0002F7B8
		public static HashFNV1A32 Create()
		{
			return new HashFNV1A32
			{
				m_Hash = 2166136261U
			};
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000315DA File Offset: 0x0002F7DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in int input)
		{
			this.m_Hash = (this.m_Hash ^ (uint)input) * 16777619U;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000315F1 File Offset: 0x0002F7F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in uint input)
		{
			this.m_Hash = (this.m_Hash ^ input) * 16777619U;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00031608 File Offset: 0x0002F808
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in bool input)
		{
			this.m_Hash = (this.m_Hash ^ (input ? 1U : 0U)) * 16777619U;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00031628 File Offset: 0x0002F828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in float input)
		{
			uint hash = this.m_Hash;
			float num = input;
			this.m_Hash = (hash ^ (uint)num.GetHashCode()) * 16777619U;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00031654 File Offset: 0x0002F854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in double input)
		{
			uint hash = this.m_Hash;
			double num = input;
			this.m_Hash = (hash ^ (uint)num.GetHashCode()) * 16777619U;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00031680 File Offset: 0x0002F880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in Vector2 input)
		{
			uint hash = this.m_Hash;
			Vector2 vector = input;
			this.m_Hash = (hash ^ (uint)vector.GetHashCode()) * 16777619U;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000316B4 File Offset: 0x0002F8B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in Vector3 input)
		{
			uint hash = this.m_Hash;
			Vector3 vector = input;
			this.m_Hash = (hash ^ (uint)vector.GetHashCode()) * 16777619U;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000316E8 File Offset: 0x0002F8E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in Vector4 input)
		{
			uint hash = this.m_Hash;
			Vector4 vector = input;
			this.m_Hash = (hash ^ (uint)vector.GetHashCode()) * 16777619U;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003171C File Offset: 0x0002F91C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append<T>(T input) where T : struct
		{
			this.m_Hash = (this.m_Hash ^ (uint)input.GetHashCode()) * 16777619U;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003173E File Offset: 0x0002F93E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(Delegate del)
		{
			this.m_Hash = (this.m_Hash ^ (uint)HashFNV1A32.GetFuncHashCode(del)) * 16777619U;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00031759 File Offset: 0x0002F959
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetFuncHashCode(Delegate del)
		{
			return del.Method.GetHashCode() ^ RuntimeHelpers.GetHashCode(del.Target);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00031772 File Offset: 0x0002F972
		public int value
		{
			get
			{
				return (int)this.m_Hash;
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0003177A File Offset: 0x0002F97A
		public override int GetHashCode()
		{
			return this.value;
		}

		// Token: 0x0400090A RID: 2314
		private const uint k_Prime = 16777619U;

		// Token: 0x0400090B RID: 2315
		private const uint k_OffsetBasis = 2166136261U;

		// Token: 0x0400090C RID: 2316
		private uint m_Hash;
	}
}
