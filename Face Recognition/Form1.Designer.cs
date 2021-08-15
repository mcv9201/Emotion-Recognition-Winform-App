
namespace Simple_Face_Recognition_App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picCapture = new System.Windows.Forms.PictureBox();
            this.btnDetector = new System.Windows.Forms.Button();
            this.btn_emotion = new System.Windows.Forms.Button();
            this.btn_Capture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // picCapture
            // 
            this.picCapture.Location = new System.Drawing.Point(146, 133);
            this.picCapture.Name = "picCapture";
            this.picCapture.Size = new System.Drawing.Size(624, 552);
            this.picCapture.TabIndex = 0;
            this.picCapture.TabStop = false;
            // 
            // btnDetector
            // 
            this.btnDetector.Location = new System.Drawing.Point(955, 377);
            this.btnDetector.Name = "btnDetector";
            this.btnDetector.Size = new System.Drawing.Size(278, 49);
            this.btnDetector.TabIndex = 2;
            this.btnDetector.Text = "Detect Faces";
            this.btnDetector.UseVisualStyleBackColor = true;
            this.btnDetector.Click += new System.EventHandler(this.btnDetector_Click);
            // 
            // btn_emotion
            // 
            this.btn_emotion.Location = new System.Drawing.Point(955, 477);
            this.btn_emotion.Name = "btn_emotion";
            this.btn_emotion.Size = new System.Drawing.Size(278, 49);
            this.btn_emotion.TabIndex = 3;
            this.btn_emotion.Text = "Detect Emotions";
            this.btn_emotion.UseVisualStyleBackColor = true;
            this.btn_emotion.Click += new System.EventHandler(this.btn_emotion_Click);
            // 
            // btn_Capture
            // 
            this.btn_Capture.Location = new System.Drawing.Point(955, 281);
            this.btn_Capture.Name = "btn_Capture";
            this.btn_Capture.Size = new System.Drawing.Size(278, 49);
            this.btn_Capture.TabIndex = 4;
            this.btn_Capture.Text = "Capture";
            this.btn_Capture.UseVisualStyleBackColor = true;
            this.btn_Capture.Click += new System.EventHandler(this.btn_Capture_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 783);
            this.Controls.Add(this.btn_Capture);
            this.Controls.Add(this.btn_emotion);
            this.Controls.Add(this.btnDetector);
            this.Controls.Add(this.picCapture);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCapture;
        private System.Windows.Forms.Button btnDetector;
        private System.Windows.Forms.Button btn_emotion;
        private System.Windows.Forms.Button btn_Capture;
    }
}

