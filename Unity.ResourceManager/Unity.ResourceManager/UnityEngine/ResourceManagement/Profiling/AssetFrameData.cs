using System;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x02000071 RID: 113
	internal struct AssetFrameData
	{
		// Token: 0x06000279 RID: 633 RVA: 0x00009F4C File Offset: 0x0000814C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is AssetFrameData)
			{
				AssetFrameData other = (AssetFrameData)obj;
				return this.AssetCode == other.AssetCode && this.BundleCode == other.BundleCode;
			}
			return false;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009F90 File Offset: 0x00008190
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int, int, int, int>(this.AssetCode.GetHashCode(), this.BundleCode.GetHashCode(), this.ReferenceCount.GetHashCode(), this.PercentComplete.GetHashCode(), this.Status.GetHashCode());
		}

		// Token: 0x04000129 RID: 297
		public int AssetCode;

		// Token: 0x0400012A RID: 298
		public int BundleCode;

		// Token: 0x0400012B RID: 299
		public int ReferenceCount;

		// Token: 0x0400012C RID: 300
		public float PercentComplete;

		// Token: 0x0400012D RID: 301
		public ContentStatus Status;
	}
}
