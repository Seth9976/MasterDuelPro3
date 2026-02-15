using System;

namespace System.Windows.Forms.Layout
{
	/// <summary>Provides the base class for implementing layout engines.</summary>
	// Token: 0x02000394 RID: 916
	public abstract class LayoutEngine
	{
		/// <summary>Requests that the layout engine perform a layout operation.</summary>
		/// <returns>true if layout should be performed again by the parent of <paramref name="container" />; otherwise, false.</returns>
		/// <param name="container">The container on which the layout engine will operate.</param>
		/// <param name="layoutEventArgs">An event argument from a <see cref="E:System.Windows.Forms.Control.Layout" /> event.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="container" /> is not a type on which <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> can perform layout.</exception>
		// Token: 0x06001DAF RID: 7599 RVA: 0x00002D70 File Offset: 0x00000F70
		public virtual bool Layout(object container, LayoutEventArgs layoutEventArgs)
		{
			return false;
		}
	}
}
