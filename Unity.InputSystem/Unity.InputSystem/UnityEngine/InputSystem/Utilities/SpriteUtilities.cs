using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000263 RID: 611
	internal static class SpriteUtilities
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x00063EC0 File Offset: 0x000620C0
		public unsafe static Sprite CreateCircleSprite(int radius, Color32 colour)
		{
			int d = radius * 2;
			Texture2D texture = new Texture2D(d, d, DefaultFormat.LDR, TextureCreationFlags.None);
			NativeArray<Color32> colours = texture.GetRawTextureData<Color32>();
			Color32* coloursPtr = (Color32*)colours.GetUnsafePtr<Color32>();
			UnsafeUtility.MemSet((void*)coloursPtr, 0, (long)(colours.Length * UnsafeUtility.SizeOf<Color32>()));
			uint* colorPtr = (uint*)UnsafeUtility.AddressOf<Color32>(ref colour);
			ulong colourAsULong = (ulong)((*(long*)colorPtr << 32) | (long)((ulong)(*colorPtr)));
			float rSquared = (float)(radius * radius);
			for (int y = -radius; y < radius; y++)
			{
				int halfWidth = (int)Mathf.Sqrt(rSquared - (float)(y * y));
				Color32* ptr = coloursPtr + (y + radius) * d + radius - halfWidth;
				for (int x = 0; x < halfWidth; x++)
				{
					*(long*)ptr = (long)colourAsULong;
					ptr += 2;
				}
			}
			texture.Apply();
			return Sprite.Create(texture, new Rect(0f, 0f, (float)d, (float)d), new Vector2((float)radius, (float)radius), 1f, 0U, SpriteMeshType.FullRect);
		}
	}
}
