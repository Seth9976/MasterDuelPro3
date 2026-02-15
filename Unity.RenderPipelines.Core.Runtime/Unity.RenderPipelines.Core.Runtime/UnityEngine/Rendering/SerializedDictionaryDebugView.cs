using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000065 RID: 101
	internal sealed class SerializedDictionaryDebugView<K, V>
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000935C File Offset: 0x0000755C
		public SerializedDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException(dictionary.ToString());
			}
			this.dict = dictionary;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000937C File Offset: 0x0000757C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] items = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(items, 0);
				return items;
			}
		}

		// Token: 0x04000142 RID: 322
		private IDictionary<K, V> dict;
	}
}
