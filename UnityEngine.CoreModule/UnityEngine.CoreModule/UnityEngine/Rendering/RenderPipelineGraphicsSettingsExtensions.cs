using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000369 RID: 873
	public static class RenderPipelineGraphicsSettingsExtensions
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x00034611 File Offset: 0x00032811
		public static void SetValueAndNotify<T>(this IRenderPipelineGraphicsSettings settings, ref T currentPropertyValue, T newValue, [CallerMemberName] string propertyName = null)
		{
			throw new Exception("Changing values of a property in standalone builds is forbidden.");
		}
	}
}
