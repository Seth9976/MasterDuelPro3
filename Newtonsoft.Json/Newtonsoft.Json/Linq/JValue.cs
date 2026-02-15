using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200018C RID: 396
	[NullableContext(2)]
	[Nullable(0)]
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>, IConvertible
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003BB50 File Offset: 0x00039D50
		[NullableContext(1)]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return AsyncUtils.CompletedTask;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				return writer.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			}
			case JTokenType.Integer:
			{
				object obj = this._value;
				if (obj is int)
				{
					int num = (int)obj;
					return writer.WriteValueAsync(num, cancellationToken);
				}
				obj = this._value;
				if (obj is long)
				{
					long num2 = (long)obj;
					return writer.WriteValueAsync(num2, cancellationToken);
				}
				obj = this._value;
				if (obj is ulong)
				{
					ulong num3 = (ulong)obj;
					return writer.WriteValueAsync(num3, cancellationToken);
				}
				obj = this._value;
				if (obj is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)obj;
					return writer.WriteValueAsync(bigInteger, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToInt64(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Float:
			{
				object obj = this._value;
				if (obj is decimal)
				{
					decimal num4 = (decimal)obj;
					return writer.WriteValueAsync(num4, cancellationToken);
				}
				obj = this._value;
				if (obj is double)
				{
					double num5 = (double)obj;
					return writer.WriteValueAsync(num5, cancellationToken);
				}
				obj = this._value;
				if (obj is float)
				{
					float num6 = (float)obj;
					return writer.WriteValueAsync(num6, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDouble(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.String:
			{
				object value2 = this._value;
				return writer.WriteValueAsync((value2 != null) ? value2.ToString() : null, cancellationToken);
			}
			case JTokenType.Boolean:
				return writer.WriteValueAsync(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture), cancellationToken);
			case JTokenType.Null:
				return writer.WriteNullAsync(cancellationToken);
			case JTokenType.Undefined:
				return writer.WriteUndefinedAsync(cancellationToken);
			case JTokenType.Date:
			{
				object obj = this._value;
				if (obj is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)obj;
					return writer.WriteValueAsync(dateTimeOffset, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Raw:
			{
				object value3 = this._value;
				return writer.WriteRawValueAsync((value3 != null) ? value3.ToString() : null, cancellationToken);
			}
			case JTokenType.Bytes:
				return writer.WriteValueAsync((byte[])this._value, cancellationToken);
			case JTokenType.Guid:
				return writer.WriteValueAsync((this._value != null) ? ((Guid?)this._value) : null, cancellationToken);
			case JTokenType.Uri:
				return writer.WriteValueAsync((Uri)this._value, cancellationToken);
			case JTokenType.TimeSpan:
				return writer.WriteValueAsync((this._value != null) ? ((TimeSpan?)this._value) : null, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003BE56 File Offset: 0x0003A056
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003BE6C File Offset: 0x0003A06C
		[NullableContext(1)]
		internal JValue(JValue other, [Nullable(2)] JsonCloneSettings settings)
			: this(other.Value, other.Type)
		{
			if (settings == null || settings.CopyAnnotations)
			{
				base.CopyAnnotations(this, other);
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003BE96 File Offset: 0x0003A096
		[NullableContext(1)]
		public JValue(JValue other)
			: this(other.Value, other.Type)
		{
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003BEAA File Offset: 0x0003A0AA
		public JValue(long value)
			: this(BoxedPrimitives.Get(value), JTokenType.Integer)
		{
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003BEB9 File Offset: 0x0003A0B9
		public JValue(decimal value)
			: this(BoxedPrimitives.Get(value), JTokenType.Float)
		{
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
		public JValue(char value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003BED7 File Offset: 0x0003A0D7
		[CLSCompliant(false)]
		public JValue(ulong value)
			: this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003BEE6 File Offset: 0x0003A0E6
		public JValue(double value)
			: this(BoxedPrimitives.Get(value), JTokenType.Float)
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003BEF5 File Offset: 0x0003A0F5
		public JValue(float value)
			: this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003BF04 File Offset: 0x0003A104
		public JValue(DateTime value)
			: this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003BF14 File Offset: 0x0003A114
		public JValue(DateTimeOffset value)
			: this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003BF24 File Offset: 0x0003A124
		public JValue(bool value)
			: this(BoxedPrimitives.Get(value), JTokenType.Boolean)
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003BF34 File Offset: 0x0003A134
		public JValue(string value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003BF3E File Offset: 0x0003A13E
		public JValue(Guid value)
			: this(value, JTokenType.Guid)
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003BF4E File Offset: 0x0003A14E
		public JValue(Uri value)
			: this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003BF66 File Offset: 0x0003A166
		public JValue(TimeSpan value)
			: this(value, JTokenType.TimeSpan)
		{
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003BF78 File Offset: 0x0003A178
		public JValue(object value)
			: this(value, JValue.GetValueType(null, value))
		{
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003BF9C File Offset: 0x0003A19C
		[NullableContext(1)]
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00016B42 File Offset: 0x00014D42
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
		[NullableContext(1)]
		private static int CompareBigInteger(BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
				return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
			}
			return num;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003C050 File Offset: 0x0003A250
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string text = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string text2 = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(text, text2);
			}
			case JTokenType.Integer:
				if (objA is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)objA;
					return JValue.CompareBigInteger(bigInteger, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger bigInteger2 = (BigInteger)objB;
					return -JValue.CompareBigInteger(bigInteger2, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				if (objA is BigInteger)
				{
					BigInteger bigInteger3 = (BigInteger)objA;
					return JValue.CompareBigInteger(bigInteger3, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger bigInteger4 = (BigInteger)objB;
					return -JValue.CompareBigInteger(bigInteger4, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool flag2 = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(flag2);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime dateTime2;
					if (objB is DateTimeOffset)
					{
						dateTime2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						dateTime2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(dateTime2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset dateTimeOffset2;
				if (objB is DateTimeOffset)
				{
					dateTimeOffset2 = (DateTimeOffset)objB;
				}
				else
				{
					dateTimeOffset2 = new DateTimeOffset(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(dateTimeOffset2);
			}
			case JTokenType.Bytes:
			{
				byte[] array = objB as byte[];
				if (array == null)
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], array);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid guid2 = (Guid)objB;
				return guid.CompareTo(guid2);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan timeSpan2 = (TimeSpan)objB;
				return timeSpan.CompareTo(timeSpan2);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003C37C File Offset: 0x0003A57C
		[NullableContext(1)]
		private static int CompareFloat(object objA, object objB)
		{
			double num = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num2 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(num, num2))
			{
				return 0;
			}
			return num.CompareTo(num2);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003C3B4 File Offset: 0x0003A5B4
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == ExpressionType.Add || operation == ExpressionType.AddAssign))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is BigInteger || objB is BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
				BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_0393;
							}
							goto IL_00DE;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_00CE;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_0393;
						}
						goto IL_00BE;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_0393;
						}
						goto IL_00DE;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_00CE;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_0393;
					}
					goto IL_00BE;
				}
				result = bigInteger + bigInteger2;
				return true;
				IL_00BE:
				result = bigInteger - bigInteger2;
				return true;
				IL_00CE:
				result = bigInteger * bigInteger2;
				return true;
				IL_00DE:
				result = bigInteger / bigInteger2;
				return true;
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal num = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal num2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_0393;
							}
							goto IL_01AD;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_019D;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_0393;
						}
						goto IL_018D;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_0393;
						}
						goto IL_01AD;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_019D;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_0393;
					}
					goto IL_018D;
				}
				result = num + num2;
				return true;
				IL_018D:
				result = num - num2;
				return true;
				IL_019D:
				result = num * num2;
				return true;
				IL_01AD:
				result = num / num2;
				return true;
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num3 = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
				double num4 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_0393;
							}
							goto IL_0278;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_026A;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_0393;
						}
						goto IL_025C;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_0393;
						}
						goto IL_0278;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_026A;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_0393;
					}
					goto IL_025C;
				}
				result = num3 + num4;
				return true;
				IL_025C:
				result = num3 - num4;
				return true;
				IL_026A:
				result = num3 * num4;
				return true;
				IL_0278:
				result = num3 / num4;
				return true;
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num5 = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
				long num6 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_0393;
							}
							goto IL_0385;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_0377;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_0393;
						}
						goto IL_0369;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_0393;
						}
						goto IL_0385;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_0377;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_0393;
					}
					goto IL_0369;
				}
				result = num5 + num6;
				return true;
				IL_0369:
				result = num5 - num6;
				return true;
				IL_0377:
				result = num5 * num6;
				return true;
				IL_0385:
				result = num5 / num6;
				return true;
			}
			IL_0393:
			result = null;
			return false;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0003C758 File Offset: 0x0003A958
		[NullableContext(1)]
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JValue(this, settings);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003C761 File Offset: 0x0003A961
		[NullableContext(1)]
		public static JValue CreateComment([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003C76A File Offset: 0x0003A96A
		[NullableContext(1)]
		public static JValue CreateString([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003C773 File Offset: 0x0003A973
		[NullableContext(1)]
		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003C77D File Offset: 0x0003A97D
		[NullableContext(1)]
		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003C788 File Offset: 0x0003A988
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is BigInteger)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003C88C File Offset: 0x0003AA8C
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == JTokenType.Comment || valueOrDefault == JTokenType.String || valueOrDefault == JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return JTokenType.String;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003C8C2 File Offset: 0x0003AAC2
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003C8CA File Offset: 0x0003AACA
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		public new object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				object value2 = this._value;
				Type type = ((value2 != null) ? value2.GetType() : null);
				Type type2 = ((value != null) ? value.GetType() : null);
				if (type != type2)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003C928 File Offset: 0x0003AB28
		[NullableContext(1)]
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
			{
				object obj = this._value;
				if (obj is int)
				{
					int num = (int)obj;
					writer.WriteValue(num);
					return;
				}
				obj = this._value;
				if (obj is long)
				{
					long num2 = (long)obj;
					writer.WriteValue(num2);
					return;
				}
				obj = this._value;
				if (obj is ulong)
				{
					ulong num3 = (ulong)obj;
					writer.WriteValue(num3);
					return;
				}
				obj = this._value;
				if (obj is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)obj;
					writer.WriteValue(bigInteger);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Float:
			{
				object obj = this._value;
				if (obj is decimal)
				{
					decimal num4 = (decimal)obj;
					writer.WriteValue(num4);
					return;
				}
				obj = this._value;
				if (obj is double)
				{
					double num5 = (double)obj;
					writer.WriteValue(num5);
					return;
				}
				obj = this._value;
				if (obj is float)
				{
					float num6 = (float)obj;
					writer.WriteValue(num6);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.String:
			{
				object value2 = this._value;
				writer.WriteValue((value2 != null) ? value2.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
			{
				object obj = this._value;
				if (obj is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)obj;
					writer.WriteValue(dateTimeOffset);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Raw:
			{
				object value3 = this._value;
				writer.WriteRawValue((value3 != null) ? value3.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : null);
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : null);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003CC14 File Offset: 0x0003AE14
		internal override int GetDeepHashCode()
		{
			int num = ((this._value != null) ? this._value.GetHashCode() : 0);
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003CC48 File Offset: 0x0003AE48
		[NullableContext(1)]
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003CC7A File Offset: 0x0003AE7A
		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003CC88 File Offset: 0x0003AE88
		public override bool Equals(object obj)
		{
			JValue jvalue = obj as JValue;
			return jvalue != null && this.Equals(jvalue);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003CCBF File Offset: 0x0003AEBF
		[NullableContext(1)]
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003CCDA File Offset: 0x0003AEDA
		[NullableContext(1)]
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003CD32 File Offset: 0x0003AF32
		[NullableContext(1)]
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003CD40 File Offset: 0x0003AF40
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue = obj as JValue;
			object obj2;
			JTokenType jtokenType;
			if (jvalue != null)
			{
				obj2 = jvalue.Value;
				jtokenType = ((this._valueType == JTokenType.String && this._valueType != jvalue._valueType) ? jvalue._valueType : this._valueType);
			}
			else
			{
				obj2 = obj;
				jtokenType = this._valueType;
			}
			return JValue.Compare(jtokenType, this._value, obj2);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003CDA1 File Offset: 0x0003AFA1
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare((this._valueType == JTokenType.String && this._valueType != obj._valueType) ? obj._valueType : this._valueType, this._value, obj._value);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003CDE0 File Offset: 0x0003AFE0
		TypeCode IConvertible.GetTypeCode()
		{
			if (this._value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = this._value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003CE0E File Offset: 0x0003B00E
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003CE16 File Offset: 0x0003B016
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003CE1E File Offset: 0x0003B01E
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)this;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003CE26 File Offset: 0x0003B026
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003CE2E File Offset: 0x0003B02E
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003CE36 File Offset: 0x0003B036
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)this;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003CE3E File Offset: 0x0003B03E
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003CE46 File Offset: 0x0003B046
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)this;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003CE4E File Offset: 0x0003B04E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003CE56 File Offset: 0x0003B056
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)this;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003CE5E File Offset: 0x0003B05E
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)this;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003CE67 File Offset: 0x0003B067
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003CE70 File Offset: 0x0003B070
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003CE78 File Offset: 0x0003B078
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return (DateTime)this;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003CE80 File Offset: 0x0003B080
		[NullableContext(1)]
		object IConvertible.ToType(Type conversionType, [Nullable(2)] IFormatProvider provider)
		{
			return base.ToObject(conversionType);
		}

		// Token: 0x04000759 RID: 1881
		private JTokenType _valueType;

		// Token: 0x0400075A RID: 1882
		private object _value;

		// Token: 0x0200018D RID: 397
		[NullableContext(1)]
		[Nullable(new byte[] { 0, 1 })]
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x06000DF0 RID: 3568 RVA: 0x0003CE8C File Offset: 0x0003B08C
			public override bool TryConvert(JValue instance, ConvertBinder binder, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x06000DF1 RID: 3569 RVA: 0x0003CEFC File Offset: 0x0003B0FC
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out object result)
			{
				JValue jvalue = arg as JValue;
				object obj = ((jvalue != null) ? jvalue.Value : arg);
				ExpressionType operation = binder.Operation;
				if (operation <= ExpressionType.NotEqual)
				{
					if (operation <= ExpressionType.LessThanOrEqual)
					{
						if (operation != ExpressionType.Add)
						{
							switch (operation)
							{
							case ExpressionType.Divide:
								break;
							case ExpressionType.Equal:
								result = JValue.Compare(instance.Type, instance.Value, obj) == 0;
								return true;
							case ExpressionType.ExclusiveOr:
							case ExpressionType.Invoke:
							case ExpressionType.Lambda:
							case ExpressionType.LeftShift:
								goto IL_018D;
							case ExpressionType.GreaterThan:
								result = JValue.Compare(instance.Type, instance.Value, obj) > 0;
								return true;
							case ExpressionType.GreaterThanOrEqual:
								result = JValue.Compare(instance.Type, instance.Value, obj) >= 0;
								return true;
							case ExpressionType.LessThan:
								result = JValue.Compare(instance.Type, instance.Value, obj) < 0;
								return true;
							case ExpressionType.LessThanOrEqual:
								result = JValue.Compare(instance.Type, instance.Value, obj) <= 0;
								return true;
							default:
								goto IL_018D;
							}
						}
					}
					else if (operation != ExpressionType.Multiply)
					{
						if (operation != ExpressionType.NotEqual)
						{
							goto IL_018D;
						}
						result = JValue.Compare(instance.Type, instance.Value, obj) != 0;
						return true;
					}
				}
				else if (operation <= ExpressionType.AddAssign)
				{
					if (operation != ExpressionType.Subtract && operation != ExpressionType.AddAssign)
					{
						goto IL_018D;
					}
				}
				else if (operation != ExpressionType.DivideAssign && operation != ExpressionType.MultiplyAssign && operation != ExpressionType.SubtractAssign)
				{
					goto IL_018D;
				}
				if (JValue.Operation(binder.Operation, instance.Value, obj, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_018D:
				result = null;
				return false;
			}
		}
	}
}
