using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B3 RID: 435
	internal class ScriptingUtility
	{
		// Token: 0x06001111 RID: 4369 RVA: 0x0002449C File Offset: 0x0002269C
		[RequiredByNativeCode]
		private static bool IsManagedCodeWorking()
		{
			ScriptingUtility.TestClass testClass = new ScriptingUtility.TestClass
			{
				value = 42
			};
			return testClass.value == 42;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00003D56 File Offset: 0x00001F56
		[RequiredByNativeCode]
		private static void SetupCallbacks(IntPtr p)
		{
		}

		// Token: 0x020001B4 RID: 436
		private struct TestClass
		{
			// Token: 0x04000682 RID: 1666
			public int value;
		}
	}
}
