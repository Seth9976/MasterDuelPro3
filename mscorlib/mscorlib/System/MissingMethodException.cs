using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a method that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000122 RID: 290
	[Serializable]
	public class MissingMethodException : MissingMemberException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class.</summary>
		// Token: 0x060009A8 RID: 2472 RVA: 0x00029731 File Offset: 0x00027931
		public MissingMethodException()
			: base("Attempted to access a missing method.")
		{
			base.HResult = -2146233069;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x060009A9 RID: 2473 RVA: 0x00029749 File Offset: 0x00027949
		public MissingMethodException(string message)
			: base(message)
		{
			base.HResult = -2146233069;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with the specified class name and method name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent method was attempted. </param>
		/// <param name="methodName">The name of the method that cannot be accessed. </param>
		// Token: 0x060009AA RID: 2474 RVA: 0x0002975D File Offset: 0x0002795D
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060009AB RID: 2475 RVA: 0x00029773 File Offset: 0x00027973
		protected MissingMethodException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the text string showing the class name, the method name, and the signature of the missing method. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00029780 File Offset: 0x00027980
		public override string Message
		{
			get
			{
				if (this.ClassName != null)
				{
					return SR.Format("Method '{0}' not found.", this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
				}
				return base.Message;
			}
		}
	}
}
