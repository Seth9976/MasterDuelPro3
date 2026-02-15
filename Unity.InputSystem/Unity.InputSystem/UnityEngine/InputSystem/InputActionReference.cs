using System;
using System.Linq;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003E RID: 62
	public class InputActionReference : ScriptableObject
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00009AFF File Offset: 0x00007CFF
		public InputActionAsset asset
		{
			get
			{
				return this.m_Asset;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00009B07 File Offset: 0x00007D07
		public InputAction action
		{
			get
			{
				if (this.m_Action == null)
				{
					if (this.m_Asset == null)
					{
						return null;
					}
					this.m_Action = this.m_Asset.FindAction(new Guid(this.m_ActionId));
				}
				return this.m_Action;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009B44 File Offset: 0x00007D44
		public void Set(InputAction action)
		{
			if (action == null)
			{
				this.m_Asset = null;
				this.m_ActionId = null;
				return;
			}
			InputActionMap map = action.actionMap;
			if (map == null || map.asset == null)
			{
				throw new InvalidOperationException(string.Format("Action '{0}' must be part of an InputActionAsset in order to be able to create an InputActionReference for it", action));
			}
			this.SetInternal(map.asset, action);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009B9C File Offset: 0x00007D9C
		public void Set(InputActionAsset asset, string mapName, string actionName)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(mapName))
			{
				throw new ArgumentNullException("mapName");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentNullException("actionName");
			}
			InputActionMap inputActionMap = asset.FindActionMap(mapName, false);
			if (inputActionMap == null)
			{
				throw new ArgumentException(string.Format("No action map '{0}' in '{1}'", mapName, asset), "mapName");
			}
			InputAction action = inputActionMap.FindAction(actionName, false);
			if (action == null)
			{
				throw new ArgumentException(string.Format("No action '{0}' in map '{1}' of asset '{2}'", actionName, mapName, asset), "actionName");
			}
			this.SetInternal(asset, action);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009C30 File Offset: 0x00007E30
		private void SetInternal(InputActionAsset asset, InputAction action)
		{
			InputActionMap actionMap = action.actionMap;
			if (!asset.actionMaps.Contains(actionMap))
			{
				throw new ArgumentException(string.Format("Action '{0}' is not contained in asset '{1}'", action, asset), "action");
			}
			this.m_Asset = asset;
			this.m_ActionId = action.id.ToString();
			base.name = InputActionReference.GetDisplayName(action);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009C9C File Offset: 0x00007E9C
		public override string ToString()
		{
			try
			{
				InputAction action = this.action;
				return string.Concat(new string[]
				{
					this.m_Asset.name,
					":",
					action.actionMap.name,
					"/",
					action.name
				});
			}
			catch
			{
				if (this.m_Asset != null)
				{
					return this.m_Asset.name + ":" + this.m_ActionId;
				}
			}
			return base.ToString();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009D3C File Offset: 0x00007F3C
		internal static string GetDisplayName(InputAction action)
		{
			string text;
			if (action == null)
			{
				text = null;
			}
			else
			{
				InputActionMap actionMap = action.actionMap;
				text = ((actionMap != null) ? actionMap.name : null);
			}
			if (!string.IsNullOrEmpty(text))
			{
				InputActionMap actionMap2 = action.actionMap;
				return ((actionMap2 != null) ? actionMap2.name : null) + "/" + action.name;
			}
			if (action == null)
			{
				return null;
			}
			return action.name;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009D96 File Offset: 0x00007F96
		internal string ToDisplayName()
		{
			if (!string.IsNullOrEmpty(base.name))
			{
				return base.name;
			}
			return InputActionReference.GetDisplayName(this.action);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009DB7 File Offset: 0x00007FB7
		public static implicit operator InputAction(InputActionReference reference)
		{
			if (reference == null)
			{
				return null;
			}
			return reference.action;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009DC4 File Offset: 0x00007FC4
		public static InputActionReference Create(InputAction action)
		{
			if (action == null)
			{
				return null;
			}
			InputActionReference inputActionReference = ScriptableObject.CreateInstance<InputActionReference>();
			inputActionReference.Set(action);
			return inputActionReference;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009DD8 File Offset: 0x00007FD8
		internal static void ResetCachedAction()
		{
			Object[] array = Resources.FindObjectsOfTypeAll(typeof(InputActionReference));
			for (int i = 0; i < array.Length; i++)
			{
				((InputActionReference)array[i]).m_Action = null;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009E11 File Offset: 0x00008011
		public InputAction ToInputAction()
		{
			return this.action;
		}

		// Token: 0x04000162 RID: 354
		[SerializeField]
		internal InputActionAsset m_Asset;

		// Token: 0x04000163 RID: 355
		[SerializeField]
		internal string m_ActionId;

		// Token: 0x04000164 RID: 356
		[NonSerialized]
		private InputAction m_Action;
	}
}
