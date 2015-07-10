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
            
            Bitmap Imagen1;
            Bitmap grises, negro;
            
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

                            // Bitmaps grises y negro toman el tamano de la imagen

                            grises = new Bitmap(Imagen1.Width, Imagen1.Height); 
                            negro = new Bitmap(Imagen1.Width, Imagen1.Height);

                            //Algoritmo convertir a grises y negros

                            int umbral = 130; //Umbral de seleccion para los negros

                            for (int x = 0; x < Imagen1.Width; x++) {
                                for (int y = 0; y < Imagen1.Height; y++) {
                                    
                                    Color px1 = Imagen1.GetPixel(x,y);

                                    //Aplicamos la formmula Gris=0.21*R+072*G+0.07*B  

                                    double R = 0.21*(px1.R);
                                    double G = 0.72*(px1.G);
                                    double B = 0.07*(px1.B);
                                    int suma =(int)(R + G + B);

                                    Color pixelgris = Color.FromArgb(suma,suma,suma);
                                    grises.SetPixel(x, y, pixelgris); // Pintamos el bitmap de grises

                                    //Comparamos la suma con el umbral establecido y pintamos de negro
                                    if (suma <= umbral)
                                    {
                                        Color pixel = Color.FromArgb(0, 0, 0);
                                        negro.SetPixel(x, y, pixel);
                                    }
                                    else
                                    {
                                        Color pixel = Color.FromArgb(255, 255, 255);
                                        negro.SetPixel(x, y, pixel);
                                    }

                                }
                            }

                            pictureBox2.Image = grises;
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBox3.Image = negro;
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                                        
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


