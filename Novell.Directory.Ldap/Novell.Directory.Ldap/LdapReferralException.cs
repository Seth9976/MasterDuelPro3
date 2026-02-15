using System;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003F RID: 63
	public class LdapReferralException : LdapException
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A77B File Offset: 0x0000897B
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A783 File Offset: 0x00008983
		public virtual string FailedReferral
		{
			get
			{
				return this.failedReferral;
			}
			set
			{
				this.failedReferral = value;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008FEA File Offset: 0x000071EA
		public LdapReferralException()
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A78C File Offset: 0x0000898C
		public LdapReferralException(string message)
			: base(message, 10, null)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A798 File Offset: 0x00008998
		public LdapReferralException(string message, object[] arguments)
			: base(message, arguments, 10, null)
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A7A5 File Offset: 0x000089A5
		public LdapReferralException(string message, Exception rootException)
			: base(message, 10, null, rootException)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A7B2 File Offset: 0x000089B2
		public LdapReferralException(string message, object[] arguments, Exception rootException)
			: base(message, arguments, 10, null, rootException)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A7C0 File Offset: 0x000089C0
		public LdapReferralException(string message, int resultCode, string serverMessage)
			: base(message, resultCode, serverMessage)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A7CB File Offset: 0x000089CB
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage)
			: base(message, arguments, resultCode, serverMessage)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public LdapReferralException(string message, int resultCode, string serverMessage, Exception rootException)
			: base(message, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A7E5 File Offset: 0x000089E5
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage, Exception rootException)
			: base(message, arguments, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public virtual string[] getReferrals()
		{
			return this.referrals;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A7FC File Offset: 0x000089FC
		internal virtual void setReferrals(string[] urls)
		{
			this.referrals = urls;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A808 File Offset: 0x00008A08
		public override string ToString()
		{
			string text = this.getExceptionString("LdapReferralException");
			if (this.failedReferral != null)
			{
				string text2 = ResourcesHandler.getMessage("FAILED_REFERRAL", new object[] { "LdapReferralException", this.failedReferral });
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = "LdapReferralException: Failed Referral: " + this.failedReferral;
				}
				text = text + "\n" + text2;
			}
			if (this.referrals != null)
			{
				for (int i = 0; i < this.referrals.Length; i++)
				{
					string text2 = ResourcesHandler.getMessage("REFERRAL_ITEM", new object[]
					{
						"LdapReferralException",
						this.referrals[i]
					});
					if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
					{
						text2 = "LdapReferralException: Referral: " + this.referrals[i];
					}
					text = text + "\n" + text2;
				}
			}
			return text;
		}

		// Token: 0x04000153 RID: 339
		private string failedReferral;

		// Token: 0x04000154 RID: 340
		private string[] referrals;
	}
}
