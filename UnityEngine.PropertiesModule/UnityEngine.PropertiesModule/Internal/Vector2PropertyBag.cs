using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000075 RID: 117
	internal class Vector2PropertyBag : ContainerPropertyBag<Vector2>
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000A54C File Offset: 0x0000874C
		public Vector2PropertyBag()
		{
			base.AddProperty<float>(new Vector2PropertyBag.XProperty());
			base.AddProperty<float>(new Vector2PropertyBag.YProperty());
		}

		// Token: 0x02000076 RID: 118
		private class XProperty : Property<Vector2, float>
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000282 RID: 642 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000283 RID: 643 RVA: 0x0000A575 File Offset: 0x00008775
			public override float GetValue(ref Vector2 container)
			{
				return container.x;
			}

			// Token: 0x06000284 RID: 644 RVA: 0x0000A57D File Offset: 0x0000877D
			public override void SetValue(ref Vector2 container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x02000077 RID: 119
		private class YProperty : Property<Vector2, float>
		{
			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000287 RID: 647 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000288 RID: 648 RVA: 0x0000A596 File Offset: 0x00008796
			public override float GetValue(ref Vector2 container)
			{
				return container.y;
			}

			// Token: 0x06000289 RID: 649 RVA: 0x0000A59E File Offset: 0x0000879E
			public override void SetValue(ref Vector2 container, float value)
			{
				container.y = value;
			}
		}
	}
}
