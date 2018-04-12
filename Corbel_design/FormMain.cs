using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Rhino.Geometry;
using Corbel_design.Display_classes;
using Rhino;

namespace Corbel_design
{
    [Guid("890F7AE7-6BEB-4A21-960A-64736819F6D0")]
    public partial class FormMain : UserControl
    {
        private CorbeldesignPlugIn _cdPlugin;
        List<Corbel> corbelList = new List<Corbel>();
        Corbel currentCorbel;
        private CorbelConduit _corbelConduit;

        public FormMain()
        {
            _cdPlugin = CorbeldesignPlugIn.Instance;
            _corbelConduit = new CorbelConduit();
            _corbelConduit.Enabled = true;
            InitializeComponent();
            initializeComponentValues();
            sampleCorbel();
            getFields();
            textBox_bw.Text = "200";
            textBox_ac.Text = "250";
            createConsoleDrawing1();

        }

        private void initializeComponentValues()
        {

            dataGridViewLoadcases.Rows[0].Cells[0].Value = 1;

            comboBoxConcreteStrength.Items.AddRange(new string[]{
                "C20/25",
                "C25/30",
                "C30/37",
                "C35/45",
                "C40/50",
                "C50/60"
            });
            comboBoxConcreteStrength.SelectedItem = comboBoxConcreteStrength.Items[2];
            comboBoxSteelStrength.Items.AddRange(new string[]{
                "B500B"
            });
            comboBoxSteelStrength.SelectedItem = comboBoxSteelStrength.Items[0];

            comboBoxDiam.Items.AddRange(new string[] { "6", "8", "10", "12", "16", "20" });
            comboBoxDiam.SelectedItem = comboBoxDiam.Items[3];


        }

        private void getFields()
        {
            if (currentCorbel.A5 != 0) textBox_a5.Text = (currentCorbel.A5*1000).ToString(); else textBox_a5.Text = "";
            if (currentCorbel.Ac != 0) textBox_ac.Text = (currentCorbel.Ac*1000).ToString(); else textBox_ac.Text = "";
            if (currentCorbel.Hn != 0) textBox_hn.Text = (currentCorbel.Hn*1000).ToString(); else textBox_hn.Text = "";
            if (currentCorbel.Hc != 0) textBox_hc.Text = (currentCorbel.Hc*1000).ToString(); else textBox_hc.Text = "";
            if (currentCorbel.B != 0) textBox_b.Text = (currentCorbel.B*1000).ToString(); else textBox_b.Text = "";
            if (currentCorbel.N != 0) textBox_n.Text = (currentCorbel.N).ToString(); else textBox_n.Text = "";
            if (currentCorbel.Cc != 0) textBox_cc.Text = (currentCorbel.Cc * 1000).ToString(); else textBox_cc.Text = "";
            if (currentCorbel.Gammac != 0) textBox_gammac.Text = (currentCorbel.Gammac).ToString(); else textBox_gammac.Text = "";
            if (currentCorbel.Gammas != 0) textBox_gammas.Text = (currentCorbel.Gammas).ToString(); else textBox_gammas.Text = "";
            if (!String.IsNullOrEmpty(currentCorbel.CStrengthClass)) comboBoxConcreteStrength.SelectedItem = currentCorbel.CStrengthClass;
            if (currentCorbel.Fii1 != 0) comboBoxDiam.Text = (currentCorbel.Fii1*1000).ToString();
            if (!String.IsNullOrEmpty(currentCorbel.SStrengthClass)) comboBoxSteelStrength.SelectedItem = currentCorbel.SStrengthClass;
        }

