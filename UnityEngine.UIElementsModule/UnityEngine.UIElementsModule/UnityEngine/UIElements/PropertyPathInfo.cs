using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003B RID: 59
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal readonly struct PropertyPathInfo
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000960A File Offset: 0x0000780A
		internal PropertyPathInfo(in PropertyPath propertyPath, Type type)
		{
			this.propertyPath = propertyPath;
			this.type = type;
		}

		// Token: 0x04000134 RID: 308
		public readonly PropertyPath propertyPath;

		// Token: 0x04000135 RID: 309
		public readonly Type type;
	}
}
