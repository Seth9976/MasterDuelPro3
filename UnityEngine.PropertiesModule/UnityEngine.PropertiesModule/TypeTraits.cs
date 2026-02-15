using System;

namespace Unity.Properties
{
	// Token: 0x02000060 RID: 96
	public static class TypeTraits
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000094D0 File Offset: 0x000076D0
		public static bool IsContainer(Type type)
		{
			bool flag = null == type;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			return !type.IsPrimitive && !type.IsPointer && !type.IsEnum && !(type == typeof(string));
		}
	}
}
