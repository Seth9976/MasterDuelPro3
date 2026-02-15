using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000163 RID: 355
	public abstract class RenderPipelineResources : ScriptableObject
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000104E9 File Offset: 0x0000E6E9
		protected virtual string packagePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00024F23 File Offset: 0x00023123
		internal string packagePath_Internal
		{
			get
			{
				return this.packagePath;
			}
		}
	}
}
