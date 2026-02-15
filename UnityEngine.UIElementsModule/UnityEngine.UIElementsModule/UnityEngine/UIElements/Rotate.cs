using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003C0 RID: 960
	public struct Rotate : IEquatable<Rotate>
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x0006A2FE File Offset: 0x000684FE
		public Rotate(Angle angle)
		{
			this.m_Angle = angle;
			this.m_Axis = Vector3.forward;
			this.m_IsNone = false;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0006A31C File Offset: 0x0006851C
		internal Rotate(Quaternion quaternion)
		{
			float angle;
			Vector3 axis;
			quaternion.ToAngleAxis(out angle, out axis);
			this.m_Angle = angle;
			this.m_Axis = axis;
			this.m_IsNone = false;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0006A350 File Offset: 0x00068550
		internal static Rotate Initial()
		{
			return new Rotate(0f);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0006A374 File Offset: 0x00068574
		public static Rotate None()
		{
			Rotate none = Rotate.Initial();
			none.m_IsNone = true;
			return none;
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0006A395 File Offset: 0x00068595
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x0006A39D File Offset: 0x0006859D
		public Angle angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0006A3A6 File Offset: 0x000685A6
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x0006A3AE File Offset: 0x000685AE
		internal Vector3 axis
		{
			get
			{
				return this.m_Axis;
			}
			set
			{
				this.m_Axis = value;
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0006A3B8 File Offset: 0x000685B8
		public static bool operator ==(Rotate lhs, Rotate rhs)
		{
			return lhs.m_Angle == rhs.m_Angle && lhs.m_Axis == rhs.m_Axis && lhs.m_IsNone == rhs.m_IsNone;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0006A404 File Offset: 0x00068604
		public static bool operator !=(Rotate lhs, Rotate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0006A420 File Offset: 0x00068620
		public bool Equals(Rotate other)
		{
			return other == this;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0006A440 File Offset: 0x00068640
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Rotate)
			{
				Rotate other = (Rotate)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0006A46C File Offset: 0x0006866C
		public override int GetHashCode()
		{
			return (this.m_Angle.GetHashCode() * 793) ^ (this.m_Axis.GetHashCode() * 791) ^ (this.m_IsNone.GetHashCode() * 197);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0006A4C0 File Offset: 0x000686C0
		public override string ToString()
		{
			return this.m_Angle.ToString() + " " + this.m_Axis.ToString();
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0006A500 File Offset: 0x00068700
		internal Quaternion ToQuaternion()
		{
			return Quaternion.AngleAxis(this.m_Angle.ToDegrees(), this.m_Axis);
		}

		// Token: 0x04000C75 RID: 3189
		private Angle m_Angle;

		// Token: 0x04000C76 RID: 3190
		private Vector3 m_Axis;

		// Token: 0x04000C77 RID: 3191
		private bool m_IsNone;

		// Token: 0x020003C1 RID: 961
		internal class PropertyBag : ContainerPropertyBag<Rotate>
		{
			// Token: 0x06001C57 RID: 7255 RVA: 0x0006A528 File Offset: 0x00068728
			public PropertyBag()
			{
				base.AddProperty<Angle>(new Rotate.PropertyBag.AngleProperty());
				base.AddProperty<Vector3>(new Rotate.PropertyBag.AxisProperty());
			}

			// Token: 0x020003C2 RID: 962
			private class AngleProperty : Property<Rotate, Angle>
			{
				// Token: 0x170007FD RID: 2045
				// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0006A54A File Offset: 0x0006874A
				public override string Name { get; } = "angle";

				// Token: 0x170007FE RID: 2046
				// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0006A552 File Offset: 0x00068752
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001C5A RID: 7258 RVA: 0x0006A55A File Offset: 0x0006875A
				public override Angle GetValue(ref Rotate container)
				{
					return container.angle;
				}

				// Token: 0x06001C5B RID: 7259 RVA: 0x0006A562 File Offset: 0x00068762
				public override void SetValue(ref Rotate container, Angle value)
				{
					container.angle = value;
				}
			}

			// Token: 0x020003C3 RID: 963
			private class AxisProperty : Property<Rotate, Vector3>
			{
				// Token: 0x170007FF RID: 2047
				// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0006A587 File Offset: 0x00068787
				public override string Name { get; } = "axis";

				// Token: 0x17000800 RID: 2048
				// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0006A58F File Offset: 0x0006878F
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001C5F RID: 7263 RVA: 0x0006A597 File Offset: 0x00068797
				public override Vector3 GetValue(ref Rotate container)
				{
					return container.axis;
				}

				// Token: 0x06001C60 RID: 7264 RVA: 0x0006A59F File Offset: 0x0006879F
				public override void SetValue(ref Rotate container, Vector3 value)
				{
					container.axis = value;
				}
			}
		}
	}
}
