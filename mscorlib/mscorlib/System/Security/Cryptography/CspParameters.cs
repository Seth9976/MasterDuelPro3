using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains parameters that are passed to the cryptographic service provider (CSP) that performs cryptographic computations. This class cannot be inherited.</summary>
	// Token: 0x02000388 RID: 904
	[ComVisible(true)]
	public sealed class CspParameters
	{
		/// <summary>Represents the flags for <see cref="T:System.Security.Cryptography.CspParameters" /> that modify the behavior of the cryptographic service provider (CSP).</summary>
		/// <returns>An enumeration value, or a bitwise combination of enumeration values.</returns>
		/// <exception cref="T:System.ArgumentException">Value is not a valid enumeration value.</exception>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0007C29A File Offset: 0x0007A49A
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x0007C2A4 File Offset: 0x0007A4A4
		public CspProviderFlags Flags
		{
			get
			{
				return (CspProviderFlags)this.m_flags;
			}
			set
			{
				int num = 255;
				if ((value & (CspProviderFlags)(~(CspProviderFlags)num)) != CspProviderFlags.NoFlags)
				{
					throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)value }), "value");
				}
				this.m_flags = (int)value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class.</summary>
		// Token: 0x06001F4E RID: 8014 RVA: 0x0007C2EA File Offset: 0x0007A4EA
		public CspParameters()
			: this(1, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create. </param>
		// Token: 0x06001F4F RID: 8015 RVA: 0x0007C2F5 File Offset: 0x0007A4F5
		public CspParameters(int dwTypeIn)
			: this(dwTypeIn, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name, and the specified container name.</summary>
		/// <param name="dwTypeIn">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name. </param>
		/// <param name="strContainerNameIn">A container name. </param>
		// Token: 0x06001F50 RID: 8016 RVA: 0x0007C300 File Offset: 0x0007A500
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
			: this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
		{
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0007C30C File Offset: 0x0007A50C
		internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
		{
			this.ProviderType = providerType;
			this.ProviderName = providerName;
			this.KeyContainerName = keyContainerName;
			this.KeyNumber = -1;
			this.Flags = flags;
		}

		/// <summary>Represents the provider type code for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04000EB5 RID: 3765
		public int ProviderType;

		/// <summary>Represents the provider name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04000EB6 RID: 3766
		public string ProviderName;

		/// <summary>Represents the key container name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04000EB7 RID: 3767
		public string KeyContainerName;

		/// <summary>Specifies whether an asymmetric key is created as a signature key or an exchange key.</summary>
		// Token: 0x04000EB8 RID: 3768
		public int KeyNumber;

		// Token: 0x04000EB9 RID: 3769
		private int m_flags;
	}
}
