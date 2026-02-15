using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Duel;

namespace YgomGame.Bg
{
	// Token: 0x02001149 RID: 4425
	public class BgUnit
	{
		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600839D RID: 33693 RVA: 0x000029CC File Offset: 0x00000BCC
		public int BgNo
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600839E RID: 33694 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600839F RID: 33695 RVA: 0x0000216D File Offset: 0x0000036D
		public int AvatarNo
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

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060083A0 RID: 33696 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060083A1 RID: 33697 RVA: 0x0000216D File Offset: 0x0000036D
		public int SubAvatarNo
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

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x060083A2 RID: 33698 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060083A3 RID: 33699 RVA: 0x0000216D File Offset: 0x0000036D
		public BgUnit.AvatarStandType StandType
		{
			[CompilerGenerated]
			get
			{
				return BgUnit.AvatarStandType.None;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x060083A4 RID: 33700 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060083A5 RID: 33701 RVA: 0x0000216D File Offset: 0x0000036D
		public int StandNo
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

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060083A6 RID: 33702 RVA: 0x000029CC File Offset: 0x00000BCC
		public BgGrave.GraveType GraveType
		{
			get
			{
				return BgGrave.GraveType.None;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060083A7 RID: 33703 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GraveNo
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060083A8 RID: 33704 RVA: 0x00002739 File Offset: 0x00000939
		public BgUnit(BgUnit.Side side)
		{
		}

		// Token: 0x060083A9 RID: 33705 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignSetting(int matNo, string matSeLabel, int avatarNo, int subAvatarNo, AvatarModelSetting modelSetting, BgUnit.AvatarStandType standType, int standNo, BgGrave.GraveType graveType, int graveNo)
		{
		}

		// Token: 0x060083AA RID: 33706 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAvatarNo(int avatarNo, string avatarResPath, int subAvatarNo = 0, string subAvatarResPath = "", string changeEffectPath = "")
		{
		}

		// Token: 0x060083AB RID: 33707 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBgNo(int no, string seLabel)
		{
		}

		// Token: 0x060083AC RID: 33708 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAvatarStandTypeAndNo(BgUnit.AvatarStandType type, int no)
		{
		}

		// Token: 0x060083AD RID: 33709 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGraveTypeAndNo(BgGrave.GraveType type, int no)
		{
		}

		// Token: 0x060083AE RID: 33710 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetGraveTypeInital()
		{
			return null;
		}

		// Token: 0x060083AF RID: 33711 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetObjectTapCallback(Action callback)
		{
		}

		// Token: 0x060083B0 RID: 33712 RVA: 0x0000216D File Offset: 0x0000036D
		public void Load(Action onLoad)
		{
		}

		// Token: 0x060083B1 RID: 33713 RVA: 0x0000216D File Offset: 0x0000036D
		private void load(BgUnit.BgResourceIdx idx, Action onLoad)
		{
		}

		// Token: 0x060083B2 RID: 33714 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(Transform root, AvatarModelSetting setting)
		{
		}

		// Token: 0x060083B3 RID: 33715 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeMat(Transform root)
		{
		}

		// Token: 0x060083B4 RID: 33716 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeGrave()
		{
		}

		// Token: 0x060083B5 RID: 33717 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeOtherSideEffectManager()
		{
		}

		// Token: 0x060083B6 RID: 33718 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeAvatar(AvatarModelSetting setting)
		{
		}

		// Token: 0x060083B7 RID: 33719 RVA: 0x0000216D File Offset: 0x0000036D
		private void CharaModelInit(AvatarModelSetting setting)
		{
		}

		// Token: 0x060083B8 RID: 33720 RVA: 0x0000216D File Offset: 0x0000036D
		private void CharaModelDestroy()
		{
		}

		// Token: 0x060083B9 RID: 33721 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeCharaModelSelector()
		{
		}

		// Token: 0x060083BA RID: 33722 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060083BB RID: 33723 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayBgStartAnimation()
		{
		}

		// Token: 0x060083BC RID: 33724 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayCharaEntryAnimation()
		{
		}

		// Token: 0x060083BD RID: 33725 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFinishAnimation()
		{
		}

		// Token: 0x060083BE RID: 33726 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayWinAnimation()
		{
		}

		// Token: 0x060083BF RID: 33727 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLoseAnimation()
		{
		}

		// Token: 0x060083C0 RID: 33728 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEndAnimation(Engine.ResultType resultType)
		{
		}

		// Token: 0x060083C1 RID: 33729 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayTurnChangeAnimation()
		{
		}

		// Token: 0x060083C2 RID: 33730 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayDamageAnimation(Engine.DamageType type, int lp)
		{
		}

		// Token: 0x060083C3 RID: 33731 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAttackAnimation()
		{
		}

		// Token: 0x060083C4 RID: 33732 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAvatarChangeAnimation(Character.SubAvatarChange condition, bool flg, Action callback = null)
		{
		}

		// Token: 0x060083C5 RID: 33733 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PlayAvatarChangeAnimationInner(Character.SubAvatarChange condition, Action callback = null)
		{
			return null;
		}

		// Token: 0x060083C6 RID: 33734 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckSubAvatarCondition(Character.SubAvatarChange condition)
		{
			return false;
		}

		// Token: 0x060083C7 RID: 33735 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckSubAvatarConditionParam(Character.SubAvatarChange condition, int target)
		{
			return false;
		}

		// Token: 0x060083C8 RID: 33736 RVA: 0x0000216A File Offset: 0x0000036A
		public Character GetActiveCharacter()
		{
			return null;
		}

		// Token: 0x060083C9 RID: 33737 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSubAvatar()
		{
			return false;
		}

		// Token: 0x060083CA RID: 33738 RVA: 0x0000216D File Offset: 0x0000036D
		public void MateEnable(bool flg)
		{
		}

		// Token: 0x060083CB RID: 33739 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayMatTapAnimation()
		{
		}

		// Token: 0x060083CC RID: 33740 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMatTapPlaying()
		{
			return false;
		}

		// Token: 0x060083CD RID: 33741 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnObjectTapCallback()
		{
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x0000216D File Offset: 0x0000036D
		public void CheckDamagePhase(int lp, BgEffectManagerInner.CheckDamageCallback cb = null)
		{
		}

		// Token: 0x060083CF RID: 33743 RVA: 0x0000216D File Offset: 0x0000036D
		public void CheckDamagePhaseForAudienceReplay(int lp)
		{
		}

		// Token: 0x060083D0 RID: 33744 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEffect(BgEffectSettingInner.AnimationLabelDefine label)
		{
		}

		// Token: 0x060083D1 RID: 33745 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableTapEffect(BgEffectSettingInner.AnimationLabelDefine label, bool flg)
		{
		}

		// Token: 0x060083D2 RID: 33746 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableLoopEffect(BgEffectSettingInner.AnimationLabelDefine label, bool flg)
		{
		}

		// Token: 0x060083D3 RID: 33747 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine label, string seLabel = "")
		{
		}

		// Token: 0x060083D4 RID: 33748 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetMatSeLabel(BgUnit.BgPhase phase)
		{
			return null;
		}

		// Token: 0x060083D5 RID: 33749 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetMatTapSeLabel()
		{
			return null;
		}

		// Token: 0x060083D6 RID: 33750 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayStartAnimationForPreviewParts(GameObject obj)
		{
		}

		// Token: 0x0400BF08 RID: 48904
		private BgEffectSettingInner.TriggerLabelDefine[] damageTriggerDefines;

		// Token: 0x0400BF09 RID: 48905
		private BgEffectSettingInner.TriggerLabelDefine[] phaseChangeDefines;

		// Token: 0x0400BF0A RID: 48906
		private BgEffectManagerInner.BgAnimationEventParam[] eventParams;

		// Token: 0x0400BF0B RID: 48907
		private BgEffectSettingInner.TriggerLabelDefine[] phaseKeepDefines;

		// Token: 0x0400BF0C RID: 48908
		private BgEffectSettingInner.AnimationLabelDefine[] loopDefines;

		// Token: 0x0400BF0D RID: 48909
		private BgEffectSettingInner.AnimationLabelDefine[] tapPhaseDefines;

		// Token: 0x0400BF0E RID: 48910
		private const float avatarSePan = 0.4f;

		// Token: 0x0400BF0F RID: 48911
		private BgUnit.Side playerSide;

		// Token: 0x0400BF10 RID: 48912
		private int bgNo;

		// Token: 0x0400BF11 RID: 48913
		private string bgSeLabel;

		// Token: 0x0400BF12 RID: 48914
		private string[] bgResPath;

		// Token: 0x0400BF13 RID: 48915
		private GameObject[] bgModelSrc;

		// Token: 0x0400BF14 RID: 48916
		private GameObject bgModel;

		// Token: 0x0400BF15 RID: 48917
		private BgEffectManagerInner effectManager;

		// Token: 0x0400BF16 RID: 48918
		private GameObject avatarStandModel;

		// Token: 0x0400BF17 RID: 48919
		private int graveNo;

		// Token: 0x0400BF18 RID: 48920
		private BgGrave.GraveType graveType;

		// Token: 0x0400BF19 RID: 48921
		public BgGrave bgGrave;

		// Token: 0x0400BF1A RID: 48922
		private int loadCount;

		// Token: 0x0400BF1B RID: 48923
		public bool isErrModelSrc;

		// Token: 0x0400BF1C RID: 48924
		public bool bgResLoaded;

		// Token: 0x0400BF1D RID: 48925
		private BgUnit.BgPhase phase;

		// Token: 0x0400BF1E RID: 48926
		public Transform avatarStandRoot;

		// Token: 0x0400BF1F RID: 48927
		public Transform avatarRoot;

		// Token: 0x0400BF20 RID: 48928
		public Transform graveRoot;

		// Token: 0x0400BF21 RID: 48929
		private Character activeCharacter;

		// Token: 0x0400BF22 RID: 48930
		private Character charaModel;

		// Token: 0x0400BF23 RID: 48931
		private Character subCharaModel;

		// Token: 0x0400BF24 RID: 48932
		private Dictionary<Character.SubAvatarChange, List<int>> conditionDic;

		// Token: 0x0400BF25 RID: 48933
		private bool usePreCharaChangeMotion;

		// Token: 0x0400BF26 RID: 48934
		private float changeDelay;

		// Token: 0x0400BF27 RID: 48935
		private BgAvatarChangeEffect changeEffect;

		// Token: 0x0400BF28 RID: 48936
		public BgUnit otherUnit;

		// Token: 0x0400BF29 RID: 48937
		private Action objectTapCallback;

		// Token: 0x0200114A RID: 4426
		public enum Side
		{
			// Token: 0x0400BF2B RID: 48939
			Near,
			// Token: 0x0400BF2C RID: 48940
			Far
		}

		// Token: 0x0200114B RID: 4427
		public enum BgPhase
		{
			// Token: 0x0400BF2E RID: 48942
			Phase1,
			// Token: 0x0400BF2F RID: 48943
			Phase2,
			// Token: 0x0400BF30 RID: 48944
			Phase3,
			// Token: 0x0400BF31 RID: 48945
			Phase4,
			// Token: 0x0400BF32 RID: 48946
			PhaseEnd
		}

		// Token: 0x0200114C RID: 4428
		public enum BgResourceIdx
		{
			// Token: 0x0400BF34 RID: 48948
			BgModel,
			// Token: 0x0400BF35 RID: 48949
			AvatarStand,
			// Token: 0x0400BF36 RID: 48950
			Grave,
			// Token: 0x0400BF37 RID: 48951
			AvatarBase,
			// Token: 0x0400BF38 RID: 48952
			AvatarModel,
			// Token: 0x0400BF39 RID: 48953
			SubAvatarModel,
			// Token: 0x0400BF3A RID: 48954
			ChangeEffect,
			// Token: 0x0400BF3B RID: 48955
			Max
		}

		// Token: 0x0200114D RID: 4429
		public enum AvatarStandType
		{
			// Token: 0x0400BF3D RID: 48957
			None,
			// Token: 0x0400BF3E RID: 48958
			Common,
			// Token: 0x0400BF3F RID: 48959
			Unique
		}
	}
}
