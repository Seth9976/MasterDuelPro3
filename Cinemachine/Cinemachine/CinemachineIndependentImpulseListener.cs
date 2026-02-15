using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000C4 RID: 196
	[SaveDuringPlay]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/api/Cinemachine.CinemachineIndependentImpulseListener.html")]
	public class CinemachineIndependentImpulseListener : MonoBehaviour
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00018AC4 File Offset: 0x00016CC4
		private void Reset()
		{
			this.m_ChannelMask = 1;
			this.m_Gain = 1f;
			this.m_Use2DDistance = false;
			this.m_UseLocalSpace = true;
			this.m_ReactionSettings = new CinemachineImpulseListener.ImpulseReaction
			{
				m_AmplitudeGain = 1f,
				m_FrequencyGain = 1f,
				m_Duration = 1f
			};
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00018B24 File Offset: 0x00016D24
		private void OnEnable()
		{
			this.impulsePosLastFrame = Vector3.zero;
			this.impulseRotLastFrame = Quaternion.identity;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00018B3C File Offset: 0x00016D3C
		private void Update()
		{
			base.transform.position -= this.impulsePosLastFrame;
			base.transform.rotation = base.transform.rotation * Quaternion.Inverse(this.impulseRotLastFrame);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00018B8C File Offset: 0x00016D8C
		private void LateUpdate()
		{
			bool impulseAt = CinemachineImpulseManager.Instance.GetImpulseAt(base.transform.position, this.m_Use2DDistance, this.m_ChannelMask, out this.impulsePosLastFrame, out this.impulseRotLastFrame);
			Vector3 reactionPos;
			Quaternion reactionRot;
			bool haveReaction = this.m_ReactionSettings.GetReaction(Time.deltaTime, this.impulsePosLastFrame, out reactionPos, out reactionRot);
			if (impulseAt)
			{
				this.impulseRotLastFrame = Quaternion.SlerpUnclamped(Quaternion.identity, this.impulseRotLastFrame, this.m_Gain);
				this.impulsePosLastFrame *= this.m_Gain;
			}
			if (haveReaction)
			{
				this.impulsePosLastFrame += reactionPos;
				this.impulseRotLastFrame *= reactionRot;
			}
			if (impulseAt || haveReaction)
			{
				if (this.m_UseLocalSpace)
				{
					this.impulsePosLastFrame = base.transform.rotation * this.impulsePosLastFrame;
				}
				base.transform.position += this.impulsePosLastFrame;
				base.transform.rotation = base.transform.rotation * this.impulseRotLastFrame;
			}
		}

		// Token: 0x04000401 RID: 1025
		private Vector3 impulsePosLastFrame;

		// Token: 0x04000402 RID: 1026
		private Quaternion impulseRotLastFrame;

		// Token: 0x04000403 RID: 1027
		[Tooltip("Impulse events on channels not included in the mask will be ignored.")]
		[CinemachineImpulseChannelProperty]
		public int m_ChannelMask;

		// Token: 0x04000404 RID: 1028
		[Tooltip("Gain to apply to the Impulse signal.  1 is normal strength.  Setting this to 0 completely mutes the signal.")]
		public float m_Gain;

		// Token: 0x04000405 RID: 1029
		[Tooltip("Enable this to perform distance calculation in 2D (ignore Z)")]
		public bool m_Use2DDistance;

		// Token: 0x04000406 RID: 1030
		[Tooltip("Enable this to process all impulse signals in camera space")]
		public bool m_UseLocalSpace;

		// Token: 0x04000407 RID: 1031
		[Tooltip("This controls the secondary reaction of the listener to the incoming impulse.  The impulse might be for example a sharp shock, and the secondary reaction could be a vibration whose amplitude and duration is controlled by the size of the original impulse.  This allows different listeners to respond in different ways to the same impulse signal.")]
		public CinemachineImpulseListener.ImpulseReaction m_ReactionSettings;
	}
}
