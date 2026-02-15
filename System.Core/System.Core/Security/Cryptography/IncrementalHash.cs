using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000013 RID: 19
	public sealed class IncrementalHash : IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002093 File Offset: 0x00000293
		private IncrementalHash(HashAlgorithmName name, HashAlgorithm hash)
		{
			this._algorithmName = name;
			this._hash = hash;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000020AC File Offset: 0x000002AC
		public void AppendData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non negative number is required.");
			}
			if (count < 0 || count > data.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (data.Length - count < offset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
				this._resetPending = false;
			}
			this._hash.TransformBlock(data, offset, count, null, 0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000214C File Offset: 0x0000034C
		public byte[] GetHashAndReset()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
			}
			this._hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			byte[] hash = this._hash.Hash;
			this._resetPending = true;
			return hash;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000021AE File Offset: 0x000003AE
		public void Dispose()
		{
			this._disposed = true;
			if (this._hash != null)
			{
				this._hash.Dispose();
				this._hash = null;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000021D1 File Offset: 0x000003D1
		public static IncrementalHash CreateHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHMAC(hashAlgorithm, key));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000220C File Offset: 0x0000040C
		private static HashAlgorithm GetHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new HMACMD5(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(key);
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x04000001 RID: 1
		private readonly HashAlgorithmName _algorithmName;

		// Token: 0x04000002 RID: 2
		private HashAlgorithm _hash;

		// Token: 0x04000003 RID: 3
		private bool _disposed;

		// Token: 0x04000004 RID: 4
		private bool _resetPending;
	}
}
