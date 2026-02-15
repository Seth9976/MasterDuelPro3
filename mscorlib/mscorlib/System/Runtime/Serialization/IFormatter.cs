using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides functionality for formatting serialized objects.</summary>
	// Token: 0x020004BA RID: 1210
	[ComVisible(true)]
	public interface IFormatter
	{
		/// <summary>Serializes an object, or graph of objects with the given root to the provided stream.</summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. This stream can reference a variety of backing stores (such as files, network, memory, and so on). </param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized. </param>
		// Token: 0x06002684 RID: 9860
		void Serialize(Stream serializationStream, object graph);
	}
}
