using System;
using System.Runtime.InteropServices;
using UnityEngine.Android;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeConditional("PLATFORM_ANDROID")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[UsedByNativeCode]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	public static class AndroidJNIHelper
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00005394 File Offset: 0x00003594
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000053B0 File Offset: 0x000035B0
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000053CC File Offset: 0x000035CC
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000053E4 File Offset: 0x000035E4
		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			GCHandle handle = GCHandle.Alloc(proxy);
			IntPtr intPtr;
			try
			{
				intPtr = _AndroidJNIHelper.CreateJavaProxy(AndroidApplication.UnityPlayerRaw, GCHandle.ToIntPtr(handle), proxy);
			}
			catch
			{
				handle.Free();
				throw;
			}
			return intPtr;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000542C File Offset: 0x0000362C
		public static void CreateJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			bool flag = args.Length != jniArgs.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Both arrays must be of the same length, but are {0} and {1}", args.Length, jniArgs.Length));
			}
			_AndroidJNIHelper.CreateJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005479 File Offset: 0x00003679
		public static void DeleteJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00005484 File Offset: 0x00003684
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000054A0 File Offset: 0x000036A0
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000054B8 File Offset: 0x000036B8
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000054D4 File Offset: 0x000036D4
		private unsafe static IntPtr Box(jvalue val, string boxedClass, string signature)
		{
			IntPtr clazz = AndroidJNISafe.FindClass(boxedClass);
			IntPtr intPtr;
			try
			{
				IntPtr method = AndroidJNISafe.GetStaticMethodID(clazz, "valueOf", signature);
				Span<jvalue> args = new Span<jvalue>((void*)(&val), 1);
				intPtr = AndroidJNISafe.CallStaticObjectMethod(clazz, method, args);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(clazz);
			}
			return intPtr;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000552C File Offset: 0x0000372C
		public static IntPtr Box(int value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				i = value
			}, "java/lang/Integer", "(I)Ljava/lang/Integer;");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005560 File Offset: 0x00003760
		public static IntPtr Box(bool value)
		{
			return AndroidJNIHelper.Box(new jvalue
			{
				z = value
			}, "java/lang/Boolean", "(Z)Ljava/lang/Boolean;");
		}
	}
}
