using System;

namespace Spine
{
	// Token: 0x02000037 RID: 55
	public class TrackEntry : Pool<TrackEntry>.IPoolable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000125 RID: 293 RVA: 0x00008980 File Offset: 0x00006B80
		// (remove) Token: 0x06000126 RID: 294 RVA: 0x000089B8 File Offset: 0x00006BB8
		public event AnimationState.TrackEntryDelegate Start;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000127 RID: 295 RVA: 0x000089F0 File Offset: 0x00006BF0
		// (remove) Token: 0x06000128 RID: 296 RVA: 0x00008A28 File Offset: 0x00006C28
		public event AnimationState.TrackEntryDelegate Interrupt;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000129 RID: 297 RVA: 0x00008A60 File Offset: 0x00006C60
		// (remove) Token: 0x0600012A RID: 298 RVA: 0x00008A98 File Offset: 0x00006C98
		public event AnimationState.TrackEntryDelegate End;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600012B RID: 299 RVA: 0x00008AD0 File Offset: 0x00006CD0
		// (remove) Token: 0x0600012C RID: 300 RVA: 0x00008B08 File Offset: 0x00006D08
		public event AnimationState.TrackEntryDelegate Dispose;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600012D RID: 301 RVA: 0x00008B40 File Offset: 0x00006D40
		// (remove) Token: 0x0600012E RID: 302 RVA: 0x00008B78 File Offset: 0x00006D78
		public event AnimationState.TrackEntryDelegate Complete;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600012F RID: 303 RVA: 0x00008BB0 File Offset: 0x00006DB0
		// (remove) Token: 0x06000130 RID: 304 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public event AnimationState.TrackEntryEventDelegate Event;

