using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of cryptographic random number generators derive.</summary>
	// Token: 0x0200039F RID: 927
	[ComVisible(true)]
	public abstract class RandomNumberGenerator : IDisposable
	{
		/// <summary>When overridden in a derived class, creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001FC7 RID: 8135 RVA: 0x0007D870 File Offset: 0x0007BA70
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		/// <summary>When overridden in a derived class, creates an instance of the specified implementation of a cryptographic random number generator.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <param name="rngName">The name of the random number generator implementation to use. </param>
		// Token: 0x06001FC8 RID: 8136 RVA: 0x0007D87C File Offset: 0x0007BA7C
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		/// <summary>When overridden in a derived class, releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> class.</summary>
		// Token: 0x06001FC9 RID: 8137 RVA: 0x0007D889 File Offset: 0x0007BA89
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001FCA RID: 8138 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random bytes. </param>
		// Token: 0x06001FCB RID: 8139
		public abstract void GetBytes(byte[] data);

		// Token: 0x06001FCC RID: 8140 RVA: 0x0007D898 File Offset: 0x0007BA98
		public virtual void GetBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (offset + count > data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count > 0)
			{
				byte[] array = new byte[count];
				this.GetBytes(array);
				Array.Copy(array, 0, data, offset, count);
			}
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of nonzero values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random nonzero bytes. </param>
		// Token: 0x06001FCD RID: 8141 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}
	}
}
