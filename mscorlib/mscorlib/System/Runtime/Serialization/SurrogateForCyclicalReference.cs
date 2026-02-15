using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004B9 RID: 1209
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x06002682 RID: 9858 RVA: 0x0009B6E1 File Offset: 0x000998E1
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0009B6F1 File Offset: 0x000998F1
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x04001262 RID: 4706
		private ISerializationSurrogate innerSurrogate;
	}
}
