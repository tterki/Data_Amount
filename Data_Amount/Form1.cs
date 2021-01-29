using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Data_Amount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Series.Remove(series1);
        }
        //=====================================================================================

        private void check_Click(object sender, EventArgs e)
        {
            if (tbPath.Text == "")
            {
                toolTip.Show("Data source", tbPath, 20, 10, 3000);
            }

            else
            {
                if (radioButton1.Checked)
                {
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    // creating txt files :
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    string[] stations = new string[54];
                    stations[0] = "ABKD"; stations[1] = "ABSD"; stations[2] = "ABZH"; stations[3] = "ADCK"; stations[4] = "ADJF";
                    stations[5] = "AFDJ"; stations[6] = "AFRS"; stations[7] = "AKDR"; stations[8] = "AKET"; stations[9] = "ATKJ";

                    stations[10] = "CABS"; stations[11] = "CAEH"; stations[12] = "CAEK"; stations[13] = "CBBR"; stations[14] = "CBCK";
                    stations[15] = "CBEJ"; stations[16] = "CCOL"; stations[17] = "CFKZ"; stations[18] = "CJIJ"; stations[19] = "CJSR";
                    stations[20] = "CKAL"; stations[21] = "CKHR"; stations[22] = "CKTS"; stations[23] = "CMAR"; stations[24] = "CNAJ";
                    stations[25] = "CNGR"; stations[26] = "CRHA"; stations[27] = "CSVB"; stations[28] = "CTCH";

                    stations[29] = "EADB"; stations[30] = "EARB"; stations[31] = "EASA"; stations[32] = "EBGR"; stations[33] = "EBNH";
                    stations[34] = "EECH"; stations[35] = "EKMS"; stations[36] = "EMHD"; stations[37] = "ESKN"; stations[38] = "ETJN";
                    stations[39] = "EZEA";

                    stations[40] = "OAIN"; stations[41] = "OAIR"; stations[42] = "OASF"; stations[43] = "OBBL"; stations[44] = "OBHM";
                    stations[45] = "ODJA"; stations[46] = "OJGS"; stations[47] = "OKHE"; stations[48] = "OKHM"; stations[49] = "OLHC";
                    stations[50] = "ORAN"; stations[51] = "OSDA"; stations[52] = "OTSS";

                    stations[53] = "TTAM";
                    //----------------------
                    string MyPath = tbPath.Text;

                    progress.Maximum = stations.Length;
                    progress.Value = 0;

                    for (int a = 0; a < stations.Length; a++)
                    {
                        label1.Text = stations[a];
                        progress.Value++;
                        //Console.WriteLine(stations[a]);

                        // Create Ok\ folder :
                        //-------------------
                        if (!Directory.Exists(MyPath + @"\MDB_Statistiques\"))
                        {
                            Directory.CreateDirectory(MyPath + @"\MDB_Statistiques\");
                        }
                        //-------------------
                        // Create files :
                        //--------------
                        if (!File.Exists(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt"))
                        {
                            FileInfo fichier = new FileInfo(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt");
                            FileStream stream = fichier.Create();
                            stream.Close();

                            //File.Create(@"Ok\" + stations[a] + ".txt");
                            //Thread.Sleep(500);
                        }
                        File.WriteAllText(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt", String.Empty);

                        string Day;
                        for (int i = 1; i <= 366; i++)
                        {
                            // GPS date on 3 digits :
                            //----------------------
                            if (i < 10)
                            {
                                Day = "00" + i;
                            }
                            else if (i < 100)
                            {
                                Day = "0" + i;
                            }
                            else
                            {
                                Day = i.ToString();
                            }

                            string myDir = MyPath + "\\" + stations[a] + "\\" + Day;

                            long size = 0;
                            //Thread.Sleep(500);
                            if (Directory.Exists(myDir))
                            {
                                size = 0;

                                foreach (FileInfo file in new DirectoryInfo(myDir).GetFiles())
                                {
                                    size += file.Length;
                                }
                                //Console.WriteLine(size);
                                //Console.WriteLine(Directory.GetCurrentDirectory() + "\\" + stations[a] + ".txt");
                                File.AppendAllText(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt", size.ToString() + Environment.NewLine);
                            }
                            else
                            {
                                size = 0;
                                File.AppendAllText(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt", size.ToString() + Environment.NewLine);
                                //Console.WriteLine(size);
                            }
                        }
                    }
                    //MessageBox.Show("size checking done


                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    // Plotting :
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    progress.Value = 0;
                    this.chart1.Visible = false;
                    chart1.Height = 1080;
                    chart1.Width = 1920;

                    for (int a = 0; a < stations.Length; a++)
                    {
                        progress.Value++;
                        //chart1.ChartAreas.Clear();
                        this.chart1.Titles.Clear();
                        this.chart1.Series.Clear();
                        //this.chart1.Series Font(Font.Name, 5, FontStyle.Regular)

                        if (File.Exists(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt"))
                        {
                            string[] myPointsByte = new string[366];
                            myPointsByte = File.ReadAllLines(MyPath + @"\MDB_Statistiques\" + stations[a] + ".txt");

                            Series series = this.chart1.Series.Add(stations[a]);
                            
                            //int count = 1;
                            //string[] xval = new string[366];
                            //for (int i = 0; i < xval.Length; i++)
                            //{
                            //    xval[i] = i.ToString();
                            //}

                            //foreach (CustomLabel lbl in chart1.ChartAreas[0].AxisX.CustomLabels)
                            //{
                            //    lbl.Text = xval[count];
                            //    count++;
                            //}

                            // Axes :
                            //------
                            //Axis myX = new Axis();
                            //chart1.ChartAreas[0].AxisX.Maximum = 380;
                            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                            chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                            chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 5;
                            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                            chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
                            chart1.ChartAreas[0].AxisX.MinorTickMark.Interval = 30;
                            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 30;
                            chart1.ChartAreas[0].AxisX.Title = "Date GPS (jour de l'an)";
                            chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);

                            // chart1.ChartAreas[0].AxisX.Label = (Double.Parse(chart1.ChartAreas[0].AxisX.) + 1).ToString();

                            //chart1.ChartAreas[0].AxisY.Maximum = 2;
                            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                            chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                            chart1.ChartAreas[0].AxisY.MinorGrid.Interval = 0.2;
                            chart1.ChartAreas[0].AxisY.Title = "Taille (Mo)";
                            chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);



                            //series.XAxisType = AxisType.Primary;
                            //series.Font = new System.Drawing.Font("Trebuchet MS", 35, FontStyle.Bold);

                            double[] myPointsKiloBytes = new double[366];
                            double[] DAY = new double[366];

                            for (int i = 0; i < 366; i++)
                            {
                                myPointsKiloBytes[i] = Convert.ToDouble(myPointsByte[i]) / (1024 * 1024);
                                DAY[i] = Convert.ToDouble(i+1);

                                MessageBox.Show(DAY[i].ToString());
                                series.ChartType = SeriesChartType.Point;
                                chart1.Series[0].SmartLabelStyle.Enabled = true;

                                //series.ChartType = SeriesChartType.Bubble;
                                //series.ChartType = SeriesChartType.Column;
                                chart1.Series[0].MarkerSize = 8;
                                series.Points.AddXY(DAY[i], myPointsKiloBytes[i]);
                                //series.Points.Add(myPointsKiloBytes[i]);
                            }
                            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                            chart1.Series[0].MarkerColor = Color.Blue;
                            // Set palette.
                            // this.chart1.Palette = ChartColorPalette.Grayscale;

                            // Set title.
                            Title title = this.chart1.Titles.Add("Station " + stations[a]);
                            title.Font = new System.Drawing.Font("Cambria", 20, FontStyle.Regular);
                            title.ForeColor = System.Drawing.Color.FromArgb(0, 0, 205);
                            //this.chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Trebuchet MS", 35F, System.Drawing.FontStyle.Bold);
                        }
                        this.chart1.SaveImage(MyPath + @"\MDB_Statistiques\" + stations[a] + ".png", ChartImageFormat.Png);
                    }
                    MessageBox.Show("Done !");
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                else //(radio2 checked)
                {
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    // creating txt files :
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    string[] stations = new string[54];
                    stations[0] = "ABKD"; stations[1] = "ABSD"; stations[2] = "ABZH"; stations[3] = "ADCK"; stations[4] = "ADJF";
                    stations[5] = "AFDJ"; stations[6] = "AFRS"; stations[7] = "AKDR"; stations[8] = "AKET"; stations[9] = "ATKJ";

                    stations[10] = "CABS"; stations[11] = "CAEH"; stations[12] = "CAEK"; stations[13] = "CBBR"; stations[14] = "CBCK";
                    stations[15] = "CBEJ"; stations[16] = "CCOL"; stations[17] = "CFKZ"; stations[18] = "CJIJ"; stations[19] = "CJSR";
                    stations[20] = "CKAL"; stations[21] = "CKHR"; stations[22] = "CKTS"; stations[23] = "CMAR"; stations[24] = "CNAJ";
                    stations[25] = "CNGR"; stations[26] = "CRHA"; stations[27] = "CSVB"; stations[28] = "CTCH";

                    stations[29] = "EADB"; stations[30] = "EARB"; stations[31] = "EASA"; stations[32] = "EBGR"; stations[33] = "EBNH";
                    stations[34] = "EECH"; stations[35] = "EKMS"; stations[36] = "EMHD"; stations[37] = "ESKN"; stations[38] = "ETJN";
                    stations[39] = "EZEA";

                    stations[40] = "OAIN"; stations[41] = "OAIR"; stations[42] = "OASF"; stations[43] = "OBBL"; stations[44] = "OBHM";
                    stations[45] = "ODJA"; stations[46] = "OJGS"; stations[47] = "OKHE"; stations[48] = "OKHM"; stations[49] = "OLHC";
                    stations[50] = "ORAN"; stations[51] = "OSDA"; stations[52] = "OTSS";

                    stations[53] = "TTAM";

                    string MyPath = tbPath.Text;

                    progress.Maximum = stations.Length;
                    progress.Value = 0;

                    //----------------------
                    // Create Ok\ folder :
                    //-------------------
                    if (!Directory.Exists(MyPath + @"\Rinex_Statistiques"))
                    {
                        Directory.CreateDirectory(MyPath + @"\Rinex_Statistiques");
                        //MessageBox.Show("création de " + MyPath + @"\Rinex_Statistiques");
                    }

                    //-------------------
                    string Day;

                    for (int a = 0; a < stations.Length; a++)
                    {
                        progress.Value++;
                        //-------------------
                        // Create files :
                        //--------------
                        if (!File.Exists(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt"))
                        {
                            FileInfo fichier = new FileInfo(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt");
                            FileStream stream = fichier.Create();
                            stream.Close();

                            //File.Create(@"Ok\" + stations[a] + ".txt");
                            //Thread.Sleep(500);
                        }

                        File.WriteAllText(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt", String.Empty);

                        for (int i = 1; i <= 366; i++)
                        {

                            // GPS date on 3 digits :
                            //----------------------
                            if (i < 10)
                            {
                                Day = "00" + i;
                            }
                            else if (i < 100)
                            {
                                Day = "0" + i;
                            }
                            else
                            {
                                Day = i.ToString();
                            }

                            string myDir = MyPath + "\\" + Day;
                            string myFile = stations[a] + Day;
                            // MessageBox.Show(myFile);


                            // calculate size :
                            //-----------------
                            long size = 0;
                            if (Directory.Exists(myDir))
                            {
                                size = 0;

                                // FileInfo file = new FileInfo(myDir);

                                foreach (FileInfo file in new DirectoryInfo(myDir).GetFiles(stations[a] + Day + "0*")) //stations[a] + Day+"0.***.***"
                                {
                                    //if ((stations[a] + Day + "*.*"))
                                    //{
                                    //MessageBox.Show("ca commence par " + stations[a]);
                                    size += file.Length;

                                    //MessageBox.Show(MyPath + @"\Ok\" + stations[a] + ".txt...." + size.ToString() + Environment.NewLine);
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("ca ne commence pas par " + stations[a]);
                                    //    size = 0;
                                    //    //MessageBox.Show(stations[a]+Day+" => size : "+size);
                                    //    File.AppendAllText(MyPath + @"\Ok\" + stations[a] + ".txt", size.ToString() + Environment.NewLine);
                                    //}
                                }
                                File.AppendAllText(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt", size.ToString() + Environment.NewLine);
                            }
                            else
                            {
                                size = 0;
                                //MessageBox.Show(stations[a]+Day+" => size : "+size);
                                File.AppendAllText(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt", size.ToString() + Environment.NewLine);
                                //Console.WriteLine(size);
                            }
                        }
                    }




                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    // Plotting :
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    progress.Value = 0;
                    this.chart1.Visible = false;
                    chart1.Height = 1080;
                    chart1.Width = 1920;

                    for (int a = 0; a < stations.Length; a++)
                    {
                        progress.Value++;
                        //chart1.ChartAreas.Clear();
                        this.chart1.Titles.Clear();
                        this.chart1.Series.Clear();
                        //this.chart1.Series Font(Font.Name, 5, FontStyle.Regular)

                        if (File.Exists(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt"))
                        {
                            //MessageBox.Show(@"D:\_Work\Data_Amount\Test\" + stations[a] + ".txt existe");
                            string[] myPointsByte = new string[366];
                            myPointsByte = File.ReadAllLines(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".txt");

                            Series series = this.chart1.Series.Add(stations[a]);

                            // Axes :
                            //------
                            //Axis myX = new Axis();
                            //chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("your required font",20, FontStyle.Italic);
                            chart1.ChartAreas[0].AxisX.Maximum = 380;
                            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                            chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                            chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 5;
                            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                            chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
                            chart1.ChartAreas[0].AxisX.MinorTickMark.Interval = 30;
                            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 30;
                            chart1.ChartAreas[0].AxisX.Title = "Date GPS (jour de l'an)";
                            chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);
                            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                            // chart1.ChartAreas[0].AxisX.Label = (Double.Parse(chart1.ChartAreas[0].AxisX.) + 1).ToString();

                            int count = 1;
                            string[] xval = new string[366];
                            for (int i = 0; i < xval.Length; i++)
                            {
                                xval[i] = i.ToString();
                            }

                            foreach (CustomLabel lbl in chart1.ChartAreas[0].AxisX.CustomLabels)
                            {
                                lbl.Text = xval[count];
                                count++;
                            }

                            //chart1.ChartAreas[0].AxisY.Maximum = 2;
                            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                            chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                            chart1.ChartAreas[0].AxisY.MinorGrid.Interval = 0.2;
                            chart1.ChartAreas[0].AxisY.Title = "Taille (Mo)";
                            chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);



                            //series.XAxisType = AxisType.Primary;
                            //series.Font = new System.Drawing.Font("Trebuchet MS", 35, FontStyle.Bold);

                            double[] myPointsKiloBytes = new double[366];
                            for (int i = 0; i < 366; i++)
                            {
                                myPointsKiloBytes[i] = Convert.ToDouble(myPointsByte[i]) / (1024 * 1024);
                                series.ChartType = SeriesChartType.Point;
                                chart1.Series[0].SmartLabelStyle.Enabled = true;

                                //series.ChartType = SeriesChartType.Bubble;
                                //series.ChartType = SeriesChartType.Column;
                                chart1.Series[0].MarkerSize = 8;
                                series.Points.Add(myPointsKiloBytes[i]);
                            }
                            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                            chart1.Series[0].MarkerColor = Color.Blue;
                            // Set palette.
                            // this.chart1.Palette = ChartColorPalette.Grayscale;

                            // Set title.
                            Title title = this.chart1.Titles.Add("Station " + stations[a]);
                            title.Font = new System.Drawing.Font("Cambria", 20, FontStyle.Regular);
                            title.ForeColor = System.Drawing.Color.FromArgb(0, 0, 205);
                            //this.chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Trebuchet MS", 35F, System.Drawing.FontStyle.Bold);
                        }
                        this.chart1.SaveImage(MyPath + @"\Rinex_Statistiques\" + stations[a] + ".png", ChartImageFormat.Png);
                    }
                    MessageBox.Show("Done !");
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_Checked(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
        }

        private void radioButton2_Checked(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = true;
        }
    }
}
