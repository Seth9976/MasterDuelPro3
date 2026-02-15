using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000065 RID: 101
	internal struct GPUInstanceIndex : IEquatable<GPUInstanceIndex>, IComparable<GPUInstanceIndex>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B7D7 File Offset: 0x000099D7
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000B7DF File Offset: 0x000099DF
		public int index { readonly get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public bool valid
		{
			get
			{
				return this.index != -1;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B7F6 File Offset: 0x000099F6
		public bool Equals(GPUInstanceIndex other)
		{
			return this.index == other.index;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B808 File Offset: 0x00009A08
		public int CompareTo(GPUInstanceIndex other)
		{
			return this.index.CompareTo(other.index);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B82A File Offset: 0x00009A2A
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x040001E8 RID: 488
		public static readonly GPUInstanceIndex Invalid = new GPUInstanceIndex
		{
			index = -1
		};
	}
}
