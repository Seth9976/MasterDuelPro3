using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[NativeConditional("PLATFORM_ANDROID")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	public static class AndroidJNI
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00005594 File Offset: 0x00003794
		[ThreadSafe]
		private static void ReleaseStringChars(AndroidJNI.JStringBinding str)
		{
			AndroidJNI.ReleaseStringChars_Injected(ref str);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000055A8 File Offset: 0x000037A8
		[RequiredByNativeCode]
		private static void InvokeAction(Action action)
		{
			action();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000055B4 File Offset: 0x000037B4
		[ThreadSafe]
		public unsafe static IntPtr FindClass(string name)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = AndroidJNI.FindClass_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x0600005B RID: 91
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		// Token: 0x0600005C RID: 92
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ExceptionOccurred();

		// Token: 0x0600005D RID: 93
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionClear();

		// Token: 0x0600005E RID: 94
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PushLocalFrame(int capacity);

		// Token: 0x0600005F RID: 95
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr PopLocalFrame(IntPtr ptr);

		// Token: 0x06000060 RID: 96
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		// Token: 0x06000061 RID: 97
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void QueueDeleteGlobalRef(IntPtr obj);

		// Token: 0x06000062 RID: 98
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewWeakGlobalRef(IntPtr obj);

		// Token: 0x06000063 RID: 99
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteWeakGlobalRef(IntPtr obj);

		// Token: 0x06000064 RID: 100
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		// Token: 0x06000065 RID: 101
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteLocalRef(IntPtr obj);

		// Token: 0x06000066 RID: 102
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		// Token: 0x06000067 RID: 103 RVA: 0x0000560C File Offset: 0x0000380C
		public unsafe static IntPtr NewObject(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.NewObjectA(clazz, methodID, a);
			}
		}

		// Token: 0x06000068 RID: 104
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr NewObjectA(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000069 RID: 105
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		// Token: 0x0600006A RID: 106 RVA: 0x00005634 File Offset: 0x00003834
		[ThreadSafe]
		public unsafe static IntPtr GetMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr methodID_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sig, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sig.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				methodID_Injected = AndroidJNI.GetMethodID_Injected(clazz, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return methodID_Injected;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000056C0 File Offset: 0x000038C0
		[ThreadSafe]
		public unsafe static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sig, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sig.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				staticMethodID_Injected = AndroidJNI.GetStaticMethodID_Injected(clazz, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return staticMethodID_Injected;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000574C File Offset: 0x0000394C
		public static IntPtr NewString(string chars)
		{
			return AndroidJNI.NewStringFromStr(chars);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005764 File Offset: 0x00003964
		[ThreadSafe]
		private unsafe static IntPtr NewStringFromStr(string chars)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(chars, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = chars.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = AndroidJNI.NewStringFromStr_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000057BC File Offset: 0x000039BC
		public static string GetStringChars(IntPtr str)
		{
			string text;
			using (AndroidJNI.JStringBinding jstring = AndroidJNI.GetStringCharsInternal(str))
			{
				text = jstring.ToString();
			}
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005804 File Offset: 0x00003A04
		[ThreadSafe]
		private static AndroidJNI.JStringBinding GetStringCharsInternal(IntPtr str)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.GetStringCharsInternal_Injected(str, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000581C File Offset: 0x00003A1C
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStringMethod(obj, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000583C File Offset: 0x00003A3C
		public unsafe static string CallStringMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStringMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005864 File Offset: 0x00003A64
		public unsafe static string CallStringMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args)
		{
			string text;
			using (AndroidJNI.JStringBinding jstring = AndroidJNI.CallStringMethodUnsafeInternal(obj, methodID, args))
			{
				text = jstring.ToString();
			}
			return text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000058AC File Offset: 0x00003AAC
		[ThreadSafe]
		private unsafe static AndroidJNI.JStringBinding CallStringMethodUnsafeInternal(IntPtr obj, IntPtr methodID, jvalue* args)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.CallStringMethodUnsafeInternal_Injected(obj, methodID, args, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000058C4 File Offset: 0x00003AC4
		public unsafe static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallObjectMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000075 RID: 117
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr CallObjectMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000076 RID: 118 RVA: 0x000058EC File Offset: 0x00003AEC
		public unsafe static int CallIntMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallIntMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000077 RID: 119
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int CallIntMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000078 RID: 120 RVA: 0x00005914 File Offset: 0x00003B14
		public unsafe static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallBooleanMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000079 RID: 121
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern bool CallBooleanMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x0600007A RID: 122 RVA: 0x0000593C File Offset: 0x00003B3C
		public unsafe static short CallShortMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallShortMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x0600007B RID: 123
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern short CallShortMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x0600007C RID: 124 RVA: 0x00005964 File Offset: 0x00003B64
		public unsafe static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallSByteMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x0600007D RID: 125
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern sbyte CallSByteMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x0600007E RID: 126 RVA: 0x0000598C File Offset: 0x00003B8C
		public unsafe static char CallCharMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallCharMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x0600007F RID: 127
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char CallCharMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000080 RID: 128 RVA: 0x000059B4 File Offset: 0x00003BB4
		public unsafe static float CallFloatMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallFloatMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000081 RID: 129
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern float CallFloatMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000082 RID: 130 RVA: 0x000059DC File Offset: 0x00003BDC
		public unsafe static double CallDoubleMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallDoubleMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000083 RID: 131
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern double CallDoubleMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000084 RID: 132 RVA: 0x00005A04 File Offset: 0x00003C04
		public unsafe static long CallLongMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallLongMethodUnsafe(obj, methodID, a);
			}
		}

		// Token: 0x06000085 RID: 133
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern long CallLongMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		// Token: 0x06000086 RID: 134 RVA: 0x00005A2C File Offset: 0x00003C2C
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return AndroidJNI.CallStaticStringMethod(clazz, methodID, new Span<jvalue>(args));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005A4C File Offset: 0x00003C4C
		public unsafe static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticStringMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005A74 File Offset: 0x00003C74
		public unsafe static string CallStaticStringMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args)
		{
			string text;
			using (AndroidJNI.JStringBinding jstring = AndroidJNI.CallStaticStringMethodUnsafeInternal(clazz, methodID, args))
			{
				text = jstring.ToString();
			}
			return text;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005ABC File Offset: 0x00003CBC
		[ThreadSafe]
		private unsafe static AndroidJNI.JStringBinding CallStaticStringMethodUnsafeInternal(IntPtr clazz, IntPtr methodID, jvalue* args)
		{
			AndroidJNI.JStringBinding jstringBinding;
			AndroidJNI.CallStaticStringMethodUnsafeInternal_Injected(clazz, methodID, args, out jstringBinding);
			return jstringBinding;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public unsafe static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticObjectMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x0600008B RID: 139
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr CallStaticObjectMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600008C RID: 140 RVA: 0x00005AFC File Offset: 0x00003CFC
		public unsafe static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticIntMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x0600008D RID: 141
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int CallStaticIntMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600008E RID: 142 RVA: 0x00005B24 File Offset: 0x00003D24
		public unsafe static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticBooleanMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x0600008F RID: 143
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern bool CallStaticBooleanMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000090 RID: 144 RVA: 0x00005B4C File Offset: 0x00003D4C
		public unsafe static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticShortMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x06000091 RID: 145
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern short CallStaticShortMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000092 RID: 146 RVA: 0x00005B74 File Offset: 0x00003D74
		public unsafe static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticSByteMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x06000093 RID: 147
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern sbyte CallStaticSByteMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000094 RID: 148 RVA: 0x00005B9C File Offset: 0x00003D9C
		public unsafe static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticCharMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x06000095 RID: 149
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char CallStaticCharMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000096 RID: 150 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public unsafe static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticFloatMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x06000097 RID: 151
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern float CallStaticFloatMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x06000098 RID: 152 RVA: 0x00005BEC File Offset: 0x00003DEC
		public unsafe static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticDoubleMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x06000099 RID: 153
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern double CallStaticDoubleMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600009A RID: 154 RVA: 0x00005C14 File Offset: 0x00003E14
		public unsafe static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* pinnableReference = args.GetPinnableReference())
			{
				jvalue* a = pinnableReference;
				return AndroidJNI.CallStaticLongMethodUnsafe(clazz, methodID, a);
			}
		}

		// Token: 0x0600009B RID: 155
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern long CallStaticLongMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		// Token: 0x0600009C RID: 156 RVA: 0x00005C3C File Offset: 0x00003E3C
		[ThreadSafe]
		public unsafe static IntPtr ToBooleanArray(bool[] array)
		{
			Span<bool> span = new Span<bool>(array);
			IntPtr intPtr;
			fixed (bool* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AndroidJNI.ToBooleanArray_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005C74 File Offset: 0x00003E74
		[ThreadSafe]
		[Obsolete("AndroidJNI.ToByteArray is obsolete. Use AndroidJNI.ToSByteArray method instead")]
		public unsafe static IntPtr ToByteArray(byte[] array)
		{
			Span<byte> span = new Span<byte>(array);
			IntPtr intPtr;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AndroidJNI.ToByteArray_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005CAC File Offset: 0x00003EAC
		public unsafe static IntPtr ToSByteArray(sbyte[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				sbyte* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToSByteArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x0600009F RID: 159
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToSByteArray(sbyte* array, int length);

		// Token: 0x060000A0 RID: 160 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public unsafe static IntPtr ToCharArray(char[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				char* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToCharArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x060000A1 RID: 161
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToCharArray(char* array, int length);

		// Token: 0x060000A2 RID: 162 RVA: 0x00005D3C File Offset: 0x00003F3C
		public unsafe static IntPtr ToShortArray(short[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				short* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToShortArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x060000A3 RID: 163
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToShortArray(short* array, int length);

		// Token: 0x060000A4 RID: 164 RVA: 0x00005D84 File Offset: 0x00003F84
		public unsafe static IntPtr ToIntArray(int[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				int* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToIntArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x060000A5 RID: 165
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToIntArray(int* array, int length);

		// Token: 0x060000A6 RID: 166 RVA: 0x00005DCC File Offset: 0x00003FCC
		public unsafe static IntPtr ToLongArray(long[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				long* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToLongArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x060000A7 RID: 167
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToLongArray(long* array, int length);

		// Token: 0x060000A8 RID: 168 RVA: 0x00005E14 File Offset: 0x00004014
		public unsafe static IntPtr ToFloatArray(float[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				float* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToFloatArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x060000A9 RID: 169
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToFloatArray(float* array, int length);

		// Token: 0x060000AA RID: 170 RVA: 0x00005E5C File Offset: 0x0000405C
		public unsafe static IntPtr ToDoubleArray(double[] array)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				double* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToDoubleArray(ptr, array.Length);
			}
			return intPtr;
		}

		// Token: 0x060000AB RID: 171
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToDoubleArray(double* array, int length);

		// Token: 0x060000AC RID: 172
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern IntPtr ToObjectArray(IntPtr* array, int length, IntPtr arrayClass);

		// Token: 0x060000AD RID: 173 RVA: 0x00005EA4 File Offset: 0x000040A4
		public unsafe static IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass)
		{
			bool flag = array == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				IntPtr* ptr;
				if (array == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				intPtr = AndroidJNI.ToObjectArray(ptr, array.Length, arrayClass);
			}
			return intPtr;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005EEC File Offset: 0x000040EC
		[ThreadSafe]
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				AndroidJNI.FromBooleanArray_Injected(array, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				bool[] array2;
				blittableArrayWrapper.Unmarshal<bool>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005F20 File Offset: 0x00004120
		[Obsolete("AndroidJNI.FromByteArray is obsolete. Use AndroidJNI.FromSByteArray method instead")]
		[ThreadSafe]
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				AndroidJNI.FromByteArray_Injected(array, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x060000B0 RID: 176
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern sbyte[] FromSByteArray(IntPtr array);

		// Token: 0x060000B1 RID: 177
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern char[] FromCharArray(IntPtr array);

		// Token: 0x060000B2 RID: 178
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern short[] FromShortArray(IntPtr array);

		// Token: 0x060000B3 RID: 179
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern int[] FromIntArray(IntPtr array);

		// Token: 0x060000B4 RID: 180
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern long[] FromLongArray(IntPtr array);

		// Token: 0x060000B5 RID: 181
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern float[] FromFloatArray(IntPtr array);

		// Token: 0x060000B6 RID: 182
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern double[] FromDoubleArray(IntPtr array);

		// Token: 0x060000B7 RID: 183
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetArrayLength(IntPtr array);

		// Token: 0x060000B8 RID: 184
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		// Token: 0x060000B9 RID: 185
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		// Token: 0x060000BA RID: 186
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseStringChars_Injected([In] ref AndroidJNI.JStringBinding str);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindClass_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMethodID_Injected(IntPtr clazz, ref ManagedSpanWrapper name, ref ManagedSpanWrapper sig);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetStaticMethodID_Injected(IntPtr clazz, ref ManagedSpanWrapper name, ref ManagedSpanWrapper sig);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NewStringFromStr_Injected(ref ManagedSpanWrapper chars);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStringCharsInternal_Injected(IntPtr str, out AndroidJNI.JStringBinding ret);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CallStringMethodUnsafeInternal_Injected(IntPtr obj, IntPtr methodID, jvalue* args, out AndroidJNI.JStringBinding ret);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CallStaticStringMethodUnsafeInternal_Injected(IntPtr clazz, IntPtr methodID, jvalue* args, out AndroidJNI.JStringBinding ret);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ToBooleanArray_Injected(ref ManagedSpanWrapper array);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ToByteArray_Injected(ref ManagedSpanWrapper array);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromBooleanArray_Injected(IntPtr array, out BlittableArrayWrapper ret);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromByteArray_Injected(IntPtr array, out BlittableArrayWrapper ret);

		// Token: 0x0200000E RID: 14
		private struct JStringBinding : IDisposable
		{
			// Token: 0x060000C7 RID: 199 RVA: 0x00005F54 File Offset: 0x00004154
			public unsafe override string ToString()
			{
				bool flag = this.length == 0;
				string text;
				if (flag)
				{
					text = ((this.chars == IntPtr.Zero) ? null : string.Empty);
				}
				else
				{
					text = new string((char*)(void*)this.chars, 0, this.length);
				}
				return text;
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00005FA8 File Offset: 0x000041A8
			public void Dispose()
			{
				bool flag = this.length > 0;
				if (flag)
				{
					AndroidJNI.ReleaseStringChars(this);
				}
			}

			// Token: 0x0400001E RID: 30
			private IntPtr javaString;

			// Token: 0x0400001F RID: 31
			private IntPtr chars;

			// Token: 0x04000020 RID: 32
			private int length;

			// Token: 0x04000021 RID: 33
			private bool ownsRef;
		}
	}
}
