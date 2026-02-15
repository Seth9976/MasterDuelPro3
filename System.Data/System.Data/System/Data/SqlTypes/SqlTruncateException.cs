using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when you set a value into a <see cref="N:System.Data.SqlTypes" /> structure would truncate that value.</summary>
	// Token: 0x020000D2 RID: 210
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class.</summary>
		// Token: 0x06000B0E RID: 2830 RVA: 0x0003E249 File Offset: 0x0003C449
		public SqlTruncateException()
			: this(SQLResource.TruncationMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000B0F RID: 2831 RVA: 0x0003E257 File Offset: 0x0003C457
		public SqlTruncateException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message and a reference to the <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="e">A reference to an inner <see cref="T:System.Exception" />. </param>
		// Token: 0x06000B10 RID: 2832 RVA: 0x0003E261 File Offset: 0x0003C461
		public SqlTruncateException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232014;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003E276 File Offset: 0x0003C476
		private SqlTruncateException(SerializationInfo si, StreamingContext sc)
			: base(SqlTruncateException.SqlTruncateExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003E286 File Offset: 0x0003C486
		private static SerializationInfo SqlTruncateExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTruncateException(si.GetString("SqlTruncateExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
