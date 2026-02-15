using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public class AndroidJavaObject : IDisposable
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002892 File Offset: 0x00000A92
		public AndroidJavaObject(string className, params object[] args)
			: this()
		{
			this._AndroidJavaObject(className, args);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028A8 File Offset: 0x00000AA8
		public AndroidJavaObject(IntPtr jobject)
			: this()
		{
			bool flag = jobject == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr jclass = AndroidJNISafe.GetObjectClass(jobject);
			this.m_jobject = new GlobalJavaObjectRef(jobject);
			this.m_jclass = new GlobalJavaObjectRef(jclass);
			AndroidJNISafe.DeleteLocalRef(jclass);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028FE File Offset: 0x00000AFE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002910 File Offset: 0x00000B10
		public IntPtr GetRawObject()
		{
			return this._GetRawObject();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002928 File Offset: 0x00000B28
		public IntPtr GetRawClass()
		{
			return this._GetRawClass();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002940 File Offset: 0x00000B40
		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._Call<ReturnType>(methodName, args);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000295C File Offset: 0x00000B5C
		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodName, args);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002978 File Offset: 0x00000B78
		protected void DebugPrint(string msg)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				Debug.Log(msg);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000299C File Offset: 0x00000B9C
		private void _AndroidJavaObject(string className, params object[] args)
		{
			this.DebugPrint("Creating AndroidJavaObject from " + className);
			IntPtr clazz = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(clazz);
			AndroidJNISafe.DeleteLocalRef(clazz);
			IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.m_jclass, args);
			this._AndroidJavaObject(constructorID, args);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029FC File Offset: 0x00000BFC
		private unsafe void _AndroidJavaObject(IntPtr constructorID, params object[] args)
		{
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				Span<jvalue> jniArgs = span;
				AndroidJNIHelper.CreateJNIArgArray(args, jniArgs);
				try
				{
					IntPtr jobject = AndroidJNISafe.NewObject(this.m_jclass, constructorID, jniArgs);
					this.m_jobject = new GlobalJavaObjectRef(jobject);
					AndroidJNISafe.DeleteLocalRef(jobject);
				}
				finally
				{
					AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A88 File Offset: 0x00000C88
		internal AndroidJavaObject()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A94 File Offset: 0x00000C94
		~AndroidJavaObject()
		{
			this.Dispose(false);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AC8 File Offset: 0x00000CC8
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_jobject != null;
			if (flag)
			{
				this.m_jobject.Dispose();
				this.m_jobject = null;
			}
			bool flag2 = this.m_jclass != null;
			if (flag2)
			{
				this.m_jclass.Dispose();
				this.m_jclass = null;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B1C File Offset: 0x00000D1C
		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, false);
			return this._Call<ReturnType>(methodID, args);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B4C File Offset: 0x00000D4C
		protected unsafe ReturnType _Call<ReturnType>(IntPtr methodID, params object[] args)
		{
			Span<jvalue> jniArgs;
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				jniArgs = span;
			}
			AndroidJNI.PushLocalFrame(jniArgs.Length + 1);
			AndroidJNIHelper.CreateJNIArgArray(args, jniArgs);
			ReturnType returnType;
			try
			{
				bool flag = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag)
				{
					bool flag2 = typeof(ReturnType) == typeof(int);
					if (flag2)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallIntMethod(this.m_jobject, methodID, jniArgs));
					}
					else
					{
						bool flag3 = typeof(ReturnType) == typeof(bool);
						if (flag3)
						{
							returnType = (ReturnType)((object)AndroidJNISafe.CallBooleanMethod(this.m_jobject, methodID, jniArgs));
						}
						else
						{
							bool flag4 = typeof(ReturnType) == typeof(byte);
							if (flag4)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								returnType = (ReturnType)((object)((byte)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, jniArgs)));
							}
							else
							{
								bool flag5 = typeof(ReturnType) == typeof(sbyte);
								if (flag5)
								{
									returnType = (ReturnType)((object)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, jniArgs));
								}
								else
								{
									bool flag6 = typeof(ReturnType) == typeof(short);
									if (flag6)
									{
										returnType = (ReturnType)((object)AndroidJNISafe.CallShortMethod(this.m_jobject, methodID, jniArgs));
									}
									else
									{
										bool flag7 = typeof(ReturnType) == typeof(long);
										if (flag7)
										{
											returnType = (ReturnType)((object)AndroidJNISafe.CallLongMethod(this.m_jobject, methodID, jniArgs));
										}
										else
										{
											bool flag8 = typeof(ReturnType) == typeof(float);
											if (flag8)
											{
												returnType = (ReturnType)((object)AndroidJNISafe.CallFloatMethod(this.m_jobject, methodID, jniArgs));
											}
											else
											{
												bool flag9 = typeof(ReturnType) == typeof(double);
												if (flag9)
												{
													returnType = (ReturnType)((object)AndroidJNISafe.CallDoubleMethod(this.m_jobject, methodID, jniArgs));
												}
												else
												{
													bool flag10 = typeof(ReturnType) == typeof(char);
													if (flag10)
													{
														returnType = (ReturnType)((object)AndroidJNISafe.CallCharMethod(this.m_jobject, methodID, jniArgs));
													}
													else
													{
														returnType = default(ReturnType);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag11 = typeof(ReturnType) == typeof(string);
					if (flag11)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStringMethod(this.m_jobject, methodID, jniArgs));
					}
					else
					{
						bool flag12 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag12)
						{
							IntPtr jclass = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, jniArgs);
							returnType = ((jclass == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaClass(jclass))));
						}
						else
						{
							bool flag13 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag13)
							{
								IntPtr jobject = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, jniArgs);
								returnType = ((jobject == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaObject(jobject))));
							}
							else
							{
								bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag14)
								{
									string text = "JNI: Unknown return type '";
									Type typeFromHandle = typeof(ReturnType);
									throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
								}
								IntPtr jobject2 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, jniArgs);
								returnType = AndroidJavaObject.FromJavaArray<ReturnType>(jobject2);
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
			}
			return returnType;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002FC0 File Offset: 0x000011C0
		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, true);
			return this._CallStatic<ReturnType>(methodID, args);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002FF0 File Offset: 0x000011F0
		protected unsafe ReturnType _CallStatic<ReturnType>(IntPtr methodID, params object[] args)
		{
			Span<jvalue> jniArgs;
			checked
			{
				Span<jvalue> span;
				if (args == null || args.Length == 0)
				{
					span = default(Span<jvalue>);
				}
				else
				{
					int num = args.Length;
					Span<jvalue> span2 = new Span<jvalue>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(jvalue)], num);
					span = span2;
				}
				jniArgs = span;
			}
			AndroidJNI.PushLocalFrame(jniArgs.Length + 1);
			AndroidJNIHelper.CreateJNIArgArray(args, jniArgs);
			ReturnType returnType;
			try
			{
				bool flag = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag)
				{
					bool flag2 = typeof(ReturnType) == typeof(int);
					if (flag2)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticIntMethod(this.m_jclass, methodID, jniArgs));
					}
					else
					{
						bool flag3 = typeof(ReturnType) == typeof(bool);
						if (flag3)
						{
							returnType = (ReturnType)((object)AndroidJNISafe.CallStaticBooleanMethod(this.m_jclass, methodID, jniArgs));
						}
						else
						{
							bool flag4 = typeof(ReturnType) == typeof(byte);
							if (flag4)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								returnType = (ReturnType)((object)((byte)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, jniArgs)));
							}
							else
							{
								bool flag5 = typeof(ReturnType) == typeof(sbyte);
								if (flag5)
								{
									returnType = (ReturnType)((object)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, jniArgs));
								}
								else
								{
									bool flag6 = typeof(ReturnType) == typeof(short);
									if (flag6)
									{
										returnType = (ReturnType)((object)AndroidJNISafe.CallStaticShortMethod(this.m_jclass, methodID, jniArgs));
									}
									else
									{
										bool flag7 = typeof(ReturnType) == typeof(long);
										if (flag7)
										{
											returnType = (ReturnType)((object)AndroidJNISafe.CallStaticLongMethod(this.m_jclass, methodID, jniArgs));
										}
										else
										{
											bool flag8 = typeof(ReturnType) == typeof(float);
											if (flag8)
											{
												returnType = (ReturnType)((object)AndroidJNISafe.CallStaticFloatMethod(this.m_jclass, methodID, jniArgs));
											}
											else
											{
												bool flag9 = typeof(ReturnType) == typeof(double);
												if (flag9)
												{
													returnType = (ReturnType)((object)AndroidJNISafe.CallStaticDoubleMethod(this.m_jclass, methodID, jniArgs));
												}
												else
												{
													bool flag10 = typeof(ReturnType) == typeof(char);
													if (flag10)
													{
														returnType = (ReturnType)((object)AndroidJNISafe.CallStaticCharMethod(this.m_jclass, methodID, jniArgs));
													}
													else
													{
														returnType = default(ReturnType);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag11 = typeof(ReturnType) == typeof(string);
					if (flag11)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticStringMethod(this.m_jclass, methodID, jniArgs));
					}
					else
					{
						bool flag12 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag12)
						{
							IntPtr jclass = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, jniArgs);
							returnType = ((jclass == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaClass(jclass))));
						}
						else
						{
							bool flag13 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag13)
							{
								IntPtr jobject = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, jniArgs);
								returnType = ((jobject == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)new AndroidJavaObject(jobject))));
							}
							else
							{
								bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag14)
								{
									string text = "JNI: Unknown return type '";
									Type typeFromHandle = typeof(ReturnType);
									throw new Exception(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "'");
								}
								IntPtr jobject2 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, jniArgs);
								returnType = AndroidJavaObject.FromJavaArray<ReturnType>(jobject2);
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
			}
			return returnType;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003464 File Offset: 0x00001664
		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			AndroidJavaObject androidJavaObject;
			try
			{
				androidJavaObject = new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
			return androidJavaObject;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003498 File Offset: 0x00001698
		internal static ReturnType FromJavaArray<ReturnType>(IntPtr jobject)
		{
			bool flag = jobject == IntPtr.Zero;
			ReturnType returnType;
			if (flag)
			{
				returnType = default(ReturnType);
			}
			else
			{
				returnType = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(jobject));
			}
			return returnType;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000034D8 File Offset: 0x000016D8
		protected IntPtr _GetRawObject()
		{
			return (this.m_jobject == null) ? IntPtr.Zero : this.m_jobject;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003504 File Offset: 0x00001704
		protected IntPtr _GetRawClass()
		{
			return this.m_jclass;
		}

		// Token: 0x04000009 RID: 9
		private static bool enableDebugPrints;

		// Token: 0x0400000A RID: 10
		internal GlobalJavaObjectRef m_jobject;

		// Token: 0x0400000B RID: 11
		internal GlobalJavaObjectRef m_jclass;
	}
}
