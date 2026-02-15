using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	/// <summary>The base class for all exceptions thrown on behalf of the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FA RID: 250
	[Serializable]
	public abstract class DbException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class.</summary>
		// Token: 0x06000D4D RID: 3405 RVA: 0x000451B9 File Offset: 0x000433B9
		protected DbException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message.</summary>
		/// <param name="message">The message to display for this exception.</param>
		// Token: 0x06000D4E RID: 3406 RVA: 0x000451C1 File Offset: 0x000433C1
		protected DbException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000D4F RID: 3407 RVA: 0x000451CA File Offset: 0x000433CA
		protected DbException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
