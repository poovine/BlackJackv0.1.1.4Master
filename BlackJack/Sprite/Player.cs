using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackJack {
    class Player : GameCharacter {

        public int ChipCount { get; set; } = 100;
        public int BetAmount { get; set; } = 0;
        public bool IsStanding { get; set; }


        public Player(Texture2D playerTexture, Vector2 position, Rectangle maskRectangle)
                        : base(playerTexture, position, maskRectangle) {           
        }

        public bool CanDoubleDown() {
            return ((this.CurrentHand.Count == 2) && (this.BetAmount <= this.ChipCount) && !IsStanding);
        }

        //public bool CanSplit() {

        //}

        public bool CanHit() {
            return (!BlackJackHandler.IsBust(this) && !GameManager.Instance.PlayerManager.Player.IsStanding);
        }  

        public bool CanStand() {
            return (!BlackJackHandler.IsBust(this) && !GameManager.Instance.PlayerManager.Player.IsStanding);
        }

      

        public override void Update(GameTime gameTime) {
            Console.WriteLine(this.ChipCount);
            Console.WriteLine(this.BetAmount);        
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(spriteTexture, position, Color.White);
            DrawCurrentHand(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
