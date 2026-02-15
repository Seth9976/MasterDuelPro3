using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a source <see cref="T:System.Type" /> in another assembly. </summary>
	// Token: 0x02000599 RID: 1433
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" /> class. </summary>
		/// <param name="assemblyFullName">The source <see cref="T:System.Type" /> in another assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFullName" /> is null or empty.</exception>
		// Token: 0x06002B0E RID: 11022 RVA: 0x000AAC90 File Offset: 0x000A8E90
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.AssemblyFullName = assemblyFullName;
		}

		/// <summary>Gets the assembly-qualified name of the source type.</summary>
		/// <returns>The assembly-qualified name of the source type.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000AACB2 File Offset: 0x000A8EB2
		public string AssemblyFullName { get; }
	}
}
