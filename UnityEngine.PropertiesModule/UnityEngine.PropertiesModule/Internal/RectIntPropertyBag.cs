using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x0200008D RID: 141
	internal class RectIntPropertyBag : ContainerPropertyBag<RectInt>
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000A7FB File Offset: 0x000089FB
		public RectIntPropertyBag()
		{
			base.AddProperty<int>(new RectIntPropertyBag.XProperty());
			base.AddProperty<int>(new RectIntPropertyBag.YProperty());
			base.AddProperty<int>(new RectIntPropertyBag.WidthProperty());
			base.AddProperty<int>(new RectIntPropertyBag.HeightProperty());
		}

		// Token: 0x0200008E RID: 142
		private class XProperty : Property<RectInt, int>
		{
			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000A835 File Offset: 0x00008A35
			public override int GetValue(ref RectInt container)
			{
				return container.x;
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000A83D File Offset: 0x00008A3D
			public override void SetValue(ref RectInt container, int value)
			{
				container.x = value;
			}
		}

		// Token: 0x0200008F RID: 143
		private class YProperty : Property<RectInt, int>
		{
			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0000A850 File Offset: 0x00008A50
			public override int GetValue(ref RectInt container)
			{
				return container.y;
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000A858 File Offset: 0x00008A58
			public override void SetValue(ref RectInt container, int value)
			{
				container.y = value;
			}
		}

		// Token: 0x02000090 RID: 144
		private class WidthProperty : Property<RectInt, int>
		{
			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060002EB RID: 747 RVA: 0x0000A7C9 File Offset: 0x000089C9
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060002EC RID: 748 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000A862 File Offset: 0x00008A62
			public override int GetValue(ref RectInt container)
			{
				return container.width;
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000A86A File Offset: 0x00008A6A
			public override void SetValue(ref RectInt container, int value)
			{
				container.width = value;
			}
		}

		// Token: 0x02000091 RID: 145
		private class HeightProperty : Property<RectInt, int>
		{
			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000A7E2 File Offset: 0x000089E2
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000A874 File Offset: 0x00008A74
			public override int GetValue(ref RectInt container)
			{
				return container.height;
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000A87C File Offset: 0x00008A7C
			public override void SetValue(ref RectInt container, int value)
			{
				container.height = value;
			}
		}
	}
}
