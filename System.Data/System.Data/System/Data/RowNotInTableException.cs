using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to perform an operation on a <see cref="T:System.Data.DataRow" /> that is not in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class RowNotInTableException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000BE RID: 190 RVA: 0x000044F9 File Offset: 0x000026F9
		protected RowNotInTableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class.</summary>
		// Token: 0x060000BF RID: 191 RVA: 0x00004637 File Offset: 0x00002837
		public RowNotInTableException()
			: base("Row not found in table.")
		{
			base.HResult = -2146232024;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000C0 RID: 192 RVA: 0x0000464F File Offset: 0x0000284F
		public RowNotInTableException(string s)
			: base(s)
		{
			base.HResult = -2146232024;
		}
	}
}
