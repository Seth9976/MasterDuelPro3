using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000F RID: 15
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeConditional("ENABLE_VR")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	public struct Bone : IEquatable<Bone>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000242C File Offset: 0x0000062C
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002444 File Offset: 0x00000644
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000245C File Offset: 0x0000065C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Bone);
			return !flag && this.Equals((Bone)obj);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002490 File Offset: 0x00000690
		public bool Equals(Bone other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024C4 File Offset: 0x000006C4
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x04000054 RID: 84
		private ulong m_DeviceId;

		// Token: 0x04000055 RID: 85
		private uint m_FeatureIndex;
	}
}
