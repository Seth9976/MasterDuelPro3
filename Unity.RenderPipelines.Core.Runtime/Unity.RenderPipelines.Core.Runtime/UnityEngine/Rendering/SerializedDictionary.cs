using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000066 RID: 102
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SerializedDictionaryDebugView<, >))]
	[Serializable]
	public class SerializedDictionary<K, V> : SerializedDictionary<K, V, K, V>
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x000093A8 File Offset: 0x000075A8
		public override K SerializeKey(K key)
		{
			return key;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000093A8 File Offset: 0x000075A8
		public override V SerializeValue(V val)
		{
			return val;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000093A8 File Offset: 0x000075A8
		public override K DeserializeKey(K key)
		{
			return key;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000093A8 File Offset: 0x000075A8
		public override V DeserializeValue(V val)
		{
			return val;
		}
	}
}
