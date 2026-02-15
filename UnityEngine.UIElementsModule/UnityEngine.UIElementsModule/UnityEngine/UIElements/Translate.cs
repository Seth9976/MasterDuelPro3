using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003EB RID: 1003
	public struct Translate : IEquatable<Translate>
	{
		// Token: 0x06001DCC RID: 7628 RVA: 0x0006D16C File Offset: 0x0006B36C
		public Translate(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
			this.m_isNone = false;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0006D18B File Offset: 0x0006B38B
		internal Translate(Vector3 v)
		{
			this = new Translate(v.x, v.y, v.z);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0006D1B4 File Offset: 0x0006B3B4
		public static Translate None()
		{
			return new Translate
			{
				m_isNone = true
			};
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0006D1D7 File Offset: 0x0006B3D7
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0006D1DF File Offset: 0x0006B3DF
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

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0006D1E8 File Offset: 0x0006B3E8
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0006D1F0 File Offset: 0x0006B3F0
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

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0006D1F9 File Offset: 0x0006B3F9
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0006D201 File Offset: 0x0006B401
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

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0006D20C File Offset: 0x0006B40C
		public static bool operator ==(Translate lhs, Translate rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z && lhs.m_isNone == rhs.m_isNone;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0006D264 File Offset: 0x0006B464
		public static bool operator !=(Translate lhs, Translate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0006D280 File Offset: 0x0006B480
		public bool Equals(Translate other)
		{
			return other == this;
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0006D2A0 File Offset: 0x0006B4A0
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Translate)
			{
				Translate other = (Translate)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0006D2CC File Offset: 0x0006B4CC
		public override int GetHashCode()
		{
			return (this.m_X.GetHashCode() * 793) ^ (this.m_Y.GetHashCode() * 791) ^ (this.m_Z.GetHashCode() * 571);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0006D320 File Offset: 0x0006B520
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

		// Token: 0x04000CC5 RID: 3269
		private Length m_X;

		// Token: 0x04000CC6 RID: 3270
		private Length m_Y;

		// Token: 0x04000CC7 RID: 3271
		private float m_Z;

		// Token: 0x04000CC8 RID: 3272
		private bool m_isNone;

		// Token: 0x020003EC RID: 1004
		internal class PropertyBag : ContainerPropertyBag<Translate>
		{
			// Token: 0x06001DDB RID: 7643 RVA: 0x0006D38F File Offset: 0x0006B58F
			public PropertyBag()
			{
				base.AddProperty<Length>(new Translate.PropertyBag.XProperty());
				base.AddProperty<Length>(new Translate.PropertyBag.YProperty());
				base.AddProperty<float>(new Translate.PropertyBag.ZProperty());
			}

			// Token: 0x020003ED RID: 1005
			private class XProperty : Property<Translate, Length>
			{
				// Token: 0x17000841 RID: 2113
				// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0006D3BD File Offset: 0x0006B5BD
				public override string Name { get; } = "x";

				// Token: 0x17000842 RID: 2114
				// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0006D3C5 File Offset: 0x0006B5C5
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DDE RID: 7646 RVA: 0x0006D3CD File Offset: 0x0006B5CD
				public override Length GetValue(ref Translate container)
				{
					return container.x;
				}

				// Token: 0x06001DDF RID: 7647 RVA: 0x0006D3D5 File Offset: 0x0006B5D5
				public override void SetValue(ref Translate container, Length value)
				{
					container.x = value;
				}
			}

			// Token: 0x020003EE RID: 1006
			private class YProperty : Property<Translate, Length>
			{
				// Token: 0x17000843 RID: 2115
				// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0006D3FA File Offset: 0x0006B5FA
				public override string Name { get; } = "y";

				// Token: 0x17000844 RID: 2116
				// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0006D402 File Offset: 0x0006B602
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DE3 RID: 7651 RVA: 0x0006D40A File Offset: 0x0006B60A
				public override Length GetValue(ref Translate container)
				{
					return container.y;
				}

				// Token: 0x06001DE4 RID: 7652 RVA: 0x0006D412 File Offset: 0x0006B612
				public override void SetValue(ref Translate container, Length value)
				{
					container.y = value;
				}
			}

			// Token: 0x020003EF RID: 1007
			private class ZProperty : Property<Translate, float>
			{
				// Token: 0x17000845 RID: 2117
				// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0006D437 File Offset: 0x0006B637
				public override string Name { get; } = "z";

				// Token: 0x17000846 RID: 2118
				// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0006D43F File Offset: 0x0006B63F
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001DE8 RID: 7656 RVA: 0x0006D447 File Offset: 0x0006B647
				public override float GetValue(ref Translate container)
				{
					return container.z;
				}

				// Token: 0x06001DE9 RID: 7657 RVA: 0x0006D44F File Offset: 0x0006B64F
				public override void SetValue(ref Translate container, float value)
				{
					container.z = value;
				}
			}
		}
	}
}