		// Token: 0x06000131 RID: 305 RVA: 0x00008C1D File Offset: 0x00006E1D
		internal void OnStart()
		{
			if (this.Start != null)
			{
				this.Start(this);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008C33 File Offset: 0x00006E33
		internal void OnInterrupt()
		{
			if (this.Interrupt != null)
			{
				this.Interrupt(this);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008C49 File Offset: 0x00006E49
		internal void OnEnd()
		{
			if (this.End != null)
			{
				this.End(this);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008C5F File Offset: 0x00006E5F
		internal void OnDispose()
		{
			if (this.Dispose != null)
			{
				this.Dispose(this);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008C75 File Offset: 0x00006E75
		internal void OnComplete()
		{
			if (this.Complete != null)
			{
				this.Complete(this);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008C8B File Offset: 0x00006E8B
		internal void OnEvent(Event e)
		{
			if (this.Event != null)
			{
				this.Event(this, e);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public void Reset()
		{
			this.previous = null;
			this.next = null;
			this.mixingFrom = null;
			this.mixingTo = null;
			this.animation = null;
			this.Start = null;
			this.Interrupt = null;
			this.End = null;
			this.Dispose = null;
			this.Complete = null;
			this.Event = null;
			this.timelineMode.Clear(true);
			this.timelineHoldMix.Clear(true);
			this.timelinesRotation.Clear(true);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00008D22 File Offset: 0x00006F22
		public int TrackIndex
		{
			get
			{
				return this.trackIndex;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00008D2A File Offset: 0x00006F2A
		public Animation Animation
		{
			get
			{
				return this.animation;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008D32 File Offset: 0x00006F32
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00008D3A File Offset: 0x00006F3A
		public bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				this.loop = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00008D43 File Offset: 0x00006F43
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00008D4B File Offset: 0x00006F4B
		public float Delay
		{
			get
			{
				return this.delay;
			}
			set
			{
				this.delay = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00008D54 File Offset: 0x00006F54
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00008D5C File Offset: 0x00006F5C
		public float TrackTime
		{
			get
			{
				return this.trackTime;
			}
			set
			{
				this.trackTime = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00008D65 File Offset: 0x00006F65
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00008D6D File Offset: 0x00006F6D
		public float TrackEnd
		{
			get
			{
				return this.trackEnd;
			}
			set
			{
				this.trackEnd = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00008D78 File Offset: 0x00006F78
		public float TrackComplete
		{
			get
			{
				float duration = this.animationEnd - this.animationStart;
				if (duration != 0f)
				{
					if (this.loop)
					{
						return duration * (float)(1 + (int)(this.trackTime / duration));
					}
					if (this.trackTime < duration)
					{
						return duration;
					}
				}
				return this.trackTime;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00008DC3 File Offset: 0x00006FC3
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00008DCB File Offset: 0x00006FCB
		public float AnimationStart
		{
			get
			{
				return this.animationStart;
			}
			set
			{
				this.animationStart = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00008DD4 File Offset: 0x00006FD4
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00008DDC File Offset: 0x00006FDC
		public float AnimationEnd
		{
			get
			{
				return this.animationEnd;
			}
			set
			{
				this.animationEnd = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00008DE5 File Offset: 0x00006FE5
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00008DED File Offset: 0x00006FED
		public float AnimationLast
		{
			get
			{
				return this.animationLast;
			}
			set
			{
				this.animationLast = value;
				this.nextAnimationLast = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00008E00 File Offset: 0x00007000
		public float AnimationTime
		{
			get
			{
				if (this.loop)
				{
					float duration = this.animationEnd - this.animationStart;
					if (duration == 0f)
					{
						return this.animationStart;
					}
					return this.trackTime % duration + this.animationStart;
				}
				else
				{
					float animationTime = this.trackTime + this.animationStart;
					if (this.animationEnd < this.animation.duration)
					{
						return Math.Min(animationTime, this.animationEnd);
					}
					return animationTime;
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00008E71 File Offset: 0x00007071
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00008E79 File Offset: 0x00007079
		public float TimeScale
		{
			get
			{
				return this.timeScale;
			}
			set
			{
				this.timeScale = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008E82 File Offset: 0x00007082
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00008E8A File Offset: 0x0000708A
		public float Alpha
		{
			get
			{
				return this.alpha;
			}
			set
			{
				this.alpha = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00008E93 File Offset: 0x00007093
		public float InterruptAlpha
		{
			get
			{
				return this.interruptAlpha;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00008E9B File Offset: 0x0000709B
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00008EA3 File Offset: 0x000070A3
		public float EventThreshold
		{
			get
			{
				return this.eventThreshold;
			}
			set
			{
				this.eventThreshold = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00008EAC File Offset: 0x000070AC
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00008EB4 File Offset: 0x000070B4
		public float AlphaAttachmentThreshold
		{
			get
			{
				return this.alphaAttachmentThreshold;
			}
			set
			{
				this.alphaAttachmentThreshold = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00008EBD File Offset: 0x000070BD
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00008EC5 File Offset: 0x000070C5
		public float MixAttachmentThreshold
		{
			get
			{
				return this.mixAttachmentThreshold;
			}
			set
			{
				this.mixAttachmentThreshold = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00008ECE File Offset: 0x000070CE
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00008ED6 File Offset: 0x000070D6
		public float MixDrawOrderThreshold
		{
			get
			{
				return this.mixDrawOrderThreshold;
			}
			set
			{
				this.mixDrawOrderThreshold = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008EDF File Offset: 0x000070DF
		public TrackEntry Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00008EE7 File Offset: 0x000070E7
		public TrackEntry Previous
		{
			get
			{
				return this.previous;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008EEF File Offset: 0x000070EF
		public bool WasApplied
		{
			get
			{
				return this.nextTrackLast != -1f;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008F01 File Offset: 0x00007101
		public bool IsNextReady
		{
			get
			{
				return this.next != null && this.nextTrackLast - this.next.delay >= 0f;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008F29 File Offset: 0x00007129
		public bool IsComplete
		{
			get
			{
				return this.trackTime >= this.animationEnd - this.animationStart;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008F43 File Offset: 0x00007143
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00008F4B File Offset: 0x0000714B
		public float MixTime
		{
			get
			{
				return this.mixTime;
			}
			set
			{
				this.mixTime = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008F54 File Offset: 0x00007154
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00008F5C File Offset: 0x0000715C
		public float MixDuration
		{
			get
			{
				return this.mixDuration;
			}
			set
			{
				this.mixDuration = value;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008F65 File Offset: 0x00007165
		public void SetMixDuration(float mixDuration, float delay)
		{
			this.mixDuration = mixDuration;
			if (this.previous != null && delay <= 0f)
			{
				delay += this.previous.TrackComplete - mixDuration;
			}
			this.delay = delay;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008F96 File Offset: 0x00007196
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00008F9E File Offset: 0x0000719E
		public MixBlend MixBlend
		{
			get
			{
				return this.mixBlend;
			}
			set
			{
				this.mixBlend = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008FA7 File Offset: 0x000071A7
		public TrackEntry MixingFrom
		{
			get
			{
				return this.mixingFrom;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008FAF File Offset: 0x000071AF
		public TrackEntry MixingTo
		{
			get
			{
				return this.mixingTo;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008FB7 File Offset: 0x000071B7
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00008FBF File Offset: 0x000071BF
		public bool HoldPrevious
		{
			get
			{
				return this.holdPrevious;
			}
			set
			{
				this.holdPrevious = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00008FC8 File Offset: 0x000071C8
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00008FD0 File Offset: 0x000071D0
		public bool Reverse
		{
			get
			{
				return this.reverse;
			}
			set
			{
				this.reverse = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008FD9 File Offset: 0x000071D9
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00008FE1 File Offset: 0x000071E1
		public bool ShortestRotation
		{
			get
			{
				return this.shortestRotation;
			}
			set
			{
				this.shortestRotation = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008FEA File Offset: 0x000071EA
		public bool IsEmptyAnimation
		{
			get
			{
				return this.animation == AnimationState.EmptyAnimation;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008FF9 File Offset: 0x000071F9
		public void ResetRotationDirections()
		{
			this.timelinesRotation.Clear(true);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009007 File Offset: 0x00007207
		public override string ToString()
		{
			if (this.animation != null)
			{
				return this.animation.name;
			}
			return "<none>";
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009022 File Offset: 0x00007222
		public void AllowImmediateQueue()
		{
			if (this.nextTrackLast < 0f)
			{
				this.nextTrackLast = 0f;
			}
		}

		// Token: 0x040000BA RID: 186
		internal Animation animation;

		// Token: 0x040000BB RID: 187
		internal TrackEntry previous;

		// Token: 0x040000BC RID: 188
		internal TrackEntry next;

		// Token: 0x040000BD RID: 189
		internal TrackEntry mixingFrom;

		// Token: 0x040000BE RID: 190
		internal TrackEntry mixingTo;

		// Token: 0x040000C5 RID: 197
		internal int trackIndex;

		// Token: 0x040000C6 RID: 198
		internal bool loop;

		// Token: 0x040000C7 RID: 199
		internal bool holdPrevious;

		// Token: 0x040000C8 RID: 200
		internal bool reverse;

		// Token: 0x040000C9 RID: 201
		internal bool shortestRotation;

		// Token: 0x040000CA RID: 202
		internal float eventThreshold;

		// Token: 0x040000CB RID: 203
		internal float mixAttachmentThreshold;

		// Token: 0x040000CC RID: 204
		internal float alphaAttachmentThreshold;

		// Token: 0x040000CD RID: 205
		internal float mixDrawOrderThreshold;

		// Token: 0x040000CE RID: 206
		internal float animationStart;

		// Token: 0x040000CF RID: 207
		internal float animationEnd;

		// Token: 0x040000D0 RID: 208
		internal float animationLast;

		// Token: 0x040000D1 RID: 209
		internal float nextAnimationLast;

		// Token: 0x040000D2 RID: 210
		internal float delay;

		// Token: 0x040000D3 RID: 211
		internal float trackTime;

		// Token: 0x040000D4 RID: 212
		internal float trackLast;

		// Token: 0x040000D5 RID: 213
		internal float nextTrackLast;

		// Token: 0x040000D6 RID: 214
		internal float trackEnd;

		// Token: 0x040000D7 RID: 215
		internal float timeScale = 1f;

		// Token: 0x040000D8 RID: 216
		internal float alpha;

		// Token: 0x040000D9 RID: 217
		internal float mixTime;

		// Token: 0x040000DA RID: 218
		internal float mixDuration;

		// Token: 0x040000DB RID: 219
		internal float interruptAlpha;

		// Token: 0x040000DC RID: 220
		internal float totalAlpha;

		// Token: 0x040000DD RID: 221
		internal MixBlend mixBlend = MixBlend.Replace;

		// Token: 0x040000DE RID: 222
		internal readonly ExposedList<int> timelineMode = new ExposedList<int>();

		// Token: 0x040000DF RID: 223
		internal readonly ExposedList<TrackEntry> timelineHoldMix = new ExposedList<TrackEntry>();

		// Token: 0x040000E0 RID: 224
		internal readonly ExposedList<float> timelinesRotation = new ExposedList<float>();
	}
}
