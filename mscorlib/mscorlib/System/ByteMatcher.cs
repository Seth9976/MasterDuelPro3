using System;
using System.Collections;

namespace System
{
	// Token: 0x020001E5 RID: 485
	internal class ByteMatcher
	{
		// Token: 0x060012F0 RID: 4848 RVA: 0x0004D97E File Offset: 0x0004BB7E
		public void AddMapping(TermInfoStrings key, byte[] val)
		{
			if (val.Length == 0)
			{
				return;
			}
			this.map[val] = key;
			this.starts[(int)val[0]] = true;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Sort()
		{
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004D9B0 File Offset: 0x0004BBB0
		public bool StartsWith(int c)
		{
			return this.starts[c] != null;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004D9C8 File Offset: 0x0004BBC8
		public TermInfoStrings Match(char[] buffer, int offset, int length, out int used)
		{
			foreach (object obj in this.map.Keys)
			{
				byte[] array = (byte[])obj;
				int num = 0;
				while (num < array.Length && num < length && (char)array[num] == buffer[offset + num])
				{
					if (array.Length - 1 == num)
					{
						used = array.Length;
						return (TermInfoStrings)this.map[array];
					}
					num++;
				}
			}
			used = 0;
			return (TermInfoStrings)(-1);
		}

		// Token: 0x040007B1 RID: 1969
		private Hashtable map = new Hashtable();

		// Token: 0x040007B2 RID: 1970
		private Hashtable starts = new Hashtable();
	}
}
