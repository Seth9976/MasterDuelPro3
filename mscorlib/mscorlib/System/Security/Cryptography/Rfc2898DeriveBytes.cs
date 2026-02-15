using System;
using System.Buffers;
using System.Text;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Implements password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on <see cref="T:System.Security.Cryptography.HMACSHA1" />.</summary>
	// Token: 0x02000373 RID: 883
	public class Rfc2898DeriveBytes : DeriveBytes
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00079EDF File Offset: 0x000780DF
		public HashAlgorithmName HashAlgorithm { get; }

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00079EE8 File Offset: 0x000780E8
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
		{
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			if (salt.Length < 8)
			{
				throw new ArgumentException("Salt is not at least eight bytes.", "salt");
			}
			if (iterations <= 0)
			{
				throw new ArgumentOutOfRangeException("iterations", "Positive number required.");
			}
			if (password == null)
			{
				throw new NullReferenceException();
			}
			this._salt = salt.CloneByteArray();
			this._iterations = (uint)iterations;
			this._password = password.CloneByteArray();
			this.HashAlgorithm = hashAlgorithm;
			this._hmac = this.OpenHmac();
			this._blockSize = this._hmac.HashSize >> 3;
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x06001ED2 RID: 7890 RVA: 0x00079F88 File Offset: 0x00078188
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
			: this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00079F98 File Offset: 0x00078198
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
			: this(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001ED4 RID: 7892 RVA: 0x00079FB0 File Offset: 0x000781B0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._hmac != null)
				{
					this._hmac.Dispose();
					this._hmac = null;
				}
				if (this._buffer != null)
				{
					Array.Clear(this._buffer, 0, this._buffer.Length);
				}
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				if (this._salt != null)
				{
					Array.Clear(this._salt, 0, this._salt.Length);
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Returns the pseudo-random key for this object.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cb " />is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x06001ED5 RID: 7893 RVA: 0x0007A038 File Offset: 0x00078238
		public override byte[] GetBytes(int cb)
		{
			if (cb <= 0)
			{
				throw new ArgumentOutOfRangeException("cb", "Positive number required.");
			}
			byte[] array = new byte[cb];
			int i = 0;
			int num = this._endIndex - this._startIndex;
			if (num > 0)
			{
				if (cb < num)
				{
					Buffer.BlockCopy(this._buffer, this._startIndex, array, 0, cb);
					this._startIndex += cb;
					return array;
				}
				Buffer.BlockCopy(this._buffer, this._startIndex, array, 0, num);
				this._startIndex = (this._endIndex = 0);
				i += num;
			}
			while (i < cb)
			{
				byte[] array2 = this.Func();
				int num2 = cb - i;
				if (num2 <= this._blockSize)
				{
					Buffer.BlockCopy(array2, 0, array, i, num2);
					i += num2;
					Buffer.BlockCopy(array2, num2, this._buffer, this._startIndex, this._blockSize - num2);
					this._endIndex += this._blockSize - num2;
					return array;
				}
				Buffer.BlockCopy(array2, 0, array, i, this._blockSize);
				i += this._blockSize;
			}
			return array;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0007A14C File Offset: 0x0007834C
		private HMAC OpenHmac()
		{
			HashAlgorithmName hashAlgorithm = this.HashAlgorithm;
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new CryptographicException("The hash algorithm name cannot be null or empty.");
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(this._password);
			}
			throw new CryptographicException(SR.Format("'{0}' is not a known hash algorithm.", hashAlgorithm.Name));
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0007A1F4 File Offset: 0x000783F4
		private void Initialize()
		{
			if (this._buffer != null)
			{
				Array.Clear(this._buffer, 0, this._buffer.Length);
			}
			this._buffer = new byte[this._blockSize];
			this._block = 1U;
			this._startIndex = (this._endIndex = 0);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0007A248 File Offset: 0x00078448
		private byte[] Func()
		{
			byte[] array = new byte[this._salt.Length + 4];
			Buffer.BlockCopy(this._salt, 0, array, 0, this._salt.Length);
			Helpers.WriteInt(this._block, array, this._salt.Length);
			byte[] array2 = ArrayPool<byte>.Shared.Rent(this._blockSize);
			byte[] array5;
			try
			{
				Span<byte> span = new Span<byte>(array2, 0, this._blockSize);
				int num;
				if (!this._hmac.TryComputeHash(array, span, out num) || num != this._blockSize)
				{
					throw new CryptographicException();
				}
				byte[] array3 = new byte[this._blockSize];
				span.CopyTo(array3);
				int num2 = 2;
				while ((long)num2 <= (long)((ulong)this._iterations))
				{
					if (!this._hmac.TryComputeHash(span, span, out num) || num != this._blockSize)
					{
						throw new CryptographicException();
					}
					for (int i = 0; i < this._blockSize; i++)
					{
						byte[] array4 = array3;
						int num3 = i;
						array4[num3] ^= array2[i];
					}
					num2++;
				}
				this._block += 1U;
				array5 = array3;
			}
			finally
			{
				Array.Clear(array2, 0, this._blockSize);
				ArrayPool<byte>.Shared.Return(array2, false);
			}
			return array5;
		}

		// Token: 0x04000E46 RID: 3654
		private readonly byte[] _password;

		// Token: 0x04000E47 RID: 3655
		private byte[] _salt;

		// Token: 0x04000E48 RID: 3656
		private uint _iterations;

		// Token: 0x04000E49 RID: 3657
		private HMAC _hmac;

		// Token: 0x04000E4A RID: 3658
		private int _blockSize;

		// Token: 0x04000E4B RID: 3659
		private byte[] _buffer;

		// Token: 0x04000E4C RID: 3660
		private uint _block;

		// Token: 0x04000E4D RID: 3661
		private int _startIndex;

		// Token: 0x04000E4E RID: 3662
		private int _endIndex;
	}
}
