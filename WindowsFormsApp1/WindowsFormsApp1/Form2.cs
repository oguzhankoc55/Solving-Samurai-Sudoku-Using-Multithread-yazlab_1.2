using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        static string DosyaKonumu = "deneme1.txt"; //sudoku txt konumu
        static List<durum> islemler_1 = new List<durum>(); // sudoku ekrani uzerinde yapilan islemlerin bilgileri tutulur  
        static List<durum> islemler_2 = new List<durum>();
        static List<durum> islemler_3 = new List<durum>();
        static List<durum> islemler_4 = new List<durum>();
        static List<durum> islemler_5 = new List<durum>();
        static Thread thread1;
        static Thread thread2;  // kullanilan threadlerin acilmasi
        static Thread thread3;
        static Thread thread4;
        static Thread thread5;

        static char[,] csudoku1 = new char[9, 9];
        static char[,] csudoku2 = new char[9, 9]; // txt den alinan sudokunun bilgilerin tutulacagi char arraylar 
        static char[,] csudoku3 = new char[9, 9];
        static char[,] csudoku4 = new char[9, 9];
        static char[,] csudoku5 = new char[9, 9];

        static int[,] board1 = new int[9, 9];
        static int[,] board2 = new int[9, 9];
        static int[,] sudoku1 = new int[9, 9]; // sudokularin tutuldugu arrayler
        static int[,] sudoku2 = new int[9, 9];
        static int[,] sudoku3 = new int[9, 9];
        static int[,] sudoku4 = new int[9, 9];
        static int[,] sudoku5 = new int[9, 9];

        static List<ihtimal> sudoku_1_tutulan = new List<ihtimal>(); // ortak alanlarin karsilastirilmasi  icin kullanilan Listler
        static List<ihtimal> sudoku_2_tutulan = new List<ihtimal>();
        static List<ihtimal> sudoku_tutulanx = new List<ihtimal>(); // ihtimallerin karsilastirildigi ve sonrasinda olusan ortak alanlarin tutuldugu Listtir
        static int[] sayilari = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };   // bir sudokudaki hucrelerde sayilarin olabilme ihtimalleri sayilir

        static List<Button> buttonlar_1 = new List<Button>();  // sudokularin ekranda gosterilmesi icin kullanilan button listeleri
        static List<Button> buttonlar_2 = new List<Button>();
        static List<Button> buttonlar_3 = new List<Button>();
        static List<Button> buttonlar_4 = new List<Button>();
        static List<Button> buttonlar_5 = new List<Button>();

        static int key = 0;
        static int[,] board = new int[9, 9];
        static int N = board.GetLength(0);
        static void karsilastir(int sdk_1, int sdk_2)
        {// iki ihtimaller listini karsılastırıp sudokuyu degistirir

            for (int i = 0; i < sudoku_1_tutulan.Count; i++)
            {
                ihtimal deneme = new ihtimal();
                deneme.x = sudoku_1_tutulan[i].x;
                deneme.y = sudoku_1_tutulan[i].y;
                for (int j = 0; j < sudoku_1_tutulan[i].deger.Count; j++)// ihtimallerin tutuldugu iki listenin ortak elemanlari farkli bir listeye alinir
                {
                    for (int k = 0; k < sudoku_2_tutulan[i].deger.Count; k++)
                    {
                        if (sudoku_1_tutulan[i].deger[j] == sudoku_2_tutulan[i].deger[k])
                        {
                            deneme.deger.Add(sudoku_1_tutulan[i].deger[j]); }  } }
                sudoku_tutulanx.Add(deneme);
            }
            foreach (var x in sudoku_tutulanx) // ihtimali kesin olan sayilar yazilir
            {
                if (x.deger.Count == 1)
                {
                    key = 0;
                    if (sdk_1 == 1 && sdk_2 == 5)
                    {
                        sudoku1[x.x + 6, x.y + 6] = x.deger[0];
                        durum d_1=new durum();
                        d_1.x = x.x+6;
                        d_1.y = x.y + 6;
                        d_1.deger = x.deger[0];
                        islemler_1.Add(d_1);

                        sudoku5[x.x, x.y] = x.deger[0];
                        durum d_5 = new durum();
                        d_5.x = x.x ;
                        d_5.y = x.y ;
                        d_5.deger = x.deger[0];
                        islemler_5.Add(d_5);

                    }
                    if (sdk_1 == 2 && sdk_2 == 5)
                    {
                        sudoku2[x.x + 6, x.y] = x.deger[0];
                        durum d_2 = new durum();
                        d_2.x = x.x + 6;
                        d_2.y = x.y ;
                        d_2.deger = x.deger[0];
                        islemler_2.Add(d_2);
                        sudoku5[x.x, x.y + 6] = x.deger[0];
                        durum d_5 = new durum();
                        d_5.x = x.x;
                        d_5.y = x.y+6;
                        d_5.deger = x.deger[0];
                        islemler_5.Add(d_5);
                    }
                    if (sdk_1 == 3 && sdk_2 == 5)
                    {
                        sudoku3[x.x, x.y + 6] = x.deger[0];
                        durum d_3 = new durum();
                        d_3.x = x.x ;
                        d_3.y = x.y + 6;
                        d_3.deger = x.deger[0];
                        islemler_3.Add(d_3);
                        sudoku5[x.x + 6, x.y] = x.deger[0];
                        durum d_5 = new durum();
                        d_5.x = x.x+6;
                        d_5.y = x.y;
                        d_5.deger = x.deger[0];
                        islemler_5.Add(d_5);
                    }
                    if (sdk_1 == 4 && sdk_2 == 5)
                    {
                        sudoku4[x.x, x.y] = x.deger[0];
                        durum d_4 = new durum();
                        d_4.x = x.x ;
                        d_4.y = x.y ;
                        d_4.deger = x.deger[0];
                        islemler_4.Add(d_4);
                        sudoku5[x.x + 6, x.y + 6] = x.deger[0];
                        durum d_5 = new durum();
                        d_5.x = x.x+6;
                        d_5.y = x.y+6;
                        d_5.deger = x.deger[0];
                        islemler_5.Add(d_5);
                    }
                }
            }
            //sudokulara kayıt icin yazıldı

            foreach (var x in sudoku_tutulanx)
            {
                foreach (var i in x.deger)
                {
                    sayilari[i]++;
                }
            }
            for (int j = 1; j < 10; j++)
                { if (sayilari[j] == 1)
                    {key = 0;
                    foreach (var x in sudoku_tutulanx)
                        {foreach (var i in x.deger)
                            {if (i == j){
                                if (sdk_1 == 1 && sdk_2 == 5)
                                {
                                    sudoku1[x.x + 6, x.y + 6] = j;
                                    sudoku5[x.x, x.y] = j;
                                }
                                if (sdk_1 == 2 && sdk_2 == 5)
                                {
                                    sudoku2[x.x + 6, x.y] = j;
                                    sudoku5[x.x, x.y + 6] = j;
                                }
                                if (sdk_1 == 3 && sdk_2 == 5)
                                {
                                    sudoku3[x.x, x.y + 6] = j;
                                    sudoku5[x.x + 6, x.y] = j;
                                }
                                if (sdk_1 == 4 && sdk_2 == 5)
                                {
                                    sudoku4[x.x, x.y] = j;
                                    sudoku5[x.x + 6, x.y + 6] = j; }}}}}}
         sudoku_2_tutulan.Clear();
         sudoku_1_tutulan.Clear();
         sudoku_tutulanx.Clear();
         for (int i = 0; i < 10; i++){sayilari[i] = 0; }
         
        }

        static void karsilastir_new(int sdk_1, int sdk_2)
        { // ikili ihtimalleri deneyerek dogru olanı bulmayı amaclayan algoritma

            for (int i = 0; i < sudoku_1_tutulan.Count; i++)
            {
                ihtimal deneme = new ihtimal();
                deneme.x = sudoku_1_tutulan[i].x;
                deneme.y = sudoku_1_tutulan[i].y;
                for (int j = 0; j < sudoku_1_tutulan[i].deger.Count; j++)
                {
                    for (int k = 0; k < sudoku_2_tutulan[i].deger.Count; k++)
                    {
                        if (sudoku_1_tutulan[i].deger[j] == sudoku_2_tutulan[i].deger[k])
                        {
                            deneme.deger.Add(sudoku_1_tutulan[i].deger[j]);
                        }
                    }
                }
                sudoku_tutulanx.Add(deneme);
            }
            /*
            int b = 1;
            foreach (var x in sudoku_tutulanx)
            {
                Console.WriteLine(b + ".satır");
                foreach (var i in x.deger)
                {
                    Console.WriteLine("deger " + i + " x= " + x.x + " y= " + x.y);
                }
                b++;
                Console.WriteLine();
            }
            */
            if (sudoku_tutulanx.Count == 2)
            {
                if (sdk_1 == 1 && sdk_2 == 5)
                {
                    bool key1 = false, key2 = false;
                    board1 = new int[9, 9];
                    board2 = new int[9, 9];
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            board1[i, j] = sudoku1[i, j];
                            board2[i, j] = sudoku5[i, j];
                        }
                    }
                    board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                    board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];

                    if (solveSudoku(board1, 9, 6))
                    {
                        key1 = true;
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];

                        if (solveSudoku(board2, 9, 7))
                        {
                            key2 = true;
                        }
                    }
                    if (key1 && key2)
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        sudoku1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];

                    }
                    else
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                        sudoku1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                    }
                }
                if (sdk_1 == 2 && sdk_2 == 5)
                {
                    bool key1 = false, key2 = false;
                    board1 = new int[9, 9];
                    board2 = new int[9, 9];
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            board1[i, j] = sudoku2[i, j];
                            board2[i, j] = sudoku5[i, j];
                        }
                    }
                    board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                    board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];

                    if (solveSudoku(board1, 9, 6))
                    {
                        key1 = true;
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];

                        if (solveSudoku(board2, 9, 7))
                        {
                            key2 = true;
                        }
                    }
                    if (key1 && key2)
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        sudoku2[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku2[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];

                    }
                    else
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                        sudoku2[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku2[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                    }
                }




                if (sdk_1 == 3 && sdk_2 == 5)
                {
                    bool key1 = false, key2 = false;
                    board1 = new int[9, 9];
                    board2 = new int[9, 9];
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            board1[i, j] = sudoku3[i, j];
                            board2[i, j] = sudoku5[i, j];
                        }
                    }
                    board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                    board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];

                    if (solveSudoku(board1, 9, 6))
                    {
                        key1 = true;
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];

                        if (solveSudoku(board2, 9, 7))
                        {
                            key2 = true;
                        }
                    }
                    if (key1 && key2)
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        sudoku3[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku3[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];

                    }
                    else
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                        sudoku3[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku3[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                    }
                }
                if (sdk_1 == 4 && sdk_2 == 5)
                {
                    bool key1 = false, key2 = false;
                    board1 = new int[9, 9];
                    board2 = new int[9, 9];
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            board1[i, j] = sudoku4[i, j];
                            board2[i, j] = sudoku5[i, j];
                        }
                    }
                    board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                    board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];

                    if (solveSudoku(board1, 9, 6))
                    {
                        key1 = true;
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];

                        if (solveSudoku(board2, 9, 7))
                        {
                            key2 = true;
                        }
                    }
                    if (key1 && key2)
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];
                        sudoku4[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku4[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[1];

                    }
                    else
                    {
                        board1[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        board1[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        board2[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        board2[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                        sudoku4[sudoku_tutulanx[0].x + 6, sudoku_tutulanx[0].y + 6] = sudoku_tutulanx[0].deger[1];
                        sudoku4[sudoku_tutulanx[1].x + 6, sudoku_tutulanx[1].y + 6] = sudoku_tutulanx[0].deger[0];
                        sudoku5[sudoku_tutulanx[0].x, sudoku_tutulanx[0].y] = sudoku_tutulanx[0].deger[1];
                        sudoku5[sudoku_tutulanx[1].x, sudoku_tutulanx[1].y] = sudoku_tutulanx[0].deger[0];
                    }

                }
            }


            foreach (var x in sudoku_tutulanx)
            {
                foreach (var i in x.deger)
                {
                    sayilari[i]++;
                }
            }



            sudoku_2_tutulan.Clear();
            sudoku_1_tutulan.Clear();
            sudoku_tutulanx.Clear();
            for (int i = 0; i < 10; i++)
            {
                sayilari[i] = 0;
            }

        }

        static void ihtimalleri_yaz_2(int[,] board, int x_arti, int y_arti, int liste_id)
        {int key;
            for (int i = 0 + x_arti; i < 3 + x_arti; i++)
            {
                for (int j = 0 + y_arti; j < 3 + y_arti; j++)
                {
                    ihtimal deneme = new ihtimal();
                    if (board[i, j] == 0)
                    {
                        deneme.x = i - x_arti;
                        deneme.y = j - y_arti;
                        key = 0;
                        for (int d = 1; d <= 9; d++)
                        {
                            if (uygunluk(board, i, j, d))
                            {
                                deneme.deger.Add(d);
                                key = 1;
                            }
                        }
                        if (key == 1)
                        {
                            if (liste_id == 1)
                            {
                                sudoku_1_tutulan.Add(deneme);
                            }
                            if (liste_id == 2)
                            {
                                sudoku_2_tutulan.Add(deneme);}}}}}
            
        }

     
       
        static void tekrar() //ortak alanlarin olma ihtimallerinin hesaplamalar sonrasında kesin degerleri yazabilmemizi saglar
        {
            while (key == 0)
            {
                Console.WriteLine("TEKRARLAMA DENEMESİ");  key = 1;
                Console.WriteLine("sol ust---orta");
                ihtimalleri_yaz_2(sudoku1, 6, 6, 1);//sol ust
                Console.WriteLine();
                ihtimalleri_yaz_2(sudoku5, 0, 0, 2);
                karsilastir(1, 5);
                Console.WriteLine("\n\nsag ust---orta");
                ihtimalleri_yaz_2(sudoku2, 6, 0, 1);//sag ust
                Console.WriteLine();
                ihtimalleri_yaz_2(sudoku5, 0, 6, 2);
                karsilastir(2, 5);
                Console.WriteLine("\n\nsol alt---orta");
                ihtimalleri_yaz_2(sudoku3, 0, 6, 1);//sol alt
                Console.WriteLine();
                ihtimalleri_yaz_2(sudoku5, 6, 0, 2);
                karsilastir(3, 5);
                Console.WriteLine("\n\nsag alt---orta");
                ihtimalleri_yaz_2(sudoku4, 0, 0, 1);//sag alt
                Console.WriteLine();
                ihtimalleri_yaz_2(sudoku5, 6, 6, 2);
                karsilastir(4, 5);
            }
            Console.WriteLine("sol ust---orta");
            ihtimalleri_yaz_2(sudoku1, 6, 6, 1);//sol ust
            Console.WriteLine();
            ihtimalleri_yaz_2(sudoku5, 0, 0, 2);
            karsilastir_new(1, 5); // ikili ihtimallerin karsılastırıldıgı fonksiyon


            Console.WriteLine("\n\nsag ust---orta");
            ihtimalleri_yaz_2(sudoku2, 6, 0, 1);//sag ust
            Console.WriteLine();
            ihtimalleri_yaz_2(sudoku5, 0, 6, 2);
            karsilastir_new(2, 5);

            Console.WriteLine("\n\nsol alt---orta");
            ihtimalleri_yaz_2(sudoku3, 0, 6, 1);//sol alt
            Console.WriteLine();
            ihtimalleri_yaz_2(sudoku5, 6, 0, 2);
            karsilastir_new(3, 5);

            Console.WriteLine("\n\nsag alt---orta");
            ihtimalleri_yaz_2(sudoku4, 0, 0, 1);//sag alt
            Console.WriteLine();
            ihtimalleri_yaz_2(sudoku5, 6, 6, 2);
            karsilastir_new(4, 5);



        }
        public void Dosya_yukle()
        {
            
            StreamReader DosyaOkuyucu = new StreamReader(DosyaKonumu);
            string Metin = DosyaOkuyucu.ReadToEnd();
            string[] words = Metin.Split('\n');
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (j < 3)
                    {
                        csudoku1[j, i] = words[j][i]; csudoku2[j, i] = words[j][i + 9];
                        csudoku3[j, i] = words[j + 12][i]; csudoku4[j, i] = words[j + 12][i + 12];
                        csudoku5[j, i] = words[j + 6][i + 6];
                    }
                    else if (3 <= j && j < 6)
                    {
                        csudoku1[j, i] = words[j][i]; csudoku2[j, i] = words[j][i + 9];
                        csudoku3[j, i] = words[j + 12][i]; csudoku4[j, i] = words[j + 12][i + 9];
                        csudoku5[j, i] = words[j + 6][i];
                    }
                    else if (6 <= j && j < 9)
                    {
                        csudoku1[j, i] = words[j][i]; csudoku2[j, i] = words[j][i + 12];
                        csudoku3[j, i] = words[j + 12][i]; csudoku4[j, i] = words[j + 12][i + 9];
                        csudoku5[j, i] = words[j + 6][i + 6]; } } }
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (csudoku1[j, i] == '*') { csudoku1[j, i] = '0'; }
                    if (csudoku2[j, i] == '*') { csudoku2[j, i] = '0'; }
                    if (csudoku3[j, i] == '*') { csudoku3[j, i] = '0'; }
                    if (csudoku4[j, i] == '*') { csudoku4[j, i] = '0'; }
                    if (csudoku5[j, i] == '*') { csudoku5[j, i] = '0'; } } }
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    sudoku1[j, i] = Convert.ToInt32(csudoku1[j, i].ToString());
                    sudoku2[j, i] = Convert.ToInt32(csudoku2[j, i].ToString());
                    sudoku3[j, i] = Convert.ToInt32(csudoku3[j, i].ToString());
                    sudoku4[j, i] = Convert.ToInt32(csudoku4[j, i].ToString());
                    sudoku5[j, i] = Convert.ToInt32(csudoku5[j, i].ToString()); } }
        }
        public Form2()
        {
            InitializeComponent();
            Dosya_yukle();
            tekrar();
            

            thread1 = new Thread(thread_deneme_1);
            thread2 = new Thread(thread_deneme_2);
            thread3 = new Thread(thread_deneme_3);
            thread4 = new Thread(thread_deneme_4);
            thread5 = new Thread(thread_deneme_5);
           for (int i = 0; i < 1000000000; i++) {  }

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start(); 

            islemler_kaydet();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
        
        private void add_btun_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button deneme1 = new Button();deneme1.Size = new Size(20, 20);
                    deneme1.Location = new Point(75 + (i * 20), 20 + (j * 20));
                    deneme1.Text = "" + csudoku1[j,i];deneme1.Visible = true;
                    buttonlar_1.Add(deneme1);

                    Button deneme2 = new Button();deneme2.Size = new Size(20, 20);
                    deneme2.Location = new Point(315 + (i * 20), 20 + (j * 20));
                    deneme2.Text = "" + csudoku2[j, i];deneme2.Visible = true;
                    buttonlar_2.Add(deneme2);

                    Button deneme3 = new Button();deneme3.Size = new Size(20, 20);
                    deneme3.Location = new Point(75 + (i * 20), 260 + (j * 20));
                    deneme3.Text = "" + csudoku3[j, i]; deneme3.Visible = true;
                    buttonlar_3.Add(deneme3);

                    Button deneme4 = new Button();deneme4.Size = new Size(20, 20);
                    deneme4.Location = new Point(315 + (i * 20), 260 + (j * 20));
                    deneme4.Text = "" + csudoku4[j, i]; deneme4.Visible = true;
                    buttonlar_4.Add(deneme4);

                    Button deneme5 = new Button();deneme5.Size = new Size(20, 20);
                    deneme5.Location = new Point(195 + (i * 20), 140 + (j * 20));
                    deneme5.Text = "" + csudoku5[j, i];deneme5.Visible = true;
                    buttonlar_5.Add(deneme5);
                }
            }
            for (int i = 0; i < 81; i++)
            {
                Controls.Add(buttonlar_1[i]);Controls.Add(buttonlar_2[i]);
                Controls.Add(buttonlar_3[i]);Controls.Add(buttonlar_4[i]);
                Controls.Add(buttonlar_5[i]);} add_btun.Visible = false;
        }
        private void sonuc_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    buttonlar_1[(i * 9) + j].Text = "" + Convert.ToString(sudoku1[j, i]);
                    buttonlar_2[(i * 9) + j].Text = "" + Convert.ToString(sudoku2[j, i]);
                    buttonlar_3[(i * 9) + j].Text = "" + Convert.ToString(sudoku3[j, i]);
                    buttonlar_4[(i * 9) + j].Text = "" + Convert.ToString(sudoku4[j, i]);
                    buttonlar_5[(i * 9) + j].Text = "" + Convert.ToString(sudoku5[j, i]);
                }
            }
        }
        public static void islemler_kaydet()
        {
            int uzunluk = islemler_1.Count();
            Console.WriteLine(" uzunluk = " + uzunluk);
            for (int i = 0; i < uzunluk; i++)
            {
                String satır1=(" x = " + islemler_1[i].x + " y = " + islemler_1[i].y + " deger = " + islemler_1[i].deger);
                txt_yaz(1, satır1);
            }
            uzunluk = islemler_2.Count();
            Console.WriteLine(" uzunluk = " + uzunluk);
            for (int i = 0; i < uzunluk; i++)
            {
                String satır2 = (" x = " + islemler_2[i].x + " y = " + islemler_2[i].y + " deger = " + islemler_2[i].deger);
                txt_yaz(2, satır2);
            }
            uzunluk = islemler_3.Count();
            Console.WriteLine(" uzunluk = " + uzunluk);
            for (int i = 0; i < uzunluk; i++)
            {
                String satır3 = (" x = " + islemler_3[i].x + " y = " + islemler_3[i].y + " deger = " + islemler_3[i].deger);
                txt_yaz(3, satır3);
            }
            uzunluk = islemler_4.Count();
            Console.WriteLine(" uzunluk = " + uzunluk);
            for (int i = 0; i < uzunluk; i++)
            {
                String satır4 = (" x = " + islemler_4[i].x + " y = " + islemler_4[i].y + " deger = " + islemler_4[i].deger);
                txt_yaz(4, satır4);
            }
            uzunluk = islemler_5.Count();
            Console.WriteLine(" uzunluk = " + uzunluk);
            for (int i = 0; i < uzunluk; i++)
            {
                String satır5 = (" x = " + islemler_5[i].x + " y = " + islemler_5[i].y + " deger = " + islemler_5[i].deger);
                txt_yaz(5, satır5);
            }
        }

        static void thread_deneme_1()
         {if (solveSudoku(sudoku1, N,1))   { }
            else{ Console.Write("No solution"); } }
        static void thread_deneme_2()
        {if (solveSudoku(sudoku2, N,2)) { }
            else { Console.Write("No solution"); }
        }
        static void thread_deneme_3()
        {if (solveSudoku(sudoku3, N,3)) { }
            else { Console.Write("No solution"); }
        }
        static void thread_deneme_4()
        {
            if (solveSudoku(sudoku4, N,4)) { }
            else { Console.Write("No solution"); }
        }
        static void thread_deneme_5()
        {
            if (solveSudoku(sudoku5, N,5)) { }
            else { Console.Write("No solution"); }
        }

        public static bool uygunluk(int[,] board,int row, int col,int num)
        {
            // satirlarda bulunma durumuna bakilir
            for (int d = 0; d < board.GetLength(0); d++)
            {
                if (board[row, d] == num)
                {
                    return false;
                }
            }
            // sutunlarda bulunma durumuna bakilir
            for (int r = 0; r < board.GetLength(0); r++)
            {
                if (board[r, col] == num)
                {
                    return false;
                }
            }
            
            //bulunduklari karelerde bulunma ihtimalleri tutulur
            int sqrt = (int)Math.Sqrt(board.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;r < boxRowStart + sqrt; r++)
            {for (int d = boxColStart;d < boxColStart + sqrt; d++)
                { if (board[r, d] == num){return false;}  } }
            return true;
        }

        public static bool solveSudoku(int[,] board, int n,int number )
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                   
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;} }
                if (!isEmpty) {break; }
            }
            
            if (isEmpty){ return true;}//bosalan kalmadi
            durum eldeki = new durum();
            
            for (int num = 1; num <= n; num++)// backtrack
            {
                if (uygunluk(board, row, col, num))
                {
                    eldeki.x = row;eldeki.y = col;
                    eldeki.deger = num;
                    if (number == 1) { islemler_1.Add(eldeki); }
                    if (number == 2) { islemler_2.Add(eldeki); }
                    if (number == 3) { islemler_3.Add(eldeki); }
                    if (number == 4) { islemler_4.Add(eldeki); }
                    if (number == 5) { islemler_5.Add(eldeki); }
                    board[row, col] = num;
                    if (solveSudoku(board, n,number)) {return true;  }
                    else { board[row, col] = 0; }
                }
            }
            return false;
        }
        
        public static void txt_yaz(int deger,string satır)
        {
            StreamWriter SW = File.AppendText("islemler"+ deger + ".txt");
            SW.WriteLine(satır);
            SW.Close();
        }

        private void say_btn_Click(object sender, EventArgs e)
        {
            int uzunluk1 = islemler_1.Count();
            th1_sonc.Text = Convert.ToString( uzunluk1);
            int uzunluk2 = islemler_2.Count();
            th2_son.Text = Convert.ToString(uzunluk2);
            int uzunluk3 = islemler_3.Count();
            th3_son.Text = Convert.ToString(uzunluk3);
            int uzunluk4 = islemler_4.Count();
            th4_son.Text = Convert.ToString(uzunluk4);
            int uzunluk5 = islemler_5.Count();
            th5_son.Text = Convert.ToString(uzunluk5);
        }
    }
}