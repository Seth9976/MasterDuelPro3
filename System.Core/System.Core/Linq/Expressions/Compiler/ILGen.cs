using System;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000EF RID: 239
	internal static class ILGen
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x0001989C File Offset: 0x00017A9C
		internal static void Emit(this ILGenerator il, OpCode opcode, MethodBase methodBase)
		{
			ConstructorInfo constructorInfo = methodBase as ConstructorInfo;
			if (constructorInfo != null)
			{
				il.Emit(opcode, constructorInfo);
				return;
			}
			il.Emit(opcode, (MethodInfo)methodBase);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000198CC File Offset: 0x00017ACC
		internal static void EmitLoadArg(this ILGenerator il, int index)
		{
			switch (index)
			{
			case 0:
				il.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				il.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				il.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				il.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (index <= 255)
				{
					il.Emit(OpCodes.Ldarg_S, (byte)index);
					return;
				}
				il.Emit(OpCodes.Ldarg, (short)index);
				return;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00019944 File Offset: 0x00017B44
		internal static void EmitLoadArgAddress(this ILGenerator il, int index)
		{
			if (index <= 255)
			{
				il.Emit(OpCodes.Ldarga_S, (byte)index);
				return;
			}
			il.Emit(OpCodes.Ldarga, (short)index);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019969 File Offset: 0x00017B69
		internal static void EmitStoreArg(this ILGenerator il, int index)
		{
			if (index <= 255)
			{
				il.Emit(OpCodes.Starg_S, (byte)index);
				return;
			}
			il.Emit(OpCodes.Starg, (short)index);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019990 File Offset: 0x00017B90
		internal static void EmitLoadValueIndirect(this ILGenerator il, Type type)
		{
			switch (type.GetTypeCode())
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
				il.Emit(OpCodes.Ldind_U1);
				return;
			case TypeCode.Char:
			case TypeCode.UInt16:
				il.Emit(OpCodes.Ldind_U2);
				return;
			case TypeCode.Byte:
				il.Emit(OpCodes.Ldind_I1);
				return;
			case TypeCode.Int16:
				il.Emit(OpCodes.Ldind_I2);
				return;
			case TypeCode.Int32:
				il.Emit(OpCodes.Ldind_I4);
				return;
			case TypeCode.UInt32:
				il.Emit(OpCodes.Ldind_U4);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Ldind_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Ldind_R4);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Ldind_R8);
				return;
			default:
				if (type.IsValueType)
				{
					il.Emit(OpCodes.Ldobj, type);
					return;
				}
				il.Emit(OpCodes.Ldind_Ref);
				return;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00019A6C File Offset: 0x00017C6C
		internal static void EmitStoreValueIndirect(this ILGenerator il, Type type)
		{
			switch (type.GetTypeCode())
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
			case TypeCode.Byte:
				il.Emit(OpCodes.Stind_I1);
				return;
			case TypeCode.Char:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				il.Emit(OpCodes.Stind_I2);
				return;
			case TypeCode.Int32:
			case TypeCode.UInt32:
				il.Emit(OpCodes.Stind_I4);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Stind_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Stind_R4);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Stind_R8);
				return;
			default:
				if (type.IsValueType)
				{
					il.Emit(OpCodes.Stobj, type);
					return;
				}
				il.Emit(OpCodes.Stind_Ref);
				return;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00019B24 File Offset: 0x00017D24
		internal static void EmitLoadElement(this ILGenerator il, Type type)
		{
			if (!type.IsValueType)
			{
				il.Emit(OpCodes.Ldelem_Ref);
				return;
			}
			switch (type.GetTypeCode())
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
				il.Emit(OpCodes.Ldelem_I1);
				return;
			case TypeCode.Char:
			case TypeCode.UInt16:
				il.Emit(OpCodes.Ldelem_U2);
				return;
			case TypeCode.Byte:
				il.Emit(OpCodes.Ldelem_U1);
				return;
			case TypeCode.Int16:
				il.Emit(OpCodes.Ldelem_I2);
				return;
			case TypeCode.Int32:
				il.Emit(OpCodes.Ldelem_I4);
				return;
			case TypeCode.UInt32:
				il.Emit(OpCodes.Ldelem_U4);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Ldelem_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Ldelem_R4);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Ldelem_R8);
				return;
			default:
				il.Emit(OpCodes.Ldelem, type);
				return;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00019C00 File Offset: 0x00017E00
		internal static void EmitStoreElement(this ILGenerator il, Type type)
		{
			switch (type.GetTypeCode())
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
			case TypeCode.Byte:
				il.Emit(OpCodes.Stelem_I1);
				return;
			case TypeCode.Char:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				il.Emit(OpCodes.Stelem_I2);
				return;
			case TypeCode.Int32:
			case TypeCode.UInt32:
				il.Emit(OpCodes.Stelem_I4);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Stelem_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Stelem_R4);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Stelem_R8);
				return;
			default:
				if (type.IsValueType)
				{
					il.Emit(OpCodes.Stelem, type);
					return;
				}
				il.Emit(OpCodes.Stelem_Ref);
				return;
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00019CB6 File Offset: 0x00017EB6
		internal static void EmitType(this ILGenerator il, Type type)
		{
			il.Emit(OpCodes.Ldtoken, type);
			il.Emit(OpCodes.Call, CachedReflectionInfo.Type_GetTypeFromHandle);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00019CD4 File Offset: 0x00017ED4
		internal static void EmitFieldAddress(this ILGenerator il, FieldInfo fi)
		{
			il.Emit(fi.IsStatic ? OpCodes.Ldsflda : OpCodes.Ldflda, fi);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00019CF1 File Offset: 0x00017EF1
		internal static void EmitFieldGet(this ILGenerator il, FieldInfo fi)
		{
			il.Emit(fi.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, fi);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00019D0E File Offset: 0x00017F0E
		internal static void EmitFieldSet(this ILGenerator il, FieldInfo fi)
		{
			il.Emit(fi.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld, fi);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00019D2B File Offset: 0x00017F2B
		internal static void EmitNew(this ILGenerator il, ConstructorInfo ci)
		{
			il.Emit(OpCodes.Newobj, ci);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00019D39 File Offset: 0x00017F39
		internal static void EmitNull(this ILGenerator il)
		{
			il.Emit(OpCodes.Ldnull);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00019D46 File Offset: 0x00017F46
		internal static void EmitString(this ILGenerator il, string value)
		{
			il.Emit(OpCodes.Ldstr, value);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00019D54 File Offset: 0x00017F54
		internal static void EmitPrimitive(this ILGenerator il, bool value)
		{
			il.Emit(value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00019D6C File Offset: 0x00017F6C
		internal static void EmitPrimitive(this ILGenerator il, int value)
		{
			OpCode opCode;
			switch (value)
			{
			case -1:
				opCode = OpCodes.Ldc_I4_M1;
				break;
			case 0:
				opCode = OpCodes.Ldc_I4_0;
				break;
			case 1:
				opCode = OpCodes.Ldc_I4_1;
				break;
			case 2:
				opCode = OpCodes.Ldc_I4_2;
				break;
			case 3:
				opCode = OpCodes.Ldc_I4_3;
				break;
			case 4:
				opCode = OpCodes.Ldc_I4_4;
				break;
			case 5:
				opCode = OpCodes.Ldc_I4_5;
				break;
			case 6:
				opCode = OpCodes.Ldc_I4_6;
				break;
			case 7:
				opCode = OpCodes.Ldc_I4_7;
				break;
			case 8:
				opCode = OpCodes.Ldc_I4_8;
				break;
			default:
				if (value >= -128 && value <= 127)
				{
					il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
					return;
				}
				il.Emit(OpCodes.Ldc_I4, value);
				return;
			}
			il.Emit(opCode);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019E27 File Offset: 0x00018027
		private static void EmitPrimitive(this ILGenerator il, uint value)
		{
			il.EmitPrimitive((int)value);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00019E30 File Offset: 0x00018030
		private static void EmitPrimitive(this ILGenerator il, long value)
		{
			if ((-2147483648L <= value) & (value <= (long)((ulong)(-1))))
			{
				il.EmitPrimitive((int)value);
				il.Emit((value > 0L) ? OpCodes.Conv_U8 : OpCodes.Conv_I8);
				return;
			}
			il.Emit(OpCodes.Ldc_I8, value);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00019E80 File Offset: 0x00018080
		private static void EmitPrimitive(this ILGenerator il, ulong value)
		{
			il.EmitPrimitive((long)value);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00019E89 File Offset: 0x00018089
		private static void EmitPrimitive(this ILGenerator il, double value)
		{
			il.Emit(OpCodes.Ldc_R8, value);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00019E97 File Offset: 0x00018097
		private static void EmitPrimitive(this ILGenerator il, float value)
		{
			il.Emit(OpCodes.Ldc_R4, value);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00019EA8 File Offset: 0x000180A8
		internal static bool CanEmitConstant(object value, Type type)
		{
			if (value == null || ILGen.CanEmitILConstant(type))
			{
				return true;
			}
			Type type2 = value as Type;
			if (type2 != null)
			{
				return ILGen.ShouldLdtoken(type2);
			}
			MethodBase methodBase = value as MethodBase;
			return methodBase != null && ILGen.ShouldLdtoken(methodBase);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00019EF4 File Offset: 0x000180F4
		private static bool CanEmitILConstant(Type type)
		{
			TypeCode typeCode = type.GetNonNullableType().GetTypeCode();
			return typeCode - TypeCode.Boolean <= 12 || typeCode == TypeCode.String;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00019F1C File Offset: 0x0001811C
		internal static bool TryEmitConstant(this ILGenerator il, object value, Type type, ILocalCache locals)
		{
			if (value == null)
			{
				il.EmitDefault(type, locals);
				return true;
			}
			if (il.TryEmitILConstant(value, type))
			{
				return true;
			}
			Type type2 = value as Type;
			if (type2 != null)
			{
				if (ILGen.ShouldLdtoken(type2))
				{
					il.EmitType(type2);
					if (type != typeof(Type))
					{
						il.Emit(OpCodes.Castclass, type);
					}
					return true;
				}
				return false;
			}
			else
			{
				MethodBase methodBase = value as MethodBase;
				if (methodBase != null && ILGen.ShouldLdtoken(methodBase))
				{
					il.Emit(OpCodes.Ldtoken, methodBase);
					Type declaringType = methodBase.DeclaringType;
					if (declaringType != null && declaringType.IsGenericType)
					{
						il.Emit(OpCodes.Ldtoken, declaringType);
						il.Emit(OpCodes.Call, CachedReflectionInfo.MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle);
					}
					else
					{
						il.Emit(OpCodes.Call, CachedReflectionInfo.MethodBase_GetMethodFromHandle_RuntimeMethodHandle);
					}
					if (type != typeof(MethodBase))
					{
						il.Emit(OpCodes.Castclass, type);
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001A00E File Offset: 0x0001820E
		private static bool ShouldLdtoken(Type t)
		{
			return t.IsGenericParameter || t.IsVisible;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001A020 File Offset: 0x00018220
		internal static bool ShouldLdtoken(MethodBase mb)
		{
			if (mb is DynamicMethod)
			{
				return false;
			}
			Type declaringType = mb.DeclaringType;
			return declaringType == null || ILGen.ShouldLdtoken(declaringType);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001A050 File Offset: 0x00018250
		private static bool TryEmitILConstant(this ILGenerator il, object value, Type type)
		{
			if (!type.IsNullableType())
			{
				switch (type.GetTypeCode())
				{
				case TypeCode.Boolean:
					il.EmitPrimitive((bool)value);
					return true;
				case TypeCode.Char:
					il.EmitPrimitive((int)((char)value));
					return true;
				case TypeCode.SByte:
					il.EmitPrimitive((int)((sbyte)value));
					return true;
				case TypeCode.Byte:
					il.EmitPrimitive((int)((byte)value));
					return true;
				case TypeCode.Int16:
					il.EmitPrimitive((int)((short)value));
					return true;
				case TypeCode.UInt16:
					il.EmitPrimitive((int)((ushort)value));
					return true;
				case TypeCode.Int32:
					il.EmitPrimitive((int)value);
					return true;
				case TypeCode.UInt32:
					il.EmitPrimitive((uint)value);
					return true;
				case TypeCode.Int64:
					il.EmitPrimitive((long)value);
					return true;
				case TypeCode.UInt64:
					il.EmitPrimitive((ulong)value);
					return true;
				case TypeCode.Single:
					il.EmitPrimitive((float)value);
					return true;
				case TypeCode.Double:
					il.EmitPrimitive((double)value);
					return true;
				case TypeCode.Decimal:
					il.EmitDecimal((decimal)value);
					return true;
				case TypeCode.String:
					il.EmitString((string)value);
					return true;
				}
				return false;
			}
			Type nonNullableType = type.GetNonNullableType();
			if (il.TryEmitILConstant(value, nonNullableType))
			{
				il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { nonNullableType }));
				return true;
			}
			return false;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001A1B0 File Offset: 0x000183B0
		internal static void EmitConvertToType(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked, ILocalCache locals)
		{
			if (TypeUtils.AreEquivalent(typeFrom, typeTo))
			{
				return;
			}
			bool flag = typeFrom.IsNullableType();
			bool flag2 = typeTo.IsNullableType();
			Type nonNullableType = typeFrom.GetNonNullableType();
			Type nonNullableType2 = typeTo.GetNonNullableType();
			if (typeFrom.IsInterface || typeTo.IsInterface || typeFrom == typeof(object) || typeTo == typeof(object) || typeFrom == typeof(Enum) || typeFrom == typeof(ValueType) || TypeUtils.IsLegalExplicitVariantDelegateConversion(typeFrom, typeTo))
			{
				il.EmitCastToType(typeFrom, typeTo);
				return;
			}
			if (flag || flag2)
			{
				il.EmitNullableConversion(typeFrom, typeTo, isChecked, locals);
				return;
			}
			if ((!typeFrom.IsConvertible() || !typeTo.IsConvertible()) && (nonNullableType.IsAssignableFrom(nonNullableType2) || nonNullableType2.IsAssignableFrom(nonNullableType)))
			{
				il.EmitCastToType(typeFrom, typeTo);
				return;
			}
			if (typeFrom.IsArray && typeTo.IsArray)
			{
				il.EmitCastToType(typeFrom, typeTo);
				return;
			}
			il.EmitNumericConversion(typeFrom, typeTo, isChecked);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001A2AC File Offset: 0x000184AC
		private static void EmitCastToType(this ILGenerator il, Type typeFrom, Type typeTo)
		{
			if (typeFrom.IsValueType)
			{
				il.Emit(OpCodes.Box, typeFrom);
				if (typeTo != typeof(object))
				{
					il.Emit(OpCodes.Castclass, typeTo);
					return;
				}
			}
			else
			{
				il.Emit(typeTo.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, typeTo);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001A308 File Offset: 0x00018508
		private static void EmitNumericConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked)
		{
			TypeCode typeCode = typeTo.GetTypeCode();
			TypeCode typeCode2 = typeFrom.GetTypeCode();
			if (typeCode == typeCode2)
			{
				return;
			}
			bool flag = typeCode2.IsUnsigned();
			OpCode opCode;
			switch (typeCode)
			{
			case TypeCode.Char:
			case TypeCode.UInt16:
				switch (typeCode2)
				{
				case TypeCode.Char:
				case TypeCode.Byte:
				case TypeCode.UInt16:
					return;
				case TypeCode.SByte:
				case TypeCode.Int16:
					if (!isChecked)
					{
						return;
					}
					break;
				}
				opCode = (isChecked ? (flag ? OpCodes.Conv_Ovf_U2_Un : OpCodes.Conv_Ovf_U2) : OpCodes.Conv_U2);
				break;
			case TypeCode.SByte:
				if (isChecked)
				{
					opCode = (flag ? OpCodes.Conv_Ovf_I1_Un : OpCodes.Conv_Ovf_I1);
				}
				else
				{
					if (typeCode2 == TypeCode.Byte)
					{
						return;
					}
					opCode = OpCodes.Conv_I1;
				}
				break;
			case TypeCode.Byte:
				if (isChecked)
				{
					opCode = (flag ? OpCodes.Conv_Ovf_U1_Un : OpCodes.Conv_Ovf_U1);
				}
				else
				{
					if (typeCode2 == TypeCode.SByte)
					{
						return;
					}
					opCode = OpCodes.Conv_U1;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Char:
				case TypeCode.UInt16:
					if (!isChecked)
					{
						return;
					}
					break;
				case TypeCode.SByte:
				case TypeCode.Byte:
					return;
				}
				opCode = (isChecked ? (flag ? OpCodes.Conv_Ovf_I2_Un : OpCodes.Conv_Ovf_I2) : OpCodes.Conv_I2);
				break;
			case TypeCode.Int32:
				if (typeCode2 - TypeCode.SByte <= 3)
				{
					return;
				}
				if (typeCode2 == TypeCode.UInt32)
				{
					if (!isChecked)
					{
						return;
					}
				}
				opCode = (isChecked ? (flag ? OpCodes.Conv_Ovf_I4_Un : OpCodes.Conv_Ovf_I4) : OpCodes.Conv_I4);
				break;
			case TypeCode.UInt32:
				switch (typeCode2)
				{
				case TypeCode.Char:
				case TypeCode.Byte:
				case TypeCode.UInt16:
					return;
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
					if (!isChecked)
					{
						return;
					}
					break;
				}
				opCode = (isChecked ? (flag ? OpCodes.Conv_Ovf_U4_Un : OpCodes.Conv_Ovf_U4) : OpCodes.Conv_U4);
				break;
			case TypeCode.Int64:
				if (!isChecked && typeCode2 == TypeCode.UInt64)
				{
					return;
				}
				opCode = (isChecked ? (flag ? OpCodes.Conv_Ovf_I8_Un : OpCodes.Conv_Ovf_I8) : (flag ? OpCodes.Conv_U8 : OpCodes.Conv_I8));
				break;
			case TypeCode.UInt64:
				if (!isChecked && typeCode2 == TypeCode.Int64)
				{
					return;
				}
				opCode = (isChecked ? ((flag || typeCode2.IsFloatingPoint()) ? OpCodes.Conv_Ovf_U8_Un : OpCodes.Conv_Ovf_U8) : ((flag || typeCode2.IsFloatingPoint()) ? OpCodes.Conv_U8 : OpCodes.Conv_I8));
				break;
			case TypeCode.Single:
				if (flag)
				{
					il.Emit(OpCodes.Conv_R_Un);
				}
				opCode = OpCodes.Conv_R4;
				break;
			case TypeCode.Double:
				if (flag)
				{
					il.Emit(OpCodes.Conv_R_Un);
				}
				opCode = OpCodes.Conv_R8;
				break;
			case TypeCode.Decimal:
			{
				MethodInfo methodInfo;
				switch (typeCode2)
				{
				case TypeCode.Char:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_Char;
					break;
				case TypeCode.SByte:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_SByte;
					break;
				case TypeCode.Byte:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_Byte;
					break;
				case TypeCode.Int16:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_Int16;
					break;
				case TypeCode.UInt16:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_UInt16;
					break;
				case TypeCode.Int32:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_Int32;
					break;
				case TypeCode.UInt32:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_UInt32;
					break;
				case TypeCode.Int64:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_Int64;
					break;
				case TypeCode.UInt64:
					methodInfo = CachedReflectionInfo.Decimal_op_Implicit_UInt64;
					break;
				default:
					throw ContractUtils.Unreachable;
				}
				il.Emit(OpCodes.Call, methodInfo);
				return;
			}
			default:
				throw ContractUtils.Unreachable;
			}
			il.Emit(opCode);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001A5F4 File Offset: 0x000187F4
		private static void EmitNullableToNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked, ILocalCache locals)
		{
			LocalBuilder local = locals.GetLocal(typeFrom);
			il.Emit(OpCodes.Stloc, local);
			il.Emit(OpCodes.Ldloca, local);
			il.EmitHasValue(typeFrom);
			Label label = il.DefineLabel();
			il.Emit(OpCodes.Brfalse_S, label);
			il.Emit(OpCodes.Ldloca, local);
			locals.FreeLocal(local);
			il.EmitGetValueOrDefault(typeFrom);
			Type nonNullableType = typeFrom.GetNonNullableType();
			Type nonNullableType2 = typeTo.GetNonNullableType();
			il.EmitConvertToType(nonNullableType, nonNullableType2, isChecked, locals);
			ConstructorInfo constructor = typeTo.GetConstructor(new Type[] { nonNullableType2 });
			il.Emit(OpCodes.Newobj, constructor);
			Label label2 = il.DefineLabel();
			il.Emit(OpCodes.Br_S, label2);
			il.MarkLabel(label);
			LocalBuilder local2 = locals.GetLocal(typeTo);
			il.Emit(OpCodes.Ldloca, local2);
			il.Emit(OpCodes.Initobj, typeTo);
			il.Emit(OpCodes.Ldloc, local2);
			locals.FreeLocal(local2);
			il.MarkLabel(label2);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001A6EC File Offset: 0x000188EC
		private static void EmitNonNullableToNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked, ILocalCache locals)
		{
			Type nonNullableType = typeTo.GetNonNullableType();
			il.EmitConvertToType(typeFrom, nonNullableType, isChecked, locals);
			ConstructorInfo constructor = typeTo.GetConstructor(new Type[] { nonNullableType });
			il.Emit(OpCodes.Newobj, constructor);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001A728 File Offset: 0x00018928
		private static void EmitNullableToNonNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked, ILocalCache locals)
		{
			if (typeTo.IsValueType)
			{
				il.EmitNullableToNonNullableStructConversion(typeFrom, typeTo, isChecked, locals);
				return;
			}
			il.EmitNullableToReferenceConversion(typeFrom);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001A748 File Offset: 0x00018948
		private static void EmitNullableToNonNullableStructConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked, ILocalCache locals)
		{
			LocalBuilder local = locals.GetLocal(typeFrom);
			il.Emit(OpCodes.Stloc, local);
			il.Emit(OpCodes.Ldloca, local);
			locals.FreeLocal(local);
			il.EmitGetValue(typeFrom);
			Type nonNullableType = typeFrom.GetNonNullableType();
			il.EmitConvertToType(nonNullableType, typeTo, isChecked, locals);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001A797 File Offset: 0x00018997
		private static void EmitNullableToReferenceConversion(this ILGenerator il, Type typeFrom)
		{
			il.Emit(OpCodes.Box, typeFrom);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001A7A8 File Offset: 0x000189A8
		private static void EmitNullableConversion(this ILGenerator il, Type typeFrom, Type typeTo, bool isChecked, ILocalCache locals)
		{
			bool flag = typeFrom.IsNullableType();
			bool flag2 = typeTo.IsNullableType();
			if (flag && flag2)
			{
				il.EmitNullableToNullableConversion(typeFrom, typeTo, isChecked, locals);
				return;
			}
			if (flag)
			{
				il.EmitNullableToNonNullableConversion(typeFrom, typeTo, isChecked, locals);
				return;
			}
			il.EmitNonNullableToNullableConversion(typeFrom, typeTo, isChecked, locals);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001A7F0 File Offset: 0x000189F0
		internal static void EmitHasValue(this ILGenerator il, Type nullableType)
		{
			MethodInfo method = nullableType.GetMethod("get_HasValue", BindingFlags.Instance | BindingFlags.Public);
			il.Emit(OpCodes.Call, method);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001A818 File Offset: 0x00018A18
		internal static void EmitGetValue(this ILGenerator il, Type nullableType)
		{
			MethodInfo method = nullableType.GetMethod("get_Value", BindingFlags.Instance | BindingFlags.Public);
			il.Emit(OpCodes.Call, method);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001A840 File Offset: 0x00018A40
		internal static void EmitGetValueOrDefault(this ILGenerator il, Type nullableType)
		{
			MethodInfo method = nullableType.GetMethod("GetValueOrDefault", Type.EmptyTypes);
			il.Emit(OpCodes.Call, method);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001A86C File Offset: 0x00018A6C
		internal static void EmitArray<T>(this ILGenerator il, T[] items, ILocalCache locals)
		{
			il.EmitPrimitive(items.Length);
			il.Emit(OpCodes.Newarr, typeof(T));
			for (int i = 0; i < items.Length; i++)
			{
				il.Emit(OpCodes.Dup);
				il.EmitPrimitive(i);
				il.TryEmitConstant(items[i], typeof(T), locals);
				il.EmitStoreElement(typeof(T));
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001A8E5 File Offset: 0x00018AE5
		internal static void EmitArray(this ILGenerator il, Type elementType, int count)
		{
			il.EmitPrimitive(count);
			il.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001A8FC File Offset: 0x00018AFC
		internal static void EmitArray(this ILGenerator il, Type arrayType)
		{
			if (arrayType.IsSZArray)
			{
				il.Emit(OpCodes.Newarr, arrayType.GetElementType());
				return;
			}
			Type[] array = new Type[arrayType.GetArrayRank()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = typeof(int);
			}
			ConstructorInfo constructor = arrayType.GetConstructor(array);
			il.EmitNew(constructor);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001A95C File Offset: 0x00018B5C
		private static void EmitDecimal(this ILGenerator il, decimal value)
		{
			int[] bits = decimal.GetBits(value);
			int num = (bits[3] & int.MaxValue) >> 16;
			if (num == 0)
			{
				if (-2147483648m <= value)
				{
					if (value <= 2147483647m)
					{
						int num2 = decimal.ToInt32(value);
						switch (num2)
						{
						case -1:
							il.Emit(OpCodes.Ldsfld, CachedReflectionInfo.Decimal_MinusOne);
							return;
						case 0:
							il.EmitDefault(typeof(decimal), null);
							return;
						case 1:
							il.Emit(OpCodes.Ldsfld, CachedReflectionInfo.Decimal_One);
							return;
						default:
							il.EmitPrimitive(num2);
							il.EmitNew(CachedReflectionInfo.Decimal_Ctor_Int32);
							return;
						}
					}
					else if (value <= 4294967295m)
					{
						il.EmitPrimitive(decimal.ToUInt32(value));
						il.EmitNew(CachedReflectionInfo.Decimal_Ctor_UInt32);
						return;
					}
				}
				if (-9223372036854775808m <= value)
				{
					if (value <= 9223372036854775807m)
					{
						il.EmitPrimitive(decimal.ToInt64(value));
						il.EmitNew(CachedReflectionInfo.Decimal_Ctor_Int64);
						return;
					}
					if (value <= 18446744073709551615m)
					{
						il.EmitPrimitive(decimal.ToUInt64(value));
						il.EmitNew(CachedReflectionInfo.Decimal_Ctor_UInt64);
						return;
					}
					if (value == 79228162514264337593543950335m)
					{
						il.Emit(OpCodes.Ldsfld, CachedReflectionInfo.Decimal_MaxValue);
						return;
					}
				}
				else if (value == -79228162514264337593543950335m)
				{
					il.Emit(OpCodes.Ldsfld, CachedReflectionInfo.Decimal_MinValue);
					return;
				}
			}
			il.EmitPrimitive(bits[0]);
			il.EmitPrimitive(bits[1]);
			il.EmitPrimitive(bits[2]);
			il.EmitPrimitive(((long)bits[3] & (long)((ulong)int.MinValue)) != 0L);
			il.EmitPrimitive((int)((byte)num));
			il.EmitNew(CachedReflectionInfo.Decimal_Ctor_Int32_Int32_Int32_Bool_Byte);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001AB28 File Offset: 0x00018D28
		internal static void EmitDefault(this ILGenerator il, Type type, ILocalCache locals)
		{
			switch (type.GetTypeCode())
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
			case TypeCode.String:
				break;
			case TypeCode.Object:
				if (type.IsValueType)
				{
					LocalBuilder local = locals.GetLocal(type);
					il.Emit(OpCodes.Ldloca, local);
					il.Emit(OpCodes.Initobj, type);
					il.Emit(OpCodes.Ldloc, local);
					locals.FreeLocal(local);
					return;
				}
				break;
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
				il.Emit(OpCodes.Ldc_I4_0);
				return;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				il.Emit(OpCodes.Ldc_I4_0);
				il.Emit(OpCodes.Conv_I8);
				return;
			case TypeCode.Single:
				il.Emit(OpCodes.Ldc_R4, 0f);
				return;
			case TypeCode.Double:
				il.Emit(OpCodes.Ldc_R8, 0.0);
				return;
			case TypeCode.Decimal:
				il.Emit(OpCodes.Ldsfld, CachedReflectionInfo.Decimal_Zero);
				return;
			case TypeCode.DateTime:
				il.Emit(OpCodes.Ldsfld, CachedReflectionInfo.DateTime_MinValue);
				return;
			case (TypeCode)17:
				goto IL_0111;
			default:
				goto IL_0111;
			}
			il.Emit(OpCodes.Ldnull);
			return;
			IL_0111:
			throw ContractUtils.Unreachable;
		}
	}
}
