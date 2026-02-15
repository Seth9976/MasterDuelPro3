using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000084 RID: 132
	internal class Vector3IntPropertyBag : ContainerPropertyBag<Vector3Int>
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000A6F5 File Offset: 0x000088F5
		public Vector3IntPropertyBag()
		{
			base.AddProperty<int>(new Vector3IntPropertyBag.XProperty());
			base.AddProperty<int>(new Vector3IntPropertyBag.YProperty());
			base.AddProperty<int>(new Vector3IntPropertyBag.ZProperty());
		}

		// Token: 0x02000085 RID: 133
		private class XProperty : Property<Vector3Int, int>
		{
			// Token: 0x1700006F RID: 111
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060002BD RID: 701 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002BE RID: 702 RVA: 0x0000A723 File Offset: 0x00008923
			public override int GetValue(ref Vector3Int container)
			{
				return container.x;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000A72B File Offset: 0x0000892B
			public override void SetValue(ref Vector3Int container, int value)
			{
				container.x = value;
			}
		}

		// Token: 0x02000086 RID: 134
		private class YProperty : Property<Vector3Int, int>
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000A73E File Offset: 0x0000893E
			public override int GetValue(ref Vector3Int container)
			{
				return container.y;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000A746 File Offset: 0x00008946
			public override void SetValue(ref Vector3Int container, int value)
			{
				container.y = value;
			}
		}

		// Token: 0x02000087 RID: 135
		private class ZProperty : Property<Vector3Int, int>
		{
			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A600 File Offset: 0x00008800
			public override string Name
			{
				get
				{
					return "z";
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000A750 File Offset: 0x00008950
			public override int GetValue(ref Vector3Int container)
			{
				return container.z;
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x0000A758 File Offset: 0x00008958
			public override void SetValue(ref Vector3Int container, int value)
			{
				container.z = value;
			}
		}
	}
}
