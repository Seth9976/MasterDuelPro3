using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200011F RID: 287
	public class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00027DE5 File Offset: 0x00025FE5
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00027DED File Offset: 0x00025FED
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> ISerializableCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00027DF6 File Offset: 0x00025FF6
		[NullableContext(1)]
		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
