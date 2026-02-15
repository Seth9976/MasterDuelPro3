using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x0200008A RID: 138
	[SaveDuringPlay]
	public abstract class CinemachineVirtualCameraBase : MonoBehaviour, ICinemachineCamera, ISerializationCallbackReceiver
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000138B6 File Offset: 0x00011AB6
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000138CC File Offset: 0x00011ACC
		public int ValidatingStreamVersion
		{
			get
			{
				if (!this.m_OnValidateCalled)
				{
					return CinemachineCore.kStreamingVersion;
				}
				return this.m_ValidatingStreamVersion;
			}
			private set
			{
				this.m_ValidatingStreamVersion = value;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000138D8 File Offset: 0x00011AD8
		public virtual float GetMaxDampTime()
		{
			float maxDamp = 0f;
			if (this.mExtensions != null)
			{
				for (int i = 0; i < this.mExtensions.Count; i++)
				{
					maxDamp = Mathf.Max(maxDamp, this.mExtensions[i].GetMaxDampTime());
				}
			}
			return maxDamp;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00013922 File Offset: 0x00011B22
		public float DetachedFollowTargetDamp(float initial, float dampTime, float deltaTime)
		{
			dampTime = Mathf.Lerp(Mathf.Max(1f, dampTime), dampTime, this.FollowTargetAttachment);
			deltaTime = Mathf.Lerp(0f, deltaTime, this.FollowTargetAttachment);
			return Damper.Damp(initial, dampTime, deltaTime);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00013958 File Offset: 0x00011B58
		public Vector3 DetachedFollowTargetDamp(Vector3 initial, Vector3 dampTime, float deltaTime)
		{
			dampTime = Vector3.Lerp(Vector3.Max(Vector3.one, dampTime), dampTime, this.FollowTargetAttachment);
			deltaTime = Mathf.Lerp(0f, deltaTime, this.FollowTargetAttachment);
			return Damper.Damp(initial, dampTime, deltaTime);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001398E File Offset: 0x00011B8E
		public Vector3 DetachedFollowTargetDamp(Vector3 initial, float dampTime, float deltaTime)
		{
			dampTime = Mathf.Lerp(Mathf.Max(1f, dampTime), dampTime, this.FollowTargetAttachment);
			deltaTime = Mathf.Lerp(0f, deltaTime, this.FollowTargetAttachment);
			return Damper.Damp(initial, dampTime, deltaTime);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000139C4 File Offset: 0x00011BC4
		public float DetachedLookAtTargetDamp(float initial, float dampTime, float deltaTime)
		{
			dampTime = Mathf.Lerp(Mathf.Max(1f, dampTime), dampTime, this.LookAtTargetAttachment);
			deltaTime = Mathf.Lerp(0f, deltaTime, this.LookAtTargetAttachment);
			return Damper.Damp(initial, dampTime, deltaTime);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000139FA File Offset: 0x00011BFA
		public Vector3 DetachedLookAtTargetDamp(Vector3 initial, Vector3 dampTime, float deltaTime)
		{
			dampTime = Vector3.Lerp(Vector3.Max(Vector3.one, dampTime), dampTime, this.LookAtTargetAttachment);
			deltaTime = Mathf.Lerp(0f, deltaTime, this.LookAtTargetAttachment);
			return Damper.Damp(initial, dampTime, deltaTime);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00013A30 File Offset: 0x00011C30
		public Vector3 DetachedLookAtTargetDamp(Vector3 initial, float dampTime, float deltaTime)
		{
			dampTime = Mathf.Lerp(Mathf.Max(1f, dampTime), dampTime, this.LookAtTargetAttachment);
			deltaTime = Mathf.Lerp(0f, deltaTime, this.LookAtTargetAttachment);
			return Damper.Damp(initial, dampTime, deltaTime);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00013A66 File Offset: 0x00011C66
		public virtual void AddExtension(CinemachineExtension extension)
		{
			if (this.mExtensions == null)
			{
				this.mExtensions = new List<CinemachineExtension>();
			}
			else
			{
				this.mExtensions.Remove(extension);
			}
			this.mExtensions.Add(extension);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00013A96 File Offset: 0x00011C96
		public virtual void RemoveExtension(CinemachineExtension extension)
		{
			if (this.mExtensions != null)
			{
				this.mExtensions.Remove(extension);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00013AAD File Offset: 0x00011CAD
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00013AB5 File Offset: 0x00011CB5
		internal List<CinemachineExtension> mExtensions { get; private set; }

		// Token: 0x06000335 RID: 821 RVA: 0x00013AC0 File Offset: 0x00011CC0
		protected void InvokePostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState newState, float deltaTime)
		{
			if (this.mExtensions != null)
			{
				for (int i = 0; i < this.mExtensions.Count; i++)
				{
					CinemachineExtension e = this.mExtensions[i];
					if (e == null)
					{
						this.mExtensions.RemoveAt(i);
						i--;
					}
					else if (e.enabled)
					{
						e.InvokePostPipelineStageCallback(vcam, stage, ref newState, deltaTime);
					}
				}
			}
			CinemachineVirtualCameraBase parent = this.ParentCamera as CinemachineVirtualCameraBase;
			if (parent != null)
			{
				parent.InvokePostPipelineStageCallback(vcam, stage, ref newState, deltaTime);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00013B48 File Offset: 0x00011D48
		protected void InvokePrePipelineMutateCameraStateCallback(CinemachineVirtualCameraBase vcam, ref CameraState newState, float deltaTime)
		{
			if (this.mExtensions != null)
			{
				for (int i = 0; i < this.mExtensions.Count; i++)
				{
					CinemachineExtension e = this.mExtensions[i];
					if (e == null)
					{
						this.mExtensions.RemoveAt(i);
						i--;
					}
					else if (e.enabled)
					{
						e.PrePipelineMutateCameraStateCallback(vcam, ref newState, deltaTime);
					}
				}
			}
			CinemachineVirtualCameraBase parent = this.ParentCamera as CinemachineVirtualCameraBase;
			if (parent != null)
			{
				parent.InvokePrePipelineMutateCameraStateCallback(vcam, ref newState, deltaTime);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00013BCC File Offset: 0x00011DCC
		protected bool InvokeOnTransitionInExtensions(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			bool forceUpdate = false;
			if (this.mExtensions != null)
			{
				for (int i = 0; i < this.mExtensions.Count; i++)
				{
					CinemachineExtension e = this.mExtensions[i];
					if (e == null)
					{
						this.mExtensions.RemoveAt(i);
						i--;
					}
					else if (e.enabled && e.OnTransitionFromCamera(fromCam, worldUp, deltaTime))
					{
						forceUpdate = true;
					}
				}
			}
			return forceUpdate;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00013C37 File Offset: 0x00011E37
		public string Name
		{
			get
			{
				return base.name;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00012169 File Offset: 0x00010369
		public virtual string Description
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00013C3F File Offset: 0x00011E3F
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00013C47 File Offset: 0x00011E47
		public int Priority
		{
			get
			{
				return this.m_Priority;
			}
			set
			{
				this.m_Priority = value;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00013C50 File Offset: 0x00011E50
		protected void ApplyPositionBlendMethod(ref CameraState state, CinemachineVirtualCameraBase.BlendHint hint)
		{
			switch (hint)
			{
			case CinemachineVirtualCameraBase.BlendHint.SphericalPosition:
				state.BlendHint |= CameraState.BlendHintValue.SphericalPositionBlend;
				return;
			case CinemachineVirtualCameraBase.BlendHint.CylindricalPosition:
				state.BlendHint |= CameraState.BlendHintValue.CylindricalPositionBlend;
				return;
			case CinemachineVirtualCameraBase.BlendHint.ScreenSpaceAimWhenTargetsDiffer:
				state.BlendHint |= CameraState.BlendHintValue.RadialAimBlend;
				return;
			default:
				return;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00013C8B File Offset: 0x00011E8B
		public GameObject VirtualCameraGameObject
		{
			get
			{
				if (this == null)
				{
					return null;
				}
				return base.gameObject;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00013C9E File Offset: 0x00011E9E
		public bool IsValid
		{
			get
			{
				return !(this == null);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600033F RID: 831
		public abstract CameraState State { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00013CAA File Offset: 0x00011EAA
		public ICinemachineCamera ParentCamera
		{
			get
			{
				if (!this.mSlaveStatusUpdated || !Application.isPlaying)
				{
					this.UpdateSlaveStatus();
				}
				return this.m_parentVcam;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C34E File Offset: 0x0000A54E
		public virtual bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			return false;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000342 RID: 834
		// (set) Token: 0x06000343 RID: 835
		public abstract Transform LookAt { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000344 RID: 836
		// (set) Token: 0x06000345 RID: 837
		public abstract Transform Follow { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00013CC7 File Offset: 0x00011EC7
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00013CCF File Offset: 0x00011ECF
		public virtual bool PreviousStateIsValid { get; set; }

		// Token: 0x06000348 RID: 840 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public void UpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			CinemachineCore.Instance.UpdateVirtualCamera(this, worldUp, deltaTime);
		}

		// Token: 0x06000349 RID: 841
		public abstract void InternalUpdateCameraState(Vector3 worldUp, float deltaTime);

		// Token: 0x0600034A RID: 842 RVA: 0x00013CE7 File Offset: 0x00011EE7
		public virtual void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				this.PreviousStateIsValid = false;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00013CFD File Offset: 0x00011EFD
		protected virtual void OnDestroy()
		{
			CinemachineCore.Instance.CameraDestroyed(this);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00013D0A File Offset: 0x00011F0A
		protected virtual void OnTransformParentChanged()
		{
			CinemachineCore.Instance.CameraDisabled(this);
			CinemachineCore.Instance.CameraEnabled(this);
			this.UpdateSlaveStatus();
			this.UpdateVcamPoolStatus();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00013D2E File Offset: 0x00011F2E
		protected virtual void Start()
		{
			this.m_WasStarted = true;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00013D37 File Offset: 0x00011F37
		internal virtual bool RequiresUserInput()
		{
			if (this.mExtensions != null)
			{
				return this.mExtensions.Any((CinemachineExtension extension) => extension != null && extension.RequiresUserInput);
			}
			return false;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00013D70 File Offset: 0x00011F70
		internal void EnsureStarted()
		{
			if (!this.m_WasStarted)
			{
				this.m_WasStarted = true;
				CinemachineExtension[] extensions = base.GetComponentsInChildren<CinemachineExtension>();
				for (int i = 0; i < extensions.Length; i++)
				{
					extensions[i].EnsureStarted();
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00013DAC File Offset: 0x00011FAC
		public AxisState.IInputAxisProvider GetInputAxisProvider()
		{
			MonoBehaviour[] components = base.GetComponentsInChildren<MonoBehaviour>();
			for (int i = 0; i < components.Length; i++)
			{
				AxisState.IInputAxisProvider provider = components[i] as AxisState.IInputAxisProvider;
				if (provider != null)
				{
					return provider;
				}
			}
			return null;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00013DDD File Offset: 0x00011FDD
		protected virtual void OnValidate()
		{
			this.m_OnValidateCalled = true;
			this.ValidatingStreamVersion = this.m_StreamingVersion;
			this.m_StreamingVersion = CinemachineCore.kStreamingVersion;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00013E00 File Offset: 0x00012000
		protected virtual void OnEnable()
		{
			this.UpdateSlaveStatus();
			this.UpdateVcamPoolStatus();
			if (!CinemachineCore.Instance.IsLive(this))
			{
				this.PreviousStateIsValid = false;
			}
			CinemachineCore.Instance.CameraEnabled(this);
			this.InvalidateCachedTargets();
			CinemachineVirtualCameraBase[] vcamComponents = base.GetComponents<CinemachineVirtualCameraBase>();
			for (int i = 0; i < vcamComponents.Length; i++)
			{
				if (vcamComponents[i].enabled && vcamComponents[i] != this)
				{
					Debug.LogError(this.Name + " has multiple CinemachineVirtualCameraBase-derived components.  Disabling " + base.GetType().Name + ".");
					base.enabled = false;
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00013E94 File Offset: 0x00012094
		protected virtual void OnDisable()
		{
			this.UpdateVcamPoolStatus();
			CinemachineCore.Instance.CameraDisabled(this);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00013EA7 File Offset: 0x000120A7
		protected virtual void Update()
		{
			if (this.m_Priority != this.m_QueuePriority)
			{
				this.UpdateVcamPoolStatus();
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00013EC0 File Offset: 0x000120C0
		private void UpdateSlaveStatus()
		{
			this.mSlaveStatusUpdated = true;
			this.m_parentVcam = null;
			Transform p = base.transform.parent;
			if (p != null)
			{
				p.TryGetComponent<CinemachineVirtualCameraBase>(out this.m_parentVcam);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00013F00 File Offset: 0x00012100
		public Transform ResolveLookAt(Transform localLookAt)
		{
			Transform lookAt = localLookAt;
			if (lookAt == null && this.ParentCamera != null)
			{
				lookAt = this.ParentCamera.LookAt;
			}
			return lookAt;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00013F30 File Offset: 0x00012130
		public Transform ResolveFollow(Transform localFollow)
		{
			Transform follow = localFollow;
			if (follow == null && this.ParentCamera != null)
			{
				follow = this.ParentCamera.Follow;
			}
			return follow;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00013F5D File Offset: 0x0001215D
		private void UpdateVcamPoolStatus()
		{
			CinemachineCore.Instance.RemoveActiveCamera(this);
			if (this.m_parentVcam == null && base.isActiveAndEnabled)
			{
				CinemachineCore.Instance.AddActiveCamera(this);
			}
			this.m_QueuePriority = this.m_Priority;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00013F97 File Offset: 0x00012197
		public void MoveToTopOfPrioritySubqueue()
		{
			this.UpdateVcamPoolStatus();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00013FA0 File Offset: 0x000121A0
		public virtual void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			if (this.mExtensions != null)
			{
				for (int i = 0; i < this.mExtensions.Count; i++)
				{
					this.mExtensions[i].OnTargetObjectWarped(target, positionDelta);
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00013FE0 File Offset: 0x000121E0
		public virtual void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			if (this.mExtensions != null)
			{
				for (int i = 0; i < this.mExtensions.Count; i++)
				{
					this.mExtensions[i].ForceCameraPosition(pos, rot);
				}
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001401E File Offset: 0x0001221E
		private bool GetInheritPosition(ICinemachineCamera cam)
		{
			if (cam is CinemachineVirtualCamera)
			{
				return (cam as CinemachineVirtualCamera).m_Transitions.m_InheritPosition;
			}
			return cam is CinemachineFreeLook && (cam as CinemachineFreeLook).m_Transitions.m_InheritPosition;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014054 File Offset: 0x00012254
		protected CinemachineBlend CreateBlend(ICinemachineCamera camA, ICinemachineCamera camB, CinemachineBlendDefinition blendDef, CinemachineBlend activeBlend)
		{
			if (blendDef.BlendCurve == null || blendDef.BlendTime <= 0f || (camA == null && camB == null))
			{
				this.m_blendStartPosition = 0f;
				return null;
			}
			if (activeBlend != null)
			{
				if (activeBlend != null && !activeBlend.IsComplete && activeBlend.CamA == camB && activeBlend.CamB == camA)
				{
					float progress = this.m_blendStartPosition + (1f - this.m_blendStartPosition) * activeBlend.TimeInBlend / activeBlend.Duration;
					blendDef.m_Time *= progress;
					this.m_blendStartPosition = 1f - progress;
				}
				else
				{
					this.m_blendStartPosition = 0f;
				}
				if (this.GetInheritPosition(camB))
				{
					camA = null;
				}
				else
				{
					camA = new BlendSourceVirtualCamera(activeBlend);
				}
			}
			if (camA == null)
			{
				camA = new StaticPointVirtualCamera(this.State, "(none)");
			}
			return new CinemachineBlend(camA, camB, blendDef.BlendCurve, blendDef.BlendTime, 0f);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00014144 File Offset: 0x00012344
		protected CameraState PullStateFromVirtualCamera(Vector3 worldUp, ref LensSettings lens)
		{
			CameraState state = CameraState.Default;
			state.RawPosition = TargetPositionCache.GetTargetPosition(base.transform);
			state.RawOrientation = TargetPositionCache.GetTargetRotation(base.transform);
			state.ReferenceUp = worldUp;
			CinemachineBrain brain = CinemachineCore.Instance.FindPotentialTargetBrain(this);
			if (brain != null)
			{
				lens.SnapshotCameraReadOnlyProperties(brain.OutputCamera);
			}
			state.Lens = lens;
			return state;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000141B2 File Offset: 0x000123B2
		private void InvalidateCachedTargets()
		{
			this.m_CachedFollowTarget = null;
			this.m_CachedFollowTargetVcam = null;
			this.m_CachedFollowTargetGroup = null;
			this.m_CachedLookAtTarget = null;
			this.m_CachedLookAtTargetVcam = null;
			this.m_CachedLookAtTargetGroup = null;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000141DE File Offset: 0x000123DE
		// (set) Token: 0x06000361 RID: 865 RVA: 0x000141E6 File Offset: 0x000123E6
		public bool FollowTargetChanged { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000141EF File Offset: 0x000123EF
		// (set) Token: 0x06000363 RID: 867 RVA: 0x000141F7 File Offset: 0x000123F7
		public bool LookAtTargetChanged { get; private set; }

		// Token: 0x06000364 RID: 868 RVA: 0x00014200 File Offset: 0x00012400
		protected void UpdateTargetCache()
		{
			Transform target = this.ResolveFollow(this.Follow);
			this.FollowTargetChanged = target != this.m_CachedFollowTarget;
			if (this.FollowTargetChanged)
			{
				this.m_CachedFollowTarget = target;
				this.m_CachedFollowTargetVcam = null;
				this.m_CachedFollowTargetGroup = null;
				if (this.m_CachedFollowTarget != null)
				{
					target.TryGetComponent<CinemachineVirtualCameraBase>(out this.m_CachedFollowTargetVcam);
					target.TryGetComponent<ICinemachineTargetGroup>(out this.m_CachedFollowTargetGroup);
				}
			}
			target = this.ResolveLookAt(this.LookAt);
			this.LookAtTargetChanged = target != this.m_CachedLookAtTarget;
			if (this.LookAtTargetChanged)
			{
				this.m_CachedLookAtTarget = target;
				this.m_CachedLookAtTargetVcam = null;
				this.m_CachedLookAtTargetGroup = null;
				if (target != null)
				{
					target.TryGetComponent<CinemachineVirtualCameraBase>(out this.m_CachedLookAtTargetVcam);
					target.TryGetComponent<ICinemachineTargetGroup>(out this.m_CachedLookAtTargetGroup);
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000142D0 File Offset: 0x000124D0
		public ICinemachineTargetGroup AbstractFollowTargetGroup
		{
			get
			{
				return this.m_CachedFollowTargetGroup;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000142D8 File Offset: 0x000124D8
		public CinemachineVirtualCameraBase FollowTargetAsVcam
		{
			get
			{
				return this.m_CachedFollowTargetVcam;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000142E0 File Offset: 0x000124E0
		public ICinemachineTargetGroup AbstractLookAtTargetGroup
		{
			get
			{
				return this.m_CachedLookAtTargetGroup;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000142E8 File Offset: 0x000124E8
		public CinemachineVirtualCameraBase LookAtTargetAsVcam
		{
			get
			{
				return this.m_CachedLookAtTargetVcam;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000142F0 File Offset: 0x000124F0
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.OnBeforeSerialize();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000142F8 File Offset: 0x000124F8
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_StreamingVersion < CinemachineCore.kStreamingVersion)
			{
				this.LegacyUpgrade(this.m_StreamingVersion);
			}
			this.m_StreamingVersion = CinemachineCore.kStreamingVersion;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000429A File Offset: 0x0000249A
		protected internal virtual void LegacyUpgrade(int streamedVersion)
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000429A File Offset: 0x0000249A
		internal virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00014320 File Offset: 0x00012520
		public void CancelDamping(bool updateNow = false)
		{
			this.PreviousStateIsValid = false;
			if (updateNow)
			{
				Vector3 up = this.State.ReferenceUp;
				CinemachineBrain brain = CinemachineCore.Instance.FindPotentialTargetBrain(this);
				if (brain != null)
				{
					up = brain.DefaultWorldUp;
				}
				this.InternalUpdateCameraState(up, -1f);
			}
		}

		// Token: 0x040002E1 RID: 737
		[HideInInspector]
		[SerializeField]
		[NoSaveDuringPlay]
		public string[] m_ExcludedPropertiesInInspector = new string[] { "m_Script" };

		// Token: 0x040002E2 RID: 738
		[HideInInspector]
		[SerializeField]
		[NoSaveDuringPlay]
		public CinemachineCore.Stage[] m_LockStageInInspector;

		// Token: 0x040002E3 RID: 739
		private int m_ValidatingStreamVersion;

		// Token: 0x040002E4 RID: 740
		private bool m_OnValidateCalled;

		// Token: 0x040002E5 RID: 741
		[HideInInspector]
		[SerializeField]
		[NoSaveDuringPlay]
		private int m_StreamingVersion;

		// Token: 0x040002E6 RID: 742
		[NoSaveDuringPlay]
		[Tooltip("The priority will determine which camera becomes active based on the state of other cameras and this camera.  Higher numbers have greater priority.")]
		public int m_Priority = 10;

		// Token: 0x040002E7 RID: 743
		internal int m_ActivationId;

		// Token: 0x040002E8 RID: 744
		[NonSerialized]
		public float FollowTargetAttachment;

		// Token: 0x040002E9 RID: 745
		[NonSerialized]
		public float LookAtTargetAttachment;

		// Token: 0x040002EA RID: 746
		[Tooltip("When the virtual camera is not live, this is how often the virtual camera will be updated.  Set this to tune for performance. Most of the time Never is fine, unless the virtual camera is doing shot evaluation.")]
		public CinemachineVirtualCameraBase.StandbyUpdateMode m_StandbyUpdate = CinemachineVirtualCameraBase.StandbyUpdateMode.RoundRobin;

		// Token: 0x040002ED RID: 749
		private bool m_WasStarted;

		// Token: 0x040002EE RID: 750
		private bool mSlaveStatusUpdated;

		// Token: 0x040002EF RID: 751
		private CinemachineVirtualCameraBase m_parentVcam;

		// Token: 0x040002F0 RID: 752
		private int m_QueuePriority = int.MaxValue;

		// Token: 0x040002F1 RID: 753
		private float m_blendStartPosition;

		// Token: 0x040002F2 RID: 754
		private Transform m_CachedFollowTarget;

		// Token: 0x040002F3 RID: 755
		private CinemachineVirtualCameraBase m_CachedFollowTargetVcam;

		// Token: 0x040002F4 RID: 756
		private ICinemachineTargetGroup m_CachedFollowTargetGroup;

		// Token: 0x040002F5 RID: 757
		private Transform m_CachedLookAtTarget;

		// Token: 0x040002F6 RID: 758
		private CinemachineVirtualCameraBase m_CachedLookAtTargetVcam;

		// Token: 0x040002F7 RID: 759
		private ICinemachineTargetGroup m_CachedLookAtTargetGroup;

		// Token: 0x0200008B RID: 139
		public enum StandbyUpdateMode
		{
			// Token: 0x040002FB RID: 763
			Never,
			// Token: 0x040002FC RID: 764
			Always,
			// Token: 0x040002FD RID: 765
			RoundRobin
		}

		// Token: 0x0200008C RID: 140
		public enum BlendHint
		{
			// Token: 0x040002FF RID: 767
			None,
			// Token: 0x04000300 RID: 768
			SphericalPosition,
			// Token: 0x04000301 RID: 769
			CylindricalPosition,
			// Token: 0x04000302 RID: 770
			ScreenSpaceAimWhenTargetsDiffer
		}

		// Token: 0x0200008D RID: 141
		[Serializable]
		public struct TransitionParams
		{
			// Token: 0x04000303 RID: 771
			[Tooltip("Hint for blending positions to and from this virtual camera")]
			[FormerlySerializedAs("m_PositionBlending")]
			public CinemachineVirtualCameraBase.BlendHint m_BlendHint;

			// Token: 0x04000304 RID: 772
			[Tooltip("When this virtual camera goes Live, attempt to force the position to be the same as the current position of the Unity Camera")]
			public bool m_InheritPosition;

			// Token: 0x04000305 RID: 773
			[Tooltip("This event fires when the virtual camera goes Live")]
			public CinemachineBrain.VcamActivatedEvent m_OnCameraLive;
		}
	}
}
