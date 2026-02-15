using System;
using System.Text;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000068 RID: 104
	public class CinemachineBlend
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		public float BlendWeight
		{
			get
			{
				if (this.BlendCurve == null || this.BlendCurve.length < 2 || this.IsComplete)
				{
					return 1f;
				}
				return Mathf.Clamp01(this.BlendCurve.Evaluate(this.TimeInBlend / this.Duration));
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00011C3E File Offset: 0x0000FE3E
		public bool IsValid
		{
			get
			{
				return (this.CamA != null && this.CamA.IsValid) || (this.CamB != null && this.CamB.IsValid);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public bool IsComplete
		{
			get
			{
				return this.TimeInBlend >= this.Duration || !this.IsValid;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00011C88 File Offset: 0x0000FE88
		public string Description
		{
			get
			{
				StringBuilder sb = CinemachineDebug.SBFromPool();
				if (this.CamB == null || !this.CamB.IsValid)
				{
					sb.Append("(none)");
				}
				else
				{
					sb.Append("[");
					sb.Append(this.CamB.Name);
					sb.Append("]");
				}
				sb.Append(" ");
				sb.Append((int)(this.BlendWeight * 100f));
				sb.Append("% from ");
				if (this.CamA == null || !this.CamA.IsValid)
				{
					sb.Append("(none)");
				}
				else
				{
					sb.Append("[");
					sb.Append(this.CamA.Name);
					sb.Append("]");
				}
				string text = sb.ToString();
				CinemachineDebug.ReturnToPool(sb);
				return text;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00011D70 File Offset: 0x0000FF70
		public bool Uses(ICinemachineCamera cam)
		{
			if (cam == this.CamA || cam == this.CamB)
			{
				return true;
			}
			BlendSourceVirtualCamera b = this.CamA as BlendSourceVirtualCamera;
			if (b != null && b.Blend.Uses(cam))
			{
				return true;
			}
			b = this.CamB as BlendSourceVirtualCamera;
			return b != null && b.Blend.Uses(cam);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public CinemachineBlend(ICinemachineCamera a, ICinemachineCamera b, AnimationCurve curve, float duration, float t)
		{
			this.CamA = a;
			this.CamB = b;
			this.BlendCurve = curve;
			this.TimeInBlend = t;
			this.Duration = duration;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00011E00 File Offset: 0x00010000
		public void UpdateCameraState(Vector3 worldUp, float deltaTime)
		{
			if (this.CamA != null && this.CamA.IsValid)
			{
				this.CamA.UpdateCameraState(worldUp, deltaTime);
			}
			if (this.CamB != null && this.CamB.IsValid)
			{
				this.CamB.UpdateCameraState(worldUp, deltaTime);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00011E54 File Offset: 0x00010054
		public CameraState State
		{
			get
			{
				if (this.CamA == null || !this.CamA.IsValid)
				{
					if (this.CamB == null || !this.CamB.IsValid)
					{
						return CameraState.Default;
					}
					return this.CamB.State;
				}
				else
				{
					if (this.CamB == null || !this.CamB.IsValid)
					{
						return this.CamA.State;
					}
					return CameraState.Lerp(this.CamA.State, this.CamB.State, this.BlendWeight);
				}
			}
		}

		// Token: 0x04000275 RID: 629
		public ICinemachineCamera CamA;

		// Token: 0x04000276 RID: 630
		public ICinemachineCamera CamB;

		// Token: 0x04000277 RID: 631
		public AnimationCurve BlendCurve;

		// Token: 0x04000278 RID: 632
		public float TimeInBlend;

		// Token: 0x04000279 RID: 633
		public float Duration;
	}
}
