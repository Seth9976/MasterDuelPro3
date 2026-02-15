using System;
using System.Collections.Generic;
using Spine.Unity.AnimationTools;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000027 RID: 39
	[DefaultExecutionOrder(1)]
	public abstract class SkeletonRootMotionBase : MonoBehaviour
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000EE RID: 238 RVA: 0x0000649C File Offset: 0x0000469C
		// (remove) Token: 0x060000EF RID: 239 RVA: 0x000064D4 File Offset: 0x000046D4
		public event SkeletonRootMotionBase.RootMotionDelegate ProcessRootMotionOverride;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000F0 RID: 240 RVA: 0x0000650C File Offset: 0x0000470C
		// (remove) Token: 0x060000F1 RID: 241 RVA: 0x00006544 File Offset: 0x00004744
		public event SkeletonRootMotionBase.RootMotionDelegate PhysicsUpdateRootMotionOverride;

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006579 File Offset: 0x00004779
		public Bone RootMotionBone
		{
			get
			{
				return this.rootMotionBone;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006581 File Offset: 0x00004781
		public bool UsesRigidbody
		{
			get
			{
				return this.rigidBody != null || this.rigidBody2D != null;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000659F File Offset: 0x0000479F
		public Vector2 PreviousRigidbodyRootMotion2D
		{
			get
			{
				return new Vector2(this.previousRigidbodyRootMotion.x, this.previousRigidbodyRootMotion.y);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000065BC File Offset: 0x000047BC
		public Vector3 PreviousRigidbodyRootMotion3D
		{
			get
			{
				return this.previousRigidbodyRootMotion;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000065C4 File Offset: 0x000047C4
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000065CC File Offset: 0x000047CC
		public Vector2 AdditionalRigidbody2DMovement
		{
			get
			{
				return this.additionalRigidbody2DMovement;
			}
			set
			{
				this.additionalRigidbody2DMovement = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000065D8 File Offset: 0x000047D8
		protected bool SkeletonAnimationUsesFixedUpdate
		{
			get
			{
				ISkeletonAnimation skeletonAnimation = this.skeletonComponent as ISkeletonAnimation;
				return skeletonAnimation != null && skeletonAnimation.UpdateTiming == UpdateTiming.InFixedUpdate;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000065FF File Offset: 0x000047FF
		protected virtual void Reset()
		{
			this.FindRigidbodyComponent();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006607 File Offset: 0x00004807
		protected virtual void Start()
		{
			this.Initialize();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006607 File Offset: 0x00004807
		protected void InitializeOnRebuild(ISkeletonAnimation animatedSkeletonComponent)
		{
			this.Initialize();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006610 File Offset: 0x00004810
		public virtual void Initialize()
		{
			this.skeletonComponent = base.GetComponent<ISkeletonComponent>();
			this.GatherTopLevelBones();
			this.SetRootMotionBone(this.rootMotionBoneName);
			if (this.rootMotionBone != null)
			{
				this.initialOffset = new Vector2(this.rootMotionBone.X, this.rootMotionBone.Y);
				this.initialOffsetRotation = this.rootMotionBone.Rotation;
			}
			ISkeletonAnimation skeletonAnimation = this.skeletonComponent as ISkeletonAnimation;
			if (skeletonAnimation != null)
			{
				skeletonAnimation.UpdateLocal -= this.HandleUpdateLocal;
				skeletonAnimation.UpdateLocal += this.HandleUpdateLocal;
				skeletonAnimation.OnAnimationRebuild -= this.InitializeOnRebuild;
				skeletonAnimation.OnAnimationRebuild += this.InitializeOnRebuild;
				SkeletonUtility skeletonUtility = base.GetComponent<SkeletonUtility>();
				if (skeletonUtility != null)
				{
					skeletonUtility.ResubscribeEvents();
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000066E2 File Offset: 0x000048E2
		protected virtual void FixedUpdate()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (!this.SkeletonAnimationUsesFixedUpdate)
			{
				this.PhysicsUpdate(false);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000066FC File Offset: 0x000048FC
		protected virtual void PhysicsUpdate(bool skeletonAnimationUsesFixedUpdate)
		{
			Vector2 callbackDisplacement = this.tempSkeletonDisplacement;
			float callbackRotation = this.tempSkeletonRotation;
			if (this.PhysicsUpdateRootMotionOverride == null || !this.disableOnOverride)
			{
				if (this.rigidBody2D != null)
				{
					Vector2 gravityAndVelocityMovement = Vector2.zero;
					if (this.applyRigidbody2DGravity)
					{
						float deltaTime = Time.fixedDeltaTime;
						float deltaTimeSquared = deltaTime * deltaTime;
						this.rigidBody2D.linearVelocity += this.rigidBody2D.gravityScale * Physics2D.gravity * deltaTime;
						gravityAndVelocityMovement = 0.5f * this.rigidBody2D.gravityScale * Physics2D.gravity * deltaTimeSquared + this.rigidBody2D.linearVelocity * deltaTime;
					}
					Vector2 rigidbodyDisplacement2D = new Vector2(this.rigidbodyDisplacement.x, this.rigidbodyDisplacement.y);
					this.rigidBody2D.MovePosition(gravityAndVelocityMovement + new Vector2(this.rigidBody2D.position.x, this.rigidBody2D.position.y) + rigidbodyDisplacement2D + this.additionalRigidbody2DMovement);
					this.rigidBody2D.MoveRotation(this.rigidbody2DRotation + this.rigidBody2D.rotation);
				}
				else if (this.rigidBody != null)
				{
					this.rigidBody.MovePosition(this.rigidBody.position + new Vector3(this.rigidbodyDisplacement.x, this.rigidbodyDisplacement.y, this.rigidbodyDisplacement.z));
					this.rigidBody.MoveRotation(this.rigidBody.rotation * this.rigidbodyLocalRotation);
				}
			}
			this.previousRigidbodyRootMotion = this.rigidbodyDisplacement;
			if (this.accumulatedUntilFixedUpdate)
			{
				Vector2 parentBoneScale;
				this.GetScaleAffectingRootMotion(out parentBoneScale);
				this.ClearEffectiveBoneOffsets(parentBoneScale);
				this.skeletonComponent.Skeleton.UpdateWorldTransform(Skeleton.Physics.Pose);
			}
			this.ClearRigidbodyTempMovement();
			if (this.PhysicsUpdateRootMotionOverride != null)
			{
				this.PhysicsUpdateRootMotionOverride(this, callbackDisplacement, callbackRotation);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000690E File Offset: 0x00004B0E
		protected virtual void OnDisable()
		{
			this.ClearRigidbodyTempMovement();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006918 File Offset: 0x00004B18
		protected void FindRigidbodyComponent()
		{
			this.rigidBody2D = base.GetComponent<Rigidbody2D>();
			if (!this.rigidBody2D)
			{
				this.rigidBody = base.GetComponent<Rigidbody>();
			}
			if (!this.rigidBody2D && !this.rigidBody)
			{
				this.rigidBody2D = base.GetComponentInParent<Rigidbody2D>();
				if (!this.rigidBody2D)
				{
					this.rigidBody = base.GetComponentInParent<Rigidbody>();
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006989 File Offset: 0x00004B89
		protected virtual float AdditionalScale
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000102 RID: 258
		protected abstract Vector2 CalculateAnimationsMovementDelta();

		// Token: 0x06000103 RID: 259 RVA: 0x00006990 File Offset: 0x00004B90
		protected virtual float CalculateAnimationsRotationDelta()
		{
			return 0f;
		}

		// Token: 0x06000104 RID: 260
		public abstract Vector2 GetRemainingRootMotion(int trackIndex = 0);

		// Token: 0x06000105 RID: 261
		public abstract SkeletonRootMotionBase.RootMotionInfo GetRootMotionInfo(int trackIndex = 0);

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006997 File Offset: 0x00004B97
		public ISkeletonComponent TargetSkeletonComponent
		{
			get
			{
				if (this.skeletonComponent == null)
				{
					this.skeletonComponent = base.GetComponent<ISkeletonComponent>();
				}
				return this.skeletonComponent;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000069B3 File Offset: 0x00004BB3
		public ISkeletonAnimation TargetSkeletonAnimationComponent
		{
			get
			{
				return this.TargetSkeletonComponent as ISkeletonAnimation;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000069C0 File Offset: 0x00004BC0
		public void SetRootMotionBone(string name)
		{
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			Bone bone = skeleton.FindBone(name);
			if (bone != null)
			{
				this.rootMotionBoneIndex = bone.Data.Index;
				this.rootMotionBone = bone;
				this.FindTransformConstraintsAffectingBone();
				return;
			}
			Debug.Log("Bone named \"" + name + "\" could not be found. Set 'skeletonRootMotion.rootMotionBoneName' before calling 'skeletonAnimation.Initialize(true)'.");
			this.rootMotionBoneIndex = 0;
			this.rootMotionBone = skeleton.RootBone;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006A2C File Offset: 0x00004C2C
		public void AdjustRootMotionToDistance(Vector2 distanceToTarget, int trackIndex = 0, bool adjustX = true, bool adjustY = true, float minX = 0f, float maxX = 3.4028235E+38f, float minY = 0f, float maxY = 3.4028235E+38f, bool allowXTranslation = false, bool allowYTranslation = false)
		{
			Vector2 distanceToTargetSkeletonSpace = base.transform.InverseTransformVector(distanceToTarget);
			Vector2 scaleAffectingRootMotion = this.GetScaleAffectingRootMotion();
			if (this.UsesRigidbody)
			{
				distanceToTargetSkeletonSpace -= this.tempSkeletonDisplacement;
			}
			Vector2 remainingRootMotionSkeletonSpace = this.GetRemainingRootMotion(trackIndex);
			remainingRootMotionSkeletonSpace.Scale(scaleAffectingRootMotion);
			if (remainingRootMotionSkeletonSpace.x == 0f)
			{
				remainingRootMotionSkeletonSpace.x = 0.0001f;
			}
			if (remainingRootMotionSkeletonSpace.y == 0f)
			{
				remainingRootMotionSkeletonSpace.y = 0.0001f;
			}
			if (adjustX)
			{
				this.rootMotionScaleX = Math.Min(maxX, Math.Max(minX, distanceToTargetSkeletonSpace.x / remainingRootMotionSkeletonSpace.x));
			}
			if (adjustY)
			{
				this.rootMotionScaleY = Math.Min(maxY, Math.Max(minY, distanceToTargetSkeletonSpace.y / remainingRootMotionSkeletonSpace.y));
			}
			if (allowXTranslation)
			{
				this.rootMotionTranslateXPerY = (distanceToTargetSkeletonSpace.x - remainingRootMotionSkeletonSpace.x * this.rootMotionScaleX) / remainingRootMotionSkeletonSpace.y;
			}
			if (allowYTranslation)
			{
				this.rootMotionTranslateYPerX = (distanceToTargetSkeletonSpace.y - remainingRootMotionSkeletonSpace.y * this.rootMotionScaleY) / remainingRootMotionSkeletonSpace.x;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006B41 File Offset: 0x00004D41
		public Vector2 GetAnimationRootMotion(Animation animation)
		{
			return this.GetAnimationRootMotion(0f, animation.Duration, animation);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006B58 File Offset: 0x00004D58
		public Vector2 GetAnimationRootMotion(float startTime, float endTime, Animation animation)
		{
			if (startTime == endTime)
			{
				return Vector2.zero;
			}
			TranslateTimeline translateTimeline = animation.FindTranslateTimelineForBone(this.rootMotionBoneIndex);
			TranslateXTimeline xTimeline = animation.FindTimelineForBone(this.rootMotionBoneIndex);
			TranslateYTimeline yTimeline = animation.FindTimelineForBone(this.rootMotionBoneIndex);
			Vector2 endPos = Vector2.zero;
			Vector2 startPos = Vector2.zero;
			if (translateTimeline != null)
			{
				endPos = translateTimeline.Evaluate(endTime, null);
				startPos = translateTimeline.Evaluate(startTime, null);
			}
			else if (xTimeline != null || yTimeline != null)
			{
				endPos = TimelineExtensions.Evaluate(xTimeline, yTimeline, endTime, null);
				startPos = TimelineExtensions.Evaluate(xTimeline, yTimeline, startTime, null);
			}
			TransformConstraint[] transformConstraintsItems = this.skeletonComponent.Skeleton.TransformConstraints.Items;
			foreach (int constraintIndex in this.transformConstraintIndices)
			{
				TransformConstraint constraint = transformConstraintsItems[constraintIndex];
				this.ApplyConstraintToPos(animation, constraint, constraintIndex, endTime, false, ref endPos);
				this.ApplyConstraintToPos(animation, constraint, constraintIndex, startTime, true, ref startPos);
			}
			Vector2 currentDelta = endPos - startPos;
			if (startTime > endTime)
			{
				Vector2 loopPos = Vector2.zero;
				Vector2 zeroPos = Vector2.zero;
				if (translateTimeline != null)
				{
					loopPos = translateTimeline.Evaluate(animation.Duration, null);
					zeroPos = translateTimeline.Evaluate(0f, null);
				}
				else if (xTimeline != null || yTimeline != null)
				{
					loopPos = TimelineExtensions.Evaluate(xTimeline, yTimeline, animation.Duration, null);
					zeroPos = TimelineExtensions.Evaluate(xTimeline, yTimeline, 0f, null);
				}
				foreach (int constraintIndex2 in this.transformConstraintIndices)
				{
					TransformConstraint constraint2 = transformConstraintsItems[constraintIndex2];
					this.ApplyConstraintToPos(animation, constraint2, constraintIndex2, animation.Duration, false, ref loopPos);
					this.ApplyConstraintToPos(animation, constraint2, constraintIndex2, 0f, false, ref zeroPos);
				}
				currentDelta += loopPos - zeroPos;
			}
			this.UpdateLastConstraintPos(transformConstraintsItems);
			return currentDelta;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006D40 File Offset: 0x00004F40
		public float GetAnimationRootMotionRotation(Animation animation)
		{
			return this.GetAnimationRootMotionRotation(0f, animation.Duration, animation);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006D54 File Offset: 0x00004F54
		public float GetAnimationRootMotionRotation(float startTime, float endTime, Animation animation)
		{
			if (startTime == endTime)
			{
				return 0f;
			}
			RotateTimeline rotateTimeline = animation.FindTimelineForBone(this.rootMotionBoneIndex);
			float endRotation = 0f;
			float startRotation = 0f;
			if (rotateTimeline != null)
			{
				endRotation = rotateTimeline.Evaluate(endTime, null);
				startRotation = rotateTimeline.Evaluate(startTime, null);
			}
			TransformConstraint[] transformConstraintsItems = this.skeletonComponent.Skeleton.TransformConstraints.Items;
			foreach (int constraintIndex in this.transformConstraintIndices)
			{
				TransformConstraint constraint = transformConstraintsItems[constraintIndex];
				this.ApplyConstraintToRotation(animation, constraint, constraintIndex, endTime, false, ref endRotation);
				this.ApplyConstraintToRotation(animation, constraint, constraintIndex, startTime, true, ref startRotation);
			}
			float currentDelta = endRotation - startRotation;
			if (startTime > endTime)
			{
				float loopRotation = 0f;
				float zeroPos = 0f;
				if (rotateTimeline != null)
				{
					loopRotation = rotateTimeline.Evaluate(animation.Duration, null);
					zeroPos = rotateTimeline.Evaluate(0f, null);
				}
				foreach (int constraintIndex2 in this.transformConstraintIndices)
				{
					TransformConstraint constraint2 = transformConstraintsItems[constraintIndex2];
					this.ApplyConstraintToRotation(animation, constraint2, constraintIndex2, animation.Duration, false, ref loopRotation);
					this.ApplyConstraintToRotation(animation, constraint2, constraintIndex2, 0f, false, ref zeroPos);
				}
				currentDelta += loopRotation - zeroPos;
			}
			this.UpdateLastConstraintRotation(transformConstraintsItems);
			return currentDelta;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006ECC File Offset: 0x000050CC
		private void ApplyConstraintToPos(Animation animation, TransformConstraint constraint, int constraintIndex, float time, bool useLastConstraintPos, ref Vector2 pos)
		{
			TransformConstraintTimeline timeline = animation.FindTransformConstraintTimeline(constraintIndex);
			if (timeline == null)
			{
				return;
			}
			Vector2 mixXY = timeline.EvaluateTranslateXYMix(time);
			Vector2 invMixXY = timeline.EvaluateTranslateXYMix(time);
			Vector2 constraintPos;
			if (useLastConstraintPos)
			{
				constraintPos = this.transformConstraintLastPos[this.GetConstraintLastPosIndex(constraintIndex)];
			}
			else
			{
				Bone targetBone = constraint.Target;
				constraintPos = new Vector2(targetBone.X, targetBone.Y);
			}
			pos = new Vector2(pos.x * invMixXY.x + constraintPos.x * mixXY.x, pos.y * invMixXY.y + constraintPos.y * mixXY.y);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006F74 File Offset: 0x00005174
		private void ApplyConstraintToRotation(Animation animation, TransformConstraint constraint, int constraintIndex, float time, bool useLastConstraintRotation, ref float rotation)
		{
			TransformConstraintTimeline timeline = animation.FindTransformConstraintTimeline(constraintIndex);
			if (timeline == null)
			{
				return;
			}
			float mixRotate = timeline.EvaluateRotateMix(time);
			float invMixRotate = timeline.EvaluateRotateMix(time);
			float constraintRotation;
			if (useLastConstraintRotation)
			{
				constraintRotation = this.transformConstraintLastRotation[this.GetConstraintLastPosIndex(constraintIndex)];
			}
			else
			{
				constraintRotation = constraint.Target.Rotation;
			}
			rotation = rotation * invMixRotate + constraintRotation * mixRotate;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006FD0 File Offset: 0x000051D0
		private void UpdateLastConstraintPos(TransformConstraint[] transformConstraintsItems)
		{
			foreach (int constraintIndex in this.transformConstraintIndices)
			{
				Bone targetBone = transformConstraintsItems[constraintIndex].Target;
				this.transformConstraintLastPos[this.GetConstraintLastPosIndex(constraintIndex)] = new Vector2(targetBone.X, targetBone.Y);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007048 File Offset: 0x00005248
		private void UpdateLastConstraintRotation(TransformConstraint[] transformConstraintsItems)
		{
			foreach (int constraintIndex in this.transformConstraintIndices)
			{
				Bone targetBone = transformConstraintsItems[constraintIndex].Target;
				this.transformConstraintLastRotation[this.GetConstraintLastPosIndex(constraintIndex)] = targetBone.Rotation;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000070B8 File Offset: 0x000052B8
		public SkeletonRootMotionBase.RootMotionInfo GetAnimationRootMotionInfo(Animation animation, float currentTime)
		{
			SkeletonRootMotionBase.RootMotionInfo rootMotion = default(SkeletonRootMotionBase.RootMotionInfo);
			float duration = animation.Duration;
			float mid = duration * 0.5f;
			rootMotion.timeIsPastMid = currentTime > mid;
			TranslateTimeline timeline = animation.FindTranslateTimelineForBone(this.rootMotionBoneIndex);
			if (timeline != null)
			{
				rootMotion.start = timeline.Evaluate(0f, null);
				rootMotion.current = timeline.Evaluate(currentTime, null);
				rootMotion.mid = timeline.Evaluate(mid, null);
				rootMotion.end = timeline.Evaluate(duration, null);
				return rootMotion;
			}
			TranslateXTimeline xTimeline = animation.FindTimelineForBone(this.rootMotionBoneIndex);
			TranslateYTimeline yTimeline = animation.FindTimelineForBone(this.rootMotionBoneIndex);
			if (xTimeline != null || yTimeline != null)
			{
				rootMotion.start = TimelineExtensions.Evaluate(xTimeline, yTimeline, 0f, null);
				rootMotion.current = TimelineExtensions.Evaluate(xTimeline, yTimeline, currentTime, null);
				rootMotion.mid = TimelineExtensions.Evaluate(xTimeline, yTimeline, mid, null);
				rootMotion.end = TimelineExtensions.Evaluate(xTimeline, yTimeline, duration, null);
				return rootMotion;
			}
			return rootMotion;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000071AC File Offset: 0x000053AC
		private int GetConstraintLastPosIndex(int constraintIndex)
		{
			ExposedList<TransformConstraint> transformConstraints = this.skeletonComponent.Skeleton.TransformConstraints;
			return this.transformConstraintIndices.FindIndex((int addedIndex) => addedIndex == constraintIndex);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000071F0 File Offset: 0x000053F0
		private void FindTransformConstraintsAffectingBone()
		{
			ExposedList<TransformConstraint> transformConstraints = this.skeletonComponent.Skeleton.TransformConstraints;
			TransformConstraint[] constraintsItems = transformConstraints.Items;
			int i = 0;
			int j = transformConstraints.Count;
			while (i < j)
			{
				TransformConstraint constraint = constraintsItems[i];
				if (constraint.Bones.Contains(this.rootMotionBone))
				{
					this.transformConstraintIndices.Add(i);
					Bone targetBone = constraint.Target;
					Vector2 constraintPos = new Vector2(targetBone.X, targetBone.Y);
					this.transformConstraintLastPos.Add(constraintPos);
					this.transformConstraintLastRotation.Add(targetBone.Rotation);
				}
				i++;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007288 File Offset: 0x00005488
		private Vector2 GetTimelineMovementDelta(float startTime, float endTime, TranslateXTimeline xTimeline, TranslateYTimeline yTimeline, Animation animation)
		{
			Vector2 currentDelta;
			if (startTime > endTime)
			{
				currentDelta = TimelineExtensions.Evaluate(xTimeline, yTimeline, animation.Duration, null) - TimelineExtensions.Evaluate(xTimeline, yTimeline, startTime, null) + (TimelineExtensions.Evaluate(xTimeline, yTimeline, endTime, null) - TimelineExtensions.Evaluate(xTimeline, yTimeline, 0f, null));
			}
			else if (startTime != endTime)
			{
				currentDelta = TimelineExtensions.Evaluate(xTimeline, yTimeline, endTime, null) - TimelineExtensions.Evaluate(xTimeline, yTimeline, startTime, null);
			}
			else
			{
				currentDelta = Vector2.zero;
			}
			return currentDelta;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007304 File Offset: 0x00005504
		private void GatherTopLevelBones()
		{
			this.topLevelBones.Clear();
			foreach (Bone bone in this.skeletonComponent.Skeleton.Bones)
			{
				if (bone.Parent == null)
				{
					this.topLevelBones.Add(bone);
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000737C File Offset: 0x0000557C
		private void HandleUpdateLocal(ISkeletonAnimation animatedSkeletonComponent)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			Vector2 boneLocalDelta = this.CalculateAnimationsMovementDelta();
			Vector2 parentBoneScale;
			Vector2 totalScale;
			Vector2 skeletonTranslationDelta = this.GetSkeletonSpaceMovementDelta(boneLocalDelta, out parentBoneScale, out totalScale);
			float skeletonRotationDelta = 0f;
			if (this.transformRotation)
			{
				float boneLocalDeltaRotation = this.CalculateAnimationsRotationDelta();
				boneLocalDeltaRotation *= this.rootMotionScaleRotation;
				skeletonRotationDelta = this.GetSkeletonSpaceRotationDelta(boneLocalDeltaRotation, totalScale);
			}
			bool usesFixedUpdate = this.SkeletonAnimationUsesFixedUpdate;
			this.ApplyRootMotion(skeletonTranslationDelta, skeletonRotationDelta, parentBoneScale, usesFixedUpdate);
			if (usesFixedUpdate)
			{
				this.PhysicsUpdate(usesFixedUpdate);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000073F4 File Offset: 0x000055F4
		private void ApplyRootMotion(Vector2 skeletonTranslationDelta, float skeletonRotationDelta, Vector2 parentBoneScale, bool skeletonAnimationUsesFixedUpdate)
		{
			bool usesRigidbody = this.UsesRigidbody;
			bool applyToTransform = !usesRigidbody && (this.ProcessRootMotionOverride == null || !this.disableOnOverride);
			this.accumulatedUntilFixedUpdate = !applyToTransform && !skeletonAnimationUsesFixedUpdate;
			if (this.ProcessRootMotionOverride != null)
			{
				this.ProcessRootMotionOverride(this, skeletonTranslationDelta, skeletonRotationDelta);
			}
			if (usesRigidbody)
			{
				this.rigidbodyDisplacement += base.transform.TransformVector(skeletonTranslationDelta);
				if (skeletonRotationDelta != 0f)
				{
					if (this.rigidBody != null)
					{
						Quaternion addedWorldRotation = Quaternion.Euler(0f, 0f, skeletonRotationDelta);
						this.rigidbodyLocalRotation *= addedWorldRotation;
					}
					else if (this.rigidBody2D != null)
					{
						Vector3 lossyScale = base.transform.lossyScale;
						float rotationSign = (float)((lossyScale.x * lossyScale.y > 0f) ? 1 : (-1));
						this.rigidbody2DRotation += rotationSign * skeletonRotationDelta;
					}
				}
			}
			else if (applyToTransform)
			{
				base.transform.position += base.transform.TransformVector(skeletonTranslationDelta);
				if (skeletonRotationDelta != 0f)
				{
					Vector3 lossyScale2 = base.transform.lossyScale;
					float rotationSign2 = (float)((lossyScale2.x * lossyScale2.y > 0f) ? 1 : (-1));
					base.transform.Rotate(0f, 0f, rotationSign2 * skeletonRotationDelta);
				}
			}
			this.tempSkeletonDisplacement += skeletonTranslationDelta;
			this.tempSkeletonRotation += skeletonRotationDelta;
			if (this.accumulatedUntilFixedUpdate)
			{
				this.SetEffectiveBoneOffsetsTo(this.tempSkeletonDisplacement, this.tempSkeletonRotation, parentBoneScale);
				return;
			}
			this.ClearEffectiveBoneOffsets(parentBoneScale);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000075B4 File Offset: 0x000057B4
		private void ApplyTransformConstraints()
		{
			this.rootMotionBone.AX = this.rootMotionBone.X;
			this.rootMotionBone.AY = this.rootMotionBone.Y;
			this.rootMotionBone.AppliedRotation = this.rootMotionBone.Rotation;
			TransformConstraint[] transformConstraintsItems = this.skeletonComponent.Skeleton.TransformConstraints.Items;
			foreach (int constraintIndex in this.transformConstraintIndices)
			{
				transformConstraintsItems[constraintIndex].Update(Skeleton.Physics.None);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007664 File Offset: 0x00005864
		private Vector2 GetScaleAffectingRootMotion()
		{
			Vector2 parentBoneScale;
			return this.GetScaleAffectingRootMotion(out parentBoneScale);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000767C File Offset: 0x0000587C
		private Vector2 GetScaleAffectingRootMotion(out Vector2 parentBoneScale)
		{
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			Vector2 totalScale = Vector2.one;
			totalScale.x *= skeleton.ScaleX;
			totalScale.y *= skeleton.ScaleY;
			parentBoneScale = Vector2.one;
			Bone scaleBone = this.rootMotionBone;
			while ((scaleBone = scaleBone.Parent) != null)
			{
				parentBoneScale.x *= scaleBone.AScaleX;
				parentBoneScale.y *= scaleBone.AScaleY;
			}
			totalScale = Vector2.Scale(totalScale, parentBoneScale);
			totalScale *= this.AdditionalScale;
			return totalScale;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007718 File Offset: 0x00005918
		private Vector2 GetSkeletonSpaceMovementDelta(Vector2 boneLocalDelta, out Vector2 parentBoneScale, out Vector2 totalScale)
		{
			Vector2 skeletonDelta = boneLocalDelta;
			totalScale = this.GetScaleAffectingRootMotion(out parentBoneScale);
			skeletonDelta.Scale(totalScale);
			Vector2 rootMotionTranslation = new Vector2(this.rootMotionTranslateXPerY * skeletonDelta.y, this.rootMotionTranslateYPerX * skeletonDelta.x);
			skeletonDelta.x *= this.rootMotionScaleX;
			skeletonDelta.y *= this.rootMotionScaleY;
			skeletonDelta.x += rootMotionTranslation.x;
			skeletonDelta.y += rootMotionTranslation.y;
			if (!this.transformPositionX)
			{
				skeletonDelta.x = 0f;
			}
			if (!this.transformPositionY)
			{
				skeletonDelta.y = 0f;
			}
			return skeletonDelta;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000077CF File Offset: 0x000059CF
		private float GetSkeletonSpaceRotationDelta(float boneLocalDelta, Vector2 totalScaleAffectingRootMotion)
		{
			return (float)((totalScaleAffectingRootMotion.x * totalScaleAffectingRootMotion.y > 0f) ? 1 : (-1)) * boneLocalDelta;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000077EC File Offset: 0x000059EC
		private void SetEffectiveBoneOffsetsTo(Vector2 displacementSkeletonSpace, float rotationSkeletonSpace, Vector2 parentBoneScale)
		{
			this.ApplyTransformConstraints();
			Skeleton skeleton = this.skeletonComponent.Skeleton;
			foreach (Bone topLevelBone in this.topLevelBones)
			{
				if (topLevelBone == this.rootMotionBone)
				{
					if (this.transformPositionX)
					{
						topLevelBone.X = displacementSkeletonSpace.x / skeleton.ScaleX;
					}
					if (this.transformPositionY)
					{
						topLevelBone.Y = displacementSkeletonSpace.y / skeleton.ScaleY;
					}
					if (this.transformRotation)
					{
						float rotationSign = (float)((skeleton.ScaleX * skeleton.ScaleY > 0f) ? 1 : (-1));
						topLevelBone.Rotation = rotationSign * rotationSkeletonSpace;
					}
				}
				else
				{
					bool useAppliedTransform = this.transformConstraintIndices.Count > 0;
					float rootMotionBoneX = (useAppliedTransform ? this.rootMotionBone.AX : this.rootMotionBone.X);
					float rootMotionBoneY = (useAppliedTransform ? this.rootMotionBone.AY : this.rootMotionBone.Y);
					float offsetX = (this.initialOffset.x - rootMotionBoneX) * parentBoneScale.x;
					float offsetY = (this.initialOffset.y - rootMotionBoneY) * parentBoneScale.y;
					if (this.transformPositionX)
					{
						topLevelBone.X = displacementSkeletonSpace.x / skeleton.ScaleX + offsetX;
					}
					if (this.transformPositionY)
					{
						topLevelBone.Y = displacementSkeletonSpace.y / skeleton.ScaleY + offsetY;
					}
					if (this.transformRotation)
					{
						float rootMotionBoneRotation = (useAppliedTransform ? this.rootMotionBone.AppliedRotation : this.rootMotionBone.Rotation);
						float parentBoneRotationSign = (float)((parentBoneScale.x * parentBoneScale.y > 0f) ? 1 : (-1));
						float offsetRotation = (this.initialOffsetRotation - rootMotionBoneRotation) * parentBoneRotationSign;
						float skeletonRotationSign = (float)((skeleton.ScaleX * skeleton.ScaleY > 0f) ? 1 : (-1));
						topLevelBone.Rotation = rotationSkeletonSpace * skeletonRotationSign + offsetRotation;
					}
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000079FC File Offset: 0x00005BFC
		private void ClearEffectiveBoneOffsets(Vector2 parentBoneScale)
		{
			this.SetEffectiveBoneOffsetsTo(Vector2.zero, 0f, parentBoneScale);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007A0F File Offset: 0x00005C0F
		private void ClearRigidbodyTempMovement()
		{
			this.rigidbodyDisplacement = Vector2.zero;
			this.tempSkeletonDisplacement = Vector2.zero;
			this.rigidbodyLocalRotation = Quaternion.identity;
			this.rigidbody2DRotation = 0f;
			this.tempSkeletonRotation = 0f;
		}

		// Token: 0x040000A1 RID: 161
		[SpineBone("", "", true, false)]
		public string rootMotionBoneName = "root";

		// Token: 0x040000A2 RID: 162
		public bool transformPositionX = true;

		// Token: 0x040000A3 RID: 163
		public bool transformPositionY = true;

		// Token: 0x040000A4 RID: 164
		public bool transformRotation;

		// Token: 0x040000A5 RID: 165
		public float rootMotionScaleX = 1f;

		// Token: 0x040000A6 RID: 166
		public float rootMotionScaleY = 1f;

		// Token: 0x040000A7 RID: 167
		public float rootMotionScaleRotation = 1f;

		// Token: 0x040000A8 RID: 168
		public float rootMotionTranslateXPerY;

		// Token: 0x040000A9 RID: 169
		public float rootMotionTranslateYPerX;

		// Token: 0x040000AA RID: 170
		[Header("Optional")]
		public Rigidbody2D rigidBody2D;

		// Token: 0x040000AB RID: 171
		public bool applyRigidbody2DGravity;

		// Token: 0x040000AC RID: 172
		public Rigidbody rigidBody;

		// Token: 0x040000AF RID: 175
		public bool disableOnOverride = true;

		// Token: 0x040000B0 RID: 176
		protected ISkeletonComponent skeletonComponent;

		// Token: 0x040000B1 RID: 177
		protected Bone rootMotionBone;

		// Token: 0x040000B2 RID: 178
		protected int rootMotionBoneIndex;

		// Token: 0x040000B3 RID: 179
		protected List<int> transformConstraintIndices = new List<int>();

		// Token: 0x040000B4 RID: 180
		protected List<Vector2> transformConstraintLastPos = new List<Vector2>();

		// Token: 0x040000B5 RID: 181
		protected List<float> transformConstraintLastRotation = new List<float>();

		// Token: 0x040000B6 RID: 182
		protected List<Bone> topLevelBones = new List<Bone>();

		// Token: 0x040000B7 RID: 183
		protected Vector2 initialOffset = Vector2.zero;

		// Token: 0x040000B8 RID: 184
		protected bool accumulatedUntilFixedUpdate;

		// Token: 0x040000B9 RID: 185
		protected Vector2 tempSkeletonDisplacement;

		// Token: 0x040000BA RID: 186
		protected Vector3 rigidbodyDisplacement;

		// Token: 0x040000BB RID: 187
		protected Vector3 previousRigidbodyRootMotion = Vector2.zero;

		// Token: 0x040000BC RID: 188
		protected Vector2 additionalRigidbody2DMovement = Vector2.zero;

		// Token: 0x040000BD RID: 189
		protected Quaternion rigidbodyLocalRotation = Quaternion.identity;

		// Token: 0x040000BE RID: 190
		protected float rigidbody2DRotation;

		// Token: 0x040000BF RID: 191
		protected float initialOffsetRotation;

		// Token: 0x040000C0 RID: 192
		protected float tempSkeletonRotation;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000123 RID: 291
		public delegate void RootMotionDelegate(SkeletonRootMotionBase component, Vector2 translation, float rotation);

		// Token: 0x02000029 RID: 41
		public struct RootMotionInfo
		{
			// Token: 0x040000C1 RID: 193
			public Vector2 start;

			// Token: 0x040000C2 RID: 194
			public Vector2 current;

			// Token: 0x040000C3 RID: 195
			public Vector2 mid;

			// Token: 0x040000C4 RID: 196
			public Vector2 end;

			// Token: 0x040000C5 RID: 197
			public bool timeIsPastMid;
		}
	}
}
