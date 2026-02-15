using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	public struct Eyes : IEquatable<Eyes>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002360 File Offset: 0x00000560
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002378 File Offset: 0x00000578
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002390 File Offset: 0x00000590
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Eyes);
			return !flag && this.Equals((Eyes)obj);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C4 File Offset: 0x000005C4
		public bool Equals(Eyes other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023F8 File Offset: 0x000005F8
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x04000052 RID: 82
		private ulong m_DeviceId;

		// Token: 0x04000053 RID: 83
		private uint m_FeatureIndex;
	}
}
