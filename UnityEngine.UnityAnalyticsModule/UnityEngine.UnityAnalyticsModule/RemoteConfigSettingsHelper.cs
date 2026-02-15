using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	internal static class RemoteConfigSettingsHelper
	{
		// Token: 0x02000006 RID: 6
		[RequiredByNativeCode]
		internal enum Tag
		{
			// Token: 0x04000007 RID: 7
			kUnknown,
			// Token: 0x04000008 RID: 8
			kIntVal,
			// Token: 0x04000009 RID: 9
			kInt64Val,
			// Token: 0x0400000A RID: 10
			kUInt64Val,
			// Token: 0x0400000B RID: 11
			kDoubleVal,
			// Token: 0x0400000C RID: 12
			kBoolVal,
			// Token: 0x0400000D RID: 13
			kStringVal,
			// Token: 0x0400000E RID: 14
			kArrayVal,
			// Token: 0x0400000F RID: 15
			kMixedArrayVal,
			// Token: 0x04000010 RID: 16
			kMapVal,
			// Token: 0x04000011 RID: 17
			kMaxTags
		}
	}
}
