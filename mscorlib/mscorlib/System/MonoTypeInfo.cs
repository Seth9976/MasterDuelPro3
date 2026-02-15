using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001BC RID: 444
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoTypeInfo
	{
		// Token: 0x04000711 RID: 1809
		public string full_name;

		// Token: 0x04000712 RID: 1810
		public RuntimeConstructorInfo default_ctor;
	}
}
