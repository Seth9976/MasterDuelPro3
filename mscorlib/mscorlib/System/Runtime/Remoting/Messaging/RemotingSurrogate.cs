using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000499 RID: 1177
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x060025F0 RID: 9712 RVA: 0x0009A056 File Offset: 0x00098256
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RemotingServices.GetRealProxy(obj).GetObjectData(si, sc);
				return;
			}
			RemotingServices.GetObjectData(obj, si, sc);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException();
		}
	}
}
