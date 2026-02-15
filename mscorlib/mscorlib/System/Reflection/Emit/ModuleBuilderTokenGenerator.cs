using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000677 RID: 1655
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x06003357 RID: 13143 RVA: 0x000C00E3 File Offset: 0x000BE2E3
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000C00F2 File Offset: 0x000BE2F2
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000C0100 File Offset: 0x000BE300
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.mb.GetToken(member, create_open_instance);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000C010F File Offset: 0x000BE30F
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000C011E File Offset: 0x000BE31E
		public int GetToken(SignatureHelper helper)
		{
			return this.mb.GetToken(helper);
		}

		// Token: 0x040019EC RID: 6636
		private ModuleBuilder mb;
	}
}
