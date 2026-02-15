using System;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x020005B2 RID: 1458
	public class PlatformVisibleIcon : PlatformVisibleIconBase
	{
		// Token: 0x06002E01 RID: 11777 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool IsDispPlatform()
		{
			return false;
		}

		// Token: 0x04002BBB RID: 11195
		[EnumFlags]
		public DeviceInfo.Platform platformFlags;

		// Token: 0x04002BBC RID: 11196
		[EnumFlags]
		public SelectorManager.InputDevice inputDeviceFlags;
	}
}
