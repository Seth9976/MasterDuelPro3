using System;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Configuration
{
	// Token: 0x02000031 RID: 49
	internal abstract class InternalConfigurationHost : IInternalConfigHost
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000060E8 File Offset: 0x000042E8
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return null;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000060E8 File Offset: 0x000042E8
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return null;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000060EB File Offset: 0x000042EB
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return configPath;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000060F0 File Offset: 0x000042F0
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				type = Type.GetType(typeName + ",System");
			}
			if (type == null && throwOnError)
			{
				throw new ConfigurationErrorsException("Type '" + typeName + "' not found.");
			}
			return type;
		}

		// Token: 0x06000155 RID: 341
		public abstract string GetStreamName(string configPath);

		// Token: 0x06000156 RID: 342
		public abstract void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams);

		// Token: 0x06000157 RID: 343 RVA: 0x00006140 File Offset: 0x00004340
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			if (allowDefinition != ConfigurationAllowDefinition.MachineOnly)
			{
				return allowDefinition != ConfigurationAllowDefinition.MachineToApplication || configPath == "machine" || configPath == "exe";
			}
			return configPath == "machine";
		}

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_app_config();

		// Token: 0x0600015A RID: 346 RVA: 0x00006178 File Offset: 0x00004378
		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (string.CompareOrdinal(streamName, RuntimeEnvironment.SystemConfigurationFile) == 0)
			{
				string bundled_machine_config = InternalConfigurationHost.get_bundled_machine_config();
				if (bundled_machine_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_machine_config));
				}
			}
			if (string.CompareOrdinal(streamName, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) == 0)
			{
				string bundled_app_config = InternalConfigurationHost.get_bundled_app_config();
				if (bundled_app_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_app_config));
				}
			}
			if (!File.Exists(streamName))
			{
				return null;
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}
	}
}
