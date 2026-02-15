using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000056 RID: 86
	public class BindProperties
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000E10F File Offset: 0x0000C30F
		public virtual int ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E117 File Offset: 0x0000C317
		public virtual string AuthenticationDN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000E11F File Offset: 0x0000C31F
		public virtual string AuthenticationMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E127 File Offset: 0x0000C327
		public virtual Hashtable SaslBindProperties
		{
			get
			{
				return this.bindProperties;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000E12F File Offset: 0x0000C32F
		public virtual object SaslCallbackHandler
		{
			get
			{
				return this.bindCallbackHandler;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E137 File Offset: 0x0000C337
		public virtual bool Anonymous
		{
			get
			{
				return this.anonymous;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E13F File Offset: 0x0000C33F
		public BindProperties(int version, string dn, string method, bool anonymous, Hashtable bindProperties, object bindCallbackHandler)
		{
			this.version = version;
			this.dn = dn;
			this.method = method;
			this.anonymous = anonymous;
			this.bindProperties = bindProperties;
			this.bindCallbackHandler = bindCallbackHandler;
		}

		// Token: 0x040001BF RID: 447
		private int version = 3;

		// Token: 0x040001C0 RID: 448
		private string dn;

		// Token: 0x040001C1 RID: 449
		private string method;

		// Token: 0x040001C2 RID: 450
		private bool anonymous;

		// Token: 0x040001C3 RID: 451
		private Hashtable bindProperties;

		// Token: 0x040001C4 RID: 452
		private object bindCallbackHandler;
	}
}
