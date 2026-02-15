using System;
using System.Collections;

namespace System.Resources
{
	/// <summary>Provides the base functionality for reading data from resource files.</summary>
	// Token: 0x020005C1 RID: 1473
	public interface IResourceReader : IEnumerable, IDisposable
	{
		/// <summary>Closes the resource reader after releasing any resources associated with it.</summary>
		// Token: 0x06002BB2 RID: 11186
		void Close();

		/// <summary>Returns a dictionary enumerator of the resources for this reader.</summary>
		/// <returns>A dictionary enumerator for the resources for this reader.</returns>
		// Token: 0x06002BB3 RID: 11187
		IDictionaryEnumerator GetEnumerator();
	}
}
