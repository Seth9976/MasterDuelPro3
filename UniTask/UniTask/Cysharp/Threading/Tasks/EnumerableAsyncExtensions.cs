using System;
using System.Collections.Generic;
using System.Linq;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000027 RID: 39
	public static class EnumerableAsyncExtensions
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00003D59 File Offset: 0x00001F59
		public static IEnumerable<UniTask> Select<T>(this IEnumerable<T> source, Func<T, UniTask> selector)
		{
			return source.Select(selector);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D62 File Offset: 0x00001F62
		public static IEnumerable<UniTask<TR>> Select<T, TR>(this IEnumerable<T> source, Func<T, UniTask<TR>> selector)
		{
			return source.Select(selector);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003D6B File Offset: 0x00001F6B
		public static IEnumerable<UniTask> Select<T>(this IEnumerable<T> source, Func<T, int, UniTask> selector)
		{
			return source.Select(selector);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D74 File Offset: 0x00001F74
		public static IEnumerable<UniTask<TR>> Select<T, TR>(this IEnumerable<T> source, Func<T, int, UniTask<TR>> selector)
		{
			return source.Select(selector);
		}
	}
}
