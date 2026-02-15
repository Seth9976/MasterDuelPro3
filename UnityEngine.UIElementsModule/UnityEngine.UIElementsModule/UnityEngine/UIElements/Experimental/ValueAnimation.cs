using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005DF RID: 1503
	public sealed class ValueAnimation<T> : IValueAnimationUpdate
	{
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000A75EC File Offset: 0x000A57EC
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x000A7604 File Offset: 0x000A5804
		public int durationMs
		{
			get
			{
				return this.m_DurationMs;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					value = 1;
				}
				this.m_DurationMs = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x000A7626 File Offset: 0x000A5826
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x000A762E File Offset: 0x000A582E
		public Func<float, float> easingCurve { get; set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000A7637 File Offset: 0x000A5837
		// (set) Token: 0x060028A9 RID: 10409 RVA: 0x000A763F File Offset: 0x000A583F
		public bool isRunning { get; private set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000A7648 File Offset: 0x000A5848
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000A7650 File Offset: 0x000A5850
		public Action onAnimationCompleted { get; set; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000A7659 File Offset: 0x000A5859
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000A7661 File Offset: 0x000A5861
		public bool autoRecycle { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000A766A File Offset: 0x000A586A
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x000A7672 File Offset: 0x000A5872
		private bool recycled { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000A767B File Offset: 0x000A587B
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x000A7683 File Offset: 0x000A5883
		private VisualElement owner { get; set; }

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000A768C File Offset: 0x000A588C
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000A7694 File Offset: 0x000A5894
		public Action<VisualElement, T> valueUpdated { get; set; }

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000A769D File Offset: 0x000A589D
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000A76A5 File Offset: 0x000A58A5
		public Func<VisualElement, T> initialValue { get; set; }

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000A76AE File Offset: 0x000A58AE
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x000A76B6 File Offset: 0x000A58B6
		public Func<T, T, float, T> interpolator { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000A76C0 File Offset: 0x000A58C0
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x000A770E File Offset: 0x000A590E
		public T from
		{
			get
			{
				bool flag = !this.fromValueSet;
				if (flag)
				{
					bool flag2 = this.initialValue != null;
					if (flag2)
					{
						this.from = this.initialValue(this.owner);
					}
				}
				return this._from;
			}
			set
			{
				this.fromValueSet = true;
				this._from = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x000A771F File Offset: 0x000A591F
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x000A7727 File Offset: 0x000A5927
		public T to { get; set; }

		// Token: 0x060028BC RID: 10428 RVA: 0x000A7730 File Offset: 0x000A5930
		public ValueAnimation()
		{
			this.SetDefaultValues();
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000A7748 File Offset: 0x000A5948
		public void Start()
		{
			this.CheckNotRecycled();
			bool flag = this.owner != null;
			if (flag)
			{
				this.m_StartTimeMs = Panel.TimeSinceStartupMs();
				this.Register();
				this.isRunning = true;
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000A7788 File Offset: 0x000A5988
		public void Stop()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
				this.isRunning = false;
				Action onAnimationCompleted = this.onAnimationCompleted;
				if (onAnimationCompleted != null)
				{
					onAnimationCompleted();
				}
				bool autoRecycle = this.autoRecycle;
				if (autoRecycle)
				{
					bool flag = !this.recycled;
					if (flag)
					{
						this.Recycle();
					}
				}
			}
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000A77EC File Offset: 0x000A59EC
		public void Recycle()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				bool flag = !this.autoRecycle;
				if (!flag)
				{
					this.Stop();
					return;
				}
				this.Stop();
			}
			this.SetDefaultValues();
			this.recycled = true;
			ValueAnimation<T>.sObjectPool.Release(this);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000A784C File Offset: 0x000A5A4C
		void IValueAnimationUpdate.Tick(long currentTimeMs)
		{
			this.CheckNotRecycled();
			long interval = currentTimeMs - this.m_StartTimeMs;
			float progress = (float)interval / (float)this.durationMs;
			bool done = false;
			bool flag = progress >= 1f;
			if (flag)
			{
				progress = 1f;
				done = true;
			}
			Func<float, float> easingCurve = this.easingCurve;
			progress = ((easingCurve != null) ? easingCurve(progress) : progress);
			bool flag2 = this.interpolator != null;
			if (flag2)
			{
				T value = this.interpolator(this.from, this.to, progress);
				Action<VisualElement, T> valueUpdated = this.valueUpdated;
				if (valueUpdated != null)
				{
					valueUpdated(this.owner, value);
				}
			}
			bool flag3 = done;
			if (flag3)
			{
				this.Stop();
			}
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000A78FC File Offset: 0x000A5AFC
		private void SetDefaultValues()
		{
			this.m_DurationMs = 400;
			this.autoRecycle = true;
			this.owner = null;
			this.m_StartTimeMs = 0L;
			this.onAnimationCompleted = null;
			this.valueUpdated = null;
			this.initialValue = null;
			this.interpolator = null;
			this.to = default(T);
			this.from = default(T);
			this.fromValueSet = false;
			this.easingCurve = new Func<float, float>(Easing.OutQuad);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000A7988 File Offset: 0x000A5B88
		private void Unregister()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.UnregisterAnimation(this);
			}
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000A79B4 File Offset: 0x000A5BB4
		private void Register()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.RegisterAnimation(this);
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000A79E0 File Offset: 0x000A5BE0
		internal void SetOwner(VisualElement e)
		{
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
			}
			this.owner = e;
			bool isRunning2 = this.isRunning;
			if (isRunning2)
			{
				this.Register();
			}
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000A7A1C File Offset: 0x000A5C1C
		private void CheckNotRecycled()
		{
			bool recycled = this.recycled;
			if (recycled)
			{
				throw new InvalidOperationException("Animation object has been recycled. Use KeepAlive() to keep a reference to an animation after it has been stopped.");
			}
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000A7A40 File Offset: 0x000A5C40
		public static ValueAnimation<T> Create(VisualElement e, Func<T, T, float, T> interpolator)
		{
			ValueAnimation<T> result = ValueAnimation<T>.sObjectPool.Get();
			result.recycled = false;
			result.SetOwner(e);
			result.interpolator = interpolator;
			return result;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000A7A78 File Offset: 0x000A5C78
		public ValueAnimation<T> KeepAlive()
		{
			this.autoRecycle = false;
			return this;
		}

		// Token: 0x04001577 RID: 5495
		private long m_StartTimeMs;

		// Token: 0x04001578 RID: 5496
		private int m_DurationMs;

		// Token: 0x0400157E RID: 5502
		private static ObjectPool<ValueAnimation<T>> sObjectPool = new ObjectPool<ValueAnimation<T>>(() => new ValueAnimation<T>(), 100);

		// Token: 0x04001583 RID: 5507
		private T _from;

		// Token: 0x04001584 RID: 5508
		private bool fromValueSet = false;
	}
}
