using System;

namespace System.ComponentModel
{
	/// <summary>Provides the ability to retrieve the full nested name of a component.</summary>
	// Token: 0x02000280 RID: 640
	public interface INestedSite : ISite, IServiceProvider
	{
		/// <summary>Gets the full name of the component in this site.</summary>
		/// <returns>The full name of the component in this site.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F44 RID: 3908
		string FullName { get; }
	}
}
