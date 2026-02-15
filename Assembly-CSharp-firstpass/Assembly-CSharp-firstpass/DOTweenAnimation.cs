using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
	// Token: 0x0200005F RID: 95
	[AddComponentMenu("DOTween/DOTween Animation")]
	public class DOTweenAnimation : ABSAnimationComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000151 RID: 337 RVA: 0x0000574C File Offset: 0x0000394C
		// (remove) Token: 0x06000152 RID: 338 RVA: 0x00005780 File Offset: 0x00003980
		public static event Action<DOTweenAnimation> OnReset;

		// Token: 0x06000153 RID: 339 RVA: 0x000057B3 File Offset: 0x000039B3
		private static void Dispatch_OnReset(DOTweenAnimation anim)
		{
			if (DOTweenAnimation.OnReset != null)
			{
				DOTweenAnimation.OnReset(anim);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000057C7 File Offset: 0x000039C7
		private void Awake()
		{
			if (!this.isActive || !this.autoGenerate)
			{
				return;
			}
			if (this.animationType != DOTweenAnimation.AnimationType.Move || !this.useTargetAsV3)
			{
				this.CreateTween(false, this.autoPlay);
				this._tweenAutoGenerationCalled = true;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000057FF File Offset: 0x000039FF
		private void Start()
		{
			if (this._tweenAutoGenerationCalled || !this.isActive || !this.autoGenerate)
			{
				return;
			}
			this.CreateTween(false, this.autoPlay);
			this._tweenAutoGenerationCalled = true;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000582E File Offset: 0x00003A2E
		private void Reset()
		{
			DOTweenAnimation.Dispatch_OnReset(this);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005836 File Offset: 0x00003A36
		private void OnDestroy()
		{
			if (this.tween != null && this.tween.active)
			{
				this.tween.Kill(false);
			}
			this.tween = null;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005860 File Offset: 0x00003A60
		public void RewindThenRecreateTween()
		{
			if (this.tween != null && this.tween.active)
			{
				this.tween.Rewind(true);
			}
			this.CreateTween(true, false);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000588B File Offset: 0x00003A8B
		public void RewindThenRecreateTweenAndPlay()
		{
			if (this.tween != null && this.tween.active)
			{
				this.tween.Rewind(true);
			}
			this.CreateTween(true, true);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000058B6 File Offset: 0x00003AB6
		public void RecreateTween()
		{
			this.CreateTween(true, false);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000058C0 File Offset: 0x00003AC0
		public void RecreateTweenAndPlay()
		{
			this.CreateTween(true, true);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000058CC File Offset: 0x00003ACC
		public void CreateTween(bool regenerateIfExists = false, bool andPlay = true)
		{
			if (!this.isValid)
			{
				if (regenerateIfExists)
				{
					Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation isn't valid and its tween won't be created", base.gameObject.name), base.gameObject);
				}
				return;
			}
			if (this.tween != null)
			{
				if (this.tween.active)
				{
					if (!regenerateIfExists)
					{
						return;
					}
					this.tween.Kill(false);
				}
				this.tween = null;
			}
			GameObject tweenGO = this.GetTweenGO();
			if (this.target == null || tweenGO == null)
			{
				if (this.targetIsSelf && this.target == null)
				{
					Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation's target is NULL, because the animation was created with a DOTween Pro version older than 0.9.255. To fix this, exit Play mode then simply select this object, and it will update automatically", base.gameObject.name), base.gameObject);
					return;
				}
				Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation's target/GameObject is unset: the tween will not be created.", base.gameObject.name), base.gameObject);
				return;
			}
			else
			{
				if (this.forcedTargetType != DOTweenAnimation.TargetType.Unset)
				{
					this.targetType = this.forcedTargetType;
				}
				if (this.targetType == DOTweenAnimation.TargetType.Unset)
				{
					this.targetType = DOTweenAnimation.TypeToDOTargetType(this.target.GetType());
				}
				switch (this.animationType)
				{
				case DOTweenAnimation.AnimationType.Move:
					if (this.useTargetAsV3)
					{
						this.isRelative = false;
						if (this.endValueTransform == null)
						{
							Debug.LogWarning(string.Format("{0} :: This tween's TO target is NULL, a Vector3 of (0,0,0) will be used instead", base.gameObject.name), base.gameObject);
							this.endValueV3 = Vector3.zero;
						}
						else if (this.targetType == DOTweenAnimation.TargetType.RectTransform)
						{
							RectTransform endValueT = this.endValueTransform as RectTransform;
							if (endValueT == null)
							{
								Debug.LogWarning(string.Format("{0} :: This tween's TO target should be a RectTransform, a Vector3 of (0,0,0) will be used instead", base.gameObject.name), base.gameObject);
								this.endValueV3 = Vector3.zero;
							}
							else
							{
								RectTransform rTarget = this.target as RectTransform;
								if (rTarget == null)
								{
									Debug.LogWarning(string.Format("{0} :: This tween's target and TO target are not of the same type. Please reassign the values", base.gameObject.name), base.gameObject);
								}
								else
								{
									this.endValueV3 = DOTweenModuleUI.Utils.SwitchToRectTransform(endValueT, rTarget);
								}
							}
						}
						else
						{
							this.endValueV3 = this.endValueTransform.position;
						}
					}
					switch (this.targetType)
					{
					case DOTweenAnimation.TargetType.RectTransform:
						this.tween = ((RectTransform)this.target).DOAnchorPos3D(this.endValueV3, this.duration, this.optionalBool0);
						break;
					case DOTweenAnimation.TargetType.Rigidbody:
						this.tween = ((Rigidbody)this.target).DOMove(this.endValueV3, this.duration, this.optionalBool0);
						break;
					case DOTweenAnimation.TargetType.Rigidbody2D:
						this.tween = ((Rigidbody2D)this.target).DOMove(this.endValueV3, this.duration, this.optionalBool0);
						break;
					case DOTweenAnimation.TargetType.Transform:
						this.tween = ((Transform)this.target).DOMove(this.endValueV3, this.duration, this.optionalBool0);
						break;
					}
					break;
				case DOTweenAnimation.AnimationType.LocalMove:
					this.tween = tweenGO.transform.DOLocalMove(this.endValueV3, this.duration, this.optionalBool0);
					break;
				case DOTweenAnimation.AnimationType.Rotate:
					switch (this.targetType)
					{
					case DOTweenAnimation.TargetType.Rigidbody:
						this.tween = ((Rigidbody)this.target).DORotate(this.endValueV3, this.duration, this.optionalRotationMode);
						break;
					case DOTweenAnimation.TargetType.Rigidbody2D:
						this.tween = ((Rigidbody2D)this.target).DORotate(this.endValueFloat, this.duration);
						break;
					case DOTweenAnimation.TargetType.Transform:
						this.tween = ((Transform)this.target).DORotate(this.endValueV3, this.duration, this.optionalRotationMode);
						break;
					}
					break;
				case DOTweenAnimation.AnimationType.LocalRotate:
					this.tween = tweenGO.transform.DOLocalRotate(this.endValueV3, this.duration, this.optionalRotationMode);
					break;
				case DOTweenAnimation.AnimationType.Scale:
				{
					DOTweenAnimation.TargetType targetType = this.targetType;
					this.tween = tweenGO.transform.DOScale(this.optionalBool0 ? new Vector3(this.endValueFloat, this.endValueFloat, this.endValueFloat) : this.endValueV3, this.duration);
					break;
				}
				case DOTweenAnimation.AnimationType.Color:
					this.isRelative = false;
					switch (this.targetType)
					{
					case DOTweenAnimation.TargetType.Image:
						this.tween = ((Graphic)this.target).DOColor(this.endValueColor, this.duration);
						break;
					case DOTweenAnimation.TargetType.Light:
						this.tween = ((Light)this.target).DOColor(this.endValueColor, this.duration);
						break;
					case DOTweenAnimation.TargetType.Renderer:
						this.tween = ((Renderer)this.target).material.DOColor(this.endValueColor, this.duration);
						break;
					case DOTweenAnimation.TargetType.SpriteRenderer:
						this.tween = ((SpriteRenderer)this.target).DOColor(this.endValueColor, this.duration);
						break;
					case DOTweenAnimation.TargetType.Text:
						this.tween = ((Text)this.target).DOColor(this.endValueColor, this.duration);
						break;
					}
					break;
				case DOTweenAnimation.AnimationType.Fade:
					this.isRelative = false;
					switch (this.targetType)
					{
					case DOTweenAnimation.TargetType.CanvasGroup:
						this.tween = ((CanvasGroup)this.target).DOFade(this.endValueFloat, this.duration);
						break;
					case DOTweenAnimation.TargetType.Image:
						this.tween = ((Graphic)this.target).DOFade(this.endValueFloat, this.duration);
						break;
					case DOTweenAnimation.TargetType.Light:
						this.tween = ((Light)this.target).DOIntensity(this.endValueFloat, this.duration);
						break;
					case DOTweenAnimation.TargetType.Renderer:
						this.tween = ((Renderer)this.target).material.DOFade(this.endValueFloat, this.duration);
						break;
					case DOTweenAnimation.TargetType.SpriteRenderer:
						this.tween = ((SpriteRenderer)this.target).DOFade(this.endValueFloat, this.duration);
						break;
					case DOTweenAnimation.TargetType.Text:
						this.tween = ((Text)this.target).DOFade(this.endValueFloat, this.duration);
						break;
					}
					break;
				case DOTweenAnimation.AnimationType.Text:
					if (this.targetType == DOTweenAnimation.TargetType.Text)
					{
						this.tween = ((Text)this.target).DOText(this.endValueString, this.duration, this.optionalBool0, this.optionalScrambleMode, this.optionalString);
					}
					break;
				case DOTweenAnimation.AnimationType.PunchPosition:
				{
					DOTweenAnimation.TargetType targetType2 = this.targetType;
					if (targetType2 != DOTweenAnimation.TargetType.RectTransform)
					{
						if (targetType2 == DOTweenAnimation.TargetType.Transform)
						{
							this.tween = ((Transform)this.target).DOPunchPosition(this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0, this.optionalBool0);
						}
					}
					else
					{
						this.tween = ((RectTransform)this.target).DOPunchAnchorPos(this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0, this.optionalBool0);
					}
					break;
				}
				case DOTweenAnimation.AnimationType.PunchRotation:
					this.tween = tweenGO.transform.DOPunchRotation(this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0);
					break;
				case DOTweenAnimation.AnimationType.PunchScale:
					this.tween = tweenGO.transform.DOPunchScale(this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0);
					break;
				case DOTweenAnimation.AnimationType.ShakePosition:
				{
					DOTweenAnimation.TargetType targetType2 = this.targetType;
					if (targetType2 != DOTweenAnimation.TargetType.RectTransform)
					{
						if (targetType2 == DOTweenAnimation.TargetType.Transform)
						{
							this.tween = ((Transform)this.target).DOShakePosition(this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, this.optionalBool0, this.optionalBool1, this.optionalShakeRandomnessMode);
						}
					}
					else
					{
						this.tween = ((RectTransform)this.target).DOShakeAnchorPos(this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, this.optionalBool0, this.optionalBool1, this.optionalShakeRandomnessMode);
					}
					break;
				}
				case DOTweenAnimation.AnimationType.ShakeRotation:
					this.tween = tweenGO.transform.DOShakeRotation(this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, this.optionalBool1, this.optionalShakeRandomnessMode);
					break;
				case DOTweenAnimation.AnimationType.ShakeScale:
					this.tween = tweenGO.transform.DOShakeScale(this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, this.optionalBool1, this.optionalShakeRandomnessMode);
					break;
				case DOTweenAnimation.AnimationType.CameraAspect:
					this.tween = ((Camera)this.target).DOAspect(this.endValueFloat, this.duration);
					break;
				case DOTweenAnimation.AnimationType.CameraBackgroundColor:
					this.tween = ((Camera)this.target).DOColor(this.endValueColor, this.duration);
					break;
				case DOTweenAnimation.AnimationType.CameraFieldOfView:
					this.tween = ((Camera)this.target).DOFieldOfView(this.endValueFloat, this.duration);
					break;
				case DOTweenAnimation.AnimationType.CameraOrthoSize:
					this.tween = ((Camera)this.target).DOOrthoSize(this.endValueFloat, this.duration);
					break;
				case DOTweenAnimation.AnimationType.CameraPixelRect:
					this.tween = ((Camera)this.target).DOPixelRect(this.endValueRect, this.duration);
					break;
				case DOTweenAnimation.AnimationType.CameraRect:
					this.tween = ((Camera)this.target).DORect(this.endValueRect, this.duration);
					break;
				case DOTweenAnimation.AnimationType.UIWidthHeight:
					this.tween = ((RectTransform)this.target).DOSizeDelta(this.optionalBool0 ? new Vector2(this.endValueFloat, this.endValueFloat) : this.endValueV2, this.duration, false);
					break;
				}
				if (this.tween == null)
				{
					return;
				}
				if (this.isFrom)
				{
					((Tweener)this.tween).From(this.isRelative);
				}
				else
				{
					this.tween.SetRelative(this.isRelative);
				}
				GameObject setTarget = this.GetTweenTarget();
				this.tween.SetTarget(setTarget).SetDelay(this.delay).SetLoops(this.loops, this.loopType)
					.SetAutoKill(this.autoKill)
					.OnKill(delegate
					{
						this.tween = null;
					});
				if (this.isSpeedBased)
				{
					this.tween.SetSpeedBased<Tween>();
				}
				if (this.easeType == Ease.INTERNAL_Custom)
				{
					this.tween.SetEase(this.easeCurve);
				}
				else
				{
					this.tween.SetEase(this.easeType);
				}
				if (!string.IsNullOrEmpty(this.id))
				{
					this.tween.SetId(this.id);
				}
				this.tween.SetUpdate(this.isIndependentUpdate);
				if (this.hasOnStart)
				{
					if (this.onStart != null)
					{
						this.tween.OnStart(new TweenCallback(this.onStart.Invoke));
					}
				}
				else
				{
					this.onStart = null;
				}
				if (this.hasOnPlay)
				{
					if (this.onPlay != null)
					{
						this.tween.OnPlay(new TweenCallback(this.onPlay.Invoke));
					}
				}
				else
				{
					this.onPlay = null;
				}
				if (this.hasOnUpdate)
				{
					if (this.onUpdate != null)
					{
						this.tween.OnUpdate(new TweenCallback(this.onUpdate.Invoke));
					}
				}
				else
				{
					this.onUpdate = null;
				}
				if (this.hasOnStepComplete)
				{
					if (this.onStepComplete != null)
					{
						this.tween.OnStepComplete(new TweenCallback(this.onStepComplete.Invoke));
					}
				}
				else
				{
					this.onStepComplete = null;
				}
				if (this.hasOnComplete)
				{
					if (this.onComplete != null)
					{
						this.tween.OnComplete(new TweenCallback(this.onComplete.Invoke));
					}
				}
				else
				{
					this.onComplete = null;
				}
				if (this.hasOnRewind)
				{
					if (this.onRewind != null)
					{
						this.tween.OnRewind(new TweenCallback(this.onRewind.Invoke));
					}
				}
				else
				{
					this.onRewind = null;
				}
				if (andPlay)
				{
					this.tween.Play<Tween>();
				}
				else
				{
					this.tween.Pause<Tween>();
				}
				if (this.hasOnTweenCreated && this.onTweenCreated != null)
				{
					this.onTweenCreated.Invoke();
				}
				return;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006584 File Offset: 0x00004784
		public List<Tween> GetTweens()
		{
			List<Tween> result = new List<Tween>();
			foreach (DOTweenAnimation anim in base.GetComponents<DOTweenAnimation>())
			{
				if (anim.tween != null && anim.tween.active)
				{
					result.Add(anim.tween);
				}
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000065D4 File Offset: 0x000047D4
		public void SetAnimationTarget(Component tweenTarget, bool useTweenTargetGameObjectForGroupOperations = true)
		{
			if (DOTweenAnimation.TypeToDOTargetType(this.target.GetType()) != this.targetType)
			{
				Debug.LogError("DOTweenAnimation ► SetAnimationTarget: the new target is of a different type from the one set in the Inspector");
				return;
			}
			this.target = tweenTarget;
			this.targetGO = this.target.gameObject;
			this.tweenTargetIsTargetGO = useTweenTargetGameObjectForGroupOperations;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006623 File Offset: 0x00004823
		public override void DOPlay()
		{
			DOTween.Play(this.GetTweenTarget());
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006631 File Offset: 0x00004831
		public override void DOPlayBackwards()
		{
			DOTween.PlayBackwards(this.GetTweenTarget());
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000663F File Offset: 0x0000483F
		public override void DOPlayForward()
		{
			DOTween.PlayForward(this.GetTweenTarget());
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000664D File Offset: 0x0000484D
		public override void DOPause()
		{
			DOTween.Pause(this.GetTweenTarget());
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000665B File Offset: 0x0000485B
		public override void DOTogglePause()
		{
			DOTween.TogglePause(this.GetTweenTarget());
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000666C File Offset: 0x0000486C
		public override void DORewind()
		{
			this._playCount = -1;
			DOTweenAnimation[] anims = base.gameObject.GetComponents<DOTweenAnimation>();
			for (int i = anims.Length - 1; i > -1; i--)
			{
				Tween t = anims[i].tween;
				if (t != null && t.IsInitialized())
				{
					anims[i].tween.Rewind(true);
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000066BE File Offset: 0x000048BE
		public override void DORestart()
		{
			this.DORestart(false);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000066C8 File Offset: 0x000048C8
		public override void DORestart(bool fromHere)
		{
			this._playCount = -1;
			if (this.tween == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(this.tween);
				}
				return;
			}
			if (fromHere && this.isRelative)
			{
				this.ReEvaluateRelativeTween();
			}
			DOTween.Restart(this.GetTweenTarget(), true, -1f);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000671B File Offset: 0x0000491B
		public override void DOComplete()
		{
			DOTween.Complete(this.GetTweenTarget(), false);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000672A File Offset: 0x0000492A
		public override void DOKill()
		{
			DOTween.Kill(this.GetTweenTarget(), false);
			this.tween = null;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006740 File Offset: 0x00004940
		public void DOPlayById(string id)
		{
			DOTween.Play(this.GetTweenTarget(), id);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000674F File Offset: 0x0000494F
		public void DOPlayAllById(string id)
		{
			DOTween.Play(id);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006758 File Offset: 0x00004958
		public void DOPauseAllById(string id)
		{
			DOTween.Pause(id);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006761 File Offset: 0x00004961
		public void DOPlayBackwardsById(string id)
		{
			DOTween.PlayBackwards(this.GetTweenTarget(), id);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006770 File Offset: 0x00004970
		public void DOPlayBackwardsAllById(string id)
		{
			DOTween.PlayBackwards(id);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006779 File Offset: 0x00004979
		public void DOPlayForwardById(string id)
		{
			DOTween.PlayForward(this.GetTweenTarget(), id);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006788 File Offset: 0x00004988
		public void DOPlayForwardAllById(string id)
		{
			DOTween.PlayForward(id);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006794 File Offset: 0x00004994
		public void DOPlayNext()
		{
			DOTweenAnimation[] anims = base.GetComponents<DOTweenAnimation>();
			while (this._playCount < anims.Length - 1)
			{
				this._playCount++;
				DOTweenAnimation anim = anims[this._playCount];
				if (anim != null && anim.tween != null && anim.tween.active && !anim.tween.IsPlaying() && !anim.tween.IsComplete())
				{
					anim.tween.Play<Tween>();
					return;
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006813 File Offset: 0x00004A13
		public void DORewindAndPlayNext()
		{
			this._playCount = -1;
			DOTween.Rewind(this.GetTweenTarget(), true);
			this.DOPlayNext();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000682F File Offset: 0x00004A2F
		public void DORewindAllById(string id)
		{
			this._playCount = -1;
			DOTween.Rewind(id, true);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006840 File Offset: 0x00004A40
		public void DORestartById(string id)
		{
			this._playCount = -1;
			DOTween.Restart(this.GetTweenTarget(), id, true, -1f);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000685C File Offset: 0x00004A5C
		public void DORestartAllById(string id)
		{
			this._playCount = -1;
			DOTween.Restart(id, true, -1f);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006872 File Offset: 0x00004A72
		public void DOKillById(string id)
		{
			DOTween.Kill(this.GetTweenTarget(), id, false);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006882 File Offset: 0x00004A82
		public void DOKillAllById(string id)
		{
			DOTween.Kill(id, false);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000688C File Offset: 0x00004A8C
		public static DOTweenAnimation.TargetType TypeToDOTargetType(Type t)
		{
			string str = t.ToString();
			int dotIndex = str.LastIndexOf(".");
			if (dotIndex != -1)
			{
				str = str.Substring(dotIndex + 1);
			}
			if (str.IndexOf("Renderer") != -1 && str != "SpriteRenderer")
			{
				str = "Renderer";
			}
			if (str == "RawImage" || str == "Graphic")
			{
				str = "Image";
			}
			return (DOTweenAnimation.TargetType)Enum.Parse(typeof(DOTweenAnimation.TargetType), str);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006910 File Offset: 0x00004B10
		public Tween CreateEditorPreview()
		{
			if (Application.isPlaying)
			{
				return null;
			}
			this.CreateTween(true, this.autoPlay);
			return this.tween;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000692E File Offset: 0x00004B2E
		private GameObject GetTweenGO()
		{
			if (!this.targetIsSelf)
			{
				return this.targetGO;
			}
			return base.gameObject;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006945 File Offset: 0x00004B45
		private GameObject GetTweenTarget()
		{
			if (!this.targetIsSelf && this.tweenTargetIsTargetGO)
			{
				return this.targetGO;
			}
			return base.gameObject;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006964 File Offset: 0x00004B64
		private void ReEvaluateRelativeTween()
		{
			GameObject tweenGO = this.GetTweenGO();
			if (tweenGO == null)
			{
				Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation's target/GameObject is unset: the tween will not be created.", base.gameObject.name), base.gameObject);
				return;
			}
			if (this.animationType == DOTweenAnimation.AnimationType.Move)
			{
				((Tweener)this.tween).ChangeEndValue(tweenGO.transform.position + this.endValueV3, true);
				return;
			}
			if (this.animationType == DOTweenAnimation.AnimationType.LocalMove)
			{
				((Tweener)this.tween).ChangeEndValue(tweenGO.transform.localPosition + this.endValueV3, true);
			}
		}

		// Token: 0x0400008E RID: 142
		public bool targetIsSelf = true;

		// Token: 0x0400008F RID: 143
		public GameObject targetGO;

		// Token: 0x04000090 RID: 144
		public bool tweenTargetIsTargetGO = true;

		// Token: 0x04000091 RID: 145
		public float delay;

		// Token: 0x04000092 RID: 146
		public float duration = 1f;

		// Token: 0x04000093 RID: 147
		public Ease easeType = Ease.OutQuad;

		// Token: 0x04000094 RID: 148
		public AnimationCurve easeCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04000095 RID: 149
		public LoopType loopType;

		// Token: 0x04000096 RID: 150
		public int loops = 1;

		// Token: 0x04000097 RID: 151
		public string id = "";

		// Token: 0x04000098 RID: 152
		public bool isRelative;

		// Token: 0x04000099 RID: 153
		public bool isFrom;

		// Token: 0x0400009A RID: 154
		public bool isIndependentUpdate;

		// Token: 0x0400009B RID: 155
		public bool autoKill = true;

		// Token: 0x0400009C RID: 156
		public bool autoGenerate = true;

		// Token: 0x0400009D RID: 157
		public bool isActive = true;

		// Token: 0x0400009E RID: 158
		public bool isValid;

		// Token: 0x0400009F RID: 159
		public Component target;

		// Token: 0x040000A0 RID: 160
		public DOTweenAnimation.AnimationType animationType;

		// Token: 0x040000A1 RID: 161
		public DOTweenAnimation.TargetType targetType;

		// Token: 0x040000A2 RID: 162
		public DOTweenAnimation.TargetType forcedTargetType;

		// Token: 0x040000A3 RID: 163
		public bool autoPlay = true;

		// Token: 0x040000A4 RID: 164
		public bool useTargetAsV3;

		// Token: 0x040000A5 RID: 165
		public float endValueFloat;

		// Token: 0x040000A6 RID: 166
		public Vector3 endValueV3;

		// Token: 0x040000A7 RID: 167
		public Vector2 endValueV2;

		// Token: 0x040000A8 RID: 168
		public Color endValueColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040000A9 RID: 169
		public string endValueString = "";

		// Token: 0x040000AA RID: 170
		public Rect endValueRect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x040000AB RID: 171
		public Transform endValueTransform;

		// Token: 0x040000AC RID: 172
		public bool optionalBool0;

		// Token: 0x040000AD RID: 173
		public bool optionalBool1;

		// Token: 0x040000AE RID: 174
		public float optionalFloat0;

		// Token: 0x040000AF RID: 175
		public int optionalInt0;

		// Token: 0x040000B0 RID: 176
		public RotateMode optionalRotationMode;

		// Token: 0x040000B1 RID: 177
		public ScrambleMode optionalScrambleMode;

		// Token: 0x040000B2 RID: 178
		public ShakeRandomnessMode optionalShakeRandomnessMode;

		// Token: 0x040000B3 RID: 179
		public string optionalString;

		// Token: 0x040000B4 RID: 180
		private bool _tweenAutoGenerationCalled;

		// Token: 0x040000B5 RID: 181
		private int _playCount = -1;

		// Token: 0x02000060 RID: 96
		public enum AnimationType
		{
			// Token: 0x040000B7 RID: 183
			None,
			// Token: 0x040000B8 RID: 184
			Move,
			// Token: 0x040000B9 RID: 185
			LocalMove,
			// Token: 0x040000BA RID: 186
			Rotate,
			// Token: 0x040000BB RID: 187
			LocalRotate,
			// Token: 0x040000BC RID: 188
			Scale,
			// Token: 0x040000BD RID: 189
			Color,
			// Token: 0x040000BE RID: 190
			Fade,
			// Token: 0x040000BF RID: 191
			Text,
			// Token: 0x040000C0 RID: 192
			PunchPosition,
			// Token: 0x040000C1 RID: 193
			PunchRotation,
			// Token: 0x040000C2 RID: 194
			PunchScale,
			// Token: 0x040000C3 RID: 195
			ShakePosition,
			// Token: 0x040000C4 RID: 196
			ShakeRotation,
			// Token: 0x040000C5 RID: 197
			ShakeScale,
			// Token: 0x040000C6 RID: 198
			CameraAspect,
			// Token: 0x040000C7 RID: 199
			CameraBackgroundColor,
			// Token: 0x040000C8 RID: 200
			CameraFieldOfView,
			// Token: 0x040000C9 RID: 201
			CameraOrthoSize,
			// Token: 0x040000CA RID: 202
			CameraPixelRect,
			// Token: 0x040000CB RID: 203
			CameraRect,
			// Token: 0x040000CC RID: 204
			UIWidthHeight
		}

		// Token: 0x02000061 RID: 97
		public enum TargetType
		{
			// Token: 0x040000CE RID: 206
			Unset,
			// Token: 0x040000CF RID: 207
			Camera,
			// Token: 0x040000D0 RID: 208
			CanvasGroup,
			// Token: 0x040000D1 RID: 209
			Image,
			// Token: 0x040000D2 RID: 210
			Light,
			// Token: 0x040000D3 RID: 211
			RectTransform,
			// Token: 0x040000D4 RID: 212
			Renderer,
			// Token: 0x040000D5 RID: 213
			SpriteRenderer,
			// Token: 0x040000D6 RID: 214
			Rigidbody,
			// Token: 0x040000D7 RID: 215
			Rigidbody2D,
			// Token: 0x040000D8 RID: 216
			Text,
			// Token: 0x040000D9 RID: 217
			Transform,
			// Token: 0x040000DA RID: 218
			tk2dBaseSprite,
			// Token: 0x040000DB RID: 219
			tk2dTextMesh,
			// Token: 0x040000DC RID: 220
			TextMeshPro,
			// Token: 0x040000DD RID: 221
			TextMeshProUGUI
		}
	}
}
