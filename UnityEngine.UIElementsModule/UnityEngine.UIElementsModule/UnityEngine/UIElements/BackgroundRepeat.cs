using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000F RID: 15
	public struct BackgroundRepeat : IEquatable<BackgroundRepeat>
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002D6D File Offset: 0x00000F6D
		public BackgroundRepeat(Repeat repeatX, Repeat repeatY)
		{
			this.x = repeatX;
			this.y = repeatY;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D80 File Offset: 0x00000F80
		internal static BackgroundRepeat Initial()
		{
			return BackgroundPropertyHelper.ConvertScaleModeToBackgroundRepeat(ScaleMode.StretchToFill);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D98 File Offset: 0x00000F98
		public override bool Equals(object obj)
		{
			return obj is BackgroundRepeat && this.Equals((BackgroundRepeat)obj);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public bool Equals(BackgroundRepeat other)
		{
			return other.x == this.x && other.y == this.y;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public override int GetHashCode()
		{
			int hashCode = 1500536833;
			hashCode = hashCode * -1521134295 + this.x.GetHashCode();
			return hashCode * -1521134295 + this.y.GetHashCode();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E48 File Offset: 0x00001048
		public static bool operator ==(BackgroundRepeat style1, BackgroundRepeat style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E64 File Offset: 0x00001064
		public static bool operator !=(BackgroundRepeat style1, BackgroundRepeat style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E80 File Offset: 0x00001080
		public override string ToString()
		{
			return string.Format("(x:{0}, y:{1})", this.x, this.y);
		}

		// Token: 0x0400001E RID: 30
		public Repeat x;

		// Token: 0x0400001F RID: 31
		public Repeat y;

		// Token: 0x02000010 RID: 16
		internal class PropertyBag : ContainerPropertyBag<BackgroundRepeat>
		{
			// Token: 0x0600004B RID: 75 RVA: 0x00002EB2 File Offset: 0x000010B2
			public PropertyBag()
			{
				base.AddProperty<Repeat>(new BackgroundRepeat.PropertyBag.XProperty());
				base.AddProperty<Repeat>(new BackgroundRepeat.PropertyBag.YProperty());
			}

			// Token: 0x02000011 RID: 17
			private class XProperty : Property<BackgroundRepeat, Repeat>
			{
				// Token: 0x1700000C RID: 12
				// (get) Token: 0x0600004C RID: 76 RVA: 0x00002ED4 File Offset: 0x000010D4
				public override string Name { get; } = "x";

				// Token: 0x1700000D RID: 13
				// (get) Token: 0x0600004D RID: 77 RVA: 0x00002EDC File Offset: 0x000010DC
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600004E RID: 78 RVA: 0x00002EE4 File Offset: 0x000010E4
				public override Repeat GetValue(ref BackgroundRepeat container)
				{
					return container.x;
				}

				// Token: 0x0600004F RID: 79 RVA: 0x00002EEC File Offset: 0x000010EC
				public override void SetValue(ref BackgroundRepeat container, Repeat value)
				{
					container.x = value;
				}
			}

			// Token: 0x02000012 RID: 18
			private class YProperty : Property<BackgroundRepeat, Repeat>
			{
				// Token: 0x1700000E RID: 14
				// (get) Token: 0x06000051 RID: 81 RVA: 0x00002F10 File Offset: 0x00001110
				public override string Name { get; } = "y";

				// Token: 0x1700000F RID: 15
				// (get) Token: 0x06000052 RID: 82 RVA: 0x00002F18 File Offset: 0x00001118
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000053 RID: 83 RVA: 0x00002F20 File Offset: 0x00001120
				public override Repeat GetValue(ref BackgroundRepeat container)
				{
					return container.y;
				}

				// Token: 0x06000054 RID: 84 RVA: 0x00002F28 File Offset: 0x00001128
				public override void SetValue(ref BackgroundRepeat container, Repeat value)
				{
					container.y = value;
				}
			}
		}
	}
}
