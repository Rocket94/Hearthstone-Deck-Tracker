using HearthDb.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	public partial class TurnCounter : UserControl
	{
		private Lazy<BattlegroundsDb> _db = new Lazy<BattlegroundsDb>();
		private string _unavailableRaces;

		public TurnCounter()
		{
			InitializeComponent();
		}

		internal void UpdateTurn(int turn, bool showBan = true)
		{
			if(showBan && turn == 1)
			{
				var availableRaces = BattlegroundsUtils.GetAvailableRaces(Core.Game.CurrentGameStats?.GameId) ?? _db.Value.Races;
				_unavailableRaces = string.Join(", ", _db.Value.Races.Where(x => !availableRaces.Contains(x) && x != Race.INVALID && x != Race.ALL).Select(x => HearthDbConverter.RaceChineseConverter(x)));
			}

			var text = string.Format(LocUtil.Get("Overlay_Battlegrounds_Turn_Counter"), turn);

			if(showBan && !string.IsNullOrEmpty(_unavailableRaces))
			{
				text = text + " 禁" + _unavailableRaces;
			}

			TurnText.Text = text;
		}
	}
}
