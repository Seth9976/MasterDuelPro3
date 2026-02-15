using System;

namespace System.Reflection
{
	// Token: 0x02000646 RID: 1606
	internal struct MonoPropertyInfo
	{
		// Token: 0x04001881 RID: 6273
		public Type parent;

		// Token: 0x04001882 RID: 6274
		public Type declaring_type;

		// Token: 0x04001883 RID: 6275
		public string name;

		// Token: 0x04001884 RID: 6276
		public MethodInfo get_method;

		// Token: 0x04001885 RID: 6277
		public MethodInfo set_method;

		// Token: 0x04001886 RID: 6278
		public PropertyAttributes attrs;
	}
}
