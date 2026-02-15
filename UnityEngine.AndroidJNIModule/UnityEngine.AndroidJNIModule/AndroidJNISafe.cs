using System;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	internal class AndroidJNISafe
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00005FD0 File Offset: 0x000041D0
		public static void CheckException()
		{
			IntPtr jthrowable = AndroidJNI.ExceptionOccurred();
			bool flag = jthrowable != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.ExceptionClear();
				IntPtr jthrowableClass = AndroidJNI.FindClass("java/lang/Throwable");
				IntPtr androidUtilLogClass = AndroidJNI.FindClass("android/util/Log");
				try
				{
					IntPtr toStringMethodId = AndroidJNI.GetMethodID(jthrowableClass, "toString", "()Ljava/lang/String;");
					IntPtr getStackTraceStringMethodId = AndroidJNI.GetStaticMethodID(androidUtilLogClass, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
					string exceptionMessage = AndroidJNI.CallStringMethod(jthrowable, toStringMethodId, new jvalue[0]);
					jvalue[] jniArgs = new jvalue[1];
					jniArgs[0].l = jthrowable;
					string exceptionCallStack = AndroidJNI.CallStaticStringMethod(androidUtilLogClass, getStackTraceStringMethodId, jniArgs);
					throw new AndroidJavaException(exceptionMessage, exceptionCallStack);
				}
				finally
				{
					AndroidJNISafe.DeleteLocalRef(jthrowable);
					AndroidJNISafe.DeleteLocalRef(jthrowableClass);
					AndroidJNISafe.DeleteLocalRef(androidUtilLogClass);
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000609C File Offset: 0x0000429C
		public static void QueueDeleteGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.QueueDeleteGlobalRef(globalref);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000060C0 File Offset: 0x000042C0
		public static void DeleteWeakGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteWeakGlobalRef(globalref);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000060E4 File Offset: 0x000042E4
		public static void DeleteLocalRef(IntPtr localref)
		{
			bool flag = localref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteLocalRef(localref);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006108 File Offset: 0x00004308
		public static IntPtr NewString(string chars)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewString(chars);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000613C File Offset: 0x0000433C
		public static string GetStringChars(IntPtr str)
		{
			string stringChars;
			try
			{
				stringChars = AndroidJNI.GetStringChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringChars;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006170 File Offset: 0x00004370
		public static IntPtr GetObjectClass(IntPtr ptr)
		{
			IntPtr objectClass;
			try
			{
				objectClass = AndroidJNI.GetObjectClass(ptr);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectClass;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000061A4 File Offset: 0x000043A4
		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNI.GetStaticMethodID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticMethodID;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000061D8 File Offset: 0x000043D8
		public static IntPtr GetMethodID(IntPtr obj, string name, string sig)
		{
			IntPtr methodID;
			try
			{
				methodID = AndroidJNI.GetMethodID(obj, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return methodID;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000620C File Offset: 0x0000440C
		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FromReflectedMethod(refMethod);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006240 File Offset: 0x00004440
		public static IntPtr FindClass(string name)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FindClass(name);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006274 File Offset: 0x00004474
		public static void PushLocalFrame(int capacity)
		{
			bool flag = AndroidJNI.PushLocalFrame(capacity) < 0;
			if (flag)
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006298 File Offset: 0x00004498
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewObject(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000062CC File Offset: 0x000044CC
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNISafe.CallStaticObjectMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000062EC File Offset: 0x000044EC
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.CallStaticObjectMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006320 File Offset: 0x00004520
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			string ret = null;
			string text;
			try
			{
				ret = AndroidJNI.CallStaticStringMethod(clazz, methodID, args);
				text = ret;
			}
			finally
			{
				bool flag = ret == null;
				if (flag)
				{
					AndroidJNISafe.CheckException();
				}
			}
			return text;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006360 File Offset: 0x00004560
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			char c;
			try
			{
				c = AndroidJNI.CallStaticCharMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return c;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006394 File Offset: 0x00004594
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			double num;
			try
			{
				num = AndroidJNI.CallStaticDoubleMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000063C8 File Offset: 0x000045C8
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			float num;
			try
			{
				num = AndroidJNI.CallStaticFloatMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000063FC File Offset: 0x000045FC
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			long num;
			try
			{
				num = AndroidJNI.CallStaticLongMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006430 File Offset: 0x00004630
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			short num;
			try
			{
				num = AndroidJNI.CallStaticShortMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006464 File Offset: 0x00004664
		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			sbyte b;
			try
			{
				b = AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return b;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006498 File Offset: 0x00004698
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			bool flag;
			try
			{
				flag = AndroidJNI.CallStaticBooleanMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return flag;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000064CC File Offset: 0x000046CC
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			int num;
			try
			{
				num = AndroidJNI.CallStaticIntMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006500 File Offset: 0x00004700
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.CallObjectMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006534 File Offset: 0x00004734
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			string ret = null;
			string text;
			try
			{
				ret = AndroidJNI.CallStringMethod(obj, methodID, args);
				text = ret;
			}
			finally
			{
				bool flag = ret == null;
				if (flag)
				{
					AndroidJNISafe.CheckException();
				}
			}
			return text;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006574 File Offset: 0x00004774
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			char c;
			try
			{
				c = AndroidJNI.CallCharMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return c;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000065A8 File Offset: 0x000047A8
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			double num;
			try
			{
				num = AndroidJNI.CallDoubleMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000065DC File Offset: 0x000047DC
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			float num;
			try
			{
				num = AndroidJNI.CallFloatMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006610 File Offset: 0x00004810
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			long num;
			try
			{
				num = AndroidJNI.CallLongMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006644 File Offset: 0x00004844
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			short num;
			try
			{
				num = AndroidJNI.CallShortMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006678 File Offset: 0x00004878
		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			sbyte b;
			try
			{
				b = AndroidJNI.CallSByteMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return b;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000066AC File Offset: 0x000048AC
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			bool flag;
			try
			{
				flag = AndroidJNI.CallBooleanMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return flag;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000066E0 File Offset: 0x000048E0
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			int num;
			try
			{
				num = AndroidJNI.CallIntMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006714 File Offset: 0x00004914
		public static char[] FromCharArray(IntPtr array)
		{
			char[] array2;
			try
			{
				array2 = AndroidJNI.FromCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006748 File Offset: 0x00004948
		public static double[] FromDoubleArray(IntPtr array)
		{
			double[] array2;
			try
			{
				array2 = AndroidJNI.FromDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000677C File Offset: 0x0000497C
		public static float[] FromFloatArray(IntPtr array)
		{
			float[] array2;
			try
			{
				array2 = AndroidJNI.FromFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000067B0 File Offset: 0x000049B0
		public static long[] FromLongArray(IntPtr array)
		{
			long[] array2;
			try
			{
				array2 = AndroidJNI.FromLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000067E4 File Offset: 0x000049E4
		public static short[] FromShortArray(IntPtr array)
		{
			short[] array2;
			try
			{
				array2 = AndroidJNI.FromShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006818 File Offset: 0x00004A18
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] array2;
			try
			{
				array2 = AndroidJNI.FromByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000684C File Offset: 0x00004A4C
		public static sbyte[] FromSByteArray(IntPtr array)
		{
			sbyte[] array2;
			try
			{
				array2 = AndroidJNI.FromSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006880 File Offset: 0x00004A80
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] array2;
			try
			{
				array2 = AndroidJNI.FromBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000068B4 File Offset: 0x00004AB4
		public static int[] FromIntArray(IntPtr array)
		{
			int[] array2;
			try
			{
				array2 = AndroidJNI.FromIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToObjectArray(array, type);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000691C File Offset: 0x00004B1C
		public static IntPtr ToCharArray(char[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006950 File Offset: 0x00004B50
		public static IntPtr ToDoubleArray(double[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006984 File Offset: 0x00004B84
		public static IntPtr ToFloatArray(float[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000069B8 File Offset: 0x00004BB8
		public static IntPtr ToLongArray(long[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000069EC File Offset: 0x00004BEC
		public static IntPtr ToShortArray(short[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006A20 File Offset: 0x00004C20
		public static IntPtr ToByteArray(byte[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006A54 File Offset: 0x00004C54
		public static IntPtr ToSByteArray(sbyte[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006A88 File Offset: 0x00004C88
		public static IntPtr ToBooleanArray(bool[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006ABC File Offset: 0x00004CBC
		public static IntPtr ToIntArray(int[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			IntPtr objectArrayElement;
			try
			{
				objectArrayElement = AndroidJNI.GetObjectArrayElement(array, index);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectArrayElement;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006B24 File Offset: 0x00004D24
		public static int GetArrayLength(IntPtr array)
		{
			int arrayLength;
			try
			{
				arrayLength = AndroidJNI.GetArrayLength(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return arrayLength;
		}
	}
}
