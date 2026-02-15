using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001535 RID: 5429
	public enum GameMessage
	{
		// Token: 0x0400DC94 RID: 56468
		Retry = 1,
		// Token: 0x0400DC95 RID: 56469
		Hint,
		// Token: 0x0400DC96 RID: 56470
		Waiting,
		// Token: 0x0400DC97 RID: 56471
		Start,
		// Token: 0x0400DC98 RID: 56472
		Win,
		// Token: 0x0400DC99 RID: 56473
		UpdateData,
		// Token: 0x0400DC9A RID: 56474
		UpdateCard,
		// Token: 0x0400DC9B RID: 56475
		RequestDeck,
		// Token: 0x0400DC9C RID: 56476
		SelectBattleCmd = 10,
		// Token: 0x0400DC9D RID: 56477
		SelectIdleCmd,
		// Token: 0x0400DC9E RID: 56478
		SelectEffectYn,
		// Token: 0x0400DC9F RID: 56479
		SelectYesNo,
		// Token: 0x0400DCA0 RID: 56480
		SelectOption,
		// Token: 0x0400DCA1 RID: 56481
		SelectCard,
		// Token: 0x0400DCA2 RID: 56482
		SelectChain,
		// Token: 0x0400DCA3 RID: 56483
		SelectPlace = 18,
		// Token: 0x0400DCA4 RID: 56484
		SelectPosition,
		// Token: 0x0400DCA5 RID: 56485
		SelectTribute,
		// Token: 0x0400DCA6 RID: 56486
		SortChain,
		// Token: 0x0400DCA7 RID: 56487
		SelectCounter,
		// Token: 0x0400DCA8 RID: 56488
		SelectSum,
		// Token: 0x0400DCA9 RID: 56489
		SelectDisfield,
		// Token: 0x0400DCAA RID: 56490
		SortCard,
		// Token: 0x0400DCAB RID: 56491
		SelectUnselect,
		// Token: 0x0400DCAC RID: 56492
		ConfirmDecktop = 30,
		// Token: 0x0400DCAD RID: 56493
		ConfirmCards,
		// Token: 0x0400DCAE RID: 56494
		ShuffleDeck,
		// Token: 0x0400DCAF RID: 56495
		ShuffleHand,
		// Token: 0x0400DCB0 RID: 56496
		RefreshDeck,
		// Token: 0x0400DCB1 RID: 56497
		SwapGraveDeck,
		// Token: 0x0400DCB2 RID: 56498
		ShuffleSetCard,
		// Token: 0x0400DCB3 RID: 56499
		ReverseDeck,
		// Token: 0x0400DCB4 RID: 56500
		DeckTop,
		// Token: 0x0400DCB5 RID: 56501
		ShuffleExtra,
		// Token: 0x0400DCB6 RID: 56502
		NewTurn,
		// Token: 0x0400DCB7 RID: 56503
		NewPhase,
		// Token: 0x0400DCB8 RID: 56504
		ConfirmExtratop,
		// Token: 0x0400DCB9 RID: 56505
		Move = 50,
		// Token: 0x0400DCBA RID: 56506
		PosChange = 53,
		// Token: 0x0400DCBB RID: 56507
		Set,
		// Token: 0x0400DCBC RID: 56508
		Swap,
		// Token: 0x0400DCBD RID: 56509
		FieldDisabled,
		// Token: 0x0400DCBE RID: 56510
		Summoning = 60,
		// Token: 0x0400DCBF RID: 56511
		Summoned,
		// Token: 0x0400DCC0 RID: 56512
		SpSummoning,
		// Token: 0x0400DCC1 RID: 56513
		SpSummoned,
		// Token: 0x0400DCC2 RID: 56514
		FlipSummoning,
		// Token: 0x0400DCC3 RID: 56515
		FlipSummoned,
		// Token: 0x0400DCC4 RID: 56516
		Chaining = 70,
		// Token: 0x0400DCC5 RID: 56517
		Chained,
		// Token: 0x0400DCC6 RID: 56518
		ChainSolving,
		// Token: 0x0400DCC7 RID: 56519
		ChainSolved,
		// Token: 0x0400DCC8 RID: 56520
		ChainEnd,
		// Token: 0x0400DCC9 RID: 56521
		ChainNegated,
		// Token: 0x0400DCCA RID: 56522
		ChainDisabled,
		// Token: 0x0400DCCB RID: 56523
		CardSelected = 80,
		// Token: 0x0400DCCC RID: 56524
		RandomSelected,
		// Token: 0x0400DCCD RID: 56525
		BecomeTarget = 83,
		// Token: 0x0400DCCE RID: 56526
		Draw = 90,
		// Token: 0x0400DCCF RID: 56527
		Damage,
		// Token: 0x0400DCD0 RID: 56528
		Recover,
		// Token: 0x0400DCD1 RID: 56529
		Equip,
		// Token: 0x0400DCD2 RID: 56530
		LpUpdate,
		// Token: 0x0400DCD3 RID: 56531
		Unequip,
		// Token: 0x0400DCD4 RID: 56532
		CardTarget,
		// Token: 0x0400DCD5 RID: 56533
		CancelTarget,
		// Token: 0x0400DCD6 RID: 56534
		PayLpCost = 100,
		// Token: 0x0400DCD7 RID: 56535
		AddCounter,
		// Token: 0x0400DCD8 RID: 56536
		RemoveCounter,
		// Token: 0x0400DCD9 RID: 56537
		Attack = 110,
		// Token: 0x0400DCDA RID: 56538
		Battle,
		// Token: 0x0400DCDB RID: 56539
		AttackDisabled,
		// Token: 0x0400DCDC RID: 56540
		DamageStepStart,
		// Token: 0x0400DCDD RID: 56541
		DamageStepEnd,
		// Token: 0x0400DCDE RID: 56542
		MissedEffect = 120,
		// Token: 0x0400DCDF RID: 56543
		BeChainTarget,
		// Token: 0x0400DCE0 RID: 56544
		CreateRelation,
		// Token: 0x0400DCE1 RID: 56545
		ReleaseRelation,
		// Token: 0x0400DCE2 RID: 56546
		TossCoin = 130,
		// Token: 0x0400DCE3 RID: 56547
		TossDice,
		// Token: 0x0400DCE4 RID: 56548
		RockPaperScissors,
		// Token: 0x0400DCE5 RID: 56549
		HandResult,
		// Token: 0x0400DCE6 RID: 56550
		AnnounceRace = 140,
		// Token: 0x0400DCE7 RID: 56551
		AnnounceAttrib,
		// Token: 0x0400DCE8 RID: 56552
		AnnounceCard,
		// Token: 0x0400DCE9 RID: 56553
		AnnounceNumber,
		// Token: 0x0400DCEA RID: 56554
		CardHint = 160,
		// Token: 0x0400DCEB RID: 56555
		TagSwap,
		// Token: 0x0400DCEC RID: 56556
		ReloadField,
		// Token: 0x0400DCED RID: 56557
		AiName,
		// Token: 0x0400DCEE RID: 56558
		ShowHint,
		// Token: 0x0400DCEF RID: 56559
		PlayerHint,
		// Token: 0x0400DCF0 RID: 56560
		MatchKill = 170,
		// Token: 0x0400DCF1 RID: 56561
		CustomMsg = 180,
		// Token: 0x0400DCF2 RID: 56562
		DuelWinner = 200,
		// Token: 0x0400DCF3 RID: 56563
		sibyl_chat = 230,
		// Token: 0x0400DCF4 RID: 56564
		sibyl_replay,
		// Token: 0x0400DCF5 RID: 56565
		sibyl_clear,
		// Token: 0x0400DCF6 RID: 56566
		sibyl_delay,
		// Token: 0x0400DCF7 RID: 56567
		sibyl_book,
		// Token: 0x0400DCF8 RID: 56568
		sibyl_name,
		// Token: 0x0400DCF9 RID: 56569
		sibyl_quit
	}
}
