using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public struct XRFeatureDescriptor
	{
		// Token: 0x04000580 RID: 1408
		public string name;

		// Token: 0x04000581 RID: 1409
		public List<UsageHint> usageHints;

		// Token: 0x04000582 RID: 1410
		public FeatureType featureType;

		// Token: 0x04000583 RID: 1411
		public uint customSize;
	}
}
