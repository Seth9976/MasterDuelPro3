using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CE0 RID: 3296
	[Serializable]
	public class CardLocator
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06005E3A RID: 24122 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E3B RID: 24123 RVA: 0x0000216D File Offset: 0x0000036D
		public bool removed
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E3D RID: 24125 RVA: 0x0000216D File Offset: 0x0000036D
		public int index
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E3F RID: 24127 RVA: 0x0000216D File Offset: 0x0000036D
		public int viewIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06005E40 RID: 24128 RVA: 0x000F512C File Offset: 0x000F332C
		// (set) Token: 0x06005E41 RID: 24129 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 pos
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x000F5144 File Offset: 0x000F3344
		// (set) Token: 0x06005E43 RID: 24131 RVA: 0x0000216D File Offset: 0x0000036D
		public Quaternion rot
		{
			get
			{
				return default(Quaternion);
			}
			set
			{
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x000F515C File Offset: 0x000F335C
		// (set) Token: 0x06005E45 RID: 24133 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 scale
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005E47 RID: 24135 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPlace cardPlace
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E49 RID: 24137 RVA: 0x0000216D File Offset: 0x0000036D
		public int refCounter
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06005E4A RID: 24138 RVA: 0x000F5174 File Offset: 0x000F3374
		// (set) Token: 0x06005E4B RID: 24139 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 posOffsetORU
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06005E4C RID: 24140 RVA: 0x000F518C File Offset: 0x000F338C
		// (set) Token: 0x06005E4D RID: 24141 RVA: 0x0000216D File Offset: 0x0000036D
		public Quaternion rotOffsetORU
		{
			[CompilerGenerated]
			get
			{
				return default(Quaternion);
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06005E4E RID: 24142 RVA: 0x000F51A4 File Offset: 0x000F33A4
		// (set) Token: 0x06005E4F RID: 24143 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 posOffsetHand
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetOffset()
		{
		}

		// Token: 0x040099C2 RID: 39362
		private int m_index;

		// Token: 0x040099C3 RID: 39363
		private int m_viewIndex;

		// Token: 0x040099C4 RID: 39364
		private Vector3 _pos;

		// Token: 0x040099C5 RID: 39365
		private Quaternion _rot;
	}
}
