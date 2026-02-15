using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000661 RID: 1633
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x060031A5 RID: 12709 RVA: 0x000BAF27 File Offset: 0x000B9127
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000BAF36 File Offset: 0x000B9136
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000B1DD9 File Offset: 0x000AFFD9
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000BAF36 File Offset: 0x000B9136
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000BAF36 File Offset: 0x000B9136
		public int GetToken(SignatureHelper helper)
		{
			return this.m.AddRef(helper);
		}

		// Token: 0x0400194C RID: 6476
		private DynamicMethod m;
	}
}
