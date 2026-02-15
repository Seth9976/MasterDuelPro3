using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BD RID: 189
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class.</summary>
		// Token: 0x060004A1 RID: 1185 RVA: 0x0001886C File Offset: 0x00016A6C
		public ArgumentNullException()
			: base("Value cannot be null.")
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		// Token: 0x060004A2 RID: 1186 RVA: 0x00018884 File Offset: 0x00016A84
		public ArgumentNullException(string paramName)
			: base("Value cannot be null.", paramName)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="message">A message that describes the error. </param>
		// Token: 0x060004A3 RID: 1187 RVA: 0x0001889D File Offset: 0x00016A9D
		public ArgumentNullException(string paramName, string message)
			: base(message, paramName)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		// Token: 0x060004A4 RID: 1188 RVA: 0x000188B2 File Offset: 0x00016AB2
		protected ArgumentNullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
