using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200047B RID: 1147
	internal class CADObjRef
	{
		// Token: 0x06002510 RID: 9488 RVA: 0x00097195 File Offset: 0x00095395
		public CADObjRef(ObjRef o, int sourceDomain)
		{
			this.objref = o;
			this.TypeInfo = o.SerializeType();
			this.SourceDomain = sourceDomain;
		}

		// Token: 0x040011D7 RID: 4567
		internal ObjRef objref;

		// Token: 0x040011D8 RID: 4568
		internal int SourceDomain;

		// Token: 0x040011D9 RID: 4569
		internal byte[] TypeInfo;
	}
}
