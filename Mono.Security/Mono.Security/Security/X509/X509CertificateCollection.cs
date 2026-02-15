using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class X509CertificateCollection : CollectionBase, IEnumerable
	{
		// Token: 0x1700003C RID: 60
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007E82 File Offset: 0x00006082
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007EA0 File Offset: 0x000060A0
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007EE4 File Offset: 0x000060E4
		public bool Contains(X509Certificate value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007EF3 File Offset: 0x000060F3
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007EFB File Offset: 0x000060FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007F08 File Offset: 0x00006108
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007F18 File Offset: 0x00006118
		public int IndexOf(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] hash = value.Hash;
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (this.Compare(x509Certificate.Hash, hash))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007F74 File Offset: 0x00006174
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0200001B RID: 27
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x060000DB RID: 219 RVA: 0x00007FB4 File Offset: 0x000061B4
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00007FC8 File Offset: 0x000061C8
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000DD RID: 221 RVA: 0x00007FDA File Offset: 0x000061DA
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00007FE7 File Offset: 0x000061E7
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00007FF4 File Offset: 0x000061F4
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00007FE7 File Offset: 0x000061E7
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x04000072 RID: 114
			private IEnumerator enumerator;
		}
	}
}
