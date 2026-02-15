using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines the lock that implements single-writer/multiple-reader semantics. This is a value type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027E RID: 638
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x060017B3 RID: 6067 RVA: 0x0005BAA2 File Offset: 0x00059CA2
		internal LockCookie(int thread_id)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = 0;
			this.WriterLocks = 0;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005BAB9 File Offset: 0x00059CB9
		internal LockCookie(int thread_id, int reader_locks, int writer_locks)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = reader_locks;
			this.WriterLocks = writer_locks;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060017B5 RID: 6069 RVA: 0x0005BAD0 File Offset: 0x00059CD0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Threading.LockCookie" />.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Threading.LockCookie" /> to compare to the current instance.</param>
		// Token: 0x060017B6 RID: 6070 RVA: 0x0005BAE2 File Offset: 0x00059CE2
		public bool Equals(LockCookie obj)
		{
			return this.ThreadId == obj.ThreadId && this.ReaderLocks == obj.ReaderLocks && this.WriterLocks == obj.WriterLocks;
		}

		/// <summary>Indicates whether a specified object is a <see cref="T:System.Threading.LockCookie" /> and is equal to the current instance.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		// Token: 0x060017B7 RID: 6071 RVA: 0x0005BB11 File Offset: 0x00059D11
		public override bool Equals(object obj)
		{
			return obj is LockCookie && obj.Equals(this);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060017B8 RID: 6072 RVA: 0x0005BB2E File Offset: 0x00059D2E
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060017B9 RID: 6073 RVA: 0x0005BB38 File Offset: 0x00059D38
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000B37 RID: 2871
		internal int ThreadId;

		// Token: 0x04000B38 RID: 2872
		internal int ReaderLocks;

		// Token: 0x04000B39 RID: 2873
		internal int WriterLocks;
	}
}
