using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004D1 RID: 1233
	public class Sound : MonoBehaviour
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sound Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002764 RID: 10084 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsReady
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002766 RID: 10086 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsLoadingLabelXml
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SoundDebugPrint(string str)
		{
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SoundDebugPrintWarning(string str)
		{
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0000216A File Offset: 0x0000036A
		private string getMasterName(Sound.Master master)
		{
			return null;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadSoundSettingCoroutine()
		{
			return null;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadSoundResourcesXml()
		{
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadSoundAssetBundleXml()
		{
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadSysSoundDataAsync(Action finishedCallback)
		{
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadDuelSoundXml(string[] NameList, bool checkExist = true)
		{
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadDuelSoundDataAsync(Action finishedCallback)
		{
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadMateSoundDataAsync(Action finishedCallback)
		{
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadFieldSoundDataAsync(Action finishedCallback)
		{
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadDuelSoundData()
		{
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadAllSoundData()
		{
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadSoundXml(string[] NameList, bool checkExist = true)
		{
			return null;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCategorySoundDataAsync(string categoryName, Action finishedCallback, bool updateRndSrc = true)
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadCategorySoundData(string categoryName, bool removeLabel = false)
		{
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadSoundClip(string Label)
		{
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator LoadSoundClipAsyncCoroutine(List<string> targetList, Action finishedCallback)
		{
			return null;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool LoadSoundClipAsync(string Label, Action finishedCallback)
		{
			return false;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UnloadSoundClip(string Label)
		{
			return false;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsCategoryMember(string label, string category)
		{
			return false;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLoadingXml()
		{
			return false;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsAudioManagerLoadingLabel()
		{
			return false;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearObject()
		{
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Play(string label)
		{
			return 0;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000029CC File Offset: 0x00000BCC
		private int PlayImpl(string label, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Play3D(string label, Vector3 position)
		{
			return 0;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Play3D(string label, GameObject traceTarget)
		{
			return 0;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000029CC File Offset: 0x00000BCC
		public int PlayOption(string label, float volume = -1f, float fadeTime = -1f, float pan = -1f, int pitch = -1, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPan(int instanceID, float newPan, float moveTime = 0f)
		{
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop(int instanceId, float fade = -1f)
		{
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop(string label, float fade = -1f)
		{
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopLoopInCategory(string categoryName, float fade = -1f)
		{
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopAll()
		{
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMasterVolume(Sound.Master masterName, float volume, float moveTime = -1f)
		{
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetMasterVolume(Sound.Master masterName)
		{
			return 0f;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000029CC File Offset: 0x00000BCC
		public int PlayBGM(string label, float delay = -1f)
		{
			return 0;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadBGM()
		{
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingBGM()
		{
			return false;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingBGM(string label)
		{
			return false;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLoopSe(string label)
		{
			return false;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSe(string label)
		{
			return false;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingLoopSe(string label)
		{
			return false;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingSe(string label)
		{
			return false;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingLabel(string label)
		{
			return false;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayingBgmLabel()
		{
			return null;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0000216D File Offset: 0x0000036D
		public void ForceStopBGMAll()
		{
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopBGM(float fade = -1f)
		{
		}

		// Token: 0x0400284F RID: 10319
		private static Sound s_instance;

		// Token: 0x04002850 RID: 10320
		private string[] ResourcesXmlList;

		// Token: 0x04002851 RID: 10321
		private string[] AssetBundleXmlList;

		// Token: 0x04002852 RID: 10322
		private string CurrentBGM;

		// Token: 0x04002853 RID: 10323
		private static List<string> BGMList;

		// Token: 0x04002854 RID: 10324
		private List<string> AB_SE_DUEL;

		// Token: 0x020004D2 RID: 1234
		public enum Master
		{
			// Token: 0x04002856 RID: 10326
			BGM,
			// Token: 0x04002857 RID: 10327
			SE,
			// Token: 0x04002858 RID: 10328
			VOICE
		}
	}
}
