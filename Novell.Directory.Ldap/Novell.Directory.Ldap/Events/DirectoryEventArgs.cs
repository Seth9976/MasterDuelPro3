using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000BA RID: 186
	public class DirectoryEventArgs : BaseEventArgs
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00015818 File Offset: 0x00013A18
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00015820 File Offset: 0x00013A20
		public EventClassifiers EventClassification
		{
			get
			{
				return this.eClassification;
			}
			set
			{
				this.eClassification = value;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00015829 File Offset: 0x00013A29
		public DirectoryEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification)
			: base(sourceMessage)
		{
			this.eClassification = aClassification;
		}

		// Token: 0x04000338 RID: 824
		protected EventClassifiers eClassification;
	}
}
