using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap R;
            Bitmap G;
            Bitmap B;
            Bitmap Imagen1;
            
            // Abrimos imagen mediante openfiledialog
            Stream myStream = null;
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            //Picturebox1 toma imagen de openfiledialog
                            Imagen1 = new Bitmap(openFileDialog1.FileName);
                            pictureBox1.Image = Imagen1;
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                            
                            R= new Bitmap(Imagen1.Width, Imagen1.Height);
                            G = new Bitmap(Imagen1.Width, Imagen1.Height);
                            B = new Bitmap(Imagen1.Width, Imagen1.Height);

                            
                            
                            //Funciones para Dividir colores

                            for (int x = 0; x < Imagen1.Width;x++ )
                            {
                                for (int y = 0; y < Imagen1.Height; y++)
                                {
                                    Color px1 = Imagen1.GetPixel(x,y); // Obtiene el color del pixel de la imagen
    
                                    Color pixelrojo = Color.FromArgb(px1.R,0,0); // Elimina los colores Verde y Azul
                                    Color pixelverde = Color.FromArgb(0, px1.G, 0); // Elimina los colores Rojo y azul
                                    Color pixelazul = Color.FromArgb(0, 0, px1.B); // Elimina los colores Rojo y Verde

                                    R.SetPixel(x, y, pixelrojo);
                                    G.SetPixel(x,y,pixelverde);
                                    B.SetPixel(x, y, pixelazul);
                                }
                            }

                            pictureBox2.Image = R;    
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBox3.Image = G;
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBox4.Image = B;
                            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                            
                            //Funcion para obtener Valor Verde

                           

                                        
                       }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
             
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}


