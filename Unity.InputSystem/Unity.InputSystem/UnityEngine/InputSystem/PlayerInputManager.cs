using System;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000DA RID: 218
	[AddComponentMenu("Input/Player Input Manager")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/PlayerInputManager.html")]
	public class PlayerInputManager : MonoBehaviour
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0003CDEF File Offset: 0x0003AFEF
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0003CDF8 File Offset: 0x0003AFF8
		public bool splitScreen
		{
			get
			{
				return this.m_SplitScreen;
			}
			set
			{
				if (this.m_SplitScreen == value)
				{
					return;
				}
				this.m_SplitScreen = value;
				if (!this.m_SplitScreen)
				{
					using (ReadOnlyArray<PlayerInput>.Enumerator enumerator = PlayerInput.all.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PlayerInput playerInput = enumerator.Current;
							Camera camera = playerInput.camera;
							if (camera != null)
							{
								camera.rect = new Rect(0f, 0f, 1f, 1f);
							}
						}
						return;
					}
				}
				this.UpdateSplitScreen();
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0003CE94 File Offset: 0x0003B094
		public bool maintainAspectRatioInSplitScreen
		{
			get
			{
				return this.m_MaintainAspectRatioInSplitScreen;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0003CE9C File Offset: 0x0003B09C
		public int fixedNumberOfSplitScreens
		{
			get
			{
				return this.m_FixedNumberOfSplitScreens;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		public Rect splitScreenArea
		{
			get
			{
				return this.m_SplitScreenRect;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0003CEAC File Offset: 0x0003B0AC
		public int playerCount
		{
			get
			{
				return PlayerInput.s_AllActivePlayersCount;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0003CEB3 File Offset: 0x0003B0B3
		public int maxPlayerCount
		{
			get
			{
				return this.m_MaxPlayerCount;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003CEBB File Offset: 0x0003B0BB
		public bool joiningEnabled
		{
			get
			{
				return this.m_AllowJoining;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0003CEC3 File Offset: 0x0003B0C3
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0003CECB File Offset: 0x0003B0CB
		public PlayerJoinBehavior joinBehavior
		{
			get
			{
				return this.m_JoinBehavior;
			}
			set
			{
				if (this.m_JoinBehavior == value)
				{
					return;
				}
				bool allowJoining = this.m_AllowJoining;
				if (allowJoining)
				{
					this.DisableJoining();
				}
				this.m_JoinBehavior = value;
				if (allowJoining)
				{
					this.EnableJoining();
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0003CEF5 File Offset: 0x0003B0F5
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0003CEFD File Offset: 0x0003B0FD
		public InputActionProperty joinAction
		{
			get
			{
				return this.m_JoinAction;
			}
			set
			{
				if (this.m_JoinAction == value)
				{
					return;
				}
				bool flag = this.m_AllowJoining && this.m_JoinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered;
				if (flag)
				{
					this.DisableJoining();
				}
				this.m_JoinAction = value;
				if (flag)
				{
					this.EnableJoining();
				}
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003CF3A File Offset: 0x0003B13A
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x0003CF42 File Offset: 0x0003B142
		public PlayerNotifications notificationBehavior
		{
			get
			{
				return this.m_NotificationBehavior;
			}
			set
			{
				this.m_NotificationBehavior = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003CF4B File Offset: 0x0003B14B
		public PlayerInputManager.PlayerJoinedEvent playerJoinedEvent
		{
			get
			{
				if (this.m_PlayerJoinedEvent == null)
				{
					this.m_PlayerJoinedEvent = new PlayerInputManager.PlayerJoinedEvent();
				}
				return this.m_PlayerJoinedEvent;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0003CF66 File Offset: 0x0003B166
		public PlayerInputManager.PlayerLeftEvent playerLeftEvent
		{
			get
			{
				if (this.m_PlayerLeftEvent == null)
				{
					this.m_PlayerLeftEvent = new PlayerInputManager.PlayerLeftEvent();
				}
				return this.m_PlayerLeftEvent;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000BB6 RID: 2998 RVA: 0x0003CF81 File Offset: 0x0003B181
		// (remove) Token: 0x06000BB7 RID: 2999 RVA: 0x0003CF9D File Offset: 0x0003B19D
		public event Action<PlayerInput> onPlayerJoined
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerJoinedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerJoinedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000BB8 RID: 3000 RVA: 0x0003CFB9 File Offset: 0x0003B1B9
		// (remove) Token: 0x06000BB9 RID: 3001 RVA: 0x0003CFD5 File Offset: 0x0003B1D5
		public event Action<PlayerInput> onPlayerLeft
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerLeftCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerLeftCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0003CFF1 File Offset: 0x0003B1F1
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0003CFF9 File Offset: 0x0003B1F9
		public GameObject playerPrefab
		{
			get
			{
				return this.m_PlayerPrefab;
			}
			set
			{
				this.m_PlayerPrefab = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0003D002 File Offset: 0x0003B202
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0003D009 File Offset: 0x0003B209
		public static PlayerInputManager instance { get; private set; }

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003D014 File Offset: 0x0003B214
		public void EnableJoining()
		{
			PlayerJoinBehavior joinBehavior = this.m_JoinBehavior;
			if (joinBehavior != PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (joinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered)
				{
					if (this.m_JoinAction.action != null)
					{
						if (!this.m_JoinActionDelegateHooked)
						{
							if (this.m_JoinActionDelegate == null)
							{
								this.m_JoinActionDelegate = new Action<InputAction.CallbackContext>(this.JoinPlayerFromActionIfNotAlreadyJoined);
							}
							this.m_JoinAction.action.performed += this.m_JoinActionDelegate;
							this.m_JoinActionDelegateHooked = true;
						}
						this.m_JoinAction.action.Enable();
					}
					else
					{
						Debug.LogError("No join action configured on PlayerInputManager but join behavior is set to JoinPlayersWhenJoinActionIsTriggered", this);
					}
				}
			}
			else
			{
				this.ValidateInputActionAsset();
				if (!this.m_UnpairedDeviceUsedDelegateHooked)
				{
					if (this.m_UnpairedDeviceUsedDelegate == null)
					{
						this.m_UnpairedDeviceUsedDelegate = new Action<InputControl, InputEventPtr>(this.OnUnpairedDeviceUsed);
					}
					InputUser.onUnpairedDeviceUsed += this.m_UnpairedDeviceUsedDelegate;
					this.m_UnpairedDeviceUsedDelegateHooked = true;
					InputUser.listenForUnpairedDeviceActivity++;
				}
			}
			this.m_AllowJoining = true;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003D0F0 File Offset: 0x0003B2F0
		public void DisableJoining()
		{
			PlayerJoinBehavior joinBehavior = this.m_JoinBehavior;
			if (joinBehavior != PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (joinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered)
				{
					if (this.m_JoinActionDelegateHooked)
					{
						if (this.m_JoinAction.action != null)
						{
							this.m_JoinAction.action.performed -= this.m_JoinActionDelegate;
						}
						this.m_JoinActionDelegateHooked = false;
					}
					InputAction action = this.m_JoinAction.action;
					if (action != null)
					{
						action.Disable();
					}
				}
			}
			else if (this.m_UnpairedDeviceUsedDelegateHooked)
			{
				InputUser.onUnpairedDeviceUsed -= this.m_UnpairedDeviceUsedDelegate;
				this.m_UnpairedDeviceUsedDelegateHooked = false;
				InputUser.listenForUnpairedDeviceActivity--;
			}
			this.m_AllowJoining = false;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003D184 File Offset: 0x0003B384
		internal void JoinPlayerFromUI()
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0003D198 File Offset: 0x0003B398
		public void JoinPlayerFromAction(InputAction.CallbackContext context)
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			InputDevice device = context.control.device;
			this.JoinPlayer(-1, -1, null, device);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		public void JoinPlayerFromActionIfNotAlreadyJoined(InputAction.CallbackContext context)
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			InputDevice device = context.control.device;
			if (PlayerInput.FindFirstPairedToDevice(device) != null)
			{
				return;
			}
			this.JoinPlayer(-1, -1, null, device);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003D208 File Offset: 0x0003B408
		public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null)
		{
			if (!this.CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return PlayerInput.Instantiate(this.m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevice);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003D238 File Offset: 0x0003B438
		public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, params InputDevice[] pairWithDevices)
		{
			if (!this.CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return PlayerInput.Instantiate(this.m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevices);
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0003D268 File Offset: 0x0003B468
		internal static string[] messages
		{
			get
			{
				return new string[] { "OnPlayerJoined", "OnPlayerLeft" };
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003D280 File Offset: 0x0003B480
		private bool CheckIfPlayerCanJoin(int playerIndex = -1)
		{
			if (this.m_PlayerPrefab == null)
			{
				Debug.LogError("playerPrefab must be set in order to be able to join new players", this);
				return false;
			}
			if (this.m_MaxPlayerCount >= 0 && this.playerCount >= this.m_MaxPlayerCount)
			{
				Debug.LogWarning("Maximum number of supported players reached: " + this.maxPlayerCount.ToString(), this);
				return false;
			}
			if (playerIndex != -1)
			{
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					if (PlayerInput.s_AllActivePlayers[i].playerIndex == playerIndex)
					{
						Debug.LogError(string.Format("Player index #{0} is already taken by player {1}", playerIndex, PlayerInput.s_AllActivePlayers[i]), PlayerInput.s_AllActivePlayers[i]);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003D328 File Offset: 0x0003B528
		private void OnUnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
		{
			if (!this.m_AllowJoining)
			{
				return;
			}
			if (this.m_JoinBehavior == PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (!(control is ButtonControl))
				{
					return;
				}
				if (!this.IsDeviceUsableWithPlayerActions(control.device))
				{
					return;
				}
				this.JoinPlayer(-1, -1, null, control.device);
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003D364 File Offset: 0x0003B564
		private void OnEnable()
		{
			if (PlayerInputManager.instance == null)
			{
				PlayerInputManager.instance = this;
				if (this.joinAction.reference != null)
				{
					InputAction action = this.joinAction.action;
					Object @object;
					if (action == null)
					{
						@object = null;
					}
					else
					{
						InputActionMap actionMap = action.actionMap;
						@object = ((actionMap != null) ? actionMap.asset : null);
					}
					if (@object != null)
					{
						InputActionReference inputActionReference = InputActionReference.Create(Object.Instantiate<InputActionAsset>(this.joinAction.action.actionMap.asset).FindAction(this.joinAction.action.name, false));
						this.joinAction = new InputActionProperty(inputActionReference);
					}
				}
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					this.NotifyPlayerJoined(PlayerInput.s_AllActivePlayers[i]);
				}
				if (this.m_AllowJoining)
				{
					this.EnableJoining();
				}
				return;
			}
			Debug.LogWarning("Multiple PlayerInputManagers in the game. There should only be one PlayerInputManager", this);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0003D449 File Offset: 0x0003B649
		private void OnDisable()
		{
			if (PlayerInputManager.instance == this)
			{
				PlayerInputManager.instance = null;
			}
			if (this.m_AllowJoining)
			{
				this.DisableJoining();
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0003D46C File Offset: 0x0003B66C
		private void UpdateSplitScreen()
		{
			if (!this.m_SplitScreen)
			{
				return;
			}
			int minSplitScreenCount = 0;
			foreach (PlayerInput player in PlayerInput.all)
			{
				if (player.playerIndex >= minSplitScreenCount)
				{
					minSplitScreenCount = player.playerIndex + 1;
				}
			}
			if (this.m_FixedNumberOfSplitScreens > 0)
			{
				if (this.m_FixedNumberOfSplitScreens < minSplitScreenCount)
				{
					Debug.LogWarning(string.Format("Highest playerIndex of {0} exceeds fixed number of split-screens of {1}", minSplitScreenCount, this.m_FixedNumberOfSplitScreens), this);
				}
				minSplitScreenCount = this.m_FixedNumberOfSplitScreens;
			}
			int numDivisionsX = Mathf.CeilToInt(Mathf.Sqrt((float)minSplitScreenCount));
			int numDivisionsY = numDivisionsX;
			if (!this.m_MaintainAspectRatioInSplitScreen && numDivisionsX * (numDivisionsX - 1) >= minSplitScreenCount)
			{
				numDivisionsY--;
			}
			foreach (PlayerInput player2 in PlayerInput.all)
			{
				int splitScreenIndex = player2.splitScreenIndex;
				if (splitScreenIndex >= numDivisionsX * numDivisionsY)
				{
					Debug.LogError(string.Format("Split-screen index of {0} on player is out of range (have {1} screens); resetting to playerIndex", splitScreenIndex, numDivisionsX * numDivisionsY), player2);
					player2.m_SplitScreenIndex = player2.playerIndex;
				}
				Camera camera = player2.camera;
				if (camera == null)
				{
					Debug.LogError("Player has no camera associated with it. Cannot set up split-screen. Point PlayerInput.camera to camera for player.", player2);
				}
				else
				{
					int column = splitScreenIndex % numDivisionsX;
					int row = splitScreenIndex / numDivisionsX;
					Rect rect = new Rect
					{
						width = this.m_SplitScreenRect.width / (float)numDivisionsX,
						height = this.m_SplitScreenRect.height / (float)numDivisionsY
					};
					rect.x = this.m_SplitScreenRect.x + (float)column * rect.width;
					rect.y = this.m_SplitScreenRect.y + this.m_SplitScreenRect.height - (float)(row + 1) * rect.height;
					camera.rect = rect;
				}
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003D68C File Offset: 0x0003B88C
		private bool IsDeviceUsableWithPlayerActions(InputDevice device)
		{
			if (this.m_PlayerPrefab == null)
			{
				return true;
			}
			PlayerInput playerInput = this.m_PlayerPrefab.GetComponentInChildren<PlayerInput>();
			if (playerInput == null)
			{
				return true;
			}
			InputActionAsset actions = playerInput.actions;
			if (actions == null)
			{
				return true;
			}
			if (actions.controlSchemes.Count > 0)
			{
				using (InputControlList<InputDevice> unpairedDevices = InputUser.GetUnpairedInputDevices())
				{
					if (InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(unpairedDevices, actions.controlSchemes, device, false) == null)
					{
						return false;
					}
				}
				return true;
			}
			using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = actions.actionMaps.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsUsableWithDevice(device))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000049FE File Offset: 0x00002BFE
		private void ValidateInputActionAsset()
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003D77C File Offset: 0x0003B97C
		internal void NotifyPlayerJoined(PlayerInput player)
		{
			this.UpdateSplitScreen();
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnPlayerJoined", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnPlayerJoined", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInputManager.PlayerJoinedEvent playerJoinedEvent = this.m_PlayerJoinedEvent;
				if (playerJoinedEvent == null)
				{
					return;
				}
				playerJoinedEvent.Invoke(player);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_PlayerJoinedCallbacks, player, "onPlayerJoined", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003D7F0 File Offset: 0x0003B9F0
		internal void NotifyPlayerLeft(PlayerInput player)
		{
			this.UpdateSplitScreen();
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnPlayerLeft", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnPlayerLeft", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInputManager.PlayerLeftEvent playerLeftEvent = this.m_PlayerLeftEvent;
				if (playerLeftEvent == null)
				{
					return;
				}
				playerLeftEvent.Invoke(player);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_PlayerLeftCallbacks, player, "onPlayerLeft", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x0400051C RID: 1308
		public const string PlayerJoinedMessage = "OnPlayerJoined";

		// Token: 0x0400051D RID: 1309
		public const string PlayerLeftMessage = "OnPlayerLeft";

		// Token: 0x0400051F RID: 1311
		[SerializeField]
		internal PlayerNotifications m_NotificationBehavior;

		// Token: 0x04000520 RID: 1312
		[Tooltip("Set a limit for the maximum number of players who are able to join.")]
		[SerializeField]
		internal int m_MaxPlayerCount = -1;

		// Token: 0x04000521 RID: 1313
		[SerializeField]
		internal bool m_AllowJoining = true;

		// Token: 0x04000522 RID: 1314
		[SerializeField]
		internal PlayerJoinBehavior m_JoinBehavior;

		// Token: 0x04000523 RID: 1315
		[SerializeField]
		internal PlayerInputManager.PlayerJoinedEvent m_PlayerJoinedEvent;

		// Token: 0x04000524 RID: 1316
		[SerializeField]
		internal PlayerInputManager.PlayerLeftEvent m_PlayerLeftEvent;

		// Token: 0x04000525 RID: 1317
		[SerializeField]
		internal InputActionProperty m_JoinAction;

		// Token: 0x04000526 RID: 1318
		[SerializeField]
		internal GameObject m_PlayerPrefab;

		// Token: 0x04000527 RID: 1319
		[SerializeField]
		internal bool m_SplitScreen;

		// Token: 0x04000528 RID: 1320
		[SerializeField]
		internal bool m_MaintainAspectRatioInSplitScreen;

		// Token: 0x04000529 RID: 1321
		[Tooltip("Explicitly set a fixed number of screens or otherwise allow the screen to be divided automatically to best fit the number of players.")]
		[SerializeField]
		internal int m_FixedNumberOfSplitScreens = -1;

		// Token: 0x0400052A RID: 1322
		[SerializeField]
		internal Rect m_SplitScreenRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x0400052B RID: 1323
		[NonSerialized]
		private bool m_JoinActionDelegateHooked;

		// Token: 0x0400052C RID: 1324
		[NonSerialized]
		private bool m_UnpairedDeviceUsedDelegateHooked;

		// Token: 0x0400052D RID: 1325
		[NonSerialized]
		private Action<InputAction.CallbackContext> m_JoinActionDelegate;

		// Token: 0x0400052E RID: 1326
		[NonSerialized]
		private Action<InputControl, InputEventPtr> m_UnpairedDeviceUsedDelegate;

		// Token: 0x0400052F RID: 1327
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_PlayerJoinedCallbacks;

		// Token: 0x04000530 RID: 1328
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_PlayerLeftCallbacks;

		// Token: 0x020000DB RID: 219
		[Serializable]
		public class PlayerJoinedEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020000DC RID: 220
		[Serializable]
		public class PlayerLeftEvent : UnityEvent<PlayerInput>
		{
		}
	}
}
