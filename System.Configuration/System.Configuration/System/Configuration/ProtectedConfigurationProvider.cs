using System;
using System.Configuration.Provider;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Is the base class to create providers for encrypting and decrypting protected-configuration data.</summary>
	// Token: 0x02000039 RID: 57
	public abstract class ProtectedConfigurationProvider : ProviderBase
	{
		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing decrypted data.</returns>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt.</param>
		// Token: 0x06000181 RID: 385
		public abstract XmlNode Decrypt(XmlNode encryptedNode);
	}
}
