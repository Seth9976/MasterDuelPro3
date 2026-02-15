using System;

namespace Percy
{
	// Token: 0x020011DB RID: 4571
	internal enum GameMessage
	{
		// Token: 0x0400C253 RID: 49747
		Retry = 1,
		// Token: 0x0400C254 RID: 49748
		Hint,
		// Token: 0x0400C255 RID: 49749
		Waiting,
		// Token: 0x0400C256 RID: 49750
		Start,
		// Token: 0x0400C257 RID: 49751
		Win,
		// Token: 0x0400C258 RID: 49752
		UpdateData,
		// Token: 0x0400C259 RID: 49753
		UpdateCard,
		// Token: 0x0400C25A RID: 49754
		RequestDeck,
		// Token: 0x0400C25B RID: 49755
		SelectBattleCmd = 10,
		// Token: 0x0400C25C RID: 49756
		SelectIdleCmd,
		// Token: 0x0400C25D RID: 49757
		SelectEffectYn,
		// Token: 0x0400C25E RID: 49758
		SelectYesNo,
		// Token: 0x0400C25F RID: 49759
		SelectOption,
		// Token: 0x0400C260 RID: 49760
		SelectCard,
		// Token: 0x0400C261 RID: 49761
		SelectChain,
		// Token: 0x0400C262 RID: 49762
		SelectPlace = 18,
		// Token: 0x0400C263 RID: 49763
		SelectPosition,
		// Token: 0x0400C264 RID: 49764
		SelectTribute,
		// Token: 0x0400C265 RID: 49765
		SortChain,
		// Token: 0x0400C266 RID: 49766
		SelectCounter,
		// Token: 0x0400C267 RID: 49767
		SelectSum,
		// Token: 0x0400C268 RID: 49768
		SelectDisfield,
		// Token: 0x0400C269 RID: 49769
		SortCard,
		// Token: 0x0400C26A RID: 49770
		SelectUnselectCard,
		// Token: 0x0400C26B RID: 49771
		ConfirmDecktop = 30,
		// Token: 0x0400C26C RID: 49772
		ConfirmCards,
		// Token: 0x0400C26D RID: 49773
		ShuffleDeck,
		// Token: 0x0400C26E RID: 49774
		ShuffleHand,
		// Token: 0x0400C26F RID: 49775
		RefreshDeck,
		// Token: 0x0400C270 RID: 49776
		SwapGraveDeck,
		// Token: 0x0400C271 RID: 49777
		ShuffleSetCard,
		// Token: 0x0400C272 RID: 49778
		ReverseDeck,
		// Token: 0x0400C273 RID: 49779
		DeckTop,
		// Token: 0x0400C274 RID: 49780
		ShuffleExtra,
		// Token: 0x0400C275 RID: 49781
		NewTurn,
		// Token: 0x0400C276 RID: 49782
		NewPhase,
		// Token: 0x0400C277 RID: 49783
		ConfirmExtratop,
		// Token: 0x0400C278 RID: 49784
		Move = 50,
		// Token: 0x0400C279 RID: 49785
		PosChange = 53,
		// Token: 0x0400C27A RID: 49786
		Set,
		// Token: 0x0400C27B RID: 49787
		Swap,
		// Token: 0x0400C27C RID: 49788
		FieldDisabled,
		// Token: 0x0400C27D RID: 49789
		Summoning = 60,
		// Token: 0x0400C27E RID: 49790
		Summoned,
		// Token: 0x0400C27F RID: 49791
		SpSummoning,
		// Token: 0x0400C280 RID: 49792
		SpSummoned,
		// Token: 0x0400C281 RID: 49793
		FlipSummoning,
		// Token: 0x0400C282 RID: 49794
		FlipSummoned,
		// Token: 0x0400C283 RID: 49795
		Chaining = 70,
		// Token: 0x0400C284 RID: 49796
		Chained,
		// Token: 0x0400C285 RID: 49797
		ChainSolving,
		// Token: 0x0400C286 RID: 49798
		ChainSolved,
		// Token: 0x0400C287 RID: 49799
		ChainEnd,
		// Token: 0x0400C288 RID: 49800
		ChainNegated,
		// Token: 0x0400C289 RID: 49801
		ChainDisabled,
		// Token: 0x0400C28A RID: 49802
		CardSelected = 80,
		// Token: 0x0400C28B RID: 49803
		RandomSelected,
		// Token: 0x0400C28C RID: 49804
		BecomeTarget = 83,
		// Token: 0x0400C28D RID: 49805
		Draw = 90,
		// Token: 0x0400C28E RID: 49806
		Damage,
		// Token: 0x0400C28F RID: 49807
		Recover,
		// Token: 0x0400C290 RID: 49808
		Equip,
		// Token: 0x0400C291 RID: 49809
		LpUpdate,
		// Token: 0x0400C292 RID: 49810
		Unequip,
		// Token: 0x0400C293 RID: 49811
		CardTarget,
		// Token: 0x0400C294 RID: 49812
		CancelTarget,
		// Token: 0x0400C295 RID: 49813
		PayLpCost = 100,
		// Token: 0x0400C296 RID: 49814
		AddCounter,
		// Token: 0x0400C297 RID: 49815
		RemoveCounter,
		// Token: 0x0400C298 RID: 49816
		Attack = 110,
		// Token: 0x0400C299 RID: 49817
		Battle,
		// Token: 0x0400C29A RID: 49818
		AttackDiabled,
		// Token: 0x0400C29B RID: 49819
		DamageStepStart,
		// Token: 0x0400C29C RID: 49820
		DamageStepEnd,
		// Token: 0x0400C29D RID: 49821
		MissedEffect = 120,
		// Token: 0x0400C29E RID: 49822
		BeChainTarget,
		// Token: 0x0400C29F RID: 49823
		CreateRelation,
		// Token: 0x0400C2A0 RID: 49824
		ReleaseRelation,
		// Token: 0x0400C2A1 RID: 49825
		TossCoin = 130,
		// Token: 0x0400C2A2 RID: 49826
		TossDice,
		// Token: 0x0400C2A3 RID: 49827
		RockPaperScissors,
		// Token: 0x0400C2A4 RID: 49828
		HandResult,
		// Token: 0x0400C2A5 RID: 49829
		AnnounceRace = 140,
		// Token: 0x0400C2A6 RID: 49830
		AnnounceAttrib,
		// Token: 0x0400C2A7 RID: 49831
		AnnounceCard,
		// Token: 0x0400C2A8 RID: 49832
		AnnounceNumber,
		// Token: 0x0400C2A9 RID: 49833
		CardHint = 160,
		// Token: 0x0400C2AA RID: 49834
		TagSwap,
		// Token: 0x0400C2AB RID: 49835
		ReloadField,
		// Token: 0x0400C2AC RID: 49836
		AiName,
		// Token: 0x0400C2AD RID: 49837
		ShowHint,
		// Token: 0x0400C2AE RID: 49838
		PlayerHint,
		// Token: 0x0400C2AF RID: 49839
		MatchKill = 170,
		// Token: 0x0400C2B0 RID: 49840
		CustomMsg = 180,
		// Token: 0x0400C2B1 RID: 49841
		DuelWinner = 200
	}
}
