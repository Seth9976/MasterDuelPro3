using System;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made while using a network protocol.</summary>
	// Token: 0x020003B8 RID: 952
	[Serializable]
	public class ProtocolViolationException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class.</summary>
		// Token: 0x060017A4 RID: 6052 RVA: 0x00064AD7 File Offset: 0x00062CD7
		public ProtocolViolationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class with the specified message.</summary>
		/// <param name="message">The error message string. </param>
		// Token: 0x060017A5 RID: 6053 RVA: 0x00064ADF File Offset: 0x00062CDF
		public ProtocolViolationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information that is required to deserialize the <see cref="T:System.Net.ProtocolViolationException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.ProtocolViolationException" />. </param>
		// Token: 0x060017A6 RID: 6054 RVA: 0x00064AE8 File Offset: 0x00062CE8
		protected ProtocolViolationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.ProtocolViolationException" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060017A7 RID: 6055 RVA: 0x0001A2DA File Offset: 0x000184DA
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060017A8 RID: 6056 RVA: 0x0001A2DA File Offset: 0x000184DA
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
