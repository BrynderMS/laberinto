using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace creadorlaberinto
{
    public partial class Form1 : Form
    {
        private Dictionary<Point, cuadradoplus> cuadrados = new Dictionary<Point, cuadradoplus>();
        private const int posx = 10;//// <--------- esquina superior izquierda
        private const int posy = 10;//// <--------- esquina superior izquierda
        private personaje j1;
        private bool listo;
        private bool dibujado;
        private Graphics lienzo;
        public Form1()
        {
            InitializeComponent();
        }

        private void formload(object sender, EventArgs e)
        {

            ajustes();

            lienzo = this.CreateGraphics();
            var x = posx;
            var y = posy;
            var cordenadas = new Point(1, 1);
            for (var i = 1; i < Properties.Settings.Default.tamano+1; i++)
            {
                cuadrados.Add(cordenadas, new cuadradoplus(Properties.Settings.Default.tamanocasillas, x, y));
                if (i % (Math.Floor(Math.Sqrt(Properties.Settings.Default.tamano))) == 0)
                {
                    y += Properties.Settings.Default.tamanocasillas;
                    x = posx;
                    cordenadas.Y++;
                    cordenadas.X = 1;
                }
                else
                {
                    x += Properties.Settings.Default.tamanocasillas;
                    cordenadas.X++;
                }
            }
            creadorlaberinto creador = new creadorlaberinto(cuadrados);
            j1 = new personaje(cuadrados);
            creador.crear(ref cuadrados);
            listo = true;
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            pintar();
        }
        private void pintar()
        {
            if (listo)
            {
               //lienzo.Clear(this.BackColor);

                //PINTAR LABERINTO
                if (!dibujado)
                {
                    foreach (KeyValuePair<Point, cuadradoplus> casilla in cuadrados)
                    {
                        casilla.Value.dibujarrect(lienzo);
                    }
                    lienzo.DrawIcon(new Icon(SystemIcons.Asterisk, new Size(Properties.Settings.Default.tamanocasillas, Properties.Settings.Default.tamanocasillas)), cuadrados.ToList()[0].Value.ToRectangle());
                    dibujado = true;
                }
                //lienzo.FillEllipse(new SolidBrush(Color.Red), cuadrados[j1.Posicion].ToRectangle());

                //PINTAR RECORRIDO j1
                Pen boli = new Pen(Color.Red, 2);
                if (j1.Posicicionesanteriores.Contains(j1.Posicion))
                {
                    boli.Color = this.BackColor;
                }
                lienzo.DrawLine(boli, new Point(cuadrados[j1.Posicicionesanteriores[j1.Posicicionesanteriores.Count() - 1]].Posx + (Properties.Settings.Default.tamanocasillas / 2), cuadrados[j1.Posicicionesanteriores[j1.Posicicionesanteriores.Count() - 1]].Posy + (Properties.Settings.Default.tamanocasillas / 2)), new Point(cuadrados[j1.Posicion].Posx + (Properties.Settings.Default.tamanocasillas / 2), cuadrados[j1.Posicion].Posy + (Properties.Settings.Default.tamanocasillas / 2)));

                //PINTAR PRIMERA PERSONA j1
                var direcciones = new char[4] { 'S','W','N','E'};
                var direccion = j1.Direccion;
                groupBox1.Text = direcciones[izquierda(j1.Direccion)].ToString();
                groupBox2.Text = direcciones[j1.Direccion].ToString();
                groupBox3.Text = direcciones[derecha(j1.Direccion)].ToString();
                switch (j1.Direccion)
                {
                    case 2:
                        if (cuadrados[j1.Posicion].Top) groupBox2.BackColor = Color.DarkGray;
                        else groupBox2.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Left) groupBox1.BackColor = Color.DarkGray;
                        else groupBox1.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Right) groupBox3.BackColor = Color.DarkGray;
                        else groupBox3.BackColor = this.BackColor;
                        break;
                    case 0:
                        if (cuadrados[j1.Posicion].Bottom) groupBox2.BackColor = Color.DarkGray;
                        else groupBox2.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Right) groupBox1.BackColor = Color.DarkGray;
                        else groupBox1.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Left) groupBox3.BackColor = Color.DarkGray;
                        else groupBox3.BackColor = this.BackColor;
                        break;
                    case 1:
                        if (cuadrados[j1.Posicion].Left) groupBox2.BackColor = Color.DarkGray;
                        else groupBox2.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Bottom) groupBox1.BackColor = Color.DarkGray;
                        else groupBox1.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Top) groupBox3.BackColor = Color.DarkGray;
                        else groupBox3.BackColor = this.BackColor;
                        break;
                    case 3:
                        if (cuadrados[j1.Posicion].Right) groupBox2.BackColor = Color.DarkGray;
                        else groupBox2.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Top) groupBox1.BackColor = Color.DarkGray;
                        else groupBox1.BackColor = this.BackColor;

                        if (cuadrados[j1.Posicion].Bottom) groupBox3.BackColor = Color.DarkGray;
                        else groupBox3.BackColor = this.BackColor;
                        break;
                    default:
                        break;
                }
            }
        }

        private int derecha(short direccion)
        {
            direccion++;
            if (direccion > 3)
            {
                direccion = 0;
            }
            return direccion;
        }

        private int izquierda(short direccion)
        {
            direccion--;
            if (direccion < 0)
            {
                direccion = 3;
            }
            return direccion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                if (listo)
                {
                    if(e.KeyCode != Keys.A && e.KeyCode != Keys.D && e.KeyCode != Keys.W)
                    {
                        j1.Mover(e.KeyCode, cuadrados);
                        pintar();
                    }
                    else
                    {
                        j1.moverprimerapersona(e.KeyCode,cuadrados);
                        pintar();
                    }
                }
                if (cuadrados.ToList()[cuadrados.Count - 2].Key == j1.Posicion)
                {
                    MessageBox.Show("Conseguido!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (cuadrados.ToList()[cuadrados.Count - 2].Key == j1.Posicion)
            //{
            //    var selection = from i in j1.Posicicionesanteriores
            //                    where (j1.Posicicionesanteriores.Count(n => n == i) == 1)
            //                    select i;

            //    MessageBox.Show($"MD = {selection.ToList().Count()}");
            //}
            //else j1.movermanoderecha(cuadrados);

            while (cuadrados.ToList()[cuadrados.Count - 2].Key != j1.Posicion)
            {
                j1.movermanoderecha(cuadrados);
                //System.Threading.Thread.Sleep(30);
                pintar();
            }

            if (cuadrados.ToList()[cuadrados.Count - 2].Key == j1.Posicion)
            {
                var selection = from i in j1.Posicicionesanteriores
                                where (j1.Posicicionesanteriores.Count(n => n == i) == 1)
                                select i;

                MessageBox.Show($"MD = {selection.ToList().Count()}","Conseguido!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cuadrados.All(c =>
            {
                c.Value.Bottom = true;
                c.Value.Top = true;
                c.Value.Left = true;
                c.Value.Right = true;
                c.Value.Visitado = false;
                return true;
            });
            creadorlaberinto creador = new creadorlaberinto(cuadrados);
            creador.crear(ref cuadrados);
            dibujado = false;
            lienzo.Clear(this.BackColor);
            j1.Dispose();
            j1 = new personaje(cuadrados);
            pintar();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name)
            {
                case "pictureBox1":
                    j1.moverprimerapersona(Keys.A, cuadrados);
                    break;
                case "pictureBox2":
                    j1.moverprimerapersona(Keys.W, cuadrados);
                    break;
                case "pictureBox3":
                    j1.moverprimerapersona(Keys.D, cuadrados);
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ajustes();

            cuadrados.Clear();
            var x = posx;
            var y = posy;
            var cordenadas = new Point(1, 1);
            for (var i = 1; i < Properties.Settings.Default.tamano + 1; i++)
            {
                cuadrados.Add(cordenadas, new cuadradoplus(Properties.Settings.Default.tamanocasillas, x, y));
                if (i % (Math.Floor(Math.Sqrt(Properties.Settings.Default.tamano))) == 0)
                {
                    y += Properties.Settings.Default.tamanocasillas;
                    x = posx;
                    cordenadas.Y++;
                    cordenadas.X = 1;
                }
                else
                {
                    x += Properties.Settings.Default.tamanocasillas;
                    cordenadas.X++;
                }

            }

            creadorlaberinto creador = new creadorlaberinto(cuadrados);
            creador.crear(ref cuadrados);
            dibujado = false;
            lienzo.Clear(this.BackColor);
            j1.Dispose();
            j1 = new personaje(cuadrados);
            pintar();

        }

        private static void ajustes()
        {
            var casillas = Properties.Settings.Default.tamano.ToString();
            ShowInputDialog(ref casillas, "Cantidad");
            Properties.Settings.Default.tamano = Int32.Parse(casillas);
            Properties.Settings.Default.Save();

            var tamanocasillas = Properties.Settings.Default.tamanocasillas.ToString();
            ShowInputDialog(ref tamanocasillas, "Tamaño");
            Properties.Settings.Default.tamanocasillas = Int32.Parse(tamanocasillas);
            Properties.Settings.Default.Save();
        }

        private static DialogResult ShowInputDialog(ref string input, string aintroducir)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = aintroducir;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
    }
}
