using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200002B RID: 43
	[ExecuteAlways]
	[AddComponentMenu("Spine/SkeletonAnimation")]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonAnimation-Component")]
	public class SkeletonAnimation : SkeletonRenderer, ISkeletonAnimation, ISpineComponent, IAnimationStateComponent
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007B0C File Offset: 0x00005D0C
		public AnimationState AnimationState
		{
			get
			{
				this.Initialize(false, false);
				return this.state;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000129 RID: 297 RVA: 0x00007B1C File Offset: 0x00005D1C
		// (remove) Token: 0x0600012A RID: 298 RVA: 0x00007B54 File Offset: 0x00005D54
		protected event ISkeletonAnimationDelegate _OnAnimationRebuild;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600012B RID: 299 RVA: 0x00007B8C File Offset: 0x00005D8C
		// (remove) Token: 0x0600012C RID: 300 RVA: 0x00007BC4 File Offset: 0x00005DC4
		protected event UpdateBonesDelegate _BeforeApply;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600012D RID: 301 RVA: 0x00007BFC File Offset: 0x00005DFC
		// (remove) Token: 0x0600012E RID: 302 RVA: 0x00007C34 File Offset: 0x00005E34
		protected event UpdateBonesDelegate _UpdateLocal;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600012F RID: 303 RVA: 0x00007C6C File Offset: 0x00005E6C
		// (remove) Token: 0x06000130 RID: 304 RVA: 0x00007CA4 File Offset: 0x00005EA4
		protected event UpdateBonesDelegate _UpdateWorld;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000131 RID: 305 RVA: 0x00007CDC File Offset: 0x00005EDC
		// (remove) Token: 0x06000132 RID: 306 RVA: 0x00007D14 File Offset: 0x00005F14
		protected event UpdateBonesDelegate _UpdateComplete;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000133 RID: 307 RVA: 0x00007D49 File Offset: 0x00005F49
		// (remove) Token: 0x06000134 RID: 308 RVA: 0x00007D52 File Offset: 0x00005F52
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

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000135 RID: 309 RVA: 0x00007D5B File Offset: 0x00005F5B
		// (remove) Token: 0x06000136 RID: 310 RVA: 0x00007D64 File Offset: 0x00005F64
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

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000137 RID: 311 RVA: 0x00007D6D File Offset: 0x00005F6D
		// (remove) Token: 0x06000138 RID: 312 RVA: 0x00007D76 File Offset: 0x00005F76
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

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000139 RID: 313 RVA: 0x00007D7F File Offset: 0x00005F7F
		// (remove) Token: 0x0600013A RID: 314 RVA: 0x00007D88 File Offset: 0x00005F88
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

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600013B RID: 315 RVA: 0x00007D91 File Offset: 0x00005F91
		// (remove) Token: 0x0600013C RID: 316 RVA: 0x00007D9A File Offset: 0x00005F9A
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007DA3 File Offset: 0x00005FA3
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00007DAB File Offset: 0x00005FAB
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007DB4 File Offset: 0x00005FB4
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00007DBC File Offset: 0x00005FBC
		public bool UnscaledTime
		{
			get
			{
				return this.unscaledTime;
			}
			set
			{
				this.unscaledTime = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007DC8 File Offset: 0x00005FC8
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007E04 File Offset: 0x00006004
		public string AnimationName
		{
			get
			{
				if (!this.valid)
				{
					return this._animationName;
				}
				TrackEntry entry = this.state.GetCurrent(0);
				if (entry != null)
				{
					return entry.Animation.Name;
				}
				return null;
			}
			set
			{
				this.Initialize(false, false);
				if (this._animationName == value)
				{
					TrackEntry entry = this.state.GetCurrent(0);
					if (entry != null && entry.Loop == this.loop)
					{
						return;
					}
				}
				this._animationName = value;
				if (string.IsNullOrEmpty(value))
				{
					this.state.ClearTrack(0);
					return;
				}
				Animation animationObject = this.skeletonDataAsset.GetSkeletonData(false).FindAnimation(value);
				if (animationObject != null)
				{
					this.state.SetAnimation(0, animationObject, this.loop);
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007E8C File Offset: 0x0000608C
		public static SkeletonAnimation AddToGameObject(GameObject gameObject, SkeletonDataAsset skeletonDataAsset, bool quiet = false)
		{
			return SkeletonRenderer.AddSpineComponent<SkeletonAnimation>(gameObject, skeletonDataAsset, quiet);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007E96 File Offset: 0x00006096
		public static SkeletonAnimation NewSkeletonAnimationGameObject(SkeletonDataAsset skeletonDataAsset, bool quiet = false)
		{
			return SkeletonRenderer.NewSpineGameObject<SkeletonAnimation>(skeletonDataAsset, quiet);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007E9F File Offset: 0x0000609F
		public override void ClearState()
		{
			base.ClearState();
			if (this.state != null)
			{
				this.state.ClearTracks();
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007EBC File Offset: 0x000060BC
		public override void Initialize(bool overwrite, bool quiet = false)
		{
			if (this.valid && !overwrite)
			{
				return;
			}
			this.state = null;
			base.Initialize(overwrite, quiet);
			if (!this.valid)
			{
				return;
			}
			this.state = new AnimationState(this.skeletonDataAsset.GetAnimationStateData());
			this.wasUpdatedAfterInit = false;
			if (!string.IsNullOrEmpty(this._animationName))
			{
				Animation animationObject = this.skeletonDataAsset.GetSkeletonData(false).FindAnimation(this._animationName);
				if (animationObject != null)
				{
					this.state.SetAnimation(0, animationObject, this.loop);
				}
			}
			if (this._OnAnimationRebuild != null)
			{
				this._OnAnimationRebuild(this);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007F5A File Offset: 0x0000615A
		protected virtual void Update()
		{
			if (this.updateTiming != UpdateTiming.InUpdate)
			{
				return;
			}
			this.Update(this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007F80 File Offset: 0x00006180
		protected virtual void FixedUpdate()
		{
			if (this.updateTiming != UpdateTiming.InFixedUpdate)
			{
				return;
			}
			this.Update(this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007FA6 File Offset: 0x000061A6
		public void Update(float deltaTime)
		{
			if (!this.valid || this.state == null)
			{
				return;
			}
			this.wasUpdatedAfterInit = true;
			if (this.updateMode < UpdateMode.OnlyAnimationStatus)
			{
				return;
			}
			this.UpdateAnimationStatus(deltaTime);
			if (this.updateMode == UpdateMode.OnlyAnimationStatus)
			{
				return;
			}
			this.ApplyAnimation();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007FE4 File Offset: 0x000061E4
		protected void UpdateAnimationStatus(float deltaTime)
		{
			deltaTime *= this.timeScale;
			this.state.Update(deltaTime);
			this.skeleton.Update(deltaTime);
			this.ApplyTransformMovementToPhysics();
			if (this.updateMode == UpdateMode.OnlyAnimationStatus)
			{
				this.state.ApplyEventTimelinesOnly(this.skeleton, false);
				return;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00008038 File Offset: 0x00006238
		public virtual void ApplyAnimation()
		{
			if (this._BeforeApply != null)
			{
				this._BeforeApply(this);
			}
			if (this.updateMode != UpdateMode.OnlyEventTimelines)
			{
				this.state.Apply(this.skeleton);
			}
			else
			{
				this.state.ApplyEventTimelinesOnly(this.skeleton, true);
			}
			this.AfterAnimationApplied();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00008090 File Offset: 0x00006290
		public void AfterAnimationApplied()
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

		// Token: 0x0600014D RID: 333 RVA: 0x000080F0 File Offset: 0x000062F0
		public override void LateUpdate()
		{
			if (this.updateTiming == UpdateTiming.InLateUpdate && this.valid)
			{
				this.Update(this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
			}
			if (!this.wasUpdatedAfterInit)
			{
				this.Update(0f);
			}
			base.LateUpdate();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008144 File Offset: 0x00006344
		public override void OnBecameVisible()
		{
			UpdateMode previousUpdateMode = this.updateMode;
			this.updateMode = UpdateMode.FullUpdate;
			if (previousUpdateMode != UpdateMode.FullUpdate && previousUpdateMode != UpdateMode.EverythingExceptMesh)
			{
				this.Update(0f);
			}
			if (previousUpdateMode != UpdateMode.FullUpdate)
			{
				this.LateUpdate();
			}
		}

		// Token: 0x040000C7 RID: 199
		public AnimationState state;

		// Token: 0x040000C8 RID: 200
		private bool wasUpdatedAfterInit = true;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		protected UpdateTiming updateTiming = UpdateTiming.InUpdate;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		protected bool unscaledTime;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		[SpineAnimation("", "", true, false, false)]
		private string _animationName;

		// Token: 0x040000D1 RID: 209
		public bool loop;

		// Token: 0x040000D2 RID: 210
		public float timeScale = 1f;
	}
}
