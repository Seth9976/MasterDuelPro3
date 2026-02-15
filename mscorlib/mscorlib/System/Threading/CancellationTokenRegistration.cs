using System;
using System.Threading.Tasks;

namespace System.Threading
{
	/// <summary>Represents a callback delegate that has been registered with a <see cref="T:System.Threading.CancellationToken" />. </summary>
	// Token: 0x02000234 RID: 564
	public readonly struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable, IAsyncDisposable
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x0005473A File Offset: 0x0005293A
		internal CancellationTokenRegistration(CancellationCallbackInfo callbackInfo, SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo)
		{
			this.m_callbackInfo = callbackInfo;
			this.m_registrationInfo = registrationInfo;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0005474C File Offset: 0x0005294C
		public CancellationToken Token
		{
			get
			{
				CancellationCallbackInfo callbackInfo = this.m_callbackInfo;
				if (callbackInfo == null)
				{
					return default(CancellationToken);
				}
				return callbackInfo.CancellationTokenSource.Token;
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00054778 File Offset: 0x00052978
		public bool Unregister()
		{
			return this.m_registrationInfo.Source != null && this.m_registrationInfo.Source.SafeAtomicRemove(this.m_registrationInfo.Index, this.m_callbackInfo) == this.m_callbackInfo;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CancellationTokenRegistration" /> class.</summary>
		// Token: 0x060014F2 RID: 5362 RVA: 0x000547CC File Offset: 0x000529CC
		public void Dispose()
		{
			bool flag = this.Unregister();
			CancellationCallbackInfo callbackInfo = this.m_callbackInfo;
			if (callbackInfo != null)
			{
				CancellationTokenSource cancellationTokenSource = callbackInfo.CancellationTokenSource;
				if (cancellationTokenSource.IsCancellationRequested && !cancellationTokenSource.IsCancellationCompleted && !flag && cancellationTokenSource.ThreadIDExecutingCallbacks != Environment.CurrentManagedThreadId)
				{
					cancellationTokenSource.WaitForCallbackToComplete(this.m_callbackInfo);
				}
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are equal.</summary>
		/// <returns>True if the instances are equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		// Token: 0x060014F3 RID: 5363 RVA: 0x0005481D File Offset: 0x00052A1D
		public static bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are not equal.</summary>
		/// <returns>True if the instances are not equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		// Token: 0x060014F4 RID: 5364 RVA: 0x00054827 File Offset: 0x00052A27
		public static bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return !left.Equals(right);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationTokenRegistration" /> instance is equal to the specified <see cref="T:System.Threading.CancellationTokenRegistration" />.</summary>
		/// <returns>True, if both this and <paramref name="obj" /> are equal. False, otherwise.Two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are equal if they both refer to the output of a single call to the same Register method of a <see cref="T:System.Threading.CancellationToken" />.</returns>
		/// <param name="obj">The other object to which to compare this instance.</param>
		// Token: 0x060014F5 RID: 5365 RVA: 0x00054834 File Offset: 0x00052A34
		public override bool Equals(object obj)
		{
			return obj is CancellationTokenRegistration && this.Equals((CancellationTokenRegistration)obj);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationTokenRegistration" /> instance is equal to the specified <see cref="T:System.Threading.CancellationTokenRegistration" />.</summary>
		/// <returns>True, if both this and <paramref name="other" /> are equal. False, otherwise. Two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are equal if they both refer to the output of a single call to the same Register method of a <see cref="T:System.Threading.CancellationToken" />.</returns>
		/// <param name="other">The other <see cref="T:System.Threading.CancellationTokenRegistration" /> to which to compare this instance.</param>
		// Token: 0x060014F6 RID: 5366 RVA: 0x0005484C File Offset: 0x00052A4C
		public bool Equals(CancellationTokenRegistration other)
		{
			return this.m_callbackInfo == other.m_callbackInfo && this.m_registrationInfo.Source == other.m_registrationInfo.Source && this.m_registrationInfo.Index == other.m_registrationInfo.Index;
		}

		/// <summary>Serves as a hash function for a <see cref="T:System.Threading.CancellationTokenRegistration" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.CancellationTokenRegistration" /> instance.</returns>
		// Token: 0x060014F7 RID: 5367 RVA: 0x000548A8 File Offset: 0x00052AA8
		public override int GetHashCode()
		{
			if (this.m_registrationInfo.Source != null)
			{
				return this.m_registrationInfo.Source.GetHashCode() ^ this.m_registrationInfo.Index.GetHashCode();
			}
			return this.m_registrationInfo.Index.GetHashCode();
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00054906 File Offset: 0x00052B06
		public ValueTask DisposeAsync()
		{
			this.Dispose();
			return new ValueTask(Task.FromResult<object>(null));
		}

		// Token: 0x04000A41 RID: 2625
		private readonly CancellationCallbackInfo m_callbackInfo;

		// Token: 0x04000A42 RID: 2626
		private readonly SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> m_registrationInfo;
	}
}
