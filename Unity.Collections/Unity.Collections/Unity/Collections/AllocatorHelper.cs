using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000032 RID: 50
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
	public struct AllocatorHelper<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType, AllocatorManager.IAllocator
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public unsafe ref T Allocator
		{
			get
			{
				return UnsafeUtility.AsRef<T>((void*)this.m_allocator);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003AC4 File Offset: 0x00001CC4
		[ExcludeFromBurstCompatTesting("CreateAllocator is unburstable")]
		public unsafe AllocatorHelper(AllocatorManager.AllocatorHandle backingAllocator, bool isGlobal = false, int globalIndex = 0)
		{
			ref T allocator = ref AllocatorManager.CreateAllocator<T>(backingAllocator, isGlobal, globalIndex);
			this.m_allocator = (T*)UnsafeUtility.AddressOf<T>(ref allocator);
			this.m_backingAllocator = backingAllocator;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003AED File Offset: 0x00001CED
		[ExcludeFromBurstCompatTesting("DestroyAllocator is unburstable")]
		public unsafe void Dispose()
		{
			UnsafeUtility.AsRef<T>((void*)this.m_allocator).DestroyAllocator(this.m_backingAllocator);
		}

		// Token: 0x04000075 RID: 117
		private unsafe readonly T* m_allocator;

		// Token: 0x04000076 RID: 118
		private AllocatorManager.AllocatorHandle m_backingAllocator;
	}
}
