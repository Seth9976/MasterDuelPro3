using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000120 RID: 288
	public class JsonLinqContract : JsonContract
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00027E06 File Offset: 0x00026006
		[NullableContext(1)]
		public JsonLinqContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
