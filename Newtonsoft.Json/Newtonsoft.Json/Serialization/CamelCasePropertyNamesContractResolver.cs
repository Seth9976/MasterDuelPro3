using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(1)]
	[Nullable(0)]
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00024304 File Offset: 0x00022504
		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00024328 File Offset: 0x00022528
		public override JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			StructMultiKey<Type, Type> structMultiKey = new StructMultiKey<Type, Type>(base.GetType(), type);
			Dictionary<StructMultiKey<Type, Type>, JsonContract> dictionary = CamelCasePropertyNamesContractResolver._contractCache;
			JsonContract jsonContract;
			if (dictionary == null || !dictionary.TryGetValue(structMultiKey, out jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = CamelCasePropertyNamesContractResolver.TypeContractCacheLock;
				lock (typeContractCacheLock)
				{
					dictionary = CamelCasePropertyNamesContractResolver._contractCache;
					Dictionary<StructMultiKey<Type, Type>, JsonContract> dictionary2 = ((dictionary != null) ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(dictionary) : new Dictionary<StructMultiKey<Type, Type>, JsonContract>());
					dictionary2[structMultiKey] = jsonContract;
					CamelCasePropertyNamesContractResolver._contractCache = dictionary2;
				}
			}
			return jsonContract;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000243C8 File Offset: 0x000225C8
		internal override DefaultJsonNameTable GetNameTable()
		{
			return CamelCasePropertyNamesContractResolver.NameTable;
		}

		// Token: 0x040004DB RID: 1243
		private static readonly object TypeContractCacheLock = new object();

		// Token: 0x040004DC RID: 1244
		private static readonly DefaultJsonNameTable NameTable = new DefaultJsonNameTable();

		// Token: 0x040004DD RID: 1245
		[Nullable(new byte[] { 2, 0, 1, 1, 1 })]
		private static Dictionary<StructMultiKey<Type, Type>, JsonContract> _contractCache;
	}
}
