using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000B RID: 11
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	public struct InputFeatureUsage : IEquatable<InputFeatureUsage>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
		internal InputFeatureType internalType
		{
			get
			{
				return this.m_InternalType;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage);
			return !flag && this.Equals((InputFeatureUsage)obj);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public bool Equals(InputFeatureUsage other)
		{
			return this.name == other.name && this.internalType == other.internalType;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ (this.internalType.GetHashCode() << 1);
		}

		// Token: 0x0400004C RID: 76
		internal string m_Name;

		// Token: 0x0400004D RID: 77
		[NativeName("m_FeatureType")]
		internal InputFeatureType m_InternalType;
	}
}
