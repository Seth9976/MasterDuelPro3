using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BF RID: 959
	public abstract class RenderPipelineGlobalSettings : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00038798 File Offset: 0x00036998
		protected virtual List<IRenderPipelineGraphicsSettings> settingsList
		{
			get
			{
				Debug.LogWarning(string.Format("To be able to use {0} in your {1} you must override {2}", "IRenderPipelineGraphicsSettings", base.GetType(), "settingsList"));
				Debug.LogWarning(string.Format("Create your own '[{0}] List<{1}> m_Settings = new();' in your {2} and override {3} returning m_Settings;", new object[]
				{
					"SerializeReference",
					"IRenderPipelineGraphicsSettings",
					base.GetType(),
					"settingsList"
				}));
				return null;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00038802 File Offset: 0x00036A02
		private Dictionary<Type, int> settingsMap { get; } = new Dictionary<Type, int>();

		// Token: 0x060019F0 RID: 6640 RVA: 0x0003880C File Offset: 0x00036A0C
		private void RecreateSettingsMap()
		{
			this.settingsMap.Clear();
			bool flag = this.settingsList == null;
			if (!flag)
			{
				for (int i = 0; i < this.settingsList.Count; i++)
				{
					IRenderPipelineGraphicsSettings element = this.settingsList[i];
					bool flag2 = element == null;
					if (!flag2)
					{
						this.settingsMap.Add(element.GetType(), i);
					}
				}
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00038880 File Offset: 0x00036A80
		protected internal bool TryGet(Type type, out IRenderPipelineGraphicsSettings settings)
		{
			settings = null;
			bool flag = this.settingsList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int index;
				bool flag3 = !this.settingsMap.TryGetValue(type, out index);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					settings = this.settingsList[index];
					flag2 = settings != null;
				}
			}
			return flag2;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000388D4 File Offset: 0x00036AD4
		protected internal bool TryGetFirstSettingsImplementingInterface<TSettingsInterfaceType>(out TSettingsInterfaceType settings) where TSettingsInterfaceType : class, IRenderPipelineGraphicsSettings
		{
			settings = default(TSettingsInterfaceType);
			bool flag = this.settingsList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.settingsList.Count; i++)
				{
					TSettingsInterfaceType match = this.settingsList[i] as TSettingsInterfaceType;
					bool flag3 = match != null;
					if (flag3)
					{
						settings = match;
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00038950 File Offset: 0x00036B50
		protected internal bool GetSettingsImplementingInterface<TSettingsInterfaceType>(out List<TSettingsInterfaceType> settings) where TSettingsInterfaceType : class, IRenderPipelineGraphicsSettings
		{
			settings = new List<TSettingsInterfaceType>();
			bool flag = this.settingsList == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.settingsList.Count; i++)
				{
					TSettingsInterfaceType match = this.settingsList[i] as TSettingsInterfaceType;
					bool flag3 = match != null;
					if (flag3)
					{
						settings.Add(match);
					}
				}
				flag2 = settings.Count > 0;
			}
			return flag2;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000389D4 File Offset: 0x00036BD4
		protected internal bool Contains(Type type)
		{
			return this.settingsList != null && this.settingsMap.ContainsKey(type);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000389FD File Offset: 0x00036BFD
		public virtual void OnAfterDeserialize()
		{
			this.RecreateSettingsMap();
		}
	}
}
