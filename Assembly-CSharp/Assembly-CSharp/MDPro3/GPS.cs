using System;
using MDPro3.Duel.YGOSharp;

namespace MDPro3
{
	// Token: 0x020011FA RID: 4602
	public class GPS
	{
		// Token: 0x06008884 RID: 34948 RVA: 0x000FEA2A File Offset: 0x000FCC2A
		public GPS Copy()
		{
			return (GPS)base.MemberwiseClone();
		}

		// Token: 0x06008885 RID: 34949 RVA: 0x000FEA37 File Offset: 0x000FCC37
		public bool InMyControl()
		{
			return this.controller == 0U;
		}

		// Token: 0x06008886 RID: 34950 RVA: 0x000FEA42 File Offset: 0x000FCC42
		public bool InLocation(CardLocation location)
		{
			return (this.location & (uint)location) > 0U;
		}

		// Token: 0x06008887 RID: 34951 RVA: 0x000FEA4F File Offset: 0x000FCC4F
		public bool InLocation(CardLocation location1, CardLocation location2)
		{
			return this.InLocation(location1) || this.InLocation(location2);
		}

		// Token: 0x06008888 RID: 34952 RVA: 0x000FEA63 File Offset: 0x000FCC63
		public bool InLocation(CardLocation location1, CardLocation location2, CardLocation location3)
		{
			return this.InLocation(location1) || this.InLocation(location2) || this.InLocation(location3);
		}

		// Token: 0x06008889 RID: 34953 RVA: 0x000FEA80 File Offset: 0x000FCC80
		public bool InPosition(CardPosition position)
		{
			return ((long)this.position & (long)((ulong)position)) > 0L;
		}

		// Token: 0x0600888A RID: 34954 RVA: 0x000FEA90 File Offset: 0x000FCC90
		public bool InPendulumSequence()
		{
			return this.sequence == 0U || this.sequence == 4U || this.sequence == 6U || this.sequence == 7U;
		}

		// Token: 0x0600888B RID: 34955 RVA: 0x000FEAB7 File Offset: 0x000FCCB7
		public bool IsReason(CardReason reason)
		{
			return GPS.IsReason(this.reason, reason);
		}

		// Token: 0x0600888C RID: 34956 RVA: 0x000FEAC5 File Offset: 0x000FCCC5
		public bool InSequence(uint player, CardLocation location, int targetSequence)
		{
			return player == this.controller && this.InLocation(location) && (ulong)this.sequence == (ulong)((long)targetSequence);
		}

		// Token: 0x0600888D RID: 34957 RVA: 0x000FEAE6 File Offset: 0x000FCCE6
		public static bool IsReason(uint reason, CardReason cardReason)
		{
			return (reason & (uint)cardReason) > 0U;
		}

		// Token: 0x0400C3A8 RID: 50088
		public uint controller;

		// Token: 0x0400C3A9 RID: 50089
		public uint location;

		// Token: 0x0400C3AA RID: 50090
		public uint sequence;

		// Token: 0x0400C3AB RID: 50091
		public int position;

		// Token: 0x0400C3AC RID: 50092
		public uint reason;
	}
}
