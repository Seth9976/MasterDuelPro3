using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when time zone information is invalid.</summary>
	// Token: 0x02000114 RID: 276
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a system-supplied message.</summary>
		// Token: 0x06000913 RID: 2323 RVA: 0x00028040 File Offset: 0x00026240
		public InvalidTimeZoneException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x06000914 RID: 2324 RVA: 0x00028048 File Offset: 0x00026248
		public InvalidTimeZoneException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data. </param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.-or-The <paramref name="context" /> parameter is null.</exception>
		// Token: 0x06000915 RID: 2325 RVA: 0x0001876A File Offset: 0x0001696A
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
