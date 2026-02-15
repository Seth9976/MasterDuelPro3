using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000051 RID: 81
	public class RSAManaged : RSA
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000B198 File Offset: 0x00009398
		public RSAManaged()
			: this(1024)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B1A5 File Offset: 0x000093A5
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B1E0 File Offset: 0x000093E0
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B210 File Offset: 0x00009410
		private void GenerateKeyPair()
		{
			int num = this.KeySize + 1 >> 1;
			int num2 = this.KeySize - num;
			this.e = 65537U;
			do
			{
				this.p = BigInteger.GeneratePseudoPrime(num);
			}
			while (this.p % 65537U == 1U);
			for (;;)
			{
				this.q = BigInteger.GeneratePseudoPrime(num2);
				if (this.q % 65537U != 1U && this.p != this.q)
				{
					this.n = this.p * this.q;
					if (this.n.BitCount() == this.KeySize)
					{
						break;
					}
					if (this.p < this.q)
					{
						this.p = this.q;
					}
				}
			}
			BigInteger bigInteger = this.p - 1;
			BigInteger bigInteger2 = this.q - 1;
			BigInteger bigInteger3 = bigInteger * bigInteger2;
			this.d = this.e.ModInverse(bigInteger3);
			this.dp = this.d % bigInteger;
			this.dq = this.d % bigInteger2;
			this.qInv = this.q.ModInverse(this.p);
			this.keypairGenerated = true;
			this.isCRTpossible = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000B37C File Offset: 0x0000957C
		public override int KeySize
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
				}
				if (this.keypairGenerated)
				{
					int num = this.n.BitCount();
					if ((num & 7) != 0)
					{
						num += 8 - (num & 7);
					}
					return num;
				}
				return base.KeySize;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000B3CA File Offset: 0x000095CA
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("private key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = null;
			if (this.keyBlinding)
			{
				bigInteger2 = BigInteger.GenerateRandom(this.n.BitCount());
				bigInteger = bigInteger2.ModPow(this.e, this.n) * bigInteger % this.n;
			}
			BigInteger bigInteger6;
			if (this.isCRTpossible)
			{
				BigInteger bigInteger3 = bigInteger.ModPow(this.dp, this.p);
				BigInteger bigInteger4 = bigInteger.ModPow(this.dq, this.q);
				if (bigInteger4 > bigInteger3)
				{
					BigInteger bigInteger5 = this.p - (bigInteger4 - bigInteger3) * this.qInv % this.p;
					bigInteger6 = bigInteger4 + this.q * bigInteger5;
				}
				else
				{
					BigInteger bigInteger5 = (bigInteger3 - bigInteger4) * this.qInv % this.p;
					bigInteger6 = bigInteger4 + this.q * bigInteger5;
				}
			}
			else
			{
				if (this.PublicOnly)
				{
					throw new CryptographicException(Locale.GetText("Missing private key to decrypt value."));
				}
				bigInteger6 = bigInteger.ModPow(this.d, this.n);
			}
			if (this.keyBlinding)
			{
				bigInteger6 = bigInteger6 * bigInteger2.ModInverse(this.n) % this.n;
				bigInteger2.Clear();
			}
			byte[] paddedValue = this.GetPaddedValue(bigInteger6, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger6.Clear();
			return paddedValue;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000B594 File Offset: 0x00009794
		public override byte[] EncryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("public key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = bigInteger.ModPow(this.e, this.n);
			byte[] paddedValue = this.GetPaddedValue(bigInteger2, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger2.Clear();
			return paddedValue;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Exponent = this.e.GetBytes();
			rsaparameters.Modulus = this.n.GetBytes();
			if (includePrivateParameters)
			{
				if (this.d == null)
				{
					throw new CryptographicException("Missing private key");
				}
				rsaparameters.D = this.d.GetBytes();
				if (rsaparameters.D.Length != rsaparameters.Modulus.Length)
				{
					byte[] array = new byte[rsaparameters.Modulus.Length];
					Buffer.BlockCopy(rsaparameters.D, 0, array, array.Length - rsaparameters.D.Length, rsaparameters.D.Length);
					rsaparameters.D = array;
				}
				if (this.p != null && this.q != null && this.dp != null && this.dq != null && this.qInv != null)
				{
					int num = this.KeySize >> 4;
					rsaparameters.P = this.GetPaddedValue(this.p, num);
					rsaparameters.Q = this.GetPaddedValue(this.q, num);
					rsaparameters.DP = this.GetPaddedValue(this.dp, num);
					rsaparameters.DQ = this.GetPaddedValue(this.dq, num);
					rsaparameters.InverseQ = this.GetPaddedValue(this.qInv, num);
				}
			}
			return rsaparameters;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B790 File Offset: 0x00009990
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.Exponent == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Exponent"));
			}
			if (parameters.Modulus == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Modulus"));
			}
			this.e = new BigInteger(parameters.Exponent);
			this.n = new BigInteger(parameters.Modulus);
			this.d = (this.dp = (this.dq = (this.qInv = (this.p = (this.q = null)))));
			if (parameters.D != null)
			{
				this.d = new BigInteger(parameters.D);
			}
			if (parameters.DP != null)
			{
				this.dp = new BigInteger(parameters.DP);
			}
			if (parameters.DQ != null)
			{
				this.dq = new BigInteger(parameters.DQ);
			}
			if (parameters.InverseQ != null)
			{
				this.qInv = new BigInteger(parameters.InverseQ);
			}
			if (parameters.P != null)
			{
				this.p = new BigInteger(parameters.P);
			}
			if (parameters.Q != null)
			{
				this.q = new BigInteger(parameters.Q);
			}
			this.keypairGenerated = true;
			bool flag = this.p != null && this.q != null && this.dp != null;
			this.isCRTpossible = flag && this.dq != null && this.qInv != null;
			if (!flag)
			{
				return;
			}
			bool flag2 = this.n == this.p * this.q;
			if (flag2)
			{
				BigInteger bigInteger = this.p - 1;
				BigInteger bigInteger2 = this.q - 1;
				BigInteger bigInteger3 = bigInteger * bigInteger2;
				BigInteger bigInteger4 = this.e.ModInverse(bigInteger3);
				flag2 = this.d == bigInteger4;
				if (!flag2 && this.isCRTpossible)
				{
					flag2 = this.dp == bigInteger4 % bigInteger && this.dq == bigInteger4 % bigInteger2 && this.qInv == this.q.ModInverse(this.p);
				}
			}
			if (!flag2)
			{
				throw new CryptographicException(Locale.GetText("Private/public key mismatch"));
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000BA08 File Offset: 0x00009C08
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.d != null)
				{
					this.d.Clear();
					this.d = null;
				}
				if (this.p != null)
				{
					this.p.Clear();
					this.p = null;
				}
				if (this.q != null)
				{
					this.q.Clear();
					this.q = null;
				}
				if (this.dp != null)
				{
					this.dp.Clear();
					this.dp = null;
				}
				if (this.dq != null)
				{
					this.dq.Clear();
					this.dq = null;
				}
				if (this.qInv != null)
				{
					this.qInv.Clear();
					this.qInv = null;
				}
				if (disposing)
				{
					if (this.e != null)
					{
						this.e.Clear();
						this.e = null;
					}
					if (this.n != null)
					{
						this.n.Clear();
						this.n = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000BB2C File Offset: 0x00009D2C
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<RSAKeyValue>");
				stringBuilder.Append("<Modulus>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Modulus));
				stringBuilder.Append("</Modulus>");
				stringBuilder.Append("<Exponent>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Exponent));
				stringBuilder.Append("</Exponent>");
				if (includePrivateParameters)
				{
					if (rsaparameters.P != null)
					{
						stringBuilder.Append("<P>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.P));
						stringBuilder.Append("</P>");
					}
					if (rsaparameters.Q != null)
					{
						stringBuilder.Append("<Q>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.Q));
						stringBuilder.Append("</Q>");
					}
					if (rsaparameters.DP != null)
					{
						stringBuilder.Append("<DP>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DP));
						stringBuilder.Append("</DP>");
					}
					if (rsaparameters.DQ != null)
					{
						stringBuilder.Append("<DQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DQ));
						stringBuilder.Append("</DQ>");
					}
					if (rsaparameters.InverseQ != null)
					{
						stringBuilder.Append("<InverseQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.InverseQ));
						stringBuilder.Append("</InverseQ>");
					}
					stringBuilder.Append("<D>");
					stringBuilder.Append(Convert.ToBase64String(rsaparameters.D));
					stringBuilder.Append("</D>");
				}
				stringBuilder.Append("</RSAKeyValue>");
			}
			catch
			{
				if (rsaparameters.P != null)
				{
					Array.Clear(rsaparameters.P, 0, rsaparameters.P.Length);
				}
				if (rsaparameters.Q != null)
				{
					Array.Clear(rsaparameters.Q, 0, rsaparameters.Q.Length);
				}
				if (rsaparameters.DP != null)
				{
					Array.Clear(rsaparameters.DP, 0, rsaparameters.DP.Length);
				}
				if (rsaparameters.DQ != null)
				{
					Array.Clear(rsaparameters.DQ, 0, rsaparameters.DQ.Length);
				}
				if (rsaparameters.InverseQ != null)
				{
					Array.Clear(rsaparameters.InverseQ, 0, rsaparameters.InverseQ.Length);
				}
				if (rsaparameters.D != null)
				{
					Array.Clear(rsaparameters.D, 0, rsaparameters.D.Length);
				}
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		private byte[] GetPaddedValue(BigInteger value, int length)
		{
			byte[] bytes = value.GetBytes();
			if (bytes.Length >= length)
			{
				return bytes;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(bytes, 0, array, length - bytes.Length, bytes.Length);
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x04000213 RID: 531
		private bool isCRTpossible;

		// Token: 0x04000214 RID: 532
		private bool keyBlinding = true;

		// Token: 0x04000215 RID: 533
		private bool keypairGenerated;

		// Token: 0x04000216 RID: 534
		private bool m_disposed;

		// Token: 0x04000217 RID: 535
		private BigInteger d;

		// Token: 0x04000218 RID: 536
		private BigInteger p;

		// Token: 0x04000219 RID: 537
		private BigInteger q;

		// Token: 0x0400021A RID: 538
		private BigInteger dp;

		// Token: 0x0400021B RID: 539
		private BigInteger dq;

		// Token: 0x0400021C RID: 540
		private BigInteger qInv;

		// Token: 0x0400021D RID: 541
		private BigInteger n;

		// Token: 0x0400021E RID: 542
		private BigInteger e;

		// Token: 0x0400021F RID: 543
		[CompilerGenerated]
		private RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060001DB RID: 475
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
