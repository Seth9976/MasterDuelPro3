using System;

namespace System.Windows.Forms
{
	/// <summary>Defines the communication layer between a control and the Win32 API.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CF RID: 207
	public interface IWindowTarget
	{
		/// <summary>Sets the handle of the <see cref="T:System.Windows.Forms.IWindowTarget" /> to the specified handle.</summary>
		/// <param name="newHandle">The new handle of the <see cref="T:System.Windows.Forms.IWindowTarget" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007C5 RID: 1989
		void OnHandleChange(IntPtr newHandle);

		/// <summary>Processes the Windows messages.</summary>
		/// <param name="m">The Windows message to process. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007C6 RID: 1990
		void OnMessage(ref Message m);
	}
}
