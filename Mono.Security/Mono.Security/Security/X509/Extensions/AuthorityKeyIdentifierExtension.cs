using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000035 RID: 53
	public class AuthorityKeyIdentifierExtension : X509Extension
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00008F6C File Offset: 0x0000716C
		public AuthorityKeyIdentifierExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008F78 File Offset: 0x00007178
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid AuthorityKeyIdentifier extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				ASN1 asn2 = asn[i];
				if (asn2.Tag == 128)
				{
					this.aki = asn2.Value;
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008FE0 File Offset: 0x000071E0
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			if (this.aki == null)
			{
				throw new InvalidOperationException("Invalid AuthorityKeyIdentifier extension");
			}
			asn.Add(new ASN1(128, this.aki));
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009038 File Offset: 0x00007238
		public byte[] Identifier
		{
			get
			{
				if (this.aki == null)
				{
					return null;
				}
				return (byte[])this.aki.Clone();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009054 File Offset: 0x00007254
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.aki != null)
			{
				int i = 0;
				stringBuilder.Append("KeyID=");
				while (i < this.aki.Length)
				{
					stringBuilder.Append(this.aki[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000096 RID: 150
		private byte[] aki;
	}
}
