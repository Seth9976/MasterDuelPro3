using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000036 RID: 54
	public class BasicConstraintsExtension : X509Extension
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00008F6C File Offset: 0x0000716C
		public BasicConstraintsExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000090C8 File Offset: 0x000072C8
		protected override void Decode()
		{
			this.cA = false;
			this.pathLenConstraint = -1;
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid BasicConstraints extension");
			}
			int num = 0;
			ASN1 asn2 = asn[num++];
			if (asn2 != null && asn2.Tag == 1)
			{
				this.cA = asn2.Value[0] == byte.MaxValue;
				asn2 = asn[num++];
			}
			if (asn2 != null && asn2.Tag == 2)
			{
				this.pathLenConstraint = ASN1Convert.ToInt32(asn2);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000915C File Offset: 0x0000735C
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			if (this.cA)
			{
				asn.Add(new ASN1(1, new byte[] { byte.MaxValue }));
			}
			if (this.cA && this.pathLenConstraint >= 0)
			{
				asn.Add(ASN1Convert.FromInt32(this.pathLenConstraint));
			}
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000091D0 File Offset: 0x000073D0
		public bool CertificateAuthority
		{
			get
			{
				return this.cA;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000091D8 File Offset: 0x000073D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Subject Type=");
			stringBuilder.Append(this.cA ? "CA" : "End Entity");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("Path Length Constraint=");
			if (this.pathLenConstraint == -1)
			{
				stringBuilder.Append("None");
			}
			else
			{
				stringBuilder.Append(this.pathLenConstraint.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04000097 RID: 151
		private bool cA;

		// Token: 0x04000098 RID: 152
		private int pathLenConstraint;
	}
}
