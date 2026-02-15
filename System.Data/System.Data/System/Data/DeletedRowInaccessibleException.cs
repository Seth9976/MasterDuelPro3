using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when an action is tried on a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class DeletedRowInaccessibleException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000AC RID: 172 RVA: 0x000044F9 File Offset: 0x000026F9
		protected DeletedRowInaccessibleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class.</summary>
		// Token: 0x060000AD RID: 173 RVA: 0x0000452F File Offset: 0x0000272F
		public DeletedRowInaccessibleException()
			: base("Deleted rows inaccessible.")
		{
			base.HResult = -2146232031;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000AE RID: 174 RVA: 0x00004547 File Offset: 0x00002747
		public DeletedRowInaccessibleException(string s)
			: base(s)
		{
			base.HResult = -2146232031;
		}
	}
}
