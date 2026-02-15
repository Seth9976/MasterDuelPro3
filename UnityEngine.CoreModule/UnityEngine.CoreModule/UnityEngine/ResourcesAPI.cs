using System;

namespace UnityEngine
{
	// Token: 0x0200016E RID: 366
	public class ResourcesAPI
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x000205D4 File Offset: 0x0001E7D4
		internal static ResourcesAPI ActiveAPI
		{
			get
			{
				return ResourcesAPI.overrideAPI ?? ResourcesAPI.s_DefaultAPI;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000205E4 File Offset: 0x0001E7E4
		public static ResourcesAPI overrideAPI { get; }

		// Token: 0x06000F5C RID: 3932 RVA: 0x000205EB File Offset: 0x0001E7EB
		protected internal ResourcesAPI()
		{
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000205F5 File Offset: 0x0001E7F5
		protected internal virtual Object[] FindObjectsOfTypeAll(Type systemTypeInstance)
		{
			return ResourcesAPIInternal.FindObjectsOfTypeAll(systemTypeInstance);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000205FD File Offset: 0x0001E7FD
		protected internal virtual Shader FindShaderByName(string name)
		{
			return ResourcesAPIInternal.FindShaderByName(name);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00020605 File Offset: 0x0001E805
		protected internal virtual Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.Load(path, systemTypeInstance);
		}

		// Token: 0x04000610 RID: 1552
		private static ResourcesAPI s_DefaultAPI = new ResourcesAPI();
	}
}
