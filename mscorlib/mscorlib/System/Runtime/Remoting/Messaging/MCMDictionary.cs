using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000491 RID: 1169
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x00098B3C File Offset: 0x00096D3C
		public MCMDictionary(IMethodMessage message)
			: base(message)
		{
			base.MethodKeys = MCMDictionary.InternalKeys;
		}

		// Token: 0x04001200 RID: 4608
		public static string[] InternalKeys = new string[] { "__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__Args", "__CallContext" };
	}
}
