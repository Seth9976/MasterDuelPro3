using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020003C4 RID: 964
	public struct Scale : IEquatable<Scale>
	{
		// Token: 0x06001C62 RID: 7266 RVA: 0x0006A5C4 File Offset: 0x000687C4
		public Scale(Vector3 scale)
		{
			this.m_Scale = scale;
			this.m_IsNone = false;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0006A5D8 File Offset: 0x000687D8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static Scale Initial()
		{
			return new Scale(Vector3.one);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0006A5F4 File Offset: 0x000687F4
		public static Scale None()
		{
			Scale none = Scale.Initial();
			none.m_IsNone = true;
			return none;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0006A615 File Offset: 0x00068815
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x0006A61D File Offset: 0x0006881D
		public Vector3 value
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0006A628 File Offset: 0x00068828
		public static bool operator ==(Scale lhs, Scale rhs)
		{
			return lhs.m_Scale == rhs.m_Scale;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0006A64C File Offset: 0x0006884C
		public static bool operator !=(Scale lhs, Scale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0006A668 File Offset: 0x00068868
		public bool Equals(Scale other)
		{
			return other == this;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0006A688 File Offset: 0x00068888
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Scale)
			{
				Scale other = (Scale)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0006A6B4 File Offset: 0x000688B4
		public override int GetHashCode()
		{
			return this.m_Scale.GetHashCode() * 793;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0006A6E0 File Offset: 0x000688E0
		public override string ToString()
		{
			return this.m_Scale.ToString();
		}

		// Token: 0x04000C7C RID: 3196
		private Vector3 m_Scale;

		// Token: 0x04000C7D RID: 3197
		private bool m_IsNone;

		// Token: 0x020003C5 RID: 965
		internal class PropertyBag : ContainerPropertyBag<Scale>
		{
			// Token: 0x06001C6D RID: 7277 RVA: 0x0006A703 File Offset: 0x00068903
			public PropertyBag()
			{
				base.AddProperty<Vector3>(new Scale.PropertyBag.ValueProperty());
			}

			// Token: 0x020003C6 RID: 966
			private class ValueProperty : Property<Scale, Vector3>
			{
				// Token: 0x17000802 RID: 2050
				// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0006A719 File Offset: 0x00068919
				public override string Name { get; } = "value";

				// Token: 0x17000803 RID: 2051
				// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0006A721 File Offset: 0x00068921
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001C70 RID: 7280 RVA: 0x0006A729 File Offset: 0x00068929
				public override Vector3 GetValue(ref Scale container)
				{
					return container.value;
				}

				// Token: 0x06001C71 RID: 7281 RVA: 0x0006A731 File Offset: 0x00068931
				public override void SetValue(ref Scale container, Vector3 value)
				{
					container.value = value;
				}
			}
		}
	}
}