        private void setCorbelValues()
        {
            if (!(currentCorbel == null))
            {
                if (!Double.IsNaN(currentCorbel.A5) && textBox_a5.Text != (currentCorbel.A5 * 1000).ToString() && textBox_a5.Text != "")
                    currentCorbel.A5 = convertToDouble(textBox_a5.Text) * 0.001;
                if (!Double.IsNaN(currentCorbel.Ac) && textBox_ac.Text != (currentCorbel.Ac * 1000).ToString() && textBox_ac.Text != "")
                    currentCorbel.Ac = convertToDouble(textBox_ac.Text) * 0.001;
                if (!Double.IsNaN(currentCorbel.Hn) && textBox_hn.Text != (currentCorbel.Hn * 1000).ToString() && textBox_hn.Text != "")
                    currentCorbel.Hn = convertToDouble(textBox_hn.Text) * 0.001;
                if (!Double.IsNaN(currentCorbel.Hc) && textBox_hc.Text != (currentCorbel.Hc * 1000).ToString() && textBox_hc.Text != "")
                    currentCorbel.Hc = convertToDouble(textBox_hc.Text) * 0.001;
                if (!Double.IsNaN(currentCorbel.B) && textBox_b.Text != (currentCorbel.B * 1000).ToString() && textBox_b.Text != "")
                    currentCorbel.B = convertToDouble(textBox_b.Text) * 0.001;
                if (!Double.IsNaN(currentCorbel.N) && textBox_n.Text != (currentCorbel.N).ToString() && textBox_n.Text != "")
                    currentCorbel.N = convertToDouble(textBox_n.Text);
                if (!Double.IsNaN(currentCorbel.Cc) && textBox_cc.Text != (currentCorbel.Cc * 1000).ToString() && textBox_cc.Text != "")
                    currentCorbel.Cc = convertToDouble(textBox_cc.Text);
                if (!Double.IsNaN(currentCorbel.Gammac) && textBox_cc.Text != (currentCorbel.Gammac).ToString() && textBox_gammac.Text != "")
                    currentCorbel.Gammac = convertToDouble(textBox_gammac.Text);
                if (!Double.IsNaN(currentCorbel.Gammas) && textBox_cc.Text != (currentCorbel.Gammas).ToString() && textBox_gammas.Text != "")
                    currentCorbel.Gammas = convertToDouble(textBox_gammas.Text);
                if (currentCorbel.CStrengthClass != (string)comboBoxConcreteStrength.SelectedItem)
                    currentCorbel.CStrengthClass = (string)comboBoxConcreteStrength.SelectedItem;
                if (currentCorbel.SStrengthClass != (string)comboBoxSteelStrength.SelectedItem)
                    currentCorbel.SStrengthClass = (string)comboBoxSteelStrength.SelectedItem;
                if ((currentCorbel.Fii1*1000).ToString() != (string)comboBoxDiam.SelectedItem)
                    currentCorbel.Fii1 = Double.Parse((string)comboBoxDiam.SelectedItem)*0.001;
                upgradeChart();
                checkAllLoadCases();
            }

        }

        private void sampleCorbel()
        {
            Corbel Corbel1 = new Corbel("testi");

            Corbel1.Fck = 30 * Math.Pow(10, 6);
            Corbel1.Fyk = 500 * Math.Pow(10, 6);
            Corbel1.Hc = 0.59;
            Corbel1.B = 0.28;
            Corbel1.Cc = 0.035;
            Corbel1.Ac = 0.25;
            Corbel1.A5 = 0.25;
            Corbel1.Hn = 0.01;
            Corbel1.Fcd = Corbel1.Fck / Corbel1.Gammac*Corbel1.Acc;
            Corbel1.Fyd = Corbel1.Fyk / Corbel1.Gammas;
            Corbel1.Fii1 = 0.012;
            Corbel1.N = 6;


            Corbel1.createGraph();

            
            

            corbelList.Add(Corbel1);
            listBoxCorbels.Items.Add(Corbel1.Name);
            currentCorbel = Corbel1;
            //currentCorbel.loadCases.Add(new LoadCase(10000, 100000, currentCorbel));
            upgradeChart();


            Corbel corbel2 = new Corbel("testi2");
            corbelList.Add(corbel2);
            listBoxCorbels.Items.Add(corbel2.Name);
        }

        private void upgradeChart()
        {
            chartStrength.Series["Strength"].Points.Clear();
            if (currentCorbel.Resistance.Count != 0)
            {
                foreach (Tuple<double, double> value in currentCorbel.Resistance)
                {
                    chartStrength.Series["Strength"].Points.AddXY(value.Item1 * 0.001, value.Item2 * 0.001);
                }

                double maxValue = currentCorbel.Resistance.Max(x => x.Item1);

                Tuple<double, double> intAndMax = createInterval(maxValue * 0.001);
                chartStrength.ChartAreas["ChartArea1"].AxisX.Interval = intAndMax.Item1;
                chartStrength.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = intAndMax.Item1 / 5;
                chartStrength.ChartAreas["ChartArea1"].AxisX.Maximum = intAndMax.Item2;

                intAndMax = createInterval(currentCorbel.Resistance[currentCorbel.Resistance.Count - 1].Item2 * 0.001);
                chartStrength.ChartAreas["ChartArea1"].AxisY.Interval = intAndMax.Item1;
                chartStrength.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval = intAndMax.Item1 / 5;
                chartStrength.ChartAreas["ChartArea1"].AxisY.Maximum = intAndMax.Item2;
            }



        }


