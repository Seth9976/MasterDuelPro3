using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a cache of runtime binding rules.</summary>
	/// <typeparam name="T">The delegate type.</typeparam>
	// Token: 0x0200011A RID: 282
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RuleCache<T> where T : class
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x00025E60 File Offset: 0x00024060
		internal RuleCache()
		{
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00025E7E File Offset: 0x0002407E
		internal T[] GetRules()
		{
			return this._rules;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00025E88 File Offset: 0x00024088
		internal void MoveRule(T rule, int i)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				int num = this._rules.Length - i;
				if (num > 8)
				{
					num = 8;
				}
				int num2 = -1;
				int num3 = Math.Min(this._rules.Length, i + num);
				for (int j = i; j < num3; j++)
				{
					if (this._rules[j] == rule)
					{
						num2 = j;
						break;
					}
				}
				if (num2 >= 2)
				{
					T t = this._rules[num2];
					this._rules[num2] = this._rules[num2 - 1];
					this._rules[num2 - 1] = this._rules[num2 - 2];
					this._rules[num2 - 2] = t;
				}
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00025F74 File Offset: 0x00024174
		internal void AddRule(T newRule)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00025FC0 File Offset: 0x000241C0
		private static T[] AddOrInsert(T[] rules, T item)
		{
			if (rules.Length < 64)
			{
				return rules.AddLast(item);
			}
			int num = rules.Length + 1;
			T[] array;
			if (num > 128)
			{
				num = 128;
				array = rules;
			}
			else
			{
				array = new T[num];
				Array.Copy(rules, 0, array, 0, 64);
			}
			array[64] = item;
			Array.Copy(rules, 64, array, 65, num - 64 - 1);
			return array;
		}

		// Token: 0x040002F8 RID: 760
		private T[] _rules = Array.Empty<T>();

		// Token: 0x040002F9 RID: 761
		private readonly object _cacheLock = new object();
	}
}
