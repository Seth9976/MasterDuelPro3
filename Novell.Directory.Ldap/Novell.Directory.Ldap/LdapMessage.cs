using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000038 RID: 56
	public class LdapMessage
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000094EF File Offset: 0x000076EF
		internal virtual LdapMessage RequestingMessage
		{
			get
			{
				return this.message.RequestingMessage;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000094FC File Offset: 0x000076FC
		public virtual LdapControl[] Controls
		{
			get
			{
				LdapControl[] array = null;
				RfcControls controls = this.message.Controls;
				if (controls != null)
				{
					array = new LdapControl[controls.size()];
					for (int i = 0; i < controls.size(); i++)
					{
						RfcControl rfcControl = (RfcControl)controls.get_Renamed(i);
						string text = rfcControl.ControlType.stringValue();
						sbyte[] array2 = rfcControl.ControlValue.byteValue();
						bool flag = rfcControl.Criticality.booleanValue();
						array[i] = this.controlFactory(text, flag, array2);
					}
				}
				return array;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009577 File Offset: 0x00007777
		public virtual int MessageID
		{
			get
			{
				if (this.imsgNum == -1)
				{
					this.imsgNum = this.message.MessageID;
				}
				return this.imsgNum;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009599 File Offset: 0x00007799
		public virtual int Type
		{
			get
			{
				if (this.messageType == -1)
				{
					this.messageType = this.message.Type;
				}
				return this.messageType;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000095BB File Offset: 0x000077BB
		public virtual bool Request
		{
			get
			{
				return this.message.isRequest();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000095C8 File Offset: 0x000077C8
		internal virtual RfcLdapMessage Asn1Object
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000095D0 File Offset: 0x000077D0
		private string Name
		{
			get
			{
				switch (this.Type)
				{
				case 0:
					return "LdapBindRequest";
				case 1:
					return "LdapBindResponse";
				case 2:
					return "LdapUnbindRequest";
				case 3:
					return "LdapSearchRequest";
				case 4:
					return "LdapSearchResponse";
				case 5:
					return "LdapSearchResult";
				case 6:
					return "LdapModifyRequest";
				case 7:
					return "LdapModifyResponse";
				case 8:
					return "LdapAddRequest";
				case 9:
					return "LdapAddResponse";
				case 10:
					return "LdapDelRequest";
				case 11:
					return "LdapDelResponse";
				case 12:
					return "LdapModifyRDNRequest";
				case 13:
					return "LdapModifyRDNResponse";
				case 14:
					return "LdapCompareRequest";
				case 15:
					return "LdapCompareResponse";
				case 16:
					return "LdapAbandonRequest";
				case 19:
					return "LdapSearchResultReference";
				case 23:
					return "LdapExtendedRequest";
				case 24:
					return "LdapExtendedResponse";
				case 25:
					return "LdapIntermediateResponse";
				}
				throw new SystemException("LdapMessage: Unknown Type " + this.Type.ToString());
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009738 File Offset: 0x00007938
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00009770 File Offset: 0x00007970
		public virtual string Tag
		{
			get
			{
				if (this.stringTag != null)
				{
					return this.stringTag;
				}
				if (this.Request)
				{
					return null;
				}
				LdapMessage requestingMessage = this.RequestingMessage;
				if (requestingMessage == null)
				{
					return null;
				}
				return requestingMessage.stringTag;
			}
			set
			{
				this.stringTag = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009779 File Offset: 0x00007979
		internal LdapMessage()
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009790 File Offset: 0x00007990
		internal LdapMessage(int type, RfcRequest op, LdapControl[] controls)
		{
			this.messageType = type;
			RfcControls rfcControls = null;
			if (controls != null)
			{
				rfcControls = new RfcControls();
				for (int i = 0; i < controls.Length; i++)
				{
					rfcControls.add(controls[i].Asn1Object);
				}
			}
			this.message = new RfcLdapMessage(op, rfcControls);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000097EC File Offset: 0x000079EC
		protected internal LdapMessage(RfcLdapMessage message)
		{
			this.message = message;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009809 File Offset: 0x00007A09
		internal LdapMessage Clone(string dn, string filter, bool reference)
		{
			return new LdapMessage((RfcLdapMessage)this.message.dupMessage(dn, filter, reference));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009824 File Offset: 0x00007A24
		private LdapControl controlFactory(string oid, bool critical, sbyte[] value_Renamed)
		{
			RespControlVector registeredControls = LdapControl.RegisteredControls;
			try
			{
				Type type = registeredControls.findResponseControl(oid);
				if (type == null)
				{
					return new LdapControl(oid, critical, value_Renamed);
				}
				Type[] array = new Type[]
				{
					typeof(string),
					typeof(bool),
					typeof(sbyte[])
				};
				object[] array2 = new object[] { oid, critical, value_Renamed };
				try
				{
					ConstructorInfo constructor = type.GetConstructor(array);
					try
					{
						return (LdapControl)constructor.Invoke(array2);
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (TargetInvocationException)
					{
					}
					catch (Exception)
					{
					}
				}
				catch (MethodAccessException)
				{
				}
			}
			catch (FieldAccessException)
			{
			}
			return new LdapControl(oid, critical, value_Renamed);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009914 File Offset: 0x00007B14
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Name,
				"(",
				this.MessageID.ToString(),
				"): ",
				this.message.ToString()
			});
		}

		// Token: 0x04000127 RID: 295
		public const int BIND_REQUEST = 0;

		// Token: 0x04000128 RID: 296
		public const int BIND_RESPONSE = 1;

		// Token: 0x04000129 RID: 297
		public const int UNBIND_REQUEST = 2;

		// Token: 0x0400012A RID: 298
		public const int SEARCH_REQUEST = 3;

		// Token: 0x0400012B RID: 299
		public const int SEARCH_RESPONSE = 4;

		// Token: 0x0400012C RID: 300
		public const int SEARCH_RESULT = 5;

		// Token: 0x0400012D RID: 301
		public const int MODIFY_REQUEST = 6;

		// Token: 0x0400012E RID: 302
		public const int MODIFY_RESPONSE = 7;

		// Token: 0x0400012F RID: 303
		public const int ADD_REQUEST = 8;

		// Token: 0x04000130 RID: 304
		public const int ADD_RESPONSE = 9;

		// Token: 0x04000131 RID: 305
		public const int DEL_REQUEST = 10;

		// Token: 0x04000132 RID: 306
		public const int DEL_RESPONSE = 11;

		// Token: 0x04000133 RID: 307
		public const int MODIFY_RDN_REQUEST = 12;

		// Token: 0x04000134 RID: 308
		public const int MODIFY_RDN_RESPONSE = 13;

		// Token: 0x04000135 RID: 309
		public const int COMPARE_REQUEST = 14;

		// Token: 0x04000136 RID: 310
		public const int COMPARE_RESPONSE = 15;

		// Token: 0x04000137 RID: 311
		public const int ABANDON_REQUEST = 16;

		// Token: 0x04000138 RID: 312
		public const int SEARCH_RESULT_REFERENCE = 19;

		// Token: 0x04000139 RID: 313
		public const int EXTENDED_REQUEST = 23;

		// Token: 0x0400013A RID: 314
		public const int EXTENDED_RESPONSE = 24;

		// Token: 0x0400013B RID: 315
		public const int INTERMEDIATE_RESPONSE = 25;

		// Token: 0x0400013C RID: 316
		protected internal RfcLdapMessage message;

		// Token: 0x0400013D RID: 317
		private int imsgNum = -1;

		// Token: 0x0400013E RID: 318
		private int messageType = -1;

		// Token: 0x0400013F RID: 319
		private string stringTag;
	}
}
