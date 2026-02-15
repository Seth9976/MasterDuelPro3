using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003E2 RID: 994
	public struct TimeValue : IEquatable<TimeValue>
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0006CC4C File Offset: 0x0006AE4C
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0006CC54 File Offset: 0x0006AE54
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

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0006CC5D File Offset: 0x0006AE5D
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0006CC65 File Offset: 0x0006AE65
		public TimeUnit unit
		{
			get
			{
				return this.m_Unit;
			}
			set
			{
				this.m_Unit = value;
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0006CC6E File Offset: 0x0006AE6E
		public TimeValue(float value)
		{
			this = new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0006CC7A File Offset: 0x0006AE7A
		public TimeValue(float value, TimeUnit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0006CC8C File Offset: 0x0006AE8C
		public static implicit operator TimeValue(float value)
		{
			return new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0006CCA8 File Offset: 0x0006AEA8
		public static bool operator ==(TimeValue lhs, TimeValue rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0006CCDC File Offset: 0x0006AEDC
		public static bool operator !=(TimeValue lhs, TimeValue rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0006CCF8 File Offset: 0x0006AEF8
		public bool Equals(TimeValue other)
		{
			return other == this;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0006CD18 File Offset: 0x0006AF18
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is TimeValue)
			{
				TimeValue other = (TimeValue)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0006CD44 File Offset: 0x0006AF44
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0006CD70 File Offset: 0x0006AF70
		public override string ToString()
		{
			string valueStr = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string unitStr = string.Empty;
			TimeUnit unit = this.unit;
			TimeUnit timeUnit = unit;
			if (timeUnit != TimeUnit.Second)
			{
				if (timeUnit == TimeUnit.Millisecond)
				{
					unitStr = "ms";
				}
			}
			else
			{
				unitStr = "s";
			}
			return valueStr + unitStr;
		}

		// Token: 0x04000CB6 RID: 3254
		private float m_Value;

		// Token: 0x04000CB7 RID: 3255
		private TimeUnit m_Unit;

		// Token: 0x020003E3 RID: 995
		internal class PropertyBag : ContainerPropertyBag<TimeValue>
		{
			// Token: 0x06001DA2 RID: 7586 RVA: 0x0006CDD0 File Offset: 0x0006AFD0
			public PropertyBag()
			{
				base.AddProperty<float>(new TimeValue.PropertyBag.ValueProperty());
				base.AddProperty<TimeUnit>(new TimeValue.PropertyBag.UnitProperty());
			}

			// Token: 0x020003E4 RID: 996
			private class ValueProperty : Property<TimeValue, float>
			{
				// Token: 0x17000831 RID: 2097
				// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0006CDF2 File Offset: 0x0006AFF2
				public override string Name { get; } = "value";

				// Token: 0x17000832 RID: 2098
				// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0006CDFA File Offset: 0x0006AFFA
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DA5 RID: 7589 RVA: 0x0006CE02 File Offset: 0x0006B002
				public override float GetValue(ref TimeValue container)
				{
					return container.value;
				}

				// Token: 0x06001DA6 RID: 7590 RVA: 0x0006CE0A File Offset: 0x0006B00A
				public override void SetValue(ref TimeValue container, float value)
				{
					container.value = value;
				}
			}

			// Token: 0x020003E5 RID: 997
			private class UnitProperty : Property<TimeValue, TimeUnit>
			{
				// Token: 0x17000833 RID: 2099
				// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0006CE2F File Offset: 0x0006B02F
				public override string Name { get; } = "unit";

				// Token: 0x17000834 RID: 2100
				// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0006CE37 File Offset: 0x0006B037
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DAA RID: 7594 RVA: 0x0006CE3F File Offset: 0x0006B03F
				public override TimeUnit GetValue(ref TimeValue container)
				{
					return container.unit;
				}

				// Token: 0x06001DAB RID: 7595 RVA: 0x0006CE47 File Offset: 0x0006B047
				public override void SetValue(ref TimeValue container, TimeUnit value)
				{
					container.unit = value;
				}
			}
		}
	}
}
