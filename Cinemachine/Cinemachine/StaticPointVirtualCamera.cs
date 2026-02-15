using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200006B RID: 107
	internal class StaticPointVirtualCamera : ICinemachineCamera
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00012139 File Offset: 0x00010339
		public StaticPointVirtualCamera(CameraState state, string name)
		{
			this.State = state;
			this.Name = name;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001214F File Offset: 0x0001034F
		public void SetState(CameraState state)
		{
			this.State = state;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00012158 File Offset: 0x00010358
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00012160 File Offset: 0x00010360
		public string Name { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00012169 File Offset: 0x00010369
		public string Description
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00012170 File Offset: 0x00010370
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00012178 File Offset: 0x00010378
		public int Priority { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00012181 File Offset: 0x00010381
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00012189 File Offset: 0x00010389
		public Transform LookAt { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00012192 File Offset: 0x00010392
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0001219A File Offset: 0x0001039A
		public Transform Follow { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000121A3 File Offset: 0x000103A3
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000121AB File Offset: 0x000103AB
		public CameraState State { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000121B4 File Offset: 0x000103B4
		public GameObject VirtualCameraGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000771A File Offset: 0x0000591A
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000121B4 File Offset: 0x000103B4
		public ICinemachineCamera ParentCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C34E File Offset: 0x0000A54E
		public bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			return false;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000429A File Offset: 0x0000249A
		public void UpdateCameraState(Vector3 worldUp, float deltaTime)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000429A File Offset: 0x0000249A
		public void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000429A File Offset: 0x0000249A
		public void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000429A File Offset: 0x0000249A
		public void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
		}
	}
}
