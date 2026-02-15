using System;

namespace System.Threading
{
	/// <summary>Provides the functionality to restore the migration, or flow, of the execution context between threads.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000257 RID: 599
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x00057BEC File Offset: 0x00055DEC
		internal void Setup()
		{
			this.useEC = true;
			Thread currentThread = Thread.CurrentThread;
			this._ec = currentThread.GetMutableExecutionContext();
			this._ec.isFlowSuppressed = true;
			this._thread = currentThread;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.AsyncFlowControl" /> class.</summary>
		// Token: 0x060015CE RID: 5582 RVA: 0x00057C25 File Offset: 0x00055E25
		public void Dispose()
		{
			this.Undo();
		}

		/// <summary>Restores the flow of the execution context between threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.-or-The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015CF RID: 5583 RVA: 0x00057C30 File Offset: 0x00055E30
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object can be used only once to call Undo()."));
			}
			if (this._thread != Thread.CurrentThread)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object must be used on the thread where it was created."));
			}
			if (this.useEC)
			{
				if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
				{
					throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl objects can be used to restore flow only on the Context that had its flow suppressed."));
				}
				ExecutionContext.RestoreFlow();
			}
			this._thread = null;
		}

		/// <summary>Gets a hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</returns>
		// Token: 0x060015D0 RID: 5584 RVA: 0x00057CA8 File Offset: 0x00055EA8
		public override int GetHashCode()
		{
			if (this._thread != null)
			{
				return this._thread.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure. </summary>
		/// <returns>true if <paramref name="obj" /> is an <see cref="T:System.Threading.AsyncFlowControl" /> structure and is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, false. </returns>
		/// <param name="obj">An object to compare with the current structure.</param>
		// Token: 0x060015D1 RID: 5585 RVA: 0x00057CCF File Offset: 0x00055ECF
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Threading.AsyncFlowControl" /> structure is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Threading.AsyncFlowControl" /> structure to compare with the current structure.</param>
		// Token: 0x060015D2 RID: 5586 RVA: 0x00057CE7 File Offset: 0x00055EE7
		public bool Equals(AsyncFlowControl obj)
		{
			return obj.useEC == this.useEC && obj._ec == this._ec && obj._thread == this._thread;
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are equal. </summary>
		/// <returns>true if the two structures are equal; otherwise, false. </returns>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		// Token: 0x060015D3 RID: 5587 RVA: 0x00057D15 File Offset: 0x00055F15
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are not equal. </summary>
		/// <returns>true if the structures are not equal; otherwise, false. </returns>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		// Token: 0x060015D4 RID: 5588 RVA: 0x00057D1F File Offset: 0x00055F1F
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x04000ABC RID: 2748
		private bool useEC;

		// Token: 0x04000ABD RID: 2749
		private ExecutionContext _ec;

		// Token: 0x04000ABE RID: 2750
		private Thread _thread;
	}
}
