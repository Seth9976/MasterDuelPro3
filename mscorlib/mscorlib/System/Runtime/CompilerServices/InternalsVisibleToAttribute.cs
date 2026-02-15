using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that types that are ordinarily visible only within the current assembly are visible to a specified assembly.</summary>
	// Token: 0x020005B6 RID: 1462
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> class with the name of the specified friend assembly. </summary>
		/// <param name="assemblyName">The name of a friend assembly.</param>
		// Token: 0x06002B7B RID: 11131 RVA: 0x000ABE8E File Offset: 0x000AA08E
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		/// <summary>This property is not implemented.</summary>
		/// <returns>This property does not return a value.</returns>
		// Token: 0x1700058A RID: 1418
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x000ABEA4 File Offset: 0x000AA0A4
		public bool AllInternalsVisible
		{
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x0400160A RID: 5642
		private string _assemblyName;

		// Token: 0x0400160B RID: 5643
		private bool _allInternalsVisible = true;
	}
}
