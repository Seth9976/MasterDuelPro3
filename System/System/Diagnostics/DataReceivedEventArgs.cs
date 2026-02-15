using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017C RID: 380
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0002FD7A File Offset: 0x0002DF7A
		internal DataReceivedEventArgs(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0002FD89 File Offset: 0x0002DF89
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x040006EB RID: 1771
		private string data;
	}
}
