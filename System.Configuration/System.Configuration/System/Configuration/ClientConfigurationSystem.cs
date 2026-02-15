using System;
using System.Configuration.Internal;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x02000005 RID: 5
	internal class ClientConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020BC File Offset: 0x000002BC
		private Configuration Configuration
		{
			get
			{
				if (this.cfg == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					try
					{
						this.cfg = ConfigurationManager.OpenExeConfigurationInternal(ConfigurationUserLevel.None, entryAssembly, null);
					}
					catch (Exception ex)
					{
						throw new ConfigurationErrorsException("Error Initializing the configuration system.", ex);
					}
				}
				return this.cfg;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000210C File Offset: 0x0000030C
		object IInternalConfigSystem.GetSection(string configKey)
		{
			ConfigurationSection section = this.Configuration.GetSection(configKey);
			if (section == null)
			{
				return null;
			}
			return section.GetRuntimeObject();
		}

		// Token: 0x04000003 RID: 3
		private Configuration cfg;
	}
}
