using System;

namespace System.Resources
{
	/// <summary>Instructs a <see cref="T:System.Resources.ResourceManager" /> object to ask for a particular version of a satellite assembly.</summary>
	// Token: 0x020005C9 RID: 1481
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> class.</summary>
		/// <param name="version">A string that specifies the version of the satellite assemblies to load. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="version" /> parameter is null. </exception>
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000ACE4C File Offset: 0x000AB04C
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		/// <summary>Gets the version of the satellite assemblies with the required resources.</summary>
		/// <returns>A string that contains the version of the satellite assemblies with the required resources.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000ACE69 File Offset: 0x000AB069
		public string Version { get; }
	}
}
