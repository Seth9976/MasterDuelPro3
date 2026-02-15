using System;
using System.Globalization;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005AD RID: 1453
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct Dimension : IEquatable<Dimension>
	{
		// Token: 0x0600277F RID: 10111 RVA: 0x000A0C45 File Offset: 0x0009EE45
		public Dimension(float value, Dimension.Unit unit)
		{
			this.unit = unit;
			this.value = value;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000A0C58 File Offset: 0x0009EE58
		public Length ToLength()
		{
			LengthUnit lengthUnit = ((this.unit == Dimension.Unit.Percent) ? LengthUnit.Percent : LengthUnit.Pixel);
			return new Length(this.value, lengthUnit);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000A0C84 File Offset: 0x0009EE84
		public TimeValue ToTime()
		{
			TimeUnit timeUnit = ((this.unit == Dimension.Unit.Millisecond) ? TimeUnit.Millisecond : TimeUnit.Second);
			return new TimeValue(this.value, timeUnit);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000A0CB0 File Offset: 0x0009EEB0
		public Angle ToAngle()
		{
			Angle angle;
			switch (this.unit)
			{
			case Dimension.Unit.Degree:
				angle = new Angle(this.value, AngleUnit.Degree);
				break;
			case Dimension.Unit.Gradian:
				angle = new Angle(this.value, AngleUnit.Gradian);
				break;
			case Dimension.Unit.Radian:
				angle = new Angle(this.value, AngleUnit.Radian);
				break;
			case Dimension.Unit.Turn:
				angle = new Angle(this.value, AngleUnit.Turn);
				break;
			default:
				angle = new Angle(this.value, AngleUnit.Degree);
				break;
			}
			return angle;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000A0D30 File Offset: 0x0009EF30
		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.value == rhs.value && lhs.unit == rhs.unit;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000A0D64 File Offset: 0x0009EF64
		public bool Equals(Dimension other)
		{
			return other == this;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000A0D84 File Offset: 0x0009EF84
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Dimension);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Dimension v = (Dimension)obj;
				flag2 = v == this;
			}
			return flag2;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000A0DC0 File Offset: 0x0009EFC0
		public override int GetHashCode()
		{
			int hashCode = -799583767;
			hashCode = hashCode * -1521134295 + this.unit.GetHashCode();
			return hashCode * -1521134295 + this.value.GetHashCode();
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000A0E08 File Offset: 0x0009F008
		public override string ToString()
		{
			string unitStr = string.Empty;
			switch (this.unit)
			{
			case Dimension.Unit.Unitless:
				unitStr = string.Empty;
				break;
			case Dimension.Unit.Pixel:
				unitStr = "px";
				break;
			case Dimension.Unit.Percent:
				unitStr = "%";
				break;
			case Dimension.Unit.Second:
				unitStr = "s";
				break;
			case Dimension.Unit.Millisecond:
				unitStr = "ms";
				break;
			case Dimension.Unit.Degree:
				unitStr = "deg";
				break;
			case Dimension.Unit.Gradian:
				unitStr = "grad";
				break;
			case Dimension.Unit.Radian:
				unitStr = "rad";
				break;
			case Dimension.Unit.Turn:
				unitStr = "turn";
				break;
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + unitStr;
		}

		// Token: 0x040014DC RID: 5340
		public Dimension.Unit unit;

		// Token: 0x040014DD RID: 5341
		public float value;

		// Token: 0x020005AE RID: 1454
		public enum Unit
		{
			// Token: 0x040014DF RID: 5343
			Unitless,
			// Token: 0x040014E0 RID: 5344
			Pixel,
			// Token: 0x040014E1 RID: 5345
			Percent,
			// Token: 0x040014E2 RID: 5346
			Second,
			// Token: 0x040014E3 RID: 5347
			Millisecond,
			// Token: 0x040014E4 RID: 5348
			Degree,
			// Token: 0x040014E5 RID: 5349
			Gradian,
			// Token: 0x040014E6 RID: 5350
			Radian,
			// Token: 0x040014E7 RID: 5351
			Turn
		}
	}
}
