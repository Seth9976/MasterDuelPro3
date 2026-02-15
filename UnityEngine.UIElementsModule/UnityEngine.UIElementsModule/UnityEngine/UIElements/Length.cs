using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003BB RID: 955
	[Serializable]
	public struct Length : IEquatable<Length>
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x0006A014 File Offset: 0x00068214
		public static Length Percent(float value)
		{
			return new Length(value, LengthUnit.Percent);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0006A030 File Offset: 0x00068230
		public static Length Auto()
		{
			return new Length(0f, Length.Unit.Auto);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0006A050 File Offset: 0x00068250
		public static Length None()
		{
			return new Length(0f, Length.Unit.None);
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0006A06D File Offset: 0x0006826D
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0006A075 File Offset: 0x00068275
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, -8388608f, 8388608f);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0006A08D File Offset: 0x0006828D
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x0006A095 File Offset: 0x00068295
		public LengthUnit unit
		{
			get
			{
				return (LengthUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Length.Unit)value;
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0006A09E File Offset: 0x0006829E
		public bool IsAuto()
		{
			return this.m_Unit == Length.Unit.Auto;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0006A0A9 File Offset: 0x000682A9
		public bool IsNone()
		{
			return this.m_Unit == Length.Unit.None;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0006A0B4 File Offset: 0x000682B4
		public Length(float value)
		{
			this = new Length(value, Length.Unit.Pixel);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0006A0C0 File Offset: 0x000682C0
		public Length(float value, LengthUnit unit)
		{
			this = new Length(value, (Length.Unit)unit);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0006A0CC File Offset: 0x000682CC
		private Length(float value, Length.Unit unit)
		{
			this = default(Length);
			this.value = value;
			this.m_Unit = unit;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0006A0E8 File Offset: 0x000682E8
		public static implicit operator Length(float value)
		{
			return new Length(value, LengthUnit.Pixel);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0006A104 File Offset: 0x00068304
		public static bool operator ==(Length lhs, Length rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006A138 File Offset: 0x00068338
		public static bool operator !=(Length lhs, Length rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0006A154 File Offset: 0x00068354
		public bool Equals(Length other)
		{
			return other == this;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006A174 File Offset: 0x00068374
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Length)
			{
				Length other = (Length)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0006A1A0 File Offset: 0x000683A0
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0006A1CC File Offset: 0x000683CC
		public override string ToString()
		{
			string valueStr = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string unitStr = string.Empty;
			switch (this.m_Unit)
			{
			case Length.Unit.Pixel:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					unitStr = "px";
				}
				break;
			}
			case Length.Unit.Percent:
				unitStr = "%";
				break;
			case Length.Unit.Auto:
				valueStr = "auto";
				break;
			case Length.Unit.None:
				valueStr = "none";
				break;
			}
			return valueStr + unitStr;
		}

		// Token: 0x04000C69 RID: 3177
		internal const float k_MaxValue = 8388608f;

		// Token: 0x04000C6A RID: 3178
		[SerializeField]
		private float m_Value;

		// Token: 0x04000C6B RID: 3179
		[SerializeField]
		private Length.Unit m_Unit;

		// Token: 0x020003BC RID: 956
		private enum Unit
		{
			// Token: 0x04000C6D RID: 3181
			Pixel,
			// Token: 0x04000C6E RID: 3182
			Percent,
			// Token: 0x04000C6F RID: 3183
			Auto,
			// Token: 0x04000C70 RID: 3184
			None
		}

		// Token: 0x020003BD RID: 957
		internal class PropertyBag : ContainerPropertyBag<Length>
		{
			// Token: 0x06001C3D RID: 7229 RVA: 0x0006A262 File Offset: 0x00068462
			public PropertyBag()
			{
				base.AddProperty<float>(new Length.PropertyBag.ValueProperty());
				base.AddProperty<LengthUnit>(new Length.PropertyBag.UnitProperty());
			}

			// Token: 0x020003BE RID: 958
			private class ValueProperty : Property<Length, float>
			{
				// Token: 0x170007F7 RID: 2039
				// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0006A284 File Offset: 0x00068484
				public override string Name { get; } = "value";

				// Token: 0x170007F8 RID: 2040
				// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0006A28C File Offset: 0x0006848C
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001C40 RID: 7232 RVA: 0x0006A294 File Offset: 0x00068494
				public override float GetValue(ref Length container)
				{
					return container.value;
				}

				// Token: 0x06001C41 RID: 7233 RVA: 0x0006A29C File Offset: 0x0006849C
				public override void SetValue(ref Length container, float value)
				{
					container.value = value;
				}
			}

			// Token: 0x020003BF RID: 959
			private class UnitProperty : Property<Length, LengthUnit>
			{
				// Token: 0x170007F9 RID: 2041
				// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0006A2C1 File Offset: 0x000684C1
				public override string Name { get; } = "unit";

				// Token: 0x170007FA RID: 2042
				// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0006A2C9 File Offset: 0x000684C9
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001C45 RID: 7237 RVA: 0x0006A2D1 File Offset: 0x000684D1
				public override LengthUnit GetValue(ref Length container)
				{
					return container.unit;
				}

				// Token: 0x06001C46 RID: 7238 RVA: 0x0006A2D9 File Offset: 0x000684D9
				public override void SetValue(ref Length container, LengthUnit value)
				{
					container.unit = value;
				}
			}
		}
	}
}
