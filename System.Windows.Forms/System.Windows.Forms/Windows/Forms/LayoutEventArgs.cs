using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Layout" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FD RID: 253
	public sealed class LayoutEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified control and property affected.</summary>
		/// <param name="affectedControl">The <see cref="T:System.Windows.Forms.Control" /> affected by the layout change.</param>
		/// <param name="affectedProperty">The property affected by the layout change.</param>
		// Token: 0x060008DB RID: 2267 RVA: 0x00025785 File Offset: 0x00023985
		public LayoutEventArgs(Control affectedControl, string affectedProperty)
		{
			this.affected_control = affectedControl;
			this.affected_property = affectedProperty;
		}

		// Token: 0x0400065E RID: 1630
		private Control affected_control;

		// Token: 0x0400065F RID: 1631
		private string affected_property;
	}
}
