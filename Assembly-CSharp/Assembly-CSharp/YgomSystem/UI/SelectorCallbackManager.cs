using System;
using System.Collections.Generic;

namespace YgomSystem.UI
{
	// Token: 0x020005E1 RID: 1505
	public class SelectorCallbackManager
	{
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000029CC File Offset: 0x00000BCC
		public uint AddSelectedCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return 0U;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> ClearSelectedCallback(SelectionItem item)
		{
			return null;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> RemoveSelectedCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return null;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> RemoveSelectedCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog)
		{
			return null;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> RemoveCallback(uint packID)
		{
			return null;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000029CC File Offset: 0x00000BCC
		public uint AddShortcutCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return 0U;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> ClearShortcutCallback(SelectionItem item)
		{
			return null;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> RemoveShortcutCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return null;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> RemoveShortcutCallback(SelectionItem item, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog)
		{
			return null;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000029CC File Offset: 0x00000BCC
		public uint AddShortcutCallback(int priority, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return 0U;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0000216A File Offset: 0x0000036A
		public List<uint> RemoveShortcutCallback(int priority, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog, Func<bool> callback)
		{
			return null;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectorCallbackManager.CallbackInfo GetCallbackInfo(List<SelectorCallbackManager.CallbackInfo> targetList, SelectorManager.KeyStatus status, SelectorManager.KeyType main = SelectorManager.KeyType.None, SelectorManager.KeyType sub = SelectorManager.KeyType.None, SelectorManager.MouseType mouse = SelectorManager.MouseType.None, SelectorManager.AnalogType analog = SelectorManager.AnalogType.None)
		{
			return null;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000029CC File Offset: 0x00000BCC
		private uint IssueCallbackPackID()
		{
			return 0U;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool InvokeSelectedCallback(SelectionItem selectedItem, SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog)
		{
			return false;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool InvokeShortcutCallback(SelectorManager.KeyStatus status, SelectorManager.KeyType main, SelectorManager.KeyType sub, SelectorManager.MouseType mouse, SelectorManager.AnalogType analog)
		{
			return false;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x0000216D File Offset: 0x0000036D
		public void Cleanup()
		{
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveClusterPriority(int min, bool force)
		{
		}

		// Token: 0x04002CAD RID: 11437
		private List<SelectorCallbackManager.CallbackInfo> selectedCallbacks;

		// Token: 0x04002CAE RID: 11438
		private List<SelectorCallbackManager.CallbackInfo> shortcutCallbacks;

		// Token: 0x04002CAF RID: 11439
		private List<uint> packIDList;

		// Token: 0x04002CB0 RID: 11440
		private uint createCount;

		// Token: 0x04002CB1 RID: 11441
		private static List<uint> resultID;

		// Token: 0x04002CB2 RID: 11442
		private int activeClusterPriorityMin;

		// Token: 0x020005E2 RID: 1506
		private class CallbackInfo
		{
			// Token: 0x06003008 RID: 12296 RVA: 0x00002739 File Offset: 0x00000939
			public CallbackInfo(SelectorManager.KeyType main = SelectorManager.KeyType.None, SelectorManager.KeyType sub = SelectorManager.KeyType.None, SelectorManager.MouseType mouse = SelectorManager.MouseType.None, SelectorManager.AnalogType analog = SelectorManager.AnalogType.None, SelectorManager.KeyStatus status = SelectorManager.KeyStatus.OnPush)
			{
			}

			// Token: 0x06003009 RID: 12297 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Match(SelectorManager.KeyType main = SelectorManager.KeyType.None, SelectorManager.KeyType sub = SelectorManager.KeyType.None, SelectorManager.MouseType mouse = SelectorManager.MouseType.None, SelectorManager.AnalogType analog = SelectorManager.AnalogType.None, SelectorManager.KeyStatus status = SelectorManager.KeyStatus.OnPush)
			{
				return false;
			}

			// Token: 0x0600300A RID: 12298 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Equals(SelectorManager.KeyType main = SelectorManager.KeyType.None, SelectorManager.KeyType sub = SelectorManager.KeyType.None, SelectorManager.MouseType mouse = SelectorManager.MouseType.None, SelectorManager.AnalogType analog = SelectorManager.AnalogType.None, SelectorManager.KeyStatus status = SelectorManager.KeyStatus.OnPush)
			{
				return false;
			}

			// Token: 0x0600300B RID: 12299 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddCallback(uint id, SelectionItem item, Func<bool> callback)
			{
			}

			// Token: 0x0600300C RID: 12300 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddCallback(uint id, int priority, Func<bool> callback)
			{
			}

			// Token: 0x0600300D RID: 12301 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool InvokeCallback(int priorityMin, SelectionItem item = null)
			{
				return false;
			}

			// Token: 0x0600300E RID: 12302 RVA: 0x0000216A File Offset: 0x0000036A
			public List<uint> RemoveCallback(SelectionItem item)
			{
				return null;
			}

			// Token: 0x0600300F RID: 12303 RVA: 0x0000216A File Offset: 0x0000036A
			public List<uint> RemoveCallback(SelectionItem item, Func<bool> callback)
			{
				return null;
			}

			// Token: 0x06003010 RID: 12304 RVA: 0x0000216A File Offset: 0x0000036A
			public List<uint> RemoveCallback(int priority, Func<bool> callback)
			{
				return null;
			}

			// Token: 0x06003011 RID: 12305 RVA: 0x0000216A File Offset: 0x0000036A
			public List<uint> RemoveCallback(uint id)
			{
				return null;
			}

			// Token: 0x06003012 RID: 12306 RVA: 0x0000216A File Offset: 0x0000036A
			public List<uint> ClearCallback()
			{
				return null;
			}

			// Token: 0x06003013 RID: 12307 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool Exist(uint id)
			{
				return false;
			}

			// Token: 0x06003014 RID: 12308 RVA: 0x0000216D File Offset: 0x0000036D
			public void Cleanup()
			{
			}

			// Token: 0x06003015 RID: 12309 RVA: 0x0000216A File Offset: 0x0000036A
			public override string ToString()
			{
				return null;
			}

			// Token: 0x04002CB3 RID: 11443
			public SelectorManager.KeyType keyTypeMain;

			// Token: 0x04002CB4 RID: 11444
			public SelectorManager.KeyType keyTypeSub;

			// Token: 0x04002CB5 RID: 11445
			public SelectorManager.MouseType mouseType;

			// Token: 0x04002CB6 RID: 11446
			public SelectorManager.AnalogType analogType;

			// Token: 0x04002CB7 RID: 11447
			public SelectorManager.KeyStatus status;

			// Token: 0x04002CB8 RID: 11448
			public List<SelectorCallbackManager.CallbackPack> packs;

			// Token: 0x04002CB9 RID: 11449
			private static List<uint> resultID;
		}

		// Token: 0x020005E3 RID: 1507
		private class CallbackPack
		{
			// Token: 0x06003016 RID: 12310 RVA: 0x00002739 File Offset: 0x00000939
			public CallbackPack(uint id, SelectionItem item, Func<bool> callback)
			{
			}

			// Token: 0x06003017 RID: 12311 RVA: 0x00002739 File Offset: 0x00000939
			public CallbackPack(uint id, int priority, Func<bool> callback)
			{
			}

			// Token: 0x06003018 RID: 12312 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearCallback()
			{
			}

			// Token: 0x06003019 RID: 12313 RVA: 0x0000216A File Offset: 0x0000036A
			public override string ToString()
			{
				return null;
			}

			// Token: 0x04002CBA RID: 11450
			public uint id;

			// Token: 0x04002CBB RID: 11451
			public SelectionItem item;

			// Token: 0x04002CBC RID: 11452
			public Func<bool> callback;

			// Token: 0x04002CBD RID: 11453
			public int priority;
		}
	}
}
