using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to support a configuration root object.</summary>
	// Token: 0x0200004C RID: 76
	[ComVisible(false)]
	public interface IInternalConfigRoot
	{
		/// <summary>Initializes a configuration object.</summary>
		/// <param name="host">An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</param>
		/// <param name="isDesignTime">true if design time; false if run time.</param>
		// Token: 0x060001E9 RID: 489
		void Init(IInternalConfigHost host, bool isDesignTime);
	}
}
