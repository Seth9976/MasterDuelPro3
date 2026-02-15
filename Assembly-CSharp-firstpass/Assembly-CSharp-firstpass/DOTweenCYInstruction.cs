using System;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000056 RID: 86
	public static class DOTweenCYInstruction
	{
		// Token: 0x02000057 RID: 87
		public class WaitForCompletion : CustomYieldInstruction
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600013F RID: 319 RVA: 0x000054CE File Offset: 0x000036CE
			public override bool keepWaiting
			{
				get
				{
					return this.t.active && !this.t.IsComplete();
				}
			}

			// Token: 0x06000140 RID: 320 RVA: 0x000054ED File Offset: 0x000036ED
			public WaitForCompletion(Tween tween)
			{
				this.t = tween;
			}

			// Token: 0x04000084 RID: 132
			private readonly Tween t;
		}

		// Token: 0x02000058 RID: 88
		public class WaitForRewind : CustomYieldInstruction
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000141 RID: 321 RVA: 0x000054FC File Offset: 0x000036FC
			public override bool keepWaiting
			{
				get
				{
					return this.t.active && (!this.t.playedOnce || this.t.position * (float)(this.t.CompletedLoops() + 1) > 0f);
				}
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00005548 File Offset: 0x00003748
			public WaitForRewind(Tween tween)
			{
				this.t = tween;
			}

			// Token: 0x04000085 RID: 133
			private readonly Tween t;
		}

		// Token: 0x02000059 RID: 89
		public class WaitForKill : CustomYieldInstruction
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00005557 File Offset: 0x00003757
			public override bool keepWaiting
			{
				get
				{
					return this.t.active;
				}
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00005564 File Offset: 0x00003764
			public WaitForKill(Tween tween)
			{
				this.t = tween;
			}

			// Token: 0x04000086 RID: 134
			private readonly Tween t;
		}

		// Token: 0x0200005A RID: 90
		public class WaitForElapsedLoops : CustomYieldInstruction
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00005573 File Offset: 0x00003773
			public override bool keepWaiting
			{
				get
				{
					return this.t.active && this.t.CompletedLoops() < this.elapsedLoops;
				}
			}

			// Token: 0x06000146 RID: 326 RVA: 0x00005597 File Offset: 0x00003797
			public WaitForElapsedLoops(Tween tween, int elapsedLoops)
			{
				this.t = tween;
				this.elapsedLoops = elapsedLoops;
			}

			// Token: 0x04000087 RID: 135
			private readonly Tween t;

			// Token: 0x04000088 RID: 136
			private readonly int elapsedLoops;
		}

		// Token: 0x0200005B RID: 91
		public class WaitForPosition : CustomYieldInstruction
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000147 RID: 327 RVA: 0x000055AD File Offset: 0x000037AD
			public override bool keepWaiting
			{
				get
				{
					return this.t.active && this.t.position * (float)(this.t.CompletedLoops() + 1) < this.position;
				}
			}

			// Token: 0x06000148 RID: 328 RVA: 0x000055E0 File Offset: 0x000037E0
			public WaitForPosition(Tween tween, float position)
			{
				this.t = tween;
				this.position = position;
			}

			// Token: 0x04000089 RID: 137
			private readonly Tween t;

			// Token: 0x0400008A RID: 138
			private readonly float position;
		}

		// Token: 0x0200005C RID: 92
		public class WaitForStart : CustomYieldInstruction
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000149 RID: 329 RVA: 0x000055F6 File Offset: 0x000037F6
			public override bool keepWaiting
			{
				get
				{
					return this.t.active && !this.t.playedOnce;
				}
			}

			// Token: 0x0600014A RID: 330 RVA: 0x00005615 File Offset: 0x00003815
			public WaitForStart(Tween tween)
			{
				this.t = tween;
			}

			// Token: 0x0400008B RID: 139
			private readonly Tween t;
		}
	}
}
