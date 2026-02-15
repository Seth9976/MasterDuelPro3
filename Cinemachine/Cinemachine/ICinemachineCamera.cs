using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000096 RID: 150
	public interface ICinemachineCamera
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600038C RID: 908
		string Name { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600038D RID: 909
		string Description { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600038E RID: 910
		// (set) Token: 0x0600038F RID: 911
		int Priority { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000390 RID: 912
		// (set) Token: 0x06000391 RID: 913
		Transform LookAt { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000392 RID: 914
		// (set) Token: 0x06000393 RID: 915
		Transform Follow { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000394 RID: 916
		CameraState State { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000395 RID: 917
		GameObject VirtualCameraGameObject { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000396 RID: 918
		bool IsValid { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000397 RID: 919
		ICinemachineCamera ParentCamera { get; }

		// Token: 0x06000398 RID: 920
		bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false);

		// Token: 0x06000399 RID: 921
		void UpdateCameraState(Vector3 worldUp, float deltaTime);

		// Token: 0x0600039A RID: 922
		void InternalUpdateCameraState(Vector3 worldUp, float deltaTime);

		// Token: 0x0600039B RID: 923
		void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime);

		// Token: 0x0600039C RID: 924
		void OnTargetObjectWarped(Transform target, Vector3 positionDelta);
	}
}
