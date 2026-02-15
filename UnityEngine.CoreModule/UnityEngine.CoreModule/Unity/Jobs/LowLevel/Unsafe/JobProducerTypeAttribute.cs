using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Interface)]
	public sealed class JobProducerTypeAttribute : Attribute
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002530 File Offset: 0x00000730
		public JobProducerTypeAttribute(Type producerType)
		{
			this.<ProducerType>k__BackingField = producerType;
		}
	}
}
