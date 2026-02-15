using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Provides data for the <see cref="T:System.Exception.SerializeObjectState" /> event.</summary>
	// Token: 0x020004C4 RID: 1220
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x060026EB RID: 9963 RVA: 0x0009D202 File Offset: 0x0009B402
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_streamingContext = streamingContext;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0009D21C File Offset: 0x0009B41C
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		// Token: 0x0400128E RID: 4750
		private StreamingContext m_streamingContext;

		// Token: 0x0400128F RID: 4751
		private List<object> m_serializedStates = new List<object>();
	}
}
