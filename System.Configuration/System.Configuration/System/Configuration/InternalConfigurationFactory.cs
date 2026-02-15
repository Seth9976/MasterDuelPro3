using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200002F RID: 47
	internal class InternalConfigurationFactory : IInternalConfigConfigurationFactory
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00006078 File Offset: 0x00004278
		public Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			InternalConfigurationSystem internalConfigurationSystem = new InternalConfigurationSystem();
			internalConfigurationSystem.Init(typeConfigHost, hostInitConfigurationParams);
			return new Configuration(internalConfigurationSystem, null);
		}
	}
}
