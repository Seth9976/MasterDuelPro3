using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000067 RID: 103
	internal struct InstanceAllocators
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000B960 File Offset: 0x00009B60
		public void Initialize()
		{
			this.m_InstanceAlloc_MeshRenderer = default(InstanceAllocator);
			this.m_InstanceAlloc_SpeedTree = default(InstanceAllocator);
			this.m_InstanceAlloc_MeshRenderer.Initialize(0, 2);
			this.m_InstanceAlloc_SpeedTree.Initialize(1, 2);
			this.m_SharedInstanceAlloc = default(InstanceAllocator);
			this.m_SharedInstanceAlloc.Initialize(0, 1);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public void Dispose()
		{
			this.m_InstanceAlloc_MeshRenderer.Dispose();
			this.m_InstanceAlloc_SpeedTree.Dispose();
			this.m_SharedInstanceAlloc.Dispose();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B9DB File Offset: 0x00009BDB
		private InstanceAllocator GetInstanceAllocator(InstanceType type)
		{
			if (type == InstanceType.MeshRenderer)
			{
				return this.m_InstanceAlloc_MeshRenderer;
			}
			if (type != InstanceType.SpeedTree)
			{
				throw new ArgumentException("Allocator for this type is not created.");
			}
			return this.m_InstanceAlloc_SpeedTree;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000BA00 File Offset: 0x00009C00
		public int GetInstanceHandlesLength(InstanceType type)
		{
			return this.GetInstanceAllocator(type).length;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000BA1C File Offset: 0x00009C1C
		public int GetInstancesLength(InstanceType type)
		{
			return this.GetInstanceAllocator(type).GetNumAllocated();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000BA38 File Offset: 0x00009C38
		public InstanceHandle AllocateInstance(InstanceType type)
		{
			return InstanceHandle.FromInt(this.GetInstanceAllocator(type).AllocateInstance());
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BA5C File Offset: 0x00009C5C
		public void FreeInstance(InstanceHandle instance)
		{
			this.GetInstanceAllocator(instance.type).FreeInstance(instance.index);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BA88 File Offset: 0x00009C88
		public SharedInstanceHandle AllocateSharedInstance()
		{
			return new SharedInstanceHandle
			{
				index = this.m_SharedInstanceAlloc.AllocateInstance()
			};
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public void FreeSharedInstance(SharedInstanceHandle instance)
		{
			this.m_SharedInstanceAlloc.FreeInstance(instance.index);
		}

		// Token: 0x040001ED RID: 493
		private InstanceAllocator m_InstanceAlloc_MeshRenderer;

		// Token: 0x040001EE RID: 494
		private InstanceAllocator m_InstanceAlloc_SpeedTree;

		// Token: 0x040001EF RID: 495
		private InstanceAllocator m_SharedInstanceAlloc;
	}
}
