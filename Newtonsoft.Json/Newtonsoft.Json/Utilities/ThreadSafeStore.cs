using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(1)]
	[Nullable(0)]
	internal class ThreadSafeStore<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x000240EF File Offset: 0x000222EF
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			this._creator = creator;
			this._concurrentStore = new ConcurrentDictionary<TKey, TValue>();
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00024114 File Offset: 0x00022314
		public TValue Get(TKey key)
		{
			return this._concurrentStore.GetOrAdd(key, this._creator);
		}

		// Token: 0x040004D8 RID: 1240
		private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;

		// Token: 0x040004D9 RID: 1241
		private readonly Func<TKey, TValue> _creator;
	}
}
