using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack {
    static class BlackJackHandler {
        public static int CheckWinner(Dealer dealer, Player player) {
            //return 0 for dealer win, 1 for player win, -1 for push
            if (dealer.HighHandValue > player.HighHandValue)
                return 0;
            else if (dealer.HighHandValue < player.HighHandValue) {
                return 1;
            }
            else
                return -1;
        }

        public static bool IsBlackJack(GameCharacter gameCharacter) {
            return (gameCharacter.CurrentHand.Count == 2 && gameCharacter.HighHandValue == 21);
        }

        public static void DealerSelfAIAction() {
            var dealer = GameManager.Instance.PlayerManager.Dealer;
            var hit = GameManager.Instance.CommandManager.Hit;
            var stand = GameManager.Instance.CommandManager.Stand;

            if (dealer.HighHandValue < 17) {
                hit.Execute(dealer);
            }
            else if (dealer.HighHandValue > 17) {
                stand.Execute(dealer);
            }
            else if (dealer.HighHandValue == 17 && dealer.LowHandValue < 17) {
                hit.Execute(dealer);
            }
            else
                stand.Execute(dealer);
        }

        public static bool IsBust(GameCharacter gameCharacter) {
            if (gameCharacter.LowHandValue > 21 && gameCharacter.HighHandValue > 21) {
                return true;
            }
            else
                return false;
        }
    }
}

