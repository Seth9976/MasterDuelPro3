using System;
using System.Reflection;

namespace System
{
	/// <summary>Provides data for loader resolution events, such as the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, <see cref="E:System.AppDomain.ReflectionOnlyAssemblyResolve" />, and <see cref="E:System.AppDomain.AssemblyResolve" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013C RID: 316
	public class ResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve.</summary>
		/// <param name="name">The name of an item to resolve. </param>
		// Token: 0x06000A96 RID: 2710 RVA: 0x0002FD22 File Offset: 0x0002DF22
		public ResolveEventArgs(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve and the assembly whose dependency is being resolved.</summary>
		/// <param name="name">The name of an item to resolve. </param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved.</param>
		// Token: 0x06000A97 RID: 2711 RVA: 0x0002FD31 File Offset: 0x0002DF31
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this.Name = name;
			this.<RequestingAssembly>k__BackingField = requestingAssembly;
		}

		/// <summary>Gets the name of the item to resolve.</summary>
		/// <returns>The name of the item to resolve.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002FD47 File Offset: 0x0002DF47
		public string Name { get; }
	}
}
