using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006E9 RID: 1769
	public class Resource
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x0000216A File Offset: 0x0000036A
		public List<Resource.HandlerData> CompleteHandlerList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600371A RID: 14106 RVA: 0x0000216D File Offset: 0x0000036D
		public Resource.Type ResType
		{
			[CompilerGenerated]
			get
			{
				return Resource.Type.None;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600371C RID: 14108 RVA: 0x0000216D File Offset: 0x0000036D
		public int RefCount
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600371E RID: 14110 RVA: 0x0000216D File Offset: 0x0000036D
		public global::UnityEngine.Object[] Assets
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

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003720 RID: 14112 RVA: 0x0000216D File Offset: 0x0000036D
		public byte[] Bytes
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003722 RID: 14114 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Cancel
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Error
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Done
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Busy
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600372A RID: 14122 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Retry
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600372C RID: 14124 RVA: 0x0000216D File Offset: 0x0000036D
		public global::System.Type SystemType
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x0000216D File Offset: 0x0000036D
		public string Path
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x0000216D File Offset: 0x0000036D
		public string LoadPath
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x0000216D File Offset: 0x0000036D
		public bool DisableErrorNotify
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x0000216D File Offset: 0x0000036D
		public ResourceManager.ReqType queueId
		{
			[CompilerGenerated]
			get
			{
				return ResourceManager.ReqType.Sound;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003736 RID: 14134 RVA: 0x0000216D File Offset: 0x0000036D
		public bool needMaterialRebuild
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

		// Token: 0x06003737 RID: 14135 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddHandler(string path, ResourceManager.RequestCompleteHandler handler)
		{
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0000216D File Offset: 0x0000036D
		public void CallHandler()
		{
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearHandler()
		{
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddCancelHandler(Resource.CancelHandler handler)
		{
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveCancelHandler(Resource.CancelHandler handler)
		{
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0000216D File Offset: 0x0000036D
		public void CallCancel()
		{
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddUnloadHandler(Resource.UnloadHandler handler)
		{
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveUnloadHandler(Resource.UnloadHandler handler)
		{
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x0000216D File Offset: 0x0000036D
		public void CallUnload()
		{
		}

		// Token: 0x0400314E RID: 12622
		private List<Resource.HandlerData> handlerList;

		// Token: 0x0400314F RID: 12623
		private Resource.CancelHandler cancelHandler;

		// Token: 0x04003150 RID: 12624
		private Resource.UnloadHandler unloadHandler;

		// Token: 0x020006EA RID: 1770
		public enum Type
		{
			// Token: 0x04003152 RID: 12626
			None,
			// Token: 0x04003153 RID: 12627
			BuiltIn,
			// Token: 0x04003154 RID: 12628
			AssetBundle,
			// Token: 0x04003155 RID: 12629
			Binary,
			// Token: 0x04003156 RID: 12630
			Network,
			// Token: 0x04003157 RID: 12631
			StreamingAssets,
			// Token: 0x04003158 RID: 12632
			StreamingBinary,
			// Token: 0x04003159 RID: 12633
			LocalFile,
			// Token: 0x0400315A RID: 12634
			StreaminFile,
			// Token: 0x0400315B RID: 12635
			PlayAssetDelivery
		}

		// Token: 0x020006EB RID: 1771
		public struct HandlerData
		{
			// Token: 0x06003741 RID: 14145 RVA: 0x0000216D File Offset: 0x0000036D
			public void Call()
			{
			}

			// Token: 0x0400315C RID: 12636
			public string path;

			// Token: 0x0400315D RID: 12637
			public ResourceManager.RequestCompleteHandler handler;
		}

		// Token: 0x020006EC RID: 1772
		// (Invoke) Token: 0x06003743 RID: 14147
		public delegate void CancelHandler(Resource res);

		// Token: 0x020006ED RID: 1773
		// (Invoke) Token: 0x06003747 RID: 14151
		public delegate void UnloadHandler(Resource res);
	}
}
