using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000034 RID: 52
	public class DataColumnChangeEventArgs : EventArgs
	{
		// Token: 0x06000389 RID: 905 RVA: 0x000134F1 File Offset: 0x000116F1
		internal DataColumnChangeEventArgs(DataRow row)
		{
			this.<Row>k__BackingField = row;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumnChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> of the column with the changing value. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> with the changing value. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x0600038A RID: 906 RVA: 0x00013500 File Offset: 0x00011700
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this.<Row>k__BackingField = row;
			this._column = column;
			this.ProposedValue = value;
		}

		/// <summary>Gets or sets the proposed new value for the column.</summary>
		/// <returns>The proposed value, of type <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001351D File Offset: 0x0001171D
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00013525 File Offset: 0x00011725
		public object ProposedValue { get; set; }

		// Token: 0x0600038D RID: 909 RVA: 0x0001352E File Offset: 0x0001172E
		internal void InitializeColumnChangeEvent(DataColumn column, object value)
		{
			this._column = column;
			this.ProposedValue = value;
		}

		// Token: 0x04000116 RID: 278
		private DataColumn _column;
	}
}
