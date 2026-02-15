using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000007 RID: 7
	internal abstract class ConfigInfo
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual object CreateInstance()
		{
			if (this.Type == null)
			{
				this.Type = this.ConfigHost.GetConfigType(this.TypeName, true);
			}
			return Activator.CreateInstance(this.Type, true);
		}

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000219E File Offset: 0x0000039E
		public string StreamName
		{
			set
			{
				this.streamName = value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021A7 File Offset: 0x000003A7
		protected void ThrowException(string text, XmlReader reader)
		{
			throw new ConfigurationErrorsException(text, reader);
		}

		// Token: 0x06000012 RID: 18
		public abstract void ReadConfig(Configuration cfg, string streamName, XmlReader reader);

		// Token: 0x06000013 RID: 19
		public abstract void ReadData(Configuration config, XmlReader reader, bool overrideAllowed);

		// Token: 0x06000014 RID: 20
		internal abstract void Merge(ConfigInfo data);

		// Token: 0x06000015 RID: 21
		internal abstract void ResetModified(Configuration config);

		// Token: 0x04000005 RID: 5
		public string Name;

		// Token: 0x04000006 RID: 6
		public string TypeName;

		// Token: 0x04000007 RID: 7
		protected Type Type;

		// Token: 0x04000008 RID: 8
		private string streamName;

		// Token: 0x04000009 RID: 9
		public ConfigInfo Parent;

		// Token: 0x0400000A RID: 10
		public IInternalConfigHost ConfigHost;
	}
}
