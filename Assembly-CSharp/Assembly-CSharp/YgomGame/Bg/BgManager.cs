using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Duel;

namespace YgomGame.Bg
{
	// Token: 0x02001142 RID: 4418
	public class BgManager : MonoBehaviour
	{
		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600835A RID: 33626 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool useSphere
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600835B RID: 33627 RVA: 0x0000216A File Offset: 0x0000036A
		public BgMatModelSetting MatModelSetting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600835C RID: 33628 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600835D RID: 33629 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
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

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600835E RID: 33630 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600835F RID: 33631 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
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

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06008360 RID: 33632 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06008361 RID: 33633 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
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

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06008362 RID: 33634 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool bgCameraEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008363 RID: 33635 RVA: 0x0000216A File Offset: 0x0000036A
		public BgUnit GetBgUnit(BgUnit.Side side)
		{
			return null;
		}

		// Token: 0x06008364 RID: 33636 RVA: 0x0000216A File Offset: 0x0000036A
		public BgUnit GetBgUnit(bool isMyself)
		{
			return null;
		}

		// Token: 0x06008365 RID: 33637 RVA: 0x0000216A File Offset: 0x0000036A
		public static BgManager Create(DuelGameObjectManager goManager, GameObject root, string name, bool useCwId = true)
		{
			return null;
		}

		// Token: 0x06008366 RID: 33638 RVA: 0x0000216A File Offset: 0x0000036A
		public static BgManager Create(Transform root, string name, int sphereId, params int[] ids)
		{
			return null;
		}

		// Token: 0x06008367 RID: 33639 RVA: 0x0000216A File Offset: 0x0000036A
		private static BgManager Create(Transform root, string name, bool useCwId, int sphereId = 1)
		{
			return null;
		}

		// Token: 0x06008368 RID: 33640 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06008369 RID: 33641 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitalizeLoad()
		{
		}

		// Token: 0x0600836A RID: 33642 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadAudioClipCoroutine()
		{
			return null;
		}

		// Token: 0x0600836B RID: 33643 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayCameraStartAnimation()
		{
		}

		// Token: 0x0600836C RID: 33644 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayPlayCameraStartAnimationTimer(GameObject timer = null)
		{
		}

		// Token: 0x0600836D RID: 33645 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayPlayCameraStartAnimationPhaseSelect(GameObject phaseSelect = null)
		{
		}

		// Token: 0x0600836E RID: 33646 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEntryAnimation()
		{
		}

		// Token: 0x0600836F RID: 33647 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayDamageAnimation(Engine.DamageType type, int team, int damage)
		{
		}

		// Token: 0x06008370 RID: 33648 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFinishAnimation(int team)
		{
		}

		// Token: 0x06008371 RID: 33649 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayWinAnimation(int team)
		{
		}

		// Token: 0x06008372 RID: 33650 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLoseAnimation(int team)
		{
		}

		// Token: 0x06008373 RID: 33651 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEndAnimation(Engine.ResultType resultType)
		{
		}

		// Token: 0x06008374 RID: 33652 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayTurnChangeAnimation()
		{
		}

		// Token: 0x06008375 RID: 33653 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEffect(int side, BgEffectSettingInner.AnimationLabelDefine label)
		{
		}

		// Token: 0x06008376 RID: 33654 RVA: 0x0000216D File Offset: 0x0000036D
		public void ObjectTapCallback()
		{
		}

		// Token: 0x06008377 RID: 33655 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06008378 RID: 33656 RVA: 0x0000216D File Offset: 0x0000036D
		public void Inactivate()
		{
		}

		// Token: 0x06008379 RID: 33657 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingLightStep()
		{
		}

		// Token: 0x0600837B RID: 33659 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingSphereStep()
		{
		}

		// Token: 0x0600837C RID: 33660 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingMyMatStep()
		{
		}

		// Token: 0x0600837D RID: 33661 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingRivalMatStep()
		{
		}

		// Token: 0x0600837E RID: 33662 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingMyGraveStep()
		{
		}

		// Token: 0x0600837F RID: 33663 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingRivalGraveStep()
		{
		}

		// Token: 0x06008380 RID: 33664 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingAvatarStep()
		{
		}

		// Token: 0x06008381 RID: 33665 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x06008382 RID: 33666 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeSelector()
		{
		}

		// Token: 0x06008383 RID: 33667 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBgGraveHighlight(bool flg)
		{
		}

		// Token: 0x0400BED6 RID: 48854
		public const string bgLightResPath = "Duel/BG/Light/GlobalFieldLight";

		// Token: 0x0400BED7 RID: 48855
		public const string bgSphereResPath = "Duel/BG/CelestialSphere/CelestialSphere_{0}{1:000}/CelestialSphere_{0}{1:000}";

		// Token: 0x0400BED8 RID: 48856
		private BgManager.Step step;

		// Token: 0x0400BED9 RID: 48857
		private int[] matId;

		// Token: 0x0400BEDA RID: 48858
		private int[] standId;

		// Token: 0x0400BEDB RID: 48859
		private int[] graveId;

		// Token: 0x0400BEDC RID: 48860
		private int[] avatarId;

		// Token: 0x0400BEDD RID: 48861
		private int sphereId;

		// Token: 0x0400BEDE RID: 48862
		private bool useClientworkId;

		// Token: 0x0400BEDF RID: 48863
		private bool bgResLoaded;

		// Token: 0x0400BEE0 RID: 48864
		private GameObject bgLightRes;

		// Token: 0x0400BEE1 RID: 48865
		private GameObject bgLightObj;

		// Token: 0x0400BEE2 RID: 48866
		private bool bgLightLoaded;

		// Token: 0x0400BEE3 RID: 48867
		private GameObject bgSphereRes;

		// Token: 0x0400BEE4 RID: 48868
		private GameObject bgSphereObj;

		// Token: 0x0400BEE5 RID: 48869
		private bool bgSphereLoaded;

		// Token: 0x0400BEE6 RID: 48870
		private bool isErrModelSrc;

		// Token: 0x0400BEE7 RID: 48871
		private BgUnit[] bgUnits;

		// Token: 0x0400BEE8 RID: 48872
		private BgMatModelSetting matModelSetting;

		// Token: 0x0400BEE9 RID: 48873
		private BgGraveModelSetting graveModelSetting;

		// Token: 0x0400BEEA RID: 48874
		private BgStandModelSetting standModelSetting;

		// Token: 0x0400BEEB RID: 48875
		private AvatarModelSetting characterModelSetting;

		// Token: 0x02001143 RID: 4419
		private enum Step
		{
			// Token: 0x0400BEED RID: 48877
			InitializingLoad,
			// Token: 0x0400BEEE RID: 48878
			InitializingLight,
			// Token: 0x0400BEEF RID: 48879
			InitializingSphere,
			// Token: 0x0400BEF0 RID: 48880
			InitializingMyMat,
			// Token: 0x0400BEF1 RID: 48881
			InitializingRivalMat,
			// Token: 0x0400BEF2 RID: 48882
			InitializingMyGrave,
			// Token: 0x0400BEF3 RID: 48883
			InitializingRivalGrave,
			// Token: 0x0400BEF4 RID: 48884
			InitializingAvatar,
			// Token: 0x0400BEF5 RID: 48885
			Idle
		}
	}
}
