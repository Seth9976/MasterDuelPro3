using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200012E RID: 302
	public class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x0002E245 File Offset: 0x0002C445
		[NullableContext(1)]
		public JsonStringContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
