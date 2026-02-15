using System;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D1 RID: 209
	internal class LightCookieManager : IDisposable
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00013CBF File Offset: 0x00011EBF
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00013CC7 File Offset: 0x00011EC7
		internal bool IsKeywordLightCookieEnabled { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00013CD0 File Offset: 0x00011ED0
		internal RTHandle AdditionalLightsCookieAtlasTexture
		{
			get
			{
				Texture2DAtlas additionalLightsCookieAtlas = this.m_AdditionalLightsCookieAtlas;
				if (additionalLightsCookieAtlas == null)
				{
					return null;
				}
				return additionalLightsCookieAtlas.AtlasTexture;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013CE3 File Offset: 0x00011EE3
		public LightCookieManager(ref LightCookieManager.Settings settings)
		{
			this.m_Settings = settings;
			this.m_WorkMem = new LightCookieManager.WorkMemory();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00013D18 File Offset: 0x00011F18
		private void InitAdditionalLights(int size)
		{
			Vector2Int vector2Int = this.m_Settings.atlas.resolution;
			int x = vector2Int.x;
			vector2Int = this.m_Settings.atlas.resolution;
			this.m_AdditionalLightsCookieAtlas = new Texture2DAtlas(x, vector2Int.y, this.m_Settings.atlas.format, FilterMode.Bilinear, false, "Universal Light Cookie Atlas", false);
			this.m_AdditionalLightsCookieShaderData = new LightCookieManager.LightCookieShaderData(size, this.m_Settings.useStructuredBuffer);
			this.m_VisibleLightIndexToShaderDataIndex = new int[this.m_Settings.maxAdditionalLights + 1];
			this.m_CookieSizeDivisor = 1;
			this.m_PrevCookieRequestPixelCount = uint.MaxValue;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00013DB5 File Offset: 0x00011FB5
		public bool isInitialized()
		{
			return this.m_AdditionalLightsCookieAtlas != null && this.m_AdditionalLightsCookieShaderData != null;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00013DCA File Offset: 0x00011FCA
		public void Dispose()
		{
			Texture2DAtlas additionalLightsCookieAtlas = this.m_AdditionalLightsCookieAtlas;
			if (additionalLightsCookieAtlas != null)
			{
				additionalLightsCookieAtlas.Release();
			}
			LightCookieManager.LightCookieShaderData additionalLightsCookieShaderData = this.m_AdditionalLightsCookieShaderData;
			if (additionalLightsCookieShaderData == null)
			{
				return;
			}
			additionalLightsCookieShaderData.Dispose();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00013DED File Offset: 0x00011FED
		public int GetLightCookieShaderDataIndex(int visibleLightIndex)
		{
			if (!this.isInitialized())
			{
				return -1;
			}
			return this.m_VisibleLightIndexToShaderDataIndex[visibleLightIndex];
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00013E04 File Offset: 0x00012004
		public void Setup(CommandBuffer cmd, UniversalLightData lightData)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.LightCookies)))
			{
				bool isMainLightAvailable = lightData.mainLightIndex >= 0;
				if (isMainLightAvailable)
				{
					VisibleLight mainLight = lightData.visibleLights[lightData.mainLightIndex];
					isMainLightAvailable = this.SetupMainLight(cmd, ref mainLight);
				}
				bool isAdditionalLightsAvailable = lightData.additionalLightsCount > 0;
				if (isAdditionalLightsAvailable)
				{
					isAdditionalLightsAvailable = this.SetupAdditionalLights(cmd, lightData);
				}
				if (!isAdditionalLightsAvailable)
				{
					if (this.m_VisibleLightIndexToShaderDataIndex != null && this.m_AdditionalLightsCookieShaderData.isUploaded)
					{
						int len = this.m_VisibleLightIndexToShaderDataIndex.Length;
						for (int i = 0; i < len; i++)
						{
							this.m_VisibleLightIndexToShaderDataIndex[i] = -1;
						}
					}
					LightCookieManager.LightCookieShaderData additionalLightsCookieShaderData = this.m_AdditionalLightsCookieShaderData;
					if (additionalLightsCookieShaderData != null)
					{
						additionalLightsCookieShaderData.Clear(cmd);
					}
				}
				this.IsKeywordLightCookieEnabled = isMainLightAvailable || isAdditionalLightsAvailable;
				cmd.SetKeyword(in ShaderGlobalKeywords.LightCookies, this.IsKeywordLightCookieEnabled);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00013EEC File Offset: 0x000120EC
		private bool SetupMainLight(CommandBuffer cmd, ref VisibleLight visibleMainLight)
		{
			Light mainLight = visibleMainLight.light;
			Texture cookieTexture = mainLight.cookie;
			bool flag = cookieTexture != null;
			if (flag)
			{
				Matrix4x4 cookieUVTransform = Matrix4x4.identity;
				float cookieFormat = (float)this.GetLightCookieShaderFormat(cookieTexture.graphicsFormat);
				UniversalAdditionalLightData additionalLightData;
				if (mainLight.TryGetComponent<UniversalAdditionalLightData>(out additionalLightData))
				{
					this.GetLightUVScaleOffset(ref additionalLightData, ref cookieUVTransform);
				}
				Matrix4x4 cookieMatrix = LightCookieManager.s_DirLightProj * cookieUVTransform * visibleMainLight.localToWorldMatrix.inverse;
				cmd.SetGlobalTexture(LightCookieManager.ShaderProperty.mainLightTexture, cookieTexture);
				cmd.SetGlobalMatrix(LightCookieManager.ShaderProperty.mainLightWorldToLight, cookieMatrix);
				cmd.SetGlobalFloat(LightCookieManager.ShaderProperty.mainLightCookieTextureFormat, cookieFormat);
				return flag;
			}
			cmd.SetGlobalTexture(LightCookieManager.ShaderProperty.mainLightTexture, Texture2D.whiteTexture);
			cmd.SetGlobalMatrix(LightCookieManager.ShaderProperty.mainLightWorldToLight, Matrix4x4.identity);
			cmd.SetGlobalFloat(LightCookieManager.ShaderProperty.mainLightCookieTextureFormat, -1f);
			return flag;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00013FBC File Offset: 0x000121BC
		private LightCookieManager.LightCookieShaderFormat GetLightCookieShaderFormat(GraphicsFormat cookieFormat)
		{
			if (cookieFormat <= GraphicsFormat.R16_UInt)
			{
				if (cookieFormat <= GraphicsFormat.R8_UInt)
				{
					if (cookieFormat <= GraphicsFormat.R8_UNorm)
					{
						if (cookieFormat == GraphicsFormat.R8_SRGB || cookieFormat == GraphicsFormat.R8_UNorm)
						{
							return LightCookieManager.LightCookieShaderFormat.Red;
						}
					}
					else if (cookieFormat == GraphicsFormat.R8_SNorm || cookieFormat == GraphicsFormat.R8_UInt)
					{
						return LightCookieManager.LightCookieShaderFormat.Red;
					}
				}
				else if (cookieFormat <= GraphicsFormat.R16_UNorm)
				{
					if (cookieFormat == GraphicsFormat.R8_SInt || cookieFormat == GraphicsFormat.R16_UNorm)
					{
						return LightCookieManager.LightCookieShaderFormat.Red;
					}
				}
				else if (cookieFormat == GraphicsFormat.R16_SNorm || cookieFormat == GraphicsFormat.R16_UInt)
				{
					return LightCookieManager.LightCookieShaderFormat.Red;
				}
			}
			else if (cookieFormat <= GraphicsFormat.R16_SFloat)
			{
				if (cookieFormat <= GraphicsFormat.R32_UInt)
				{
					if (cookieFormat == GraphicsFormat.R16_SInt || cookieFormat == GraphicsFormat.R32_UInt)
					{
						return LightCookieManager.LightCookieShaderFormat.Red;
					}
				}
				else if (cookieFormat == GraphicsFormat.R32_SInt || cookieFormat == GraphicsFormat.R16_SFloat)
				{
					return LightCookieManager.LightCookieShaderFormat.Red;
				}
			}
			else if (cookieFormat <= (GraphicsFormat)55)
			{
				if (cookieFormat == GraphicsFormat.R32_SFloat)
				{
					return LightCookieManager.LightCookieShaderFormat.Red;
				}
				if (cookieFormat - (GraphicsFormat)54 <= 1)
				{
					return LightCookieManager.LightCookieShaderFormat.Alpha;
				}
			}
			else if (cookieFormat - GraphicsFormat.R_BC4_UNorm <= 1 || cookieFormat - GraphicsFormat.R_EAC_UNorm <= 1)
			{
				return LightCookieManager.LightCookieShaderFormat.Red;
			}
			return LightCookieManager.LightCookieShaderFormat.RGB;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00014054 File Offset: 0x00012254
		private void GetLightUVScaleOffset(ref UniversalAdditionalLightData additionalLightData, ref Matrix4x4 uvTransform)
		{
			Vector2 uvScale = Vector2.one / additionalLightData.lightCookieSize;
			Vector2 uvOffset = additionalLightData.lightCookieOffset;
			if (Mathf.Abs(uvScale.x) < half.MinValue)
			{
				uvScale.x = Mathf.Sign(uvScale.x) * half.MinValue;
			}
			if (Mathf.Abs(uvScale.y) < half.MinValue)
			{
				uvScale.y = Mathf.Sign(uvScale.y) * half.MinValue;
			}
			uvTransform = Matrix4x4.Scale(new Vector3(uvScale.x, uvScale.y, 1f));
			uvTransform.SetColumn(3, new Vector4(-uvOffset.x * uvScale.x, -uvOffset.y * uvScale.y, 0f, 1f));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00014124 File Offset: 0x00012324
		private bool SetupAdditionalLights(CommandBuffer cmd, UniversalLightData lightData)
		{
			int maxLightCount = Math.Min(this.m_Settings.maxAdditionalLights, lightData.visibleLights.Length);
			this.m_WorkMem.Resize(maxLightCount);
			int validLightCount = this.FilterAndValidateAdditionalLights(lightData, this.m_WorkMem.lightMappings);
			if (validLightCount <= 0)
			{
				return false;
			}
			if (!this.isInitialized())
			{
				this.InitAdditionalLights(validLightCount);
			}
			LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLights = new LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping>(this.m_WorkMem.lightMappings, validLightCount);
			int validUVRectCount = this.UpdateAdditionalLightsAtlas(cmd, ref validLights, this.m_WorkMem.uvRects);
			LightCookieManager.WorkSlice<Vector4> validUvRects = new LightCookieManager.WorkSlice<Vector4>(this.m_WorkMem.uvRects, validUVRectCount);
			this.UploadAdditionalLights(cmd, lightData, ref validLights, ref validUvRects);
			return validUvRects.length > 0;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000141D4 File Offset: 0x000123D4
		private int FilterAndValidateAdditionalLights(UniversalLightData lightData, LightCookieManager.LightCookieMapping[] validLightMappings)
		{
			int skipMainLightIndex = lightData.mainLightIndex;
			int lightBufferOffset = 0;
			int validLightCount = 0;
			int visibleLightCount = lightData.visibleLights.Length;
			for (int i = 0; i < visibleLightCount; i++)
			{
				if (i == skipMainLightIndex)
				{
					lightBufferOffset--;
				}
				else
				{
					ref VisibleLight visLight = ref lightData.visibleLights.UnsafeElementAtMutable(i);
					Light light = visLight.light;
					if (!(light.cookie == null))
					{
						LightType lightType = visLight.lightType;
						if (lightType != LightType.Spot && lightType != LightType.Point && lightType != LightType.Directional)
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Additional ",
								lightType.ToString(),
								" light called '",
								light.name,
								"' has a light cookie which will not be visible."
							}), light);
						}
						else
						{
							LightCookieManager.LightCookieMapping lp;
							lp.visibleLightIndex = (ushort)i;
							lp.lightBufferIndex = (ushort)(i + lightBufferOffset);
							lp.light = light;
							if ((int)lp.lightBufferIndex >= validLightMappings.Length || validLightCount + 1 >= validLightMappings.Length)
							{
								if (visibleLightCount > this.m_Settings.maxAdditionalLights && Time.frameCount - this.m_PrevWarnFrame > 3600)
								{
									this.m_PrevWarnFrame = Time.frameCount;
									Debug.LogWarning(string.Concat(new string[]
									{
										"Max light cookies (",
										validLightMappings.Length.ToString(),
										") reached. Some visible lights (",
										(visibleLightCount - i - 1).ToString(),
										") might skip light cookie rendering."
									}));
									break;
								}
								break;
							}
							else
							{
								validLightMappings[validLightCount++] = lp;
							}
						}
					}
				}
			}
			return validLightCount;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014364 File Offset: 0x00012564
		private int UpdateAdditionalLightsAtlas(CommandBuffer cmd, ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings, Vector4[] textureAtlasUVRects)
		{
			validLightMappings.Sort(LightCookieManager.LightCookieMapping.s_CompareByCookieSize);
			uint cookieRequestPixelCount = this.ComputeCookieRequestPixelCount(ref validLightMappings);
			Vector2Int atlasSize = this.m_AdditionalLightsCookieAtlas.AtlasTexture.referenceSize;
			float requestAtlasRatio = cookieRequestPixelCount / (float)(atlasSize.x * atlasSize.y);
			int cookieSizeDivisorApprox = this.ApproximateCookieSizeDivisor(requestAtlasRatio);
			if (cookieSizeDivisorApprox < this.m_CookieSizeDivisor && cookieRequestPixelCount < this.m_PrevCookieRequestPixelCount)
			{
				this.m_AdditionalLightsCookieAtlas.ResetAllocator();
				this.m_CookieSizeDivisor = cookieSizeDivisorApprox;
			}
			int uvRectCount = 0;
			while (uvRectCount <= 0)
			{
				uvRectCount = this.FetchUVRects(cmd, ref validLightMappings, textureAtlasUVRects, this.m_CookieSizeDivisor);
				if (uvRectCount <= 0)
				{
					this.m_AdditionalLightsCookieAtlas.ResetAllocator();
					this.m_CookieSizeDivisor = Mathf.Max(this.m_CookieSizeDivisor + 1, cookieSizeDivisorApprox);
					this.m_PrevCookieRequestPixelCount = cookieRequestPixelCount;
				}
			}
			return uvRectCount;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014420 File Offset: 0x00012620
		private int FetchUVRects(CommandBuffer cmd, ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings, Vector4[] textureAtlasUVRects, int cookieSizeDivisor)
		{
			int uvRectCount = 0;
			int i = 0;
			while (i < validLightMappings.length)
			{
				Texture cookie = validLightMappings[i].light.cookie;
				Vector4 uvScaleOffset = Vector4.zero;
				if (cookie.dimension == TextureDimension.Cube)
				{
					uvScaleOffset = this.FetchCube(cmd, cookie, cookieSizeDivisor);
				}
				else
				{
					uvScaleOffset = this.Fetch2D(cmd, cookie, cookieSizeDivisor);
				}
				if (!(uvScaleOffset != Vector4.zero))
				{
					if (cookieSizeDivisor > 16)
					{
						Debug.LogWarning("Light cookies atlas is extremely full! Some of the light cookies were discarded. Increase light cookie atlas space or reduce the amount of unique light cookies.");
						return uvRectCount;
					}
					return 0;
				}
				else
				{
					if (!SystemInfo.graphicsUVStartsAtTop)
					{
						uvScaleOffset.w = 1f - uvScaleOffset.w - uvScaleOffset.y;
					}
					textureAtlasUVRects[uvRectCount++] = uvScaleOffset;
					i++;
				}
			}
			return uvRectCount;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000144D0 File Offset: 0x000126D0
		private uint ComputeCookieRequestPixelCount(ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings)
		{
			uint requestPixelCount = 0U;
			int prevCookieID = 0;
			for (int i = 0; i < validLightMappings.length; i++)
			{
				Texture cookie = validLightMappings[i].light.cookie;
				int cookieID = cookie.GetInstanceID();
				if (cookieID != prevCookieID)
				{
					prevCookieID = cookieID;
					int pixelCookieCount = cookie.width * cookie.height;
					requestPixelCount += (uint)pixelCookieCount;
				}
			}
			return requestPixelCount;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014529 File Offset: 0x00012729
		private int ApproximateCookieSizeDivisor(float requestAtlasRatio)
		{
			return (int)Mathf.Max(Mathf.Ceil(Mathf.Sqrt(requestAtlasRatio)), 1f);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014544 File Offset: 0x00012744
		private Vector4 Fetch2D(CommandBuffer cmd, Texture cookie, int cookieSizeDivisor = 1)
		{
			Vector4 uvScaleOffset = Vector4.zero;
			int scaledWidth = Mathf.Max(cookie.width / cookieSizeDivisor, 4);
			int scaledHeight = Mathf.Max(cookie.height / cookieSizeDivisor, 4);
			Vector2 scaledCookieSize = new Vector2((float)scaledWidth, (float)scaledHeight);
			if (this.m_AdditionalLightsCookieAtlas.IsCached(out uvScaleOffset, cookie))
			{
				this.m_AdditionalLightsCookieAtlas.UpdateTexture(cmd, cookie, ref uvScaleOffset, true, true);
			}
			else
			{
				this.m_AdditionalLightsCookieAtlas.AllocateTexture(cmd, ref uvScaleOffset, cookie, scaledWidth, scaledHeight, -1);
			}
			this.AdjustUVRect(ref uvScaleOffset, cookie, ref scaledCookieSize);
			return uvScaleOffset;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000145C4 File Offset: 0x000127C4
		private Vector4 FetchCube(CommandBuffer cmd, Texture cookie, int cookieSizeDivisor = 1)
		{
			Vector4 uvScaleOffset = Vector4.zero;
			int scaledOctCookieSize = Mathf.Max(this.ComputeOctahedralCookieSize(cookie) / cookieSizeDivisor, 4);
			if (this.m_AdditionalLightsCookieAtlas.IsCached(out uvScaleOffset, cookie))
			{
				this.m_AdditionalLightsCookieAtlas.UpdateTexture(cmd, cookie, ref uvScaleOffset, true, true);
			}
			else
			{
				this.m_AdditionalLightsCookieAtlas.AllocateTexture(cmd, ref uvScaleOffset, cookie, scaledOctCookieSize, scaledOctCookieSize, -1);
			}
			Vector2 scaledCookieSize = Vector2.one * (float)scaledOctCookieSize;
			this.AdjustUVRect(ref uvScaleOffset, cookie, ref scaledCookieSize);
			return uvScaleOffset;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014638 File Offset: 0x00012838
		private int ComputeOctahedralCookieSize(Texture cookie)
		{
			int octCookieSize = Math.Max(cookie.width, cookie.height);
			LightCookieManager.Settings.AtlasSettings atlas = this.m_Settings.atlas;
			if (atlas.isPow2)
			{
				octCookieSize *= Mathf.NextPowerOfTwo((int)this.m_Settings.cubeOctahedralSizeScale);
			}
			else
			{
				octCookieSize = (int)((float)octCookieSize * this.m_Settings.cubeOctahedralSizeScale + 0.5f);
			}
			return octCookieSize;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014699 File Offset: 0x00012899
		private void AdjustUVRect(ref Vector4 uvScaleOffset, Texture cookie, ref Vector2 cookieSize)
		{
			if (uvScaleOffset != Vector4.zero)
			{
				this.ShrinkUVRect(ref uvScaleOffset, 0.5f, ref cookieSize);
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000146BC File Offset: 0x000128BC
		private void ShrinkUVRect(ref Vector4 uvScaleOffset, float amountPixels, ref Vector2 cookieSize)
		{
			Vector2 shrinkOffset = Vector2.one * amountPixels / cookieSize;
			Vector2 shrinkScale = (cookieSize - Vector2.one * (amountPixels * 2f)) / cookieSize;
			uvScaleOffset.z += uvScaleOffset.x * shrinkOffset.x;
			uvScaleOffset.w += uvScaleOffset.y * shrinkOffset.y;
			uvScaleOffset.x *= shrinkScale.x;
			uvScaleOffset.y *= shrinkScale.y;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014758 File Offset: 0x00012958
		private void UploadAdditionalLights(CommandBuffer cmd, UniversalLightData lightData, ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings, ref LightCookieManager.WorkSlice<Vector4> validUvRects)
		{
			cmd.SetGlobalTexture(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasTexture, this.m_AdditionalLightsCookieAtlas.AtlasTexture);
			cmd.SetGlobalFloat(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasTextureFormat, (float)this.GetLightCookieShaderFormat(this.m_AdditionalLightsCookieAtlas.AtlasTexture.rt.graphicsFormat));
			if (this.m_VisibleLightIndexToShaderDataIndex.Length < lightData.visibleLights.Length)
			{
				this.m_VisibleLightIndexToShaderDataIndex = new int[lightData.visibleLights.Length];
			}
			int len = Math.Min(this.m_VisibleLightIndexToShaderDataIndex.Length, lightData.visibleLights.Length);
			for (int i = 0; i < len; i++)
			{
				this.m_VisibleLightIndexToShaderDataIndex[i] = -1;
			}
			this.m_AdditionalLightsCookieShaderData.Resize(this.m_Settings.maxAdditionalLights);
			Matrix4x4[] worldToLights = this.m_AdditionalLightsCookieShaderData.worldToLights;
			ShaderBitArray cookieEnableBits = this.m_AdditionalLightsCookieShaderData.cookieEnableBits;
			Vector4[] atlasUVRects = this.m_AdditionalLightsCookieShaderData.atlasUVRects;
			float[] lightTypes = this.m_AdditionalLightsCookieShaderData.lightTypes;
			Array.Clear(atlasUVRects, 0, atlasUVRects.Length);
			cookieEnableBits.Clear();
			for (int j = 0; j < validUvRects.length; j++)
			{
				int visIndex = (int)validLightMappings[j].visibleLightIndex;
				int bufIndex = (int)validLightMappings[j].lightBufferIndex;
				this.m_VisibleLightIndexToShaderDataIndex[visIndex] = bufIndex;
				ref VisibleLight visLight = ref lightData.visibleLights.UnsafeElementAtMutable(visIndex);
				lightTypes[bufIndex] = (float)visLight.lightType;
				worldToLights[bufIndex] = visLight.localToWorldMatrix.inverse;
				atlasUVRects[bufIndex] = validUvRects[j];
				cookieEnableBits[bufIndex] = true;
				if (visLight.lightType == LightType.Spot)
				{
					float spotAngle = visLight.spotAngle;
					float spotRange = visLight.range;
					Matrix4x4 perp = Matrix4x4.Perspective(spotAngle, 1f, 0.001f, spotRange);
					perp.SetColumn(2, perp.GetColumn(2) * -1f);
					worldToLights[bufIndex] = perp * worldToLights[bufIndex];
				}
				else if (visLight.lightType == LightType.Directional)
				{
					UniversalAdditionalLightData additionalLightData;
					visLight.light.TryGetComponent<UniversalAdditionalLightData>(out additionalLightData);
					Matrix4x4 cookieUVTransform = Matrix4x4.identity;
					this.GetLightUVScaleOffset(ref additionalLightData, ref cookieUVTransform);
					Matrix4x4 cookieMatrix = LightCookieManager.s_DirLightProj * cookieUVTransform * visLight.localToWorldMatrix.inverse;
					worldToLights[bufIndex] = cookieMatrix;
				}
			}
			this.m_AdditionalLightsCookieShaderData.Upload(cmd);
		}

		// Token: 0x0400049F RID: 1183
		private static readonly Matrix4x4 s_DirLightProj = Matrix4x4.Ortho(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f, 0.5f);

		// Token: 0x040004A0 RID: 1184
		private Texture2DAtlas m_AdditionalLightsCookieAtlas;

		// Token: 0x040004A1 RID: 1185
		private LightCookieManager.LightCookieShaderData m_AdditionalLightsCookieShaderData;

		// Token: 0x040004A2 RID: 1186
		private readonly LightCookieManager.Settings m_Settings;

		// Token: 0x040004A3 RID: 1187
		private LightCookieManager.WorkMemory m_WorkMem;

		// Token: 0x040004A4 RID: 1188
		private int[] m_VisibleLightIndexToShaderDataIndex;

		// Token: 0x040004A5 RID: 1189
		private const int k_MaxCookieSizeDivisor = 16;

		// Token: 0x040004A6 RID: 1190
		private int m_CookieSizeDivisor = 1;

		// Token: 0x040004A7 RID: 1191
		private uint m_PrevCookieRequestPixelCount = uint.MaxValue;

		// Token: 0x040004A8 RID: 1192
		private int m_PrevWarnFrame = -1;

		// Token: 0x020000D2 RID: 210
		private static class ShaderProperty
		{
			// Token: 0x040004AA RID: 1194
			public static readonly int mainLightTexture = Shader.PropertyToID("_MainLightCookieTexture");

			// Token: 0x040004AB RID: 1195
			public static readonly int mainLightWorldToLight = Shader.PropertyToID("_MainLightWorldToLight");

			// Token: 0x040004AC RID: 1196
			public static readonly int mainLightCookieTextureFormat = Shader.PropertyToID("_MainLightCookieTextureFormat");

			// Token: 0x040004AD RID: 1197
			public static readonly int additionalLightsCookieAtlasTexture = Shader.PropertyToID("_AdditionalLightsCookieAtlasTexture");

			// Token: 0x040004AE RID: 1198
			public static readonly int additionalLightsCookieAtlasTextureFormat = Shader.PropertyToID("_AdditionalLightsCookieAtlasTextureFormat");

			// Token: 0x040004AF RID: 1199
			public static readonly int additionalLightsCookieEnableBits = Shader.PropertyToID("_AdditionalLightsCookieEnableBits");

			// Token: 0x040004B0 RID: 1200
			public static readonly int additionalLightsCookieAtlasUVRectBuffer = Shader.PropertyToID("_AdditionalLightsCookieAtlasUVRectBuffer");

			// Token: 0x040004B1 RID: 1201
			public static readonly int additionalLightsCookieAtlasUVRects = Shader.PropertyToID("_AdditionalLightsCookieAtlasUVRects");

			// Token: 0x040004B2 RID: 1202
			public static readonly int additionalLightsWorldToLightBuffer = Shader.PropertyToID("_AdditionalLightsWorldToLightBuffer");

			// Token: 0x040004B3 RID: 1203
			public static readonly int additionalLightsLightTypeBuffer = Shader.PropertyToID("_AdditionalLightsLightTypeBuffer");

			// Token: 0x040004B4 RID: 1204
			public static readonly int additionalLightsWorldToLights = Shader.PropertyToID("_AdditionalLightsWorldToLights");

			// Token: 0x040004B5 RID: 1205
			public static readonly int additionalLightsLightTypes = Shader.PropertyToID("_AdditionalLightsLightTypes");
		}

		// Token: 0x020000D3 RID: 211
		private enum LightCookieShaderFormat
		{
			// Token: 0x040004B7 RID: 1207
			None = -1,
			// Token: 0x040004B8 RID: 1208
			RGB,
			// Token: 0x040004B9 RID: 1209
			Alpha,
			// Token: 0x040004BA RID: 1210
			Red
		}

		// Token: 0x020000D4 RID: 212
		public struct Settings
		{
			// Token: 0x0600057C RID: 1404 RVA: 0x00014AA4 File Offset: 0x00012CA4
			public static LightCookieManager.Settings Create()
			{
				LightCookieManager.Settings s;
				s.atlas.resolution = new Vector2Int(1024, 1024);
				s.atlas.format = GraphicsFormat.R8G8B8A8_SRGB;
				s.maxAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
				s.cubeOctahedralSizeScale = 2.5f;
				s.useStructuredBuffer = RenderingUtils.useStructuredBuffer;
				return s;
			}

			// Token: 0x040004BB RID: 1211
			public LightCookieManager.Settings.AtlasSettings atlas;

			// Token: 0x040004BC RID: 1212
			public int maxAdditionalLights;

			// Token: 0x040004BD RID: 1213
			public float cubeOctahedralSizeScale;

			// Token: 0x040004BE RID: 1214
			public bool useStructuredBuffer;

			// Token: 0x020000D5 RID: 213
			public struct AtlasSettings
			{
				// Token: 0x17000152 RID: 338
				// (get) Token: 0x0600057D RID: 1405 RVA: 0x00014AFE File Offset: 0x00012CFE
				public bool isPow2
				{
					get
					{
						return Mathf.IsPowerOfTwo(this.resolution.x) && Mathf.IsPowerOfTwo(this.resolution.y);
					}
				}

				// Token: 0x17000153 RID: 339
				// (get) Token: 0x0600057E RID: 1406 RVA: 0x00014B24 File Offset: 0x00012D24
				public bool isSquare
				{
					get
					{
						return this.resolution.x == this.resolution.y;
					}
				}

				// Token: 0x040004BF RID: 1215
				public Vector2Int resolution;

				// Token: 0x040004C0 RID: 1216
				public GraphicsFormat format;
			}
		}

		// Token: 0x020000D6 RID: 214
		private struct LightCookieMapping
		{
			// Token: 0x040004C1 RID: 1217
			public ushort visibleLightIndex;

			// Token: 0x040004C2 RID: 1218
			public ushort lightBufferIndex;

			// Token: 0x040004C3 RID: 1219
			public Light light;

			// Token: 0x040004C4 RID: 1220
			public static Func<LightCookieManager.LightCookieMapping, LightCookieManager.LightCookieMapping, int> s_CompareByCookieSize = delegate(LightCookieManager.LightCookieMapping a, LightCookieManager.LightCookieMapping b)
			{
				Texture alc = a.light.cookie;
				Texture blc = b.light.cookie;
				int a2 = alc.width * alc.height;
				int d = blc.width * blc.height - a2;
				if (d == 0)
				{
					int instanceID = alc.GetInstanceID();
					int bi = blc.GetInstanceID();
					return instanceID - bi;
				}
				return d;
			};

			// Token: 0x040004C5 RID: 1221
			public static Func<LightCookieManager.LightCookieMapping, LightCookieManager.LightCookieMapping, int> s_CompareByBufferIndex = (LightCookieManager.LightCookieMapping a, LightCookieManager.LightCookieMapping b) => (int)(a.lightBufferIndex - b.lightBufferIndex);
		}

		// Token: 0x020000D8 RID: 216
		private readonly struct WorkSlice<T>
		{
			// Token: 0x06000584 RID: 1412 RVA: 0x00014BE0 File Offset: 0x00012DE0
			public WorkSlice(T[] src, int srcLen = -1)
			{
				this = new LightCookieManager.WorkSlice<T>(src, 0, srcLen);
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x00014BEB File Offset: 0x00012DEB
			public WorkSlice(T[] src, int srcStart, int srcLen = -1)
			{
				this.m_Data = src;
				this.m_Start = srcStart;
				this.m_Length = ((srcLen < 0) ? src.Length : Math.Min(srcLen, src.Length));
			}

			// Token: 0x17000154 RID: 340
			public T this[int index]
			{
				get
				{
					return this.m_Data[this.m_Start + index];
				}
				set
				{
					this.m_Data[this.m_Start + index] = value;
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000588 RID: 1416 RVA: 0x00014C3E File Offset: 0x00012E3E
			public int length
			{
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000589 RID: 1417 RVA: 0x00014C46 File Offset: 0x00012E46
			public int capacity
			{
				get
				{
					return this.m_Data.Length;
				}
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x00014C50 File Offset: 0x00012E50
			public void Sort(Func<T, T, int> compare)
			{
				if (this.m_Length > 1)
				{
					Sorting.QuickSort<T>(this.m_Data, this.m_Start, this.m_Start + this.m_Length - 1, compare);
				}
			}

			// Token: 0x040004C7 RID: 1223
			private readonly T[] m_Data;

			// Token: 0x040004C8 RID: 1224
			private readonly int m_Start;

			// Token: 0x040004C9 RID: 1225
			private readonly int m_Length;
		}

		// Token: 0x020000D9 RID: 217
		private class WorkMemory
		{
			// Token: 0x0600058B RID: 1419 RVA: 0x00014C7C File Offset: 0x00012E7C
			public void Resize(int size)
			{
				int num = size;
				LightCookieManager.LightCookieMapping[] array = this.lightMappings;
				int? num2 = ((array != null) ? new int?(array.Length) : null);
				if ((num <= num2.GetValueOrDefault()) & (num2 != null))
				{
					return;
				}
				size = Math.Max(size, (size + 15) / 16 * 16);
				this.lightMappings = new LightCookieManager.LightCookieMapping[size];
				this.uvRects = new Vector4[size];
			}

			// Token: 0x040004CA RID: 1226
			public LightCookieManager.LightCookieMapping[] lightMappings;

			// Token: 0x040004CB RID: 1227
			public Vector4[] uvRects;
		}

		// Token: 0x020000DA RID: 218
		private class LightCookieShaderData : IDisposable
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x00014CE8 File Offset: 0x00012EE8
			public Matrix4x4[] worldToLights
			{
				get
				{
					return this.m_WorldToLightCpuData;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x0600058E RID: 1422 RVA: 0x00014CF0 File Offset: 0x00012EF0
			public ShaderBitArray cookieEnableBits
			{
				get
				{
					return this.m_CookieEnableBitsCpuData;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x0600058F RID: 1423 RVA: 0x00014CF8 File Offset: 0x00012EF8
			public Vector4[] atlasUVRects
			{
				get
				{
					return this.m_AtlasUVRectCpuData;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x00014D00 File Offset: 0x00012F00
			public float[] lightTypes
			{
				get
				{
					return this.m_LightTypeCpuData;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000591 RID: 1425 RVA: 0x00014D08 File Offset: 0x00012F08
			// (set) Token: 0x06000592 RID: 1426 RVA: 0x00014D10 File Offset: 0x00012F10
			public bool isUploaded { get; set; }

			// Token: 0x06000593 RID: 1427 RVA: 0x00014D19 File Offset: 0x00012F19
			public LightCookieShaderData(int size, bool useStructuredBuffer)
			{
				this.m_UseStructuredBuffer = useStructuredBuffer;
				this.Resize(size);
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00014D2F File Offset: 0x00012F2F
			public void Dispose()
			{
				if (this.m_UseStructuredBuffer)
				{
					ComputeBuffer worldToLightBuffer = this.m_WorldToLightBuffer;
					if (worldToLightBuffer != null)
					{
						worldToLightBuffer.Dispose();
					}
					ComputeBuffer atlasUVRectBuffer = this.m_AtlasUVRectBuffer;
					if (atlasUVRectBuffer != null)
					{
						atlasUVRectBuffer.Dispose();
					}
					ComputeBuffer lightTypeBuffer = this.m_LightTypeBuffer;
					if (lightTypeBuffer == null)
					{
						return;
					}
					lightTypeBuffer.Dispose();
				}
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x00014D6C File Offset: 0x00012F6C
			public void Resize(int size)
			{
				if (size <= this.m_Size)
				{
					return;
				}
				if (this.m_Size > 0)
				{
					this.Dispose();
				}
				this.m_WorldToLightCpuData = new Matrix4x4[size];
				this.m_AtlasUVRectCpuData = new Vector4[size];
				this.m_LightTypeCpuData = new float[size];
				this.m_CookieEnableBitsCpuData.Resize(size);
				if (this.m_UseStructuredBuffer)
				{
					this.m_WorldToLightBuffer = new ComputeBuffer(size, Marshal.SizeOf<Matrix4x4>());
					this.m_AtlasUVRectBuffer = new ComputeBuffer(size, Marshal.SizeOf<Vector4>());
					this.m_LightTypeBuffer = new ComputeBuffer(size, Marshal.SizeOf<float>());
				}
				this.m_Size = size;
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x00014E04 File Offset: 0x00013004
			public void Upload(CommandBuffer cmd)
			{
				if (this.m_UseStructuredBuffer)
				{
					this.m_WorldToLightBuffer.SetData(this.m_WorldToLightCpuData);
					this.m_AtlasUVRectBuffer.SetData(this.m_AtlasUVRectCpuData);
					this.m_LightTypeBuffer.SetData(this.m_LightTypeCpuData);
					cmd.SetGlobalBuffer(LightCookieManager.ShaderProperty.additionalLightsWorldToLightBuffer, this.m_WorldToLightBuffer);
					cmd.SetGlobalBuffer(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasUVRectBuffer, this.m_AtlasUVRectBuffer);
					cmd.SetGlobalBuffer(LightCookieManager.ShaderProperty.additionalLightsLightTypeBuffer, this.m_LightTypeBuffer);
				}
				else
				{
					cmd.SetGlobalMatrixArray(LightCookieManager.ShaderProperty.additionalLightsWorldToLights, this.m_WorldToLightCpuData);
					cmd.SetGlobalVectorArray(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasUVRects, this.m_AtlasUVRectCpuData);
					cmd.SetGlobalFloatArray(LightCookieManager.ShaderProperty.additionalLightsLightTypes, this.m_LightTypeCpuData);
				}
				cmd.SetGlobalFloatArray(LightCookieManager.ShaderProperty.additionalLightsCookieEnableBits, this.m_CookieEnableBitsCpuData.data);
				this.isUploaded = true;
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00014ED1 File Offset: 0x000130D1
			public void Clear(CommandBuffer cmd)
			{
				if (this.isUploaded)
				{
					this.m_CookieEnableBitsCpuData.Clear();
					cmd.SetGlobalFloatArray(LightCookieManager.ShaderProperty.additionalLightsCookieEnableBits, this.m_CookieEnableBitsCpuData.data);
					this.isUploaded = false;
				}
			}

			// Token: 0x040004CC RID: 1228
			private int m_Size;

			// Token: 0x040004CD RID: 1229
			private bool m_UseStructuredBuffer;

			// Token: 0x040004CE RID: 1230
			private Matrix4x4[] m_WorldToLightCpuData;

			// Token: 0x040004CF RID: 1231
			private Vector4[] m_AtlasUVRectCpuData;

			// Token: 0x040004D0 RID: 1232
			private float[] m_LightTypeCpuData;

			// Token: 0x040004D1 RID: 1233
			private ShaderBitArray m_CookieEnableBitsCpuData;

			// Token: 0x040004D2 RID: 1234
			private ComputeBuffer m_WorldToLightBuffer;

			// Token: 0x040004D3 RID: 1235
			private ComputeBuffer m_AtlasUVRectBuffer;

			// Token: 0x040004D4 RID: 1236
			private ComputeBuffer m_LightTypeBuffer;
		}
	}
}
