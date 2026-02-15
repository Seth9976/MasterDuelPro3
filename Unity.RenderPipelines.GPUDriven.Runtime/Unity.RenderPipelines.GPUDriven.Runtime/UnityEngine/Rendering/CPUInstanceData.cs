using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000068 RID: 104
	internal struct CPUInstanceData : IDisposable
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000BAD2 File Offset: 0x00009CD2
		public int instancesLength
		{
			get
			{
				return this.m_StructData[0];
			}
			set
			{
				this.m_StructData[0] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000BAE1 File Offset: 0x00009CE1
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000BAEF File Offset: 0x00009CEF
		public int instancesCapacity
		{
			get
			{
				return this.m_StructData[1];
			}
			set
			{
				this.m_StructData[1] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000BAFE File Offset: 0x00009CFE
		public int handlesLength
		{
			get
			{
				return this.m_InstanceIndices.Length;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public void Initialize(int initCapacity)
		{
			this.m_StructData = new NativeArray<int>(2, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.instancesCapacity = initCapacity;
			this.m_InstanceIndices = new NativeList<int>(Allocator.Persistent);
			this.instances = new NativeArray<InstanceHandle>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			(ref this.instances).FillArray(in InstanceHandle.Invalid, 0, -1);
			this.sharedInstances = new NativeArray<SharedInstanceHandle>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			(ref this.sharedInstances).FillArray(in SharedInstanceHandle.Invalid, 0, -1);
			this.localToWorldIsFlippedBits = new ParallelBitArray(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.worldAABBs = new NativeArray<AABB>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.tetrahedronCacheIndices = new NativeArray<int>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			int num = -1;
			(ref this.tetrahedronCacheIndices).FillArray(in num, 0, -1);
			this.movedInCurrentFrameBits = new ParallelBitArray(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.movedInPreviousFrameBits = new ParallelBitArray(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.visibleInPreviousFrameBits = new ParallelBitArray(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.editorData.Initialize(initCapacity);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000BC18 File Offset: 0x00009E18
		public void Dispose()
		{
			this.m_StructData.Dispose();
			this.m_InstanceIndices.Dispose();
			this.instances.Dispose();
			this.sharedInstances.Dispose();
			this.localToWorldIsFlippedBits.Dispose();
			this.worldAABBs.Dispose();
			this.tetrahedronCacheIndices.Dispose();
			this.movedInCurrentFrameBits.Dispose();
			this.movedInPreviousFrameBits.Dispose();
			this.visibleInPreviousFrameBits.Dispose();
			this.editorData.Dispose();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		private void Grow(int newCapacity)
		{
			(ref this.instances).ResizeArray(newCapacity);
			(ref this.instances).FillArray(in InstanceHandle.Invalid, this.instancesCapacity, -1);
			(ref this.sharedInstances).ResizeArray(newCapacity);
			(ref this.sharedInstances).FillArray(in SharedInstanceHandle.Invalid, this.instancesCapacity, -1);
			this.localToWorldIsFlippedBits.Resize(newCapacity);
			(ref this.worldAABBs).ResizeArray(newCapacity);
			(ref this.tetrahedronCacheIndices).ResizeArray(newCapacity);
			int num = -1;
			(ref this.tetrahedronCacheIndices).FillArray(in num, this.instancesCapacity, -1);
			this.movedInCurrentFrameBits.Resize(newCapacity);
			this.movedInPreviousFrameBits.Resize(newCapacity);
			this.visibleInPreviousFrameBits.Resize(newCapacity);
			this.editorData.Grow(newCapacity);
			this.instancesCapacity = newCapacity;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000BD64 File Offset: 0x00009F64
		private void AddUnsafe(InstanceHandle instance)
		{
			if (instance.index >= this.m_InstanceIndices.Length)
			{
				int length = this.m_InstanceIndices.Length;
				this.m_InstanceIndices.ResizeUninitialized(instance.index + 1);
				for (int i = length; i < this.m_InstanceIndices.Length - 1; i++)
				{
					this.m_InstanceIndices[i] = -1;
				}
			}
			this.m_InstanceIndices[instance.index] = this.instancesLength;
			this.instances[this.instancesLength] = instance;
			int num = this.instancesLength + 1;
			this.instancesLength = num;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000BE02 File Offset: 0x0000A002
		public int InstanceToIndex(InstanceHandle instance)
		{
			return this.m_InstanceIndices[instance.index];
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BE16 File Offset: 0x0000A016
		public InstanceHandle IndexToInstance(int index)
		{
			return this.instances[index];
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000BE24 File Offset: 0x0000A024
		public bool IsValidInstance(InstanceHandle instance)
		{
			if (instance.valid && instance.index < this.m_InstanceIndices.Length)
			{
				int index = this.m_InstanceIndices[instance.index];
				return index >= 0 && index < this.instancesLength && this.instances[index].Equals(instance);
			}
			return false;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000BE87 File Offset: 0x0000A087
		public bool IsFreeInstanceHandle(InstanceHandle instance)
		{
			return instance.valid && (instance.index >= this.m_InstanceIndices.Length || this.m_InstanceIndices[instance.index] == -1);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.instancesLength && index == this.m_InstanceIndices[this.instances[index].index];
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		public int GetFreeInstancesCount()
		{
			return this.instancesCapacity - this.instancesLength;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000BF10 File Offset: 0x0000A110
		public void EnsureFreeInstances(int instancesCount)
		{
			int freeInstancesCount = this.GetFreeInstancesCount();
			int needInstances = instancesCount - freeInstancesCount;
			if (needInstances > 0)
			{
				this.Grow(this.instancesCapacity + needInstances + 256);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000BF40 File Offset: 0x0000A140
		public void AddNoGrow(InstanceHandle instance)
		{
			this.AddUnsafe(instance);
			this.SetDefault(instance);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000BF50 File Offset: 0x0000A150
		public void Add(InstanceHandle instance)
		{
			this.EnsureFreeInstances(1);
			this.AddNoGrow(instance);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000BF60 File Offset: 0x0000A160
		public void Remove(InstanceHandle instance)
		{
			int index = this.InstanceToIndex(instance);
			int lastIndex = this.instancesLength - 1;
			this.instances[index] = this.instances[lastIndex];
			this.sharedInstances[index] = this.sharedInstances[lastIndex];
			this.localToWorldIsFlippedBits.Set(index, this.localToWorldIsFlippedBits.Get(lastIndex));
			this.worldAABBs[index] = this.worldAABBs[lastIndex];
			this.tetrahedronCacheIndices[index] = this.tetrahedronCacheIndices[lastIndex];
			this.movedInCurrentFrameBits.Set(index, this.movedInCurrentFrameBits.Get(lastIndex));
			this.movedInPreviousFrameBits.Set(index, this.movedInPreviousFrameBits.Get(lastIndex));
			this.visibleInPreviousFrameBits.Set(index, this.visibleInPreviousFrameBits.Get(lastIndex));
			this.editorData.Remove(index, lastIndex);
			this.m_InstanceIndices[this.instances[lastIndex].index] = index;
			this.m_InstanceIndices[instance.index] = -1;
			this.instancesLength--;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C08C File Offset: 0x0000A28C
		public void Set(InstanceHandle instance, SharedInstanceHandle sharedInstance, bool localToWorldIsFlipped, in AABB worldAABB, int tetrahedronCacheIndex, bool movedInCurrentFrame, bool movedInPreviousFrame, bool visibleInPreviousFrame)
		{
			int index = this.InstanceToIndex(instance);
			this.sharedInstances[index] = sharedInstance;
			this.localToWorldIsFlippedBits.Set(index, localToWorldIsFlipped);
			this.worldAABBs[index] = worldAABB;
			this.tetrahedronCacheIndices[index] = tetrahedronCacheIndex;
			this.movedInCurrentFrameBits.Set(index, movedInCurrentFrame);
			this.movedInPreviousFrameBits.Set(index, movedInPreviousFrame);
			this.visibleInPreviousFrameBits.Set(index, visibleInPreviousFrame);
			this.editorData.SetDefault(index);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C114 File Offset: 0x0000A314
		public void SetDefault(InstanceHandle instance)
		{
			SharedInstanceHandle invalid = SharedInstanceHandle.Invalid;
			bool flag = false;
			AABB aabb = default(AABB);
			this.Set(instance, invalid, flag, in aabb, -1, false, false, false);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C13B File Offset: 0x0000A33B
		public SharedInstanceHandle Get_SharedInstance(InstanceHandle instance)
		{
			return this.sharedInstances[this.InstanceToIndex(instance)];
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C14F File Offset: 0x0000A34F
		public bool Get_LocalToWorldIsFlipped(InstanceHandle instance)
		{
			return this.localToWorldIsFlippedBits.Get(this.InstanceToIndex(instance));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C163 File Offset: 0x0000A363
		public AABB Get_WorldAABB(InstanceHandle instance)
		{
			return this.worldAABBs[this.InstanceToIndex(instance)];
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C177 File Offset: 0x0000A377
		public int Get_TetrahedronCacheIndex(InstanceHandle instance)
		{
			return this.tetrahedronCacheIndices[this.InstanceToIndex(instance)];
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C18B File Offset: 0x0000A38B
		public ref AABB Get_WorldBounds(InstanceHandle instance)
		{
			return UnsafeUtility.ArrayElementAsRef<AABB>(this.worldAABBs.GetUnsafePtr<AABB>(), this.InstanceToIndex(instance));
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		public bool Get_MovedInCurrentFrame(InstanceHandle instance)
		{
			return this.movedInCurrentFrameBits.Get(this.InstanceToIndex(instance));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public bool Get_MovedInPreviousFrame(InstanceHandle instance)
		{
			return this.movedInPreviousFrameBits.Get(this.InstanceToIndex(instance));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public bool Get_VisibleInPreviousFrame(InstanceHandle instance)
		{
			return this.visibleInPreviousFrameBits.Get(this.InstanceToIndex(instance));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public void Set_SharedInstance(InstanceHandle instance, SharedInstanceHandle sharedInstance)
		{
			this.sharedInstances[this.InstanceToIndex(instance)] = sharedInstance;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public void Set_LocalToWorldIsFlipped(InstanceHandle instance, bool isFlipped)
		{
			this.localToWorldIsFlippedBits.Set(this.InstanceToIndex(instance), isFlipped);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C20A File Offset: 0x0000A40A
		public void Set_WorldAABB(InstanceHandle instance, in AABB worldBounds)
		{
			this.worldAABBs[this.InstanceToIndex(instance)] = worldBounds;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C224 File Offset: 0x0000A424
		public void Set_TetrahedronCacheIndex(InstanceHandle instance, int tetrahedronCacheIndex)
		{
			this.tetrahedronCacheIndices[this.InstanceToIndex(instance)] = tetrahedronCacheIndex;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C239 File Offset: 0x0000A439
		public void Set_MovedInCurrentFrame(InstanceHandle instance, bool movedInCurrentFrame)
		{
			this.movedInCurrentFrameBits.Set(this.InstanceToIndex(instance), movedInCurrentFrame);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C24E File Offset: 0x0000A44E
		public void Set_MovedInPreviousFrame(InstanceHandle instance, bool movedInPreviousFrame)
		{
			this.movedInPreviousFrameBits.Set(this.InstanceToIndex(instance), movedInPreviousFrame);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C263 File Offset: 0x0000A463
		public void Set_VisibleInPreviousFrame(InstanceHandle instance, bool visibleInPreviousFrame)
		{
			this.visibleInPreviousFrameBits.Set(this.InstanceToIndex(instance), visibleInPreviousFrame);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C278 File Offset: 0x0000A478
		public CPUInstanceData.ReadOnly AsReadOnly()
		{
			return new CPUInstanceData.ReadOnly(in this);
		}

		// Token: 0x040001F0 RID: 496
		private const int k_InvalidIndex = -1;

		// Token: 0x040001F1 RID: 497
		private NativeArray<int> m_StructData;

		// Token: 0x040001F2 RID: 498
		private NativeList<int> m_InstanceIndices;

		// Token: 0x040001F3 RID: 499
		public NativeArray<InstanceHandle> instances;

		// Token: 0x040001F4 RID: 500
		public NativeArray<SharedInstanceHandle> sharedInstances;

		// Token: 0x040001F5 RID: 501
		public ParallelBitArray localToWorldIsFlippedBits;

		// Token: 0x040001F6 RID: 502
		public NativeArray<AABB> worldAABBs;

		// Token: 0x040001F7 RID: 503
		public NativeArray<int> tetrahedronCacheIndices;

		// Token: 0x040001F8 RID: 504
		public ParallelBitArray movedInCurrentFrameBits;

		// Token: 0x040001F9 RID: 505
		public ParallelBitArray movedInPreviousFrameBits;

		// Token: 0x040001FA RID: 506
		public ParallelBitArray visibleInPreviousFrameBits;

		// Token: 0x040001FB RID: 507
		public EditorInstanceDataArrays editorData;

		// Token: 0x02000069 RID: 105
		internal readonly struct ReadOnly
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000C280 File Offset: 0x0000A480
			public int handlesLength
			{
				get
				{
					return this.instanceIndices.Length;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000C29C File Offset: 0x0000A49C
			public int instancesLength
			{
				get
				{
					return this.instances.Length;
				}
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
			public ReadOnly(in CPUInstanceData instanceData)
			{
				NativeList<int> nativeList = instanceData.m_InstanceIndices;
				this.instanceIndices = nativeList.AsArray().AsReadOnly();
				NativeArray<InstanceHandle> nativeArray = instanceData.instances;
				int num = 0;
				CPUInstanceData cpuinstanceData = instanceData;
				this.instances = nativeArray.GetSubArray(num, cpuinstanceData.instancesLength).AsReadOnly();
				NativeArray<SharedInstanceHandle> nativeArray2 = instanceData.sharedInstances;
				int num2 = 0;
				cpuinstanceData = instanceData;
				this.sharedInstances = nativeArray2.GetSubArray(num2, cpuinstanceData.instancesLength).AsReadOnly();
				ParallelBitArray parallelBitArray = instanceData.localToWorldIsFlippedBits;
				cpuinstanceData = instanceData;
				this.localToWorldIsFlippedBits = parallelBitArray.GetSubArray(cpuinstanceData.instancesLength);
				NativeArray<AABB> nativeArray3 = instanceData.worldAABBs;
				int num3 = 0;
				cpuinstanceData = instanceData;
				this.worldAABBs = nativeArray3.GetSubArray(num3, cpuinstanceData.instancesLength).AsReadOnly();
				NativeArray<int> nativeArray4 = instanceData.tetrahedronCacheIndices;
				int num4 = 0;
				cpuinstanceData = instanceData;
				this.tetrahedronCacheIndices = nativeArray4.GetSubArray(num4, cpuinstanceData.instancesLength).AsReadOnly();
				parallelBitArray = instanceData.movedInCurrentFrameBits;
				cpuinstanceData = instanceData;
				this.movedInCurrentFrameBits = parallelBitArray.GetSubArray(cpuinstanceData.instancesLength);
				parallelBitArray = instanceData.movedInPreviousFrameBits;
				cpuinstanceData = instanceData;
				this.movedInPreviousFrameBits = parallelBitArray.GetSubArray(cpuinstanceData.instancesLength);
				parallelBitArray = instanceData.visibleInPreviousFrameBits;
				cpuinstanceData = instanceData;
				this.visibleInPreviousFrameBits = parallelBitArray.GetSubArray(cpuinstanceData.instancesLength);
				this.editorData = new EditorInstanceDataArrays.ReadOnly(in instanceData);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x0000C42C File Offset: 0x0000A62C
			public int InstanceToIndex(InstanceHandle instance)
			{
				return this.instanceIndices[instance.index];
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x0000C450 File Offset: 0x0000A650
			public InstanceHandle IndexToInstance(int index)
			{
				return this.instances[index];
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x0000C46C File Offset: 0x0000A66C
			public bool IsValidInstance(InstanceHandle instance)
			{
				if (instance.valid && instance.index < this.instanceIndices.Length)
				{
					int index = this.instanceIndices[instance.index];
					return index >= 0 && index < this.instances.Length && this.instances[index].Equals(instance);
				}
				return false;
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
			public bool IsValidIndex(int index)
			{
				if (index >= 0 && index < this.instances.Length)
				{
					InstanceHandle instance = this.instances[index];
					return index == this.instanceIndices[instance.index];
				}
				return false;
			}

			// Token: 0x040001FC RID: 508
			public readonly NativeArray<int>.ReadOnly instanceIndices;

			// Token: 0x040001FD RID: 509
			public readonly NativeArray<InstanceHandle>.ReadOnly instances;

			// Token: 0x040001FE RID: 510
			public readonly NativeArray<SharedInstanceHandle>.ReadOnly sharedInstances;

			// Token: 0x040001FF RID: 511
			public readonly ParallelBitArray localToWorldIsFlippedBits;

			// Token: 0x04000200 RID: 512
			public readonly NativeArray<AABB>.ReadOnly worldAABBs;

			// Token: 0x04000201 RID: 513
			public readonly NativeArray<int>.ReadOnly tetrahedronCacheIndices;

			// Token: 0x04000202 RID: 514
			public readonly ParallelBitArray movedInCurrentFrameBits;

			// Token: 0x04000203 RID: 515
			public readonly ParallelBitArray movedInPreviousFrameBits;

			// Token: 0x04000204 RID: 516
			public readonly ParallelBitArray visibleInPreviousFrameBits;

			// Token: 0x04000205 RID: 517
			public readonly EditorInstanceDataArrays.ReadOnly editorData;
		}
	}
}
