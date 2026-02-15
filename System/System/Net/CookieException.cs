using System;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made adding a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieContainer" />.</summary>
	// Token: 0x020003E9 RID: 1001
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class.</summary>
		// Token: 0x060018E1 RID: 6369 RVA: 0x0001A2B5 File Offset: 0x000184B5
		public CookieException()
		{
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0001A2BD File Offset: 0x000184BD
		internal CookieException(string message)
			: base(message)
		{
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0001A2C6 File Offset: 0x000184C6
		internal CookieException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class with specific values of <paramref name="serializationInfo" /> and <paramref name="streamingContext" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060018E4 RID: 6372 RVA: 0x0001A2D0 File Offset: 0x000184D0
		protected CookieException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060018E5 RID: 6373 RVA: 0x0001A2DA File Offset: 0x000184DA
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">The contextual information about the source or destination. A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060018E6 RID: 6374 RVA: 0x0001A2DA File Offset: 0x000184DA
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
