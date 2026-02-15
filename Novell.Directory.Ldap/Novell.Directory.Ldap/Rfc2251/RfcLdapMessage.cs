using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000081 RID: 129
	public class RfcLdapMessage : Asn1Sequence
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000136E4 File Offset: 0x000118E4
		public virtual int MessageID
		{
			get
			{
				return ((Asn1Integer)base.get_Renamed(0)).intValue();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000136F7 File Offset: 0x000118F7
		public virtual int Type
		{
			get
			{
				return base.get_Renamed(1).getIdentifier().Tag;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001370A File Offset: 0x0001190A
		public virtual Asn1Object Response
		{
			get
			{
				return base.get_Renamed(1);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00013713 File Offset: 0x00011913
		public virtual RfcControls Controls
		{
			get
			{
				if (base.size() > 2)
				{
					return (RfcControls)base.get_Renamed(2);
				}
				return null;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001372C File Offset: 0x0001192C
		public virtual string RequestDN
		{
			get
			{
				return ((RfcRequest)this.op).getRequestDN();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001373E File Offset: 0x0001193E
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00013746 File Offset: 0x00011946
		public virtual LdapMessage RequestingMessage
		{
			get
			{
				return this.requestMessage;
			}
			set
			{
				this.requestMessage = value;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00013750 File Offset: 0x00011950
		internal RfcLdapMessage(Asn1Object[] origContent, RfcRequest origRequest, string dn, string filter, bool reference)
			: base(origContent, origContent.Length)
		{
			base.set_Renamed(0, new RfcMessageID());
			RfcRequest rfcRequest = ((RfcRequest)origContent[1]).dupRequest(dn, filter, reference);
			this.op = (Asn1Object)rfcRequest;
			base.set_Renamed(1, (Asn1Object)rfcRequest);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001379F File Offset: 0x0001199F
		public RfcLdapMessage(RfcRequest op)
			: this(op, null)
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000137A9 File Offset: 0x000119A9
		public RfcLdapMessage(RfcRequest op, RfcControls controls)
			: base(3)
		{
			this.op = (Asn1Object)op;
			this.controls = controls;
			base.add(new RfcMessageID());
			base.add((Asn1Object)op);
			if (controls != null)
			{
				base.add(controls);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000137E6 File Offset: 0x000119E6
		public RfcLdapMessage(Asn1Sequence op)
			: this(op, null)
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000137F0 File Offset: 0x000119F0
		public RfcLdapMessage(Asn1Sequence op, RfcControls controls)
			: base(3)
		{
			this.op = op;
			this.controls = controls;
			base.add(new RfcMessageID());
			base.add(op);
			if (controls != null)
			{
				base.add(controls);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00013824 File Offset: 0x00011A24
		[CLSCompliant(false)]
		public RfcLdapMessage(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(1);
			Asn1Identifier identifier = asn1Tagged.getIdentifier();
			sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
			MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
			int tag = identifier.Tag;
			if (tag <= 19)
			{
				switch (tag)
				{
				case 1:
					base.set_Renamed(1, new RfcBindResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 2:
				case 3:
				case 6:
				case 8:
				case 10:
				case 12:
				case 14:
					break;
				case 4:
					base.set_Renamed(1, new RfcSearchResultEntry(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 5:
					base.set_Renamed(1, new RfcSearchResultDone(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 7:
					base.set_Renamed(1, new RfcModifyResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 9:
					base.set_Renamed(1, new RfcAddResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 11:
					base.set_Renamed(1, new RfcDelResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 13:
					base.set_Renamed(1, new RfcModifyDNResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				case 15:
					base.set_Renamed(1, new RfcCompareResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				default:
					if (tag == 19)
					{
						base.set_Renamed(1, new RfcSearchResultReference(dec, memoryStream, array.Length));
						goto IL_01A6;
					}
					break;
				}
			}
			else
			{
				if (tag == 24)
				{
					base.set_Renamed(1, new RfcExtendedResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				}
				if (tag == 25)
				{
					base.set_Renamed(1, new RfcIntermediateResponse(dec, memoryStream, array.Length));
					goto IL_01A6;
				}
			}
			throw new SystemException("RfcLdapMessage: Invalid tag: " + identifier.Tag.ToString());
			IL_01A6:
			if (base.size() > 2)
			{
				array = ((Asn1OctetString)((Asn1Tagged)base.get_Renamed(2)).taggedValue()).byteValue();
				memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
				base.set_Renamed(2, new RfcControls(dec, memoryStream, array.Length));
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00013A19 File Offset: 0x00011C19
		public RfcRequest getRequest()
		{
			return (RfcRequest)base.get_Renamed(1);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00013A27 File Offset: 0x00011C27
		public virtual bool isRequest()
		{
			return base.get_Renamed(1) is RfcRequest;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00013A38 File Offset: 0x00011C38
		public object dupMessage(string dn, string filter, bool reference)
		{
			if (this.op == null)
			{
				throw new LdapException("DUP_ERROR", 82, null);
			}
			return new RfcLdapMessage(base.toArray(), (RfcRequest)base.get_Renamed(1), dn, filter, reference);
		}

		// Token: 0x0400027D RID: 637
		private Asn1Object op;

		// Token: 0x0400027E RID: 638
		private RfcControls controls;

		// Token: 0x0400027F RID: 639
		private LdapMessage requestMessage;
	}
}
