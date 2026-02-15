using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000098 RID: 152
	internal class SystemVersionPropertyBag : ContainerPropertyBag<Version>
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000A940 File Offset: 0x00008B40
		public SystemVersionPropertyBag()
		{
			base.AddProperty<int>(new SystemVersionPropertyBag.MajorProperty());
			base.AddProperty<int>(new SystemVersionPropertyBag.MinorProperty());
			base.AddProperty<int>(new SystemVersionPropertyBag.BuildProperty());
			base.AddProperty<int>(new SystemVersionPropertyBag.RevisionProperty());
		}

		// Token: 0x02000099 RID: 153
		private class MajorProperty : Property<Version, int>
		{
			// Token: 0x0600030C RID: 780 RVA: 0x0000A97A File Offset: 0x00008B7A
			public MajorProperty()
			{
				base.AddAttribute(new MinAttribute(0f));
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600030D RID: 781 RVA: 0x0000A995 File Offset: 0x00008B95
			public override string Name
			{
				get
				{
					return "Major";
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x0600030E RID: 782 RVA: 0x000044A9 File Offset: 0x000026A9
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000A99C File Offset: 0x00008B9C
			public override int GetValue(ref Version container)
			{
				return container.Major;
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000467B File Offset: 0x0000287B
			public override void SetValue(ref Version container, int value)
			{
			}
		}

		// Token: 0x0200009A RID: 154
		private class MinorProperty : Property<Version, int>
		{
			// Token: 0x06000311 RID: 785 RVA: 0x0000A97A File Offset: 0x00008B7A
			public MinorProperty()
			{
				base.AddAttribute(new MinAttribute(0f));
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000312 RID: 786 RVA: 0x0000A9A5 File Offset: 0x00008BA5
			public override string Name
			{
				get
				{
					return "Minor";
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000313 RID: 787 RVA: 0x000044A9 File Offset: 0x000026A9
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000314 RID: 788 RVA: 0x0000A9AC File Offset: 0x00008BAC
			public override int GetValue(ref Version container)
			{
				return container.Minor;
			}

			// Token: 0x06000315 RID: 789 RVA: 0x0000467B File Offset: 0x0000287B
			public override void SetValue(ref Version container, int value)
			{
			}
		}

		// Token: 0x0200009B RID: 155
		private class BuildProperty : Property<Version, int>
		{
			// Token: 0x06000316 RID: 790 RVA: 0x0000A97A File Offset: 0x00008B7A
			public BuildProperty()
			{
				base.AddAttribute(new MinAttribute(0f));
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000317 RID: 791 RVA: 0x0000A9B5 File Offset: 0x00008BB5
			public override string Name
			{
				get
				{
					return "Build";
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000318 RID: 792 RVA: 0x000044A9 File Offset: 0x000026A9
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000319 RID: 793 RVA: 0x0000A9BC File Offset: 0x00008BBC
			public override int GetValue(ref Version container)
			{
				return container.Build;
			}

			// Token: 0x0600031A RID: 794 RVA: 0x0000467B File Offset: 0x0000287B
			public override void SetValue(ref Version container, int value)
			{
			}
		}

		// Token: 0x0200009C RID: 156
		private class RevisionProperty : Property<Version, int>
		{
			// Token: 0x0600031B RID: 795 RVA: 0x0000A97A File Offset: 0x00008B7A
			public RevisionProperty()
			{
				base.AddAttribute(new MinAttribute(0f));
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000A9C5 File Offset: 0x00008BC5
			public override string Name
			{
				get
				{
					return "Revision";
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x0600031D RID: 797 RVA: 0x000044A9 File Offset: 0x000026A9
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600031E RID: 798 RVA: 0x0000A9CC File Offset: 0x00008BCC
			public override int GetValue(ref Version container)
			{
				return container.Revision;
			}

			// Token: 0x0600031F RID: 799 RVA: 0x0000467B File Offset: 0x0000287B
			public override void SetValue(ref Version container, int value)
			{
			}
		}
	}
}
