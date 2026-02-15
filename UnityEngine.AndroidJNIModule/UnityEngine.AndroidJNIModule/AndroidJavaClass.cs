using System;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003521 File Offset: 0x00001721
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003534 File Offset: 0x00001734
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			IntPtr clazz = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(clazz);
			this.m_jobject = null;
			AndroidJNISafe.DeleteLocalRef(clazz);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003580 File Offset: 0x00001780
		internal AndroidJavaClass(IntPtr jclass)
		{
			bool flag = jclass == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = new GlobalJavaObjectRef(jclass);
			this.m_jobject = null;
		}
	}
}
