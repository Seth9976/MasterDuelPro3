using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001B0 RID: 432
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00040CC8 File Offset: 0x0003EEC8
		[NullableContext(1)]
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
