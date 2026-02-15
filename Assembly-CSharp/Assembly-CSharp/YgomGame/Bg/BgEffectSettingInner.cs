using System;
using System.Collections;
using System.Collections.Generic;
using MDPro3;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;

namespace YgomGame.Bg
{
	// Token: 0x02001136 RID: 4406
	public class BgEffectSettingInner : ElementObject
	{
		// Token: 0x06008319 RID: 33561 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnValidate()
		{
		}

		// Token: 0x0600831A RID: 33562 RVA: 0x000F7134 File Offset: 0x000F5334
		private void Awake()
		{
			if (this.animator != null)
			{
				this.animatorParams = new List<string>();
				foreach (AnimatorControllerParameter ani in this.animator.parameters)
				{
					this.animatorParams.Add(ani.name);
				}
			}
			if (this.disableLowEndPlatform && Program.root != "StandaloneWindows64/")
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.enableLowEndPlatformOnly && Program.root == "StandaloneWindows64/")
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.playingOnlyActive)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600831B RID: 33563 RVA: 0x000F71E8 File Offset: 0x000F53E8
		public bool PlayEffect(BgEffectSettingInner.TriggerLabelDefine triggerLabel)
		{
			if (this.disableLowEndPlatform && Program.root != "StandaloneWindows64/")
			{
				base.gameObject.SetActive(false);
				return false;
			}
			if (this.enableLowEndPlatformOnly && Program.root == "StandaloneWindows64/")
			{
				base.gameObject.SetActive(false);
				return false;
			}
			if (this.particle != null)
			{
				this.particle.Play();
			}
			if (this.animator != null)
			{
				this.animator.SetTrigger(triggerLabel.ToString());
			}
			base.StartCoroutine(this.CheckActiveTime());
			return true;
		}

		// Token: 0x0600831C RID: 33564 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableLoop(bool flg)
		{
		}

		// Token: 0x0600831D RID: 33565 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLoopEffect()
		{
		}

		// Token: 0x0600831E RID: 33566 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableTap(bool flg)
		{
		}

		// Token: 0x0600831F RID: 33567 RVA: 0x000F7290 File Offset: 0x000F5490
		public void PlayTapEffect()
		{
			if (this.IsTapPlaying())
			{
				return;
			}
			if (this.animator != null)
			{
				this.animator.SetTrigger("TapAll");
			}
			if (this.particle != null)
			{
				this.particle.Play();
			}
			base.StartCoroutine(this.CheckTapAnimationEnd());
		}

		// Token: 0x06008320 RID: 33568 RVA: 0x000F72EA File Offset: 0x000F54EA
		public bool IsTapPlaying()
		{
			return this.tapPlaying;
		}

		// Token: 0x06008321 RID: 33569 RVA: 0x000F72F2 File Offset: 0x000F54F2
		private IEnumerator CheckTapAnimationEnd()
		{
			this.tapPlaying = true;
			yield return new WaitForSeconds((this.activeTime == 0f) ? 4f : this.activeTime);
			this.tapPlaying = false;
			if (this.playingOnlyActive && base.name != "snakeEyes")
			{
				base.gameObject.SetActive(false);
			}
			yield break;
		}

		// Token: 0x06008322 RID: 33570 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator CheckParticleEffectEnd()
		{
			return null;
		}

		// Token: 0x06008323 RID: 33571 RVA: 0x000F7301 File Offset: 0x000F5501
		private IEnumerator CheckActiveTime()
		{
			yield return new WaitForSeconds((this.activeTime == 0f) ? 4f : this.activeTime);
			if (this.playingOnlyActive)
			{
				base.gameObject.SetActive(false);
			}
			yield break;
		}

		// Token: 0x06008324 RID: 33572 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine label)
		{
		}

		// Token: 0x06008325 RID: 33573 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAnimationEvent(string str)
		{
		}

		// Token: 0x06008326 RID: 33574 RVA: 0x000029CC File Offset: 0x00000BCC
		private DeviceInfo.ResourceType GetResourceType()
		{
			return DeviceInfo.ResourceType.Unknown;
		}

