using System;

namespace System.Reflection
{
	// Token: 0x0200063C RID: 1596
	internal struct MonoEventInfo
	{
		// Token: 0x0400185B RID: 6235
		public Type declaring_type;

		// Token: 0x0400185C RID: 6236
		public Type reflected_type;

		// Token: 0x0400185D RID: 6237
		public string name;

		// Token: 0x0400185E RID: 6238
		public MethodInfo add_method;

		// Token: 0x0400185F RID: 6239
		public MethodInfo remove_method;

		// Token: 0x04001860 RID: 6240
		public MethodInfo raise_method;

		// Token: 0x04001861 RID: 6241
		public EventAttributes attrs;

		// Token: 0x04001862 RID: 6242
		public MethodInfo[] other_methods;
	}
}
