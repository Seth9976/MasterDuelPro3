using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000360 RID: 864
	public sealed class ResourcePathAttribute : ResourcePathsBaseAttribute
	{
		// Token: 0x0600168F RID: 5775 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		public ResourcePathAttribute(string path, SearchType location = SearchType.ProjectPath)
			: base(null, true, location)
		{
		}
	}
}
