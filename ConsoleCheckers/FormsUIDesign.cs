using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YotamControls;

namespace ConsoleCheckers
{
    public partial class FormsUI : Form
    {
        private YotamButton buttonPreviousMove;
        private YotamButton buttonNextMove;
        private YotamButton buttonExit;
        private YotamPanel panelChooseOponent;
        private YotamButton buttonChooseComputerGame;
        private YotamButton buttonChooseComputer;
        private YotamButton buttonChoose2Player;
        private YotamButton ButtonStartMatch;
        private YotamButton ComputerMatch;
        private YotamPanel panelCompueterSettings;
        private TrackBar TrackBarQueenMoveLimit;
        private Label LabelQueenMoveLimit;
        private Label LabelQueenLimitText;
        private YotamPanel panelCheckers;
        private YotamImagePanel panelWhiteCapture;
        private YotamImagePanel panelBlackCapture;
        private YotamPanel mainPanel;
        private CheckersButton CheckersButton8;
        private CheckersButton CheckersButton7;
        private CheckersButton CheckersButton6;
        private CheckersButton CheckersButton5;
        private CheckersButton CheckersButton4;
        private CheckersButton CheckersButton3;
        private CheckersButton CheckersButton2;
        private CheckersButton CheckersButton1;
        private YotamPanel panel2;
        private CheckersButton CheckersButton33;
        private CheckersButton CheckersButton34;
        private CheckersButton CheckersButton35;
        private CheckersButton CheckersButton36;
        private CheckersButton CheckersButton37;
        private CheckersButton CheckersButton38;
        private CheckersButton CheckersButton39;
        private CheckersButton CheckersButton40;
        private CheckersButton CheckersButton41;
        private CheckersButton CheckersButton42;
        private CheckersButton CheckersButton43;
        private CheckersButton CheckersButton44;
        private CheckersButton CheckersButton45;
        private CheckersButton CheckersButton46;
        private CheckersButton CheckersButton47;
        private CheckersButton CheckersButton48;
        private CheckersButton CheckersButton49;
        private CheckersButton CheckersButton50;
        private CheckersButton CheckersButton51;
        private CheckersButton CheckersButton52;
        private CheckersButton CheckersButton53;
        private CheckersButton CheckersButton54;
        private CheckersButton CheckersButton55;
        private CheckersButton CheckersButton56;
        private CheckersButton CheckersButton57;
        private CheckersButton CheckersButton58;
        private CheckersButton CheckersButton59;
        private CheckersButton CheckersButton60;
        private CheckersButton CheckersButton61;
        private CheckersButton CheckersButton62;
        private CheckersButton CheckersButton63;
        private CheckersButton CheckersButton64;
        private CheckersButton CheckersButton25;
        private CheckersButton CheckersButton26;
        private CheckersButton CheckersButton27;
        private CheckersButton CheckersButton28;
        private CheckersButton CheckersButton29;
        private CheckersButton CheckersButton30;
        private CheckersButton CheckersButton31;
        private CheckersButton CheckersButton32;
        private CheckersButton CheckersButton17;
        private CheckersButton CheckersButton18;
        private CheckersButton CheckersButton19;
        private CheckersButton CheckersButton20;
        private CheckersButton CheckersButton21;
        private CheckersButton CheckersButton22;
        private CheckersButton CheckersButton23;
        private CheckersButton CheckersButton24;
        private CheckersButton CheckersButton9;
        private CheckersButton CheckersButton10;
        private CheckersButton CheckersButton11;
        private CheckersButton CheckersButton12;
        private CheckersButton CheckersButton13;
        private CheckersButton CheckersButton14;
        private CheckersButton CheckersButton15;
        private CheckersButton CheckersButton16;
        private YotamButton QuitButton;
        private YotamButton ButtonGameSettings;
        private YotamPanel panel3;
        private YotamPanel panelGameSettings;
        private YotamButton ButtonClosePanel;
        private Panel panel1;
        private Label labelChooseComputer2;
        private Label labelChooseComputer1;
        private ComboBox comboBoxComputer2;
        private ComboBox comboBoxComputer1;
        private Label labelDepth;
        private NumericUpDown numericUpDownDepth;
        private Label labelChooseWhite;
        private ComboBox comboBoxChooseWhite;

