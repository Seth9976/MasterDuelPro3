using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000E9A RID: 3738
	public class HandCardManager
	{
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006CAB RID: 27819 RVA: 0x0000216D File Offset: 0x0000036D
		public bool nearAllOpen
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06006CAC RID: 27820 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006CAD RID: 27821 RVA: 0x0000216D File Offset: 0x0000036D
		public bool farAllOpen
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06006CAE RID: 27822 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006CAF RID: 27823 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelClient host
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006CB1 RID: 27825 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
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

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006CB3 RID: 27827 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
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

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006CB5 RID: 27829 RVA: 0x0000216D File Offset: 0x0000036D
		public HandCardManager.DispMode nearHandDispMode
		{
			get
			{
				return HandCardManager.DispMode.Full;
			}
			set
			{
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006CB7 RID: 27831 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<HandCardManager.DispMode> onNearHandDispModeChanged
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x0000216A File Offset: 0x0000036A
		public static HandCardManager Create(DuelClient host)
		{
			return null;
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelClient host)
		{
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddNearHandCard(int mrk, int unique_id, int index = -1, bool is_hide = false, HandCardManager.ViewSortMode sortMode = HandCardManager.ViewSortMode.EngineIndex)
		{
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddFarHandCard(int mrk, int unique_id, int index = -1, bool is_hide = false, HandCardManager.ViewSortMode sortMode = HandCardManager.ViewSortMode.EngineIndex)
		{
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddHandCard(List<HandCardManager.HandInfo> info_list, int mrk, int unique_id, int index = -1, bool is_hide = false, HandCardManager.ViewSortMode sortMode = HandCardManager.ViewSortMode.EngineIndex)
		{
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetNearHandInfo(int index, int mrk)
		{
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFarHandInfo(int index, int mrk)
		{
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetHandCardInfo(List<HandCardManager.HandInfo> infoList, int index, int mrk)
		{
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveNearHandCard(int index)
		{
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveFarHandCard(int index)
		{
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveHandCard(List<HandCardManager.HandInfo> info_list, int index)
		{
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNearHandCardNum()
		{
			return 0;
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFarHandCardNum()
		{
			return 0;
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SyncNearHandInfo(int[] uniqueIdList)
		{
			return false;
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SyncFarHandInfo(int[] uniqueIdList)
		{
			return false;
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool SyncHandInfo(List<HandCardManager.HandInfo> info_list, int[] uniqueIdList)
		{
			return false;
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x0000216A File Offset: 0x0000036A
		public HandCardManager.HandInfo GetNearHandInfo(int index)
		{
			return null;
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x0000216A File Offset: 0x0000036A
		public HandCardManager.HandInfo GetFarHandInfo(int index)
		{
			return null;
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x0000216A File Offset: 0x0000036A
		private HandCardManager.HandInfo GetHandInfo(List<HandCardManager.HandInfo> info_list, int index)
		{
			return null;
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupNearViewIndex(HandCardManager.ViewSortMode sortMode)
		{
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupFarViewIndex(HandCardManager.ViewSortMode sortMode)
		{
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupViewIndex(List<HandCardManager.HandInfo> infoList, HandCardManager.ViewSortMode sortMode)
		{
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupViewIndexEngineIndex(List<HandCardManager.HandInfo> infoList)
		{
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupViewIndexRandom(List<HandCardManager.HandInfo> infoList)
		{
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupViewIndexCustom(List<HandCardManager.HandInfo> infoList)
		{
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertNearViewIndex(int targetIndex, int insertViewIndex)
		{
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertFarViewIndex(int targetIndex, int insertViewIndex)
		{
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertViewIndex(List<HandCardManager.HandInfo> infoList, int targetIndex, int insertViewIndex)
		{
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertNearViewIndexIfOpen()
		{
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertFarViewIndexIfOpen()
		{
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertViewIndexIfOpen(List<HandCardManager.HandInfo> infoList, int insertViewIndex)
		{
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNearViewIndex(int index)
		{
			return 0;
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFarViewIndex(int index)
		{
			return 0;
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetViewIndex(List<HandCardManager.HandInfo> infoList, int index)
		{
			return 0;
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNearIndexByViewIndex(int viewIndex)
		{
			return 0;
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFarIndexByViewIndex(int viewIndex)
		{
			return 0;
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetIndexByViewIndex(List<HandCardManager.HandInfo> infoList, int viewIndex)
		{
			return 0;
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNearIndexByUniqueID(int uniqueID)
		{
			return 0;
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFarIndexByUniqueID(int uniqueID)
		{
			return 0;
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetIndexByUniqueID(List<HandCardManager.HandInfo> infoList, int uniqueID)
		{
			return 0;
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetNearHide(int index, bool hide)
		{
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFarHide(int index, bool hide)
		{
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetHide(List<HandCardManager.HandInfo> infoList, int index, bool hide)
		{
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetNearHideAll(bool hide)
		{
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFarHideAll(bool hide)
		{
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetHideAll(List<HandCardManager.HandInfo> infoList, bool hide)
		{
		}

		// Token: 0x0400A7FD RID: 43005
		private List<HandCardManager.HandInfo> nearHandInfoList;

		// Token: 0x0400A7FE RID: 43006
		private List<HandCardManager.HandInfo> farHandInfoList;

		// Token: 0x0400A7FF RID: 43007
		public static DefinitionSetting handCardDefinision;

		// Token: 0x0400A800 RID: 43008
		private HandCardManager.DispMode _nearHandDispMode;

		// Token: 0x02000E9B RID: 3739
		public class HandInfo
		{
			// Token: 0x0400A801 RID: 43009
			public int m_Mrk;

			// Token: 0x0400A802 RID: 43010
			public int m_UniqueId;

			// Token: 0x0400A803 RID: 43011
			public bool m_IsHide;

			// Token: 0x0400A804 RID: 43012
			public int m_styleID;

			// Token: 0x0400A805 RID: 43013
			public int m_ViewIndex;
		}

		// Token: 0x02000E9C RID: 3740
		public enum ViewSortMode
		{
			// Token: 0x0400A807 RID: 43015
			EngineIndex,
			// Token: 0x0400A808 RID: 43016
			Random,
			// Token: 0x0400A809 RID: 43017
			Custom
		}

		// Token: 0x02000E9D RID: 3741
		public enum DispMode
		{
			// Token: 0x0400A80B RID: 43019
			Full,
			// Token: 0x0400A80C RID: 43020
			Small
		}
	}
}
