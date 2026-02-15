using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.CardPack;
using YgomGame.Duel;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x0200093E RID: 2366
	public class ProductViewerWidget : ElementWidgetBase, ILoadingIconHandler
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<ProductViewerWidget.IThumbPlayer> thumbPlayers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004561 RID: 17761 RVA: 0x0000216D File Offset: 0x0000036D
		public bool enabledThumbAspectRatio
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004563 RID: 17763 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentIdx
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPlayingThumbRoll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06004565 RID: 17765 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004566 RID: 17766 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onChangeIdxEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06004567 RID: 17767 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004568 RID: 17768 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNextIdx(int baseIdx)
		{
			return 0;
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x000029CC File Offset: 0x00000BCC
		public int IndexOfContext(HighlightContext context)
		{
			return 0;
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x000029CC File Offset: 0x00000BCC
		public int IndexOfPlayContext(HighlightContext context)
		{
			return 0;
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductViewerWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IReadOnlyList<ProductViewerWidget.IThumbPlayer> thumbPlayers, int previewMateId, bool isSpProduct, ProductContext productCtx)
		{
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayThumbRoll(int startIdx)
		{
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayThumbOnce(int startIdx, int dstIdx = -1)
		{
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayCardThumbRoll()
		{
			return null;
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopThumbRoll()
		{
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetThumb(int idx)
		{
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayMateMotion(AvatarMotionSetting.MotionID motionId)
		{
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLoadingCoverVisible(HighlightContext context)
		{
		}

		// Token: 0x0400835D RID: 33629
		public readonly ProductViewerWidget.ThumbWidget thumbMain;

		// Token: 0x0400835E RID: 33630
		public readonly ProductViewerWidget.ThumbWidget thumbSub;

		// Token: 0x0400835F RID: 33631
		public readonly RawImage summonRawImage;

		// Token: 0x04008360 RID: 33632
		public readonly GameObject infoRoot;

		// Token: 0x04008361 RID: 33633
		public readonly GameObject limitGroup;

		// Token: 0x04008362 RID: 33634
		public readonly TMP_Text limitText;

		// Token: 0x04008363 RID: 33635
		public readonly GameObject limitDateGroup;

		// Token: 0x04008364 RID: 33636
		public readonly TMP_Text limitDateText;

		// Token: 0x04008365 RID: 33637
		public readonly GameObject numGroup;

		// Token: 0x04008366 RID: 33638
		public readonly TMP_Text numText;

		// Token: 0x04008367 RID: 33639
		public readonly CardPackChartWidget chartWidget;

		// Token: 0x04008368 RID: 33640
		public readonly GameObject packPickupMessageGroup;

		// Token: 0x04008369 RID: 33641
		public readonly TMP_Text packPickupMessage;

		// Token: 0x0400836A RID: 33642
		public readonly GameObject loadingCoverRoot;

		// Token: 0x0400836B RID: 33643
		private Coroutine m_ThumbRollRoutine;

		// Token: 0x0400836C RID: 33644
		private int m_CurrentIdx;

		// Token: 0x0400836D RID: 33645
		private int m_DstIdx;

		// Token: 0x0400836E RID: 33646
		public int loopStartIdx;

		// Token: 0x0400836F RID: 33647
		public int loopBreakIdx;

		// Token: 0x04008370 RID: 33648
		private IReadOnlyList<ProductViewerWidget.IThumbPlayer> m_ThumbPlayers;

		// Token: 0x04008371 RID: 33649
		public bool reverseFrag;

		// Token: 0x04008372 RID: 33650
		private ProductContext m_ProductCtx;

		// Token: 0x0200093F RID: 2367
		public class ThumbWidget : ElementWidgetBase
		{
			// Token: 0x06004576 RID: 17782 RVA: 0x0000216D File Offset: 0x0000036D
			public void ForceFinishTween()
			{
			}

			// Token: 0x06004577 RID: 17783 RVA: 0x0000216D File Offset: 0x0000036D
			public override void Clear()
			{
			}

			// Token: 0x06004578 RID: 17784 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsExistsThumb()
			{
				return false;
			}

			// Token: 0x06004579 RID: 17785 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsExistsSummon()
			{
				return false;
			}

			// Token: 0x0600457A RID: 17786 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isReady()
			{
				return false;
			}

			// Token: 0x0600457B RID: 17787 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public ThumbWidget(ElementObjectManager eom, bool isMain)
				: base(null)
			{
			}

			// Token: 0x0600457C RID: 17788 RVA: 0x0000216D File Offset: 0x0000036D
			public void SwitchDXIconOverriderGroup(bool isExistsLimitDate)
			{
			}

			// Token: 0x0600457D RID: 17789 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActivateByContext(HighlightContext context)
			{
			}

			// Token: 0x0600457E RID: 17790 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActivateToMate()
			{
			}

			// Token: 0x0600457F RID: 17791 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActivateToSummon()
			{
			}

			// Token: 0x06004580 RID: 17792 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActivateToThumb()
			{
			}

			// Token: 0x06004581 RID: 17793 RVA: 0x0000216A File Offset: 0x0000036A
			public IAsyncProgressContent ApplyContext(HighlightContext highlightContext)
			{
				return null;
			}

			// Token: 0x06004582 RID: 17794 RVA: 0x0000216D File Offset: 0x0000036D
			public void CaptureTo(ProductViewerWidget.ThumbWidget target)
			{
			}

			// Token: 0x04008373 RID: 33651
			private const string k_GLabel_DXIcon_Default = "DXIcon_Default";

			// Token: 0x04008374 RID: 33652
			private const string k_GLabel_DXIcon_UpperLimitDate = "DXIcon_UpperLimitDate";

			// Token: 0x04008375 RID: 33653
			public readonly BindingShopProductThumb bindingShopThumb;

			// Token: 0x04008376 RID: 33654
			public readonly BindingMateRenderTexture mateBinder;

			// Token: 0x04008377 RID: 33655
			public readonly RawImage summonRawImage;

			// Token: 0x04008378 RID: 33656
			public readonly SelectionButton mateButton;

			// Token: 0x04008379 RID: 33657
			public readonly GameObject dxIconRoot;

			// Token: 0x0400837A RID: 33658
			public bool isSpProduct;

			// Token: 0x0400837B RID: 33659
			public BindingGameObjectEx dxIconBinding;
		}

		// Token: 0x02000940 RID: 2368
		public interface IThumbPlayer
		{
			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06004583 RID: 17795
			bool enabled { get; }

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06004584 RID: 17796
			IAsyncProgressContent asyncProgressContent { get; }

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06004585 RID: 17797
			HighlightContext context { get; }

			// Token: 0x06004586 RID: 17798
			IEnumerator yPlay(ProductViewerWidget.ThumbWidget thumbWidgetMain, ProductViewerWidget.ThumbWidget thumbWidgetSub);

			// Token: 0x06004587 RID: 17799
			void SetImmediate(ProductViewerWidget.ThumbWidget thumbWidgetMain, ProductViewerWidget.ThumbWidget thumbWidgetSub);
		}

		// Token: 0x02000941 RID: 2369
		public class ThumbPlayer
		{
			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06004588 RID: 17800 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool YgomGame_002EShop_002EProductViewerWidget_002EIThumbPlayer_002Eenabled
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06004589 RID: 17801 RVA: 0x0000216A File Offset: 0x0000036A
			public IAsyncProgressContent asyncProgressContent
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x0600458A RID: 17802 RVA: 0x0000216A File Offset: 0x0000036A
			public HighlightContext context
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600458B RID: 17803 RVA: 0x00002739 File Offset: 0x00000939
			public ThumbPlayer(HighlightContext context)
			{
			}

			// Token: 0x0600458C RID: 17804 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator yPlay(ProductViewerWidget.ThumbWidget thumbWidgetMain, ProductViewerWidget.ThumbWidget thumbWidgetSub)
			{
				return null;
			}

			// Token: 0x0600458D RID: 17805 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetImmediate(ProductViewerWidget.ThumbWidget thumbWidgetMain, ProductViewerWidget.ThumbWidget thumbWidgetSub)
			{
			}

			// Token: 0x0400837C RID: 33660
			private readonly string k_TweenFadeInThumb;

			// Token: 0x0400837D RID: 33661
			private readonly string k_TweenRollWaitThumb;

			// Token: 0x0400837E RID: 33662
			private readonly string k_TweenRollSwapThumb;

			// Token: 0x0400837F RID: 33663
			private readonly HighlightContext m_Context;

			// Token: 0x04008380 RID: 33664
			public bool enabled;

			// Token: 0x04008381 RID: 33665
			private AsyncContainContent m_AsyncContainContent;
		}

		// Token: 0x02000942 RID: 2370
		public class SummonPlayer
		{
			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x0600458E RID: 17806 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool YgomGame_002EShop_002EProductViewerWidget_002EIThumbPlayer_002Eenabled
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x0600458F RID: 17807 RVA: 0x0000216A File Offset: 0x0000036A
			public IAsyncProgressContent asyncProgressContent
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06004590 RID: 17808 RVA: 0x0000216A File Offset: 0x0000036A
			public HighlightContext context
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004591 RID: 17809 RVA: 0x00002739 File Offset: 0x00000939
			public SummonPlayer(ShopPreviewContainer previewContainer, HighlightContext context)
			{
			}

			// Token: 0x06004592 RID: 17810 RVA: 0x0000216A File Offset: 0x0000036A
			public IEnumerator yPlay(ProductViewerWidget.ThumbWidget thumbWidgetMain, ProductViewerWidget.ThumbWidget thumbWidgetSub)
			{
				return null;
			}

			// Token: 0x06004593 RID: 17811 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetImmediate(ProductViewerWidget.ThumbWidget thumbWidgetMain, ProductViewerWidget.ThumbWidget thumbWidgetSub)
			{
			}

			// Token: 0x04008382 RID: 33666
			private readonly ShopPreviewContainer m_PreviewContainer;

			// Token: 0x04008383 RID: 33667
			private readonly HighlightContext m_Context;

			// Token: 0x04008384 RID: 33668
			public bool enabled;

			// Token: 0x04008385 RID: 33669
			public string bgThumbPath;

			// Token: 0x04008386 RID: 33670
			private AsyncProgressLoadingCountContent m_AsyncLoadingCountContent;
		}
	}
}
