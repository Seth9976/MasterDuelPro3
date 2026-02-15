using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a field that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017E RID: 382
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class.</summary>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003A119 File Offset: 0x00038319
		public MissingFieldException()
			: base("Attempted to access a non-existing field.")
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003A131 File Offset: 0x00038331
		public MissingFieldException(string message)
			: base(message)
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with the specified class name and field name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent field was attempted. </param>
		/// <param name="fieldName">The name of the field that cannot be accessed. </param>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002975D File Offset: 0x0002795D
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00029773 File Offset: 0x00027973
		protected MissingFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the text string showing the signature of the missing field, the class name, and the field name. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0003A148 File Offset: 0x00038348
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format("Field '{0}' not found.", ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName);
			}
		}
	}
}
