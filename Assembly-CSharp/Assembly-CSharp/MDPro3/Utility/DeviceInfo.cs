using System;
using UnityEngine;

namespace MDPro3.Utility
{
	// Token: 0x020012BD RID: 4797
	public static class DeviceInfo
	{
		// Token: 0x06008C6C RID: 35948 RVA: 0x001241DA File Offset: 0x001223DA
		public static bool OnMobile()
		{
			return SystemInfo.deviceName == "STEAMDECK";
		}

		// Token: 0x06008C6D RID: 35949 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool OnAndroid()
		{
			return false;
		}

		// Token: 0x020012BE RID: 4798
		public enum Platform
		{
			// Token: 0x0400CA51 RID: 51793
			Unknown,
			// Token: 0x0400CA52 RID: 51794
			PS4,
			// Token: 0x0400CA53 RID: 51795
			PS5,
			// Token: 0x0400CA54 RID: 51796
			XboxOne,
			// Token: 0x0400CA55 RID: 51797
			XboxSeriesX,
			// Token: 0x0400CA56 RID: 51798
			Switch,
			// Token: 0x0400CA57 RID: 51799
			Android,
			// Token: 0x0400CA58 RID: 51800
			iOS,
			// Token: 0x0400CA59 RID: 51801
			PC,
			// Token: 0x0400CA5A RID: 51802
			Stadia,
			// Token: 0x0400CA5B RID: 51803
			Mac
		}

		// Token: 0x020012BF RID: 4799
		public enum PlatformType
		{
			// Token: 0x0400CA5D RID: 51805
			Unknown,
			// Token: 0x0400CA5E RID: 51806
			Console,
			// Token: 0x0400CA5F RID: 51807
			Mobile,
			// Token: 0x0400CA60 RID: 51808
			PC
		}
	}
}
