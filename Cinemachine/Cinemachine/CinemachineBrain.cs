using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Cinemachine
{
	// Token: 0x02000010 RID: 16
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[AddComponentMenu("Cinemachine/CinemachineBrain")]
	[SaveDuringPlay]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineBrainProperties.html")]
	public class CinemachineBrain : MonoBehaviour, ICameraOverrideStack
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000309D File Offset: 0x0000129D
		public Camera OutputCamera
		{
			get
			{
				if (this.m_OutputCamera == null && !Application.isPlaying)
				{
					this.ControlledObject.TryGetComponent<Camera>(out this.m_OutputCamera);
				}
				return this.m_OutputCamera;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000030CC File Offset: 0x000012CC
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000030E9 File Offset: 0x000012E9
		public GameObject ControlledObject
		{
			get
			{
				if (!(this.m_TargetOverride == null))
				{
					return this.m_TargetOverride;
				}
				return base.gameObject;
			}
			set
			{
				if (this.m_TargetOverride != value)
				{
					this.m_TargetOverride = value;
					this.ControlledObject.TryGetComponent<Camera>(out this.m_OutputCamera);
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000310D File Offset: 0x0000130D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003114 File Offset: 0x00001314
		public static ICinemachineCamera SoloCamera
		{
			get
			{
				return CinemachineBrain.mSoloCamera;
			}
			set
			{
				if (value != null && !CinemachineCore.Instance.IsLive(value))
				{
					value.OnTransitionFromCamera(null, Vector3.up, CinemachineCore.DeltaTime);
				}
				CinemachineBrain.mSoloCamera = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000313D File Offset: 0x0000133D
		public static Color GetSoloGUIColor()
		{
			return Color.Lerp(Color.red, Color.yellow, 0.8f);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003153 File Offset: 0x00001353
		public Vector3 DefaultWorldUp
		{
			get
			{
				if (!(this.m_WorldUpOverride != null))
				{
					return Vector3.up;
				}
				return this.m_WorldUpOverride.transform.up;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000317C File Offset: 0x0000137C
		private void OnEnable()
		{
			if (this.mFrameStack.Count == 0)
			{
				this.mFrameStack.Add(new CinemachineBrain.BrainFrame());
			}
			CinemachineCore.Instance.AddActiveBrain(this);
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Remove(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Combine(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
			this.mPhysicsCoroutine = base.StartCoroutine(this.AfterPhysics());
			SceneManager.sceneLoaded += this.OnSceneLoaded;
			SceneManager.sceneUnloaded += this.OnSceneUnloaded;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003228 File Offset: 0x00001428
		private void OnDisable()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
			SceneManager.sceneUnloaded -= this.OnSceneUnloaded;
			CinemachineDebug.OnGUIHandlers = (CinemachineDebug.OnGUIDelegate)Delegate.Remove(CinemachineDebug.OnGUIHandlers, new CinemachineDebug.OnGUIDelegate(this.OnGuiHandler));
			CinemachineCore.Instance.RemoveActiveBrain(this);
			this.mFrameStack.Clear();
			base.StopCoroutine(this.mPhysicsCoroutine);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003299 File Offset: 0x00001499
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (Time.frameCount == this.m_LastFrameUpdated && this.mFrameStack.Count > 0)
			{
				this.ManualUpdate();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003299 File Offset: 0x00001499
		private void OnSceneUnloaded(Scene scene)
		{
			if (Time.frameCount == this.m_LastFrameUpdated && this.mFrameStack.Count > 0)
			{
				this.ManualUpdate();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032BC File Offset: 0x000014BC
		private void Awake()
		{
			this.ControlledObject.TryGetComponent<Camera>(out this.m_OutputCamera);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000032D0 File Offset: 0x000014D0
		private void Start()
		{
			this.m_LastFrameUpdated = -1;
			this.UpdateVirtualCameras(CinemachineCore.UpdateFilter.Late, -1f);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000032E8 File Offset: 0x000014E8
		private void OnGuiHandler()
		{
			if (!this.m_ShowDebugText)
			{
				CinemachineDebug.ReleaseScreenPos(this);
				return;
			}
			StringBuilder sb = CinemachineDebug.SBFromPool();
			Color color = GUI.color;
			sb.Length = 0;
			sb.Append("CM ");
			sb.Append(base.gameObject.name);
			sb.Append(": ");
			if (CinemachineBrain.SoloCamera != null)
			{
				sb.Append("SOLO ");
				GUI.color = CinemachineBrain.GetSoloGUIColor();
			}
			if (this.IsBlending)
			{
				sb.Append(this.ActiveBlend.Description);
			}
			else
			{
				ICinemachineCamera vcam = this.ActiveVirtualCamera;
				if (vcam == null)
				{
					sb.Append("(none)");
				}
				else
				{
					sb.Append("[");
					sb.Append(vcam.Name);
					sb.Append("]");
				}
			}
			string text = sb.ToString();
			GUI.Label(CinemachineDebug.GetScreenPos(this, text, GUI.skin.box), text, GUI.skin.box);
			GUI.color = color;
			CinemachineDebug.ReturnToPool(sb);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033EB File Offset: 0x000015EB
		private IEnumerator AfterPhysics()
		{
			for (;;)
			{
				yield return this.mWaitForFixedUpdate;
				if (this.m_UpdateMethod == CinemachineBrain.UpdateMethod.FixedUpdate || this.m_UpdateMethod == CinemachineBrain.UpdateMethod.SmartUpdate)
				{
					CinemachineCore.UpdateFilter filter = CinemachineCore.UpdateFilter.Fixed;
					if (this.m_UpdateMethod == CinemachineBrain.UpdateMethod.SmartUpdate)
					{
						UpdateTracker.OnUpdate(UpdateTracker.UpdateClock.Fixed);
						filter = CinemachineCore.UpdateFilter.Smart;
					}
					this.UpdateVirtualCameras(filter, this.GetEffectiveDeltaTime(true));
				}
				if (this.m_BlendUpdateMethod == CinemachineBrain.BrainUpdateMethod.FixedUpdate)
				{
					this.UpdateFrame0(Time.fixedDeltaTime);
					this.ProcessActiveCamera(Time.fixedDeltaTime);
				}
			}
			yield break;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033FA File Offset: 0x000015FA
		private void LateUpdate()
		{
			if (this.m_UpdateMethod != CinemachineBrain.UpdateMethod.ManualUpdate)
			{
				this.ManualUpdate();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000340C File Offset: 0x0000160C
		public void ManualUpdate()
		{
			this.m_LastFrameUpdated = Time.frameCount;
			float deltaTime = this.GetEffectiveDeltaTime(false);
			if (!Application.isPlaying || this.m_BlendUpdateMethod != CinemachineBrain.BrainUpdateMethod.FixedUpdate)
			{
				this.UpdateFrame0(deltaTime);
			}
			this.ComputeCurrentBlend(ref this.mCurrentLiveCameras, 0);
			if (Application.isPlaying && this.m_UpdateMethod == CinemachineBrain.UpdateMethod.FixedUpdate)
			{
				if (this.m_BlendUpdateMethod != CinemachineBrain.BrainUpdateMethod.FixedUpdate)
				{
					CinemachineCore.Instance.m_CurrentUpdateFilter = CinemachineCore.UpdateFilter.Fixed;
					if (CinemachineBrain.SoloCamera == null)
					{
						this.mCurrentLiveCameras.UpdateCameraState(this.DefaultWorldUp, this.GetEffectiveDeltaTime(true));
					}
				}
			}
			else
			{
				CinemachineCore.UpdateFilter filter = CinemachineCore.UpdateFilter.Late;
				if (this.m_UpdateMethod == CinemachineBrain.UpdateMethod.SmartUpdate)
				{
					UpdateTracker.OnUpdate(UpdateTracker.UpdateClock.Late);
					filter = CinemachineCore.UpdateFilter.SmartLate;
				}
				this.UpdateVirtualCameras(filter, deltaTime);
			}
			if (!Application.isPlaying || this.m_BlendUpdateMethod != CinemachineBrain.BrainUpdateMethod.FixedUpdate)
			{
				this.ProcessActiveCamera(deltaTime);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000034C4 File Offset: 0x000016C4
		private float GetEffectiveDeltaTime(bool fixedDelta)
		{
			if (CinemachineCore.UniformDeltaTimeOverride >= 0f)
			{
				return CinemachineCore.UniformDeltaTimeOverride;
			}
			if (CinemachineBrain.SoloCamera != null)
			{
				return Time.unscaledDeltaTime;
			}
			if (!Application.isPlaying)
			{
				for (int i = this.mFrameStack.Count - 1; i > 0; i--)
				{
					CinemachineBrain.BrainFrame frame = this.mFrameStack[i];
					if (frame.Active)
					{
						return frame.deltaTimeOverride;
					}
				}
				return -1f;
			}
			if (this.m_IgnoreTimeScale)
			{
				if (!fixedDelta)
				{
					return Time.unscaledDeltaTime;
				}
				return Time.fixedDeltaTime;
			}
			else
			{
				if (!fixedDelta)
				{
					return Time.deltaTime;
				}
				return Time.fixedDeltaTime;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003558 File Offset: 0x00001758
		private void UpdateVirtualCameras(CinemachineCore.UpdateFilter updateFilter, float deltaTime)
		{
			CinemachineCore.Instance.m_CurrentUpdateFilter = updateFilter;
			Camera camera = this.OutputCamera;
			CinemachineCore.Instance.UpdateAllActiveVirtualCameras((camera == null) ? (-1) : camera.cullingMask, this.DefaultWorldUp, deltaTime);
			if (CinemachineBrain.SoloCamera != null)
			{
				CinemachineBrain.SoloCamera.UpdateCameraState(this.DefaultWorldUp, deltaTime);
			}
			this.mCurrentLiveCameras.UpdateCameraState(this.DefaultWorldUp, deltaTime);
			updateFilter = CinemachineCore.UpdateFilter.Late;
			if (Application.isPlaying)
			{
				if (this.m_UpdateMethod == CinemachineBrain.UpdateMethod.SmartUpdate)
				{
					updateFilter |= CinemachineCore.UpdateFilter.Smart;
				}
				else if (this.m_UpdateMethod == CinemachineBrain.UpdateMethod.FixedUpdate)
				{
					updateFilter = CinemachineCore.UpdateFilter.Fixed;
				}
			}
			CinemachineCore.Instance.m_CurrentUpdateFilter = updateFilter;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000035F4 File Offset: 0x000017F4
		public ICinemachineCamera ActiveVirtualCamera
		{
			get
			{
				if (CinemachineBrain.SoloCamera != null)
				{
					return CinemachineBrain.SoloCamera;
				}
				return CinemachineBrain.DeepCamBFromBlend(this.mCurrentLiveCameras);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003610 File Offset: 0x00001810
		private static ICinemachineCamera DeepCamBFromBlend(CinemachineBlend blend)
		{
			ICinemachineCamera vcam;
			BlendSourceVirtualCamera bs;
			for (vcam = blend.CamB; vcam != null; vcam = bs.Blend.CamB)
			{
				if (!vcam.IsValid)
				{
					return null;
				}
				bs = vcam as BlendSourceVirtualCamera;
				if (bs == null)
				{
					break;
				}
			}
			return vcam;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000364C File Offset: 0x0000184C
		public bool IsLiveInBlend(ICinemachineCamera vcam)
		{
			if (vcam == this.mCurrentLiveCameras.CamA)
			{
				return true;
			}
			BlendSourceVirtualCamera b = this.mCurrentLiveCameras.CamA as BlendSourceVirtualCamera;
			if (b != null && b.Blend.Uses(vcam))
			{
				return true;
			}
			ICinemachineCamera parent = vcam.ParentCamera;
			return parent != null && parent.IsLiveChild(vcam, false) && this.IsLiveInBlend(parent);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000036AA File Offset: 0x000018AA
		public bool IsBlending
		{
			get
			{
				return this.ActiveBlend != null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000036B5 File Offset: 0x000018B5
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000036F0 File Offset: 0x000018F0
		public CinemachineBlend ActiveBlend
		{
			get
			{
				if (CinemachineBrain.SoloCamera != null)
				{
					return null;
				}
				if (this.mCurrentLiveCameras.CamA == null || this.mCurrentLiveCameras.Equals(null) || this.mCurrentLiveCameras.IsComplete)
				{
					return null;
				}
				return this.mCurrentLiveCameras;
			}
			set
			{
				if (value == null)
				{
					this.mFrameStack[0].blend.Duration = 0f;
					return;
				}
				this.mFrameStack[0].blend = value;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003724 File Offset: 0x00001924
		private int GetBrainFrame(int withId)
		{
			for (int i = this.mFrameStack.Count - 1; i > 0; i--)
			{
				if (this.mFrameStack[i].id == withId)
				{
					return i;
				}
			}
			this.mFrameStack.Add(new CinemachineBrain.BrainFrame
			{
				id = withId
			});
			return this.mFrameStack.Count - 1;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003784 File Offset: 0x00001984
		public int SetCameraOverride(int overrideId, ICinemachineCamera camA, ICinemachineCamera camB, float weightB, float deltaTime)
		{
			if (overrideId < 0)
			{
				int num = this.mNextFrameId;
				this.mNextFrameId = num + 1;
				overrideId = num;
			}
			CinemachineBrain.BrainFrame brainFrame = this.mFrameStack[this.GetBrainFrame(overrideId)];
			brainFrame.deltaTimeOverride = deltaTime;
			brainFrame.blend.CamA = camA;
			brainFrame.blend.CamB = camB;
			brainFrame.blend.BlendCurve = CinemachineBrain.mDefaultLinearAnimationCurve;
			brainFrame.blend.Duration = 1f;
			brainFrame.blend.TimeInBlend = weightB;
			CinemachineVirtualCameraBase cam = camA as CinemachineVirtualCameraBase;
			if (cam != null)
			{
				cam.EnsureStarted();
			}
			cam = camB as CinemachineVirtualCameraBase;
			if (cam != null)
			{
				cam.EnsureStarted();
			}
			return overrideId;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003834 File Offset: 0x00001A34
		public void ReleaseCameraOverride(int overrideId)
		{
			for (int i = this.mFrameStack.Count - 1; i > 0; i--)
			{
				if (this.mFrameStack[i].id == overrideId)
				{
					this.mFrameStack.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000387C File Offset: 0x00001A7C
		private void ProcessActiveCamera(float deltaTime)
		{
			ICinemachineCamera activeCamera = this.ActiveVirtualCamera;
			if (CinemachineBrain.SoloCamera != null)
			{
				CameraState state = CinemachineBrain.SoloCamera.State;
				this.PushStateToUnityCamera(ref state);
			}
			else if (activeCamera == null)
			{
				CameraState state2 = CameraState.Default;
				Transform target = this.ControlledObject.transform;
				state2.RawPosition = target.position;
				state2.RawOrientation = target.rotation;
				state2.Lens = LensSettings.FromCamera(this.m_OutputCamera);
				state2.BlendHint |= (CameraState.BlendHintValue)67;
				this.PushStateToUnityCamera(ref state2);
			}
			else
			{
				if (this.mActiveCameraPreviousFrameGameObject == null)
				{
					this.mActiveCameraPreviousFrame = null;
				}
				if (activeCamera != this.mActiveCameraPreviousFrame)
				{
					activeCamera.OnTransitionFromCamera(this.mActiveCameraPreviousFrame, this.DefaultWorldUp, deltaTime);
					if (this.m_CameraActivatedEvent != null)
					{
						this.m_CameraActivatedEvent.Invoke(activeCamera, this.mActiveCameraPreviousFrame);
					}
					if (!this.IsBlending || (this.mActiveCameraPreviousFrame != null && !this.ActiveBlend.Uses(this.mActiveCameraPreviousFrame)))
					{
						if (this.m_CameraCutEvent != null)
						{
							this.m_CameraCutEvent.Invoke(this);
						}
						if (CinemachineCore.CameraCutEvent != null)
						{
							CinemachineCore.CameraCutEvent.Invoke(this);
						}
					}
					activeCamera.UpdateCameraState(this.DefaultWorldUp, deltaTime);
				}
				CameraState state3 = this.mCurrentLiveCameras.State;
				this.PushStateToUnityCamera(ref state3);
			}
			this.mActiveCameraPreviousFrame = activeCamera;
			this.mActiveCameraPreviousFrameGameObject = ((activeCamera == null) ? null : activeCamera.VirtualCameraGameObject);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000039DC File Offset: 0x00001BDC
		private void UpdateFrame0(float deltaTime)
		{
			if (this.mFrameStack.Count == 0)
			{
				this.mFrameStack.Add(new CinemachineBrain.BrainFrame());
			}
			CinemachineBrain.BrainFrame frame = this.mFrameStack[0];
			ICinemachineCamera activeCamera = this.TopCameraFromPriorityQueue();
			ICinemachineCamera outGoingCamera = frame.blend.CamB;
			if (activeCamera != outGoingCamera)
			{
				if ((global::UnityEngine.Object)activeCamera != null && (global::UnityEngine.Object)outGoingCamera != null && deltaTime >= 0f)
				{
					CinemachineBlendDefinition blendDef = this.LookupBlend(outGoingCamera, activeCamera);
					float blendDuration = blendDef.BlendTime;
					float blendStartPosition = 0f;
					if (blendDef.BlendCurve != null && blendDuration > 0.0001f)
					{
						if (frame.blend.IsComplete)
						{
							frame.blend.CamA = outGoingCamera;
						}
						else
						{
							if (frame.blend.CamA != activeCamera)
							{
								BlendSourceVirtualCamera blendSourceVirtualCamera = frame.blend.CamA as BlendSourceVirtualCamera;
								if (((blendSourceVirtualCamera != null) ? blendSourceVirtualCamera.Blend.CamB : null) != activeCamera)
								{
									goto IL_013E;
								}
							}
							if (frame.blend.CamB == outGoingCamera)
							{
								float progress = frame.blendStartPosition + (1f - frame.blendStartPosition) * frame.blend.TimeInBlend / frame.blend.Duration;
								blendDuration *= progress;
								blendStartPosition = 1f - progress;
							}
							IL_013E:
							frame.blend.CamA = new BlendSourceVirtualCamera(new CinemachineBlend(frame.blend.CamA, frame.blend.CamB, frame.blend.BlendCurve, frame.blend.Duration, frame.blend.TimeInBlend));
						}
					}
					frame.blend.BlendCurve = blendDef.BlendCurve;
					frame.blend.Duration = blendDuration;
					frame.blend.TimeInBlend = 0f;
					frame.blendStartPosition = blendStartPosition;
				}
				frame.blend.CamB = activeCamera;
			}
			if (frame.blend.CamA != null)
			{
				frame.blend.TimeInBlend += ((deltaTime >= 0f) ? deltaTime : frame.blend.Duration);
				if (frame.blend.IsComplete)
				{
					frame.blend.CamA = null;
					frame.blend.BlendCurve = null;
					frame.blend.Duration = 0f;
					frame.blend.TimeInBlend = 0f;
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003C30 File Offset: 0x00001E30
		public void ComputeCurrentBlend(ref CinemachineBlend outputBlend, int numTopLayersToExclude)
		{
			if (this.mFrameStack.Count == 0)
			{
				this.mFrameStack.Add(new CinemachineBrain.BrainFrame());
			}
			int lastActive = 0;
			int topLayer = Mathf.Max(1, this.mFrameStack.Count - numTopLayersToExclude);
			for (int i = 0; i < topLayer; i++)
			{
				CinemachineBrain.BrainFrame frame = this.mFrameStack[i];
				if (i == 0 || frame.Active)
				{
					frame.workingBlend.CamA = frame.blend.CamA;
					frame.workingBlend.CamB = frame.blend.CamB;
					frame.workingBlend.BlendCurve = frame.blend.BlendCurve;
					frame.workingBlend.Duration = frame.blend.Duration;
					frame.workingBlend.TimeInBlend = frame.blend.TimeInBlend;
					if (i > 0 && !frame.blend.IsComplete)
					{
						if (frame.workingBlend.CamA == null)
						{
							if (this.mFrameStack[lastActive].blend.IsComplete)
							{
								frame.workingBlend.CamA = this.mFrameStack[lastActive].blend.CamB;
							}
							else
							{
								frame.workingBlendSource.Blend = this.mFrameStack[lastActive].workingBlend;
								frame.workingBlend.CamA = frame.workingBlendSource;
							}
						}
						else if (frame.workingBlend.CamB == null)
						{
							if (this.mFrameStack[lastActive].blend.IsComplete)
							{
								frame.workingBlend.CamB = this.mFrameStack[lastActive].blend.CamB;
							}
							else
							{
								frame.workingBlendSource.Blend = this.mFrameStack[lastActive].workingBlend;
								frame.workingBlend.CamB = frame.workingBlendSource;
							}
						}
					}
					lastActive = i;
				}
			}
			CinemachineBlend workingBlend = this.mFrameStack[lastActive].workingBlend;
			outputBlend.CamA = workingBlend.CamA;
			outputBlend.CamB = workingBlend.CamB;
			outputBlend.BlendCurve = workingBlend.BlendCurve;
			outputBlend.Duration = workingBlend.Duration;
			outputBlend.TimeInBlend = workingBlend.TimeInBlend;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003E80 File Offset: 0x00002080
		public bool IsLive(ICinemachineCamera vcam, bool dominantChildOnly = false)
		{
			if (CinemachineBrain.SoloCamera == vcam)
			{
				return true;
			}
			if (this.mCurrentLiveCameras.Uses(vcam))
			{
				return true;
			}
			ICinemachineCamera parent = vcam.ParentCamera;
			while (parent != null && parent.IsLiveChild(vcam, dominantChildOnly))
			{
				if (CinemachineBrain.SoloCamera == parent || this.mCurrentLiveCameras.Uses(parent))
				{
					return true;
				}
				vcam = parent;
				parent = vcam.ParentCamera;
			}
			return false;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003EE0 File Offset: 0x000020E0
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003EE8 File Offset: 0x000020E8
		public CameraState CurrentCameraState { get; private set; }

		// Token: 0x06000065 RID: 101 RVA: 0x00003EF4 File Offset: 0x000020F4
		protected virtual ICinemachineCamera TopCameraFromPriorityQueue()
		{
			CinemachineCore core = CinemachineCore.Instance;
			Camera outputCamera = this.OutputCamera;
			int mask = ((outputCamera == null) ? (-1) : outputCamera.cullingMask);
			int numCameras = core.VirtualCameraCount;
			for (int i = 0; i < numCameras; i++)
			{
				CinemachineVirtualCameraBase cam = core.GetVirtualCamera(i);
				GameObject go = ((cam != null) ? cam.gameObject : null);
				if (go != null && (mask & (1 << go.layer)) != 0)
				{
					return cam;
				}
			}
			return null;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003F78 File Offset: 0x00002178
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

		// Token: 0x06000067 RID: 103 RVA: 0x00003FE4 File Offset: 0x000021E4
		private void PushStateToUnityCamera(ref CameraState state)
		{
			this.CurrentCameraState = state;
			Transform transform = this.ControlledObject.transform;
			Vector3 pos = transform.position;
			Quaternion rot = transform.rotation;
			if ((state.BlendHint & CameraState.BlendHintValue.NoPosition) == CameraState.BlendHintValue.Nothing)
			{
				pos = state.FinalPosition;
			}
			if ((state.BlendHint & CameraState.BlendHintValue.NoOrientation) == CameraState.BlendHintValue.Nothing)
			{
				rot = state.FinalOrientation;
			}
			transform.ConservativeSetPositionAndRotation(pos, rot);
			if ((state.BlendHint & CameraState.BlendHintValue.NoLens) == CameraState.BlendHintValue.Nothing)
			{
				Camera cam = this.OutputCamera;
				if (cam != null)
				{
					cam.nearClipPlane = state.Lens.NearClipPlane;
					cam.farClipPlane = state.Lens.FarClipPlane;
					cam.orthographicSize = state.Lens.OrthographicSize;
					cam.fieldOfView = state.Lens.FieldOfView;
					cam.lensShift = state.Lens.LensShift;
					if (state.Lens.ModeOverride != LensSettings.OverrideModes.None)
					{
						cam.orthographic = state.Lens.Orthographic;
					}
					bool isPhysical = ((state.Lens.ModeOverride == LensSettings.OverrideModes.None) ? cam.usePhysicalProperties : state.Lens.IsPhysicalCamera);
					cam.usePhysicalProperties = isPhysical;
					if (isPhysical && state.Lens.IsPhysicalCamera)
					{
						cam.sensorSize = state.Lens.SensorSize;
						cam.gateFit = state.Lens.GateFit;
						cam.focalLength = Camera.FieldOfViewToFocalLength(state.Lens.FieldOfView, state.Lens.SensorSize.y);
						cam.focusDistance = state.Lens.FocusDistance;
					}
				}
			}
			if (CinemachineCore.CameraUpdatedEvent != null)
			{
				CinemachineCore.CameraUpdatedEvent.Invoke(this);
			}
		}

		// Token: 0x0400002F RID: 47
		[Tooltip("When enabled, the current camera and blend will be indicated in the game window, for debugging")]
		public bool m_ShowDebugText;

		// Token: 0x04000030 RID: 48
		[Tooltip("When enabled, the camera's frustum will be shown at all times in the scene view")]
		public bool m_ShowCameraFrustum = true;

		// Token: 0x04000031 RID: 49
		[Tooltip("When enabled, the cameras will always respond in real-time to user input and damping, even if the game is running in slow motion")]
		public bool m_IgnoreTimeScale;

		// Token: 0x04000032 RID: 50
		[Tooltip("If set, this object's Y axis will define the worldspace Up vector for all the virtual cameras.  This is useful for instance in top-down game environments.  If not set, Up is worldspace Y.  Setting this appropriately is important, because Virtual Cameras don't like looking straight up or straight down.")]
		public Transform m_WorldUpOverride;

		// Token: 0x04000033 RID: 51
		[Tooltip("The update time for the vcams.  Use FixedUpdate if all your targets are animated during FixedUpdate (e.g. RigidBodies), LateUpdate if all your targets are animated during the normal Update loop, and SmartUpdate if you want Cinemachine to do the appropriate thing on a per-target basis.  SmartUpdate is the recommended setting")]
		public CinemachineBrain.UpdateMethod m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;

		// Token: 0x04000034 RID: 52
		[Tooltip("The update time for the Brain, i.e. when the blends are evaluated and the brain's transform is updated")]
		public CinemachineBrain.BrainUpdateMethod m_BlendUpdateMethod = CinemachineBrain.BrainUpdateMethod.LateUpdate;

		// Token: 0x04000035 RID: 53
		[CinemachineBlendDefinitionProperty]
		[Tooltip("The blend that is used in cases where you haven't explicitly defined a blend between two Virtual Cameras")]
		public CinemachineBlendDefinition m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);

		// Token: 0x04000036 RID: 54
		[Tooltip("This is the asset that contains custom settings for blends between specific virtual cameras in your scene")]
		public CinemachineBlenderSettings m_CustomBlends;

		// Token: 0x04000037 RID: 55
		private Camera m_OutputCamera;

		// Token: 0x04000038 RID: 56
		private GameObject m_TargetOverride;

		// Token: 0x04000039 RID: 57
		[Tooltip("This event will fire whenever a virtual camera goes live and there is no blend")]
		public CinemachineBrain.BrainEvent m_CameraCutEvent = new CinemachineBrain.BrainEvent();

		// Token: 0x0400003A RID: 58
		[Tooltip("This event will fire whenever a virtual camera goes live.  If a blend is involved, then the event will fire on the first frame of the blend.")]
		public CinemachineBrain.VcamActivatedEvent m_CameraActivatedEvent = new CinemachineBrain.VcamActivatedEvent();

		// Token: 0x0400003B RID: 59
		private static ICinemachineCamera mSoloCamera;

		// Token: 0x0400003C RID: 60
		private Coroutine mPhysicsCoroutine;

		// Token: 0x0400003D RID: 61
		private int m_LastFrameUpdated;

		// Token: 0x0400003E RID: 62
		private WaitForFixedUpdate mWaitForFixedUpdate = new WaitForFixedUpdate();

		// Token: 0x0400003F RID: 63
		private List<CinemachineBrain.BrainFrame> mFrameStack = new List<CinemachineBrain.BrainFrame>();

		// Token: 0x04000040 RID: 64
		private int mNextFrameId = 1;

		// Token: 0x04000041 RID: 65
		private CinemachineBlend mCurrentLiveCameras = new CinemachineBlend(null, null, null, 0f, 0f);

		// Token: 0x04000042 RID: 66
		private static readonly AnimationCurve mDefaultLinearAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x04000043 RID: 67
		private ICinemachineCamera mActiveCameraPreviousFrame;

		// Token: 0x04000044 RID: 68
		private GameObject mActiveCameraPreviousFrameGameObject;

		// Token: 0x02000011 RID: 17
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum UpdateMethod
		{
			// Token: 0x04000047 RID: 71
			FixedUpdate,
			// Token: 0x04000048 RID: 72
			LateUpdate,
			// Token: 0x04000049 RID: 73
			SmartUpdate,
			// Token: 0x0400004A RID: 74
			ManualUpdate
		}

		// Token: 0x02000012 RID: 18
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum BrainUpdateMethod
		{
			// Token: 0x0400004C RID: 76
			FixedUpdate,
			// Token: 0x0400004D RID: 77
			LateUpdate
		}

		// Token: 0x02000013 RID: 19
		[Serializable]
		public class BrainEvent : UnityEvent<CinemachineBrain>
		{
		}

		// Token: 0x02000014 RID: 20
		[Serializable]
		public class VcamActivatedEvent : UnityEvent<ICinemachineCamera, ICinemachineCamera>
		{
		}

		// Token: 0x02000015 RID: 21
		private class BrainFrame
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600006C RID: 108 RVA: 0x0000422C File Offset: 0x0000242C
			public bool Active
			{
				get
				{
					return this.blend.IsValid;
				}
			}

			// Token: 0x0400004E RID: 78
			public int id;

			// Token: 0x0400004F RID: 79
			public CinemachineBlend blend = new CinemachineBlend(null, null, null, 0f, 0f);

			// Token: 0x04000050 RID: 80
			public CinemachineBlend workingBlend = new CinemachineBlend(null, null, null, 0f, 0f);

			// Token: 0x04000051 RID: 81
			public BlendSourceVirtualCamera workingBlendSource = new BlendSourceVirtualCamera(null);

			// Token: 0x04000052 RID: 82
			public float deltaTimeOverride;

			// Token: 0x04000053 RID: 83
			public float blendStartPosition;
		}
	}
}
