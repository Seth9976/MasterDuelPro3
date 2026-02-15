using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Internal.Cryptography.Pal;
using Microsoft.Win32.SafeHandles;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001C1 RID: 449
	internal abstract class X509Certificate2ImplUnix : X509Certificate2Impl
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x00036CE5 File Offset: 0x00034EE5
		private void EnsureCertData()
		{
			if (this.readCertData)
			{
				return;
			}
			base.ThrowIfContextInvalid();
			this.certData = new CertificateData(this.GetRawCertData());
			this.readCertData = true;
		}

		// Token: 0x06000AAD RID: 2733
		protected abstract byte[] GetRawCertData();

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00036D0E File Offset: 0x00034F0E
		public sealed override string KeyAlgorithm
		{
			get
			{
				this.EnsureCertData();
				return this.certData.PublicKeyAlgorithm.AlgorithmId;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00036D26 File Offset: 0x00034F26
		public sealed override byte[] KeyAlgorithmParameters
		{
			get
			{
				this.EnsureCertData();
				return this.certData.PublicKeyAlgorithm.Parameters;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00036D3E File Offset: 0x00034F3E
		public sealed override byte[] PublicKeyValue
		{
			get
			{
				this.EnsureCertData();
				return this.certData.PublicKey;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00036D51 File Offset: 0x00034F51
		public sealed override byte[] SerialNumber
		{
			get
			{
				this.EnsureCertData();
				return this.certData.SerialNumber;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00036D64 File Offset: 0x00034F64
		public sealed override string SignatureAlgorithm
		{
			get
			{
				this.EnsureCertData();
				return this.certData.SignatureAlgorithm.AlgorithmId;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00036D7C File Offset: 0x00034F7C
		public sealed override int Version
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Version + 1;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00036D91 File Offset: 0x00034F91
		public sealed override X500DistinguishedName SubjectName
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Subject;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00036DA4 File Offset: 0x00034FA4
		public sealed override X500DistinguishedName IssuerName
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Issuer;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00036DB7 File Offset: 0x00034FB7
		public sealed override string Subject
		{
			get
			{
				return this.SubjectName.Name;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00036DC4 File Offset: 0x00034FC4
		public sealed override string Issuer
		{
			get
			{
				return this.IssuerName.Name;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00036DD1 File Offset: 0x00034FD1
		public sealed override byte[] RawData
		{
			get
			{
				this.EnsureCertData();
				return this.certData.RawData;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public sealed override byte[] Thumbprint
		{
			get
			{
				this.EnsureCertData();
				byte[] array;
				using (SHA1 sha = SHA1.Create())
				{
					array = sha.ComputeHash(this.certData.RawData);
				}
				return array;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00036E2C File Offset: 0x0003502C
		public sealed override string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			this.EnsureCertData();
			return this.certData.GetNameInfo(nameType, forIssuer);
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00036E41 File Offset: 0x00035041
		public sealed override IEnumerable<X509Extension> Extensions
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Extensions;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00036E54 File Offset: 0x00035054
		public sealed override DateTime NotAfter
		{
			get
			{
				this.EnsureCertData();
				return this.certData.NotAfter.ToLocalTime();
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00036E6C File Offset: 0x0003506C
		public sealed override DateTime NotBefore
		{
			get
			{
				this.EnsureCertData();
				return this.certData.NotBefore.ToLocalTime();
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00036E84 File Offset: 0x00035084
		public sealed override void AppendPrivateKeyInfo(StringBuilder sb)
		{
			if (!this.HasPrivateKey)
			{
				return;
			}
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("[Private Key]");
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00036EAC File Offset: 0x000350AC
		public sealed override byte[] Export(X509ContentType contentType, SafePasswordHandle password)
		{
			base.ThrowIfContextInvalid();
			switch (contentType)
			{
			case X509ContentType.Cert:
				return this.RawData;
			case X509ContentType.SerializedCert:
			case X509ContentType.SerializedStore:
				throw new PlatformNotSupportedException("X509ContentType.SerializedCert and X509ContentType.SerializedStore are not supported on Unix.");
			case X509ContentType.Pfx:
				return this.ExportPkcs12(password);
			case X509ContentType.Pkcs7:
				return this.ExportPkcs12(null);
			default:
				throw new CryptographicException("Invalid content type.");
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00036F0C File Offset: 0x0003510C
		private byte[] ExportPkcs12(SafePasswordHandle password)
		{
			if (password == null || password.IsInvalid)
			{
				return this.ExportPkcs12(null);
			}
			string text = password.Mono_DangerousGetString();
			return this.ExportPkcs12(text);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00036F3C File Offset: 0x0003513C
		private byte[] ExportPkcs12(string password)
		{
			PKCS12 pkcs = new PKCS12();
			byte[] bytes;
			try
			{
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = arrayList;
				byte[] array = new byte[4];
				array[0] = 1;
				arrayList2.Add(array);
				hashtable.Add("1.2.840.113549.1.9.21", arrayList);
				if (password != null)
				{
					pkcs.Password = password;
				}
				pkcs.AddCertificate(new X509Certificate(this.RawData), hashtable);
				if (this.IntermediateCertificates != null)
				{
					for (int i = 0; i < this.IntermediateCertificates.Count; i++)
					{
						pkcs.AddCertificate(new X509Certificate(this.IntermediateCertificates[i].RawData));
					}
				}
				AsymmetricAlgorithm privateKey = this.PrivateKey;
				if (privateKey != null)
				{
					pkcs.AddPkcs8ShroudedKeyBag(privateKey, hashtable);
				}
				bytes = pkcs.GetBytes();
			}
			finally
			{
				pkcs.Password = null;
			}
			return bytes;
		}

		// Token: 0x0400082C RID: 2092
		private bool readCertData;

		// Token: 0x0400082D RID: 2093
		private CertificateData certData;
	}
}
