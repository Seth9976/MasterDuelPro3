using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the source for <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> automatic completion functionality.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000013 RID: 19
	public enum AutoCompleteSource
	{
		/// <summary>Specifies the file system as the source.</summary>
		// Token: 0x04000080 RID: 128
		FileSystem = 1,
		/// <summary>Includes the Uniform Resource Locators (URLs) in the history list.</summary>
		// Token: 0x04000081 RID: 129
		HistoryList,
		/// <summary>Includes the Uniform Resource Locators (URLs) in the list of those URLs most recently used.</summary>
		// Token: 0x04000082 RID: 130
		RecentlyUsedList = 4,
		/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.HistoryList" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.RecentlyUsedList" /> as the source.</summary>
		// Token: 0x04000083 RID: 131
		AllUrl = 6,
		/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.FileSystem" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.AllUrl" /> as the source. This is the default value when <see cref="T:System.Windows.Forms.AutoCompleteMode" /> has been set to a value other than the default.</summary>
		// Token: 0x04000084 RID: 132
		AllSystemSources,
		/// <summary>Specifies that only directory names and not file names will be automatically completed.</summary>
		// Token: 0x04000085 RID: 133
		FileSystemDirectories = 32,
		/// <summary>Specifies strings from a built-in <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> as the source.</summary>
		// Token: 0x04000086 RID: 134
		CustomSource = 64,
		/// <summary>Specifies that no <see cref="T:System.Windows.Forms.AutoCompleteSource" /> is currently in use. This is the default value of <see cref="T:System.Windows.Forms.AutoCompleteSource" />.</summary>
		// Token: 0x04000087 RID: 135
		None = 128,
		/// <summary>Specifies that the items of the <see cref="T:System.Windows.Forms.ComboBox" /> represent the source.</summary>
		// Token: 0x04000088 RID: 136
		ListItems = 256
	}
}
