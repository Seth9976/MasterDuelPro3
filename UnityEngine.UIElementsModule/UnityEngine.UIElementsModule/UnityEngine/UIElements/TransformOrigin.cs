using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003E6 RID: 998
	public struct TransformOrigin : IEquatable<TransformOrigin>
	{
		// Token: 0x06001DAD RID: 7597 RVA: 0x0006CE6C File Offset: 0x0006B06C
		public TransformOrigin(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0006CE84 File Offset: 0x0006B084
		internal TransformOrigin(Vector3 vector)
		{
			this = new TransformOrigin(vector.x, vector.y, vector.z);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0006CEAC File Offset: 0x0006B0AC
		public static TransformOrigin Initial()
		{
			return new TransformOrigin(Length.Percent(50f), Length.Percent(50f), 0f);
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0006CEDC File Offset: 0x0006B0DC
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0006CEE4 File Offset: 0x0006B0E4
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0006CEED File Offset: 0x0006B0ED
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x0006CEF5 File Offset: 0x0006B0F5
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0006CEFE File Offset: 0x0006B0FE
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x0006CF06 File Offset: 0x0006B106
		public float z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0006CF10 File Offset: 0x0006B110
		public static bool operator ==(TransformOrigin lhs, TransformOrigin rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0006CF5C File Offset: 0x0006B15C
		public static bool operator !=(TransformOrigin lhs, TransformOrigin rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0006CF78 File Offset: 0x0006B178
		public bool Equals(TransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0006CF98 File Offset: 0x0006B198
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is TransformOrigin)
			{
				TransformOrigin other = (TransformOrigin)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0006CFC4 File Offset: 0x0006B1C4
		public override int GetHashCode()
		{
			return (this.m_X.GetHashCode() * 793) ^ (this.m_Y.GetHashCode() * 791) ^ (this.m_Z.GetHashCode() * 571);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0006D018 File Offset: 0x0006B218
		public override string ToString()
		{
			string zStr = this.m_Z.ToString(CultureInfo.InvariantCulture.NumberFormat);
			return string.Concat(new string[]
			{
				this.m_X.ToString(),
				" ",
				this.m_Y.ToString(),
				" ",
				zStr
			});
		}

		// Token: 0x04000CBC RID: 3260
		private Length m_X;

		// Token: 0x04000CBD RID: 3261
		private Length m_Y;

		// Token: 0x04000CBE RID: 3262
		private float m_Z;

		// Token: 0x020003E7 RID: 999
		internal class PropertyBag : ContainerPropertyBag<TransformOrigin>
		{
			// Token: 0x06001DBC RID: 7612 RVA: 0x0006D087 File Offset: 0x0006B287
			public PropertyBag()
			{
				base.AddProperty<Length>(new TransformOrigin.PropertyBag.XProperty());
				base.AddProperty<Length>(new TransformOrigin.PropertyBag.YProperty());
				base.AddProperty<float>(new TransformOrigin.PropertyBag.ZProperty());
			}

			// Token: 0x020003E8 RID: 1000
			private class XProperty : Property<TransformOrigin, Length>
			{
				// Token: 0x17000838 RID: 2104
				// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0006D0B5 File Offset: 0x0006B2B5
				public override string Name { get; } = "x";

				// Token: 0x17000839 RID: 2105
				// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0006D0BD File Offset: 0x0006B2BD
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DBF RID: 7615 RVA: 0x0006D0C5 File Offset: 0x0006B2C5
				public override Length GetValue(ref TransformOrigin container)
				{
					return container.x;
				}

				// Token: 0x06001DC0 RID: 7616 RVA: 0x0006D0CD File Offset: 0x0006B2CD
				public override void SetValue(ref TransformOrigin container, Length value)
				{
					container.x = value;
				}
			}

			// Token: 0x020003E9 RID: 1001
			private class YProperty : Property<TransformOrigin, Length>
			{
				// Token: 0x1700083A RID: 2106
				// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0006D0F2 File Offset: 0x0006B2F2
				public override string Name { get; } = "y";

				// Token: 0x1700083B RID: 2107
				// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0006D0FA File Offset: 0x0006B2FA
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DC4 RID: 7620 RVA: 0x0006D102 File Offset: 0x0006B302
				public override Length GetValue(ref TransformOrigin container)
				{
					return container.y;
				}

				// Token: 0x06001DC5 RID: 7621 RVA: 0x0006D10A File Offset: 0x0006B30A
				public override void SetValue(ref TransformOrigin container, Length value)
				{
					container.y = value;
				}
			}

			// Token: 0x020003EA RID: 1002
			private class ZProperty : Property<TransformOrigin, float>
			{
				// Token: 0x1700083C RID: 2108
				// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0006D12F File Offset: 0x0006B32F
				public override string Name { get; } = "z";

				// Token: 0x1700083D RID: 2109
				// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0006D137 File Offset: 0x0006B337
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DC9 RID: 7625 RVA: 0x0006D13F File Offset: 0x0006B33F
				public override float GetValue(ref TransformOrigin container)
				{
					return container.z;
				}

				// Token: 0x06001DCA RID: 7626 RVA: 0x0006D147 File Offset: 0x0006B347
				public override void SetValue(ref TransformOrigin container, float value)
				{
					container.z = value;
				}
			}
		}
	}
}
