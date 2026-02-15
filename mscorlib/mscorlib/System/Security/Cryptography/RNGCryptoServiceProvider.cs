using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Implements a cryptographic Random Number Generator (RNG) using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x020003C3 RID: 963
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x060020D6 RID: 8406 RVA: 0x00088B07 File Offset: 0x00086D07
		static RNGCryptoServiceProvider()
		{
			if (RNGCryptoServiceProvider.RngOpen())
			{
				RNGCryptoServiceProvider._lock = new object();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		// Token: 0x060020D7 RID: 8407 RVA: 0x00088B1A File Offset: 0x00086D1A
		public RNGCryptoServiceProvider()
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null, IntPtr.Zero);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="rgb">A byte array. This value is ignored.</param>
		// Token: 0x060020D8 RID: 8408 RVA: 0x00088B3C File Offset: 0x00086D3C
		public unsafe RNGCryptoServiceProvider(byte[] rgb)
		{
			fixed (byte[] array = rgb)
			{
				byte* ptr;
				if (rgb == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this._handle = RNGCryptoServiceProvider.RngInitialize(ptr, (rgb != null) ? ((IntPtr)rgb.Length) : IntPtr.Zero);
			}
			this.Check();
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00088B8D File Offset: 0x00086D8D
		private void Check()
		{
			if (this._handle == IntPtr.Zero)
			{
				throw new CryptographicException(Locale.GetText("Couldn't access random source."));
			}
		}

		// Token: 0x060020DA RID: 8410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RngOpen();

		// Token: 0x060020DB RID: 8411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr RngInitialize(byte* seed, IntPtr seed_length);

		// Token: 0x060020DC RID: 8412
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr RngGetBytes(IntPtr handle, byte* data, IntPtr data_length);

		// Token: 0x060020DD RID: 8413
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RngClose(IntPtr handle);

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060020DE RID: 8414 RVA: 0x00088BB4 File Offset: 0x00086DB4
		public unsafe override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			fixed (byte[] array = data)
			{
				byte* ptr;
				if (data == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (RNGCryptoServiceProvider._lock == null)
				{
					this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, ptr, (IntPtr)((long)data.Length));
				}
				else
				{
					object @lock = RNGCryptoServiceProvider._lock;
					lock (@lock)
					{
						this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, ptr, (IntPtr)((long)data.Length));
					}
				}
			}
			this.Check();
		}

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random nonzero values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random nonzero values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060020DF RID: 8415 RVA: 0x00088C58 File Offset: 0x00086E58
		public unsafe override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = new byte[(long)data.Length * 2L];
			long num = 0L;
			while (num < (long)data.Length)
			{
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, ptr, (IntPtr)((long)array.Length));
				array2 = null;
				this.Check();
				long num2 = 0L;
				while (num2 < (long)array.Length && num != (long)data.Length)
				{
					checked
					{
						if (array[(int)((IntPtr)num2)] != 0)
						{
							long num3 = num;
							num = unchecked(num3 + 1L);
							data[(int)((IntPtr)num3)] = array[(int)((IntPtr)num2)];
						}
					}
					num2 += 1L;
				}
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00088CF4 File Offset: 0x00086EF4
		~RNGCryptoServiceProvider()
		{
			if (this._handle != IntPtr.Zero)
			{
				RNGCryptoServiceProvider.RngClose(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00088D44 File Offset: 0x00086F44
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000F66 RID: 3942
		private static object _lock;

		// Token: 0x04000F67 RID: 3943
		private IntPtr _handle;
	}
}
