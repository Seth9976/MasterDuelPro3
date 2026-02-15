using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D5 RID: 213
	public interface IVolumeDebugSettings
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006EE RID: 1774
		// (set) Token: 0x060006EF RID: 1775
		int selectedComponent { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006F0 RID: 1776
		Camera selectedCamera { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006F1 RID: 1777
		IEnumerable<Camera> cameras { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060006F2 RID: 1778
		// (set) Token: 0x060006F3 RID: 1779
		int selectedCameraIndex { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060006F4 RID: 1780
		VolumeStack selectedCameraVolumeStack { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060006F5 RID: 1781
		LayerMask selectedCameraLayerMask { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060006F6 RID: 1782
		Vector3 selectedCameraPosition { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060006F7 RID: 1783
		// (set) Token: 0x060006F8 RID: 1784
		Type selectedComponentType { get; set; }

		// Token: 0x060006F9 RID: 1785
		Volume[] GetVolumes();

		// Token: 0x060006FA RID: 1786
		bool VolumeHasInfluence(Volume volume);

		// Token: 0x060006FB RID: 1787
		bool RefreshVolumes(Volume[] newVolumes);

		// Token: 0x060006FC RID: 1788
		float GetVolumeWeight(Volume volume);
	}
}
