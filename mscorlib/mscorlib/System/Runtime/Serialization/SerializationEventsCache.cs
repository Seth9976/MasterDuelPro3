using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization
{
	// Token: 0x020004B3 RID: 1203
	internal static class SerializationEventsCache
	{
		// Token: 0x0600265C RID: 9820 RVA: 0x0009AB90 File Offset: 0x00098D90
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			return SerializationEventsCache.s_cache.GetOrAdd(t, (Type type) => new SerializationEvents(type));
		}

		// Token: 0x04001253 RID: 4691
		private static readonly ConcurrentDictionary<Type, SerializationEvents> s_cache = new ConcurrentDictionary<Type, SerializationEvents>();
	}
}
