using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000053 RID: 83
	public class ArrayEnumeration : IEnumerator
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000D8FD File Offset: 0x0000BAFD
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D914 File Offset: 0x0000BB14
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D91D File Offset: 0x0000BB1D
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000D925 File Offset: 0x0000BB25
		public ArrayEnumeration(object[] eArray)
		{
			this.eArray = eArray;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000D934 File Offset: 0x0000BB34
		public bool hasMoreElements()
		{
			return this.eArray != null && this.index < this.eArray.Length;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000D950 File Offset: 0x0000BB50
		public object nextElement()
		{
			if (this.eArray == null || this.index >= this.eArray.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			object[] array = this.eArray;
			int num = this.index;
			this.index = num + 1;
			return array[num];
		}

		// Token: 0x040001B5 RID: 437
		private object tempAuxObj;

		// Token: 0x040001B6 RID: 438
		private object[] eArray;

		// Token: 0x040001B7 RID: 439
		private int index;
	}
}
