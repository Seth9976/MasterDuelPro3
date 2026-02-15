using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides a format-independent mechanism for transferring data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CB RID: 203
	[ComVisible(true)]
	public interface IDataObject
	{
		/// <summary>Retrieves the data associated with the specified data format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007BC RID: 1980
		object GetData(string format);

		/// <summary>Retrieves the data associated with the specified data format, using a Boolean to determine whether to convert the data to the format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to convert the data to the specified format; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007BD RID: 1981
		object GetData(string format, bool autoConvert);

		/// <summary>Determines whether data stored in this instance is associated with, or can be converted to, the specified format.</summary>
		/// <returns>true if data stored in this instance is associated with, or can be converted to, the specified format; otherwise false.</returns>
		/// <param name="format">The format for which to check. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007BE RID: 1982
		bool GetDataPresent(string format);

		/// <summary>Returns a list of all formats that data stored in this instance is associated with or can be converted to.</summary>
		/// <returns>An array of the names that represents a list of all formats that are supported by the data stored in this object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007BF RID: 1983
		string[] GetFormats();

		/// <summary>Stores the specified data in this instance, using the class of the data for the format.</summary>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007C0 RID: 1984
		void SetData(object data);

		/// <summary>Stores the specified data and its associated format in this instance.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007C1 RID: 1985
		void SetData(string format, object data);
	}
}
