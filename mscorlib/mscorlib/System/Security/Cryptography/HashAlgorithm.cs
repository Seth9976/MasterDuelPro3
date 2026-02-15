using System;
using System.Buffers;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of cryptographic hash algorithms must derive.</summary>
	// Token: 0x0200037C RID: 892
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		/// <summary>Creates an instance of the specified implementation of a hash algorithm.</summary>
		/// <returns>A new instance of the specified hash algorithm, or null if <paramref name="hashName" /> is not a valid hash algorithm.</returns>
		/// <param name="hashName">The hash algorithm implementation to use. The following table shows the valid values for the <paramref name="hashName" /> parameter and the algorithms they map to. Parameter value Implements SHA <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.HashAlgorithm <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />System.Security.Cryptography.MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA-256 <see cref="T:System.Security.Cryptography.SHA256Managed" />System.Security.Cryptography.SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA-384 <see cref="T:System.Security.Cryptography.SHA384Managed" />System.Security.Cryptography.SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" />SHA-512 <see cref="T:System.Security.Cryptography.SHA512Managed" />System.Security.Cryptography.SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" /></param>
		// Token: 0x06001F08 RID: 7944 RVA: 0x0007BBD8 File Offset: 0x00079DD8
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfigForwarder.CreateFromName(hashName);
		}

		/// <summary>Gets the size, in bits, of the computed hash code.</summary>
		/// <returns>The size, in bits, of the computed hash code.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0007BBE5 File Offset: 0x00079DE5
		public virtual int HashSize
		{
			get
			{
				return this.HashSizeValue;
			}
		}

		/// <summary>Gets the value of the computed hash code.</summary>
		/// <returns>The current value of the computed hash code.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
		///   <see cref="F:System.Security.Cryptography.HashAlgorithm.HashValue" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0007BBED File Offset: 0x00079DED
		public virtual byte[] Hash
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.State != 0)
				{
					throw new CryptographicUnexpectedOperationException("Hash must be finalized before the hash value is retrieved.");
				}
				byte[] hashValue = this.HashValue;
				return (byte[])((hashValue != null) ? hashValue.Clone() : null);
			}
		}

		/// <summary>Computes the hash value for the specified byte array.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="buffer">The input to compute the hash code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001F0B RID: 7947 RVA: 0x0007BC28 File Offset: 0x00079E28
		public byte[] ComputeHash(byte[] buffer)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.HashCore(buffer, 0, buffer.Length);
			return this.CaptureHashCodeAndReinitialize();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0007BC58 File Offset: 0x00079E58
		public bool TryComputeHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (destination.Length < this.HashSizeValue / 8)
			{
				bytesWritten = 0;
				return false;
			}
			this.HashCore(source);
			if (!this.TryHashFinal(destination, out bytesWritten))
			{
				throw new InvalidOperationException("The algorithm's implementation is incorrect.");
			}
			this.HashValue = null;
			this.Initialize();
			return true;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="buffer">The input to compute the hash code for. </param>
		/// <param name="offset">The offset into the byte array from which to begin using data. </param>
		/// <param name="count">The number of bytes in the array to use as data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is an invalid value.-or-<paramref name="buffer" /> length is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001F0D RID: 7949 RVA: 0x0007BCB4 File Offset: 0x00079EB4
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentException("Argument {0} should be larger than {1}.");
			}
			if (buffer.Length - count < offset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(buffer, offset, count);
			return this.CaptureHashCodeAndReinitialize();
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0007BD29 File Offset: 0x00079F29
		private byte[] CaptureHashCodeAndReinitialize()
		{
			this.HashValue = this.HashFinal();
			byte[] array = (byte[])this.HashValue.Clone();
			this.Initialize();
			return array;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06001F0F RID: 7951 RVA: 0x0007BD4D File Offset: 0x00079F4D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06001F10 RID: 7952 RVA: 0x0007BD5C File Offset: 0x00079F5C
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F11 RID: 7953 RVA: 0x0007BD64 File Offset: 0x00079F64
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._disposed = true;
			}
		}

		/// <summary>When overridden in a derived class, gets the input block size.</summary>
		/// <returns>The input block size.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0000C091 File Offset: 0x0000A291
		public virtual int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets the output block size.</summary>
		/// <returns>The output block size.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0000C091 File Offset: 0x0000A291
		public virtual int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0000C091 File Offset: 0x0000A291
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Computes the hash value for the specified region of the input byte array and copies the specified region of the input byte array to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute the hash code for. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">A copy of the part of the input array used to compute the hash code. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001F15 RID: 7957 RVA: 0x0007BD70 File Offset: 0x00079F70
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.ValidateTransformBlock(inputBuffer, inputOffset, inputCount);
			this.State = 1;
			this.HashCore(inputBuffer, inputOffset, inputCount);
			if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <returns>An array that is a copy of the part of the input that is hashed.</returns>
		/// <param name="inputBuffer">The input to compute the hash code for. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001F16 RID: 7958 RVA: 0x0007BDA8 File Offset: 0x00079FA8
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.ValidateTransformBlock(inputBuffer, inputOffset, inputCount);
			this.HashCore(inputBuffer, inputOffset, inputCount);
			this.HashValue = this.CaptureHashCodeAndReinitialize();
			byte[] array;
			if (inputCount != 0)
			{
				array = new byte[inputCount];
				Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			}
			else
			{
				array = Array.Empty<byte>();
			}
			this.State = 0;
			return array;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0007BDF8 File Offset: 0x00079FF8
		private void ValidateTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "Non-negative number required.");
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException("Argument {0} should be larger than {1}.");
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.</summary>
		/// <param name="array">The input to compute the hash code for. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the byte array to use as data. </param>
		// Token: 0x06001F18 RID: 7960
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x06001F19 RID: 7961
		protected abstract byte[] HashFinal();

		/// <summary>Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06001F1A RID: 7962
		public abstract void Initialize();

		// Token: 0x06001F1B RID: 7963 RVA: 0x0007BE60 File Offset: 0x0007A060
		protected virtual void HashCore(ReadOnlySpan<byte> source)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(source.Length);
			try
			{
				source.CopyTo(array);
				this.HashCore(array, 0, source.Length);
			}
			finally
			{
				Array.Clear(array, 0, source.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0007BEC8 File Offset: 0x0007A0C8
		protected virtual bool TryHashFinal(Span<byte> destination, out int bytesWritten)
		{
			int num = this.HashSizeValue / 8;
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			byte[] array = this.HashFinal();
			if (array.Length == num)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			throw new InvalidOperationException("The algorithm's implementation is incorrect.");
		}

		// Token: 0x04000E93 RID: 3731
		private bool _disposed;

		/// <summary>Represents the size, in bits, of the computed hash code.</summary>
		// Token: 0x04000E94 RID: 3732
		protected int HashSizeValue;

		/// <summary>Represents the value of the computed hash code.</summary>
		// Token: 0x04000E95 RID: 3733
		protected internal byte[] HashValue;

		/// <summary>Represents the state of the hash computation.</summary>
		// Token: 0x04000E96 RID: 3734
		protected int State;
	}
}
