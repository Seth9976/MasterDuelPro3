using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x02000298 RID: 664
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed. </param>
		// Token: 0x06000FFA RID: 4090 RVA: 0x00043C48 File Offset: 0x00041E48
		public RefreshEventArgs(object componentChanged)
		{
			this.<ComponentChanged>k__BackingField = componentChanged;
			this.<TypeChanged>k__BackingField = componentChanged.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed. </param>
		// Token: 0x06000FFB RID: 4091 RVA: 0x00043C63 File Offset: 0x00041E63
		public RefreshEventArgs(Type typeChanged)
		{
			this.<TypeChanged>k__BackingField = typeChanged;
		}
	}
}
