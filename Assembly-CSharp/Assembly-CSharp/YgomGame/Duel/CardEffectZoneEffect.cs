using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CCB RID: 3275
	public class CardEffectZoneEffect : CardEffectBase
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06005D2F RID: 23855 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005D30 RID: 23856 RVA: 0x0000216D File Offset: 0x0000036D
		public ZoneCard.Zone zone
		{
			[CompilerGenerated]
			get
			{
				return ZoneCard.Zone.Grave;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06005D31 RID: 23857 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005D32 RID: 23858 RVA: 0x0000216D File Offset: 0x0000036D
		public ZoneCard.Mode mode
		{
			[CompilerGenerated]
			get
			{
				return ZoneCard.Mode.Out;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectZoneEffect Create(CardRoot cardRoot, ZoneCard.Zone zone, ZoneCard.Mode mode, Vector3 placePosition, Quaternion placeRotation, Vector3 placeScale, bool isFace)
		{
			return null;
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectZoneEffect CreateNoZoneEffect(CardRoot cardRoot)
		{
			return null;
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardEffectZoneEffect Create(CardRoot cardRoot, int position, bool getOut, Vector3 placePosition, Quaternion placeRotation, Vector3 placeScale, bool isFace)
		{
			return null;
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StartEffect()
		{
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateEffect()
		{
			return false;
		}

		// Token: 0x0400989F RID: 39071
		private bool zoneEffect;

		// Token: 0x040098A0 RID: 39072
		private Vector3 placePosition;

		// Token: 0x040098A1 RID: 39073
		private Quaternion placeRotation;

		// Token: 0x040098A2 RID: 39074
		private Vector3 placeScale;

		// Token: 0x040098A3 RID: 39075
		private bool isFace;
	}
}
