using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000032 RID: 50
	[RequireComponent(typeof(Animator))]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonMecanim-Component")]
	public class SkeletonMecanim : SkeletonRenderer, ISkeletonAnimation, ISpineComponent
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A808 File Offset: 0x00008A08
		public SkeletonMecanim.MecanimTranslator Translator
		{
			get
			{
				return this.translator;
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060001D5 RID: 469 RVA: 0x0000A810 File Offset: 0x00008A10
		// (remove) Token: 0x060001D6 RID: 470 RVA: 0x0000A848 File Offset: 0x00008A48
		protected event ISkeletonAnimationDelegate _OnAnimationRebuild;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060001D7 RID: 471 RVA: 0x0000A880 File Offset: 0x00008A80
		// (remove) Token: 0x060001D8 RID: 472 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		protected event UpdateBonesDelegate _BeforeApply;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060001D9 RID: 473 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		// (remove) Token: 0x060001DA RID: 474 RVA: 0x0000A928 File Offset: 0x00008B28
		protected event UpdateBonesDelegate _UpdateLocal;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060001DB RID: 475 RVA: 0x0000A960 File Offset: 0x00008B60
		// (remove) Token: 0x060001DC RID: 476 RVA: 0x0000A998 File Offset: 0x00008B98
		protected event UpdateBonesDelegate _UpdateWorld;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060001DD RID: 477 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		// (remove) Token: 0x060001DE RID: 478 RVA: 0x0000AA08 File Offset: 0x00008C08
		protected event UpdateBonesDelegate _UpdateComplete;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060001DF RID: 479 RVA: 0x0000AA3D File Offset: 0x00008C3D
		// (remove) Token: 0x060001E0 RID: 480 RVA: 0x0000AA46 File Offset: 0x00008C46
		public event ISkeletonAnimationDelegate OnAnimationRebuild
		{
			add
			{
				this._OnAnimationRebuild += value;
			}
			remove
			{
				this._OnAnimationRebuild -= value;
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060001E1 RID: 481 RVA: 0x0000AA4F File Offset: 0x00008C4F
		// (remove) Token: 0x060001E2 RID: 482 RVA: 0x0000AA58 File Offset: 0x00008C58
		public event UpdateBonesDelegate BeforeApply
		{
			add
			{
				this._BeforeApply += value;
			}
			remove
			{
				this._BeforeApply -= value;
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060001E3 RID: 483 RVA: 0x0000AA61 File Offset: 0x00008C61
		// (remove) Token: 0x060001E4 RID: 484 RVA: 0x0000AA6A File Offset: 0x00008C6A
		public event UpdateBonesDelegate UpdateLocal
		{
			add
			{
				this._UpdateLocal += value;
			}
			remove
			{
				this._UpdateLocal -= value;
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060001E5 RID: 485 RVA: 0x0000AA73 File Offset: 0x00008C73
		// (remove) Token: 0x060001E6 RID: 486 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public event UpdateBonesDelegate UpdateWorld
		{
			add
			{
				this._UpdateWorld += value;
			}
			remove
			{
				this._UpdateWorld -= value;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060001E7 RID: 487 RVA: 0x0000AA85 File Offset: 0x00008C85
		// (remove) Token: 0x060001E8 RID: 488 RVA: 0x0000AA8E File Offset: 0x00008C8E
		public event UpdateBonesDelegate UpdateComplete
		{
			add
			{
				this._UpdateComplete += value;
			}
			remove
			{
				this._UpdateComplete -= value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000AA97 File Offset: 0x00008C97
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000AA9F File Offset: 0x00008C9F
		public UpdateTiming UpdateTiming
		{
			get
			{
				return this.updateTiming;
			}
			set
			{
				this.updateTiming = value;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public override void Initialize(bool overwrite, bool quiet = false)
		{
			if (this.valid && !overwrite)
			{
				return;
			}
			base.Initialize(overwrite, quiet);
			if (!this.valid)
			{
				return;
			}
			if (this.translator == null)
			{
				this.translator = new SkeletonMecanim.MecanimTranslator();
			}
			this.translator.Initialize(base.GetComponent<Animator>(), this.skeletonDataAsset);
			this.wasUpdatedAfterInit = false;
			if (this._OnAnimationRebuild != null)
			{
				this._OnAnimationRebuild(this);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000AB17 File Offset: 0x00008D17
		public virtual void Update()
		{
			if (!this.valid || this.updateTiming != UpdateTiming.InUpdate)
			{
				return;
			}
			this.UpdateAnimation(Time.deltaTime);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000AB36 File Offset: 0x00008D36
		public virtual void FixedUpdate()
		{
			if (!this.valid || this.updateTiming != UpdateTiming.InFixedUpdate)
			{
				return;
			}
			this.UpdateAnimation(Time.deltaTime);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AB55 File Offset: 0x00008D55
		public virtual void Update(float deltaTime)
		{
			if (!this.valid)
			{
				return;
			}
			this.UpdateAnimation(deltaTime);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AB67 File Offset: 0x00008D67
		protected void UpdateAnimation(float deltaTime)
		{
			this.wasUpdatedAfterInit = true;
			if (this.updateMode <= UpdateMode.OnlyAnimationStatus)
			{
				return;
			}
			this.skeleton.Update(deltaTime);
			this.ApplyTransformMovementToPhysics();
			this.ApplyAnimation();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AB92 File Offset: 0x00008D92
		public virtual void ApplyAnimation()
		{
			if (this._BeforeApply != null)
			{
				this._BeforeApply(this);
			}
			this.translator.Apply(this.skeleton);
			this.AfterAnimationApplied();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public virtual void AfterAnimationApplied()
		{
			if (this._UpdateLocal != null)
			{
				this._UpdateLocal(this);
			}
			if (this._UpdateWorld == null)
			{
				this.UpdateWorldTransform(Skeleton.Physics.Update);
			}
			else
			{
				this.UpdateWorldTransform(Skeleton.Physics.Pose);
				this._UpdateWorld(this);
				this.UpdateWorldTransform(Skeleton.Physics.Update);
			}
			if (this._UpdateComplete != null)
			{
				this._UpdateComplete(this);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AC20 File Offset: 0x00008E20
		public override void LateUpdate()
		{
			if (this.updateTiming == UpdateTiming.InLateUpdate && this.valid && this.translator != null && this.translator.Animator != null)
			{
				this.UpdateAnimation(Time.deltaTime);
			}
			if (!this.wasUpdatedAfterInit)
			{
				this.Update();
			}
			base.LateUpdate();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AC78 File Offset: 0x00008E78
		public override void OnBecameVisible()
		{
			UpdateMode previousUpdateMode = this.updateMode;
			this.updateMode = UpdateMode.FullUpdate;
			if (previousUpdateMode != UpdateMode.FullUpdate && previousUpdateMode != UpdateMode.EverythingExceptMesh)
			{
				this.Update();
			}
			if (previousUpdateMode != UpdateMode.FullUpdate)
			{
				this.LateUpdate();
			}
		}

		// Token: 0x0400011A RID: 282
		[SerializeField]
		protected SkeletonMecanim.MecanimTranslator translator;

		// Token: 0x0400011B RID: 283
		private bool wasUpdatedAfterInit = true;

		// Token: 0x04000121 RID: 289
		[SerializeField]
		protected UpdateTiming updateTiming = UpdateTiming.InUpdate;

		// Token: 0x02000033 RID: 51
		[Serializable]
		public class MecanimTranslator
		{
			// Token: 0x1400002C RID: 44
			// (add) Token: 0x060001F5 RID: 501 RVA: 0x0000ACC4 File Offset: 0x00008EC4
			// (remove) Token: 0x060001F6 RID: 502 RVA: 0x0000ACFC File Offset: 0x00008EFC
			protected event SkeletonMecanim.MecanimTranslator.OnClipAppliedDelegate _OnClipApplied;

			// Token: 0x1400002D RID: 45
			// (add) Token: 0x060001F7 RID: 503 RVA: 0x0000AD31 File Offset: 0x00008F31
			// (remove) Token: 0x060001F8 RID: 504 RVA: 0x0000AD3A File Offset: 0x00008F3A
			public event SkeletonMecanim.MecanimTranslator.OnClipAppliedDelegate OnClipApplied
			{
				add
				{
					this._OnClipApplied += value;
				}
				remove
				{
					this._OnClipApplied -= value;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000AD43 File Offset: 0x00008F43
			public Animator Animator
			{
				get
				{
					return this.animator;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060001FA RID: 506 RVA: 0x0000AD4B File Offset: 0x00008F4B
			public int MecanimLayerCount
			{
				get
				{
					if (!this.animator)
					{
						return 0;
					}
					return this.animator.layerCount;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060001FB RID: 507 RVA: 0x0000AD68 File Offset: 0x00008F68
			public string[] MecanimLayerNames
			{
				get
				{
					if (!this.animator)
					{
						return new string[0];
					}
					string[] layerNames = new string[this.animator.layerCount];
					for (int i = 0; i < this.animator.layerCount; i++)
					{
						layerNames[i] = this.animator.GetLayerName(i);
					}
					return layerNames;
				}
			}

			// Token: 0x060001FC RID: 508 RVA: 0x0000ADC0 File Offset: 0x00008FC0
			public void Initialize(Animator animator, SkeletonDataAsset skeletonDataAsset)
			{
				this.animator = animator;
				this.previousAnimations.Clear();
				this.animationTable.Clear();
				foreach (Animation a in skeletonDataAsset.GetSkeletonData(true).Animations)
				{
					this.animationTable.Add(a.Name.GetHashCode(), a);
				}
				this.clipNameHashCodeTable.Clear();
				this.ClearClipInfosForLayers();
			}

			// Token: 0x060001FD RID: 509 RVA: 0x0000AE58 File Offset: 0x00009058
			private bool ApplyAnimation(Skeleton skeleton, AnimatorClipInfo info, AnimatorStateInfo stateInfo, int layerIndex, float layerWeight, MixBlend layerBlendMode, bool useClipWeight1 = false)
			{
				float weight = info.weight * layerWeight;
				if (weight < 0.0001f)
				{
					return false;
				}
				Animation clip = this.GetAnimation(info.clip);
				if (clip == null)
				{
					return false;
				}
				float time = SkeletonMecanim.MecanimTranslator.AnimationTime(stateInfo.normalizedTime, info.clip.length, info.clip.isLooping, stateInfo.speed < 0f);
				weight = (useClipWeight1 ? layerWeight : weight);
				clip.Apply(skeleton, 0f, time, info.clip.isLooping, null, weight, layerBlendMode, MixDirection.In);
				if (this._OnClipApplied != null)
				{
					this.OnClipAppliedCallback(clip, stateInfo, layerIndex, time, info.clip.isLooping, weight);
				}
				return true;
			}

			// Token: 0x060001FE RID: 510 RVA: 0x0000AF0C File Offset: 0x0000910C
			private bool ApplyInterruptionAnimation(Skeleton skeleton, bool interpolateWeightTo1, AnimatorClipInfo info, AnimatorStateInfo stateInfo, int layerIndex, float layerWeight, MixBlend layerBlendMode, float interruptingClipTimeAddition, bool useClipWeight1 = false)
			{
				float weight = (interpolateWeightTo1 ? ((info.weight + 1f) * 0.5f) : info.weight) * layerWeight;
				if (weight < 0.0001f)
				{
					return false;
				}
				Animation clip = this.GetAnimation(info.clip);
				if (clip == null)
				{
					return false;
				}
				float time = SkeletonMecanim.MecanimTranslator.AnimationTime(stateInfo.normalizedTime + interruptingClipTimeAddition, info.clip.length, info.clip.isLooping, stateInfo.speed < 0f);
				weight = (useClipWeight1 ? layerWeight : weight);
				clip.Apply(skeleton, 0f, time, info.clip.isLooping, null, weight, layerBlendMode, MixDirection.In);
				if (this._OnClipApplied != null)
				{
					this.OnClipAppliedCallback(clip, stateInfo, layerIndex, time, info.clip.isLooping, weight);
				}
				return true;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x0000AFDC File Offset: 0x000091DC
			private void OnClipAppliedCallback(Animation clip, AnimatorStateInfo stateInfo, int layerIndex, float time, bool isLooping, float weight)
			{
				float speedFactor = stateInfo.speedMultiplier * stateInfo.speed;
				float lastTime = time - Time.deltaTime * speedFactor;
				float clipDuration = clip.Duration;
				if (isLooping && clipDuration != 0f)
				{
					time %= clipDuration;
					lastTime %= clipDuration;
				}
				this._OnClipApplied(clip, layerIndex, weight, time, lastTime, speedFactor < 0f);
			}

			// Token: 0x06000200 RID: 512 RVA: 0x0000B03C File Offset: 0x0000923C
			public void Apply(Skeleton skeleton)
			{
				if (this.layerMixModes.Length < this.animator.layerCount)
				{
					int num = this.layerMixModes.Length;
					Array.Resize<SkeletonMecanim.MecanimTranslator.MixMode>(ref this.layerMixModes, this.animator.layerCount);
					for (int layer = num; layer < this.animator.layerCount; layer++)
					{
						bool isAdditiveLayer = false;
						if (layer < this.layerBlendModes.Length)
						{
							isAdditiveLayer = this.layerBlendModes[layer] == MixBlend.Add;
						}
						this.layerMixModes[layer] = (isAdditiveLayer ? SkeletonMecanim.MecanimTranslator.MixMode.AlwaysMix : SkeletonMecanim.MecanimTranslator.MixMode.MixNext);
					}
				}
				this.InitClipInfosForLayers();
				int layer2 = 0;
				int i = this.animator.layerCount;
				while (layer2 < i)
				{
					this.GetStateUpdatesFromAnimator(layer2);
					layer2++;
				}
				if (this.autoReset)
				{
					List<Animation> previousAnimations = this.previousAnimations;
					int j = 0;
					int k = previousAnimations.Count;
					while (j < k)
					{
						previousAnimations[j].Apply(skeleton, 0f, 0f, false, null, 0f, MixBlend.Setup, MixDirection.Out);
						j++;
					}
					previousAnimations.Clear();
					int layer3 = 0;
					int l = this.animator.layerCount;
					while (layer3 < l)
					{
						float layerWeight = ((layer3 == 0) ? 1f : this.animator.GetLayerWeight(layer3));
						if (layerWeight > 0f)
						{
							bool hasNext = this.animator.GetNextAnimatorStateInfo(layer3).fullPathHash != 0;
							bool isInterruptionActive;
							int clipInfoCount;
							int nextClipInfoCount;
							int interruptingClipInfoCount;
							IList<AnimatorClipInfo> clipInfo;
							IList<AnimatorClipInfo> nextClipInfo;
							IList<AnimatorClipInfo> interruptingClipInfo;
							bool shallInterpolateWeightTo;
							this.GetAnimatorClipInfos(layer3, out isInterruptionActive, out clipInfoCount, out nextClipInfoCount, out interruptingClipInfoCount, out clipInfo, out nextClipInfo, out interruptingClipInfo, out shallInterpolateWeightTo);
							for (int c = 0; c < clipInfoCount; c++)
							{
								AnimatorClipInfo info = clipInfo[c];
								if (info.weight * layerWeight >= 0.0001f)
								{
									Animation clip = this.GetAnimation(info.clip);
									if (clip != null)
									{
										previousAnimations.Add(clip);
									}
								}
							}
							if (hasNext)
							{
								for (int c2 = 0; c2 < nextClipInfoCount; c2++)
								{
									AnimatorClipInfo info2 = nextClipInfo[c2];
									if (info2.weight * layerWeight >= 0.0001f)
									{
										Animation clip2 = this.GetAnimation(info2.clip);
										if (clip2 != null)
										{
											previousAnimations.Add(clip2);
										}
									}
								}
							}
							if (isInterruptionActive)
							{
								for (int c3 = 0; c3 < interruptingClipInfoCount; c3++)
								{
									AnimatorClipInfo info3 = interruptingClipInfo[c3];
									if ((shallInterpolateWeightTo ? ((info3.weight + 1f) * 0.5f) : info3.weight) * layerWeight >= 0.0001f)
									{
										Animation clip3 = this.GetAnimation(info3.clip);
										if (clip3 != null)
										{
											previousAnimations.Add(clip3);
										}
									}
								}
							}
						}
						layer3++;
					}
				}
				int layer4 = 0;
				int m = this.animator.layerCount;
				while (layer4 < m)
				{
					float layerWeight2 = ((layer4 == 0) ? 1f : this.animator.GetLayerWeight(layer4));
					bool isInterruptionActive2;
					AnimatorStateInfo stateInfo;
					AnimatorStateInfo nextStateInfo;
					AnimatorStateInfo interruptingStateInfo;
					float interruptingClipTimeAddition;
					this.GetAnimatorStateInfos(layer4, out isInterruptionActive2, out stateInfo, out nextStateInfo, out interruptingStateInfo, out interruptingClipTimeAddition);
					bool hasNext2 = nextStateInfo.fullPathHash != 0;
					int clipInfoCount2;
					int nextClipInfoCount2;
					int interruptingClipInfoCount2;
					IList<AnimatorClipInfo> clipInfo2;
					IList<AnimatorClipInfo> nextClipInfo2;
					IList<AnimatorClipInfo> interruptingClipInfo2;
					bool interpolateWeightTo;
					this.GetAnimatorClipInfos(layer4, out isInterruptionActive2, out clipInfoCount2, out nextClipInfoCount2, out interruptingClipInfoCount2, out clipInfo2, out nextClipInfo2, out interruptingClipInfo2, out interpolateWeightTo);
					MixBlend layerBlendMode = ((layer4 < this.layerBlendModes.Length) ? this.layerBlendModes[layer4] : MixBlend.Replace);
					SkeletonMecanim.MecanimTranslator.MixMode mode = this.GetMixMode(layer4, layerBlendMode);
					if (mode != SkeletonMecanim.MecanimTranslator.MixMode.AlwaysMix)
					{
						int c4 = 0;
						while (c4 < clipInfoCount2)
						{
							if (this.ApplyAnimation(skeleton, clipInfo2[c4], stateInfo, layer4, layerWeight2, layerBlendMode, true))
							{
								c4++;
								IL_03E1:
								while (c4 < clipInfoCount2)
								{
									this.ApplyAnimation(skeleton, clipInfo2[c4], stateInfo, layer4, layerWeight2, layerBlendMode, false);
									c4++;
								}
								c4 = 0;
								if (hasNext2)
								{
									if (mode == SkeletonMecanim.MecanimTranslator.MixMode.Hard)
									{
										while (c4 < nextClipInfoCount2)
										{
											if (this.ApplyAnimation(skeleton, nextClipInfo2[c4], nextStateInfo, layer4, layerWeight2, layerBlendMode, true))
											{
												c4++;
												break;
											}
											c4++;
										}
									}
									while (c4 < nextClipInfoCount2)
									{
										this.ApplyAnimation(skeleton, nextClipInfo2[c4], nextStateInfo, layer4, layerWeight2, layerBlendMode, false);
										c4++;
									}
								}
								c4 = 0;
								if (isInterruptionActive2)
								{
									if (mode == SkeletonMecanim.MecanimTranslator.MixMode.Hard)
									{
										while (c4 < interruptingClipInfoCount2)
										{
											if (this.ApplyInterruptionAnimation(skeleton, interpolateWeightTo, interruptingClipInfo2[c4], interruptingStateInfo, layer4, layerWeight2, layerBlendMode, interruptingClipTimeAddition, true))
											{
												c4++;
												break;
											}
											c4++;
										}
									}
									while (c4 < interruptingClipInfoCount2)
									{
										this.ApplyInterruptionAnimation(skeleton, interpolateWeightTo, interruptingClipInfo2[c4], interruptingStateInfo, layer4, layerWeight2, layerBlendMode, interruptingClipTimeAddition, false);
										c4++;
									}
									goto IL_04B9;
								}
								goto IL_04B9;
							}
							else
							{
								c4++;
							}
						}
						goto IL_03E1;
					}
					for (int c5 = 0; c5 < clipInfoCount2; c5++)
					{
						this.ApplyAnimation(skeleton, clipInfo2[c5], stateInfo, layer4, layerWeight2, layerBlendMode, false);
					}
					if (hasNext2)
					{
						for (int c6 = 0; c6 < nextClipInfoCount2; c6++)
						{
							this.ApplyAnimation(skeleton, nextClipInfo2[c6], nextStateInfo, layer4, layerWeight2, layerBlendMode, false);
						}
					}
					if (isInterruptionActive2)
					{
						for (int c7 = 0; c7 < interruptingClipInfoCount2; c7++)
						{
							this.ApplyInterruptionAnimation(skeleton, interpolateWeightTo, interruptingClipInfo2[c7], interruptingStateInfo, layer4, layerWeight2, layerBlendMode, interruptingClipTimeAddition, false);
						}
					}
					IL_04B9:
					layer4++;
				}
			}

			// Token: 0x06000201 RID: 513 RVA: 0x0000B514 File Offset: 0x00009714
			public KeyValuePair<Animation, float> GetActiveAnimationAndTime(int layer)
			{
				if (layer >= this.layerClipInfos.Length)
				{
					return new KeyValuePair<Animation, float>(null, 0f);
				}
				SkeletonMecanim.MecanimTranslator.ClipInfos layerInfos = this.layerClipInfos[layer];
				AnimationClip clip;
				AnimatorStateInfo stateInfo;
				if (layerInfos.isInterruptionActive && layerInfos.interruptingClipInfoCount > 0)
				{
					clip = layerInfos.interruptingClipInfos[0].clip;
					stateInfo = layerInfos.interruptingStateInfo;
				}
				else
				{
					clip = layerInfos.clipInfos[0].clip;
					stateInfo = layerInfos.stateInfo;
				}
				Animation animation = this.GetAnimation(clip);
				float time = SkeletonMecanim.MecanimTranslator.AnimationTime(stateInfo.normalizedTime, clip.length, clip.isLooping, stateInfo.speed < 0f);
				return new KeyValuePair<Animation, float>(animation, time);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x0000B5C4 File Offset: 0x000097C4
			private static float AnimationTime(float normalizedTime, float clipLength, bool loop, bool reversed)
			{
				float time = SkeletonMecanim.MecanimTranslator.ToSpineAnimationTime(normalizedTime, clipLength, loop, reversed);
				if (loop)
				{
					return time;
				}
				if (clipLength - time >= 0.033333335f)
				{
					return time;
				}
				return clipLength;
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000B5ED File Offset: 0x000097ED
			private static float ToSpineAnimationTime(float normalizedTime, float clipLength, bool loop, bool reversed)
			{
				if (reversed)
				{
					normalizedTime = 1f - normalizedTime;
				}
				if (normalizedTime < 0f)
				{
					normalizedTime = (loop ? (normalizedTime % 1f + 1f) : 0f);
				}
				return normalizedTime * clipLength;
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000B620 File Offset: 0x00009820
			private void InitClipInfosForLayers()
			{
				if (this.layerClipInfos.Length < this.animator.layerCount)
				{
					Array.Resize<SkeletonMecanim.MecanimTranslator.ClipInfos>(ref this.layerClipInfos, this.animator.layerCount);
					int layer = 0;
					int i = this.animator.layerCount;
					while (layer < i)
					{
						if (this.layerClipInfos[layer] == null)
						{
							this.layerClipInfos[layer] = new SkeletonMecanim.MecanimTranslator.ClipInfos();
						}
						layer++;
					}
				}
			}

			// Token: 0x06000205 RID: 517 RVA: 0x0000B688 File Offset: 0x00009888
			private void ClearClipInfosForLayers()
			{
				int layer = 0;
				int i = this.layerClipInfos.Length;
				while (layer < i)
				{
					if (this.layerClipInfos[layer] == null)
					{
						this.layerClipInfos[layer] = new SkeletonMecanim.MecanimTranslator.ClipInfos();
					}
					else
					{
						this.layerClipInfos[layer].isInterruptionActive = false;
						this.layerClipInfos[layer].isLastFrameOfInterruption = false;
						this.layerClipInfos[layer].clipInfos.Clear();
						this.layerClipInfos[layer].nextClipInfos.Clear();
						this.layerClipInfos[layer].interruptingClipInfos.Clear();
					}
					layer++;
				}
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000B718 File Offset: 0x00009918
			private SkeletonMecanim.MecanimTranslator.MixMode GetMixMode(int layer, MixBlend layerBlendMode)
			{
				if (this.useCustomMixMode)
				{
					SkeletonMecanim.MecanimTranslator.MixMode mode = this.layerMixModes[layer];
					if (layerBlendMode == MixBlend.Add && mode == SkeletonMecanim.MecanimTranslator.MixMode.MixNext)
					{
						mode = SkeletonMecanim.MecanimTranslator.MixMode.AlwaysMix;
						this.layerMixModes[layer] = mode;
					}
					return mode;
				}
				if (layerBlendMode != MixBlend.Add)
				{
					return SkeletonMecanim.MecanimTranslator.MixMode.MixNext;
				}
				return SkeletonMecanim.MecanimTranslator.MixMode.AlwaysMix;
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000B754 File Offset: 0x00009954
			private void GetStateUpdatesFromAnimator(int layer)
			{
				SkeletonMecanim.MecanimTranslator.ClipInfos layerInfos = this.layerClipInfos[layer];
				int clipInfoCount = this.animator.GetCurrentAnimatorClipInfoCount(layer);
				int nextClipInfoCount = this.animator.GetNextAnimatorClipInfoCount(layer);
				List<AnimatorClipInfo> clipInfos = layerInfos.clipInfos;
				List<AnimatorClipInfo> nextClipInfos = layerInfos.nextClipInfos;
				List<AnimatorClipInfo> interruptingClipInfos = layerInfos.interruptingClipInfos;
				layerInfos.isInterruptionActive = clipInfoCount == 0 && clipInfos.Count != 0 && nextClipInfoCount == 0 && nextClipInfos.Count != 0;
				if (layerInfos.isInterruptionActive)
				{
					AnimatorStateInfo interruptingStateInfo = this.animator.GetNextAnimatorStateInfo(layer);
					layerInfos.isLastFrameOfInterruption = interruptingStateInfo.fullPathHash == 0;
					if (!layerInfos.isLastFrameOfInterruption)
					{
						this.animator.GetNextAnimatorClipInfo(layer, interruptingClipInfos);
						layerInfos.interruptingClipInfoCount = interruptingClipInfos.Count;
						float oldTime = layerInfos.interruptingStateInfo.normalizedTime;
						float newTime = interruptingStateInfo.normalizedTime;
						layerInfos.interruptingClipTimeAddition = newTime - oldTime;
						layerInfos.interruptingStateInfo = interruptingStateInfo;
						return;
					}
				}
				else
				{
					layerInfos.clipInfoCount = clipInfoCount;
					layerInfos.nextClipInfoCount = nextClipInfoCount;
					layerInfos.interruptingClipInfoCount = 0;
					layerInfos.isLastFrameOfInterruption = false;
					if (clipInfos.Capacity < clipInfoCount)
					{
						clipInfos.Capacity = clipInfoCount;
					}
					if (nextClipInfos.Capacity < nextClipInfoCount)
					{
						nextClipInfos.Capacity = nextClipInfoCount;
					}
					this.animator.GetCurrentAnimatorClipInfo(layer, clipInfos);
					this.animator.GetNextAnimatorClipInfo(layer, nextClipInfos);
					layerInfos.stateInfo = this.animator.GetCurrentAnimatorStateInfo(layer);
					layerInfos.nextStateInfo = this.animator.GetNextAnimatorStateInfo(layer);
				}
			}

			// Token: 0x06000208 RID: 520 RVA: 0x0000B8B0 File Offset: 0x00009AB0
			private void GetAnimatorClipInfos(int layer, out bool isInterruptionActive, out int clipInfoCount, out int nextClipInfoCount, out int interruptingClipInfoCount, out IList<AnimatorClipInfo> clipInfo, out IList<AnimatorClipInfo> nextClipInfo, out IList<AnimatorClipInfo> interruptingClipInfo, out bool shallInterpolateWeightTo1)
			{
				SkeletonMecanim.MecanimTranslator.ClipInfos layerInfos = this.layerClipInfos[layer];
				isInterruptionActive = layerInfos.isInterruptionActive;
				clipInfoCount = layerInfos.clipInfoCount;
				nextClipInfoCount = layerInfos.nextClipInfoCount;
				interruptingClipInfoCount = layerInfos.interruptingClipInfoCount;
				clipInfo = layerInfos.clipInfos;
				nextClipInfo = layerInfos.nextClipInfos;
				interruptingClipInfo = (isInterruptionActive ? layerInfos.interruptingClipInfos : null);
				shallInterpolateWeightTo1 = layerInfos.isLastFrameOfInterruption;
			}

			// Token: 0x06000209 RID: 521 RVA: 0x0000B914 File Offset: 0x00009B14
			private void GetAnimatorStateInfos(int layer, out bool isInterruptionActive, out AnimatorStateInfo stateInfo, out AnimatorStateInfo nextStateInfo, out AnimatorStateInfo interruptingStateInfo, out float interruptingClipTimeAddition)
			{
				SkeletonMecanim.MecanimTranslator.ClipInfos layerInfos = this.layerClipInfos[layer];
				isInterruptionActive = layerInfos.isInterruptionActive;
				stateInfo = layerInfos.stateInfo;
				nextStateInfo = layerInfos.nextStateInfo;
				interruptingStateInfo = layerInfos.interruptingStateInfo;
				interruptingClipTimeAddition = (layerInfos.isLastFrameOfInterruption ? layerInfos.interruptingClipTimeAddition : 0f);
			}

			// Token: 0x0600020A RID: 522 RVA: 0x0000B970 File Offset: 0x00009B70
			private Animation GetAnimation(AnimationClip clip)
			{
				int clipNameHashCode;
				if (!this.clipNameHashCodeTable.TryGetValue(clip, out clipNameHashCode))
				{
					clipNameHashCode = clip.name.GetHashCode();
					this.clipNameHashCodeTable.Add(clip, clipNameHashCode);
				}
				Animation animation;
				this.animationTable.TryGetValue(clipNameHashCode, out animation);
				return animation;
			}

			// Token: 0x04000122 RID: 290
			private const float WeightEpsilon = 0.0001f;

			// Token: 0x04000123 RID: 291
			public bool autoReset = true;

			// Token: 0x04000124 RID: 292
			public bool useCustomMixMode = true;

			// Token: 0x04000125 RID: 293
			public SkeletonMecanim.MecanimTranslator.MixMode[] layerMixModes = new SkeletonMecanim.MecanimTranslator.MixMode[0];

			// Token: 0x04000126 RID: 294
			public MixBlend[] layerBlendModes = new MixBlend[0];

			// Token: 0x04000128 RID: 296
			private readonly Dictionary<int, Animation> animationTable = new Dictionary<int, Animation>(SkeletonMecanim.MecanimTranslator.IntEqualityComparer.Instance);

			// Token: 0x04000129 RID: 297
			private readonly Dictionary<AnimationClip, int> clipNameHashCodeTable = new Dictionary<AnimationClip, int>(SkeletonMecanim.MecanimTranslator.AnimationClipEqualityComparer.Instance);

			// Token: 0x0400012A RID: 298
			private readonly List<Animation> previousAnimations = new List<Animation>();

			// Token: 0x0400012B RID: 299
			protected SkeletonMecanim.MecanimTranslator.ClipInfos[] layerClipInfos = new SkeletonMecanim.MecanimTranslator.ClipInfos[0];

			// Token: 0x0400012C RID: 300
			private Animator animator;

			// Token: 0x02000034 RID: 52
			// (Invoke) Token: 0x0600020D RID: 525
			public delegate void OnClipAppliedDelegate(Animation clip, int layerIndex, float weight, float time, float lastTime, bool playsBackward);

			// Token: 0x02000035 RID: 53
			public enum MixMode
			{
				// Token: 0x0400012E RID: 302
				AlwaysMix,
				// Token: 0x0400012F RID: 303
				MixNext,
				// Token: 0x04000130 RID: 304
				Hard
			}

			// Token: 0x02000036 RID: 54
			protected class ClipInfos
			{
				// Token: 0x04000131 RID: 305
				public bool isInterruptionActive;

				// Token: 0x04000132 RID: 306
				public bool isLastFrameOfInterruption;

				// Token: 0x04000133 RID: 307
				public int clipInfoCount;

				// Token: 0x04000134 RID: 308
				public int nextClipInfoCount;

				// Token: 0x04000135 RID: 309
				public int interruptingClipInfoCount;

				// Token: 0x04000136 RID: 310
				public readonly List<AnimatorClipInfo> clipInfos = new List<AnimatorClipInfo>();

				// Token: 0x04000137 RID: 311
				public readonly List<AnimatorClipInfo> nextClipInfos = new List<AnimatorClipInfo>();

				// Token: 0x04000138 RID: 312
				public readonly List<AnimatorClipInfo> interruptingClipInfos = new List<AnimatorClipInfo>();

				// Token: 0x04000139 RID: 313
				public AnimatorStateInfo stateInfo;

				// Token: 0x0400013A RID: 314
				public AnimatorStateInfo nextStateInfo;

				// Token: 0x0400013B RID: 315
				public AnimatorStateInfo interruptingStateInfo;

				// Token: 0x0400013C RID: 316
				public float interruptingClipTimeAddition;
			}

			// Token: 0x02000037 RID: 55
			private class AnimationClipEqualityComparer : IEqualityComparer<AnimationClip>
			{
				// Token: 0x06000211 RID: 529 RVA: 0x0000BA51 File Offset: 0x00009C51
				public bool Equals(AnimationClip x, AnimationClip y)
				{
					return x.GetInstanceID() == y.GetInstanceID();
				}

				// Token: 0x06000212 RID: 530 RVA: 0x0000BA61 File Offset: 0x00009C61
				public int GetHashCode(AnimationClip o)
				{
					return o.GetInstanceID();
				}

				// Token: 0x0400013D RID: 317
				internal static readonly IEqualityComparer<AnimationClip> Instance = new SkeletonMecanim.MecanimTranslator.AnimationClipEqualityComparer();
			}

			// Token: 0x02000038 RID: 56
			private class IntEqualityComparer : IEqualityComparer<int>
			{
				// Token: 0x06000215 RID: 533 RVA: 0x0000BA75 File Offset: 0x00009C75
				public bool Equals(int x, int y)
				{
					return x == y;
				}

				// Token: 0x06000216 RID: 534 RVA: 0x0000BA7B File Offset: 0x00009C7B
				public int GetHashCode(int o)
				{
					return o;
				}

				// Token: 0x0400013E RID: 318
				internal static readonly IEqualityComparer<int> Instance = new SkeletonMecanim.MecanimTranslator.IntEqualityComparer();
			}
		}
	}
}
