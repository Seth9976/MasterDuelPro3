using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000078 RID: 120
	[AddComponentMenu("UI/Toggle Group", 31)]
	[DisallowMultipleComponent]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00016201 File Offset: 0x00014401
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00016209 File Offset: 0x00014409
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016212 File Offset: 0x00014412
		protected ToggleGroup()
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016225 File Offset: 0x00014425
		protected override void Start()
		{
			this.EnsureValidState();
			base.Start();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016233 File Offset: 0x00014433
		protected override void OnEnable()
		{
			this.EnsureValidState();
			base.OnEnable();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016241 File Offset: 0x00014441
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016278 File Offset: 0x00014478
		public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					if (sendCallback)
					{
						this.m_Toggles[i].isOn = false;
					}
					else
					{
						this.m_Toggles[i].SetIsOnWithoutNotify(false);
					}
				}
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000162DF File Offset: 0x000144DF
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000162FC File Offset: 0x000144FC
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016318 File Offset: 0x00014518
		public void EnsureValidState()
		{
			if (!this.allowSwitchOff && !this.AnyTogglesOn() && this.m_Toggles.Count != 0)
			{
				this.m_Toggles[0].isOn = true;
				this.NotifyToggleOn(this.m_Toggles[0], true);
			}
			IEnumerable<Toggle> activeToggles = this.ActiveToggles();
			if (activeToggles.Count<Toggle>() > 1)
			{
				Toggle firstActive = this.GetFirstActiveToggle();
				foreach (Toggle toggle in activeToggles)
				{
					if (!(toggle == firstActive))
					{
						toggle.isOn = false;
					}
				}
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000163C4 File Offset: 0x000145C4
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000163F6 File Offset: 0x000145F6
		public IEnumerable<Toggle> ActiveToggles()
		{
			return this.m_Toggles.Where((Toggle x) => x.isOn);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00016424 File Offset: 0x00014624
		public Toggle GetFirstActiveToggle()
		{
			IEnumerable<Toggle> activeToggles = this.ActiveToggles();
			if (activeToggles.Count<Toggle>() <= 0)
			{
				return null;
			}
			return activeToggles.First<Toggle>();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001644C File Offset: 0x0001464C
		public void SetAllTogglesOff(bool sendCallback = true)
		{
			bool oldAllowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			if (sendCallback)
			{
				for (int i = 0; i < this.m_Toggles.Count; i++)
				{
					this.m_Toggles[i].isOn = false;
				}
			}
			else
			{
				for (int j = 0; j < this.m_Toggles.Count; j++)
				{
					this.m_Toggles[j].SetIsOnWithoutNotify(false);
				}
			}
			this.m_AllowSwitchOff = oldAllowSwitchOff;
		}

		// Token: 0x04000256 RID: 598
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x04000257 RID: 599
		protected List<Toggle> m_Toggles = new List<Toggle>();
	}
}
