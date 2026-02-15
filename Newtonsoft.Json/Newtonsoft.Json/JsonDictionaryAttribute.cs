using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002380 File Offset: 0x00000580
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002397 File Offset: 0x00000597
		[NullableContext(1)]
		public JsonDictionaryAttribute(string id)
			: base(id)
		{
		}
	}
}
