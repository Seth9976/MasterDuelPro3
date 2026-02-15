using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Deck
{
	// Token: 0x02000FA6 RID: 4006
	[Serializable]
	public struct CardBaseData : IEquatable<CardBaseData>
	{
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600766E RID: 30318 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600766F RID: 30319 RVA: 0x0000216D File Offset: 0x0000036D
		public int CardID
		{
			[CompilerGenerated]
			readonly get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06007670 RID: 30320 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007671 RID: 30321 RVA: 0x0000216D File Offset: 0x0000036D
		public int PremiumID
		{
			[CompilerGenerated]
			readonly get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06007672 RID: 30322 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007673 RID: 30323 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsOwned
		{
			[CompilerGenerated]
			readonly get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06007674 RID: 30324 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007675 RID: 30325 RVA: 0x0000216D File Offset: 0x0000036D
		public int Obtained
		{
			[CompilerGenerated]
			readonly get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06007676 RID: 30326 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007677 RID: 30327 RVA: 0x0000216D File Offset: 0x0000036D
		public int Inventory
		{
			[CompilerGenerated]
			readonly get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06007678 RID: 30328 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007679 RID: 30329 RVA: 0x0000216D File Offset: 0x0000036D
		public int Rarity
		{
			[CompilerGenerated]
			readonly get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x0600767A RID: 30330 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600767B RID: 30331 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsRental
		{
			[CompilerGenerated]
			readonly get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x0600767C RID: 30332 RVA: 0x0000216D File Offset: 0x0000036D
		public CardBaseData(int c_id = 0, int p_id = 0, bool owned = true, bool rental = false)
		{
		}

		// Token: 0x0600767D RID: 30333 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool operator ==(CardBaseData a, CardBaseData b)
		{
			return false;
		}

		// Token: 0x0600767E RID: 30334 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool operator !=(CardBaseData a, CardBaseData b)
		{
			return false;
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Equals(CardBaseData data)
		{
			return false;
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
