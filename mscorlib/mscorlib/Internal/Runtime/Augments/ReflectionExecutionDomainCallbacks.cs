using System;
using System.Reflection;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000095 RID: 149
	internal class ReflectionExecutionDomainCallbacks
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0001091B File Offset: 0x0000EB1B
		internal Exception CreateMissingMetadataException(Type attributeType)
		{
			return new MissingMetadataException();
		}
	}
}
