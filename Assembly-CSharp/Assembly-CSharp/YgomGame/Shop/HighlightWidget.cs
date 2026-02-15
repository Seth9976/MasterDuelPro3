using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000925 RID: 2341
	public class HighlightWidget : ElementWidgetBase
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton prevButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton nextButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool existPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004448 RID: 17480 RVA: 0x0000216D File Offset: 0x0000036D
		public bool shortcutIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06004449 RID: 17481 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600444A RID: 17482 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<HighlightWidget.IThumbWidget> onClickThumbEvent
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

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600444B RID: 17483 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600444C RID: 17484 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<HighlightWidget.IThumbWidget> onClickPlayEvent
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

		// Token: 0x0600444D RID: 17485 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public HighlightWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetContexts(IReadOnlyList<HighlightContext> contexts, ProductContext productCtx, ShopSettings shopSettings)
		{
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateButtons()
		{
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayIndicatorsTween()
		{
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0000216A File Offset: 0x0000036A
		private HighlightWidget.PageWidget CreatePageWidget(ElementObjectManager template)
		{
			return null;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateIndicatorChild(GameObject template)
		{
			return null;
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectItem(int idx, bool initializeSelection = false)
		{
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x0000216D File Offset: 0x0000036D
		private void MovePage(int dstPage)
		{
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yMovePage(int dstPage, Action onComplete)
		{
			return null;
		}

		// Token: 0x040082FC RID: 33532
		private readonly string k_ELabelPageTemplate;

		// Token: 0x040082FD RID: 33533
		private readonly string k_IndicatorChildTemplate;

		// Token: 0x040082FE RID: 33534
		private readonly string k_PrevButton;

		// Token: 0x040082FF RID: 33535
		private readonly string k_NextButton;

		// Token: 0x04008300 RID: 33536
		private readonly string k_PrevShortcutIcon;

		// Token: 0x04008301 RID: 33537
		private readonly string k_NextShortcutIcon;

		// Token: 0x04008302 RID: 33538
		private readonly int k_PageChildAmountMax;

		// Token: 0x04008303 RID: 33539
		private readonly ScrollRectPageSnap m_PageSnap;

		// Token: 0x04008304 RID: 33540
		private readonly SelectionButton m_PrevButton;

		// Token: 0x04008305 RID: 33541
		private readonly SelectionButton m_NextButton;

		// Token: 0x04008306 RID: 33542
		public readonly Selector selector;

		// Token: 0x04008307 RID: 33543
		public IReadOnlyList<HighlightContext> contexts;

		// Token: 0x04008308 RID: 33544
		public List<HighlightWidget.IThumbWidget> m_ThumbWidgets;

		// Token: 0x04008309 RID: 33545
		public Action onUpInputCallback;

		// Token: 0x02000926 RID: 2342
		public interface IThumbWidget
		{
			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x06004456 RID: 17494
			ShopDef.HighlightType highlightType { get; }

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x06004457 RID: 17495
			int page { get; }

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x06004458 RID: 17496
			int idx { get; }

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x06004459 RID: 17497
			bool isHead { get; }

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x0600445A RID: 17498
			int widthAmount { get; }

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x0600445B RID: 17499
			SelectionButton button { get; }

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x0600445C RID: 17500
			GameObject playRoot { get; }

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x0600445D RID: 17501
			SelectionButton playButton { get; }

			// Token: 0x14000051 RID: 81
			// (add) Token: 0x0600445E RID: 17502
			// (remove) Token: 0x0600445F RID: 17503
			event Action<HighlightWidget.IThumbWidget> onClickEvent;

			// Token: 0x14000052 RID: 82
			// (add) Token: 0x06004460 RID: 17504
			// (remove) Token: 0x06004461 RID: 17505
			event Action<HighlightWidget.IThumbWidget> onClickPlayEvent;
		}

		// Token: 0x02000927 RID: 2343
		public class PageWidget : ElementWidgetBase
		{
			// Token: 0x06004462 RID: 17506 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public PageWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x06004463 RID: 17507 RVA: 0x0000216A File Offset: 0x0000036A
			public HighlightWidget.IThumbWidget CreateThumb(HighlightContext context, int page, int idx, int shopId, ShopSettings shopSettings)
			{
				return null;
			}

			// Token: 0x06004464 RID: 17508 RVA: 0x0000216A File Offset: 0x0000036A
			private HighlightWidget.IThumbWidget CreateCardThumb(HighlightContext context, int page, int idx)
			{
				return null;
			}

			// Token: 0x06004465 RID: 17509 RVA: 0x0000216A File Offset: 0x0000036A
			private HighlightWidget.IThumbWidget CreateWideThumb(HighlightContext context, int page, int idx)
			{
				return null;
			}

			// Token: 0x0400830A RID: 33546
			private readonly string k_ELabelCardThumbTemplate;

			// Token: 0x0400830B RID: 33547
			private readonly string k_ELabelWideThumbTemplate;

			// Token: 0x0400830C RID: 33548
			private readonly ElementObjectManager m_CardThumbTemplate;

			// Token: 0x0400830D RID: 33549
			private readonly ElementObjectManager m_WideThumbTemplate;

			// Token: 0x02000928 RID: 2344
			public abstract class ThumbBase : ElementWidgetBase
			{
				// Token: 0x1700056E RID: 1390
				// (get) Token: 0x06004466 RID: 17510 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x06004467 RID: 17511 RVA: 0x0000216D File Offset: 0x0000036D
				public SelectionButton button
				{
					[CompilerGenerated]
					get
					{
						return null;
					}
					[CompilerGenerated]
					private set
					{
					}
				}

				// Token: 0x1700056F RID: 1391
				// (get) Token: 0x06004468 RID: 17512 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x06004469 RID: 17513 RVA: 0x0000216D File Offset: 0x0000036D
				public GameObject playRoot
				{
					[CompilerGenerated]
					get
					{
						return null;
					}
					[CompilerGenerated]
					private set
					{
					}
				}

				// Token: 0x17000570 RID: 1392
				// (get) Token: 0x0600446A RID: 17514 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x0600446B RID: 17515 RVA: 0x0000216D File Offset: 0x0000036D
				public SelectionButton playButton
				{
					[CompilerGenerated]
					get
					{
						return null;
					}
					[CompilerGenerated]
					private set
					{
					}
				}

				// Token: 0x17000571 RID: 1393
				// (get) Token: 0x0600446C RID: 17516
				public abstract int widthAmount { get; }

				// Token: 0x17000572 RID: 1394
				// (get) Token: 0x0600446D RID: 17517
				public abstract ShopDef.HighlightType highlightType { get; }

				// Token: 0x17000573 RID: 1395
				// (get) Token: 0x0600446E RID: 17518 RVA: 0x000029CC File Offset: 0x00000BCC
				public bool isHead
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17000574 RID: 1396
				// (get) Token: 0x0600446F RID: 17519 RVA: 0x000029CC File Offset: 0x00000BCC
				// (set) Token: 0x06004470 RID: 17520 RVA: 0x0000216D File Offset: 0x0000036D
				public bool interactable
				{
					get
					{
						return false;
					}
					set
					{
					}
				}

				// Token: 0x17000575 RID: 1397
				// (get) Token: 0x06004471 RID: 17521 RVA: 0x000029CC File Offset: 0x00000BCC
				private int YgomGame_002EShop_002EHighlightWidget_002EIThumbWidget_002Epage
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x17000576 RID: 1398
				// (get) Token: 0x06004472 RID: 17522 RVA: 0x000029CC File Offset: 0x00000BCC
				private int YgomGame_002EShop_002EHighlightWidget_002EIThumbWidget_002Eidx
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x14000053 RID: 83
				// (add) Token: 0x06004473 RID: 17523 RVA: 0x0000216D File Offset: 0x0000036D
				// (remove) Token: 0x06004474 RID: 17524 RVA: 0x0000216D File Offset: 0x0000036D
				public event Action<HighlightWidget.IThumbWidget> onClickEvent
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

				// Token: 0x14000054 RID: 84
				// (add) Token: 0x06004475 RID: 17525 RVA: 0x0000216D File Offset: 0x0000036D
				// (remove) Token: 0x06004476 RID: 17526 RVA: 0x0000216D File Offset: 0x0000036D
				public event Action<HighlightWidget.IThumbWidget> onClickPlayEvent
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

				// Token: 0x06004477 RID: 17527 RVA: 0x000F2C76 File Offset: 0x000F0E76
				public ThumbBase(ElementObjectManager eom, int page, int idx, HighlightContext context)
					: base(null)
				{
				}

				// Token: 0x06004478 RID: 17528 RVA: 0x0000216D File Offset: 0x0000036D
				private void OnClick()
				{
				}

				// Token: 0x06004479 RID: 17529 RVA: 0x0000216D File Offset: 0x0000036D
				private void OnClickPlay()
				{
				}

				// Token: 0x0400830E RID: 33550
				private readonly string k_ELabel_ThumbButton;

				// Token: 0x0400830F RID: 33551
				private readonly string k_ELabel_PlayRoot;

				// Token: 0x04008310 RID: 33552
				private readonly string k_ELabel_PlayButton;

				// Token: 0x04008311 RID: 33553
				public readonly int page;

				// Token: 0x04008312 RID: 33554
				public readonly int idx;

				// Token: 0x04008313 RID: 33555
				public BindingShopProductThumb bindingShopThumb;
			}

			// Token: 0x02000929 RID: 2345
			public class CardThumbWidget : HighlightWidget.PageWidget.ThumbBase
			{
				// Token: 0x17000577 RID: 1399
				// (get) Token: 0x0600447A RID: 17530 RVA: 0x000029CC File Offset: 0x00000BCC
				public override int widthAmount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x17000578 RID: 1400
				// (get) Token: 0x0600447B RID: 17531 RVA: 0x000029CC File Offset: 0x00000BCC
				public override ShopDef.HighlightType highlightType
				{
					get
					{
						return (ShopDef.HighlightType)0;
					}
				}

				// Token: 0x0600447C RID: 17532 RVA: 0x000F4824 File Offset: 0x000F2A24
				public CardThumbWidget(ElementObjectManager eom, int page, int idx, HighlightContext context)
					: base(null, 0, 0, null)
				{
				}

				// Token: 0x04008314 RID: 33556
				private readonly string k_ELabelCardImage;

				// Token: 0x04008315 RID: 33557
				private readonly string k_ELabelRarityIcon;

				// Token: 0x04008316 RID: 33558
				public readonly int mrk;
			}

			// Token: 0x0200092A RID: 2346
			public class WideThumbWidget : HighlightWidget.PageWidget.ThumbBase
			{
				// Token: 0x17000579 RID: 1401
				// (get) Token: 0x0600447D RID: 17533 RVA: 0x000029CC File Offset: 0x00000BCC
				public override int widthAmount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x1700057A RID: 1402
				// (get) Token: 0x0600447E RID: 17534 RVA: 0x000029CC File Offset: 0x00000BCC
				public override ShopDef.HighlightType highlightType
				{
					get
					{
						return (ShopDef.HighlightType)0;
					}
				}

				// Token: 0x0600447F RID: 17535 RVA: 0x000F4824 File Offset: 0x000F2A24
				public WideThumbWidget(ElementObjectManager eom, int page, int idx, HighlightContext context)
					: base(null, 0, 0, null)
				{
				}

				// Token: 0x04008317 RID: 33559
				private readonly string k_ELabelThumbImage;

				// Token: 0x04008318 RID: 33560
				public readonly string thumbPath;
			}
		}
	}
}
