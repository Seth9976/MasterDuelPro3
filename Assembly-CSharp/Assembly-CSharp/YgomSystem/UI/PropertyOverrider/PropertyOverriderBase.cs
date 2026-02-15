using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000681 RID: 1665
	public abstract class PropertyOverriderBase<Target> : PlatformPropertyOverriderInterface, IPlatformPropertyOverrider where Target : Component
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003382 RID: 13186 RVA: 0x0000216D File Offset: 0x0000036D
		public override OverrideMode overrideMode
		{
			get
			{
				return OverrideMode.ToCurrentPlatform;
			}
			set
			{
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000F2B80 File Offset: 0x000F0D80
		protected virtual Target GetTargetComponent()
		{
			return default(Target);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000029CC File Offset: 0x00000BCC
		private DeviceInfo.PlatformType GetCurrentPlatformType()
		{
			return DeviceInfo.PlatformType.Unknown;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ApplyImmediate()
		{
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ApplyImmediate(DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import()
		{
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export()
		{
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600338D RID: 13197
		public abstract void Export(Target target, DeviceInfo.PlatformType platformType);

		// Token: 0x0600338E RID: 13198
		public abstract void Import(Target target, DeviceInfo.PlatformType platformType);

		// Token: 0x04002FA8 RID: 12200
		[SerializeField]
		private PlatformOverriderGroup m_Group;

		// Token: 0x04002FA9 RID: 12201
		[SerializeField]
		private string m_SwitchLabel;

		// Token: 0x04002FAA RID: 12202
		[SerializeField]
		[HideInInspector]
		private OverrideMode m_OverrideMode;

		// Token: 0x04002FAB RID: 12203
		private bool m_IsDone;
	}
}
