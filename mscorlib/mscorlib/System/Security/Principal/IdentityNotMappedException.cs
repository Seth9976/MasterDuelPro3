using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	/// <summary>Represents an exception for a principal whose identity could not be mapped to a known identity.</summary>
	// Token: 0x020003D5 RID: 981
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class.</summary>
		// Token: 0x06002156 RID: 8534 RVA: 0x0008A6B6 File Offset: 0x000888B6
		public IdentityNotMappedException()
			: base(Locale.GetText("Couldn't translate some identities."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002157 RID: 8535 RVA: 0x000536B7 File Offset: 0x000518B7
		public IdentityNotMappedException(string message)
			: base(message)
		{
		}

		/// <summary>Gets serialization information with the data needed to create an instance of this <see cref="T:System.Security.Principal.IdentityNotMappedException" /> object. </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization." /><see cref="SerializationInfoobject" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.SerializationInfo." /><see cref="StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06002158 RID: 8536 RVA: 0x00002C89 File Offset: 0x00000E89
		[MonoTODO("not implemented")]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}
	}
}
