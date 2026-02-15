using System;

namespace System.Transactions
{
	/// <summary>Contains additional information that specifies transaction behaviors.</summary>
	// Token: 0x02000010 RID: 16
	public struct TransactionOptions
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000023C1 File Offset: 0x000005C1
		internal TransactionOptions(IsolationLevel level, TimeSpan timeout)
		{
			this.level = level;
			this.timeout = timeout;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.TransactionOptions" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x000023D1 File Offset: 0x000005D1
		public static bool operator ==(TransactionOptions x, TransactionOptions y)
		{
			return x.level == y.level && x.timeout == y.timeout;
		}

		/// <summary>Determines whether this <see cref="T:System.Transactions.TransactionOptions" /> instance and the specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this <see cref="T:System.Transactions.TransactionOptions" /> instance are identical; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x0600002C RID: 44 RVA: 0x000023F4 File Offset: 0x000005F4
		public override bool Equals(object obj)
		{
			return obj is TransactionOptions && this == (TransactionOptions)obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600002D RID: 45 RVA: 0x00002411 File Offset: 0x00000611
		public override int GetHashCode()
		{
			return (int)(this.level ^ (IsolationLevel)this.timeout.GetHashCode());
		}

		// Token: 0x0400001F RID: 31
		private IsolationLevel level;

		// Token: 0x04000020 RID: 32
		private TimeSpan timeout;
	}
}
