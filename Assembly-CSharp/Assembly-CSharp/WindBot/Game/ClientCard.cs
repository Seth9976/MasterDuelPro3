using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
	// Token: 0x020001F1 RID: 497
	public class ClientCard
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00028B64 File Offset: 0x00026D64
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00028B6C File Offset: 0x00026D6C
		public int Id { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00028B75 File Offset: 0x00026D75
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00028B7D File Offset: 0x00026D7D
		public NamedCard Data { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00028B86 File Offset: 0x00026D86
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00028B8E File Offset: 0x00026D8E
		public string Name { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00028B97 File Offset: 0x00026D97
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00028B9F File Offset: 0x00026D9F
		public int Position { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00028BA8 File Offset: 0x00026DA8
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00028BB0 File Offset: 0x00026DB0
		public int Sequence { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00028BB9 File Offset: 0x00026DB9
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00028BC1 File Offset: 0x00026DC1
		public CardLocation Location { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00028BCA File Offset: 0x00026DCA
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00028BD2 File Offset: 0x00026DD2
		public CardLocation LastLocation { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00028BDB File Offset: 0x00026DDB
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00028BE3 File Offset: 0x00026DE3
		public int Alias { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00028BEC File Offset: 0x00026DEC
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00028BF4 File Offset: 0x00026DF4
		public int Level { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00028BFD File Offset: 0x00026DFD
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00028C05 File Offset: 0x00026E05
		public int Rank { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00028C0E File Offset: 0x00026E0E
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00028C16 File Offset: 0x00026E16
		public int Type { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00028C1F File Offset: 0x00026E1F
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00028C27 File Offset: 0x00026E27
		public int Attribute { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00028C30 File Offset: 0x00026E30
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00028C38 File Offset: 0x00026E38
		public int Race { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00028C41 File Offset: 0x00026E41
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00028C49 File Offset: 0x00026E49
		public int Attack { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00028C52 File Offset: 0x00026E52
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00028C5A File Offset: 0x00026E5A
		public int Defense { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00028C63 File Offset: 0x00026E63
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00028C6B File Offset: 0x00026E6B
		public int LScale { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00028C74 File Offset: 0x00026E74
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00028C7C File Offset: 0x00026E7C
		public int RScale { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00028C85 File Offset: 0x00026E85
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00028C8D File Offset: 0x00026E8D
		public int LinkCount { get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00028C96 File Offset: 0x00026E96
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00028C9E File Offset: 0x00026E9E
		public int LinkMarker { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00028CA7 File Offset: 0x00026EA7
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00028CAF File Offset: 0x00026EAF
		public int BaseAttack { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00028CB8 File Offset: 0x00026EB8
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00028CC0 File Offset: 0x00026EC0
		public int BaseDefense { get; private set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00028CC9 File Offset: 0x00026EC9
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00028CD1 File Offset: 0x00026ED1
		public int RealPower { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00028CDA File Offset: 0x00026EDA
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00028CE2 File Offset: 0x00026EE2
		public List<int> Overlays { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00028CEB File Offset: 0x00026EEB
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00028CF3 File Offset: 0x00026EF3
		public int Owner { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00028CFC File Offset: 0x00026EFC
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x00028D04 File Offset: 0x00026F04
		public int Controller { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00028D0D File Offset: 0x00026F0D
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00028D15 File Offset: 0x00026F15
		public int Disabled { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00028D1E File Offset: 0x00026F1E
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00028D26 File Offset: 0x00026F26
		public int ProcCompleted { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00028D2F File Offset: 0x00026F2F
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00028D37 File Offset: 0x00026F37
		public int SelectSeq { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00028D40 File Offset: 0x00026F40
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00028D48 File Offset: 0x00026F48
		public int OpParam1 { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00028D51 File Offset: 0x00026F51
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00028D59 File Offset: 0x00026F59
		public int OpParam2 { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00028D62 File Offset: 0x00026F62
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00028D6A File Offset: 0x00026F6A
		public List<ClientCard> EquipCards { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00028D73 File Offset: 0x00026F73
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00028D7B File Offset: 0x00026F7B
		public List<ClientCard> OwnTargets { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00028D84 File Offset: 0x00026F84
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00028D8C File Offset: 0x00026F8C
		public List<ClientCard> TargetCards { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00028D95 File Offset: 0x00026F95
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00028D9D File Offset: 0x00026F9D
		public bool CanDirectAttack { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00028DA6 File Offset: 0x00026FA6
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00028DAE File Offset: 0x00026FAE
		public bool ShouldDirectAttack { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00028DB7 File Offset: 0x00026FB7
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00028DBF File Offset: 0x00026FBF
		public bool Attacked { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00028DC8 File Offset: 0x00026FC8
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00028DD0 File Offset: 0x00026FD0
		public bool IsLastAttacker { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00028DD9 File Offset: 0x00026FD9
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00028DE1 File Offset: 0x00026FE1
		public bool IsSpecialSummoned { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00028DEA File Offset: 0x00026FEA
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x00028DF2 File Offset: 0x00026FF2
		public int[] ActionIndex { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00028DFB File Offset: 0x00026FFB
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00028E03 File Offset: 0x00027003
		public IDictionary<int, int> ActionActivateIndex { get; private set; }

		// Token: 0x06000947 RID: 2375 RVA: 0x00028E0C File Offset: 0x0002700C
		public ClientCard(int id, CardLocation loc, int sequence)
			: this(id, loc, -1, 0)
		{
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00028E18 File Offset: 0x00027018
		public ClientCard(int id, CardLocation loc, int sequence, int position)
		{
			this.SetId(id);
			this.Sequence = sequence;
			this.Position = position;
			this.Overlays = new List<int>();
			this.EquipCards = new List<ClientCard>();
			this.OwnTargets = new List<ClientCard>();
			this.TargetCards = new List<ClientCard>();
			this.ActionIndex = new int[16];
			this.ActionActivateIndex = new Dictionary<int, int>();
			this.Location = loc;
			this.LastLocation = (CardLocation)0;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00028E94 File Offset: 0x00027094
		public void SetId(int id)
		{
			if (this.Id == id)
			{
				return;
			}
			this.Id = id;
			this.Data = NamedCard.Get(this.Id);
			if (this.Data != null)
			{
				this.Name = this.Data.Name;
				this.Alias = this.Data.Alias;
				return;
			}
			this.Name = null;
			this.Alias = 0;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00028EFC File Offset: 0x000270FC
		public void Update(BinaryReader packet, Duel duel)
		{
			int flag = packet.ReadInt32();
			if ((flag & 1) != 0)
			{
				this.SetId(packet.ReadInt32());
			}
			if ((flag & 2) != 0)
			{
				this.Controller = duel.GetLocalPlayer((int)packet.ReadByte());
				this.Location = (CardLocation)packet.ReadByte();
				this.Sequence = (int)packet.ReadByte();
				this.Position = (int)packet.ReadByte();
			}
			if ((flag & 4) != 0)
			{
				this.Alias = packet.ReadInt32();
			}
			if ((flag & 8) != 0)
			{
				this.Type = packet.ReadInt32();
			}
			if ((flag & 16) != 0)
			{
				this.Level = packet.ReadInt32();
			}
			if ((flag & 32) != 0)
			{
				this.Rank = packet.ReadInt32();
			}
			if ((flag & 64) != 0)
			{
				this.Attribute = packet.ReadInt32();
			}
			if ((flag & 128) != 0)
			{
				this.Race = packet.ReadInt32();
			}
			if ((flag & 256) != 0)
			{
				this.Attack = packet.ReadInt32();
			}
			if ((flag & 512) != 0)
			{
				this.Defense = packet.ReadInt32();
			}
			if ((flag & 1024) != 0)
			{
				this.BaseAttack = packet.ReadInt32();
			}
			if ((flag & 2048) != 0)
			{
				this.BaseDefense = packet.ReadInt32();
			}
			if ((flag & 4096) != 0)
			{
				packet.ReadInt32();
			}
			if ((flag & 8192) != 0)
			{
				packet.ReadInt32();
			}
			if ((flag & 16384) != 0)
			{
				packet.ReadInt32();
			}
			if ((flag & 32768) != 0)
			{
				int count = packet.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					packet.ReadInt32();
				}
			}
			if ((flag & 65536) != 0)
			{
				this.Overlays.Clear();
				int count2 = packet.ReadInt32();
				for (int j = 0; j < count2; j++)
				{
					this.Overlays.Add(packet.ReadInt32());
				}
			}
			if ((flag & 131072) != 0)
			{
				int count3 = packet.ReadInt32();
				for (int k = 0; k < count3; k++)
				{
					packet.ReadInt32();
				}
			}
			if ((flag & 262144) != 0)
			{
				this.Owner = duel.GetLocalPlayer(packet.ReadInt32());
			}
			if ((flag & 524288) != 0)
			{
				int status = packet.ReadInt32();
				this.Disabled = status & 1;
				this.ProcCompleted = status & 8;
			}
			if ((flag & 2097152) != 0)
			{
				this.LScale = packet.ReadInt32();
			}
			if ((flag & 4194304) != 0)
			{
				this.RScale = packet.ReadInt32();
			}
			if ((flag & 8388608) != 0)
			{
				this.LinkCount = packet.ReadInt32();
				this.LinkMarker = packet.ReadInt32();
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00029160 File Offset: 0x00027360
		public void ClearCardTargets()
		{
			foreach (ClientCard clientCard in this.TargetCards)
			{
				clientCard.OwnTargets.Remove(this);
			}
			foreach (ClientCard clientCard2 in this.OwnTargets)
			{
				clientCard2.TargetCards.Remove(this);
			}
			this.OwnTargets.Clear();
			this.TargetCards.Clear();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00029214 File Offset: 0x00027414
		public bool HasLinkMarker(int dir)
		{
			return (this.LinkMarker & dir) != 0;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00029214 File Offset: 0x00027414
		public bool HasLinkMarker(CardLinkMarker dir)
		{
			return (this.LinkMarker & (int)dir) != 0;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00029224 File Offset: 0x00027424
		public int GetLinkedZones()
		{
			if (!this.HasType(CardType.Link) || this.Location != CardLocation.MonsterZone)
			{
				return 0;
			}
			int zones = 0;
			if (this.Sequence > 0 && this.Sequence <= 4 && this.HasLinkMarker(CardLinkMarker.Left))
			{
				zones |= 1 << this.Sequence - 1;
			}
			if (this.Sequence <= 3 && this.HasLinkMarker(CardLinkMarker.Right))
			{
				zones |= 1 << this.Sequence + 1;
			}
			if ((this.Sequence == 0 && this.HasLinkMarker(CardLinkMarker.TopRight)) || (this.Sequence == 1 && this.HasLinkMarker(CardLinkMarker.Top)) || (this.Sequence == 2 && this.HasLinkMarker(CardLinkMarker.TopLeft)))
			{
				zones |= 4194336;
			}
			if ((this.Sequence == 2 && this.HasLinkMarker(CardLinkMarker.TopRight)) || (this.Sequence == 3 && this.HasLinkMarker(CardLinkMarker.Top)) || (this.Sequence == 4 && this.HasLinkMarker(CardLinkMarker.TopLeft)))
			{
				zones |= 2097216;
			}
			if (this.Sequence == 5)
			{
				if (this.HasLinkMarker(CardLinkMarker.BottomLeft))
				{
					zones |= 1;
				}
				if (this.HasLinkMarker(CardLinkMarker.Bottom))
				{
					zones |= 2;
				}
				if (this.HasLinkMarker(CardLinkMarker.BottomRight))
				{
					zones |= 4;
				}
				if (this.HasLinkMarker(CardLinkMarker.TopLeft))
				{
					zones |= 1048576;
				}
				if (this.HasLinkMarker(CardLinkMarker.Top))
				{
					zones |= 524288;
				}
				if (this.HasLinkMarker(CardLinkMarker.TopRight))
				{
					zones |= 262144;
				}
			}
			if (this.Sequence == 6)
			{
				if (this.HasLinkMarker(CardLinkMarker.BottomLeft))
				{
					zones |= 4;
				}
				if (this.HasLinkMarker(CardLinkMarker.Bottom))
				{
					zones |= 8;
				}
				if (this.HasLinkMarker(CardLinkMarker.BottomRight))
				{
					zones |= 16;
				}
				if (this.HasLinkMarker(CardLinkMarker.TopLeft))
				{
					zones |= 262144;
				}
				if (this.HasLinkMarker(CardLinkMarker.Top))
				{
					zones |= 131072;
				}
				if (this.HasLinkMarker(CardLinkMarker.TopRight))
				{
					zones |= 65536;
				}
			}
			return zones;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00029400 File Offset: 0x00027600
		public bool HasType(CardType type)
		{
			return (this.Type & (int)type) != 0;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002940D File Offset: 0x0002760D
		public bool HasPosition(CardPosition position)
		{
			return (this.Position & (int)position) != 0;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002941A File Offset: 0x0002761A
		public bool HasAttribute(CardAttribute attribute)
		{
			return (this.Attribute & (int)attribute) != 0;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00029427 File Offset: 0x00027627
		public bool HasRace(CardRace race)
		{
			return (this.Race & (int)race) != 0;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00029434 File Offset: 0x00027634
		public bool HasSetcode(int setcode)
		{
			if (this.Data == null)
			{
				return false;
			}
			long setcodes = this.Data.Setcode;
			int settype = setcode & 4095;
			int setsubtype = setcode & 61440;
			while (setcodes > 0L)
			{
				long check_setcode = setcodes & 65535L;
				setcodes >>= 16;
				if ((check_setcode & 4095L) == (long)settype && (check_setcode & 61440L & (long)setsubtype) == (long)setsubtype)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002949A File Offset: 0x0002769A
		public bool IsMonster()
		{
			return this.HasType(CardType.Monster);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000294A3 File Offset: 0x000276A3
		public bool IsTuner()
		{
			return this.HasType(CardType.Tuner);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000294B0 File Offset: 0x000276B0
		public bool IsSpell()
		{
			return this.HasType(CardType.Spell);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000294B9 File Offset: 0x000276B9
		public bool IsTrap()
		{
			return this.HasType(CardType.Trap);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000294C2 File Offset: 0x000276C2
		public bool IsExtraCard()
		{
			return this.HasType(CardType.Fusion) || this.HasType(CardType.Synchro) || this.HasType(CardType.Xyz) || this.HasType(CardType.Link);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000294F5 File Offset: 0x000276F5
		public bool IsFaceup()
		{
			return this.HasPosition(CardPosition.FaceUp);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000294FE File Offset: 0x000276FE
		public bool IsFacedown()
		{
			return this.HasPosition(CardPosition.FaceDown);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00029508 File Offset: 0x00027708
		public bool IsAttack()
		{
			return this.HasPosition(CardPosition.Attack);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00029511 File Offset: 0x00027711
		public bool IsDefense()
		{
			return this.HasPosition(CardPosition.Defence);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002951B File Offset: 0x0002771B
		public bool IsDisabled()
		{
			return this.Disabled != 0;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00029526 File Offset: 0x00027726
		public bool IsCanRevive()
		{
			return this.ProcCompleted != 0 || (!this.IsExtraCard() && !this.HasType(CardType.Ritual) && !this.HasType(CardType.SpSummon));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00029557 File Offset: 0x00027757
		public bool IsCode(int id)
		{
			return this.Id == id || (this.Alias != 0 && this.Alias == id);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00029577 File Offset: 0x00027777
		public bool IsCode(IList<int> ids)
		{
			return ids.Contains(this.Id) || (this.Alias != 0 && ids.Contains(this.Alias));
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002959F File Offset: 0x0002779F
		public bool IsCode(params int[] ids)
		{
			return ids.Contains(this.Id) || (this.Alias != 0 && ids.Contains(this.Alias));
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000295C7 File Offset: 0x000277C7
		public bool IsOriginalCode(int id)
		{
			return this.Id == id || (this.Alias - this.Id < 20 && this.Alias == id);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000295F0 File Offset: 0x000277F0
		public bool IsOnField()
		{
			return this.Location == CardLocation.MonsterZone || this.Location == CardLocation.SpellZone || this.Location == CardLocation.PendulumZone || this.Location == CardLocation.FieldZone;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00029620 File Offset: 0x00027820
		public bool HasXyzMaterial()
		{
			return this.Overlays.Count > 0;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00029630 File Offset: 0x00027830
		public bool HasXyzMaterial(int count)
		{
			return this.Overlays.Count >= count;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00029643 File Offset: 0x00027843
		public bool HasXyzMaterial(int count, int cardid)
		{
			return this.Overlays.Count >= count && this.Overlays.Contains(cardid);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00029661 File Offset: 0x00027861
		public int GetDefensePower()
		{
			if (!this.IsAttack())
			{
				return this.Defense;
			}
			return this.Attack;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00029678 File Offset: 0x00027878
		public int GetOriginCode()
		{
			int code = this.Id;
			if (this.Data != null)
			{
				if (this.Data.Alias > 0)
				{
					code = this.Data.Alias;
				}
				else
				{
					code = this.Data.Id;
				}
			}
			return code;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00024CB2 File Offset: 0x00022EB2
		public bool Equals(ClientCard card)
		{
			return this == card;
		}

		// Token: 0x04000D74 RID: 3444
		public ClientCard EquipTarget;
	}
}
