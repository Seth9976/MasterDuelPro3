using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200059E RID: 1438
	internal struct LayoutValue
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x0009B4AC File Offset: 0x000996AC
		public LayoutUnit Unit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x0009B4B4 File Offset: 0x000996B4
		public float Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0009B4BC File Offset: 0x000996BC
		public static LayoutValue Point(float value)
		{
			return new LayoutValue
			{
				value = value,
				unit = (float.IsNaN(value) ? LayoutUnit.Undefined : LayoutUnit.Point)
			};
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0009B4F4 File Offset: 0x000996F4
		public bool Equals(LayoutValue other)
		{
			return this.Unit == other.Unit && (this.Value.Equals(other.Value) || this.Unit == LayoutUnit.Undefined);
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0009B53C File Offset: 0x0009973C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3;
				if (obj is LayoutValue)
				{
					LayoutValue yogaValue = (LayoutValue)obj;
					flag3 = this.Equals(yogaValue);
				}
				else
				{
					flag3 = false;
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x0009B574 File Offset: 0x00099774
		public override int GetHashCode()
		{
			return (this.Value.GetHashCode() * 397) ^ (int)this.Unit;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0009B5A4 File Offset: 0x000997A4
		public static LayoutValue Undefined()
		{
			return new LayoutValue
			{
				value = float.NaN,
				unit = LayoutUnit.Undefined
			};
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0009B5D4 File Offset: 0x000997D4
		public static LayoutValue Auto()
		{
			return new LayoutValue
			{
				value = float.NaN,
				unit = LayoutUnit.Auto
			};
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x0009B604 File Offset: 0x00099804
		public static LayoutValue Percent(float value)
		{
			return new LayoutValue
			{
				value = value,
				unit = (float.IsNaN(value) ? LayoutUnit.Undefined : LayoutUnit.Percent)
			};
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0009B63C File Offset: 0x0009983C
		public static implicit operator LayoutValue(float value)
		{
			return LayoutValue.Point(value);
		}

		// Token: 0x04001425 RID: 5157
		private float value;

		// Token: 0x04001426 RID: 5158
		private LayoutUnit unit;
	}
}
