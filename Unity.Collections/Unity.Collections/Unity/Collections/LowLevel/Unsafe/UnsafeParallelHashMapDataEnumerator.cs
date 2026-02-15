using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012A RID: 298
	internal struct UnsafeParallelHashMapDataEnumerator
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x00025A9B File Offset: 0x00023C9B
		internal unsafe UnsafeParallelHashMapDataEnumerator(UnsafeParallelHashMapData* data)
		{
			this.m_Buffer = data;
			this.m_Index = -1;
			this.m_BucketIndex = 0;
			this.m_NextIndex = -1;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00025AB9 File Offset: 0x00023CB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool MoveNext()
		{
			return UnsafeParallelHashMapData.MoveNext(this.m_Buffer, ref this.m_BucketIndex, ref this.m_NextIndex, out this.m_Index);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00025AD8 File Offset: 0x00023CD8
		internal void Reset()
		{
			this.m_Index = -1;
			this.m_BucketIndex = 0;
			this.m_NextIndex = -1;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00025AF0 File Offset: 0x00023CF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal KeyValue<TKey, TValue> GetCurrent<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>() where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
		{
			return new KeyValue<TKey, TValue>
			{
				m_Buffer = this.m_Buffer,
				m_Index = this.m_Index
			};
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00025B20 File Offset: 0x00023D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe TKey GetCurrentKey<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey>() where TKey : struct, ValueType, IEquatable<TKey>
		{
			if (this.m_Index != -1)
			{
				return UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
			}
			return default(TKey);
		}

		// Token: 0x040004FE RID: 1278
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040004FF RID: 1279
		internal int m_Index;

		// Token: 0x04000500 RID: 1280
		internal int m_BucketIndex;

		// Token: 0x04000501 RID: 1281
		internal int m_NextIndex;
	}
}
