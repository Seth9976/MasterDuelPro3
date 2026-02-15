using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000CA RID: 202
	public class EdirEventArgs : DirectoryEventArgs
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000161C0 File Offset: 0x000143C0
		public EdirEventIntermediateResponse IntermediateResponse
		{
			get
			{
				if (this.ldap_message is EdirEventIntermediateResponse)
				{
					return (EdirEventIntermediateResponse)this.ldap_message;
				}
				return null;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000161DC File Offset: 0x000143DC
		public EdirEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification)
			: base(sourceMessage, aClassification)
		{
		}
	}
}
