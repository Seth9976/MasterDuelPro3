using System;

namespace System.Resources
{
	/// <summary>Informs the resource manager of an app's default culture. This class cannot be inherited.</summary>
	// Token: 0x020005C4 RID: 1476
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cultureName" /> parameter is null. </exception>
		// Token: 0x06002BBA RID: 11194 RVA: 0x000AC7F5 File Offset: 0x000AA9F5
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this.CultureName = cultureName;
			this.Location = UltimateResourceFallbackLocation.MainAssembly;
		}

		/// <summary>Gets the culture name.</summary>
		/// <returns>The name of the default culture for the main assembly.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000AC819 File Offset: 0x000AAA19
		public string CultureName { get; }

		/// <summary>Gets the location for the <see cref="T:System.Resources.ResourceManager" /> class to use to retrieve neutral resources by using the resource fallback process.</summary>
		/// <returns>One of the enumeration values that indicates the location (main assembly or satellite) from which to retrieve neutral resources.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000AC821 File Offset: 0x000AAA21
		public UltimateResourceFallbackLocation Location { get; }
	}
}
