using System;

namespace System.Configuration
{
	/// <summary>Encapsulates the context information that is associated with a <see cref="T:System.Configuration.ConfigurationElement" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000027 RID: 39
	public sealed class ContextInformation
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00005C9C File Offset: 0x00003E9C
		internal ContextInformation(Configuration config, object ctx)
		{
			this.ctx = ctx;
			this.config = config;
		}

		/// <summary>Gets a value specifying whether the configuration property is being evaluated at the machine configuration level.</summary>
		/// <returns>true if the configuration property is being evaluated at the machine configuration level; otherwise, false.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005CB2 File Offset: 0x00003EB2
		[MonoInternalNote("should this use HostingContext instead?")]
		public bool IsMachineLevel
		{
			get
			{
				return this.config.ConfigPath == "machine";
			}
		}

		// Token: 0x04000096 RID: 150
		private object ctx;

		// Token: 0x04000097 RID: 151
		private Configuration config;
	}
}
