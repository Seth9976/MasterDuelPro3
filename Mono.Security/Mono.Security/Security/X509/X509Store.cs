using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000020 RID: 32
	public class X509Store
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00008810 File Offset: 0x00006A10
		internal X509Store(string path, bool crl, bool newFormat)
		{
			this._storePath = path;
			this._crl = crl;
			this._newFormat = newFormat;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000882D File Offset: 0x00006A2D
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certificates == null)
				{
					this._certificates = this.BuildCertificatesCollection(this._storePath);
				}
				return this._certificates;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000884F File Offset: 0x00006A4F
		public ArrayList Crls
		{
			get
			{
				if (!this._crl)
				{
					this._crls = new ArrayList();
				}
				if (this._crls == null)
				{
					this._crls = this.BuildCrlsCollection(this._storePath);
				}
				return this._crls;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008884 File Offset: 0x00006A84
		private byte[] Load(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(filename))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000088D8 File Offset: 0x00006AD8
		private X509Certificate LoadCertificate(string filename)
		{
			X509Certificate x509Certificate = new X509Certificate(this.Load(filename));
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(x509Certificate.Hash);
			if (this._storePath.StartsWith(X509StoreManager.LocalMachinePath) || this._storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			KeyPairPersistence keyPairPersistence = new KeyPairPersistence(cspParameters);
			try
			{
				if (!keyPairPersistence.Load())
				{
					return x509Certificate;
				}
			}
			catch
			{
				return x509Certificate;
			}
			if (x509Certificate.RSA != null)
			{
				x509Certificate.RSA = new RSACryptoServiceProvider(cspParameters);
			}
			else if (x509Certificate.DSA != null)
			{
				x509Certificate.DSA = new DSACryptoServiceProvider(cspParameters);
			}
			return x509Certificate;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000898C File Offset: 0x00006B8C
		private X509Crl LoadCrl(string filename)
		{
			return new X509Crl(this.Load(filename));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000899C File Offset: 0x00006B9C
		private bool CheckStore(string path, bool throwException)
		{
			bool flag;
			try
			{
				if (Directory.Exists(path))
				{
					flag = true;
				}
				else
				{
					Directory.CreateDirectory(path);
					flag = Directory.Exists(path);
				}
			}
			catch
			{
				if (throwException)
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000089E0 File Offset: 0x00006BE0
		private X509CertificateCollection BuildCertificatesCollection(string storeName)
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			string text = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(text, false))
			{
				return x509CertificateCollection;
			}
			string[] files = Directory.GetFiles(text, this._newFormat ? "*.0" : "*.cer");
			if (files != null && files.Length != 0)
			{
				foreach (string text2 in files)
				{
					try
					{
						X509Certificate x509Certificate = this.LoadCertificate(text2);
						x509CertificateCollection.Add(x509Certificate);
					}
					catch
					{
					}
				}
			}
			return x509CertificateCollection;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008A74 File Offset: 0x00006C74
		private ArrayList BuildCrlsCollection(string storeName)
		{
			ArrayList arrayList = new ArrayList();
			string text = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(text, false))
			{
				return arrayList;
			}
			string[] files = Directory.GetFiles(text, "*.crl");
			if (files != null && files.Length != 0)
			{
				foreach (string text2 in files)
				{
					try
					{
						X509Crl x509Crl = this.LoadCrl(text2);
						arrayList.Add(x509Crl);
					}
					catch
					{
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04000084 RID: 132
		private string _storePath;

		// Token: 0x04000085 RID: 133
		private X509CertificateCollection _certificates;

		// Token: 0x04000086 RID: 134
		private ArrayList _crls;

		// Token: 0x04000087 RID: 135
		private bool _crl;

		// Token: 0x04000088 RID: 136
		private bool _newFormat;
	}
}
