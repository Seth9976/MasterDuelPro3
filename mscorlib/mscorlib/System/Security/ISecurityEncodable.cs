using System;

namespace System.Security
{
	/// <summary>Defines the methods that convert permission object state to and from XML element representation.</summary>
	// Token: 0x02000310 RID: 784
	public interface ISecurityEncodable
	{
		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06001C4F RID: 7247
		void FromXml(SecurityElement e);

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001C50 RID: 7248
		SecurityElement ToXml();
	}
}
