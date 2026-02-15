using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200049A RID: 1178
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x060025F3 RID: 9715 RVA: 0x0009A082 File Offset: 0x00098282
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0009A0A9 File Offset: 0x000982A9
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}
	}
}
