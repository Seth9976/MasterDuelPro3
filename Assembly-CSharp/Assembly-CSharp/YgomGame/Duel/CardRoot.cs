using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000CF2 RID: 3314
	public class CardRoot : MonoBehaviour, ICardStatusIconAnchor
	{
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06005EEB RID: 24299 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EEC RID: 24300 RVA: 0x0000216D File Offset: 0x0000036D
		public SharedDefinition.SummonMaterialType currentSummonMaterialType
		{
			[CompilerGenerated]
			get
			{
				return SharedDefinition.SummonMaterialType.None;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06005EED RID: 24301 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EEE RID: 24302 RVA: 0x0000216D File Offset: 0x0000036D
		public SharedDefinition.SummonMaterialType preparedSummonMaterialEffectType
		{
			[CompilerGenerated]
			get
			{
				return SharedDefinition.SummonMaterialType.None;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06005EEF RID: 24303 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EF0 RID: 24304 RVA: 0x0000216D File Offset: 0x0000036D
		public Dictionary<string, SimpleEffect> holdEffects
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06005EF1 RID: 24305 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isTerminated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInMonsterZone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06005EF3 RID: 24307 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInMagicZoon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoaded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06005EF5 RID: 24309 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EF6 RID: 24310 RVA: 0x0000216D File Offset: 0x0000036D
		public CardInstancePool cardPool
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06005EF7 RID: 24311 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EF8 RID: 24312 RVA: 0x0000216D File Offset: 0x0000036D
		public CardLocator cardLocator
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EFA RID: 24314 RVA: 0x0000216D File Offset: 0x0000036D
		public CardPlane cardPlane
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06005EFB RID: 24315 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EFC RID: 24316 RVA: 0x0000216D File Offset: 0x0000036D
		public CardStatus cardStatus
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EFE RID: 24318 RVA: 0x0000216D File Offset: 0x0000036D
		public CardRoot.State state
		{
			[CompilerGenerated]
			get
			{
				return CardRoot.State.Invalid;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform cardPostureTransform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F01 RID: 24321 RVA: 0x0000216D File Offset: 0x0000036D
		public bool manualUpdateSRT
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

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06005F02 RID: 24322 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject effectAnchor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06005F03 RID: 24323 RVA: 0x000F5234 File Offset: 0x000F3434
		public Quaternion localRot
		{
			get
			{
				return default(Quaternion);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06005F04 RID: 24324 RVA: 0x000F524C File Offset: 0x000F344C
		public Vector3 centerOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x000F5264 File Offset: 0x000F3464
		public Vector3 surfaceOffset
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06005F06 RID: 24326 RVA: 0x000F527C File Offset: 0x000F347C
		public Vector3 affectIconOffset
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06005F07 RID: 24327 RVA: 0x000F5294 File Offset: 0x000F3494
		public Vector3 atkDefOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06005F08 RID: 24328 RVA: 0x000F52AC File Offset: 0x000F34AC
		public Vector3 levelOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x000F52C4 File Offset: 0x000F34C4
		public Vector3 attrOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06005F0A RID: 24330 RVA: 0x000F52DC File Offset: 0x000F34DC
		public Vector3 typeOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06005F0B RID: 24331 RVA: 0x000F52F4 File Offset: 0x000F34F4
		public Vector3 counterOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x000F530C File Offset: 0x000F350C
		public Vector3 turnsOfs
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F0E RID: 24334 RVA: 0x0000216D File Offset: 0x0000036D
		public int team
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06005F0F RID: 24335 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F10 RID: 24336 RVA: 0x0000216D File Offset: 0x0000036D
		public int index
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

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F12 RID: 24338 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06005F13 RID: 24339 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F14 RID: 24340 RVA: 0x0000216D File Offset: 0x0000036D
		public int sleeveId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06005F15 RID: 24341 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F16 RID: 24342 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isFace
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

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F18 RID: 24344 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isAttack
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

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06005F19 RID: 24345 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F1A RID: 24346 RVA: 0x0000216D File Offset: 0x0000036D
		public int uniqueId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06005F1B RID: 24347 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F1C RID: 24348 RVA: 0x0000216D File Offset: 0x0000036D
		public int atk
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06005F1D RID: 24349 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F1E RID: 24350 RVA: 0x0000216D File Offset: 0x0000036D
		public int def
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06005F1F RID: 24351 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F20 RID: 24352 RVA: 0x0000216D File Offset: 0x0000036D
		public int orgAtk
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06005F21 RID: 24353 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F22 RID: 24354 RVA: 0x0000216D File Offset: 0x0000036D
		public int orgDef
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06005F23 RID: 24355 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F24 RID: 24356 RVA: 0x0000216D File Offset: 0x0000036D
		public int level
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F26 RID: 24358 RVA: 0x0000216D File Offset: 0x0000036D
		public int rank
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06005F27 RID: 24359 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F28 RID: 24360 RVA: 0x0000216D File Offset: 0x0000036D
		public int scale
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06005F29 RID: 24361 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F2A RID: 24362 RVA: 0x0000216D File Offset: 0x0000036D
		public int orgScale
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F2C RID: 24364 RVA: 0x0000216D File Offset: 0x0000036D
		public int orgLevel
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F2E RID: 24366 RVA: 0x0000216D File Offset: 0x0000036D
		public int orgRank
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F30 RID: 24368 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardAttr
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F32 RID: 24370 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cardAttrChanged
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06005F33 RID: 24371 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F34 RID: 24372 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardType
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06005F35 RID: 24373 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F36 RID: 24374 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cardTypeChanged
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F38 RID: 24376 RVA: 0x0000216D File Offset: 0x0000036D
		public int validCounter
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F3A RID: 24378 RVA: 0x0000216D File Offset: 0x0000036D
		public int validCounterCount
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005F3C RID: 24380 RVA: 0x0000216D File Offset: 0x0000036D
		public List<Engine.CounterType> validCounters
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06005F3D RID: 24381 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005F3E RID: 24382 RVA: 0x0000216D File Offset: 0x0000036D
		public List<int> validCountersCount
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F40 RID: 24384 RVA: 0x0000216D File Offset: 0x0000036D
		public int elapsedTurns
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F42 RID: 24386 RVA: 0x0000216D File Offset: 0x0000036D
		public bool cardDisabled
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F44 RID: 24388 RVA: 0x0000216D File Offset: 0x0000036D
		public int linkNum
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06005F45 RID: 24389 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005F46 RID: 24390 RVA: 0x0000216D File Offset: 0x0000036D
		public int orgLinkNum
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardRoot Create(CardInstancePool cardPool, GameObject go)
		{
			return null;
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNeedToCallRegister(CardLocator from, CardLocator to)
		{
			return false;
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardRoot.ModelType CheckModeByPosition(int position)
		{
			return (CardRoot.ModelType)0;
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCard(int cardId, int sleeveId, int uniqueId, Action onLoaded = null)
		{
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCard(int cardId, int sleeveId, int uniqueId, int styleId, Action onLoaded = null)
		{
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReloadTexture(Action onLoaded = null)
		{
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBasicVal()
		{
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateValidCounter()
		{
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStatusIcon()
		{
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x0000216D File Offset: 0x0000036D
		public void TransitionToActive(bool showModel)
		{
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x0000216D File Offset: 0x0000036D
		public void TransitionToReady()
		{
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x0000216D File Offset: 0x0000036D
		public void TransitionToSuspend()
		{
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x0000216D File Offset: 0x0000036D
		public void Placement(CardLocator cardLocator, bool isFace, bool isAttack, bool show)
		{
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqMoveFlipTurnEffect(CardLocator from, CardLocator to, bool isFace, bool isAttack, bool immediate, CardRootTransition transition, Action onStarted, Action onFinished)
		{
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqFlipTurn(bool isFace, bool isAttack, bool immediate, Action onStarted, Action onFinished)
		{
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartMove()
		{
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqMoveEffect(CardPlane.MoveTrailType type)
		{
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqMoveEffect(DuelEffectPool.Type eff_type, bool persitent_vision = false)
		{
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqStopMoveEffect()
		{
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqPlaySE(string label, bool is3D)
		{
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqStopSE(string label)
		{
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqDisplay(bool disp)
		{
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqHighlight(bool enable, SharedDefinition.ActivateAura type, int order = 0)
		{
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqChangeModelType(CardRoot.ModelType modelType)
		{
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x0000216D File Offset: 0x0000036D
		public void ModelChange(CardRoot.ModelType type)
		{
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsModelShowing()
		{
			return false;
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqAppealEffect(Action on_finished)
		{
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqCrackEffect(bool enable)
		{
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqBrokenEffect(CardPlane.BrokenType brokenType, Quaternion rotation)
		{
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqCastShadow(bool isOn)
		{
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqZoneEffect(int player, int position, bool getOut, bool isFace, CardRoot.ModelType modelType)
		{
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartSummonMaterialTargetEffect(SharedDefinition.SummonMaterialType material_type)
		{
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndSacrificeTargetEffect()
		{
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareSummonMaterialEffect(SharedDefinition.SummonMaterialType type)
		{
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqPreparedSummonMaterialEffect()
		{
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqSummonMaterialEffect(SharedDefinition.SummonMaterialType type)
		{
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqOneShotEffect(DuelEffectPool.Type type, CardEffectOneShotEffect.Mode mode, bool waitEffect, Quaternion rotation)
		{
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReqPlayHoldEffect(string holdEffectLabel, DuelEffectPool.Type type, CardEffectHoldEffect.Mode mode)
		{
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReqStopHoldEffect(string holdEffectLabel, bool immediate)
		{
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqDisappear(Action onFinished)
		{
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqCallback(Action callback, float delay = 0f)
		{
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqWait(Func<bool> waitFunc)
		{
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqAppearEffect(bool isToken, bool waitEffect, Action onFinished)
		{
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x0000216D File Offset: 0x0000036D
		public void Disapper()
		{
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateState()
		{
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowPlane()
		{
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x0000216D File Offset: 0x0000036D
		public void HidePlane()
		{
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetStatusValue()
		{
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnReturnInstance()
		{
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardPicture()
		{
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateSRT(bool force = false)
		{
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x0000216D File Offset: 0x0000036D
		private void ValidateFlipTurn()
		{
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x000F5324 File Offset: 0x000F3524
		public T GetElement<T>(string label) where T : global::UnityEngine.Object
		{
			return default(T);
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddEffect(CardEffectBase effect)
		{
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEffectFinished()
		{
			return false;
		}

		// Token: 0x06005F83 RID: 24451 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTargetEffectPlaying(Type type)
		{
			return false;
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsZoneEffectPlaying(ZoneCard.Zone zone, ZoneCard.Mode mode)
		{
			return false;
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMoveEffectRequested()
		{
			return false;
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateEffect()
		{
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearAllEffect()
		{
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPlace(int team, int position, int index)
		{
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x0000216D File Offset: 0x0000036D
		private void BuffEffect()
		{
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x0000216D File Offset: 0x0000036D
		private void DebuffEffect()
		{
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeEffect()
		{
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x0000216D File Offset: 0x0000036D
		public void EffectReset()
		{
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartAttackReady()
		{
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishAttackReady()
		{
		}

		// Token: 0x04009A47 RID: 39495
		private CardRoot.Step step;

		// Token: 0x04009A48 RID: 39496
		private ElementObjectManager eoManager;

		// Token: 0x04009A49 RID: 39497
		private Queue<CardEffectBase> effectQueue;

		// Token: 0x04009A4A RID: 39498
		private CardEffectBase currentEffect;

		// Token: 0x04009A4B RID: 39499
		private bool attackReady;

		// Token: 0x04009A4C RID: 39500
		public bool validVal;

		// Token: 0x04009A4D RID: 39501
		public int position;

		// Token: 0x04009A4E RID: 39502
		private bool m_AtkBuffFlag;

		// Token: 0x04009A4F RID: 39503
		private bool m_AtkDebuffFlag;

		// Token: 0x04009A50 RID: 39504
		private bool m_DefBuffFlag;

		// Token: 0x04009A51 RID: 39505
		private bool m_DefDebuffFlag;

		// Token: 0x04009A52 RID: 39506
		private int m_Atk;

		// Token: 0x04009A53 RID: 39507
		private int m_Def;

		// Token: 0x04009A54 RID: 39508
		private int m_Level;

		// Token: 0x04009A55 RID: 39509
		private int m_Rank;

		// Token: 0x04009A56 RID: 39510
		private int m_Scale;

		// Token: 0x04009A57 RID: 39511
		private int m_CardAttr;

		// Token: 0x04009A58 RID: 39512
		public int m_CardType;

		// Token: 0x02000CF3 RID: 3315
		public enum State
		{
			// Token: 0x04009A5A RID: 39514
			Invalid,
			// Token: 0x04009A5B RID: 39515
			Suspend,
			// Token: 0x04009A5C RID: 39516
			Ready,
			// Token: 0x04009A5D RID: 39517
			Active
		}

		// Token: 0x02000CF4 RID: 3316
		public enum ModelType
		{
			// Token: 0x04009A5F RID: 39519
			StoneMonster = 1,
			// Token: 0x04009A60 RID: 39520
			StoneMagic = 4,
			// Token: 0x04009A61 RID: 39521
			OcgCard = 8,
			// Token: 0x04009A62 RID: 39522
			Soul = 16,
			// Token: 0x04009A63 RID: 39523
			SummonMaterial = 32
		}

		// Token: 0x02000CF5 RID: 3317
		private enum Step
		{
			// Token: 0x04009A65 RID: 39525
			Idle,
			// Token: 0x04009A66 RID: 39526
			Move,
			// Token: 0x04009A67 RID: 39527
			Summon
		}
	}
}
