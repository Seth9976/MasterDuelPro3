using System;
using System.Collections.Generic;
using MDPro3.Net;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013F2 RID: 5106
	public class WatchListHandler : MonoBehaviour
	{
		// Token: 0x06009410 RID: 37904 RVA: 0x001512AC File Offset: 0x0014F4AC
		private void OnEnable()
		{
			this.Print();
		}

		// Token: 0x06009411 RID: 37905 RVA: 0x001512B4 File Offset: 0x0014F4B4
		private void OnDisable()
		{
			this.Clear();
		}

		// Token: 0x06009412 RID: 37906 RVA: 0x001512BC File Offset: 0x0014F4BC
		private List<string[]> GetSearchedTasks()
		{
			List<string[]> returnValue = new List<string[]>();
			foreach (string[] task in this.tasks)
			{
				if (task[2].Contains(this.inputSearch.text) || task[3].Contains(this.inputSearch.text))
				{
					returnValue.Add(task);
				}
			}
			return returnValue;
		}

		// Token: 0x06009413 RID: 37907 RVA: 0x00151340 File Offset: 0x0014F540
		public void SetRooms(List<MyCardRoom> rooms)
		{
			this.rooms = rooms;
			this.tasks.Clear();
			foreach (MyCardRoom room in rooms)
			{
				string[] task = new string[]
				{
					room.id,
					room.title,
					room.users[0].username,
					room.users[1].username,
					room.arena,
					room.options.lflist.ToString(),
					room.options.rule.ToString(),
					room.options.mode.ToString(),
					room.options.duel_rule.ToString(),
					room.options.no_check_deck ? "T" : "F",
					room.options.no_shuffle_deck ? "T" : "F",
					room.options.start_lp.ToString(),
					room.options.start_hand.ToString(),
					room.options.draw_count.ToString(),
					room.options.time_limit.ToString(),
					room.options.no_watch ? "T" : "F",
					room.options.auto_death ? "T" : "F",
					room.options.replay_mode.ToString()
				};
				this.tasks.Add(task);
			}
			this.Print();
		}

		// Token: 0x06009414 RID: 37908 RVA: 0x00151524 File Offset: 0x0014F724
		public void CreateRoom(MyCardRoom room)
		{
			string[] task = new string[]
			{
				room.id,
				room.title,
				room.users[0].username,
				room.users[1].username,
				room.arena,
				room.options.lflist.ToString(),
				room.options.rule.ToString(),
				room.options.mode.ToString(),
				room.options.duel_rule.ToString(),
				room.options.no_check_deck ? "T" : "F",
				room.options.no_shuffle_deck ? "T" : "F",
				room.options.start_lp.ToString(),
				room.options.start_hand.ToString(),
				room.options.draw_count.ToString(),
				room.options.time_limit.ToString(),
				room.options.no_watch ? "T" : "F",
				room.options.auto_death ? "T" : "F",
				room.options.replay_mode.ToString()
			};
			this.tasks.Add(task);
			if (base.gameObject.activeInHierarchy)
			{
				this.superScrollView.Add(task);
			}
		}

		// Token: 0x06009415 RID: 37909 RVA: 0x001516C0 File Offset: 0x0014F8C0
		public void UpdateRoom(MyCardRoom room)
		{
			string[] task = new string[]
			{
				room.id,
				room.title,
				room.users[0].username,
				room.users[1].username,
				room.arena,
				room.options.lflist.ToString(),
				room.options.rule.ToString(),
				room.options.mode.ToString(),
				room.options.duel_rule.ToString(),
				room.options.no_check_deck ? "T" : "F",
				room.options.no_shuffle_deck ? "T" : "F",
				room.options.start_lp.ToString(),
				room.options.start_hand.ToString(),
				room.options.draw_count.ToString(),
				room.options.time_limit.ToString(),
				room.options.no_watch ? "T" : "F",
				room.options.auto_death ? "T" : "F",
				room.options.replay_mode.ToString()
			};
			int i = 0;
			while (i < this.tasks.Count)
			{
				if (this.tasks[i][0] == task[0])
				{
					for (int j = 1; j < task.Length; j++)
					{
						this.tasks[i][j] = task[j];
					}
					if (base.gameObject.activeInHierarchy)
					{
						this.superScrollView.UpdateAt(i, task);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06009416 RID: 37910 RVA: 0x0015189C File Offset: 0x0014FA9C
		public void DeleteRoom(string roomId)
		{
			int i = 0;
			while (i < this.rooms.Count)
			{
				if (this.rooms[i].id == roomId)
				{
					this.rooms.Remove(this.rooms[i]);
					if (base.gameObject.activeInHierarchy)
					{
						this.superScrollView.RemoveAt(i);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06009417 RID: 37911 RVA: 0x0015190C File Offset: 0x0014FB0C
		public void Print()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemWatch.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 370f : 300f);
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 178f : 140f);
				float topPadding = (PropertyOverrider.NeedMobileLayout() ? 18f : 14f);
				float bottomPadding = (PropertyOverrider.NeedMobileLayout() ? 64f : 54f);
				this.superScrollView = new SuperScrollView(-1, itemWidth, itemHeight, topPadding, bottomPadding, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.scrollRect, 2);
				this.superScrollView.Print(this.GetSearchedTasks());
				if (this.superScrollView.gameObjects.Count > 0)
				{
					Program.instance.online.lastSelectedWatchItem = this.superScrollView.gameObjects[0].GetComponent<SelectionToggle_Watch>();
				}
			};
		}

		// Token: 0x06009418 RID: 37912 RVA: 0x00151958 File Offset: 0x0014FB58
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Watch component = item.GetComponent<SelectionToggle_Watch>();
			component.roomId = task[0];
			component.roomTitile = task[1];
			component.player0Name = task[2];
			component.player1Name = task[3];
			component.arena = task[4];
			component.options.lflist = int.Parse(task[5]);
			component.options.rule = int.Parse(task[6]);
			component.options.mode = int.Parse(task[7]);
			component.options.duel_rule = int.Parse(task[8]);
			component.options.no_check_deck = task[9] == "T";
			component.options.no_shuffle_deck = task[10] == "T";
			component.options.start_lp = int.Parse(task[11]);
			component.options.start_hand = int.Parse(task[12]);
			component.options.draw_count = int.Parse(task[13]);
			component.options.time_limit = int.Parse(task[14]);
			component.options.no_watch = task[15] == "T";
			component.options.auto_death = task[16] == "T";
			component.options.replay_mode = int.Parse(task[17]);
			component.Refresh();
		}

		// Token: 0x06009419 RID: 37913 RVA: 0x00151AB1 File Offset: 0x0014FCB1
		public void Clear()
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView == null)
			{
				return;
			}
			superScrollView.Clear();
		}

		// Token: 0x0400D26A RID: 53866
		public ScrollRect scrollRect;

		// Token: 0x0400D26B RID: 53867
		public TMP_InputField inputSearch;

		// Token: 0x0400D26C RID: 53868
		public SuperScrollView superScrollView;

		// Token: 0x0400D26D RID: 53869
		private List<MyCardRoom> rooms;

		// Token: 0x0400D26E RID: 53870
		private List<string[]> tasks = new List<string[]>();
	}
}
