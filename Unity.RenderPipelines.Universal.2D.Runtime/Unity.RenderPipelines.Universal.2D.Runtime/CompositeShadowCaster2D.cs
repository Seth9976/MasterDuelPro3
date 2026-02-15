using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006A RID: 106
	[AddComponentMenu("Rendering/2D/Composite Shadow Caster 2D")]
	[MovedFrom(false, "UnityEngine.Experimental.Rendering.Universal", "com.unity.render-pipelines.universal", null)]
	[ExecuteInEditMode]
	public class CompositeShadowCaster2D : ShadowCasterGroup2D
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00014E46 File Offset: 0x00013046
		protected void OnEnable()
		{
			ShadowCasterGroup2DManager.AddGroup(this);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00014E4E File Offset: 0x0001304E
		protected void OnDisable()
		{
			ShadowCasterGroup2DManager.RemoveGroup(this);
		}
	}
}
