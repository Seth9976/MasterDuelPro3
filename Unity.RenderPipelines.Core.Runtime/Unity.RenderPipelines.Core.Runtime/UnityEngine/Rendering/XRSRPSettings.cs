using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.XR;

namespace UnityEngine.Rendering
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	public class XRSRPSettings
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003517D File Offset: 0x0003337D
		public static bool enabled
		{
			get
			{
				return XRSettings.enabled;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00035184 File Offset: 0x00033384
		public static bool isDeviceActive
		{
			get
			{
				return XRSRPSettings.enabled && XRSettings.isDeviceActive;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00035194 File Offset: 0x00033394
		public static string loadedDeviceName
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSettings.loadedDeviceName;
				}
				return "No XR device loaded";
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x000351A8 File Offset: 0x000333A8
		public static string[] supportedDevices
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSettings.supportedDevices;
				}
				return new string[1];
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x000351BD File Offset: 0x000333BD
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSettings.eyeTextureDesc;
				}
				return new RenderTextureDescriptor(0, 0);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x000351D3 File Offset: 0x000333D3
		public static int eyeTextureWidth
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSettings.eyeTextureWidth;
				}
				return 0;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x000351E3 File Offset: 0x000333E3
		public static int eyeTextureHeight
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSettings.eyeTextureHeight;
				}
				return 0;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x000351F3 File Offset: 0x000333F3
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00035207 File Offset: 0x00033407
		public static float occlusionMeshScale
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSystem.GetOcclusionMeshScale();
				}
				return 0f;
			}
			set
			{
				if (XRSRPSettings.enabled)
				{
					XRSystem.SetOcclusionMeshScale(value);
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00035216 File Offset: 0x00033416
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x00035226 File Offset: 0x00033426
		public static int mirrorViewMode
		{
			get
			{
				if (XRSRPSettings.enabled)
				{
					return XRSystem.GetMirrorViewMode();
				}
				return 0;
			}
			set
			{
				if (XRSRPSettings.enabled)
				{
					XRSystem.SetMirrorViewMode(value);
				}
			}
		}
	}
}
