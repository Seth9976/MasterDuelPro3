using System;
using System.Collections.Generic;
using System.IO;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001479 RID: 5241
	public class PuzzleSelectorUI : ServantUI
	{
		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x0600985E RID: 39006 RVA: 0x00166E04 File Offset: 0x00165004
		public SelectionButton ButtonPlay
		{
			get
			{
				return this.m_ButtonPlay = ((this.m_ButtonPlay != null) ? this.m_ButtonPlay : base.Manager.GetElement<SelectionButton>("ButtonPlay"));
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x0600985F RID: 39007 RVA: 0x00166E40 File Offset: 0x00165040
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06009860 RID: 39008 RVA: 0x00166E7C File Offset: 0x0016507C
		private TextMeshProUGUI TextOverview
		{
			get
			{
				return this.m_TextOverview = ((this.m_TextOverview != null) ? this.m_TextOverview : base.Manager.GetElement<TextMeshProUGUI>("TextOverview"));
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06009861 RID: 39009 RVA: 0x00166EB8 File Offset: 0x001650B8
		public ArtRawImageHandler Art
		{
			get
			{
				return this.m_Art = ((this.m_Art != null) ? this.m_Art : base.Manager.GetElement<ArtRawImageHandler>("ArtImage"));
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06009862 RID: 39010 RVA: 0x00166EF4 File Offset: 0x001650F4
		public Image ImageHover
		{
			get
			{
				return this.m_ImageHover = ((this.m_ImageHover != null) ? this.m_ImageHover : base.Manager.GetElement<Image>("ButtonHover"));
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06009863 RID: 39011 RVA: 0x00166F30 File Offset: 0x00165130
		public Image ImageOut
		{
			get
			{
				return this.m_ImageOut = ((this.m_ImageOut != null) ? this.m_ImageOut : base.Manager.GetElement<Image>("ButtonOut"));
			}
		}

		// Token: 0x06009864 RID: 39012 RVA: 0x00166F6C File Offset: 0x0016516C
		private void Awake()
		{
			this.GetPuzzles();
			this.Print();
		}

		// Token: 0x06009865 RID: 39013 RVA: 0x00166F7C File Offset: 0x0016517C
		private void GetPuzzles()
		{
			this.puzzles = new List<PuzzleSelectorUI.Puzzle>();
			if (!Directory.Exists("Puzzle/"))
			{
				Directory.CreateDirectory("Puzzle/");
			}
			foreach (FileInfo fileInfo in new DirectoryInfo("Puzzle/").GetFiles("*.lua"))
			{
				string[] lines = File.ReadAllText(fileInfo.FullName).Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
				string card = "0";
				int messageStart = 0;
				int messageEnd = 0;
				int solutionStart = 0;
				int solutionEnd = 0;
				for (int i = 0; i < lines.Length; i++)
				{
					if (lines[i].StartsWith("Debug.AddCard(") && card == "0")
					{
						card = lines[i].Replace("Debug.AddCard(", "").Split(',', StringSplitOptions.None)[0];
					}
					else if (lines[i].StartsWith("--[[message"))
					{
						messageStart = i + 1;
					}
					else if (lines[i].StartsWith("Solution:"))
					{
						solutionStart = i;
					}
					else if (lines[i].StartsWith("]]"))
					{
						if (messageEnd == 0)
						{
							messageEnd = i;
						}
						else
						{
							solutionEnd = i;
						}
					}
				}
				string description = "";
				string solution = "";
				if (messageStart != 0 && messageEnd != 0)
				{
					for (int j = messageStart; j < messageEnd; j++)
					{
						description = description + lines[j] + "\r\n";
					}
				}
				if (solutionStart != 0 && solutionEnd != 0)
				{
					for (int k = solutionStart; k < solutionEnd; k++)
					{
						solution = solution + lines[k] + "\r\n";
					}
				}
				description = description.Replace("\r\n\t\r\n\t", "\r\n\t");
				PuzzleSelectorUI.Puzzle puzzle = new PuzzleSelectorUI.Puzzle
				{
					name = fileInfo.Name.Replace(".lua", string.Empty),
					firstCard = card,
					description = description,
					solution = solution
				};
				this.puzzles.Add(puzzle);
			}
		}

		// Token: 0x06009866 RID: 39014 RVA: 0x00167188 File Offset: 0x00165388
		public void Print()
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			List<string[]> tasks = new List<string[]>();
			for (int i = 0; i < this.puzzles.Count; i++)
			{
				string[] task = new string[]
				{
					i.ToString(),
					this.puzzles[i].name,
					this.puzzles[i].firstCard,
					this.puzzles[i].description,
					this.puzzles[i].solution
				};
				tasks.Add(task);
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemPuzzle.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 180f : 150f);
				float topPadding = (PropertyOverrider.NeedMobileLayout() ? 148f : 134f);
				float space = itemHeight - (PropertyOverrider.NeedMobileLayout() ? 152f : 122f);
				float bottomPadding = (PropertyOverrider.NeedMobileLayout() ? 64f : 54f) - space;
				this.superScrollView = new SuperScrollView(1, 700f, itemHeight, topPadding, bottomPadding, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRect, 2);
				this.superScrollView.Print(tasks);
				this.SelectFirst();
			};
		}

		// Token: 0x06009867 RID: 39015 RVA: 0x0016725F File Offset: 0x0016545F
		private void SelectFirst()
		{
			if (this.superScrollView == null || this.superScrollView.gameObjects.Count == 0)
			{
				return;
			}
			this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_Puzzle>().SetToggleOn(true);
		}

		// Token: 0x06009868 RID: 39016 RVA: 0x001672A0 File Offset: 0x001654A0
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Puzzle component = item.GetComponent<SelectionToggle_Puzzle>();
			PuzzleSelectorUI.Puzzle puzzle = new PuzzleSelectorUI.Puzzle
			{
				name = task[1],
				firstCard = task[2],
				description = task[3],
				solution = task[4]
			};
			component.puzzle = puzzle;
			component.Refresh();
		}

		// Token: 0x06009869 RID: 39017 RVA: 0x001672F1 File Offset: 0x001654F1
		public void SetOverview(string overview)
		{
			this.TextOverview.text = overview;
			this.TextOverview.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}

		// Token: 0x0600986A RID: 39018 RVA: 0x00167314 File Offset: 0x00165514
		public void OnPlayPuzzle()
		{
			Program.instance.puzzle.StartCurrentPuzzle();
		}

		// Token: 0x0600986B RID: 39019 RVA: 0x00167325 File Offset: 0x00165525
		public void SelectLastPuzzleItem()
		{
			UserInput.NextSelectionIsAxis = true;
			Program.instance.puzzle.lastPuzzleItem.GetSelectable().Select();
		}

		// Token: 0x0400D67C RID: 54908
		private const string LABEL_SBN_PLAY = "ButtonPlay";

		// Token: 0x0400D67D RID: 54909
		private SelectionButton m_ButtonPlay;

		// Token: 0x0400D67E RID: 54910
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D67F RID: 54911
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D680 RID: 54912
		private const string LABEL_TXT_OVERVIEW = "TextOverview";

		// Token: 0x0400D681 RID: 54913
		private TextMeshProUGUI m_TextOverview;

		// Token: 0x0400D682 RID: 54914
		private const string LABEL_MONO_ART = "ArtImage";

		// Token: 0x0400D683 RID: 54915
		private ArtRawImageHandler m_Art;

		// Token: 0x0400D684 RID: 54916
		private const string LABEL_IMG_HOVER = "ButtonHover";

		// Token: 0x0400D685 RID: 54917
		private Image m_ImageHover;

		// Token: 0x0400D686 RID: 54918
		private const string LABEL_IMG_OUT = "ButtonOut";

		// Token: 0x0400D687 RID: 54919
		private Image m_ImageOut;

		// Token: 0x0400D688 RID: 54920
		private List<PuzzleSelectorUI.Puzzle> puzzles;

		// Token: 0x0400D689 RID: 54921
		public SuperScrollView superScrollView;

		// Token: 0x0200147A RID: 5242
		public struct Puzzle
		{
			// Token: 0x0400D68A RID: 54922
			public string name;

			// Token: 0x0400D68B RID: 54923
			public string firstCard;

			// Token: 0x0400D68C RID: 54924
			public string description;

			// Token: 0x0400D68D RID: 54925
			public string solution;
		}
	}
}
