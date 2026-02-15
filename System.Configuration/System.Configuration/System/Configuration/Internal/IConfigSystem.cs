using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support the initialization of configuration properties.</summary>
	// Token: 0x02000049 RID: 73
	public interface IConfigSystem
	{
		/// <summary>Gets the configuration host.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object that is used by the .NET Framework to initialize application configuration properties.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001DF RID: 479
		IInternalConfigHost Host { get; }
	}
}