        private void InitializeComponent()
        {
            buttonPreviousMove = new YotamButton();
            buttonNextMove = new YotamButton();
            buttonExit = new YotamButton();
            panel1 = new Panel();
            labelDepth = new Label();
            numericUpDownDepth = new NumericUpDown();
            labelChooseComputer2 = new Label();
            labelChooseComputer1 = new Label();
            comboBoxComputer2 = new ComboBox();
            comboBoxComputer1 = new ComboBox();
            comboBoxChooseWhite = new ComboBox();
            labelChooseWhite = new Label();
            ButtonStartMatch = new YotamButton();
            bool fill = true;
            mainPanel = new YotamPanel(!fill);
            this.panelCheckers = new YotamPanel(!fill);
            this.CheckersButton8 = new CheckersButton(BitUtils.BitPositionToUInt(3));
            this.CheckersButton7 = new CheckersButton(0);
            this.CheckersButton6 = new CheckersButton(BitUtils.BitPositionToUInt(11));
            this.CheckersButton5 = new CheckersButton(0);
            this.CheckersButton4 = new CheckersButton(BitUtils.BitPositionToUInt(19));
            this.CheckersButton3 = new CheckersButton(0);
            this.CheckersButton2 = new CheckersButton(BitUtils.BitPositionToUInt(27));
            this.CheckersButton1 = new CheckersButton(0);
            this.panel2 = new YotamPanel(!fill);
            this.panel3 = new YotamPanel(!fill);
            this.CheckersButton9 = new CheckersButton(0);
            this.CheckersButton10 = new CheckersButton(BitUtils.BitPositionToUInt(7));
            this.CheckersButton11 = new CheckersButton(0);
            this.CheckersButton12 = new CheckersButton(BitUtils.BitPositionToUInt(15));
            this.CheckersButton13 = new CheckersButton(0);
            this.CheckersButton14 = new CheckersButton(BitUtils.BitPositionToUInt(23));
            this.CheckersButton15 = new CheckersButton(0);
            this.CheckersButton16 = new CheckersButton(BitUtils.BitPositionToUInt(31));
            this.CheckersButton17 = new CheckersButton(BitUtils.BitPositionToUInt(2));
            this.CheckersButton18 = new CheckersButton(0);
            this.CheckersButton19 = new CheckersButton(BitUtils.BitPositionToUInt(10));
            this.CheckersButton20 = new CheckersButton(0);
            this.CheckersButton21 = new CheckersButton(BitUtils.BitPositionToUInt(18));
            this.CheckersButton22 = new CheckersButton(0);
            this.CheckersButton23 = new CheckersButton(BitUtils.BitPositionToUInt(26));
            this.CheckersButton24 = new CheckersButton(0);
            this.CheckersButton25 = new CheckersButton(0);
            this.CheckersButton26 = new CheckersButton(BitUtils.BitPositionToUInt(6));
            this.CheckersButton27 = new CheckersButton(0);
            this.CheckersButton28 = new CheckersButton(BitUtils.BitPositionToUInt(14));
            this.CheckersButton29 = new CheckersButton(0);
            this.CheckersButton30 = new CheckersButton(BitUtils.BitPositionToUInt(22));
            this.CheckersButton31 = new CheckersButton(0);
            this.CheckersButton32 = new CheckersButton(BitUtils.BitPositionToUInt(30));
            this.CheckersButton33 = new CheckersButton(0);
            this.CheckersButton34 = new CheckersButton(BitUtils.BitPositionToUInt(4));
            this.CheckersButton35 = new CheckersButton(0);
            this.CheckersButton36 = new CheckersButton(BitUtils.BitPositionToUInt(12));
            this.CheckersButton37 = new CheckersButton(0);
            this.CheckersButton38 = new CheckersButton(BitUtils.BitPositionToUInt(20));
            this.CheckersButton39 = new CheckersButton(0);
            this.CheckersButton40 = new CheckersButton(BitUtils.BitPositionToUInt(28));
            this.CheckersButton41 = new CheckersButton(BitUtils.BitPositionToUInt(0));
            this.CheckersButton42 = new CheckersButton(0);
            this.CheckersButton43 = new CheckersButton(BitUtils.BitPositionToUInt(8));
            this.CheckersButton44 = new CheckersButton(0);
            this.CheckersButton45 = new CheckersButton(BitUtils.BitPositionToUInt(16));
            this.CheckersButton46 = new CheckersButton(0);
            this.CheckersButton47 = new CheckersButton(BitUtils.BitPositionToUInt(24));
            this.CheckersButton48 = new CheckersButton(0);
            this.CheckersButton49 = new CheckersButton(0);
            this.CheckersButton50 = new CheckersButton(BitUtils.BitPositionToUInt(5));
            this.CheckersButton51 = new CheckersButton(0);
            this.CheckersButton52 = new CheckersButton(BitUtils.BitPositionToUInt(13));
            this.CheckersButton53 = new CheckersButton(0);
            this.CheckersButton54 = new CheckersButton(BitUtils.BitPositionToUInt(21));
            this.CheckersButton55 = new CheckersButton(0);
            this.CheckersButton56 = new CheckersButton(BitUtils.BitPositionToUInt(29));
            this.CheckersButton57 = new CheckersButton(BitUtils.BitPositionToUInt(1));
            this.CheckersButton58 = new CheckersButton(0);
            this.CheckersButton59 = new CheckersButton(BitUtils.BitPositionToUInt(9));
            this.CheckersButton60 = new CheckersButton(0);
            this.CheckersButton61 = new CheckersButton(BitUtils.BitPositionToUInt(17));
            this.CheckersButton62 = new CheckersButton(0);
            this.CheckersButton63 = new CheckersButton(BitUtils.BitPositionToUInt(25));
            this.CheckersButton64 = new CheckersButton(0);
            this.ButtonGameSettings = new YotamControls.YotamButton();
            this.QuitButton = new YotamControls.YotamButton();
            this.panelGameSettings = new YotamPanel();
            this.panelCompueterSettings = new YotamPanel();
            this.panelCheckers.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCheckers
            // 
            this.panelCheckers.Controls.Add(this.CheckersButton33);
            this.panelCheckers.Controls.Add(this.CheckersButton34);
            this.panelCheckers.Controls.Add(this.CheckersButton35);
            this.panelCheckers.Controls.Add(this.CheckersButton36);
            this.panelCheckers.Controls.Add(this.CheckersButton37);
            this.panelCheckers.Controls.Add(this.CheckersButton38);
            this.panelCheckers.Controls.Add(this.CheckersButton39);
            this.panelCheckers.Controls.Add(this.CheckersButton40);
            this.panelCheckers.Controls.Add(this.CheckersButton41);
            this.panelCheckers.Controls.Add(this.CheckersButton42);
            this.panelCheckers.Controls.Add(this.CheckersButton43);
            this.panelCheckers.Controls.Add(this.CheckersButton44);
            this.panelCheckers.Controls.Add(this.CheckersButton45);
            this.panelCheckers.Controls.Add(this.CheckersButton46);
            this.panelCheckers.Controls.Add(this.CheckersButton47);
            this.panelCheckers.Controls.Add(this.CheckersButton48);
            this.panelCheckers.Controls.Add(this.CheckersButton49);
            this.panelCheckers.Controls.Add(this.CheckersButton50);
            this.panelCheckers.Controls.Add(this.CheckersButton51);
            this.panelCheckers.Controls.Add(this.CheckersButton52);
            this.panelCheckers.Controls.Add(this.CheckersButton53);
            this.panelCheckers.Controls.Add(this.CheckersButton54);
            this.panelCheckers.Controls.Add(this.CheckersButton55);
            this.panelCheckers.Controls.Add(this.CheckersButton56);
            this.panelCheckers.Controls.Add(this.CheckersButton57);
            this.panelCheckers.Controls.Add(this.CheckersButton58);
            this.panelCheckers.Controls.Add(this.CheckersButton59);
            this.panelCheckers.Controls.Add(this.CheckersButton60);
            this.panelCheckers.Controls.Add(this.CheckersButton61);
            this.panelCheckers.Controls.Add(this.CheckersButton62);
            this.panelCheckers.Controls.Add(this.CheckersButton63);
            this.panelCheckers.Controls.Add(this.CheckersButton64);
            this.panelCheckers.Controls.Add(this.CheckersButton25);
            this.panelCheckers.Controls.Add(this.CheckersButton26);
            this.panelCheckers.Controls.Add(this.CheckersButton27);
            this.panelCheckers.Controls.Add(this.CheckersButton28);
            this.panelCheckers.Controls.Add(this.CheckersButton29);
            this.panelCheckers.Controls.Add(this.CheckersButton30);
            this.panelCheckers.Controls.Add(this.CheckersButton31);
            this.panelCheckers.Controls.Add(this.CheckersButton32);
            this.panelCheckers.Controls.Add(this.CheckersButton17);
            this.panelCheckers.Controls.Add(this.CheckersButton18);
            this.panelCheckers.Controls.Add(this.CheckersButton19);
            this.panelCheckers.Controls.Add(this.CheckersButton20);
            this.panelCheckers.Controls.Add(this.CheckersButton21);
            this.panelCheckers.Controls.Add(this.CheckersButton22);
            this.panelCheckers.Controls.Add(this.CheckersButton23);
            this.panelCheckers.Controls.Add(this.CheckersButton24);
            this.panelCheckers.Controls.Add(this.CheckersButton9);
            this.panelCheckers.Controls.Add(this.CheckersButton10);
            this.panelCheckers.Controls.Add(this.CheckersButton11);
            this.panelCheckers.Controls.Add(this.CheckersButton12);
            this.panelCheckers.Controls.Add(this.CheckersButton13);
            this.panelCheckers.Controls.Add(this.CheckersButton14);
            this.panelCheckers.Controls.Add(this.CheckersButton15);
            this.panelCheckers.Controls.Add(this.CheckersButton16);
            this.panelCheckers.Controls.Add(this.CheckersButton8);
            this.panelCheckers.Controls.Add(this.CheckersButton7);
            this.panelCheckers.Controls.Add(this.CheckersButton6);
            this.panelCheckers.Controls.Add(this.CheckersButton5);
            this.panelCheckers.Controls.Add(this.CheckersButton4);
            this.panelCheckers.Controls.Add(this.CheckersButton3);
            this.panelCheckers.Controls.Add(this.CheckersButton2);
            this.panelCheckers.Controls.Add(this.CheckersButton1);
            this.panelCheckers.Location = new System.Drawing.Point(121, 50);
            this.panelCheckers.Name = "panelCheckers";
            this.panelCheckers.Size = new System.Drawing.Size(650, 615);
            this.panelCheckers.TabIndex = 0;
            panelCheckers.ChangeColor(eColors.DarkGray);
            //
            //Computer Match
            //
            panelChooseOponent = new YotamPanel(!fill);
            buttonChoose2Player = new YotamButton();
            buttonChooseComputer = new YotamButton();
            buttonChooseComputerGame = new YotamButton();

            // 
            // buttonPreviousMove
            // 
            buttonPreviousMove.Location = new Point(792, 671);
            buttonPreviousMove.Name = "buttonPreviousMove";
            buttonPreviousMove.Size = new Size(64, 42);
            buttonPreviousMove.TabIndex = 0;
            buttonPreviousMove.Text = "Prev";
            buttonPreviousMove.UseVisualStyleBackColor = true;
            buttonPreviousMove.SelectThemeColor(eColors.DarkGrayAlternate);
            buttonPreviousMove.Click += ButtonPrev_Click;
            // 
            // buttonNextMove
            // 
            buttonNextMove.Location = new Point(865, 671);
            buttonNextMove.Name = "buttonNextMove";
            buttonNextMove.Size = new Size(64, 42);
            buttonNextMove.TabIndex = 1;
            buttonNextMove.Text = "Next";
            buttonNextMove.UseVisualStyleBackColor = true;
            buttonNextMove.SelectThemeColor(eColors.DarkGrayAlternate);
            buttonNextMove.Click += ButtonNext_Click;

            panelChooseOponent.Controls.Add(buttonChooseComputerGame);
            panelChooseOponent.Controls.Add(buttonChooseComputer);
            panelChooseOponent.Controls.Add(buttonChoose2Player);
            panelChooseOponent.Location = new Point(200, 200);
            panelChooseOponent.Name = "panelChooseOponent";
            panelChooseOponent.Size = new Size(542, 193);
            panelChooseOponent.TabIndex = 0;
            panelChooseOponent.ChangeColor(eColors.DarkGray);
            // 
            // buttonChoose2Player
            // 
            buttonChoose2Player.Location = new Point(3, 3);
            buttonChoose2Player.Name = "buttonChoose2Player";
            buttonChoose2Player.Size = new Size(536, 58);
            buttonChoose2Player.TabIndex = 0;
            buttonChoose2Player.Text = "2 Player";
            buttonChoose2Player.UseVisualStyleBackColor = true;
            buttonChoose2Player.SelectThemeColor(eColors.SoftRed);
            buttonChoose2Player.Click += ButtonStart2PlayerMatch_Click;
            // 
            // buttonChooseComputer
            // 
            buttonChooseComputer.Location = new Point(3, 67);
            buttonChooseComputer.Name = "buttonChooseComputer";
            buttonChooseComputer.Size = new Size(536, 58);
            buttonChooseComputer.TabIndex = 1;
            buttonChooseComputer.Text = "1 Player";
            buttonChooseComputer.UseVisualStyleBackColor = true;
            buttonChooseComputer.SelectThemeColor(eColors.ReddishPurple);
            buttonChooseComputer.Click += ButtonHumanVsComputer_Click;
            // 
            // buttonChooseComputerGame
            // 
            buttonChooseComputerGame.Location = new Point(3, 131);
            buttonChooseComputerGame.Name = "buttonChooseComputerGame";
            buttonChooseComputerGame.Size = new Size(536, 58);
            buttonChooseComputerGame.TabIndex = 2;
            buttonChooseComputerGame.Text = "Computer Game";
            buttonChooseComputerGame.UseVisualStyleBackColor = true;
            buttonChooseComputerGame.SelectThemeColor(eColors.MutedPink);
            buttonChooseComputerGame.Click += ButtonComputerMatch_Click;
            // 
            // CheckersButton8
            // 
            this.CheckersButton8.Location = new System.Drawing.Point(3, 541);
            this.CheckersButton8.Name = "CheckersButton8";
            this.CheckersButton8.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton8.TabIndex = 7;
            this.CheckersButton8.UseVisualStyleBackColor = true;
            // 
            // CheckersButton7
            // 
            this.CheckersButton7.Location = new System.Drawing.Point(3, 465);
            this.CheckersButton7.Name = "CheckersButton7";
            this.CheckersButton7.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton7.TabIndex = 6;
            this.CheckersButton7.UseVisualStyleBackColor = true;
            // 
            // CheckersButton6
            // 
            this.CheckersButton6.Location = new System.Drawing.Point(3, 388);
            this.CheckersButton6.Name = "CheckersButton6";
            this.CheckersButton6.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton6.TabIndex = 5;
            this.CheckersButton6.UseVisualStyleBackColor = true;
            // 
            // CheckersButton5
            // 
            this.CheckersButton5.Location = new System.Drawing.Point(3, 311);
            this.CheckersButton5.Name = "CheckersButton5";
            this.CheckersButton5.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton5.TabIndex = 4;
            this.CheckersButton5.UseVisualStyleBackColor = true;
            // 
            // CheckersButton4
            // 
            this.CheckersButton4.Location = new System.Drawing.Point(3, 234);
            this.CheckersButton4.Name = "CheckersButton4";
            this.CheckersButton4.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton4.TabIndex = 3;
            this.CheckersButton4.UseVisualStyleBackColor = true;
            // 
            // CheckersButton3
            // 
            this.CheckersButton3.Location = new System.Drawing.Point(3, 157);
            this.CheckersButton3.Name = "CheckersButton3";
            this.CheckersButton3.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton3.TabIndex = 2;
            this.CheckersButton3.UseVisualStyleBackColor = true;
            // 
            // CheckersButton2
            // 
            this.CheckersButton2.Location = new System.Drawing.Point(3, 80);
            this.CheckersButton2.Name = "CheckersButton2";
            this.CheckersButton2.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton2.TabIndex = 1;
            this.CheckersButton2.UseVisualStyleBackColor = true;
            // 
            // CheckersButton1
            // 
            this.CheckersButton1.Location = new System.Drawing.Point(3, 3);
            this.CheckersButton1.Name = "CheckersButton1";
            this.CheckersButton1.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton1.TabIndex = 0;
            this.CheckersButton1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.QuitButton);
            this.panel2.Controls.Add(this.ButtonGameSettings);
            this.panel2.Location = new System.Drawing.Point(792, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(137, 615);
            this.panel2.TabIndex = 1;
            panel2.ChangeColor(eColors.DarkGray);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(93, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(22, 612);
            this.panel3.TabIndex = 2;
            // 
            // CheckersButton9
            // 
            this.CheckersButton9.Location = new System.Drawing.Point(84, 541);
            this.CheckersButton9.Name = "CheckersButton9";
            this.CheckersButton9.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton9.TabIndex = 15;
            this.CheckersButton9.UseVisualStyleBackColor = true;
            // 
            // CheckersButton10
            // 
            this.CheckersButton10.Location = new System.Drawing.Point(84, 465);
            this.CheckersButton10.Name = "CheckersButton10";
            this.CheckersButton10.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton10.TabIndex = 14;
            this.CheckersButton10.UseVisualStyleBackColor = true;
            // 
            // CheckersButton11
            // 
            this.CheckersButton11.Location = new System.Drawing.Point(84, 388);
            this.CheckersButton11.Name = "CheckersButton11";
            this.CheckersButton11.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton11.TabIndex = 13;
            this.CheckersButton11.UseVisualStyleBackColor = true;
            // 
            // CheckersButton12
            // 
            this.CheckersButton12.Location = new System.Drawing.Point(84, 311);
            this.CheckersButton12.Name = "CheckersButton12";
            this.CheckersButton12.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton12.TabIndex = 12;
            this.CheckersButton12.UseVisualStyleBackColor = true;
            // 
            // CheckersButton13
            // 
            this.CheckersButton13.Location = new System.Drawing.Point(84, 234);
            this.CheckersButton13.Name = "CheckersButton13";
            this.CheckersButton13.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton13.TabIndex = 11;
            this.CheckersButton13.UseVisualStyleBackColor = true;
            // 
            // CheckersButton14
            // 
            this.CheckersButton14.Location = new System.Drawing.Point(84, 157);
            this.CheckersButton14.Name = "CheckersButton14";
            this.CheckersButton14.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton14.TabIndex = 10;
            this.CheckersButton14.UseVisualStyleBackColor = true;
            // 
            // CheckersButton15
            // 
            this.CheckersButton15.Location = new System.Drawing.Point(84, 80);
            this.CheckersButton15.Name = "CheckersButton15";
            this.CheckersButton15.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton15.TabIndex = 9;
            this.CheckersButton15.UseVisualStyleBackColor = true;
            // 
            // CheckersButton16
            // 
            this.CheckersButton16.Location = new System.Drawing.Point(84, 3);
            this.CheckersButton16.Name = "CheckersButton16";
            this.CheckersButton16.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton16.TabIndex = 8;
            this.CheckersButton16.UseVisualStyleBackColor = true;
            // 
            // CheckersButton17
            // 
            this.CheckersButton17.Location = new System.Drawing.Point(165, 541);
            this.CheckersButton17.Name = "CheckersButton17";
            this.CheckersButton17.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton17.TabIndex = 23;
            this.CheckersButton17.UseVisualStyleBackColor = true;
            // 
            // CheckersButton18
            // 
            this.CheckersButton18.Location = new System.Drawing.Point(165, 465);
            this.CheckersButton18.Name = "CheckersButton18";
            this.CheckersButton18.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton18.TabIndex = 22;
            this.CheckersButton18.UseVisualStyleBackColor = true;
            // 
            // CheckersButton19
            // 
            this.CheckersButton19.Location = new System.Drawing.Point(165, 388);
            this.CheckersButton19.Name = "CheckersButton19";
            this.CheckersButton19.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton19.TabIndex = 21;
            this.CheckersButton19.UseVisualStyleBackColor = true;
            // 
            // CheckersButton20
            // 
            this.CheckersButton20.Location = new System.Drawing.Point(165, 311);
            this.CheckersButton20.Name = "CheckersButton20";
            this.CheckersButton20.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton20.TabIndex = 20;
            this.CheckersButton20.UseVisualStyleBackColor = true;
            // 
            // CheckersButton21
            // 
            this.CheckersButton21.Location = new System.Drawing.Point(165, 234);
            this.CheckersButton21.Name = "CheckersButton21";
            this.CheckersButton21.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton21.TabIndex = 19;
            this.CheckersButton21.UseVisualStyleBackColor = true;
            // 
            // CheckersButton22
            // 
            this.CheckersButton22.Location = new System.Drawing.Point(165, 157);
            this.CheckersButton22.Name = "CheckersButton22";
            this.CheckersButton22.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton22.TabIndex = 18;
            this.CheckersButton22.UseVisualStyleBackColor = true;
            // 
            // CheckersButton23
            // 
            this.CheckersButton23.Location = new System.Drawing.Point(165, 80);
            this.CheckersButton23.Name = "CheckersButton23";
            this.CheckersButton23.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton23.TabIndex = 17;
            this.CheckersButton23.UseVisualStyleBackColor = true;
            // 
            // CheckersButton24
            // 
            this.CheckersButton24.Location = new System.Drawing.Point(165, 3);
            this.CheckersButton24.Name = "CheckersButton24";
            this.CheckersButton24.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton24.TabIndex = 16;
            this.CheckersButton24.UseVisualStyleBackColor = true;
            // 
            // CheckersButton25
            // 
            this.CheckersButton25.Location = new System.Drawing.Point(246, 541);
            this.CheckersButton25.Name = "CheckersButton25";
            this.CheckersButton25.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton25.TabIndex = 31;
            this.CheckersButton25.UseVisualStyleBackColor = true;
            // 
            // CheckersButton26
            // 
            this.CheckersButton26.Location = new System.Drawing.Point(246, 465);
            this.CheckersButton26.Name = "CheckersButton26";
            this.CheckersButton26.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton26.TabIndex = 30;
            this.CheckersButton26.UseVisualStyleBackColor = true;
            // 
            // CheckersButton27
            // 
            this.CheckersButton27.Location = new System.Drawing.Point(246, 388);
            this.CheckersButton27.Name = "CheckersButton27";
            this.CheckersButton27.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton27.TabIndex = 29;
            this.CheckersButton27.UseVisualStyleBackColor = true;
            // 
            // CheckersButton28
            // 
            this.CheckersButton28.Location = new System.Drawing.Point(246, 311);
            this.CheckersButton28.Name = "CheckersButton28";
            this.CheckersButton28.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton28.TabIndex = 28;
            this.CheckersButton28.UseVisualStyleBackColor = true;
            // 
            // CheckersButton29
            // 
            this.CheckersButton29.Location = new System.Drawing.Point(246, 234);
            this.CheckersButton29.Name = "CheckersButton29";
            this.CheckersButton29.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton29.TabIndex = 27;
            this.CheckersButton29.UseVisualStyleBackColor = true;
            // 
            // CheckersButton30
            // 
            this.CheckersButton30.Location = new System.Drawing.Point(246, 157);
            this.CheckersButton30.Name = "CheckersButton30";
            this.CheckersButton30.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton30.TabIndex = 26;
            this.CheckersButton30.UseVisualStyleBackColor = true;
            // 
            // CheckersButton31
            // 
            this.CheckersButton31.Location = new System.Drawing.Point(246, 80);
            this.CheckersButton31.Name = "CheckersButton31";
            this.CheckersButton31.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton31.TabIndex = 25;
            this.CheckersButton31.UseVisualStyleBackColor = true;
            // 
            // CheckersButton32
            // 
            this.CheckersButton32.Location = new System.Drawing.Point(246, 3);
            this.CheckersButton32.Name = "CheckersButton32";
            this.CheckersButton32.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton32.TabIndex = 24;
            this.CheckersButton32.UseVisualStyleBackColor = true;
            // 
            // CheckersButton33
            // 
            this.CheckersButton33.Location = new System.Drawing.Point(571, 541);
            this.CheckersButton33.Name = "CheckersButton33";
            this.CheckersButton33.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton33.TabIndex = 63;
            this.CheckersButton33.UseVisualStyleBackColor = true;
            // 
            // CheckersButton34
            // 
            this.CheckersButton34.Location = new System.Drawing.Point(571, 465);
            this.CheckersButton34.Name = "CheckersButton34";
            this.CheckersButton34.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton34.TabIndex = 62;
            this.CheckersButton34.UseVisualStyleBackColor = true;
            // 
            // CheckersButton35
            // 
            this.CheckersButton35.Location = new System.Drawing.Point(571, 388);
            this.CheckersButton35.Name = "CheckersButton35";
            this.CheckersButton35.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton35.TabIndex = 61;
            this.CheckersButton35.UseVisualStyleBackColor = true;
            // 
            // CheckersButton36
            // 
            this.CheckersButton36.Location = new System.Drawing.Point(571, 311);
            this.CheckersButton36.Name = "CheckersButton36";
            this.CheckersButton36.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton36.TabIndex = 60;
            this.CheckersButton36.UseVisualStyleBackColor = true;
            // 
            // CheckersButton37
            // 
            this.CheckersButton37.Location = new System.Drawing.Point(571, 234);
            this.CheckersButton37.Name = "CheckersButton37";
            this.CheckersButton37.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton37.TabIndex = 59;
            this.CheckersButton37.UseVisualStyleBackColor = true;
            // 
            // CheckersButton38
            // 
            this.CheckersButton38.Location = new System.Drawing.Point(571, 157);
            this.CheckersButton38.Name = "CheckersButton38";
            this.CheckersButton38.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton38.TabIndex = 58;
            this.CheckersButton38.UseVisualStyleBackColor = true;
            // 
            // CheckersButton39
            // 
            this.CheckersButton39.Location = new System.Drawing.Point(571, 80);
            this.CheckersButton39.Name = "CheckersButton39";
            this.CheckersButton39.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton39.TabIndex = 57;
            this.CheckersButton39.UseVisualStyleBackColor = true;
            // 
            // CheckersButton40
            // 
            this.CheckersButton40.Location = new System.Drawing.Point(571, 3);
            this.CheckersButton40.Name = "CheckersButton40";
            this.CheckersButton40.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton40.TabIndex = 56;
            this.CheckersButton40.UseVisualStyleBackColor = true;
            // 
            // CheckersButton41
            // 
            this.CheckersButton41.Location = new System.Drawing.Point(490, 541);
            this.CheckersButton41.Name = "CheckersButton41";
            this.CheckersButton41.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton41.TabIndex = 55;
            this.CheckersButton41.UseVisualStyleBackColor = true;
            // 
            // CheckersButton42
            // 
            this.CheckersButton42.Location = new System.Drawing.Point(490, 465);
            this.CheckersButton42.Name = "CheckersButton42";
            this.CheckersButton42.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton42.TabIndex = 54;
            this.CheckersButton42.UseVisualStyleBackColor = true;
            // 
            // CheckersButton43
            // 
            this.CheckersButton43.Location = new System.Drawing.Point(490, 388);
            this.CheckersButton43.Name = "CheckersButton43";
            this.CheckersButton43.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton43.TabIndex = 53;
            this.CheckersButton43.UseVisualStyleBackColor = true;
            // 
            // CheckersButton44
            // 
            this.CheckersButton44.Location = new System.Drawing.Point(490, 311);
            this.CheckersButton44.Name = "CheckersButton44";
            this.CheckersButton44.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton44.TabIndex = 52;
            this.CheckersButton44.UseVisualStyleBackColor = true;
            // 
            // CheckersButton45
            // 
            this.CheckersButton45.Location = new System.Drawing.Point(490, 234);
            this.CheckersButton45.Name = "CheckersButton45";
            this.CheckersButton45.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton45.TabIndex = 51;
            this.CheckersButton45.UseVisualStyleBackColor = true;
            // 
            // CheckersButton46
            // 
            this.CheckersButton46.Location = new System.Drawing.Point(490, 157);
            this.CheckersButton46.Name = "CheckersButton46";
            this.CheckersButton46.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton46.TabIndex = 50;
            this.CheckersButton46.UseVisualStyleBackColor = true;
            // 
            // CheckersButton47
            // 
            this.CheckersButton47.Location = new System.Drawing.Point(490, 80);
            this.CheckersButton47.Name = "CheckersButton47";
            this.CheckersButton47.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton47.TabIndex = 49;
            this.CheckersButton47.UseVisualStyleBackColor = true;
            // 
            // CheckersButton48
            // 
            this.CheckersButton48.Location = new System.Drawing.Point(490, 3);
            this.CheckersButton48.Name = "CheckersButton48";
            this.CheckersButton48.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton48.TabIndex = 48;
            this.CheckersButton48.UseVisualStyleBackColor = true;
            // 
            // CheckersButton49
            // 
            this.CheckersButton49.Location = new System.Drawing.Point(409, 541);
            this.CheckersButton49.Name = "CheckersButton49";
            this.CheckersButton49.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton49.TabIndex = 47;
            this.CheckersButton49.UseVisualStyleBackColor = true;
            // 
            // CheckersButton50
            // 
            this.CheckersButton50.Location = new System.Drawing.Point(409, 465);
            this.CheckersButton50.Name = "CheckersButton50";
            this.CheckersButton50.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton50.TabIndex = 46;
            this.CheckersButton50.UseVisualStyleBackColor = true;
            // 
            // CheckersButton51
            // 
            this.CheckersButton51.Location = new System.Drawing.Point(409, 388);
            this.CheckersButton51.Name = "CheckersButton51";
            this.CheckersButton51.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton51.TabIndex = 45;
            this.CheckersButton51.UseVisualStyleBackColor = true;
            // 
            // CheckersButton52
            // 
            this.CheckersButton52.Location = new System.Drawing.Point(409, 311);
            this.CheckersButton52.Name = "CheckersButton52";
            this.CheckersButton52.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton52.TabIndex = 44;
            this.CheckersButton52.UseVisualStyleBackColor = true;
            // 
            // CheckersButton53
            // 
            this.CheckersButton53.Location = new System.Drawing.Point(409, 234);
            this.CheckersButton53.Name = "CheckersButton53";
            this.CheckersButton53.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton53.TabIndex = 43;
            this.CheckersButton53.UseVisualStyleBackColor = true;
            // 
            // CheckersButton54
            // 
            this.CheckersButton54.Location = new System.Drawing.Point(409, 157);
            this.CheckersButton54.Name = "CheckersButton54";
            this.CheckersButton54.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton54.TabIndex = 42;
            this.CheckersButton54.UseVisualStyleBackColor = true;
            // 
            // CheckersButton55
            // 
            this.CheckersButton55.Location = new System.Drawing.Point(409, 80);
            this.CheckersButton55.Name = "CheckersButton55";
            this.CheckersButton55.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton55.TabIndex = 41;
            this.CheckersButton55.UseVisualStyleBackColor = true;
            // 
            // CheckersButton56
            // 
            this.CheckersButton56.Location = new System.Drawing.Point(409, 3);
            this.CheckersButton56.Name = "CheckersButton56";
            this.CheckersButton56.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton56.TabIndex = 40;
            this.CheckersButton56.UseVisualStyleBackColor = true;
            // 
            // CheckersButton57
            // 
            this.CheckersButton57.Location = new System.Drawing.Point(328, 541);
            this.CheckersButton57.Name = "CheckersButton57";
            this.CheckersButton57.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton57.TabIndex = 39;
            this.CheckersButton57.UseVisualStyleBackColor = true;
            // 
            // CheckersButton58
            // 
            this.CheckersButton58.Location = new System.Drawing.Point(328, 465);
            this.CheckersButton58.Name = "CheckersButton58";
            this.CheckersButton58.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton58.TabIndex = 38;
            this.CheckersButton58.UseVisualStyleBackColor = true;
            // 
            // CheckersButton59
            // 
            this.CheckersButton59.Location = new System.Drawing.Point(328, 388);
            this.CheckersButton59.Name = "CheckersButton59";
            this.CheckersButton59.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton59.TabIndex = 37;
            this.CheckersButton59.UseVisualStyleBackColor = true;
            // 
            // CheckersButton60
            // 
            this.CheckersButton60.Location = new System.Drawing.Point(328, 311);
            this.CheckersButton60.Name = "CheckersButton60";
            this.CheckersButton60.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton60.TabIndex = 36;
            this.CheckersButton60.UseVisualStyleBackColor = true;
            // 
            // CheckersButton61
            // 
            this.CheckersButton61.Location = new System.Drawing.Point(328, 234);
            this.CheckersButton61.Name = "CheckersButton61";
            this.CheckersButton61.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton61.TabIndex = 35;
            this.CheckersButton61.UseVisualStyleBackColor = true;
            // 
            // CheckersButton62
            // 
            this.CheckersButton62.Location = new System.Drawing.Point(328, 157);
            this.CheckersButton62.Name = "CheckersButton62";
            this.CheckersButton62.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton62.TabIndex = 34;
            this.CheckersButton62.UseVisualStyleBackColor = true;
            // 
            // CheckersButton63
            // 
            this.CheckersButton63.Location = new System.Drawing.Point(328, 80);
            this.CheckersButton63.Name = "CheckersButton63";
            this.CheckersButton63.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton63.TabIndex = 33;
            this.CheckersButton63.UseVisualStyleBackColor = true;
            // 
            // CheckersButton64
            // 
            this.CheckersButton64.Location = new System.Drawing.Point(328, 3);
            this.CheckersButton64.Name = "CheckersButton64";
            this.CheckersButton64.Size = new System.Drawing.Size(75, 71);
            this.CheckersButton64.TabIndex = 32;
            this.CheckersButton64.UseVisualStyleBackColor = true;
            // 
            // CheckersButton65
            // 
            this.ButtonGameSettings.Location = new System.Drawing.Point(3, 3);
            this.ButtonGameSettings.Name = "ButtonGameSettings";
            this.ButtonGameSettings.Size = new System.Drawing.Size(131, 71);
            this.ButtonGameSettings.TabIndex = 0;
            this.ButtonGameSettings.UseVisualStyleBackColor = true;
            this.ButtonGameSettings.Text = "Game Settings";
            ButtonGameSettings.SelectThemeColor(eColors.DarkGrayAlternate);
            ButtonGameSettings.Click += ButtonGameSettings_Click;
            // 
            // CheckersButton67
            // 
            this.QuitButton.Location = new System.Drawing.Point(3, 541);
            this.QuitButton.Name = "CheckersButton67";
            this.QuitButton.Size = new System.Drawing.Size(131, 71);
            this.QuitButton.TabIndex = 2;
            this.QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Text = "Quit";
            QuitButton.SelectThemeColor(eColors.ReddishPurple);
            QuitButton.Click += ButtonQuit_Click;
            //
            //Button Close Panel
            //
            ButtonClosePanel = new YotamButton();
            this.ButtonClosePanel.SelectThemeColor(eColors.ReddishPurple);
            ButtonClosePanel.Name = "ButtonClosePanel";
            ButtonClosePanel.Size = this.QuitButton.Size = new System.Drawing.Size(131, 71);
            ButtonClosePanel.Location = new Point(3, 541);
            ButtonClosePanel.Text = "Close";
            ButtonClosePanel.Click += ButtonClosePanel_Click;

            ComputerMatch = new YotamButton();
            ComputerMatch.SelectThemeColor(eColors.MutedPink);
            ComputerMatch.Location = ButtonGameSettings.Location;
            ComputerMatch.Size = ButtonGameSettings.Size;
            ComputerMatch.Text = "Enable Computer Match";
            ComputerMatch.Click += ButtonComputerMatch_Click;

            ButtonStartMatch.SelectThemeColor(eColors.Green);
            ButtonStartMatch.Location = new Point(12, 137); 
            ButtonStartMatch.Size = new Size(518, 44);
            ButtonStartMatch.Text = "Start";

            buttonExit.Location = new Point(506, 4);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(33, 33);
            buttonExit.TabIndex = 9;
            buttonExit.Text = "X";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.TextAlign = ContentAlignment.MiddleCenter; // Align text to the center
            buttonExit.Click += ButtonExit_Click;
            buttonExit.SelectThemeColor(eColors.ReddishPurple);

            labelDepth.AutoSize = true;
            labelDepth.Location = new Point(239, 24);
            labelDepth.Name = "labelDepth";
            labelDepth.Size = new Size(52, 15);
            labelDepth.TabIndex = 5;
            labelDepth.Text = "Strength";
            labelDepth.ForeColor = System.Drawing.Color.White;
            // 
            // numericUpDownDepth
            // 
            numericUpDownDepth.Location = new Point(248, 42);
            numericUpDownDepth.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownDepth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownDepth.Name = "numericUpDownDepth";
            numericUpDownDepth.Size = new Size(34, 23);
            numericUpDownDepth.TabIndex = 4;
            numericUpDownDepth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelChooseComputer2
            // 
            labelChooseComputer2.AutoSize = true;
            labelChooseComputer2.Location = new Point(27, 80);
            labelChooseComputer2.Name = "labelChooseComputer2";
            labelChooseComputer2.Size = new Size(113, 15);
            labelChooseComputer2.TabIndex = 3;
            labelChooseComputer2.Text = "Choose Computer 2";
            labelChooseComputer2.ForeColor = System.Drawing.Color.White;
            // 
            // labelChooseComputer1
            // 
            labelChooseComputer1.AutoSize = true;
            labelChooseComputer1.Location = new Point(27, 24);
            labelChooseComputer1.Name = "labelChooseComputer1";
            labelChooseComputer1.Size = new Size(113, 15);
            labelChooseComputer1.TabIndex = 2;
            labelChooseComputer1.Text = "Choose Computer";
            labelChooseComputer1.ForeColor = System.Drawing.Color.White;
            // 
            // comboBoxComputer2
            // 
            comboBoxComputer2.FormattingEnabled = true;
            comboBoxComputer2.Location = new Point(27, 98);
            comboBoxComputer2.Name = "comboBoxComputer2";
            comboBoxComputer2.Size = new Size(113, 23);
            comboBoxComputer2.TabIndex = 1;
            UIUtils.AddComputerOptions(comboBoxComputer2);
            // 
            // comboBoxComputer1
            // 
            comboBoxComputer1.FormattingEnabled = true;
            comboBoxComputer1.Location = new Point(27, 42);
            comboBoxComputer1.Name = "comboBoxComputer1";
            comboBoxComputer1.Size = new Size(113, 23);
            comboBoxComputer1.TabIndex = 0;
            UIUtils.AddComputerOptions(comboBoxComputer1);
            // 
            // comboBoxChooseWhite
            // 
            comboBoxChooseWhite.FormattingEnabled = true;
            comboBoxChooseWhite.Location = new Point(392, 42);
            comboBoxChooseWhite.Name = "comboBoxChooseWhite";
            comboBoxChooseWhite.Size = new Size(81, 23);
            comboBoxChooseWhite.TabIndex = 6;
            // 
            // labelChooseWhite
            // 
            labelChooseWhite.AutoSize = true;
            labelChooseWhite.Location = new Point(392, 24);
            labelChooseWhite.Name = "labelChooseWhite";
            labelChooseWhite.Size = new Size(81, 15);
            labelChooseWhite.TabIndex = 7;
            labelChooseWhite.Text = "Choose White";
            labelChooseWhite.ForeColor = System.Drawing.Color.White;
            //
            //panelsCapture
            //
            panelWhiteCapture = new YotamImagePanel();
            panelBlackCapture = new YotamImagePanel();
            panelBlackCapture.Location = new Point(panelCheckers.Location.X, panelCheckers.Top - 35);
            panelWhiteCapture.Location = new Point(panelCheckers.Location.X, panelCheckers.Bottom + 5);
            panelWhiteCapture.Size = new Size(panelCheckers.Width, 30);
            panelBlackCapture.Size = new Size(panelCheckers.Width, 30);
            panelBlackCapture.ChangeColor(eColors.MediumGray);
            panelWhiteCapture.ChangeColor(eColors.MediumGray);
            panelWhiteCapture.BorderStyle = BorderStyle.FixedSingle;
            panelBlackCapture.BorderStyle = BorderStyle.FixedSingle;
            panelBlackCapture.Image = Images.white_pawn_icon;
            panelWhiteCapture.Image = Images.black_pawn_icon;
            //
            //panel game settings and it's components
            //
            panelGameSettings.Size = panel2.Size;
            panelGameSettings.ChangeColor(eColors.DarkGrayAlternate);
            LabelQueenMoveLimit = new Label();
            LabelQueenLimitText = new Label();
            TrackBarQueenMoveLimit = new TrackBar();
            panelGameSettings.Controls.Add(LabelQueenLimitText);
            panelGameSettings.Controls.Add(LabelQueenMoveLimit);
            panelGameSettings.Controls.Add(TrackBarQueenMoveLimit);
            //
            // panel computer settings
            //
            panelCompueterSettings.Size = panel2.Size;
            panelCompueterSettings.ChangeColor(eColors.DarkGrayAlternate);
            panelCompueterSettings.Controls.Add(ComputerMatch);
            panelCompueterSettings.Controls.Add(ButtonStartMatch);
            panelCompueterSettings.Controls.Add(labelDepth);
            panelCompueterSettings.Controls.Add(numericUpDownDepth);
            panelCompueterSettings.Controls.Add(labelChooseComputer2);
            panelCompueterSettings.Controls.Add(labelChooseComputer1);
            panelCompueterSettings.Controls.Add(comboBoxComputer1);
            panelCompueterSettings.Controls.Add(comboBoxComputer2);
            panelCompueterSettings.Controls.Add(comboBoxChooseWhite);
            panelCompueterSettings.Controls.Add(labelChooseWhite);

            // 
            // LabelQueenMoveLimit
            // 
            LabelQueenMoveLimit.AutoSize = true;
            LabelQueenMoveLimit.Name = "LabelQueenMoveLimit";
            LabelQueenMoveLimit.TabIndex = 2;
            LabelQueenMoveLimit.Text = "1";
            LabelQueenMoveLimit.ForeColor = Color.White;
            LabelQueenMoveLimit.AutoSize = false;
            LabelQueenMoveLimit.TextAlign = ContentAlignment.MiddleCenter;
            LabelQueenMoveLimit.Size = new Size(panelGameSettings.Size.Width - 20, 30); // Adjusted size based on panelGameSettings
            LabelQueenMoveLimit.Location = new Point((panelGameSettings.Width - LabelQueenMoveLimit.Width) / 2, 20);
            // 
            // label1
            // 
            LabelQueenLimitText.AutoSize = true;
            LabelQueenLimitText.Name = "label1";
            LabelQueenLimitText.TabIndex = 1;
            LabelQueenLimitText.Text = "Queen Move Limit:";
            LabelQueenLimitText.ForeColor = Color.White;
            LabelQueenLimitText.AutoSize = true;
            LabelQueenLimitText.TextAlign = ContentAlignment.MiddleCenter;
            LabelQueenLimitText.Size = new Size(panelGameSettings.Size.Width - 20, 30); // Adjusted size based on panelGameSettings
            LabelQueenLimitText.Location = new Point((panelGameSettings.Width - LabelQueenLimitText.Width) / 2, 5);
            // 
            // trackBarQueenMoveLimit
            // 
            TrackBarQueenMoveLimit.LargeChange = 1;
            TrackBarQueenMoveLimit.Maximum = 7;
            TrackBarQueenMoveLimit.Minimum = 1;
            TrackBarQueenMoveLimit.Name = "trackBarQueenMoveLimit";
            TrackBarQueenMoveLimit.TabIndex = 0;
            TrackBarQueenMoveLimit.TabStop = false;
            TrackBarQueenMoveLimit.Value = 1;
            TrackBarQueenMoveLimit.ValueChanged += TrackBarQueenMoveLimit_ValueChanged;
            TrackBarQueenMoveLimit.Size = new Size(panelGameSettings.Size.Width - 20, 45); // Adjusted size based on panelGameSettings
            TrackBarQueenMoveLimit.Location = new Point((panelGameSettings.Width - TrackBarQueenMoveLimit.Width) / 2, 50);
            // 
            // FormsUI
            // 
            this.ClientSize = new System.Drawing.Size(941, 714);
            mainPanel.Size = this.Size;
            mainPanel.ChangeColor(eColors.MediumGray);
            this.mainPanel.Controls.Add(panel3);
            this.mainPanel.Controls.Add(panel2);
            this.mainPanel.Controls.Add(panelCheckers);
            this.mainPanel.Controls.Add(panelWhiteCapture);
            this.mainPanel.Controls.Add(panelBlackCapture);
            mainPanel.Controls.Add(buttonNextMove);
            mainPanel.Controls.Add(buttonPreviousMove);
            
            mainPanel.Controls.Add(panelChooseOponent);
            hideCheckersBoardPanels();


            panelWhiteCapture.RefreshPanel();
            this.Controls.Add(this.mainPanel);
            this.Name = "FormsUI";
            this.panelCheckers.ResumeLayout(false);
            this.panelCheckers.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            panel3.Visible = false;
            this.ResumeLayout(false);

        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            hideSelectionOptions();
            showPanelSetupGame();
        }

        private void showPanelSetupGame()
        {
            panelChooseOponent.Controls.Add(buttonChoose2Player);
            panelChooseOponent.Controls.Add(buttonChooseComputerGame);
            panelChooseOponent.Controls.Add(buttonChooseComputer);
        }


        private void ButtonQuit_Click(object sender, EventArgs e)
        {
            GameMasterSingleton.Instance.Board = null;
            GameMasterSingleton.Instance.MatchManager.StopMatch();
            hideCheckersBoardPanels();
            showPanelSetupGame();
        }

        private void hideSelectionOptions()
        {
            panelChooseOponent.Controls.Remove(comboBoxComputer1);
            panelChooseOponent.Controls.Remove(labelChooseComputer2);
            panelChooseOponent.Controls.Remove(labelChooseComputer1);
            panelChooseOponent.Controls.Remove(ButtonStartMatch);
            panelChooseOponent.Controls.Remove(labelChooseWhite);
            panelChooseOponent.Controls.Remove(comboBoxComputer2);
            panelChooseOponent.Controls.Remove(comboBoxChooseWhite);
            panelChooseOponent.Controls.Remove(labelDepth);
            panelChooseOponent.Controls.Remove(numericUpDownDepth);
            panelChooseOponent.Controls.Remove(buttonExit);
        }

        private void ButtonGameSettings_Click(object sender, EventArgs e)
        {
            panelGameSettings.Controls.Add(ButtonClosePanel);
            panel2.Controls.Add(panelGameSettings);
            panelGameSettings.BringToFront();
        }

        private void ButtonClosePanel_Click(object sender, EventArgs e)
        {
            YotamPanel parentPanel = (sender as Control).Parent as YotamPanel;
            panel2.Controls.Remove(parentPanel);
        }

        private void ButtonComputerMatch_Click(object sender, EventArgs e)
        {
            comboBoxChooseWhite.Items.Clear();
            comboBoxChooseWhite.Items.AddRange(new object[] { "Computer1", "Computer2" });
            panel2.Controls.Remove(ButtonGameSettings);
            panelChooseOponent.Controls.Remove(buttonChoose2Player);
            panelChooseOponent.Controls.Remove(buttonChooseComputerGame);
            panelChooseOponent.Controls.Remove(buttonChooseComputer);
            panelChooseOponent.Controls.Add(comboBoxComputer1);
            panelChooseOponent.Controls.Add(labelChooseComputer2);
            panelChooseOponent.Controls.Add(labelChooseComputer1);
            panelChooseOponent.Controls.Add(ButtonStartMatch);
            panelChooseOponent.Controls.Add(labelChooseWhite);
            panelChooseOponent.Controls.Add(comboBoxComputer2);
            panelChooseOponent.Controls.Add(comboBoxChooseWhite);
            panelChooseOponent.Controls.Add(labelDepth);
            panelChooseOponent.Controls.Add(numericUpDownDepth);
            panelChooseOponent.Controls.Add(buttonExit);
            ButtonStartMatch.Click += ButtonStartComputerMatch_Click;
        }

        private void ButtonHumanVsComputer_Click(object sender, EventArgs e)
        {
            comboBoxChooseWhite.Items.Clear();
            comboBoxChooseWhite.Items.AddRange(new object[] { "Player", "Computer" });
            panelChooseOponent.Controls.Remove(buttonChoose2Player);
            panelChooseOponent.Controls.Remove(buttonChooseComputerGame);
            panelChooseOponent.Controls.Remove(buttonChooseComputer);
            panelChooseOponent.Controls.Add(comboBoxComputer1);
            panelChooseOponent.Controls.Add(labelChooseComputer1);
            panelChooseOponent.Controls.Add(ButtonStartMatch);
            panelChooseOponent.Controls.Add(labelChooseWhite);
            panelChooseOponent.Controls.Add(comboBoxChooseWhite);
            panelChooseOponent.Controls.Add(labelDepth);
            panelChooseOponent.Controls.Add(numericUpDownDepth);
            panelChooseOponent.Controls.Add(buttonExit);
            ButtonStartMatch.Click += ButtonStart1PlayerMatch_Click;
        }

        private void ButtonStart2PlayerMatch_Click(object sender, EventArgs e)
        {
            GameMasterSingleton.Instance.Board = new Board();
            this.Reset();
            showCheckersBoardPanels();
            Start2PlayerMatch();
        }

        private void showCheckersBoardPanels()
        {
            panelChooseOponent.Visible = false;
            panelBlackCapture.Visible = true;
            panelWhiteCapture.Visible = true;
            panel2.Visible = true;
            panelCheckers.Visible = true;
            buttonPreviousMove.Visible = true;
            buttonNextMove.Visible = true;
    }

        private void hideCheckersBoardPanels()
        {
            panelChooseOponent.Visible = true;
            panelBlackCapture.Visible = false;
            panelWhiteCapture.Visible = false;
            panel2.Visible = false;
            panelCheckers.Visible = false;
            buttonPreviousMove.Visible = false;
            buttonNextMove.Visible = false;
        }

        private void ButtonStart1PlayerMatch_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            bool validComputer1 = validateComputerMatchEntry(clickedButton, comboBoxComputer1, labelChooseComputer1);
            bool validColorChoice = validateComputerMatchEntry(clickedButton, comboBoxChooseWhite, labelChooseWhite);
            bool validEntry = validComputer1 && validColorChoice;
            if (validEntry)
            {
                showValidButtonPress(clickedButton);
                GameMasterSingleton.Instance.Board = new Board();
                this.Reset();

                bool isPlayerStarting = comboBoxChooseWhite.Text.Equals("Player");
                GameMasterSingleton.Instance.ComputerDepth = (int)numericUpDownDepth.Value;
                EvaluationType computerStrategyType = (EvaluationType)comboBoxComputer1.SelectedIndex;

                Func<uint[], int> computerStrategy = EvaluationMethodsFactory.GetEvaluationStrategy(computerStrategyType);

                hideSelectionOptions();
                showCheckersBoardPanels();
                GameMasterSingleton.Instance.SetUp1PlayerMatch(computerStrategy, this, isPlayerStarting);
                signUpForBoardEvents();
                GameMasterSingleton.Instance.StartMatch();
                clearSubscribersList(sender as Button);
            }
        }

