using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000088 RID: 136
	internal class RectPropertyBag : ContainerPropertyBag<Rect>
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000A762 File Offset: 0x00008962
		public RectPropertyBag()
		{
			base.AddProperty<float>(new RectPropertyBag.XProperty());
			base.AddProperty<float>(new RectPropertyBag.YProperty());
			base.AddProperty<float>(new RectPropertyBag.WidthProperty());
			base.AddProperty<float>(new RectPropertyBag.HeightProperty());
		}

		// Token: 0x02000089 RID: 137
		private class XProperty : Property<Rect, float>
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060002CD RID: 717 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000A79C File Offset: 0x0000899C
			public override float GetValue(ref Rect container)
			{
				return container.x;
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0000A7A4 File Offset: 0x000089A4
			public override void SetValue(ref Rect container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x0200008A RID: 138
		private class YProperty : Property<Rect, float>
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000A7B7 File Offset: 0x000089B7
			public override float GetValue(ref Rect container)
			{
				return container.y;
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000A7BF File Offset: 0x000089BF
			public override void SetValue(ref Rect container, float value)
			{
				container.y = value;
			}
		}

		// Token: 0x0200008B RID: 139
		private class WidthProperty : Property<Rect, float>
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A7C9 File Offset: 0x000089C9
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000A7D0 File Offset: 0x000089D0
			public override float GetValue(ref Rect container)
			{
				return container.width;
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x0000A7D8 File Offset: 0x000089D8
			public override void SetValue(ref Rect container, float value)
			{
				container.width = value;
			}
		}

		// Token: 0x0200008C RID: 140
		private class HeightProperty : Property<Rect, float>
		{
			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060002DB RID: 731 RVA: 0x0000A7E2 File Offset: 0x000089E2
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060002DC RID: 732 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000A7E9 File Offset: 0x000089E9
			public override float GetValue(ref Rect container)
			{
				return container.height;
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000A7F1 File Offset: 0x000089F1
			public override void SetValue(ref Rect container, float value)
			{
				container.height = value;
			}
		}
	}
}
