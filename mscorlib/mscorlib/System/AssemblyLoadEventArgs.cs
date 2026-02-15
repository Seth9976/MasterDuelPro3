using System;
using System.Reflection;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.AppDomain.AssemblyLoad" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C3 RID: 195
	public class AssemblyLoadEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AssemblyLoadEventArgs" /> class using the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="loadedAssembly">An instance that represents the currently loaded assembly. </param>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00018CE3 File Offset: 0x00016EE3
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this.<LoadedAssembly>k__BackingField = loadedAssembly;
		}
	}
}
