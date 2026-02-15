using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000F0 RID: 240
	internal sealed class KeyedStack<TKey, TValue> where TValue : class
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x0001AC4C File Offset: 0x00018E4C
		internal void Push(TKey key, TValue value)
		{
			Stack<TValue> stack;
			if (!this._data.TryGetValue(key, out stack))
			{
				this._data.Add(key, stack = new Stack<TValue>());
			}
			stack.Push(value);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001AC84 File Offset: 0x00018E84
		internal TValue TryPop(TKey key)
		{
			Stack<TValue> stack;
			TValue tvalue;
			if (!this._data.TryGetValue(key, out stack) || !stack.TryPop(out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x04000267 RID: 615
		private readonly Dictionary<TKey, Stack<TValue>> _data = new Dictionary<TKey, Stack<TValue>>();
	}
}
