using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000070 RID: 112
	public class UniversalRenderPipelineVolumeDebugSettings : VolumeDebugSettings<UniversalAdditionalCameraData>
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000088B0 File Offset: 0x00006AB0
		public override VolumeStack selectedCameraVolumeStack
		{
			get
			{
				if (base.selectedCamera == null)
				{
					return null;
				}
				UniversalAdditionalCameraData additionalCameraData = base.selectedCamera.GetComponent<UniversalAdditionalCameraData>();
				if (additionalCameraData == null)
				{
					return null;
				}
				VolumeStack stack = additionalCameraData.volumeStack;
				if (stack != null)
				{
					return stack;
				}
				return VolumeManager.instance.stack;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000088FC File Offset: 0x00006AFC
		public override LayerMask selectedCameraLayerMask
		{
			get
			{
				UniversalAdditionalCameraData selectedAdditionalCameraData;
				if (base.selectedCamera != null && base.selectedCamera.TryGetComponent<UniversalAdditionalCameraData>(out selectedAdditionalCameraData))
				{
					return selectedAdditionalCameraData.volumeLayerMask;
				}
				return 1;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00008933 File Offset: 0x00006B33
		public override Vector3 selectedCameraPosition
		{
			get
			{
				if (!(base.selectedCamera != null))
				{
					return Vector3.zero;
				}
				return base.selectedCamera.transform.position;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00008959 File Offset: 0x00006B59
		[Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(23.2)", false)]
		public override Type targetRenderPipeline
		{
			get
			{
				return typeof(UniversalRenderPipeline);
			}
		}
	}
}
