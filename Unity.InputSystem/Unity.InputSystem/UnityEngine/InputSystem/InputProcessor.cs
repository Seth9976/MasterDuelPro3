using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000084 RID: 132
	public abstract class InputProcessor
	{
		// Token: 0x06000625 RID: 1573
		public abstract object ProcessAsObject(object value, InputControl control);

		// Token: 0x06000626 RID: 1574
		public unsafe abstract void Process(void* buffer, int bufferSize, InputControl control);

		// Token: 0x06000627 RID: 1575 RVA: 0x000196D7 File Offset: 0x000178D7
		internal static Type GetValueTypeFromType(Type processorType)
		{
			if (processorType == null)
			{
				throw new ArgumentNullException("processorType");
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(processorType, typeof(InputProcessor<>), 0);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001751C File Offset: 0x0001571C
		public virtual InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.CacheResult;
			}
		}

		// Token: 0x040002E6 RID: 742
		internal static TypeTable s_Processors;

		// Token: 0x02000085 RID: 133
		public enum CachingPolicy
		{
			// Token: 0x040002E8 RID: 744
			CacheResult,
			// Token: 0x040002E9 RID: 745
			EvaluateOnEveryRead
		}
	}
}
