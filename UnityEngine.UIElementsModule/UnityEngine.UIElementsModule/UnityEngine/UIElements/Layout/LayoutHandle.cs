using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200056D RID: 1389
	internal readonly struct LayoutHandle
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000987B4 File Offset: 0x000969B4
		public static LayoutHandle Undefined
		{
			get
			{
				return default(LayoutHandle);
			}
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000987CA File Offset: 0x000969CA
		internal LayoutHandle(int index, int version)
		{
			this.Index = index;
			this.Version = version;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000987DB File Offset: 0x000969DB
		public bool Equals(LayoutHandle other)
		{
			return this.Index == other.Index && this.Version == other.Version;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000987FC File Offset: 0x000969FC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is LayoutHandle)
			{
				LayoutHandle other = (LayoutHandle)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00098824 File Offset: 0x00096A24
		public override int GetHashCode()
		{
			return (this.Index * 397) ^ this.Version;
		}

		// Token: 0x0400135C RID: 4956
		public readonly int Index;

		// Token: 0x0400135D RID: 4957
		public readonly int Version;
	}
}
