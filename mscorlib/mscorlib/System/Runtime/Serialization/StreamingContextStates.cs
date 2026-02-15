using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Defines a set of flags that specifies the source or destination context for the stream during serialization.</summary>
	// Token: 0x020004CF RID: 1231
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum StreamingContextStates
	{
		/// <summary>Specifies that the source or destination context is a different process on the same computer.</summary>
		// Token: 0x040012A7 RID: 4775
		CrossProcess = 1,
		/// <summary>Specifies that the source or destination context is a different computer.</summary>
		// Token: 0x040012A8 RID: 4776
		CrossMachine = 2,
		/// <summary>Specifies that the source or destination context is a file. Users can assume that files will last longer than the process that created them and not serialize objects in such a way that deserialization will require accessing any data from the current process.</summary>
		// Token: 0x040012A9 RID: 4777
		File = 4,
		/// <summary>Specifies that the source or destination context is a persisted store, which could include databases, files, or other backing stores. Users can assume that persisted data will last longer than the process that created the data and not serialize objects so that deserialization will require accessing any data from the current process.</summary>
		// Token: 0x040012AA RID: 4778
		Persistence = 8,
		/// <summary>Specifies that the data is remoted to a context in an unknown location. Users cannot make any assumptions whether this is on the same computer.</summary>
		// Token: 0x040012AB RID: 4779
		Remoting = 16,
		/// <summary>Specifies that the serialization context is unknown.</summary>
		// Token: 0x040012AC RID: 4780
		Other = 32,
		/// <summary>Specifies that the object graph is being cloned. Users can assume that the cloned graph will continue to exist within the same process and be safe to access handles or other references to unmanaged resources.</summary>
		// Token: 0x040012AD RID: 4781
		Clone = 64,
		/// <summary>Specifies that the source or destination context is a different AppDomain. (For a description of AppDomains, see [&lt;topic://cpconapplicationdomains&gt;]).</summary>
		// Token: 0x040012AE RID: 4782
		CrossAppDomain = 128,
		/// <summary>Specifies that the serialized data can be transmitted to or received from any of the other contexts.</summary>
		// Token: 0x040012AF RID: 4783
		All = 255
	}
}
