using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000066 RID: 102
	internal struct InstanceAllocator
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000B857 File Offset: 0x00009A57
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000B865 File Offset: 0x00009A65
		public int length
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000B874 File Offset: 0x00009A74
		public bool valid
		{
			get
			{
				return this.m_StructData.IsCreated;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000B881 File Offset: 0x00009A81
		public void Initialize(int baseInstanceOffset = 0, int instanceStride = 1)
		{
			this.m_StructData = new NativeArray<int>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_FreeInstances = new NativeList<int>(Allocator.Persistent);
			this.m_BaseInstanceOffset = baseInstanceOffset;
			this.m_InstanceStride = instanceStride;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public void Dispose()
		{
			this.m_StructData.Dispose();
			this.m_FreeInstances.Dispose();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public int AllocateInstance()
		{
			int instance;
			if (this.m_FreeInstances.Length > 0)
			{
				instance = this.m_FreeInstances[this.m_FreeInstances.Length - 1];
				this.m_FreeInstances.RemoveAtSwapBack(this.m_FreeInstances.Length - 1);
			}
			else
			{
				instance = this.length * this.m_InstanceStride + this.m_BaseInstanceOffset;
				this.length++;
			}
			return instance;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B93A File Offset: 0x00009B3A
		public void FreeInstance(int instance)
		{
			this.m_FreeInstances.Add(in instance);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000B949 File Offset: 0x00009B49
		public int GetNumAllocated()
		{
			return this.length - this.m_FreeInstances.Length;
		}

		// Token: 0x040001E9 RID: 489
		private NativeArray<int> m_StructData;

		// Token: 0x040001EA RID: 490
		private NativeList<int> m_FreeInstances;

		// Token: 0x040001EB RID: 491
		private int m_BaseInstanceOffset;

		// Token: 0x040001EC RID: 492
		private int m_InstanceStride;
	}
}
