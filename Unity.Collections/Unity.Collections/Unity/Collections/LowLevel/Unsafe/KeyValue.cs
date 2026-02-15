using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000129 RID: 297
	[DebuggerDisplay("Key = {Key}, Value = {Value}")]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct KeyValue<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000259C4 File Offset: 0x00023BC4
		public static KeyValue<TKey, TValue> Null
		{
			get
			{
				return new KeyValue<TKey, TValue>
				{
					m_Index = -1
				};
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000259E4 File Offset: 0x00023BE4
		public unsafe TKey Key
		{
			get
			{
				if (this.m_Index != -1)
				{
					return UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
				}
				return default(TKey);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00025A1A File Offset: 0x00023C1A
		public unsafe ref TValue Value
		{
			get
			{
				return UnsafeUtility.AsRef<TValue>((void*)(this.m_Buffer->values + UnsafeUtility.SizeOf<TValue>() * this.m_Index));
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00025A3C File Offset: 0x00023C3C
		public unsafe bool GetKeyValue(out TKey key, out TValue value)
		{
			if (this.m_Index != -1)
			{
				key = UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
				value = UnsafeUtility.ReadArrayElement<TValue>((void*)this.m_Buffer->values, this.m_Index);
				return true;
			}
			key = default(TKey);
			value = default(TValue);
			return false;
		}

		// Token: 0x040004FB RID: 1275
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040004FC RID: 1276
		internal int m_Index;

		// Token: 0x040004FD RID: 1277
		internal int m_Next;
	}
}
