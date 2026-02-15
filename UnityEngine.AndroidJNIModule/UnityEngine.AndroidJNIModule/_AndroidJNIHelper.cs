using System;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[UsedByNativeCode]
	internal sealed class _AndroidJNIHelper
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00003910 File Offset: 0x00001B10
		public static IntPtr CreateJavaProxy(IntPtr player, IntPtr delegateHandle, AndroidJavaProxy proxy)
		{
			return AndroidReflection.NewProxyInstance(player, delegateHandle, proxy.javaInterface.GetRawClass());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003934 File Offset: 0x00001B34
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return AndroidJNIHelper.CreateJavaProxy(new AndroidJavaRunnableProxy(jrunnable));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003954 File Offset: 0x00001B54
		[RequiredByNativeCode]
		public static IntPtr InvokeJavaProxyMethod(AndroidJavaProxy proxy, IntPtr jmethodName, IntPtr jargs)
		{
			IntPtr intPtr;
			try
			{
				intPtr = proxy.Invoke(AndroidJNI.GetStringChars(jmethodName), jargs);
			}
			catch (Exception e)
			{
				intPtr = AndroidReflection.CreateInvocationError(e, false);
			}
			return intPtr;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003990 File Offset: 0x00001B90
		public static void CreateJNIArgArray(object[] args, Span<jvalue> ret)
		{
			int i = 0;
			foreach (object obj in args)
			{
				bool flag = obj == null;
				if (flag)
				{
					ret[i].l = IntPtr.Zero;
				}
				else
				{
					bool flag2 = AndroidReflection.IsPrimitive(obj.GetType());
					if (flag2)
					{
						bool flag3 = obj is int;
						if (flag3)
						{
							ret[i].i = (int)obj;
						}
						else
						{
							bool flag4 = obj is bool;
							if (flag4)
							{
								ret[i].z = (bool)obj;
							}
							else
							{
								bool flag5 = obj is byte;
								if (flag5)
								{
									Debug.LogWarning("Passing Byte arguments to Java methods is obsolete, pass SByte parameters instead");
									ret[i].b = (sbyte)((byte)obj);
								}
								else
								{
									bool flag6 = obj is sbyte;
									if (flag6)
									{
										ret[i].b = (sbyte)obj;
									}
									else
									{
										bool flag7 = obj is short;
										if (flag7)
										{
											ret[i].s = (short)obj;
										}
										else
										{
											bool flag8 = obj is long;
											if (flag8)
											{
												ret[i].j = (long)obj;
											}
											else
											{
												bool flag9 = obj is float;
												if (flag9)
												{
													ret[i].f = (float)obj;
												}
												else
												{
													bool flag10 = obj is double;
													if (flag10)
													{
														ret[i].d = (double)obj;
													}
													else
													{
														bool flag11 = obj is char;
														if (flag11)
														{
															ret[i].c = (char)obj;
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
						bool flag12 = obj is string;
						if (flag12)
						{
							ret[i].l = AndroidJNISafe.NewString((string)obj);
						}
						else
						{
							bool flag13 = obj is AndroidJavaClass;
							if (flag13)
							{
								ret[i].l = ((AndroidJavaClass)obj).GetRawClass();
							}
							else
							{
								bool flag14 = obj is AndroidJavaObject;
								if (flag14)
								{
									ret[i].l = ((AndroidJavaObject)obj).GetRawObject();
								}
								else
								{
									bool flag15 = obj is Array;
									if (flag15)
									{
										ret[i].l = _AndroidJNIHelper.ConvertToJNIArray((Array)obj);
									}
									else
									{
										bool flag16 = obj is AndroidJavaProxy;
										if (flag16)
										{
											ret[i].l = ((AndroidJavaProxy)obj).GetRawProxy();
										}
										else
										{
											bool flag17 = obj is AndroidJavaRunnable;
											if (!flag17)
											{
												string text = "JNI; Unknown argument type '";
												Type type = obj.GetType();
												throw new Exception(text + ((type != null) ? type.ToString() : null) + "'");
											}
											ret[i].l = AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj);
										}
									}
								}
							}
						}
					}
				}
				i++;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003C90 File Offset: 0x00001E90
		public static object UnboxArray(AndroidJavaObject obj)
		{
			bool flag = obj == null;
			object obj2;
			if (flag)
			{
				obj2 = null;
			}
			else
			{
				AndroidJavaClass arrayUtil = new AndroidJavaClass("java/lang/reflect/Array");
				AndroidJavaObject objClass = obj.Call<AndroidJavaObject>("getClass", Array.Empty<object>());
				AndroidJavaObject compClass = objClass.Call<AndroidJavaObject>("getComponentType", Array.Empty<object>());
				string className = compClass.Call<string>("getName", Array.Empty<object>());
				int arrayLength = arrayUtil.CallStatic<int>("getLength", new object[] { obj });
				bool flag2 = compClass.Call<bool>("isPrimitive", Array.Empty<object>());
				Array array;
				if (flag2)
				{
					bool flag3 = "int" == className;
					if (flag3)
					{
						array = new int[arrayLength];
					}
					else
					{
						bool flag4 = "boolean" == className;
						if (flag4)
						{
							array = new bool[arrayLength];
						}
						else
						{
							bool flag5 = "byte" == className;
							if (flag5)
							{
								array = new sbyte[arrayLength];
							}
							else
							{
								bool flag6 = "short" == className;
								if (flag6)
								{
									array = new short[arrayLength];
								}
								else
								{
									bool flag7 = "long" == className;
									if (flag7)
									{
										array = new long[arrayLength];
									}
									else
									{
										bool flag8 = "float" == className;
										if (flag8)
										{
											array = new float[arrayLength];
										}
										else
										{
											bool flag9 = "double" == className;
											if (flag9)
											{
												array = new double[arrayLength];
											}
											else
											{
												bool flag10 = "char" == className;
												if (!flag10)
												{
													throw new Exception("JNI; Unknown argument type '" + className + "'");
												}
												array = new char[arrayLength];
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
					bool flag11 = "java.lang.String" == className;
					if (flag11)
					{
						array = new string[arrayLength];
					}
					else
					{
						bool flag12 = "java.lang.Class" == className;
						if (flag12)
						{
							array = new AndroidJavaClass[arrayLength];
						}
						else
						{
							array = new AndroidJavaObject[arrayLength];
						}
					}
				}
				for (int i = 0; i < arrayLength; i++)
				{
					array.SetValue(_AndroidJNIHelper.Unbox(arrayUtil.CallStatic<AndroidJavaObject>("get", new object[] { obj, i })), i);
				}
				arrayUtil.Dispose();
				obj2 = array;
			}
			return obj2;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003EBC File Offset: 0x000020BC
		public static object Unbox(AndroidJavaObject obj)
		{
			bool flag = obj == null;
			object obj2;
			if (flag)
			{
				obj2 = null;
			}
			else
			{
				using (AndroidJavaObject clazz = obj.Call<AndroidJavaObject>("getClass", Array.Empty<object>()))
				{
					string className = clazz.Call<string>("getName", Array.Empty<object>());
					bool flag2 = "java.lang.Integer" == className;
					if (flag2)
					{
						obj2 = obj.Call<int>("intValue", Array.Empty<object>());
					}
					else
					{
						bool flag3 = "java.lang.Boolean" == className;
						if (flag3)
						{
							obj2 = obj.Call<bool>("booleanValue", Array.Empty<object>());
						}
						else
						{
							bool flag4 = "java.lang.Byte" == className;
							if (flag4)
							{
								obj2 = obj.Call<sbyte>("byteValue", Array.Empty<object>());
							}
							else
							{
								bool flag5 = "java.lang.Short" == className;
								if (flag5)
								{
									obj2 = obj.Call<short>("shortValue", Array.Empty<object>());
								}
								else
								{
									bool flag6 = "java.lang.Long" == className;
									if (flag6)
									{
										obj2 = obj.Call<long>("longValue", Array.Empty<object>());
									}
									else
									{
										bool flag7 = "java.lang.Float" == className;
										if (flag7)
										{
											obj2 = obj.Call<float>("floatValue", Array.Empty<object>());
										}
										else
										{
											bool flag8 = "java.lang.Double" == className;
											if (flag8)
											{
												obj2 = obj.Call<double>("doubleValue", Array.Empty<object>());
											}
											else
											{
												bool flag9 = "java.lang.Character" == className;
												if (flag9)
												{
													obj2 = obj.Call<char>("charValue", Array.Empty<object>());
												}
												else
												{
													bool flag10 = "java.lang.String" == className;
													if (flag10)
													{
														obj2 = obj.Call<string>("toString", Array.Empty<object>());
													}
													else
													{
														bool flag11 = "java.lang.Class" == className;
														if (flag11)
														{
															obj2 = new AndroidJavaClass(obj.GetRawObject());
														}
														else
														{
															bool flag12 = clazz.Call<bool>("isArray", Array.Empty<object>());
															if (flag12)
															{
																obj2 = _AndroidJNIHelper.UnboxArray(obj);
															}
															else
															{
																obj2 = obj;
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
					}
				}
			}
			return obj2;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000040E8 File Offset: 0x000022E8
		public static AndroidJavaObject Box(object obj)
		{
			bool flag = obj == null;
			AndroidJavaObject androidJavaObject;
			if (flag)
			{
				androidJavaObject = null;
			}
			else
			{
				bool flag2 = AndroidReflection.IsPrimitive(obj.GetType());
				if (flag2)
				{
					bool flag3 = obj is int;
					if (flag3)
					{
						androidJavaObject = new AndroidJavaObject("java.lang.Integer", new object[] { (int)obj });
					}
					else
					{
						bool flag4 = obj is bool;
						if (flag4)
						{
							androidJavaObject = new AndroidJavaObject("java.lang.Boolean", new object[] { (bool)obj });
						}
						else
						{
							bool flag5 = obj is byte;
							if (flag5)
							{
								androidJavaObject = new AndroidJavaObject("java.lang.Byte", new object[] { (sbyte)obj });
							}
							else
							{
								bool flag6 = obj is sbyte;
								if (flag6)
								{
									androidJavaObject = new AndroidJavaObject("java.lang.Byte", new object[] { (sbyte)obj });
								}
								else
								{
									bool flag7 = obj is short;
									if (flag7)
									{
										androidJavaObject = new AndroidJavaObject("java.lang.Short", new object[] { (short)obj });
									}
									else
									{
										bool flag8 = obj is long;
										if (flag8)
										{
											androidJavaObject = new AndroidJavaObject("java.lang.Long", new object[] { (long)obj });
										}
										else
										{
											bool flag9 = obj is float;
											if (flag9)
											{
												androidJavaObject = new AndroidJavaObject("java.lang.Float", new object[] { (float)obj });
											}
											else
											{
												bool flag10 = obj is double;
												if (flag10)
												{
													androidJavaObject = new AndroidJavaObject("java.lang.Double", new object[] { (double)obj });
												}
												else
												{
													bool flag11 = obj is char;
													if (!flag11)
													{
														string text = "JNI; Unknown argument type '";
														Type type = obj.GetType();
														throw new Exception(text + ((type != null) ? type.ToString() : null) + "'");
													}
													androidJavaObject = new AndroidJavaObject("java.lang.Character", new object[] { (char)obj });
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
					bool flag12 = obj is string;
					if (flag12)
					{
						androidJavaObject = new AndroidJavaObject("java.lang.String", new object[] { (string)obj });
					}
					else
					{
						bool flag13 = obj is AndroidJavaClass;
						if (flag13)
						{
							androidJavaObject = new AndroidJavaObject(((AndroidJavaClass)obj).GetRawClass());
						}
						else
						{
							bool flag14 = obj is AndroidJavaObject;
							if (flag14)
							{
								androidJavaObject = (AndroidJavaObject)obj;
							}
							else
							{
								bool flag15 = obj is Array;
								if (flag15)
								{
									androidJavaObject = AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(_AndroidJNIHelper.ConvertToJNIArray((Array)obj));
								}
								else
								{
									bool flag16 = obj is AndroidJavaProxy;
									if (flag16)
									{
										androidJavaObject = ((AndroidJavaProxy)obj).GetProxyObject();
									}
									else
									{
										bool flag17 = obj is AndroidJavaRunnable;
										if (!flag17)
										{
											string text2 = "JNI; Unknown argument type '";
											Type type2 = obj.GetType();
											throw new Exception(text2 + ((type2 != null) ? type2.ToString() : null) + "'");
										}
										androidJavaObject = AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj));
									}
								}
							}
						}
					}
				}
			}
			return androidJavaObject;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004408 File Offset: 0x00002608
		public static void DeleteJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			bool flag = args == null;
			if (!flag)
			{
				int i = 0;
				foreach (object obj in args)
				{
					bool flag2 = obj is string || obj is AndroidJavaRunnable || obj is AndroidJavaProxy || obj is Array;
					if (flag2)
					{
						AndroidJNISafe.DeleteLocalRef(jniArgs[i].l);
					}
					i++;
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004480 File Offset: 0x00002680
		public static IntPtr ConvertToJNIArray(Array array)
		{
			Type type = array.GetType().GetElementType();
			bool flag = AndroidReflection.IsPrimitive(type);
			IntPtr intPtr;
			if (flag)
			{
				bool flag2 = type == typeof(int);
				if (flag2)
				{
					intPtr = AndroidJNISafe.ToIntArray((int[])array);
				}
				else
				{
					bool flag3 = type == typeof(bool);
					if (flag3)
					{
						intPtr = AndroidJNISafe.ToBooleanArray((bool[])array);
					}
					else
					{
						bool flag4 = type == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("AndroidJNIHelper: converting Byte array is obsolete, use SByte array instead");
							intPtr = AndroidJNISafe.ToByteArray((byte[])array);
						}
						else
						{
							bool flag5 = type == typeof(sbyte);
							if (flag5)
							{
								intPtr = AndroidJNISafe.ToSByteArray((sbyte[])array);
							}
							else
							{
								bool flag6 = type == typeof(short);
								if (flag6)
								{
									intPtr = AndroidJNISafe.ToShortArray((short[])array);
								}
								else
								{
									bool flag7 = type == typeof(long);
									if (flag7)
									{
										intPtr = AndroidJNISafe.ToLongArray((long[])array);
									}
									else
									{
										bool flag8 = type == typeof(float);
										if (flag8)
										{
											intPtr = AndroidJNISafe.ToFloatArray((float[])array);
										}
										else
										{
											bool flag9 = type == typeof(double);
											if (flag9)
											{
												intPtr = AndroidJNISafe.ToDoubleArray((double[])array);
											}
											else
											{
												bool flag10 = type == typeof(char);
												if (flag10)
												{
													intPtr = AndroidJNISafe.ToCharArray((char[])array);
												}
												else
												{
													intPtr = IntPtr.Zero;
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
				bool flag11 = type == typeof(string);
				if (flag11)
				{
					IntPtr res = IntPtr.Zero;
					bool framePushed = false;
					try
					{
						string[] strArray = (string[])array;
						int arrayLen = array.GetLength(0);
						int frameSize = arrayLen;
						bool flag12 = frameSize > _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS;
						if (flag12)
						{
							frameSize = _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS;
						}
						IntPtr arrayType = AndroidJNISafe.FindClass("java/lang/String");
						IntPtr result = AndroidJNI.NewObjectArray(arrayLen, arrayType, IntPtr.Zero);
						AndroidJNISafe.DeleteLocalRef(arrayType);
						bool flag13 = frameSize > 0;
						if (flag13)
						{
							AndroidJNISafe.PushLocalFrame(frameSize);
							framePushed = true;
						}
						for (int i = 0; i < arrayLen; i++)
						{
							bool flag14 = i % _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS == 0;
							if (flag14)
							{
								AndroidJNI.PopLocalFrame(IntPtr.Zero);
								framePushed = false;
								AndroidJNISafe.PushLocalFrame(frameSize);
								framePushed = true;
							}
							IntPtr jstring = AndroidJNISafe.NewString(strArray[i]);
							AndroidJNI.SetObjectArrayElement(result, i, jstring);
						}
						res = result;
					}
					finally
					{
						bool flag15 = framePushed;
						if (flag15)
						{
							AndroidJNI.PopLocalFrame(IntPtr.Zero);
						}
					}
					intPtr = res;
				}
				else
				{
					bool flag16 = type == typeof(AndroidJavaObject);
					if (flag16)
					{
						AndroidJavaObject[] objArray = (AndroidJavaObject[])array;
						int arrayLen2 = array.GetLength(0);
						IntPtr[] jniObjs = new IntPtr[arrayLen2];
						IntPtr fallBackType = AndroidJNISafe.FindClass("java/lang/Object");
						IntPtr arrayType2 = IntPtr.Zero;
						for (int j = 0; j < arrayLen2; j++)
						{
							bool flag17 = objArray[j] != null;
							if (flag17)
							{
								jniObjs[j] = objArray[j].GetRawObject();
								IntPtr objectType = objArray[j].GetRawClass();
								bool flag18 = arrayType2 == IntPtr.Zero;
								if (flag18)
								{
									arrayType2 = objectType;
								}
								else
								{
									bool flag19 = arrayType2 != fallBackType && !AndroidJNI.IsSameObject(arrayType2, objectType);
									if (flag19)
									{
										arrayType2 = fallBackType;
									}
								}
							}
							else
							{
								jniObjs[j] = IntPtr.Zero;
							}
						}
						IntPtr res2 = AndroidJNISafe.ToObjectArray(jniObjs, arrayType2);
						AndroidJNISafe.DeleteLocalRef(fallBackType);
						intPtr = res2;
					}
					else
					{
						bool flag20 = AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), type);
						if (!flag20)
						{
							string text = "JNI; Unknown array type '";
							Type type2 = type;
							throw new Exception(text + ((type2 != null) ? type2.ToString() : null) + "'");
						}
						AndroidJavaProxy[] objArray2 = (AndroidJavaProxy[])array;
						int arrayLen3 = array.GetLength(0);
						IntPtr[] jniObjs2 = new IntPtr[arrayLen3];
						IntPtr fallBackType2 = AndroidJNISafe.FindClass("java/lang/Object");
						IntPtr arrayType3 = IntPtr.Zero;
						for (int k = 0; k < arrayLen3; k++)
						{
							bool flag21 = objArray2[k] != null;
							if (flag21)
							{
								jniObjs2[k] = objArray2[k].GetRawProxy();
								IntPtr objectType2 = objArray2[k].javaInterface.GetRawClass();
								bool flag22 = arrayType3 == IntPtr.Zero;
								if (flag22)
								{
									arrayType3 = objectType2;
								}
								else
								{
									bool flag23 = arrayType3 != fallBackType2 && !AndroidJNI.IsSameObject(arrayType3, objectType2);
									if (flag23)
									{
										arrayType3 = fallBackType2;
									}
								}
							}
							else
							{
								jniObjs2[k] = IntPtr.Zero;
							}
						}
						IntPtr res3 = AndroidJNISafe.ToObjectArray(jniObjs2, arrayType3);
						AndroidJNISafe.DeleteLocalRef(fallBackType2);
						intPtr = res3;
					}
				}
			}
			return intPtr;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004958 File Offset: 0x00002B58
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			Type type = typeof(ArrayType).GetElementType();
			bool flag = AndroidReflection.IsPrimitive(type);
			ArrayType arrayType;
			if (flag)
			{
				bool flag2 = type == typeof(int);
				if (flag2)
				{
					arrayType = (ArrayType)((object)AndroidJNISafe.FromIntArray(array));
				}
				else
				{
					bool flag3 = type == typeof(bool);
					if (flag3)
					{
						arrayType = (ArrayType)((object)AndroidJNISafe.FromBooleanArray(array));
					}
					else
					{
						bool flag4 = type == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("AndroidJNIHelper: converting from Byte array is obsolete, use SByte array instead");
							arrayType = (ArrayType)((object)AndroidJNISafe.FromByteArray(array));
						}
						else
						{
							bool flag5 = type == typeof(sbyte);
							if (flag5)
							{
								arrayType = (ArrayType)((object)AndroidJNISafe.FromSByteArray(array));
							}
							else
							{
								bool flag6 = type == typeof(short);
								if (flag6)
								{
									arrayType = (ArrayType)((object)AndroidJNISafe.FromShortArray(array));
								}
								else
								{
									bool flag7 = type == typeof(long);
									if (flag7)
									{
										arrayType = (ArrayType)((object)AndroidJNISafe.FromLongArray(array));
									}
									else
									{
										bool flag8 = type == typeof(float);
										if (flag8)
										{
											arrayType = (ArrayType)((object)AndroidJNISafe.FromFloatArray(array));
										}
										else
										{
											bool flag9 = type == typeof(double);
											if (flag9)
											{
												arrayType = (ArrayType)((object)AndroidJNISafe.FromDoubleArray(array));
											}
											else
											{
												bool flag10 = type == typeof(char);
												if (flag10)
												{
													arrayType = (ArrayType)((object)AndroidJNISafe.FromCharArray(array));
												}
												else
												{
													arrayType = default(ArrayType);
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
				bool flag11 = type == typeof(string);
				if (flag11)
				{
					int arrayLen = AndroidJNISafe.GetArrayLength(array);
					string[] strArray = new string[arrayLen];
					bool flag12 = arrayLen == 0;
					if (flag12)
					{
						arrayType = (ArrayType)((object)strArray);
					}
					else
					{
						int frameSize = ((arrayLen > _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS) ? _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS : arrayLen);
						AndroidJNISafe.PushLocalFrame(frameSize);
						bool framePushed = true;
						try
						{
							for (int i = 0; i < arrayLen; i++)
							{
								bool flag13 = i % _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS == 0;
								if (flag13)
								{
									AndroidJNI.PopLocalFrame(IntPtr.Zero);
									framePushed = false;
									AndroidJNISafe.PushLocalFrame(frameSize);
									framePushed = true;
								}
								IntPtr jstring = AndroidJNI.GetObjectArrayElement(array, i);
								strArray[i] = AndroidJNISafe.GetStringChars(jstring);
							}
						}
						finally
						{
							bool flag14 = framePushed;
							if (flag14)
							{
								AndroidJNI.PopLocalFrame(IntPtr.Zero);
							}
						}
						arrayType = (ArrayType)((object)strArray);
					}
				}
				else
				{
					bool flag15 = type == typeof(AndroidJavaObject);
					if (!flag15)
					{
						string text = "JNI: Unknown generic array type '";
						Type type2 = type;
						throw new Exception(text + ((type2 != null) ? type2.ToString() : null) + "'");
					}
					int arrayLen2 = AndroidJNISafe.GetArrayLength(array);
					AndroidJavaObject[] objArray = new AndroidJavaObject[arrayLen2];
					bool flag16 = arrayLen2 == 0;
					if (flag16)
					{
						arrayType = (ArrayType)((object)objArray);
					}
					else
					{
						int frameSize2 = ((arrayLen2 > _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS) ? _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS : arrayLen2);
						AndroidJNISafe.PushLocalFrame(frameSize2);
						bool framePushed2 = true;
						try
						{
							for (int j = 0; j < arrayLen2; j++)
							{
								bool flag17 = j % _AndroidJNIHelper.FRAME_SIZE_FOR_ARRAYS == 0;
								if (flag17)
								{
									AndroidJNI.PopLocalFrame(IntPtr.Zero);
									framePushed2 = false;
									AndroidJNISafe.PushLocalFrame(frameSize2);
									framePushed2 = true;
								}
								IntPtr jobject = AndroidJNI.GetObjectArrayElement(array, j);
								objArray[j] = new AndroidJavaObject(jobject);
							}
						}
						finally
						{
							bool flag18 = framePushed2;
							if (flag18)
							{
								AndroidJNI.PopLocalFrame(IntPtr.Zero);
							}
						}
						arrayType = (ArrayType)((object)objArray);
					}
				}
			}
			return arrayType;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004D08 File Offset: 0x00002F08
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return AndroidJNIHelper.GetConstructorID(jclass, _AndroidJNIHelper.GetSignature(args));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004D28 File Offset: 0x00002F28
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, _AndroidJNIHelper.GetSignature<ReturnType>(args), isStatic);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004D48 File Offset: 0x00002F48
		public static IntPtr GetConstructorID(IntPtr jclass, string signature)
		{
			IntPtr constructor = IntPtr.Zero;
			IntPtr intPtr;
			try
			{
				constructor = AndroidReflection.GetConstructorMember(jclass, signature);
				intPtr = AndroidJNISafe.FromReflectedMethod(constructor);
			}
			catch (Exception e)
			{
				IntPtr memberID = AndroidJNISafe.GetMethodID(jclass, "<init>", signature);
				bool flag = memberID != IntPtr.Zero;
				if (!flag)
				{
					throw e;
				}
				intPtr = memberID;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(constructor);
			}
			return intPtr;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004DBC File Offset: 0x00002FBC
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			IntPtr method = IntPtr.Zero;
			IntPtr intPtr;
			try
			{
				method = AndroidReflection.GetMethodMember(jclass, methodName, signature, isStatic);
				intPtr = AndroidJNISafe.FromReflectedMethod(method);
			}
			catch (Exception e)
			{
				IntPtr memberID = _AndroidJNIHelper.GetMethodIDFallback(jclass, methodName, signature, isStatic);
				bool flag = memberID != IntPtr.Zero;
				if (!flag)
				{
					throw e;
				}
				intPtr = memberID;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(method);
			}
			return intPtr;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004E30 File Offset: 0x00003030
		private static IntPtr GetMethodIDFallback(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			try
			{
				return isStatic ? AndroidJNISafe.GetStaticMethodID(jclass, methodName, signature) : AndroidJNISafe.GetMethodID(jclass, methodName, signature);
			}
			catch (Exception)
			{
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004E78 File Offset: 0x00003078
		public static string GetSignature(object obj)
		{
			bool flag = obj == null;
			string text;
			if (flag)
			{
				text = "Ljava/lang/Object;";
			}
			else
			{
				Type type = ((obj is Type) ? ((Type)obj) : obj.GetType());
				bool flag2 = AndroidReflection.IsPrimitive(type);
				if (flag2)
				{
					bool flag3 = type.Equals(typeof(int));
					if (flag3)
					{
						text = "I";
					}
					else
					{
						bool flag4 = type.Equals(typeof(bool));
						if (flag4)
						{
							text = "Z";
						}
						else
						{
							bool flag5 = type.Equals(typeof(byte));
							if (flag5)
							{
								Debug.LogWarning("AndroidJNIHelper.GetSignature: using Byte parameters is obsolete, use SByte parameters instead");
								text = "B";
							}
							else
							{
								bool flag6 = type.Equals(typeof(sbyte));
								if (flag6)
								{
									text = "B";
								}
								else
								{
									bool flag7 = type.Equals(typeof(short));
									if (flag7)
									{
										text = "S";
									}
									else
									{
										bool flag8 = type.Equals(typeof(long));
										if (flag8)
										{
											text = "J";
										}
										else
										{
											bool flag9 = type.Equals(typeof(float));
											if (flag9)
											{
												text = "F";
											}
											else
											{
												bool flag10 = type.Equals(typeof(double));
												if (flag10)
												{
													text = "D";
												}
												else
												{
													bool flag11 = type.Equals(typeof(char));
													if (flag11)
													{
														text = "C";
													}
													else
													{
														text = "";
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
					bool flag12 = type.Equals(typeof(string));
					if (flag12)
					{
						text = "Ljava/lang/String;";
					}
					else
					{
						bool flag13 = obj is AndroidJavaProxy;
						if (flag13)
						{
							using (AndroidJavaObject javaClass = new AndroidJavaObject(((AndroidJavaProxy)obj).javaInterface.GetRawClass()))
							{
								return "L" + javaClass.Call<string>("getName", Array.Empty<object>()) + ";";
							}
						}
						bool flag14 = obj == type && AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), type);
						if (flag14)
						{
							text = "";
						}
						else
						{
							bool flag15 = type.Equals(typeof(AndroidJavaRunnable));
							if (flag15)
							{
								text = "Ljava/lang/Runnable;";
							}
							else
							{
								bool flag16 = obj is AndroidJavaClass || (obj == type && AndroidReflection.IsAssignableFrom(typeof(AndroidJavaClass), type));
								if (flag16)
								{
									text = "Ljava/lang/Class;";
								}
								else
								{
									bool flag17 = obj is AndroidJavaObject;
									if (flag17)
									{
										AndroidJavaObject javaObject = (AndroidJavaObject)obj;
										using (AndroidJavaObject javaClass2 = javaObject.Call<AndroidJavaObject>("getClass", Array.Empty<object>()))
										{
											return "L" + javaClass2.Call<string>("getName", Array.Empty<object>()) + ";";
										}
									}
									bool flag18 = obj == type && AndroidReflection.IsAssignableFrom(typeof(AndroidJavaObject), type);
									if (flag18)
									{
										text = "Ljava/lang/Object;";
									}
									else
									{
										bool flag19 = AndroidReflection.IsAssignableFrom(typeof(Array), type);
										if (!flag19)
										{
											string[] array = new string[6];
											array[0] = "JNI: Unknown signature for type '";
											int num = 1;
											Type type2 = type;
											array[num] = ((type2 != null) ? type2.ToString() : null);
											array[2] = "' (obj = ";
											array[3] = ((obj != null) ? obj.ToString() : null);
											array[4] = ") ";
											array[5] = ((type == obj) ? "equal" : "instance");
											throw new Exception(string.Concat(array));
										}
										bool flag20 = type.GetArrayRank() != 1;
										if (flag20)
										{
											throw new Exception("JNI: System.Array in n dimensions is not allowed");
										}
										StringBuilder sb = new StringBuilder();
										sb.Append('[');
										sb.Append(_AndroidJNIHelper.GetSignature(type.GetElementType()));
										text = ((sb.Length > 1) ? sb.ToString() : "");
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005274 File Offset: 0x00003474
		public static string GetSignature(object[] args)
		{
			bool flag = args == null || args.Length == 0;
			string text;
			if (flag)
			{
				text = "()V";
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append('(');
				foreach (object obj in args)
				{
					sb.Append(_AndroidJNIHelper.GetSignature(obj));
				}
				sb.Append(")V");
				text = sb.ToString();
			}
			return text;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000052EC File Offset: 0x000034EC
		public static string GetSignature<ReturnType>(object[] args)
		{
			bool flag = args == null || args.Length == 0;
			string text;
			if (flag)
			{
				text = "()" + _AndroidJNIHelper.GetSignature(typeof(ReturnType));
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append('(');
				foreach (object obj in args)
				{
					sb.Append(_AndroidJNIHelper.GetSignature(obj));
				}
				sb.Append(')');
				sb.Append(_AndroidJNIHelper.GetSignature(typeof(ReturnType)));
				text = sb.ToString();
			}
			return text;
		}

		// Token: 0x04000014 RID: 20
		private static int FRAME_SIZE_FOR_ARRAYS = 100;
	}
}
