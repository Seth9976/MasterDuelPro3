using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class represents the extended protection policy used by the server to validate incoming client connections. </summary>
	// Token: 0x020001A1 RID: 417
	[MonoTODO]
	[TypeConverter(typeof(ExtendedProtectionPolicyTypeConverter))]
	[Serializable]
	public class ExtendedProtectionPolicy : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		// Token: 0x060009EF RID: 2543 RVA: 0x000026E5 File Offset: 0x000008E5
		[MonoTODO("Not implemented.")]
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class from a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the required data to populate the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</param>
		// Token: 0x060009F0 RID: 2544 RVA: 0x00033B17 File Offset: 0x00031D17
		protected ExtendedProtectionPolicy(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a string representation for the extended protection policy instance.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the representation of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</returns>
		// Token: 0x060009F1 RID: 2545 RVA: 0x00033B24 File Offset: 0x00031D24
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized data for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</param>
		// Token: 0x060009F2 RID: 2546 RVA: 0x0000A4AB File Offset: 0x000086AB
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
}
