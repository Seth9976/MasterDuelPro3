using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	/// <summary>Propagates notification that operations should be canceled.</summary>
	// Token: 0x0200022B RID: 555
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	public readonly struct CancellationToken
	{
		/// <summary>Returns an empty CancellationToken value.</summary>
		/// <returns>Returns an empty CancellationToken value.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00053B60 File Offset: 0x00051D60
		public static CancellationToken None
		{
			get
			{
				return default(CancellationToken);
			}
		}

		/// <summary>Gets whether cancellation has been requested for this token.</summary>
		/// <returns>true if cancellation has been requested for this token; otherwise false.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00053B76 File Offset: 0x00051D76
		public bool IsCancellationRequested
		{
			get
			{
				return this._source != null && this._source.IsCancellationRequested;
			}
		}

		/// <summary>Gets whether this token is capable of being in the canceled state.</summary>
		/// <returns>true if this token is capable of being in the canceled state; otherwise false.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00053B8D File Offset: 0x00051D8D
		public bool CanBeCanceled
		{
			get
			{
				return this._source != null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00053B98 File Offset: 0x00051D98
		public WaitHandle WaitHandle
		{
			get
			{
				return (this._source ?? CancellationTokenSource.s_neverCanceledSource).WaitHandle;
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00053BAE File Offset: 0x00051DAE
		internal CancellationToken(CancellationTokenSource source)
		{
			this._source = source;
		}

		/// <summary>Initializes the <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="canceled">The canceled state for the token.</param>
		// Token: 0x060014AB RID: 5291 RVA: 0x00053BB7 File Offset: 0x00051DB7
		public CancellationToken(bool canceled)
		{
			this = new CancellationToken(canceled ? CancellationTokenSource.s_canceledSource : null);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060014AC RID: 5292 RVA: 0x00053BCA File Offset: 0x00051DCA
		public CancellationTokenRegistration Register(Action callback)
		{
			Action<object> action = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(action, callback, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="useSynchronizationContext">A Boolean value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060014AD RID: 5293 RVA: 0x00053BE9 File Offset: 0x00051DE9
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			Action<object> action = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(action, callback, useSynchronizationContext, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060014AE RID: 5294 RVA: 0x00053C08 File Offset: 0x00051E08
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <param name="useSynchronizationContext">A Boolean value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060014AF RID: 5295 RVA: 0x00053C14 File Offset: 0x00051E14
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00053C20 File Offset: 0x00051E20
		internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00053C2C File Offset: 0x00051E2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			CancellationTokenSource source = this._source;
			if (source == null)
			{
				return default(CancellationTokenRegistration);
			}
			return source.InternalRegister(callback, state, useSynchronizationContext ? SynchronizationContext.Current : null, useExecutionContext ? ExecutionContext.Capture() : null);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified token.</summary>
		/// <returns>True if the instances are equal; otherwise, false. Two tokens are equal if they are associated with the same <see cref="T:System.Threading.CancellationTokenSource" /> or if they were both constructed from public CancellationToken constructors and their <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> values are equal.</returns>
		/// <param name="other">The other <see cref="T:System.Threading.CancellationToken" /> to which to compare this instance.</param>
		// Token: 0x060014B2 RID: 5298 RVA: 0x00053C7A File Offset: 0x00051E7A
		public bool Equals(CancellationToken other)
		{
			return this._source == other._source;
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified <see cref="T:System.Object" />.</summary>
		/// <returns>True if <paramref name="other" /> is a <see cref="T:System.Threading.CancellationToken" /> and if the two instances are equal; otherwise, false. Two tokens are equal if they are associated with the same <see cref="T:System.Threading.CancellationTokenSource" /> or if they were both constructed from public CancellationToken constructors and their <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> values are equal.</returns>
		/// <param name="other">The other object to which to compare this instance.</param>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060014B3 RID: 5299 RVA: 0x00053C8A File Offset: 0x00051E8A
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		/// <summary>Serves as a hash function for a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.CancellationToken" /> instance.</returns>
		// Token: 0x060014B4 RID: 5300 RVA: 0x00053CA2 File Offset: 0x00051EA2
		public override int GetHashCode()
		{
			return (this._source ?? CancellationTokenSource.s_neverCanceledSource).GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are equal.</summary>
		/// <returns>True if the instances are equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060014B5 RID: 5301 RVA: 0x00053CB8 File Offset: 0x00051EB8
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are not equal.</summary>
		/// <returns>True if the instances are not equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060014B6 RID: 5302 RVA: 0x00053CC2 File Offset: 0x00051EC2
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Throws a <see cref="T:System.OperationCanceledException" /> if this token has had cancellation requested.</summary>
		/// <exception cref="T:System.OperationCanceledException">The token has had cancellation requested.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060014B7 RID: 5303 RVA: 0x00053CCF File Offset: 0x00051ECF
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00053CDF File Offset: 0x00051EDF
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException("The operation was canceled.", this);
		}

		// Token: 0x04000A20 RID: 2592
		private readonly CancellationTokenSource _source;

		// Token: 0x04000A21 RID: 2593
		private static readonly Action<object> s_actionToActionObjShunt = delegate(object obj)
		{
			((Action)obj)();
		};
	}
}
