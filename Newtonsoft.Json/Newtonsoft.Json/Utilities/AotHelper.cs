using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200008F RID: 143
	[NullableContext(2)]
	[Nullable(0)]
	public static class AotHelper
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001A5D4 File Offset: 0x000187D4
		[NullableContext(1)]
		public static void Ensure(Action action)
		{
			if (AotHelper.IsFalse())
			{
				try
				{
					action();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("", ex);
				}
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001A610 File Offset: 0x00018810
		public static void EnsureType<T>() where T : new()
		{
			AotHelper.Ensure(delegate
			{
				new T();
			});
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001A636 File Offset: 0x00018836
		public static void EnsureList<T>()
		{
			AotHelper.Ensure(delegate
			{
				List<T> list = new List<T>();
				new HashSet<T>();
				new CollectionWrapper<T>(list);
				new CollectionWrapper<T>(list);
			});
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001A65C File Offset: 0x0001885C
		public static void EnsureDictionary<TKey, TValue>()
		{
			AotHelper.Ensure(delegate
			{
				new Dictionary<TKey, TValue>();
				new DictionaryWrapper<TKey, TValue>(null);
				new DictionaryWrapper<TKey, TValue>(null);
				new DefaultContractResolver.EnumerableDictionaryWrapper<TKey, TValue>(null);
			});
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001A682 File Offset: 0x00018882
		public static bool IsFalse()
		{
			return AotHelper.s_alwaysFalse;
		}

		// Token: 0x04000360 RID: 864
		private static bool s_alwaysFalse = DateTime.UtcNow.Year < 0;
	}
}
