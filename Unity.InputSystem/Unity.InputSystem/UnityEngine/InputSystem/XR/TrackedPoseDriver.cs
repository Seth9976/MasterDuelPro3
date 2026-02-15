using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E5 RID: 229
	[AddComponentMenu("XR/Tracked Pose Driver (Input System)")]
	[Serializable]
	public class TrackedPoseDriver : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003DF6B File Offset: 0x0003C16B
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x0003DF73 File Offset: 0x0003C173
		public TrackedPoseDriver.TrackingType trackingType
		{
			get
			{
				return this.m_TrackingType;
			}
			set
			{
				this.m_TrackingType = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0003DF7C File Offset: 0x0003C17C
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0003DF84 File Offset: 0x0003C184
		public TrackedPoseDriver.UpdateType updateType
		{
			get
			{
				return this.m_UpdateType;
			}
			set
			{
				this.m_UpdateType = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0003DF8D File Offset: 0x0003C18D
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x0003DF95 File Offset: 0x0003C195
		public bool ignoreTrackingState
		{
			get
			{
				return this.m_IgnoreTrackingState;
			}
			set
			{
				this.m_IgnoreTrackingState = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0003DF9E File Offset: 0x0003C19E
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0003DFA6 File Offset: 0x0003C1A6
		public InputActionProperty positionInput
		{
			get
			{
				return this.m_PositionInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindPosition();
				}
				this.m_PositionInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindPosition();
				}
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0003DFD1 File Offset: 0x0003C1D1
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0003DFD9 File Offset: 0x0003C1D9
		public InputActionProperty rotationInput
		{
			get
			{
				return this.m_RotationInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindRotation();
				}
				this.m_RotationInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindRotation();
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003E004 File Offset: 0x0003C204
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0003E00C File Offset: 0x0003C20C
		public InputActionProperty trackingStateInput
		{
			get
			{
				return this.m_TrackingStateInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindTrackingState();
				}
				this.m_TrackingStateInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindTrackingState();
				}
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0003E037 File Offset: 0x0003C237
		private void BindActions()
		{
			this.BindPosition();
			this.BindRotation();
			this.BindTrackingState();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0003E04B File Offset: 0x0003C24B
		private void UnbindActions()
		{
			this.UnbindPosition();
			this.UnbindRotation();
			this.UnbindTrackingState();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0003E060 File Offset: 0x0003C260
		private void BindPosition()
		{
			if (this.m_PositionBound)
			{
				return;
			}
			InputAction action = this.m_PositionInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnPositionPerformed;
			action.canceled += this.OnPositionCanceled;
			this.m_PositionBound = true;
			if (this.m_PositionInput.reference == null)
			{
				this.RenameAndEnable(action, base.gameObject.name + " - TPD - Position");
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0003E0E0 File Offset: 0x0003C2E0
		private void BindRotation()
		{
			if (this.m_RotationBound)
			{
				return;
			}
			InputAction action = this.m_RotationInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnRotationPerformed;
			action.canceled += this.OnRotationCanceled;
			this.m_RotationBound = true;
			if (this.m_RotationInput.reference == null)
			{
				this.RenameAndEnable(action, base.gameObject.name + " - TPD - Rotation");
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0003E160 File Offset: 0x0003C360
		private void BindTrackingState()
		{
			if (this.m_TrackingStateBound)
			{
				return;
			}
			InputAction action = this.m_TrackingStateInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnTrackingStatePerformed;
			action.canceled += this.OnTrackingStateCanceled;
			this.m_TrackingStateBound = true;
			if (this.m_TrackingStateInput.reference == null)
			{
				this.RenameAndEnable(action, base.gameObject.name + " - TPD - Tracking State");
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0003E1E0 File Offset: 0x0003C3E0
		private void RenameAndEnable(InputAction action, string name)
		{
			action.Rename(name);
			action.Enable();
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0003E1F0 File Offset: 0x0003C3F0
		private void UnbindPosition()
		{
			if (!this.m_PositionBound)
			{
				return;
			}
			InputAction action = this.m_PositionInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_PositionInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnPositionPerformed;
			action.canceled -= this.OnPositionCanceled;
			this.m_PositionBound = false;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0003E25C File Offset: 0x0003C45C
		private void UnbindRotation()
		{
			if (!this.m_RotationBound)
			{
				return;
			}
			InputAction action = this.m_RotationInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_RotationInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnRotationPerformed;
			action.canceled -= this.OnRotationCanceled;
			this.m_RotationBound = false;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		private void UnbindTrackingState()
		{
			if (!this.m_TrackingStateBound)
			{
				return;
			}
			InputAction action = this.m_TrackingStateInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_TrackingStateInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnTrackingStatePerformed;
			action.canceled -= this.OnTrackingStateCanceled;
			this.m_TrackingStateBound = false;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0003E332 File Offset: 0x0003C532
		private void OnPositionPerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentPosition = context.ReadValue<Vector3>();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0003E341 File Offset: 0x0003C541
		private void OnPositionCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentPosition = Vector3.zero;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0003E34E File Offset: 0x0003C54E
		private void OnRotationPerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentRotation = context.ReadValue<Quaternion>();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0003E35D File Offset: 0x0003C55D
		private void OnRotationCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentRotation = Quaternion.identity;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003E36A File Offset: 0x0003C56A
		private void OnTrackingStatePerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentTrackingState = (TrackedPoseDriver.TrackingStates)context.ReadValue<int>();
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003E379 File Offset: 0x0003C579
		private void OnTrackingStateCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0003E384 File Offset: 0x0003C584
		protected void Reset()
		{
			this.m_PositionInput = new InputActionProperty(new InputAction("Position", InputActionType.Value, null, null, null, "Vector3"));
			this.m_RotationInput = new InputActionProperty(new InputAction("Rotation", InputActionType.Value, null, null, null, "Quaternion"));
			this.m_TrackingStateInput = new InputActionProperty(new InputAction("Tracking State", InputActionType.Value, null, null, null, "Integer"));
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0003E3EC File Offset: 0x0003C5EC
		protected virtual void Awake()
		{
			Camera cameraComponent;
			if (this.HasStereoCamera(out cameraComponent))
			{
				XRDevice.DisableAutoXRCameraTracking(cameraComponent, true);
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003E40A File Offset: 0x0003C60A
		protected void OnEnable()
		{
			InputSystem.onAfterUpdate += this.UpdateCallback;
			this.BindActions();
			this.m_IsFirstUpdate = true;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0003E42A File Offset: 0x0003C62A
		protected void OnDisable()
		{
			this.UnbindActions();
			InputSystem.onAfterUpdate -= this.UpdateCallback;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003E444 File Offset: 0x0003C644
		protected virtual void OnDestroy()
		{
			Camera cameraComponent;
			if (this.HasStereoCamera(out cameraComponent))
			{
				XRDevice.DisableAutoXRCameraTracking(cameraComponent, false);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003E464 File Offset: 0x0003C664
		protected void UpdateCallback()
		{
			if (this.m_IsFirstUpdate)
			{
				if (this.m_PositionInput.action != null)
				{
					this.m_CurrentPosition = this.m_PositionInput.action.ReadValue<Vector3>();
				}
				if (this.m_RotationInput.action != null)
				{
					this.m_CurrentRotation = this.m_RotationInput.action.ReadValue<Quaternion>();
				}
				this.ReadTrackingState();
				this.m_IsFirstUpdate = false;
			}
			if (InputState.currentUpdateType == InputUpdateType.BeforeRender)
			{
				this.OnBeforeRender();
				return;
			}
			this.OnUpdate();
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0003E4E4 File Offset: 0x0003C6E4
		private unsafe void ReadTrackingState()
		{
			InputAction trackingStateAction = this.m_TrackingStateInput.action;
			if (trackingStateAction != null && !trackingStateAction.enabled)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
				return;
			}
			if (trackingStateAction == null || trackingStateAction.m_BindingsCount == 0)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position | TrackedPoseDriver.TrackingStates.Rotation;
				return;
			}
			InputActionMap orCreateActionMap = trackingStateAction.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			InputActionState state = orCreateActionMap.m_State;
			bool hasResolvedControl = false;
			if (state != null)
			{
				int actionIndex = trackingStateAction.m_ActionIndexInState;
				int totalBindingCount = state.totalBindingCount;
				for (int i = 0; i < totalBindingCount; i++)
				{
					ref InputActionState.BindingState bindingState = ref state.bindingStates[i];
					if (bindingState.actionIndex == actionIndex && !bindingState.isComposite && bindingState.controlCount > 0)
					{
						hasResolvedControl = true;
						break;
					}
				}
			}
			if (hasResolvedControl)
			{
				this.m_CurrentTrackingState = (TrackedPoseDriver.TrackingStates)trackingStateAction.ReadValue<int>();
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0003E59E File Offset: 0x0003C79E
		protected virtual void OnUpdate()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.Update || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003E5B7 File Offset: 0x0003C7B7
		protected virtual void OnBeforeRender()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.BeforeRender || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0003E5D0 File Offset: 0x0003C7D0
		protected virtual void PerformUpdate()
		{
			this.SetLocalTransform(this.m_CurrentPosition, this.m_CurrentRotation);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003E5E4 File Offset: 0x0003C7E4
		protected virtual void SetLocalTransform(Vector3 newPosition, Quaternion newRotation)
		{
			bool positionValid = this.m_IgnoreTrackingState || (this.m_CurrentTrackingState & TrackedPoseDriver.TrackingStates.Position) > TrackedPoseDriver.TrackingStates.None;
			bool rotationValid = this.m_IgnoreTrackingState || (this.m_CurrentTrackingState & TrackedPoseDriver.TrackingStates.Rotation) > TrackedPoseDriver.TrackingStates.None;
			if (this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition && rotationValid && positionValid)
			{
				base.transform.SetLocalPositionAndRotation(newPosition, newRotation);
				return;
			}
			if (rotationValid && (this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition || this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationOnly))
			{
				base.transform.localRotation = newRotation;
			}
			if (positionValid && (this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition || this.m_TrackingType == TrackedPoseDriver.TrackingType.PositionOnly))
			{
				base.transform.localPosition = newPosition;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003E67C File Offset: 0x0003C87C
		private bool HasStereoCamera(out Camera cameraComponent)
		{
			return base.TryGetComponent<Camera>(out cameraComponent) && cameraComponent.stereoEnabled;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0003E690 File Offset: 0x0003C890
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0003E69D File Offset: 0x0003C89D
		public InputAction positionAction
		{
			get
			{
				return this.m_PositionInput.action;
			}
			set
			{
				this.positionInput = new InputActionProperty(value);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0003E6AB File Offset: 0x0003C8AB
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0003E6B8 File Offset: 0x0003C8B8
		public InputAction rotationAction
		{
			get
			{
				return this.m_RotationInput.action;
			}
			set
			{
				this.rotationInput = new InputActionProperty(value);
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000049FE File Offset: 0x00002BFE
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003E6C8 File Offset: 0x0003C8C8
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_PositionInput.serializedReference == null && this.m_PositionInput.serializedAction == null && this.m_PositionAction != null)
			{
				this.m_PositionInput = new InputActionProperty(this.m_PositionAction);
			}
			if (this.m_RotationInput.serializedReference == null && this.m_RotationInput.serializedAction == null && this.m_RotationAction != null)
			{
				this.m_RotationInput = new InputActionProperty(this.m_RotationAction);
			}
		}

		// Token: 0x04000550 RID: 1360
		[SerializeField]
		[Tooltip("Which Transform properties to update.")]
		private TrackedPoseDriver.TrackingType m_TrackingType;

		// Token: 0x04000551 RID: 1361
		[SerializeField]
		[Tooltip("Updates the Transform properties after these phases of Input System event processing.")]
		private TrackedPoseDriver.UpdateType m_UpdateType;

		// Token: 0x04000552 RID: 1362
		[SerializeField]
		[Tooltip("Ignore Tracking State and always treat the input pose as valid.")]
		private bool m_IgnoreTrackingState;

		// Token: 0x04000553 RID: 1363
		[SerializeField]
		[Tooltip("The input action to read the position value of a tracked device. Must be a Vector 3 control type.")]
		private InputActionProperty m_PositionInput;

		// Token: 0x04000554 RID: 1364
		[SerializeField]
		[Tooltip("The input action to read the rotation value of a tracked device. Must be a Quaternion control type.")]
		private InputActionProperty m_RotationInput;

		// Token: 0x04000555 RID: 1365
		[SerializeField]
		[Tooltip("The input action to read the tracking state value of a tracked device. Identifies if position and rotation have valid data. Must be an Integer control type.")]
		private InputActionProperty m_TrackingStateInput;

		// Token: 0x04000556 RID: 1366
		private Vector3 m_CurrentPosition = Vector3.zero;

		// Token: 0x04000557 RID: 1367
		private Quaternion m_CurrentRotation = Quaternion.identity;

		// Token: 0x04000558 RID: 1368
		private TrackedPoseDriver.TrackingStates m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position | TrackedPoseDriver.TrackingStates.Rotation;

		// Token: 0x04000559 RID: 1369
		private bool m_RotationBound;

		// Token: 0x0400055A RID: 1370
		private bool m_PositionBound;

		// Token: 0x0400055B RID: 1371
		private bool m_TrackingStateBound;

		// Token: 0x0400055C RID: 1372
		private bool m_IsFirstUpdate = true;

		// Token: 0x0400055D RID: 1373
		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private InputAction m_PositionAction;

		// Token: 0x0400055E RID: 1374
		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private InputAction m_RotationAction;

		// Token: 0x020000E6 RID: 230
		public enum TrackingType
		{
			// Token: 0x04000560 RID: 1376
			RotationAndPosition,
			// Token: 0x04000561 RID: 1377
			RotationOnly,
			// Token: 0x04000562 RID: 1378
			PositionOnly
		}

		// Token: 0x020000E7 RID: 231
		[Flags]
		private enum TrackingStates
		{
			// Token: 0x04000564 RID: 1380
			None = 0,
			// Token: 0x04000565 RID: 1381
			Position = 1,
			// Token: 0x04000566 RID: 1382
			Rotation = 2
		}

		// Token: 0x020000E8 RID: 232
		public enum UpdateType
		{
			// Token: 0x04000568 RID: 1384
			UpdateAndBeforeRender,
			// Token: 0x04000569 RID: 1385
			Update,
			// Token: 0x0400056A RID: 1386
			BeforeRender
		}
	}
}
