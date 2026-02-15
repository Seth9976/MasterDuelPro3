using System;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanged" /> event.</summary>
	// Token: 0x020000DE RID: 222
	public class UserPreferenceChangedEventArgs : EventArgs
	{
		/// <summary>Gets the category of user preferences that has changed.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the category of user preferences that has changed.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000EB63 File Offset: 0x0000CD63
		public UserPreferenceCategory Category
		{
			get
			{
				return this.mycategory;
			}
		}

		// Token: 0x04000374 RID: 884
		private UserPreferenceCategory mycategory;
	}
}
