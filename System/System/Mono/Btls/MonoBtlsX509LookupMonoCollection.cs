using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Btls
{
	// Token: 0x020000C5 RID: 197
	internal class MonoBtlsX509LookupMonoCollection : MonoBtlsX509LookupMono
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000C863 File Offset: 0x0000AA63
		internal MonoBtlsX509LookupMonoCollection(X509CertificateCollection collection, MonoBtlsX509TrustKind trust)
		{
			this.collection = collection;
			this.trust = trust;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private void Initialize()
		{
			if (this.certificates != null)
			{
				return;
			}
			this.hashes = new long[this.collection.Count];
			this.certificates = new MonoBtlsX509[this.collection.Count];
			for (int i = 0; i < this.collection.Count; i++)
			{
				byte[] rawCertData = this.collection[i].GetRawCertData();
				this.certificates[i] = MonoBtlsX509.LoadFromData(rawCertData, MonoBtlsX509Format.DER);
				this.certificates[i].AddExplicitTrust(this.trust);
				this.hashes[i] = this.certificates[i].GetSubjectNameHash();
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C920 File Offset: 0x0000AB20
		protected override MonoBtlsX509 OnGetBySubject(MonoBtlsX509Name name)
		{
			this.Initialize();
			long hash = name.GetHash();
			MonoBtlsX509 monoBtlsX = null;
			for (int i = 0; i < this.certificates.Length; i++)
			{
				if (this.hashes[i] == hash)
				{
					monoBtlsX = this.certificates[i];
					base.AddCertificate(monoBtlsX);
				}
			}
			return monoBtlsX;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000C96C File Offset: 0x0000AB6C
		protected override void Close()
		{
			try
			{
				if (this.certificates != null)
				{
					for (int i = 0; i < this.certificates.Length; i++)
					{
						if (this.certificates[i] != null)
						{
							this.certificates[i].Dispose();
							this.certificates[i] = null;
						}
					}
					this.certificates = null;
					this.hashes = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x040002F8 RID: 760
		private long[] hashes;

		// Token: 0x040002F9 RID: 761
		private MonoBtlsX509[] certificates;

		// Token: 0x040002FA RID: 762
		private X509CertificateCollection collection;

		// Token: 0x040002FB RID: 763
		private MonoBtlsX509TrustKind trust;
	}
}
