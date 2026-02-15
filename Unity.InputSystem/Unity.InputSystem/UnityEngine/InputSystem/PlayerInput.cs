using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000D5 RID: 213
	[AddComponentMenu("Input/Player Input")]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/PlayerInput.html")]
	public class PlayerInput : MonoBehaviour
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0003B1BE File Offset: 0x000393BE
		public bool inputIsActive
		{
			get
			{
				return this.m_InputActive;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0003B1C6 File Offset: 0x000393C6
		[Obsolete("Use inputIsActive instead.")]
		public bool active
		{
			get
			{
				return this.inputIsActive;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0003B1CE File Offset: 0x000393CE
		public int playerIndex
		{
			get
			{
				return this.m_PlayerIndex;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0003B1D6 File Offset: 0x000393D6
		public int splitScreenIndex
		{
			get
			{
				return this.m_SplitScreenIndex;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0003B1DE File Offset: 0x000393DE
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0003B204 File Offset: 0x00039404
		public InputActionAsset actions
		{
			get
			{
				if (!this.m_ActionsInitialized && base.gameObject.activeInHierarchy)
				{
					this.InitializeActions();
				}
				return this.m_Actions;
			}
			set
			{
				if (this.m_Actions == value)
				{
					return;
				}
				if (this.m_Actions != null)
				{
					this.m_Actions.Disable();
					if (this.m_ActionsInitialized)
					{
						this.UninitializeActions();
					}
				}
				this.m_Actions = value;
				if (this.m_Enabled)
				{
					this.ClearCaches();
					this.AssignUserAndDevices();
					this.InitializeActions();
					if (this.m_InputActive)
					{
						this.ActivateInput();
					}
				}
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0003B278 File Offset: 0x00039478
		public string currentControlScheme
		{
			get
			{
				if (!this.m_InputUser.valid)
				{
					return null;
				}
				InputControlScheme? scheme = this.m_InputUser.controlScheme;
				if (scheme == null)
				{
					return null;
				}
				return scheme.GetValueOrDefault().name;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0003B2BA File Offset: 0x000394BA
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0003B2C2 File Offset: 0x000394C2
		public string defaultControlScheme
		{
			get
			{
				return this.m_DefaultControlScheme;
			}
			set
			{
				this.m_DefaultControlScheme = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0003B2CB File Offset: 0x000394CB
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0003B2D3 File Offset: 0x000394D3
		public bool neverAutoSwitchControlSchemes
		{
			get
			{
				return this.m_NeverAutoSwitchControlSchemes;
			}
			set
			{
				if (this.m_NeverAutoSwitchControlSchemes == value)
				{
					return;
				}
				this.m_NeverAutoSwitchControlSchemes = value;
				if (this.m_Enabled)
				{
					if (!value && !this.m_OnUnpairedDeviceUsedHooked)
					{
						this.StartListeningForUnpairedDeviceActivity();
						return;
					}
					if (value && this.m_OnUnpairedDeviceUsedHooked)
					{
						this.StopListeningForUnpairedDeviceActivity();
					}
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0003B311 File Offset: 0x00039511
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0003B319 File Offset: 0x00039519
		public InputActionMap currentActionMap
		{
			get
			{
				return this.m_CurrentActionMap;
			}
			set
			{
				InputActionMap currentActionMap = this.m_CurrentActionMap;
				this.m_CurrentActionMap = null;
				if (currentActionMap != null)
				{
					currentActionMap.Disable();
				}
				this.m_CurrentActionMap = value;
				InputActionMap currentActionMap2 = this.m_CurrentActionMap;
				if (currentActionMap2 == null)
				{
					return;
				}
				currentActionMap2.Enable();
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0003B34A File Offset: 0x0003954A
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0003B352 File Offset: 0x00039552
		public string defaultActionMap
		{
			get
			{
				return this.m_DefaultActionMap;
			}
			set
			{
				this.m_DefaultActionMap = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0003B35B File Offset: 0x0003955B
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0003B363 File Offset: 0x00039563
		public PlayerNotifications notificationBehavior
		{
			get
			{
				return this.m_NotificationBehavior;
			}
			set
			{
				if (this.m_NotificationBehavior == value)
				{
					return;
				}
				if (this.m_Enabled)
				{
					this.UninitializeActions();
				}
				this.m_NotificationBehavior = value;
				if (this.m_Enabled)
				{
					this.InitializeActions();
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0003B392 File Offset: 0x00039592
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0003B39F File Offset: 0x0003959F
		public ReadOnlyArray<PlayerInput.ActionEvent> actionEvents
		{
			get
			{
				return this.m_ActionEvents;
			}
			set
			{
				if (this.m_Enabled)
				{
					this.UninitializeActions();
				}
				this.m_ActionEvents = value.ToArray();
				if (this.m_Enabled)
				{
					this.InitializeActions();
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0003B3CA File Offset: 0x000395CA
		public PlayerInput.DeviceLostEvent deviceLostEvent
		{
			get
			{
				if (this.m_DeviceLostEvent == null)
				{
					this.m_DeviceLostEvent = new PlayerInput.DeviceLostEvent();
				}
				return this.m_DeviceLostEvent;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0003B3E5 File Offset: 0x000395E5
		public PlayerInput.DeviceRegainedEvent deviceRegainedEvent
		{
			get
			{
				if (this.m_DeviceRegainedEvent == null)
				{
					this.m_DeviceRegainedEvent = new PlayerInput.DeviceRegainedEvent();
				}
				return this.m_DeviceRegainedEvent;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0003B400 File Offset: 0x00039600
		public PlayerInput.ControlsChangedEvent controlsChangedEvent
		{
			get
			{
				if (this.m_ControlsChangedEvent == null)
				{
					this.m_ControlsChangedEvent = new PlayerInput.ControlsChangedEvent();
				}
				return this.m_ControlsChangedEvent;
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000B64 RID: 2916 RVA: 0x0003B41B File Offset: 0x0003961B
		// (remove) Token: 0x06000B65 RID: 2917 RVA: 0x0003B437 File Offset: 0x00039637
		public event Action<InputAction.CallbackContext> onActionTriggered
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ActionTriggeredCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ActionTriggeredCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000B66 RID: 2918 RVA: 0x0003B453 File Offset: 0x00039653
		// (remove) Token: 0x06000B67 RID: 2919 RVA: 0x0003B46F File Offset: 0x0003966F
		public event Action<PlayerInput> onDeviceLost
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceLostCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceLostCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B68 RID: 2920 RVA: 0x0003B48B File Offset: 0x0003968B
		// (remove) Token: 0x06000B69 RID: 2921 RVA: 0x0003B4A7 File Offset: 0x000396A7
		public event Action<PlayerInput> onDeviceRegained
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceRegainedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceRegainedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000B6A RID: 2922 RVA: 0x0003B4C3 File Offset: 0x000396C3
		// (remove) Token: 0x06000B6B RID: 2923 RVA: 0x0003B4DF File Offset: 0x000396DF
		public event Action<PlayerInput> onControlsChanged
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ControlsChangedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ControlsChangedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0003B4FB File Offset: 0x000396FB
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0003B503 File Offset: 0x00039703
		public Camera camera
		{
			get
			{
				return this.m_Camera;
			}
			set
			{
				this.m_Camera = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0003B50C File Offset: 0x0003970C
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0003B514 File Offset: 0x00039714
		public InputSystemUIInputModule uiInputModule
		{
			get
			{
				return this.m_UIInputModule;
			}
			set
			{
				if (this.m_UIInputModule == value)
				{
					return;
				}
				if (this.m_UIInputModule != null && this.m_UIInputModule.actionsAsset == this.m_Actions)
				{
					this.m_UIInputModule.actionsAsset = null;
				}
				this.m_UIInputModule = value;
				if (this.m_UIInputModule != null && this.m_Actions != null)
				{
					this.m_UIInputModule.actionsAsset = this.m_Actions;
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0003B596 File Offset: 0x00039796
		public InputUser user
		{
			get
			{
				return this.m_InputUser;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0003B5A0 File Offset: 0x000397A0
		public ReadOnlyArray<InputDevice> devices
		{
			get
			{
				if (!this.m_InputUser.valid)
				{
					return default(ReadOnlyArray<InputDevice>);
				}
				return this.m_InputUser.pairedDevices;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0003B5D0 File Offset: 0x000397D0
		public bool hasMissingRequiredDevices
		{
			get
			{
				return this.user.valid && this.user.hasMissingRequiredDevices;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0003B5FD File Offset: 0x000397FD
		public static ReadOnlyArray<PlayerInput> all
		{
			get
			{
				return new ReadOnlyArray<PlayerInput>(PlayerInput.s_AllActivePlayers, 0, PlayerInput.s_AllActivePlayersCount);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0003B60F File Offset: 0x0003980F
		public static bool isSinglePlayer
		{
			get
			{
				return PlayerInput.s_AllActivePlayersCount <= 1 && (PlayerInputManager.instance == null || !PlayerInputManager.instance.joiningEnabled);
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003B638 File Offset: 0x00039838
		public TDevice GetDevice<TDevice>() where TDevice : InputDevice
		{
			foreach (InputDevice inputDevice in this.devices)
			{
				TDevice deviceOfType = inputDevice as TDevice;
				if (deviceOfType != null)
				{
					return deviceOfType;
				}
			}
			return default(TDevice);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0003B6AC File Offset: 0x000398AC
		public void ActivateInput()
		{
			this.m_InputActive = true;
			if (this.m_CurrentActionMap == null && this.m_Actions != null && !string.IsNullOrEmpty(this.m_DefaultActionMap))
			{
				this.SwitchCurrentActionMap(this.m_DefaultActionMap);
				return;
			}
			InputActionMap currentActionMap = this.m_CurrentActionMap;
			if (currentActionMap == null)
			{
				return;
			}
			currentActionMap.Enable();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0003B700 File Offset: 0x00039900
		public void DeactivateInput()
		{
			InputActionMap currentActionMap = this.m_CurrentActionMap;
			if (currentActionMap != null)
			{
				currentActionMap.Disable();
			}
			this.m_InputActive = false;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003B71A File Offset: 0x0003991A
		[Obsolete("Use DeactivateInput instead.")]
		public void PassivateInput()
		{
			this.DeactivateInput();
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0003B724 File Offset: 0x00039924
		public bool SwitchCurrentControlScheme(params InputDevice[] devices)
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (this.actions == null)
			{
				throw new InvalidOperationException("Must set actions on PlayerInput in order to be able to switch control schemes");
			}
			InputControlScheme? scheme = InputControlScheme.FindControlSchemeForDevices<InputDevice[], ReadOnlyArray<InputControlScheme>>(devices, this.actions.controlSchemes, null, false);
			if (scheme == null)
			{
				return false;
			}
			InputControlScheme controlScheme = scheme.Value;
			this.SwitchControlSchemeInternal(ref controlScheme, devices);
			return true;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0003B78C File Offset: 0x0003998C
		public void SwitchCurrentControlScheme(string controlScheme, params InputDevice[] devices)
		{
			if (string.IsNullOrEmpty(controlScheme))
			{
				throw new ArgumentNullException("controlScheme");
			}
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			InputControlScheme scheme;
			this.user.FindControlScheme(controlScheme, out scheme);
			this.SwitchControlSchemeInternal(ref scheme, devices);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003B7D4 File Offset: 0x000399D4
		public void SwitchCurrentActionMap(string mapNameOrId)
		{
			if (!this.m_Enabled)
			{
				Debug.LogError("Cannot switch to actions '" + mapNameOrId + "'; input is not enabled", this);
				return;
			}
			if (this.m_Actions == null)
			{
				Debug.LogError("Cannot switch to actions '" + mapNameOrId + "'; no actions set on PlayerInput", this);
				return;
			}
			InputActionMap actionMap = this.m_Actions.FindActionMap(mapNameOrId, false);
			if (actionMap == null)
			{
				Debug.LogError(string.Format("Cannot find action map '{0}' in actions '{1}'", mapNameOrId, this.m_Actions), this);
				return;
			}
			this.currentActionMap = actionMap;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0003B858 File Offset: 0x00039A58
		public static PlayerInput GetPlayerByIndex(int playerIndex)
		{
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				if (PlayerInput.s_AllActivePlayers[i].playerIndex == playerIndex)
				{
					return PlayerInput.s_AllActivePlayers[i];
				}
			}
			return null;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0003B890 File Offset: 0x00039A90
		public static PlayerInput FindFirstPairedToDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				if (PlayerInput.s_AllActivePlayers[i].devices.ContainsReference(device))
				{
					return PlayerInput.s_AllActivePlayers[i];
				}
			}
			return null;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		public static PlayerInput Instantiate(GameObject prefab, int playerIndex = -1, string controlScheme = null, int splitScreenIndex = -1, InputDevice pairWithDevice = null)
		{
			if (prefab == null)
			{
				throw new ArgumentNullException("prefab");
			}
			PlayerInput.s_InitPlayerIndex = playerIndex;
			PlayerInput.s_InitSplitScreenIndex = splitScreenIndex;
			PlayerInput.s_InitControlScheme = controlScheme;
			if (pairWithDevice != null)
			{
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref PlayerInput.s_InitPairWithDevices, ref PlayerInput.s_InitPairWithDevicesCount, pairWithDevice, 10);
			}
			return PlayerInput.DoInstantiate(prefab);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003B92C File Offset: 0x00039B2C
		public static PlayerInput Instantiate(GameObject prefab, int playerIndex = -1, string controlScheme = null, int splitScreenIndex = -1, params InputDevice[] pairWithDevices)
		{
			if (prefab == null)
			{
				throw new ArgumentNullException("prefab");
			}
			PlayerInput.s_InitPlayerIndex = playerIndex;
			PlayerInput.s_InitSplitScreenIndex = splitScreenIndex;
			PlayerInput.s_InitControlScheme = controlScheme;
			if (pairWithDevices != null)
			{
				for (int i = 0; i < pairWithDevices.Length; i++)
				{
					ArrayHelpers.AppendWithCapacity<InputDevice>(ref PlayerInput.s_InitPairWithDevices, ref PlayerInput.s_InitPairWithDevicesCount, pairWithDevices[i], 10);
				}
			}
			return PlayerInput.DoInstantiate(prefab);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003B990 File Offset: 0x00039B90
		private static PlayerInput DoInstantiate(GameObject prefab)
		{
			bool destroyIfDeviceSetupUnsuccessful = PlayerInput.s_DestroyIfDeviceSetupUnsuccessful;
			GameObject instance;
			try
			{
				instance = Object.Instantiate<GameObject>(prefab);
				instance.SetActive(true);
			}
			finally
			{
				PlayerInput.s_InitPairWithDevicesCount = 0;
				if (PlayerInput.s_InitPairWithDevices != null)
				{
					Array.Clear(PlayerInput.s_InitPairWithDevices, 0, PlayerInput.s_InitPairWithDevicesCount);
				}
				PlayerInput.s_InitControlScheme = null;
				PlayerInput.s_InitPlayerIndex = -1;
				PlayerInput.s_InitSplitScreenIndex = -1;
				PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = false;
			}
			PlayerInput playerInput = instance.GetComponentInChildren<PlayerInput>();
			if (playerInput == null)
			{
				Object.DestroyImmediate(instance);
				Debug.LogError("The GameObject does not have a PlayerInput component", prefab);
				return null;
			}
			if (destroyIfDeviceSetupUnsuccessful && (!playerInput.user.valid || playerInput.hasMissingRequiredDevices))
			{
				Object.DestroyImmediate(instance);
				return null;
			}
			return playerInput;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0003BA40 File Offset: 0x00039C40
		private void InitializeActions()
		{
			if (this.m_ActionsInitialized)
			{
				return;
			}
			if (this.m_Actions == null)
			{
				return;
			}
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				if (PlayerInput.s_AllActivePlayers[i].m_Actions == this.m_Actions && PlayerInput.s_AllActivePlayers[i] != this)
				{
					InputActionAsset oldActions = this.m_Actions;
					this.m_Actions = Object.Instantiate<InputActionAsset>(this.m_Actions);
					for (int actionMap = 0; actionMap < oldActions.actionMaps.Count; actionMap++)
					{
						for (int binding = 0; binding < oldActions.actionMaps[actionMap].bindings.Count; binding++)
						{
							this.m_Actions.actionMaps[actionMap].ApplyBindingOverride(binding, oldActions.actionMaps[actionMap].bindings[binding]);
						}
					}
					break;
				}
			}
			if (this.uiInputModule != null)
			{
				this.uiInputModule.actionsAsset = this.m_Actions;
			}
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
			case PlayerNotifications.BroadcastMessages:
				this.InstallOnActionTriggeredHook();
				if (this.m_ActionMessageNames == null)
				{
					this.CacheMessageNames();
				}
				break;
			case PlayerNotifications.InvokeUnityEvents:
				if (this.m_ActionEvents != null)
				{
					foreach (PlayerInput.ActionEvent actionEvent in this.m_ActionEvents)
					{
						string id = actionEvent.actionId;
						if (!string.IsNullOrEmpty(id))
						{
							InputAction action = this.m_Actions.FindAction(id, false);
							if (action != null)
							{
								action.performed += actionEvent.Invoke;
								action.canceled += actionEvent.Invoke;
								action.started += actionEvent.Invoke;
							}
						}
					}
				}
				break;
			case PlayerNotifications.InvokeCSharpEvents:
				this.InstallOnActionTriggeredHook();
				break;
			}
			this.m_ActionsInitialized = true;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0003BC40 File Offset: 0x00039E40
		private void UninitializeActions()
		{
			if (!this.m_ActionsInitialized)
			{
				return;
			}
			if (this.m_Actions == null)
			{
				return;
			}
			this.UninstallOnActionTriggeredHook();
			if (this.m_NotificationBehavior == PlayerNotifications.InvokeUnityEvents && this.m_ActionEvents != null)
			{
				foreach (PlayerInput.ActionEvent actionEvent in this.m_ActionEvents)
				{
					string id = actionEvent.actionId;
					if (!string.IsNullOrEmpty(id))
					{
						InputAction action = this.m_Actions.FindAction(id, false);
						if (action != null)
						{
							action.performed -= actionEvent.Invoke;
							action.canceled -= actionEvent.Invoke;
							action.started -= actionEvent.Invoke;
						}
					}
				}
			}
			this.m_CurrentActionMap = null;
			this.m_ActionsInitialized = false;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003BD00 File Offset: 0x00039F00
		private void InstallOnActionTriggeredHook()
		{
			if (this.m_ActionTriggeredDelegate == null)
			{
				this.m_ActionTriggeredDelegate = new Action<InputAction.CallbackContext>(this.OnActionTriggered);
			}
			foreach (InputActionMap inputActionMap in this.m_Actions.actionMaps)
			{
				inputActionMap.actionTriggered += this.m_ActionTriggeredDelegate;
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003BD78 File Offset: 0x00039F78
		private void UninstallOnActionTriggeredHook()
		{
			if (this.m_ActionTriggeredDelegate != null)
			{
				foreach (InputActionMap inputActionMap in this.m_Actions.actionMaps)
				{
					inputActionMap.actionTriggered -= this.m_ActionTriggeredDelegate;
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		private void OnActionTriggered(InputAction.CallbackContext context)
		{
			if (!this.m_InputActive)
			{
				return;
			}
			PlayerNotifications notificationBehavior = this.m_NotificationBehavior;
			if (notificationBehavior > PlayerNotifications.BroadcastMessages)
			{
				if (notificationBehavior == PlayerNotifications.InvokeCSharpEvents)
				{
					DelegateHelpers.InvokeCallbacksSafe<InputAction.CallbackContext>(ref this.m_ActionTriggeredCallbacks, context, "PlayerInput.onActionTriggered", null);
					return;
				}
			}
			else
			{
				InputAction action = context.action;
				if (!context.performed && (!context.canceled || action.type != InputActionType.Value))
				{
					return;
				}
				if (this.m_ActionMessageNames == null)
				{
					this.CacheMessageNames();
				}
				string messageName = this.m_ActionMessageNames[action.m_Id];
				if (this.m_InputValueObject == null)
				{
					this.m_InputValueObject = new InputValue();
				}
				this.m_InputValueObject.m_Context = new InputAction.CallbackContext?(context);
				if (this.m_NotificationBehavior == PlayerNotifications.BroadcastMessages)
				{
					base.BroadcastMessage(messageName, this.m_InputValueObject, SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					base.SendMessage(messageName, this.m_InputValueObject, SendMessageOptions.DontRequireReceiver);
				}
				this.m_InputValueObject.m_Context = null;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		private void CacheMessageNames()
		{
			if (this.m_Actions == null)
			{
				return;
			}
			if (this.m_ActionMessageNames != null)
			{
				this.m_ActionMessageNames.Clear();
			}
			else
			{
				this.m_ActionMessageNames = new Dictionary<string, string>();
			}
			foreach (InputAction action in this.m_Actions)
			{
				action.MakeSureIdIsInPlace();
				string name = CSharpCodeHelpers.MakeTypeName(action.name, "");
				this.m_ActionMessageNames[action.m_Id] = "On" + name;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000049FE File Offset: 0x00002BFE
		private void ClearCaches()
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0003BF68 File Offset: 0x0003A168
		private void AssignUserAndDevices()
		{
			if (this.m_InputUser.valid)
			{
				this.m_InputUser.UnpairDevices();
			}
			if (!(this.m_Actions == null))
			{
				if (this.m_Actions.controlSchemes.Count > 0)
				{
					if (!string.IsNullOrEmpty(PlayerInput.s_InitControlScheme))
					{
						InputControlScheme? controlScheme = this.m_Actions.FindControlScheme(PlayerInput.s_InitControlScheme);
						if (controlScheme == null)
						{
							Debug.LogError(string.Format("No control scheme '{0}' in '{1}'", PlayerInput.s_InitControlScheme, this.m_Actions), this);
						}
						else
						{
							this.TryToActivateControlScheme(controlScheme.Value);
						}
					}
					else if (!string.IsNullOrEmpty(this.m_DefaultControlScheme))
					{
						InputControlScheme? controlScheme2 = this.m_Actions.FindControlScheme(this.m_DefaultControlScheme);
						if (controlScheme2 == null)
						{
							Debug.LogError(string.Format("Cannot find default control scheme '{0}' in '{1}'", this.m_DefaultControlScheme, this.m_Actions), this);
						}
						else
						{
							this.TryToActivateControlScheme(controlScheme2.Value);
						}
					}
					if (PlayerInput.s_InitPairWithDevicesCount > 0 && (!this.m_InputUser.valid || this.m_InputUser.controlScheme == null))
					{
						InputControlScheme? controlScheme3 = InputControlScheme.FindControlSchemeForDevices<ReadOnlyArray<InputDevice>, ReadOnlyArray<InputControlScheme>>(new ReadOnlyArray<InputDevice>(PlayerInput.s_InitPairWithDevices, 0, PlayerInput.s_InitPairWithDevicesCount), this.m_Actions.controlSchemes, null, true);
						if (controlScheme3 != null)
						{
							this.TryToActivateControlScheme(controlScheme3.Value);
							goto IL_02D7;
						}
						goto IL_02D7;
					}
					else
					{
						if ((this.m_InputUser.valid && this.m_InputUser.controlScheme != null) || !string.IsNullOrEmpty(PlayerInput.s_InitControlScheme))
						{
							goto IL_02D7;
						}
						using (InputControlList<InputDevice> availableDevices = InputUser.GetUnpairedInputDevices())
						{
							InputControlScheme? controlScheme4 = InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(availableDevices, this.m_Actions.controlSchemes, null, false);
							if (controlScheme4 != null)
							{
								this.TryToActivateControlScheme(controlScheme4.Value);
								goto IL_02D7;
							}
							if (InputSystem.devices.Count > 0 && availableDevices.Count == 0)
							{
								Debug.LogWarning("Cannot find matching control scheme for " + base.name + " (all control schemes are already paired to matching devices)", this);
							}
							goto IL_02D7;
						}
					}
				}
				if (PlayerInput.s_InitPairWithDevicesCount > 0)
				{
					for (int i = 0; i < PlayerInput.s_InitPairWithDevicesCount; i++)
					{
						this.m_InputUser = InputUser.PerformPairingWithDevice(PlayerInput.s_InitPairWithDevices[i], this.m_InputUser, InputUserPairingOptions.None);
					}
				}
				else
				{
					using (InputControlList<InputDevice> availableDevices2 = InputUser.GetUnpairedInputDevices())
					{
						for (int j = 0; j < availableDevices2.Count; j++)
						{
							InputDevice device = availableDevices2[j];
							if (this.HaveBindingForDevice(device))
							{
								this.m_InputUser = InputUser.PerformPairingWithDevice(device, this.m_InputUser, InputUserPairingOptions.None);
							}
						}
					}
				}
				IL_02D7:
				if (this.m_InputUser.valid)
				{
					this.m_InputUser.AssociateActionsWithUser(this.m_Actions);
				}
				return;
			}
			if (PlayerInput.s_InitPairWithDevicesCount > 0)
			{
				for (int k = 0; k < PlayerInput.s_InitPairWithDevicesCount; k++)
				{
					this.m_InputUser = InputUser.PerformPairingWithDevice(PlayerInput.s_InitPairWithDevices[k], this.m_InputUser, InputUserPairingOptions.None);
				}
				return;
			}
			this.m_InputUser = default(InputUser);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003C288 File Offset: 0x0003A488
		private bool HaveBindingForDevice(InputDevice device)
		{
			if (this.m_Actions == null)
			{
				return false;
			}
			ReadOnlyArray<InputActionMap> actionMaps = this.m_Actions.actionMaps;
			for (int i = 0; i < actionMaps.Count; i++)
			{
				if (actionMaps[i].IsUsableWithDevice(device))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003C2D8 File Offset: 0x0003A4D8
		private void UnassignUserAndDevices()
		{
			if (this.m_InputUser.valid)
			{
				this.m_InputUser.UnpairDevicesAndRemoveUser();
			}
			if (this.m_Actions != null)
			{
				this.m_Actions.devices = null;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0003C320 File Offset: 0x0003A520
		private bool TryToActivateControlScheme(InputControlScheme controlScheme)
		{
			if (PlayerInput.s_InitPairWithDevicesCount > 0)
			{
				for (int i = 0; i < PlayerInput.s_InitPairWithDevicesCount; i++)
				{
					InputDevice device = PlayerInput.s_InitPairWithDevices[i];
					if (!controlScheme.SupportsDevice(device))
					{
						return false;
					}
				}
				for (int j = 0; j < PlayerInput.s_InitPairWithDevicesCount; j++)
				{
					InputDevice device2 = PlayerInput.s_InitPairWithDevices[j];
					this.m_InputUser = InputUser.PerformPairingWithDevice(device2, this.m_InputUser, InputUserPairingOptions.None);
				}
			}
			if (!this.m_InputUser.valid)
			{
				this.m_InputUser = InputUser.CreateUserWithoutPairedDevices();
			}
			this.m_InputUser.ActivateControlScheme(controlScheme).AndPairRemainingDevices();
			if (this.user.hasMissingRequiredDevices)
			{
				this.m_InputUser.ActivateControlScheme(null);
				this.m_InputUser.UnpairDevices();
				return false;
			}
			return true;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		private void AssignPlayerIndex()
		{
			if (PlayerInput.s_InitPlayerIndex != -1)
			{
				this.m_PlayerIndex = PlayerInput.s_InitPlayerIndex;
				return;
			}
			int minPlayerIndex = int.MaxValue;
			int maxPlayerIndex = int.MinValue;
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				int playerIndex = PlayerInput.s_AllActivePlayers[i].playerIndex;
				minPlayerIndex = Math.Min(minPlayerIndex, playerIndex);
				maxPlayerIndex = Math.Max(maxPlayerIndex, playerIndex);
			}
			if (minPlayerIndex != 2147483647 && minPlayerIndex > 0)
			{
				this.m_PlayerIndex = minPlayerIndex - 1;
				return;
			}
			if (maxPlayerIndex != -2147483648)
			{
				for (int j = minPlayerIndex; j < maxPlayerIndex; j++)
				{
					if (PlayerInput.GetPlayerByIndex(j) == null)
					{
						this.m_PlayerIndex = j;
						return;
					}
				}
				this.m_PlayerIndex = maxPlayerIndex + 1;
				return;
			}
			this.m_PlayerIndex = 0;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003C494 File Offset: 0x0003A694
		private void OnEnable()
		{
			this.m_Enabled = true;
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				this.AssignPlayerIndex();
				this.InitializeActions();
				this.AssignUserAndDevices();
				this.ActivateInput();
			}
			if (PlayerInput.s_InitSplitScreenIndex >= 0)
			{
				this.m_SplitScreenIndex = this.splitScreenIndex;
			}
			else
			{
				this.m_SplitScreenIndex = this.playerIndex;
			}
			ArrayHelpers.AppendWithCapacity<PlayerInput>(ref PlayerInput.s_AllActivePlayers, ref PlayerInput.s_AllActivePlayersCount, this, 10);
			for (int i = 1; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				int j = i;
				while (j > 0 && PlayerInput.s_AllActivePlayers[j - 1].playerIndex > PlayerInput.s_AllActivePlayers[j].playerIndex)
				{
					PlayerInput.s_AllActivePlayers.SwapElements(j, j - 1);
					j--;
				}
			}
			if (PlayerInput.s_AllActivePlayersCount == 1)
			{
				if (PlayerInput.s_UserChangeDelegate == null)
				{
					PlayerInput.s_UserChangeDelegate = new Action<InputUser, InputUserChange, InputDevice>(PlayerInput.OnUserChange);
				}
				InputUser.onChange += PlayerInput.s_UserChangeDelegate;
			}
			if (PlayerInput.isSinglePlayer)
			{
				if (this.m_Actions != null && this.m_Actions.controlSchemes.Count == 0)
				{
					this.StartListeningForDeviceChanges();
				}
				else if (!this.neverAutoSwitchControlSchemes)
				{
					this.StartListeningForUnpairedDeviceActivity();
				}
			}
			this.HandleControlsChanged();
			PlayerInputManager instance = PlayerInputManager.instance;
			if (instance == null)
			{
				return;
			}
			instance.NotifyPlayerJoined(this);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		private void StartListeningForUnpairedDeviceActivity()
		{
			if (this.m_OnUnpairedDeviceUsedHooked)
			{
				return;
			}
			if (this.m_UnpairedDeviceUsedDelegate == null)
			{
				this.m_UnpairedDeviceUsedDelegate = new Action<InputControl, InputEventPtr>(this.OnUnpairedDeviceUsed);
			}
			if (this.m_PreFilterUnpairedDeviceUsedDelegate == null)
			{
				this.m_PreFilterUnpairedDeviceUsedDelegate = new Func<InputDevice, InputEventPtr, bool>(PlayerInput.OnPreFilterUnpairedDeviceUsed);
			}
			InputUser.onUnpairedDeviceUsed += this.m_UnpairedDeviceUsedDelegate;
			InputUser.onPrefilterUnpairedDeviceActivity += this.m_PreFilterUnpairedDeviceUsedDelegate;
			InputUser.listenForUnpairedDeviceActivity++;
			this.m_OnUnpairedDeviceUsedHooked = true;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003C653 File Offset: 0x0003A853
		private void StopListeningForUnpairedDeviceActivity()
		{
			if (!this.m_OnUnpairedDeviceUsedHooked)
			{
				return;
			}
			InputUser.onUnpairedDeviceUsed -= this.m_UnpairedDeviceUsedDelegate;
			InputUser.onPrefilterUnpairedDeviceActivity -= this.m_PreFilterUnpairedDeviceUsedDelegate;
			InputUser.listenForUnpairedDeviceActivity--;
			this.m_OnUnpairedDeviceUsedHooked = false;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003C687 File Offset: 0x0003A887
		private void StartListeningForDeviceChanges()
		{
			if (this.m_OnDeviceChangeHooked)
			{
				return;
			}
			if (this.m_DeviceChangeDelegate == null)
			{
				this.m_DeviceChangeDelegate = new Action<InputDevice, InputDeviceChange>(this.OnDeviceChange);
			}
			InputSystem.onDeviceChange += this.m_DeviceChangeDelegate;
			this.m_OnDeviceChangeHooked = true;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0003C6BE File Offset: 0x0003A8BE
		private void StopListeningForDeviceChanges()
		{
			if (!this.m_OnDeviceChangeHooked)
			{
				return;
			}
			InputSystem.onDeviceChange -= this.m_DeviceChangeDelegate;
			this.m_OnDeviceChangeHooked = false;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		private void OnDisable()
		{
			this.m_Enabled = false;
			int index = PlayerInput.s_AllActivePlayers.IndexOfReference(this, PlayerInput.s_AllActivePlayersCount);
			if (index != -1)
			{
				PlayerInput.s_AllActivePlayers.EraseAtWithCapacity(ref PlayerInput.s_AllActivePlayersCount, index);
			}
			if (PlayerInput.s_AllActivePlayersCount == 0 && PlayerInput.s_UserChangeDelegate != null)
			{
				InputUser.onChange -= PlayerInput.s_UserChangeDelegate;
			}
			this.StopListeningForUnpairedDeviceActivity();
			this.StopListeningForDeviceChanges();
			PlayerInputManager instance = PlayerInputManager.instance;
			if (instance != null)
			{
				instance.NotifyPlayerLeft(this);
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				this.DeactivateInput();
				this.UnassignUserAndDevices();
				this.UninitializeActions();
			}
			this.m_PlayerIndex = -1;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003C788 File Offset: 0x0003A988
		public void DebugLogAction(InputAction.CallbackContext context)
		{
			Debug.Log(context.ToString());
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003C79C File Offset: 0x0003A99C
		private void HandleDeviceLost()
		{
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnDeviceLost", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnDeviceLost", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInput.DeviceLostEvent deviceLostEvent = this.m_DeviceLostEvent;
				if (deviceLostEvent == null)
				{
					return;
				}
				deviceLostEvent.Invoke(this);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_DeviceLostCallbacks, this, "onDeviceLost", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0003C808 File Offset: 0x0003AA08
		private void HandleDeviceRegained()
		{
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnDeviceRegained", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnDeviceRegained", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInput.DeviceRegainedEvent deviceRegainedEvent = this.m_DeviceRegainedEvent;
				if (deviceRegainedEvent == null)
				{
					return;
				}
				deviceRegainedEvent.Invoke(this);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_DeviceRegainedCallbacks, this, "onDeviceRegained", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003C874 File Offset: 0x0003AA74
		private void HandleControlsChanged()
		{
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnControlsChanged", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnControlsChanged", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInput.ControlsChangedEvent controlsChangedEvent = this.m_ControlsChangedEvent;
				if (controlsChangedEvent == null)
				{
					return;
				}
				controlsChangedEvent.Invoke(this);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_ControlsChangedCallbacks, this, "onControlsChanged", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003C8E0 File Offset: 0x0003AAE0
		private static void OnUserChange(InputUser user, InputUserChange change, InputDevice device)
		{
			if (change - InputUserChange.DeviceLost <= 1)
			{
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					PlayerInput player = PlayerInput.s_AllActivePlayers[i];
					if (player.m_InputUser == user)
					{
						if (change == InputUserChange.DeviceLost)
						{
							player.HandleDeviceLost();
						}
						else if (change == InputUserChange.DeviceRegained)
						{
							player.HandleDeviceRegained();
						}
					}
				}
				return;
			}
			if (change != InputUserChange.ControlsChanged)
			{
				return;
			}
			for (int j = 0; j < PlayerInput.s_AllActivePlayersCount; j++)
			{
				PlayerInput player2 = PlayerInput.s_AllActivePlayers[j];
				if (player2.m_InputUser == user)
				{
					player2.HandleControlsChanged();
				}
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0003C964 File Offset: 0x0003AB64
		private static bool OnPreFilterUnpairedDeviceUsed(InputDevice device, InputEventPtr eventPtr)
		{
			InputActionAsset actions = PlayerInput.all[0].actions;
			return actions != null && actions.IsUsableWithDevice(device);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003C998 File Offset: 0x0003AB98
		private void OnUnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
		{
			if (!PlayerInput.isSinglePlayer || this.neverAutoSwitchControlSchemes)
			{
				return;
			}
			PlayerInput player = PlayerInput.all[0];
			if (player.m_Actions == null)
			{
				return;
			}
			InputDevice device = control.device;
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				using (InputControlList<InputDevice> availableDevices = InputUser.GetUnpairedInputDevices())
				{
					if (availableDevices.Count > 1)
					{
						int indexOfDevice = availableDevices.IndexOf(device);
						availableDevices.SwapElements(0, indexOfDevice);
					}
					ReadOnlyArray<InputDevice> currentDevices = player.devices;
					for (int i = 0; i < currentDevices.Count; i++)
					{
						availableDevices.Add(currentDevices[i]);
					}
					InputControlScheme controlScheme;
					InputControlScheme.MatchResult matchResult;
					if (InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(availableDevices, player.m_Actions.controlSchemes, out controlScheme, out matchResult, device, false))
					{
						try
						{
							bool userValid = player.user.valid;
							if (userValid)
							{
								player.user.UnpairDevices();
							}
							InputControlList<InputDevice> newDevices = matchResult.devices;
							for (int j = 0; j < newDevices.Count; j++)
							{
								player.m_InputUser = InputUser.PerformPairingWithDevice(newDevices[j], player.m_InputUser, InputUserPairingOptions.None);
								if (!userValid && player.actions != null)
								{
									player.m_InputUser.AssociateActionsWithUser(player.actions);
								}
							}
							player.user.ActivateControlScheme(controlScheme);
						}
						finally
						{
							matchResult.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003CB54 File Offset: 0x0003AD54
		private void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change == InputDeviceChange.Added && PlayerInput.isSinglePlayer && this.m_Actions != null && this.m_Actions.controlSchemes.Count == 0 && this.HaveBindingForDevice(device) && this.m_InputUser.valid)
			{
				InputUser.PerformPairingWithDevice(device, this.m_InputUser, InputUserPairingOptions.None);
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
		private void SwitchControlSchemeInternal(ref InputControlScheme controlScheme, params InputDevice[] devices)
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				for (int i = this.user.pairedDevices.Count - 1; i >= 0; i--)
				{
					if (!devices.ContainsReference(this.user.pairedDevices[i]))
					{
						this.user.UnpairDevice(this.user.pairedDevices[i]);
					}
				}
				foreach (InputDevice device in devices)
				{
					if (!this.user.pairedDevices.ContainsReference(device))
					{
						InputUser.PerformPairingWithDevice(device, this.user, InputUserPairingOptions.None);
					}
				}
				if (this.user.controlScheme == null || !this.user.controlScheme.Value.Equals(controlScheme))
				{
					this.user.ActivateControlScheme(controlScheme);
				}
			}
		}

		// Token: 0x040004F0 RID: 1264
		public const string DeviceLostMessage = "OnDeviceLost";

		// Token: 0x040004F1 RID: 1265
		public const string DeviceRegainedMessage = "OnDeviceRegained";

		// Token: 0x040004F2 RID: 1266
		public const string ControlsChangedMessage = "OnControlsChanged";

		// Token: 0x040004F3 RID: 1267
		[Tooltip("Input actions associated with the player.")]
		[SerializeField]
		internal InputActionAsset m_Actions;

		// Token: 0x040004F4 RID: 1268
		[Tooltip("Determine how notifications should be sent when an input-related event associated with the player happens.")]
		[SerializeField]
		internal PlayerNotifications m_NotificationBehavior;

		// Token: 0x040004F5 RID: 1269
		[Tooltip("UI InputModule that should have it's input actions synchronized to this PlayerInput's actions.")]
		[SerializeField]
		internal InputSystemUIInputModule m_UIInputModule;

		// Token: 0x040004F6 RID: 1270
		[Tooltip("Event that is triggered when the PlayerInput loses a paired device (e.g. its battery runs out).")]
		[SerializeField]
		internal PlayerInput.DeviceLostEvent m_DeviceLostEvent;

		// Token: 0x040004F7 RID: 1271
		[SerializeField]
		internal PlayerInput.DeviceRegainedEvent m_DeviceRegainedEvent;

		// Token: 0x040004F8 RID: 1272
		[SerializeField]
		internal PlayerInput.ControlsChangedEvent m_ControlsChangedEvent;

		// Token: 0x040004F9 RID: 1273
		[SerializeField]
		internal PlayerInput.ActionEvent[] m_ActionEvents;

		// Token: 0x040004FA RID: 1274
		[SerializeField]
		internal bool m_NeverAutoSwitchControlSchemes;

		// Token: 0x040004FB RID: 1275
		[SerializeField]
		internal string m_DefaultControlScheme;

		// Token: 0x040004FC RID: 1276
		[SerializeField]
		internal string m_DefaultActionMap;

		// Token: 0x040004FD RID: 1277
		[SerializeField]
		internal int m_SplitScreenIndex = -1;

		// Token: 0x040004FE RID: 1278
		[Tooltip("Reference to the player's view camera. Note that this is only required when using split-screen and/or per-player UIs. Otherwise it is safe to leave this property uninitialized.")]
		[SerializeField]
		internal Camera m_Camera;

		// Token: 0x040004FF RID: 1279
		[NonSerialized]
		private InputValue m_InputValueObject;

		// Token: 0x04000500 RID: 1280
		[NonSerialized]
		internal InputActionMap m_CurrentActionMap;

		// Token: 0x04000501 RID: 1281
		[NonSerialized]
		private int m_PlayerIndex = -1;

		// Token: 0x04000502 RID: 1282
		[NonSerialized]
		private bool m_InputActive;

		// Token: 0x04000503 RID: 1283
		[NonSerialized]
		private bool m_Enabled;

		// Token: 0x04000504 RID: 1284
		[NonSerialized]
		internal bool m_ActionsInitialized;

		// Token: 0x04000505 RID: 1285
		[NonSerialized]
		private Dictionary<string, string> m_ActionMessageNames;

		// Token: 0x04000506 RID: 1286
		[NonSerialized]
		private InputUser m_InputUser;

		// Token: 0x04000507 RID: 1287
		[NonSerialized]
		private Action<InputAction.CallbackContext> m_ActionTriggeredDelegate;

		// Token: 0x04000508 RID: 1288
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_DeviceLostCallbacks;

		// Token: 0x04000509 RID: 1289
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_DeviceRegainedCallbacks;

		// Token: 0x0400050A RID: 1290
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_ControlsChangedCallbacks;

		// Token: 0x0400050B RID: 1291
		[NonSerialized]
		private CallbackArray<Action<InputAction.CallbackContext>> m_ActionTriggeredCallbacks;

		// Token: 0x0400050C RID: 1292
		[NonSerialized]
		private Action<InputControl, InputEventPtr> m_UnpairedDeviceUsedDelegate;

		// Token: 0x0400050D RID: 1293
		[NonSerialized]
		private Func<InputDevice, InputEventPtr, bool> m_PreFilterUnpairedDeviceUsedDelegate;

		// Token: 0x0400050E RID: 1294
		[NonSerialized]
		private bool m_OnUnpairedDeviceUsedHooked;

		// Token: 0x0400050F RID: 1295
		[NonSerialized]
		private Action<InputDevice, InputDeviceChange> m_DeviceChangeDelegate;

		// Token: 0x04000510 RID: 1296
		[NonSerialized]
		private bool m_OnDeviceChangeHooked;

		// Token: 0x04000511 RID: 1297
		internal static int s_AllActivePlayersCount;

		// Token: 0x04000512 RID: 1298
		internal static PlayerInput[] s_AllActivePlayers;

		// Token: 0x04000513 RID: 1299
		private static Action<InputUser, InputUserChange, InputDevice> s_UserChangeDelegate;

		// Token: 0x04000514 RID: 1300
		private static int s_InitPairWithDevicesCount;

		// Token: 0x04000515 RID: 1301
		private static InputDevice[] s_InitPairWithDevices;

		// Token: 0x04000516 RID: 1302
		private static int s_InitPlayerIndex = -1;

		// Token: 0x04000517 RID: 1303
		private static int s_InitSplitScreenIndex = -1;

		// Token: 0x04000518 RID: 1304
		private static string s_InitControlScheme;

		// Token: 0x04000519 RID: 1305
		internal static bool s_DestroyIfDeviceSetupUnsuccessful;

		// Token: 0x020000D6 RID: 214
		[Serializable]
		public class ActionEvent : UnityEvent<InputAction.CallbackContext>
		{
			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0003CD14 File Offset: 0x0003AF14
			public string actionId
			{
				get
				{
					return this.m_ActionId;
				}
			}

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0003CD1C File Offset: 0x0003AF1C
			public string actionName
			{
				get
				{
					return this.m_ActionName;
				}
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x0003CD24 File Offset: 0x0003AF24
			public ActionEvent()
			{
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x0003CD2C File Offset: 0x0003AF2C
			public ActionEvent(InputAction action)
			{
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new ArgumentException(string.Format("Action must be part of an asset (given action '{0}' is a singleton)", action));
				}
				if (action.actionMap.asset == null)
				{
					throw new ArgumentException(string.Format("Action must be part of an asset (given action '{0}' is not)", action));
				}
				this.m_ActionId = action.id.ToString();
				this.m_ActionName = action.actionMap.name + "/" + action.name;
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x0003CDC5 File Offset: 0x0003AFC5
			public ActionEvent(Guid actionGUID, string name = null)
			{
				this.m_ActionId = actionGUID.ToString();
				this.m_ActionName = name;
			}

			// Token: 0x0400051A RID: 1306
			[SerializeField]
			private string m_ActionId;

			// Token: 0x0400051B RID: 1307
			[SerializeField]
			private string m_ActionName;
		}

		// Token: 0x020000D7 RID: 215
		[Serializable]
		public class DeviceLostEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020000D8 RID: 216
		[Serializable]
		public class DeviceRegainedEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020000D9 RID: 217
		[Serializable]
		public class ControlsChangedEvent : UnityEvent<PlayerInput>
		{
		}
	}
}
