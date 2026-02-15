using System;
using System.Linq;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B0 RID: 432
	internal static class CameraTypeUtility
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0002DD44 File Offset: 0x0002BF44
		public static string GetName(this CameraRenderType type)
		{
			int typeInt = (int)type;
			if (typeInt < 0 || typeInt >= CameraTypeUtility.s_CameraTypeNames.Length)
			{
				typeInt = 0;
			}
			return CameraTypeUtility.s_CameraTypeNames[typeInt];
		}

		// Token: 0x04000971 RID: 2417
		private static string[] s_CameraTypeNames = Enum.GetNames(typeof(CameraRenderType)).ToArray<string>();
	}
}
