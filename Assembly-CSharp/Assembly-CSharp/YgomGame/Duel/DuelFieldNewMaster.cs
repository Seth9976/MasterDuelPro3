using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D74 RID: 3444
	public class DuelFieldNewMaster : DuelFieldBase
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600647F RID: 25727 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int numMonsterPlaces
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int numMagicPlaces
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int monsterStartIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int monsterEndIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06006483 RID: 25731 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int magicStartIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int magicEndIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string nearMatResourcePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string farMatResourcePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06006487 RID: 25735 RVA: 0x000F5C10 File Offset: 0x000F3E10
		protected override Vector3 matSize
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void AssignAll(SharedDefinition.Location loc, GameObject parent)
		{
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GameObject GetFrame(SharedDefinition.Location loc, int position)
		{
			return null;
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<GameObject> GetFrames(SharedDefinition.Location loc)
		{
			return null;
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GameObject GetPlayMat(SharedDefinition.Location loc)
		{
			return null;
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x0000216A File Offset: 0x0000036A
		protected override MeshRenderer GetPlayMatRenderer(SharedDefinition.Location loc)
		{
			return null;
		}

		// Token: 0x04009F30 RID: 40752
		private DuelFieldNewMaster.HalfMat[] mats;

		// Token: 0x02000D75 RID: 3445
		public class HalfMat
		{
			// Token: 0x04009F31 RID: 40753
			public GameObject root;

			// Token: 0x04009F32 RID: 40754
			public GameObject matModel;

			// Token: 0x04009F33 RID: 40755
			public GameObject[] monsters;

			// Token: 0x04009F34 RID: 40756
			public GameObject[] magics;

			// Token: 0x04009F35 RID: 40757
			public GameObject mainDeck;

			// Token: 0x04009F36 RID: 40758
			public GameObject grave;

			// Token: 0x04009F37 RID: 40759
			public GameObject exclude;

			// Token: 0x04009F38 RID: 40760
			public GameObject fieldMagic;

			// Token: 0x04009F39 RID: 40761
			public GameObject extra;
		}
	}
}
