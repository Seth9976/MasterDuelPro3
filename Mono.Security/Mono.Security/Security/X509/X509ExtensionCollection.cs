using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x0200001F RID: 31
	public sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00007E67 File Offset: 0x00006067
		public X509ExtensionCollection()
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008730 File Offset: 0x00006930
		public X509ExtensionCollection(ASN1 asn1)
			: this()
		{
			this.readOnly = true;
			if (asn1 == null)
			{
				return;
			}
			if (asn1.Tag != 48)
			{
				throw new Exception("Invalid extensions format");
			}
			for (int i = 0; i < asn1.Count; i++)
			{
				X509Extension x509Extension = new X509Extension(asn1[i]);
				base.InnerList.Add(x509Extension);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008790 File Offset: 0x00006990
		public int IndexOf(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Oid == oid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007EFB File Offset: 0x000060FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x17000043 RID: 67
		public X509Extension this[string oid]
		{
			get
			{
				int num = this.IndexOf(oid);
				if (num == -1)
				{
					return null;
				}
				return (X509Extension)base.InnerList[num];
			}
		}

		// Token: 0x04000083 RID: 131
		private bool readOnly;
	}
}
