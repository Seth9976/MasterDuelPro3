using System;

namespace System.ComponentModel
{
	/// <summary>Defines the mechanism for querying the object for changes and resetting of the changed status.</summary>
	// Token: 0x020002AA RID: 682
	public interface IChangeTracking
	{
		/// <summary>Gets the object's changed status.</summary>
		/// <returns>true if the object’s content has changed since the last call to <see cref="M:System.ComponentModel.IChangeTracking.AcceptChanges" />; otherwise, false.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600104E RID: 4174
		bool IsChanged { get; }

		/// <summary>Resets the object’s state to unchanged by accepting the modifications.</summary>
		// Token: 0x0600104F RID: 4175
		void AcceptChanges();
	}
}
