using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001F RID: 31
	public class InputActionAsset : ScriptableObject, IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004360 File Offset: 0x00002560
		public bool enabled
		{
			get
			{
				using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = this.actionMaps.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.enabled)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000043BC File Offset: 0x000025BC
		public ReadOnlyArray<InputActionMap> actionMaps
		{
			get
			{
				return new ReadOnlyArray<InputActionMap>(this.m_ActionMaps);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000043C9 File Offset: 0x000025C9
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				return new ReadOnlyArray<InputControlScheme>(this.m_ControlSchemes);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000043D6 File Offset: 0x000025D6
		public IEnumerable<InputBinding> bindings
		{
			get
			{
				int numActionMaps = this.m_ActionMaps.LengthSafe<InputActionMap>();
				if (numActionMaps == 0)
				{
					yield break;
				}
				int num;
				for (int i = 0; i < numActionMaps; i = num)
				{
					InputActionMap actionMap = this.m_ActionMaps[i];
					InputBinding[] bindings = actionMap.m_Bindings;
					int numBindings = bindings.LengthSafe<InputBinding>();
					for (int j = 0; j < numBindings; j = num)
					{
						yield return bindings[j];
						num = j + 1;
					}
					bindings = null;
					num = i + 1;
				}
				yield break;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000043E6 File Offset: 0x000025E6
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000043F0 File Offset: 0x000025F0
		public InputBinding? bindingMask
		{
			get
			{
				return this.m_BindingMask;
			}
			set
			{
				if (this.m_BindingMask == value)
				{
					return;
				}
				this.m_BindingMask = value;
				this.ReResolveIfNecessary(true);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004449 File Offset: 0x00002649
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00004456 File Offset: 0x00002656
		public ReadOnlyArray<InputDevice>? devices
		{
			get
			{
				return this.m_Devices.Get();
			}
			set
			{
				if (this.m_Devices.Set(value))
				{
					this.ReResolveIfNecessary(false);
				}
			}
		}

		// Token: 0x170000AD RID: 173
		public InputAction this[string actionNameOrId]
		{
			get
			{
				InputAction inputAction = this.FindAction(actionNameOrId, false);
				if (inputAction == null)
				{
					throw new KeyNotFoundException(string.Format("Cannot find action '{0}' in '{1}'", actionNameOrId, this));
				}
				return inputAction;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000448C File Offset: 0x0000268C
		public string ToJson()
		{
			return JsonUtility.ToJson(new InputActionAsset.WriteFileJson
			{
				name = base.name,
				maps = InputActionMap.WriteFileJson.FromMaps(this.m_ActionMaps).maps,
				controlSchemes = InputControlScheme.SchemeJson.ToJson(this.m_ControlSchemes)
			}, true);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000044E4 File Offset: 0x000026E4
		public void LoadFromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			JsonUtility.FromJson<InputActionAsset.ReadFileJson>(json).ToAsset(this);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004513 File Offset: 0x00002713
		public static InputActionAsset FromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			InputActionAsset inputActionAsset = ScriptableObject.CreateInstance<InputActionAsset>();
			inputActionAsset.LoadFromJson(json);
			return inputActionAsset;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004534 File Offset: 0x00002734
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			if (actionNameOrId == null)
			{
				throw new ArgumentNullException("actionNameOrId");
			}
			if (this.m_ActionMaps != null)
			{
				int indexOfSlash = actionNameOrId.IndexOf('/');
				if (indexOfSlash == -1)
				{
					InputAction firstActionFound = null;
					for (int i = 0; i < this.m_ActionMaps.Length; i++)
					{
						InputAction action = this.m_ActionMaps[i].FindAction(actionNameOrId, false);
						if (action != null)
						{
							if (action.enabled || action.m_Id == actionNameOrId)
							{
								return action;
							}
							if (firstActionFound == null)
							{
								firstActionFound = action;
							}
						}
					}
					if (firstActionFound != null)
					{
						return firstActionFound;
					}
				}
				else
				{
					Substring mapName = new Substring(actionNameOrId, 0, indexOfSlash);
					Substring actionName = new Substring(actionNameOrId, indexOfSlash + 1);
					if (mapName.isEmpty || actionName.isEmpty)
					{
						throw new ArgumentException("Malformed action path: " + actionNameOrId, "actionNameOrId");
					}
					int j = 0;
					while (j < this.m_ActionMaps.Length)
					{
						InputActionMap map = this.m_ActionMaps[j];
						if (Substring.Compare(map.name, mapName, StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							InputAction[] actions = map.m_Actions;
							if (actions != null)
							{
								foreach (InputAction action2 in actions)
								{
									if (Substring.Compare(action2.name, actionName, StringComparison.InvariantCultureIgnoreCase) == 0)
									{
										return action2;
									}
								}
								break;
							}
							break;
						}
						else
						{
							j++;
						}
					}
				}
			}
			if (throwIfNotFound)
			{
				throw new ArgumentException(string.Format("No action '{0}' in '{1}'", actionNameOrId, this));
			}
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004684 File Offset: 0x00002884
		public int FindBinding(InputBinding mask, out InputAction action)
		{
			int numMaps = this.m_ActionMaps.LengthSafe<InputActionMap>();
			for (int i = 0; i < numMaps; i++)
			{
				int bindingIndex = this.m_ActionMaps[i].FindBinding(mask, out action);
				if (bindingIndex >= 0)
				{
					return bindingIndex;
				}
			}
			action = null;
			return -1;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000046C4 File Offset: 0x000028C4
		public InputActionMap FindActionMap(string nameOrId, bool throwIfNotFound = false)
		{
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			if (this.m_ActionMaps == null)
			{
				return null;
			}
			Guid id;
			if (nameOrId.Contains('-') && Guid.TryParse(nameOrId, out id))
			{
				for (int i = 0; i < this.m_ActionMaps.Length; i++)
				{
					InputActionMap map = this.m_ActionMaps[i];
					if (map.idDontGenerate == id)
					{
						return map;
					}
				}
			}
			for (int j = 0; j < this.m_ActionMaps.Length; j++)
			{
				InputActionMap map2 = this.m_ActionMaps[j];
				if (string.Compare(nameOrId, map2.name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return map2;
				}
			}
			if (throwIfNotFound)
			{
				throw new ArgumentException(string.Format("Cannot find action map '{0}' in '{1}'", nameOrId, this));
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004770 File Offset: 0x00002970
		public InputActionMap FindActionMap(Guid id)
		{
			if (this.m_ActionMaps == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_ActionMaps.Length; i++)
			{
				InputActionMap map = this.m_ActionMaps[i];
				if (map.idDontGenerate == id)
				{
					return map;
				}
			}
			return null;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000047B4 File Offset: 0x000029B4
		public InputAction FindAction(Guid guid)
		{
			if (this.m_ActionMaps == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_ActionMaps.Length; i++)
			{
				InputAction action = this.m_ActionMaps[i].FindAction(guid);
				if (action != null)
				{
					return action;
				}
			}
			return null;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000047F4 File Offset: 0x000029F4
		public int FindControlSchemeIndex(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (this.m_ControlSchemes == null)
			{
				return -1;
			}
			for (int i = 0; i < this.m_ControlSchemes.Length; i++)
			{
				if (string.Compare(name, this.m_ControlSchemes[i].name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004850 File Offset: 0x00002A50
		public InputControlScheme? FindControlScheme(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int index = this.FindControlSchemeIndex(name);
			if (index == -1)
			{
				return null;
			}
			return new InputControlScheme?(this.m_ControlSchemes[index]);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004898 File Offset: 0x00002A98
		public bool IsUsableWithDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int numControlSchemes = this.m_ControlSchemes.LengthSafe<InputControlScheme>();
			if (numControlSchemes > 0)
			{
				for (int i = 0; i < numControlSchemes; i++)
				{
					if (this.m_ControlSchemes[i].SupportsDevice(device))
					{
						return true;
					}
				}
			}
			else
			{
				int actionMapCount = this.m_ActionMaps.LengthSafe<InputActionMap>();
				for (int j = 0; j < actionMapCount; j++)
				{
					if (this.m_ActionMaps[j].IsUsableWithDevice(device))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004914 File Offset: 0x00002B14
		public void Enable()
		{
			foreach (InputActionMap inputActionMap in this.actionMaps)
			{
				inputActionMap.Enable();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004968 File Offset: 0x00002B68
		public void Disable()
		{
			foreach (InputActionMap inputActionMap in this.actionMaps)
			{
				inputActionMap.Disable();
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000049BC File Offset: 0x00002BBC
		public bool Contains(InputAction action)
		{
			InputActionMap map = ((action != null) ? action.actionMap : null);
			return map != null && map.asset == this;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000049E7 File Offset: 0x00002BE7
		public IEnumerator<InputAction> GetEnumerator()
		{
			if (this.m_ActionMaps == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.m_ActionMaps.Length; i = num)
			{
				ReadOnlyArray<InputAction> actions = this.m_ActionMaps[i].actions;
				int actionCount = actions.Count;
				for (int j = 0; j < actionCount; j = num)
				{
					yield return actions[j];
					num = j + 1;
				}
				actions = default(ReadOnlyArray<InputAction>);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000049F6 File Offset: 0x00002BF6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000049FE File Offset: 0x00002BFE
		internal void MarkAsDirty()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004A00 File Offset: 0x00002C00
		internal bool IsEmpty()
		{
			return this.actionMaps.Count == 0 && this.controlSchemes.Count == 0;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004A30 File Offset: 0x00002C30
		internal void OnWantToChangeSetup()
		{
			if (this.m_ActionMaps.LengthSafe<InputActionMap>() > 0)
			{
				this.m_ActionMaps[0].OnWantToChangeSetup();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004A4D File Offset: 0x00002C4D
		internal void OnSetupChanged()
		{
			this.MarkAsDirty();
			if (this.m_ActionMaps.LengthSafe<InputActionMap>() > 0)
			{
				this.m_ActionMaps[0].OnSetupChanged();
				return;
			}
			this.m_SharedStateForAllMaps = null;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004A78 File Offset: 0x00002C78
		private void ReResolveIfNecessary(bool fullResolve)
		{
			if (this.m_SharedStateForAllMaps == null)
			{
				return;
			}
			this.m_ActionMaps[0].LazyResolveBindings(fullResolve);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004A94 File Offset: 0x00002C94
		internal void ResolveBindingsIfNecessary()
		{
			if (this.m_ActionMaps.LengthSafe<InputActionMap>() > 0)
			{
				InputActionMap[] actionMaps = this.m_ActionMaps;
				int num = 0;
				while (num < actionMaps.Length && !actionMaps[num].ResolveBindingsIfNecessary())
				{
					num++;
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004ACE File Offset: 0x00002CCE
		private void OnDestroy()
		{
			this.Disable();
			if (this.m_SharedStateForAllMaps != null)
			{
				this.m_SharedStateForAllMaps.Dispose();
				this.m_SharedStateForAllMaps = null;
			}
		}

		// Token: 0x0400009B RID: 155
		public const string Extension = "inputactions";

		// Token: 0x0400009C RID: 156
		internal const string kDefaultAssetLayoutJson = "{}";

		// Token: 0x0400009D RID: 157
		[SerializeField]
		internal InputActionMap[] m_ActionMaps;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		internal InputControlScheme[] m_ControlSchemes;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		internal bool m_IsProjectWide;

		// Token: 0x040000A0 RID: 160
		[NonSerialized]
		internal InputActionState m_SharedStateForAllMaps;

		// Token: 0x040000A1 RID: 161
		[NonSerialized]
		internal InputBinding? m_BindingMask;

		// Token: 0x040000A2 RID: 162
		[NonSerialized]
		internal int m_ParameterOverridesCount;

		// Token: 0x040000A3 RID: 163
		[NonSerialized]
		internal InputActionRebindingExtensions.ParameterOverride[] m_ParameterOverrides;

		// Token: 0x040000A4 RID: 164
		[NonSerialized]
		internal InputActionMap.DeviceArray m_Devices;

		// Token: 0x02000020 RID: 32
		[Serializable]
		internal struct WriteFileJson
		{
			// Token: 0x040000A5 RID: 165
			public string name;

			// Token: 0x040000A6 RID: 166
			public InputActionMap.WriteMapJson[] maps;

			// Token: 0x040000A7 RID: 167
			public InputControlScheme.SchemeJson[] controlSchemes;
		}

		// Token: 0x02000021 RID: 33
		[Serializable]
		internal struct WriteFileJsonNoName
		{
			// Token: 0x040000A8 RID: 168
			public InputActionMap.WriteMapJson[] maps;

			// Token: 0x040000A9 RID: 169
			public InputControlScheme.SchemeJson[] controlSchemes;
		}

		// Token: 0x02000022 RID: 34
		[Serializable]
		internal struct ReadFileJson
		{
			// Token: 0x060001A1 RID: 417 RVA: 0x00004AF8 File Offset: 0x00002CF8
			public void ToAsset(InputActionAsset asset)
			{
				asset.name = this.name;
				InputActionMap.ReadFileJson readFileJson = default(InputActionMap.ReadFileJson);
				readFileJson.maps = this.maps;
				asset.m_ActionMaps = readFileJson.ToMaps();
				asset.m_ControlSchemes = InputControlScheme.SchemeJson.ToSchemes(this.controlSchemes);
				if (asset.m_ActionMaps != null)
				{
					InputActionMap[] actionMaps = asset.m_ActionMaps;
					for (int i = 0; i < actionMaps.Length; i++)
					{
						actionMaps[i].m_Asset = asset;
					}
				}
			}

			// Token: 0x040000AA RID: 170
			public string name;

			// Token: 0x040000AB RID: 171
			public InputActionMap.ReadMapJson[] maps;

			// Token: 0x040000AC RID: 172
			public InputControlScheme.SchemeJson[] controlSchemes;
		}
	}
}
