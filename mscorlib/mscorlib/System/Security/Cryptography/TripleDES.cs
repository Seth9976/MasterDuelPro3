using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class for Triple Data Encryption Standard algorithms from which all <see cref="T:System.Security.Cryptography.TripleDES" /> implementations must derive.</summary>
	// Token: 0x020003BB RID: 955
	[ComVisible(true)]
	public abstract class TripleDES : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDES" /> class.</summary>
		// Token: 0x0600208D RID: 8333 RVA: 0x00084713 File Offset: 0x00082913
		protected TripleDES()
		{
			this.KeySizeValue = 192;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = TripleDES.s_legalBlockSizes;
			this.LegalKeySizesValue = TripleDES.s_legalKeySizes;
		}

		/// <summary>Gets or sets the secret key for the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>The secret key for the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set a key whose length is invalid.-or- An attempt was made to set a weak key (see <see cref="M:System.Security.Cryptography.TripleDES.IsWeakKey(System.Byte[])" />). </exception>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00084750 File Offset: 0x00082950
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x00084780 File Offset: 0x00082980
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					do
					{
						this.GenerateKey();
					}
					while (TripleDES.IsWeakKey(this.KeyValue));
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!base.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				if (TripleDES.IsWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "TripleDES");
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		/// <summary>Creates an instance of a cryptographic object to perform the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002090 RID: 8336 RVA: 0x000847F1 File Offset: 0x000829F1
		public static TripleDES Create()
		{
			return TripleDES.Create("System.Security.Cryptography.TripleDES");
		}

		/// <summary>Creates an instance of a cryptographic object to perform the specified implementation of the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		/// <param name="str">The name of the specific implementation of <see cref="T:System.Security.Cryptography.TripleDES" /> to use. </param>
		// Token: 0x06002091 RID: 8337 RVA: 0x000847FD File Offset: 0x000829FD
		public new static TripleDES Create(string str)
		{
			return (TripleDES)CryptoConfig.CreateFromName(str);
		}

		/// <summary>Determines whether the specified key is weak.</summary>
		/// <returns>true if the key is weak; otherwise, false.</returns>
		/// <param name="rgbKey">The secret key to test for weakness. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the <paramref name="rgbKey" /> parameter is not valid. </exception>
		// Token: 0x06002092 RID: 8338 RVA: 0x0008480C File Offset: 0x00082A0C
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (!TripleDES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
			byte[] array = Utils.FixupKeyParity(rgbKey);
			return TripleDES.EqualBytes(array, 0, 8, 8) || (array.Length == 24 && TripleDES.EqualBytes(array, 8, 16, 8));
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0008485C File Offset: 0x00082A5C
		private static bool EqualBytes(byte[] rgbKey, int start1, int start2, int count)
		{
			if (start1 < 0)
			{
				throw new ArgumentOutOfRangeException("start1", Environment.GetResourceString("Non-negative number required."));
			}
			if (start2 < 0)
			{
				throw new ArgumentOutOfRangeException("start2", Environment.GetResourceString("Non-negative number required."));
			}
			if (start1 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (start2 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			for (int i = 0; i < count; i++)
			{
				if (rgbKey[start1 + i] != rgbKey[start2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000848E6 File Offset: 0x00082AE6
		private static bool IsLegalKeySize(byte[] rgbKey)
		{
			return rgbKey != null && (rgbKey.Length == 16 || rgbKey.Length == 24);
		}

		// Token: 0x04000F40 RID: 3904
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000F41 RID: 3905
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 192, 64)
		};
	}
}
