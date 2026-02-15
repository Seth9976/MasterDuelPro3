using System;
using System.Globalization;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200050D RID: 1293
	internal sealed class PrimitiveArray
	{
		// Token: 0x060028D2 RID: 10450 RVA: 0x000A76BC File Offset: 0x000A58BC
		internal PrimitiveArray(InternalPrimitiveTypeE code, Array array)
		{
			this.Init(code, array);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000A76CC File Offset: 0x000A58CC
		internal void Init(InternalPrimitiveTypeE code, Array array)
		{
			this.code = code;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA = (bool[])array;
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				this.charA = (char[])array;
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA = (double[])array;
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A = (short[])array;
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A = (int[])array;
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A = (long[])array;
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA = (sbyte[])array;
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA = (float[])array;
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A = (ushort[])array;
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A = (uint[])array;
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A = (ulong[])array;
				break;
			default:
				return;
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000A77B8 File Offset: 0x000A59B8
		internal void SetValue(string value, int index)
		{
			switch (this.code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA[index] = bool.Parse(value);
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				if (value[0] == '_' && value.Equals("_0x00_"))
				{
					this.charA[index] = '\0';
					return;
				}
				this.charA[index] = char.Parse(value);
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA[index] = double.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A[index] = short.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A[index] = int.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A[index] = long.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA[index] = sbyte.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA[index] = float.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A[index] = ushort.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A[index] = uint.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A[index] = ulong.Parse(value, CultureInfo.InvariantCulture);
				break;
			default:
				return;
			}
		}

		// Token: 0x040014B6 RID: 5302
		private InternalPrimitiveTypeE code;

		// Token: 0x040014B7 RID: 5303
		private bool[] booleanA;

		// Token: 0x040014B8 RID: 5304
		private char[] charA;

		// Token: 0x040014B9 RID: 5305
		private double[] doubleA;

		// Token: 0x040014BA RID: 5306
		private short[] int16A;

		// Token: 0x040014BB RID: 5307
		private int[] int32A;

		// Token: 0x040014BC RID: 5308
		private long[] int64A;

		// Token: 0x040014BD RID: 5309
		private sbyte[] sbyteA;

		// Token: 0x040014BE RID: 5310
		private float[] singleA;

		// Token: 0x040014BF RID: 5311
		private ushort[] uint16A;

		// Token: 0x040014C0 RID: 5312
		private uint[] uint32A;

		// Token: 0x040014C1 RID: 5313
		private ulong[] uint64A;
	}
}
