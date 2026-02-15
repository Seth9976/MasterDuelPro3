using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000107 RID: 263
	public struct InputUser : IEquatable<InputUser>
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0003FB68 File Offset: 0x0003DD68
		public bool valid
		{
			get
			{
				if (this.m_Id == 0U)
				{
					return false;
				}
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUsers[i].m_Id == this.m_Id)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0003FBB4 File Offset: 0x0003DDB4
		public int index
		{
			get
			{
				if (this.m_Id == 0U)
				{
					throw new InvalidOperationException("Invalid user");
				}
				int num = InputUser.TryFindUserIndex(this.m_Id);
				if (num == -1)
				{
					throw new InvalidOperationException(string.Format("User with ID {0} is no longer valid", this.m_Id));
				}
				return num;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003FBF3 File Offset: 0x0003DDF3
		public uint id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0003FBFB File Offset: 0x0003DDFB
		public InputUserAccountHandle? platformUserAccountHandle
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountHandle;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0003FC17 File Offset: 0x0003DE17
		public string platformUserAccountName
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountName;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0003FC33 File Offset: 0x0003DE33
		public string platformUserAccountId
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountId;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0003FC50 File Offset: 0x0003DE50
		public ReadOnlyArray<InputDevice> pairedDevices
		{
			get
			{
				int userIndex = this.index;
				return new ReadOnlyArray<InputDevice>(InputUser.s_GlobalState.allPairedDevices, InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex, InputUser.s_GlobalState.allUserData[userIndex].deviceCount);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0003FCA0 File Offset: 0x0003DEA0
		public ReadOnlyArray<InputDevice> lostDevices
		{
			get
			{
				int userIndex = this.index;
				return new ReadOnlyArray<InputDevice>(InputUser.s_GlobalState.allLostDevices, InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex, InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0003FCED File Offset: 0x0003DEED
		public IInputActionCollection actions
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].actions;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003FD09 File Offset: 0x0003DF09
		public InputControlScheme? controlScheme
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlScheme;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0003FD25 File Offset: 0x0003DF25
		public InputControlScheme.MatchResult controlSchemeMatch
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlSchemeMatch;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0003FD41 File Offset: 0x0003DF41
		public bool hasMissingRequiredDevices
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlSchemeMatch.hasMissingRequiredDevices;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0003FD62 File Offset: 0x0003DF62
		public static ReadOnlyArray<InputUser> all
		{
			get
			{
				return new ReadOnlyArray<InputUser>(InputUser.s_GlobalState.allUsers, 0, InputUser.s_GlobalState.allUserCount);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000CAA RID: 3242 RVA: 0x0003FD7E File Offset: 0x0003DF7E
		// (remove) Token: 0x06000CAB RID: 3243 RVA: 0x0003FD9E File Offset: 0x0003DF9E
		public static event Action<InputUser, InputUserChange, InputDevice> onChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onChange.RemoveCallback(value);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000CAC RID: 3244 RVA: 0x0003FDBE File Offset: 0x0003DFBE
		// (remove) Token: 0x06000CAD RID: 3245 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		public static event Action<InputControl, InputEventPtr> onUnpairedDeviceUsed
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.AddCallback(value);
				if (InputUser.s_GlobalState.listenForUnpairedDeviceActivity > 0)
				{
					InputUser.HookIntoEvents();
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.RemoveCallback(value);
				if (InputUser.s_GlobalState.onUnpairedDeviceUsed.length == 0)
				{
					InputUser.UnhookFromDeviceStateChange();
				}
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000CAE RID: 3246 RVA: 0x0003FE26 File Offset: 0x0003E026
		// (remove) Token: 0x06000CAF RID: 3247 RVA: 0x0003FE46 File Offset: 0x0003E046
		public static event Func<InputDevice, InputEventPtr, bool> onPrefilterUnpairedDeviceActivity
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed.RemoveCallback(value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0003FE66 File Offset: 0x0003E066
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0003FE74 File Offset: 0x0003E074
		public static int listenForUnpairedDeviceActivity
		{
			get
			{
				return InputUser.s_GlobalState.listenForUnpairedDeviceActivity;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be negative");
				}
				if (value > 0 && InputUser.s_GlobalState.onUnpairedDeviceUsed.length > 0)
				{
					InputUser.HookIntoEvents();
				}
				else if (value == 0)
				{
					InputUser.UnhookFromDeviceStateChange();
				}
				InputUser.s_GlobalState.listenForUnpairedDeviceActivity = value;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0003FEC8 File Offset: 0x0003E0C8
		public override string ToString()
		{
			if (!this.valid)
			{
				return string.Format("<Invalid> (id: {0})", this.m_Id);
			}
			string deviceList = string.Join<InputDevice>(",", this.pairedDevices);
			return string.Format("User #{0} (id: {1}, devices: {2}, actions: {3})", new object[] { this.index, this.m_Id, deviceList, this.actions });
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003FF44 File Offset: 0x0003E144
		public void AssociateActionsWithUser(IInputActionCollection actions)
		{
			int userIndex = this.index;
			if (InputUser.s_GlobalState.allUserData[userIndex].actions == actions)
			{
				return;
			}
			IInputActionCollection oldActions = InputUser.s_GlobalState.allUserData[userIndex].actions;
			if (oldActions != null)
			{
				oldActions.devices = null;
				oldActions.bindingMask = null;
			}
			InputUser.s_GlobalState.allUserData[userIndex].actions = actions;
			if (actions != null)
			{
				InputUser.HookIntoActionChange();
				actions.devices = new ReadOnlyArray<InputDevice>?(this.pairedDevices);
				if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null)
				{
					this.ActivateControlSchemeInternal(userIndex, InputUser.s_GlobalState.allUserData[userIndex].controlScheme.Value);
				}
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00040014 File Offset: 0x0003E214
		public InputUser.ControlSchemeChangeSyntax ActivateControlScheme(string schemeName)
		{
			if (!string.IsNullOrEmpty(schemeName))
			{
				InputControlScheme scheme;
				this.FindControlScheme(schemeName, out scheme);
				return this.ActivateControlScheme(scheme);
			}
			return this.ActivateControlScheme(default(InputControlScheme));
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0004004C File Offset: 0x0003E24C
		private bool TryFindControlScheme(string schemeName, out InputControlScheme scheme)
		{
			if (string.IsNullOrEmpty(schemeName))
			{
				scheme = default(InputControlScheme);
				return false;
			}
			if (InputUser.s_GlobalState.allUserData[this.index].actions == null)
			{
				throw new InvalidOperationException(string.Format("Cannot set control scheme '{0}' by name on user #{1} as not actions have been associated with the user yet (AssociateActionsWithUser)", schemeName, this.index));
			}
			ReadOnlyArray<InputControlScheme> controlSchemes = InputUser.s_GlobalState.allUserData[this.index].actions.controlSchemes;
			for (int i = 0; i < controlSchemes.Count; i++)
			{
				if (string.Compare(controlSchemes[i].name, schemeName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					scheme = controlSchemes[i];
					return true;
				}
			}
			scheme = default(InputControlScheme);
			return false;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00040105 File Offset: 0x0003E305
		internal void FindControlScheme(string schemeName, out InputControlScheme scheme)
		{
			if (this.TryFindControlScheme(schemeName, out scheme))
			{
				return;
			}
			throw new ArgumentException(string.Format("Cannot find control scheme '{0}' in actions '{1}'", schemeName, InputUser.s_GlobalState.allUserData[this.index].actions));
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0004013C File Offset: 0x0003E33C
		public InputUser.ControlSchemeChangeSyntax ActivateControlScheme(InputControlScheme scheme)
		{
			int userIndex = this.index;
			InputControlScheme? controlScheme = InputUser.s_GlobalState.allUserData[userIndex].controlScheme;
			if (controlScheme == null || (controlScheme != null && controlScheme.GetValueOrDefault() != scheme) || (scheme == default(InputControlScheme) && InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null))
			{
				this.ActivateControlSchemeInternal(userIndex, scheme);
				InputUser.Notify(userIndex, InputUserChange.ControlSchemeChanged, null);
			}
			return new InputUser.ControlSchemeChangeSyntax
			{
				m_UserIndex = userIndex
			};
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000401E0 File Offset: 0x0003E3E0
		private void ActivateControlSchemeInternal(int userIndex, InputControlScheme scheme)
		{
			bool isEmpty = scheme == default(InputControlScheme);
			if (isEmpty)
			{
				InputUser.s_GlobalState.allUserData[userIndex].controlScheme = null;
			}
			else
			{
				InputUser.s_GlobalState.allUserData[userIndex].controlScheme = new InputControlScheme?(scheme);
			}
			if (InputUser.s_GlobalState.allUserData[userIndex].actions != null)
			{
				if (isEmpty)
				{
					InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
					InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
					InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = default(InputControlScheme.MatchResult);
					return;
				}
				InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = new InputBinding?(new InputBinding
				{
					groups = scheme.bindingGroup
				});
				InputUser.UpdateControlSchemeMatch(userIndex, false);
				if (InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.isSuccessfulMatch)
				{
					InputUser.RemoveLostDevicesForUser(userIndex);
				}
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00040310 File Offset: 0x0003E510
		public void UnpairDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int userIndex = this.index;
			if (!this.pairedDevices.ContainsReference(device))
			{
				return;
			}
			InputUser.RemoveDeviceFromUser(userIndex, device, false);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0004034C File Offset: 0x0003E54C
		public void UnpairDevices()
		{
			int userIndex = this.index;
			InputUser.RemoveLostDevicesForUser(userIndex);
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				while (InputUser.s_GlobalState.allUserData[userIndex].deviceCount > 0)
				{
					this.UnpairDevice(InputUser.s_GlobalState.allPairedDevices[InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex + InputUser.s_GlobalState.allUserData[userIndex].deviceCount - 1]);
				}
			}
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null)
			{
				InputUser.UpdateControlSchemeMatch(userIndex, false);
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00040404 File Offset: 0x0003E604
		private static void RemoveLostDevicesForUser(int userIndex)
		{
			int lostDeviceCount = InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount;
			if (lostDeviceCount > 0)
			{
				int lostDeviceStartIndex = InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex;
				ArrayHelpers.EraseSliceWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDevices, ref InputUser.s_GlobalState.allLostDeviceCount, lostDeviceStartIndex, lostDeviceCount);
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount = 0;
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = 0;
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex > lostDeviceStartIndex)
					{
						InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
						int num = i;
						allUserData[num].lostDeviceStartIndex = allUserData[num].lostDeviceStartIndex - lostDeviceCount;
					}
				}
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000404D2 File Offset: 0x0003E6D2
		public void UnpairDevicesAndRemoveUser()
		{
			this.UnpairDevices();
			InputUser.RemoveUser(this.index);
			this.m_Id = 0U;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000404EC File Offset: 0x0003E6EC
		public static InputControlList<InputDevice> GetUnpairedInputDevices()
		{
			InputControlList<InputDevice> list = new InputControlList<InputDevice>(Allocator.Temp, 0);
			InputUser.GetUnpairedInputDevices(ref list);
			return list;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0004050C File Offset: 0x0003E70C
		public static int GetUnpairedInputDevices(ref InputControlList<InputDevice> list)
		{
			int countBefore = list.Count;
			foreach (InputDevice device in InputSystem.devices)
			{
				if (!InputUser.s_GlobalState.allPairedDevices.ContainsReference(InputUser.s_GlobalState.allPairedDeviceCount, device))
				{
					list.Add(device);
				}
			}
			return list.Count - countBefore;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0004058C File Offset: 0x0003E78C
		public static InputUser? FindUserPairedToDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int userIndex = InputUser.TryFindUserIndex(device);
			if (userIndex == -1)
			{
				return null;
			}
			return new InputUser?(InputUser.s_GlobalState.allUsers[userIndex]);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000405D4 File Offset: 0x0003E7D4
		public static InputUser? FindUserByAccount(InputUserAccountHandle platformUserAccountHandle)
		{
			if (platformUserAccountHandle == default(InputUserAccountHandle))
			{
				throw new ArgumentException("Empty platform user account handle", "platformUserAccountHandle");
			}
			int userIndex = InputUser.TryFindUserIndex(platformUserAccountHandle);
			if (userIndex == -1)
			{
				return null;
			}
			return new InputUser?(InputUser.s_GlobalState.allUsers[userIndex]);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0004062C File Offset: 0x0003E82C
		public static InputUser CreateUserWithoutPairedDevices()
		{
			int userIndex = InputUser.AddUser();
			return InputUser.s_GlobalState.allUsers[userIndex];
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00040650 File Offset: 0x0003E850
		public static InputUser PerformPairingWithDevice(InputDevice device, InputUser user = default(InputUser), InputUserPairingOptions options = InputUserPairingOptions.None)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (user != default(InputUser) && !user.valid)
			{
				throw new ArgumentException("Invalid user", "user");
			}
			int userIndex;
			if (user == default(InputUser))
			{
				userIndex = InputUser.AddUser();
			}
			else
			{
				userIndex = user.index;
				if ((options & InputUserPairingOptions.UnpairCurrentDevicesFromUser) != InputUserPairingOptions.None)
				{
					user.UnpairDevices();
				}
				if (user.pairedDevices.ContainsReference(device))
				{
					if ((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) != InputUserPairingOptions.None)
					{
						InputUser.InitiateUserAccountSelection(userIndex, device, options);
					}
					return user;
				}
			}
			if (!InputUser.InitiateUserAccountSelection(userIndex, device, options))
			{
				InputUser.AddDeviceToUser(userIndex, device, false, false);
			}
			return InputUser.s_GlobalState.allUsers[userIndex];
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00040704 File Offset: 0x0003E904
		private static bool InitiateUserAccountSelection(int userIndex, InputDevice device, InputUserPairingOptions options)
		{
			long queryUserAccountResult = (((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) == InputUserPairingOptions.None) ? InputUser.UpdatePlatformUserAccount(userIndex, device) : 0L);
			if (((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) != InputUserPairingOptions.None || (queryUserAccountResult != -1L && (queryUserAccountResult & 2L) == 0L && (options & InputUserPairingOptions.ForceNoPlatformUserAccountSelection) == InputUserPairingOptions.None)) && InputUser.InitiateUserAccountSelectionAtPlatformLevel(device))
			{
				InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
				allUserData[userIndex].flags = allUserData[userIndex].flags | InputUser.UserFlags.UserAccountSelectionInProgress;
				InputUser.s_GlobalState.ongoingAccountSelections.Append(new InputUser.OngoingAccountSelection
				{
					device = device,
					userId = InputUser.s_GlobalState.allUsers[userIndex].id
				});
				InputUser.HookIntoDeviceChange();
				InputUser.Notify(userIndex, InputUserChange.AccountSelectionInProgress, device);
				return true;
			}
			return false;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000407A6 File Offset: 0x0003E9A6
		public bool Equals(InputUser other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000407B6 File Offset: 0x0003E9B6
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputUser && this.Equals((InputUser)obj);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003FBF3 File Offset: 0x0003DDF3
		public override int GetHashCode()
		{
			return (int)this.m_Id;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000407A6 File Offset: 0x0003E9A6
		public static bool operator ==(InputUser left, InputUser right)
		{
			return left.m_Id == right.m_Id;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000407D3 File Offset: 0x0003E9D3
		public static bool operator !=(InputUser left, InputUser right)
		{
			return left.m_Id != right.m_Id;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000407E8 File Offset: 0x0003E9E8
		private static int AddUser()
		{
			uint num = InputUser.s_GlobalState.lastUserId + 1U;
			InputUser.s_GlobalState.lastUserId = num;
			uint id = num;
			int userCount = InputUser.s_GlobalState.allUserCount;
			ArrayHelpers.AppendWithCapacity<InputUser>(ref InputUser.s_GlobalState.allUsers, ref userCount, new InputUser
			{
				m_Id = id
			}, 10);
			int num2 = ArrayHelpers.AppendWithCapacity<InputUser.UserData>(ref InputUser.s_GlobalState.allUserData, ref InputUser.s_GlobalState.allUserCount, default(InputUser.UserData), 10);
			InputUser.Notify(num2, InputUserChange.Added, null);
			return num2;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00040868 File Offset: 0x0003EA68
		private static void RemoveUser(int userIndex)
		{
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null && InputUser.s_GlobalState.allUserData[userIndex].actions != null)
			{
				InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
			}
			InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			InputUser.RemoveLostDevicesForUser(userIndex);
			for (int i = 0; i < InputUser.s_GlobalState.ongoingAccountSelections.length; i++)
			{
				if (InputUser.s_GlobalState.ongoingAccountSelections[i].userId == InputUser.s_GlobalState.allUsers[userIndex].id)
				{
					InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(i);
					i--;
				}
			}
			InputUser.Notify(userIndex, InputUserChange.Removed, null);
			int userCount = InputUser.s_GlobalState.allUserCount;
			InputUser.s_GlobalState.allUsers.EraseAtWithCapacity(ref userCount, userIndex);
			InputUser.s_GlobalState.allUserData.EraseAtWithCapacity(ref InputUser.s_GlobalState.allUserCount, userIndex);
			if (InputUser.s_GlobalState.allUserCount == 0)
			{
				InputUser.UnhookFromDeviceChange();
				InputUser.UnhookFromActionChange();
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0004099C File Offset: 0x0003EB9C
		private static void Notify(int userIndex, InputUserChange change, InputDevice device)
		{
			if (InputUser.s_GlobalState.onChange.length == 0)
			{
				return;
			}
			InputUser.s_GlobalState.onChange.LockForChanges();
			for (int i = 0; i < InputUser.s_GlobalState.onChange.length; i++)
			{
				try
				{
					InputUser.s_GlobalState.onChange[i](InputUser.s_GlobalState.allUsers[userIndex], change, device);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onChange' callbacks");
					Debug.LogException(ex);
				}
			}
			InputUser.s_GlobalState.onChange.UnlockForChanges();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00040A50 File Offset: 0x0003EC50
		private static int TryFindUserIndex(uint userId)
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				if (InputUser.s_GlobalState.allUsers[i].m_Id == userId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00040A90 File Offset: 0x0003EC90
		private static int TryFindUserIndex(InputUserAccountHandle platformHandle)
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				InputUserAccountHandle? platformUserAccountHandle = InputUser.s_GlobalState.allUserData[i].platformUserAccountHandle;
				if (platformUserAccountHandle != null && (platformUserAccountHandle == null || platformUserAccountHandle.GetValueOrDefault() == platformHandle))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00040AF4 File Offset: 0x0003ECF4
		private static int TryFindUserIndex(InputDevice device)
		{
			int indexOfDevice = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount);
			if (indexOfDevice == -1)
			{
				return -1;
			}
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				int startIndex = InputUser.s_GlobalState.allUserData[i].deviceStartIndex;
				if (startIndex <= indexOfDevice && indexOfDevice < startIndex + InputUser.s_GlobalState.allUserData[i].deviceCount)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00040B70 File Offset: 0x0003ED70
		private static void AddDeviceToUser(int userIndex, InputDevice device, bool asLostDevice = false, bool dontUpdateControlScheme = false)
		{
			int deviceCount = (asLostDevice ? InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount : InputUser.s_GlobalState.allUserData[userIndex].deviceCount);
			int deviceStartIndex = (asLostDevice ? InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex);
			InputUser.s_GlobalState.pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion + 1;
			if (deviceCount > 0)
			{
				ArrayHelpers.MoveSlice<InputDevice>(asLostDevice ? InputUser.s_GlobalState.allLostDevices : InputUser.s_GlobalState.allPairedDevices, deviceStartIndex, asLostDevice ? (InputUser.s_GlobalState.allLostDeviceCount - deviceCount) : (InputUser.s_GlobalState.allPairedDeviceCount - deviceCount), deviceCount);
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (i != userIndex && (asLostDevice ? InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[i].deviceStartIndex) > deviceStartIndex)
					{
						if (asLostDevice)
						{
							InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
							int num = i;
							allUserData[num].lostDeviceStartIndex = allUserData[num].lostDeviceStartIndex - deviceCount;
						}
						else
						{
							InputUser.UserData[] allUserData2 = InputUser.s_GlobalState.allUserData;
							int num2 = i;
							allUserData2[num2].deviceStartIndex = allUserData2[num2].deviceStartIndex - deviceCount;
						}
					}
				}
			}
			if (asLostDevice)
			{
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = InputUser.s_GlobalState.allLostDeviceCount - deviceCount;
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDevices, ref InputUser.s_GlobalState.allLostDeviceCount, device, 10);
				InputUser.UserData[] allUserData3 = InputUser.s_GlobalState.allUserData;
				allUserData3[userIndex].lostDeviceCount = allUserData3[userIndex].lostDeviceCount + 1;
			}
			else
			{
				InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex = InputUser.s_GlobalState.allPairedDeviceCount - deviceCount;
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allPairedDevices, ref InputUser.s_GlobalState.allPairedDeviceCount, device, 10);
				InputUser.UserData[] allUserData4 = InputUser.s_GlobalState.allUserData;
				allUserData4[userIndex].deviceCount = allUserData4[userIndex].deviceCount + 1;
				IInputActionCollection actions = InputUser.s_GlobalState.allUserData[userIndex].actions;
				if (actions != null)
				{
					actions.devices = new ReadOnlyArray<InputDevice>?(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices);
					if (!dontUpdateControlScheme && InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null)
					{
						InputUser.UpdateControlSchemeMatch(userIndex, false);
					}
				}
			}
			InputUser.HookIntoDeviceChange();
			InputUser.Notify(userIndex, asLostDevice ? InputUserChange.DeviceLost : InputUserChange.DevicePaired, device);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00040DE0 File Offset: 0x0003EFE0
		private static void RemoveDeviceFromUser(int userIndex, InputDevice device, bool asLostDevice = false)
		{
			int deviceIndex = (asLostDevice ? InputUser.s_GlobalState.allLostDevices.IndexOfReference(device, InputUser.s_GlobalState.allLostDeviceCount) : InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex, InputUser.s_GlobalState.allUserData[userIndex].deviceCount));
			if (deviceIndex == -1)
			{
				return;
			}
			if (asLostDevice)
			{
				InputUser.s_GlobalState.allLostDevices.EraseAtWithCapacity(ref InputUser.s_GlobalState.allLostDeviceCount, deviceIndex);
				InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
				allUserData[userIndex].lostDeviceCount = allUserData[userIndex].lostDeviceCount - 1;
			}
			else
			{
				InputUser.s_GlobalState.pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion + 1;
				InputUser.s_GlobalState.allPairedDevices.EraseAtWithCapacity(ref InputUser.s_GlobalState.allPairedDeviceCount, deviceIndex);
				InputUser.UserData[] allUserData2 = InputUser.s_GlobalState.allUserData;
				allUserData2[userIndex].deviceCount = allUserData2[userIndex].deviceCount - 1;
			}
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				if ((asLostDevice ? InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[i].deviceStartIndex) > deviceIndex)
				{
					if (asLostDevice)
					{
						InputUser.UserData[] allUserData3 = InputUser.s_GlobalState.allUserData;
						int num = i;
						allUserData3[num].lostDeviceStartIndex = allUserData3[num].lostDeviceStartIndex - 1;
					}
					else
					{
						InputUser.UserData[] allUserData4 = InputUser.s_GlobalState.allUserData;
						int num2 = i;
						allUserData4[num2].deviceStartIndex = allUserData4[num2].deviceStartIndex - 1;
					}
				}
			}
			if (!asLostDevice)
			{
				for (int j = 0; j < InputUser.s_GlobalState.ongoingAccountSelections.length; j++)
				{
					if (InputUser.s_GlobalState.ongoingAccountSelections[j].userId == InputUser.s_GlobalState.allUsers[userIndex].id && InputUser.s_GlobalState.ongoingAccountSelections[j].device == device)
					{
						InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(j);
						j--;
					}
				}
				IInputActionCollection actions = InputUser.s_GlobalState.allUserData[userIndex].actions;
				if (actions != null)
				{
					actions.devices = new ReadOnlyArray<InputDevice>?(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices);
					if (InputUser.s_GlobalState.allUsers[userIndex].controlScheme != null)
					{
						InputUser.UpdateControlSchemeMatch(userIndex, false);
					}
				}
				InputUser.Notify(userIndex, InputUserChange.DeviceUnpaired, device);
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00041028 File Offset: 0x0003F228
		private static void UpdateControlSchemeMatch(int userIndex, bool autoPairMissing = false)
		{
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme == null)
			{
				return;
			}
			InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			InputControlScheme.MatchResult matchResult = default(InputControlScheme.MatchResult);
			try
			{
				InputControlScheme scheme = InputUser.s_GlobalState.allUserData[userIndex].controlScheme.Value;
				if (scheme.deviceRequirements.Count > 0)
				{
					InputControlList<InputDevice> availableDevices = new InputControlList<InputDevice>(Allocator.Temp, 0);
					try
					{
						availableDevices.AddSlice<ReadOnlyArray<InputDevice>>(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices, -1, -1, 0);
						if (autoPairMissing)
						{
							int startIndex = availableDevices.Count;
							int count = InputUser.GetUnpairedInputDevices(ref availableDevices);
							if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle != null)
							{
								availableDevices.Sort<InputUser.CompareDevicesByUserAccount>(startIndex, count, new InputUser.CompareDevicesByUserAccount
								{
									platformUserAccountHandle = InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle.Value
								});
							}
						}
						matchResult = scheme.PickDevicesFrom<InputControlList<InputDevice>>(availableDevices, null);
						if (matchResult.isSuccessfulMatch && autoPairMissing)
						{
							InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
							foreach (InputDevice device in matchResult.devices)
							{
								if (!InputUser.s_GlobalState.allUsers[userIndex].pairedDevices.ContainsReference(device))
								{
									InputUser.AddDeviceToUser(userIndex, device, false, true);
								}
							}
						}
					}
					finally
					{
						availableDevices.Dispose();
					}
				}
				InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
			}
			catch (Exception)
			{
				matchResult.Dispose();
				throw;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00041234 File Offset: 0x0003F434
		private static long UpdatePlatformUserAccount(int userIndex, InputDevice device)
		{
			InputUserAccountHandle? platformUserAccountHandle;
			string platformUserAccountName;
			string platformUserAccountId;
			long queryResult = InputUser.QueryPairedPlatformUserAccount(device, out platformUserAccountHandle, out platformUserAccountName, out platformUserAccountId);
			if (queryResult == -1L)
			{
				if ((InputUser.s_GlobalState.allUserData[userIndex].flags & InputUser.UserFlags.UserAccountSelectionInProgress) != (InputUser.UserFlags)0)
				{
					InputUser.Notify(userIndex, InputUserChange.AccountSelectionCanceled, null);
				}
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = null;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = null;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = null;
				return queryResult;
			}
			if ((InputUser.s_GlobalState.allUserData[userIndex].flags & InputUser.UserFlags.UserAccountSelectionInProgress) != (InputUser.UserFlags)0)
			{
				if ((queryResult & 4L) == 0L)
				{
					if ((queryResult & 16L) != 0L)
					{
						InputUser.Notify(userIndex, InputUserChange.AccountSelectionCanceled, device);
					}
					else
					{
						InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
						allUserData[userIndex].flags = allUserData[userIndex].flags & ~InputUser.UserFlags.UserAccountSelectionInProgress;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = platformUserAccountHandle;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = platformUserAccountName;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = platformUserAccountId;
						InputUser.Notify(userIndex, InputUserChange.AccountSelectionComplete, device);
					}
				}
			}
			else if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle != platformUserAccountHandle || InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId != platformUserAccountId)
			{
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = platformUserAccountHandle;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = platformUserAccountName;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = platformUserAccountId;
				InputUser.Notify(userIndex, InputUserChange.AccountChanged, device);
			}
			else if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName != platformUserAccountName)
			{
				InputUser.Notify(userIndex, InputUserChange.AccountNameChanged, device);
			}
			return queryResult;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00041440 File Offset: 0x0003F640
		private static long QueryPairedPlatformUserAccount(InputDevice device, out InputUserAccountHandle? platformAccountHandle, out string platformAccountName, out string platformAccountId)
		{
			QueryPairedUserAccountCommand queryPairedUser = QueryPairedUserAccountCommand.Create();
			long result = device.ExecuteCommand<QueryPairedUserAccountCommand>(ref queryPairedUser);
			if (result == -1L)
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
				return -1L;
			}
			if ((result & 2L) != 0L)
			{
				platformAccountHandle = new InputUserAccountHandle?(new InputUserAccountHandle(device.description.interfaceName ?? "<Unknown>", queryPairedUser.handle));
				platformAccountName = queryPairedUser.name;
				platformAccountId = queryPairedUser.id;
			}
			else
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
			}
			return result;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000414C8 File Offset: 0x0003F6C8
		private static bool InitiateUserAccountSelectionAtPlatformLevel(InputDevice device)
		{
			InitiateUserAccountPairingCommand initiateUserPairing = InitiateUserAccountPairingCommand.Create();
			long num = device.ExecuteCommand<InitiateUserAccountPairingCommand>(ref initiateUserPairing);
			if (num == -2L)
			{
				throw new InvalidOperationException("User pairing already in progress");
			}
			return num == 1L;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000414F8 File Offset: 0x0003F6F8
		private static void OnActionChange(object obj, InputActionChange change)
		{
			if (change == InputActionChange.BoundControlsChanged)
			{
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUsers[i].actions == obj)
					{
						InputUser.Notify(i, InputUserChange.ControlsChanged, null);
					}
				}
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00041540 File Offset: 0x0003F740
		private static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change == InputDeviceChange.Added)
			{
				for (int deviceIndex = InputUser.FindLostDevice(device, 0); deviceIndex != -1; deviceIndex = InputUser.FindLostDevice(device, deviceIndex))
				{
					int userIndex = -1;
					for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
					{
						int deviceStartIndex = InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex;
						if (deviceStartIndex <= deviceIndex && deviceIndex < deviceStartIndex + InputUser.s_GlobalState.allUserData[i].lostDeviceCount)
						{
							userIndex = i;
							break;
						}
					}
					InputUser.RemoveDeviceFromUser(userIndex, InputUser.s_GlobalState.allLostDevices[deviceIndex], true);
					InputUser.Notify(userIndex, InputUserChange.DeviceRegained, device);
					InputUser.AddDeviceToUser(userIndex, device, false, false);
				}
				return;
			}
			if (change == InputDeviceChange.Removed)
			{
				for (int deviceIndex2 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount); deviceIndex2 != -1; deviceIndex2 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount))
				{
					int userIndex2 = -1;
					for (int j = 0; j < InputUser.s_GlobalState.allUserCount; j++)
					{
						int deviceStartIndex2 = InputUser.s_GlobalState.allUserData[j].deviceStartIndex;
						if (deviceStartIndex2 <= deviceIndex2 && deviceIndex2 < deviceStartIndex2 + InputUser.s_GlobalState.allUserData[j].deviceCount)
						{
							userIndex2 = j;
							break;
						}
					}
					InputUser.AddDeviceToUser(userIndex2, device, true, false);
					InputUser.RemoveDeviceFromUser(userIndex2, device, false);
				}
				return;
			}
			if (change != InputDeviceChange.ConfigurationChanged)
			{
				return;
			}
			bool wasOngoingAccountSelection = false;
			for (int k = 0; k < InputUser.s_GlobalState.ongoingAccountSelections.length; k++)
			{
				if (InputUser.s_GlobalState.ongoingAccountSelections[k].device == device)
				{
					InputUser inputUser = default(InputUser);
					inputUser.m_Id = InputUser.s_GlobalState.ongoingAccountSelections[k].userId;
					int userIndex3 = inputUser.index;
					if ((InputUser.UpdatePlatformUserAccount(userIndex3, device) & 4L) == 0L)
					{
						wasOngoingAccountSelection = true;
						InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(k);
						k--;
						if (!InputUser.s_GlobalState.allUsers[userIndex3].pairedDevices.ContainsReference(device))
						{
							InputUser.AddDeviceToUser(userIndex3, device, false, false);
						}
					}
				}
			}
			if (!wasOngoingAccountSelection)
			{
				int offsetNextSlice;
				for (int deviceIndex3 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount); deviceIndex3 != -1; deviceIndex3 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, offsetNextSlice, InputUser.s_GlobalState.allPairedDeviceCount - offsetNextSlice))
				{
					int userIndex4 = -1;
					for (int l = 0; l < InputUser.s_GlobalState.allUserCount; l++)
					{
						int deviceStartIndex3 = InputUser.s_GlobalState.allUserData[l].deviceStartIndex;
						if (deviceStartIndex3 <= deviceIndex3 && deviceIndex3 < deviceStartIndex3 + InputUser.s_GlobalState.allUserData[l].deviceCount)
						{
							userIndex4 = l;
							break;
						}
					}
					InputUser.UpdatePlatformUserAccount(userIndex4, device);
					offsetNextSlice = deviceIndex3 + Math.Max(1, InputUser.s_GlobalState.allUserData[userIndex4].deviceCount);
				}
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00041838 File Offset: 0x0003FA38
		private static int FindLostDevice(InputDevice device, int startIndex = 0)
		{
			int newDeviceId = device.deviceId;
			for (int i = startIndex; i < InputUser.s_GlobalState.allLostDeviceCount; i++)
			{
				InputDevice lostDevice = InputUser.s_GlobalState.allLostDevices[i];
				if (device == lostDevice || lostDevice.deviceId == newDeviceId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00041880 File Offset: 0x0003FA80
		private static void OnEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (InputUser.s_GlobalState.listenForUnpairedDeviceActivity == 0)
			{
				return;
			}
			FourCC eventType = eventPtr.type;
			if (eventType != 1398030676 && eventType != 1145852993)
			{
				return;
			}
			if (!device.enabled)
			{
				return;
			}
			if (InputUser.s_GlobalState.allPairedDevices.ContainsReference(InputUser.s_GlobalState.allPairedDeviceCount, device))
			{
				return;
			}
			if (!DelegateHelpers.InvokeCallbacksSafe_AnyCallbackReturnsTrue<InputDevice, InputEventPtr>(ref InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed, device, eventPtr, "InputUser.onPreFilterUnpairedDeviceActivity", null))
			{
				return;
			}
			foreach (InputControl control in eventPtr.EnumerateChangedControls(device, 0.0001f))
			{
				bool deviceHasBeenPaired = false;
				InputUser.s_GlobalState.onUnpairedDeviceUsed.LockForChanges();
				for (int i = 0; i < InputUser.s_GlobalState.onUnpairedDeviceUsed.length; i++)
				{
					int pairingStateVersionBefore = InputUser.s_GlobalState.pairingStateVersion;
					try
					{
						InputUser.s_GlobalState.onUnpairedDeviceUsed[i](control, eventPtr);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onUnpairedDeviceUsed' callbacks");
						Debug.LogException(ex);
					}
					if (pairingStateVersionBefore != InputUser.s_GlobalState.pairingStateVersion && InputUser.FindUserPairedToDevice(device) != null)
					{
						deviceHasBeenPaired = true;
						break;
					}
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.UnlockForChanges();
				if (deviceHasBeenPaired)
				{
					break;
				}
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00041A14 File Offset: 0x0003FC14
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<InputUser.GlobalState>(ref InputUser.s_GlobalState, delegate(ref InputUser.GlobalState state)
			{
				InputUser.s_GlobalState = state;
			}, delegate
			{
				InputUser.DisposeAndResetGlobalState();
			});
			InputUser.s_GlobalState = default(InputUser.GlobalState);
			return savedState;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00041A74 File Offset: 0x0003FC74
		private static void HookIntoActionChange()
		{
			if (InputUser.s_GlobalState.onActionChangeHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.actionChangeDelegate == null)
			{
				InputUser.s_GlobalState.actionChangeDelegate = new Action<object, InputActionChange>(InputUser.OnActionChange);
			}
			InputSystem.onActionChange += InputUser.OnActionChange;
			InputUser.s_GlobalState.onActionChangeHooked = true;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00041ACC File Offset: 0x0003FCCC
		private static void UnhookFromActionChange()
		{
			if (!InputUser.s_GlobalState.onActionChangeHooked)
			{
				return;
			}
			InputSystem.onActionChange -= InputUser.OnActionChange;
			InputUser.s_GlobalState.onActionChangeHooked = false;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		private static void HookIntoDeviceChange()
		{
			if (InputUser.s_GlobalState.onDeviceChangeHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.onDeviceChangeDelegate == null)
			{
				InputUser.s_GlobalState.onDeviceChangeDelegate = new Action<InputDevice, InputDeviceChange>(InputUser.OnDeviceChange);
			}
			InputSystem.onDeviceChange += InputUser.s_GlobalState.onDeviceChangeDelegate;
			InputUser.s_GlobalState.onDeviceChangeHooked = true;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00041B4E File Offset: 0x0003FD4E
		private static void UnhookFromDeviceChange()
		{
			if (!InputUser.s_GlobalState.onDeviceChangeHooked)
			{
				return;
			}
			InputSystem.onDeviceChange -= InputUser.s_GlobalState.onDeviceChangeDelegate;
			InputUser.s_GlobalState.onDeviceChangeHooked = false;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00041B78 File Offset: 0x0003FD78
		private static void HookIntoEvents()
		{
			if (InputUser.s_GlobalState.onEventHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.onEventDelegate == null)
			{
				InputUser.s_GlobalState.onEventDelegate = new Action<InputEventPtr, InputDevice>(InputUser.OnEvent);
			}
			InputSystem.onEvent += InputUser.s_GlobalState.onEventDelegate;
			InputUser.s_GlobalState.onEventHooked = true;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00041BD8 File Offset: 0x0003FDD8
		private static void UnhookFromDeviceStateChange()
		{
			if (!InputUser.s_GlobalState.onEventHooked)
			{
				return;
			}
			InputSystem.onEvent -= InputUser.s_GlobalState.onEventDelegate;
			InputUser.s_GlobalState.onEventHooked = false;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00041C0C File Offset: 0x0003FE0C
		private static void DisposeAndResetGlobalState()
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				InputUser.s_GlobalState.allUserData[i].controlSchemeMatch.Dispose();
			}
			uint storedLastUserId = InputUser.s_GlobalState.lastUserId;
			InputUser.s_GlobalState = default(InputUser.GlobalState);
			InputUser.s_GlobalState.lastUserId = storedLastUserId;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00041C69 File Offset: 0x0003FE69
		internal static void ResetGlobals()
		{
			InputUser.UnhookFromActionChange();
			InputUser.UnhookFromDeviceChange();
			InputUser.UnhookFromDeviceStateChange();
			InputUser.DisposeAndResetGlobalState();
		}

		// Token: 0x040005F2 RID: 1522
		public const uint InvalidId = 0U;

		// Token: 0x040005F3 RID: 1523
		private static readonly ProfilerMarker k_InputUserOnChangeMarker = new ProfilerMarker("InputUser.onChange");

		// Token: 0x040005F4 RID: 1524
		private static readonly ProfilerMarker k_InputCheckForUnpairMarker = new ProfilerMarker("InputCheckForUnpairedDeviceActivity");

		// Token: 0x040005F5 RID: 1525
		private uint m_Id;

		// Token: 0x040005F6 RID: 1526
		private static InputUser.GlobalState s_GlobalState;

		// Token: 0x02000108 RID: 264
		public struct ControlSchemeChangeSyntax
		{
			// Token: 0x06000CE3 RID: 3299 RVA: 0x00041C9F File Offset: 0x0003FE9F
			public InputUser.ControlSchemeChangeSyntax AndPairRemainingDevices()
			{
				InputUser.UpdateControlSchemeMatch(this.m_UserIndex, true);
				return this;
			}

			// Token: 0x040005F7 RID: 1527
			internal int m_UserIndex;
		}

		// Token: 0x02000109 RID: 265
		[Flags]
		internal enum UserFlags
		{
			// Token: 0x040005F9 RID: 1529
			BindToAllDevices = 1,
			// Token: 0x040005FA RID: 1530
			UserAccountSelectionInProgress = 2
		}

		// Token: 0x0200010A RID: 266
		private struct UserData
		{
			// Token: 0x040005FB RID: 1531
			public InputUserAccountHandle? platformUserAccountHandle;

			// Token: 0x040005FC RID: 1532
			public string platformUserAccountName;

			// Token: 0x040005FD RID: 1533
			public string platformUserAccountId;

			// Token: 0x040005FE RID: 1534
			public int deviceCount;

			// Token: 0x040005FF RID: 1535
			public int deviceStartIndex;

			// Token: 0x04000600 RID: 1536
			public IInputActionCollection actions;

			// Token: 0x04000601 RID: 1537
			public InputControlScheme? controlScheme;

			// Token: 0x04000602 RID: 1538
			public InputControlScheme.MatchResult controlSchemeMatch;

			// Token: 0x04000603 RID: 1539
			public int lostDeviceCount;

			// Token: 0x04000604 RID: 1540
			public int lostDeviceStartIndex;

			// Token: 0x04000605 RID: 1541
			public InputUser.UserFlags flags;
		}

		// Token: 0x0200010B RID: 267
		private struct CompareDevicesByUserAccount : IComparer<InputDevice>
		{
			// Token: 0x06000CE4 RID: 3300 RVA: 0x00041CB4 File Offset: 0x0003FEB4
			public int Compare(InputDevice x, InputDevice y)
			{
				InputUserAccountHandle? firstAccountHandle = InputUser.CompareDevicesByUserAccount.GetUserAccountHandleForDevice(x);
				InputUserAccountHandle? secondAccountHandle = InputUser.CompareDevicesByUserAccount.GetUserAccountHandleForDevice(x);
				InputUserAccountHandle? inputUserAccountHandle = firstAccountHandle;
				InputUserAccountHandle inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					inputUserAccountHandle = secondAccountHandle;
					inputUserAccountHandle2 = this.platformUserAccountHandle;
					if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
					{
						return 0;
					}
				}
				inputUserAccountHandle = firstAccountHandle;
				inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					return -1;
				}
				inputUserAccountHandle = secondAccountHandle;
				inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x00041D98 File Offset: 0x0003FF98
			private static InputUserAccountHandle? GetUserAccountHandleForDevice(InputDevice device)
			{
				return null;
			}

			// Token: 0x04000606 RID: 1542
			public InputUserAccountHandle platformUserAccountHandle;
		}

		// Token: 0x0200010C RID: 268
		private struct OngoingAccountSelection
		{
			// Token: 0x04000607 RID: 1543
			public InputDevice device;

			// Token: 0x04000608 RID: 1544
			public uint userId;
		}

		// Token: 0x0200010D RID: 269
		private struct GlobalState
		{
			// Token: 0x04000609 RID: 1545
			internal int pairingStateVersion;

			// Token: 0x0400060A RID: 1546
			internal uint lastUserId;

			// Token: 0x0400060B RID: 1547
			internal int allUserCount;

			// Token: 0x0400060C RID: 1548
			internal int allPairedDeviceCount;

			// Token: 0x0400060D RID: 1549
			internal int allLostDeviceCount;

			// Token: 0x0400060E RID: 1550
			internal InputUser[] allUsers;

			// Token: 0x0400060F RID: 1551
			internal InputUser.UserData[] allUserData;

			// Token: 0x04000610 RID: 1552
			internal InputDevice[] allPairedDevices;

			// Token: 0x04000611 RID: 1553
			internal InputDevice[] allLostDevices;

			// Token: 0x04000612 RID: 1554
			internal InlinedArray<InputUser.OngoingAccountSelection> ongoingAccountSelections;

			// Token: 0x04000613 RID: 1555
			internal CallbackArray<Action<InputUser, InputUserChange, InputDevice>> onChange;

			// Token: 0x04000614 RID: 1556
			internal CallbackArray<Action<InputControl, InputEventPtr>> onUnpairedDeviceUsed;

			// Token: 0x04000615 RID: 1557
			internal CallbackArray<Func<InputDevice, InputEventPtr, bool>> onPreFilterUnpairedDeviceUsed;

			// Token: 0x04000616 RID: 1558
			internal Action<object, InputActionChange> actionChangeDelegate;

			// Token: 0x04000617 RID: 1559
			internal Action<InputDevice, InputDeviceChange> onDeviceChangeDelegate;

			// Token: 0x04000618 RID: 1560
			internal Action<InputEventPtr, InputDevice> onEventDelegate;

			// Token: 0x04000619 RID: 1561
			internal bool onActionChangeHooked;

			// Token: 0x0400061A RID: 1562
			internal bool onDeviceChangeHooked;

			// Token: 0x0400061B RID: 1563
			internal bool onEventHooked;

			// Token: 0x0400061C RID: 1564
			internal int listenForUnpairedDeviceActivity;
		}
	}
}
