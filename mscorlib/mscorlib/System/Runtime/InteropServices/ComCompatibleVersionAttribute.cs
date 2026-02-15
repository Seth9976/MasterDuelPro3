using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates to a COM client that all classes in the current version of an assembly are compatible with classes in an earlier version of the assembly.</summary>
	// Token: 0x0200053B RID: 1339
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> class with the major version, minor version, build, and revision numbers of the assembly.</summary>
		/// <param name="major">The major version number of the assembly. </param>
		/// <param name="minor">The minor version number of the assembly. </param>
		/// <param name="build">The build number of the assembly. </param>
		/// <param name="revision">The revision number of the assembly. </param>
		// Token: 0x06002926 RID: 10534 RVA: 0x000A8114 File Offset: 0x000A6314
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		// Token: 0x04001579 RID: 5497
		internal int _major;

		// Token: 0x0400157A RID: 5498
		internal int _minor;

		// Token: 0x0400157B RID: 5499
		internal int _build;

		// Token: 0x0400157C RID: 5500
		internal int _revision;
	}
}