		// Token: 0x0400BE2C RID: 48684
		public float delay;

		// Token: 0x0400BE2D RID: 48685
		public float activeTime;

		// Token: 0x0400BE2E RID: 48686
		public BgEffectSettingInner.AnimationLabelDefine animationLabel;

		// Token: 0x0400BE2F RID: 48687
		public bool playingOnlyActive;

		// Token: 0x0400BE30 RID: 48688
		public Vector3 tapSize;

		// Token: 0x0400BE31 RID: 48689
		public Vector3 tapOffset;

		// Token: 0x0400BE32 RID: 48690
		public bool otherSide;

		// Token: 0x0400BE33 RID: 48691
		public bool isRootAnimator;

		// Token: 0x0400BE34 RID: 48692
		public float endDelay;

		// Token: 0x0400BE35 RID: 48693
		public bool disableLowEndPlatform;

		// Token: 0x0400BE36 RID: 48694
		public bool enableLowEndPlatformOnly;

		// Token: 0x0400BE37 RID: 48695
		public bool disableAudienceReplay;

		// Token: 0x0400BE38 RID: 48696
		public bool initialized;

		// Token: 0x0400BE39 RID: 48697
		public BgEffectManagerInner manager;

		// Token: 0x0400BE3A RID: 48698
		public ParticleSystem particle;

		// Token: 0x0400BE3B RID: 48699
		public Animator animator;

		// Token: 0x0400BE3C RID: 48700
		private MonoBehaviour monoBehaviour;

		// Token: 0x0400BE3D RID: 48701
		private float time;

		// Token: 0x0400BE3E RID: 48702
		private bool enableTap;

		// Token: 0x0400BE3F RID: 48703
		private bool tapPlaying;

		// Token: 0x0400BE40 RID: 48704
		private bool enableLoop;

		// Token: 0x0400BE41 RID: 48705
		private List<string> animatorParams;

		// Token: 0x02001137 RID: 4407
		public enum AnimationLabelDefine
		{
			// Token: 0x0400BE43 RID: 48707
			None,
			// Token: 0x0400BE44 RID: 48708
			Start,
			// Token: 0x0400BE45 RID: 48709
			LoopPhase1,
			// Token: 0x0400BE46 RID: 48710
			LoopPhase2,
			// Token: 0x0400BE47 RID: 48711
			LoopPhase3,
			// Token: 0x0400BE48 RID: 48712
			LoopPhase4,
			// Token: 0x0400BE49 RID: 48713
			LoopAll,
			// Token: 0x0400BE4A RID: 48714
			DamagePhase1,
			// Token: 0x0400BE4B RID: 48715
			DamagePhase2,
			// Token: 0x0400BE4C RID: 48716
			DamagePhase3,
			// Token: 0x0400BE4D RID: 48717
			DamagePhase4,
			// Token: 0x0400BE4E RID: 48718
			DamagePhaseAll,
			// Token: 0x0400BE4F RID: 48719
			ToPhase2,
			// Token: 0x0400BE50 RID: 48720
			ToPhase3,
			// Token: 0x0400BE51 RID: 48721
			ToPhase4,
			// Token: 0x0400BE52 RID: 48722
			ToEnd,
			// Token: 0x0400BE53 RID: 48723
			ToPhaseAll,
			// Token: 0x0400BE54 RID: 48724
			End,
			// Token: 0x0400BE55 RID: 48725
			EndWin,
			// Token: 0x0400BE56 RID: 48726
			EndLose,
			// Token: 0x0400BE57 RID: 48727
			TapPhase1,
			// Token: 0x0400BE58 RID: 48728
			TapPhase2,
			// Token: 0x0400BE59 RID: 48729
			TapPhase3,
			// Token: 0x0400BE5A RID: 48730
			TapPhase4,
			// Token: 0x0400BE5B RID: 48731
			TapAll,
			// Token: 0x0400BE5C RID: 48732
			KeepPhaseAll,
			// Token: 0x0400BE5D RID: 48733
			DefineMax
		}

