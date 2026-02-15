using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> contains a syntax error.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class SyntaxErrorException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a specific serialized stream. </param>
		// Token: 0x0600062C RID: 1580 RVA: 0x0001F74C File Offset: 0x0001D94C
		protected SyntaxErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class.</summary>
		// Token: 0x0600062D RID: 1581 RVA: 0x0001F756 File Offset: 0x0001D956
		public SyntaxErrorException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x0600062E RID: 1582 RVA: 0x0001F75E File Offset: 0x0001D95E
		public SyntaxErrorException(string s)
			: base(s)
		{
		}
	}
}
