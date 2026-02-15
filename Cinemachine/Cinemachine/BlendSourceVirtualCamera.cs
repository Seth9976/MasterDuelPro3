using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200006C RID: 108
	internal class BlendSourceVirtualCamera : ICinemachineCamera
	{
		// Token: 0x06000293 RID: 659 RVA: 0x000121B7 File Offset: 0x000103B7
		public BlendSourceVirtualCamera(CinemachineBlend blend)
		{
			this.Blend = blend;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000121C6 File Offset: 0x000103C6
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000121CE File Offset: 0x000103CE
		public CinemachineBlend Blend { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000121D7 File Offset: 0x000103D7
		public string Name
		{
			get
			{
				return "Mid-blend";
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000121DE File Offset: 0x000103DE
		public string Description
		{
			get
			{
				if (this.Blend != null)
				{
					return this.Blend.Description;
				}
				return "(null)";
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000121F9 File Offset: 0x000103F9
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00012201 File Offset: 0x00010401
		public int Priority { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0001220A File Offset: 0x0001040A
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00012212 File Offset: 0x00010412
		public Transform LookAt { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001221B File Offset: 0x0001041B
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00012223 File Offset: 0x00010423
		public Transform Follow { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001222C File Offset: 0x0001042C
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00012234 File Offset: 0x00010434
		public CameraState State { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000121B4 File Offset: 0x000103B4
		public GameObject VirtualCameraGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0001223D File Offset: 0x0001043D
		public bool IsValid
		{
			get
			{
				return this.Blend != null && this.Blend.IsValid;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000121B4 File Offset: 0x000103B4
		public ICinemachineCamera ParentCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00012254 File Offset: 0x00010454
		public bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			return this.Blend != null && (vcam == this.Blend.CamA || vcam == this.Blend.CamB);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001227E File Offset: 0x0001047E
		public CameraState CalculateNewState(float deltaTime)
		{
			return this.State;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00012286 File Offset: 0x00010486
		public void UpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			if (this.Blend != null)
			{
				this.Blend.UpdateCameraState(worldUp, deltaTime);
				this.State = this.Blend.State;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000429A File Offset: 0x0000249A
		public void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000429A File Offset: 0x0000249A
		public void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000429A File Offset: 0x0000249A
		public void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
		}
	}
}
