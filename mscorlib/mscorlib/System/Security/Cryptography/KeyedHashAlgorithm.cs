using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of keyed hash algorithms must derive. </summary>
	// Token: 0x0200039B RID: 923
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001FAC RID: 8108 RVA: 0x0007D416 File Offset: 0x0007B616
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				this.KeyValue = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to change the <see cref="P:System.Security.Cryptography.KeyedHashAlgorithm.Key" /> property after hashing has begun. </exception>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0007CD95 File Offset: 0x0007AF95
		// (set) Token: 0x06001FAE RID: 8110 RVA: 0x0007D445 File Offset: 0x0007B645
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash key cannot be changed after the first write to the stream."));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		/// <summary>The key to use in the hash algorithm.</summary>
		// Token: 0x04000EE0 RID: 3808
		protected byte[] KeyValue;
	}
}
