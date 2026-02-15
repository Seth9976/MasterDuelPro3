using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B6 RID: 182
	internal struct SilhouettePlaneCache : IDisposable
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00011E90 File Offset: 0x00010090
		public void Init()
		{
			this.m_SubviewIDToIndexMap = new NativeParallelHashMap<int, int>(16, Allocator.Persistent);
			this.m_SlotFreeList = new NativeList<int>(16, Allocator.Persistent);
			this.m_Slots = new NativeList<SilhouettePlaneCache.Slot>(16, Allocator.Persistent);
			this.m_PlaneStorage = new NativeList<Plane>(96, Allocator.Persistent);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011EE9 File Offset: 0x000100E9
		public void Dispose()
		{
			this.m_SubviewIDToIndexMap.Dispose();
			this.m_SlotFreeList.Dispose();
			this.m_Slots.Dispose();
			this.m_PlaneStorage.Dispose();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00011F18 File Offset: 0x00010118
		public void Update(int viewInstanceID, NativeArray<Plane> planes, int frameIndex)
		{
			int planeCount = Math.Min(planes.Length, 6);
			int slotIndex;
			if (!this.m_SubviewIDToIndexMap.TryGetValue(viewInstanceID, out slotIndex))
			{
				if (this.m_SlotFreeList.Length > 0)
				{
					slotIndex = this.m_SlotFreeList[this.m_SlotFreeList.Length - 1];
					this.m_SlotFreeList.Length = this.m_SlotFreeList.Length - 1;
				}
				else
				{
					if (this.m_Slots.Length == this.m_Slots.Capacity)
					{
						int newCapacity = this.m_Slots.Length + 8;
						this.m_Slots.SetCapacity(newCapacity);
						this.m_PlaneStorage.SetCapacity(newCapacity * 6);
					}
					slotIndex = this.m_Slots.Length;
					int newSlotCount = slotIndex + 1;
					this.m_Slots.ResizeUninitialized(newSlotCount);
					this.m_PlaneStorage.ResizeUninitialized(newSlotCount * 6);
				}
				this.m_SubviewIDToIndexMap.Add(viewInstanceID, slotIndex);
			}
			this.m_Slots[slotIndex] = new SilhouettePlaneCache.Slot(viewInstanceID, planeCount, frameIndex);
			this.m_PlaneStorage.AsArray().GetSubArray(slotIndex * 6, planeCount).CopyFrom(planes);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00012034 File Offset: 0x00010234
		public void FreeUnusedSlots(int frameIndex, int maximumAge)
		{
			for (int slotIndex = 0; slotIndex < this.m_Slots.Length; slotIndex++)
			{
				SilhouettePlaneCache.Slot slot = this.m_Slots[slotIndex];
				if (slot.isActive && frameIndex - slot.lastUsedFrameIndex > maximumAge)
				{
					slot.isActive = false;
					this.m_Slots[slotIndex] = slot;
					this.m_SubviewIDToIndexMap.Remove(slot.viewInstanceID);
					this.m_SlotFreeList.Add(in slotIndex);
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000120AC File Offset: 0x000102AC
		public NativeArray<Plane> GetSubArray(int viewInstanceID)
		{
			int planeOffset = 0;
			int planeCount = 0;
			int slotIndex;
			if (this.m_SubviewIDToIndexMap.TryGetValue(viewInstanceID, out slotIndex))
			{
				planeOffset = slotIndex * 6;
				planeCount = this.m_Slots[slotIndex].planeCount;
			}
			return this.m_PlaneStorage.AsArray().GetSubArray(planeOffset, planeCount);
		}

		// Token: 0x040003A2 RID: 930
		private const int kMaxSilhouettePlanes = 6;

		// Token: 0x040003A3 RID: 931
		private NativeParallelHashMap<int, int> m_SubviewIDToIndexMap;

		// Token: 0x040003A4 RID: 932
		private NativeList<int> m_SlotFreeList;

		// Token: 0x040003A5 RID: 933
		private NativeList<SilhouettePlaneCache.Slot> m_Slots;

		// Token: 0x040003A6 RID: 934
		private NativeList<Plane> m_PlaneStorage;

		// Token: 0x020000B7 RID: 183
		private struct Slot
		{
			// Token: 0x060002C2 RID: 706 RVA: 0x000120F8 File Offset: 0x000102F8
			public Slot(int viewInstanceID, int planeCount, int frameIndex)
			{
				this.isActive = true;
				this.viewInstanceID = viewInstanceID;
				this.planeCount = planeCount;
				this.lastUsedFrameIndex = frameIndex;
			}

			// Token: 0x040003A7 RID: 935
			public bool isActive;

			// Token: 0x040003A8 RID: 936
			public int viewInstanceID;

			// Token: 0x040003A9 RID: 937
			public int planeCount;

			// Token: 0x040003AA RID: 938
			public int lastUsedFrameIndex;
		}
	}
}
