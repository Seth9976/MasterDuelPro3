using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x0200007C RID: 124
	internal class Vector4PropertyBag : ContainerPropertyBag<Vector4>
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000A618 File Offset: 0x00008818
		public Vector4PropertyBag()
		{
			base.AddProperty<float>(new Vector4PropertyBag.XProperty());
			base.AddProperty<float>(new Vector4PropertyBag.YProperty());
			base.AddProperty<float>(new Vector4PropertyBag.ZProperty());
			base.AddProperty<float>(new Vector4PropertyBag.WProperty());
		}

		// Token: 0x0200007D RID: 125
		private class XProperty : Property<Vector4, float>
		{
			// Token: 0x17000063 RID: 99
			// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x0600029D RID: 669 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000A652 File Offset: 0x00008852
			public override float GetValue(ref Vector4 container)
			{
				return container.x;
			}

			// Token: 0x0600029F RID: 671 RVA: 0x0000A65A File Offset: 0x0000885A
			public override void SetValue(ref Vector4 container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x0200007E RID: 126
		private class YProperty : Property<Vector4, float>
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x0000A66C File Offset: 0x0000886C
			public override float GetValue(ref Vector4 container)
			{
				return container.y;
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x0000A674 File Offset: 0x00008874
			public override void SetValue(ref Vector4 container, float value)
			{
				container.y = value;
			}
		}

		// Token: 0x0200007F RID: 127
		private class ZProperty : Property<Vector4, float>
		{
			// Token: 0x17000067 RID: 103
			// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A600 File Offset: 0x00008800
			public override string Name
			{
				get
				{
					return "z";
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x0000A67D File Offset: 0x0000887D
			public override float GetValue(ref Vector4 container)
			{
				return container.z;
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x0000A685 File Offset: 0x00008885
			public override void SetValue(ref Vector4 container, float value)
			{
				container.z = value;
			}
		}

		// Token: 0x02000080 RID: 128
		private class WProperty : Property<Vector4, float>
		{
			// Token: 0x17000069 RID: 105
			// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A68E File Offset: 0x0000888E
			public override string Name
			{
				get
				{
					return "w";
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x060002AC RID: 684 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002AD RID: 685 RVA: 0x0000A695 File Offset: 0x00008895
			public override float GetValue(ref Vector4 container)
			{
				return container.w;
			}

			// Token: 0x060002AE RID: 686 RVA: 0x0000A69D File Offset: 0x0000889D
			public override void SetValue(ref Vector4 container, float value)
			{
				container.w = value;
			}
		}
	}
}
