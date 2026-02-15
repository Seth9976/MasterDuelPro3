using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Common.DbConnection.StateChange" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.StateChangeEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A2 RID: 162
	// (Invoke) Token: 0x060007E4 RID: 2020
	public delegate void StateChangeEventHandler(object sender, StateChangeEventArgs e);
}
