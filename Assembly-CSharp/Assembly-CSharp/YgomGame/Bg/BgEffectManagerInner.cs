using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Bg
{
	// Token: 0x0200112C RID: 4396
	public class BgEffectManagerInner : ElementObjectManager
	{
		// Token: 0x060082F0 RID: 33520 RVA: 0x0000216A File Offset: 0x0000036A
		public BgUnit GetBgUnit()
		{
			return null;
		}

		// Token: 0x060082F1 RID: 33521 RVA: 0x000F69A6 File Offset: 0x000F4BA6
		public ElementObject[] GetSerializedElements()
		{
			return this.serializedElements;
		}

		// Token: 0x060082F2 RID: 33522 RVA: 0x000F69AE File Offset: 0x000F4BAE
		private void Awake()
		{
			this.Initialize(this.animationEventParams, new BgUnit(base.name.ToLower().Contains("near") ? BgUnit.Side.Near : BgUnit.Side.Far));
		}

		// Token: 0x060082F3 RID: 33523 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060082F4 RID: 33524 RVA: 0x000F69DC File Offset: 0x000F4BDC
		public void Initialize(BgEffectManagerInner.BgAnimationEventParam[] animationEventParams, BgUnit unit)
		{
			this.initalized = true;
			this.triggerSettings = new List<BgEffectSettingInner>();
			foreach (ElementObject element in this.serializedElements)
			{
				if (element is BgEffectSettingInner)
				{
					this.triggerSettings.Add(element as BgEffectSettingInner);
				}
			}
			this.effectSettings = new Dictionary<BgEffectSettingInner.AnimationLabelDefine, List<BgEffectSettingInner>>();
			for (int i = 0; i < 27; i++)
			{
				BgEffectSettingInner.AnimationLabelDefine labelDefine = (BgEffectSettingInner.AnimationLabelDefine)i;
				List<BgEffectSettingInner> list = new List<BgEffectSettingInner>();
				foreach (BgEffectSettingInner setting in this.triggerSettings)
				{
					if (setting.animationLabel == labelDefine)
					{
						list.Add(setting);
					}
				}
				if (list.Count > 0)
				{
					this.effectSettings.Add(labelDefine, list);
				}
			}
		}

		// Token: 0x060082F5 RID: 33525 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(BgEffectManagerInner childlenMng, ElementObject[] elementObjects, bool isOtherSideElement = false)
		{
		}

		// Token: 0x060082F6 RID: 33526 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupBgEfectSettings(ElementObject[] elementObjects, bool isOtherSideElement = false)
		{
		}

		// Token: 0x060082F7 RID: 33527 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAnimationEvent(string str)
		{
		}

		// Token: 0x060082F8 RID: 33528 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayAdditionalSeEvent(string label)
		{
		}

		// Token: 0x060082F9 RID: 33529 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEffect(BgEffectSettingInner.AnimationLabelDefine label)
		{
		}

		// Token: 0x060082FA RID: 33530 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableTapEffect(BgEffectSettingInner.AnimationLabelDefine label, bool flg)
		{
		}

		// Token: 0x060082FB RID: 33531 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableLoopEffect(BgEffectSettingInner.AnimationLabelDefine label, bool flg)
		{
		}

		// Token: 0x060082FC RID: 33532 RVA: 0x000F6AC0 File Offset: 0x000F4CC0
		public void PlayAnimatorTriggerDelay(BgEffectSettingInner.TriggerLabelDefine label, float delay, string seLabel = "")
		{
			base.StartCoroutine(this.PlayAnimatorTriggerDelayCoroutine(label, delay, seLabel));
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x000F6AD2 File Offset: 0x000F4CD2
		private IEnumerator PlayAnimatorTriggerDelayCoroutine(BgEffectSettingInner.TriggerLabelDefine label, float delay, string seLabel = "")
		{
			yield return new WaitForSeconds(delay);
			this.PlayAnimatorTrigger(label, seLabel);
			yield break;
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x000F6AF8 File Offset: 0x000F4CF8
		public void PlayAnimatorTrigger(BgEffectSettingInner.TriggerLabelDefine label, string seLabel = "")
		{
			AudioManager.PlaySE(seLabel, 1f);
			using (List<BgEffectManagerInner.BgEffectAdditionalSe>.Enumerator enumerator = this.additionalSeList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BgEffectManagerInner.BgEffectAdditionalSe add = enumerator.Current;
					if (add.animationName == label.ToString())
					{
						DOTween.To(delegate(float v)
						{
						}, 0f, 0f, add.time).OnComplete(delegate
						{
							AudioManager.PlaySE(add.seLabel, 0.6f);
						});
					}
				}
			}
			BgEffectSettingInner.AnimationLabelDefine animationLabel = BgEffectSettingInner.AnimationLabelDefine.None;
			foreach (BgEffectManagerInner.BgAnimationEventParam param in this.animationEventParams)
			{
				if (param.trigger == label)
				{
					animationLabel = param.animationLabel;
					break;
				}
			}
			Animator animator;
			if (base.TryGetComponent<Animator>(out animator))
			{
				animator.SetTrigger(label.ToString());
			}
			List<BgEffectSettingInner> settings;
			this.effectSettings.TryGetValue(BgEffectSettingInner.AnimationLabelDefine.None, out settings);
			if (settings != null)
			{
				foreach (BgEffectSettingInner setting in settings)
				{
					if (setting != null && setting.gameObject != null)
					{
						setting.gameObject.SetActive(true);
						setting.PlayEffect(label);
						if (setting.particle != null)
						{
							setting.particle.Play();
						}
						if (setting.animator != null)
						{
							setting.animator.SetTrigger(label.ToString());
						}
						else
						{
							setting.gameObject.SetActive(false);
							setting.gameObject.SetActive(true);
						}
					}
				}
			}
			this.effectSettings.TryGetValue(animationLabel, out settings);
			if (settings != null)
			{
				foreach (BgEffectSettingInner setting2 in settings)
				{
					if (setting2 != null && setting2.gameObject != null)
					{
						setting2.gameObject.SetActive(true);
						setting2.PlayEffect(label);
						if (setting2.particle != null)
						{
							setting2.particle.Play();
						}
						if (setting2.animator != null)
						{
							setting2.animator.SetTrigger(label.ToString());
						}
						else
						{
							setting2.gameObject.SetActive(false);
							setting2.gameObject.SetActive(true);
						}
					}
				}
			}
		}

		// Token: 0x060082FF RID: 33535 RVA: 0x000F6DEC File Offset: 0x000F4FEC
		public void PlayTapAnimation()
		{
			if (this.IsTapPlaying())
			{
				return;
			}
			List<BgEffectSettingInner> settings;
			this.effectSettings.TryGetValue(BgEffectSettingInner.AnimationLabelDefine.TapAll, out settings);
			foreach (BgEffectSettingInner bgEffectSettingInner in settings)
			{
				bgEffectSettingInner.gameObject.SetActive(true);
				bgEffectSettingInner.PlayTapEffect();
			}
			string se = "SE_FIELD_MAT" + base.name.Substring(4, 3) + "_TAP";
			try
			{
				AudioManager.PlaySE(se, 1f);
			}
			catch
			{
			}
			if (base.name.ToLower().Contains("near"))
			{
				se += "_P";
			}
			else
			{
				se += "_R";
			}
			try
			{
				AudioManager.PlaySE(se, 1f);
			}
			catch
			{
			}
		}

		// Token: 0x06008300 RID: 33536 RVA: 0x000F6EE4 File Offset: 0x000F50E4
		public bool IsTapPlaying()
		{
			List<BgEffectSettingInner> settings;
			this.effectSettings.TryGetValue(BgEffectSettingInner.AnimationLabelDefine.TapAll, out settings);
			using (List<BgEffectSettingInner>.Enumerator enumerator = settings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsTapPlaying())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008301 RID: 33537 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnableSE(bool flg)
		{
		}

		// Token: 0x06008302 RID: 33538 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRootAnimatorSpeed(float speed)
		{
		}

		// Token: 0x06008303 RID: 33539 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCheckDamageCallback(BgUnit.BgPhase phase, BgEffectManagerInner.CheckDamageCallback cb)
		{
		}

		// Token: 0x0400BE06 RID: 48646
		private List<BgEffectSettingInner.TriggerLabelDefine> phaseCheckLabel;

		// Token: 0x0400BE07 RID: 48647
		private const string animationEventFuncName = "PlayAnimationEvent";

		// Token: 0x0400BE08 RID: 48648
		private const string additionalSeEventFuncName = "PlayAdditionalSeEvent";

		// Token: 0x0400BE09 RID: 48649
		private Dictionary<BgEffectSettingInner.AnimationLabelDefine, List<BgEffectSettingInner>> effectSettings;

		// Token: 0x0400BE0A RID: 48650
		private List<BgEffectSettingInner> triggerSettings;

		// Token: 0x0400BE0B RID: 48651
		private List<BgEffectManagerInner.BgEffectRequest> playEffectReqList;

		// Token: 0x0400BE0C RID: 48652
		private List<BgEffectManagerInner.BgEffectRequest> removeEffectList;

		// Token: 0x0400BE0D RID: 48653
		private List<BgEffectManagerInner.BgPhaseChangeSeRequest> playSeReqList;

		// Token: 0x0400BE0E RID: 48654
		private Animator rootAnimator;

		// Token: 0x0400BE0F RID: 48655
		public float finishAnimationDelay;

		// Token: 0x0400BE10 RID: 48656
		private Dictionary<string, BgEffectManagerInner.BgAnimationEventParam> animationEventParamDic;

		// Token: 0x0400BE11 RID: 48657
		private BgUnit bgUnit;

		// Token: 0x0400BE12 RID: 48658
		private bool initalized;

		// Token: 0x0400BE13 RID: 48659
		private bool enableSe;

		// Token: 0x0400BE14 RID: 48660
		private BgEffectManagerInner.CheckDamageCallback checkDamageCallback;

		// Token: 0x0400BE15 RID: 48661
		private BgUnit.BgPhase checkDamagePhase;

		// Token: 0x0400BE16 RID: 48662
		public List<BgEffectManagerInner.BgEffectAdditionalSe> additionalSeList;

		// Token: 0x0400BE17 RID: 48663
		private BgEffectManagerInner.BgAnimationEventParam[] animationEventParams = new BgEffectManagerInner.BgAnimationEventParam[]
		{
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.StartToPhase1, BgEffectSettingInner.AnimationLabelDefine.Start),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.Phase1ToDamagePhase1, BgEffectSettingInner.AnimationLabelDefine.DamagePhase1),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase1, BgEffectSettingInner.AnimationLabelDefine.LoopPhase1),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase1ToPhase2, BgEffectSettingInner.AnimationLabelDefine.ToPhase2),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.Phase2ToDamagePhase2, BgEffectSettingInner.AnimationLabelDefine.DamagePhase2),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase2, BgEffectSettingInner.AnimationLabelDefine.LoopPhase2),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase2ToPhase3, BgEffectSettingInner.AnimationLabelDefine.ToPhase3),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.Phase3ToDamagePhase3, BgEffectSettingInner.AnimationLabelDefine.DamagePhase3),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase3, BgEffectSettingInner.AnimationLabelDefine.LoopPhase3),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase3ToPhase4, BgEffectSettingInner.AnimationLabelDefine.ToPhase4),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.Phase4ToDamagePhase4, BgEffectSettingInner.AnimationLabelDefine.DamagePhase4),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase4ToPhase4, BgEffectSettingInner.AnimationLabelDefine.LoopPhase4),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhase4ToEnd, BgEffectSettingInner.AnimationLabelDefine.ToEnd),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.PhaseToDamagePhaseAll, BgEffectSettingInner.AnimationLabelDefine.DamagePhaseAll),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhaseToPhaseAll, BgEffectSettingInner.AnimationLabelDefine.ToPhaseAll),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.DamagePhaseToNextPhaseAll, BgEffectSettingInner.AnimationLabelDefine.KeepPhaseAll),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.EndWin, BgEffectSettingInner.AnimationLabelDefine.EndWin),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.EndLose, BgEffectSettingInner.AnimationLabelDefine.EndLose),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.TapPhase1, BgEffectSettingInner.AnimationLabelDefine.TapPhase1),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.TapPhase2, BgEffectSettingInner.AnimationLabelDefine.TapPhase2),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.TapPhase3, BgEffectSettingInner.AnimationLabelDefine.TapPhase3),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.TapPhase4, BgEffectSettingInner.AnimationLabelDefine.TapPhase4),
			new BgEffectManagerInner.BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine.TapAll, BgEffectSettingInner.AnimationLabelDefine.TapAll)
		};

		// Token: 0x0200112D RID: 4397
		public class BgEffectRequest
		{
			// Token: 0x0400BE18 RID: 48664
			public BgEffectSettingInner setting;

			// Token: 0x0400BE19 RID: 48665
			public float time;

			// Token: 0x0400BE1A RID: 48666
			public BgEffectSettingInner.AnimationLabelDefine animationLabel;

			// Token: 0x0400BE1B RID: 48667
			public BgEffectSettingInner.TriggerLabelDefine trigerLabel;
		}

		// Token: 0x0200112E RID: 4398
		public class BgPhaseChangeSeRequest
		{
			// Token: 0x0400BE1C RID: 48668
			public BgEffectSettingInner.TriggerLabelDefine trigerLabel;

			// Token: 0x0400BE1D RID: 48669
			public string seLabel;
		}

		// Token: 0x0200112F RID: 4399
		public class BgAnimationEventParam
		{
			// Token: 0x06008307 RID: 33543 RVA: 0x000F707B File Offset: 0x000F527B
			public BgAnimationEventParam(BgEffectSettingInner.TriggerLabelDefine tLabel, BgEffectSettingInner.AnimationLabelDefine aLabel)
			{
				this.trigger = tLabel;
				this.animationLabel = aLabel;
			}

			// Token: 0x0400BE1E RID: 48670
			public BgEffectSettingInner.TriggerLabelDefine trigger;

			// Token: 0x0400BE1F RID: 48671
			public BgEffectSettingInner.AnimationLabelDefine animationLabel;
		}

		// Token: 0x02001130 RID: 4400
		[Serializable]
		public class BgEffectAdditionalSe
		{
			// Token: 0x0400BE20 RID: 48672
			public string animationName;

			// Token: 0x0400BE21 RID: 48673
			public string seLabel;

			// Token: 0x0400BE22 RID: 48674
			public float time;
		}

		// Token: 0x02001131 RID: 4401
		// (Invoke) Token: 0x0600830A RID: 33546
		public delegate void CheckDamageCallback();
	}
}
