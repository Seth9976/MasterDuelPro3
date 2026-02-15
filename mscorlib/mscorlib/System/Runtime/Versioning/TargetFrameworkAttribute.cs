using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the version of the .NET Framework that a particular assembly was compiled against.</summary>
	// Token: 0x020004A2 RID: 1186
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Versioning.TargetFrameworkAttribute" /> class by specifying the .NET Framework version against which an assembly was built.</summary>
		/// <param name="frameworkName">The version of the .NET Framework against which the assembly was built.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is null.</exception>
		// Token: 0x0600261F RID: 9759 RVA: 0x0009A66C File Offset: 0x0009886C
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		/// <summary>Gets the display name of the .NET Framework version against which an assembly was built.</summary>
		/// <returns>The display name of the .NET Framework version.</returns>
		// Token: 0x17000500 RID: 1280
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x0009A689 File Offset: 0x00098889
		public string FrameworkDisplayName
		{
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x04001241 RID: 4673
		private string _frameworkName;

		// Token: 0x04001242 RID: 4674
		private string _frameworkDisplayName;
	}
}
