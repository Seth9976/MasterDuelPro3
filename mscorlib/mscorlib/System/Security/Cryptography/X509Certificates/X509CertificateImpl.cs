using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003CD RID: 973
	internal abstract class X509CertificateImpl : IDisposable
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002129 RID: 8489
		public abstract bool IsValid { get; }

		// Token: 0x0600212A RID: 8490 RVA: 0x0008A42E File Offset: 0x0008862E
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x0600212B RID: 8491
		public abstract X509CertificateImpl Clone();

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600212C RID: 8492
		public abstract string Issuer { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600212D RID: 8493
		public abstract string Subject { get; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600212E RID: 8494
		public abstract byte[] RawData { get; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600212F RID: 8495
		public abstract DateTime NotAfter { get; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002130 RID: 8496
		public abstract DateTime NotBefore { get; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06002131 RID: 8497
		public abstract byte[] Thumbprint { get; }

		// Token: 0x06002132 RID: 8498 RVA: 0x0008A440 File Offset: 0x00088640
		public sealed override int GetHashCode()
		{
			if (!this.IsValid)
			{
				return 0;
			}
			byte[] thumbprint = this.Thumbprint;
			int num = 0;
			int num2 = 0;
			while (num2 < thumbprint.Length && num2 < 4)
			{
				num = (num << 8) | (int)thumbprint[num2];
				num2++;
			}
			return num;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002133 RID: 8499
		public abstract string KeyAlgorithm { get; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002134 RID: 8500
		public abstract byte[] KeyAlgorithmParameters { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06002135 RID: 8501
		public abstract byte[] PublicKeyValue { get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06002136 RID: 8502
		public abstract byte[] SerialNumber { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06002137 RID: 8503
		public abstract bool HasPrivateKey { get; }

		// Token: 0x06002138 RID: 8504
		public abstract RSA GetRSAPrivateKey();

		// Token: 0x06002139 RID: 8505
		public abstract DSA GetDSAPrivateKey();

		// Token: 0x0600213A RID: 8506
		public abstract byte[] Export(X509ContentType contentType, SafePasswordHandle password);

		// Token: 0x0600213B RID: 8507
		public abstract X509CertificateImpl CopyWithPrivateKey(RSA privateKey);

		// Token: 0x0600213C RID: 8508 RVA: 0x0008A47C File Offset: 0x0008867C
		public sealed override bool Equals(object obj)
		{
			X509CertificateImpl x509CertificateImpl = obj as X509CertificateImpl;
			if (x509CertificateImpl == null)
			{
				return false;
			}
			if (!this.IsValid || !x509CertificateImpl.IsValid)
			{
				return false;
			}
			if (!this.Issuer.Equals(x509CertificateImpl.Issuer))
			{
				return false;
			}
			byte[] serialNumber = this.SerialNumber;
			byte[] serialNumber2 = x509CertificateImpl.SerialNumber;
			if (serialNumber.Length != serialNumber2.Length)
			{
				return false;
			}
			for (int i = 0; i < serialNumber.Length; i++)
			{
				if (serialNumber[i] != serialNumber2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0008A4ED File Offset: 0x000886ED
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0008A4FC File Offset: 0x000886FC
		~X509CertificateImpl()
		{
			this.Dispose(false);
		}
	}
}
