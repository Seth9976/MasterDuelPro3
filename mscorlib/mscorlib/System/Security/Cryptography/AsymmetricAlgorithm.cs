using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric algorithms must inherit.</summary>
	// Token: 0x0200037F RID: 895
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x06001F2E RID: 7982 RVA: 0x0007C077 File Offset: 0x0007A277
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x06001F2F RID: 7983 RVA: 0x0007C07F File Offset: 0x0007A27F
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F30 RID: 7984 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets or sets the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		/// <returns>The size, in bits, of the key modulus used by the asymmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key modulus size is invalid. </exception>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0007C08E File Offset: 0x0007A28E
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x0007C098 File Offset: 0x0007A298
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalKeySizesValue.Length; i++)
				{
					if (this.LegalKeySizesValue[i].SkipSize == 0)
					{
						if (this.LegalKeySizesValue[i].MinSize == value)
						{
							this.KeySizeValue = value;
							return;
						}
					}
					else
					{
						for (int j = this.LegalKeySizesValue[i].MinSize; j <= this.LegalKeySizesValue[i].MaxSize; j += this.LegalKeySizesValue[i].SkipSize)
						{
							if (j == value)
							{
								this.KeySizeValue = value;
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
		}

		/// <summary>When overridden in a derived class, reconstructs an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object from an XML string.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object. </param>
		// Token: 0x06001F33 RID: 7987 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual void FromXmlString(string xmlString)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</summary>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x06001F34 RID: 7988 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		// Token: 0x04000E9A RID: 3738
		protected int KeySizeValue;

		/// <summary>Specifies the key sizes that are supported by the asymmetric algorithm.</summary>
		// Token: 0x04000E9B RID: 3739
		protected KeySizes[] LegalKeySizesValue;
	}
}
