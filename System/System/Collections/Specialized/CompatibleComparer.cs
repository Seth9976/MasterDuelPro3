using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x02000312 RID: 786
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x00055593 File Offset: 0x00053793
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000555AC File Offset: 0x000537AC
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0005561C File Offset: 0x0005381C
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00055647 File Offset: 0x00053847
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0005564F File Offset: 0x0005384F
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00055657 File Offset: 0x00053857
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0005567A File Offset: 0x0005387A
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x04000B9B RID: 2971
		private IComparer _comparer;

		// Token: 0x04000B9C RID: 2972
		private static volatile IComparer defaultComparer;

		// Token: 0x04000B9D RID: 2973
		private IHashCodeProvider _hcp;

		// Token: 0x04000B9E RID: 2974
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}
