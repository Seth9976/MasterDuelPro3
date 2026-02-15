using System;
using MDPro3;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x0200066F RID: 1647
	public class PlatformActiveOverrider : PropertyOverriderBase<Transform>
	{
		// Token: 0x06003329 RID: 13097 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(Transform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(Transform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000F2AB8 File Offset: 0x000F0CB8
		private void Start()
		{
			if (this.m_Active.m_DefaultValue && Program.root != "StandaloneWindows64/")
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.m_Active.m_MobileValue && Program.root == "StandaloneWindows64/")
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04002F69 RID: 12137
		[SerializeField]
		private OverrideBoolProperty m_Active;
	}
}
