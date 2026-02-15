using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039F RID: 927
	[UsedByNativeCode]
	internal struct CoreCameraValues : IEquatable<CoreCameraValues>
	{
		// Token: 0x06001945 RID: 6469 RVA: 0x0003667C File Offset: 0x0003487C
		public bool Equals(CoreCameraValues other)
		{
			return this.filterMode == other.filterMode && this.cullingMask == other.cullingMask && this.instanceID == other.instanceID;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000366BC File Offset: 0x000348BC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CoreCameraValues && this.Equals((CoreCameraValues)obj);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000366F4 File Offset: 0x000348F4
		public override int GetHashCode()
		{
			int hashCode = this.filterMode;
			hashCode = (hashCode * 397) ^ (int)this.cullingMask;
			return (hashCode * 397) ^ this.instanceID;
		}

		// Token: 0x04000B8D RID: 2957
		private int filterMode;

		// Token: 0x04000B8E RID: 2958
		private uint cullingMask;

		// Token: 0x04000B8F RID: 2959
		private int instanceID;
	}
}
