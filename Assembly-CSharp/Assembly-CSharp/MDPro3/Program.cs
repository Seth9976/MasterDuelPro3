using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3
{
	// Token: 0x0200128C RID: 4748
	public class Program : MonoBehaviour
	{
		// Token: 0x06008B57 RID: 35671 RVA: 0x0011B038 File Offset: 0x00119238
		private async UniTask Initialize()
		{
			if (!Directory.Exists("Data/"))
			{
				Directory.CreateDirectory("Data/");
			}
			Config.Initialize("Data/config.conf");
			Screen.sleepTimeout = -1;
			OnlineService.Initialize();
			if (!ABLoader.mdCached)
			{
				await ABLoader.CacheMasterDuelOutDuelBundles();
			}
			if (Program.items == null)
			{
				AsyncOperationHandle<Items> handle = Addressables.LoadAssetAsync<Items>("ScriptableObjects/Items.asset");
				await handle.Task;
				Program.items = handle.Result;
				handle = default(AsyncOperationHandle<Items>);
			}
			this.cardRenderer = (await Addressables.InstantiateAsync("Prefab/CardRenderer.prefab", null, false, true)).GetComponent<CardRenderer>();
			ZipHelper.Initialize();
			Program.items.Initialize();
			BanlistManager.Initialize();
			this.InitializeAllManagers();
			this.InitializeAllServants();
		}

		// Token: 0x06008B58 RID: 35672 RVA: 0x0011B07C File Offset: 0x0011927C
		public void ReadParams()
		{
			string[] args = Environment.GetCommandLineArgs();
			string nick = null;
			string host = null;
			string port = null;
			string password = null;
			string deck = null;
			string replay = null;
			string puzzle = null;
			bool join = false;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].ToLower() == "-n" && args.Length > i + 1)
				{
					nick = args[++i];
					Config.Set("DuelPlayerName0", nick);
					Config.Save();
				}
				if (args[i].ToLower() == "-h" && args.Length > i + 1)
				{
					host = args[++i];
				}
				if (args[i].ToLower() == "-p" && args.Length > i + 1)
				{
					port = args[++i];
				}
				if (args[i].ToLower() == "-w" && args.Length > i + 1)
				{
					password = args[++i];
				}
				if (args[i].ToLower() == "-d" && args.Length > i + 1)
				{
					deck = args[++i];
				}
				if (args[i].ToLower() == "-r" && args.Length > i + 1)
				{
					replay = args[++i];
				}
				if (args[i].ToLower() == "-s" && args.Length > i + 1)
				{
					puzzle = args[++i];
				}
				if (args[i].ToLower() == "-j")
				{
					join = true;
					Config.SetConfigDeck(deck, false);
					Config.Save();
				}
			}
			if (join)
			{
				this.online.KF_OnlineGame(nick, host, port, password);
				Program.exitOnReturn = true;
				return;
			}
			if (deck != null)
			{
				Config.SetConfigDeck(deck, false);
				this.deckEditor.SwitchCondition(DeckEditor.Condition.EditDeck, "", null);
				this.ShiftToServant(this.deckEditor);
				Program.exitOnReturn = true;
				return;
			}
			if (replay != null)
			{
				Program.exitOnReturn = true;
				this.replay.PlayReplay(replay);
				return;
			}
			if (puzzle != null)
			{
				this.puzzle.StartPuzzle("Puzzle/" + puzzle);
				Program.exitOnReturn = true;
			}
		}

		// Token: 0x06008B59 RID: 35673 RVA: 0x0011B292 File Offset: 0x00119492
		public void InitializeForDataChange()
		{
			ZipHelper.Initialize();
			BanlistManager.Initialize();
			StringHelper.Initialize();
			CardsManager.Initialize();
		}

		// Token: 0x06008B5A RID: 35674 RVA: 0x0011B2A8 File Offset: 0x001194A8
		private void InitializeAllManagers()
		{
			this.managers.Add(this.texture_);
			this.managers.Add(this.ui_);
			this.managers.Add(this.camera_);
			this.managers.Add(this.audio_);
			this.managers.Add(this.background_);
			this.managers.Add(this.message_);
			foreach (Manager manager in this.managers)
			{
				manager.Initialize();
			}
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x0011B360 File Offset: 0x00119560
		private void InitializeAllServants()
		{
			this.servants.Add(this.setting);
			this.servants.Add(this.menu);
			this.servants.Add(this.solo);
			this.servants.Add(this.online);
			this.servants.Add(this.puzzle);
			this.servants.Add(this.replay);
			this.servants.Add(this.cutin);
			this.servants.Add(this.mate);
			this.servants.Add(this.deckSelector);
			this.servants.Add(this.appearance);
			this.servants.Add(this.character);
			this.servants.Add(this.ocgcore);
			this.servants.Add(this.room);
			this.servants.Add(this.deckEditor);
			this.servants.Add(this.onlineDeckViewer);
			this.servants.Add(this.deckBrowser);
			foreach (Servant servant in this.servants)
			{
				servant.Initialize();
			}
		}

		// Token: 0x06008B5C RID: 35676 RVA: 0x0011B4C0 File Offset: 0x001196C0
		private void Awake()
		{
			Program.SetRoot();
			Program.instance = this;
			this.Initialize();
		}

		// Token: 0x06008B5D RID: 35677 RVA: 0x0011B4D4 File Offset: 0x001196D4
		public static void SetRoot()
		{
			Program.root = "StandaloneWindows64/";
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06008B5E RID: 35678 RVA: 0x0011B4E0 File Offset: 0x001196E0
		// (set) Token: 0x06008B5F RID: 35679 RVA: 0x0011B4E8 File Offset: 0x001196E8
		public float TimeScale
		{
			get
			{
				return this.m_TimeScale;
			}
			set
			{
				this.m_TimeScale = value;
				Time.timeScale = value;
			}
		}

		// Token: 0x06008B60 RID: 35680 RVA: 0x0011B4F8 File Offset: 0x001196F8
		private void Update()
		{
			TcpHelper.PerFrameFunction();
			foreach (Manager manager in this.managers)
			{
				manager.PerFrameFunction();
			}
			foreach (Servant servant in this.servants)
			{
				servant.PerFrameFunction();
			}
		}

		// Token: 0x06008B61 RID: 35681 RVA: 0x0011B58C File Offset: 0x0011978C
		public void UnloadUnusedAssets()
		{
			this.UnloadUnusedAssetsAsync();
		}

		// Token: 0x06008B62 RID: 35682 RVA: 0x0011B598 File Offset: 0x00119798
		private async UniTask UnloadUnusedAssetsAsync()
		{
			await Resources.UnloadUnusedAssets();
		}

		// Token: 0x06008B63 RID: 35683 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Debug(string text)
		{
		}

		// Token: 0x06008B64 RID: 35684 RVA: 0x0011B5D4 File Offset: 0x001197D4
		public void ShiftToServant(Servant servant)
		{
			this.currentServant = servant;
			foreach (Servant ser in this.servants)
			{
				if (ser != servant)
				{
					ser.Hide(servant.Depth);
				}
			}
			servant.Show(this.depth);
			this.depth = servant.Depth;
		}

		// Token: 0x06008B65 RID: 35685 RVA: 0x0011B654 File Offset: 0x00119854
		public void ShowSubServant(Servant servant)
		{
			if (this.currentSubServant == null)
			{
				servant.Show(0);
				this.currentSubServant = servant;
				return;
			}
			this.currentSubServant.Hide(servant.Depth);
			servant.Show(this.currentSubServant.Depth);
			this.currentSubServant = servant;
		}

		// Token: 0x06008B66 RID: 35686 RVA: 0x0011B6A8 File Offset: 0x001198A8
		public void ExitCurrentServant()
		{
			if (this.currentSubServant != null)
			{
				this.currentSubServant.OnReturn();
				return;
			}
			if (this.currentServant == null)
			{
				foreach (Servant servant in this.servants)
				{
					if (servant.showing)
					{
						this.currentServant = servant;
						break;
					}
				}
				if (this.currentServant == null)
				{
					this.currentServant = this.online;
				}
			}
			this.currentServant.OnReturn();
		}

		// Token: 0x06008B67 RID: 35687 RVA: 0x0011B754 File Offset: 0x00119954
		public void ExitDuel()
		{
			this.currentSubServant.OnReturn();
			this.currentServant.OnReturn();
		}

		// Token: 0x06008B68 RID: 35688 RVA: 0x0011B76C File Offset: 0x0011996C
		private void OnApplicationQuit()
		{
			Program.Running = false;
			Config.Save();
			this.ClearCache();
			YgoServer.StopServer();
			ZipHelper.Dispose();
			try
			{
				TcpHelper.tcpClient.Close();
			}
			catch
			{
			}
			TcpHelper.tcpClient = null;
			MyCard.CloseAthleticWatchListWebSocket();
		}

		// Token: 0x06008B69 RID: 35689 RVA: 0x0011B7C0 File Offset: 0x001199C0
		private void ClearCache()
		{
			if (Directory.Exists("TempFolder/"))
			{
				Directory.Delete("TempFolder/", true);
			}
		}

		// Token: 0x06008B6A RID: 35690 RVA: 0x0011B7D9 File Offset: 0x001199D9
		public static void GameQuit()
		{
			Application.Quit();
		}

		// Token: 0x0400C6F5 RID: 50933
		[Header("Public References")]
		public Transform container_3D;

		// Token: 0x0400C6F6 RID: 50934
		public Transform container_2D;

		// Token: 0x0400C6F7 RID: 50935
		public CardRenderer cardRenderer;

		// Token: 0x0400C6F8 RID: 50936
		[Header("Manager")]
		public CameraManager camera_;

		// Token: 0x0400C6F9 RID: 50937
		public UIManager ui_;

		// Token: 0x0400C6FA RID: 50938
		public BackgroundManager background_;

		// Token: 0x0400C6FB RID: 50939
		public AudioManager audio_;

		// Token: 0x0400C6FC RID: 50940
		public TextureManager texture_;

		// Token: 0x0400C6FD RID: 50941
		public MessageManager message_;

		// Token: 0x0400C6FE RID: 50942
		[Header("Servants")]
		public MainMenu menu;

		// Token: 0x0400C6FF RID: 50943
		public SoloSelector solo;

		// Token: 0x0400C700 RID: 50944
		public OnlineServant online;

		// Token: 0x0400C701 RID: 50945
		public PuzzleSelector puzzle;

		// Token: 0x0400C702 RID: 50946
		public ReplaySelector replay;

		// Token: 0x0400C703 RID: 50947
		public CutinViewer cutin;

		// Token: 0x0400C704 RID: 50948
		public MateViewer mate;

		// Token: 0x0400C705 RID: 50949
		public DeckSelector deckSelector;

		// Token: 0x0400C706 RID: 50950
		public SettingServant setting;

		// Token: 0x0400C707 RID: 50951
		public Appearance appearance;

		// Token: 0x0400C708 RID: 50952
		public CharacterSelector character;

		// Token: 0x0400C709 RID: 50953
		public OcgCore ocgcore;

		// Token: 0x0400C70A RID: 50954
		public RoomServant room;

		// Token: 0x0400C70B RID: 50955
		public DeckEditor deckEditor;

		// Token: 0x0400C70C RID: 50956
		public OnlineDeckViewer onlineDeckViewer;

		// Token: 0x0400C70D RID: 50957
		public DeckBrowser deckBrowser;

		// Token: 0x0400C70E RID: 50958
		[HideInInspector]
		public Servant currentServant;

		// Token: 0x0400C70F RID: 50959
		[HideInInspector]
		public Servant currentSubServant;

		// Token: 0x0400C710 RID: 50960
		[HideInInspector]
		public int depth;

		// Token: 0x0400C711 RID: 50961
		public static bool Running = true;

		// Token: 0x0400C712 RID: 50962
		public const string PATH_ART = "Picture/Art/";

		// Token: 0x0400C713 RID: 50963
		public const string PATH_ALT_ART = "Picture/Art2/";

		// Token: 0x0400C714 RID: 50964
		public const string PATH_CARD_PIC = "Picture/CardGenerated/";

		// Token: 0x0400C715 RID: 50965
		public const string PATH_CLOSEUP = "Picture/Closeup/";

		// Token: 0x0400C716 RID: 50966
		public const string PATH_DATA = "Data/";

		// Token: 0x0400C717 RID: 50967
		public const string PATH_LOCALES = "Data/locales/";

		// Token: 0x0400C718 RID: 50968
		public const string PATH_CONFIG = "Data/config.conf";

		// Token: 0x0400C719 RID: 50969
		public const string PATH_LFLIST = "Data/lflist.conf";

		// Token: 0x0400C71A RID: 50970
		public const string PATH_DECK = "Deck/";

		// Token: 0x0400C71B RID: 50971
		public const string PATH_EXPANSIONS = "Expansions/";

		// Token: 0x0400C71C RID: 50972
		public const string PATH_PUZZLE = "Puzzle/";

		// Token: 0x0400C71D RID: 50973
		public const string PATH_REPLAY = "Replay/";

		// Token: 0x0400C71E RID: 50974
		public const string PATH_DIY = "Picture/DIY/";

		// Token: 0x0400C71F RID: 50975
		public const string PATH_VIDEO_ART = "Video/Art/";

		// Token: 0x0400C720 RID: 50976
		public const string EXPANSION_CONF = ".conf";

		// Token: 0x0400C721 RID: 50977
		public const string EXPANSION_YDK = ".ydk";

		// Token: 0x0400C722 RID: 50978
		public const string EXPANSION_PNG = ".png";

		// Token: 0x0400C723 RID: 50979
		public const string EXPANSION_JPG = ".jpg";

		// Token: 0x0400C724 RID: 50980
		public const string EXPANSION_YRP = ".yrp";

		// Token: 0x0400C725 RID: 50981
		public const string EXPANSION_YRP3D = ".yrp3d";

		// Token: 0x0400C726 RID: 50982
		public const string EXPANSION_LUA = ".lua";

		// Token: 0x0400C727 RID: 50983
		public const string EXPANSION_MP4 = ".mp4";

		// Token: 0x0400C728 RID: 50984
		public const string STRING_SLASH = "/";

		// Token: 0x0400C729 RID: 50985
		public const string STRING_LINE_BREAK = "\r\n";

		// Token: 0x0400C72A RID: 50986
		public static Program instance;

		// Token: 0x0400C72B RID: 50987
		public static Items items;

		// Token: 0x0400C72C RID: 50988
		private readonly List<Manager> managers = new List<Manager>();

		// Token: 0x0400C72D RID: 50989
		private readonly List<Servant> servants = new List<Servant>();

		// Token: 0x0400C72E RID: 50990
		public static bool exitOnReturn = false;

		// Token: 0x0400C72F RID: 50991
		public const string PATH_ROOT_EDITOR = "Platforms/";

		// Token: 0x0400C730 RID: 50992
		public const string PATH_ROOT_WINDOWS64 = "StandaloneWindows64/";

		// Token: 0x0400C731 RID: 50993
		public const string PATH_ROOT_LINUX = "StandaloneLinux64/";

		// Token: 0x0400C732 RID: 50994
		public const string PATH_ROOT_ANDROID = "Android/";

		// Token: 0x0400C733 RID: 50995
		public const string PATH_ROOT_IOS = "iOS/";

		// Token: 0x0400C734 RID: 50996
		public const string PATH_ROOT_MAC = "StandaloneOSX/";

		// Token: 0x0400C735 RID: 50997
		public const string PATH_TEMP_FOLDER = "TempFolder/";

		// Token: 0x0400C736 RID: 50998
		public static string root = "StandaloneWindows64/";

		// Token: 0x0400C737 RID: 50999
		private float m_TimeScale = 1f;

		// Token: 0x0400C738 RID: 51000
		public static bool noAccess = false;
	}
}
