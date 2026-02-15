using System;

namespace System.Reflection
{
	/// <summary>Instructs a compiler to use a specific version number for the Win32 file version resource. The Win32 file version is not required to be the same as the assembly's version number.</summary>
	// Token: 0x020005E8 RID: 1512
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> class, specifying the file version.</summary>
		/// <param name="version">The file version. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		// Token: 0x06002C8C RID: 11404 RVA: 0x000B17AD File Offset: 0x000AF9AD
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.<Version>k__BackingField = version;
		}
	}
}
