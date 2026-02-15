using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000379 RID: 889
	public struct BatchPackedCullingViewID : IEquatable<BatchPackedCullingViewID>
	{
		// Token: 0x060018CB RID: 6347 RVA: 0x00035180 File Offset: 0x00033380
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000351A0 File Offset: 0x000333A0
		public bool Equals(BatchPackedCullingViewID other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000351C0 File Offset: 0x000333C0
		public override bool Equals(object obj)
		{
			bool flag = !(obj is BatchPackedCullingViewID);
			return !flag && this.Equals((BatchPackedCullingViewID)obj);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000351F4 File Offset: 0x000333F4
		public int GetInstanceID()
		{
			return (int)(this.handle & (ulong)(-1));
		}

		// Token: 0x04000A7F RID: 2687
		internal ulong handle;
	}
}
