using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all classes that derive byte sequences of a specified length inherit.</summary>
	// Token: 0x02000389 RID: 905
	[ComVisible(true)]
	public abstract class DeriveBytes : IDisposable
	{
		/// <summary>When overridden in a derived class, returns pseudo-random key bytes.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		// Token: 0x06001F52 RID: 8018
		public abstract byte[] GetBytes(int cb);

		/// <summary>When overridden in a derived class, releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class.</summary>
		// Token: 0x06001F53 RID: 8019 RVA: 0x0007C338 File Offset: 0x0007A538
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001F54 RID: 8020 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
