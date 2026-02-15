using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you call the <see cref="M:System.Data.DataRow.EndEdit" /> method within the <see cref="E:System.Data.DataTable.RowChanging" /> event.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class InRowChangingEventException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000B2 RID: 178 RVA: 0x000044F9 File Offset: 0x000026F9
		protected InRowChangingEventException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class.</summary>
		// Token: 0x060000B3 RID: 179 RVA: 0x00004587 File Offset: 0x00002787
		public InRowChangingEventException()
			: base("Operation not supported in the RowChanging event.")
		{
			base.HResult = -2146232029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000B4 RID: 180 RVA: 0x0000459F File Offset: 0x0000279F
		public InRowChangingEventException(string s)
			: base(s)
		{
			base.HResult = -2146232029;
		}
	}
}
