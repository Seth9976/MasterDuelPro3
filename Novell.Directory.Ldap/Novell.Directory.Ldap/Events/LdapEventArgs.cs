using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000BC RID: 188
	public class LdapEventArgs : DirectoryEventArgs
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00015851 File Offset: 0x00013A51
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00015859 File Offset: 0x00013A59
		public LdapEventType EventType
		{
			get
			{
				return this.eType;
			}
			set
			{
				this.eType = value;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015862 File Offset: 0x00013A62
		public LdapEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, aClassification)
		{
			this.eType = aType;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015874 File Offset: 0x00013A74
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.AppendFormat("{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.eType);
			stringBuilder.AppendFormat("(EventInformation:{0})", this.ldap_message);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400033A RID: 826
		protected LdapEventType eType;
	}
}
