using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000568 RID: 1384
	internal struct BMPAlloc
	{
		// Token: 0x060025FD RID: 9725 RVA: 0x0009731C File Offset: 0x0009551C
		public bool Equals(BMPAlloc other)
		{
			return this.page == other.page && this.pageLine == other.pageLine && this.bitIndex == other.bitIndex;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0009735C File Offset: 0x0009555C
		public bool IsValid()
		{
			return this.page >= 0;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0009737C File Offset: 0x0009557C
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", this.page, this.pageLine, this.bitIndex);
		}

		// Token: 0x04001331 RID: 4913
		public static readonly BMPAlloc Invalid = new BMPAlloc
		{
			page = -1
		};

		// Token: 0x04001332 RID: 4914
		public int page;

		// Token: 0x04001333 RID: 4915
		public ushort pageLine;

		// Token: 0x04001334 RID: 4916
		public byte bitIndex;

		// Token: 0x04001335 RID: 4917
		public OwnedState ownedState;
	}
}
