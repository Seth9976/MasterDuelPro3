using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Bg;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D35 RID: 3381
	public class DuelClient : ViewController, IFadeSupported
	{
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06006223 RID: 25123 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006224 RID: 25124 RVA: 0x0000216D File Offset: 0x0000036D
		private List<Func<bool>> OnBackList
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

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006226 RID: 25126 RVA: 0x0000216D File Offset: 0x0000036D
		public EngineInitializer engineInitializer
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

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006228 RID: 25128 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelHUD duelHUD
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

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06006229 RID: 25129 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600622A RID: 25130 RVA: 0x0000216D File Offset: 0x0000036D
		public HandCardManager handCardManager
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

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600622B RID: 25131 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600622C RID: 25132 RVA: 0x0000216D File Offset: 0x0000036D
		public RunEffectWorker effectWorker
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

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600622D RID: 25133 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600622E RID: 25134 RVA: 0x0000216D File Offset: 0x0000036D
		public static DuelCursor cursor
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

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600622F RID: 25135 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006230 RID: 25136 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelClient.ActivateConfirmMode activateConfirmMode
		{
			[CompilerGenerated]
			get
			{
				return DuelClient.ActivateConfirmMode.Default;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06006232 RID: 25138 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006233 RID: 25139 RVA: 0x0000216D File Offset: 0x0000036D
		public bool pvpProgress
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

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06006234 RID: 25140 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006235 RID: 25141 RVA: 0x0000216D File Offset: 0x0000036D
		private DuelClient.Step step
		{
			get
			{
				return DuelClient.Step.InitLoadRes;
			}
			set
			{
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06006236 RID: 25142 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006237 RID: 25143 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelEndOperation duelEndOperation
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

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEngineInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06006239 RID: 25145 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600623A RID: 25146 RVA: 0x0000216D File Offset: 0x0000036D
		public int chapterId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600623B RID: 25147 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600623C RID: 25148 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isRentalDeck
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

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float initProgress
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x0600623E RID: 25150 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600623F RID: 25151 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action OnUpdate
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

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06006240 RID: 25152 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006241 RID: 25153 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<Engine.ViewType, int, int, int> onPreRunEffect
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

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06006242 RID: 25154 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006243 RID: 25155 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<Engine.ViewType, int, int, int> onPostRunEffect
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

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06006244 RID: 25156 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006245 RID: 25157 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onAudienceReplayFinished
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

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06006246 RID: 25158 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006247 RID: 25159 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onShowAffectDelegate onShowAffectHandler
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

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06006248 RID: 25160 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006249 RID: 25161 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onHideAffectDelegate onHideAffectHandler
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

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600624A RID: 25162 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600624B RID: 25163 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onTapDownFieldDelegate onTapDownFieldHandler
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

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600624C RID: 25164 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600624D RID: 25165 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onTapUpFieldDelegate onTapUpFieldHandler
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

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600624E RID: 25166 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600624F RID: 25167 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onTapEnterFieldDelegate onTapEnterFieldHandler
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

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06006250 RID: 25168 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006251 RID: 25169 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onTapExitFieldDelegate onTapExitFieldHandler
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

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06006252 RID: 25170 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006253 RID: 25171 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onCursorEnterFieldDelegate onCursorEnterFieldHandler
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

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06006254 RID: 25172 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006255 RID: 25173 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onCursorExitFieldDelegate onCursorExitFieldHandler
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

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06006256 RID: 25174 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006257 RID: 25175 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onSelectFieldDelegate onSelectFieldHandler
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

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06006258 RID: 25176 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006259 RID: 25177 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDeselectFieldDelegate onDeselectFieldHandler
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

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600625A RID: 25178 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600625B RID: 25179 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onFocusFieldDelegate onFocusFieldHandler
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

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600625C RID: 25180 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600625D RID: 25181 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onUnfocusFieldDelegate onUnfocusFieldHandler
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

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x0600625E RID: 25182 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600625F RID: 25183 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDecideFieldDelegate onDecideFieldHandler
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

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06006260 RID: 25184 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006261 RID: 25185 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDoubleClickFieldDelegate onDoubleClickFieldHandler
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

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06006262 RID: 25186 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006263 RID: 25187 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDragFieldBeginDelegate onDragFieldBeginHandler
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

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06006264 RID: 25188 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006265 RID: 25189 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDragFieldDelegate onDragFieldHandler
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

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06006266 RID: 25190 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006267 RID: 25191 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDragFieldEndDelegate onDragFieldEndHandler
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

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06006268 RID: 25192 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006269 RID: 25193 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onHoldFieldBeginDelegate onHoldFieldBeginHandler
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

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x0600626A RID: 25194 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600626B RID: 25195 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onDecideAttackTargetDelegate onDecideAttackTargetHandler
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

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x0600626C RID: 25196 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600626D RID: 25197 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onFieldViewChangedDelegate onFieldViewChangedHandler
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

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x0600626E RID: 25198 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600626F RID: 25199 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onChangeActivateConfirmMode onChangeActivateConfirmModeHandler
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

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06006270 RID: 25200 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006271 RID: 25201 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onChangeDetailShowing onChangeDetailShowingHandler
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

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06006272 RID: 25202 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006273 RID: 25203 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onFieldBackKey onFieldBackKeyHandler
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

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06006274 RID: 25204 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006275 RID: 25205 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onPlayScreenEffect onPlayScreenEffectHandler
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

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06006276 RID: 25206 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006277 RID: 25207 RVA: 0x0000216D File Offset: 0x0000036D
		public event DuelClient.onStopScreenEffect onStopScreenEffectHandler
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

		// Token: 0x06006278 RID: 25208 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetChildRoot(DuelClient.GUIPriority priority)
		{
			return null;
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFinishedOtherWorker<T>(Engine.ViewType viewType) where T : AbstractRunEffectWorker
		{
			return false;
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float Progress()
		{
			return 0f;
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int RunEffect(int id, int param1, int param2, int param3)
		{
			return 0;
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x000029CC File Offset: 0x00000BCC
		private int RunEffectImpl(int id, int param1, int param2, int param3)
		{
			return 0;
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int IsBusyEffect(int id)
		{
			return 0;
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x000029CC File Offset: 0x00000BCC
		private int IsBusyEffectImpl(int id)
		{
			return 0;
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddEffectHandler(Engine.ViewType type, DuelClient.EffectHandler eh)
		{
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveEffectHandler(Engine.ViewType type, DuelClient.EffectHandler eh)
		{
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddAudienceReplayEffectHandler(Engine.ViewType type, DuelClient.EffectHandler eh)
		{
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAudienceReplayEffectHandler(Engine.ViewType type, DuelClient.EffectHandler eh)
		{
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddBackEvent(Func<bool> func, bool first = false)
		{
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveBackEvent(Func<bool> func)
		{
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x000F5A34 File Offset: 0x000F3C34
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveInputBlocker(bool active)
		{
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeactiveInputBlocker(bool force)
		{
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveFieldInputBlocker(bool active)
		{
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupPvp()
		{
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishPvP()
		{
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x0000216D File Offset: 0x0000036D
		private void PvpNetworkCompleteHandler(PvP.Event ev, int code)
		{
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x0000216D File Offset: 0x0000036D
		private void PvpNetworkErrorHandler(PvP.Event ev, int code)
		{
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x0000216D File Offset: 0x0000036D
		private void PvpNetworkFatalErrorHandler(PvP.Event ev, int code)
		{
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x0000216D File Offset: 0x0000036D
		private void PvpProgress(bool enable)
		{
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReplayRealtime(bool replayQueued, bool mainQueued)
		{
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetPvpTimeout()
		{
			return false;
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReplayRealtime()
		{
			return false;
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetReplayRealtimeFlag()
		{
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UseMinimumEffect()
		{
			return false;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeShowAffect(int player, int position, int index)
		{
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeHideAffect()
		{
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeTapDownField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeTapUpField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeTapEnterField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeTapExitField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeCursorEnterField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeCursorExitField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeSelectField(SharedDefinition.Location location, int position, int viewIndex)
		{
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeSelectField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDeselectField(SharedDefinition.Location location, int position, int viewIndex)
		{
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDeselectField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeFocusField(SharedDefinition.Location location, int position, int viewIndex)
		{
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeFocusField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeUnfocusField(SharedDefinition.Location location, int position, int viewIndex)
		{
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeUnfocusField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDecideField(SharedDefinition.Location location, int position, int viewIndex)
		{
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDecideField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDoubleClickField(int player, int position, int viewIndex)
		{
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDragFieldBegin(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDragField(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDragFieldEnd(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeHoldFieldBegin(int player, int position, int viewIndex, Vector2 screenPoint)
		{
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeDecideAttackTarget(int attackerPlayer, int attackerPosition, int attackerIndex, int targetPlayer, int targetPosition, int targetIndex)
		{
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeFieldViewChanged(bool fieldViewing)
		{
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeChangeActivateConfirmModeCheck(DuelClient.ActivateConfirmMode mode)
		{
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeChangeDetailShowing(bool showing)
		{
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeFieldBackKey()
		{
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokePlayScreenEffect()
		{
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeStopScreenEffect()
		{
		}

		// Token: 0x060062BA RID: 25274 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetFieldCardFace(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFieldCardID(int player, int position, int index)
		{
			return 0;
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLP(int player)
		{
			return 0;
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DuelClient.DuelSpeed GetDuelSpeed()
		{
			return DuelClient.DuelSpeed.Normal;
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDuelSpeed(DuelClient.DuelSpeed duelSpeed)
		{
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetDuelDeltaTime()
		{
			return 0f;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetDuelTimeScale()
		{
			return 0f;
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0000216D File Offset: 0x0000036D
		public static void QuitReplay()
		{
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPauseReplay(bool pause)
		{
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x000F5A4C File Offset: 0x000F3C4C
		public static Vector3 PositionToScreenPoint(int player, int position, int index = 0)
		{
			return default(Vector3);
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x000F5A64 File Offset: 0x000F3C64
		public static Vector3 PositionToScreenLocalPoint(int player, int position)
		{
			return default(Vector3);
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x000F5A7C File Offset: 0x000F3C7C
		public static Vector3 PositionToScreenLocalPoint(int player, int position, int index)
		{
			return default(Vector3);
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x000F5A94 File Offset: 0x000F3C94
		public static Vector3 ScreenPointToLocalPointDuelClient(Vector3 pos)
		{
			return default(Vector3);
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x000F5AAC File Offset: 0x000F3CAC
		public static Vector3 MateToScreenLocalPoint(BgUnit.Side side)
		{
			return default(Vector3);
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x000F5AC4 File Offset: 0x000F3CC4
		public static Vector3 ScreenPointToDuelCameraWorldPosition(Vector3 screenPoint)
		{
			return default(Vector3);
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x000F5ADC File Offset: 0x000F3CDC
		public static Vector2 PositionToHighlightPoint(int player, int position)
		{
			return default(Vector2);
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x000F5AF4 File Offset: 0x000F3CF4
		public static ValueTuple<int, int, int> GetCardPosition(Vector2 screenPoint)
		{
			return default(ValueTuple<int, int, int>);
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDetailShowing()
		{
			return false;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float GetProgress(DuelClient.Step current, DuelClient.Step start, DuelClient.Step end)
		{
			return 0f;
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitEngineImpl()
		{
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x0000216D File Offset: 0x0000036D
		private void EvalEachSteps()
		{
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateInitStep()
		{
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateExecAndTermStep()
		{
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x0000216D File Offset: 0x0000036D
		private void BeginningStep()
		{
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitLoadResStep()
		{
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitLoadResStep()
		{
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeProcessStep()
		{
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishInitializeStep()
		{
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitConnectingStep()
		{
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitEngineStep()
		{
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitSoundStep()
		{
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitSoundStep()
		{
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitLoadSoundStep()
		{
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitLoadSoundStep()
		{
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitGameObjectInitStep()
		{
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x0000216D File Offset: 0x0000036D
		private void PrepareProcessStep()
		{
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishPrepareStep()
		{
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCameraWork()
		{
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowUpDuelStep()
		{
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitShowUpStep()
		{
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDuel()
		{
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecDuelStep()
		{
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsToMainQueueIsEmpty()
		{
			return false;
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndDuelStep()
		{
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelEndStep()
		{
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitTermStep()
		{
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitTermStep()
		{
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitEndNetworkStep()
		{
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x0000216D File Offset: 0x0000036D
		private void EndStep()
		{
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitDestroyStep()
		{
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x0000216D File Offset: 0x0000036D
		private void ConnectingErrorStep()
		{
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowErrorDialog(string msg, DuelClient.Step nextStep)
		{
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x0000216D File Offset: 0x0000036D
		private void DestroyStep()
		{
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseResources()
		{
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x0000216D File Offset: 0x0000036D
		private void WriteResultToSendWork()
		{
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplicationQuit()
		{
		}

		// Token: 0x04009D0A RID: 40202
		private static DuelClient instance;

		// Token: 0x04009D0B RID: 40203
		private Dictionary<Engine.ViewType, DuelClient.EffectHandler> effectTable;

		// Token: 0x04009D0C RID: 40204
		private Dictionary<Engine.ViewType, DuelClient.EffectHandler> audienceReplayEffectTable;

		// Token: 0x04009D0D RID: 40205
		private bool isSetLastBgmLabel;

		// Token: 0x04009D0E RID: 40206
		private bool isPlatformChecker;

		// Token: 0x04009D0F RID: 40207
		private DuelClient.DuelSpeed duelSpeed;

		// Token: 0x04009D10 RID: 40208
		private List<AbstractRunEffectWorker> workers;

		// Token: 0x04009D11 RID: 40209
		private Dictionary<DuelClient.GUIPriority, GameObject> childRoots;

		// Token: 0x04009D12 RID: 40210
		[SerializeField]
		private global::UnityEngine.Object duelHUDSrc;

		// Token: 0x04009D13 RID: 40211
		private GameObject inputBlocker;

		// Token: 0x04009D14 RID: 40212
		private int inputBlockCounter;

		// Token: 0x04009D15 RID: 40213
		private InputBlockerFlexible fieldInputBlocker;

		// Token: 0x04009D16 RID: 40214
		private string lastBgmLabel;

		// Token: 0x04009D17 RID: 40215
		private int pvpErrorCount;

		// Token: 0x04009D18 RID: 40216
		private bool pvpError;

		// Token: 0x04009D19 RID: 40217
		private bool pvpTimeout;

		// Token: 0x04009D1A RID: 40218
		private float replayTimeMargin;

		// Token: 0x04009D1B RID: 40219
		private bool replayRealtime;

		// Token: 0x04009D1C RID: 40220
		private bool quitReplay;

		// Token: 0x04009D1D RID: 40221
		private bool pauseReplay;

		// Token: 0x04009D1E RID: 40222
		private bool detailShowing;

		// Token: 0x04009D1F RID: 40223
		private DuelClient.Step m_Step;

		// Token: 0x04009D20 RID: 40224
		private DuelClient.InitStep initStep;

		// Token: 0x04009D21 RID: 40225
		private DuelClient.PrepareStep prepareStep;

		// Token: 0x04009D22 RID: 40226
		private Dictionary<string, object> dicResult;

		// Token: 0x04009D23 RID: 40227
		private bool resultSending;

		// Token: 0x04009D24 RID: 40228
		private ReplayStream replayStream;

		// Token: 0x04009D25 RID: 40229
		private RecordManager recordManager;

		// Token: 0x04009D26 RID: 40230
		private bool isEngineInitialized;

		// Token: 0x04009D27 RID: 40231
		private const int ExistWorkSize = 512;

		// Token: 0x04009D28 RID: 40232
		private float networkTimeOutThrethold;

		// Token: 0x04009D29 RID: 40233
		private float networkTimeOutTimer;

		// Token: 0x04009D2A RID: 40234
		private const int DEFAULT_TIMEOUT_TIME = 30;

		// Token: 0x04009D2B RID: 40235
		private bool isAbend_s;

		// Token: 0x04009D2C RID: 40236
		private bool isAbend_c;

		// Token: 0x04009D2D RID: 40237
		private bool isKicked;

		// Token: 0x02000D36 RID: 3382
		// (Invoke) Token: 0x060062F4 RID: 25332
		public delegate void EffectHandler(int param1, int param2, int param3);

		// Token: 0x02000D37 RID: 3383
		public enum GUIPriority
		{
			// Token: 0x04009D2F RID: 40239
			Low,
			// Token: 0x04009D30 RID: 40240
			Middle,
			// Token: 0x04009D31 RID: 40241
			High,
			// Token: 0x04009D32 RID: 40242
			CountDown,
			// Token: 0x04009D33 RID: 40243
			Command,
			// Token: 0x04009D34 RID: 40244
			DuelLog,
			// Token: 0x04009D35 RID: 40245
			ProfileCard,
			// Token: 0x04009D36 RID: 40246
			InstantMessage,
			// Token: 0x04009D37 RID: 40247
			CardReportTelop,
			// Token: 0x04009D38 RID: 40248
			FadePlane,
			// Token: 0x04009D39 RID: 40249
			PhaseWindow,
			// Token: 0x04009D3A RID: 40250
			CardInfoDetail,
			// Token: 0x04009D3B RID: 40251
			CardInfo,
			// Token: 0x04009D3C RID: 40252
			MessageDialog,
			// Token: 0x04009D3D RID: 40253
			TutorialNavigatorTop,
			// Token: 0x04009D3E RID: 40254
			TutorialNavigatorCenter
		}

		// Token: 0x02000D38 RID: 3384
		public enum DuelSpeed
		{
			// Token: 0x04009D40 RID: 40256
			Normal,
			// Token: 0x04009D41 RID: 40257
			Fastest
		}

		// Token: 0x02000D39 RID: 3385
		public enum ActivateConfirmMode
		{
			// Token: 0x04009D43 RID: 40259
			Default,
			// Token: 0x04009D44 RID: 40260
			Off,
			// Token: 0x04009D45 RID: 40261
			On
		}

		// Token: 0x02000D3A RID: 3386
		public enum FocusCardSituation
		{
			// Token: 0x04009D47 RID: 40263
			TYPICAL,
			// Token: 0x04009D48 RID: 40264
			TYPICAL_FIX,
			// Token: 0x04009D49 RID: 40265
			CARD_MOVE,
			// Token: 0x04009D4A RID: 40266
			TARGET_SELECT,
			// Token: 0x04009D4B RID: 40267
			IGNORE_RESET_ONCE
		}

		// Token: 0x02000D3B RID: 3387
		// (Invoke) Token: 0x060062F8 RID: 25336
		public delegate void onShowAffectDelegate(int player, int position, int index);

		// Token: 0x02000D3C RID: 3388
		// (Invoke) Token: 0x060062FC RID: 25340
		public delegate void onHideAffectDelegate();

		// Token: 0x02000D3D RID: 3389
		// (Invoke) Token: 0x06006300 RID: 25344
		public delegate void onTapDownFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D3E RID: 3390
		// (Invoke) Token: 0x06006304 RID: 25348
		public delegate void onTapUpFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D3F RID: 3391
		// (Invoke) Token: 0x06006308 RID: 25352
		public delegate void onTapEnterFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D40 RID: 3392
		// (Invoke) Token: 0x0600630C RID: 25356
		public delegate void onTapExitFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D41 RID: 3393
		// (Invoke) Token: 0x06006310 RID: 25360
		public delegate void onCursorEnterFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D42 RID: 3394
		// (Invoke) Token: 0x06006314 RID: 25364
		public delegate void onCursorExitFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D43 RID: 3395
		// (Invoke) Token: 0x06006318 RID: 25368
		public delegate void onSelectFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D44 RID: 3396
		// (Invoke) Token: 0x0600631C RID: 25372
		public delegate void onDeselectFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D45 RID: 3397
		// (Invoke) Token: 0x06006320 RID: 25376
		public delegate void onFocusFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D46 RID: 3398
		// (Invoke) Token: 0x06006324 RID: 25380
		public delegate void onUnfocusFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D47 RID: 3399
		// (Invoke) Token: 0x06006328 RID: 25384
		public delegate void onDecideFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D48 RID: 3400
		// (Invoke) Token: 0x0600632C RID: 25388
		public delegate void onDoubleClickFieldDelegate(int team, int position, int viewIndex);

		// Token: 0x02000D49 RID: 3401
		// (Invoke) Token: 0x06006330 RID: 25392
		public delegate void onDragFieldBeginDelegate(int team, int position, int viewIndex, Vector2 screenPoint);

		// Token: 0x02000D4A RID: 3402
		// (Invoke) Token: 0x06006334 RID: 25396
		public delegate void onDragFieldDelegate(int team, int position, int viewIndex, Vector2 screenPoint);

		// Token: 0x02000D4B RID: 3403
		// (Invoke) Token: 0x06006338 RID: 25400
		public delegate void onDragFieldEndDelegate(int team, int position, int viewIndex, Vector2 screenPoint);

		// Token: 0x02000D4C RID: 3404
		// (Invoke) Token: 0x0600633C RID: 25404
		public delegate void onHoldFieldBeginDelegate(int team, int position, int viewIndex, Vector2 screenPoint);

		// Token: 0x02000D4D RID: 3405
		// (Invoke) Token: 0x06006340 RID: 25408
		public delegate void onDecideAttackTargetDelegate(int attackerPlayer, int attackerPosition, int attackerIndex, int targetPlayer, int targetPosition, int targetIndex);

		// Token: 0x02000D4E RID: 3406
		// (Invoke) Token: 0x06006344 RID: 25412
		public delegate void onFieldViewChangedDelegate(bool fieldViewing);

		// Token: 0x02000D4F RID: 3407
		// (Invoke) Token: 0x06006348 RID: 25416
		public delegate void onChangeActivateConfirmMode(DuelClient.ActivateConfirmMode mode);

		// Token: 0x02000D50 RID: 3408
		// (Invoke) Token: 0x0600634C RID: 25420
		public delegate void onChangeDetailShowing(bool showing);

		// Token: 0x02000D51 RID: 3409
		// (Invoke) Token: 0x06006350 RID: 25424
		public delegate void onFieldBackKey();

		// Token: 0x02000D52 RID: 3410
		// (Invoke) Token: 0x06006354 RID: 25428
		public delegate void onPlayScreenEffect();

		// Token: 0x02000D53 RID: 3411
		// (Invoke) Token: 0x06006358 RID: 25432
		public delegate void onStopScreenEffect();

		// Token: 0x02000D54 RID: 3412
		private enum Step
		{
			// Token: 0x04009D4D RID: 40269
			InitLoadRes,
			// Token: 0x04009D4E RID: 40270
			WaitLoadRes,
			// Token: 0x04009D4F RID: 40271
			InitializeProcess,
			// Token: 0x04009D50 RID: 40272
			FinishInitialize,
			// Token: 0x04009D51 RID: 40273
			WaitConnecting,
			// Token: 0x04009D52 RID: 40274
			InitEngine,
			// Token: 0x04009D53 RID: 40275
			InitSound,
			// Token: 0x04009D54 RID: 40276
			WaitSound,
			// Token: 0x04009D55 RID: 40277
			InitLoadSound,
			// Token: 0x04009D56 RID: 40278
			WaitLoadSound,
			// Token: 0x04009D57 RID: 40279
			WaitGameObjectInit,
			// Token: 0x04009D58 RID: 40280
			PrepareProcess,
			// Token: 0x04009D59 RID: 40281
			FinishPrepare,
			// Token: 0x04009D5A RID: 40282
			WaitCameraWork,
			// Token: 0x04009D5B RID: 40283
			ShowUpDuel,
			// Token: 0x04009D5C RID: 40284
			WaitShowUp,
			// Token: 0x04009D5D RID: 40285
			ExecDuel,
			// Token: 0x04009D5E RID: 40286
			EndDuel,
			// Token: 0x04009D5F RID: 40287
			WaitEndNetwork,
			// Token: 0x04009D60 RID: 40288
			DuelEnd,
			// Token: 0x04009D61 RID: 40289
			InitTerm,
			// Token: 0x04009D62 RID: 40290
			WaitTerm,
			// Token: 0x04009D63 RID: 40291
			End,
			// Token: 0x04009D64 RID: 40292
			WaitDestroy,
			// Token: 0x04009D65 RID: 40293
			ConnectingError,
			// Token: 0x04009D66 RID: 40294
			Beginning,
			// Token: 0x04009D67 RID: 40295
			InitSequenceStart = 0,
			// Token: 0x04009D68 RID: 40296
			InitSequenceEnd = 12
		}

		// Token: 0x02000D55 RID: 3413
		private enum InitStep
		{
			// Token: 0x04009D6A RID: 40298
			Start,
			// Token: 0x04009D6B RID: 40299
			StartDuelHUD,
			// Token: 0x04009D6C RID: 40300
			WaitDuelHUD,
			// Token: 0x04009D6D RID: 40301
			StartEffectWorker,
			// Token: 0x04009D6E RID: 40302
			AdoptiveGoManager,
			// Token: 0x04009D6F RID: 40303
			Finish
		}

		// Token: 0x02000D56 RID: 3414
		private enum PrepareStep
		{
			// Token: 0x04009D71 RID: 40305
			Start,
			// Token: 0x04009D72 RID: 40306
			StartDuelHUD,
			// Token: 0x04009D73 RID: 40307
			WaitDuelHUD,
			// Token: 0x04009D74 RID: 40308
			StartEffectWorker,
			// Token: 0x04009D75 RID: 40309
			WaitEffectWorker,
			// Token: 0x04009D76 RID: 40310
			StartTutorial,
			// Token: 0x04009D77 RID: 40311
			WaitTutorial,
			// Token: 0x04009D78 RID: 40312
			Finished
		}
	}
}
