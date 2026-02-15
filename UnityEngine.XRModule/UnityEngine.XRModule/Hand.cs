using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000D RID: 13
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	public struct Hand : IEquatable<Hand>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002294 File Offset: 0x00000494
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022AC File Offset: 0x000004AC
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C4 File Offset: 0x000004C4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Hand);
			return !flag && this.Equals((Hand)obj);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F8 File Offset: 0x000004F8
		public bool Equals(Hand other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000232C File Offset: 0x0000052C
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x04000050 RID: 80
		private ulong m_DeviceId;

		// Token: 0x04000051 RID: 81
		private uint m_FeatureIndex;
	}
}
