using System;

namespace System.Collections
{
	// Token: 0x02000705 RID: 1797
	[Serializable]
	internal sealed class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06003833 RID: 14387 RVA: 0x000DC05D File Offset: 0x000DA25D
		internal CompatibleComparer(IHashCodeProvider hashCodeProvider, IComparer comparer)
		{
			this._hcp = hashCodeProvider;
			this._comparer = comparer;
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000DC073 File Offset: 0x000DA273
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000DC07B File Offset: 0x000DA27B
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000DC083 File Offset: 0x000DA283
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000DC090 File Offset: 0x000DA290
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000DC0DF File Offset: 0x000DA2DF
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp == null)
			{
				return obj.GetHashCode();
			}
			return this._hcp.GetHashCode(obj);
		}

		// Token: 0x04001E58 RID: 7768
		private readonly IHashCodeProvider _hcp;

		// Token: 0x04001E59 RID: 7769
		private readonly IComparer _comparer;
	}
}
