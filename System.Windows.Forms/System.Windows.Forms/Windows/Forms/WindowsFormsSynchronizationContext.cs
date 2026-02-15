using System;
using System.ComponentModel;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides a synchronization context for the Windows Forms application model. </summary>
	// Token: 0x0200022B RID: 555
	public sealed class WindowsFormsSynchronizationContext : SynchronizationContext, IDisposable
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x000705D0 File Offset: 0x0006E7D0
		static WindowsFormsSynchronizationContext()
		{
			WindowsFormsSynchronizationContext.invoke_control.CreateControl();
			WindowsFormsSynchronizationContext.auto_installed = true;
			WindowsFormsSynchronizationContext.previous_context = SynchronizationContext.Current;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> is installed when a control is created.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> is installed; otherwise, false. The default is true.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x000705F6 File Offset: 0x0006E7F6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool AutoInstall
		{
			get
			{
				return WindowsFormsSynchronizationContext.auto_installed;
			}
		}

		/// <summary>Copies the synchronization context.</summary>
		/// <returns>A copy of the synchronization context.</returns>
		// Token: 0x0600169D RID: 5789 RVA: 0x000705FD File Offset: 0x0006E7FD
		public override SynchronizationContext CreateCopy()
		{
			return base.CreateCopy();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" />. </summary>
		// Token: 0x0600169E RID: 5790 RVA: 0x0000493C File Offset: 0x00002B3C
		public void Dispose()
		{
		}

		/// <summary>Dispatches an asynchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		// Token: 0x0600169F RID: 5791 RVA: 0x00070605 File Offset: 0x0006E805
		public override void Post(SendOrPostCallback d, object state)
		{
			WindowsFormsSynchronizationContext.invoke_control.BeginInvoke(d, new object[] { state });
		}

		/// <summary>Dispatches a synchronous message to a synchronization context</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <exception cref="T:System.ComponentModel.InvalidAsynchronousStateException">The destination thread no longer exists.</exception>
		// Token: 0x060016A0 RID: 5792 RVA: 0x0007061D File Offset: 0x0006E81D
		public override void Send(SendOrPostCallback d, object state)
		{
			WindowsFormsSynchronizationContext.invoke_control.Invoke(d, new object[] { state });
		}

		// Token: 0x04000DB6 RID: 3510
		private static bool auto_installed;

		// Token: 0x04000DB7 RID: 3511
		private static Control invoke_control = new Control();

		// Token: 0x04000DB8 RID: 3512
		private static SynchronizationContext previous_context;
	}
}
