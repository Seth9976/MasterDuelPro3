using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x0200001E RID: 30
	public class X509Extension
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000083B8 File Offset: 0x000065B8
		public X509Extension(ASN1 asn1)
		{
			if (asn1.Tag != 48 || asn1.Count < 2)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			if (asn1[0].Tag != 6)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = ASN1Convert.ToOid(asn1[0]);
			this.extnCritical = asn1[1].Tag == 1 && asn1[1].Value[0] == byte.MaxValue;
			this.extnValue = asn1[asn1.Count - 1];
			if (this.extnValue.Tag == 4 && this.extnValue.Length > 0 && this.extnValue.Count == 0)
			{
				try
				{
					ASN1 asn2 = new ASN1(this.extnValue.Value);
					this.extnValue.Value = null;
					this.extnValue.Add(asn2);
				}
				catch
				{
				}
			}
			this.Decode();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000084D0 File Offset: 0x000066D0
		public X509Extension(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (extension.Value == null || extension.Value.Tag != 4 || extension.Value.Count != 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = extension.Oid;
			this.extnCritical = extension.Critical;
			this.extnValue = extension.Value;
			this.Decode();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002945 File Offset: 0x00000B45
		protected virtual void Decode()
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002945 File Offset: 0x00000B45
		protected virtual void Encode()
		{
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000854F File Offset: 0x0000674F
		public string Oid
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00008557 File Offset: 0x00006757
		public bool Critical
		{
			get
			{
				return this.extnCritical;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000855F File Offset: 0x0000675F
		public ASN1 Value
		{
			get
			{
				if (this.extnValue == null)
				{
					this.Encode();
				}
				return this.extnValue;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008578 File Offset: 0x00006778
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			X509Extension x509Extension = obj as X509Extension;
			if (x509Extension == null)
			{
				return false;
			}
			if (this.extnCritical != x509Extension.extnCritical)
			{
				return false;
			}
			if (this.extnOid != x509Extension.extnOid)
			{
				return false;
			}
			if (this.extnValue.Length != x509Extension.extnValue.Length)
			{
				return false;
			}
			for (int i = 0; i < this.extnValue.Length; i++)
			{
				if (this.extnValue[i] != x509Extension.extnValue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008608 File Offset: 0x00006808
		public override int GetHashCode()
		{
			return this.extnOid.GetHashCode();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008618 File Offset: 0x00006818
		private void WriteLine(StringBuilder sb, int n, int pos)
		{
			byte[] value = this.extnValue.Value;
			int num = pos;
			for (int i = 0; i < 8; i++)
			{
				if (i < n)
				{
					sb.Append(value[num++].ToString("X2", CultureInfo.InvariantCulture));
					sb.Append(" ");
				}
				else
				{
					sb.Append("   ");
				}
			}
			sb.Append("  ");
			num = pos;
			for (int j = 0; j < n; j++)
			{
				byte b = value[num++];
				if (b < 32)
				{
					sb.Append(".");
				}
				else
				{
					sb.Append(Convert.ToChar(b));
				}
			}
			sb.Append(Environment.NewLine);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000086D0 File Offset: 0x000068D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.extnValue.Length >> 3;
			int num2 = this.extnValue.Length - (num << 3);
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				this.WriteLine(stringBuilder, 8, num3);
				num3 += 8;
			}
			this.WriteLine(stringBuilder, num2, num3);
			return stringBuilder.ToString();
		}

		// Token: 0x04000080 RID: 128
		protected string extnOid;

		// Token: 0x04000081 RID: 129
		protected bool extnCritical;

		// Token: 0x04000082 RID: 130
		protected ASN1 extnValue;
	}
}
