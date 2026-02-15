using System;

namespace Mono
{
	// Token: 0x0200003A RID: 58
	internal static class RuntimeStructs
	{
		// Token: 0x0200003B RID: 59
		internal struct RemoteClass
		{
			// Token: 0x04000112 RID: 274
			internal IntPtr default_vtable;

			// Token: 0x04000113 RID: 275
			internal IntPtr xdomain_vtable;

			// Token: 0x04000114 RID: 276
			internal unsafe RuntimeStructs.MonoClass* proxy_class;

			// Token: 0x04000115 RID: 277
			internal IntPtr proxy_class_name;

			// Token: 0x04000116 RID: 278
			internal uint interface_count;
		}

		// Token: 0x0200003C RID: 60
		internal struct MonoClass
		{
		}

		// Token: 0x0200003D RID: 61
		internal struct GenericParamInfo
		{
			// Token: 0x04000117 RID: 279
			internal unsafe RuntimeStructs.MonoClass* pklass;

			// Token: 0x04000118 RID: 280
			internal IntPtr name;

			// Token: 0x04000119 RID: 281
			internal ushort flags;

			// Token: 0x0400011A RID: 282
			internal uint token;

			// Token: 0x0400011B RID: 283
			internal unsafe RuntimeStructs.MonoClass** constraints;
		}

		// Token: 0x0200003E RID: 62
		internal struct GPtrArray
		{
			// Token: 0x0400011C RID: 284
			internal unsafe IntPtr* data;

			// Token: 0x0400011D RID: 285
			internal int len;
		}
	}
}
