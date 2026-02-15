using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x0200004B RID: 75
	public struct DoubleRewindableAllocators : IDisposable
	{
		// Token: 0x060001EB RID: 491 RVA: 0x000069AC File Offset: 0x00004BAC
		public unsafe void Update()
		{
			RewindableAllocator* UpdateAllocator0 = (RewindableAllocator*)UnsafeUtility.AddressOf<RewindableAllocator>(this.UpdateAllocatorHelper0.Allocator);
			RewindableAllocator* UpdateAllocator = (RewindableAllocator*)UnsafeUtility.AddressOf<RewindableAllocator>(this.UpdateAllocatorHelper1.Allocator);
			this.Pointer = ((this.Pointer == UpdateAllocator0) ? UpdateAllocator : UpdateAllocator0);
			this.Allocator.Rewind();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000069F9 File Offset: 0x00004BF9
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIsCreated()
		{
			if (!this.IsCreated)
			{
				throw new InvalidOperationException("DoubleRewindableAllocators is not created.");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006A0E File Offset: 0x00004C0E
		public unsafe ref RewindableAllocator Allocator
		{
			get
			{
				return UnsafeUtility.AsRef<RewindableAllocator>((void*)this.Pointer);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00006A1B File Offset: 0x00004C1B
		public bool IsCreated
		{
			get
			{
				return this.Pointer != null;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006A2A File Offset: 0x00004C2A
		public DoubleRewindableAllocators(AllocatorManager.AllocatorHandle backingAllocator, int initialSizeInBytes)
		{
			this = default(DoubleRewindableAllocators);
			this.Initialize(backingAllocator, initialSizeInBytes);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006A3C File Offset: 0x00004C3C
		public void Initialize(AllocatorManager.AllocatorHandle backingAllocator, int initialSizeInBytes)
		{
			this.UpdateAllocatorHelper0 = new AllocatorHelper<RewindableAllocator>(backingAllocator, false, 0);
			this.UpdateAllocatorHelper1 = new AllocatorHelper<RewindableAllocator>(backingAllocator, false, 0);
			this.UpdateAllocatorHelper0.Allocator.Initialize(initialSizeInBytes, false);
			this.UpdateAllocatorHelper1.Allocator.Initialize(initialSizeInBytes, false);
			this.Pointer = null;
			this.Update();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006A98 File Offset: 0x00004C98
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.UpdateAllocatorHelper0.Allocator.Dispose();
			this.UpdateAllocatorHelper1.Allocator.Dispose();
			this.UpdateAllocatorHelper0.Dispose();
			this.UpdateAllocatorHelper1.Dispose();
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006AE4 File Offset: 0x00004CE4
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00006AF6 File Offset: 0x00004CF6
		internal bool EnableBlockFree
		{
			get
			{
				return this.UpdateAllocatorHelper0.Allocator.EnableBlockFree;
			}
			set
			{
				this.UpdateAllocatorHelper0.Allocator.EnableBlockFree = value;
				this.UpdateAllocatorHelper1.Allocator.EnableBlockFree = value;
			}
		}

		// Token: 0x040000BB RID: 187
		private unsafe RewindableAllocator* Pointer;

		// Token: 0x040000BC RID: 188
		private AllocatorHelper<RewindableAllocator> UpdateAllocatorHelper0;

		// Token: 0x040000BD RID: 189
		private AllocatorHelper<RewindableAllocator> UpdateAllocatorHelper1;
	}
}
