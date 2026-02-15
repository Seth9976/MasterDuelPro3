using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C9 RID: 201
	public class UniversalResourceData : UniversalResourceDataBase
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00013003 File Offset: 0x00011203
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001300B File Offset: 0x0001120B
		internal UniversalResourceDataBase.ActiveID activeColorID { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00013014 File Offset: 0x00011214
		public TextureHandle activeColorTexture
		{
			get
			{
				if (!base.CheckAndWarnAboutAccessibility())
				{
					return TextureHandle.nullHandle;
				}
				UniversalResourceDataBase.ActiveID activeColorID = this.activeColorID;
				if (activeColorID == UniversalResourceDataBase.ActiveID.Camera)
				{
					return this.cameraColor;
				}
				if (activeColorID != UniversalResourceDataBase.ActiveID.BackBuffer)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.backBufferColor;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00013052 File Offset: 0x00011252
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001305A File Offset: 0x0001125A
		internal UniversalResourceDataBase.ActiveID activeDepthID { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00013064 File Offset: 0x00011264
		public TextureHandle activeDepthTexture
		{
			get
			{
				if (!base.CheckAndWarnAboutAccessibility())
				{
					return TextureHandle.nullHandle;
				}
				UniversalResourceDataBase.ActiveID activeDepthID = this.activeDepthID;
				if (activeDepthID == UniversalResourceDataBase.ActiveID.Camera)
				{
					return this.cameraDepth;
				}
				if (activeDepthID != UniversalResourceDataBase.ActiveID.BackBuffer)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.backBufferDepth;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000130A2 File Offset: 0x000112A2
		public bool isActiveTargetBackBuffer
		{
			get
			{
				if (!base.isAccessible)
				{
					Debug.LogError("Trying to access frameData outside of the current frame setup.");
					return false;
				}
				return this.activeColorID == UniversalResourceDataBase.ActiveID.BackBuffer;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000130C1 File Offset: 0x000112C1
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000130CF File Offset: 0x000112CF
		public TextureHandle backBufferColor
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._backBufferColor);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._backBufferColor, value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000130DE File Offset: 0x000112DE
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000130EC File Offset: 0x000112EC
		public TextureHandle backBufferDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._backBufferDepth);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._backBufferDepth, value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000130FB File Offset: 0x000112FB
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00013109 File Offset: 0x00011309
		public TextureHandle cameraColor
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraColor);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._cameraColor, value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00013118 File Offset: 0x00011318
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00013126 File Offset: 0x00011326
		public TextureHandle cameraDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraDepth);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._cameraDepth, value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00013135 File Offset: 0x00011335
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00013143 File Offset: 0x00011343
		public TextureHandle mainShadowsTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._mainShadowsTexture);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._mainShadowsTexture, value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00013152 File Offset: 0x00011352
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00013160 File Offset: 0x00011360
		public TextureHandle additionalShadowsTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._additionalShadowsTexture);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._additionalShadowsTexture, value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001316F File Offset: 0x0001136F
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0001317D File Offset: 0x0001137D
		public TextureHandle[] gBuffer
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._gBuffer);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._gBuffer, value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001318C File Offset: 0x0001138C
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0001319A File Offset: 0x0001139A
		public TextureHandle cameraOpaqueTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraOpaqueTexture);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._cameraOpaqueTexture, value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000131A9 File Offset: 0x000113A9
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x000131B7 File Offset: 0x000113B7
		public TextureHandle cameraDepthTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraDepthTexture);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._cameraDepthTexture, value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000131C6 File Offset: 0x000113C6
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000131D4 File Offset: 0x000113D4
		public TextureHandle cameraNormalsTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraNormalsTexture);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._cameraNormalsTexture, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000131E3 File Offset: 0x000113E3
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x000131F1 File Offset: 0x000113F1
		public TextureHandle motionVectorColor
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._motionVectorColor);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._motionVectorColor, value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00013200 File Offset: 0x00011400
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0001320E File Offset: 0x0001140E
		public TextureHandle motionVectorDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._motionVectorDepth);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._motionVectorDepth, value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001321D File Offset: 0x0001141D
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001322B File Offset: 0x0001142B
		public TextureHandle internalColorLut
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._internalColorLut);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._internalColorLut, value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001323A File Offset: 0x0001143A
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00013248 File Offset: 0x00011448
		internal TextureHandle debugScreenColor
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._debugScreenColor);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._debugScreenColor, value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00013257 File Offset: 0x00011457
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00013265 File Offset: 0x00011465
		internal TextureHandle debugScreenDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._debugScreenDepth);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._debugScreenDepth, value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00013274 File Offset: 0x00011474
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00013282 File Offset: 0x00011482
		public TextureHandle afterPostProcessColor
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._afterPostProcessColor);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._afterPostProcessColor, value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00013291 File Offset: 0x00011491
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0001329F File Offset: 0x0001149F
		public TextureHandle overlayUITexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._overlayUITexture);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._overlayUITexture, value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000132AE File Offset: 0x000114AE
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000132BC File Offset: 0x000114BC
		public TextureHandle renderingLayersTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._renderingLayersTexture);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._renderingLayersTexture, value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000132CB File Offset: 0x000114CB
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x000132D9 File Offset: 0x000114D9
		public TextureHandle[] dBuffer
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._dBuffer);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._dBuffer, value);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000132E8 File Offset: 0x000114E8
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x000132F6 File Offset: 0x000114F6
		public TextureHandle dBufferDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._dBufferDepth);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._dBufferDepth, value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00013305 File Offset: 0x00011505
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00013313 File Offset: 0x00011513
		public TextureHandle ssaoTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._ssaoTexture);
			}
			internal set
			{
				base.CheckAndSetTextureHandle(ref this._ssaoTexture, value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00013322 File Offset: 0x00011522
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00013330 File Offset: 0x00011530
		internal TextureHandle stpDebugView
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._stpDebugView);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._stpDebugView, value);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00013340 File Offset: 0x00011540
		public override void Reset()
		{
			this._backBufferColor = TextureHandle.nullHandle;
			this._backBufferDepth = TextureHandle.nullHandle;
			this._cameraColor = TextureHandle.nullHandle;
			this._cameraDepth = TextureHandle.nullHandle;
			this._mainShadowsTexture = TextureHandle.nullHandle;
			this._additionalShadowsTexture = TextureHandle.nullHandle;
			this._cameraOpaqueTexture = TextureHandle.nullHandle;
			this._cameraDepthTexture = TextureHandle.nullHandle;
			this._cameraNormalsTexture = TextureHandle.nullHandle;
			this._motionVectorColor = TextureHandle.nullHandle;
			this._motionVectorDepth = TextureHandle.nullHandle;
			this._internalColorLut = TextureHandle.nullHandle;
			this._debugScreenColor = TextureHandle.nullHandle;
			this._debugScreenDepth = TextureHandle.nullHandle;
			this._afterPostProcessColor = TextureHandle.nullHandle;
			this._overlayUITexture = TextureHandle.nullHandle;
			this._renderingLayersTexture = TextureHandle.nullHandle;
			this._dBufferDepth = TextureHandle.nullHandle;
			this._ssaoTexture = TextureHandle.nullHandle;
			this._stpDebugView = TextureHandle.nullHandle;
			for (int i = 0; i < this._gBuffer.Length; i++)
			{
				this._gBuffer[i] = TextureHandle.nullHandle;
			}
			for (int j = 0; j < this._dBuffer.Length; j++)
			{
				this._dBuffer[j] = TextureHandle.nullHandle;
			}
		}

		// Token: 0x0400045F RID: 1119
		private TextureHandle _backBufferColor;

		// Token: 0x04000460 RID: 1120
		private TextureHandle _backBufferDepth;

		// Token: 0x04000461 RID: 1121
		private TextureHandle _cameraColor;

		// Token: 0x04000462 RID: 1122
		private TextureHandle _cameraDepth;

		// Token: 0x04000463 RID: 1123
		private TextureHandle _mainShadowsTexture;

		// Token: 0x04000464 RID: 1124
		private TextureHandle _additionalShadowsTexture;

		// Token: 0x04000465 RID: 1125
		private TextureHandle[] _gBuffer = new TextureHandle[7];

		// Token: 0x04000466 RID: 1126
		private TextureHandle _cameraOpaqueTexture;

		// Token: 0x04000467 RID: 1127
		private TextureHandle _cameraDepthTexture;

		// Token: 0x04000468 RID: 1128
		private TextureHandle _cameraNormalsTexture;

		// Token: 0x04000469 RID: 1129
		private TextureHandle _motionVectorColor;

		// Token: 0x0400046A RID: 1130
		private TextureHandle _motionVectorDepth;

		// Token: 0x0400046B RID: 1131
		private TextureHandle _internalColorLut;

		// Token: 0x0400046C RID: 1132
		internal TextureHandle _debugScreenColor;

		// Token: 0x0400046D RID: 1133
		internal TextureHandle _debugScreenDepth;

		// Token: 0x0400046E RID: 1134
		private TextureHandle _afterPostProcessColor;

		// Token: 0x0400046F RID: 1135
		private TextureHandle _overlayUITexture;

		// Token: 0x04000470 RID: 1136
		private TextureHandle _renderingLayersTexture;

		// Token: 0x04000471 RID: 1137
		private TextureHandle[] _dBuffer = new TextureHandle[3];

		// Token: 0x04000472 RID: 1138
		private TextureHandle _dBufferDepth;

		// Token: 0x04000473 RID: 1139
		private TextureHandle _ssaoTexture;

		// Token: 0x04000474 RID: 1140
		private TextureHandle _stpDebugView;
	}
}
