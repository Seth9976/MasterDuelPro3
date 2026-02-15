using System;

namespace YGOSharp.OCGWrapper.Enums
{
	// Token: 0x020001D5 RID: 469
	public enum GameMessage
	{
		// Token: 0x04000C34 RID: 3124
		Retry = 1,
		// Token: 0x04000C35 RID: 3125
		Hint,
		// Token: 0x04000C36 RID: 3126
		Waiting,
		// Token: 0x04000C37 RID: 3127
		Start,
		// Token: 0x04000C38 RID: 3128
		Win,
		// Token: 0x04000C39 RID: 3129
		UpdateData,
		// Token: 0x04000C3A RID: 3130
		UpdateCard,
		// Token: 0x04000C3B RID: 3131
		RequestDeck,
		// Token: 0x04000C3C RID: 3132
		SelectBattleCmd = 10,
		// Token: 0x04000C3D RID: 3133
		SelectIdleCmd,
		// Token: 0x04000C3E RID: 3134
		SelectEffectYn,
		// Token: 0x04000C3F RID: 3135
		SelectYesNo,
		// Token: 0x04000C40 RID: 3136
		SelectOption,
		// Token: 0x04000C41 RID: 3137
		SelectCard,
		// Token: 0x04000C42 RID: 3138
		SelectChain,
		// Token: 0x04000C43 RID: 3139
		SelectPlace = 18,
		// Token: 0x04000C44 RID: 3140
		SelectPosition,
		// Token: 0x04000C45 RID: 3141
		SelectTribute,
		// Token: 0x04000C46 RID: 3142
		SortChain,
		// Token: 0x04000C47 RID: 3143
		SelectCounter,
		// Token: 0x04000C48 RID: 3144
		SelectSum,
		// Token: 0x04000C49 RID: 3145
		SelectDisfield,
		// Token: 0x04000C4A RID: 3146
		SortCard,
		// Token: 0x04000C4B RID: 3147
		SelectUnselect,
		// Token: 0x04000C4C RID: 3148
		ConfirmDecktop = 30,
		// Token: 0x04000C4D RID: 3149
		ConfirmCards,
		// Token: 0x04000C4E RID: 3150
		ShuffleDeck,
		// Token: 0x04000C4F RID: 3151
		ShuffleHand,
		// Token: 0x04000C50 RID: 3152
		RefreshDeck,
		// Token: 0x04000C51 RID: 3153
		SwapGraveDeck,
		// Token: 0x04000C52 RID: 3154
		ShuffleSetCard,
		// Token: 0x04000C53 RID: 3155
		ReverseDeck,
		// Token: 0x04000C54 RID: 3156
		DeckTop,
		// Token: 0x04000C55 RID: 3157
		ShuffleExtra,
		// Token: 0x04000C56 RID: 3158
		NewTurn,
		// Token: 0x04000C57 RID: 3159
		NewPhase,
		// Token: 0x04000C58 RID: 3160
		ConfirmExtratop,
		// Token: 0x04000C59 RID: 3161
		Move = 50,
		// Token: 0x04000C5A RID: 3162
		PosChange = 53,
		// Token: 0x04000C5B RID: 3163
		Set,
		// Token: 0x04000C5C RID: 3164
		Swap,
		// Token: 0x04000C5D RID: 3165
		FieldDisabled,
		// Token: 0x04000C5E RID: 3166
		Summoning = 60,
		// Token: 0x04000C5F RID: 3167
		Summoned,
		// Token: 0x04000C60 RID: 3168
		SpSummoning,
		// Token: 0x04000C61 RID: 3169
		SpSummoned,
		// Token: 0x04000C62 RID: 3170
		FlipSummoning,
		// Token: 0x04000C63 RID: 3171
		FlipSummoned,
		// Token: 0x04000C64 RID: 3172
		Chaining = 70,
		// Token: 0x04000C65 RID: 3173
		Chained,
		// Token: 0x04000C66 RID: 3174
		ChainSolving,
		// Token: 0x04000C67 RID: 3175
		ChainSolved,
		// Token: 0x04000C68 RID: 3176
		ChainEnd,
		// Token: 0x04000C69 RID: 3177
		ChainNegated,
		// Token: 0x04000C6A RID: 3178
		ChainDisabled,
		// Token: 0x04000C6B RID: 3179
		CardSelected = 80,
		// Token: 0x04000C6C RID: 3180
		RandomSelected,
		// Token: 0x04000C6D RID: 3181
		BecomeTarget = 83,
		// Token: 0x04000C6E RID: 3182
		Draw = 90,
		// Token: 0x04000C6F RID: 3183
		Damage,
		// Token: 0x04000C70 RID: 3184
		Recover,
		// Token: 0x04000C71 RID: 3185
		Equip,
		// Token: 0x04000C72 RID: 3186
		LpUpdate,
		// Token: 0x04000C73 RID: 3187
		Unequip,
		// Token: 0x04000C74 RID: 3188
		CardTarget,
		// Token: 0x04000C75 RID: 3189
		CancelTarget,
		// Token: 0x04000C76 RID: 3190
		PayLpCost = 100,
		// Token: 0x04000C77 RID: 3191
		AddCounter,
		// Token: 0x04000C78 RID: 3192
		RemoveCounter,
		// Token: 0x04000C79 RID: 3193
		Attack = 110,
		// Token: 0x04000C7A RID: 3194
		Battle,
		// Token: 0x04000C7B RID: 3195
		AttackDisabled,
		// Token: 0x04000C7C RID: 3196
		DamageStepStart,
		// Token: 0x04000C7D RID: 3197
		DamageStepEnd,
		// Token: 0x04000C7E RID: 3198
		MissedEffect = 120,
		// Token: 0x04000C7F RID: 3199
		BeChainTarget,
		// Token: 0x04000C80 RID: 3200
		CreateRelation,
		// Token: 0x04000C81 RID: 3201
		ReleaseRelation,
		// Token: 0x04000C82 RID: 3202
		TossCoin = 130,
		// Token: 0x04000C83 RID: 3203
		TossDice,
		// Token: 0x04000C84 RID: 3204
		RockPaperScissors,
		// Token: 0x04000C85 RID: 3205
		HandResult,
		// Token: 0x04000C86 RID: 3206
		AnnounceRace = 140,
		// Token: 0x04000C87 RID: 3207
		AnnounceAttrib,
		// Token: 0x04000C88 RID: 3208
		AnnounceCard,
		// Token: 0x04000C89 RID: 3209
		AnnounceNumber,
		// Token: 0x04000C8A RID: 3210
		AnnounceCardFilter,
		// Token: 0x04000C8B RID: 3211
		CardHint = 160,
		// Token: 0x04000C8C RID: 3212
		TagSwap,
		// Token: 0x04000C8D RID: 3213
		ReloadField,
		// Token: 0x04000C8E RID: 3214
		AiName,
		// Token: 0x04000C8F RID: 3215
		ShowHint,
		// Token: 0x04000C90 RID: 3216
		PlayerHint,
		// Token: 0x04000C91 RID: 3217
		MatchKill = 170,
		// Token: 0x04000C92 RID: 3218
		CustomMsg = 180,
		// Token: 0x04000C93 RID: 3219
		DuelWinner = 200
	}
}
