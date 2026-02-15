using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000C RID: 12
	[NativeConditional("ENABLE_VR")]
	[UsedByNativeCode]
	public struct InputDevice : IEquatable<InputDevice>
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021E4 File Offset: 0x000003E4
		internal InputDevice(ulong deviceId)
		{
			this.m_DeviceId = deviceId;
			this.m_Initialized = true;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021F8 File Offset: 0x000003F8
		private ulong deviceId
		{
			get
			{
				return this.m_Initialized ? this.m_DeviceId : ulong.MaxValue;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputDevice);
			return !flag && this.Equals((InputDevice)obj);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002250 File Offset: 0x00000450
		public bool Equals(InputDevice other)
		{
			return this.deviceId == other.deviceId;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002274 File Offset: 0x00000474
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode();
		}

		// Token: 0x0400004E RID: 78
		private ulong m_DeviceId;

		// Token: 0x0400004F RID: 79
		private bool m_Initialized;
	}
}
