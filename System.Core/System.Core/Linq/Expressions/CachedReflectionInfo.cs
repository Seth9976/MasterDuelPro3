using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000058 RID: 88
	internal static class CachedReflectionInfo
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B925 File Offset: 0x00009B25
		public static MethodInfo CallSiteOps_SetNotMatched
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_SetNotMatched) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_SetNotMatched = typeof(CallSiteOps).GetMethod("SetNotMatched"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B94A File Offset: 0x00009B4A
		public static MethodInfo CallSiteOps_CreateMatchmaker
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_CreateMatchmaker) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_CreateMatchmaker = typeof(CallSiteOps).GetMethod("CreateMatchmaker"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B96F File Offset: 0x00009B6F
		public static MethodInfo CallSiteOps_GetMatch
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetMatch) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetMatch = typeof(CallSiteOps).GetMethod("GetMatch"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000B994 File Offset: 0x00009B94
		public static MethodInfo CallSiteOps_ClearMatch
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_ClearMatch) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_ClearMatch = typeof(CallSiteOps).GetMethod("ClearMatch"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		public static MethodInfo CallSiteOps_UpdateRules
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_UpdateRules) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_UpdateRules = typeof(CallSiteOps).GetMethod("UpdateRules"));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B9DE File Offset: 0x00009BDE
		public static MethodInfo CallSiteOps_GetRules
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetRules) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetRules = typeof(CallSiteOps).GetMethod("GetRules"));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000BA03 File Offset: 0x00009C03
		public static MethodInfo CallSiteOps_GetRuleCache
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetRuleCache) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetRuleCache = typeof(CallSiteOps).GetMethod("GetRuleCache"));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000BA28 File Offset: 0x00009C28
		public static MethodInfo CallSiteOps_GetCachedRules
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetCachedRules) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetCachedRules = typeof(CallSiteOps).GetMethod("GetCachedRules"));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000BA4D File Offset: 0x00009C4D
		public static MethodInfo CallSiteOps_AddRule
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_AddRule) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_AddRule = typeof(CallSiteOps).GetMethod("AddRule"));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000BA72 File Offset: 0x00009C72
		public static MethodInfo CallSiteOps_MoveRule
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_MoveRule) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_MoveRule = typeof(CallSiteOps).GetMethod("MoveRule"));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000BA97 File Offset: 0x00009C97
		public static MethodInfo CallSiteOps_Bind
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_Bind) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_Bind = typeof(CallSiteOps).GetMethod("Bind"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000BABC File Offset: 0x00009CBC
		public static ConstructorInfo Nullable_Boolean_Ctor
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Nullable_Boolean_Ctor) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Nullable_Boolean_Ctor = typeof(bool?).GetConstructor(new Type[] { typeof(bool) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000BAEF File Offset: 0x00009CEF
		public static ConstructorInfo Decimal_Ctor_Int32
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_Int32) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_Int32 = typeof(decimal).GetConstructor(new Type[] { typeof(int) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000BB22 File Offset: 0x00009D22
		public static ConstructorInfo Decimal_Ctor_UInt32
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_UInt32) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_UInt32 = typeof(decimal).GetConstructor(new Type[] { typeof(uint) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000BB55 File Offset: 0x00009D55
		public static ConstructorInfo Decimal_Ctor_Int64
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_Int64) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_Int64 = typeof(decimal).GetConstructor(new Type[] { typeof(long) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000BB88 File Offset: 0x00009D88
		public static ConstructorInfo Decimal_Ctor_UInt64
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_UInt64) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_UInt64 = typeof(decimal).GetConstructor(new Type[] { typeof(ulong) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public static ConstructorInfo Decimal_Ctor_Int32_Int32_Int32_Bool_Byte
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(bool),
						typeof(byte)
					}));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000BC2E File Offset: 0x00009E2E
		public static FieldInfo Decimal_One
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_One) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_One = typeof(decimal).GetField("One"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000BC53 File Offset: 0x00009E53
		public static FieldInfo Decimal_MinusOne
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_MinusOne) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_MinusOne = typeof(decimal).GetField("MinusOne"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BC78 File Offset: 0x00009E78
		public static FieldInfo Decimal_MinValue
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_MinValue) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_MinValue = typeof(decimal).GetField("MinValue"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000BC9D File Offset: 0x00009E9D
		public static FieldInfo Decimal_MaxValue
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_MaxValue) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_MaxValue = typeof(decimal).GetField("MaxValue"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BCC2 File Offset: 0x00009EC2
		public static FieldInfo Decimal_Zero
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_Zero) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_Zero = typeof(decimal).GetField("Zero"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000BCE7 File Offset: 0x00009EE7
		public static FieldInfo DateTime_MinValue
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_DateTime_MinValue) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_DateTime_MinValue = typeof(DateTime).GetField("MinValue"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public static MethodInfo MethodBase_GetMethodFromHandle_RuntimeMethodHandle
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle) == null)
				{
					methodInfo = (CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000BD44 File Offset: 0x00009F44
		public static MethodInfo MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle) == null)
				{
					methodInfo = (CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
					{
						typeof(RuntimeMethodHandle),
						typeof(RuntimeTypeHandle)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000BD94 File Offset: 0x00009F94
		public static MethodInfo MethodInfo_CreateDelegate_Type_Object
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_MethodInfo_CreateDelegate_Type_Object) == null)
				{
					methodInfo = (CachedReflectionInfo.s_MethodInfo_CreateDelegate_Type_Object = typeof(MethodInfo).GetMethod("CreateDelegate", new Type[]
					{
						typeof(Type),
						typeof(object)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public static MethodInfo String_op_Equality_String_String
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_String_op_Equality_String_String) == null)
				{
					methodInfo = (CachedReflectionInfo.s_String_op_Equality_String_String = typeof(string).GetMethod("op_Equality", new Type[]
					{
						typeof(string),
						typeof(string)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000BE34 File Offset: 0x0000A034
		public static MethodInfo String_Equals_String_String
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_String_Equals_String_String) == null)
				{
					methodInfo = (CachedReflectionInfo.s_String_Equals_String_String = typeof(string).GetMethod("Equals", new Type[]
					{
						typeof(string),
						typeof(string)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000BE84 File Offset: 0x0000A084
		public static MethodInfo DictionaryOfStringInt32_Add_String_Int32
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DictionaryOfStringInt32_Add_String_Int32) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DictionaryOfStringInt32_Add_String_Int32 = typeof(Dictionary<string, int>).GetMethod("Add", new Type[]
					{
						typeof(string),
						typeof(int)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public static ConstructorInfo DictionaryOfStringInt32_Ctor_Int32
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_DictionaryOfStringInt32_Ctor_Int32) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_DictionaryOfStringInt32_Ctor_Int32 = typeof(Dictionary<string, int>).GetConstructor(new Type[] { typeof(int) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000BF07 File Offset: 0x0000A107
		public static MethodInfo Type_GetTypeFromHandle
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Type_GetTypeFromHandle) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Type_GetTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000BF2C File Offset: 0x0000A12C
		public static MethodInfo Object_GetType
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Object_GetType) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Object_GetType = typeof(object).GetMethod("GetType"));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000BF51 File Offset: 0x0000A151
		public static MethodInfo Decimal_op_Implicit_Byte
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Byte) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Byte = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(byte) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000BF89 File Offset: 0x0000A189
		public static MethodInfo Decimal_op_Implicit_SByte
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_SByte) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_SByte = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(sbyte) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BFC1 File Offset: 0x0000A1C1
		public static MethodInfo Decimal_op_Implicit_Int16
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Int16) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Int16 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(short) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000BFF9 File Offset: 0x0000A1F9
		public static MethodInfo Decimal_op_Implicit_UInt16
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_UInt16) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt16 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(ushort) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000C031 File Offset: 0x0000A231
		public static MethodInfo Decimal_op_Implicit_Int32
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Int32) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Int32 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(int) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000C069 File Offset: 0x0000A269
		public static MethodInfo Decimal_op_Implicit_UInt32
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_UInt32) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt32 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(uint) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public static MethodInfo Decimal_op_Implicit_Int64
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Int64) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Int64 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(long) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000C0D9 File Offset: 0x0000A2D9
		public static MethodInfo Decimal_op_Implicit_UInt64
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_UInt64) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt64 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(ulong) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000C111 File Offset: 0x0000A311
		public static MethodInfo Decimal_op_Implicit_Char
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Char) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Char = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(char) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000C14C File Offset: 0x0000A34C
		public static MethodInfo Math_Pow_Double_Double
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Math_Pow_Double_Double) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Math_Pow_Double_Double = typeof(Math).GetMethod("Pow", new Type[]
					{
						typeof(double),
						typeof(double)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000C19C File Offset: 0x0000A39C
		public static ConstructorInfo Closure_ObjectArray_ObjectArray
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Closure_ObjectArray_ObjectArray) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Closure_ObjectArray_ObjectArray = typeof(Closure).GetConstructor(new Type[]
					{
						typeof(object[]),
						typeof(object[])
					}));
				}
				return constructorInfo;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public static FieldInfo Closure_Constants
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Closure_Constants) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Closure_Constants = typeof(Closure).GetField("Constants"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000C201 File Offset: 0x0000A401
		public static FieldInfo Closure_Locals
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Closure_Locals) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Closure_Locals = typeof(Closure).GetField("Locals"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000C228 File Offset: 0x0000A428
		public static MethodInfo RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array = typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", new Type[]
					{
						typeof(object[]),
						typeof(long[])
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000C278 File Offset: 0x0000A478
		public static MethodInfo RuntimeOps_CreateRuntimeVariables
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables = typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", Type.EmptyTypes));
				}
				return methodInfo;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000C2A2 File Offset: 0x0000A4A2
		public static MethodInfo RuntimeOps_Quote
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_Quote) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_Quote = typeof(RuntimeOps).GetMethod("Quote"));
				}
				return methodInfo;
			}
		}

		// Token: 0x040000E0 RID: 224
		private static MethodInfo s_CallSiteOps_SetNotMatched;

		// Token: 0x040000E1 RID: 225
		private static MethodInfo s_CallSiteOps_CreateMatchmaker;

		// Token: 0x040000E2 RID: 226
		private static MethodInfo s_CallSiteOps_GetMatch;

		// Token: 0x040000E3 RID: 227
		private static MethodInfo s_CallSiteOps_ClearMatch;

		// Token: 0x040000E4 RID: 228
		private static MethodInfo s_CallSiteOps_UpdateRules;

		// Token: 0x040000E5 RID: 229
		private static MethodInfo s_CallSiteOps_GetRules;

		// Token: 0x040000E6 RID: 230
		private static MethodInfo s_CallSiteOps_GetRuleCache;

		// Token: 0x040000E7 RID: 231
		private static MethodInfo s_CallSiteOps_GetCachedRules;

		// Token: 0x040000E8 RID: 232
		private static MethodInfo s_CallSiteOps_AddRule;

		// Token: 0x040000E9 RID: 233
		private static MethodInfo s_CallSiteOps_MoveRule;

		// Token: 0x040000EA RID: 234
		private static MethodInfo s_CallSiteOps_Bind;

		// Token: 0x040000EB RID: 235
		private static ConstructorInfo s_Nullable_Boolean_Ctor;

		// Token: 0x040000EC RID: 236
		private static ConstructorInfo s_Decimal_Ctor_Int32;

		// Token: 0x040000ED RID: 237
		private static ConstructorInfo s_Decimal_Ctor_UInt32;

		// Token: 0x040000EE RID: 238
		private static ConstructorInfo s_Decimal_Ctor_Int64;

		// Token: 0x040000EF RID: 239
		private static ConstructorInfo s_Decimal_Ctor_UInt64;

		// Token: 0x040000F0 RID: 240
		private static ConstructorInfo s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte;

		// Token: 0x040000F1 RID: 241
		private static FieldInfo s_Decimal_One;

		// Token: 0x040000F2 RID: 242
		private static FieldInfo s_Decimal_MinusOne;

		// Token: 0x040000F3 RID: 243
		private static FieldInfo s_Decimal_MinValue;

		// Token: 0x040000F4 RID: 244
		private static FieldInfo s_Decimal_MaxValue;

		// Token: 0x040000F5 RID: 245
		private static FieldInfo s_Decimal_Zero;

		// Token: 0x040000F6 RID: 246
		private static FieldInfo s_DateTime_MinValue;

		// Token: 0x040000F7 RID: 247
		private static MethodInfo s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle;

		// Token: 0x040000F8 RID: 248
		private static MethodInfo s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle;

		// Token: 0x040000F9 RID: 249
		private static MethodInfo s_MethodInfo_CreateDelegate_Type_Object;

		// Token: 0x040000FA RID: 250
		private static MethodInfo s_String_op_Equality_String_String;

		// Token: 0x040000FB RID: 251
		private static MethodInfo s_String_Equals_String_String;

		// Token: 0x040000FC RID: 252
		private static MethodInfo s_DictionaryOfStringInt32_Add_String_Int32;

		// Token: 0x040000FD RID: 253
		private static ConstructorInfo s_DictionaryOfStringInt32_Ctor_Int32;

		// Token: 0x040000FE RID: 254
		private static MethodInfo s_Type_GetTypeFromHandle;

		// Token: 0x040000FF RID: 255
		private static MethodInfo s_Object_GetType;

		// Token: 0x04000100 RID: 256
		private static MethodInfo s_Decimal_op_Implicit_Byte;

		// Token: 0x04000101 RID: 257
		private static MethodInfo s_Decimal_op_Implicit_SByte;

		// Token: 0x04000102 RID: 258
		private static MethodInfo s_Decimal_op_Implicit_Int16;

		// Token: 0x04000103 RID: 259
		private static MethodInfo s_Decimal_op_Implicit_UInt16;

		// Token: 0x04000104 RID: 260
		private static MethodInfo s_Decimal_op_Implicit_Int32;

		// Token: 0x04000105 RID: 261
		private static MethodInfo s_Decimal_op_Implicit_UInt32;

		// Token: 0x04000106 RID: 262
		private static MethodInfo s_Decimal_op_Implicit_Int64;

		// Token: 0x04000107 RID: 263
		private static MethodInfo s_Decimal_op_Implicit_UInt64;

		// Token: 0x04000108 RID: 264
		private static MethodInfo s_Decimal_op_Implicit_Char;

		// Token: 0x04000109 RID: 265
		private static MethodInfo s_Math_Pow_Double_Double;

		// Token: 0x0400010A RID: 266
		private static ConstructorInfo s_Closure_ObjectArray_ObjectArray;

		// Token: 0x0400010B RID: 267
		private static FieldInfo s_Closure_Constants;

		// Token: 0x0400010C RID: 268
		private static FieldInfo s_Closure_Locals;

		// Token: 0x0400010D RID: 269
		private static MethodInfo s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array;

		// Token: 0x0400010E RID: 270
		private static MethodInfo s_RuntimeOps_CreateRuntimeVariables;

		// Token: 0x0400010F RID: 271
		private static MethodInfo s_RuntimeOps_Quote;
	}
}
