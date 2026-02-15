using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000095 RID: 149
	internal class BoundsIntPropertyBag : ContainerPropertyBag<BoundsInt>
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000A8E3 File Offset: 0x00008AE3
		public BoundsIntPropertyBag()
		{
			base.AddProperty<Vector3Int>(new BoundsIntPropertyBag.PositionProperty());
			base.AddProperty<Vector3Int>(new BoundsIntPropertyBag.SizeProperty());
		}

		// Token: 0x02000096 RID: 150
		private class PositionProperty : Property<BoundsInt, Vector3Int>
		{
			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0000A905 File Offset: 0x00008B05
			public override string Name
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000A90C File Offset: 0x00008B0C
			public override Vector3Int GetValue(ref BoundsInt container)
			{
				return container.position;
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000A914 File Offset: 0x00008B14
			public override void SetValue(ref BoundsInt container, Vector3Int value)
			{
				container.position = value;
			}
		}

		// Token: 0x02000097 RID: 151
		private class SizeProperty : Property<BoundsInt, Vector3Int>
		{
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000306 RID: 774 RVA: 0x0000A927 File Offset: 0x00008B27
			public override string Name
			{
				get
				{
					return "size";
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000307 RID: 775 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000A92E File Offset: 0x00008B2E
			public override Vector3Int GetValue(ref BoundsInt container)
			{
				return container.size;
			}

			// Token: 0x06000309 RID: 777 RVA: 0x0000A936 File Offset: 0x00008B36
			public override void SetValue(ref BoundsInt container, Vector3Int value)
			{
				container.size = value;
			}
		}
	}
}
