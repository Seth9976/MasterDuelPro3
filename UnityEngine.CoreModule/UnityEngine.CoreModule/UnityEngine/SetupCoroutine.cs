using System;
using System.Collections;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000199 RID: 409
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x00021FF4 File Offset: 0x000201F4
		[RequiredByNativeCode]
		public unsafe static void InvokeMoveNext(IEnumerator enumerator, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			*(byte*)(void*)returnValueAddress = (enumerator.MoveNext() ? 1 : 0);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00022030 File Offset: 0x00020230
		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] args = null;
			bool flag = variable != null;
			if (flag)
			{
				args = new object[] { variable };
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, args, null, null, null);
		}
	}
}
