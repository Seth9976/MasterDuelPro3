using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000148 RID: 328
	internal static class RenderPassEventsEnumValues
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x00022B5C File Offset: 0x00020D5C
		static RenderPassEventsEnumValues()
		{
			Array array = Enum.GetValues(typeof(RenderPassEvent));
			RenderPassEventsEnumValues.values = new int[array.Length];
			int index = 0;
			foreach (object obj in array)
			{
				int value = (int)obj;
				RenderPassEventsEnumValues.values[index] = value;
				index++;
			}
		}

		// Token: 0x040007AB RID: 1963
		public static int[] values;
	}
}
