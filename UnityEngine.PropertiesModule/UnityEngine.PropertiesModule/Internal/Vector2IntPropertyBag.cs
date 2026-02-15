using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000081 RID: 129
	internal class Vector2IntPropertyBag : ContainerPropertyBag<Vector2Int>
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000A6A6 File Offset: 0x000088A6
		public Vector2IntPropertyBag()
		{
			base.AddProperty<int>(new Vector2IntPropertyBag.XProperty());
			base.AddProperty<int>(new Vector2IntPropertyBag.YProperty());
		}

		// Token: 0x02000082 RID: 130
		private class XProperty : Property<Vector2Int, int>
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002B3 RID: 691 RVA: 0x0000A6C8 File Offset: 0x000088C8
			public override int GetValue(ref Vector2Int container)
			{
				return container.x;
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x0000A6D0 File Offset: 0x000088D0
			public override void SetValue(ref Vector2Int container, int value)
			{
				container.x = value;
			}
		}

		// Token: 0x02000083 RID: 131
		private class YProperty : Property<Vector2Int, int>
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0000A6E3 File Offset: 0x000088E3
			public override int GetValue(ref Vector2Int container)
			{
				return container.y;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000A6EB File Offset: 0x000088EB
			public override void SetValue(ref Vector2Int container, int value)
			{
				container.y = value;
			}
		}
	}
}
