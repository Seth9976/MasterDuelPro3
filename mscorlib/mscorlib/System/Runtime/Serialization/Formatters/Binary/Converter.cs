using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F5 RID: 1269
	internal sealed class Converter
	{
		// Token: 0x060027A3 RID: 10147 RVA: 0x0009FC7C File Offset: 0x0009DE7C
		internal static InternalPrimitiveTypeE ToCode(Type type)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE;
			if (type != null && !type.IsPrimitive)
			{
				if (type == Converter.typeofDateTime)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.DateTime;
				}
				else if (type == Converter.typeofTimeSpan)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.TimeSpan;
				}
				else if (type == Converter.typeofDecimal)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Decimal;
				}
				else
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Invalid;
				}
			}
			else
			{
				internalPrimitiveTypeE = Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type));
			}
			return internalPrimitiveTypeE;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0009FCCC File Offset: 0x0009DECC
		internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
		{
			bool flag = false;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Char:
			case InternalPrimitiveTypeE.Double:
			case InternalPrimitiveTypeE.Int16:
			case InternalPrimitiveTypeE.Int32:
			case InternalPrimitiveTypeE.Int64:
			case InternalPrimitiveTypeE.SByte:
			case InternalPrimitiveTypeE.Single:
			case InternalPrimitiveTypeE.UInt16:
			case InternalPrimitiveTypeE.UInt32:
			case InternalPrimitiveTypeE.UInt64:
				flag = true;
				break;
			}
			return flag;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x0009FD28 File Offset: 0x0009DF28
		internal static int TypeLength(InternalPrimitiveTypeE code)
		{
			int num = 0;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Byte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Char:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Double:
				num = 8;
				break;
			case InternalPrimitiveTypeE.Int16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Int32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.Int64:
				num = 8;
				break;
			case InternalPrimitiveTypeE.SByte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Single:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.UInt32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt64:
				num = 8;
				break;
			}
			return num;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0009FDB0 File Offset: 0x0009DFB0
		internal static Type ToArrayType(InternalPrimitiveTypeE code)
		{
			if (Converter.arrayTypeA == null)
			{
				Converter.InitArrayTypeA();
			}
			return Converter.arrayTypeA[(int)code];
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0009FDCC File Offset: 0x0009DFCC
		private static void InitTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBoolean;
			array[2] = Converter.typeofByte;
			array[3] = Converter.typeofChar;
			array[5] = Converter.typeofDecimal;
			array[6] = Converter.typeofDouble;
			array[7] = Converter.typeofInt16;
			array[8] = Converter.typeofInt32;
			array[9] = Converter.typeofInt64;
			array[10] = Converter.typeofSByte;
			array[11] = Converter.typeofSingle;
			array[12] = Converter.typeofTimeSpan;
			array[13] = Converter.typeofDateTime;
			array[14] = Converter.typeofUInt16;
			array[15] = Converter.typeofUInt32;
			array[16] = Converter.typeofUInt64;
			Converter.typeA = array;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0009FE70 File Offset: 0x0009E070
		private static void InitArrayTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBooleanArray;
			array[2] = Converter.typeofByteArray;
			array[3] = Converter.typeofCharArray;
			array[5] = Converter.typeofDecimalArray;
			array[6] = Converter.typeofDoubleArray;
			array[7] = Converter.typeofInt16Array;
			array[8] = Converter.typeofInt32Array;
			array[9] = Converter.typeofInt64Array;
			array[10] = Converter.typeofSByteArray;
			array[11] = Converter.typeofSingleArray;
			array[12] = Converter.typeofTimeSpanArray;
			array[13] = Converter.typeofDateTimeArray;
			array[14] = Converter.typeofUInt16Array;
			array[15] = Converter.typeofUInt32Array;
			array[16] = Converter.typeofUInt64Array;
			Converter.arrayTypeA = array;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0009FF12 File Offset: 0x0009E112
		internal static Type ToType(InternalPrimitiveTypeE code)
		{
			if (Converter.typeA == null)
			{
				Converter.InitTypeA();
			}
			return Converter.typeA[(int)code];
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0009FF2C File Offset: 0x0009E12C
		internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
		{
			Array array = null;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				array = new bool[length];
				break;
			case InternalPrimitiveTypeE.Byte:
				array = new byte[length];
				break;
			case InternalPrimitiveTypeE.Char:
				array = new char[length];
				break;
			case InternalPrimitiveTypeE.Decimal:
				array = new decimal[length];
				break;
			case InternalPrimitiveTypeE.Double:
				array = new double[length];
				break;
			case InternalPrimitiveTypeE.Int16:
				array = new short[length];
				break;
			case InternalPrimitiveTypeE.Int32:
				array = new int[length];
				break;
			case InternalPrimitiveTypeE.Int64:
				array = new long[length];
				break;
			case InternalPrimitiveTypeE.SByte:
				array = new sbyte[length];
				break;
			case InternalPrimitiveTypeE.Single:
				array = new float[length];
				break;
			case InternalPrimitiveTypeE.TimeSpan:
				array = new TimeSpan[length];
				break;
			case InternalPrimitiveTypeE.DateTime:
				array = new DateTime[length];
				break;
			case InternalPrimitiveTypeE.UInt16:
				array = new ushort[length];
				break;
			case InternalPrimitiveTypeE.UInt32:
				array = new uint[length];
				break;
			case InternalPrimitiveTypeE.UInt64:
				array = new ulong[length];
				break;
			}
			return array;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000A0010 File Offset: 0x0009E210
		internal static bool IsPrimitiveArray(Type type, out object typeInformation)
		{
			typeInformation = null;
			bool flag = true;
			if (type == Converter.typeofBooleanArray)
			{
				typeInformation = InternalPrimitiveTypeE.Boolean;
			}
			else if (type == Converter.typeofByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.Byte;
			}
			else if (type == Converter.typeofCharArray)
			{
				typeInformation = InternalPrimitiveTypeE.Char;
			}
			else if (type == Converter.typeofDoubleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Double;
			}
			else if (type == Converter.typeofInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int16;
			}
			else if (type == Converter.typeofInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int32;
			}
			else if (type == Converter.typeofInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int64;
			}
			else if (type == Converter.typeofSByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.SByte;
			}
			else if (type == Converter.typeofSingleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Single;
			}
			else if (type == Converter.typeofUInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt16;
			}
			else if (type == Converter.typeofUInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt32;
			}
			else if (type == Converter.typeofUInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt64;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000A0114 File Offset: 0x0009E314
		private static void InitValueA()
		{
			string[] array = new string[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = "Boolean";
			array[2] = "Byte";
			array[3] = "Char";
			array[5] = "Decimal";
			array[6] = "Double";
			array[7] = "Int16";
			array[8] = "Int32";
			array[9] = "Int64";
			array[10] = "SByte";
			array[11] = "Single";
			array[12] = "TimeSpan";
			array[13] = "DateTime";
			array[14] = "UInt16";
			array[15] = "UInt32";
			array[16] = "UInt64";
			Converter.valueA = array;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000A01B6 File Offset: 0x0009E3B6
		internal static string ToComType(InternalPrimitiveTypeE code)
		{
			if (Converter.valueA == null)
			{
				Converter.InitValueA();
			}
			return Converter.valueA[(int)code];
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000A01D0 File Offset: 0x0009E3D0
		private static void InitTypeCodeA()
		{
			TypeCode[] array = new TypeCode[Converter.primitiveTypeEnumLength];
			array[0] = TypeCode.Object;
			array[1] = TypeCode.Boolean;
			array[2] = TypeCode.Byte;
			array[3] = TypeCode.Char;
			array[5] = TypeCode.Decimal;
			array[6] = TypeCode.Double;
			array[7] = TypeCode.Int16;
			array[8] = TypeCode.Int32;
			array[9] = TypeCode.Int64;
			array[10] = TypeCode.SByte;
			array[11] = TypeCode.Single;
			array[12] = TypeCode.Object;
			array[13] = TypeCode.DateTime;
			array[14] = TypeCode.UInt16;
			array[15] = TypeCode.UInt32;
			array[16] = TypeCode.UInt64;
			Converter.typeCodeA = array;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000A023E File Offset: 0x0009E43E
		internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
		{
			if (Converter.typeCodeA == null)
			{
				Converter.InitTypeCodeA();
			}
			return Converter.typeCodeA[(int)code];
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000A0258 File Offset: 0x0009E458
		private static void InitCodeA()
		{
			Converter.codeA = new InternalPrimitiveTypeE[]
			{
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Boolean,
				InternalPrimitiveTypeE.Char,
				InternalPrimitiveTypeE.SByte,
				InternalPrimitiveTypeE.Byte,
				InternalPrimitiveTypeE.Int16,
				InternalPrimitiveTypeE.UInt16,
				InternalPrimitiveTypeE.Int32,
				InternalPrimitiveTypeE.UInt32,
				InternalPrimitiveTypeE.Int64,
				InternalPrimitiveTypeE.UInt64,
				InternalPrimitiveTypeE.Single,
				InternalPrimitiveTypeE.Double,
				InternalPrimitiveTypeE.Decimal,
				InternalPrimitiveTypeE.DateTime,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid
			};
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000A02D0 File Offset: 0x0009E4D0
		internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
		{
			if (Converter.codeA == null)
			{
				Converter.InitCodeA();
			}
			return Converter.codeA[(int)typeCode];
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000A02EC File Offset: 0x0009E4EC
		internal static object FromString(string value, InternalPrimitiveTypeE code)
		{
			object obj;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				obj = Convert.ChangeType(value, Converter.ToTypeCode(code), CultureInfo.InvariantCulture);
			}
			else
			{
				obj = value;
			}
			return obj;
		}

		// Token: 0x04001390 RID: 5008
		private static int primitiveTypeEnumLength = 17;

		// Token: 0x04001391 RID: 5009
		private static volatile Type[] typeA;

		// Token: 0x04001392 RID: 5010
		private static volatile Type[] arrayTypeA;

		// Token: 0x04001393 RID: 5011
		private static volatile string[] valueA;

		// Token: 0x04001394 RID: 5012
		private static volatile TypeCode[] typeCodeA;

		// Token: 0x04001395 RID: 5013
		private static volatile InternalPrimitiveTypeE[] codeA;

		// Token: 0x04001396 RID: 5014
		internal static Type typeofISerializable = typeof(ISerializable);

		// Token: 0x04001397 RID: 5015
		internal static Type typeofString = typeof(string);

		// Token: 0x04001398 RID: 5016
		internal static Type typeofConverter = typeof(Converter);

		// Token: 0x04001399 RID: 5017
		internal static Type typeofBoolean = typeof(bool);

		// Token: 0x0400139A RID: 5018
		internal static Type typeofByte = typeof(byte);

		// Token: 0x0400139B RID: 5019
		internal static Type typeofChar = typeof(char);

		// Token: 0x0400139C RID: 5020
		internal static Type typeofDecimal = typeof(decimal);

		// Token: 0x0400139D RID: 5021
		internal static Type typeofDouble = typeof(double);

		// Token: 0x0400139E RID: 5022
		internal static Type typeofInt16 = typeof(short);

		// Token: 0x0400139F RID: 5023
		internal static Type typeofInt32 = typeof(int);

		// Token: 0x040013A0 RID: 5024
		internal static Type typeofInt64 = typeof(long);

		// Token: 0x040013A1 RID: 5025
		internal static Type typeofSByte = typeof(sbyte);

		// Token: 0x040013A2 RID: 5026
		internal static Type typeofSingle = typeof(float);

		// Token: 0x040013A3 RID: 5027
		internal static Type typeofTimeSpan = typeof(TimeSpan);

		// Token: 0x040013A4 RID: 5028
		internal static Type typeofDateTime = typeof(DateTime);

		// Token: 0x040013A5 RID: 5029
		internal static Type typeofUInt16 = typeof(ushort);

		// Token: 0x040013A6 RID: 5030
		internal static Type typeofUInt32 = typeof(uint);

		// Token: 0x040013A7 RID: 5031
		internal static Type typeofUInt64 = typeof(ulong);

		// Token: 0x040013A8 RID: 5032
		internal static Type typeofObject = typeof(object);

		// Token: 0x040013A9 RID: 5033
		internal static Type typeofSystemVoid = typeof(void);

		// Token: 0x040013AA RID: 5034
		internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);

		// Token: 0x040013AB RID: 5035
		internal static string urtAssemblyString = Converter.urtAssembly.FullName;

		// Token: 0x040013AC RID: 5036
		internal static Type typeofTypeArray = typeof(Type[]);

		// Token: 0x040013AD RID: 5037
		internal static Type typeofObjectArray = typeof(object[]);

		// Token: 0x040013AE RID: 5038
		internal static Type typeofStringArray = typeof(string[]);

		// Token: 0x040013AF RID: 5039
		internal static Type typeofBooleanArray = typeof(bool[]);

		// Token: 0x040013B0 RID: 5040
		internal static Type typeofByteArray = typeof(byte[]);

		// Token: 0x040013B1 RID: 5041
		internal static Type typeofCharArray = typeof(char[]);

		// Token: 0x040013B2 RID: 5042
		internal static Type typeofDecimalArray = typeof(decimal[]);

		// Token: 0x040013B3 RID: 5043
		internal static Type typeofDoubleArray = typeof(double[]);

		// Token: 0x040013B4 RID: 5044
		internal static Type typeofInt16Array = typeof(short[]);

		// Token: 0x040013B5 RID: 5045
		internal static Type typeofInt32Array = typeof(int[]);

		// Token: 0x040013B6 RID: 5046
		internal static Type typeofInt64Array = typeof(long[]);

		// Token: 0x040013B7 RID: 5047
		internal static Type typeofSByteArray = typeof(sbyte[]);

		// Token: 0x040013B8 RID: 5048
		internal static Type typeofSingleArray = typeof(float[]);

		// Token: 0x040013B9 RID: 5049
		internal static Type typeofTimeSpanArray = typeof(TimeSpan[]);

		// Token: 0x040013BA RID: 5050
		internal static Type typeofDateTimeArray = typeof(DateTime[]);

		// Token: 0x040013BB RID: 5051
		internal static Type typeofUInt16Array = typeof(ushort[]);

		// Token: 0x040013BC RID: 5052
		internal static Type typeofUInt32Array = typeof(uint[]);

		// Token: 0x040013BD RID: 5053
		internal static Type typeofUInt64Array = typeof(ulong[]);

		// Token: 0x040013BE RID: 5054
		internal static Type typeofMarshalByRefObject = typeof(MarshalByRefObject);
	}
}
