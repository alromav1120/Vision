using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bordes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //pictureBox3.Visible = false;
            //pictureBox2.Visible = false;
        
        }
        

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos JPEG(*.jpg)|*.jpg";
            abrir.InitialDirectory = "C:/Users/Roman/Mis imágenes:";

            // Cargar imagen
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                string dir = abrir.FileName;
                Bitmap imagen = new Bitmap(dir);
                Bitmap imagen2 = new Bitmap(dir);
                pictureBox1.Image = (Image)imagen;
            }

            /////////////////////////////////////////////// GRISES
            Bitmap n = (new Bitmap(pictureBox1.Image));
            for (int i = 0; i < n.Width; i++)
            {
                for (int j = 0; j < n.Height; j++)
                {
                    Color l = (n.GetPixel(i, j));
                    int k = l.ToArgb();
                    int rojo = (int)((k & 0x0000FF) * 0.3);
                    int Verde = (int)(((k & 0x00FF00) >> 8) * .59);
                    int blue = (int)(((k & 0xFF0000) >> 16) * .11);
                    int NColor = rojo + Verde + blue;
                    n.SetPixel(i, j, Color.FromArgb(NColor | NColor << 8 | NColor << 16 | 255 << 24));
                }

            }

            pictureBox2.Image = n;
            

            ///////////////////////////////////////////////////////////

            //BINARIO
            int umbral=128,dato=0;
            

            Bitmap q = (new Bitmap(pictureBox1.Image));
            int[,] maux = new int[q.Width+10,q.Height+10];
            for (int i = 0; i < q.Width; i++)
            {
                for (int j = 0; j < q.Height; j++)
                {

                    Color l = (q.GetPixel(i, j));
                    int k = l.ToArgb();
                    int rojo = (int)((k & 0x0000FF) * 0.3);
                    int Verde = (int)(((k & 0x00FF00) >> 8) * .59);
                    int blue = (int)(((k & 0xFF0000) >> 16) * .11);
                   
                    int NColor = rojo + Verde + blue;
                    if(NColor<umbral)
                    {
                        dato = 0;
                        maux[i, j] = 0;
                    }
                    if (NColor >= umbral)
                    {
                        dato = 255;
                        maux[i, j] = 1;
                    }
                    
                    
                    q.SetPixel(i, j, Color.FromArgb(dato | dato << 8 | dato << 16 | 255 << 24));
                }

            }

            pictureBox3.Image = q;
      
           
           
            //////////////////////////////////////////////////////////


            // BORDES
            
            //////Matriz de pesos

            int[,] pesos = new int[3, 3]
            {
                { -1, -1, -1, },
                { -1,  8, -1, },
                { -1, -1, -1, }
            };

            int [,] producto=new int[3,3];

            int suma = 0,datobin=0;
            double umbral2=0.5;
         

            Bitmap borde = (new Bitmap(pictureBox1.Image));
            for (int i = 0; i < borde.Width;i++ )
            {
                for (int j = 0; j < borde.Height; j++)
                {
                    
                    
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3;y++ )
                        {
                            producto[x,y]=maux[i+x,j+y]*pesos[x,y];
                            suma = suma + producto[x, y];

                        }

                    }

                    if (suma < umbral2)
                    {
                        datobin = 0;
                    }
                    else { datobin = 255; }

                    suma = 0;

                        borde.SetPixel(i, j, Color.FromArgb(datobin | datobin << 8 | datobin << 16 | 255 << 24));

                }

                
            }
            pictureBox4.Image = borde;

            //////////////////////////////////////////////////

            }
          
        }
    }

