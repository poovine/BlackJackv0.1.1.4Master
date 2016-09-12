using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackJack {
    class Button : Sprite {

        public Vector2 Position { get { return position; } }
        public Rectangle SourceRectangle { get { return sourceRectangle; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public string Name { get { return name; } }
        public int Value { get; set; }

        public enum ButtonTag {
            CardActionButton,
            BettingButton
        }

        private ButtonTag buttonTag;

        private string name;

        public Button(int value, ButtonTag buttonTag, string name, Texture2D buttonTexture, Vector2 buttonPosition, Rectangle sourceRectangle,
                      float scale = 1, float rotation = 0, float layerDepth = 1
            ) : base(buttonTexture, buttonPosition, sourceRectangle) {
            this.Value = value;
            this.buttonTag = buttonTag;
            this.name = name;
            this.scale = scale;
            this.rotation = rotation;
            this.layerDepth = layerDepth;
        }

        private void UpdateIsActive() {
            var player = GameManager.Instance.PlayerManager.Player;
            var dealer = GameManager.Instance.PlayerManager.Dealer;

            if (Game1.gameState == Game1.GameState.Game && GameManager.Instance.gamePlayState == GameManager.GamePlayState.PlaceBets) {

                switch (this.buttonTag) {
                    case ButtonTag.CardActionButton:
                        this.IsActive = false;
                        this.isVisible = false;
                        break;
                    case ButtonTag.BettingButton:
                        if (this.Value > player.ChipCount) {
                            this.IsActive = false;
                            this.isVisible = false;
                        }
                        else {
                            this.IsActive = true;
                            this.isVisible = true;
                        }
                        break;
                }
            }
            else if (Game1.gameState == Game1.GameState.Game && GameManager.Instance.gamePlayState == GameManager.GamePlayState.PlayerAction) {
                switch (this.buttonTag) {
                    case ButtonTag.CardActionButton: {
                            if (player.CanHit() && this.Name == "Hit") {
                                this.IsActive = true;
                                this.isVisible = true;
                            }

                            else if (player.CanDoubleDown() && this.Name == "DoubleDown") {
                                this.IsActive = true;
                                this.isVisible = true;
                            }

                            else if (player.CanStand() && this.Name == "Stand") {
                                this.IsActive = true;
                                this.isVisible = true;
                            }

                            // else if (player.CanSplit() && this.Name == "Split"){
                            //    this.IsActive = true;
                            //    this.isVisible = true;   
                            //} 
                            else {
                                this.IsActive = false;
                                this.isVisible = false;
                            }
                        }
                        break;

                    case ButtonTag.BettingButton:
                        this.IsActive = false;
                        this.isVisible = false;
                        break;
                }
            }

            else {
                switch (this.buttonTag) {
                    case ButtonTag.CardActionButton:
                        this.IsActive = false;
                        this.isVisible = false;
                        break;
                    case ButtonTag.BettingButton:
                        this.IsActive = false;
                        this.isVisible = false;
                        break;
                }
            }

        }

        public override void Update(GameTime gameTime) {
            UpdateIsActive();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (this.isVisible) {
                spriteBatch.Draw(spriteTexture, position, sourceRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
            base.Draw(spriteBatch);
        }

    }
}
