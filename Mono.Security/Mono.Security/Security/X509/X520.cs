using System;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x02000023 RID: 35
	public class X520
	{
		// Token: 0x02000024 RID: 36
		public abstract class AttributeTypeAndValue
		{
			// Token: 0x0600010E RID: 270 RVA: 0x00008CD9 File Offset: 0x00006ED9
			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = byte.MaxValue;
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00008CFA File Offset: 0x00006EFA
			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			// Token: 0x1700004E RID: 78
			// (set) Token: 0x06000110 RID: 272 RVA: 0x00008D18 File Offset: 0x00006F18
			public string Value
			{
				set
				{
					if (this.attrValue != null && this.attrValue.Length > this.upperBound)
					{
						throw new FormatException(string.Format(Locale.GetText("Value length bigger than upperbound ({0})."), this.upperBound));
					}
					this.attrValue = value;
				}
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00008D68 File Offset: 0x00006F68
			internal ASN1 GetASN1(byte encoding)
			{
				byte b = encoding;
				if (b == 255)
				{
					b = this.SelectBestEncoding();
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.oid));
				if (b != 19)
				{
					if (b != 22)
					{
						if (b == 30)
						{
							asn.Add(new ASN1(30, Encoding.BigEndianUnicode.GetBytes(this.attrValue)));
						}
					}
					else
					{
						asn.Add(new ASN1(22, Encoding.ASCII.GetBytes(this.attrValue)));
					}
				}
				else
				{
					asn.Add(new ASN1(19, Encoding.ASCII.GetBytes(this.attrValue)));
				}
				return asn;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00008E10 File Offset: 0x00007010
			internal ASN1 GetASN1()
			{
				return this.GetASN1(this.encoding);
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00008E20 File Offset: 0x00007020
			private byte SelectBestEncoding()
			{
				foreach (char c in this.attrValue)
				{
					if (c == '@' || c == '_')
					{
						return 30;
					}
					if (c > '\u007f')
					{
						return 30;
					}
				}
				return 19;
			}

			// Token: 0x04000092 RID: 146
			private string oid;

			// Token: 0x04000093 RID: 147
			private string attrValue;

			// Token: 0x04000094 RID: 148
			private int upperBound;

			// Token: 0x04000095 RID: 149
			private byte encoding;
		}

		// Token: 0x02000025 RID: 37
		public class CommonName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000114 RID: 276 RVA: 0x00008E64 File Offset: 0x00007064
			public CommonName()
				: base("2.5.4.3", 64)
			{
			}
		}

		// Token: 0x02000026 RID: 38
		public class SerialNumber : X520.AttributeTypeAndValue
		{
			// Token: 0x06000115 RID: 277 RVA: 0x00008E73 File Offset: 0x00007073
			public SerialNumber()
				: base("2.5.4.5", 64, 19)
			{
			}
		}

		// Token: 0x02000027 RID: 39
		public class LocalityName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000116 RID: 278 RVA: 0x00008E84 File Offset: 0x00007084
			public LocalityName()
				: base("2.5.4.7", 128)
			{
			}
		}

		// Token: 0x02000028 RID: 40
		public class StateOrProvinceName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000117 RID: 279 RVA: 0x00008E96 File Offset: 0x00007096
			public StateOrProvinceName()
				: base("2.5.4.8", 128)
			{
			}
		}

		// Token: 0x02000029 RID: 41
		public class OrganizationName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000118 RID: 280 RVA: 0x00008EA8 File Offset: 0x000070A8
			public OrganizationName()
				: base("2.5.4.10", 64)
			{
			}
		}

		// Token: 0x0200002A RID: 42
		public class OrganizationalUnitName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000119 RID: 281 RVA: 0x00008EB7 File Offset: 0x000070B7
			public OrganizationalUnitName()
				: base("2.5.4.11", 64)
			{
			}
		}

		// Token: 0x0200002B RID: 43
		public class EmailAddress : X520.AttributeTypeAndValue
		{
			// Token: 0x0600011A RID: 282 RVA: 0x00008EC6 File Offset: 0x000070C6
			public EmailAddress()
				: base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		// Token: 0x0200002C RID: 44
		public class DomainComponent : X520.AttributeTypeAndValue
		{
			// Token: 0x0600011B RID: 283 RVA: 0x00008EDA File Offset: 0x000070DA
			public DomainComponent()
				: base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		// Token: 0x0200002D RID: 45
		public class UserId : X520.AttributeTypeAndValue
		{
			// Token: 0x0600011C RID: 284 RVA: 0x00008EEE File Offset: 0x000070EE
			public UserId()
				: base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		// Token: 0x0200002E RID: 46
		public class Oid : X520.AttributeTypeAndValue
		{
			// Token: 0x0600011D RID: 285 RVA: 0x00008F00 File Offset: 0x00007100
			public Oid(string oid)
				: base(oid, int.MaxValue)
			{
			}
		}

		// Token: 0x0200002F RID: 47
		public class Title : X520.AttributeTypeAndValue
		{
			// Token: 0x0600011E RID: 286 RVA: 0x00008F0E File Offset: 0x0000710E
			public Title()
				: base("2.5.4.12", 64)
			{
			}
		}

		// Token: 0x02000030 RID: 48
		public class CountryName : X520.AttributeTypeAndValue
		{
			// Token: 0x0600011F RID: 287 RVA: 0x00008F1D File Offset: 0x0000711D
			public CountryName()
				: base("2.5.4.6", 2, 19)
			{
			}
		}

		// Token: 0x02000031 RID: 49
		public class DnQualifier : X520.AttributeTypeAndValue
		{
			// Token: 0x06000120 RID: 288 RVA: 0x00008F2D File Offset: 0x0000712D
			public DnQualifier()
				: base("2.5.4.46", 2, 19)
			{
			}
		}

		// Token: 0x02000032 RID: 50
		public class Surname : X520.AttributeTypeAndValue
		{
			// Token: 0x06000121 RID: 289 RVA: 0x00008F3D File Offset: 0x0000713D
			public Surname()
				: base("2.5.4.4", 32768)
			{
			}
		}

		// Token: 0x02000033 RID: 51
		public class GivenName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000122 RID: 290 RVA: 0x00008F4F File Offset: 0x0000714F
			public GivenName()
				: base("2.5.4.42", 16)
			{
			}
		}

		// Token: 0x02000034 RID: 52
		public class Initial : X520.AttributeTypeAndValue
		{
			// Token: 0x06000123 RID: 291 RVA: 0x00008F5E File Offset: 0x0000715E
			public Initial()
				: base("2.5.4.43", 5)
			{
			}
		}
	}
}
