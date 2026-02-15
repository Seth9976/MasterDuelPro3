using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	internal class AndroidJavaRunnableProxy : AndroidJavaProxy
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002143 File Offset: 0x00000343
		public AndroidJavaRunnableProxy(AndroidJavaRunnable runnable)
			: base("java/lang/Runnable")
		{
			this.mRunnable = runnable;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002159 File Offset: 0x00000359
		public void run()
		{
			this.mRunnable();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002168 File Offset: 0x00000368
		public override IntPtr Invoke(string methodName, IntPtr javaArgs)
		{
			int arrayLen = 0;
			bool flag = javaArgs != IntPtr.Zero;
			if (flag)
			{
				arrayLen = AndroidJNISafe.GetArrayLength(javaArgs);
			}
			bool flag2 = arrayLen == 0 && methodName == "run";
			IntPtr intPtr;
			if (flag2)
			{
				this.run();
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = base.Invoke(methodName, javaArgs);
			}
			return intPtr;
		}

		// Token: 0x04000004 RID: 4
		private AndroidJavaRunnable mRunnable;
	}
}
