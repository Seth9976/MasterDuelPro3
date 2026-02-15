using System;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000D1B RID: 3355
	public class CommandZoneIconController : ZoneIconController
	{
		// Token: 0x06006112 RID: 24850 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommandZoneIconController Create(RunEffectWorker worker)
		{
			return null;
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ActivateCommandAvailableZone(uint commandMask, int cardID, int player, int position, int index, bool ignoreCard)
		{
			return 0;
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ActivateDecideAvailableZone(bool ignoreCard)
		{
			return 0;
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ActivateSelectStandZone(uint mask)
		{
			return 0;
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ActivateSelectStandZone(int cardID, int player, int position)
		{
			return 0;
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsAvailableZoneHandCommand(Engine.CommandType command, int commandPlayer, int commandPosition, int commandIndex, int targetPlayer, int targetPosition)
		{
			return false;
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCommandAvailableZone(Engine.CommandType command, int commandPlayer, int commandPosition, int commandIndex, int targetPlayer, int targetPosition, Content.Attribute attribute, bool isExMonster, bool isFieldMagic)
		{
			return false;
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckAvailableZone(int player, int position)
		{
			return false;
		}
	}
}
