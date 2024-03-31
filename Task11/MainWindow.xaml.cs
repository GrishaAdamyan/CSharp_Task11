using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int[,] board = {
                                { 0, 0, 0, 0, -1000, -33, -30, -50 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 10,   0, 0, 0 },
                                { 0, 0, 0, 0, 1000, 0, 0, 0 }
        };

        bool Wpawn = false, Wking = false;
        double Xplace, Yplace;

        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Wpawn)
                wp.Margin = new Thickness(e.GetPosition(this).X - Xplace,
                e.GetPosition(this).Y - Yplace, 0, 0);
            if (Wking)
                wk.Margin = new Thickness(e.GetPosition(this).X - Xplace,
                e.GetPosition(this).Y - Yplace, 0, 0);
        }

        int t = 0, l = 0;

        void wp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                StackPanel.SetZIndex(wp, 1);
                StackPanel.SetZIndex(wk, 0);
                if (Wpawn == false)
                {
                    t = (int)wp.Margin.Top;
                    l = (int)wp.Margin.Left;
                }
                Wpawn = true;
                Xplace = e.GetPosition(this).X - wp.Margin.Left;
                Yplace = e.GetPosition(this).Y - wp.Margin.Top;
            }
        }

        int wp_xindex = 6, wp_yindex = 4;

        void wp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Wpawn = false;
            wp.Margin = new Thickness((int)(wp.Margin.Left + 25) / 50 * 50, (int)(wp.Margin.Top + 25) / 50 * 50, 0, 0);
            board[(int)(wp.Margin.Top + 25) / 50, (int)(wp.Margin.Left + 25) / 50] = 10;
            board[wp_xindex, wp_yindex] = 0;
            wp_xindex = (int)(wp.Margin.Top + 25) / 50;
            wp_yindex = (int)(wp.Margin.Left + 25) / 50;
            Black();
        }

        void wk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                StackPanel.SetZIndex(wk, 1);
                StackPanel.SetZIndex(wp, 0);
                if (!Wking)
                {
                    t = (int)wk.Margin.Top;
                    l = (int)wk.Margin.Left;
                }
                Wking = true;
                Xplace = e.GetPosition(this).X - wk.Margin.Left;
                Yplace = e.GetPosition(this).Y - wk.Margin.Top;
            }
        }

        int wk_xindex = 7, wk_yindex = 4;

        void wk_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Wking = false;
            wk.Margin = new Thickness((int)(wk.Margin.Left + 25) / 50 * 50, (int)(wk.Margin.Top + 25) / 50 * 50, 0, 0);
            board[(int)(wk.Margin.Top + 25) / 50, (int)(wk.Margin.Left + 25) / 50] = 1000;
            board[wk_xindex, wk_yindex] = 0;
            wk_xindex = (int)(wk.Margin.Top + 25) / 50;
            wk_yindex = (int)(wk.Margin.Left + 25) / 50;
            Black();
        }

        int bktplace = 0, bklplace = 4, bbtplace = 0, bblplace = 5, bntplace = 0, bnlplace = 6, brtplace = 0, brlplace = 7;
        int bbtplacetemp = 0, bblplacetemp = 5, bntplacetemp = 0, bnlplacetemp = 6, brtplacetemp = 0, brlplacetemp = 7;
        bool bbcheck = false, brcheck = false, bbchecksec = false, brchecksec = false, bbchecktrd = false, brchecktrd = false, bkcheck;
        bool randcheck = false;

        void Black()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (((Math.Abs(bntplace - i) == 2 && Math.Abs(bnlplace - j) == 1) || (Math.Abs(bntplace - i) == 1 && Math.Abs(bnlplace - j) == 2)) && board[i, j] == 1000)
                    {
                        board[bntplace, bnlplace] = 0;
                        bntplace = i;
                        bnlplace = j;
                        board[bntplace, bnlplace] = -30;
                        bn.Margin = new Thickness(j * 50, i * 50, 0, 0);
                        return;
                    }
                    else if (Math.Abs(bbtplace - i) == Math.Abs(bblplace - j) && board[i, j] == 1000)
                    {
                        bbcheck = false;
                        for (int l = 1; l < Math.Abs(i - bbtplace); l++)
                        {
                            if (i > bbtplace && j < bblplace && board[bbtplace + l, bblplace - l] != 0)
                            {
                                bbchecksec = true;
                                break;
                            }
                            if (i > bbtplace && j > bblplace && board[bbtplace + l, bblplace + l] != 0)
                            {
                                bbchecksec = true;
                                break;
                            }
                            if (i < bbtplace && j < bblplace && board[bbtplace - l, bblplace - l] != 0)
                            {
                                bbchecksec = true;
                                break;
                            }
                            if (i < bbtplace && j > bblplace && board[bbtplace - l, bblplace + l] != 0)
                            {
                                bbchecksec = true;
                                break;
                            }
                        }
                        if (!bbchecksec)
                        {
                            board[bbtplace, bblplace] = 0;
                            bbtplace = i;
                            bblplace = j;
                            board[bbtplace, bblplace] = -33;
                            bb.Margin = new Thickness(j * 50, i * 50, 0, 0);
                            return;
                        }
                    }
                    else if ((j == brlplace || i == brtplace) && board[i, j] == 1000)
                    {
                        brchecksec = false;
                        if (j == brlplace)
                        {
                            for (int l = 1; l < Math.Abs(i - brtplace); l++)
                            {
                                if (i > brtplace && board[brtplace + l, j] != 0)
                                {
                                    brchecksec = true;
                                    break;
                                }
                                if (i < brtplace && board[brtplace - l, j] != 0)
                                {
                                    brchecksec = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int l = 1; l < Math.Abs(j - brlplace); l++)
                            {
                                if (j > brlplace && board[i, brlplace + l] != 0)
                                {
                                    brchecksec = true;
                                    break;
                                }
                                if (j < brlplace && board[i, brlplace - l] != 0)
                                {
                                    brchecksec = true;
                                    break;
                                }
                            }
                        }
                        if (!brchecksec)
                        {
                            board[brtplace, brlplace] = 0;
                            brtplace = i;
                            brlplace = j;
                            board[brtplace, brlplace] = -50;
                            br.Margin = new Thickness(j * 50, i * 50, 0, 0);
                            return;
                        }
                    }
                }
            }
            bntplacetemp = bntplace;
            bnlplacetemp = bnlplace;
            bbtplacetemp = bbtplace;
            bblplacetemp = bblplace;
            brtplacetemp = brtplace;
            brlplacetemp = brlplace;
            bchecksec();
        }

        void bchecksec()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (((Math.Abs(bntplacetemp - i) == 2 && Math.Abs(bnlplacetemp - j) == 1) || (Math.Abs(bntplacetemp - i) == 1 && Math.Abs(bnlplacetemp - j) == 2)) && board[i, j] >= 0)
                    {
                        if (Knight(i, j) == 11)
                        {
                            return;
                        }
                    }
                    if (Math.Abs(bbtplacetemp - i) == Math.Abs(bblplacetemp - j) && board[i, j] >= 0)
                    {
                        bbcheck = false;
                        for (int l = 1; l < Math.Abs(i - bbtplacetemp); l++)
                        {
                            if (i > bbtplacetemp && j < bblplacetemp && board[bbtplacetemp + l, bblplacetemp - l] != 0)
                            {
                                bbcheck = true;
                            }
                            if (i > bbtplacetemp && j > bblplacetemp && board[bbtplacetemp + l, bblplacetemp + l] != 0)
                            {
                                bbcheck = true;
                            }
                            if (i < bbtplacetemp && j < bblplacetemp && board[bbtplacetemp - l, bblplacetemp - l] != 0)
                            {
                                bbcheck = true;
                            }
                            if (i < bbtplacetemp && j > bblplacetemp && board[bbtplacetemp - l, bblplacetemp + l] != 0)
                            {
                                bbcheck = true;
                            }
                        }
                        if (!bbcheck)
                        {
                            if (Bishop(i, j) == 11)
                            {
                                return;
                            }
                        }
                    }
                    if ((j == brlplacetemp || i == brtplacetemp) && board[i, j] >= 0)
                    {
                        brcheck = false;
                        if (j == brlplacetemp)
                        {
                            for (int l = 1; l < Math.Abs(i - brtplacetemp); l++)
                            {
                                if (i > brtplacetemp && board[brtplacetemp + l, j] != 0)
                                {
                                    brcheck = true;
                                    break;
                                }
                                if (i < brtplacetemp && board[brtplacetemp - l, j] != 0)
                                {
                                    brcheck = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int l = 1; l < Math.Abs(j - brlplacetemp); l++)
                            {
                                if (j > brlplacetemp && board[i, brlplacetemp + l] != 0)
                                {
                                    brcheck = true;
                                    break;
                                }
                                if (j < brlplacetemp && board[i, brlplacetemp - l] != 0)
                                {
                                    brcheck = true;
                                    break;
                                }
                            }
                        }
                        if (!brcheck)
                        {
                            if (Rook(i, j) == 11)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            RandomMove();
        }

        int Knight(int bntplacetemp, int bnlplacetemp)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (((Math.Abs(bntplacetemp - i) == 2 && Math.Abs(bnlplacetemp - j) == 1) || (Math.Abs(bntplacetemp - i) == 1 && Math.Abs(bnlplacetemp - j) == 2)) && board[i, j] == 1000)
                    {
                        if (board[bntplacetemp, bnlplacetemp] >= 0)
                        {
                            board[bntplace, bnlplace] = 0;
                            bntplace = bntplacetemp;
                            bnlplace = bnlplacetemp;
                            board[bntplace, bnlplace] = -30;
                            bn.Margin = new Thickness(bnlplacetemp * 50, bntplacetemp * 50, 0, 0);
                            return 11;
                        }
                    }
                }
            }
            return 0;
        }

        int Bishop(int bbtplacetemp, int bblplacetemp)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(bbtplacetemp - i) == Math.Abs(bblplacetemp - j) && board[i, j] == 1000)
                    {
                        for (int l = 1; l < Math.Abs(wk_xindex - bbtplacetemp); l++)
                        {
                            if (wk_xindex > bbtplacetemp && wk_yindex < bblplacetemp && board[bbtplacetemp + l, bblplacetemp - l] != 0)
                            {
                                return 0;
                            }
                            if (wk_xindex > bbtplacetemp && wk_yindex > bblplacetemp && board[bbtplacetemp + l, bblplacetemp + l] != 0)
                            {
                                return 0;
                            }
                            if (wk_xindex < bbtplacetemp && wk_yindex < bblplacetemp && board[bbtplacetemp - l, bblplacetemp - l] != 0)
                            {
                                return 0;
                            }
                            if (wk_xindex < bbtplacetemp && wk_yindex > bblplacetemp && board[bbtplacetemp - l, bblplacetemp + l] != 0)
                            {
                                return 0;
                            }
                        }
                        if (board[bbtplacetemp, bblplacetemp] >= 0)
                        {
                            board[bbtplace, bblplace] = 0;
                            bbtplace = bbtplacetemp;
                            bblplace = bblplacetemp;
                            board[bbtplace, bblplace] = -33;
                            bb.Margin = new Thickness(bblplacetemp * 50, bbtplacetemp * 50, 0, 0);
                            return 11;
                        }
                    }
                }
            }
            return 0;

        }
        int Rook(int brtplacetemp, int brlplacetemp)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((j == brlplacetemp || i == brtplacetemp) && board[i, j] == 1000)
                    {
                        if (j == brlplacetemp)
                        {
                            for (int l = 1; l < Math.Abs(wk_xindex - brtplacetemp); l++)
                            {
                                if (wk_xindex > brtplacetemp && board[brtplacetemp + l, j] != 0)
                                {
                                    return 0;
                                }
                                if (wk_xindex < brtplacetemp && board[brtplacetemp - l, j] != 0)
                                {
                                    return 0;
                                }
                            }
                        }
                        else
                        {
                            for (int l = 1; l < Math.Abs(wk_yindex - brlplacetemp); l++)
                            {
                                if (wk_yindex > brlplacetemp && board[i, brlplacetemp + l] != 0)
                                {
                                    return 0;
                                }
                                if (wk_yindex < brlplacetemp && board[i, brlplacetemp - l] != 0)
                                {
                                    return 0;
                                }
                            }
                        }
                        if (board[brtplacetemp, brlplacetemp] >= 0)
                        {
                            board[brtplace, brlplace] = 0;
                            brtplace = brtplacetemp;
                            brlplace = brlplacetemp;
                            board[brtplace, brlplace] = -50;
                            br.Margin = new Thickness(brlplacetemp * 50, brtplacetemp * 50, 0, 0);
                            return 11;
                        }
                    }
                }
            }
            return 0;
        }

        void RandomMove()
        {
            randcheck = false;
            Random rand = new Random();
            while (!randcheck)
            {
                int randrow = rand.Next(0, 8);
                int randcol = rand.Next(0, 8);

                if (((Math.Abs(bntplace - randrow) == 2 && Math.Abs(bnlplace - randcol) == 1) || (Math.Abs(bntplace - randrow) == 1 && Math.Abs(bnlplace - randcol) == 2)) && board[randrow, randcol] >= 0)
                {
                    board[bntplace, bnlplace] = 0;
                    bntplace = randrow;
                    bnlplace = randcol;
                    board[bntplace, bnlplace] = -30;
                    bn.Margin = new Thickness(randcol * 50, randrow * 50, 0, 0);
                    randcheck = true;
                    return;
                }
                else if (Math.Abs(bntplace - randrow) == Math.Abs(bblplace - randcol) && board[randrow, randcol] >= 0)
                {
                    bbchecktrd = false;
                    for (int l = 1; l < Math.Abs(randrow - bbtplace); l++)
                    {
                        if (randrow > bbtplace && randcol < bblplace && board[bbtplace + l, bblplace - l] != 0)
                        {
                            bbchecktrd = true;
                            break;
                        }
                        if (randrow > bbtplace && randcol > bblplace && board[bbtplace + l, bblplace + l] != 0)
                        {
                            bbchecktrd = true;
                            break;
                        }
                        if (randrow < bbtplace && randcol < bblplace && board[bbtplace - l, bblplace - l] != 0)
                        {
                            bbchecktrd = true;
                            break;
                        }
                        if (randrow < bbtplace && randcol > bblplace && board[bbtplace - l, bblplace + l] != 0)
                        {
                            bbchecktrd = true;
                            break;
                        }
                    }
                    if (!bbchecktrd)
                    {
                        board[bbtplace, bblplace] = 0;
                        bbtplace = randrow;
                        bblplace = randcol;
                        board[bbtplace, bblplace] = -33;
                        bb.Margin = new Thickness(randcol * 50, randrow * 50, 0, 0);
                        randcheck = true;
                        return;
                    }
                }
                else if ((randcol == brlplace || randrow == brtplace) && board[randrow, randcol] >= 0)
                {
                    brchecktrd = false;
                    if (randcol == brlplace)
                    {
                        for (int l = 1; l < Math.Abs(randrow - brtplace); l++)
                        {
                            if (randrow > brtplace && board[brtplace + l, randcol] != 0)
                            {
                                brchecktrd = true;
                                break;
                            }
                            if (randrow < brtplace && board[brtplace - l, randcol] != 0)
                            {
                                brchecktrd = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int l = 1; l < Math.Abs(randcol - brlplace); l++)
                        {
                            if (randcol > brlplace && board[randrow, brlplace + l] != 0)
                            {
                                brchecktrd = true;
                                break;
                            }
                            if (randcol < brlplace && board[randrow, brlplace - l] != 0)
                            {
                                brchecktrd = true;
                                break;
                            }
                        }
                    }
                    if (!brchecktrd)
                    {
                        board[brtplace, brlplace] = 0;
                        brtplace = randrow;
                        brlplace = randcol;
                        board[brtplace, brlplace] = -50;
                        br.Margin = new Thickness(randcol * 50, randrow * 50, 0, 0);
                        randcheck = true;
                        return;
                    }
                }
                else if (((Math.Abs(randcol - bklplace) == 1 && Math.Abs(randrow - bktplace) == 1) || (Math.Abs(randcol - bklplace) == 0 && Math.Abs(randrow - bktplace) == 1) || (Math.Abs(randcol - bklplace) == 1 && Math.Abs(randrow - bktplace) == 0)) && board[randrow, randcol] >= 0)
                {
                    bkcheck = false;
                    for (int i = randrow - 1; i < randrow + 2; i++)
                    {
                        for (int j = randcol - 1; j < randcol + 2; j++)
                        {
                            if (i > -1 && i < 8 && j > -1 && j < 8)
                            {
                                if (board[i, j] == -1000)
                                {
                                    bkcheck = true;
                                }
                            }
                        }
                    }
                    if (!bkcheck && board[randrow + 1, randcol + 1] != 10 && board[randrow + 1, randcol - 1] != 10)
                    {
                        board[bktplace, bklplace] = 0;
                        bktplace = randrow;
                        bklplace = randcol;
                        board[bktplace, bklplace] = -50;
                        bk.Margin = new Thickness(randcol * 50, randrow * 50, 0, 0);
                        randcheck = true;
                        return;
                    }
                }
            }
        }
    }
}
