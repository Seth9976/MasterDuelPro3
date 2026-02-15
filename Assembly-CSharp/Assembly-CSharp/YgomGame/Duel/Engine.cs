using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using YgomSystem.Network;

namespace YgomGame.Duel
{
	// Token: 0x02000E2D RID: 3629
	public sealed class Engine
	{
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060068C8 RID: 26824 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsNotOnline
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060068C9 RID: 26825 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int CounterTypeMax
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (set) Token: 0x060068CA RID: 26826 RVA: 0x0000216D File Offset: 0x0000036D
		public static ReplayStream ReplayStream
		{
			set
			{
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060068CB RID: 26827 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060068CC RID: 26828 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool InputTimerSetting
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060068CD RID: 26829 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool InputNow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060068CE RID: 26830 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.GameMode CachedGameMode
		{
			get
			{
				return Util.GameMode.Normal;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060068CF RID: 26831 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOnline
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckPosBit(uint mask, int player, int pos)
		{
			return false;
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEnableBattleDamagePlayer(int prop, int player, Engine.BtlPropFlag flag)
		{
			return false;
		}

		// Token: 0x060068D2 RID: 26834
		[PreserveSig]
		private static extern int DLL_DuelDlgGetMixNum();

		// Token: 0x060068D3 RID: 26835
		[PreserveSig]
		private static extern int DLL_DuelDlgGetMixType(int index);

		// Token: 0x060068D4 RID: 26836
		[PreserveSig]
		private static extern int DLL_DuelDlgGetMixData(int index);

		// Token: 0x060068D5 RID: 26837
		[PreserveSig]
		private static extern void DLL_DuelDlgSetResult(uint result);

		// Token: 0x060068D6 RID: 26838
		[PreserveSig]
		private static extern int DLL_DuelDlgCanYesNoSkip();

		// Token: 0x060068D7 RID: 26839
		[PreserveSig]
		private static extern int DLL_DuelDlgGetPosMaskOfThisSummon();

		// Token: 0x060068D8 RID: 26840
		[PreserveSig]
		private static extern int DLL_DuelDlgGetSelectItemNum();

		// Token: 0x060068D9 RID: 26841
		[PreserveSig]
		private static extern int DLL_DuelDlgGetSelectItemStr(int index);

		// Token: 0x060068DA RID: 26842
		[PreserveSig]
		private static extern int DLL_DuelDlgGetSelectItemEnable(int index);

		// Token: 0x060068DB RID: 26843
		[PreserveSig]
		private static extern int DLL_DlgProcGetSummoningMonsterUniqueID();

		// Token: 0x060068DC RID: 26844 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int DialogGetMixNum()
		{
			return 0;
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.DialogMixTextType DialogGetMixType(int index)
		{
			return Engine.DialogMixTextType.Null;
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int DialogGetMixData(int index)
		{
			return 0;
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DequeueDialogMixData()
		{
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DialogSetResult(uint result)
		{
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool DialogCanYesNoSkip()
		{
			return false;
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint DialogGetPosMaskOfThisSummon()
		{
			return 0U;
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int DialogGetSelectItemNum()
		{
			return 0;
		}

		// Token: 0x060068E4 RID: 26852 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int DialogGetSelectItemStr(int index)
		{
			return 0;
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool DialogGetSelectItemEnable(int index)
		{
			return false;
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int DialogGetSummoningMonsterUniqueID()
		{
			return 0;
		}

		// Token: 0x060068E7 RID: 26855
		[PreserveSig]
		private static extern int DLL_GetRevision();

		// Token: 0x060068E8 RID: 26856
		[PreserveSig]
		private static extern int DLL_GetBinHash(int iIndex);

		// Token: 0x060068E9 RID: 26857
		[PreserveSig]
		private static extern int DLL_SetWorkMemory(IntPtr pWork);

		// Token: 0x060068EA RID: 26858
		[PreserveSig]
		private static extern int DLL_DuelSysInitRush();

		// Token: 0x060068EB RID: 26859
		[PreserveSig]
		private static extern int DLL_DuelSysInitQuestion(IntPtr pScript);

		// Token: 0x060068EC RID: 26860
		[PreserveSig]
		private static extern int DLL_DuelSysInitCustom(int fDuelType, bool tag, int life0, int life1, int hand0, int hand1, bool shuf);

		// Token: 0x060068ED RID: 26861
		[PreserveSig]
		private static extern int DLL_DuelSysAct();

		// Token: 0x060068EE RID: 26862
		[PreserveSig]
		private static extern void DLL_DuelSysClearWork();

		// Token: 0x060068EF RID: 26863
		[PreserveSig]
		private static extern void DLL_DuelSysSetDeck2(int player, int[] mainDeck, int mainNum, int[] extraDeck, int extraNum, int[] sideDeck, int sideNum);

		// Token: 0x060068F0 RID: 26864
		[PreserveSig]
		private static extern void DLL_DuelSetRandomSeed(uint seed);

		// Token: 0x060068F1 RID: 26865
		[PreserveSig]
		private static extern void DLL_DuelSetMyPlayerNum(int player);

		// Token: 0x060068F2 RID: 26866
		[PreserveSig]
		private static extern void DLL_DuelSetPlayerType(int player, int type);

		// Token: 0x060068F3 RID: 26867
		[PreserveSig]
		private static extern int DLL_DuelIsHuman(int player);

		// Token: 0x060068F4 RID: 26868
		[PreserveSig]
		private static extern int DLL_DuelMyself();

		// Token: 0x060068F5 RID: 26869
		[PreserveSig]
		private static extern int DLL_DuelRival();

		// Token: 0x060068F6 RID: 26870
		[PreserveSig]
		private static extern int DLL_DuelIsMyself(int player);

		// Token: 0x060068F7 RID: 26871
		[PreserveSig]
		private static extern int DLL_DuelIsRival(int player);

		// Token: 0x060068F8 RID: 26872
		[PreserveSig]
		private static extern void DLL_DuelSetCpuParam(int player, uint param);

		// Token: 0x060068F9 RID: 26873
		[PreserveSig]
		private static extern void DLL_DuelSetFirstPlayer(int player);

		// Token: 0x060068FA RID: 26874
		[PreserveSig]
		private static extern int DLL_DuelGetDuelResult();

		// Token: 0x060068FB RID: 26875
		[PreserveSig]
		private static extern int DLL_DuelGetDuelFinish();

		// Token: 0x060068FC RID: 26876
		[PreserveSig]
		private static extern int DLL_DuelGetDuelFinishCardID();

		// Token: 0x060068FD RID: 26877
		[PreserveSig]
		private static extern void DLL_DuelSetDuelLimitedType(uint limitedType);

		// Token: 0x060068FE RID: 26878
		[PreserveSig]
		private static extern void DLL_SetEffectDelegate(Engine.ThreadRunEffectDeleg runEffct, Engine.ThreadIsBusyEffectDeleg isBusyEffect);

		// Token: 0x060068FF RID: 26879
		[PreserveSig]
		private static extern int DLL_DuelIsThisQuickDuel();

		// Token: 0x06006900 RID: 26880
		[PreserveSig]
		private static extern void DLL_SetCardExistWork(IntPtr pWork, int size, int count);

		// Token: 0x06006901 RID: 26881
		[PreserveSig]
		private static extern int DLL_GetCardExistNum();

		// Token: 0x06006902 RID: 26882
		[PreserveSig]
		private static extern int DLL_DuelGetLP(int player);

		// Token: 0x06006903 RID: 26883
		[PreserveSig]
		private static extern int DLL_DuelWhichTurnNow();

		// Token: 0x06006904 RID: 26884
		[PreserveSig]
		private static extern uint DLL_DuelGetCurrentPhase();

		// Token: 0x06006905 RID: 26885
		[PreserveSig]
		private static extern uint DLL_DuelGetCurrentStep();

		// Token: 0x06006906 RID: 26886
		[PreserveSig]
		private static extern uint DLL_DuelGetCurrentDmgStep();

		// Token: 0x06006907 RID: 26887
		[PreserveSig]
		private static extern uint DLL_DuelGetTurnNum();

		// Token: 0x06006908 RID: 26888
		[PreserveSig]
		private static extern IntPtr DLL_DuelGetCardPropByUniqueID(int uniqueId);

		// Token: 0x06006909 RID: 26889
		[PreserveSig]
		private static extern int DLL_DuelGetCardUniqueID(int player, int position, int index);

		// Token: 0x0600690A RID: 26890
		[PreserveSig]
		private static extern int DLL_DuelGetCardTurn(int player, int position, int index);

		// Token: 0x0600690B RID: 26891
		[PreserveSig]
		private static extern int DLL_DuelGetCardFace(int player, int position, int index);

		// Token: 0x0600690C RID: 26892
		[PreserveSig]
		private static extern int DLL_DuelGetCardNum(int player, int locate);

		// Token: 0x0600690D RID: 26893
		[PreserveSig]
		private static extern int DLL_DuelGetTopCardIndex(int player, int locate);

		// Token: 0x0600690E RID: 26894
		[PreserveSig]
		private static extern bool DLL_DuelGetHandCardOpen(int player, int index);

		// Token: 0x0600690F RID: 26895
		[PreserveSig]
		private static extern uint DLL_DuelSearchCardByUniqueID(int uniqueId);

		// Token: 0x06006910 RID: 26896
		[PreserveSig]
		private static extern uint DLL_DuelGetCardIDByUniqueID2(int uniqueId);

		// Token: 0x06006911 RID: 26897
		[PreserveSig]
		private static extern uint DLL_DuelCanIDoPutMonster(int player);

		// Token: 0x06006912 RID: 26898
		[PreserveSig]
		private static extern bool DLL_DuelCanIDoSummonMonster(int player);

		// Token: 0x06006913 RID: 26899
		[PreserveSig]
		private static extern bool DLL_DuelCanIDoSpecialSummon(int player);

		// Token: 0x06006914 RID: 26900
		[PreserveSig]
		private static extern uint DLL_DuelGetCardInHand(int player);

		// Token: 0x06006915 RID: 26901
		[PreserveSig]
		private static extern void DLL_DuelGetCardBasicVal(int player, int pos, int index, ref Engine.BasicVal pVal);

		// Token: 0x06006916 RID: 26902
		[PreserveSig]
		private static extern int DLL_DuelGetTrapMonstBasicVal(int cardId, ref Engine.BasicVal pVal);

		// Token: 0x06006917 RID: 26903
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardOverlayNum(int player, int locate);

		// Token: 0x06006918 RID: 26904
		[PreserveSig]
		private static extern int DLL_FusionGetMaterialList(int uniqueId, IntPtr list);

		// Token: 0x06006919 RID: 26905
		[PreserveSig]
		private static extern int DLL_FusionIsThisTunedMonsterInTuning(int wUniqueID);

		// Token: 0x0600691A RID: 26906
		[PreserveSig]
		private static extern int DLL_FusionGetMonsterLevelInTuning(int wUniqueID);

		// Token: 0x0600691B RID: 26907
		[PreserveSig]
		private static extern int DLL_DuelIsThisCardExist(int player, int locate);

		// Token: 0x0600691C RID: 26908
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardEffectIDAtChain(int player, int locate);

		// Token: 0x0600691D RID: 26909
		[PreserveSig]
		private static extern int DLL_DuelGetAttackTargetMask(int player, int locate);

		// Token: 0x0600691E RID: 26910
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardDirectFlag(int player, int index);

		// Token: 0x0600691F RID: 26911
		[PreserveSig]
		private static extern void DLL_DuelGetFldAffectIcon(int player, int locate, IntPtr ptr, int view_player);

		// Token: 0x06006920 RID: 26912
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardCounter(int player, int locate, int counter);

		// Token: 0x06006921 RID: 26913
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardTurnCounter(int player, int locate);

		// Token: 0x06006922 RID: 26914
		[PreserveSig]
		private static extern int DLL_DuelIsThisTunerMonster(int player, int locate);

		// Token: 0x06006923 RID: 26915
		[PreserveSig]
		private static extern int DLL_DuelIsThisNormalMonster(int player, int locate);

		// Token: 0x06006924 RID: 26916
		[PreserveSig]
		private static extern bool DLL_DuelIsThisEffectiveMonster(int player, int index);

		// Token: 0x06006925 RID: 26917
		[PreserveSig]
		private static extern bool DLL_DeulIsThisEffectiveMonsterWithDual(int player, int index);

		// Token: 0x06006926 RID: 26918
		[PreserveSig]
		private static extern bool DLL_DuelIsThisNormalMonsterInGrave(int player, int index);

		// Token: 0x06006927 RID: 26919
		[PreserveSig]
		private static extern bool DLL_DuelIsThisNormalMonsterInHand(int wCardID);

		// Token: 0x06006928 RID: 26920
		[PreserveSig]
		private static extern int DLL_DuelIsThisTrapMonster(int player, int locate);

		// Token: 0x06006929 RID: 26921
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardEffectFlags(int player, int locate);

		// Token: 0x0600692A RID: 26922
		[PreserveSig]
		private static extern int DLL_DuelGetFldMonstOrgLevel(int player, int locate);

		// Token: 0x0600692B RID: 26923
		[PreserveSig]
		private static extern int DLL_DuelGetFldMonstOrgType(int player, int locate);

		// Token: 0x0600692C RID: 26924
		[PreserveSig]
		private static extern int DLL_DuelGetFldPendScale(int player, int locate);

		// Token: 0x0600692D RID: 26925
		[PreserveSig]
		private static extern int DLL_DuelGetFldPendOrgScale(int player, int locate);

		// Token: 0x0600692E RID: 26926
		[PreserveSig]
		private static extern int DLL_DuelGetFldMonstRank(int player, int locate);

		// Token: 0x0600692F RID: 26927
		[PreserveSig]
		private static extern int DLL_DuelGetFldMonstOrgRank(int player, int locate);

		// Token: 0x06006930 RID: 26928
		[PreserveSig]
		private static extern int DLL_DuelIsThisZoneAvailable(int player, int locate);

		// Token: 0x06006931 RID: 26929
		[PreserveSig]
		private static extern int DLL_DuelIsThisZoneAvailable2(int player, int locate, bool visibleOnly);

		// Token: 0x06006932 RID: 26930
		[PreserveSig]
		private static extern int DLL_DuelGetThisCardShowParameter(int player, int locate);

		// Token: 0x06006933 RID: 26931
		[PreserveSig]
		private static extern uint DLL_DuelGetThisCardParameter(int player, int locate);

		// Token: 0x06006934 RID: 26932
		[PreserveSig]
		private static extern uint DLL_DuelGetThisCardEffectList(int player, int locate, IntPtr list);

		// Token: 0x06006935 RID: 26933
		[PreserveSig]
		private static extern uint DLL_DUELCOMGetPosMaskOfThisHand(int player, int index, int commandId);

		// Token: 0x06006936 RID: 26934
		[PreserveSig]
		private static extern int DLL_DuelIsThisContinuousCard(int player, int locate);

		// Token: 0x06006937 RID: 26935
		[PreserveSig]
		private static extern int DLL_DuelIsThisEquipCard(int player, int locate);

		// Token: 0x06006938 RID: 26936
		[PreserveSig]
		private static extern bool DLL_DuelIsThisMagic(int player, int locate);

		// Token: 0x06006939 RID: 26937
		[PreserveSig]
		private static extern bool DLL_DuelIsThisTrap(int player, int locate);

		// Token: 0x0600693A RID: 26938
		[PreserveSig]
		private static extern bool DLL_DuelGetThisMonsterFightableOnEffect(int player, int locate);

		// Token: 0x0600693B RID: 26939
		[PreserveSig]
		private static extern int DLL_DUELCOMGetRecommendSide();

		// Token: 0x0600693C RID: 26940
		[PreserveSig]
		private static extern bool DLL_DuelGetDuelFlagDeckReverse();

		// Token: 0x0600693D RID: 26941
		[PreserveSig]
		private static extern uint DLL_DuelComGetCommandMask(int player, int position, int index);

		// Token: 0x0600693E RID: 26942
		[PreserveSig]
		private static extern uint DLL_DuelComGetTextIDOfThisCommand(int player, int position, int index);

		// Token: 0x0600693F RID: 26943
		[PreserveSig]
		private static extern void DLL_DuelComDoCommand(int player, int position, int index, int commandId);

		// Token: 0x06006940 RID: 26944
		[PreserveSig]
		private static extern int DLL_DuelComCancelCommand2(bool decide);

		// Token: 0x06006941 RID: 26945
		[PreserveSig]
		private static extern void DLL_DuelComDefaultLocation();

		// Token: 0x06006942 RID: 26946
		[PreserveSig]
		private static extern uint DLL_DuelComGetMovablePhase();

		// Token: 0x06006943 RID: 26947
		[PreserveSig]
		private static extern void DLL_DuelComMovePhase(int phase);

		// Token: 0x06006944 RID: 26948
		[PreserveSig]
		private static extern void DLL_DuelComDebugCommand();

		// Token: 0x06006945 RID: 26949
		[PreserveSig]
		private static extern uint DLL_CardRareGetBufferSize();

		// Token: 0x06006946 RID: 26950
		[PreserveSig]
		private static extern void DLL_CardRareSetRare(IntPtr pBuf, IntPtr rare0, IntPtr rare1, IntPtr rare2, IntPtr rare3);

		// Token: 0x06006947 RID: 26951
		[PreserveSig]
		private static extern int DLL_CardRareGetRareByUniqueID(int uniqueId);

		// Token: 0x06006948 RID: 26952
		[PreserveSig]
		private static extern void DLL_CardRareSetBuffer(IntPtr pBuf);

		// Token: 0x06006949 RID: 26953
		[PreserveSig]
		private static extern int DLL_DuelResultGetMemo(int player, IntPtr dst);

		// Token: 0x0600694A RID: 26954
		[PreserveSig]
		private static extern int DLL_DuelResultGetData(int player, IntPtr dst);

		// Token: 0x0600694B RID: 26955
		[PreserveSig]
		private static extern void DLL_SetAddRecordDelegate(Engine.AddRecord addRecord);

		// Token: 0x0600694C RID: 26956
		[PreserveSig]
		private static extern void DLL_SetPlayRecordDelegate(Engine.NowRecord nowRecord, Engine.RecordNext recordNext, Engine.RecordBegin recordBegin, Engine.IsRecordEnd isRecordEnd);

		// Token: 0x0600694D RID: 26957
		[PreserveSig]
		private static extern int DLL_DuelIsReplayMode();

		// Token: 0x0600694E RID: 26958
		[PreserveSig]
		private static extern void DLL_SetDuelChallenge(int flagbit);

		// Token: 0x0600694F RID: 26959
		[PreserveSig]
		private static extern void DLL_SetDuelChallenge2(int player, int flagbit);

		// Token: 0x06006950 RID: 26960 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Revision()
		{
			return 0;
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int BinHash(int iIndex)
		{
			return 0;
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int SetWorkMemory(IntPtr pWork)
		{
			return 0;
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardOwnerByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SysInitQuestion(byte[] data)
		{
			return false;
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SysInitCustom(int fDuelType = 0, bool tag = false, int life0 = -1, int life1 = -1, int hand0 = -1, int hand1 = -1, bool shuf = false)
		{
			return false;
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x0000216D File Offset: 0x0000036D
		private void recvData()
		{
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x0000216D File Offset: 0x0000036D
		private void recvDataImpl(PvP.Event ev)
		{
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPreRecvData(PvP.Event ev)
		{
		}

		// Token: 0x06006959 RID: 26969 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SysAct()
		{
			return false;
		}

		// Token: 0x0600695A RID: 26970 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PvpForceFinish()
		{
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSysActLoopExecute()
		{
			return false;
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearWork()
		{
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SysSetDeck(int player, int[] mainDeck, int[] extraDeck, int[] sideDeck)
		{
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetRandomSeed(uint seed)
		{
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetMyPlayerNum(int player)
		{
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPlayerType(int player, Engine.PlayerType type)
		{
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHuman(int player)
		{
			return false;
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Myself()
		{
			return 0;
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Rival()
		{
			return 0;
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMyself(int player)
		{
			return false;
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsRival(int player)
		{
			return false;
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int OtherSide(int player)
		{
			return 0;
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitCpuParam(int player, uint param)
		{
		}

		// Token: 0x06006968 RID: 26984 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCpuParam(int player, uint param)
		{
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetFirstPlayer(int player)
		{
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.ResultType GetDuelResult()
		{
			return Engine.ResultType.None;
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.FinishType GetDuelFinish()
		{
			return Engine.FinishType.None;
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelFinishCardID()
		{
			return 0;
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDuelLimitedType(Engine.LimitedType limited)
		{
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetEffectDelegate(Engine.RunEffect runEffect, Engine.IsBusyEffect isBusyEffect)
		{
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardExistWork(int size)
		{
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResumeCardExistWork(byte[] data, int count)
		{
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardExistNum()
		{
			return 0;
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CpuSysCheckFinishAttack()
		{
			return false;
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLP(int player)
		{
			return 0;
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int WhichTurnNow()
		{
			return 0;
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.Phase GetCurrentPhase()
		{
			return Engine.Phase.Draw;
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.StepType GetCurrentStep()
		{
			return Engine.StepType.Null;
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.DmgStepType GetCurrentDmgStep()
		{
			return Engine.DmgStepType.Null;
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTurnNum()
		{
			return 0;
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelCanIDoPutMonster(int player)
		{
			return 0;
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetDuelCanIDoSummonMonster(int player)
		{
			return false;
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetDuelCanIDoSpecialSummon(int player)
		{
			return false;
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x000F5E78 File Offset: 0x000F4078
		public static Engine.CardProp GetCardPropByUniqueID(int uniqueId)
		{
			return default(Engine.CardProp);
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardUniqueID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetCardTurn(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetCardFace(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardNum(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTopCardIndex(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int SearchCardByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x000F5E90 File Offset: 0x000F4090
		public static ValueTuple<int, int, int> GetParamsByUniqueId(int uniqueid)
		{
			return default(ValueTuple<int, int, int>);
		}

		// Token: 0x06006985 RID: 27013 RVA: 0x000F5EA8 File Offset: 0x000F40A8
		public static Engine.BasicVal GetBasicValByUniqueId(int uniqueid)
		{
			return default(Engine.BasicVal);
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardIDByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetCardInHand(int player)
		{
			return 0U;
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetCardBasicVal(int player, int position, int index, ref Engine.BasicVal val)
		{
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetTrapMonsterBasicVal(int cardId, ref Engine.BasicVal val)
		{
			return false;
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetThisCardOverlayNum(int player, int locate)
		{
			return 0;
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetFusionMaterialList(int uniqueId)
		{
			return null;
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool FusionIsThisTunedMonsterInTuning()
		{
			return false;
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FusionGetMonsterLevelInTuning()
		{
			return 0;
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisCardExist(int player, int locate)
		{
			return false;
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetThisCardEffectIDAtChain(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetAttackTargetMask(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x0000216A File Offset: 0x0000036A
		public static Engine.AffectType[][] GetFieldAffectIcon(int player, int locate)
		{
			return null;
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x000F5EC0 File Offset: 0x000F40C0
		public static Engine.CardStatus SearchCardStatusByUniqueID(int uniqueId)
		{
			return default(Engine.CardStatus);
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetThisCardCounter(int player, int locate, Engine.CounterType counter)
		{
			return 0;
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetThisCardTurnCounter(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisTunerMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisNormalMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisTrapMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int IsThisContinuousCard(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int IsThisEquipCard(int player, int locate)
		{
			return 0;
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisMagic(int player, int locate)
		{
			return false;
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisTrap(int player, int locate)
		{
			return false;
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetThisMonsterFightableOnEffect(int player, int locate)
		{
			return false;
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetThisCardEffectFlags(int player, int locate)
		{
			return 0;
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFldMonstOrgLevel(int player, int locate)
		{
			return 0;
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFldMonstOrgType(int player, int locate)
		{
			return 0;
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFldPendScale(int player, int locate)
		{
			return 0;
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFldPendOrgScale(int player, int locate)
		{
			return 0;
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFldMonstRank(int player, int locate)
		{
			return 0;
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFldMonstOrgRank(int player, int locate)
		{
			return 0;
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThisZoneAvailable(int player, int locate, bool visibleOnly = false)
		{
			return false;
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.ShowParam GetThisCardShowParameter(int player, int locate)
		{
			return Engine.ShowParam.Null;
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetThisCardParameter(int player, int locate)
		{
			return 0U;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetThisCardDirectFlag(int player, int locate)
		{
			return 0;
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetThisCardEffectList(int player, int locate)
		{
			return null;
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetPosMaskOfThisHand(int player, int index, Engine.CommandType commandId)
		{
			return 0U;
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint ComGetCommandMask(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint ComGetTextIDOfThisCommand(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint ComGetCommandMaskEach(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ComDoCommand(int player, int position, int index, Engine.CommandType commandId)
		{
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ComCancelCommand(bool decide = true)
		{
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint ComGetMovablePhase()
		{
			return 0U;
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ComMovePhase(Engine.Phase phase)
		{
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRecommendSide()
		{
			return 0;
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetFlagDeckReverse()
		{
			return false;
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ComDebugCommand()
		{
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int CardRareBufferSize()
		{
			return 0;
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardRare(int[] rare0, int[] rare1, int[] rare2 = null, int[] rare3 = null)
		{
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardRareByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAddRecordDelegate(Engine.AddRecord addRecord)
		{
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPlayRecordDelegate(Engine.NowRecord nowRecord, Engine.RecordNext recordNext, Engine.RecordBegin recordBegin, Engine.IsRecordEnd isRecordEnd)
		{
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsReplayMode()
		{
			return false;
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetPvpDuelInfoTimeLeft()
		{
			return 0U;
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetPvpDuelInfoTimeTotal()
		{
			return 0U;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDuelChallenge(int flagbit)
		{
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDuelChallenge2(int player, int flagbit)
		{
		}

		// Token: 0x060069BE RID: 27070 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.CounterType GetCounterType(int id)
		{
			return Engine.CounterType.Magic;
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCounterId(Engine.CounterType type)
		{
			return 0;
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool TransExMonsterPosition(int team, int pos, ref int oppTeam, ref int oppPos)
		{
			return false;
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool TransExMonsterPositionUnAvailable(ref int team, ref int pos)
		{
			return false;
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ForceSimpleCpu(int player, Engine.CpuParam param = Engine.CpuParam.Simple)
		{
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x0000216D File Offset: 0x0000036D
		private static void RestoreCpuParam(int player)
		{
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int CachedMatId_Get(int player)
		{
			return 0;
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CachedMatId_Set(int player, int mid)
		{
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int CachedSleeveId_Get(int player)
		{
			return 0;
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CachedSleeveId_Set(int player, int sid)
		{
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetInitialLP(int player)
		{
			return 0;
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetInitialLP(int player, int val)
		{
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Util.GameMode gamemode, bool isOnline = false)
		{
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Destroy()
		{
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x0000216D File Offset: 0x0000036D
		private void Release()
		{
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitCounterDictionary(Dictionary<string, object> dic)
		{
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateCardRareBuffer()
		{
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseCardRareBuffer()
		{
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] GetRecvData(PvP.Command cmd)
		{
			return null;
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRecvCount(PvP.Command cmd)
		{
			return 0;
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetRecvOrder(PvP.Command cmd)
		{
			return 0U;
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearRecvQueue(PvP.Command cmd)
		{
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateCardExistBuffer(int size)
		{
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] GetCardExistBuffer()
		{
			return null;
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseCardExistBuffer()
		{
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetStartInputCallback(Action callback)
		{
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetUpdateTimerCallback(Action callback)
		{
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartInput()
		{
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EndInput()
		{
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SendPvpTime()
		{
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetNoResponseCallback(Action noResponse, Action recovery, Action closed)
		{
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x0000216D File Offset: 0x0000036D
		public static void NoResponse()
		{
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecoveryResponse()
		{
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x0000216D File Offset: 0x0000036D
		public static void NoResponseClosed()
		{
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateQuestionData(byte[] data)
		{
		}

		// Token: 0x060069E1 RID: 27105 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseQuestionData()
		{
		}

		// Token: 0x060069E2 RID: 27106 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint MakeCpuParam(int val, Engine.CpuParam param = Engine.CpuParam.None)
		{
			return 0U;
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetEngineWork()
		{
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLatency(byte[] recvData)
		{
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLatency(int player)
		{
			return 0;
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SendSurrender(bool autoSurrender = false)
		{
		}

		// Token: 0x060069E7 RID: 27111
		[PreserveSig]
		private static extern int DLL_DuelListIsMultiMode();

		// Token: 0x060069E8 RID: 27112
		[PreserveSig]
		private static extern void DLL_DuelListInitString();

		// Token: 0x060069E9 RID: 27113
		[PreserveSig]
		private static extern int DLL_DuelListGetSelectMax();

		// Token: 0x060069EA RID: 27114
		[PreserveSig]
		private static extern int DLL_DuelListGetSelectMin();

		// Token: 0x060069EB RID: 27115
		[PreserveSig]
		private static extern int DLL_DuelListGetItemMax();

		// Token: 0x060069EC RID: 27116
		[PreserveSig]
		private static extern int DLL_DuelListGetItemID(int index);

		// Token: 0x060069ED RID: 27117
		[PreserveSig]
		private static extern int DLL_DuelListGetItemUniqueID(int index);

		// Token: 0x060069EE RID: 27118
		[PreserveSig]
		private static extern int DLL_DuelListGetItemFrom(int index);

		// Token: 0x060069EF RID: 27119
		[PreserveSig]
		private static extern int DLL_DuelListGetItemMsg(int index);

		// Token: 0x060069F0 RID: 27120
		[PreserveSig]
		private static extern int DLL_DuelListGetItemAttribute(int index);

		// Token: 0x060069F1 RID: 27121
		[PreserveSig]
		private static extern int DLL_DuelListGetCardAttribute(int iLookPlayer, int wUniqueID);

		// Token: 0x060069F2 RID: 27122
		[PreserveSig]
		private static extern void DLL_DuelListSetIndex(int index);

		// Token: 0x060069F3 RID: 27123
		[PreserveSig]
		private static extern void DLL_DuelListSetCardExData(int index, int data);

		// Token: 0x060069F4 RID: 27124
		[PreserveSig]
		private static extern int DLL_DuelListGetItemTargetUniqueID(int index);

		// Token: 0x060069F5 RID: 27125 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ListIsMultiMode()
		{
			return false;
		}

		// Token: 0x060069F6 RID: 27126 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetSelectMax()
		{
			return 0;
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetSelectMin()
		{
			return 0;
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemMax()
		{
			return 0;
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemID(int index)
		{
			return 0;
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemTargetUniqueID(int index)
		{
			return 0;
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemUniqueID(int index)
		{
			return 0;
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemFrom(int index)
		{
			return 0;
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemMsg(int listIdx)
		{
			return 0;
		}

		// Token: 0x060069FE RID: 27134 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemMixNum(int listIdx)
		{
			return 0;
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.DialogMixTextType ListGetItemMixType(int listIdx, int mixIdx)
		{
			return Engine.DialogMixTextType.Null;
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemMixData(int listIdx, int mixIdx)
		{
			return 0;
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetItemAttribute(int index)
		{
			return 0;
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ListSendBlindIndex(int index)
		{
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ListSendIndex(int index)
		{
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ListGetCardAttribute(int lookPlayer, int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ListSendSelectMulti(int num, List<int> selected)
		{
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int _PosToIdx(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int _PosToUniqueId(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.PvpUIDBase _PosToUIDBase(int player, int position, int index)
		{
			return null;
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.PvpUIDBase _UniqueIdToUIDBase(int uniqueId)
		{
			return null;
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetLP(int player)
		{
			return 0;
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelCanIDoPutMonster(int player)
		{
			return 0;
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelCanIDoSummonMonster(int player)
		{
			return false;
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelCanIDoSpecialSummon(int player)
		{
			return false;
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelWhichTurnNow()
		{
			return 0;
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.Phase PVP_DuelGetCurrentPhase()
		{
			return Engine.Phase.Draw;
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.StepType PVP_DuelGetCurrentStep()
		{
			return Engine.StepType.Null;
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.DmgStepType PVP_DuelGetCurrentDmgStep()
		{
			return Engine.DmgStepType.Null;
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetTurnNum()
		{
			return 0;
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x000F5ED8 File Offset: 0x000F40D8
		private static Engine.CardProp PVP_GetCardPropByUniqueID(int uniqueId)
		{
			return default(Engine.CardProp);
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetCardID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetCardUniqueID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelGetCardTurn(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelGetCardFace(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetCardNum(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisMonsterFightableOnEffect(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelIsThisEquipCard(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelIsThisContinuousCard(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelIsThisMagic(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelIsThisTrap(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetTopCardIndex(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelSearchCardByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelGetCardIDByUniqueID(int uniqueId)
		{
			return 0U;
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x0000216D File Offset: 0x0000036D
		private static void PVP_DuelGetCardBasicVal(int player, int position, int index, ref Engine.BasicVal val)
		{
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelIsThisCardExist(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisCardEffectIDAtChain(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetAttackTargetMask(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.AffectType[][] PVP_GetFieldAffectIcon(int player, int locate)
		{
			return null;
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisCardCounter(int player, int locate, Engine.CounterType type)
		{
			return 0;
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x0000216A File Offset: 0x0000036A
		private static int[] PVP_GetThisCardEffectList(int player, int locate)
		{
			return null;
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DUELCOMGetPosMaskOfThisHand(int player, int index, int commandId)
		{
			return 0U;
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisCardTurnCounter(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelIsThisNormalMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelIsThisTrapMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A2C RID: 27180 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelIsThisTunerMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A2D RID: 27181 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisCardEffectFlags(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetFldMonstOrgLevel(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetFldMonstOrgType(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetFldPendScale(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetFldPendOrgScale(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetFldMonstRank(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetFldMonstOrgRank(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisCardOverlayNum(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelIsThisZoneAvailable(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelIsThisZoneAvailable2(int player, int locate, bool visibleOnly)
		{
			return false;
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.ShowParam PVP_DuelGetThisCardShowParameter(int player, int locate)
		{
			return Engine.ShowParam.Null;
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelGetThisCardParameter(int player, int locate)
		{
			return 0U;
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelGetThisCardDirectFlag(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelComGetCommandMask(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelComGettextIDOfThisCommand(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x0000216D File Offset: 0x0000036D
		private static void PVP_ComDoCommand(int player, int position, int index, Engine.CommandType commandId)
		{
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelComGetMovablePhase()
		{
			return 0U;
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_CardRareGetRareByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x0000216D File Offset: 0x0000036D
		private static void PVP_DequeDlgMixData()
		{
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelDlgGetMixNum()
		{
			return 0;
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.DialogMixTextType PVP_DuelDlgGetMixType(int index)
		{
			return Engine.DialogMixTextType.Null;
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelDlgGetMixData(int index)
		{
			return 0;
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemMsg(int listIdx)
		{
			return 0;
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemMixNum(int listIdx)
		{
			return 0;
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.DialogMixTextType PVP_DuelListGetItemMixType(int listIdx, int mixIdx)
		{
			return Engine.DialogMixTextType.Null;
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemMixData(int listIdx, int mixIdx)
		{
			return 0;
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelDlgCanYesNoSkip()
		{
			return false;
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelDlgGetPosMaskOfThisSummon()
		{
			return 0U;
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelDlgGetSelectItemNum()
		{
			return 0;
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelDlgGetSelectItemStr(int index)
		{
			return 0;
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelDlgGetSelectItemEnable(int index)
		{
			return false;
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DlgProcGetSummoningMonsterUniqueID()
		{
			return 0;
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelListIsMultiMode()
		{
			return false;
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetSelectMax()
		{
			return 0;
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetSelectMin()
		{
			return 0;
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemMax()
		{
			return 0;
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemID(int index)
		{
			return 0;
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemUniqueID(int index)
		{
			return 0;
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemAttribute(int index)
		{
			return 0;
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemFrom(int index)
		{
			return 0;
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetCardAttribute(int lookPlayer, int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelListGetItemTargetUniqueID(int index)
		{
			return 0;
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x0000216A File Offset: 0x0000036A
		private static int[] PVP_FusionGetMaterialList()
		{
			return null;
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_FusionIsThisTunedMonsterInTuning()
		{
			return 0;
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_FusionGetMonsterLevelInTuning()
		{
			return 0;
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int PVP_DuelComGetCommandMask()
		{
			return 0;
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool PVP_DuelGetDuelFlagDeckReverse()
		{
			return false;
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PVP_IsSysActLoopExecute()
		{
			return false;
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelInfoTimeLeft()
		{
			return 0U;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint PVP_DuelInfoTimeTotal()
		{
			return 0U;
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PvpAct(bool init = false)
		{
			return false;
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.ViewType getBusyCheckType(Engine.ViewType id)
		{
			return Engine.ViewType.Null;
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkFlag(BinaryReader br, List<byte> updateFlag, ref int count)
		{
			return false;
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkFlag(List<byte> flags, ref int count)
		{
			return false;
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x0000216D File Offset: 0x0000036D
		private static void PvpUpdateEngineData(Engine.PvpFieldType type, byte[] data, int n, int player)
		{
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.PvpDialogData PvpParseDialogData(BinaryReader br)
		{
			return null;
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.PvpListData PvpParseListData(BinaryReader br)
		{
			return null;
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.PvpFusionData PvpParseFusionData(BinaryReader br)
		{
			return null;
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PvpParseRecvData(byte[] recvData)
		{
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x0000216D File Offset: 0x0000036D
		private void PvpInit()
		{
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x0000216D File Offset: 0x0000036D
		private void PvpRelease()
		{
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PvpIsQueued()
		{
			return false;
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PvpInputGuard()
		{
			return false;
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x000F5EF0 File Offset: 0x000F40F0
		private static Engine.CardProp THREAD_ToCardPropNoSerial(Engine.CardPropSerial serialCard)
		{
			return default(Engine.CardProp);
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x000F5F08 File Offset: 0x000F4108
		private static Engine.BasicVal THREAD_ToBasicValNoSerial(Engine.BasicValSerial serialVal)
		{
			return default(Engine.BasicVal);
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_PosToIdx(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_PosToUniqueId(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A70 RID: 27248 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.ThreadUIDBase THREAD_PosToUIDBase(int player, int position, int index)
		{
			return null;
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.ThreadUIDBase THREAD_UniqueIdToUIDBase(int uniqueId)
		{
			return null;
		}

		// Token: 0x06006A72 RID: 27250 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelWhichTurnNow()
		{
			return 0;
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.Phase THREAD_DuelGetCurrentPhase()
		{
			return Engine.Phase.Draw;
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.StepType THREAD_DuelGetCurrentStep()
		{
			return Engine.StepType.Null;
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.DmgStepType THREAD_DuelGetCurrentDmgStep()
		{
			return Engine.DmgStepType.Null;
		}

		// Token: 0x06006A76 RID: 27254 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetTurnNum()
		{
			return 0;
		}

		// Token: 0x06006A77 RID: 27255 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetLP(int player)
		{
			return 0;
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelCanIDoPutMonster(int player)
		{
			return 0;
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelCanIDoSummonMonster(int player)
		{
			return false;
		}

		// Token: 0x06006A7A RID: 27258 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelCanIDoSpecialSummon(int player)
		{
			return false;
		}

		// Token: 0x06006A7B RID: 27259 RVA: 0x000F5F20 File Offset: 0x000F4120
		private static Engine.CardProp THREAD_GetCardPropByUniqueID(int uniqueId)
		{
			return default(Engine.CardProp);
		}

		// Token: 0x06006A7C RID: 27260 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetCardID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetCardUniqueID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelGetCardTurn(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelGetCardFace(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetCardNum(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A81 RID: 27265 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelGetThisMonsterFightableOnEffect(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A82 RID: 27266 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelIsThisEquipCard(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelIsThisContinuousCard(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisMagic(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisTrap(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A86 RID: 27270 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetTopCardIndex(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelSearchCardByUniqueID(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DuelGetCardIDByUniqueID(int uniqueId)
		{
			return 0U;
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_DuelGetCardBasicVal(int player, int position, int index, ref Engine.BasicVal val)
		{
		}

		// Token: 0x06006A8A RID: 27274 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisCardExist(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A8B RID: 27275 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetThisCardEffectIDAtChain(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A8C RID: 27276 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetAttackTargetMask(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A8D RID: 27277 RVA: 0x0000216A File Offset: 0x0000036A
		private static Engine.AffectType[][] THREAD_GetFieldAffectIcon(int player, int position)
		{
			return null;
		}

		// Token: 0x06006A8E RID: 27278 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_SetFieldAffectIcon()
		{
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetThisCardCounter(int player, int locate, Engine.CounterType type)
		{
			return 0;
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x0000216A File Offset: 0x0000036A
		private static int[] THREAD_GetThisCardEffectList(int player, int locate)
		{
			return null;
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DUELCOMGetPosMaskOfThisHand(int player, int index, int commandId)
		{
			return 0U;
		}

		// Token: 0x06006A92 RID: 27282 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetThisCardTurnCounter(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A93 RID: 27283 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisNormalMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A94 RID: 27284 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisTrapMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisTunerMonster(int player, int locate)
		{
			return false;
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetThisCardEffectFlags(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetFldMonstOrgLevel(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetFldMonstOrgType(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetFldPendScale(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A9A RID: 27290 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetFldPendOrgScale(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetFldMonstRank(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetFldMonstOrgRank(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetThisCardOverlayNum(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelIsThisZoneAvailable(int player, int locate, bool visibleOnly = false)
		{
			return false;
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.ShowParam THREAD_DuelGetThisCardShowParameter(int player, int locate)
		{
			return Engine.ShowParam.Null;
		}

		// Token: 0x06006AA0 RID: 27296 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DuelGetThisCardParameter(int player, int locate)
		{
			return 0U;
		}

		// Token: 0x06006AA1 RID: 27297 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelGetThisCardDirectFlag(int player, int locate)
		{
			return 0;
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DuelComGetCommandMask(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DuelComGetTextIDOfThisCommand(int player, int position, int index)
		{
			return 0U;
		}

		// Token: 0x06006AA4 RID: 27300 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_ComDoCommand(int player, int position, int index, int commandId)
		{
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_ComCancelCommand(bool decide)
		{
			return false;
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_ComMovePhase(int phase)
		{
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_DuelComDebugCommand()
		{
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_DuelComDoDebugCommand(int player, int position, int index, int commandId)
		{
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_DuelComCheatCard(int player, int position, int index, int cardId, int face, int turn)
		{
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DuelComGetMovablePhase()
		{
			return 0U;
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_DuelDlgSetResult(uint result)
		{
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelDlgGetMixNum()
		{
			return 0;
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.DialogMixTextType THREAD_DuelDlgGetMixType(int index)
		{
			return Engine.DialogMixTextType.Null;
		}

		// Token: 0x06006AAE RID: 27310 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelDlgGetMixData(int index)
		{
			return 0;
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemMsg(int listIdx)
		{
			return 0;
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemMixNum(int listIdx)
		{
			return 0;
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.DialogMixTextType THREAD_DuelListGetItemMixType(int listIdx, int mixIdx)
		{
			return Engine.DialogMixTextType.Null;
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemMixData(int listIdx, int mixIdx)
		{
			return 0;
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelComGetRecommendSide()
		{
			return 0;
		}

		// Token: 0x06006AB4 RID: 27316 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelGetDuelFlagDeckReverse()
		{
			return false;
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelDlgCanYesNoSkip()
		{
			return false;
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint THREAD_DuelDlgGetPosMaskOfThisSummon()
		{
			return 0U;
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelDlgGetSelectItemNum()
		{
			return 0;
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelDlgGetSelectItemStr(int index)
		{
			return 0;
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelDlgGetSelectItemEnable(int index)
		{
			return false;
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DlgProcGetSummoningMonsterUniqueID()
		{
			return 0;
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelListIsMultiMode()
		{
			return false;
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetSelectMax()
		{
			return 0;
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetSelectMin()
		{
			return 0;
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemMax()
		{
			return 0;
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemUniqueID(int index)
		{
			return 0;
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemAttribute(int index)
		{
			return 0;
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemFrom(int index)
		{
			return 0;
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemID(int index)
		{
			return 0;
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetItemTargetUniqueID(int index)
		{
			return 0;
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_DuelListGetCardAttribute(int lookPlayer, int uniqueId)
		{
			return 0;
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_ListSendBlindIndex(int index)
		{
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_ListSendIndex(int index)
		{
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x0000216D File Offset: 0x0000036D
		private static void THREAD_ListSendSelectMulti(int num, List<int> selected)
		{
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x0000216A File Offset: 0x0000036A
		private static int[] THREAD_FusionGetMaterialList()
		{
			return null;
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_FusionIsThisTunedMonsterInTuning()
		{
			return 0;
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int THREAD_FusionGetMonsterLevelInTuning()
		{
			return 0;
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool THREAD_DuelCpuSysCheckFinishAttack()
		{
			return false;
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool THREAD_IsSysActLoopExecute()
		{
			return false;
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ThreadAct(bool init = false)
		{
			return false;
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ThreadActRunEffect(Engine.ToMainRunEffect runeffectData)
		{
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x000029CC File Offset: 0x00000BCC
		private static Engine.ViewType THREAD_getBusyCheckType(Engine.ViewType id)
		{
			return Engine.ViewType.Null;
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x0000216D File Offset: 0x0000036D
		private void ThreadInit()
		{
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ThreadStart()
		{
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ThreadJoin()
		{
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ThreadAbort()
		{
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x0000216D File Offset: 0x0000036D
		private void ThreadRelease()
		{
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isThreadActive()
		{
			return false;
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCpuChangeTimeFool(float time)
		{
		}

		// Token: 0x06006AD7 RID: 27351 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCpuChangeTimeSimple(float time)
		{
		}

		// Token: 0x06006AD8 RID: 27352 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CpuThinkUpdate()
		{
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ThreadUpdate()
		{
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x0000216D File Offset: 0x0000036D
		private static void EngineCommandExecute()
		{
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SurrenderCPU()
		{
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsThreadRunEffectDuelEnd()
		{
			return false;
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ThreadRunEffect(int id, int param1, int param2, int param3)
		{
			return 0;
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x000029CC File Offset: 0x00000BCC
		private int ThreadRunEffectImpl(int id, int param1, int param2, int param3)
		{
			return 0;
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ThreadIsBusyEffect(int id)
		{
			return 0;
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x000029CC File Offset: 0x00000BCC
		private int ThreadIsBusyEffectImpl(int id)
		{
			return 0;
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x0000216A File Offset: 0x0000036A
		private Engine.ThreadMixTextData GetDlgMixVal()
		{
			return null;
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, int> GetAttackFlags()
		{
			return null;
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x0000216A File Offset: 0x0000036A
		private Engine.ThreadIconBase[] GetAffectIcon()
		{
			return null;
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x0000216A File Offset: 0x0000036A
		private Engine.ThreadPosParam[][] GetCardParameter()
		{
			return null;
		}

		// Token: 0x0400A3AF RID: 41903
		public const int CardIdStart = 3000;

		// Token: 0x0400A3B0 RID: 41904
		public const int MaxPlayer = 4;

		// Token: 0x0400A3B1 RID: 41905
		public const int MaxDuelList = 300;

		// Token: 0x0400A3B2 RID: 41906
		public const int PosMonster = 0;

		// Token: 0x0400A3B3 RID: 41907
		public const int PosMonsterLL = 0;

		// Token: 0x0400A3B4 RID: 41908
		public const int PosMonsterL = 1;

		// Token: 0x0400A3B5 RID: 41909
		public const int PosMonsterC = 2;

		// Token: 0x0400A3B6 RID: 41910
		public const int PosMonsterR = 3;

		// Token: 0x0400A3B7 RID: 41911
		public const int PosMonsterRR = 4;

		// Token: 0x0400A3B8 RID: 41912
		public const int PosMonsterMEnd = 4;

		// Token: 0x0400A3B9 RID: 41913
		public const int PosExLMonster = 5;

		// Token: 0x0400A3BA RID: 41914
		public const int PosExRMonster = 6;

		// Token: 0x0400A3BB RID: 41915
		public const int PosMonsterEnd = 6;

		// Token: 0x0400A3BC RID: 41916
		public const int PosMagic = 7;

		// Token: 0x0400A3BD RID: 41917
		public const int PosMagicLL = 7;

		// Token: 0x0400A3BE RID: 41918
		public const int PosMagicL = 8;

		// Token: 0x0400A3BF RID: 41919
		public const int PosMagicC = 9;

		// Token: 0x0400A3C0 RID: 41920
		public const int PosMagicR = 10;

		// Token: 0x0400A3C1 RID: 41921
		public const int PosMagicRR = 11;

		// Token: 0x0400A3C2 RID: 41922
		public const int PosMagicEnd = 11;

		// Token: 0x0400A3C3 RID: 41923
		public const int PosPendulumLeft = 7;

		// Token: 0x0400A3C4 RID: 41924
		public const int PosPendulumRight = 11;

		// Token: 0x0400A3C5 RID: 41925
		public const int PosField = 12;

		// Token: 0x0400A3C6 RID: 41926
		public const int PosHand = 13;

		// Token: 0x0400A3C7 RID: 41927
		public const int PosExtra = 14;

		// Token: 0x0400A3C8 RID: 41928
		public const int PosDeck = 15;

		// Token: 0x0400A3C9 RID: 41929
		public const int PosGrave = 16;

		// Token: 0x0400A3CA RID: 41930
		public const int PosExclude = 17;

		// Token: 0x0400A3CB RID: 41931
		public const int PosSelect = 18;

		// Token: 0x0400A3CC RID: 41932
		public const int PosNum = 18;

		// Token: 0x0400A3CD RID: 41933
		public const int DialogTextMixed = 1;

		// Token: 0x0400A3CE RID: 41934
		private const string LIBNAME = "duel";

		// Token: 0x0400A3CF RID: 41935
		private Engine.CachedParam cachedParam;

		// Token: 0x0400A3D0 RID: 41936
		private static Engine s_instance;

		// Token: 0x0400A3D1 RID: 41937
		private Engine.RunEffect runEffectCallback;

		// Token: 0x0400A3D2 RID: 41938
		private Engine.IsBusyEffect isBusyEffectCallback;

		// Token: 0x0400A3D3 RID: 41939
		private Engine.NowRecord nowRecordCallback;

		// Token: 0x0400A3D4 RID: 41940
		private Engine.RecordNext recordNextCallback;

		// Token: 0x0400A3D5 RID: 41941
		private Engine.RecordBegin recordBeginCallback;

		// Token: 0x0400A3D6 RID: 41942
		private Engine.IsRecordEnd isRecordEndCallback;

		// Token: 0x0400A3D7 RID: 41943
		private Engine.ThreadRunEffectDeleg threadRunEffectCallback;

		// Token: 0x0400A3D8 RID: 41944
		private Engine.ThreadIsBusyEffectDeleg threadIsBusyEffectCallback;

		// Token: 0x0400A3D9 RID: 41945
		private Action updateTimerCallback;

		// Token: 0x0400A3DA RID: 41946
		private Action inputStartCallback;

		// Token: 0x0400A3DB RID: 41947
		private Action noResponseCallback;

		// Token: 0x0400A3DC RID: 41948
		private Action recoveryResponseCallback;

		// Token: 0x0400A3DD RID: 41949
		private Action noResponseClosedCallback;

		// Token: 0x0400A3DE RID: 41950
		private IntPtr engineWork;

		// Token: 0x0400A3DF RID: 41951
		private IntPtr cardRareBufferPtr;

		// Token: 0x0400A3E0 RID: 41952
		private int cardRareBufferSize;

		// Token: 0x0400A3E1 RID: 41953
		private IntPtr cardExistBuffer;

		// Token: 0x0400A3E2 RID: 41954
		private int cardExistBufferSize;

		// Token: 0x0400A3E3 RID: 41955
		private IntPtr questionData;

		// Token: 0x0400A3E4 RID: 41956
		private Dictionary<PvP.Command, Queue<byte[]>> dicRemoteRecvQueue;

		// Token: 0x0400A3E5 RID: 41957
		private Dictionary<PvP.Command, uint> dicRemoteRecvOrder;

		// Token: 0x0400A3E6 RID: 41958
		private uint remoteRecvOrder;

		// Token: 0x0400A3E7 RID: 41959
		private float inputStartTime;

		// Token: 0x0400A3E8 RID: 41960
		private bool isInputNow;

		// Token: 0x0400A3E9 RID: 41961
		private bool isOnlineMode;

		// Token: 0x0400A3EA RID: 41962
		private bool inputTimerSetting;

		// Token: 0x0400A3EB RID: 41963
		private Util.GameMode gameMode;

		// Token: 0x0400A3EC RID: 41964
		private bool isExitPvp;

		// Token: 0x0400A3ED RID: 41965
		private int[] mat;

		// Token: 0x0400A3EE RID: 41966
		private int[] sleeve;

		// Token: 0x0400A3EF RID: 41967
		private int[] latency;

		// Token: 0x0400A3F0 RID: 41968
		private ReplayStream replayStream;

		// Token: 0x0400A3F1 RID: 41969
		private Dictionary<Engine.CounterType, int> dicCounterToId;

		// Token: 0x0400A3F2 RID: 41970
		private Dictionary<int, Engine.CounterType> dicIdToCounter;

		// Token: 0x0400A3F3 RID: 41971
		private int[] initialLP;

		// Token: 0x0400A3F4 RID: 41972
		private Engine.PvpWork pvpWork;

		// Token: 0x0400A3F5 RID: 41973
		private bool pvpFinished;

		// Token: 0x0400A3F6 RID: 41974
		public const int UNTIL_FIELD_ZONE_ONE = 13;

		// Token: 0x0400A3F7 RID: 41975
		public const int UNTIL_FIELD_ZONE_TWO = 26;

		// Token: 0x0400A3F8 RID: 41976
		public const int UNTIL_FIELD_ZONE_ALL = 676;

		// Token: 0x0400A3F9 RID: 41977
		public const int UNTIL_MONSTER_ZONE_ONE = 7;

		// Token: 0x0400A3FA RID: 41978
		public const int MAX_SYSACT_LOOP = 30;

		// Token: 0x0400A3FB RID: 41979
		public const float CPU_CHANGE_TIME_SIMPLE = 4f;

		// Token: 0x0400A3FC RID: 41980
		public const float CPU_CHANGE_TIME_FOOL = 19f;

		// Token: 0x0400A3FD RID: 41981
		private static float CpuChangeTimeSimple;

		// Token: 0x0400A3FE RID: 41982
		private static float CpuChangeTimeFool;

		// Token: 0x0400A3FF RID: 41983
		private static bool s_bSimple;

		// Token: 0x0400A400 RID: 41984
		private static bool s_bFool;

		// Token: 0x0400A401 RID: 41985
		public static BlockingQueue<Engine.ToMainWork> ToMainQueue;

		// Token: 0x0400A402 RID: 41986
		public static BlockingQueue<Engine.ToEngineWork> ToEngineQueue;

		// Token: 0x0400A403 RID: 41987
		public static bool forceSurrender;

		// Token: 0x0400A404 RID: 41988
		private static Thread DuelEngineThread;

		// Token: 0x0400A405 RID: 41989
		private const int MIX_BUFF_SIZE_MAX = 8;

		// Token: 0x0400A406 RID: 41990
		private Engine.ThreadWork threadWork;

		// Token: 0x0400A407 RID: 41991
		private static bool isBusyCheck;

		// Token: 0x0400A408 RID: 41992
		private static bool isThreadFinished;

		// Token: 0x0400A409 RID: 41993
		private static bool isSurrender;

		// Token: 0x0400A40A RID: 41994
		private static bool isThreadRunEffectDuelEnd;

		// Token: 0x0400A40B RID: 41995
		private static Engine.ThreadIconBuff[] buffAffectIcon;

		// Token: 0x0400A40C RID: 41996
		private static Engine.ThreadPosParam[][] buffPosParam;

		// Token: 0x02000E2E RID: 3630
		public enum PlayerType
		{
			// Token: 0x0400A40E RID: 41998
			Human,
			// Token: 0x0400A40F RID: 41999
			CPU,
			// Token: 0x0400A410 RID: 42000
			Remote,
			// Token: 0x0400A411 RID: 42001
			Replay,
			// Token: 0x0400A412 RID: 42002
			Replay2,
			// Token: 0x0400A413 RID: 42003
			None = -1
		}

		// Token: 0x02000E2F RID: 3631
		public enum DuelType
		{
			// Token: 0x0400A415 RID: 42005
			Normal,
			// Token: 0x0400A416 RID: 42006
			Extra,
			// Token: 0x0400A417 RID: 42007
			Tag,
			// Token: 0x0400A418 RID: 42008
			Quick,
			// Token: 0x0400A419 RID: 42009
			Rush
		}

		// Token: 0x02000E30 RID: 3632
		public enum ResultType
		{
			// Token: 0x0400A41B RID: 42011
			None,
			// Token: 0x0400A41C RID: 42012
			Win,
			// Token: 0x0400A41D RID: 42013
			Lose,
			// Token: 0x0400A41E RID: 42014
			Draw,
			// Token: 0x0400A41F RID: 42015
			Time
		}

		// Token: 0x02000E31 RID: 3633
		public enum FinishType
		{
			// Token: 0x0400A421 RID: 42017
			None,
			// Token: 0x0400A422 RID: 42018
			Normal,
			// Token: 0x0400A423 RID: 42019
			NoDeck,
			// Token: 0x0400A424 RID: 42020
			TimeOut,
			// Token: 0x0400A425 RID: 42021
			Surrender,
			// Token: 0x0400A426 RID: 42022
			Failed,
			// Token: 0x0400A427 RID: 42023
			Exodia,
			// Token: 0x0400A428 RID: 42024
			Vija,
			// Token: 0x0400A429 RID: 42025
			YataLock,
			// Token: 0x0400A42A RID: 42026
			LastBattle,
			// Token: 0x0400A42B RID: 42027
			CountDown,
			// Token: 0x0400A42C RID: 42028
			Victory,
			// Token: 0x0400A42D RID: 42029
			Venom,
			// Token: 0x0400A42E RID: 42030
			Exodios,
			// Token: 0x0400A42F RID: 42031
			God,
			// Token: 0x0400A430 RID: 42032
			Gimmick,
			// Token: 0x0400A431 RID: 42033
			Gimmick2,
			// Token: 0x0400A432 RID: 42034
			Jackpot7,
			// Token: 0x0400A433 RID: 42035
			Miracle,
			// Token: 0x0400A434 RID: 42036
			RelaySoul,
			// Token: 0x0400A435 RID: 42037
			Ghostrick,
			// Token: 0x0400A436 RID: 42038
			Genohryu,
			// Token: 0x0400A437 RID: 42039
			Winners,
			// Token: 0x0400A438 RID: 42040
			Elephant,
			// Token: 0x0400A439 RID: 42041
			Exodia2,
			// Token: 0x0400A43A RID: 42042
			Exodia3,
			// Token: 0x0400A43B RID: 42043
			CiNo1000,
			// Token: 0x0400A43C RID: 42044
			Sekitori,
			// Token: 0x0400A43D RID: 42045
			FinishError = 100,
			// Token: 0x0400A43E RID: 42046
			FinishDisconnect,
			// Token: 0x0400A43F RID: 42047
			FinishNoContest,
			// Token: 0x0400A440 RID: 42048
			FinishEngineCrash = 105
		}

		// Token: 0x02000E32 RID: 3634
		public enum LimitedType
		{
			// Token: 0x0400A442 RID: 42050
			None,
			// Token: 0x0400A443 RID: 42051
			NormalSummon,
			// Token: 0x0400A444 RID: 42052
			SpecialSummon,
			// Token: 0x0400A445 RID: 42053
			Set,
			// Token: 0x0400A446 RID: 42054
			Tribute,
			// Token: 0x0400A447 RID: 42055
			ChangePos,
			// Token: 0x0400A448 RID: 42056
			Attack,
			// Token: 0x0400A449 RID: 42057
			Draw2,
			// Token: 0x0400A44A RID: 42058
			Turn20,
			// Token: 0x0400A44B RID: 42059
			Damage,
			// Token: 0x0400A44C RID: 42060
			Beginner,
			// Token: 0x0400A44D RID: 42061
			Beginner2,
			// Token: 0x0400A44E RID: 42062
			Vs2on1,
			// Token: 0x0400A44F RID: 42063
			Vs2on1_Hand,
			// Token: 0x0400A450 RID: 42064
			FirstDraw,
			// Token: 0x0400A451 RID: 42065
			Vs3on1,
			// Token: 0x0400A452 RID: 42066
			Survival_1on3 = 256,
			// Token: 0x0400A453 RID: 42067
			Survival_3on3,
			// Token: 0x0400A454 RID: 42068
			Survival_1on2
		}

		// Token: 0x02000E33 RID: 3635
		public enum TagType
		{
			// Token: 0x0400A456 RID: 42070
			Single,
			// Token: 0x0400A457 RID: 42071
			Tag,
			// Token: 0x0400A458 RID: 42072
			Team
		}

		// Token: 0x02000E34 RID: 3636
		public enum Phase
		{
			// Token: 0x0400A45A RID: 42074
			Draw,
			// Token: 0x0400A45B RID: 42075
			Standby,
			// Token: 0x0400A45C RID: 42076
			Main1,
			// Token: 0x0400A45D RID: 42077
			Battle,
			// Token: 0x0400A45E RID: 42078
			Main2,
			// Token: 0x0400A45F RID: 42079
			End,
			// Token: 0x0400A460 RID: 42080
			Null = 7
		}

		// Token: 0x02000E35 RID: 3637
		public enum StepType
		{
			// Token: 0x0400A462 RID: 42082
			Null,
			// Token: 0x0400A463 RID: 42083
			Start,
			// Token: 0x0400A464 RID: 42084
			Battle,
			// Token: 0x0400A465 RID: 42085
			Damage,
			// Token: 0x0400A466 RID: 42086
			End
		}

		// Token: 0x02000E36 RID: 3638
		public enum DmgStepType
		{
			// Token: 0x0400A468 RID: 42088
			Null,
			// Token: 0x0400A469 RID: 42089
			Start,
			// Token: 0x0400A46A RID: 42090
			BeforeCalc,
			// Token: 0x0400A46B RID: 42091
			DamageCalc,
			// Token: 0x0400A46C RID: 42092
			AfterCalc,
			// Token: 0x0400A46D RID: 42093
			End
		}

		// Token: 0x02000E37 RID: 3639
		public enum CounterType
		{
			// Token: 0x0400A46F RID: 42095
			Magic,
			// Token: 0x0400A470 RID: 42096
			Normal,
			// Token: 0x0400A471 RID: 42097
			Clock,
			// Token: 0x0400A472 RID: 42098
			Hyper,
			// Token: 0x0400A473 RID: 42099
			Gem,
			// Token: 0x0400A474 RID: 42100
			Chronicle,
			// Token: 0x0400A475 RID: 42101
			Bushido,
			// Token: 0x0400A476 RID: 42102
			D,
			// Token: 0x0400A477 RID: 42103
			Shine,
			// Token: 0x0400A478 RID: 42104
			Gate,
			// Token: 0x0400A479 RID: 42105
			Worm,
			// Token: 0x0400A47A RID: 42106
			Deformer,
			// Token: 0x0400A47B RID: 42107
			Flower,
			// Token: 0x0400A47C RID: 42108
			Plant,
			// Token: 0x0400A47D RID: 42109
			Psycho,
			// Token: 0x0400A47E RID: 42110
			EarthBind,
			// Token: 0x0400A47F RID: 42111
			Junk,
			// Token: 0x0400A480 RID: 42112
			Genex,
			// Token: 0x0400A481 RID: 42113
			Dragonic,
			// Token: 0x0400A482 RID: 42114
			Ocean,
			// Token: 0x0400A483 RID: 42115
			BF,
			// Token: 0x0400A484 RID: 42116
			Death,
			// Token: 0x0400A485 RID: 42117
			Karakuri,
			// Token: 0x0400A486 RID: 42118
			Stone,
			// Token: 0x0400A487 RID: 42119
			Thunder,
			// Token: 0x0400A488 RID: 42120
			Donguri,
			// Token: 0x0400A489 RID: 42121
			Greed,
			// Token: 0x0400A48A RID: 42122
			Chaos,
			// Token: 0x0400A48B RID: 42123
			Double,
			// Token: 0x0400A48C RID: 42124
			Destiny,
			// Token: 0x0400A48D RID: 42125
			Orbital,
			// Token: 0x0400A48E RID: 42126
			Shark,
			// Token: 0x0400A48F RID: 42127
			Pumpkin,
			// Token: 0x0400A490 RID: 42128
			HopeSlash,
			// Token: 0x0400A491 RID: 42129
			Kattobing,
			// Token: 0x0400A492 RID: 42130
			Balloon,
			// Token: 0x0400A493 RID: 42131
			Yosen,
			// Token: 0x0400A494 RID: 42132
			Sound,
			// Token: 0x0400A495 RID: 42133
			Em,
			// Token: 0x0400A496 RID: 42134
			Kaiju,
			// Token: 0x0400A497 RID: 42135
			Defect,
			// Token: 0x0400A498 RID: 42136
			Athlete,
			// Token: 0x0400A499 RID: 42137
			Barrel,
			// Token: 0x0400A49A RID: 42138
			Summon,
			// Token: 0x0400A49B RID: 42139
			FireStar,
			// Token: 0x0400A49C RID: 42140
			Phantasm,
			// Token: 0x0400A49D RID: 42141
			Otoshidama,
			// Token: 0x0400A49E RID: 42142
			Ounokagi,
			// Token: 0x0400A49F RID: 42143
			Piece,
			// Token: 0x0400A4A0 RID: 42144
			Girl,
			// Token: 0x0400A4A1 RID: 42145
			Gardna,
			// Token: 0x0400A4A2 RID: 42146
			Alien,
			// Token: 0x0400A4A3 RID: 42147
			Ice,
			// Token: 0x0400A4A4 RID: 42148
			Venom,
			// Token: 0x0400A4A5 RID: 42149
			Fog,
			// Token: 0x0400A4A6 RID: 42150
			Guard,
			// Token: 0x0400A4A7 RID: 42151
			Wedge,
			// Token: 0x0400A4A8 RID: 42152
			Guard2,
			// Token: 0x0400A4A9 RID: 42153
			String,
			// Token: 0x0400A4AA RID: 42154
			Houkai,
			// Token: 0x0400A4AB RID: 42155
			Zushin,
			// Token: 0x0400A4AC RID: 42156
			Predator,
			// Token: 0x0400A4AD RID: 42157
			Scales,
			// Token: 0x0400A4AE RID: 42158
			Police,
			// Token: 0x0400A4AF RID: 42159
			Signal,
			// Token: 0x0400A4B0 RID: 42160
			Venemy,
			// Token: 0x0400A4B1 RID: 42161
			Burn,
			// Token: 0x0400A4B2 RID: 42162
			Illusion,
			// Token: 0x0400A4B3 RID: 42163
			GG,
			// Token: 0x0400A4B4 RID: 42164
			Rabbit,
			// Token: 0x0400A4B5 RID: 42165
			Kyoumei,
			// Token: 0x0400A4B6 RID: 42166
			Max
		}

		// Token: 0x02000E38 RID: 3640
		public enum ViewType
		{
			// Token: 0x0400A4B8 RID: 42168
			Noop = -1,
			// Token: 0x0400A4B9 RID: 42169
			Null,
			// Token: 0x0400A4BA RID: 42170
			DuelStart,
			// Token: 0x0400A4BB RID: 42171
			DuelEnd,
			// Token: 0x0400A4BC RID: 42172
			WaitFrame,
			// Token: 0x0400A4BD RID: 42173
			WaitInput,
			// Token: 0x0400A4BE RID: 42174
			PhaseChange,
			// Token: 0x0400A4BF RID: 42175
			TurnChange,
			// Token: 0x0400A4C0 RID: 42176
			FieldChange,
			// Token: 0x0400A4C1 RID: 42177
			CursorSet,
			// Token: 0x0400A4C2 RID: 42178
			BgmUpdate,
			// Token: 0x0400A4C3 RID: 42179
			BattleInit,
			// Token: 0x0400A4C4 RID: 42180
			BattleSelect,
			// Token: 0x0400A4C5 RID: 42181
			BattleAttack,
			// Token: 0x0400A4C6 RID: 42182
			BattleRun,
			// Token: 0x0400A4C7 RID: 42183
			BattleEnd,
			// Token: 0x0400A4C8 RID: 42184
			LifeSet,
			// Token: 0x0400A4C9 RID: 42185
			LifeDamage,
			// Token: 0x0400A4CA RID: 42186
			LifeReset,
			// Token: 0x0400A4CB RID: 42187
			HandShuffle,
			// Token: 0x0400A4CC RID: 42188
			HandShow,
			// Token: 0x0400A4CD RID: 42189
			HandOpen,
			// Token: 0x0400A4CE RID: 42190
			DeckShuffle,
			// Token: 0x0400A4CF RID: 42191
			DeckReset,
			// Token: 0x0400A4D0 RID: 42192
			DeckFlipTop,
			// Token: 0x0400A4D1 RID: 42193
			GraveTop,
			// Token: 0x0400A4D2 RID: 42194
			CardLockon,
			// Token: 0x0400A4D3 RID: 42195
			CardMove,
			// Token: 0x0400A4D4 RID: 42196
			CardSwap,
			// Token: 0x0400A4D5 RID: 42197
			CardFlipTurn,
			// Token: 0x0400A4D6 RID: 42198
			CardCheat,
			// Token: 0x0400A4D7 RID: 42199
			CardSet,
			// Token: 0x0400A4D8 RID: 42200
			CardVanish,
			// Token: 0x0400A4D9 RID: 42201
			CardBreak,
			// Token: 0x0400A4DA RID: 42202
			CardExplosion,
			// Token: 0x0400A4DB RID: 42203
			CardExclude,
			// Token: 0x0400A4DC RID: 42204
			CardHappen,
			// Token: 0x0400A4DD RID: 42205
			CardDisable,
			// Token: 0x0400A4DE RID: 42206
			CardEquip,
			// Token: 0x0400A4DF RID: 42207
			CardIncTurn,
			// Token: 0x0400A4E0 RID: 42208
			CardUpdate,
			// Token: 0x0400A4E1 RID: 42209
			ManaSet,
			// Token: 0x0400A4E2 RID: 42210
			MonstDeathTurn,
			// Token: 0x0400A4E3 RID: 42211
			MonstShuffle,
			// Token: 0x0400A4E4 RID: 42212
			TributeSet,
			// Token: 0x0400A4E5 RID: 42213
			TributeReset,
			// Token: 0x0400A4E6 RID: 42214
			TributeRun,
			// Token: 0x0400A4E7 RID: 42215
			MaterialSet,
			// Token: 0x0400A4E8 RID: 42216
			MaterialReset,
			// Token: 0x0400A4E9 RID: 42217
			MaterialRun,
			// Token: 0x0400A4EA RID: 42218
			TuningSet,
			// Token: 0x0400A4EB RID: 42219
			TuningReset,
			// Token: 0x0400A4EC RID: 42220
			TuningRun,
			// Token: 0x0400A4ED RID: 42221
			ChainSet,
			// Token: 0x0400A4EE RID: 42222
			ChainRun,
			// Token: 0x0400A4EF RID: 42223
			RunSurrender,
			// Token: 0x0400A4F0 RID: 42224
			RunDialog,
			// Token: 0x0400A4F1 RID: 42225
			RunList,
			// Token: 0x0400A4F2 RID: 42226
			RunSummon,
			// Token: 0x0400A4F3 RID: 42227
			RunSpSummon,
			// Token: 0x0400A4F4 RID: 42228
			RunFusion,
			// Token: 0x0400A4F5 RID: 42229
			RunDetail,
			// Token: 0x0400A4F6 RID: 42230
			RunCoin,
			// Token: 0x0400A4F7 RID: 42231
			RunDice,
			// Token: 0x0400A4F8 RID: 42232
			RunYujyo,
			// Token: 0x0400A4F9 RID: 42233
			RunSpecialWin,
			// Token: 0x0400A4FA RID: 42234
			RunVija,
			// Token: 0x0400A4FB RID: 42235
			RunExtra,
			// Token: 0x0400A4FC RID: 42236
			RunCommand,
			// Token: 0x0400A4FD RID: 42237
			CutinDraw,
			// Token: 0x0400A4FE RID: 42238
			CutinSummon,
			// Token: 0x0400A4FF RID: 42239
			CutinFusion,
			// Token: 0x0400A500 RID: 42240
			CutinChain,
			// Token: 0x0400A501 RID: 42241
			CutinActivate,
			// Token: 0x0400A502 RID: 42242
			CutinSet,
			// Token: 0x0400A503 RID: 42243
			CutinReverse,
			// Token: 0x0400A504 RID: 42244
			CutinTurn,
			// Token: 0x0400A505 RID: 42245
			CutinFlip,
			// Token: 0x0400A506 RID: 42246
			CutinTurnEnd,
			// Token: 0x0400A507 RID: 42247
			CutinDamage,
			// Token: 0x0400A508 RID: 42248
			CutinBreak,
			// Token: 0x0400A509 RID: 42249
			CpuThinking,
			// Token: 0x0400A50A RID: 42250
			HandRundom,
			// Token: 0x0400A50B RID: 42251
			OverlaySet,
			// Token: 0x0400A50C RID: 42252
			OverlayReset,
			// Token: 0x0400A50D RID: 42253
			OverlayRun,
			// Token: 0x0400A50E RID: 42254
			CutinSuccess,
			// Token: 0x0400A50F RID: 42255
			ChainEnd,
			// Token: 0x0400A510 RID: 42256
			LinkSet,
			// Token: 0x0400A511 RID: 42257
			LinkReset,
			// Token: 0x0400A512 RID: 42258
			LinkRun,
			// Token: 0x0400A513 RID: 42259
			RunJanken,
			// Token: 0x0400A514 RID: 42260
			CutinCoinDice,
			// Token: 0x0400A515 RID: 42261
			ChainStep,
			// Token: 0x0400A516 RID: 42262
			RunSpecialefx
		}

		// Token: 0x02000E39 RID: 3641
		public enum FieldAnimeType
		{
			// Token: 0x0400A518 RID: 42264
			Null,
			// Token: 0x0400A519 RID: 42265
			CardMove,
			// Token: 0x0400A51A RID: 42266
			CardWarp,
			// Token: 0x0400A51B RID: 42267
			CardSwap
		}

		// Token: 0x02000E3A RID: 3642
		public enum CardMoveType
		{
			// Token: 0x0400A51D RID: 42269
			Normal,
			// Token: 0x0400A51E RID: 42270
			Normal2,
			// Token: 0x0400A51F RID: 42271
			Summon,
			// Token: 0x0400A520 RID: 42272
			SpSummon,
			// Token: 0x0400A521 RID: 42273
			Activate,
			// Token: 0x0400A522 RID: 42274
			Set,
			// Token: 0x0400A523 RID: 42275
			Break,
			// Token: 0x0400A524 RID: 42276
			Explosion,
			// Token: 0x0400A525 RID: 42277
			Sacrifice,
			// Token: 0x0400A526 RID: 42278
			Draw,
			// Token: 0x0400A527 RID: 42279
			Drop,
			// Token: 0x0400A528 RID: 42280
			Search,
			// Token: 0x0400A529 RID: 42281
			Used,
			// Token: 0x0400A52A RID: 42282
			Put,
			// Token: 0x0400A52B RID: 42283
			Normal3
		}

		// Token: 0x02000E3B RID: 3643
		public struct CardProp
		{
			// Token: 0x17000BE0 RID: 3040
			// (get) Token: 0x06006AE6 RID: 27366 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AE7 RID: 27367 RVA: 0x0000216D File Offset: 0x0000036D
			public ushort cardId
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

			// Token: 0x17000BE1 RID: 3041
			// (get) Token: 0x06006AE8 RID: 27368 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AE9 RID: 27369 RVA: 0x0000216D File Offset: 0x0000036D
			public ushort uniqueId
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

			// Token: 0x17000BE2 RID: 3042
			// (get) Token: 0x06006AEA RID: 27370 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AEB RID: 27371 RVA: 0x0000216D File Offset: 0x0000036D
			public ushort flags
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

			// Token: 0x17000BE3 RID: 3043
			// (get) Token: 0x06006AEC RID: 27372 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AED RID: 27373 RVA: 0x0000216D File Offset: 0x0000036D
			public int CardID
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x17000BE4 RID: 3044
			// (get) Token: 0x06006AEE RID: 27374 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AEF RID: 27375 RVA: 0x0000216D File Offset: 0x0000036D
			public int UniqueID
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x17000BE5 RID: 3045
			// (get) Token: 0x06006AF0 RID: 27376 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x0000216D File Offset: 0x0000036D
			public int Owner
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x17000BE6 RID: 3046
			// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AF3 RID: 27379 RVA: 0x0000216D File Offset: 0x0000036D
			public bool Correct
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BE7 RID: 3047
			// (get) Token: 0x06006AF4 RID: 27380 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AF5 RID: 27381 RVA: 0x0000216D File Offset: 0x0000036D
			public bool ByBattle
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BE8 RID: 3048
			// (get) Token: 0x06006AF6 RID: 27382 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AF7 RID: 27383 RVA: 0x0000216D File Offset: 0x0000036D
			public bool ByAnother
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BE9 RID: 3049
			// (get) Token: 0x06006AF8 RID: 27384 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AF9 RID: 27385 RVA: 0x0000216D File Offset: 0x0000036D
			public bool ByBreak
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BEA RID: 3050
			// (get) Token: 0x06006AFA RID: 27386 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AFB RID: 27387 RVA: 0x0000216D File Offset: 0x0000036D
			public bool TurnPast
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BEB RID: 3051
			// (get) Token: 0x06006AFC RID: 27388 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AFD RID: 27389 RVA: 0x0000216D File Offset: 0x0000036D
			public bool Disabled
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BEC RID: 3052
			// (get) Token: 0x06006AFE RID: 27390 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006AFF RID: 27391 RVA: 0x0000216D File Offset: 0x0000036D
			public bool TimingPast
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x06006B00 RID: 27392 RVA: 0x0000216D File Offset: 0x0000036D
			public CardProp(uint param)
			{
			}
		}

		// Token: 0x02000E3C RID: 3644
		public struct CardStatus
		{
			// Token: 0x17000BED RID: 3053
			// (get) Token: 0x06006B01 RID: 27393 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B02 RID: 27394 RVA: 0x0000216D File Offset: 0x0000036D
			public int Player
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x17000BEE RID: 3054
			// (get) Token: 0x06006B03 RID: 27395 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B04 RID: 27396 RVA: 0x0000216D File Offset: 0x0000036D
			public int Position
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x17000BEF RID: 3055
			// (get) Token: 0x06006B05 RID: 27397 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B06 RID: 27398 RVA: 0x0000216D File Offset: 0x0000036D
			public int Index
			{
				get
				{
					return 0;
				}
				set
				{
				}
			}

			// Token: 0x17000BF0 RID: 3056
			// (get) Token: 0x06006B07 RID: 27399 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B08 RID: 27400 RVA: 0x0000216D File Offset: 0x0000036D
			public bool Face
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000BF1 RID: 3057
			// (get) Token: 0x06006B09 RID: 27401 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B0A RID: 27402 RVA: 0x0000216D File Offset: 0x0000036D
			public bool Turn
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x06006B0B RID: 27403 RVA: 0x000029CC File Offset: 0x00000BCC
			public int ToInt()
			{
				return 0;
			}

			// Token: 0x0400A52C RID: 42284
			private BitVector32.Section player;

			// Token: 0x0400A52D RID: 42285
			private BitVector32.Section position;

			// Token: 0x0400A52E RID: 42286
			private BitVector32.Section index;

			// Token: 0x0400A52F RID: 42287
			private BitVector32.Section face;

			// Token: 0x0400A530 RID: 42288
			private BitVector32.Section turn;

			// Token: 0x0400A531 RID: 42289
			private BitVector32 bitVec;
		}

		// Token: 0x02000E3D RID: 3645
		public struct BasicVal
		{
			// Token: 0x0400A532 RID: 42290
			public short CardID;

			// Token: 0x0400A533 RID: 42291
			public short EffectID;

			// Token: 0x0400A534 RID: 42292
			public int Atk;

			// Token: 0x0400A535 RID: 42293
			public int Def;

			// Token: 0x0400A536 RID: 42294
			public int OrgAtk;

			// Token: 0x0400A537 RID: 42295
			public int OrgDef;

			// Token: 0x0400A538 RID: 42296
			public short Type;

			// Token: 0x0400A539 RID: 42297
			public short Attr;

			// Token: 0x0400A53A RID: 42298
			public short Element;

			// Token: 0x0400A53B RID: 42299
			public short Level;

			// Token: 0x0400A53C RID: 42300
			public byte Rank;

			// Token: 0x0400A53D RID: 42301
			public byte VoidMagic;

			// Token: 0x0400A53E RID: 42302
			public byte VoidTrap;

			// Token: 0x0400A53F RID: 42303
			public byte VoidMonst;
		}

		// Token: 0x02000E3E RID: 3646
		public enum MenuActType
		{
			// Token: 0x0400A541 RID: 42305
			Null,
			// Token: 0x0400A542 RID: 42306
			DrawPhase,
			// Token: 0x0400A543 RID: 42307
			MainPhase,
			// Token: 0x0400A544 RID: 42308
			BattlePhase,
			// Token: 0x0400A545 RID: 42309
			CheckTiming,
			// Token: 0x0400A546 RID: 42310
			CheckChain,
			// Token: 0x0400A547 RID: 42311
			SummonChance,
			// Token: 0x0400A548 RID: 42312
			Location,
			// Token: 0x0400A549 RID: 42313
			Selection,
			// Token: 0x0400A54A RID: 42314
			LockOn
		}

		// Token: 0x02000E3F RID: 3647
		public enum MenuParamType
		{
			// Token: 0x0400A54C RID: 42316
			Force = -1,
			// Token: 0x0400A54D RID: 42317
			Cancel,
			// Token: 0x0400A54E RID: 42318
			Decide,
			// Token: 0x0400A54F RID: 42319
			TrueCancel,
			// Token: 0x0400A550 RID: 42320
			OnlyCancel,
			// Token: 0x0400A551 RID: 42321
			DecideCancel
		}

		// Token: 0x02000E40 RID: 3648
		public enum CommandType
		{
			// Token: 0x0400A553 RID: 42323
			Attack,
			// Token: 0x0400A554 RID: 42324
			Look,
			// Token: 0x0400A555 RID: 42325
			SummonSp,
			// Token: 0x0400A556 RID: 42326
			Action,
			// Token: 0x0400A557 RID: 42327
			Summon,
			// Token: 0x0400A558 RID: 42328
			Reverse,
			// Token: 0x0400A559 RID: 42329
			SetMonst,
			// Token: 0x0400A55A RID: 42330
			Set,
			// Token: 0x0400A55B RID: 42331
			Pendulum,
			// Token: 0x0400A55C RID: 42332
			TurnAtk,
			// Token: 0x0400A55D RID: 42333
			TurnDef,
			// Token: 0x0400A55E RID: 42334
			Surrender,
			// Token: 0x0400A55F RID: 42335
			Decide,
			// Token: 0x0400A560 RID: 42336
			Draw
		}

		// Token: 0x02000E41 RID: 3649
		[Flags]
		public enum CommandBit
		{
			// Token: 0x0400A562 RID: 42338
			Attack = 1,
			// Token: 0x0400A563 RID: 42339
			Look = 2,
			// Token: 0x0400A564 RID: 42340
			SummonSp = 4,
			// Token: 0x0400A565 RID: 42341
			Action = 8,
			// Token: 0x0400A566 RID: 42342
			Summon = 16,
			// Token: 0x0400A567 RID: 42343
			Reverse = 32,
			// Token: 0x0400A568 RID: 42344
			SetMonst = 64,
			// Token: 0x0400A569 RID: 42345
			Set = 128,
			// Token: 0x0400A56A RID: 42346
			Pendulum = 256,
			// Token: 0x0400A56B RID: 42347
			TurnAtk = 512,
			// Token: 0x0400A56C RID: 42348
			TurnDef = 1024,
			// Token: 0x0400A56D RID: 42349
			Surrender = 2048,
			// Token: 0x0400A56E RID: 42350
			Decide = 4096,
			// Token: 0x0400A56F RID: 42351
			Draw = 8192
		}

		// Token: 0x02000E42 RID: 3650
		public enum CardLink
		{
			// Token: 0x0400A571 RID: 42353
			UL,
			// Token: 0x0400A572 RID: 42354
			U,
			// Token: 0x0400A573 RID: 42355
			UR,
			// Token: 0x0400A574 RID: 42356
			L,
			// Token: 0x0400A575 RID: 42357
			R,
			// Token: 0x0400A576 RID: 42358
			DL,
			// Token: 0x0400A577 RID: 42359
			D,
			// Token: 0x0400A578 RID: 42360
			DR
		}

		// Token: 0x02000E43 RID: 3651
		[Flags]
		public enum CardLinkBit
		{
			// Token: 0x0400A57A RID: 42362
			UL = 1,
			// Token: 0x0400A57B RID: 42363
			U = 2,
			// Token: 0x0400A57C RID: 42364
			UR = 4,
			// Token: 0x0400A57D RID: 42365
			L = 8,
			// Token: 0x0400A57E RID: 42366
			R = 16,
			// Token: 0x0400A57F RID: 42367
			DL = 32,
			// Token: 0x0400A580 RID: 42368
			D = 64,
			// Token: 0x0400A581 RID: 42369
			DR = 128
		}

		// Token: 0x02000E44 RID: 3652
		public enum ShowParam
		{
			// Token: 0x0400A583 RID: 42371
			Null,
			// Token: 0x0400A584 RID: 42372
			Type,
			// Token: 0x0400A585 RID: 42373
			Attr,
			// Token: 0x0400A586 RID: 42374
			Card,
			// Token: 0x0400A587 RID: 42375
			Num,
			// Token: 0x0400A588 RID: 42376
			AttrMask
		}

		// Token: 0x02000E45 RID: 3653
		public enum DamageType
		{
			// Token: 0x0400A58A RID: 42378
			ByEffect,
			// Token: 0x0400A58B RID: 42379
			ByBattle,
			// Token: 0x0400A58C RID: 42380
			ByCost,
			// Token: 0x0400A58D RID: 42381
			ByLost,
			// Token: 0x0400A58E RID: 42382
			Recover
		}

		// Token: 0x02000E46 RID: 3654
		[Flags]
		public enum BtlPropFlag
		{
			// Token: 0x0400A590 RID: 42384
			Turn = 1,
			// Token: 0x0400A591 RID: 42385
			Break = 2,
			// Token: 0x0400A592 RID: 42386
			Damage = 4
		}

		// Token: 0x02000E47 RID: 3655
		public enum AffectType
		{
			// Token: 0x0400A594 RID: 42388
			Null,
			// Token: 0x0400A595 RID: 42389
			Equip,
			// Token: 0x0400A596 RID: 42390
			Permanent,
			// Token: 0x0400A597 RID: 42391
			Field,
			// Token: 0x0400A598 RID: 42392
			Bind,
			// Token: 0x0400A599 RID: 42393
			Power,
			// Token: 0x0400A59A RID: 42394
			Target,
			// Token: 0x0400A59B RID: 42395
			Disable,
			// Token: 0x0400A59C RID: 42396
			Chain = 256
		}

		// Token: 0x02000E48 RID: 3656
		public enum SpSummonType
		{
			// Token: 0x0400A59E RID: 42398
			Fusion,
			// Token: 0x0400A59F RID: 42399
			SpFusion,
			// Token: 0x0400A5A0 RID: 42400
			Synchro,
			// Token: 0x0400A5A1 RID: 42401
			Ritual,
			// Token: 0x0400A5A2 RID: 42402
			Xyz,
			// Token: 0x0400A5A3 RID: 42403
			Pendulum,
			// Token: 0x0400A5A4 RID: 42404
			Link
		}

		// Token: 0x02000E49 RID: 3657
		public enum CutinSummonType
		{
			// Token: 0x0400A5A6 RID: 42406
			Normal,
			// Token: 0x0400A5A7 RID: 42407
			Release1,
			// Token: 0x0400A5A8 RID: 42408
			Release2,
			// Token: 0x0400A5A9 RID: 42409
			Release3,
			// Token: 0x0400A5AA RID: 42410
			Reverse,
			// Token: 0x0400A5AB RID: 42411
			SpByEffect,
			// Token: 0x0400A5AC RID: 42412
			SpNormal,
			// Token: 0x0400A5AD RID: 42413
			ReSummon,
			// Token: 0x0400A5AE RID: 42414
			PreSynchro,
			// Token: 0x0400A5AF RID: 42415
			PreXyz,
			// Token: 0x0400A5B0 RID: 42416
			PrePendulum,
			// Token: 0x0400A5B1 RID: 42417
			Link
		}

		// Token: 0x02000E4A RID: 3658
		public enum CutinActivateType
		{
			// Token: 0x0400A5B3 RID: 42419
			NoChain,
			// Token: 0x0400A5B4 RID: 42420
			FromField,
			// Token: 0x0400A5B5 RID: 42421
			FromHand,
			// Token: 0x0400A5B6 RID: 42422
			Activate,
			// Token: 0x0400A5B7 RID: 42423
			Effect,
			// Token: 0x0400A5B8 RID: 42424
			FldGrave,
			// Token: 0x0400A5B9 RID: 42425
			CostEffect
		}

		// Token: 0x02000E4B RID: 3659
		[Flags]
		public enum CpuParam : uint
		{
			// Token: 0x0400A5BB RID: 42427
			None = 0U,
			// Token: 0x0400A5BC RID: 42428
			Def = 2147483648U,
			// Token: 0x0400A5BD RID: 42429
			Fool = 1073741824U,
			// Token: 0x0400A5BE RID: 42430
			Light = 536870912U,
			// Token: 0x0400A5BF RID: 42431
			MyTurnOnly = 268435456U,
			// Token: 0x0400A5C0 RID: 42432
			AttackOnly = 67108864U,
			// Token: 0x0400A5C1 RID: 42433
			Simple = 33554432U,
			// Token: 0x0400A5C2 RID: 42434
			Simple2 = 16777216U,
			// Token: 0x0400A5C3 RID: 42435
			Simples = 50331648U
		}

		// Token: 0x02000E4C RID: 3660
		public enum RunCommandType
		{
			// Token: 0x0400A5C5 RID: 42437
			Null,
			// Token: 0x0400A5C6 RID: 42438
			PriWaitInput,
			// Token: 0x0400A5C7 RID: 42439
			PriCpuThinking,
			// Token: 0x0400A5C8 RID: 42440
			PriRunDialog,
			// Token: 0x0400A5C9 RID: 42441
			PriRunList
		}

		// Token: 0x02000E4D RID: 3661
		public enum DialogType
		{
			// Token: 0x0400A5CB RID: 42443
			Ok,
			// Token: 0x0400A5CC RID: 42444
			Info,
			// Token: 0x0400A5CD RID: 42445
			Confirm,
			// Token: 0x0400A5CE RID: 42446
			YesNo,
			// Token: 0x0400A5CF RID: 42447
			Effect,
			// Token: 0x0400A5D0 RID: 42448
			Sort,
			// Token: 0x0400A5D1 RID: 42449
			Select,
			// Token: 0x0400A5D2 RID: 42450
			Phase,
			// Token: 0x0400A5D3 RID: 42451
			SelType,
			// Token: 0x0400A5D4 RID: 42452
			SelAttr,
			// Token: 0x0400A5D5 RID: 42453
			SelStand,
			// Token: 0x0400A5D6 RID: 42454
			SelCoin,
			// Token: 0x0400A5D7 RID: 42455
			SelDice,
			// Token: 0x0400A5D8 RID: 42456
			SelNum,
			// Token: 0x0400A5D9 RID: 42457
			Final,
			// Token: 0x0400A5DA RID: 42458
			Result,
			// Token: 0x0400A5DB RID: 42459
			Discard,
			// Token: 0x0400A5DC RID: 42460
			Ritual,
			// Token: 0x0400A5DD RID: 42461
			Update,
			// Token: 0x0400A5DE RID: 42462
			Close
		}

		// Token: 0x02000E4E RID: 3662
		public enum DialogInfo
		{
			// Token: 0x0400A5E0 RID: 42464
			CardName,
			// Token: 0x0400A5E1 RID: 42465
			CardName2,
			// Token: 0x0400A5E2 RID: 42466
			SelectItem,
			// Token: 0x0400A5E3 RID: 42467
			CardType,
			// Token: 0x0400A5E4 RID: 42468
			CardAttr,
			// Token: 0x0400A5E5 RID: 42469
			CardLevel,
			// Token: 0x0400A5E6 RID: 42470
			Coin,
			// Token: 0x0400A5E7 RID: 42471
			Dice,
			// Token: 0x0400A5E8 RID: 42472
			Dice2,
			// Token: 0x0400A5E9 RID: 42473
			DiceChange,
			// Token: 0x0400A5EA RID: 42474
			NotHappen,
			// Token: 0x0400A5EB RID: 42475
			CardAttr2,
			// Token: 0x0400A5EC RID: 42476
			Info,
			// Token: 0x0400A5ED RID: 42477
			Info2,
			// Token: 0x0400A5EE RID: 42478
			Confirm
		}

		// Token: 0x02000E4F RID: 3663
		public enum DialogOkType
		{
			// Token: 0x0400A5F0 RID: 42480
			Stop,
			// Token: 0x0400A5F1 RID: 42481
			Once,
			// Token: 0x0400A5F2 RID: 42482
			Forever,
			// Token: 0x0400A5F3 RID: 42483
			Sub
		}

		// Token: 0x02000E50 RID: 3664
		public enum DialogEffectType
		{
			// Token: 0x0400A5F5 RID: 42485
			None,
			// Token: 0x0400A5F6 RID: 42486
			All,
			// Token: 0x0400A5F7 RID: 42487
			More,
			// Token: 0x0400A5F8 RID: 42488
			Auto,
			// Token: 0x0400A5F9 RID: 42489
			Always
		}

		// Token: 0x02000E51 RID: 3665
		public enum DialogRitualType
		{
			// Token: 0x0400A5FB RID: 42491
			Ritual,
			// Token: 0x0400A5FC RID: 42492
			Multi,
			// Token: 0x0400A5FD RID: 42493
			Atk,
			// Token: 0x0400A5FE RID: 42494
			Sync,
			// Token: 0x0400A5FF RID: 42495
			Link
		}

		// Token: 0x02000E52 RID: 3666
		public enum DialogMixTextType
		{
			// Token: 0x0400A601 RID: 42497
			Null,
			// Token: 0x0400A602 RID: 42498
			AddString,
			// Token: 0x0400A603 RID: 42499
			AddCr,
			// Token: 0x0400A604 RID: 42500
			InsString,
			// Token: 0x0400A605 RID: 42501
			InsStringNoColor,
			// Token: 0x0400A606 RID: 42502
			InsCard,
			// Token: 0x0400A607 RID: 42503
			InsType,
			// Token: 0x0400A608 RID: 42504
			InsAttr,
			// Token: 0x0400A609 RID: 42505
			InsNum,
			// Token: 0x0400A60A RID: 42506
			InsStringIfable
		}

		// Token: 0x02000E53 RID: 3667
		private class CachedParam
		{
			// Token: 0x06006B0C RID: 27404 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetMyPlayerNum(int player)
			{
			}

			// Token: 0x06006B0D RID: 27405 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetMyPlayerNum()
			{
				return 0;
			}

			// Token: 0x06006B0E RID: 27406 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPlayerType(int player, Engine.PlayerType type)
			{
			}

			// Token: 0x06006B0F RID: 27407 RVA: 0x000029CC File Offset: 0x00000BCC
			public int IsPlayerType(int player, Engine.PlayerType type)
			{
				return 0;
			}

			// Token: 0x06006B10 RID: 27408 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Myself()
			{
				return 0;
			}

			// Token: 0x06006B11 RID: 27409 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Rival()
			{
				return 0;
			}

			// Token: 0x06006B12 RID: 27410 RVA: 0x000029CC File Offset: 0x00000BCC
			public int IsMyself(int player)
			{
				return 0;
			}

			// Token: 0x06006B13 RID: 27411 RVA: 0x000029CC File Offset: 0x00000BCC
			public int IsRival(int player)
			{
				return 0;
			}

			// Token: 0x0400A60B RID: 42507
			private int myself;

			// Token: 0x0400A60C RID: 42508
			private Engine.PlayerType[] playerType;

			// Token: 0x0400A60D RID: 42509
			public uint[] cpuParam;
		}

		// Token: 0x02000E54 RID: 3668
		// (Invoke) Token: 0x06006B16 RID: 27414
		public delegate int RunEffect(int id, int param1, int param2, int param3);

		// Token: 0x02000E55 RID: 3669
		// (Invoke) Token: 0x06006B1A RID: 27418
		public delegate int IsBusyEffect(int id);

		// Token: 0x02000E56 RID: 3670
		// (Invoke) Token: 0x06006B1E RID: 27422
		public delegate void AddRecord(IntPtr ptr, int size);

		// Token: 0x02000E57 RID: 3671
		// (Invoke) Token: 0x06006B22 RID: 27426
		public delegate IntPtr NowRecord();

		// Token: 0x02000E58 RID: 3672
		// (Invoke) Token: 0x06006B26 RID: 27430
		public delegate void RecordNext();

		// Token: 0x02000E59 RID: 3673
		// (Invoke) Token: 0x06006B2A RID: 27434
		public delegate void RecordBegin();

		// Token: 0x02000E5A RID: 3674
		// (Invoke) Token: 0x06006B2E RID: 27438
		public delegate int IsRecordEnd();

		// Token: 0x02000E5B RID: 3675
		// (Invoke) Token: 0x06006B32 RID: 27442
		public delegate int ThreadRunEffectDeleg(int id, int param1, int param2, int param3);

		// Token: 0x02000E5C RID: 3676
		// (Invoke) Token: 0x06006B36 RID: 27446
		public delegate int ThreadIsBusyEffectDeleg(int id);

		// Token: 0x02000E5D RID: 3677
		// (Invoke) Token: 0x06006B3A RID: 27450
		public delegate void ThreadAddRecordDeleg(IntPtr ptr, int size);

		// Token: 0x02000E5E RID: 3678
		// (Invoke) Token: 0x06006B3E RID: 27454
		public delegate IntPtr ThreadNowRecordDeleg();

		// Token: 0x02000E5F RID: 3679
		// (Invoke) Token: 0x06006B42 RID: 27458
		public delegate void ThreadRecordNextDeleg();

		// Token: 0x02000E60 RID: 3680
		// (Invoke) Token: 0x06006B46 RID: 27462
		public delegate void ThreadRecordBeginDeleg();

		// Token: 0x02000E61 RID: 3681
		// (Invoke) Token: 0x06006B4A RID: 27466
		public delegate int ThreadIsRecordEndDeleg();

		// Token: 0x02000E62 RID: 3682
		public enum ListType
		{
			// Token: 0x0400A60F RID: 42511
			Null,
			// Token: 0x0400A610 RID: 42512
			Fusion,
			// Token: 0x0400A611 RID: 42513
			Deck,
			// Token: 0x0400A612 RID: 42514
			Grave,
			// Token: 0x0400A613 RID: 42515
			Exclude,
			// Token: 0x0400A614 RID: 42516
			View,
			// Token: 0x0400A615 RID: 42517
			Select,
			// Token: 0x0400A616 RID: 42518
			SelectMax = 38,
			// Token: 0x0400A617 RID: 42519
			Selectable,
			// Token: 0x0400A618 RID: 42520
			SelectableMax = 71,
			// Token: 0x0400A619 RID: 42521
			SelUpTo,
			// Token: 0x0400A61A RID: 42522
			SelUpToMax = 104,
			// Token: 0x0400A61B RID: 42523
			SelFree,
			// Token: 0x0400A61C RID: 42524
			SelFreeMax = 137,
			// Token: 0x0400A61D RID: 42525
			BlindSelect,
			// Token: 0x0400A61E RID: 42526
			AutoSelect,
			// Token: 0x0400A61F RID: 42527
			SelAllCard,
			// Token: 0x0400A620 RID: 42528
			SelAllDeck,
			// Token: 0x0400A621 RID: 42529
			SelAllMonst,
			// Token: 0x0400A622 RID: 42530
			SelAllMonst2,
			// Token: 0x0400A623 RID: 42531
			SelAllGadget,
			// Token: 0x0400A624 RID: 42532
			SelAllIndeck
		}

		// Token: 0x02000E63 RID: 3683
		[Flags]
		public enum ListAttribute
		{
			// Token: 0x0400A626 RID: 42534
			FromField = 1,
			// Token: 0x0400A627 RID: 42535
			FromHand = 2,
			// Token: 0x0400A628 RID: 42536
			FromDeck = 4,
			// Token: 0x0400A629 RID: 42537
			FromGrave = 8,
			// Token: 0x0400A62A RID: 42538
			FromExtra = 16,
			// Token: 0x0400A62B RID: 42539
			FromExclude = 32,
			// Token: 0x0400A62C RID: 42540
			DisableEffect = 64,
			// Token: 0x0400A62D RID: 42541
			CantRevive = 128,
			// Token: 0x0400A62E RID: 42542
			FusionMaterial = 256,
			// Token: 0x0400A62F RID: 42543
			DemensionHole = 1024,
			// Token: 0x0400A630 RID: 42544
			LightForce = 2048,
			// Token: 0x0400A631 RID: 42545
			Targeted = 4096,
			// Token: 0x0400A632 RID: 42546
			Tuning = 8192,
			// Token: 0x0400A633 RID: 42547
			ByBattle = 16384,
			// Token: 0x0400A634 RID: 42548
			Opponent = 32768,
			// Token: 0x0400A635 RID: 42549
			Activate = 65536,
			// Token: 0x0400A636 RID: 42550
			Cost = 131072,
			// Token: 0x0400A637 RID: 42551
			End = 262144,
			// Token: 0x0400A638 RID: 42552
			ExtraExclude = 524288,
			// Token: 0x0400A639 RID: 42553
			FromMask = 63
		}

		// Token: 0x02000E64 RID: 3684
		private class PvpPosBase
		{
			// Token: 0x0400A63A RID: 42554
			public uint nComBit;

			// Token: 0x0400A63B RID: 42555
			public ushort wMrk;

			// Token: 0x0400A63C RID: 42556
			public ushort wTurnCounter;

			// Token: 0x0400A63D RID: 42557
			public byte bTopIdx;

			// Token: 0x0400A63E RID: 42558
			public byte bEffectFlags;

			// Token: 0x0400A63F RID: 42559
			public byte bMonstOrgLevel;

			// Token: 0x0400A640 RID: 42560
			public byte bMonstOrgType;

			// Token: 0x0400A641 RID: 42561
			public byte bZoneAvailable;

			// Token: 0x0400A642 RID: 42562
			public byte bZoneAvailable2;

			// Token: 0x0400A643 RID: 42563
			public byte bCardInBattle;

			// Token: 0x0400A644 RID: 42564
			public byte bNormalMonster;

			// Token: 0x0400A645 RID: 42565
			public sbyte bPendScale;

			// Token: 0x0400A646 RID: 42566
			public sbyte bPendOrgScale;

			// Token: 0x0400A647 RID: 42567
			public sbyte bMonstRank;

			// Token: 0x0400A648 RID: 42568
			public sbyte bMonstOrgRank;

			// Token: 0x0400A649 RID: 42569
			public byte bTrapMonster;

			// Token: 0x0400A64A RID: 42570
			public byte bTunerMonster;

			// Token: 0x0400A64B RID: 42571
			public ushort wOverlayNum;

			// Token: 0x0400A64C RID: 42572
			public ushort wCardNum;

			// Token: 0x0400A64D RID: 42573
			public byte[] bCounter;

			// Token: 0x0400A64E RID: 42574
			public byte bFightable;

			// Token: 0x0400A64F RID: 42575
			public ushort wEquip;

			// Token: 0x0400A650 RID: 42576
			public ushort wContinuous;

			// Token: 0x0400A651 RID: 42577
			public byte bIsMagic;

			// Token: 0x0400A652 RID: 42578
			public byte bIsTrap;

			// Token: 0x0400A653 RID: 42579
			public uint nComBitTextId;

			// Token: 0x0400A654 RID: 42580
			public int nShowParam;

			// Token: 0x0400A655 RID: 42581
			public uint nCardParam;

			// Token: 0x0400A656 RID: 42582
			public int nCardDirectFlag;

			// Token: 0x0400A657 RID: 42583
			public int[] nOtherEffect;
		}

		// Token: 0x02000E65 RID: 3685
		private class PvpIconBase
		{
			// Token: 0x0400A658 RID: 42584
			public byte player;

			// Token: 0x0400A659 RID: 42585
			public byte pos;

			// Token: 0x0400A65A RID: 42586
			public byte to_player;

			// Token: 0x0400A65B RID: 42587
			public byte to_pos;

			// Token: 0x0400A65C RID: 42588
			public short icon;
		}

		// Token: 0x02000E66 RID: 3686
		private class PvpDuelInfo
		{
			// Token: 0x0400A65D RID: 42589
			public bool isQuick;

			// Token: 0x0400A65E RID: 42590
			public uint nTurnNum;

			// Token: 0x0400A65F RID: 42591
			public uint nCurrentPhase;

			// Token: 0x0400A660 RID: 42592
			public uint nCurrentStep;

			// Token: 0x0400A661 RID: 42593
			public uint nCurrentDmgStep;

			// Token: 0x0400A662 RID: 42594
			public byte bWhichTurnNow;

			// Token: 0x0400A663 RID: 42595
			public uint nMovablePhase;

			// Token: 0x0400A664 RID: 42596
			public uint[] nLP;

			// Token: 0x0400A665 RID: 42597
			public uint[] nDoPutMonst;

			// Token: 0x0400A666 RID: 42598
			public bool[] bDoSummon;

			// Token: 0x0400A667 RID: 42599
			public bool[] bDoSpSummon;

			// Token: 0x0400A668 RID: 42600
			public Engine.PvpPosBase[,] Pos;

			// Token: 0x0400A669 RID: 42601
			public ushort wTblNum;

			// Token: 0x0400A66A RID: 42602
			public ushort wIconNum;

			// Token: 0x0400A66B RID: 42603
			public Engine.PvpIconBase[] IconBases;

			// Token: 0x0400A66C RID: 42604
			public bool isDeckReverse;
		}

		// Token: 0x02000E67 RID: 3687
		private class PvpUIDBase
		{
			// Token: 0x0400A66D RID: 42605
			public uint nCom;

			// Token: 0x0400A66E RID: 42606
			public uint nPos;

			// Token: 0x0400A66F RID: 42607
			public ushort wUid;

			// Token: 0x0400A670 RID: 42608
			public Engine.CardProp stProp;

			// Token: 0x0400A671 RID: 42609
			public bool isFace;

			// Token: 0x0400A672 RID: 42610
			public bool isTurn;

			// Token: 0x0400A673 RID: 42611
			public Engine.BasicVal stBasicVal;

			// Token: 0x0400A674 RID: 42612
			public uint nComTextId;
		}

		// Token: 0x02000E68 RID: 3688
		private class PvpEngineData
		{
			// Token: 0x0400A675 RID: 42613
			public Engine.PvpDuelInfo duelInfo;

			// Token: 0x0400A676 RID: 42614
			public Dictionary<uint, ushort> posTbl;

			// Token: 0x0400A677 RID: 42615
			public Dictionary<ushort, ushort> uidTbl;

			// Token: 0x0400A678 RID: 42616
			public Engine.PvpUIDBase[] uidBases;

			// Token: 0x0400A679 RID: 42617
			public KeyValuePair<uint, ushort>[] originPos;

			// Token: 0x0400A67A RID: 42618
			public KeyValuePair<ushort, ushort>[] originUid;

			// Token: 0x0400A67B RID: 42619
			public ushort[] attackFlags;

			// Token: 0x0400A67C RID: 42620
			public Dictionary<int, uint> flipInfo;

			// Token: 0x0400A67D RID: 42621
			public Dictionary<ushort, int>[] cardAttribute;

			// Token: 0x0400A67E RID: 42622
			public bool attackFinish;

			// Token: 0x0400A67F RID: 42623
			public byte syncNeed;

			// Token: 0x0400A680 RID: 42624
			public Dictionary<int, int> tuningMonster;

			// Token: 0x0400A681 RID: 42625
			public Dictionary<int, int> tuningLevel;

			// Token: 0x0400A682 RID: 42626
			public int effectIdAtChain;

			// Token: 0x0400A683 RID: 42627
			public int summoningUid;

			// Token: 0x0400A684 RID: 42628
			public Dictionary<string, uint> posMask;

			// Token: 0x0400A685 RID: 42629
			public int recommendSide;
		}

		// Token: 0x02000E69 RID: 3689
		private class PvpDialogData
		{
			// Token: 0x06006B52 RID: 27474 RVA: 0x00002739 File Offset: 0x00000939
			public PvpDialogData(int num)
			{
			}

			// Token: 0x06006B53 RID: 27475 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetSelectItemStr(int idx)
			{
				return 0;
			}

			// Token: 0x06006B54 RID: 27476 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool GetSelectItemEnable(int idx)
			{
				return false;
			}

			// Token: 0x0400A686 RID: 42630
			public Engine.DialogType dlgType;

			// Token: 0x0400A687 RID: 42631
			public int player;

			// Token: 0x0400A688 RID: 42632
			public int selMax;

			// Token: 0x0400A689 RID: 42633
			public uint[] sel;

			// Token: 0x0400A68A RID: 42634
			public uint posMaskSummon;
		}

		// Token: 0x02000E6A RID: 3690
		private class PvpListData
		{
			// Token: 0x0400A68B RID: 42635
			public Engine.ListType listType;

			// Token: 0x0400A68C RID: 42636
			public int selMax;

			// Token: 0x0400A68D RID: 42637
			public int selMin;

			// Token: 0x0400A68E RID: 42638
			public short itemMax;

			// Token: 0x0400A68F RID: 42639
			public ushort[] itemUids;

			// Token: 0x0400A690 RID: 42640
			public uint[] itemAttributes;

			// Token: 0x0400A691 RID: 42641
			public int[] itemFrom;

			// Token: 0x0400A692 RID: 42642
			public uint[] itemIds;

			// Token: 0x0400A693 RID: 42643
			public uint[] itemMsg;

			// Token: 0x0400A694 RID: 42644
			public uint[] itemTargetUids;

			// Token: 0x0400A695 RID: 42645
			public Engine.MixedValue[] itemMixVal;
		}

		// Token: 0x02000E6B RID: 3691
		private class PvpFusionData
		{
			// Token: 0x0400A696 RID: 42646
			public int[] material;

			// Token: 0x0400A697 RID: 42647
			public int[] mrk;
		}

		// Token: 0x02000E6C RID: 3692
		private enum PvpCommandType
		{
			// Token: 0x0400A699 RID: 42649
			Input,
			// Token: 0x0400A69A RID: 42650
			List,
			// Token: 0x0400A69B RID: 42651
			Dialog,
			// Token: 0x0400A69C RID: 42652
			Effect,
			// Token: 0x0400A69D RID: 42653
			Field,
			// Token: 0x0400A69E RID: 42654
			Data,
			// Token: 0x0400A69F RID: 42655
			Fusion,
			// Token: 0x0400A6A0 RID: 42656
			Time,
			// Token: 0x0400A6A1 RID: 42657
			ListFrom,
			// Token: 0x0400A6A2 RID: 42658
			FlipInfo,
			// Token: 0x0400A6A3 RID: 42659
			FinishAttack,
			// Token: 0x0400A6A4 RID: 42660
			MrkList,
			// Token: 0x0400A6A5 RID: 42661
			FusionNeed,
			// Token: 0x0400A6A6 RID: 42662
			TunerLevel,
			// Token: 0x0400A6A7 RID: 42663
			CutinActivate
		}

		// Token: 0x02000E6D RID: 3693
		private enum PvpFieldType
		{
			// Token: 0x0400A6A9 RID: 42665
			Prop = 1,
			// Token: 0x0400A6AA RID: 42666
			Pos,
			// Token: 0x0400A6AB RID: 42667
			Uid,
			// Token: 0x0400A6AC RID: 42668
			Vals,
			// Token: 0x0400A6AD RID: 42669
			Icon,
			// Token: 0x0400A6AE RID: 42670
			Skill,
			// Token: 0x0400A6AF RID: 42671
			Rare,
			// Token: 0x0400A6B0 RID: 42672
			Attack,
			// Token: 0x0400A6B1 RID: 42673
			Show,
			// Token: 0x0400A6B2 RID: 42674
			Step,
			// Token: 0x0400A6B3 RID: 42675
			SummoningUid,
			// Token: 0x0400A6B4 RID: 42676
			PosMask,
			// Token: 0x0400A6B5 RID: 42677
			End
		}

		// Token: 0x02000E6E RID: 3694
		private class PvpCommand
		{
			// Token: 0x06006B57 RID: 27479 RVA: 0x00002739 File Offset: 0x00000939
			public PvpCommand(Engine.PvpCommandType t, int[] p, object o)
			{
			}

			// Token: 0x0400A6B6 RID: 42678
			public Engine.PvpCommandType type;

			// Token: 0x0400A6B7 RID: 42679
			public int[] param;

			// Token: 0x0400A6B8 RID: 42680
			public object data;
		}

		// Token: 0x02000E6F RID: 3695
		private class MixedValue
		{
			// Token: 0x06006B58 RID: 27480 RVA: 0x00002739 File Offset: 0x00000939
			public MixedValue(int num, uint[] values)
			{
			}

			// Token: 0x0400A6B9 RID: 42681
			public int mixNum;

			// Token: 0x0400A6BA RID: 42682
			public uint[] mixValue;
		}

		// Token: 0x02000E70 RID: 3696
		private class PvpWork
		{
			// Token: 0x17000BF2 RID: 3058
			// (get) Token: 0x06006B59 RID: 27481 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B5A RID: 27482 RVA: 0x0000216D File Offset: 0x0000036D
			public Engine.ViewType RunningEffect
			{
				[CompilerGenerated]
				get
				{
					return Engine.ViewType.Null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BF3 RID: 3059
			// (get) Token: 0x06006B5B RID: 27483 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B5C RID: 27484 RVA: 0x0000216D File Offset: 0x0000036D
			public Engine.ViewType CurrentRunEffect
			{
				[CompilerGenerated]
				get
				{
					return Engine.ViewType.Null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BF4 RID: 3060
			// (get) Token: 0x06006B5D RID: 27485 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B5E RID: 27486 RVA: 0x0000216D File Offset: 0x0000036D
			public uint TimeLeft
			{
				[CompilerGenerated]
				get
				{
					return 0U;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BF5 RID: 3061
			// (get) Token: 0x06006B5F RID: 27487 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B60 RID: 27488 RVA: 0x0000216D File Offset: 0x0000036D
			public uint TimeTotal
			{
				[CompilerGenerated]
				get
				{
					return 0U;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BF6 RID: 3062
			// (get) Token: 0x06006B61 RID: 27489 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B62 RID: 27490 RVA: 0x0000216D File Offset: 0x0000036D
			public bool inputGuard
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

			// Token: 0x06006B63 RID: 27491 RVA: 0x0000216D File Offset: 0x0000036D
			public void Release()
			{
			}

			// Token: 0x06006B64 RID: 27492 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddCommand(Engine.PvpCommand cmd)
			{
			}

			// Token: 0x06006B65 RID: 27493 RVA: 0x0000216A File Offset: 0x0000036A
			public Engine.PvpCommand GetCommand()
			{
				return null;
			}

			// Token: 0x06006B66 RID: 27494 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsQueued()
			{
				return false;
			}

			// Token: 0x06006B67 RID: 27495 RVA: 0x0000216A File Offset: 0x0000036A
			public Engine.PvpCommand Next()
			{
				return null;
			}

			// Token: 0x06006B68 RID: 27496 RVA: 0x000029CC File Offset: 0x00000BCC
			public Engine.ViewType NextEffect()
			{
				return Engine.ViewType.Null;
			}

			// Token: 0x06006B69 RID: 27497 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsRunningEffect()
			{
				return false;
			}

			// Token: 0x06006B6A RID: 27498 RVA: 0x000029CC File Offset: 0x00000BCC
			public uint Serial()
			{
				return 0U;
			}

			// Token: 0x0400A6BB RID: 42683
			private Queue<Engine.PvpCommand> commandQueue;

			// Token: 0x0400A6BC RID: 42684
			public Engine.PvpEngineData currentEngineData;

			// Token: 0x0400A6BD RID: 42685
			public Engine.PvpDialogData currentDialogData;

			// Token: 0x0400A6BE RID: 42686
			public Engine.PvpListData currentListData;

			// Token: 0x0400A6BF RID: 42687
			public Engine.PvpFusionData currentFusionData;

			// Token: 0x0400A6C0 RID: 42688
			public Dictionary<ushort, ushort> rareTbl;

			// Token: 0x0400A6C1 RID: 42689
			public int mixNum;

			// Token: 0x0400A6C2 RID: 42690
			public uint[] mixValue;

			// Token: 0x0400A6C3 RID: 42691
			public ushort[] currentListMrk;

			// Token: 0x0400A6C4 RID: 42692
			public Queue<Engine.MixedValue> mixvalQueue;

			// Token: 0x0400A6C5 RID: 42693
			public int[] CurrentParam;

			// Token: 0x0400A6C6 RID: 42694
			private uint serializer;
		}

		// Token: 0x02000E71 RID: 3697
		public enum ToEngineActType
		{
			// Token: 0x0400A6C8 RID: 42696
			DoCommand,
			// Token: 0x0400A6C9 RID: 42697
			CancelCommand,
			// Token: 0x0400A6CA RID: 42698
			MovePhase,
			// Token: 0x0400A6CB RID: 42699
			DebugCommand,
			// Token: 0x0400A6CC RID: 42700
			DoDebug,
			// Token: 0x0400A6CD RID: 42701
			CheatCard,
			// Token: 0x0400A6CE RID: 42702
			DialogResult,
			// Token: 0x0400A6CF RID: 42703
			ListSendBlindIndex,
			// Token: 0x0400A6D0 RID: 42704
			ListSendIndex,
			// Token: 0x0400A6D1 RID: 42705
			ListSendSelectMulti,
			// Token: 0x0400A6D2 RID: 42706
			ForceSurrender
		}

		// Token: 0x02000E72 RID: 3698
		[Serializable]
		public class ThreadPosParam
		{
			// Token: 0x0400A6D3 RID: 42707
			public int nShowParam;

			// Token: 0x0400A6D4 RID: 42708
			public uint nCardParam;

			// Token: 0x0400A6D5 RID: 42709
			public int nCardDirect;
		}

		// Token: 0x02000E73 RID: 3699
		[Serializable]
		public class ThreadPosBase
		{
			// Token: 0x0400A6D6 RID: 42710
			public uint nComBit;

			// Token: 0x0400A6D7 RID: 42711
			public int wMrk;

			// Token: 0x0400A6D8 RID: 42712
			public int wTurnCounter;

			// Token: 0x0400A6D9 RID: 42713
			public int bTopIdx;

			// Token: 0x0400A6DA RID: 42714
			public int bEffectFlags;

			// Token: 0x0400A6DB RID: 42715
			public int bMonstOrgLevel;

			// Token: 0x0400A6DC RID: 42716
			public int bMonstOrgType;

			// Token: 0x0400A6DD RID: 42717
			public int bZoneAvailable;

			// Token: 0x0400A6DE RID: 42718
			public int bZoneAvailable2;

			// Token: 0x0400A6DF RID: 42719
			public int bNormalMonster;

			// Token: 0x0400A6E0 RID: 42720
			public int bPendScale;

			// Token: 0x0400A6E1 RID: 42721
			public int bPendOrgScale;

			// Token: 0x0400A6E2 RID: 42722
			public int bMonstRank;

			// Token: 0x0400A6E3 RID: 42723
			public int bMonstOrgRank;

			// Token: 0x0400A6E4 RID: 42724
			public int bTrapMonster;

			// Token: 0x0400A6E5 RID: 42725
			public int bTunerMonster;

			// Token: 0x0400A6E6 RID: 42726
			public int bOverlayNum;

			// Token: 0x0400A6E7 RID: 42727
			public int wCardNum;

			// Token: 0x0400A6E8 RID: 42728
			public int[] bCounter;

			// Token: 0x0400A6E9 RID: 42729
			public int[] otherEffect;

			// Token: 0x0400A6EA RID: 42730
			public bool Fightable;

			// Token: 0x0400A6EB RID: 42731
			public int Equip;

			// Token: 0x0400A6EC RID: 42732
			public int Continuous;

			// Token: 0x0400A6ED RID: 42733
			public bool IsMagic;

			// Token: 0x0400A6EE RID: 42734
			public bool IsTrap;

			// Token: 0x0400A6EF RID: 42735
			public uint nComBitTextId;
		}

		// Token: 0x02000E74 RID: 3700
		[Serializable]
		public class ThreadIconBase
		{
			// Token: 0x0400A6F0 RID: 42736
			public int player;

			// Token: 0x0400A6F1 RID: 42737
			public int pos;

			// Token: 0x0400A6F2 RID: 42738
			public int to_player;

			// Token: 0x0400A6F3 RID: 42739
			public int to_pos;

			// Token: 0x0400A6F4 RID: 42740
			public int icon;
		}

		// Token: 0x02000E75 RID: 3701
		[Serializable]
		public class ThreadPlayerInfo
		{
			// Token: 0x0400A6F5 RID: 42741
			public int LP;

			// Token: 0x0400A6F6 RID: 42742
			public int DoPutMonst;

			// Token: 0x0400A6F7 RID: 42743
			public bool DoSummon;

			// Token: 0x0400A6F8 RID: 42744
			public bool DoSpSummon;
		}

		// Token: 0x02000E76 RID: 3702
		[Serializable]
		public class ThreadDuelInfo
		{
			// Token: 0x0400A6F9 RID: 42745
			public uint nTurnNum;

			// Token: 0x0400A6FA RID: 42746
			public uint nCurrentPhase;

			// Token: 0x0400A6FB RID: 42747
			public int bWhichTurnNow;

			// Token: 0x0400A6FC RID: 42748
			public uint nMovablePhase;

			// Token: 0x0400A6FD RID: 42749
			public Engine.ThreadPosBase[,] Pos;

			// Token: 0x0400A6FE RID: 42750
			public uint nCurrentStep;

			// Token: 0x0400A6FF RID: 42751
			public uint nCurrentDmgStep;

			// Token: 0x0400A700 RID: 42752
			public Engine.ThreadPlayerInfo[] playerInfo;
		}

		// Token: 0x02000E77 RID: 3703
		[Serializable]
		public class CardPropSerial
		{
			// Token: 0x0400A701 RID: 42753
			public int CardID;

			// Token: 0x0400A702 RID: 42754
			public int UniqueID;

			// Token: 0x0400A703 RID: 42755
			public int Owner;

			// Token: 0x0400A704 RID: 42756
			public bool Correct;

			// Token: 0x0400A705 RID: 42757
			public bool ByBattle;

			// Token: 0x0400A706 RID: 42758
			public bool ByAnother;

			// Token: 0x0400A707 RID: 42759
			public bool ByBreak;

			// Token: 0x0400A708 RID: 42760
			public bool TurnPast;

			// Token: 0x0400A709 RID: 42761
			public bool Disabled;

			// Token: 0x0400A70A RID: 42762
			public bool TimingPast;
		}

		// Token: 0x02000E78 RID: 3704
		[Serializable]
		public class BasicValSerial
		{
			// Token: 0x0400A70B RID: 42763
			public short CardID;

			// Token: 0x0400A70C RID: 42764
			public short EffectID;

			// Token: 0x0400A70D RID: 42765
			public int Atk;

			// Token: 0x0400A70E RID: 42766
			public int Def;

			// Token: 0x0400A70F RID: 42767
			public int OrgAtk;

			// Token: 0x0400A710 RID: 42768
			public int OrgDef;

			// Token: 0x0400A711 RID: 42769
			public short Type;

			// Token: 0x0400A712 RID: 42770
			public short Attr;

			// Token: 0x0400A713 RID: 42771
			public short Element;

			// Token: 0x0400A714 RID: 42772
			public short Level;

			// Token: 0x0400A715 RID: 42773
			public byte Rank;

			// Token: 0x0400A716 RID: 42774
			public byte VoidMagic;

			// Token: 0x0400A717 RID: 42775
			public byte VoidTrap;

			// Token: 0x0400A718 RID: 42776
			public byte VoidMonst;
		}

		// Token: 0x02000E79 RID: 3705
		[Serializable]
		public class ToMainRunEffect
		{
			// Token: 0x0400A719 RID: 42777
			public int[] param;

			// Token: 0x0400A71A RID: 42778
			public Engine.ThreadEngineData engineData;

			// Token: 0x0400A71B RID: 42779
			public Engine.ThreadDialogData dialogData;

			// Token: 0x0400A71C RID: 42780
			public Engine.ThreadListData listData;

			// Token: 0x0400A71D RID: 42781
			public Engine.ThreadFusionData fusionData;

			// Token: 0x0400A71E RID: 42782
			public Engine.ThreadMixTextData mixTextData;

			// Token: 0x0400A71F RID: 42783
			public Dictionary<string, int> attackFlags;

			// Token: 0x0400A720 RID: 42784
			public Engine.ThreadIconBase[] IconBases;

			// Token: 0x0400A721 RID: 42785
			public Dictionary<int, uint> flipInfo;

			// Token: 0x0400A722 RID: 42786
			public Engine.ThreadPosParam[][] posParam;
		}

		// Token: 0x02000E7A RID: 3706
		[Serializable]
		public class ToMainWork
		{
			// Token: 0x0400A723 RID: 42787
			public bool finishedSysAct;

			// Token: 0x0400A724 RID: 42788
			public Engine.ToMainRunEffect runeffectData;
		}

		// Token: 0x02000E7B RID: 3707
		[Serializable]
		public class ToEngineWork
		{
			// Token: 0x0400A725 RID: 42789
			public Engine.ToEngineActType actType;

			// Token: 0x0400A726 RID: 42790
			public int player;

			// Token: 0x0400A727 RID: 42791
			public int position;

			// Token: 0x0400A728 RID: 42792
			public int index;

			// Token: 0x0400A729 RID: 42793
			public int commandId;

			// Token: 0x0400A72A RID: 42794
			public int phase;

			// Token: 0x0400A72B RID: 42795
			public int cardId;

			// Token: 0x0400A72C RID: 42796
			public int face;

			// Token: 0x0400A72D RID: 42797
			public int turn;

			// Token: 0x0400A72E RID: 42798
			public uint dlgResult;

			// Token: 0x0400A72F RID: 42799
			public int listIndex;

			// Token: 0x0400A730 RID: 42800
			public int listNum;

			// Token: 0x0400A731 RID: 42801
			public bool decide;

			// Token: 0x0400A732 RID: 42802
			public int[] listSelect;
		}

		// Token: 0x02000E7C RID: 3708
		[Serializable]
		public class ThreadUIDBase
		{
			// Token: 0x0400A733 RID: 42803
			public uint nCom;

			// Token: 0x0400A734 RID: 42804
			public uint nPos;

			// Token: 0x0400A735 RID: 42805
			public int wUid;

			// Token: 0x0400A736 RID: 42806
			public Engine.CardPropSerial stProp;

			// Token: 0x0400A737 RID: 42807
			public bool isFace;

			// Token: 0x0400A738 RID: 42808
			public bool isTurn;

			// Token: 0x0400A739 RID: 42809
			public Engine.BasicValSerial stBasicVal;

			// Token: 0x0400A73A RID: 42810
			public uint nComTextId;
		}

		// Token: 0x02000E7D RID: 3709
		[Serializable]
		public class ThreadEngineData : IDisposable
		{
			// Token: 0x06006B77 RID: 27511 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x0400A73B RID: 42811
			public Engine.ThreadDuelInfo duelInfo;

			// Token: 0x0400A73C RID: 42812
			public Dictionary<uint, int> posTbl;

			// Token: 0x0400A73D RID: 42813
			public Dictionary<int, int> uidTbl;

			// Token: 0x0400A73E RID: 42814
			public Engine.ThreadUIDBase[] uidBases;

			// Token: 0x0400A73F RID: 42815
			public Dictionary<int, int>[] cardAttribute;

			// Token: 0x0400A740 RID: 42816
			public Dictionary<string, uint> posMask;

			// Token: 0x0400A741 RID: 42817
			public bool attackFinish;

			// Token: 0x0400A742 RID: 42818
			public int syncNeed;

			// Token: 0x0400A743 RID: 42819
			public int[] tuningLevel;

			// Token: 0x0400A744 RID: 42820
			public int effectIdAtChain;

			// Token: 0x0400A745 RID: 42821
			public int summoningUid;

			// Token: 0x0400A746 RID: 42822
			public int recommendSide;

			// Token: 0x0400A747 RID: 42823
			public bool flagDeckReverse;
		}

		// Token: 0x02000E7E RID: 3710
		[Serializable]
		public class ThreadDialogData : IDisposable
		{
			// Token: 0x06006B79 RID: 27513 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x0400A748 RID: 42824
			public Engine.DialogType dlgType;

			// Token: 0x0400A749 RID: 42825
			public int textId;

			// Token: 0x0400A74A RID: 42826
			public int selNum;

			// Token: 0x0400A74B RID: 42827
			public int[] selStr;

			// Token: 0x0400A74C RID: 42828
			public bool[] selEnable;

			// Token: 0x0400A74D RID: 42829
			public bool yesno;

			// Token: 0x0400A74E RID: 42830
			public uint posMaskSummon;
		}

		// Token: 0x02000E7F RID: 3711
		[Serializable]
		public class ThreadListData : IDisposable
		{
			// Token: 0x06006B7B RID: 27515 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x0400A74F RID: 42831
			public Engine.ListType listType;

			// Token: 0x0400A750 RID: 42832
			public int selMax;

			// Token: 0x0400A751 RID: 42833
			public int selMin;

			// Token: 0x0400A752 RID: 42834
			public int itemMax;

			// Token: 0x0400A753 RID: 42835
			public int[] itemUids;

			// Token: 0x0400A754 RID: 42836
			public int[] itemAttributes;

			// Token: 0x0400A755 RID: 42837
			public int[] itemFrom;

			// Token: 0x0400A756 RID: 42838
			public int[] itemIds;

			// Token: 0x0400A757 RID: 42839
			public int[] itemTargetUids;

			// Token: 0x0400A758 RID: 42840
			public int[] itemMsg;

			// Token: 0x0400A759 RID: 42841
			public Engine.ThreadMixTextData[] itemMsgVal;
		}

		// Token: 0x02000E80 RID: 3712
		[Serializable]
		public class ThreadFusionData : IDisposable
		{
			// Token: 0x06006B7D RID: 27517 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x0400A75A RID: 42842
			public int[] material;

			// Token: 0x0400A75B RID: 42843
			public int[] mrk;
		}

		// Token: 0x02000E81 RID: 3713
		[Serializable]
		public class ThreadMixTextData : IDisposable
		{
			// Token: 0x06006B7F RID: 27519 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x0400A75C RID: 42844
			public int mixNum;

			// Token: 0x0400A75D RID: 42845
			public int[] mixType;

			// Token: 0x0400A75E RID: 42846
			public int[] mixData;
		}

		// Token: 0x02000E82 RID: 3714
		private class ThreadWork
		{
			// Token: 0x17000BF7 RID: 3063
			// (get) Token: 0x06006B81 RID: 27521 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B82 RID: 27522 RVA: 0x0000216D File Offset: 0x0000036D
			public Engine.ViewType RunningEffect
			{
				[CompilerGenerated]
				get
				{
					return Engine.ViewType.Null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BF8 RID: 3064
			// (get) Token: 0x06006B83 RID: 27523 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B84 RID: 27524 RVA: 0x0000216D File Offset: 0x0000036D
			public Engine.ViewType CurrentRunEffect
			{
				[CompilerGenerated]
				get
				{
					return Engine.ViewType.Null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BF9 RID: 3065
			// (get) Token: 0x06006B85 RID: 27525 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06006B86 RID: 27526 RVA: 0x0000216D File Offset: 0x0000036D
			public int thinkingPlayer
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

			// Token: 0x17000BFA RID: 3066
			// (get) Token: 0x06006B87 RID: 27527 RVA: 0x000029C5 File Offset: 0x00000BC5
			// (set) Token: 0x06006B88 RID: 27528 RVA: 0x0000216D File Offset: 0x0000036D
			public float startTime
			{
				[CompilerGenerated]
				get
				{
					return 0f;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000BFB RID: 3067
			// (get) Token: 0x06006B89 RID: 27529 RVA: 0x000029C5 File Offset: 0x00000BC5
			// (set) Token: 0x06006B8A RID: 27530 RVA: 0x0000216D File Offset: 0x0000036D
			public float lastTime
			{
				[CompilerGenerated]
				get
				{
					return 0f;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x06006B8B RID: 27531 RVA: 0x0000216D File Offset: 0x0000036D
			public void Release()
			{
			}

			// Token: 0x06006B8C RID: 27532 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsRunningEffect()
			{
				return false;
			}

			// Token: 0x0400A75F RID: 42847
			public Engine.ThreadEngineData currentEngineData;

			// Token: 0x0400A760 RID: 42848
			public Engine.ThreadDialogData currentDialogData;

			// Token: 0x0400A761 RID: 42849
			public Engine.ThreadListData currentListData;

			// Token: 0x0400A762 RID: 42850
			public Engine.ThreadFusionData currentFusionData;

			// Token: 0x0400A763 RID: 42851
			public Engine.ThreadMixTextData currentMixTextData;

			// Token: 0x0400A764 RID: 42852
			public Dictionary<string, int> attackFlags;

			// Token: 0x0400A765 RID: 42853
			public Engine.ThreadIconBase[] IconBases;

			// Token: 0x0400A766 RID: 42854
			public Dictionary<uint, object> affectsMgr;

			// Token: 0x0400A767 RID: 42855
			public Engine.AffectType[][] dummyAffects;

			// Token: 0x0400A768 RID: 42856
			public Dictionary<int, uint> flipInfo;

			// Token: 0x0400A769 RID: 42857
			public Engine.ThreadPosParam[][] posParam;
		}

		// Token: 0x02000E83 RID: 3715
		[Serializable]
		public struct ThreadIconBuff
		{
			// Token: 0x0400A76A RID: 42858
			public int player;

			// Token: 0x0400A76B RID: 42859
			public int pos;

			// Token: 0x0400A76C RID: 42860
			public int to_player;

			// Token: 0x0400A76D RID: 42861
			public int to_pos;

			// Token: 0x0400A76E RID: 42862
			public int icon;
		}
	}
}
