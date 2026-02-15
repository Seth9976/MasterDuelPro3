using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to initialize application configuration properties.</summary>
	// Token: 0x0200004D RID: 77
	[ComVisible(false)]
	public interface IInternalConfigSystem
	{
		/// <summary>Returns the configuration object based on the specified key. </summary>
		/// <returns>A configuration object.</returns>
		/// <param name="configKey">The configuration key value.</param>
		// Token: 0x060001EA RID: 490
		object GetSection(string configKey);
	}
}
