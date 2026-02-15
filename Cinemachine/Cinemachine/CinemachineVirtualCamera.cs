using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000043 RID: 67
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[ExcludeFromPreset]
	[AddComponentMenu("Cinemachine/CinemachineVirtualCamera")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineVirtualCamera.html")]
	public class CinemachineVirtualCamera : CinemachineVirtualCameraBase
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000B69A File Offset: 0x0000989A
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B6A2 File Offset: 0x000098A2
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000B6B0 File Offset: 0x000098B0
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B6B9 File Offset: 0x000098B9
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000B6C7 File Offset: 0x000098C7
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

		// Token: 0x0600018F RID: 399 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public override float GetMaxDampTime()
		{
			float maxDamp = base.GetMaxDampTime();
			this.UpdateComponentPipeline();
			if (this.m_ComponentPipeline != null)
			{
				for (int i = 0; i < this.m_ComponentPipeline.Length; i++)
				{
					maxDamp = Mathf.Max(maxDamp, this.m_ComponentPipeline[i].GetMaxDampTime());
				}
			}
			return maxDamp;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B71C File Offset: 0x0000991C
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			base.UpdateTargetCache();
			this.m_State = this.CalculateNewState(worldUp, deltaTime);
			base.ApplyPositionBlendMethod(ref this.m_State, this.m_Transitions.m_BlendHint);
			Vector3 pos;
			Quaternion rot;
			base.transform.GetPositionAndRotation(out pos, out rot);
			if (this.Follow != null)
			{
				pos = this.m_State.RawPosition;
			}
			if (this.LookAt != null)
			{
				rot = this.m_State.RawOrientation;
			}
			base.transform.ConservativeSetPositionAndRotation(pos, rot);
			this.PreviousStateIsValid = true;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000B7AC File Offset: 0x000099AC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_State = base.PullStateFromVirtualCamera(Vector3.up, ref this.m_Lens);
			this.InvalidateComponentPipeline();
			if (base.ValidatingStreamVersion < 20170927)
			{
				if (this.Follow != null && this.GetCinemachineComponent(CinemachineCore.Stage.Body) == null)
				{
					this.AddCinemachineComponent<CinemachineHardLockToTarget>();
				}
				if (this.LookAt != null && this.GetCinemachineComponent(CinemachineCore.Stage.Aim) == null)
				{
					this.AddCinemachineComponent<CinemachineHardLookAt>();
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000B834 File Offset: 0x00009A34
		protected override void OnDestroy()
		{
			foreach (object obj in base.transform)
			{
				Transform child = (Transform)obj;
				if (child.GetComponent<CinemachinePipeline>() != null)
				{
					child.gameObject.hideFlags &= ~(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
				}
			}
			base.OnDestroy();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		protected override void OnValidate()
		{
			base.OnValidate();
			this.m_Lens.Validate();
			if (this.m_LegacyBlendHint != CinemachineVirtualCameraBase.BlendHint.None)
			{
				this.m_Transitions.m_BlendHint = this.m_LegacyBlendHint;
				this.m_LegacyBlendHint = CinemachineVirtualCameraBase.BlendHint.None;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B8E3 File Offset: 0x00009AE3
		private void OnTransformChildrenChanged()
		{
			this.InvalidateComponentPipeline();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000B8EB File Offset: 0x00009AEB
		private void Reset()
		{
			this.DestroyPipeline();
			this.UpdateComponentPipeline();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000B8FC File Offset: 0x00009AFC
		internal void DestroyPipeline()
		{
			List<Transform> oldPipeline = new List<Transform>();
			foreach (object obj in base.transform)
			{
				Transform child = (Transform)obj;
				if (child.GetComponent<CinemachinePipeline>() != null)
				{
					oldPipeline.Add(child);
				}
			}
			foreach (Transform child2 in oldPipeline)
			{
				if (CinemachineVirtualCamera.DestroyPipelineOverride != null)
				{
					CinemachineVirtualCamera.DestroyPipelineOverride(child2.gameObject);
				}
				else
				{
					CinemachineComponentBase[] components = child2.GetComponents<CinemachineComponentBase>();
					for (int i = 0; i < components.Length; i++)
					{
						global::UnityEngine.Object.Destroy(components[i]);
					}
					if (!RuntimeUtility.IsPrefab(base.gameObject))
					{
						global::UnityEngine.Object.Destroy(child2.gameObject);
					}
				}
			}
			this.m_ComponentOwner = null;
			this.InvalidateComponentPipeline();
			this.PreviousStateIsValid = false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000BA10 File Offset: 0x00009C10
		internal Transform CreatePipeline(CinemachineVirtualCamera copyFrom)
		{
			CinemachineComponentBase[] components = null;
			if (copyFrom != null)
			{
				copyFrom.InvalidateComponentPipeline();
				components = copyFrom.GetComponentPipeline();
			}
			Transform newPipeline = null;
			if (CinemachineVirtualCamera.CreatePipelineOverride != null)
			{
				newPipeline = CinemachineVirtualCamera.CreatePipelineOverride(this, "cm", components);
			}
			else if (!RuntimeUtility.IsPrefab(base.gameObject))
			{
				GameObject gameObject = new GameObject("cm");
				gameObject.transform.parent = base.transform;
				gameObject.AddComponent<CinemachinePipeline>();
				newPipeline = gameObject.transform;
			}
			this.PreviousStateIsValid = false;
			return newPipeline;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000BA8F File Offset: 0x00009C8F
		public void InvalidateComponentPipeline()
		{
			this.m_ComponentPipeline = null;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000BA98 File Offset: 0x00009C98
		public Transform GetComponentOwner()
		{
			this.UpdateComponentPipeline();
			return this.m_ComponentOwner;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000BAA6 File Offset: 0x00009CA6
		public CinemachineComponentBase[] GetComponentPipeline()
		{
			this.UpdateComponentPipeline();
			return this.m_ComponentPipeline;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public CinemachineComponentBase GetCinemachineComponent(CinemachineCore.Stage stage)
		{
			CinemachineComponentBase[] components = this.GetComponentPipeline();
			if (components != null)
			{
				foreach (CinemachineComponentBase c in components)
				{
					if (c.Stage == stage)
					{
						return c;
					}
				}
			}
			return null;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public T GetCinemachineComponent<T>() where T : CinemachineComponentBase
		{
			CinemachineComponentBase[] components = this.GetComponentPipeline();
			if (components != null)
			{
				foreach (CinemachineComponentBase c in components)
				{
					if (c is T)
					{
						return c as T;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000BB38 File Offset: 0x00009D38
		public T AddCinemachineComponent<T>() where T : CinemachineComponentBase
		{
			Transform owner = this.GetComponentOwner();
			if (owner == null)
			{
				return default(T);
			}
			CinemachineComponentBase[] components = owner.GetComponents<CinemachineComponentBase>();
			T component = owner.gameObject.AddComponent<T>();
			if (component != null && components != null)
			{
				CinemachineCore.Stage stage = component.Stage;
				for (int i = components.Length - 1; i >= 0; i--)
				{
					if (components[i].Stage == stage)
					{
						components[i].enabled = false;
						RuntimeUtility.DestroyObject(components[i]);
					}
				}
			}
			this.InvalidateComponentPipeline();
			return component;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public void DestroyCinemachineComponent<T>() where T : CinemachineComponentBase
		{
			CinemachineComponentBase[] components = this.GetComponentPipeline();
			if (components != null)
			{
				foreach (CinemachineComponentBase c in components)
				{
					if (c is T)
					{
						c.enabled = false;
						RuntimeUtility.DestroyObject(c);
						this.InvalidateComponentPipeline();
					}
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000BC14 File Offset: 0x00009E14
		private void UpdateComponentPipeline()
		{
			if (this.m_ComponentOwner != null && this.m_ComponentPipeline != null)
			{
				return;
			}
			this.m_ComponentOwner = null;
			List<CinemachineComponentBase> list = new List<CinemachineComponentBase>();
			foreach (object obj in base.transform)
			{
				Transform child = (Transform)obj;
				if (child.GetComponent<CinemachinePipeline>() != null)
				{
					foreach (CinemachineComponentBase c in child.GetComponents<CinemachineComponentBase>())
					{
						if (c.enabled)
						{
							list.Add(c);
						}
					}
					this.m_ComponentOwner = child;
					break;
				}
			}
			if (this.m_ComponentOwner == null)
			{
				this.m_ComponentOwner = this.CreatePipeline(null);
			}
			if (this.m_ComponentOwner != null && this.m_ComponentOwner.gameObject != null)
			{
				list.Sort((CinemachineComponentBase c1, CinemachineComponentBase c2) => c1.Stage - c2.Stage);
				this.m_ComponentPipeline = list.ToArray();
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000BD44 File Offset: 0x00009F44
		internal static void SetFlagsForHiddenChild(GameObject child)
		{
			if (child != null)
			{
				if (CinemachineCore.sShowHiddenObjects)
				{
					child.hideFlags &= ~(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
					return;
				}
				child.hideFlags |= HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000BD74 File Offset: 0x00009F74
		private CameraState CalculateNewState(Vector3 worldUp, float deltaTime)
		{
			this.FollowTargetAttachment = 1f;
			this.LookAtTargetAttachment = 1f;
			CameraState state = base.PullStateFromVirtualCamera(worldUp, ref this.m_Lens);
			Transform lookAtTarget = this.LookAt;
			if (lookAtTarget != this.mCachedLookAtTarget)
			{
				this.mCachedLookAtTarget = lookAtTarget;
				this.mCachedLookAtTargetVcam = null;
				if (lookAtTarget != null)
				{
					this.mCachedLookAtTargetVcam = lookAtTarget.GetComponent<CinemachineVirtualCameraBase>();
				}
			}
			if (lookAtTarget != null)
			{
				if (this.mCachedLookAtTargetVcam != null)
				{
					state.ReferenceLookAt = this.mCachedLookAtTargetVcam.State.FinalPosition;
				}
				else
				{
					state.ReferenceLookAt = TargetPositionCache.GetTargetPosition(lookAtTarget);
				}
			}
			this.UpdateComponentPipeline();
			base.InvokePrePipelineMutateCameraStateCallback(this, ref state, deltaTime);
			bool haveAim = false;
			if (this.m_ComponentPipeline == null)
			{
				for (CinemachineCore.Stage stage = CinemachineCore.Stage.Body; stage <= CinemachineCore.Stage.Finalize; stage++)
				{
					base.InvokePostPipelineStageCallback(this, stage, ref state, deltaTime);
				}
			}
			else
			{
				for (int i = 0; i < this.m_ComponentPipeline.Length; i++)
				{
					if (this.m_ComponentPipeline[i] != null)
					{
						this.m_ComponentPipeline[i].PrePipelineMutateCameraState(ref state, deltaTime);
					}
				}
				int componentIndex = 0;
				CinemachineComponentBase postAimBody = null;
				CinemachineCore.Stage stage2 = CinemachineCore.Stage.Body;
				while (stage2 <= CinemachineCore.Stage.Finalize)
				{
					CinemachineComponentBase c = ((componentIndex < this.m_ComponentPipeline.Length) ? this.m_ComponentPipeline[componentIndex] : null);
					if (!(c != null) || stage2 != c.Stage)
					{
						goto IL_0176;
					}
					componentIndex++;
					if (stage2 != CinemachineCore.Stage.Body || !c.BodyAppliesAfterAim)
					{
						c.MutateCameraState(ref state, deltaTime);
						haveAim = stage2 == CinemachineCore.Stage.Aim;
						goto IL_0176;
					}
					postAimBody = c;
					IL_01A6:
					stage2++;
					continue;
					IL_0176:
					base.InvokePostPipelineStageCallback(this, stage2, ref state, deltaTime);
					if (stage2 == CinemachineCore.Stage.Aim && postAimBody != null)
					{
						postAimBody.MutateCameraState(ref state, deltaTime);
						base.InvokePostPipelineStageCallback(this, CinemachineCore.Stage.Body, ref state, deltaTime);
						goto IL_01A6;
					}
					goto IL_01A6;
				}
			}
			if (!haveAim)
			{
				state.BlendHint |= CameraState.BlendHintValue.IgnoreLookAtTarget;
			}
			return state;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000BF48 File Offset: 0x0000A148
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			if (target == this.Follow)
			{
				base.transform.position += positionDelta;
				this.m_State.RawPosition = this.m_State.RawPosition + positionDelta;
			}
			this.UpdateComponentPipeline();
			if (this.m_ComponentPipeline != null)
			{
				for (int i = 0; i < this.m_ComponentPipeline.Length; i++)
				{
					this.m_ComponentPipeline[i].OnTargetObjectWarped(target, positionDelta);
				}
			}
			base.OnTargetObjectWarped(target, positionDelta);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			this.PreviousStateIsValid = true;
			base.transform.ConservativeSetPositionAndRotation(pos, rot);
			this.m_State.RawPosition = pos;
			this.m_State.RawOrientation = rot;
			this.UpdateComponentPipeline();
			if (this.m_ComponentPipeline != null)
			{
				for (int i = 0; i < this.m_ComponentPipeline.Length; i++)
				{
					this.m_ComponentPipeline[i].ForceCameraPosition(pos, rot);
				}
			}
			base.ForceCameraPosition(pos, rot);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000C041 File Offset: 0x0000A241
		internal void SetStateRawPosition(Vector3 pos)
		{
			this.m_State.RawPosition = pos;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C050 File Offset: 0x0000A250
		public override void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
			base.InvokeOnTransitionInExtensions(fromCam, worldUp, deltaTime);
			bool forceUpdate = false;
			if (this.m_Transitions.m_InheritPosition && fromCam != null && !CinemachineCore.Instance.IsLiveInBlend(this))
			{
				this.ForceCameraPosition(fromCam.State.FinalPosition, fromCam.State.FinalOrientation);
			}
			this.UpdateComponentPipeline();
			if (this.m_ComponentPipeline != null)
			{
				for (int i = 0; i < this.m_ComponentPipeline.Length; i++)
				{
					if (this.m_ComponentPipeline[i].OnTransitionFromCamera(fromCam, worldUp, deltaTime, ref this.m_Transitions))
					{
						forceUpdate = true;
					}
				}
			}
			if (forceUpdate)
			{
				this.InternalUpdateCameraState(worldUp, deltaTime);
				this.InternalUpdateCameraState(worldUp, deltaTime);
			}
			else
			{
				base.UpdateCameraState(worldUp, deltaTime);
			}
			if (this.m_Transitions.m_OnCameraLive != null)
			{
				this.m_Transitions.m_OnCameraLive.Invoke(this, fromCam);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C128 File Offset: 0x0000A328
		internal override bool RequiresUserInput()
		{
			if (base.RequiresUserInput())
			{
				return true;
			}
			if (this.m_ComponentPipeline != null)
			{
				return this.m_ComponentPipeline.Any((CinemachineComponentBase c) => c != null && c.RequiresUserInput);
			}
			return false;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C168 File Offset: 0x0000A368
		internal override void OnBeforeSerialize()
		{
			if (!this.m_Lens.IsPhysicalCamera)
			{
				this.m_Lens.SensorSize = Vector2.one;
			}
		}

		// Token: 0x04000153 RID: 339
		[Tooltip("The object that the camera wants to look at (the Aim target).  If this is null, then the vcam's Transform orientation will define the camera's orientation.")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_LookAt;

		// Token: 0x04000154 RID: 340
		[Tooltip("The object that the camera wants to move with (the Body target).  If this is null, then the vcam's Transform position will define the camera's position.")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_Follow;

		// Token: 0x04000155 RID: 341
		[FormerlySerializedAs("m_LensAttributes")]
		[Tooltip("Specifies the lens properties of this Virtual Camera.  This generally mirrors the Unity Camera's lens settings, and will be used to drive the Unity camera when the vcam is active.")]
		public LensSettings m_Lens = LensSettings.Default;

		// Token: 0x04000156 RID: 342
		public CinemachineVirtualCameraBase.TransitionParams m_Transitions;

		// Token: 0x04000157 RID: 343
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_BlendHint")]
		[FormerlySerializedAs("m_PositionBlending")]
		private CinemachineVirtualCameraBase.BlendHint m_LegacyBlendHint;

		// Token: 0x04000158 RID: 344
		public const string PipelineName = "cm";

		// Token: 0x04000159 RID: 345
		public static CinemachineVirtualCamera.CreatePipelineDelegate CreatePipelineOverride;

		// Token: 0x0400015A RID: 346
		public static CinemachineVirtualCamera.DestroyPipelineDelegate DestroyPipelineOverride;

		// Token: 0x0400015B RID: 347
		private CameraState m_State = CameraState.Default;

		// Token: 0x0400015C RID: 348
		private CinemachineComponentBase[] m_ComponentPipeline;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		[HideInInspector]
		private Transform m_ComponentOwner;

		// Token: 0x0400015E RID: 350
		private Transform mCachedLookAtTarget;

		// Token: 0x0400015F RID: 351
		private CinemachineVirtualCameraBase mCachedLookAtTargetVcam;

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060001AA RID: 426
		public delegate Transform CreatePipelineDelegate(CinemachineVirtualCamera vcam, string name, CinemachineComponentBase[] copyFrom);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x060001AE RID: 430
		public delegate void DestroyPipelineDelegate(GameObject pipeline);
	}
}
