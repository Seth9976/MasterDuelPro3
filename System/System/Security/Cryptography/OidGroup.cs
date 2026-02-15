using System;

namespace System.Security.Cryptography
{
	/// <summary>Identifies Windows cryptographic object identifier (OID) groups.</summary>
	// Token: 0x020001AA RID: 426
	public enum OidGroup
	{
		/// <summary>All the groups.</summary>
		// Token: 0x04000789 RID: 1929
		All,
		/// <summary>The Windows group that is represented by CRYPT_HASH_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400078A RID: 1930
		HashAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_ENCRYPT_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400078B RID: 1931
		EncryptionAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_PUBKEY_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400078C RID: 1932
		PublicKeyAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_SIGN_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400078D RID: 1933
		SignatureAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_RDN_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x0400078E RID: 1934
		Attribute,
		/// <summary>The Windows group that is represented by CRYPT_EXT_OR_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x0400078F RID: 1935
		ExtensionOrAttribute,
		/// <summary>The Windows group that is represented by CRYPT_ENHKEY_USAGE_OID_GROUP_ID.</summary>
		// Token: 0x04000790 RID: 1936
		EnhancedKeyUsage,
		/// <summary>The Windows group that is represented by CRYPT_POLICY_OID_GROUP_ID.</summary>
		// Token: 0x04000791 RID: 1937
		Policy,
		/// <summary>The Windows group that is represented by CRYPT_TEMPLATE_OID_GROUP_ID.</summary>
		// Token: 0x04000792 RID: 1938
		Template,
		/// <summary>The Windows group that is represented by CRYPT_KDF_OID_GROUP_ID.</summary>
		// Token: 0x04000793 RID: 1939
		KeyDerivationFunction
	}
}
