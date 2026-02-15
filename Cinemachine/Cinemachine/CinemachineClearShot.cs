using System;
using System.Collections.Generic;
using System.Text;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000017 RID: 23
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[ExcludeFromPreset]
	[AddComponentMenu("Cinemachine/CinemachineClearShot")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineClearShot.html")]
	public class CinemachineClearShot : CinemachineVirtualCameraBase
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004344 File Offset: 0x00002544
		public override string Description
		{
			get
			{
				if (this.mActiveBlend != null)
				{
					return this.mActiveBlend.Description;
				}
				ICinemachineCamera vcam = this.LiveChild;
				if (vcam == null)
				{
					return "(none)";
				}
				StringBuilder stringBuilder = CinemachineDebug.SBFromPool();
				stringBuilder.Append("[");
				stringBuilder.Append(vcam.Name);
				stringBuilder.Append("]");
				string text = stringBuilder.ToString();
				CinemachineDebug.ReturnToPool(stringBuilder);
				return text;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000043AC File Offset: 0x000025AC
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000043B4 File Offset: 0x000025B4
		public ICinemachineCamera LiveChild { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000043BD File Offset: 0x000025BD
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000043C5 File Offset: 0x000025C5
		public override bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			return vcam == this.LiveChild || (this.mActiveBlend != null && this.mActiveBlend.Uses(vcam));
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000043E8 File Offset: 0x000025E8
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000043F6 File Offset: 0x000025F6
		public override Transform LookAt
		{
			get
			{
				return base.ResolveLookAt(this.m_LookAt);
			}
			set
			{
				this.m_LookAt = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000043FF File Offset: 0x000025FF
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000440D File Offset: 0x0000260D
		public override Transform Follow
		{
			get
			{
				return base.ResolveFollow(this.m_Follow);
			}
			set
			{
				this.m_Follow = value;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004418 File Offset: 0x00002618
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			this.UpdateListOfChildren();
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			for (int i = 0; i < childCameras.Length; i++)
			{
				childCameras[i].OnTargetObjectWarped(target, positionDelta);
			}
			base.OnTargetObjectWarped(target, positionDelta);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004454 File Offset: 0x00002654
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			this.UpdateListOfChildren();
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			for (int i = 0; i < childCameras.Length; i++)
			{
				childCameras[i].ForceCameraPosition(pos, rot);
			}
			base.ForceCameraPosition(pos, rot);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004490 File Offset: 0x00002690
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			this.UpdateListOfChildren();
			ICinemachineCamera previousCam = this.LiveChild;
			this.LiveChild = this.ChooseCurrentCamera(worldUp);
			if (previousCam != this.LiveChild && this.LiveChild != null)
			{
				this.LiveChild.OnTransitionFromCamera(previousCam, worldUp, deltaTime);
				CinemachineCore.Instance.GenerateCameraActivationEvent(this.LiveChild, previousCam);
				if (previousCam != null)
				{
					this.mActiveBlend = base.CreateBlend(previousCam, this.LiveChild, this.LookupBlend(previousCam, this.LiveChild), this.mActiveBlend);
					if (this.mActiveBlend == null || !this.mActiveBlend.Uses(previousCam))
					{
						CinemachineCore.Instance.GenerateCameraCutEvent(this.LiveChild);
					}
				}
			}
			if (this.mActiveBlend != null)
			{
				this.mActiveBlend.TimeInBlend += ((deltaTime >= 0f) ? deltaTime : this.mActiveBlend.Duration);
				if (this.mActiveBlend.IsComplete)
				{
					this.mActiveBlend = null;
				}
			}
			if (this.mActiveBlend != null)
			{
				this.mActiveBlend.UpdateCameraState(worldUp, deltaTime);
				this.m_State = this.mActiveBlend.State;
			}
			else if (this.LiveChild != null)
			{
				if (this.m_TransitioningFrom != null)
				{
					this.LiveChild.OnTransitionFromCamera(this.m_TransitioningFrom, worldUp, deltaTime);
				}
				this.m_State = this.LiveChild.State;
			}
			this.m_TransitioningFrom = null;
			base.InvokePostPipelineStageCallback(this, CinemachineCore.Stage.Finalize, ref this.m_State, deltaTime);
			this.PreviousStateIsValid = true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000045F4 File Offset: 0x000027F4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InvalidateListOfChildren();
			this.mActiveBlend = null;
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Remove(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Combine(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004654 File Offset: 0x00002854
		protected override void OnDisable()
		{
			base.OnDisable();
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Remove(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000467C File Offset: 0x0000287C
		public void OnTransformChildrenChanged()
		{
			this.InvalidateListOfChildren();
			this.UpdateListOfChildren();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000468C File Offset: 0x0000288C
		private void OnGuiHandler()
		{
			if (!this.m_ShowDebugText)
			{
				CinemachineDebug.ReleaseScreenPos(this);
				return;
			}
			StringBuilder stringBuilder = CinemachineDebug.SBFromPool();
			stringBuilder.Append(base.Name);
			stringBuilder.Append(": ");
			stringBuilder.Append(this.Description);
			string text = stringBuilder.ToString();
			GUI.Label(CinemachineDebug.GetScreenPos(this, text, GUI.skin.box), text, GUI.skin.box);
			CinemachineDebug.ReturnToPool(stringBuilder);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004700 File Offset: 0x00002900
		public bool IsBlending
		{
			get
			{
				return this.mActiveBlend != null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000470B File Offset: 0x0000290B
		public CinemachineBlend ActiveBlend
		{
			get
			{
				return this.mActiveBlend;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004713 File Offset: 0x00002913
		public CinemachineVirtualCameraBase[] ChildCameras
		{
			get
			{
				this.UpdateListOfChildren();
				return this.m_ChildCameras;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004721 File Offset: 0x00002921
		private void InvalidateListOfChildren()
		{
			this.m_ChildCameras = null;
			this.m_RandomizedChilden = null;
			this.LiveChild = null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004738 File Offset: 0x00002938
		public void ResetRandomization()
		{
			this.m_RandomizedChilden = null;
			this.mRandomizeNow = true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004748 File Offset: 0x00002948
		private void UpdateListOfChildren()
		{
			if (this.m_ChildCameras != null)
			{
				return;
			}
			List<CinemachineVirtualCameraBase> list = new List<CinemachineVirtualCameraBase>();
			foreach (CinemachineVirtualCameraBase i in base.GetComponentsInChildren<CinemachineVirtualCameraBase>(true))
			{
				if (i.transform.parent == base.transform)
				{
					list.Add(i);
				}
			}
			this.m_ChildCameras = list.ToArray();
			this.mActivationTime = (this.mPendingActivationTime = 0f);
			this.mPendingCamera = null;
			this.LiveChild = null;
			this.mActiveBlend = null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000047D4 File Offset: 0x000029D4
		private ICinemachineCamera ChooseCurrentCamera(Vector3 worldUp)
		{
			if (this.m_ChildCameras == null || this.m_ChildCameras.Length == 0)
			{
				this.mActivationTime = 0f;
				return null;
			}
			CinemachineVirtualCameraBase[] childCameras = this.m_ChildCameras;
			if (!this.m_RandomizeChoice)
			{
				this.m_RandomizedChilden = null;
			}
			else if (this.m_ChildCameras.Length > 1)
			{
				if (this.m_RandomizedChilden == null)
				{
					this.m_RandomizedChilden = this.Randomize(this.m_ChildCameras);
				}
				childCameras = this.m_RandomizedChilden;
			}
			if (this.LiveChild != null && !this.LiveChild.VirtualCameraGameObject.activeSelf)
			{
				this.LiveChild = null;
			}
			ICinemachineCamera best = this.LiveChild;
			foreach (CinemachineVirtualCameraBase vcam in childCameras)
			{
				if (vcam != null && vcam.gameObject.activeInHierarchy && (best == null || vcam.State.ShotQuality > best.State.ShotQuality || (vcam.State.ShotQuality == best.State.ShotQuality && vcam.Priority > best.Priority) || (this.m_RandomizeChoice && this.mRandomizeNow && vcam != this.LiveChild && vcam.State.ShotQuality == best.State.ShotQuality && vcam.Priority == best.Priority)))
				{
					best = vcam;
				}
			}
			this.mRandomizeNow = false;
			float now = CinemachineCore.CurrentTime;
			if (this.mActivationTime != 0f)
			{
				if (this.LiveChild == best)
				{
					this.mPendingActivationTime = 0f;
					this.mPendingCamera = null;
					return best;
				}
				if (this.PreviousStateIsValid && this.mPendingActivationTime != 0f && this.mPendingCamera == best)
				{
					if (now - this.mPendingActivationTime > this.m_ActivateAfter && now - this.mActivationTime > this.m_MinDuration)
					{
						this.m_RandomizedChilden = null;
						this.mActivationTime = now;
						this.mPendingActivationTime = 0f;
						this.mPendingCamera = null;
						return best;
					}
					return this.LiveChild;
				}
			}
			this.mPendingActivationTime = 0f;
			this.mPendingCamera = null;
			if (this.PreviousStateIsValid && this.mActivationTime > 0f && (this.m_ActivateAfter > 0f || now - this.mActivationTime < this.m_MinDuration))
			{
				this.mPendingCamera = best;
				this.mPendingActivationTime = now;
				return this.LiveChild;
			}
			this.m_RandomizedChilden = null;
			this.mActivationTime = now;
			return best;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004A38 File Offset: 0x00002C38
		private CinemachineVirtualCameraBase[] Randomize(CinemachineVirtualCameraBase[] src)
		{
			List<CinemachineClearShot.Pair> pairs = new List<CinemachineClearShot.Pair>();
			for (int i = 0; i < src.Length; i++)
			{
				pairs.Add(new CinemachineClearShot.Pair
				{
					a = i,
					b = global::UnityEngine.Random.Range(0f, 1000f)
				});
			}
			pairs.Sort((CinemachineClearShot.Pair p1, CinemachineClearShot.Pair p2) => (int)p1.b - (int)p2.b);
			CinemachineVirtualCameraBase[] dst = new CinemachineVirtualCameraBase[src.Length];
			CinemachineClearShot.Pair[] result = pairs.ToArray();
			for (int j = 0; j < src.Length; j++)
			{
				dst[j] = src[result[j].a];
			}
			return dst;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004AE4 File Offset: 0x00002CE4
		private CinemachineBlendDefinition LookupBlend(ICinemachineCamera fromKey, ICinemachineCamera toKey)
		{
			CinemachineBlendDefinition blend = this.m_DefaultBlend;
			if (this.m_CustomBlends != null)
			{
				string fromCameraName = ((fromKey != null) ? fromKey.Name : string.Empty);
				string toCameraName = ((toKey != null) ? toKey.Name : string.Empty);
				blend = this.m_CustomBlends.GetBlendForVirtualCameras(fromCameraName, toCameraName, blend);
			}
			if (CinemachineCore.GetBlendOverride != null)
			{
				blend = CinemachineCore.GetBlendOverride(fromKey, toKey, blend, this);
			}
			return blend;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004B50 File Offset: 0x00002D50
		public override void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
			base.InvokeOnTransitionInExtensions(fromCam, worldUp, deltaTime);
			this.m_TransitioningFrom = fromCam;
			if (this.m_RandomizeChoice && this.mActiveBlend == null)
			{
				this.m_RandomizedChilden = null;
				this.LiveChild = null;
			}
			this.InternalUpdateCameraState(worldUp, deltaTime);
		}

		// Token: 0x04000057 RID: 87
		[Tooltip("Default object for the camera children to look at (the aim target), if not specified in a child camera.  May be empty if all children specify targets of their own.")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_LookAt;

		// Token: 0x04000058 RID: 88
		[Tooltip("Default object for the camera children wants to move with (the body target), if not specified in a child camera.  May be empty if all children specify targets of their own.")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_Follow;

		// Token: 0x04000059 RID: 89
		[Tooltip("When enabled, the current child camera and blend will be indicated in the game window, for debugging")]
		[NoSaveDuringPlay]
		public bool m_ShowDebugText;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		[HideInInspector]
		[NoSaveDuringPlay]
		internal CinemachineVirtualCameraBase[] m_ChildCameras;

		// Token: 0x0400005B RID: 91
		[Tooltip("Wait this many seconds before activating a new child camera")]
		public float m_ActivateAfter;

		// Token: 0x0400005C RID: 92
		[Tooltip("An active camera must be active for at least this many seconds")]
		public float m_MinDuration;

		// Token: 0x0400005D RID: 93
		[Tooltip("If checked, camera choice will be randomized if multiple cameras are equally desirable.  Otherwise, child list order and child camera priority will be used.")]
		public bool m_RandomizeChoice;

		// Token: 0x0400005E RID: 94
		[CinemachineBlendDefinitionProperty]
		[Tooltip("The blend which is used if you don't explicitly define a blend between two Virtual Cameras")]
		public CinemachineBlendDefinition m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);

		// Token: 0x0400005F RID: 95
		[HideInInspector]
		public CinemachineBlenderSettings m_CustomBlends;

		// Token: 0x04000061 RID: 97
		private CameraState m_State = CameraState.Default;

		// Token: 0x04000062 RID: 98
		private float mActivationTime;

		// Token: 0x04000063 RID: 99
		private float mPendingActivationTime;

		// Token: 0x04000064 RID: 100
		private ICinemachineCamera mPendingCamera;

		// Token: 0x04000065 RID: 101
		private CinemachineBlend mActiveBlend;

		// Token: 0x04000066 RID: 102
		private bool mRandomizeNow;

		// Token: 0x04000067 RID: 103
		private CinemachineVirtualCameraBase[] m_RandomizedChilden;

		// Token: 0x04000068 RID: 104
		private ICinemachineCamera m_TransitioningFrom;

		// Token: 0x02000018 RID: 24
		private struct Pair
		{
			// Token: 0x04000069 RID: 105
			public int a;

			// Token: 0x0400006A RID: 106
			public float b;
		}
	}
}
