using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000156 RID: 342
	internal class TempAssemblyCache
	{
		// Token: 0x170003BD RID: 957
		internal TempAssembly this[string ns, object o]
		{
			get
			{
				return (TempAssembly)this.cache[new TempAssemblyCacheKey(ns, o)];
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000527B4 File Offset: 0x000509B4
		internal void Add(string ns, object o, TempAssembly assembly)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = new TempAssemblyCacheKey(ns, o);
			lock (this)
			{
				if (this.cache[tempAssemblyCacheKey] != assembly)
				{
					Hashtable hashtable = new Hashtable();
					foreach (object obj in this.cache.Keys)
					{
						hashtable.Add(obj, this.cache[obj]);
					}
					this.cache = hashtable;
					this.cache[tempAssemblyCacheKey] = assembly;
				}
			}
		}

		// Token: 0x0400081B RID: 2075
		private Hashtable cache = new Hashtable();
	}
}
