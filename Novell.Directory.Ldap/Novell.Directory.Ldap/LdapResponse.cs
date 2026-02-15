using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000041 RID: 65
	public class LdapResponse : LdapMessage
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A8F7 File Offset: 0x00008AF7
		public virtual string ErrorMessage
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.LdapErrorMessage;
				}
				return ((RfcResponse)this.message.Response).getErrorMessage().stringValue();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A927 File Offset: 0x00008B27
		public virtual string MatchedDN
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.MatchedDN;
				}
				return ((RfcResponse)this.message.Response).getMatchedDN().stringValue();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A958 File Offset: 0x00008B58
		public virtual string[] Referrals
		{
			get
			{
				string[] array = null;
				RfcReferral referral = ((RfcResponse)this.message.Response).getReferral();
				if (referral == null)
				{
					array = new string[0];
				}
				else
				{
					int num = referral.size();
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						string text = ((Asn1OctetString)referral.get_Renamed(i)).stringValue();
						try
						{
							LdapUrl ldapUrl = new LdapUrl(text);
							string requestDN;
							if (ldapUrl.getDN() == null && (requestDN = base.Asn1Object.RequestingMessage.Asn1Object.RequestDN) != null)
							{
								ldapUrl.setDN(requestDN);
								text = ldapUrl.ToString();
							}
						}
						catch (UriFormatException)
						{
						}
						finally
						{
							array[i] = text;
						}
					}
				}
				return array;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public virtual int ResultCode
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.ResultCode;
				}
				if (((RfcResponse)this.message.Response) is RfcIntermediateResponse)
				{
					return 0;
				}
				return ((RfcResponse)this.message.Response).getResultCode().intValue();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000AA70 File Offset: 0x00008C70
		internal virtual LdapException ResultException
		{
			get
			{
				LdapException ex = null;
				int resultCode = this.ResultCode;
				if (resultCode != 0 && resultCode - 5 > 1)
				{
					if (resultCode == 10)
					{
						string[] referrals = this.Referrals;
						ex = new LdapReferralException("Automatic referral following not enabled", 10, this.ErrorMessage);
						((LdapReferralException)ex).setReferrals(referrals);
					}
					else
					{
						ex = new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, this.ErrorMessage, this.MatchedDN);
					}
				}
				return ex;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public override LdapControl[] Controls
		{
			get
			{
				if (this.exception != null)
				{
					return null;
				}
				return base.Controls;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000AAF2 File Offset: 0x00008CF2
		public override int MessageID
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.MessageID;
				}
				return base.MessageID;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000AB0E File Offset: 0x00008D0E
		public override int Type
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.ReplyType;
				}
				return base.Type;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000AB2A File Offset: 0x00008D2A
		internal virtual LdapException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000AB32 File Offset: 0x00008D32
		internal virtual ReferralInfo ActiveReferral
		{
			get
			{
				return this.activeReferral;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000AB3A File Offset: 0x00008D3A
		public LdapResponse(InterThreadException ex, ReferralInfo activeReferral)
		{
			this.exception = ex;
			this.activeReferral = activeReferral;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AB50 File Offset: 0x00008D50
		internal LdapResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000AB59 File Offset: 0x00008D59
		public LdapResponse(int type)
			: this(type, 0, null, null, null, null)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AB67 File Offset: 0x00008D67
		public LdapResponse(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals, LdapControl[] controls)
			: base(new RfcLdapMessage(LdapResponse.RfcResultFactory(type, resultCode, matchedDN, serverMessage, referrals)))
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AB80 File Offset: 0x00008D80
		private static Asn1Sequence RfcResultFactory(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals)
		{
			if (matchedDN == null)
			{
				matchedDN = "";
			}
			if (serverMessage == null)
			{
				serverMessage = "";
			}
			switch (type)
			{
			case 1:
				return null;
			case 2:
			case 3:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				break;
			case 4:
				return null;
			case 5:
				return new RfcSearchResultDone(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 7:
				return new RfcModifyResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 9:
				return new RfcAddResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 11:
				return new RfcDelResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 13:
				return new RfcModifyDNResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 15:
				return new RfcCompareResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			default:
				if (type == 19)
				{
					return null;
				}
				if (type == 24)
				{
					return null;
				}
				break;
			}
			throw new SystemException("Type " + type.ToString() + " Not Supported");
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		internal virtual void chkResultCode()
		{
			if (this.exception != null)
			{
				throw this.exception;
			}
			LdapException resultException = this.ResultException;
			if (resultException != null)
			{
				throw resultException;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AD00 File Offset: 0x00008F00
		internal virtual bool hasException()
		{
			return this.exception != null;
		}

		// Token: 0x04000155 RID: 341
		private InterThreadException exception;

		// Token: 0x04000156 RID: 342
		private ReferralInfo activeReferral;
	}
}
