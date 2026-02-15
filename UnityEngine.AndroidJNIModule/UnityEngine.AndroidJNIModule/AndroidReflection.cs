using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal class AndroidReflection
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000035C4 File Offset: 0x000017C4
		public static bool IsPrimitive(Type t)
		{
			return t.IsPrimitive;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000035DC File Offset: 0x000017DC
		public static bool IsAssignableFrom(Type t, Type from)
		{
			return t.IsAssignableFrom(from);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000035F8 File Offset: 0x000017F8
		private static IntPtr GetStaticMethodID(string clazz, string methodName, string signature)
		{
			IntPtr jclass = AndroidJNISafe.FindClass(clazz);
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNISafe.GetStaticMethodID(jclass, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return staticMethodID;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003634 File Offset: 0x00001834
		private static IntPtr GetMethodID(string clazz, string methodName, string signature)
		{
			IntPtr jclass = AndroidJNISafe.FindClass(clazz);
			IntPtr methodID;
			try
			{
				methodID = AndroidJNISafe.GetMethodID(jclass, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return methodID;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003670 File Offset: 0x00001870
		public static IntPtr GetConstructorMember(IntPtr jclass, string signature)
		{
			jvalue[] jniArgs = new jvalue[2];
			IntPtr intPtr;
			try
			{
				jniArgs[0].l = jclass;
				jniArgs[1].l = AndroidJNISafe.NewString(signature);
				intPtr = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetConstructorID, jniArgs);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jniArgs[1].l);
			}
			return intPtr;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000036E4 File Offset: 0x000018E4
		public static IntPtr GetMethodMember(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			jvalue[] jniArgs = new jvalue[4];
			IntPtr intPtr;
			try
			{
				jniArgs[0].l = jclass;
				jniArgs[1].l = AndroidJNISafe.NewString(methodName);
				jniArgs[2].l = AndroidJNISafe.NewString(signature);
				jniArgs[3].z = isStatic;
				intPtr = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetMethodID, jniArgs);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jniArgs[1].l);
				AndroidJNISafe.DeleteLocalRef(jniArgs[2].l);
			}
			return intPtr;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003788 File Offset: 0x00001988
		public static IntPtr NewProxyInstance(IntPtr player, IntPtr delegateHandle, IntPtr interfaze)
		{
			jvalue[] jniArgs = new jvalue[3];
			jniArgs[0].l = player;
			jniArgs[1].j = delegateHandle.ToInt64();
			jniArgs[2].l = interfaze;
			return AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperNewProxyInstance, jniArgs);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000037E4 File Offset: 0x000019E4
		internal static IntPtr CreateInvocationError(Exception ex, bool methodNotFound)
		{
			jvalue[] jniArgs = new jvalue[2];
			jniArgs[0].j = GCHandle.ToIntPtr(GCHandle.Alloc(ex)).ToInt64();
			jniArgs[1].z = methodNotFound;
			return AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperCeateInvocationError, jniArgs);
		}

		// Token: 0x0400000C RID: 12
		private static readonly GlobalJavaObjectRef s_ReflectionHelperClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("com/unity3d/player/ReflectionHelper"));

		// Token: 0x0400000D RID: 13
		private static readonly IntPtr s_ReflectionHelperGetConstructorID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getConstructorID", "(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/reflect/Constructor;");

		// Token: 0x0400000E RID: 14
		private static readonly IntPtr s_ReflectionHelperGetMethodID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getMethodID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Method;");

		// Token: 0x0400000F RID: 15
		private static readonly IntPtr s_ReflectionHelperGetFieldID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Field;");

		// Token: 0x04000010 RID: 16
		private static readonly IntPtr s_ReflectionHelperGetFieldSignature = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldSignature", "(Ljava/lang/reflect/Field;)Ljava/lang/String;");

		// Token: 0x04000011 RID: 17
		private static readonly IntPtr s_ReflectionHelperNewProxyInstance = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "newProxyInstance", "(Lcom/unity3d/player/UnityPlayer;JLjava/lang/Class;)Ljava/lang/Object;");

		// Token: 0x04000012 RID: 18
		private static readonly IntPtr s_ReflectionHelperCeateInvocationError = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "createInvocationError", "(JZ)Ljava/lang/Object;");

		// Token: 0x04000013 RID: 19
		private static readonly IntPtr s_FieldGetDeclaringClass = AndroidReflection.GetMethodID("java/lang/reflect/Field", "getDeclaringClass", "()Ljava/lang/Class;");
	}
}
