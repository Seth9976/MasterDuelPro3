using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000064 RID: 100
	internal struct SharedInstanceHandle : IEquatable<SharedInstanceHandle>, IComparable<SharedInstanceHandle>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000B757 File Offset: 0x00009957
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000B75F File Offset: 0x0000995F
		public int index { readonly get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000B768 File Offset: 0x00009968
		public bool valid
		{
			get
			{
				return this.index != -1;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B776 File Offset: 0x00009976
		public bool Equals(SharedInstanceHandle other)
		{
			return this.index == other.index;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B788 File Offset: 0x00009988
		public int CompareTo(SharedInstanceHandle other)
		{
			return this.index.CompareTo(other.index);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B7AA File Offset: 0x000099AA
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x040001E6 RID: 486
		public static readonly SharedInstanceHandle Invalid = new SharedInstanceHandle
		{
			index = -1
		};
	}
}
