using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
	// Token: 0x020002AF RID: 687
	public class PropertyChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that changed. </param>
		// Token: 0x06001053 RID: 4179 RVA: 0x000443B9 File Offset: 0x000425B9
		public PropertyChangedEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x04000A58 RID: 2648
		private readonly string _propertyName;
	}
}
