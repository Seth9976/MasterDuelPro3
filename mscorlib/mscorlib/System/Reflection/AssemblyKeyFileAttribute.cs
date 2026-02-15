using System;

namespace System.Reflection
{
	/// <summary>Specifies the name of a file containing the key pair used to generate a strong name.</summary>
	// Token: 0x020005EA RID: 1514
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyKeyFileAttribute class with the name of the file containing the key pair to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyFile">The name of the file containing the key pair. </param>
		// Token: 0x06002C8F RID: 11407 RVA: 0x000B17E1 File Offset: 0x000AF9E1
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.<KeyFile>k__BackingField = keyFile;
		}
	}
}
