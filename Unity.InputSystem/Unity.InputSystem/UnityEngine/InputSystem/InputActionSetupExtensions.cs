using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003F RID: 63
	public static class InputActionSetupExtensions
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00009E1C File Offset: 0x0000801C
		public static InputActionMap AddActionMap(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (asset.FindActionMap(name, false) != null)
			{
				throw new InvalidOperationException("An action map called '" + name + "' already exists in the asset");
			}
			InputActionMap map = new InputActionMap(name);
			map.GenerateId();
			asset.AddActionMap(map);
			return map;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009E88 File Offset: 0x00008088
		public static void AddActionMap(this InputActionAsset asset, InputActionMap map)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(map.name))
			{
				throw new InvalidOperationException("Maps added to an input action asset must be named");
			}
			if (map.asset != null)
			{
				throw new InvalidOperationException(string.Format("Cannot add map '{0}' to asset '{1}' as it has already been added to asset '{2}'", map, asset, map.asset));
			}
			if (asset.FindActionMap(map.name, false) != null)
			{
				throw new InvalidOperationException("An action map called '" + map.name + "' already exists in the asset");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			ArrayHelpers.Append<InputActionMap>(ref asset.m_ActionMaps, map);
			map.m_Asset = asset;
			asset.OnSetupChanged();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009F48 File Offset: 0x00008148
		public static void RemoveActionMap(this InputActionAsset asset, InputActionMap map)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			if (map.m_Asset != asset)
			{
				return;
			}
			ArrayHelpers.Erase<InputActionMap>(ref asset.m_ActionMaps, map);
			map.m_Asset = null;
			asset.OnSetupChanged();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009FAC File Offset: 0x000081AC
		public static void RemoveActionMap(this InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			InputActionMap map = asset.FindActionMap(nameOrId, false);
			if (map != null)
			{
				asset.RemoveActionMap(map);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009FF0 File Offset: 0x000081F0
		public static InputAction AddAction(this InputActionMap map, string name, InputActionType type = InputActionType.Value, string binding = null, string interactions = null, string processors = null, string groups = null, string expectedControlLayout = null)
		{
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Action must have name", "name");
			}
			map.OnWantToChangeSetup();
			if (map.FindAction(name, false) != null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Cannot add action with duplicate name '", name, "' to set '", map.name, "'" }));
			}
			InputAction action = new InputAction(name, type, null, null, null, null)
			{
				expectedControlType = expectedControlLayout
			};
			action.GenerateId();
			ArrayHelpers.Append<InputAction>(ref map.m_Actions, action);
			action.m_ActionMap = map;
			if (!string.IsNullOrEmpty(binding))
			{
				action.AddBinding(binding, interactions, processors, groups);
			}
			else
			{
				if (!string.IsNullOrEmpty(groups))
				{
					throw new ArgumentException(string.Format("No binding path was specified for action '{0}' but groups was specified ('{1}'); cannot apply groups without binding", action, groups), "groups");
				}
				action.m_Interactions = interactions;
				action.m_Processors = processors;
				map.OnSetupChanged();
			}
			return action;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public static void RemoveAction(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionMap actionMap = action.actionMap;
			if (actionMap == null)
			{
				throw new ArgumentException(string.Format("Action '{0}' does not belong to an action map; nowhere to remove from", action), "action");
			}
			actionMap.OnWantToChangeSetup();
			InputBinding[] bindingsForAction = action.bindings.ToArray();
			int index = actionMap.m_Actions.IndexOfReference(action, -1);
			ArrayHelpers.EraseAt<InputAction>(ref actionMap.m_Actions, index);
			action.m_ActionMap = null;
			action.m_SingletonActionBindings = bindingsForAction;
			int newActionMapBindingCount = actionMap.m_Bindings.Length - bindingsForAction.Length;
			if (newActionMapBindingCount == 0)
			{
				actionMap.m_Bindings = null;
			}
			else
			{
				InputBinding[] newActionMapBindings = new InputBinding[newActionMapBindingCount];
				InputBinding[] oldActionMapBindings = actionMap.m_Bindings;
				int bindingIndex = 0;
				for (int i = 0; i < oldActionMapBindings.Length; i++)
				{
					InputBinding binding = oldActionMapBindings[i];
					if (bindingsForAction.IndexOf((InputBinding b) => b == binding) == -1)
					{
						newActionMapBindings[bindingIndex++] = binding;
					}
				}
				actionMap.m_Bindings = newActionMapBindings;
			}
			actionMap.OnSetupChanged();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public static void RemoveAction(this InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			InputAction inputAction = asset.FindAction(nameOrId, false);
			if (inputAction == null)
			{
				return;
			}
			inputAction.RemoveAction();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A228 File Offset: 0x00008428
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, string path, string interactions = null, string processors = null, string groups = null)
		{
			return action.AddBinding(new InputBinding
			{
				path = path,
				interactions = interactions,
				processors = processors,
				groups = groups
			});
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A265 File Offset: 0x00008465
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return action.AddBinding(control.path, null, null, null);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A284 File Offset: 0x00008484
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, InputBinding binding = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			binding.action = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			int bindingIndex = InputActionSetupExtensions.AddBindingInternal(orCreateActionMap, binding, -1);
			return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, bindingIndex, null);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, string interactions = null, string groups = null, string action = null, string processors = null)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", "Binding path cannot be null");
			}
			return actionMap.AddBinding(new InputBinding
			{
				path = path,
				interactions = interactions,
				groups = groups,
				action = action,
				processors = processors
			});
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A320 File Offset: 0x00008520
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, InputAction action, string interactions = null, string groups = null)
		{
			if (action != null && action.actionMap != actionMap)
			{
				throw new ArgumentException(string.Format("Action '{0}' is not part of action map '{1}'", action, actionMap), "action");
			}
			if (action == null)
			{
				return actionMap.AddBinding(path, interactions, groups, null, null);
			}
			return actionMap.AddBinding(path, action.id, interactions, groups);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A374 File Offset: 0x00008574
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, Guid action, string interactions = null, string groups = null)
		{
			if (action == Guid.Empty)
			{
				return actionMap.AddBinding(path, interactions, groups, null, null);
			}
			return actionMap.AddBinding(path, interactions, groups, action.ToString(), null);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, InputBinding binding)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (binding.path == null)
			{
				throw new ArgumentException("Binding path cannot be null", "binding");
			}
			int bindingIndex = InputActionSetupExtensions.AddBindingInternal(actionMap, binding, -1);
			return new InputActionSetupExtensions.BindingSyntax(actionMap, bindingIndex, null);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public static InputActionSetupExtensions.CompositeSyntax AddCompositeBinding(this InputAction action, string composite, string interactions = null, string processors = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentException("Composite name cannot be null or empty", "composite");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding binding = new InputBinding
			{
				name = NameAndParameters.ParseName(composite),
				path = composite,
				interactions = interactions,
				processors = processors,
				isComposite = true,
				action = action.name
			};
			int bindingIndex = InputActionSetupExtensions.AddBindingInternal(orCreateActionMap, binding, -1);
			return new InputActionSetupExtensions.CompositeSyntax(orCreateActionMap, action, bindingIndex);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A480 File Offset: 0x00008680
		private static int AddBindingInternal(InputActionMap map, InputBinding binding, int bindingIndex = -1)
		{
			if (string.IsNullOrEmpty(binding.m_Id))
			{
				binding.GenerateId();
			}
			if (bindingIndex < 0)
			{
				bindingIndex = ArrayHelpers.Append<InputBinding>(ref map.m_Bindings, binding);
			}
			else
			{
				ArrayHelpers.InsertAt<InputBinding>(ref map.m_Bindings, bindingIndex, binding);
			}
			if (map.asset != null)
			{
				map.asset.MarkAsDirty();
			}
			if (map.m_SingletonAction != null)
			{
				map.m_SingletonAction.m_SingletonActionBindings = map.m_Bindings;
			}
			map.OnBindingModified();
			return bindingIndex;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A4FC File Offset: 0x000086FC
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, int index)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int indexOnMap = action.BindingIndexOnActionToBindingIndexOnMap(index);
			return new InputActionSetupExtensions.BindingSyntax(action.GetOrCreateActionMap(), indexOnMap, action);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A52C File Offset: 0x0000872C
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, string name)
		{
			return action.ChangeBinding(new InputBinding
			{
				name = name
			});
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A550 File Offset: 0x00008750
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputActionMap actionMap, int index)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (index < 0 || index >= actionMap.m_Bindings.LengthSafe<InputBinding>())
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return new InputActionSetupExtensions.BindingSyntax(actionMap, index, null);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A588 File Offset: 0x00008788
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this InputAction action, string id)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				m_Id = id
			});
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A5BC File Offset: 0x000087BC
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this InputAction action, Guid id)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				id = id
			});
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithGroup(this InputAction action, string group)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				groups = group
			});
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A624 File Offset: 0x00008824
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithPath(this InputAction action, string path)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				path = path
			});
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A658 File Offset: 0x00008858
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, InputBinding match)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionMap actionMap = action.GetOrCreateActionMap();
			Guid idDontGenerate = action.idDontGenerate;
			match.action = action.id.ToString();
			int bindingIndexInMap = actionMap.FindBindingRelativeToMap(match);
			if (bindingIndexInMap == -1)
			{
				match.action = action.name;
				bindingIndexInMap = actionMap.FindBindingRelativeToMap(match);
			}
			if (bindingIndexInMap == -1)
			{
				return default(InputActionSetupExtensions.BindingSyntax);
			}
			return new InputActionSetupExtensions.BindingSyntax(actionMap, bindingIndexInMap, action);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public static InputActionSetupExtensions.BindingSyntax ChangeCompositeBinding(this InputAction action, string compositeName)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(compositeName))
			{
				throw new ArgumentNullException("compositeName");
			}
			InputActionMap actionMap = action.GetOrCreateActionMap();
			InputBinding[] bindings = actionMap.m_Bindings;
			int numBindings = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < numBindings; i++)
			{
				ref InputBinding binding = ref bindings[i];
				if (binding.isComposite && binding.TriggersAction(action) && (compositeName.Equals(binding.name, StringComparison.InvariantCultureIgnoreCase) || compositeName.Equals(NameAndParameters.ParseName(binding.path), StringComparison.InvariantCultureIgnoreCase)))
				{
					return new InputActionSetupExtensions.BindingSyntax(actionMap, i, action);
				}
			}
			return default(InputActionSetupExtensions.BindingSyntax);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A77C File Offset: 0x0000897C
		public static void Rename(this InputAction action, string newName)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(newName))
			{
				throw new ArgumentNullException("newName");
			}
			if (action.name == newName)
			{
				return;
			}
			InputActionMap actionMap = action.actionMap;
			if (((actionMap != null) ? actionMap.FindAction(newName, false) : null) != null)
			{
				throw new InvalidOperationException(string.Format("Cannot rename '{0}' to '{1}' in map '{2}' as the map already contains an action with that name", action, newName, actionMap));
			}
			string oldName = action.m_Name;
			action.m_Name = newName;
			if (actionMap != null)
			{
				actionMap.ClearActionLookupTable();
			}
			if (((actionMap != null) ? actionMap.asset : null) != null && actionMap != null)
			{
				actionMap.asset.MarkAsDirty();
			}
			InputBinding[] bindings = action.GetOrCreateActionMap().m_Bindings;
			int bindingCount = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < bindingCount; i++)
			{
				if (string.Compare(bindings[i].action, oldName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].action = newName;
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A864 File Offset: 0x00008A64
		public static void AddControlScheme(this InputActionAsset asset, InputControlScheme controlScheme)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(controlScheme.name))
			{
				throw new ArgumentException("Cannot add control scheme without name to asset " + asset.name, "controlScheme");
			}
			if (asset.FindControlScheme(controlScheme.name) != null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Asset '", asset.name, "' already contains a control scheme called '", controlScheme.name, "'" }));
			}
			ArrayHelpers.Append<InputControlScheme>(ref asset.m_ControlSchemes, controlScheme);
			asset.MarkAsDirty();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A914 File Offset: 0x00008B14
		public static InputActionSetupExtensions.ControlSchemeSyntax AddControlScheme(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int index = asset.controlSchemes.Count;
			asset.AddControlScheme(new InputControlScheme(name, null, null));
			return new InputActionSetupExtensions.ControlSchemeSyntax(asset, index);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A96C File Offset: 0x00008B6C
		public static void RemoveControlScheme(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int index = asset.FindControlSchemeIndex(name);
			if (index != -1)
			{
				ArrayHelpers.EraseAt<InputControlScheme>(ref asset.m_ControlSchemes, index);
			}
			asset.MarkAsDirty();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public static InputControlScheme WithBindingGroup(this InputControlScheme scheme, string bindingGroup)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithBindingGroup(bindingGroup).Done();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public static InputControlScheme WithDevice(this InputControlScheme scheme, string controlPath, bool required)
		{
			if (required)
			{
				return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
			}
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AA24 File Offset: 0x00008C24
		public static InputControlScheme WithRequiredDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AA48 File Offset: 0x00008C48
		public static InputControlScheme WithOptionalDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AA6C File Offset: 0x00008C6C
		public static InputControlScheme OrWithRequiredDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithRequiredDevice(controlPath).Done();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000AA90 File Offset: 0x00008C90
		public static InputControlScheme OrWithOptionalDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithOptionalDevice(controlPath).Done();
		}

		// Token: 0x02000040 RID: 64
		public struct BindingSyntax
		{
			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060002BA RID: 698 RVA: 0x0000AAB4 File Offset: 0x00008CB4
			public bool valid
			{
				get
				{
					return this.m_ActionMap != null && this.m_BindingIndexInMap >= 0 && this.m_BindingIndexInMap < this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>();
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AAE1 File Offset: 0x00008CE1
			public int bindingIndex
			{
				get
				{
					if (!this.valid)
					{
						return -1;
					}
					if (this.m_Action != null)
					{
						return this.m_Action.BindingIndexOnMapToBindingIndexOnAction(this.m_BindingIndexInMap);
					}
					return this.m_BindingIndexInMap;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000AB0D File Offset: 0x00008D0D
			public InputBinding binding
			{
				get
				{
					if (!this.valid)
					{
						throw new InvalidOperationException("BindingSyntax accessor is not valid");
					}
					return this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap];
				}
			}

			// Token: 0x060002BD RID: 701 RVA: 0x0000AB38 File Offset: 0x00008D38
			internal BindingSyntax(InputActionMap map, int bindingIndexInMap, InputAction action = null)
			{
				this.m_ActionMap = map;
				this.m_BindingIndexInMap = bindingIndexInMap;
				this.m_Action = action;
			}

			// Token: 0x060002BE RID: 702 RVA: 0x0000AB50 File Offset: 0x00008D50
			public InputActionSetupExtensions.BindingSyntax WithName(string name)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].name = name;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000ABA0 File Offset: 0x00008DA0
			public InputActionSetupExtensions.BindingSyntax WithPath(string path)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].path = path;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000ABF0 File Offset: 0x00008DF0
			public InputActionSetupExtensions.BindingSyntax WithGroup(string group)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(group))
				{
					throw new ArgumentException("Group name cannot be null or empty", "group");
				}
				if (group.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Group name cannot contain separator character '{0}'", ';'), "group");
				}
				return this.WithGroups(group);
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000AC58 File Offset: 0x00008E58
			public InputActionSetupExtensions.BindingSyntax WithGroups(string groups)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(groups))
				{
					return this;
				}
				string currentGroups = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].groups;
				if (!string.IsNullOrEmpty(currentGroups))
				{
					groups = string.Join(";", new string[] { currentGroups, groups });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].groups = groups;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000ACF4 File Offset: 0x00008EF4
			public InputActionSetupExtensions.BindingSyntax WithInteraction(string interaction)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interaction))
				{
					throw new ArgumentException("Interaction cannot be null or empty", "interaction");
				}
				if (interaction.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Interaction string cannot contain separator character '{0}'", ';'), "interaction");
				}
				return this.WithInteractions(interaction);
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000AD5C File Offset: 0x00008F5C
			public InputActionSetupExtensions.BindingSyntax WithInteractions(string interactions)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interactions))
				{
					return this;
				}
				string currentInteractions = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].interactions;
				if (!string.IsNullOrEmpty(currentInteractions))
				{
					interactions = string.Join(";", new string[] { currentInteractions, interactions });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].interactions = interactions;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000ADF8 File Offset: 0x00008FF8
			public InputActionSetupExtensions.BindingSyntax WithInteraction<TInteraction>() where TInteraction : IInputInteraction
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				InternedString interactionName = InputInteraction.s_Interactions.FindNameForType(typeof(TInteraction));
				if (interactionName.IsEmpty())
				{
					throw new NotSupportedException(string.Format("Type '{0}' has not been registered as a interaction", typeof(TInteraction)));
				}
				return this.WithInteraction(interactionName);
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x0000AE5C File Offset: 0x0000905C
			public InputActionSetupExtensions.BindingSyntax WithProcessor(string processor)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processor))
				{
					throw new ArgumentException("Processor cannot be null or empty", "processor");
				}
				if (processor.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Processor string cannot contain separator character '{0}'", ';'), "processor");
				}
				return this.WithProcessors(processor);
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0000AEC4 File Offset: 0x000090C4
			public InputActionSetupExtensions.BindingSyntax WithProcessors(string processors)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processors))
				{
					return this;
				}
				string currentProcessors = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].processors;
				if (!string.IsNullOrEmpty(currentProcessors))
				{
					processors = string.Join(";", new string[] { currentProcessors, processors });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].processors = processors;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x0000AF60 File Offset: 0x00009160
			public InputActionSetupExtensions.BindingSyntax WithProcessor<TProcessor>()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				InternedString processorName = InputProcessor.s_Processors.FindNameForType(typeof(TProcessor));
				if (processorName.IsEmpty())
				{
					throw new NotSupportedException(string.Format("Type '{0}' has not been registered as a processor", typeof(TProcessor)));
				}
				return this.WithProcessor(processorName);
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000AFC4 File Offset: 0x000091C4
			public InputActionSetupExtensions.BindingSyntax Triggering(InputAction action)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new ArgumentException(string.Format("Cannot change the action a binding triggers on singleton action '{0}'", action), "action");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].action = action.name;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x0000B044 File Offset: 0x00009244
			public InputActionSetupExtensions.BindingSyntax To(InputBinding binding)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap] = binding;
				if (this.m_ActionMap.m_SingletonAction != null)
				{
					this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].action = this.m_ActionMap.m_SingletonAction.name;
				}
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x060002CA RID: 714 RVA: 0x0000B0C4 File Offset: 0x000092C4
			public InputActionSetupExtensions.BindingSyntax NextBinding()
			{
				return this.Iterate(true);
			}

			// Token: 0x060002CB RID: 715 RVA: 0x0000B0CD File Offset: 0x000092CD
			public InputActionSetupExtensions.BindingSyntax PreviousBinding()
			{
				return this.Iterate(false);
			}

			// Token: 0x060002CC RID: 716 RVA: 0x0000B0D6 File Offset: 0x000092D6
			public InputActionSetupExtensions.BindingSyntax NextPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				return this.IteratePartBinding(true, partName);
			}

			// Token: 0x060002CD RID: 717 RVA: 0x0000B0F3 File Offset: 0x000092F3
			public InputActionSetupExtensions.BindingSyntax PreviousPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				return this.IteratePartBinding(false, partName);
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000B110 File Offset: 0x00009310
			public InputActionSetupExtensions.BindingSyntax NextCompositeBinding(string compositeName = null)
			{
				return this.IterateCompositeBinding(true, compositeName);
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0000B11A File Offset: 0x0000931A
			public InputActionSetupExtensions.BindingSyntax PreviousCompositeBinding(string compositeName = null)
			{
				return this.IterateCompositeBinding(false, compositeName);
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x0000B124 File Offset: 0x00009324
			private InputActionSetupExtensions.BindingSyntax Iterate(bool next)
			{
				if (this.m_ActionMap == null)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				InputBinding[] bindings = this.m_ActionMap.m_Bindings;
				if (bindings == null)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				int index = this.m_BindingIndexInMap;
				for (;;)
				{
					index += (next ? 1 : (-1));
					if (index < 0 || index >= bindings.Length)
					{
						break;
					}
					if (this.m_Action == null || bindings[index].TriggersAction(this.m_Action))
					{
						goto IL_006C;
					}
				}
				return default(InputActionSetupExtensions.BindingSyntax);
				IL_006C:
				return new InputActionSetupExtensions.BindingSyntax(this.m_ActionMap, index, this.m_Action);
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000B1B0 File Offset: 0x000093B0
			private InputActionSetupExtensions.BindingSyntax IterateCompositeBinding(bool next, string compositeName)
			{
				InputActionSetupExtensions.BindingSyntax accessor = this.Iterate(next);
				while (accessor.valid)
				{
					if (accessor.binding.isComposite)
					{
						if (compositeName == null)
						{
							return accessor;
						}
						if (compositeName.Equals(accessor.binding.name, StringComparison.InvariantCultureIgnoreCase))
						{
							return accessor;
						}
						string name = NameAndParameters.ParseName(accessor.binding.path);
						if (compositeName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
						{
							return accessor;
						}
					}
					accessor = accessor.Iterate(next);
				}
				return default(InputActionSetupExtensions.BindingSyntax);
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0000B234 File Offset: 0x00009434
			private InputActionSetupExtensions.BindingSyntax IteratePartBinding(bool next, string partName)
			{
				if (!this.valid)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				if (this.binding.isComposite)
				{
					if (!next)
					{
						return default(InputActionSetupExtensions.BindingSyntax);
					}
				}
				else if (!this.binding.isPartOfComposite)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				InputActionSetupExtensions.BindingSyntax accessor = this.Iterate(next);
				while (accessor.valid)
				{
					if (!accessor.binding.isPartOfComposite)
					{
						return default(InputActionSetupExtensions.BindingSyntax);
					}
					if (partName.Equals(accessor.binding.name, StringComparison.InvariantCultureIgnoreCase))
					{
						return accessor;
					}
					accessor = accessor.Iterate(next);
				}
				return default(InputActionSetupExtensions.BindingSyntax);
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000B2E4 File Offset: 0x000094E4
			public void Erase()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Instance not valid");
				}
				bool isComposite = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].isComposite;
				ArrayHelpers.EraseAt<InputBinding>(ref this.m_ActionMap.m_Bindings, this.m_BindingIndexInMap);
				if (isComposite)
				{
					while (this.m_BindingIndexInMap < this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>() && this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].isPartOfComposite)
					{
						ArrayHelpers.EraseAt<InputBinding>(ref this.m_ActionMap.m_Bindings, this.m_BindingIndexInMap);
					}
				}
				this.m_Action.m_BindingsCount = this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>();
				this.m_ActionMap.OnBindingModified();
				if (this.m_ActionMap.m_SingletonAction != null)
				{
					this.m_ActionMap.m_SingletonAction.m_SingletonActionBindings = this.m_ActionMap.m_Bindings;
				}
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000B3D4 File Offset: 0x000095D4
			public InputActionSetupExtensions.BindingSyntax InsertPartBinding(string partName, string path)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				if (!this.valid)
				{
					throw new InvalidOperationException("Binding accessor is not valid");
				}
				InputBinding binding = this.binding;
				if (!binding.isPartOfComposite && !binding.isComposite)
				{
					throw new InvalidOperationException("Binding accessor must point to composite or part binding");
				}
				InputActionMap actionMap = this.m_ActionMap;
				InputBinding inputBinding = default(InputBinding);
				inputBinding.path = path;
				inputBinding.isPartOfComposite = true;
				inputBinding.name = partName;
				InputAction action = this.m_Action;
				inputBinding.action = ((action != null) ? action.name : null);
				InputActionSetupExtensions.AddBindingInternal(actionMap, inputBinding, this.m_BindingIndexInMap + 1);
				return new InputActionSetupExtensions.BindingSyntax(this.m_ActionMap, this.m_BindingIndexInMap + 1, this.m_Action);
			}

			// Token: 0x04000165 RID: 357
			private readonly InputActionMap m_ActionMap;

			// Token: 0x04000166 RID: 358
			private readonly InputAction m_Action;

			// Token: 0x04000167 RID: 359
			internal readonly int m_BindingIndexInMap;
		}

		// Token: 0x02000041 RID: 65
		public struct CompositeSyntax
		{
			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B492 File Offset: 0x00009692
			public int bindingIndex
			{
				get
				{
					if (this.m_ActionMap == null)
					{
						return -1;
					}
					if (this.m_Action != null)
					{
						return this.m_Action.BindingIndexOnMapToBindingIndexOnAction(this.m_BindingIndexInMap);
					}
					return this.m_BindingIndexInMap;
				}
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x0000B4BE File Offset: 0x000096BE
			internal CompositeSyntax(InputActionMap map, InputAction action, int compositeIndex)
			{
				this.m_Action = action;
				this.m_ActionMap = map;
				this.m_BindingIndexInMap = compositeIndex;
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000B4D8 File Offset: 0x000096D8
			public InputActionSetupExtensions.CompositeSyntax With(string name, string binding, string groups = null, string processors = null)
			{
				using (InputActionRebindingExtensions.DeferBindingResolution())
				{
					int bindingIndex;
					if (this.m_Action != null)
					{
						bindingIndex = this.m_Action.AddBinding(binding, null, processors, groups).m_BindingIndexInMap;
					}
					else
					{
						bindingIndex = this.m_ActionMap.AddBinding(binding, null, groups, null, processors).m_BindingIndexInMap;
					}
					this.m_ActionMap.m_Bindings[bindingIndex].name = name;
					this.m_ActionMap.m_Bindings[bindingIndex].isPartOfComposite = true;
				}
				return this;
			}

			// Token: 0x04000168 RID: 360
			private readonly InputAction m_Action;

			// Token: 0x04000169 RID: 361
			private readonly InputActionMap m_ActionMap;

			// Token: 0x0400016A RID: 362
			private int m_BindingIndexInMap;
		}

		// Token: 0x02000042 RID: 66
		public struct ControlSchemeSyntax
		{
			// Token: 0x060002D8 RID: 728 RVA: 0x0000B574 File Offset: 0x00009774
			internal ControlSchemeSyntax(InputActionAsset asset, int index)
			{
				this.m_Asset = asset;
				this.m_ControlSchemeIndex = index;
				this.m_ControlScheme = default(InputControlScheme);
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x0000B590 File Offset: 0x00009790
			internal ControlSchemeSyntax(InputControlScheme controlScheme)
			{
				this.m_Asset = null;
				this.m_ControlSchemeIndex = -1;
				this.m_ControlScheme = controlScheme;
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000B5A8 File Offset: 0x000097A8
			public InputActionSetupExtensions.ControlSchemeSyntax WithBindingGroup(string bindingGroup)
			{
				if (string.IsNullOrEmpty(bindingGroup))
				{
					throw new ArgumentNullException("bindingGroup");
				}
				if (this.m_Asset == null)
				{
					this.m_ControlScheme.m_BindingGroup = bindingGroup;
				}
				else
				{
					this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex].bindingGroup = bindingGroup;
				}
				return this;
			}

			// Token: 0x060002DB RID: 731 RVA: 0x0000B606 File Offset: 0x00009806
			public InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice<TDevice>() where TDevice : InputDevice
			{
				return this.WithRequiredDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000B614 File Offset: 0x00009814
			public InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice<TDevice>() where TDevice : InputDevice
			{
				return this.WithOptionalDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000B622 File Offset: 0x00009822
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice<TDevice>() where TDevice : InputDevice
			{
				return this.OrWithRequiredDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000B630 File Offset: 0x00009830
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice<TDevice>() where TDevice : InputDevice
			{
				return this.OrWithOptionalDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000B63E File Offset: 0x0000983E
			public InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.None);
				return this;
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x0000B64E File Offset: 0x0000984E
			public InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Optional);
				return this;
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000B65E File Offset: 0x0000985E
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000B66E File Offset: 0x0000986E
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Optional | InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000B680 File Offset: 0x00009880
			private string DeviceTypeToControlPath<TDevice>() where TDevice : InputDevice
			{
				string layoutName = InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice)).ToString();
				if (string.IsNullOrEmpty(layoutName))
				{
					layoutName = typeof(TDevice).Name;
				}
				return "<" + layoutName + ">";
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000B6D8 File Offset: 0x000098D8
			public InputControlScheme Done()
			{
				if (this.m_Asset != null)
				{
					return this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex];
				}
				return this.m_ControlScheme;
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000B708 File Offset: 0x00009908
			private void AddDeviceEntry(string controlPath, InputControlScheme.DeviceRequirement.Flags flags)
			{
				if (string.IsNullOrEmpty(controlPath))
				{
					throw new ArgumentNullException("controlPath");
				}
				InputControlScheme scheme = ((this.m_Asset != null) ? this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex] : this.m_ControlScheme);
				ArrayHelpers.Append<InputControlScheme.DeviceRequirement>(ref scheme.m_DeviceRequirements, new InputControlScheme.DeviceRequirement
				{
					m_ControlPath = controlPath,
					m_Flags = flags
				});
				if (this.m_Asset == null)
				{
					this.m_ControlScheme = scheme;
					return;
				}
				this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex] = scheme;
			}

			// Token: 0x0400016B RID: 363
			private readonly InputActionAsset m_Asset;

			// Token: 0x0400016C RID: 364
			private readonly int m_ControlSchemeIndex;

			// Token: 0x0400016D RID: 365
			private InputControlScheme m_ControlScheme;
		}
	}
}
