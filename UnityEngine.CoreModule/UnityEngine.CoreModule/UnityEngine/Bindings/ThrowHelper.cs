using System;
using System.Diagnostics.CodeAnalysis;

namespace UnityEngine.Bindings
{
	// Token: 0x0200023D RID: 573
	[VisibleToOtherModules]
	internal static class ThrowHelper
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x0002B860 File Offset: 0x00029A60
		[DoesNotReturn]
		public static void ThrowArgumentNullException(object obj, string parameterName)
		{
			Object unityObj = obj as Object;
			bool flag = unityObj != null;
			if (flag)
			{
				Object.MarshalledUnityObject.TryThrowEditorNullExceptionObject(unityObj, parameterName);
			}
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0002B88C File Offset: 0x00029A8C
		[DoesNotReturn]
		public static void ThrowNullReferenceException(object obj)
		{
			Object unityObj = obj as Object;
			bool flag = unityObj != null;
			if (flag)
			{
				Object.MarshalledUnityObject.TryThrowEditorNullExceptionObject(unityObj, null);
			}
			throw new NullReferenceException();
		}
	}
}
