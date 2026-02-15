using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x02000260 RID: 608
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed. </param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred. </param>
		// Token: 0x06000E6D RID: 3693 RVA: 0x0003F470 File Offset: 0x0003D670
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.Action = action;
			this.Element = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003F486 File Offset: 0x0003D686
		public virtual CollectionChangeAction Action { get; }

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or null if you refresh the collection.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003F48E File Offset: 0x0003D68E
		public virtual object Element { get; }
	}
}
