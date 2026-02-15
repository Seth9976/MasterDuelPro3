using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x0200024B RID: 587
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct MovedFromAttributeData
	{
		// Token: 0x060014AC RID: 5292 RVA: 0x0002BB0C File Offset: 0x00029D0C
		public void Set(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.className = sourceClassName;
			this.classHasChanged = this.className != null;
			this.nameSpace = sourceNamespace;
			this.nameSpaceHasChanged = this.nameSpace != null;
			this.assembly = sourceAssembly;
			this.assemblyHasChanged = this.assembly != null;
			this.autoUdpateAPI = autoUpdateAPI;
		}

		// Token: 0x040007AB RID: 1963
		public string className;

		// Token: 0x040007AC RID: 1964
		public string nameSpace;

		// Token: 0x040007AD RID: 1965
		public string assembly;

		// Token: 0x040007AE RID: 1966
		public bool classHasChanged;

		// Token: 0x040007AF RID: 1967
		public bool nameSpaceHasChanged;

		// Token: 0x040007B0 RID: 1968
		public bool assemblyHasChanged;

		// Token: 0x040007B1 RID: 1969
		public bool autoUdpateAPI;
	}
}
