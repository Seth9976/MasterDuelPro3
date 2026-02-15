using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when you attempt to do work on a transaction that cannot accept new work.  </summary>
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class TransactionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class.</summary>
		// Token: 0x06000023 RID: 35 RVA: 0x0000232B File Offset: 0x0000052B
		public TransactionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x06000024 RID: 36 RVA: 0x00002333 File Offset: 0x00000533
		public TransactionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x06000025 RID: 37 RVA: 0x0000233C File Offset: 0x0000053C
		protected TransactionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
