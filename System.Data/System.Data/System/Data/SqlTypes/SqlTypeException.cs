using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The base exception class for the <see cref="N:System.Data.SqlTypes" />.</summary>
	// Token: 0x020000D0 RID: 208
	[Serializable]
	public class SqlTypeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class.</summary>
		// Token: 0x06000B04 RID: 2820 RVA: 0x0003E183 File Offset: 0x0003C383
		public SqlTypeException()
			: this("SqlType error.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000B05 RID: 2821 RVA: 0x0003E191 File Offset: 0x0003C391
		public SqlTypeException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000B06 RID: 2822 RVA: 0x0003E19B File Offset: 0x0003C39B
		public SqlTypeException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232016;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with serialized data.</summary>
		/// <param name="si">The object that holds the serialized object data. </param>
		/// <param name="sc">The contextual information about the source or destination. </param>
		// Token: 0x06000B07 RID: 2823 RVA: 0x0003E1B0 File Offset: 0x0003C3B0
		protected SqlTypeException(SerializationInfo si, StreamingContext sc)
			: base(SqlTypeException.SqlTypeExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0003E1C0 File Offset: 0x0003C3C0
		private static SerializationInfo SqlTypeExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTypeException(si.GetString("SqlTypeExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
