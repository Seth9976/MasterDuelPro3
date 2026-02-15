using System;

namespace Mono.Security.X509
{
	// Token: 0x02000013 RID: 19
	internal class SafeBag
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003DA6 File Offset: 0x00001FA6
		public SafeBag(string bagOID, ASN1 asn1)
		{
			this._bagOID = bagOID;
			this._asn1 = asn1;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003DBC File Offset: 0x00001FBC
		public string BagOID
		{
			get
			{
				return this._bagOID;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public ASN1 ASN1
		{
			get
			{
				return this._asn1;
			}
		}

		// Token: 0x0400002A RID: 42
		private string _bagOID;

		// Token: 0x0400002B RID: 43
		private ASN1 _asn1;
	}
}
