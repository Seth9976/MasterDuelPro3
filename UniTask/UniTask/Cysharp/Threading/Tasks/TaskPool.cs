using System;
using System.Collections.Generic;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200006D RID: 109
	public static class TaskPool
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00005434 File Offset: 0x00003634
		static TaskPool()
		{
			try
			{
				string value = Environment.GetEnvironmentVariable("UNITASK_MAX_POOLSIZE");
				int size;
				if (value != null && int.TryParse(value, out size))
				{
					TaskPool.MaxPoolSize = size;
					return;
				}
			}
			catch
			{
			}
			TaskPool.MaxPoolSize = int.MaxValue;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000548C File Offset: 0x0000368C
		public static void SetMaxPoolSize(int maxPoolSize)
		{
			TaskPool.MaxPoolSize = maxPoolSize;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005494 File Offset: 0x00003694
		public static IEnumerable<ValueTuple<Type, int>> GetCacheSizeInfo()
		{
			Dictionary<Type, Func<int>> dictionary = TaskPool.sizes;
			lock (dictionary)
			{
				foreach (KeyValuePair<Type, Func<int>> item in TaskPool.sizes)
				{
					yield return new ValueTuple<Type, int>(item.Key, item.Value());
				}
				Dictionary<Type, Func<int>>.Enumerator enumerator = default(Dictionary<Type, Func<int>>.Enumerator);
			}
			dictionary = null;
			yield break;
			yield break;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000054A0 File Offset: 0x000036A0
		public static void RegisterSizeGetter(Type type, Func<int> getSize)
		{
			Dictionary<Type, Func<int>> dictionary = TaskPool.sizes;
			lock (dictionary)
			{
				TaskPool.sizes[type] = getSize;
			}
		}

		// Token: 0x040000E6 RID: 230
		internal static int MaxPoolSize;

		// Token: 0x040000E7 RID: 231
		private static Dictionary<Type, Func<int>> sizes = new Dictionary<Type, Func<int>>();
	}
}
