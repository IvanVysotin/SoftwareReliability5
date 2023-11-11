using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareReliability5
{
    public partial class Form1 : Form
    {
        private double _stepsCount = 0;
        private double _pA6 = 0;
        private double _pA5 = 0;
        private double _pA4 = 0;
        private double _pA3 = 0;
        private double _pA2 = 0;
        private double _pA1 = 0;
        private double _pAOverall = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double successCount = 0;

            // Значения вероятностей из текстбоксов
            double probabiltyFirst = Convert.ToDouble(textBox1.Text);
            double probabiltySecond = Convert.ToDouble(textBox2.Text);
            double probabiltyThird = Convert.ToDouble(textBox3.Text);
            double probabiltyForth = Convert.ToDouble(textBox4.Text);
            double probabiltyFifth = Convert.ToDouble(textBox5.Text);
            double probabiltySixth = Convert.ToDouble(textBox6.Text);
            double probabiltySeventh = Convert.ToDouble(textBox7.Text);
            double probabiltyEighth = Convert.ToDouble(textBox8.Text);
            _stepsCount = Convert.ToDouble(tb_stepsCount.Text);

            // Подсчет вероятностей
            _pA6 = (1 - (1 - probabiltyFirst) * (1 - probabiltySecond)) * (1 - (1 - probabiltyForth) * (1 - probabiltyFifth)) * (1 - (1 - probabiltySeventh) * (1 - probabiltyEighth));
            _pA5 = (1 - (1 - probabiltyFirst) * (1 - probabiltySecond)) * ((1 - (1 - probabiltyForth * probabiltySeventh) * (1 - probabiltyFifth * probabiltyEighth)));
            _pA4 = ((1 - (1 - probabiltyFirst * probabiltyForth) * (1 - probabiltySecond * probabiltyFifth))) * (1 - (1 - probabiltySeventh) * (1 - probabiltyEighth));
            _pA3 = (1 - (1 - probabiltyFirst * probabiltyForth * probabiltySeventh) * (1 - probabiltySecond * probabiltyFifth * probabiltyEighth));
            _pA2 = probabiltySixth * _pA6 + (1 - probabiltySixth) * _pA5;
            _pA1 = probabiltySixth * _pA4 + (1 - probabiltySixth) * _pA3;
            _pAOverall = probabiltyThird * _pA2 + (1 - probabiltyThird) * _pA1;

            // Рандомы для каждого блока
            Random rnd = new Random();
            Random rnd1 = new Random(rnd.Next(1000));
            Random rnd2 = new Random(rnd.Next(1000));
            Random rnd3 = new Random(rnd.Next(1000));
            Random rnd4 = new Random(rnd.Next(1000));
            Random rnd5 = new Random(rnd.Next(1000));
            Random rnd6 = new Random(rnd.Next(1000));
            Random rnd7 = new Random(rnd.Next(1000));
            Random rnd8 = new Random(rnd.Next(1000));

            for (int i = 0; i < _stepsCount; i++)
            {
                bool value1 = rnd1.NextDouble() <= probabiltyFirst; // Так не надо. Лучше вынести за цикл и присвоить переменным
                bool value2 = rnd2.NextDouble() <= probabiltySecond;
                bool value3 = rnd3.NextDouble() <= probabiltyThird;
                bool value4 = rnd4.NextDouble() <= probabiltyForth;
                bool value5 = rnd5.NextDouble() <= probabiltyFifth;
                bool value6 = rnd6.NextDouble() <= probabiltySixth;
                bool value7 = rnd7.NextDouble() <= probabiltySeventh;
                bool value8 = rnd8.NextDouble() <= probabiltyEighth;

                bool pathFirst = value1 && value4 && value7;
                bool pathSecond = value1 && value4 && value6 && value8;
                bool pathThird = value1 && value3 && value5 && value8;
                bool pathForth = value1 && value3 && value5 && value6 && value7;
                bool pathFifth = value2 && value5 && value8;
                bool pathSixth = value2 && value5 && value6 && value7;
                bool pathSeventh = value2 && value3 && value4 && value7;
                bool pathEigth = value2 && value3 && value4 && value6 && value8;
                bool pathOverall = pathFirst || pathSecond || pathThird || pathForth || pathFifth || pathSixth || pathSeventh || pathEigth;

                if (pathOverall) successCount++;
            }
            lb_frequency.Text = (successCount / _stepsCount).ToString();
            lb_probability.Text = _pAOverall.ToString();
        }
    }
}
