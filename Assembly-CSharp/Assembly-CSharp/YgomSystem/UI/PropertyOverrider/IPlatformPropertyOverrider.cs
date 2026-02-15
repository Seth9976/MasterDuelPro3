using System;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x0200065D RID: 1629
	public interface IPlatformPropertyOverrider
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600330E RID: 13070
		// (set) Token: 0x0600330F RID: 13071
		OverrideMode overrideMode { get; set; }

		// Token: 0x06003310 RID: 13072
		void ApplyImmediate();

		// Token: 0x06003311 RID: 13073
		void ApplyImmediate(DeviceInfo.PlatformType platformType);

		// Token: 0x06003312 RID: 13074
		void Import();

		// Token: 0x06003313 RID: 13075
		void Import(DeviceInfo.PlatformType platformType);

		// Token: 0x06003314 RID: 13076
		void Export();

		// Token: 0x06003315 RID: 13077
		void Export(DeviceInfo.PlatformType platformType);
	}
}
