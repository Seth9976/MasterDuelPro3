using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000071 RID: 113
	public sealed class CinemachineCore
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00012635 File Offset: 0x00010835
		public static CinemachineCore Instance
		{
			get
			{
				if (CinemachineCore.sInstance == null)
				{
					CinemachineCore.sInstance = new CinemachineCore();
				}
				return CinemachineCore.sInstance;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0001264D File Offset: 0x0001084D
		public static float DeltaTime
		{
			get
			{
				if (CinemachineCore.UniformDeltaTimeOverride < 0f)
				{
					return Time.deltaTime;
				}
				return CinemachineCore.UniformDeltaTimeOverride;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00012666 File Offset: 0x00010866
		public static float CurrentTime
		{
			get
			{
				if (CinemachineCore.CurrentTimeOverride < 0f)
				{
					return Time.time;
				}
				return CinemachineCore.CurrentTimeOverride;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0001267F File Offset: 0x0001087F
		public int BrainCount
		{
			get
			{
				return this.mActiveBrains.Count;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001268C File Offset: 0x0001088C
		public CinemachineBrain GetActiveBrain(int index)
		{
			return this.mActiveBrains[index];
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001269A File Offset: 0x0001089A
		internal void AddActiveBrain(CinemachineBrain brain)
		{
			this.RemoveActiveBrain(brain);
			this.mActiveBrains.Insert(0, brain);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000126B0 File Offset: 0x000108B0
		internal void RemoveActiveBrain(CinemachineBrain brain)
		{
			this.mActiveBrains.Remove(brain);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000126BF File Offset: 0x000108BF
		public int VirtualCameraCount
		{
			get
			{
				return this.mActiveCameras.Count;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000126CC File Offset: 0x000108CC
		public CinemachineVirtualCameraBase GetVirtualCamera(int index)
		{
			if (!this.m_ActiveCamerasAreSorted && this.mActiveCameras.Count > 1)
			{
				this.mActiveCameras.Sort(delegate(CinemachineVirtualCameraBase x, CinemachineVirtualCameraBase y)
				{
					if (x.Priority != y.Priority)
					{
						return y.Priority.CompareTo(x.Priority);
					}
					return y.m_ActivationId.CompareTo(x.m_ActivationId);
				});
				this.m_ActiveCamerasAreSorted = true;
			}
			return this.mActiveCameras[index];
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001272C File Offset: 0x0001092C
		internal void AddActiveCamera(CinemachineVirtualCameraBase vcam)
		{
			int activationSequence = this.m_ActivationSequence;
			this.m_ActivationSequence = activationSequence + 1;
			vcam.m_ActivationId = activationSequence;
			this.mActiveCameras.Add(vcam);
			this.m_ActiveCamerasAreSorted = false;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00012763 File Offset: 0x00010963
		internal void RemoveActiveCamera(CinemachineVirtualCameraBase vcam)
		{
			if (this.mActiveCameras.Contains(vcam))
			{
				this.mActiveCameras.Remove(vcam);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00012780 File Offset: 0x00010980
		internal void CameraDestroyed(CinemachineVirtualCameraBase vcam)
		{
			if (this.mActiveCameras.Contains(vcam))
			{
				this.mActiveCameras.Remove(vcam);
			}
			if (this.mUpdateStatus != null && this.mUpdateStatus.ContainsKey(vcam))
			{
				this.mUpdateStatus.Remove(vcam);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000127C0 File Offset: 0x000109C0
		internal void CameraEnabled(CinemachineVirtualCameraBase vcam)
		{
			int parentLevel = 0;
			for (ICinemachineCamera p = vcam.ParentCamera; p != null; p = p.ParentCamera)
			{
				parentLevel++;
			}
			while (this.mAllCameras.Count <= parentLevel)
			{
				this.mAllCameras.Add(new List<CinemachineVirtualCameraBase>());
			}
			this.mAllCameras[parentLevel].Add(vcam);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00012818 File Offset: 0x00010A18
		internal void CameraDisabled(CinemachineVirtualCameraBase vcam)
		{
			for (int i = 0; i < this.mAllCameras.Count; i++)
			{
				this.mAllCameras[i].Remove(vcam);
			}
			if (this.mRoundRobinVcamLastFrame == vcam)
			{
				this.mRoundRobinVcamLastFrame = null;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00012864 File Offset: 0x00010A64
		internal void UpdateAllActiveVirtualCameras(int layerMask, Vector3 worldUp, float deltaTime)
		{
			CinemachineCore.UpdateFilter filter = this.m_CurrentUpdateFilter;
			bool canUpdateStandby = filter != CinemachineCore.UpdateFilter.Smart;
			CinemachineVirtualCameraBase currentRoundRobin = this.mRoundRobinVcamLastFrame;
			float now = CinemachineCore.CurrentTime;
			if (now != CinemachineCore.s_LastUpdateTime)
			{
				CinemachineCore.s_LastUpdateTime = now;
				if ((filter & (CinemachineCore.UpdateFilter)(-9)) == CinemachineCore.UpdateFilter.Fixed)
				{
					CinemachineCore.s_FixedFrameCount++;
				}
			}
			for (int i = this.mAllCameras.Count - 1; i >= 0; i--)
			{
				List<CinemachineVirtualCameraBase> sublist = this.mAllCameras[i];
				for (int j = sublist.Count - 1; j >= 0; j--)
				{
					CinemachineVirtualCameraBase vcam = sublist[j];
					if (canUpdateStandby && vcam == this.mRoundRobinVcamLastFrame)
					{
						currentRoundRobin = null;
					}
					if (vcam == null)
					{
						sublist.RemoveAt(j);
					}
					else if (vcam.m_StandbyUpdate == CinemachineVirtualCameraBase.StandbyUpdateMode.Always || this.IsLive(vcam))
					{
						if (((1 << vcam.gameObject.layer) & layerMask) != 0)
						{
							this.UpdateVirtualCamera(vcam, worldUp, deltaTime);
						}
					}
					else if (currentRoundRobin == null && this.mRoundRobinVcamLastFrame != vcam && canUpdateStandby && vcam.m_StandbyUpdate != CinemachineVirtualCameraBase.StandbyUpdateMode.Never && vcam.isActiveAndEnabled)
					{
						this.m_CurrentUpdateFilter &= (CinemachineCore.UpdateFilter)(-9);
						this.UpdateVirtualCamera(vcam, worldUp, deltaTime);
						this.m_CurrentUpdateFilter = filter;
						currentRoundRobin = vcam;
					}
				}
			}
			if (canUpdateStandby)
			{
				if (currentRoundRobin == this.mRoundRobinVcamLastFrame)
				{
					currentRoundRobin = null;
				}
				this.mRoundRobinVcamLastFrame = currentRoundRobin;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000129D4 File Offset: 0x00010BD4
		internal void UpdateVirtualCamera(CinemachineVirtualCameraBase vcam, Vector3 worldUp, float deltaTime)
		{
			if (vcam == null)
			{
				return;
			}
			bool flag = (this.m_CurrentUpdateFilter & CinemachineCore.UpdateFilter.Smart) == CinemachineCore.UpdateFilter.Smart;
			UpdateTracker.UpdateClock updateClock = (UpdateTracker.UpdateClock)(this.m_CurrentUpdateFilter & (CinemachineCore.UpdateFilter)(-9));
			if (flag)
			{
				Transform updateTarget = CinemachineCore.GetUpdateTarget(vcam);
				if (updateTarget == null)
				{
					return;
				}
				if (UpdateTracker.GetPreferredUpdate(updateTarget) != updateClock)
				{
					return;
				}
			}
			if (this.mUpdateStatus == null)
			{
				this.mUpdateStatus = new Dictionary<CinemachineVirtualCameraBase, CinemachineCore.UpdateStatus>();
			}
			CinemachineCore.UpdateStatus status;
			if (!this.mUpdateStatus.TryGetValue(vcam, out status))
			{
				status = new CinemachineCore.UpdateStatus
				{
					lastUpdateDeltaTime = -2f,
					lastUpdateMode = UpdateTracker.UpdateClock.Late,
					lastUpdateFrame = Time.frameCount + 2,
					lastUpdateFixedFrame = CinemachineCore.s_FixedFrameCount + 2
				};
				this.mUpdateStatus.Add(vcam, status);
			}
			int frameDelta = ((updateClock == UpdateTracker.UpdateClock.Late) ? (Time.frameCount - status.lastUpdateFrame) : (CinemachineCore.s_FixedFrameCount - status.lastUpdateFixedFrame));
			if (deltaTime >= 0f)
			{
				if (frameDelta == 0 && status.lastUpdateMode == updateClock && status.lastUpdateDeltaTime == deltaTime)
				{
					return;
				}
				if (CinemachineCore.FrameDeltaCompensationEnabled && frameDelta > 0)
				{
					deltaTime *= (float)frameDelta;
				}
			}
			vcam.InternalUpdateCameraState(worldUp, deltaTime);
			status.lastUpdateFrame = Time.frameCount;
			status.lastUpdateFixedFrame = CinemachineCore.s_FixedFrameCount;
			status.lastUpdateMode = updateClock;
			status.lastUpdateDeltaTime = deltaTime;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00012AF9 File Offset: 0x00010CF9
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeModule()
		{
			CinemachineCore.Instance.mUpdateStatus = new Dictionary<CinemachineVirtualCameraBase, CinemachineCore.UpdateStatus>();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012B0C File Offset: 0x00010D0C
		private static Transform GetUpdateTarget(CinemachineVirtualCameraBase vcam)
		{
			if (vcam == null || vcam.gameObject == null)
			{
				return null;
			}
			Transform target = vcam.LookAt;
			if (target != null)
			{
				return target;
			}
			target = vcam.Follow;
			if (target != null)
			{
				return target;
			}
			return vcam.transform;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00012B5C File Offset: 0x00010D5C
		internal UpdateTracker.UpdateClock GetVcamUpdateStatus(CinemachineVirtualCameraBase vcam)
		{
			CinemachineCore.UpdateStatus status;
			if (this.mUpdateStatus == null || !this.mUpdateStatus.TryGetValue(vcam, out status))
			{
				return UpdateTracker.UpdateClock.Late;
			}
			return status.lastUpdateMode;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00012B8C File Offset: 0x00010D8C
		public bool IsLive(ICinemachineCamera vcam)
		{
			if (vcam != null)
			{
				for (int i = 0; i < this.BrainCount; i++)
				{
					CinemachineBrain b = this.GetActiveBrain(i);
					if (b != null && b.IsLive(vcam, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00012BCC File Offset: 0x00010DCC
		public bool IsLiveInBlend(ICinemachineCamera vcam)
		{
			if (vcam != null)
			{
				for (int i = 0; i < this.BrainCount; i++)
				{
					CinemachineBrain b = this.GetActiveBrain(i);
					if (b != null && b.IsLiveInBlend(vcam))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00012C0C File Offset: 0x00010E0C
		public void GenerateCameraActivationEvent(ICinemachineCamera vcam, ICinemachineCamera vcamFrom)
		{
			if (vcam != null)
			{
				for (int i = 0; i < this.BrainCount; i++)
				{
					CinemachineBrain b = this.GetActiveBrain(i);
					if (b != null && b.IsLive(vcam, false))
					{
						b.m_CameraActivatedEvent.Invoke(vcam, vcamFrom);
					}
				}
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00012C58 File Offset: 0x00010E58
		public void GenerateCameraCutEvent(ICinemachineCamera vcam)
		{
			if (vcam != null)
			{
				for (int i = 0; i < this.BrainCount; i++)
				{
					CinemachineBrain b = this.GetActiveBrain(i);
					if (b != null && b.IsLive(vcam, false))
					{
						if (b.m_CameraCutEvent != null)
						{
							b.m_CameraCutEvent.Invoke(b);
						}
						if (CinemachineCore.CameraCutEvent != null)
						{
							CinemachineCore.CameraCutEvent.Invoke(b);
						}
					}
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00012CBC File Offset: 0x00010EBC
		public CinemachineBrain FindPotentialTargetBrain(CinemachineVirtualCameraBase vcam)
		{
			if (vcam != null)
			{
				int numBrains = this.BrainCount;
				for (int i = 0; i < numBrains; i++)
				{
					CinemachineBrain b = this.GetActiveBrain(i);
					if (b != null && b.OutputCamera != null && b.IsLive(vcam, false))
					{
						return b;
					}
				}
				int layer = 1 << vcam.gameObject.layer;
				for (int j = 0; j < numBrains; j++)
				{
					CinemachineBrain b2 = this.GetActiveBrain(j);
					if (b2 != null && b2.OutputCamera != null && (b2.OutputCamera.cullingMask & layer) != 0)
					{
						return b2;
					}
				}
			}
			return null;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00012D6C File Offset: 0x00010F6C
		public void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			int numVcams = this.VirtualCameraCount;
			for (int i = 0; i < numVcams; i++)
			{
				this.GetVirtualCamera(i).OnTargetObjectWarped(target, positionDelta);
			}
		}

		// Token: 0x04000299 RID: 665
		public static readonly int kStreamingVersion = 20170927;

		// Token: 0x0400029A RID: 666
		private static CinemachineCore sInstance = null;

		// Token: 0x0400029B RID: 667
		public static bool sShowHiddenObjects = false;

		// Token: 0x0400029C RID: 668
		public static CinemachineCore.AxisInputDelegate GetInputAxis = (string <p0>) => 0f;

		// Token: 0x0400029D RID: 669
		public static float UniformDeltaTimeOverride = -1f;

		// Token: 0x0400029E RID: 670
		public static float CurrentTimeOverride = -1f;

		// Token: 0x0400029F RID: 671
		public static CinemachineCore.GetBlendOverrideDelegate GetBlendOverride;

		// Token: 0x040002A0 RID: 672
		public static CinemachineBrain.BrainEvent CameraUpdatedEvent = new CinemachineBrain.BrainEvent();

		// Token: 0x040002A1 RID: 673
		public static CinemachineBrain.BrainEvent CameraCutEvent = new CinemachineBrain.BrainEvent();

		// Token: 0x040002A2 RID: 674
		private List<CinemachineBrain> mActiveBrains = new List<CinemachineBrain>();

		// Token: 0x040002A3 RID: 675
		internal static bool FrameDeltaCompensationEnabled = true;

		// Token: 0x040002A4 RID: 676
		private List<CinemachineVirtualCameraBase> mActiveCameras = new List<CinemachineVirtualCameraBase>();

		// Token: 0x040002A5 RID: 677
		private bool m_ActiveCamerasAreSorted;

		// Token: 0x040002A6 RID: 678
		private int m_ActivationSequence;

		// Token: 0x040002A7 RID: 679
		private List<List<CinemachineVirtualCameraBase>> mAllCameras = new List<List<CinemachineVirtualCameraBase>>();

		// Token: 0x040002A8 RID: 680
		private CinemachineVirtualCameraBase mRoundRobinVcamLastFrame;

		// Token: 0x040002A9 RID: 681
		private static float s_LastUpdateTime;

		// Token: 0x040002AA RID: 682
		private static int s_FixedFrameCount;

		// Token: 0x040002AB RID: 683
		private Dictionary<CinemachineVirtualCameraBase, CinemachineCore.UpdateStatus> mUpdateStatus;

		// Token: 0x040002AC RID: 684
		internal CinemachineCore.UpdateFilter m_CurrentUpdateFilter;

		// Token: 0x02000072 RID: 114
		public enum Stage
		{
			// Token: 0x040002AE RID: 686
			Body,
			// Token: 0x040002AF RID: 687
			Aim,
			// Token: 0x040002B0 RID: 688
			Noise,
			// Token: 0x040002B1 RID: 689
			Finalize
		}

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x060002DE RID: 734
		public delegate float AxisInputDelegate(string axisName);

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x060002E2 RID: 738
		public delegate CinemachineBlendDefinition GetBlendOverrideDelegate(ICinemachineCamera fromVcam, ICinemachineCamera toVcam, CinemachineBlendDefinition defaultBlend, MonoBehaviour owner);

		// Token: 0x02000075 RID: 117
		private class UpdateStatus
		{
			// Token: 0x040002B2 RID: 690
			public int lastUpdateFrame;

			// Token: 0x040002B3 RID: 691
			public int lastUpdateFixedFrame;

			// Token: 0x040002B4 RID: 692
			public UpdateTracker.UpdateClock lastUpdateMode;

			// Token: 0x040002B5 RID: 693
			public float lastUpdateDeltaTime;
		}

		// Token: 0x02000076 RID: 118
		internal enum UpdateFilter
		{
			// Token: 0x040002B7 RID: 695
			Fixed,
			// Token: 0x040002B8 RID: 696
			Late,
			// Token: 0x040002B9 RID: 697
			Smart = 8,
			// Token: 0x040002BA RID: 698
			SmartFixed = 8,
			// Token: 0x040002BB RID: 699
			SmartLate
		}
	}
}
