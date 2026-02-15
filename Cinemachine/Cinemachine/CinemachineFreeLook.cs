using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000028 RID: 40
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[ExcludeFromPreset]
	[AddComponentMenu("Cinemachine/CinemachineFreeLook")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineFreeLook.html")]
	public class CinemachineFreeLook : CinemachineVirtualCameraBase
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x000070C8 File Offset: 0x000052C8
		protected override void OnValidate()
		{
			base.OnValidate();
			if (this.m_LegacyHeadingBias != 3.4028235E+38f)
			{
				this.m_Heading.m_Bias = this.m_LegacyHeadingBias;
				this.m_LegacyHeadingBias = float.MaxValue;
				int heading = (int)this.m_Heading.m_Definition;
				if (this.m_RecenterToTargetHeading.LegacyUpgrade(ref heading, ref this.m_Heading.m_VelocityFilterStrength))
				{
					this.m_Heading.m_Definition = (CinemachineOrbitalTransposer.Heading.HeadingDefinition)heading;
				}
				this.mUseLegacyRigDefinitions = true;
			}
			if (this.m_LegacyBlendHint != CinemachineVirtualCameraBase.BlendHint.None)
			{
				this.m_Transitions.m_BlendHint = this.m_LegacyBlendHint;
				this.m_LegacyBlendHint = CinemachineVirtualCameraBase.BlendHint.None;
			}
			this.m_YAxis.Validate();
			this.m_XAxis.Validate();
			this.m_RecenterToTargetHeading.Validate();
			this.m_YAxisRecentering.Validate();
			this.m_Lens.Validate();
			this.InvalidateRigCache();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000719A File Offset: 0x0000539A
		public CinemachineVirtualCamera GetRig(int i)
		{
			if (!this.UpdateRigCache() || i < 0 || i >= 3)
			{
				return null;
			}
			return this.m_Rigs[i];
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000071B6 File Offset: 0x000053B6
		internal bool RigsAreCreated
		{
			get
			{
				return this.m_Rigs != null && this.m_Rigs.Length == 3;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000071CD File Offset: 0x000053CD
		public static string[] RigNames
		{
			get
			{
				return new string[] { "TopRig", "MiddleRig", "BottomRig" };
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000071ED File Offset: 0x000053ED
		protected override void OnEnable()
		{
			this.mIsDestroyed = false;
			base.OnEnable();
			this.InvalidateRigCache();
			this.UpdateInputAxisProvider();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007208 File Offset: 0x00005408
		public void UpdateInputAxisProvider()
		{
			this.m_XAxis.SetInputAxisProvider(0, null);
			this.m_YAxis.SetInputAxisProvider(1, null);
			AxisState.IInputAxisProvider provider = base.GetInputAxisProvider();
			if (provider != null)
			{
				this.m_XAxis.SetInputAxisProvider(0, provider);
				this.m_YAxis.SetInputAxisProvider(1, provider);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007254 File Offset: 0x00005454
		protected override void OnDestroy()
		{
			if (this.m_Rigs != null)
			{
				foreach (CinemachineVirtualCamera rig in this.m_Rigs)
				{
					if (rig != null && rig.gameObject != null)
					{
						rig.gameObject.hideFlags &= ~(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
					}
				}
			}
			this.mIsDestroyed = true;
			base.OnDestroy();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000072BA File Offset: 0x000054BA
		private void OnTransformChildrenChanged()
		{
			this.InvalidateRigCache();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000072C2 File Offset: 0x000054C2
		private void Reset()
		{
			this.DestroyRigs();
			this.UpdateRigCache();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000072D1 File Offset: 0x000054D1
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000072DC File Offset: 0x000054DC
		public override bool PreviousStateIsValid
		{
			get
			{
				return base.PreviousStateIsValid;
			}
			set
			{
				if (!value)
				{
					int i = 0;
					while (this.m_Rigs != null && i < this.m_Rigs.Length)
					{
						if (this.m_Rigs[i] != null)
						{
							this.m_Rigs[i].PreviousStateIsValid = value;
						}
						i++;
					}
				}
				base.PreviousStateIsValid = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000732C File Offset: 0x0000552C
		public override CameraState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00007334 File Offset: 0x00005534
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00007342 File Offset: 0x00005542
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000734B File Offset: 0x0000554B
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00007359 File Offset: 0x00005559
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

		// Token: 0x060000E5 RID: 229 RVA: 0x00007364 File Offset: 0x00005564
		public override bool IsLiveChild(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			if (!this.RigsAreCreated)
			{
				return false;
			}
			float y = this.GetYAxisValue();
			if (dominantChildOnly)
			{
				if (vcam == this.m_Rigs[0])
				{
					return y > 0.666f;
				}
				if (vcam == this.m_Rigs[2])
				{
					return (double)y < 0.333;
				}
				return vcam == this.m_Rigs[1] && y >= 0.333f && y <= 0.666f;
			}
			else
			{
				if (vcam == this.m_Rigs[1])
				{
					return true;
				}
				if (y < 0.5f)
				{
					return vcam == this.m_Rigs[2];
				}
				return vcam == this.m_Rigs[0];
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007404 File Offset: 0x00005604
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			this.UpdateRigCache();
			if (this.RigsAreCreated)
			{
				CinemachineVirtualCamera[] rigs = this.m_Rigs;
				for (int i = 0; i < rigs.Length; i++)
				{
					rigs[i].OnTargetObjectWarped(target, positionDelta);
				}
			}
			base.OnTargetObjectWarped(target, positionDelta);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007448 File Offset: 0x00005648
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			Vector3 up = this.m_State.ReferenceUp;
			this.m_YAxis.Value = this.GetYAxisClosestValue(pos, up);
			this.PreviousStateIsValid = true;
			base.transform.ConservativeSetPositionAndRotation(pos, rot);
			this.m_State.RawPosition = pos;
			this.m_State.RawOrientation = rot;
			if (this.UpdateRigCache())
			{
				for (int i = 0; i < 3; i++)
				{
					this.m_Rigs[i].ForceCameraPosition(pos, rot);
				}
				if (this.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					this.m_XAxis.Value = this.mOrbitals[1].m_XAxis.Value;
				}
				this.PushSettingsToRigs();
				this.InternalUpdateCameraState(up, -1f);
			}
			base.ForceCameraPosition(pos, rot);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007504 File Offset: 0x00005704
		public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			base.UpdateTargetCache();
			this.UpdateRigCache();
			if (!this.RigsAreCreated)
			{
				return;
			}
			this.m_State = this.CalculateNewState(worldUp, deltaTime);
			base.ApplyPositionBlendMethod(ref this.m_State, this.m_Transitions.m_BlendHint);
			if (this.Follow != null)
			{
				Vector3 delta = this.m_State.RawPosition - base.transform.position;
				base.transform.position = this.m_State.RawPosition;
				this.m_Rigs[0].transform.position -= delta;
				this.m_Rigs[1].transform.position -= delta;
				this.m_Rigs[2].transform.position -= delta;
			}
			base.InvokePostPipelineStageCallback(this, CinemachineCore.Stage.Finalize, ref this.m_State, deltaTime);
			if (this.PreviousStateIsValid && CinemachineCore.Instance.IsLive(this) && deltaTime >= 0f && this.m_YAxis.Update(deltaTime))
			{
				this.m_YAxisRecentering.CancelRecentering();
			}
			this.PushSettingsToRigs();
			if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
			{
				this.m_XAxis.Value = 0f;
			}
			this.PreviousStateIsValid = true;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007658 File Offset: 0x00005858
		public override void OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
			if (!this.RigsAreCreated)
			{
				return;
			}
			base.InvokeOnTransitionInExtensions(fromCam, worldUp, deltaTime);
			if (fromCam != null && this.m_Transitions.m_InheritPosition && !CinemachineCore.Instance.IsLiveInBlend(this))
			{
				Vector3 cameraPos = fromCam.State.RawPosition;
				if (fromCam is CinemachineFreeLook)
				{
					CinemachineFreeLook flFrom = fromCam as CinemachineFreeLook;
					CinemachineOrbitalTransposer orbital = ((flFrom.mOrbitals != null) ? flFrom.mOrbitals[1] : null);
					if (orbital != null)
					{
						cameraPos = orbital.GetTargetCameraPosition(worldUp);
					}
				}
				this.ForceCameraPosition(cameraPos, fromCam.State.FinalOrientation);
			}
			base.UpdateCameraState(worldUp, deltaTime);
			if (this.m_Transitions.m_OnCameraLive != null)
			{
				this.m_Transitions.m_OnCameraLive.Invoke(this, fromCam);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000771A File Offset: 0x0000591A
		internal override bool RequiresUserInput()
		{
			return true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007720 File Offset: 0x00005920
		private float GetYAxisClosestValue(Vector3 cameraPos, Vector3 up)
		{
			if (this.Follow != null)
			{
				Vector3 dir = Quaternion.FromToRotation(up, Vector3.up) * (cameraPos - this.Follow.position);
				Vector3 flatDir = dir;
				flatDir.y = 0f;
				if (!flatDir.AlmostZero())
				{
					dir = Quaternion.AngleAxis(UnityVectorExtensions.SignedAngle(flatDir, Vector3.back, Vector3.up), Vector3.up) * dir;
				}
				dir.x = 0f;
				return this.SteepestDescent(dir.normalized * (cameraPos - this.Follow.position).magnitude);
			}
			return this.m_YAxis.Value;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000077DC File Offset: 0x000059DC
		private float SteepestDescent(Vector3 cameraOffset)
		{
			CinemachineFreeLook.<>c__DisplayClass47_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cameraOffset = cameraOffset;
			float x = this.<SteepestDescent>g__InitialGuess|47_2(ref CS$<>8__locals1);
			for (int i = 0; i < 10; i++)
			{
				float angle = this.<SteepestDescent>g__AngleFunction|47_0(x, ref CS$<>8__locals1);
				float slope = this.<SteepestDescent>g__SlopeOfAngleFunction|47_1(x, ref CS$<>8__locals1);
				if (Mathf.Abs(slope) < 0.005f || Mathf.Abs(angle) < 0.005f)
				{
					break;
				}
				x = Mathf.Clamp01(x - angle / slope);
			}
			return x;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000784C File Offset: 0x00005A4C
		private void InvalidateRigCache()
		{
			this.mOrbitals = null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007858 File Offset: 0x00005A58
		private void DestroyRigs()
		{
			List<CinemachineVirtualCamera> rigs = new List<CinemachineVirtualCamera>(3);
			for (int i = 0; i < CinemachineFreeLook.RigNames.Length; i++)
			{
				foreach (object obj in base.transform)
				{
					Transform child = (Transform)obj;
					if (child.gameObject.name == CinemachineFreeLook.RigNames[i])
					{
						rigs.Add(child.GetComponent<CinemachineVirtualCamera>());
					}
				}
			}
			foreach (CinemachineVirtualCamera rig in rigs)
			{
				if (rig != null)
				{
					if (CinemachineFreeLook.DestroyRigOverride != null)
					{
						CinemachineFreeLook.DestroyRigOverride(rig.gameObject);
					}
					else
					{
						rig.DestroyPipeline();
						global::UnityEngine.Object.Destroy(rig);
						if (!RuntimeUtility.IsPrefab(base.gameObject))
						{
							global::UnityEngine.Object.Destroy(rig.gameObject);
						}
					}
				}
			}
			this.mOrbitals = null;
			this.m_Rigs = null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000797C File Offset: 0x00005B7C
		private CinemachineVirtualCamera[] CreateRigs(CinemachineVirtualCamera[] copyFrom)
		{
			float[] softCenterDefaultsV = new float[] { 0.5f, 0.55f, 0.6f };
			this.mOrbitals = null;
			this.m_Rigs = null;
			CinemachineVirtualCamera[] newRigs = new CinemachineVirtualCamera[3];
			for (int i = 0; i < newRigs.Length; i++)
			{
				CinemachineVirtualCamera src = ((copyFrom != null && copyFrom.Length > i) ? copyFrom[i] : null);
				if (CinemachineFreeLook.CreateRigOverride != null)
				{
					newRigs[i] = CinemachineFreeLook.CreateRigOverride(this, CinemachineFreeLook.RigNames[i], src);
				}
				else
				{
					GameObject go = null;
					foreach (object obj in base.transform)
					{
						Transform child = (Transform)obj;
						if (child.gameObject.name == CinemachineFreeLook.RigNames[i])
						{
							go = child.gameObject;
							break;
						}
					}
					if (go == null && !RuntimeUtility.IsPrefab(base.gameObject))
					{
						go = new GameObject(CinemachineFreeLook.RigNames[i]);
						go.transform.parent = base.transform;
					}
					if (go == null)
					{
						newRigs[i] = null;
					}
					else
					{
						newRigs[i] = go.AddComponent<CinemachineVirtualCamera>();
						newRigs[i].AddCinemachineComponent<CinemachineOrbitalTransposer>();
						newRigs[i].AddCinemachineComponent<CinemachineComposer>();
					}
				}
				if (newRigs[i] != null)
				{
					newRigs[i].InvalidateComponentPipeline();
					CinemachineOrbitalTransposer orbital = newRigs[i].GetCinemachineComponent<CinemachineOrbitalTransposer>();
					if (orbital == null)
					{
						orbital = newRigs[i].AddCinemachineComponent<CinemachineOrbitalTransposer>();
					}
					if (src == null)
					{
						orbital.m_YawDamping = 0f;
						CinemachineComposer composer = newRigs[i].GetCinemachineComponent<CinemachineComposer>();
						if (composer != null)
						{
							composer.m_HorizontalDamping = (composer.m_VerticalDamping = 0f);
							composer.m_ScreenX = 0.5f;
							composer.m_ScreenY = softCenterDefaultsV[i];
							composer.m_DeadZoneWidth = (composer.m_DeadZoneHeight = 0f);
							composer.m_SoftZoneWidth = (composer.m_SoftZoneHeight = 0.8f);
							composer.m_BiasX = (composer.m_BiasY = 0f);
						}
					}
				}
			}
			return newRigs;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007BA8 File Offset: 0x00005DA8
		private bool UpdateRigCache()
		{
			if (this.mIsDestroyed)
			{
				return false;
			}
			if (this.mOrbitals != null && this.mOrbitals.Length == 3)
			{
				return true;
			}
			this.m_CachedXAxisHeading = 0f;
			this.m_Rigs = null;
			this.mOrbitals = null;
			List<CinemachineVirtualCamera> rigs = this.LocateExistingRigs(false);
			if (rigs == null || rigs.Count != 3)
			{
				this.DestroyRigs();
				this.CreateRigs(null);
				rigs = this.LocateExistingRigs(true);
			}
			if (rigs != null && rigs.Count == 3)
			{
				this.m_Rigs = rigs.ToArray();
			}
			if (this.RigsAreCreated)
			{
				this.mOrbitals = new CinemachineOrbitalTransposer[this.m_Rigs.Length];
				for (int i = 0; i < this.m_Rigs.Length; i++)
				{
					this.mOrbitals[i] = this.m_Rigs[i].GetCinemachineComponent<CinemachineOrbitalTransposer>();
				}
				this.mBlendA = new CinemachineBlend(this.m_Rigs[1], this.m_Rigs[0], AnimationCurve.Linear(0f, 0f, 1f, 1f), 1f, 0f);
				this.mBlendB = new CinemachineBlend(this.m_Rigs[2], this.m_Rigs[1], AnimationCurve.Linear(0f, 0f, 1f, 1f), 1f, 0f);
				return true;
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007CF4 File Offset: 0x00005EF4
		private List<CinemachineVirtualCamera> LocateExistingRigs(bool forceOrbital)
		{
			this.m_CachedXAxisHeading = this.m_XAxis.Value;
			this.m_LastHeadingUpdateFrame = -1f;
			List<CinemachineVirtualCamera> rigs = new List<CinemachineVirtualCamera>(3);
			foreach (object obj in base.transform)
			{
				Transform child = (Transform)obj;
				CinemachineVirtualCamera vcam = child.GetComponent<CinemachineVirtualCamera>();
				if (vcam != null)
				{
					GameObject go = child.gameObject;
					for (int i = 0; i < CinemachineFreeLook.RigNames.Length; i++)
					{
						if (!(go.name != CinemachineFreeLook.RigNames[i]))
						{
							CinemachineOrbitalTransposer orbital = vcam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
							if (orbital == null && forceOrbital)
							{
								orbital = vcam.AddCinemachineComponent<CinemachineOrbitalTransposer>();
							}
							if (orbital != null)
							{
								orbital.m_HeadingIsSlave = true;
								orbital.HideOffsetInInspector = true;
								orbital.m_XAxis.m_InputAxisName = string.Empty;
								orbital.HeadingUpdater = new CinemachineOrbitalTransposer.UpdateHeadingDelegate(this.UpdateXAxisHeading);
								orbital.m_RecenterToTargetHeading.m_enabled = false;
								vcam.m_StandbyUpdate = this.m_StandbyUpdate;
								rigs.Add(vcam);
							}
						}
					}
				}
			}
			return rigs;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007E3C File Offset: 0x0000603C
		private float UpdateXAxisHeading(CinemachineOrbitalTransposer orbital, float deltaTime, Vector3 up)
		{
			if (this == null)
			{
				return 0f;
			}
			if (!this.PreviousStateIsValid)
			{
				deltaTime = -1f;
			}
			if (this.m_LastHeadingUpdateFrame != (float)Time.frameCount || deltaTime < 0f)
			{
				this.m_LastHeadingUpdateFrame = (float)Time.frameCount;
				float oldValue = this.m_XAxis.Value;
				this.m_CachedXAxisHeading = orbital.UpdateHeading(deltaTime, up, ref this.m_XAxis, ref this.m_RecenterToTargetHeading, CinemachineCore.Instance.IsLive(this));
				if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					this.m_XAxis.Value = oldValue;
				}
			}
			return this.m_CachedXAxisHeading;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007ED8 File Offset: 0x000060D8
		private void PushSettingsToRigs()
		{
			for (int i = 0; i < this.m_Rigs.Length; i++)
			{
				if (this.m_CommonLens)
				{
					this.m_Rigs[i].m_Lens = this.m_Lens;
				}
				if (this.mUseLegacyRigDefinitions)
				{
					this.mUseLegacyRigDefinitions = false;
					this.m_Orbits[i].m_Height = this.mOrbitals[i].m_FollowOffset.y;
					this.m_Orbits[i].m_Radius = -this.mOrbitals[i].m_FollowOffset.z;
					if (this.m_Rigs[i].Follow != null)
					{
						this.Follow = this.m_Rigs[i].Follow;
					}
				}
				this.m_Rigs[i].Follow = null;
				this.m_Rigs[i].m_StandbyUpdate = this.m_StandbyUpdate;
				this.m_Rigs[i].FollowTargetAttachment = this.FollowTargetAttachment;
				this.m_Rigs[i].LookAtTargetAttachment = this.LookAtTargetAttachment;
				if (!this.PreviousStateIsValid)
				{
					this.m_Rigs[i].PreviousStateIsValid = false;
					this.m_Rigs[i].transform.ConservativeSetPositionAndRotation(base.transform.position, base.transform.rotation);
				}
				this.mOrbitals[i].m_FollowOffset = this.GetLocalPositionForCameraFromInput(this.GetYAxisValue());
				this.mOrbitals[i].m_BindingMode = this.m_BindingMode;
				this.mOrbitals[i].m_Heading = this.m_Heading;
				this.mOrbitals[i].m_XAxis.Value = this.m_XAxis.Value;
				if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					this.m_Rigs[i].SetStateRawPosition(this.State.RawPosition);
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000809C File Offset: 0x0000629C
		private float GetYAxisValue()
		{
			float range = this.m_YAxis.m_MaxValue - this.m_YAxis.m_MinValue;
			if (range <= 0.0001f)
			{
				return 0.5f;
			}
			return this.m_YAxis.Value / range;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000080DC File Offset: 0x000062DC
		private CameraState CalculateNewState(Vector3 worldUp, float deltaTime)
		{
			CameraState state = base.PullStateFromVirtualCamera(worldUp, ref this.m_Lens);
			this.m_YAxisRecentering.DoRecentering(ref this.m_YAxis, deltaTime, 0.5f);
			float t = this.GetYAxisValue();
			if (t > 0.5f)
			{
				if (this.mBlendA != null)
				{
					this.mBlendA.TimeInBlend = (t - 0.5f) * 2f;
					this.mBlendA.UpdateCameraState(worldUp, deltaTime);
					state = this.mBlendA.State;
				}
			}
			else if (this.mBlendB != null)
			{
				this.mBlendB.TimeInBlend = t * 2f;
				this.mBlendB.UpdateCameraState(worldUp, deltaTime);
				state = this.mBlendB.State;
			}
			return state;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000818C File Offset: 0x0000638C
		public Vector3 GetLocalPositionForCameraFromInput(float t)
		{
			if (this.mOrbitals == null)
			{
				return Vector3.zero;
			}
			this.UpdateCachedSpline();
			int i = 1;
			if (t > 0.5f)
			{
				t -= 0.5f;
				i = 2;
			}
			return SplineHelpers.Bezier3(t * 2f, this.m_CachedKnots[i], this.m_CachedCtrl1[i], this.m_CachedCtrl2[i], this.m_CachedKnots[i + 1]);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008214 File Offset: 0x00006414
		private void UpdateCachedSpline()
		{
			bool cacheIsValid = this.m_CachedOrbits != null && this.m_CachedOrbits.Length == 3 && this.m_CachedTension == this.m_SplineCurvature;
			int i = 0;
			while (i < 3 && cacheIsValid)
			{
				cacheIsValid = this.m_CachedOrbits[i].m_Height == this.m_Orbits[i].m_Height && this.m_CachedOrbits[i].m_Radius == this.m_Orbits[i].m_Radius;
				i++;
			}
			if (!cacheIsValid)
			{
				float t = this.m_SplineCurvature;
				this.m_CachedKnots = new Vector4[5];
				this.m_CachedCtrl1 = new Vector4[5];
				this.m_CachedCtrl2 = new Vector4[5];
				this.m_CachedKnots[1] = new Vector4(0f, this.m_Orbits[2].m_Height, -this.m_Orbits[2].m_Radius, 0f);
				this.m_CachedKnots[2] = new Vector4(0f, this.m_Orbits[1].m_Height, -this.m_Orbits[1].m_Radius, 0f);
				this.m_CachedKnots[3] = new Vector4(0f, this.m_Orbits[0].m_Height, -this.m_Orbits[0].m_Radius, 0f);
				this.m_CachedKnots[0] = Vector4.Lerp(this.m_CachedKnots[1], Vector4.zero, t);
				this.m_CachedKnots[4] = Vector4.Lerp(this.m_CachedKnots[3], Vector4.zero, t);
				SplineHelpers.ComputeSmoothControlPoints(ref this.m_CachedKnots, ref this.m_CachedCtrl1, ref this.m_CachedCtrl2);
				this.m_CachedOrbits = new CinemachineFreeLook.Orbit[3];
				for (int j = 0; j < 3; j++)
				{
					this.m_CachedOrbits[j] = this.m_Orbits[j];
				}
				this.m_CachedTension = this.m_SplineCurvature;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008426 File Offset: 0x00006626
		internal override void OnBeforeSerialize()
		{
			if (!this.m_Lens.IsPhysicalCamera)
			{
				this.m_Lens.SensorSize = Vector2.one;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008588 File Offset: 0x00006788
		[CompilerGenerated]
		private float <SteepestDescent>g__AngleFunction|47_0(float input, ref CinemachineFreeLook.<>c__DisplayClass47_0 A_2)
		{
			Vector3 point = this.GetLocalPositionForCameraFromInput(input);
			return Mathf.Abs(UnityVectorExtensions.SignedAngle(A_2.cameraOffset, point, Vector3.right));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000085B4 File Offset: 0x000067B4
		[CompilerGenerated]
		private float <SteepestDescent>g__SlopeOfAngleFunction|47_1(float input, ref CinemachineFreeLook.<>c__DisplayClass47_0 A_2)
		{
			float angleBehind = this.<SteepestDescent>g__AngleFunction|47_0(input - 0.005f, ref A_2);
			return (this.<SteepestDescent>g__AngleFunction|47_0(input + 0.005f, ref A_2) - angleBehind) / 0.01f;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000085E8 File Offset: 0x000067E8
		[CompilerGenerated]
		private float <SteepestDescent>g__InitialGuess|47_2(ref CinemachineFreeLook.<>c__DisplayClass47_0 A_1)
		{
			this.UpdateCachedSpline();
			CinemachineFreeLook.<>c__DisplayClass47_1 CS$<>8__locals1;
			CS$<>8__locals1.best = 0.5f;
			CS$<>8__locals1.bestAngle = this.<SteepestDescent>g__AngleFunction|47_0(CS$<>8__locals1.best, ref A_1);
			for (int i = 0; i <= 5; i++)
			{
				float t = (float)i * 0.1f;
				this.<SteepestDescent>g__ChooseBestAngle|47_3(0.5f + t, ref A_1, ref CS$<>8__locals1);
				this.<SteepestDescent>g__ChooseBestAngle|47_3(0.5f - t, ref A_1, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.best;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008658 File Offset: 0x00006858
		[CompilerGenerated]
		private void <SteepestDescent>g__ChooseBestAngle|47_3(float referenceAngle, ref CinemachineFreeLook.<>c__DisplayClass47_0 A_2, ref CinemachineFreeLook.<>c__DisplayClass47_1 A_3)
		{
			float a = this.<SteepestDescent>g__AngleFunction|47_0(referenceAngle, ref A_2);
			if (a < A_3.bestAngle)
			{
				A_3.bestAngle = a;
				A_3.best = referenceAngle;
			}
		}

		// Token: 0x040000C1 RID: 193
		[Tooltip("Object for the camera children to look at (the aim target).")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_LookAt;

		// Token: 0x040000C2 RID: 194
		[Tooltip("Object for the camera children wants to move with (the body target).")]
		[NoSaveDuringPlay]
		[VcamTargetProperty]
		public Transform m_Follow;

		// Token: 0x040000C3 RID: 195
		[Tooltip("If enabled, this lens setting will apply to all three child rigs, otherwise the child rig lens settings will be used")]
		[FormerlySerializedAs("m_UseCommonLensSetting")]
		public bool m_CommonLens = true;

		// Token: 0x040000C4 RID: 196
		[FormerlySerializedAs("m_LensAttributes")]
		[Tooltip("Specifies the lens properties of this Virtual Camera.  This generally mirrors the Unity Camera's lens settings, and will be used to drive the Unity camera when the vcam is active")]
		public LensSettings m_Lens = LensSettings.Default;

		// Token: 0x040000C5 RID: 197
		public CinemachineVirtualCameraBase.TransitionParams m_Transitions;

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_BlendHint")]
		[FormerlySerializedAs("m_PositionBlending")]
		private CinemachineVirtualCameraBase.BlendHint m_LegacyBlendHint;

		// Token: 0x040000C7 RID: 199
		[Header("Axis Control")]
		[Tooltip("The Vertical axis.  Value is 0..1.  Chooses how to blend the child rigs")]
		[AxisStateProperty]
		public AxisState m_YAxis = new AxisState(0f, 1f, false, true, 2f, 0.2f, 0.1f, "Mouse Y", false);

		// Token: 0x040000C8 RID: 200
		[Tooltip("Controls how automatic recentering of the Y axis is accomplished")]
		public AxisState.Recentering m_YAxisRecentering = new AxisState.Recentering(false, 1f, 2f);

		// Token: 0x040000C9 RID: 201
		[Tooltip("The Horizontal axis.  Value is -180...180.  This is passed on to the rigs' OrbitalTransposer component")]
		[AxisStateProperty]
		public AxisState m_XAxis = new AxisState(-180f, 180f, true, false, 300f, 0.1f, 0.1f, "Mouse X", true);

		// Token: 0x040000CA RID: 202
		[OrbitalTransposerHeadingProperty]
		[Tooltip("The definition of Forward.  Camera will follow behind.")]
		public CinemachineOrbitalTransposer.Heading m_Heading = new CinemachineOrbitalTransposer.Heading(CinemachineOrbitalTransposer.Heading.HeadingDefinition.TargetForward, 4, 0f);

		// Token: 0x040000CB RID: 203
		[Tooltip("Controls how automatic recentering of the X axis is accomplished")]
		public AxisState.Recentering m_RecenterToTargetHeading = new AxisState.Recentering(false, 1f, 2f);

		// Token: 0x040000CC RID: 204
		[Header("Orbits")]
		[Tooltip("The coordinate space to use when interpreting the offset from the target.  This is also used to set the camera's Up vector, which will be maintained when aiming the camera.")]
		public CinemachineTransposer.BindingMode m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;

		// Token: 0x040000CD RID: 205
		[Tooltip("Controls how taut is the line that connects the rigs' orbits, which determines final placement on the Y axis")]
		[Range(0f, 1f)]
		[FormerlySerializedAs("m_SplineTension")]
		public float m_SplineCurvature = 0.2f;

		// Token: 0x040000CE RID: 206
		[Tooltip("The radius and height of the three orbiting rigs.")]
		public CinemachineFreeLook.Orbit[] m_Orbits = new CinemachineFreeLook.Orbit[]
		{
			new CinemachineFreeLook.Orbit(4.5f, 1.75f),
			new CinemachineFreeLook.Orbit(2.5f, 3f),
			new CinemachineFreeLook.Orbit(0.4f, 1.3f)
		};

		// Token: 0x040000CF RID: 207
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_HeadingBias")]
		private float m_LegacyHeadingBias = float.MaxValue;

		// Token: 0x040000D0 RID: 208
		private bool mUseLegacyRigDefinitions;

		// Token: 0x040000D1 RID: 209
		private bool mIsDestroyed;

		// Token: 0x040000D2 RID: 210
		private CameraState m_State = CameraState.Default;

		// Token: 0x040000D3 RID: 211
		[SerializeField]
		[HideInInspector]
		[NoSaveDuringPlay]
		private CinemachineVirtualCamera[] m_Rigs = new CinemachineVirtualCamera[3];

		// Token: 0x040000D4 RID: 212
		private CinemachineOrbitalTransposer[] mOrbitals;

		// Token: 0x040000D5 RID: 213
		private CinemachineBlend mBlendA;

		// Token: 0x040000D6 RID: 214
		private CinemachineBlend mBlendB;

		// Token: 0x040000D7 RID: 215
		public static CinemachineFreeLook.CreateRigDelegate CreateRigOverride;

		// Token: 0x040000D8 RID: 216
		public static CinemachineFreeLook.DestroyRigDelegate DestroyRigOverride;

		// Token: 0x040000D9 RID: 217
		private float m_CachedXAxisHeading;

		// Token: 0x040000DA RID: 218
		private float m_LastHeadingUpdateFrame;

		// Token: 0x040000DB RID: 219
		private CinemachineFreeLook.Orbit[] m_CachedOrbits;

		// Token: 0x040000DC RID: 220
		private float m_CachedTension;

		// Token: 0x040000DD RID: 221
		private Vector4[] m_CachedKnots;

		// Token: 0x040000DE RID: 222
		private Vector4[] m_CachedCtrl1;

		// Token: 0x040000DF RID: 223
		private Vector4[] m_CachedCtrl2;

		// Token: 0x02000029 RID: 41
		[Serializable]
		public struct Orbit
		{
			// Token: 0x060000FE RID: 254 RVA: 0x00008685 File Offset: 0x00006885
			public Orbit(float h, float r)
			{
				this.m_Height = h;
				this.m_Radius = r;
			}

			// Token: 0x040000E0 RID: 224
			public float m_Height;

			// Token: 0x040000E1 RID: 225
			public float m_Radius;
		}

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x06000100 RID: 256
		public delegate CinemachineVirtualCamera CreateRigDelegate(CinemachineFreeLook vcam, string name, CinemachineVirtualCamera copyFrom);

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x06000104 RID: 260
		public delegate void DestroyRigDelegate(GameObject rig);
	}
}
