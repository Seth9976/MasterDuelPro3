using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000B2 RID: 178
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineImpulseFixedSignals.html")]
	public class CinemachineFixedSignal : SignalSourceAsset
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000174F5 File Offset: 0x000156F5
		public override float SignalDuration
		{
			get
			{
				return Mathf.Max(this.AxisDuration(this.m_XCurve), Mathf.Max(this.AxisDuration(this.m_YCurve), this.AxisDuration(this.m_ZCurve)));
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00017528 File Offset: 0x00015728
		private float AxisDuration(AnimationCurve axis)
		{
			float duration = 0f;
			if (axis != null && axis.length > 1)
			{
				float start = axis[0].time;
				duration = axis[axis.length - 1].time - start;
			}
			return duration;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00017571 File Offset: 0x00015771
		public override void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot)
		{
			rot = Quaternion.identity;
			pos = new Vector3(this.AxisValue(this.m_XCurve, timeSinceSignalStart), this.AxisValue(this.m_YCurve, timeSinceSignalStart), this.AxisValue(this.m_ZCurve, timeSinceSignalStart));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000175B0 File Offset: 0x000157B0
		private float AxisValue(AnimationCurve axis, float time)
		{
			if (axis == null || axis.length == 0)
			{
				return 0f;
			}
			return axis.Evaluate(time);
		}

		// Token: 0x040003AA RID: 938
		[Tooltip("The raw signal shape along the X axis")]
		public AnimationCurve m_XCurve;

		// Token: 0x040003AB RID: 939
		[Tooltip("The raw signal shape along the Y axis")]
		public AnimationCurve m_YCurve;

		// Token: 0x040003AC RID: 940
		[Tooltip("The raw signal shape along the Z axis")]
		public AnimationCurve m_ZCurve;
	}
}