        private void ButtonStartComputerMatch_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            bool validComputer1 = validateComputerMatchEntry(clickedButton, comboBoxComputer1, labelChooseComputer1);
            bool validComputer2 = validateComputerMatchEntry(clickedButton, comboBoxComputer2, labelChooseComputer2);
            bool validColorChoice = validateComputerMatchEntry(clickedButton, comboBoxChooseWhite, labelChooseWhite);
            bool validEntry = validComputer1 && validComputer2 && validColorChoice;
            if (validEntry)
            {
                showValidButtonPress(clickedButton);
                GameMasterSingleton.Instance.Board = new Board();
                this.Reset();

                string starting = comboBoxChooseWhite.Text;
                bool computer1Starting = starting.Equals("Computer1");
                GameMasterSingleton.Instance.ComputerDepth = (int)numericUpDownDepth.Value;
                EvaluationType computer1StrategyType = (EvaluationType)comboBoxComputer1.SelectedIndex;
                EvaluationType computer2StrategyType = (EvaluationType)comboBoxComputer2.SelectedIndex;

                Func<uint[], int> computer1Strategy = EvaluationMethodsFactory.GetEvaluationStrategy(computer1StrategyType);
                Func<uint[], int> computer2Strategy = EvaluationMethodsFactory.GetEvaluationStrategy(computer2StrategyType);

                hideSelectionOptions();
                showCheckersBoardPanels();
                GameMasterSingleton.Instance.SetUpComputerMatch(computer1Strategy, computer2Strategy, computer1Starting);
                signUpForBoardEvents();
                GameMasterSingleton.Instance.StartMatch();
                clearSubscribersList(sender as Button);
            }
        }

