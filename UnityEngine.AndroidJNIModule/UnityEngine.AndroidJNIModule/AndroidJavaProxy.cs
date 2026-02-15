using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public class AndroidJavaProxy
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021BF File Offset: 0x000003BF
		public AndroidJavaProxy(string javaInterface)
			: this(new AndroidJavaClass(javaInterface))
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021CF File Offset: 0x000003CF
		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EC File Offset: 0x000003EC
		~AndroidJavaProxy()
		{
			AndroidJNISafe.DeleteWeakGlobalRef(this.proxyObject);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002224 File Offset: 0x00000424
		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception error = null;
			BindingFlags binderFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			int NullArgs = 0;
			Type[] argTypes = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				bool flag = args[i] == null;
				if (flag)
				{
					argTypes[i] = null;
					NullArgs++;
				}
				else
				{
					argTypes[i] = args[i].GetType();
				}
			}
			try
			{
				MethodInfo methodResult = null;
				bool flag2 = NullArgs > 0;
				if (flag2)
				{
					MethodInfo[] methods = base.GetType().GetMethods(binderFlags);
					int matches = 0;
					foreach (MethodInfo method in methods)
					{
						bool flag3 = methodName != method.Name;
						if (!flag3)
						{
							ParameterInfo[] methodParameters = method.GetParameters();
							bool flag4 = methodParameters.Length != args.Length;
							if (!flag4)
							{
								bool isOk = true;
								for (int j = 0; j < methodParameters.Length; j++)
								{
									bool flag5 = argTypes[j] == null;
									if (flag5)
									{
										bool isValueType = methodParameters[j].ParameterType.IsValueType;
										if (isValueType)
										{
											isOk = false;
											break;
										}
									}
									else
									{
										bool flag6 = !methodParameters[j].ParameterType.IsAssignableFrom(argTypes[j]);
										if (flag6)
										{
											isOk = false;
											break;
										}
									}
								}
								bool flag7 = !isOk;
								if (!flag7)
								{
									matches++;
									methodResult = method;
								}
							}
						}
					}
					bool flag8 = matches > 1;
					if (flag8)
					{
						throw new Exception("Ambiguous overloads found for " + methodName + " with given parameters");
					}
				}
				else
				{
					methodResult = base.GetType().GetMethod(methodName, binderFlags, null, argTypes, null);
				}
				bool flag9 = methodResult != null;
				if (flag9)
				{
					return _AndroidJNIHelper.Box(methodResult.Invoke(this, args));
				}
			}
			catch (TargetInvocationException invocationError)
			{
				error = invocationError.InnerException;
			}
			catch (Exception invocationError2)
			{
				error = invocationError2;
			}
			string[] argTypeNames = new string[args.Length];
			for (int k = 0; k < argTypeNames.Length; k++)
			{
				bool flag10 = argTypes[k] == null;
				if (flag10)
				{
					argTypeNames[k] = "null";
				}
				else
				{
					argTypeNames[k] = argTypes[k].ToString();
				}
			}
			bool flag11 = error != null;
			if (flag11)
			{
				string[] array2 = new string[6];
				int num = 0;
				Type type = base.GetType();
				array2[num] = ((type != null) ? type.ToString() : null);
				array2[1] = ".";
				array2[2] = methodName;
				array2[3] = "(";
				array2[4] = string.Join(",", argTypeNames);
				array2[5] = ")";
				throw new TargetInvocationException(string.Concat(array2), error);
			}
			string[] array3 = new string[7];
			array3[0] = "No such proxy method: ";
			int num2 = 1;
			Type type2 = base.GetType();
			array3[num2] = ((type2 != null) ? type2.ToString() : null);
			array3[2] = ".";
			array3[3] = methodName;
			array3[4] = "(";
			array3[5] = string.Join(",", argTypeNames);
			array3[6] = ")";
			Exception ex = new Exception(string.Concat(array3));
			IntPtr nativeError = AndroidReflection.CreateInvocationError(ex, true);
			return (nativeError == IntPtr.Zero) ? null : new AndroidJavaObject(nativeError);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002564 File Offset: 0x00000764
		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] args = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				args[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
				bool flag = !(args[i] is AndroidJavaObject);
				if (flag)
				{
					bool flag2 = javaArgs[i] != null;
					if (flag2)
					{
						javaArgs[i].Dispose();
					}
				}
			}
			return this.Invoke(methodName, args);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025D4 File Offset: 0x000007D4
		public virtual IntPtr Invoke(string methodName, IntPtr javaArgs)
		{
			int arrayLen = 0;
			bool flag = javaArgs != IntPtr.Zero;
			if (flag)
			{
				arrayLen = AndroidJNISafe.GetArrayLength(javaArgs);
			}
			bool flag2 = arrayLen == 1 && methodName == "equals";
			IntPtr intPtr;
			if (flag2)
			{
				IntPtr o = AndroidJNISafe.GetObjectArrayElement(javaArgs, 0);
				AndroidJavaObject obj = ((o == IntPtr.Zero) ? null : new AndroidJavaObject(o));
				intPtr = AndroidJNIHelper.Box(this.equals(obj));
			}
			else
			{
				bool flag3 = arrayLen == 0 && methodName == "hashCode";
				if (flag3)
				{
					intPtr = AndroidJNIHelper.Box(this.hashCode());
				}
				else
				{
					AndroidJavaObject[] args = new AndroidJavaObject[arrayLen];
					for (int i = 0; i < arrayLen; i++)
					{
						IntPtr objectRef = AndroidJNISafe.GetObjectArrayElement(javaArgs, i);
						args[i] = ((objectRef != IntPtr.Zero) ? AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectRef) : null);
					}
					using (AndroidJavaObject result = this.Invoke(methodName, args))
					{
						bool flag4 = result == null;
						if (flag4)
						{
							intPtr = IntPtr.Zero;
						}
						else
						{
							intPtr = AndroidJNI.NewLocalRef(result.GetRawObject());
						}
					}
				}
			}
			return intPtr;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002708 File Offset: 0x00000908
		public virtual bool equals(AndroidJavaObject obj)
		{
			IntPtr anotherObject = ((obj == null) ? IntPtr.Zero : obj.GetRawObject());
			return AndroidJNI.IsSameObject(this.proxyObject, anotherObject);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002738 File Offset: 0x00000938
		public unsafe virtual int hashCode()
		{
			checked
			{
				Span<jvalue> span = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)1) * (UIntPtr)sizeof(jvalue)], 1);
				Span<jvalue> jniArgs = span;
				jniArgs[0].l = this.GetRawProxy();
				return AndroidJNISafe.CallStaticIntMethod(AndroidJavaProxy.s_JavaLangSystemClass, AndroidJavaProxy.s_HashCodeMethodID, jniArgs);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002788 File Offset: 0x00000988
		public virtual string toString()
		{
			return ((this != null) ? this.ToString() : null) + " <c# proxy java object>";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027B4 File Offset: 0x000009B4
		internal AndroidJavaObject GetProxyObject()
		{
			return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(this.GetRawProxy());
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027D4 File Offset: 0x000009D4
		internal IntPtr GetRawProxy()
		{
			IntPtr ret = IntPtr.Zero;
			bool flag = this.proxyObject != IntPtr.Zero;
			if (flag)
			{
				ret = AndroidJNI.NewLocalRef(this.proxyObject);
				bool flag2 = ret == IntPtr.Zero;
				if (flag2)
				{
					AndroidJNI.DeleteWeakGlobalRef(this.proxyObject);
					this.proxyObject = IntPtr.Zero;
				}
			}
			bool flag3 = ret == IntPtr.Zero;
			if (flag3)
			{
				ret = AndroidJNIHelper.CreateJavaProxy(this);
				this.proxyObject = AndroidJNI.NewWeakGlobalRef(ret);
			}
			return ret;
		}

		// Token: 0x04000005 RID: 5
		public readonly AndroidJavaClass javaInterface;

		// Token: 0x04000006 RID: 6
		internal IntPtr proxyObject = IntPtr.Zero;

		// Token: 0x04000007 RID: 7
		private static readonly GlobalJavaObjectRef s_JavaLangSystemClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("java/lang/System"));

		// Token: 0x04000008 RID: 8
		private static readonly IntPtr s_HashCodeMethodID = AndroidJNIHelper.GetMethodID(AndroidJavaProxy.s_JavaLangSystemClass, "identityHashCode", "(Ljava/lang/Object;)I", true);
	}
}
