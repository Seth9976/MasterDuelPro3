using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000A0 RID: 160
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeKeyValueArrays<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable where TKey : struct, ValueType where TValue : struct, ValueType
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000184A2 File Offset: 0x000166A2
		public int Length
		{
			get
			{
				return this.Keys.Length;
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000184AF File Offset: 0x000166AF
		public NativeKeyValueArrays(int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options)
		{
			this.Keys = CollectionHelper.CreateNativeArray<TKey>(length, allocator, options);
			this.Values = CollectionHelper.CreateNativeArray<TValue>(length, allocator, options);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000184CD File Offset: 0x000166CD
		public void Dispose()
		{
			this.Keys.Dispose();
			this.Values.Dispose();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000184E5 File Offset: 0x000166E5
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.Keys.Dispose(this.Values.Dispose(inputDeps));
		}

		// Token: 0x040003C5 RID: 965
		public NativeArray<TKey> Keys;

		// Token: 0x040003C6 RID: 966
		public NativeArray<TValue> Values;
	}
}
