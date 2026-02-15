using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu.Common;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AB2 RID: 2738
	public class OutGameBGManager : MonoBehaviour
	{
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06004FAD RID: 20397 RVA: 0x0000216A File Offset: 0x0000036A
		public static OutGameBGManager Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004FAF RID: 20399 RVA: 0x0000216D File Offset: 0x0000036D
		public bool BGExist
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

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004FB1 RID: 20401 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsCheckingBGExistance
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

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnTransitionStart(GameObject parent, ViewController.TransitionType type, int backBgID, bool async = true, float duration = 0.5f)
		{
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx PushFrontBG(int frontID, bool async = true)
		{
			return null;
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx PushFrontBG(string pathName, bool async = true)
		{
			return null;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx PushBackBG(int backID, bool async = true, float duration = 0.5f)
		{
			return null;
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx PushBackBG(string pathName, bool async = true, float duration = 0.5f)
		{
			return null;
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddTweenInOut(string label, GameObject target, Color from, Color to, UnityAction onFinished = null, float duration = 0.5f)
		{
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTweenInOut(GameObject target, string label, float duration, bool wakeup = false)
		{
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddParticleTween(string label, GameObject target, float from, float to, float duration = 0.5f)
		{
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SetSortingOrder(GameObject target, int sortingBase)
		{
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0000216D File Offset: 0x0000036D
		public void PopFrontBG()
		{
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0000216D File Offset: 0x0000036D
		public void PopBackBG(float duration = 0.5f)
		{
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0000216D File Offset: 0x0000036D
		public void PopAll()
		{
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExistBackBG(string path)
		{
			return false;
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0000216D File Offset: 0x0000036D
		public void DispRoot(bool activeSelf)
		{
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetTopFrontBG()
		{
			return null;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetTopBackBG()
		{
			return null;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTopFrontBGPath(string defaultPath = "")
		{
			return null;
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTopBackBGPath(string defaultPath = "")
		{
			return null;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool EqualTopBGPath(string backBGPath, string frontBGPath)
		{
			return false;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetBackBGCount(int bgId)
		{
			return 0;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayTweenShow(GameObject target, bool immediate = false)
		{
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlayTweenHide(GameObject target, bool immediate = false)
		{
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0000216D File Offset: 0x0000036D
		private static void PlayTween(GameObject target, string playLabel, bool immediate = false)
		{
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPlayingShowHideTween(GameObject target)
		{
			return false;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0000216D File Offset: 0x0000036D
		public void CheckBGExistence()
		{
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator CheckBGExistenceRoutine()
		{
			return null;
		}

		// Token: 0x04008DBE RID: 36286
		private Dictionary<string, OutGameBGManager.BGInfo> m_FrontBGInfoDic;

		// Token: 0x04008DBF RID: 36287
		private Dictionary<string, OutGameBGManager.BGInfo> m_BackBGInfoDic;

		// Token: 0x04008DC0 RID: 36288
		public Stack<string> m_FrontPathStack;

		// Token: 0x04008DC1 RID: 36289
		public Stack<string> m_BackPathStack;

		// Token: 0x04008DC2 RID: 36290
		internal GameObject m_Root;

		// Token: 0x04008DC3 RID: 36291
		private GameObject m_FrontRoot;

		// Token: 0x04008DC4 RID: 36292
		private GameObject m_BackRoot;

		// Token: 0x04008DC5 RID: 36293
		private const string k_BGINIT = "BgInit";

		// Token: 0x04008DC6 RID: 36294
		private const string k_BGIN = "BgIn";

		// Token: 0x04008DC7 RID: 36295
		private const string k_BGOUT = "BgOut";

		// Token: 0x04008DC8 RID: 36296
		private const int k_ORDER_LAYER1 = -990;

		// Token: 0x04008DC9 RID: 36297
		private const int k_ORDER_LAYER2 = -970;

		// Token: 0x04008DCA RID: 36298
		public const int ID_BASE = 1;

		// Token: 0x04008DCB RID: 36299
		public const int ID_DUELPASS_GOLD = 2;

		// Token: 0x04008DCC RID: 36300
		public const int ID_DUELISTCUP = 4;

		// Token: 0x04008DCD RID: 36301
		public const int ID_WCS = 7;

		// Token: 0x04008DCE RID: 36302
		private static OutGameBGManager instance;

		// Token: 0x02000AB3 RID: 2739
		private class BGInfo
		{
			// Token: 0x06004FCE RID: 20430 RVA: 0x00002739 File Offset: 0x00000939
			public BGInfo(GameObject target)
			{
			}

			// Token: 0x06004FCF RID: 20431 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddCount()
			{
			}

			// Token: 0x06004FD0 RID: 20432 RVA: 0x0000216D File Offset: 0x0000036D
			public void DecCount()
			{
			}

			// Token: 0x06004FD1 RID: 20433 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCount()
			{
				return 0;
			}

			// Token: 0x06004FD2 RID: 20434 RVA: 0x0000216D File Offset: 0x0000036D
			public void Disp(bool isDisp)
			{
			}

			// Token: 0x06004FD3 RID: 20435 RVA: 0x0000216D File Offset: 0x0000036D
			public void EndTween()
			{
			}

			// Token: 0x06004FD4 RID: 20436 RVA: 0x0000216D File Offset: 0x0000036D
			public void Remove()
			{
			}

			// Token: 0x06004FD5 RID: 20437 RVA: 0x0000216A File Offset: 0x0000036A
			public BindingGameObjectEx GetBGOEX()
			{
				return null;
			}

			// Token: 0x04008DCF RID: 36303
			private int refCount;

			// Token: 0x04008DD0 RID: 36304
			internal GameObject target;
		}
	}
}