		// Token: 0x02001138 RID: 4408
		public enum TriggerLabelDefine
		{
			// Token: 0x0400BE5F RID: 48735
			None,
			// Token: 0x0400BE60 RID: 48736
			StartToPhase1,
			// Token: 0x0400BE61 RID: 48737
			Phase1ToDamagePhase1,
			// Token: 0x0400BE62 RID: 48738
			DamagePhase1ToPhase1,
			// Token: 0x0400BE63 RID: 48739
			DamagePhase1ToPhase2,
			// Token: 0x0400BE64 RID: 48740
			Phase2ToDamagePhase2,
			// Token: 0x0400BE65 RID: 48741
			DamagePhase2ToPhase2,
			// Token: 0x0400BE66 RID: 48742
			DamagePhase2ToPhase3,
			// Token: 0x0400BE67 RID: 48743
			Phase3ToDamagePhase3,
			// Token: 0x0400BE68 RID: 48744
			DamagePhase3ToPhase3,
			// Token: 0x0400BE69 RID: 48745
			DamagePhase3ToPhase4,
			// Token: 0x0400BE6A RID: 48746
			Phase4ToDamagePhase4,
			// Token: 0x0400BE6B RID: 48747
			DamagePhase4ToPhase4,
			// Token: 0x0400BE6C RID: 48748
			DamagePhase4ToEnd,
			// Token: 0x0400BE6D RID: 48749
			PhaseToDamagePhaseAll,
			// Token: 0x0400BE6E RID: 48750
			DamagePhaseToPhaseAll,
			// Token: 0x0400BE6F RID: 48751
			DamagePhaseToNextPhaseAll,
			// Token: 0x0400BE70 RID: 48752
			EndWin,
			// Token: 0x0400BE71 RID: 48753
			EndLose,
			// Token: 0x0400BE72 RID: 48754
			TapPhase1,
			// Token: 0x0400BE73 RID: 48755
			TapPhase2,
			// Token: 0x0400BE74 RID: 48756
			TapPhase3,
			// Token: 0x0400BE75 RID: 48757
			TapPhase4,
			// Token: 0x0400BE76 RID: 48758
			TapAll,
			// Token: 0x0400BE77 RID: 48759
			OtherSidePhase1ToDamagePhase1,
			// Token: 0x0400BE78 RID: 48760
			OtherSideDamagePhase1ToPhase1,
			// Token: 0x0400BE79 RID: 48761
			OtherSideDamagePhase1ToPhase2,
			// Token: 0x0400BE7A RID: 48762
			OtherSidePhase2ToDamagePhase2,
			// Token: 0x0400BE7B RID: 48763
			OtherSideDamagePhase2ToPhase2,
			// Token: 0x0400BE7C RID: 48764
			OtherSideDamagePhase2ToPhase3,
			// Token: 0x0400BE7D RID: 48765
			OtherSidePhase3ToDamagePhase3,
			// Token: 0x0400BE7E RID: 48766
			OtherSideDamagePhase3ToPhase3,
			// Token: 0x0400BE7F RID: 48767
			OtherSideDamagePhase3ToPhase4,
			// Token: 0x0400BE80 RID: 48768
			OtherSidePhase4ToDamagePhase4,
			// Token: 0x0400BE81 RID: 48769
			OtherSideDamagePhase4ToPhase4,
			// Token: 0x0400BE82 RID: 48770
			OtherSideDamagePhase4ToEnd,
			// Token: 0x0400BE83 RID: 48771
			OtherSidePhaseToDamagePhaseAll,
			// Token: 0x0400BE84 RID: 48772
			OtherSideDamagePhaseToPhaseAll,
			// Token: 0x0400BE85 RID: 48773
			OtherSideDamagePhaseToNextPhaseAll,
			// Token: 0x0400BE86 RID: 48774
			StartToPhase1Extra
		}
	}
}
