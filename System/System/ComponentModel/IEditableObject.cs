using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to commit or rollback changes to an object that is used as a data source.</summary>
	// Token: 0x020002AB RID: 683
	public interface IEditableObject
	{
		/// <summary>Begins an edit on an object.</summary>
		// Token: 0x06001050 RID: 4176
		void BeginEdit();

		/// <summary>Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.</summary>
		// Token: 0x06001051 RID: 4177
		void EndEdit();
	}
}
