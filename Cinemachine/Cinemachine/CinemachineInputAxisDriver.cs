using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public struct CinemachineInputAxisDriver
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00013003 File Offset: 0x00011203
		public void Validate()
		{
			this.accelTime = Mathf.Max(0f, this.accelTime);
			this.decelTime = Mathf.Max(0f, this.decelTime);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00013034 File Offset: 0x00011234
		public bool Update(float deltaTime, ref AxisBase axis)
		{
			if (!string.IsNullOrEmpty(this.name))
			{
				try
				{
					this.inputValue = CinemachineCore.GetInputAxis(this.name);
				}
				catch (ArgumentException)
				{
				}
			}
			float input = this.inputValue * this.multiplier;
			if (deltaTime < 0.0001f)
			{
				this.mCurrentSpeed = 0f;
			}
			else
			{
				float speed = input / deltaTime;
				float dampTime = ((Mathf.Abs(speed) < Mathf.Abs(this.mCurrentSpeed)) ? this.decelTime : this.accelTime);
				speed = this.mCurrentSpeed + Damper.Damp(speed - this.mCurrentSpeed, dampTime, deltaTime);
				this.mCurrentSpeed = speed;
				float range = axis.m_MaxValue - axis.m_MinValue;
				if (!axis.m_Wrap && this.decelTime > 0.0001f && range > 0.0001f)
				{
					float v0 = this.ClampValue(ref axis, axis.m_Value);
					float v = this.ClampValue(ref axis, v0 + speed * deltaTime);
					if (((speed > 0f) ? (axis.m_MaxValue - v) : (v - axis.m_MinValue)) < 0.1f * range && Mathf.Abs(speed) > 0.0001f)
					{
						speed = Damper.Damp(v - v0, this.decelTime, deltaTime) / deltaTime;
					}
				}
				input = speed * deltaTime;
			}
			axis.m_Value = this.ClampValue(ref axis, axis.m_Value + input);
			return Mathf.Abs(this.inputValue) > 0.0001f;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000131A0 File Offset: 0x000113A0
		public bool Update(float deltaTime, ref AxisState axis)
		{
			AxisBase a = new AxisBase
			{
				m_Value = axis.Value,
				m_MinValue = axis.m_MinValue,
				m_MaxValue = axis.m_MaxValue,
				m_Wrap = axis.m_Wrap
			};
			bool flag = this.Update(deltaTime, ref a);
			axis.Value = a.m_Value;
			return flag;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013200 File Offset: 0x00011400
		private float ClampValue(ref AxisBase axis, float v)
		{
			float r = axis.m_MaxValue - axis.m_MinValue;
			if (axis.m_Wrap && r > 0.0001f)
			{
				v = (v - axis.m_MinValue) % r;
				v += axis.m_MinValue + ((v < 0f) ? r : 0f);
			}
			return Mathf.Clamp(v, axis.m_MinValue, axis.m_MaxValue);
		}

		// Token: 0x040002C5 RID: 709
		[Tooltip("Multiply the input by this amount prior to processing.  Controls the input power.")]
		public float multiplier;

		// Token: 0x040002C6 RID: 710
		[Tooltip("The amount of time in seconds it takes to accelerate to a higher speed")]
		public float accelTime;

		// Token: 0x040002C7 RID: 711
		[Tooltip("The amount of time in seconds it takes to decelerate to a lower speed")]
		public float decelTime;

		// Token: 0x040002C8 RID: 712
		[Tooltip("The name of this axis as specified in Unity Input manager. Setting to an empty string will disable the automatic updating of this axis")]
		public string name;

		// Token: 0x040002C9 RID: 713
		[NoSaveDuringPlay]
		[Tooltip("The value of the input axis.  A value of 0 means no input.  You can drive this directly from a custom input system, or you can set the Axis Name and have the value driven by the internal Input Manager")]
		public float inputValue;

		// Token: 0x040002CA RID: 714
		private float mCurrentSpeed;

		// Token: 0x040002CB RID: 715
		private const float Epsilon = 0.0001f;
	}
}
