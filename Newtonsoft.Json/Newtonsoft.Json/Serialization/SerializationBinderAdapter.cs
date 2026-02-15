using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000139 RID: 313
	[NullableContext(1)]
	[Nullable(0)]
	internal class SerializationBinderAdapter : ISerializationBinder
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x0002EC70 File Offset: 0x0002CE70
		public SerializationBinderAdapter(SerializationBinder serializationBinder)
		{
			this.SerializationBinder = serializationBinder;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002EC7F File Offset: 0x0002CE7F
		public Type BindToType([Nullable(2)] string assemblyName, string typeName)
		{
			return this.SerializationBinder.BindToType(assemblyName, typeName);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002EC8E File Offset: 0x0002CE8E
		[NullableContext(2)]
		public void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName)
		{
			this.SerializationBinder.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x040005BE RID: 1470
		public readonly SerializationBinder SerializationBinder;
	}
}
