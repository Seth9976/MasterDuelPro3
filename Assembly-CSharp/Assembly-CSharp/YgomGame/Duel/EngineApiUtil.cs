using System;
using System.Collections.Generic;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000E84 RID: 3716
	public class EngineApiUtil
	{
		// Token: 0x06006B8E RID: 27534 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardCollectionInfo.Regulation GetRegulation(int cardId)
		{
			return CardCollectionInfo.Regulation.Forbidden;
		}

		// Token: 0x06006B8F RID: 27535 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardCollectionInfo.Regulation GetRegulation(int cardId, int regid)
		{
			return CardCollectionInfo.Regulation.Forbidden;
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetCardRealFace(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetCardFace(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006B92 RID: 27538 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHandOpen(int player, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06006B93 RID: 27539 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFieldPosition(int position)
		{
			return false;
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMonsterPosition(int position)
		{
			return false;
		}

		// Token: 0x06006B95 RID: 27541 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMainMonsterPosition(int position)
		{
			return false;
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExMonsterPosition(int position)
		{
			return false;
		}

		// Token: 0x06006B97 RID: 27543 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPendulumPosition(int position)
		{
			return false;
		}

		// Token: 0x06006B98 RID: 27544 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMagicPosition(int position, bool includeField = false)
		{
			return false;
		}

		// Token: 0x06006B99 RID: 27545 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsXyzMaterialPosition(int position, int index)
		{
			return false;
		}

		// Token: 0x06006B9A RID: 27546 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCardKnown(int player, int position, int index, bool face)
		{
			return false;
		}

		// Token: 0x06006B9B RID: 27547 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCardKnown(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006B9C RID: 27548 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCardKnown(int uniqueID)
		{
			return false;
		}

		// Token: 0x06006B9D RID: 27549 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCardKnown(Engine.CardStatus status)
		{
			return false;
		}

		// Token: 0x06006B9E RID: 27550 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMagicOrTrap(int cardID)
		{
			return false;
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMagicOrTrapByUniqueID(int uniqueID)
		{
			return false;
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x000029CC File Offset: 0x00000BCC
		public static ushort ToCardPos(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x000F5F38 File Offset: 0x000F4138
		public static ValueTuple<int, int, int> FromCardPos(ushort cardPos)
		{
			return default(ValueTuple<int, int, int>);
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInsight(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInsight(Engine.CardStatus status)
		{
			return false;
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x000F5F50 File Offset: 0x000F4150
		public static ValueTuple<int, int> BreakLong(int param)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x000F5F68 File Offset: 0x000F4168
		public static ValueTuple<int, int> BreakWord(int param)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06006BA6 RID: 27558 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetCardExist(int player, int position)
		{
			return false;
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMonsterNow(int position, int locate)
		{
			return false;
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardUniqueID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006BA9 RID: 27561 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<EngineApiUtil.Place> GetAttackableMonsterPlaces()
		{
			return null;
		}

		// Token: 0x06006BAA RID: 27562 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAnythingCanDo()
		{
			return false;
		}

		// Token: 0x06006BAB RID: 27563 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAnyCommand(int playerid)
		{
			return false;
		}

		// Token: 0x06006BAC RID: 27564 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint RemoveNoneDuelCommandFlag(uint commandMask)
		{
			return 0U;
		}

		// Token: 0x06006BAD RID: 27565 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetCommandMaskRemoveNoneDuelFlag(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x06006BAE RID: 27566 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<Engine.CommandType> GetCommandList(int player, int position, int index)
		{
			return null;
		}

		// Token: 0x06006BAF RID: 27567 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<Engine.CommandType> GetCommandList(uint commandMask)
		{
			return null;
		}

		// Token: 0x06006BB0 RID: 27568 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTargetTopCardIndex(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006BB1 RID: 27569 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainsAttribute(int bitMask, Engine.ListAttribute attribute)
		{
			return false;
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<Engine.ListAttribute> GetAttributeList(int bitMask)
		{
			return null;
		}

		// Token: 0x0400A76F RID: 42863
		public const int uniqueIdStart = 1;

		// Token: 0x02000E85 RID: 3717
		public struct Place
		{
			// Token: 0x0400A770 RID: 42864
			public int player;

			// Token: 0x0400A771 RID: 42865
			public int position;

			// Token: 0x0400A772 RID: 42866
			public int index;
		}
	}
}
