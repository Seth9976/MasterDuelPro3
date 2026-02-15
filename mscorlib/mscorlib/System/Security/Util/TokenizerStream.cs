using System;

namespace System.Security.Util
{
	// Token: 0x02000332 RID: 818
	internal sealed class TokenizerStream
	{
		// Token: 0x06001D04 RID: 7428 RVA: 0x000723EE File Offset: 0x000705EE
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0007241C File Offset: 0x0007061C
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			block[indexTokens] = token;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00072494 File Offset: 0x00070694
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			block[indexStrings] = str;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000724FC File Offset: 0x000706FC
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0007252C File Offset: 0x0007072C
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			return block[indexTokens];
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0007258F File Offset: 0x0007078F
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000725A0 File Offset: 0x000707A0
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			return block[indexStrings];
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000725F7 File Offset: 0x000707F7
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00072600 File Offset: 0x00070800
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0007267E File Offset: 0x0007087E
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00072688 File Offset: 0x00070888
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x04000D6C RID: 3436
		private int m_countTokens;

		// Token: 0x04000D6D RID: 3437
		private TokenizerShortBlock m_headTokens;

		// Token: 0x04000D6E RID: 3438
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x04000D6F RID: 3439
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x04000D70 RID: 3440
		private int m_indexTokens;

		// Token: 0x04000D71 RID: 3441
		private TokenizerStringBlock m_headStrings;

		// Token: 0x04000D72 RID: 3442
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x04000D73 RID: 3443
		private int m_indexStrings;
	}
}
