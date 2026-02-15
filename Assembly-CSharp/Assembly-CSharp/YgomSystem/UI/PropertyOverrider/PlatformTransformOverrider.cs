using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x0200067F RID: 1663
	public class PlatformTransformOverrider : PropertyOverriderBase<Transform>
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(Transform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(Transform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002FA3 RID: 12195
		[SerializeField]
		private OverrideVector3Property m_Position;

		// Token: 0x04002FA4 RID: 12196
		[SerializeField]
		private OverrideQuaternionProperty m_Rotation;

		// Token: 0x04002FA5 RID: 12197
		[SerializeField]
		private OverrideVector3Property m_Scale;
	}
}
