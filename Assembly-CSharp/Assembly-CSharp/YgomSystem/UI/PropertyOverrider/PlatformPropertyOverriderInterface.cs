using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000677 RID: 1655
	public abstract class PlatformPropertyOverriderInterface : MonoBehaviour, IPlatformPropertyOverrider
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06003352 RID: 13138
		// (set) Token: 0x06003353 RID: 13139
		public abstract OverrideMode overrideMode { get; set; }

		// Token: 0x06003354 RID: 13140
		public abstract void ApplyImmediate();

		// Token: 0x06003355 RID: 13141
		public abstract void ApplyImmediate(DeviceInfo.PlatformType platformType);

		// Token: 0x06003356 RID: 13142
		public abstract void Export();

		// Token: 0x06003357 RID: 13143
		public abstract void Export(DeviceInfo.PlatformType platformType);

		// Token: 0x06003358 RID: 13144
		public abstract void Import();

		// Token: 0x06003359 RID: 13145
		public abstract void Import(DeviceInfo.PlatformType platformType);
	}
}
