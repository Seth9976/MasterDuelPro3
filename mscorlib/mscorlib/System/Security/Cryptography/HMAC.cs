using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of Hash-based Message Authentication Code (HMAC) must derive.</summary>
	// Token: 0x02000390 RID: 912
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		/// <summary>Gets or sets the block size to use in the hash value.</summary>
		/// <returns>The block size to use in the hash value.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x0007CC87 File Offset: 0x0007AE87
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x0007CC8F File Offset: 0x0007AE8F
		protected int BlockSizeValue
		{
			get
			{
				return this.blockSizeValue;
			}
			set
			{
				this.blockSizeValue = value;
			}
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0007CC98 File Offset: 0x0007AE98
		private void UpdateIOPadBuffers()
		{
			if (this.m_inner == null)
			{
				this.m_inner = new byte[this.BlockSizeValue];
			}
			if (this.m_outer == null)
			{
				this.m_outer = new byte[this.BlockSizeValue];
			}
			for (int i = 0; i < this.BlockSizeValue; i++)
			{
				this.m_inner[i] = 54;
				this.m_outer[i] = 92;
			}
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				byte[] inner = this.m_inner;
				int num = i;
				inner[num] ^= this.KeyValue[i];
				byte[] outer = this.m_outer;
				int num2 = i;
				outer[num2] ^= this.KeyValue[i];
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0007CD44 File Offset: 0x0007AF44
		internal void InitializeKey(byte[] key)
		{
			this.m_inner = null;
			this.m_outer = null;
			if (key.Length > this.BlockSizeValue)
			{
				this.KeyValue = this.m_hash1.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			this.UpdateIOPadBuffers();
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt is made to change the <see cref="P:System.Security.Cryptography.HMAC.Key" /> property after hashing has begun. </exception>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0007CD95 File Offset: 0x0007AF95
		// (set) Token: 0x06001F7C RID: 8060 RVA: 0x0007CDA7 File Offset: 0x0007AFA7
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash key cannot be changed after the first write to the stream."));
				}
				this.InitializeKey(value);
			}
		}

		/// <summary>Creates an instance of the default implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <returns>A new SHA-1 instance, unless the default settings have been changed by using the &lt;cryptoClass&gt; element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F7D RID: 8061 RVA: 0x0007CDC8 File Offset: 0x0007AFC8
		public static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		/// <summary>Creates an instance of the specified implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <returns>A new instance of the specified HMAC implementation.</returns>
		/// <param name="algorithmName">The HMAC implementation to use. The following table shows the valid values for the <paramref name="algorithmName" /> parameter and the algorithms they map to.Parameter valueImplements System.Security.Cryptography.HMAC<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.KeyedHashAlgorithm<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />System.Security.Cryptography.HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />System.Security.Cryptography.HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />System.Security.Cryptography.HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />System.Security.Cryptography.HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />System.Security.Cryptography.HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" />System.Security.Cryptography.MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
		// Token: 0x06001F7E RID: 8062 RVA: 0x0007CDD4 File Offset: 0x0007AFD4
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		/// <summary>Initializes an instance of the default implementation of <see cref="T:System.Security.Cryptography.HMAC" />.</summary>
		// Token: 0x06001F7F RID: 8063 RVA: 0x0007CDE1 File Offset: 0x0007AFE1
		public override void Initialize()
		{
			this.m_hash1.Initialize();
			this.m_hash2.Initialize();
			this.m_hashing = false;
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the default <see cref="T:System.Security.Cryptography.HMAC" /> hash algorithm for computing the hash value.</summary>
		/// <param name="rgb">The input data. </param>
		/// <param name="ib">The offset into the byte array from which to begin using data. </param>
		/// <param name="cb">The number of bytes in the array to use as data. </param>
		// Token: 0x06001F80 RID: 8064 RVA: 0x0007CE00 File Offset: 0x0007B000
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code in a byte array.</returns>
		// Token: 0x06001F81 RID: 8065 RVA: 0x0007CE50 File Offset: 0x0007B050
		protected override byte[] HashFinal()
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] hashValue = this.m_hash1.HashValue;
			this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
			this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
			this.m_hashing = false;
			this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			return this.m_hash2.HashValue;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HMAC" /> class when a key change is legitimate and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F82 RID: 8066 RVA: 0x0007CF08 File Offset: 0x0007B108
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_hash1 != null)
				{
					((IDisposable)this.m_hash1).Dispose();
				}
				if (this.m_hash2 != null)
				{
					((IDisposable)this.m_hash2).Dispose();
				}
				if (this.m_inner != null)
				{
					Array.Clear(this.m_inner, 0, this.m_inner.Length);
				}
				if (this.m_outer != null)
				{
					Array.Clear(this.m_outer, 0, this.m_outer.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0007CF80 File Offset: 0x0007B180
		internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				try
				{
					return createFipsHashAlgorithmCallback();
				}
				catch (PlatformNotSupportedException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return createStandardHashAlgorithmCallback();
		}

		// Token: 0x04000EC8 RID: 3784
		private int blockSizeValue = 64;

		// Token: 0x04000EC9 RID: 3785
		internal string m_hashName;

		// Token: 0x04000ECA RID: 3786
		internal HashAlgorithm m_hash1;

		// Token: 0x04000ECB RID: 3787
		internal HashAlgorithm m_hash2;

		// Token: 0x04000ECC RID: 3788
		private byte[] m_inner;

		// Token: 0x04000ECD RID: 3789
		private byte[] m_outer;

		// Token: 0x04000ECE RID: 3790
		private bool m_hashing;
	}
}
