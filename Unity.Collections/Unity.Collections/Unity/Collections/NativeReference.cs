using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000BB RID: 187
	[NativeContainer]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeReference<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEquatable<NativeReference<T>> where T : struct, ValueType
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0001ADC7 File Offset: 0x00018FC7
		public NativeReference(AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeReference<T>.Allocate(allocator, out this);
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(this.m_Data, (long)UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001ADE5 File Offset: 0x00018FE5
		public unsafe NativeReference(T value, AllocatorManager.AllocatorHandle allocator)
		{
			NativeReference<T>.Allocate(allocator, out this);
			*(T*)this.m_Data = value;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001ADFA File Offset: 0x00018FFA
		private static void Allocate(AllocatorManager.AllocatorHandle allocator, out NativeReference<T> reference)
		{
			reference = default(NativeReference<T>);
			reference.m_Data = Memory.Unmanaged.Allocate((long)UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), allocator);
			reference.m_AllocatorLabel = allocator;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001AE21 File Offset: 0x00019021
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0001AE2E File Offset: 0x0001902E
		public unsafe T Value
		{
			get
			{
				return *(T*)this.m_Data;
			}
			set
			{
				*(T*)this.m_Data = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0001AE3C File Offset: 0x0001903C
		public readonly bool IsCreated
		{
			get
			{
				return this.m_Data != null;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001AE4B File Offset: 0x0001904B
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			if (CollectionHelper.ShouldDeallocate(this.m_AllocatorLabel))
			{
				Memory.Unmanaged.Free(this.m_Data, this.m_AllocatorLabel);
				this.m_AllocatorLabel = Allocator.Invalid;
			}
			this.m_Data = null;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001AE88 File Offset: 0x00019088
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			if (CollectionHelper.ShouldDeallocate(this.m_AllocatorLabel))
			{
				JobHandle jobHandle = new NativeReferenceDisposeJob
				{
					Data = new NativeReferenceDispose
					{
						m_Data = this.m_Data,
						m_AllocatorLabel = this.m_AllocatorLabel
					}
				}.Schedule(inputDeps);
				this.m_Data = null;
				this.m_AllocatorLabel = Allocator.Invalid;
				return jobHandle;
			}
			this.m_Data = null;
			return inputDeps;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001AF03 File Offset: 0x00019103
		public void CopyFrom(NativeReference<T> reference)
		{
			NativeReference<T>.Copy(this, reference);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001AF11 File Offset: 0x00019111
		public void CopyTo(NativeReference<T> reference)
		{
			NativeReference<T>.Copy(reference, this);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001AF20 File Offset: 0x00019120
		[ExcludeFromBurstCompatTesting("Equals boxes because Value does not implement IEquatable<T>")]
		public bool Equals(NativeReference<T> other)
		{
			T value = this.Value;
			return value.Equals(other.Value);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001AF4D File Offset: 0x0001914D
		[ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object obj)
		{
			return obj != null && obj is NativeReference<T> && this.Equals((NativeReference<T>)obj);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001AF6C File Offset: 0x0001916C
		public override int GetHashCode()
		{
			T value = this.Value;
			return value.GetHashCode();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001AF8D File Offset: 0x0001918D
		public static bool operator ==(NativeReference<T> left, NativeReference<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001AF97 File Offset: 0x00019197
		public static bool operator !=(NativeReference<T> left, NativeReference<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001AFA4 File Offset: 0x000191A4
		public static void Copy(NativeReference<T> dst, NativeReference<T> src)
		{
			UnsafeUtility.MemCpy(dst.m_Data, src.m_Data, (long)UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001AFBD File Offset: 0x000191BD
		public NativeReference<T>.ReadOnly AsReadOnly()
		{
			return new NativeReference<T>.ReadOnly(this.m_Data);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001AFCA File Offset: 0x000191CA
		public static implicit operator NativeReference<T>.ReadOnly(NativeReference<T> nativeReference)
		{
			return nativeReference.AsReadOnly();
		}

		// Token: 0x040003E4 RID: 996
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Data;

		// Token: 0x040003E5 RID: 997
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x020000BC RID: 188
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly
		{
			// Token: 0x060008EB RID: 2283 RVA: 0x0001AFD3 File Offset: 0x000191D3
			internal unsafe ReadOnly(void* data)
			{
				this.m_Data = data;
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001AFDC File Offset: 0x000191DC
			public unsafe T Value
			{
				get
				{
					return *(T*)this.m_Data;
				}
			}

			// Token: 0x040003E6 RID: 998
			[NativeDisableUnsafePtrRestriction]
			private unsafe readonly void* m_Data;
		}
	}
}
