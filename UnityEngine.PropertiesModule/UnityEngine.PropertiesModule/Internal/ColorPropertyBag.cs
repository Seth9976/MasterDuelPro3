using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000070 RID: 112
	internal class ColorPropertyBag : ContainerPropertyBag<Color>
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000A4A9 File Offset: 0x000086A9
		public ColorPropertyBag()
		{
			base.AddProperty<float>(new ColorPropertyBag.RProperty());
			base.AddProperty<float>(new ColorPropertyBag.GProperty());
			base.AddProperty<float>(new ColorPropertyBag.BProperty());
			base.AddProperty<float>(new ColorPropertyBag.AProperty());
		}

		// Token: 0x02000071 RID: 113
		private class RProperty : Property<Color, float>
		{
			// Token: 0x17000051 RID: 81
			// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A4E3 File Offset: 0x000086E3
			public override string Name
			{
				get
				{
					return "r";
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600026D RID: 621 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000A4EA File Offset: 0x000086EA
			public override float GetValue(ref Color container)
			{
				return container.r;
			}

			// Token: 0x0600026F RID: 623 RVA: 0x0000A4F2 File Offset: 0x000086F2
			public override void SetValue(ref Color container, float value)
			{
				container.r = value;
			}
		}

		// Token: 0x02000072 RID: 114
		private class GProperty : Property<Color, float>
		{
			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A504 File Offset: 0x00008704
			public override string Name
			{
				get
				{
					return "g";
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000272 RID: 626 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0000A50B File Offset: 0x0000870B
			public override float GetValue(ref Color container)
			{
				return container.g;
			}

			// Token: 0x06000274 RID: 628 RVA: 0x0000A513 File Offset: 0x00008713
			public override void SetValue(ref Color container, float value)
			{
				container.g = value;
			}
		}

		// Token: 0x02000073 RID: 115
		private class BProperty : Property<Color, float>
		{
			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A51C File Offset: 0x0000871C
			public override string Name
			{
				get
				{
					return "b";
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000277 RID: 631 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000278 RID: 632 RVA: 0x0000A523 File Offset: 0x00008723
			public override float GetValue(ref Color container)
			{
				return container.b;
			}

			// Token: 0x06000279 RID: 633 RVA: 0x0000A52B File Offset: 0x0000872B
			public override void SetValue(ref Color container, float value)
			{
				container.b = value;
			}
		}

		// Token: 0x02000074 RID: 116
		private class AProperty : Property<Color, float>
		{
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A534 File Offset: 0x00008734
			public override string Name
			{
				get
				{
					return "a";
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600027C RID: 636 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600027D RID: 637 RVA: 0x0000A53B File Offset: 0x0000873B
			public override float GetValue(ref Color container)
			{
				return container.a;
			}

			// Token: 0x0600027E RID: 638 RVA: 0x0000A543 File Offset: 0x00008743
			public override void SetValue(ref Color container, float value)
			{
				container.a = value;
			}
		}
	}
}
