using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x020012AA RID: 4778
	public static class MaterialLoader
	{
		// Token: 0x06008C22 RID: 35874 RVA: 0x00120AF0 File Offset: 0x0011ECF0
		[RuntimeInitializeOnLoadMethod]
		public static async UniTask LoadCardMaterials()
		{
			await UniTask.WaitUntil(() => TextureManager.loaded && TextureManager.container != null, PlayerLoopTiming.Update, default(CancellationToken), false);
			MaterialLoader.cardMatNormalUI = ABLoader.LoadMasterDuelMaterial("NormalStyleUI");
			MaterialLoader.cardMatShineUI = ABLoader.LoadMasterDuelMaterial("ShineStyleUI");
			MaterialLoader.cardMatRoyalUI = ABLoader.LoadMasterDuelMaterial("RoyalStyleUI");
			MaterialLoader.cardMatGoldUI = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatRoyalUI);
			MaterialLoader.cardMatGoldUI.SetFloat("_CardDistortion01", 1.2f);
			MaterialLoader.cardMatGoldUI.SetFloat("_Kira01_01Tile", 0.25f);
			MaterialLoader.cardMatGoldUI.SetFloat("_Kira01_01Power", 3f);
			MaterialLoader.cardMatGoldUI.SetColor("_KiraColor02", new Color(0.5f, 0.5f, 0f, 0f));
			MaterialLoader.cardMatGoldUI.SetColor("_CubemapColor", new Color(0.7f, 0.7f, 0f, 0f));
			MaterialLoader.cardMatMillenniumUI = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatRoyalUI);
			MaterialLoader.cardMatMillenniumUI.SetTexture("_HighlightNormal", TextureManager.container.CardKiraNormal03_Millennium);
			MaterialLoader.cardMatMillenniumUI.SetColor("_CubemapColor", new Color(0.898f, 0.3245f, 0.7723f, 0f));
			MaterialLoader.cardMatMillenniumUI.SetColor("_KiraColor02", new Color(0.3099f, 0.1633f, 0.2753f, 0f));
			MaterialLoader.cardMatMillenniumUI.SetFloat("_Kira01_01Tile", 0.25f);
			MaterialLoader.cardMatMillenniumUI.SetFloat("_Kira01_02Tile", 0f);
			MaterialLoader.cardMatMillenniumUI.SetFloat("_RanbowPower", 0.5f);
			MaterialLoader.cardMatShineRDUI = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatShineUI);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatShineRDUI);
			MaterialLoader.cardMatRoyalRDUI = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatRoyalUI);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatRoyalRDUI);
			MaterialLoader.cardMatGoldRDUI = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatGoldUI);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatGoldRDUI);
			MaterialLoader.cardMatMillenniumRDUI = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatMillenniumUI);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatMillenniumRDUI);
			MaterialLoader.cardMatNormal3D = ABLoader.LoadMasterDuelMaterial("NormalStyle3D");
			MaterialLoader.cardMatShine3D = ABLoader.LoadMasterDuelMaterial("ShineStyle3D");
			MaterialLoader.cardMatRoyal3D = ABLoader.LoadMasterDuelMaterial("RoyalStyle3D");
			MaterialLoader.cardMatGold3D = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatRoyal3D);
			MaterialLoader.cardMatGold3D.SetFloat("_CardDistortion01", 1.2f);
			MaterialLoader.cardMatGold3D.SetFloat("_Kira01_01Tile", 0.25f);
			MaterialLoader.cardMatGold3D.SetFloat("_Kira01_01Power", 3f);
			MaterialLoader.cardMatGold3D.SetColor("_KiraColor02", new Color(0.5f, 0.5f, 0f, 0f));
			MaterialLoader.cardMatGold3D.SetColor("_CubemapColor", new Color(0.7f, 0.7f, 0f, 0f));
			MaterialLoader.cardMatMillennium3D = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatRoyal3D);
			MaterialLoader.cardMatMillennium3D.SetTexture("_HighlightNormal", TextureManager.container.CardKiraNormal03_Millennium);
			MaterialLoader.cardMatMillennium3D.SetColor("_CubemapColor", new Color(0.898f, 0.3245f, 0.7723f, 0f));
			MaterialLoader.cardMatMillennium3D.SetColor("_KiraColor02", new Color(0.3099f, 0.1633f, 0.2753f, 0f));
			MaterialLoader.cardMatMillennium3D.SetFloat("_Kira01_01Tile", 0.25f);
			MaterialLoader.cardMatMillennium3D.SetFloat("_Kira01_02Tile", 0f);
			MaterialLoader.cardMatMillennium3D.SetFloat("_RanbowPower", 0.5f);
			MaterialLoader.cardMatShineRD3D = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatShine3D);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatShineRD3D);
			MaterialLoader.cardMatRoyalRD3D = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatRoyal3D);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatRoyalRD3D);
			MaterialLoader.cardMatGoldRD3D = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatGold3D);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatGoldRD3D);
			MaterialLoader.cardMatMillenniumRD3D = global::UnityEngine.Object.Instantiate<Material>(MaterialLoader.cardMatMillennium3D);
			MaterialLoader.MaterialToRD(MaterialLoader.cardMatMillenniumRD3D);
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x00120B2C File Offset: 0x0011ED2C
		private static void MaterialToRD(Material material)
		{
			material.SetTexture("_FrameMask", TextureManager.container.rd_Mask);
			material.SetTexture("_KiraMask", TextureManager.container.rd_KiraMask);
			material.SetTexture("_MainNormal", TextureManager.container.rd_CardNormal);
			material.SetTexture("_AttributeTex", TextureManager.container.rd_CardAttributeSet);
			material.SetVector("_AttributeSize_Pos", new Vector4(8.31f, 12.26f, -3.19f, -5.13f));
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		private static Color GetMillenniumFrameColor(Card data)
		{
			Color color;
			if (data.HasType(CardType.Pendulum))
			{
				color = new Color(0.3099f, 0.1633f, 0.2753f, 0f);
			}
			else if (data.HasType(CardType.Spell))
			{
				color = new Color(0f, 0.8867f, 1f, 0f);
			}
			else if (data.HasType(CardType.Trap))
			{
				color = new Color(1f, 0f, 1f, 0f);
			}
			else if (data.HasType(CardType.Normal))
			{
				color = new Color(1f, 0.6f, 0f, 0f);
			}
			else if (data.HasType(CardType.Fusion))
			{
				color = new Color(1f, 0f, 1f, 0f);
			}
			else if (data.HasType(CardType.Ritual))
			{
				color = new Color(0f, 0.2f, 1f, 0f);
			}
			else if (data.HasType(CardType.Synchro))
			{
				color = new Color(0.4f, 0.4f, 0.4f, 0f);
			}
			else if (data.HasType(CardType.Xyz))
			{
				color = new Color(0.1f, 0.1f, 0.1f, 0f);
			}
			else if (data.HasType(CardType.Link))
			{
				color = new Color(0f, 0.4f, 1f, 0f);
			}
			else
			{
				color = new Color(1f, 0.2357f, 0f, 0f);
			}
			return color;
		}

		// Token: 0x06008C25 RID: 35877 RVA: 0x00120D5C File Offset: 0x0011EF5C
		private static Color GetMillenniumNameColor(Card data)
		{
			if (data.HasType(CardType.Spell))
			{
				return new Color(0f, 1f, 1f, 1f);
			}
			if (data.HasType(CardType.Trap))
			{
				return new Color(1f, 0f, 0.5f, 1f);
			}
			if (((long)data.Attribute & 16L) > 0L)
			{
				return new Color(1f, 1f, 0f, 1f);
			}
			if (((long)data.Attribute & 64L) > 0L)
			{
				return new Color(1f, 1f, 0f, 1f);
			}
			if (((long)data.Attribute & 32L) > 0L)
			{
				return new Color(1f, 0f, 1f, 1f);
			}
			if (((long)data.Attribute & 2L) > 0L)
			{
				return new Color(0f, 1f, 1f, 1f);
			}
			if (((long)data.Attribute & 4L) > 0L)
			{
				return new Color(1f, 0f, 0f, 1f);
			}
			if (((long)data.Attribute & 1L) > 0L)
			{
				return new Color(0.8f, 0.8f, 0.8f, 1f);
			}
			if (((long)data.Attribute & 8L) > 0L)
			{
				return new Color(0f, 1f, 0f, 1f);
			}
			return new Color(1f, 1f, 0f, 1f);
		}

		// Token: 0x06008C26 RID: 35878 RVA: 0x00120EE4 File Offset: 0x0011F0E4
		public static Material GetCardMaterial(int code, bool use3D = false)
		{
			Material mat = null;
			if (code < 0)
			{
				return global::UnityEngine.Object.Instantiate<Material>(use3D ? MaterialLoader.cardMatNormal3D : MaterialLoader.cardMatNormalUI);
			}
			bool rushDuel = CardRenderer.NeedRushDuelStyle(code);
			CardRarity.Rarity rarity = CardRarity.GetRarity(code);
			bool needSet = true;
			switch (rarity)
			{
			case CardRarity.Rarity.Normal:
				mat = global::UnityEngine.Object.Instantiate<Material>(use3D ? MaterialLoader.cardMatNormal3D : MaterialLoader.cardMatNormalUI);
				needSet = false;
				break;
			case CardRarity.Rarity.Shine:
				mat = global::UnityEngine.Object.Instantiate<Material>(rushDuel ? (use3D ? MaterialLoader.cardMatShineRD3D : MaterialLoader.cardMatShineRDUI) : (use3D ? MaterialLoader.cardMatShine3D : MaterialLoader.cardMatShineUI));
				break;
			case (CardRarity.Rarity)3:
				break;
			case CardRarity.Rarity.Royal:
				mat = global::UnityEngine.Object.Instantiate<Material>(rushDuel ? (use3D ? MaterialLoader.cardMatRoyalRD3D : MaterialLoader.cardMatRoyalRDUI) : (use3D ? MaterialLoader.cardMatRoyal3D : MaterialLoader.cardMatRoyalUI));
				break;
			default:
				if (rarity != CardRarity.Rarity.Gold)
				{
					if (rarity == CardRarity.Rarity.Millennium)
					{
						mat = global::UnityEngine.Object.Instantiate<Material>(rushDuel ? (use3D ? MaterialLoader.cardMatMillenniumRD3D : MaterialLoader.cardMatMillenniumRDUI) : (use3D ? MaterialLoader.cardMatMillennium3D : MaterialLoader.cardMatMillenniumUI));
					}
				}
				else
				{
					mat = global::UnityEngine.Object.Instantiate<Material>(rushDuel ? (use3D ? MaterialLoader.cardMatGoldRD3D : MaterialLoader.cardMatGoldRDUI) : (use3D ? MaterialLoader.cardMatGold3D : MaterialLoader.cardMatGoldUI));
				}
				break;
			}
			if (needSet)
			{
				Card data = CardsManager.Get(code, false);
				if (data.HasType(CardType.Spell))
				{
					mat.SetFloat("_AttributeTile", 7f);
				}
				else if (data.HasType(CardType.Trap))
				{
					mat.SetFloat("_AttributeTile", 8f);
				}
				else if (((long)data.Attribute & 16L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 0f);
				}
				else if (((long)data.Attribute & 32L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 1f);
				}
				else if (((long)data.Attribute & 2L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 2f);
				}
				else if (((long)data.Attribute & 4L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 3f);
				}
				else if (((long)data.Attribute & 1L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 4f);
				}
				else if (((long)data.Attribute & 8L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 5f);
				}
				else if (((long)data.Attribute & 64L) > 0L)
				{
					mat.SetFloat("_AttributeTile", 6f);
				}
				Texture2D nameTask = CardImageLoader.LoadCardName(code);
				mat.SetTexture("_MonsterNameTex", nameTask);
				if (rushDuel)
				{
					if (data.HasType(CardType.Pendulum))
					{
						mat.SetTexture("_KiraMask", TextureManager.container.rd_KiraMaskPendulum);
					}
				}
				else
				{
					if (data.HasType(CardType.Link))
					{
						mat.SetTexture("_FrameMask", TextureManager.container.cardFrameMaskLink);
						mat.SetTexture("_KiraMask", TextureManager.container.cardKiraMaskLink);
						mat.SetTexture("_MainNormal", TextureManager.container.cardNormalLink);
						if (rarity == CardRarity.Rarity.Shine)
						{
							mat.SetFloat("_LinkOn_Off", 1f);
						}
					}
					else if (data.HasType(CardType.Pendulum))
					{
						mat.SetTexture("_FrameMask", TextureManager.container.cardFrameMaskPendulum);
						mat.SetTexture("_KiraMask", TextureManager.container.cardKiraMaskPendulum);
						mat.SetTexture("_MainNormal", TextureManager.container.cardNormalPendulum);
					}
					if (Language.AttributeNeedRuby())
					{
						mat.SetVector("_AttributeSize_Pos", new Vector4(9.85f, 13.96f, -3.7f, -5.81f));
					}
				}
				if (rarity == CardRarity.Rarity.Millennium)
				{
					mat.SetColor("_KiraColor02", MaterialLoader.GetMillenniumFrameColor(data));
					mat.SetColor("_CubemapColor", MaterialLoader.GetMillenniumNameColor(data));
				}
			}
			return mat;
		}

		// Token: 0x06008C27 RID: 35879 RVA: 0x00121294 File Offset: 0x0011F494
		private static async Task<Material> LoadMaterialAsync(string materialName, CancellationToken token)
		{
			Material mat = await ABLoader.LoadMaterialAsync("MasterDuel/Material/" + materialName, token);
			MaterialLoader._loadedMaterials.TryAdd(materialName, mat);
			Task<Material> task;
			MaterialLoader._loadMaterialTasks.TryRemove(materialName, out task);
			return mat;
		}

		// Token: 0x06008C28 RID: 35880 RVA: 0x001212E0 File Offset: 0x0011F4E0
		public static async UniTask<Material> LoadMaterialByNameAsync(string materialName)
		{
			Material material;
			Material material2;
			Task<Material> task;
			if (MaterialLoader._loadedMaterials.TryGetValue(materialName, out material))
			{
				material2 = material;
			}
			else if (MaterialLoader._loadMaterialTasks.TryGetValue(materialName, out task))
			{
				material2 = await task;
			}
			else
			{
				using (CancellationTokenSource cts = new CancellationTokenSource())
				{
					task = MaterialLoader.LoadMaterialAsync(materialName, cts.Token);
					if (MaterialLoader._loadMaterialTasks.TryAdd(materialName, task))
					{
						material2 = await task;
					}
					else
					{
						cts.Cancel();
						material2 = await MaterialLoader._loadMaterialTasks[materialName];
					}
				}
			}
			return material2;
		}

		// Token: 0x06008C29 RID: 35881 RVA: 0x00121324 File Offset: 0x0011F524
		private static async UniTask<Shader> LoadShaderAsync(string shaderName, CancellationToken token)
		{
			UniTask<Shader> uniTask = ABLoader.LoadShaderAsync("MasterDuel/Shader/" + shaderName, token);
			Shader shader = await uniTask;
			MaterialLoader._loadedShaders.TryAdd(shaderName, shader);
			MaterialLoader._loadShaderTasks.TryRemove(shaderName, out uniTask);
			return shader;
		}

		// Token: 0x06008C2A RID: 35882 RVA: 0x00121370 File Offset: 0x0011F570
		public static async UniTask<Shader> LoadShaderByNameAsync(string shaderName)
		{
			Shader shader;
			Shader shader2;
			UniTask<Shader> task;
			if (MaterialLoader._loadedShaders.TryGetValue(shaderName, out shader))
			{
				shader2 = shader;
			}
			else if (MaterialLoader._loadShaderTasks.TryGetValue(shaderName, out task))
			{
				shader2 = await task;
			}
			else
			{
				using (CancellationTokenSource cts = new CancellationTokenSource())
				{
					task = MaterialLoader.LoadShaderAsync(shaderName, cts.Token);
					if (MaterialLoader._loadShaderTasks.TryAdd(shaderName, task))
					{
						shader2 = await task;
					}
					else
					{
						cts.Cancel();
						shader2 = await MaterialLoader._loadShaderTasks[shaderName];
					}
				}
			}
			return shader2;
		}

		// Token: 0x0400C9C0 RID: 51648
		private static Material cardMatNormalUI;

		// Token: 0x0400C9C1 RID: 51649
		private static Material cardMatShineUI;

		// Token: 0x0400C9C2 RID: 51650
		private static Material cardMatShineRDUI;

		// Token: 0x0400C9C3 RID: 51651
		private static Material cardMatRoyalUI;

		// Token: 0x0400C9C4 RID: 51652
		private static Material cardMatRoyalRDUI;

		// Token: 0x0400C9C5 RID: 51653
		private static Material cardMatGoldUI;

		// Token: 0x0400C9C6 RID: 51654
		private static Material cardMatGoldRDUI;

		// Token: 0x0400C9C7 RID: 51655
		private static Material cardMatMillenniumUI;

		// Token: 0x0400C9C8 RID: 51656
		private static Material cardMatMillenniumRDUI;

		// Token: 0x0400C9C9 RID: 51657
		private static Material cardMatNormal3D;

		// Token: 0x0400C9CA RID: 51658
		private static Material cardMatShine3D;

		// Token: 0x0400C9CB RID: 51659
		private static Material cardMatShineRD3D;

		// Token: 0x0400C9CC RID: 51660
		private static Material cardMatRoyal3D;

		// Token: 0x0400C9CD RID: 51661
		private static Material cardMatRoyalRD3D;

		// Token: 0x0400C9CE RID: 51662
		private static Material cardMatGold3D;

		// Token: 0x0400C9CF RID: 51663
		private static Material cardMatGoldRD3D;

		// Token: 0x0400C9D0 RID: 51664
		private static Material cardMatMillennium3D;

		// Token: 0x0400C9D1 RID: 51665
		private static Material cardMatMillenniumRD3D;

		// Token: 0x0400C9D2 RID: 51666
		private static readonly ConcurrentDictionary<string, Material> _loadedMaterials = new ConcurrentDictionary<string, Material>();

		// Token: 0x0400C9D3 RID: 51667
		private static readonly ConcurrentDictionary<string, Task<Material>> _loadMaterialTasks = new ConcurrentDictionary<string, Task<Material>>();

		// Token: 0x0400C9D4 RID: 51668
		private static readonly ConcurrentDictionary<string, Shader> _loadedShaders = new ConcurrentDictionary<string, Shader>();

		// Token: 0x0400C9D5 RID: 51669
		private static readonly ConcurrentDictionary<string, UniTask<Shader>> _loadShaderTasks = new ConcurrentDictionary<string, UniTask<Shader>>();
	}
}
