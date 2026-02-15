using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001EA RID: 490
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class VolumeRequiresRendererFeatures : Attribute
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000391BC File Offset: 0x000373BC
		public VolumeRequiresRendererFeatures(params Type[] featureTypes)
		{
			this.TargetFeatureTypes = ((featureTypes != null) ? new HashSet<Type>(featureTypes) : new HashSet<Type>());
			this.TargetFeatureTypes.Remove(null);
		}

		// Token: 0x04000C08 RID: 3080
		internal HashSet<Type> TargetFeatureTypes;
	}
}
