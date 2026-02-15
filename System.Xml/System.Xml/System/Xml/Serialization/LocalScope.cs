using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200014B RID: 331
	internal class LocalScope
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00050A6D File Offset: 0x0004EC6D
		public LocalScope()
		{
			this.locals = new Dictionary<string, LocalBuilder>();
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00050A80 File Offset: 0x0004EC80
		public LocalScope(LocalScope parent)
			: this()
		{
			this.parent = parent;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00050A8F File Offset: 0x0004EC8F
		public void Add(string key, LocalBuilder value)
		{
			this.locals.Add(key, value);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00050A9E File Offset: 0x0004EC9E
		public bool ContainsKey(string key)
		{
			return this.locals.ContainsKey(key) || (this.parent != null && this.parent.ContainsKey(key));
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00050AC6 File Offset: 0x0004ECC6
		public bool TryGetValue(string key, out LocalBuilder value)
		{
			if (this.locals.TryGetValue(key, out value))
			{
				return true;
			}
			if (this.parent != null)
			{
				return this.parent.TryGetValue(key, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x170003B4 RID: 948
		public LocalBuilder this[string key]
		{
			get
			{
				LocalBuilder localBuilder;
				this.TryGetValue(key, out localBuilder);
				return localBuilder;
			}
			set
			{
				this.locals[key] = value;
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00050B1C File Offset: 0x0004ED1C
		public void AddToFreeLocals(Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals)
		{
			foreach (KeyValuePair<string, LocalBuilder> keyValuePair in this.locals)
			{
				Tuple<Type, string> tuple = new Tuple<Type, string>(keyValuePair.Value.LocalType, keyValuePair.Key);
				Queue<LocalBuilder> queue;
				if (freeLocals.TryGetValue(tuple, out queue))
				{
					queue.Enqueue(keyValuePair.Value);
				}
				else
				{
					queue = new Queue<LocalBuilder>();
					queue.Enqueue(keyValuePair.Value);
					freeLocals.Add(tuple, queue);
				}
			}
		}

		// Token: 0x040007F9 RID: 2041
		public readonly LocalScope parent;

		// Token: 0x040007FA RID: 2042
		private readonly Dictionary<string, LocalBuilder> locals;
	}
}
