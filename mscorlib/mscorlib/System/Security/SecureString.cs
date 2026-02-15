using System;

namespace System.Security
{
	/// <summary>Represents text that should be kept confidential. The text is encrypted for privacy when being used, and deleted from computer memory when no longer needed. This class cannot be inherited.</summary>
	// Token: 0x02000324 RID: 804
	[MonoTODO("work in progress - encryption is missing")]
	public sealed class SecureString : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this instance.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06001CB0 RID: 7344 RVA: 0x000701A0 File Offset: 0x0006E3A0
		public SecureString()
		{
			this.Alloc(8, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class from a subarray of <see cref="T:System.Char" /> objects.</summary>
		/// <param name="value">A pointer to an array of <see cref="T:System.Char" /> objects.</param>
		/// <param name="length">The number of elements of <paramref name="value" /> to include in the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than 65536.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x000701B0 File Offset: 0x0006E3B0
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			this.length = length;
			this.Alloc(length, false);
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = *(value++);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			this.Encrypt();
		}

		/// <summary>Gets the number of characters in the current secure string.</summary>
		/// <returns>The number of <see cref="T:System.Char" /> objects in this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00070238 File Offset: 0x0006E438
		public int Length
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SecureString");
				}
				return this.length;
			}
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Security.SecureString" /> object.</summary>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x00070253 File Offset: 0x0006E453
		public void Dispose()
		{
			this.disposed = true;
			if (this.data != null)
			{
				Array.Clear(this.data, 0, this.data.Length);
				this.data = null;
			}
			this.length = 0;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00070286 File Offset: 0x0006E486
		private void Encrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00070286 File Offset: 0x0006E486
		private void Decrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00070298 File Offset: 0x0006E498
		private void Alloc(int length, bool realloc)
		{
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			int num = (length >> 3) + (((length & 7) == 0) ? 0 : 1) << 4;
			if (realloc && this.data != null && num == this.data.Length)
			{
				return;
			}
			if (realloc)
			{
				byte[] array = new byte[num];
				Array.Copy(this.data, 0, array, 0, Math.Min(this.data.Length, array.Length));
				Array.Clear(this.data, 0, this.data.Length);
				this.data = array;
				return;
			}
			this.data = new byte[num];
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00070338 File Offset: 0x0006E538
		internal byte[] GetBuffer()
		{
			byte[] array = new byte[this.length << 1];
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, 0, array, 0, array.Length);
			}
			finally
			{
				this.Encrypt();
			}
			return array;
		}

		// Token: 0x04000D28 RID: 3368
		private int length;

		// Token: 0x04000D29 RID: 3369
		private bool disposed;

		// Token: 0x04000D2A RID: 3370
		private byte[] data;
	}
}
