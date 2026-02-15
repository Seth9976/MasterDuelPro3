using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000063 RID: 99
	internal struct InstanceHandle : IEquatable<InstanceHandle>, IComparable<InstanceHandle>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000B67D File Offset: 0x0000987D
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000B685 File Offset: 0x00009885
		public int index { readonly get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000B68E File Offset: 0x0000988E
		public int instanceIndex
		{
			get
			{
				return this.index >> 1;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000B698 File Offset: 0x00009898
		public InstanceType type
		{
			get
			{
				return (InstanceType)((long)this.index & 1L);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000B6A5 File Offset: 0x000098A5
		public bool valid
		{
			get
			{
				return this.index != -1;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public static InstanceHandle Create(int instanceIndex, InstanceType instanceType)
		{
			return new InstanceHandle
			{
				index = ((instanceIndex << 1) | (int)instanceType)
			};
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public static InstanceHandle FromInt(int value)
		{
			return new InstanceHandle
			{
				index = value
			};
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B6F6 File Offset: 0x000098F6
		public bool Equals(InstanceHandle other)
		{
			return this.index == other.index;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B708 File Offset: 0x00009908
		public int CompareTo(InstanceHandle other)
		{
			return this.index.CompareTo(other.index);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B72A File Offset: 0x0000992A
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x040001E4 RID: 484
		public static readonly InstanceHandle Invalid = new InstanceHandle
		{
			index = -1
		};
	}
}
