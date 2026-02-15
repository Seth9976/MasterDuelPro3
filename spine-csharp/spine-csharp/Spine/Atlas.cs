using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Spine
{
	// Token: 0x02000041 RID: 65
	public class Atlas : IEnumerable<AtlasRegion>, IEnumerable
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000095E6 File Offset: 0x000077E6
		public IEnumerator<AtlasRegion> GetEnumerator()
		{
			return this.regions.GetEnumerator();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000095E6 File Offset: 0x000077E6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.regions.GetEnumerator();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000095F8 File Offset: 0x000077F8
		public List<AtlasRegion> Regions
		{
			get
			{
				return this.regions;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009600 File Offset: 0x00007800
		public List<AtlasPage> Pages
		{
			get
			{
				return this.pages;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009608 File Offset: 0x00007808
		public Atlas(List<AtlasPage> pages, List<AtlasRegion> regions)
		{
			if (pages == null)
			{
				throw new ArgumentNullException("pages", "pages cannot be null.");
			}
			if (regions == null)
			{
				throw new ArgumentNullException("regions", "regions cannot be null.");
			}
			this.pages = pages;
			this.regions = regions;
			this.textureLoader = null;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000966C File Offset: 0x0000786C
		public Atlas(TextReader reader, string imagesDir, TextureLoader textureLoader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader", "reader cannot be null.");
			}
			if (imagesDir == null)
			{
				throw new ArgumentNullException("imagesDir", "imagesDir cannot be null.");
			}
			if (textureLoader == null)
			{
				throw new ArgumentNullException("textureLoader", "textureLoader cannot be null.");
			}
			this.textureLoader = textureLoader;
			string[] entry = new string[5];
			AtlasPage page = null;
			AtlasRegion region = null;
			Dictionary<string, Action> pageFields = new Dictionary<string, Action>(5);
			pageFields.Add("size", delegate
			{
				page.width = int.Parse(entry[1], CultureInfo.InvariantCulture);
				page.height = int.Parse(entry[2], CultureInfo.InvariantCulture);
			});
			pageFields.Add("format", delegate
			{
				page.format = (Format)Enum.Parse(typeof(Format), entry[1], false);
			});
			pageFields.Add("filter", delegate
			{
				page.minFilter = (TextureFilter)Enum.Parse(typeof(TextureFilter), entry[1], false);
				page.magFilter = (TextureFilter)Enum.Parse(typeof(TextureFilter), entry[2], false);
			});
			pageFields.Add("repeat", delegate
			{
				if (entry[1].IndexOf('x') != -1)
				{
					page.uWrap = TextureWrap.Repeat;
				}
				if (entry[1].IndexOf('y') != -1)
				{
					page.vWrap = TextureWrap.Repeat;
				}
			});
			pageFields.Add("pma", delegate
			{
				page.pma = entry[1] == "true";
			});
			Dictionary<string, Action> regionFields = new Dictionary<string, Action>(8);
			regionFields.Add("xy", delegate
			{
				region.x = int.Parse(entry[1], CultureInfo.InvariantCulture);
				region.y = int.Parse(entry[2], CultureInfo.InvariantCulture);
			});
			regionFields.Add("size", delegate
			{
				region.width = int.Parse(entry[1], CultureInfo.InvariantCulture);
				region.height = int.Parse(entry[2], CultureInfo.InvariantCulture);
			});
			regionFields.Add("bounds", delegate
			{
				region.x = int.Parse(entry[1], CultureInfo.InvariantCulture);
				region.y = int.Parse(entry[2], CultureInfo.InvariantCulture);
				region.width = int.Parse(entry[3], CultureInfo.InvariantCulture);
				region.height = int.Parse(entry[4], CultureInfo.InvariantCulture);
			});
			regionFields.Add("offset", delegate
			{
				region.offsetX = (float)int.Parse(entry[1], CultureInfo.InvariantCulture);
				region.offsetY = (float)int.Parse(entry[2], CultureInfo.InvariantCulture);
			});
			regionFields.Add("orig", delegate
			{
				region.originalWidth = int.Parse(entry[1], CultureInfo.InvariantCulture);
				region.originalHeight = int.Parse(entry[2], CultureInfo.InvariantCulture);
			});
			regionFields.Add("offsets", delegate
			{
				region.offsetX = (float)int.Parse(entry[1], CultureInfo.InvariantCulture);
				region.offsetY = (float)int.Parse(entry[2], CultureInfo.InvariantCulture);
				region.originalWidth = int.Parse(entry[3], CultureInfo.InvariantCulture);
				region.originalHeight = int.Parse(entry[4], CultureInfo.InvariantCulture);
			});
			regionFields.Add("rotate", delegate
			{
				string value = entry[1];
				if (value == "true")
				{
					region.degrees = 90;
					return;
				}
				if (value != "false")
				{
					region.degrees = int.Parse(value, CultureInfo.InvariantCulture);
				}
			});
			regionFields.Add("index", delegate
			{
				region.index = int.Parse(entry[1], CultureInfo.InvariantCulture);
			});
			string line = reader.ReadLine();
			while (line != null && line.Trim().Length == 0)
			{
				line = reader.ReadLine();
			}
			while (line != null && line.Trim().Length != 0 && Atlas.ReadEntry(entry, line) != 0)
			{
				line = reader.ReadLine();
			}
			List<string> names = null;
			List<int[]> values = null;
			while (line != null)
			{
				if (line.Trim().Length == 0)
				{
					page = null;
					line = reader.ReadLine();
				}
				else if (page == null)
				{
					page = new AtlasPage();
					page.name = line.Trim();
					while (Atlas.ReadEntry(entry, line = reader.ReadLine()) != 0)
					{
						Action field;
						if (pageFields.TryGetValue(entry[0], out field))
						{
							field();
						}
					}
					textureLoader.Load(page, Path.Combine(imagesDir, page.name));
					this.pages.Add(page);
				}
				else
				{
					region = new AtlasRegion();
					region.page = page;
					region.name = line;
					for (;;)
					{
						int count = Atlas.ReadEntry(entry, line = reader.ReadLine());
						if (count == 0)
						{
							break;
						}
						Action field2;
						if (regionFields.TryGetValue(entry[0], out field2))
						{
							field2();
						}
						else
						{
							if (names == null)
							{
								names = new List<string>(8);
								values = new List<int[]>(8);
							}
							names.Add(entry[0]);
							int[] entryValues = new int[count];
							for (int i = 0; i < count; i++)
							{
								int.TryParse(entry[i + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out entryValues[i]);
							}
							values.Add(entryValues);
						}
					}
					if (region.originalWidth == 0 && region.originalHeight == 0)
					{
						region.originalWidth = region.width;
						region.originalHeight = region.height;
					}
					if (names != null && names.Count > 0)
					{
						region.names = names.ToArray();
						region.values = values.ToArray();
						names.Clear();
						values.Clear();
					}
					region.u = (float)region.x / (float)page.width;
					region.v = (float)region.y / (float)page.height;
					if (region.degrees == 90)
					{
						region.u2 = (float)(region.x + region.height) / (float)page.width;
						region.v2 = (float)(region.y + region.width) / (float)page.height;
						int tempSwap = region.packedWidth;
						region.packedWidth = region.packedHeight;
						region.packedHeight = tempSwap;
					}
					else
					{
						region.u2 = (float)(region.x + region.width) / (float)page.width;
						region.v2 = (float)(region.y + region.height) / (float)page.height;
					}
					this.regions.Add(region);
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009BDC File Offset: 0x00007DDC
		private static int ReadEntry(string[] entry, string line)
		{
			if (line == null)
			{
				return 0;
			}
			line = line.Trim();
			if (line.Length == 0)
			{
				return 0;
			}
			int colon = line.IndexOf(':');
			if (colon == -1)
			{
				return 0;
			}
			entry[0] = line.Substring(0, colon).Trim();
			int i = 1;
			int lastMatch = colon + 1;
			for (;;)
			{
				int comma = line.IndexOf(',', lastMatch);
				if (comma == -1)
				{
					break;
				}
				entry[i] = line.Substring(lastMatch, comma - lastMatch).Trim();
				lastMatch = comma + 1;
				if (i == 4)
				{
					return 4;
				}
				i++;
			}
			entry[i] = line.Substring(lastMatch).Trim();
			return i;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009C68 File Offset: 0x00007E68
		public void FlipV()
		{
			int i = 0;
			int j = this.regions.Count;
			while (i < j)
			{
				AtlasRegion region = this.regions[i];
				region.v = 1f - region.v;
				region.v2 = 1f - region.v2;
				i++;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public AtlasRegion FindRegion(string name)
		{
			int i = 0;
			int j = this.regions.Count;
			while (i < j)
			{
				if (this.regions[i].name == name)
				{
					return this.regions[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009D0C File Offset: 0x00007F0C
		public void Dispose()
		{
			if (this.textureLoader == null)
			{
				return;
			}
			int i = 0;
			int j = this.pages.Count;
			while (i < j)
			{
				this.textureLoader.Unload(this.pages[i].rendererObject);
				i++;
			}
		}

		// Token: 0x040000F9 RID: 249
		private readonly List<AtlasPage> pages = new List<AtlasPage>();

		// Token: 0x040000FA RID: 250
		private List<AtlasRegion> regions = new List<AtlasRegion>();

		// Token: 0x040000FB RID: 251
		private TextureLoader textureLoader;
	}
}