        private void clearSubscribersList(Button button)
        {
            button.Click -= ButtonStart1PlayerMatch_Click;
            button.Click -= ButtonStartComputerMatch_Click;
        }

        private void Start2PlayerMatch()
        {
            GameMasterSingleton.Instance.SetUp2PlayerMatch(this);
            signUpForBoardEvents();
            GameMasterSingleton.Instance.StartMatch();
        }

        private bool validateComputerMatchEntry(Button i_StartMatchButton, ComboBox i_ComboBox, Label i_LabelComboBox)
        {
            bool validEntry;
            if (i_ComboBox.SelectedIndex == -1)
            {
                showInvalidButtonPress(i_StartMatchButton);
                showInvalidComboBoxEntry(i_LabelComboBox);
                validEntry = false;
            }
            else
            {
                showValidComboBoxEntry(i_LabelComboBox);
                validEntry= true;
            }

            return validEntry;
        }

        private void showInvalidButtonPress(Button i_Button)
        {
            i_Button.FlatStyle = FlatStyle.Flat;
            i_Button.FlatAppearance.BorderColor = Color.Red;
            i_Button.FlatAppearance.BorderSize = 1;
        }

        private void showValidButtonPress(Button i_Button)
        {
            i_Button.FlatAppearance.BorderSize = 0;
        }

        private void showInvalidComboBoxEntry(Label i_Label)
        {
            i_Label.ForeColor = Color.Red;
        }

        private void showValidComboBoxEntry(Label i_Label)
        {
            i_Label.ForeColor = Color.White;
        }

        private void signUpForBoardEvents()
        {
            GameMasterSingleton.Instance.Board.MadeMove += OnMadeMove;
            GameMasterSingleton.Instance.Board.GameEnded += OnGameEnd;
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (m_CurrentlyViewingPositionNumber + 1 < GameMasterSingleton.Instance.Board.PositionsHistory.Count)
            {
                m_CurrentlyViewingPositionNumber += 1;
                viewHistoryPosition(m_CurrentlyViewingPositionNumber);
            }
        }

        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            if (m_CurrentlyViewingPositionNumber > 0)
            {
                m_CurrentlyViewingPositionNumber -= 1;
                viewHistoryPosition(m_CurrentlyViewingPositionNumber);
            }
        }
    }
}
