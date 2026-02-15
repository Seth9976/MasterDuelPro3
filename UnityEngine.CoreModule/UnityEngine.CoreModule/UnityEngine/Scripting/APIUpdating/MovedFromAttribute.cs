using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x0200024C RID: 588
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public class MovedFromAttribute : Attribute
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x0002BB64 File Offset: 0x00029D64
		public MovedFromAttribute(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.data.Set(autoUpdateAPI, sourceNamespace, sourceAssembly, sourceClassName);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0002BB7F File Offset: 0x00029D7F
		public MovedFromAttribute(string sourceNamespace)
		{
			this.data.Set(true, sourceNamespace, null, null);
		}

		// Token: 0x040007B2 RID: 1970
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal MovedFromAttributeData data;
	}
}
