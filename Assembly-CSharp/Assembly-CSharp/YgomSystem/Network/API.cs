using System;
using System.Collections.Generic;

namespace YgomSystem.Network
{
	// Token: 0x020006FA RID: 1786
	public class API
	{
		// Token: 0x06003792 RID: 14226 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle System_info()
		{
			return null;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle System_set_language(string _lang_)
		{
			return null;
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle System_toggle_crossplay()
		{
			return null;
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_create(int _agreement_type_, string[] _agree_info_, int _country_, Dictionary<string, object> _enquete_results_)
		{
			return null;
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_create(string _auth_session_, int _agreement_type_, string[] _agree_info_, int _country_, Dictionary<string, object> _enquete_results_)
		{
			return null;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_auth()
		{
			return null;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_auth(string _auth_session_, bool _valid_steam_overlay_)
		{
			return null;
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_auth(string _auth_session_, int _label_)
		{
			return null;
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_re_agree(string[] _agree_info_, bool _optout_)
		{
			return null;
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_is_regist_platform()
		{
			return null;
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_regist_platform()
		{
			return null;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_inherit_platform()
		{
			return null;
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_kid_get_link_url()
		{
			return null;
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_kid_check_linked()
		{
			return null;
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_kid_get_inherit_url()
		{
			return null;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_kid_check_inherited(string _kid_inherit_nonce_, string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_kid_get_neuron_token()
		{
			return null;
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_set_opt_out()
		{
			return null;
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_report_user(int _reported_pcode_, int[] _report_ids_)
		{
			return null;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_unlock(string _pass_)
		{
			return null;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_set_official_settings()
		{
			return null;
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_Steam_get_user_id(string _session_ticket_)
		{
			return null;
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_Steam_re_auth(string _session_ticket_)
		{
			return null;
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_PS_get_user_id(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_PS_re_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_PS_refresh_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_XBox_get_user_id(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_XBox_re_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_XBox_refresh_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_Nx_get_user_id(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_Nx_re_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Account_Nx_refresh_auth(string _auth_session_)
		{
			return null;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_entry()
		{
			return null;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_home()
		{
			return null;
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_profile(long _pcode_)
		{
			return null;
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_record(long _pcode_)
		{
			return null;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_set_profile(Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_replay_list(long _pcode_)
		{
			return null;
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_first_name_entry(string _name_)
		{
			return null;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_name_entry(string _name_)
		{
			return null;
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_complete_home_guide()
		{
			return null;
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle User_item_get_history()
		{
			return null;
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_begin(Dictionary<string, object> _rule_)
		{
			return null;
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_end(Dictionary<string, object> _params_)
		{
			return null;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_matching(Dictionary<string, object> _rule_)
		{
			return null;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_matching_cancel()
		{
			return null;
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_start_waiting()
		{
			return null;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_start_selecting(int _select_)
		{
			return null;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_team_matching_leader(Dictionary<string, object> _rule_)
		{
			return null;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duel_team_matching_member(Dictionary<string, object> _rule_)
		{
			return null;
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_info()
		{
			return null;
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_entry(int _tid_)
		{
			return null;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_detail(int _tid_)
		{
			return null;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_reward_list(int _tid_)
		{
			return null;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_duel_history(int _tid_)
		{
			return null;
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_ranking(int _tid_)
		{
			return null;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_get_deck_list(int _tid_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_set_deck(int _tid_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_set_deck_accessory(int _tid_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Tournament_delete_deck(int _tid_)
		{
			return null;
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_get_deck()
		{
			return null;
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_get_deck_list(int _deck_id_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_update_deck(int _deck_id_, string _name_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _pick_cards_, Dictionary<string, object> _accessory_)
		{
			return null;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_update_deck_reg(int _deck_id_, string _name_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _pick_cards_, Dictionary<string, object> _accessory_, int _regulation_id_)
		{
			return null;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_delete_deck(int _deck_id_)
		{
			return null;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_delete_deck_multi(int[] _deck_id_list_)
		{
			return null;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_check_deck_regulation(int _deck_id_, int _regulation_id_)
		{
			return null;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_set_deck_accessory(int _deck_id_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_set_select_deck(int _mode_, int _deck_id_)
		{
			return null;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_CopyStructure(int _structure_id_)
		{
			return null;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_SetFavoriteCards(Dictionary<string, object> _card_list_)
		{
			return null;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_ExportDeck(string _N_token_, int _deck_id_)
		{
			return null;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Deck_GetAccessoryDetail()
		{
			return null;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Download_begin()
		{
			return null;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Download_complete()
		{
			return null;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Download_progress(string _dl_end_)
		{
			return null;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Craft_exchange(int _card_id_)
		{
			return null;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Craft_exchange_multi(Dictionary<string, object> _card_list_, int[] _compensation_list_)
		{
			return null;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Craft_generate(int _card_id_)
		{
			return null;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Craft_generate_multi(Dictionary<string, object> _card_list_)
		{
			return null;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Craft_get_card_route(int _card_id_)
		{
			return null;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_follow(long _pcode_, int _delete_)
		{
			return null;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_set_pin(long _pcode_, int _delete_, int _update_work_)
		{
			return null;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_get_follower(long _date_, long _pcode_, int _dir_)
		{
			return null;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_get_list(bool _all_)
		{
			return null;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_id_search(long _pcode_)
		{
			return null;
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_tag_search(int[] _tag_)
		{
			return null;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_block(long _pcode_, int _delete_)
		{
			return null;
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Friend_refresh_info(long[] _pcode_list_)
		{
			return null;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Mission_get_list()
		{
			return null;
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Mission_receive(int _pool_id_, int _mission_id_, int _goal_pos_)
		{
			return null;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Mission_bulk_receive(int[] _bulk_pool_id_, int[] _bulk_mission_id_, int[] _bulk_goal_pos_)
		{
			return null;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_watch_duel(long _pcode_, long _rapid_)
		{
			return null;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_replay_duel(long _pcode_, long _did_)
		{
			return null;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_save_replay(int _mode_, long _did_, int _eid_)
		{
			return null;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_remove_replay(long _did_)
		{
			return null;
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_replay_duel_history(int _idx_, int _mode_, long _did_, int _eid_)
		{
			return null;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_replay_duel_history_with_room(long _did_, long _pcode_)
		{
			return null;
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_duel_history(int _mode_)
		{
			return null;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_set_replay_open(long _did_, bool _open_)
		{
			return null;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_get_history_deck(long _did_, int _mode_, int _idx_)
		{
			return null;
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PvP_get_replay_deck(long _did_)
		{
			return null;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Structure_first(int _structure_id_)
		{
			return null;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Structure_check_have_structure()
		{
			return null;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Gacha_get_card_list(int _card_list_id_)
		{
			return null;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Gacha_get_probability(int _gacha_id_, int _shop_id_)
		{
			return null;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Shop_get_list(int _category_)
		{
			return null;
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Shop_purchase(int _shop_id_, int _price_id_, int _count_, Dictionary<string, object> _args_)
		{
			return null;
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Shop_visit(int[] _shop_ids_)
		{
			return null;
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Challenge_detail(int _mode_, int _season_id_)
		{
			return null;
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Challenge_ranking(int _mode_, int _season_id_)
		{
			return null;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Challenge_set_deck(int _mode_, int _deck_id_)
		{
			return null;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Challenge_duel_history(int _mode_, int _season_id_)
		{
			return null;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Challenge_reward_list(int _mode_, int _season_id_)
		{
			return null;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Casual_detail()
		{
			return null;
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Casual_duel_history()
		{
			return null;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_info(bool _back_)
		{
			return null;
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_detail(int _chapter_)
		{
			return null;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_start(int _chapter_)
		{
			return null;
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_deck_check()
		{
			return null;
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_set_use_deck_type(int _chapter_, int _deck_type_)
		{
			return null;
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_skip(int _chapter_)
		{
			return null;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Solo_gate_entry(int _gate_)
		{
			return null;
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PresentBox_get_list()
		{
			return null;
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PresentBox_receive(int _present_box_id_, int _is_all_)
		{
			return null;
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelMenu_info()
		{
			return null;
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelMenu_deck_check(int _kind_, int _tid_, int _regulation_id_)
		{
			return null;
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Notification_get_list()
		{
			return null;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Notification_read(int _id_)
		{
			return null;
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle EventNotify_get_list()
		{
			return null;
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle EventNotify_delete_badge(int _type_, int _subtype_, int[] _target_list_)
		{
			return null;
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_product_list()
		{
			return null;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_reservation(int _shop_id_, int _merchID_, string _price_, string _currency_)
		{
			return null;
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_Nx_reservation(int _merchID_, string _price_, string _currency_)
		{
			return null;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_XBox_reservation(int _merchID_, string _price_, string _currency_)
		{
			return null;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_PS_reservation(int _merchID_, string _price_, string _currency_)
		{
			return null;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_purchase(string _receipt_, string _adid_, string _idfa_, string _idfv_, string _gps_adid_)
		{
			return null;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_purchase(string _receipt_, string _orderid_, string _transactionid_)
		{
			return null;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_Nx_purchase()
		{
			return null;
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_XBox_purchase()
		{
			return null;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_PS_purchase()
		{
			return null;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_cancel()
		{
			return null;
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_re_store(string _receipt_)
		{
			return null;
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_Steam_re_store(long _orderid_, long _transactionid_)
		{
			return null;
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_Nx_re_store()
		{
			return null;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_XBox_re_store(string _tracking_id_, int _merchID_, string _product_id_, string _price_, string _currency_, bool _is_un_complete_add_item_)
		{
			return null;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_PS_re_store(string _transaction_id_, int _merchID_, string _product_id_, string _entitlement_label_, string _price_, string _currency_, bool _is_un_complete_add_item_)
		{
			return null;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_PS_add_incentive_item(string _transaction_id_, int _ps_incentive_id_, string _product_id_, string _entitlement_label_, int _service_label_)
		{
			return null;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_register_age(int _age_reg_id_)
		{
			return null;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_add_purchased_item()
		{
			return null;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_in_complete_item_check()
		{
			return null;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_Steam_in_complete_item_check()
		{
			return null;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_Nx_in_complete_item_check()
		{
			return null;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_XBox_in_complete_item_check()
		{
			return null;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_PS_in_complete_item_check()
		{
			return null;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Billing_history(string _month_, int _page_, int _page_count_)
		{
			return null;
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cgdb_deck_search_init()
		{
			return null;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cgdb_deck_search(int _typeCode_, int[] _categoryList_, int[] _tagList_, int[] _cardIdList_, string _keyword_, int _sortCode_, int _sizePerPage_, int _requestPageNo_)
		{
			return null;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cgdb_deck_search_detail(string _targetId_, int _deckNo_)
		{
			return null;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cgdb_mydeck_search(string _userToken_, int _sortCode_, int _sizePerPage_, int _requestPageNo_)
		{
			return null;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cgdb_mydeck_search_detail(string _userToken_, int _deckNo_)
		{
			return null;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_create(Dictionary<string, object> _room_settings_)
		{
			return null;
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_create(Dictionary<string, object> _room_settings_, string _context_id_)
		{
			return null;
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_entry(int _id_, int _is_specter_, Dictionary<string, object> _options_)
		{
			return null;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_entry(int _id_, int _is_specter_, Dictionary<string, object> _options_, string _context_id_)
		{
			return null;
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_exit()
		{
			return null;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_get_room_list(Dictionary<string, object> _search_options_)
		{
			return null;
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_table_arrive(int _table_no_)
		{
			return null;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_table_leave()
		{
			return null;
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_friend_invite(long[] _invite_list_)
		{
			return null;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_friend_invite(long[] _invite_list_, string _context_id_)
		{
			return null;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_room_table_polling()
		{
			return null;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_is_room_battle_ready(bool _isBattleReady_, long _opp_pcode_)
		{
			return null;
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_set_user_comment(int _comment_id_)
		{
			return null;
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Room_get_result_list()
		{
			return null;
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_detail(int _exhid_)
		{
			return null;
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_duel_history(int _exhid_)
		{
			return null;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_set_deck(int _exhid_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_set_deck_accessory(int _exhid_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_delete_deck(int _exhid_)
		{
			return null;
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_copy_to_deck(int _exhid_)
		{
			return null;
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_rental_deck_detail(int _exhid_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_copy_rental_deck(int _exhid_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_set_use_deck(int _exhid_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Exhibition_get_deck_list(int _exhid_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duelpass_get_info()
		{
			return null;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Duelpass_bulk_receive(int _season_id_, int[] _reward_id_list_)
		{
			return null;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelLive_replay_list(int _menu_id_, int _section_id_)
		{
			return null;
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelLive_replay_duel(int _menu_id_, int _idx_)
		{
			return null;
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Enquete_get_questions(int _enquete_id_)
		{
			return null;
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Enquete_send_answers(int _enquete_id_, Dictionary<string, object> _results_)
		{
			return null;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_detail(int _rank_event_id_)
		{
			return null;
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_duel_history(int _rank_event_id_)
		{
			return null;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_reward_list(int _rank_event_id_)
		{
			return null;
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_get_deck_list(int _rank_event_id_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_set_deck(int _rank_event_id_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_set_deck_accessory(int _rank_event_id_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle RankEvent_delete_deck(int _rank_event_id_)
		{
			return null;
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_detail(int _cid_)
		{
			return null;
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_get_deck_list(int _cid_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_set_deck(int _cid_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_delete_deck(int _cid_)
		{
			return null;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_duel_history(int _cid_)
		{
			return null;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_set_deck_accessory(int _cid_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Cup_get_ranking(int _cid_)
		{
			return null;
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle CardTermData_get_list()
		{
			return null;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_set_team_regulation_group_id(int _team_regulation_group_id_)
		{
			return null;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_create(Dictionary<string, object> _team_settings_, int _team_match_type_)
		{
			return null;
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_create(Dictionary<string, object> _team_settings_, int _team_match_type_, string _context_id_)
		{
			return null;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_exit()
		{
			return null;
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_entry(int _team_id_, int _team_match_type_)
		{
			return null;
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_entry(int _team_id_, int _team_match_type_, string _context_id_)
		{
			return null;
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_entry_and_arrive(int _team_id_, int _team_match_type_, int _table_no_)
		{
			return null;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_entry_and_arrive(int _team_id_, int _team_match_type_, string _context_id_, int _table_no_)
		{
			return null;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_invite(long[] _invite_list_)
		{
			return null;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_invite(long[] _invite_list_, string _context_id_)
		{
			return null;
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_recruit(int _num_)
		{
			return null;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_search(int _num_, bool _cancel_flg_)
		{
			return null;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_polling(int _team_match_type_)
		{
			return null;
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_result_polling()
		{
			return null;
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_table_arrive(int _table_no_)
		{
			return null;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_table_leave()
		{
			return null;
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_post_comment(int _comment_id_)
		{
			return null;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_duel_request(int _team_id_, int _duel_time_)
		{
			return null;
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_duel_request_cancel()
		{
			return null;
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Team_team_duel_reply(bool _reply_)
		{
			return null;
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle LoginBonus_get_info(int _login_bonus_id_)
		{
			return null;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle LoginBonus_get_list()
		{
			return null;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle LoginBonus_receive(int _login_bonus_id_)
		{
			return null;
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_detail(int _duel_trial_id_)
		{
			return null;
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_duel_history(int _duel_trial_id_)
		{
			return null;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_get_deck_list(int _duel_trial_id_, int _idx_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_set_deck(int _duel_trial_id_, int _idx_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_set_deck_accessory(int _duel_trial_id_, int _idx_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_delete_deck(int _duel_trial_id_, int _idx_)
		{
			return null;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_set_use_deck(int _duel_trial_id_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_get_rental_deck_list(int _duel_trial_id_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle DuelTrial_receive_bonus(int _duel_trial_id_, int _item_id_, int _num_)
		{
			return null;
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle PromoCodes_send_code(int _promo_codes_id_, string _send_code_, string _check_code_)
		{
			return null;
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_detail(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_get_deck_list(int _wcs_id_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_set_deck(int _wcs_id_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_delete_deck(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_duel_history(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_set_deck_accessory(int _wcs_id_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_get_ranking(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_set_region(int _wcs_id_, int _region_)
		{
			return null;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_get_participation()
		{
			return null;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_confirm_participation(int _id_)
		{
			return null;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_get_final_deck_info()
		{
			return null;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_set_final_deck(int _wcs_id_, int _member_idx_, int _slot_, string _name_, Dictionary<string, object> _deck_list_)
		{
			return null;
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_delete_final_deck(int _wcs_id_, int _member_idx_, int _slot_)
		{
			return null;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_set_final_share_card(int _wcs_id_, Dictionary<string, object> _share_cards_)
		{
			return null;
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_check_final_deck_regulation(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_complete_final_deck(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Wcs_unregister_final_deck(int _wcs_id_)
		{
			return null;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_detail(int _versus_id_, int _set_group_id_)
		{
			return null;
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_duel_history(int _versus_id_)
		{
			return null;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_get_deck_list(int _versus_id_, int _idx_, bool _is_empty_get_)
		{
			return null;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_set_deck(int _versus_id_, int _idx_, Dictionary<string, object> _deck_list_, Dictionary<string, object> _accessory_, Dictionary<string, object> _pick_cards_)
		{
			return null;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_set_deck_accessory(int _versus_id_, int _idx_, Dictionary<string, object> _param_)
		{
			return null;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_delete_deck(int _versus_id_, int _idx_)
		{
			return null;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_set_use_deck(int _versus_id_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Versus_get_rental_deck_list(int _versus_id_, int _rental_idx_)
		{
			return null;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsFinal_table_polling()
		{
			return null;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsFinal_is_duel_ready(bool _isReady_, long _opp_pcode_)
		{
			return null;
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsFinal_admin_table_polling(int _room_id_)
		{
			return null;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsFinal_admin_primary_polling()
		{
			return null;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsFinal_admin_final_polling()
		{
			return null;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_info()
		{
			return null;
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_primary_polling()
		{
			return null;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_final_polling()
		{
			return null;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_table_polling(int _room_id_, string _room_unique_id_)
		{
			return null;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_watch_duel(int _room_id_, string _room_unique_id_, int _tno_, int _rapid_)
		{
			return null;
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_set_support_team(int _team_id_)
		{
			return null;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle WcsfCampaign_support_entry()
		{
			return null;
		}
	}
}
