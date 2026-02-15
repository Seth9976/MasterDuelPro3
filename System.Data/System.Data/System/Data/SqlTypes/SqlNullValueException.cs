using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</summary>
	// Token: 0x020000D1 RID: 209
	[Serializable]
	public sealed class SqlNullValueException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000B09 RID: 2825 RVA: 0x0003E1E6 File Offset: 0x0003C3E6
		public SqlNullValueException()
			: this(SQLResource.NullValueMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000B0A RID: 2826 RVA: 0x0003E1F4 File Offset: 0x0003C3F4
		public SqlNullValueException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000B0B RID: 2827 RVA: 0x0003E1FE File Offset: 0x0003C3FE
		public SqlNullValueException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0003E213 File Offset: 0x0003C413
		private SqlNullValueException(SerializationInfo si, StreamingContext sc)
			: base(SqlNullValueException.SqlNullValueExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003E223 File Offset: 0x0003C423
		private static SerializationInfo SqlNullValueExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlNullValueException(si.GetString("SqlNullValueExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
