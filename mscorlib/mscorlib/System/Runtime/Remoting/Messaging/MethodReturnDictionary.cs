using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000495 RID: 1173
	internal class MethodReturnDictionary : MessageDictionary
	{
		// Token: 0x060025CA RID: 9674 RVA: 0x00099A26 File Offset: 0x00097C26
		public MethodReturnDictionary(IMethodReturnMessage message)
			: base(message)
		{
			if (message.Exception == null)
			{
				base.MethodKeys = MethodReturnDictionary.InternalReturnKeys;
				return;
			}
			base.MethodKeys = MethodReturnDictionary.InternalExceptionKeys;
		}

		// Token: 0x04001217 RID: 4631
		public static string[] InternalReturnKeys = new string[] { "__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__OutArgs", "__Return", "__CallContext" };

		// Token: 0x04001218 RID: 4632
		public static string[] InternalExceptionKeys = new string[] { "__CallContext" };
	}
}
