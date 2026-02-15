using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	/// <summary>Provides an <see cref="T:System.IProgress`1" /> that invokes callbacks for each reported progress value.</summary>
	/// <typeparam name="T">Specifies the type of the progress report value.</typeparam>
	// Token: 0x02000136 RID: 310
	public class Progress<T> : IProgress<T>
	{
		/// <summary>Initializes the <see cref="T:System.Progress`1" /> object.</summary>
		// Token: 0x06000A60 RID: 2656 RVA: 0x0002F270 File Offset: 0x0002D470
		public Progress()
		{
			this._synchronizationContext = SynchronizationContext.Current ?? ProgressStatics.DefaultContext;
			this._invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
		}

		/// <summary>Reports a progress change.</summary>
		/// <param name="value">The value of the updated progress.</param>
		// Token: 0x06000A61 RID: 2657 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		protected virtual void OnReport(T value)
		{
			bool handler = this._handler != null;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler || progressChanged != null)
			{
				this._synchronizationContext.Post(this._invokeHandlers, value);
			}
		}

		/// <summary>Reports a progress change.</summary>
		/// <param name="value">The value of the updated progress.</param>
		// Token: 0x06000A62 RID: 2658 RVA: 0x0002F2D6 File Offset: 0x0002D4D6
		void IProgress<T>.Report(T value)
		{
			this.OnReport(value);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002F2E0 File Offset: 0x0002D4E0
		private void InvokeHandlers(object state)
		{
			T t = (T)((object)state);
			Action<T> handler = this._handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null)
			{
				handler(t);
			}
			if (progressChanged != null)
			{
				progressChanged(this, t);
			}
		}

		// Token: 0x0400046D RID: 1133
		private readonly SynchronizationContext _synchronizationContext;

		// Token: 0x0400046E RID: 1134
		private readonly Action<T> _handler;

		// Token: 0x0400046F RID: 1135
		private readonly SendOrPostCallback _invokeHandlers;

		// Token: 0x04000470 RID: 1136
		[CompilerGenerated]
		private EventHandler<T> ProgressChanged;
	}
}
