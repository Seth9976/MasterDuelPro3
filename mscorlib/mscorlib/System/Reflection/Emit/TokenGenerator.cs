using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200066B RID: 1643
	internal interface TokenGenerator
	{
		// Token: 0x06003235 RID: 12853
		int GetToken(string str);

		// Token: 0x06003236 RID: 12854
		int GetToken(MemberInfo member, bool create_open_instance);

		// Token: 0x06003237 RID: 12855
		int GetToken(MethodBase method, Type[] opt_param_types);

		// Token: 0x06003238 RID: 12856
		int GetToken(SignatureHelper helper);
	}
}