        //Create new corbel
        private void buttonNewCorbel_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Give the name of the corbel", "New Corbel");
            Corbel newCorbel = new Corbel(name);
            corbelList.Add(newCorbel);
            listBoxCorbels.Items.Add(newCorbel.Name);
        }

        private void setDefValues()
        {
            textBox_a5.Text = "";
            textBox_bw.Text = "";
            textBox_ac.Text = "";
            textBox_hn.Text = "";
            textBox_hc.Text = "";
            textBox_b.Text = "";
            textBox_n.Text = "";
            textBox_cc.Text = "";
            textBox_gammac.Text = "";
            textBox_gammas.Text = "";
            
        }


        private void buttonOpenDetailedResults_Click(object sender, EventArgs e)
        {
            FormDetailedResults dResults = new FormDetailedResults();
            dResults.fillForm(corbelList[0],corbelList[0].loadCases[0]);
            dResults.Show();

        }

        private void comboBoxSteelStrength_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCorbelValues();
        }

        private void createConsoleDrawing1()
        {
            Curve curve = createOutline();
            Brep brep = Extrusion.CreateExtrusion(curve,
                new Vector3d(0, 0, convertToDouble(textBox_b.Text))).ToBrep()
                .CapPlanarHoles(RhinoDoc.ActiveDoc.ModelAbsoluteTolerance);
            RhinoDoc.ActiveDoc.Objects.AddBrep(brep);


            _corbelConduit.reinforcements.Clear();
            _corbelConduit.corbel = brep;
            Brep reinf = createMainReinforcement();
            double width = convertToDouble(textBox_b.Text);
            double distance = (width - convertToDouble(textBox_cc.Text)*2 -2*32) / (convertToDouble(textBox_n.Text)-1);

            Brep first = (Brep)reinf.DuplicateShallow();
            first.Translate(new Vector3d(0, 0, convertToDouble(textBox_cc.Text) + 32));
            
            _corbelConduit.reinforcements.Add(first);

            for (int i = 0; i < convertToDouble(textBox_n.Text); i++)
			{

                Brep copy = (Brep)first.DuplicateShallow();
                copy.Translate(new Vector3d(0,0,i*distance));
                _corbelConduit.reinforcements.Add(copy);
			}

            RhinoDoc.ActiveDoc.Views.Redraw();
        }

        private Brep createMainReinforcement()
        {
            List<Point3d> points = new List<Point3d>();

            points.Add(new Point3d(convertToDouble(textBox_cc.Text) + 10, 250, 0));
            points.Add(new Point3d(convertToDouble(textBox_cc.Text) + 10, 250 + convertToDouble(textBox_hc.Text), 0));
            points.Add(new Point3d(400.0 + convertToDouble(textBox_ac.Text) + convertToDouble(textBox_a5.Text) / 2 -
                convertToDouble(textBox_cc.Text) , 250 + convertToDouble(textBox_hc.Text), 0));
            points.Add(new Point3d(400.0 + convertToDouble(textBox_ac.Text) + convertToDouble(textBox_a5.Text) / 2 -
                convertToDouble(textBox_cc.Text), 250 + convertToDouble(textBox_cc.Text), 0));
            points.Add(new Point3d(convertToDouble(textBox_cc.Text) + 10, 250 + convertToDouble(textBox_cc.Text),
                -convertToDouble((string)comboBoxDiam.SelectedItem)));

            double radius = getBendingRadius();

            List<Curve> curves = new List<Curve>();
            for (int i = 0; i < points.Count-1; i++)
			{
                curves.Add(new Line(points[i],points[i+1]).ToNurbsCurve());
			}
            Curve joinedCurve = new Line().ToNurbsCurve();
            for (int i = 0; i < curves.Count-1; i++)
			{
                if (i == 0)
                    joinedCurve = Curve.CreateFilletCurves(curves[i],curves[i].PointAtNormalizedLength(0.9),
                        curves[i+1],curves[i+1].PointAtNormalizedLength(0.1),radius,
                        true,true,false,RhinoDoc.ActiveDoc.PageAbsoluteTolerance,RhinoDoc.ActiveDoc.PageAngleToleranceRadians)[0];
                else
                    joinedCurve = Curve.CreateFilletCurves(joinedCurve,joinedCurve.PointAtNormalizedLength(0.9),
                        curves[i+1],curves[i+1].PointAtNormalizedLength(0.1),radius,
                        true,true,false,RhinoDoc.ActiveDoc.PageAbsoluteTolerance,RhinoDoc.ActiveDoc.PageAngleToleranceRadians)[0];
			}

            return Brep.CreatePipe(joinedCurve, convertToDouble((string)comboBoxDiam.SelectedItem) / 2, false, PipeCapMode.Flat, false,
                RhinoDoc.ActiveDoc.PageAbsoluteTolerance, RhinoDoc.ActiveDoc.PageAngleToleranceRadians)[0];
            
            
            





        }
        //This is from EC2-1-1 finnish national annex 8.3(2) (taken 8.4.2018). We use the bending radius of stirrups
        private double getBendingRadius()
        {
            double diameter = convertToDouble((string)comboBoxDiam.SelectedItem);
            if (diameter <= 16)
                return diameter * 2.75;
            else
                return diameter * 5;

        }
   

        //Checks are all values needed for geometry plotting defined.
        private void updateGeometryChart()
        {
            if (textBox_a5.Text != "" &&
                textBox_bw.Text != "" &&
                textBox_ac.Text != "" &&
                textBox_hn.Text != "" &&
                textBox_hc.Text != "" &&
                textBox_b.Text != "" &&
                textBox_cc.Text != "" &&
                textBox_gammac.Text != "" &&
                textBox_gammas.Text != ""
            )
            {
                createConsoleDrawing1();
            }
        }

        private Curve createOutline()
        {
            List<Point3d> points = new List<Point3d>();

            points.Add(new Point3d(0.0, 0.0, 0.0));
            points.Add(new Point3d(0.0, convertToDouble(textBox_hc.Text) + 500,0.0));
            points.Add(new Point3d(400.0, convertToDouble(textBox_hc.Text) + 500,0.0));
            points.Add(new Point3d(400.0, convertToDouble(textBox_hc.Text) + 250,0.0));
            points.Add(new Point3d(400.0 + convertToDouble(textBox_ac.Text) + convertToDouble(textBox_a5.Text) / 2,
                convertToDouble(textBox_hc.Text) + 250,0.0));
            points.Add(new Point3d(400.0 + convertToDouble(textBox_ac.Text) + convertToDouble(textBox_a5.Text) / 2,
                250.0,0.0));
            points.Add(new Point3d(400.0, 250.0, 0));
            points.Add(new Point3d(400.0, 0.0, 0));
            points.Add(new Point3d(0.0, 0.0, 0));


            Polyline pl = new Polyline(points);
            Curve curve =pl.ToNurbsCurve();
            curve.MakeClosed(RhinoDoc.ActiveDoc.ModelAbsoluteTolerance);
            RhinoDoc.ActiveDoc.Objects.AddCurve(curve);
            return curve;
        }

        private void listBoxCorbels_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentCorbel = corbelList.Find(x => x.Name == (string)listBoxCorbels.SelectedItem);
            getFields();
            //updates the geometry chart if all values are defined
            updateGeometryChart();
            updateGridview();
        }

        private void updateGridview()
        {
            dataGridViewLoadcases.Rows.Clear();
            int i = 0;
            foreach (LoadCase lc in currentCorbel.loadCases)
            {
                dataGridViewLoadcases.Rows.Add();
                dataGridViewLoadcases.Rows[i].Cells[0].Value = i + 1;
                dataGridViewLoadcases.Rows[i].Cells[1].Value = lc.F_Ed*0.001;
                dataGridViewLoadcases.Rows[i].Cells[2].Value = lc.H_Ed*0.001;
                dataGridViewLoadcases.Rows[i].Cells[3].Value = lc.KA_max;
                i++;
            }

        }


        //Converts string into double. if cannot convert then returns an error
        private double convertToDouble(string str)
        {
            try
            {
                return Convert.ToDouble(str);
            }
            catch (FormatException)
            {
                MessageBox.Show(String.Format("'{0}' is not a number",str), "Error", MessageBoxButtons.OK);
            }
            catch (OverflowException)
            {
                MessageBox.Show(String.Format("'{0}' too big or small number",str), "Error", MessageBoxButtons.OK);
            }
            return -10000000;
        }
        //TODO not implemented

        private void updateCorbelAndChart(object sender, EventArgs e)
        {
            setCorbelValues();
            updateGeometryChart();
        }


        private void updateCorbel(object sender, EventArgs e)
        {
            setCorbelValues();
        }

        //Checks through all the load cases. Did some of the values in the datagridview change to different
        //value compared to the value in the current corbel. If did, then make the changes into the current corbel
        //and update the chart
        private void checkAllLoadCases()
        {
            for (int i = 0;i<dataGridViewLoadcases.Rows.Count;i++)
            {
                if (currentCorbel != null)
                {

                    if (currentCorbel.loadCases.Find(e => e.Name == (i+1).ToString()) != null)
                    {
                        LoadCase lcModify = currentCorbel.loadCases.Find(e => e.Name == (i + 1).ToString());


                        if(chartStrength.Series.IndexOf((i + 1).ToString()) == -1)
                        {
                            createNewPointChart((i + 1).ToString());
                            chartStrength.Series[(i + 1).ToString()].Points.Clear();
                            chartStrength.Series[(i + 1).ToString()].Points.AddXY(lcModify.H_Ed * 0.001,
                                lcModify.F_Ed * 0.001);
                            
                        }

                        //If the loadcase excists change loadcase values and chart values
                        if (((string)dataGridViewLoadcases.Rows[i].Cells[1].Value != null &&
                            (string)dataGridViewLoadcases.Rows[i].Cells[2].Value != null))
                        {
                            if ((lcModify.F_Ed * 0.001).ToString() != (string)dataGridViewLoadcases.Rows[i].Cells[1].Value ||
                                (lcModify.H_Ed * 0.001).ToString() != (string)dataGridViewLoadcases.Rows[i].Cells[2].Value)
                            {
                                lcModify.F_Ed = convertToDouble((string)dataGridViewLoadcases.Rows[i].Cells[1].Value) * 1000;
                                lcModify.H_Ed = convertToDouble((string)dataGridViewLoadcases.Rows[i].Cells[2].Value) * 1000;
                                dataGridViewLoadcases.Rows[i].Cells[3].Value = lcModify.KA_max;

                                chartStrength.Series[(i + 1).ToString()].Points.Clear();
                                chartStrength.Series[(i + 1).ToString()].Points.AddXY(lcModify.H_Ed * 0.001,
                                    lcModify.F_Ed * 0.001);
                                
                            }
                        }
                        else chartStrength.Series.Remove(chartStrength.Series.FindByName((i + 1).ToString()));


                    }

                    //else create new loadcase and add it to the chart
                    else
                    {
                        if ((string)dataGridViewLoadcases.Rows[i].Cells[1].Value != null &&
                            (string)dataGridViewLoadcases.Rows[i].Cells[2].Value != null)
                        {
                            currentCorbel.loadCases.Add(new LoadCase(
                                convertToDouble((string)dataGridViewLoadcases.Rows[i].Cells[1].Value)*1000,
                                convertToDouble((string)dataGridViewLoadcases.Rows[i].Cells[1].Value)*1000,
                                currentCorbel, (i + 1).ToString()));

                            createNewPointChart((i + 1).ToString());

                            chartStrength.Series[(i + 1).ToString()].Points.AddXY(currentCorbel.loadCases[i].H_Ed * 0.001,
                                currentCorbel.loadCases[i].F_Ed * 0.001);
                            dataGridViewLoadcases.Rows[i].Cells[3].Value = currentCorbel.loadCases[i].KA_max;
                        }

                    }
                }

            }


            
        }

        private void createNewPointChart(string name)
        {
            chartStrength.Series.Add(name);
            chartStrength.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chartStrength.Series[name].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chartStrength.Series[name].MarkerSize = 10;
        }


        private void dataGridViewLoadcases_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridViewLoadcases.Rows[dataGridViewLoadcases.Rows.Count - 1].Cells[0].Value =
                dataGridViewLoadcases.Rows.Count;
        }


        private void dataGridViewLoadcases_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            checkAllLoadCases();
        }

        private Tuple<double,double> createInterval(double maxValue)
        {
            double numb = Math.Floor(Math.Log10(maxValue));
            char firstNum = maxValue.ToString()[0];
            char secondNum = maxValue.ToString()[1];
            if (firstNum == '9' || firstNum == '8' || firstNum == '7')
                return new Tuple<double,double>(2 * Math.Pow(10, numb),(Char.GetNumericValue(firstNum) + 1)* Math.Pow(10, numb));
            else if (firstNum == '6' || firstNum == '5' || firstNum == '4')
                return new Tuple<double, double>(Math.Pow(10, numb), (Char.GetNumericValue(firstNum) + 1) * Math.Pow(10, numb));
            else
            {
                double firstTwo = Math.Floor(Double.Parse(Char.ToString(firstNum) + Char.ToString(secondNum)) / 5)*5;
                return new Tuple<double, double>(0.5 * Math.Pow(10, numb), (firstTwo + 5) * Math.Pow(10, numb-1));
            }
                

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_corbelConduit.corbel != null)
                RhinoDoc.ActiveDoc.Objects.AddBrep(_corbelConduit.corbel);
        }


    }
}
