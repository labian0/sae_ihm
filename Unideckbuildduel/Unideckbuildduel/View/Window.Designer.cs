﻿using System.Drawing;

namespace Unideckbuildduel.View
{
    partial class Window
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.quitButton = new System.Windows.Forms.Button();
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.nextTurnButton = new System.Windows.Forms.Button();
            this.turnLabel = new System.Windows.Forms.Label();
            this.playerTwoScoreLabel = new System.Windows.Forms.Label();
            this.playerOneScoreLabel = new System.Windows.Forms.Label();
            this.restartButton = new System.Windows.Forms.Button();
            this.placeAllButton = new System.Windows.Forms.Button();
            this.deckLabel = new System.Windows.Forms.Label();
            this.discardLabel = new System.Windows.Forms.Label();
            this.DrawOncePerTurnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(1100, 555);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 21);
            this.quitButton.TabIndex = 0;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // outputListBox
            // 
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.Location = new System.Drawing.Point(914, 28);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(261, 433);
            this.outputListBox.TabIndex = 1;
            // 
            // nextTurnButton
            // 
            this.nextTurnButton.Location = new System.Drawing.Point(1100, 474);
            this.nextTurnButton.Name = "nextTurnButton";
            this.nextTurnButton.Size = new System.Drawing.Size(75, 21);
            this.nextTurnButton.TabIndex = 3;
            this.nextTurnButton.Text = "Next Turn";
            this.nextTurnButton.UseVisualStyleBackColor = true;
            this.nextTurnButton.Click += new System.EventHandler(this.NextTurnButton_Click);
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Location = new System.Drawing.Point(967, 561);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(35, 13);
            this.turnLabel.TabIndex = 4;
            this.turnLabel.Text = "label1";
            // 
            // playerTwoScoreLabel
            // 
            this.playerTwoScoreLabel.AutoSize = true;
            this.playerTwoScoreLabel.Location = new System.Drawing.Point(967, 528);
            this.playerTwoScoreLabel.Name = "playerTwoScoreLabel";
            this.playerTwoScoreLabel.Size = new System.Drawing.Size(35, 13);
            this.playerTwoScoreLabel.TabIndex = 5;
            this.playerTwoScoreLabel.Text = "label1";
            // 
            // playerOneScoreLabel
            // 
            this.playerOneScoreLabel.AutoSize = true;
            this.playerOneScoreLabel.Location = new System.Drawing.Point(967, 504);
            this.playerOneScoreLabel.Name = "playerOneScoreLabel";
            this.playerOneScoreLabel.Size = new System.Drawing.Size(35, 13);
            this.playerOneScoreLabel.TabIndex = 6;
            this.playerOneScoreLabel.Text = "label1";
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(1100, 528);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(75, 21);
            this.restartButton.TabIndex = 7;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Visible = false;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // placeAllButton
            // 
            this.placeAllButton.Location = new System.Drawing.Point(1100, 501);
            this.placeAllButton.Name = "placeAllButton";
            this.placeAllButton.Size = new System.Drawing.Size(75, 21);
            this.placeAllButton.TabIndex = 8;
            this.placeAllButton.Text = "Place all";
            this.placeAllButton.UseVisualStyleBackColor = true;
            this.placeAllButton.Click += new System.EventHandler(this.placeAllButton_Click);
            // 
            // deckLabel
            // 
            this.deckLabel.AutoSize = true;
            this.deckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deckLabel.Location = new System.Drawing.Point(782, 229);
            this.deckLabel.Name = "deckLabel";
            this.deckLabel.Size = new System.Drawing.Size(69, 20);
            this.deckLabel.TabIndex = 9;
            this.deckLabel.Text = "Pioche : ";
            // 
            // discardLabel
            // 
            this.discardLabel.AutoSize = true;
            this.discardLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discardLabel.Location = new System.Drawing.Point(782, 262);
            this.discardLabel.Name = "discardLabel";
            this.discardLabel.Size = new System.Drawing.Size(90, 20);
            this.discardLabel.TabIndex = 10;
            this.discardLabel.Text = "Defausse : ";
            // 
            // DrawOncePerTurnButton
            // 
            this.DrawOncePerTurnButton.Location = new System.Drawing.Point(786, 456);
            this.DrawOncePerTurnButton.Name = "DrawOncePerTurnButton";
            this.DrawOncePerTurnButton.Size = new System.Drawing.Size(75, 21);
            this.DrawOncePerTurnButton.TabIndex = 11;
            this.DrawOncePerTurnButton.UseVisualStyleBackColor = true;
            this.DrawOncePerTurnButton.Visible = false;
            this.DrawOncePerTurnButton.Click += new System.EventHandler(this.DrawOncePerTurnButton_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.DrawOncePerTurnButton);
            this.Controls.Add(this.discardLabel);
            this.Controls.Add(this.deckLabel);
            this.Controls.Add(this.placeAllButton);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.playerOneScoreLabel);
            this.Controls.Add(this.playerTwoScoreLabel);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.nextTurnButton);
            this.Controls.Add(this.outputListBox);
            this.Controls.Add(this.quitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Window";
            this.Text = "Insert title here";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Window_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Window_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.ListBox outputListBox;
        private System.Windows.Forms.Button nextTurnButton;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label playerTwoScoreLabel;
        private System.Windows.Forms.Label playerOneScoreLabel;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Button placeAllButton;
        private System.Windows.Forms.Label deckLabel;
        private System.Windows.Forms.Label discardLabel;
        private System.Windows.Forms.Button DrawOncePerTurnButton;
    }
}

