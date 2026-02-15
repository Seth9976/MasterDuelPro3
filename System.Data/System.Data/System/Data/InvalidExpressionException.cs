using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to add a <see cref="T:System.Data.DataColumn" /> that contains an invalid <see cref="P:System.Data.DataColumn.Expression" /> to a <see cref="T:System.Data.DataColumnCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class InvalidExpressionException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream. </param>
		// Token: 0x06000626 RID: 1574 RVA: 0x000044F9 File Offset: 0x000026F9
		protected InvalidExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class.</summary>
		// Token: 0x06000627 RID: 1575 RVA: 0x0001F73B File Offset: 0x0001D93B
		public InvalidExpressionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000628 RID: 1576 RVA: 0x0001F743 File Offset: 0x0001D943
		public InvalidExpressionException(string s)
			: base(s)
		{
		}
	}
}
