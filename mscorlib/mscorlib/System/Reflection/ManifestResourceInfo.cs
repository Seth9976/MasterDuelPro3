using System;

namespace System.Reflection
{
	/// <summary>Provides access to manifest resources, which are XML files that describe application dependencies.  </summary>
	// Token: 0x02000606 RID: 1542
	public class ManifestResourceInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ManifestResourceInfo" /> class for a resource that is contained by the specified assembly and file, and that has the specified location.</summary>
		/// <param name="containingAssembly">The assembly that contains the manifest resource.</param>
		/// <param name="containingFileName">The name of the file that contains the manifest resource, if the file is not the same as the manifest file.</param>
		/// <param name="resourceLocation">A bitwise combination of enumeration values that provides information about the location of the manifest resource. </param>
		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B1D78 File Offset: 0x000AFF78
		public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
		{
			this.ReferencedAssembly = containingAssembly;
			this.FileName = containingFileName;
			this.ResourceLocation = resourceLocation;
		}

		/// <summary>Gets the containing assembly for the manifest resource. </summary>
		/// <returns>The manifest resource's containing assembly.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000B1D95 File Offset: 0x000AFF95
		public virtual Assembly ReferencedAssembly { get; }

		/// <summary>Gets the name of the file that contains the manifest resource, if it is not the same as the manifest file.  </summary>
		/// <returns>The manifest resource's file name.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000B1D9D File Offset: 0x000AFF9D
		public virtual string FileName { get; }

		/// <summary>Gets the manifest resource's location. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.ResourceLocation" /> flags that indicates the location of the manifest resource. </returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000B1DA5 File Offset: 0x000AFFA5
		public virtual ResourceLocation ResourceLocation { get; }
	}
}
