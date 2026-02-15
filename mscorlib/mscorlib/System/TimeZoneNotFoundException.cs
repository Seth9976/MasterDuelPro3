using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a time zone cannot be found.</summary>
	// Token: 0x02000151 RID: 337
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with a system-supplied message.</summary>
		// Token: 0x06000B6F RID: 2927 RVA: 0x00028040 File Offset: 0x00026240
		public TimeZoneNotFoundException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.-or-The <paramref name="context" /> parameter is null.</exception>
		// Token: 0x06000B70 RID: 2928 RVA: 0x0001876A File Offset: 0x0001696A
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
