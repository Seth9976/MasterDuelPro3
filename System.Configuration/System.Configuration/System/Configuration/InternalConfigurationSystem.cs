using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000030 RID: 48
	internal class InternalConfigurationSystem : IConfigSystem
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000608D File Offset: 0x0000428D
		public void Init(Type typeConfigHost, params object[] hostInitParams)
		{
			this.hostInitParams = hostInitParams;
			this.host = (IInternalConfigHost)Activator.CreateInstance(typeConfigHost);
			this.root = new InternalConfigurationRoot();
			this.root.Init(this.host, false);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000060C4 File Offset: 0x000042C4
		public void InitForConfiguration(ref string locationConfigPath, out string parentConfigPath, out string parentLocationConfigPath)
		{
			this.host.InitForConfiguration(ref locationConfigPath, out parentConfigPath, out parentLocationConfigPath, this.root, this.hostInitParams);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000060E0 File Offset: 0x000042E0
		public IInternalConfigHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x040000A9 RID: 169
		private IInternalConfigHost host;

		// Token: 0x040000AA RID: 170
		private IInternalConfigRoot root;

		// Token: 0x040000AB RID: 171
		private object[] hostInitParams;
	}
}
