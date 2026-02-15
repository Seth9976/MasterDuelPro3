using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MDPro3.UI.PropertyOverride;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013E9 RID: 5097
	public class PageLegacy : MonoBehaviour
	{
		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x060093A1 RID: 37793 RVA: 0x0014E49C File Offset: 0x0014C69C
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x060093A2 RID: 37794 RVA: 0x0014E4D0 File Offset: 0x0014C6D0
		public ScrollRect ScrollRectPreset
		{
			get
			{
				return this.m_ScrollRectPreset = ((this.m_ScrollRectPreset != null) ? this.m_ScrollRectPreset : this.Manager.GetElement<ScrollRect>("ScrollRectPreset"));
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x060093A3 RID: 37795 RVA: 0x0014E50C File Offset: 0x0014C70C
		private TMP_InputField InputName
		{
			get
			{
				return this.m_InputName = ((this.m_InputName != null) ? this.m_InputName : this.Manager.GetElement<TMP_InputField>("InputFieldName"));
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x060093A4 RID: 37796 RVA: 0x0014E548 File Offset: 0x0014C748
		private TMP_InputField InputHost
		{
			get
			{
				return this.m_InputHost = ((this.m_InputHost != null) ? this.m_InputHost : this.Manager.GetElement<TMP_InputField>("InputFieldHost"));
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x060093A5 RID: 37797 RVA: 0x0014E584 File Offset: 0x0014C784
		private TMP_InputField InputPort
		{
			get
			{
				return this.m_InputPort = ((this.m_InputPort != null) ? this.m_InputPort : this.Manager.GetElement<TMP_InputField>("InputFieldPort"));
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x060093A6 RID: 37798 RVA: 0x0014E5C0 File Offset: 0x0014C7C0
		private TMP_InputField InputPassword
		{
			get
			{
				return this.m_InputPassword = ((this.m_InputPassword != null) ? this.m_InputPassword : this.Manager.GetElement<TMP_InputField>("InputFieldPassword"));
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x060093A7 RID: 37799 RVA: 0x0014E5FC File Offset: 0x0014C7FC
		private SelectionButton ButtonSave
		{
			get
			{
				return this.m_ButtonSave = ((this.m_ButtonSave != null) ? this.m_ButtonSave : this.Manager.GetElement<SelectionButton>("ButtonSave"));
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x060093A8 RID: 37800 RVA: 0x0014E638 File Offset: 0x0014C838
		private SelectionButton ButtonJoin
		{
			get
			{
				return this.m_ButtonJoin = ((this.m_ButtonJoin != null) ? this.m_ButtonJoin : this.Manager.GetElement<SelectionButton>("ButtonJoin"));
			}
		}

		// Token: 0x060093A9 RID: 37801 RVA: 0x0014E674 File Offset: 0x0014C874
		private void Awake()
		{
			this.ResetLegacy();
		}

		// Token: 0x060093AA RID: 37802 RVA: 0x0014E67C File Offset: 0x0014C87C
		private void ResetLegacy()
		{
			this.InputName.text = Config.Get("DuelPlayerName0", "@ui");
			this.InputHost.text = Config.Get("Host", "s1.ygo233.com");
			this.InputPort.text = Config.Get("Port", "233");
			this.InputPassword.text = Config.Get("Password", "@ui");
		}

		// Token: 0x060093AB RID: 37803 RVA: 0x0014E6F4 File Offset: 0x0014C8F4
		private void LoadHostAddresses()
		{
			if (!File.Exists(this.PATH_ADDRESS_SAVE))
			{
				return;
			}
			string[] lines = File.ReadAllText(this.PATH_ADDRESS_SAVE).Replace("\r", "").Split('\n', StringSplitOptions.None);
			for (int i = 0; i < lines.Length; i++)
			{
				string[] mats = Regex.Split(lines[i], " ");
				PageLegacy.HostAddress address = default(PageLegacy.HostAddress);
				if (mats.Length >= 3)
				{
					address.name = mats[0];
					address.host = mats[1];
					address.port = mats[2];
					address.password = string.Empty;
					if (mats.Length > 3)
					{
						address.password = mats[3];
					}
					this.addresses.Add(address);
				}
			}
			this.addressedLoaded = true;
		}

		// Token: 0x060093AC RID: 37804 RVA: 0x0014E7AC File Offset: 0x0014C9AC
		private void SaveHostAddresses()
		{
			string content = string.Empty;
			foreach (PageLegacy.HostAddress address in this.addresses)
			{
				content = content + address.name + " ";
				content = content + address.host + " ";
				content = content + address.port + " ";
				content = content + address.password + "\r\n";
			}
			File.WriteAllText(this.PATH_ADDRESS_SAVE, content);
		}

		// Token: 0x060093AD RID: 37805 RVA: 0x0014E854 File Offset: 0x0014CA54
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Address component = item.GetComponent<SelectionToggle_Address>();
			component.addressName = task[0];
			component.addressHost = task[1];
			component.addressPort = task[2];
			component.addressPassword = task[3];
			component.Refresh();
		}

		// Token: 0x060093AE RID: 37806 RVA: 0x0014E888 File Offset: 0x0014CA88
		private void AddAddress(string name)
		{
			PageLegacy.HostAddress address = new PageLegacy.HostAddress
			{
				name = name,
				host = this.InputHost.text,
				port = this.InputPort.text,
				password = this.InputPassword.text
			};
			foreach (PageLegacy.HostAddress add in this.addresses)
			{
				if (add.name == name)
				{
					this.addresses.Remove(add);
					break;
				}
			}
			this.addresses.Add(address);
			this.SaveHostAddresses();
			this.PrintAddresses("");
		}

		// Token: 0x060093AF RID: 37807 RVA: 0x0014E958 File Offset: 0x0014CB58
		public void PrintAddresses(string search = "")
		{
			if (!this.addressedLoaded)
			{
				this.LoadHostAddresses();
			}
			SuperScrollView superScrollView = this.hostSuperScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			List<string[]> tasks = new List<string[]>();
			foreach (PageLegacy.HostAddress address in this.addresses)
			{
				if (address.name.Contains(search))
				{
					string[] task = new string[] { address.name, address.host, address.port, address.password };
					tasks.Add(task);
				}
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemAddress.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 460f : 360f);
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 80f : 40f);
				this.hostSuperScrollView = new SuperScrollView(1, itemWidth, itemHeight, 0f, 0f, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRectPreset, 2);
				this.hostSuperScrollView.Print(tasks);
				if (this.hostSuperScrollView.items.Count > 0)
				{
					Program.instance.online.lastSelectedAddressItem = this.hostSuperScrollView.items[0].gameObject.GetComponent<SelectionToggle_Address>();
				}
			};
		}

		// Token: 0x060093B0 RID: 37808 RVA: 0x0014EA40 File Offset: 0x0014CC40
		public void SetHost(string host, string port, string passwd)
		{
			this.InputHost.text = host;
			this.InputPort.text = port;
			this.InputPassword.text = passwd;
			this.OnNameChange(this.InputName.text);
			this.OnHostChange(host);
			this.OnPortChange(port);
			this.OnPasswordChange(passwd);
		}

		// Token: 0x060093B1 RID: 37809 RVA: 0x0014EA98 File Offset: 0x0014CC98
		public void DeleteAddress(string hostName)
		{
			foreach (PageLegacy.HostAddress address in this.addresses)
			{
				if (address.name == hostName)
				{
					this.addresses.Remove(address);
				}
			}
			this.SaveHostAddresses();
			this.PrintAddresses("");
		}

		// Token: 0x060093B2 RID: 37810 RVA: 0x0014EB10 File Offset: 0x0014CD10
		public void AddressMoveUp(string hostName)
		{
			int index = -1;
			for (int i = 0; i < this.addresses.Count; i++)
			{
				if (this.addresses[i].name == hostName)
				{
					index = i;
					break;
				}
			}
			if (index < 0)
			{
				Debug.LogError("Did not find target host.");
				return;
			}
			if (index == 0)
			{
				return;
			}
			PageLegacy.HostAddress host = this.addresses[index];
			this.addresses.RemoveAt(index);
			index--;
			this.addresses.Insert(index, host);
			this.SaveHostAddresses();
			this.PrintAddresses("");
		}

		// Token: 0x060093B3 RID: 37811 RVA: 0x0014EBA0 File Offset: 0x0014CDA0
		public void OnNameChange(string name)
		{
			Config.Set("DuelPlayerName0", (name == "") ? "@ui" : name);
			Config.Save();
		}

		// Token: 0x060093B4 RID: 37812 RVA: 0x0014EBC6 File Offset: 0x0014CDC6
		public void OnHostChange(string host)
		{
			Config.Set("Host", host);
			Config.Save();
		}

		// Token: 0x060093B5 RID: 37813 RVA: 0x0014EBD8 File Offset: 0x0014CDD8
		public void OnPortChange(string port)
		{
			Config.Set("Port", port);
			Config.Save();
		}

		// Token: 0x060093B6 RID: 37814 RVA: 0x0014EBEA File Offset: 0x0014CDEA
		public void OnPasswordChange(string password)
		{
			Config.Set("Password", (password == "") ? "@ui" : password);
			Config.Save();
		}

		// Token: 0x060093B7 RID: 37815 RVA: 0x0014EC10 File Offset: 0x0014CE10
		public void OnPresetSave()
		{
			UIManager.ShowPopupInput(new List<string>
			{
				InterString.Get("请输入预设名称", 0),
				string.Empty
			}, new Action<string>(this.AddAddress), null, TmpInputValidation.ValidationType.NoSpace);
		}

		// Token: 0x060093B8 RID: 37816 RVA: 0x0014EC46 File Offset: 0x0014CE46
		public void OnJoin()
		{
			Program.instance.online.KF_OnlineGame(this.InputName.text, this.InputHost.text, this.InputPort.text, this.InputPassword.text);
		}

		// Token: 0x060093B9 RID: 37817 RVA: 0x0014EC83 File Offset: 0x0014CE83
		public void SelectLastAddressItem()
		{
			if (Program.instance.online.lastSelectedAddressItem != null)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.online.lastSelectedAddressItem.GetSelectable().Select();
			}
		}

		// Token: 0x060093BA RID: 37818 RVA: 0x0014ECBB File Offset: 0x0014CEBB
		public void SelectDefault()
		{
			this.ButtonJoin.GetSelectable().Select();
		}

		// Token: 0x0400D202 RID: 53762
		private ElementObjectManager m_Manager;

		// Token: 0x0400D203 RID: 53763
		private const string LABEL_SR_PRESET = "ScrollRectPreset";

		// Token: 0x0400D204 RID: 53764
		private ScrollRect m_ScrollRectPreset;

		// Token: 0x0400D205 RID: 53765
		private const string LABEL_IPT_NAME = "InputFieldName";

		// Token: 0x0400D206 RID: 53766
		private TMP_InputField m_InputName;

		// Token: 0x0400D207 RID: 53767
		private const string LABEL_IPT_HOST = "InputFieldHost";

		// Token: 0x0400D208 RID: 53768
		private TMP_InputField m_InputHost;

		// Token: 0x0400D209 RID: 53769
		private const string LABEL_IPT_PORT = "InputFieldPort";

		// Token: 0x0400D20A RID: 53770
		private TMP_InputField m_InputPort;

		// Token: 0x0400D20B RID: 53771
		private const string LABEL_IPT_PASSWORD = "InputFieldPassword";

		// Token: 0x0400D20C RID: 53772
		private TMP_InputField m_InputPassword;

		// Token: 0x0400D20D RID: 53773
		private const string LABEL_SBN_SAVE = "ButtonSave";

		// Token: 0x0400D20E RID: 53774
		private SelectionButton m_ButtonSave;

		// Token: 0x0400D20F RID: 53775
		private const string LABEL_SBN_JOIN = "ButtonJoin";

		// Token: 0x0400D210 RID: 53776
		private SelectionButton m_ButtonJoin;

		// Token: 0x0400D211 RID: 53777
		private readonly List<PageLegacy.HostAddress> addresses = new List<PageLegacy.HostAddress>();

		// Token: 0x0400D212 RID: 53778
		private readonly string PATH_ADDRESS_SAVE = (Language.UseChinese() ? "Data/hosts.conf" : "Data/hosts2.conf");

		// Token: 0x0400D213 RID: 53779
		private SuperScrollView hostSuperScrollView;

		// Token: 0x0400D214 RID: 53780
		private bool addressedLoaded;

		// Token: 0x020013EA RID: 5098
		private struct HostAddress
		{
			// Token: 0x0400D215 RID: 53781
			public string name;

			// Token: 0x0400D216 RID: 53782
			public string host;

			// Token: 0x0400D217 RID: 53783
			public string port;

			// Token: 0x0400D218 RID: 53784
			public string password;
		}
	}
}
