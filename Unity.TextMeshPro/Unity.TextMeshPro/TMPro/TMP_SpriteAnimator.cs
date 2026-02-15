using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000074 RID: 116
	[DisallowMultipleComponent]
	public class TMP_SpriteAnimator : MonoBehaviour
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00013B26 File Offset: 0x00011D26
		private void Awake()
		{
			this.m_TextComponent = base.GetComponent<TMP_Text>();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00002AAB File Offset: 0x00000CAB
		private void OnEnable()
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00002AAB File Offset: 0x00000CAB
		private void OnDisable()
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00013B34 File Offset: 0x00011D34
		public void StopAllAnimations()
		{
			base.StopAllCoroutines();
			this.m_animations.Clear();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00013B48 File Offset: 0x00011D48
		public void DoSpriteAnimation(int currentCharacter, TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			bool isPlaying;
			if (!this.m_animations.TryGetValue(currentCharacter, out isPlaying))
			{
				base.StartCoroutine(this.DoSpriteAnimationInternal(currentCharacter, spriteAsset, start, end, framerate));
				this.m_animations.Add(currentCharacter, true);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00013B86 File Offset: 0x00011D86
		private IEnumerator DoSpriteAnimationInternal(int currentCharacter, TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			if (this.m_TextComponent == null)
			{
				yield break;
			}
			yield return null;
			int currentFrame = start;
			if (end > spriteAsset.spriteCharacterTable.Count)
			{
				end = spriteAsset.spriteCharacterTable.Count - 1;
			}
			TMP_CharacterInfo charInfo = this.m_TextComponent.textInfo.characterInfo[currentCharacter];
			int materialIndex = charInfo.materialReferenceIndex;
			int vertexIndex = charInfo.vertexIndex;
			TMP_MeshInfo meshInfo = this.m_TextComponent.textInfo.meshInfo[materialIndex];
			float baseSpriteScale = spriteAsset.spriteCharacterTable[start].scale * spriteAsset.spriteCharacterTable[start].glyph.scale;
			float elapsedTime = 0f;
			float targetTime = 1f / (float)Mathf.Abs(framerate);
			for (;;)
			{
				if (elapsedTime > targetTime)
				{
					elapsedTime = 0f;
					uint character = (uint)this.m_TextComponent.textInfo.characterInfo[currentCharacter].character;
					if (character == 3U || character == 8230U)
					{
						break;
					}
					TMP_SpriteCharacter spriteCharacter = spriteAsset.spriteCharacterTable[currentFrame];
					Vector3[] vertices = meshInfo.vertices;
					Vector2 origin = new Vector2(charInfo.origin, charInfo.baseLine);
					float spriteScale = charInfo.scale / baseSpriteScale * spriteCharacter.scale * spriteCharacter.glyph.scale;
					Vector3 bl = new Vector3(origin.x + spriteCharacter.glyph.metrics.horizontalBearingX * spriteScale, origin.y + (spriteCharacter.glyph.metrics.horizontalBearingY - spriteCharacter.glyph.metrics.height) * spriteScale);
					Vector3 tl = new Vector3(bl.x, origin.y + spriteCharacter.glyph.metrics.horizontalBearingY * spriteScale);
					Vector3 tr = new Vector3(origin.x + (spriteCharacter.glyph.metrics.horizontalBearingX + spriteCharacter.glyph.metrics.width) * spriteScale, tl.y);
					Vector3 br = new Vector3(tr.x, bl.y);
					vertices[vertexIndex] = bl;
					vertices[vertexIndex + 1] = tl;
					vertices[vertexIndex + 2] = tr;
					vertices[vertexIndex + 3] = br;
					Vector4[] uvs0 = meshInfo.uvs0;
					Vector2 uv0 = new Vector2((float)spriteCharacter.glyph.glyphRect.x / (float)spriteAsset.spriteSheet.width, (float)spriteCharacter.glyph.glyphRect.y / (float)spriteAsset.spriteSheet.height);
					Vector2 uv = new Vector2(uv0.x, (float)(spriteCharacter.glyph.glyphRect.y + spriteCharacter.glyph.glyphRect.height) / (float)spriteAsset.spriteSheet.height);
					Vector2 uv2 = new Vector2((float)(spriteCharacter.glyph.glyphRect.x + spriteCharacter.glyph.glyphRect.width) / (float)spriteAsset.spriteSheet.width, uv.y);
					Vector2 uv3 = new Vector2(uv2.x, uv0.y);
					uvs0[vertexIndex] = uv0;
					uvs0[vertexIndex + 1] = uv;
					uvs0[vertexIndex + 2] = uv2;
					uvs0[vertexIndex + 3] = uv3;
					meshInfo.mesh.vertices = vertices;
					meshInfo.mesh.SetUVs(0, uvs0);
					this.m_TextComponent.UpdateGeometry(meshInfo.mesh, materialIndex);
					if (framerate > 0)
					{
						if (currentFrame < end)
						{
							currentFrame++;
						}
						else
						{
							currentFrame = start;
						}
					}
					else if (currentFrame > start)
					{
						currentFrame--;
					}
					else
					{
						currentFrame = end;
					}
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			this.m_animations.Remove(currentCharacter);
			yield break;
			yield break;
		}

		// Token: 0x04000384 RID: 900
		private Dictionary<int, bool> m_animations = new Dictionary<int, bool>(16);

		// Token: 0x04000385 RID: 901
		private TMP_Text m_TextComponent;
	}
}
