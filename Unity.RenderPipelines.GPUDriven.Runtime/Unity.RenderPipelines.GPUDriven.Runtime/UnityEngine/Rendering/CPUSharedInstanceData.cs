using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006A RID: 106
	internal struct CPUSharedInstanceData : IDisposable
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000C52C File Offset: 0x0000A72C
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000C53A File Offset: 0x0000A73A
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000C549 File Offset: 0x0000A749
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000C557 File Offset: 0x0000A757
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000C566 File Offset: 0x0000A766
		public int handlesLength
		{
			get
			{
				return this.m_InstanceIndices.Length;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000C574 File Offset: 0x0000A774
		public void Initialize(int initCapacity)
		{
			this.m_StructData = new NativeArray<int>(2, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.instancesCapacity = initCapacity;
			this.m_InstanceIndices = new NativeList<int>(Allocator.Persistent);
			this.instances = new NativeArray<SharedInstanceHandle>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			(ref this.instances).FillArray(in SharedInstanceHandle.Invalid, 0, -1);
			this.rendererGroupIDs = new NativeArray<int>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.materialIDArrays = new NativeArray<SmallIntegerArray>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.meshIDs = new NativeArray<int>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.localAABBs = new NativeArray<AABB>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.flags = new NativeArray<CPUSharedInstanceFlags>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.lodGroupAndMasks = new NativeArray<uint>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			uint maxValue = uint.MaxValue;
			(ref this.lodGroupAndMasks).FillArray(in maxValue, 0, -1);
			this.gameObjectLayers = new NativeArray<int>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.refCounts = new NativeArray<int>(this.instancesCapacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C678 File Offset: 0x0000A878
		public void Dispose()
		{
			this.m_StructData.Dispose();
			this.m_InstanceIndices.Dispose();
			this.instances.Dispose();
			this.rendererGroupIDs.Dispose();
			foreach (SmallIntegerArray materialIDs in this.materialIDArrays)
			{
				materialIDs.Dispose();
			}
			this.materialIDArrays.Dispose();
			this.meshIDs.Dispose();
			this.localAABBs.Dispose();
			this.flags.Dispose();
			this.lodGroupAndMasks.Dispose();
			this.gameObjectLayers.Dispose();
			this.refCounts.Dispose();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C744 File Offset: 0x0000A944
		private void Grow(int newCapacity)
		{
			(ref this.instances).ResizeArray(newCapacity);
			(ref this.instances).FillArray(in SharedInstanceHandle.Invalid, this.instancesCapacity, -1);
			(ref this.rendererGroupIDs).ResizeArray(newCapacity);
			(ref this.materialIDArrays).ResizeArray(newCapacity);
			SmallIntegerArray smallIntegerArray = default(SmallIntegerArray);
			(ref this.materialIDArrays).FillArray(in smallIntegerArray, this.instancesCapacity, -1);
			(ref this.meshIDs).ResizeArray(newCapacity);
			(ref this.localAABBs).ResizeArray(newCapacity);
			(ref this.flags).ResizeArray(newCapacity);
			(ref this.lodGroupAndMasks).ResizeArray(newCapacity);
			uint maxValue = uint.MaxValue;
			(ref this.lodGroupAndMasks).FillArray(in maxValue, this.instancesCapacity, -1);
			(ref this.gameObjectLayers).ResizeArray(newCapacity);
			(ref this.refCounts).ResizeArray(newCapacity);
			this.instancesCapacity = newCapacity;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C80C File Offset: 0x0000AA0C
		private void AddUnsafe(SharedInstanceHandle instance)
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

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C8AA File Offset: 0x0000AAAA
		public int SharedInstanceToIndex(SharedInstanceHandle instance)
		{
			return this.m_InstanceIndices[instance.index];
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C8BE File Offset: 0x0000AABE
		public SharedInstanceHandle IndexToSharedInstance(int index)
		{
			return this.instances[index];
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public int InstanceToIndex(in CPUInstanceData instanceData, InstanceHandle instance)
		{
			CPUInstanceData cpuinstanceData = instanceData;
			int instanceIndex = cpuinstanceData.InstanceToIndex(instance);
			NativeArray<SharedInstanceHandle> sharedInstances = instanceData.sharedInstances;
			SharedInstanceHandle sharedInstance = sharedInstances[instanceIndex];
			return this.SharedInstanceToIndex(sharedInstance);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C900 File Offset: 0x0000AB00
		public bool IsValidInstance(SharedInstanceHandle instance)
		{
			if (instance.valid && instance.index < this.m_InstanceIndices.Length)
			{
				int index = this.m_InstanceIndices[instance.index];
				return index >= 0 && index < this.instancesLength && this.instances[index].Equals(instance);
			}
			return false;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C963 File Offset: 0x0000AB63
		public bool IsFreeInstanceHandle(SharedInstanceHandle instance)
		{
			return instance.valid && (instance.index >= this.m_InstanceIndices.Length || this.m_InstanceIndices[instance.index] == -1);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.instancesLength && index == this.m_InstanceIndices[this.instances[index].index];
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		public int GetFreeInstancesCount()
		{
			return this.instancesCapacity - this.instancesLength;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		public void EnsureFreeInstances(int instancesCount)
		{
			int freeInstancesCount = this.GetFreeInstancesCount();
			int needInstances = instancesCount - freeInstancesCount;
			if (needInstances > 0)
			{
				this.Grow(this.instancesCapacity + needInstances + 256);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public void AddNoGrow(SharedInstanceHandle instance)
		{
			this.AddUnsafe(instance);
			this.SetDefault(instance);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		public void Add(SharedInstanceHandle instance)
		{
			this.EnsureFreeInstances(1);
			this.AddNoGrow(instance);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public void Remove(SharedInstanceHandle instance)
		{
			int index = this.SharedInstanceToIndex(instance);
			int lastIndex = this.instancesLength - 1;
			this.instances[index] = this.instances[lastIndex];
			this.rendererGroupIDs[index] = this.rendererGroupIDs[lastIndex];
			this.materialIDArrays[index].Dispose();
			this.materialIDArrays[index] = this.materialIDArrays[lastIndex];
			this.meshIDs[index] = this.meshIDs[lastIndex];
			this.localAABBs[index] = this.localAABBs[lastIndex];
			this.flags[index] = this.flags[lastIndex];
			this.lodGroupAndMasks[index] = this.lodGroupAndMasks[lastIndex];
			this.gameObjectLayers[index] = this.gameObjectLayers[lastIndex];
			this.refCounts[index] = this.refCounts[lastIndex];
			this.m_InstanceIndices[this.instances[lastIndex].index] = index;
			this.m_InstanceIndices[instance.index] = -1;
			this.instancesLength--;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000CB87 File Offset: 0x0000AD87
		public int Get_RendererGroupID(SharedInstanceHandle instance)
		{
			return this.rendererGroupIDs[this.SharedInstanceToIndex(instance)];
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000CB9B File Offset: 0x0000AD9B
		public int Get_MeshID(SharedInstanceHandle instance)
		{
			return this.meshIDs[this.SharedInstanceToIndex(instance)];
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CBAF File Offset: 0x0000ADAF
		public ref AABB Get_LocalAABB(SharedInstanceHandle instance)
		{
			return UnsafeUtility.ArrayElementAsRef<AABB>(this.localAABBs.GetUnsafePtr<AABB>(), this.SharedInstanceToIndex(instance));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		public CPUSharedInstanceFlags Get_Flags(SharedInstanceHandle instance)
		{
			return this.flags[this.SharedInstanceToIndex(instance)];
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		public uint Get_LODGroupAndMask(SharedInstanceHandle instance)
		{
			return this.lodGroupAndMasks[this.SharedInstanceToIndex(instance)];
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public int Get_GameObjectLayer(SharedInstanceHandle instance)
		{
			return this.gameObjectLayers[this.SharedInstanceToIndex(instance)];
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public int Get_RefCount(SharedInstanceHandle instance)
		{
			return this.refCounts[this.SharedInstanceToIndex(instance)];
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public ref SmallIntegerArray Get_MaterialIDs(SharedInstanceHandle instance)
		{
			return UnsafeUtility.ArrayElementAsRef<SmallIntegerArray>(this.materialIDArrays.GetUnsafePtr<SmallIntegerArray>(), this.SharedInstanceToIndex(instance));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CC31 File Offset: 0x0000AE31
		public void Set_RendererGroupID(SharedInstanceHandle instance, int rendererGroupID)
		{
			this.rendererGroupIDs[this.SharedInstanceToIndex(instance)] = rendererGroupID;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CC46 File Offset: 0x0000AE46
		public void Set_MeshID(SharedInstanceHandle instance, int meshID)
		{
			this.meshIDs[this.SharedInstanceToIndex(instance)] = meshID;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CC5B File Offset: 0x0000AE5B
		public void Set_LocalAABB(SharedInstanceHandle instance, in AABB localAABB)
		{
			this.localAABBs[this.SharedInstanceToIndex(instance)] = localAABB;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000CC75 File Offset: 0x0000AE75
		public void Set_Flags(SharedInstanceHandle instance, CPUSharedInstanceFlags instanceFlags)
		{
			this.flags[this.SharedInstanceToIndex(instance)] = instanceFlags;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000CC8A File Offset: 0x0000AE8A
		public void Set_LODGroupAndMask(SharedInstanceHandle instance, uint lodGroupAndMask)
		{
			this.lodGroupAndMasks[this.SharedInstanceToIndex(instance)] = lodGroupAndMask;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000CC9F File Offset: 0x0000AE9F
		public void Set_GameObjectLayer(SharedInstanceHandle instance, int gameObjectLayer)
		{
			this.gameObjectLayers[this.SharedInstanceToIndex(instance)] = gameObjectLayer;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public void Set_RefCount(SharedInstanceHandle instance, int refCount)
		{
			this.refCounts[this.SharedInstanceToIndex(instance)] = refCount;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000CCCC File Offset: 0x0000AECC
		public void Set_MaterialIDs(SharedInstanceHandle instance, in SmallIntegerArray materialIDs)
		{
			int index = this.SharedInstanceToIndex(instance);
			this.materialIDArrays[index].Dispose();
			this.materialIDArrays[index] = materialIDs;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000CD08 File Offset: 0x0000AF08
		public void Set(SharedInstanceHandle instance, int rendererGroupID, in SmallIntegerArray materialIDs, int meshID, in AABB localAABB, TransformUpdateFlags transformUpdateFlags, InstanceFlags instanceFlags, uint lodGroupAndMask, int gameObjectLayer, int refCount)
		{
			int index = this.SharedInstanceToIndex(instance);
			this.rendererGroupIDs[index] = rendererGroupID;
			this.materialIDArrays[index].Dispose();
			this.materialIDArrays[index] = materialIDs;
			this.meshIDs[index] = meshID;
			this.localAABBs[index] = localAABB;
			this.flags[index] = new CPUSharedInstanceFlags
			{
				transformUpdateFlags = transformUpdateFlags,
				instanceFlags = instanceFlags
			};
			this.lodGroupAndMasks[index] = lodGroupAndMask;
			this.gameObjectLayers[index] = gameObjectLayer;
			this.refCounts[index] = refCount;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public void SetDefault(SharedInstanceHandle instance)
		{
			int num = 0;
			SmallIntegerArray smallIntegerArray = default(SmallIntegerArray);
			int num2 = 0;
			AABB aabb = default(AABB);
			this.Set(instance, num, in smallIntegerArray, num2, in aabb, TransformUpdateFlags.None, InstanceFlags.None, uint.MaxValue, 0, 0);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		public CPUSharedInstanceData.ReadOnly AsReadOnly()
		{
			return new CPUSharedInstanceData.ReadOnly(in this);
		}

		// Token: 0x04000206 RID: 518
		private const int k_InvalidIndex = -1;

		// Token: 0x04000207 RID: 519
		private const uint k_InvalidLODGroupAndMask = 4294967295U;

		// Token: 0x04000208 RID: 520
		private NativeArray<int> m_StructData;

		// Token: 0x04000209 RID: 521
		private NativeList<int> m_InstanceIndices;

		// Token: 0x0400020A RID: 522
		public NativeArray<SharedInstanceHandle> instances;

		// Token: 0x0400020B RID: 523
		public NativeArray<int> rendererGroupIDs;

		// Token: 0x0400020C RID: 524
		public NativeArray<SmallIntegerArray> materialIDArrays;

		// Token: 0x0400020D RID: 525
		public NativeArray<int> meshIDs;

		// Token: 0x0400020E RID: 526
		public NativeArray<AABB> localAABBs;

		// Token: 0x0400020F RID: 527
		public NativeArray<CPUSharedInstanceFlags> flags;

		// Token: 0x04000210 RID: 528
		public NativeArray<uint> lodGroupAndMasks;

		// Token: 0x04000211 RID: 529
		public NativeArray<int> gameObjectLayers;

		// Token: 0x04000212 RID: 530
		public NativeArray<int> refCounts;

		// Token: 0x0200006B RID: 107
		internal readonly struct ReadOnly
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000211 RID: 529 RVA: 0x0000CDFC File Offset: 0x0000AFFC
			public int handlesLength
			{
				get
				{
					return this.instanceIndices.Length;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000212 RID: 530 RVA: 0x0000CE18 File Offset: 0x0000B018
			public int instancesLength
			{
				get
				{
					return this.instances.Length;
				}
			}

			// Token: 0x06000213 RID: 531 RVA: 0x0000CE34 File Offset: 0x0000B034
			public ReadOnly(in CPUSharedInstanceData instanceData)
			{
				NativeList<int> nativeList = instanceData.m_InstanceIndices;
				this.instanceIndices = nativeList.AsArray().AsReadOnly();
				NativeArray<SharedInstanceHandle> nativeArray = instanceData.instances;
				int num = 0;
				CPUSharedInstanceData cpusharedInstanceData = instanceData;
				this.instances = nativeArray.GetSubArray(num, cpusharedInstanceData.instancesLength).AsReadOnly();
				NativeArray<int> nativeArray2 = instanceData.rendererGroupIDs;
				int num2 = 0;
				cpusharedInstanceData = instanceData;
				this.rendererGroupIDs = nativeArray2.GetSubArray(num2, cpusharedInstanceData.instancesLength).AsReadOnly();
				NativeArray<SmallIntegerArray> nativeArray3 = instanceData.materialIDArrays;
				int num3 = 0;
				cpusharedInstanceData = instanceData;
				this.materialIDArrays = nativeArray3.GetSubArray(num3, cpusharedInstanceData.instancesLength).AsReadOnly();
				nativeArray2 = instanceData.meshIDs;
				int num4 = 0;
				cpusharedInstanceData = instanceData;
				this.meshIDs = nativeArray2.GetSubArray(num4, cpusharedInstanceData.instancesLength).AsReadOnly();
				NativeArray<AABB> nativeArray4 = instanceData.localAABBs;
				int num5 = 0;
				cpusharedInstanceData = instanceData;
				this.localAABBs = nativeArray4.GetSubArray(num5, cpusharedInstanceData.instancesLength).AsReadOnly();
				NativeArray<CPUSharedInstanceFlags> nativeArray5 = instanceData.flags;
				int num6 = 0;
				cpusharedInstanceData = instanceData;
				this.flags = nativeArray5.GetSubArray(num6, cpusharedInstanceData.instancesLength).AsReadOnly();
				NativeArray<uint> nativeArray6 = instanceData.lodGroupAndMasks;
				int num7 = 0;
				cpusharedInstanceData = instanceData;
				this.lodGroupAndMasks = nativeArray6.GetSubArray(num7, cpusharedInstanceData.instancesLength).AsReadOnly();
				nativeArray2 = instanceData.gameObjectLayers;
				int num8 = 0;
				cpusharedInstanceData = instanceData;
				this.gameObjectLayers = nativeArray2.GetSubArray(num8, cpusharedInstanceData.instancesLength).AsReadOnly();
				nativeArray2 = instanceData.refCounts;
				int num9 = 0;
				cpusharedInstanceData = instanceData;
				this.refCounts = nativeArray2.GetSubArray(num9, cpusharedInstanceData.instancesLength).AsReadOnly();
			}

			// Token: 0x06000214 RID: 532 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
			public int SharedInstanceToIndex(SharedInstanceHandle instance)
			{
				return this.instanceIndices[instance.index];
			}

			// Token: 0x06000215 RID: 533 RVA: 0x0000D00C File Offset: 0x0000B20C
			public SharedInstanceHandle IndexToSharedInstance(int index)
			{
				return this.instances[index];
			}

			// Token: 0x06000216 RID: 534 RVA: 0x0000D028 File Offset: 0x0000B228
			public bool IsValidSharedInstance(SharedInstanceHandle instance)
			{
				if (instance.valid && instance.index < this.instanceIndices.Length)
				{
					int index = this.instanceIndices[instance.index];
					return index >= 0 && index < this.instances.Length && this.instances[index].Equals(instance);
				}
				return false;
			}

			// Token: 0x06000217 RID: 535 RVA: 0x0000D09C File Offset: 0x0000B29C
			public bool IsValidIndex(int index)
			{
				if (index >= 0 && index < this.instances.Length)
				{
					SharedInstanceHandle instance = this.instances[index];
					return index == this.instanceIndices[instance.index];
				}
				return false;
			}

			// Token: 0x06000218 RID: 536 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
			public int InstanceToIndex(in CPUInstanceData.ReadOnly instanceData, InstanceHandle instance)
			{
				int instanceIndex = instanceData.InstanceToIndex(instance);
				SharedInstanceHandle sharedInstance = instanceData.sharedInstances[instanceIndex];
				return this.SharedInstanceToIndex(sharedInstance);
			}

			// Token: 0x04000213 RID: 531
			public readonly NativeArray<int>.ReadOnly instanceIndices;

			// Token: 0x04000214 RID: 532
			public readonly NativeArray<SharedInstanceHandle>.ReadOnly instances;

			// Token: 0x04000215 RID: 533
			public readonly NativeArray<int>.ReadOnly rendererGroupIDs;

			// Token: 0x04000216 RID: 534
			public readonly NativeArray<SmallIntegerArray>.ReadOnly materialIDArrays;

			// Token: 0x04000217 RID: 535
			public readonly NativeArray<int>.ReadOnly meshIDs;

			// Token: 0x04000218 RID: 536
			public readonly NativeArray<AABB>.ReadOnly localAABBs;

			// Token: 0x04000219 RID: 537
			public readonly NativeArray<CPUSharedInstanceFlags>.ReadOnly flags;

			// Token: 0x0400021A RID: 538
			public readonly NativeArray<uint>.ReadOnly lodGroupAndMasks;

			// Token: 0x0400021B RID: 539
			public readonly NativeArray<int>.ReadOnly gameObjectLayers;

			// Token: 0x0400021C RID: 540
			public readonly NativeArray<int>.ReadOnly refCounts;
		}
	}
}
