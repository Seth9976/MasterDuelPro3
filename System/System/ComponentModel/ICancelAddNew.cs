using System;

namespace System.ComponentModel
{
	/// <summary>Adds transactional capability when adding a new item to a collection.</summary>
	// Token: 0x0200027C RID: 636
	public interface ICancelAddNew
	{
		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection. </param>
		// Token: 0x06000F35 RID: 3893
		void EndNew(int itemIndex);
	}
}
