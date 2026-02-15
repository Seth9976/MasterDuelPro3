using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000059 RID: 89
	public class EnumeratedIterator : IEnumerator
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000EBA2 File Offset: 0x0000CDA2
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000EBB9 File Offset: 0x0000CDB9
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000EBCA File Offset: 0x0000CDCA
		public EnumeratedIterator(IEnumerator iterator)
		{
			this.i = iterator;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
		public bool hasMoreElements()
		{
			return this.i.MoveNext();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000EBE6 File Offset: 0x0000CDE6
		public object nextElement()
		{
			return this.i.Current;
		}

		// Token: 0x040001D3 RID: 467
		private object tempAuxObj;

		// Token: 0x040001D4 RID: 468
		private IEnumerator i;
	}
}
