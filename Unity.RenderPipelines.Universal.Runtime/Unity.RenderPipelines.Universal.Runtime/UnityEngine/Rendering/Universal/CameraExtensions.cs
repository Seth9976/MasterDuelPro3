using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001AF RID: 431
	public static class CameraExtensions
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x0002DB98 File Offset: 0x0002BD98
		public static UniversalAdditionalCameraData GetUniversalAdditionalCameraData(this Camera camera)
		{
			GameObject gameObject = camera.gameObject;
			UniversalAdditionalCameraData cameraData;
			if (!gameObject.TryGetComponent<UniversalAdditionalCameraData>(out cameraData))
			{
				cameraData = gameObject.AddComponent<UniversalAdditionalCameraData>();
			}
			return cameraData;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002DBBE File Offset: 0x0002BDBE
		public static VolumeFrameworkUpdateMode GetVolumeFrameworkUpdateMode(this Camera camera)
		{
			return camera.GetUniversalAdditionalCameraData().volumeFrameworkUpdateMode;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public static void SetVolumeFrameworkUpdateMode(this Camera camera, VolumeFrameworkUpdateMode mode)
		{
			UniversalAdditionalCameraData cameraData = camera.GetUniversalAdditionalCameraData();
			if (cameraData.volumeFrameworkUpdateMode == mode)
			{
				return;
			}
			bool requiresVolumeFrameworkUpdate = cameraData.requiresVolumeFrameworkUpdate;
			cameraData.volumeFrameworkUpdateMode = mode;
			if (requiresVolumeFrameworkUpdate && !cameraData.requiresVolumeFrameworkUpdate)
			{
				camera.UpdateVolumeStack(cameraData);
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002DC08 File Offset: 0x0002BE08
		public static void UpdateVolumeStack(this Camera camera)
		{
			UniversalAdditionalCameraData cameraData = camera.GetUniversalAdditionalCameraData();
			camera.UpdateVolumeStack(cameraData);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002DC24 File Offset: 0x0002BE24
		public static void UpdateVolumeStack(this Camera camera, UniversalAdditionalCameraData cameraData)
		{
			if (cameraData.requiresVolumeFrameworkUpdate)
			{
				return;
			}
			if (cameraData.volumeStack == null)
			{
				cameraData.GetOrCreateVolumeStack();
			}
			LayerMask layerMask;
			Transform trigger;
			camera.GetVolumeLayerMaskAndTrigger(cameraData, out layerMask, out trigger);
			VolumeManager.instance.Update(cameraData.volumeStack, trigger, layerMask);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002DC68 File Offset: 0x0002BE68
		public static void DestroyVolumeStack(this Camera camera)
		{
			UniversalAdditionalCameraData cameraData = camera.GetUniversalAdditionalCameraData();
			camera.DestroyVolumeStack(cameraData);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002DC83 File Offset: 0x0002BE83
		public static void DestroyVolumeStack(this Camera camera, UniversalAdditionalCameraData cameraData)
		{
			if (cameraData == null || cameraData.volumeStack == null)
			{
				return;
			}
			cameraData.volumeStack = null;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		internal static void GetVolumeLayerMaskAndTrigger(this Camera camera, UniversalAdditionalCameraData cameraData, out LayerMask layerMask, out Transform trigger)
		{
			layerMask = 1;
			trigger = camera.transform;
			if (cameraData != null)
			{
				layerMask = cameraData.volumeLayerMask;
				trigger = ((cameraData.volumeTrigger != null) ? cameraData.volumeTrigger : trigger);
				return;
			}
			if (camera.cameraType == CameraType.SceneView)
			{
				Camera mainCamera = Camera.main;
				UniversalAdditionalCameraData mainAdditionalCameraData = null;
				if (mainCamera != null && mainCamera.TryGetComponent<UniversalAdditionalCameraData>(out mainAdditionalCameraData))
				{
					layerMask = mainAdditionalCameraData.volumeLayerMask;
				}
				trigger = ((mainAdditionalCameraData != null && mainAdditionalCameraData.volumeTrigger != null) ? mainAdditionalCameraData.volumeTrigger : trigger);
			}
		}
	}
}
