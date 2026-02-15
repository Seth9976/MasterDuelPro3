using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x020002DB RID: 731
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x060011D5 RID: 4565 RVA: 0x000516EE File Offset: 0x0004F8EE
		internal WeakHashtable()
			: base(WeakHashtable._comparer)
		{
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000516FB File Offset: 0x0004F8FB
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00051703 File Offset: 0x0004F903
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x04000AE4 RID: 2788
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x020002DC RID: 732
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x060011D9 RID: 4569 RVA: 0x00051718 File Offset: 0x0004F918
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return x == y;
				}
				return false;
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0005177C File Offset: 0x0004F97C
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
