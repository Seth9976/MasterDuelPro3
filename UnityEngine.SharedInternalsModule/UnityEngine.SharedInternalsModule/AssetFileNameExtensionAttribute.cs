using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal sealed class AssetFileNameExtensionAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AssetFileNameExtensionAttribute(string preferredExtension, params string[] otherExtensions)
		{
			this.<preferredExtension>k__BackingField = preferredExtension;
			this.<otherExtensions>k__BackingField = otherExtensions;
		}
	}
}
