using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BA RID: 698
	public struct Angle : IEquatable<Angle>
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x0004E3F4 File Offset: 0x0004C5F4
		internal static Angle None()
		{
			return new Angle(0f, Angle.Unit.None);
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004E411 File Offset: 0x0004C611
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0004E419 File Offset: 0x0004C619
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004E422 File Offset: 0x0004C622
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0004E42A File Offset: 0x0004C62A
		public AngleUnit unit
		{
			get
			{
				return (AngleUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Angle.Unit)value;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004E433 File Offset: 0x0004C633
		public Angle(float value, AngleUnit unit)
		{
			this = new Angle(value, (Angle.Unit)unit);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004E43F File Offset: 0x0004C63F
		private Angle(float value, Angle.Unit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0004E450 File Offset: 0x0004C650
		public float ToDegrees()
		{
			float num;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				num = this.m_Value;
				break;
			case Angle.Unit.Gradian:
				num = this.m_Value * 360f / 400f;
				break;
			case Angle.Unit.Radian:
				num = this.m_Value * 180f / 3.1415927f;
				break;
			case Angle.Unit.Turn:
				num = this.m_Value * 360f;
				break;
			case Angle.Unit.None:
				num = 0f;
				break;
			default:
				num = 0f;
				break;
			}
			return num;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004E4D8 File Offset: 0x0004C6D8
		public static implicit operator Angle(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
		public static bool operator ==(Angle lhs, Angle rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004E528 File Offset: 0x0004C728
		public bool Equals(Angle other)
		{
			return other == this;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0004E548 File Offset: 0x0004C748
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Angle)
			{
				Angle other = (Angle)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0004E574 File Offset: 0x0004C774
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0004E5A0 File Offset: 0x0004C7A0
		public override string ToString()
		{
			string valueStr = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string unitStr = string.Empty;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					unitStr = "deg";
				}
				break;
			}
			case Angle.Unit.Gradian:
				unitStr = "grad";
				break;
			case Angle.Unit.Radian:
				unitStr = "rad";
				break;
			case Angle.Unit.Turn:
				unitStr = "turn";
				break;
			case Angle.Unit.None:
				valueStr = "";
				break;
			}
			return valueStr + unitStr;
		}

		// Token: 0x04000AEB RID: 2795
		private float m_Value;

		// Token: 0x04000AEC RID: 2796
		private Angle.Unit m_Unit;

		// Token: 0x020002BB RID: 699
		private enum Unit
		{
			// Token: 0x04000AEE RID: 2798
			Degree,
			// Token: 0x04000AEF RID: 2799
			Gradian,
			// Token: 0x04000AF0 RID: 2800
			Radian,
			// Token: 0x04000AF1 RID: 2801
			Turn,
			// Token: 0x04000AF2 RID: 2802
			None
		}

		// Token: 0x020002BC RID: 700
		internal class PropertyBag : ContainerPropertyBag<Angle>
		{
			// Token: 0x060012E6 RID: 4838 RVA: 0x0004E642 File Offset: 0x0004C842
			public PropertyBag()
			{
				base.AddProperty<float>(new Angle.PropertyBag.ValueProperty());
				base.AddProperty<AngleUnit>(new Angle.PropertyBag.UnitProperty());
			}

			// Token: 0x020002BD RID: 701
			private class ValueProperty : Property<Angle, float>
			{
				// Token: 0x170003A1 RID: 929
				// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0004E664 File Offset: 0x0004C864
				public override string Name { get; } = "value";

				// Token: 0x170003A2 RID: 930
				// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004E66C File Offset: 0x0004C86C
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060012E9 RID: 4841 RVA: 0x0004E674 File Offset: 0x0004C874
				public override float GetValue(ref Angle container)
				{
					return container.value;
				}

				// Token: 0x060012EA RID: 4842 RVA: 0x0004E67C File Offset: 0x0004C87C
				public override void SetValue(ref Angle container, float value)
				{
					container.value = value;
				}
			}

			// Token: 0x020002BE RID: 702
			private class UnitProperty : Property<Angle, AngleUnit>
			{
				// Token: 0x170003A3 RID: 931
				// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004E6A1 File Offset: 0x0004C8A1
				public override string Name { get; } = "unit";

				// Token: 0x170003A4 RID: 932
				// (get) Token: 0x060012ED RID: 4845 RVA: 0x0004E6A9 File Offset: 0x0004C8A9
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060012EE RID: 4846 RVA: 0x0004E6B1 File Offset: 0x0004C8B1
				public override AngleUnit GetValue(ref Angle container)
				{
					return container.unit;
				}

				// Token: 0x060012EF RID: 4847 RVA: 0x0004E6B9 File Offset: 0x0004C8B9
				public override void SetValue(ref Angle container, AngleUnit value)
				{
					container.unit = value;
				}
			}
		}
	}
}
