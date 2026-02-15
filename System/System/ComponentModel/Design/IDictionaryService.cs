using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a basic, component site-specific, key-value pair dictionary through a service that a designer can use to store user-defined data.</summary>
	// Token: 0x020002E4 RID: 740
	public interface IDictionaryService
	{
		/// <summary>Gets the value corresponding to the specified key.</summary>
		/// <returns>The associated value, or null if no value exists.</returns>
		/// <param name="key">The key to look up the value for. </param>
		// Token: 0x060011F4 RID: 4596
		object GetValue(object key);

		/// <summary>Sets the specified key-value pair.</summary>
		/// <param name="key">An object to use as the key to associate the value with. </param>
		/// <param name="value">The value to store. </param>
		// Token: 0x060011F5 RID: 4597
		void SetValue(object key, object value);
	}
}
