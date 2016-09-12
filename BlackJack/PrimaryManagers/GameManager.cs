using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Input;

namespace BlackJack {

    class GameManager {
        private static GameManager instance;

        public static GameManager Instance {
            get {
                if (instance == null) {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public enum GamePlayState {
            PlaceBets, //pre cards dealt phase...place bets...once bet placed...state changes to deal cards...only bet buttons active...hit/stand are not
            DealCards, //dealer and player have 2 cards each...check for blackjack...if no blackjack go to player action...if blackjack then get winner           
            PlayerAction, //player able to use hit/stand buttons...bet buttons turned off...after every hit check for bust...
            //hit until bust then go to GetWinner, hit until stand then go to DealerAction, DoubleDown (effectively 1 hit then stand)...if hit causes bust, Get winner,
            //if hit doesnt cause bust go to dealer action...split...create 2 hands...repeat player actions on one hand and then the other...stand, go to dealer Action.
            DealerAction, //go through dealer AI...if dealer hit, check for bust each time....if bust ever go to get winner...if dealer stand...go to get winner.../all buttons deactivated at this point.
            GetWinner//check winner logic...transfer won or lost chips. clear the table of cards...back to place bets
        }

        public ButtonManager ButtonManager { get { return buttonManager; } private set { } }
        public CommandManager CommandManager { get { return commandManager; } private set { } }
        public PlayerManager PlayerManager { get { return playerManager; } private set { } }

        private ButtonManager buttonManager;
        private CommandManager commandManager;
        private PlayerManager playerManager;
        private ContentManager content;

        public GamePlayState gamePlayState = GamePlayState.PlaceBets;

        public static int SubBet;

        public GameManager() {
            this.content = new ContentManager(Game1.content.ServiceProvider, "Content");
            CardManager.Instance.LoadContent();
            commandManager = new CommandManager();
            playerManager = new PlayerManager();
            buttonManager = new ButtonManager();
        }

        public void LoadContent() {
            PlayerManager.LoadContent();
            ButtonManager.LoadContent();

            /******** Test Area *******/
            HitCommand hitCommand = new HitCommand();

            PlayerManager.Dealer.DealCards(PlayerManager.Player);
            Console.WriteLine(PlayerManager.Dealer.HighHandValue);
            Console.WriteLine(PlayerManager.Player.HighHandValue);
           // CommandManager.Hit.Execute(PlayerManager.Dealer);
           // CommandManager.Hit.Execute(PlayerManager.Dealer, PlayerManager.Player);

            //Dealer.Hit(Player);
            Console.WriteLine(PlayerManager.Player.HighHandValue);
            Console.WriteLine(BlackJackHandler.IsBlackJack(PlayerManager.Player));
            // Dealer.Hit(Dealer);
            Console.WriteLine(PlayerManager.Dealer.HighHandValue);
            Console.WriteLine(BlackJackHandler.IsBlackJack(PlayerManager.Dealer));

            Console.WriteLine(BlackJackHandler.CheckWinner(PlayerManager.Dealer, PlayerManager.Player));

            /******Test Area ********/
        }

        public void UnLoadContent() {

        }

        public void Update(GameTime gameTime) {
            CardManager.Instance.Update(gameTime);
            PlayerManager.Update(gameTime);
            ButtonManager.Update(gameTime);
            
            /****/

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                gamePlayState = GamePlayState.PlayerAction;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                playerManager.Player.ChipCount += 100;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                playerManager.Player.ChipCount -= 100;
            }

           




            /*****/

            
        }

        public void Draw(SpriteBatch spriteBatch) {
            PlayerManager.Draw(spriteBatch);
            ButtonManager.Draw(spriteBatch);
        }
    }
}
