using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CompGraphics_Laba7
{
    public partial class Form1 : Form // Size Form: 1 000, 600, Size PictureBox: 975, 490
    {
        // Битовая картинка pictureBox
        Bitmap pictureBoxBitMap;
        // Битовая картинка динамического изображения
        Bitmap spriteBitMap;

        Bitmap shipBitMap;
        // Битовая картинка для временного хранения области экрана
        Bitmap cloneBitMap;
        // Графический контекст picturebox
        Graphics g_pictureBox;
        //Drawing Ship
        Graphics g_ship;
        // Графический контекст спрайта
        Graphics g_sprite;
        int x, y; // Координаты ракеты
        int width = 300, height = 175; // Ширина и высота 
        Timer timer;

        public Form1()
        {
            InitializeComponent();
        }
        void DrawShip()
        {
            Pen myWindows = new Pen(Color.BurlyWood, 2);
            // Определение кистей
            SolidBrush mylepestok = new SolidBrush(Color.LightSeaGreen);
            SolidBrush mystebel = new SolidBrush(Color.DarkGreen);
            // Рисование и закраска труб, трюма и корпуса корабля
            g_ship.FillPolygon(mystebel, new Point[] {
                new Point(150, 160),new Point(180, 20),
                new Point(190,20), new Point(160, 160),
                new Point(150, 160)
            });
            g_ship.FillPolygon(mylepestok, new Point[] {
                new Point(180, 35),new Point(230, 5),
                new Point(230,65), new Point(180, 35),});
            g_ship.FillPolygon(mylepestok, new Point[] {
                new Point(180, 35),new Point(210, 85),
                new Point(150,85), new Point(180, 35),});
            g_ship.FillPolygon(mylepestok, new Point[] {
                new Point(180, 35),new Point(210, -15),
                new Point(150,-15), new Point(180, 35),});
            g_ship.FillPolygon(mylepestok, new Point[] {
                new Point(180, 35),new Point(130, 5),
                new Point(130,65), new Point(180, 35),});

        }
        // Функция рисования спрайта (листик)
        void DrawSprite()
        {
            //// Задаем красный цвет для носа ракеты
            //SolidBrush myNose = new SolidBrush(Color.LightSeaGreen);
            //// Задаем белый и серый цвет для корпуса ракеты
            //SolidBrush myRBody = new SolidBrush(Color.Silver);
            //SolidBrush myLine = new SolidBrush(Color.Gray);
            {
                Pen myWindows = new Pen(Color.BurlyWood, 2);
                // Определение кистей
                SolidBrush mylepestok = new SolidBrush(Color.LightSeaGreen);
                SolidBrush mystebel = new SolidBrush(Color.DarkGreen);
                // Рисование и закраска труб, трюма и корпуса корабля

                g_sprite.FillPolygon(mystebel, new Point[] {
                new Point(150+100, 160),new Point(180+100, 20),
                new Point(190+100,20), new Point(160+100, 160),
                new Point(150+100, 160)
            });
                g_sprite.FillPolygon(mylepestok, new Point[] {
                new Point(180+100, 35),new Point(230+100, 5),
                new Point(230+100, 65), new Point(180+100, 35),});
                g_sprite.FillPolygon(mylepestok, new Point[] {
                new Point(180+100, 35),new Point(210+100, 85),
                new Point(150+100,85), new Point(180+100, 35),});
                g_sprite.FillPolygon(mylepestok, new Point[] {
                new Point(180+100, 35),new Point(210+100, -15),
                new Point(150+100,-15), new Point(180+100, 35),});
                g_sprite.FillPolygon(mylepestok, new Point[] {
                new Point(180+100, 35),new Point(130+100, 5),
                new Point(130+100,65), new Point(180+100, 35),});

                
                //    //Рисуем нос ракеты
                //    g_sprite.FillPolygon(myNose, new Point[]
                //{
                //new Point(180+50,35-30),new Point(230,5+60),
                //new Point(230-50,65-30), new Point(180,35) });

                //}


            }
        }

        // Функция сохранения части изображения шириной
        void SavePart(int xt, int yt)
        {
            Rectangle cloneRect = new Rectangle(xt, yt, width, height);
            System.Drawing.Imaging.PixelFormat format =
            pictureBoxBitMap.PixelFormat;
            // Клонируем изображение, заданное прямоугольной областью
            cloneBitMap = pictureBoxBitMap.Clone(cloneRect, format);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Создаём Bitmap для pictureBox1 и графический контекст
            pictureBox1.Image = Image.FromFile("C:\\Users\\Сашок\\Downloads\\C-gr_Lab7-master (1)\\C-gr_Lab7-master\\ocean.jpg");
            pictureBoxBitMap = new Bitmap(pictureBox1.Image);
            g_pictureBox = Graphics.FromImage(pictureBox1.Image);
            // Создаём Bitmap для спрайта и графический контекст
            spriteBitMap = new Bitmap(width, height);
            g_sprite = Graphics.FromImage(spriteBitMap);
            // Создаём Bitmap для корбля и графический контекст
            shipBitMap = new Bitmap(width, height);
            g_ship = Graphics.FromImage(shipBitMap);
            // Создаём Bitmap для временного хранения части изображения
            cloneBitMap = new Bitmap(width, height);
            // Задаем начальные координаты вывода движущегося объекта
            x = 0; y = 200;
            // Сохраняем область экрана перед первым выводом объекта
            SavePart(x, y);
            // Выводим корабль на графический контекст g_pictureBox
            DrawShip();
            g_pictureBox.DrawImage(shipBitMap, x-87, y);
            g_pictureBox.DrawImage(spriteBitMap, x+100, y);
                        // Перерисовываем pictureBox1
            pictureBox1.Invalidate();
            // Создаём таймер с интервалом 100 миллисекунд
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer1_Tick);

        }


        // Обрабатываем событие от таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Восстанавливаем затёртую область статического изображения
            g_pictureBox.DrawImage(cloneBitMap, x+213, y);
            // Изменяем координаты для следующего вывода
            x += 20;
            // Проверяем на выход изображения автобуса за правую границу
            if (x > pictureBox1.Width - 1) x = pictureBox1.Location.X;
            // Сохраняем область экрана перед первым выводом автобуса
            SavePart(x, y);
            // Выводим
            g_pictureBox.DrawImage(spriteBitMap, x, y);
            // Перерисовываем pictureBox1
            pictureBox1.Invalidate();
        }
        // Включаем таймер по нажатию на кнопку
        private void button1_Click(object sender, EventArgs e)
        {
            DrawSprite();
            timer.Enabled = true;
        }
    }
}
