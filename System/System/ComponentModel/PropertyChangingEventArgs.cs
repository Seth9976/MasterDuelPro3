using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event. </summary>
	// Token: 0x020002B1 RID: 689
	public class PropertyChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> class. </summary>
		/// <param name="propertyName">The name of the property whose value is changing.</param>
		// Token: 0x06001056 RID: 4182 RVA: 0x000443C8 File Offset: 0x000425C8
		public PropertyChangingEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x04000A59 RID: 2649
		private readonly string _propertyName;
	}
}
