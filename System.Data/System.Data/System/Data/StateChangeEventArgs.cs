using System;

namespace System.Data
{
	/// <summary>Provides data for the state change event of a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A1 RID: 161
	public sealed class StateChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StateChangeEventArgs" /> class, when given the original state and the current state of the object.</summary>
		/// <param name="originalState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		/// <param name="currentState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		// Token: 0x060007E2 RID: 2018 RVA: 0x0002822A File Offset: 0x0002642A
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this._originalState = originalState;
			this._currentState = currentState;
		}

		// Token: 0x04000328 RID: 808
		private ConnectionState _originalState;

		// Token: 0x04000329 RID: 809
		private ConnectionState _currentState;
	}
}
